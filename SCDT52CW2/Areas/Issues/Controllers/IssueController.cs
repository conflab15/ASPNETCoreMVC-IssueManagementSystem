using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCDT52CW2Data;
using SCDT52CW2Models;
using SCDT52CW2Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SCDT52CW2.Areas.Issues.Controllers
{
    [Area("Issues")]
    //[Authorize]
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueController(ApplicationDbContext context)
        {
            //Injecting db dependency
            _context = context;
        }

        public IActionResult Index()
        {
            return View(); //No data is returned to this view because the data is displayed using DataTable and the two API calls below.
        }

        //API Calls for the Data Tables in the Issue Area Index
        [HttpGet]
        public IActionResult GetGeneralIssues()
        {
            //Getting General Issues and returning then as JSON data
            var general = _context.Issues.Where(model => model.isTechnical == false);
            return Json(new { data = general });
        }

        [HttpGet]
        public IActionResult GetTechnicalIssues()
        {
            //Getting Technical Issues and returning them as JSON data
            var technical = _context.Issues.Where(model => model.isTechnical);
            return Json(new { data = technical });
        }

        //------ Functionality for Issues ------
        //GET - Create data and send to view to populate fields
        [HttpGet]
        public ActionResult Create()
        {
            //Instantiating a new GeneralIssue Object to set variables beforehand...
            var general = new CreateIssueViewModel(); //Issue View Model consists of a Issue and a SelectList of Assets
            general.Issue = new Issue();
            general.Issue.Date = DateTime.Now; //Creating Data that isn't to be made by the user, which is passed from the controller to the view.
            general.Issue.Author = User.Identity.Name;

            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            general.Issue.UserId = user.Id;
            general.Issue.isClosed = false; //Default false as the ticket is only being opened.

            //Generates a new select list item...
            general.AssetsSelect = _context.Assets.Select(i => new SelectListItem
            {
                Text = i.Desc,
                Value = i.Id.ToString()
            }); //New Select List of Assets


            return View(general); //Passing the View Model to the View...
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIssueViewModel vm, int id, DateTime date, string author, string userid, string desc, bool istechnical, bool isclosed) //Passing ViewModel and the parameters from the form to controller
        {
            if (vm.Issue.Id.ToString() != null)
            {
                //Assigning values to the model...
                vm.Issue.Id = id;
                vm.Issue.Date = date;
                vm.Issue.Author = author;
                vm.Issue.UserId = userid;
                vm.Issue.Desc = desc;
                vm.Issue.isTechnical = istechnical;
                vm.Issue.isClosed = isclosed;
                vm.Issue.AffectedAsset = vm.Issue.AffectedAsset;

                //Add Affected Asset Object ID? Find from list and add?


                await _context.Issues.AddAsync(vm.Issue);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            //Previous Attempt
            //var issue = await _context.Issues.FirstOrDefaultAsync(m => m.Id == id); 

            //Gets the Asset using the ForeignKey Annotation in the model. 
            var issue = await _context.Issues.Include("Asset").FirstOrDefaultAsync(m => m.Id == id);

            if (issue == null)
            {
                return NotFound();
            }

            //Get comments and add to the list, which is iterated over in the view. 
            issue.Actions = await _context.Updates.Where(action => action.IssueID == id).ToListAsync();

            return View(issue);
        }

        //------ Functionality to perform Updates/Actions ------
        [HttpPost]
        public async Task<IActionResult> UpdateTicket(int? id, string notes, bool resolved) //Parameters from the UpdateTicket Form
        {
            //Getting the Issue Model...
            var issueModel = await _context.Issues.FirstOrDefaultAsync(m => m.Id == id);

            if (issueModel == null)
            {
                return NotFound();
            }

            //Creating an update and assigning data to send to the view as auto filled data for the comment... 
            var update = new Update();
            update.Date = DateTime.Now;
            update.Author = User.Identity.Name;
            update.IssueID = issueModel.Id;

            //Data passed back from the view implemented here...
            update.Notes = notes;
            update.isResolved = resolved;

            //Getting the Users ID...
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            update.UserId = user.Id;

            //Add comment to list and save to db...
            _context.Update(update);
            await _context.SaveChangesAsync();

            issueModel.Actions.Add(update);

            _context.Update(issueModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = id }); //Returns the same view so the action is automatically available to view
        }
    }
}
