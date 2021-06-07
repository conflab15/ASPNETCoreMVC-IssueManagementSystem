using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCDT52CW2Data;
using SCDT52CW2Models;

namespace SCDT52CW2.Areas.Asset.Controllers
{
    [Area("Asset")]
    //Authorise Statement here...
    public class AssetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetController(ApplicationDbContext context)
        {
            //Injecting Database dependecy
            _context = context;
        }

        // GET: Asset/Asset
        public IActionResult Index()
        {
            return View(); //No data is returned to this view as it is dealt with using the DataTable and Ajax API call below
        }

        //API Call for the data table to get all of the Assets, as this needs to be returned as a json object
        [HttpGet]
        //Get Asset/Asset/GetAssets
        public IActionResult GetAssets()
        {
            var all = _context.Assets;
            return Json(new { data = all }); //Returns JSON variables to the Datatable in the asset.js script file
        }

        //Upsert Action
        //GET: Asset/Asset/Upsert/ID
        public async Task<IActionResult> Upsert(int? id)
        {
            Assets asset = new Assets();

            if (id == null)
            {
                //If the ID is empty, create a new empty Asset and return it...
                return View(asset);
            }

            asset = await _context.Assets.FindAsync(id.GetValueOrDefault());

            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Upsert(int Id, string AssetID, string Desc, string Location) //Passing Variables from the form
        {
            if (Id == 0)
            {
                //IF the ID = 0, a new asset is created
                Assets newAsset = new Assets();
                newAsset.AssetID = AssetID;
                newAsset.Desc = Desc;
                newAsset.Location = Location;

                await _context.Assets.AddAsync(newAsset);
            }
            else
            {
                //Else, the current Asset is found, and the details are then updated
                var updateAsset = await _context.Assets.FirstOrDefaultAsync(m => m.Id == Id);

                updateAsset.AssetID = AssetID;
                updateAsset.Desc = Desc;
                updateAsset.Location = Location;

                _context.Assets.Update(updateAsset);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //Delete Function which is called by the Sweet Alert Function in the asset.js script
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return Json(new { success = false, message = "Error - Asset Not Found" });
            }

            _context.Assets.Remove(asset);

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Asset Deleted" });
        }
    }



}
