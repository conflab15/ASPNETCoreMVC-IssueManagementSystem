using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCDT52CW2Data;
using SCDT52CW2Models;
using SCDT52CW2Models.ViewModels;
using System;
using System.Collections.Generic;
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
            _context = context;
        }

        //Delete Index below, create API calls for the Data Tables for general and technical issues...

        public IActionResult Index()
        {
            return View();
        }

        //API Calls for the Data Tables in the Issue Area Index
        [HttpGet]
        public IActionResult GetGeneralIssues()
        {
            var general = _context.Issues.Where(model => model.isTechnical == false);
            return Json(new { data = general });
        }

        [HttpGet]
        public IActionResult GetTechnicalIssues()
        {
            var technical = _context.Issues.Where(model => model.isTechnical);
            return Json(new { data = technical });
        }


        //------ Functionality for Issues ------

        //GET - Create data and send to view to populate fields
        [HttpGet]
        public ActionResult Create()
        {
            //Instantiating a new GeneralIssue Object to set variables beforehand...
            var general = new CreateIssueViewModel();
            general.Date = DateTime.Now;
            general.Author = User.Identity.Name;

            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            general.UserId = user.Id;
            general.isClosed = false;

            //Generates a new select list item...
            general.AssetsSelect = _context.Assets.Select(i => new SelectListItem
            {
                Text = i.AssetID,
                Value = i.Id.ToString()
            }); 


            return View(general); //Passing the View Model to the View...
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, DateTime date, string author, string userid, string desc, bool istechnical, bool isclosed) //Need ID of Asset from select list...
        {
            if (id.ToString() != null)
            {
                Issue currentIssue = new Issue();

                currentIssue.Id = id;
                currentIssue.Date = date;
                currentIssue.Author = author;
                currentIssue.UserId = userid;
                currentIssue.Desc = desc;
                currentIssue.isTechnical = istechnical;
                currentIssue.isClosed = isclosed;

                //Add Affected Asset Object ID? Find from list and add?


                await _context.Issues.AddAsync(currentIssue);

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

            var issue = await _context.Issues.FirstOrDefaultAsync(m => m.Id == id);

            if(issue == null)
            {
                return NotFound();
            }

            //Get comments and add to the list...
            issue.Actions = await _context.Updates.Where(action => action.IssueID == id).ToListAsync();

            return View(issue);
        }

        //------ Functionality to perform Updates/Actions ------
        [ActionName("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket(int? id, string notes, bool resolved)
        {
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

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
