using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult Index()
        {
            IssuesModel issues = new IssuesModel
            {
                generalIssue = from e in _context.GeneralIssues
                               where e.UserId == User.Identity.Name
                               select e,

                technicalIssue = from f in _context.TechnicalIssues
                                 where f.UserId == User.Identity.Name
                                 select f
            };

            return View(issues);
        }

        //------ Functionality for General Issues ------

        public IActionResult CreateGeneral()
        {
            GeneralIssueModel issue = new GeneralIssueModel(); //Creating a ViewModel

            //Instantiating a new GeneralIssue Object to set variables beforehand...
            GeneralIssue general = new GeneralIssue();
            general.IssueID = "GEN00" + general.Id;
            general.Date = DateTime.Now.Date;
            general.Time = DateTime.Now.TimeOfDay;
            general.UserId = User.Identity.Name;
            general.isClosed = false;

            //Instantiating a new Update Object to pass this to the object when sending to the model...
            Update update = new Update();
            update.UpdateType = "Init";
            update.Date = DateTime.Now.Date;
            update.Time = DateTime.Now.TimeOfDay;
            update.UserId = User.Identity.Name;
            update.Notes = "Ticket Initialised";
            update.isResolved = false;

            //general.Actions.Add(update);

            issue.issue = general; //Assigning the Issue to the ViewModel
            issue.issueUpdates.Add(update); //Assigning the Updates to the ViewModel

            return View(issue); //Passing the View Model to the View...
        }

        public async Task<IActionResult> UpsertGeneral(int? id)
        {
            GeneralIssue generalIssue = new GeneralIssue();

            if (id == null)
            {
                return View(generalIssue);
            }

            generalIssue = await _context.GeneralIssues.FindAsync(id.GetValueOrDefault());

            if (generalIssue == null)
            {
                return NotFound();
            }

            return View(generalIssue);
        }

        //POST
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpsertGeneral(GeneralIssue generalIssue)
        {
            if (ModelState.IsValid)
            {
                if (generalIssue.Id == 0)
                {
                    await _context.GeneralIssues.AddAsync(generalIssue);
                }
                else
                {
                    _context.GeneralIssues.Update(generalIssue);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(generalIssue);
        }

        //------ Functionality for Technical Issues ------

        public async Task<IActionResult> UpsertTechnical(int? id)
        {
            TechnicalIssue technicalIssue = new TechnicalIssue();

            if (id == null)
            {
                return View(technicalIssue);
            }

            technicalIssue = await _context.TechnicalIssues.FindAsync(id.GetValueOrDefault());

            if (technicalIssue == null)
            {
                return NotFound();
            }

            return View(technicalIssue);
        }

        //POST
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpsertTechnical(TechnicalIssue technicalIssue)
        {
            if (ModelState.IsValid)
            {
                if (technicalIssue.Id == 0)
                {
                    await _context.TechnicalIssues.AddAsync(technicalIssue);
                }
                else
                {
                    _context.TechnicalIssues.Update(technicalIssue);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technicalIssue);
        }
    }
}
