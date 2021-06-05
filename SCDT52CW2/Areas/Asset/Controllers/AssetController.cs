using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCDT52CW2Data;
using SCDT52CW2Models;

namespace SCDT52CW2.Areas.Asset.Controllers
{
    [Area("Asset")]
    public class AssetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Asset/Asset
        public IActionResult Index()
        {
            return View();
        }

        //API Call for the data table to get all of the Assets, as this needs to be returned as a json object...
        [HttpGet]
        public IActionResult GetAssets()
        {
            var all = _context.Assets;
            return Json(new { data = all });
        }

        //Upsert Action
        public async Task<IActionResult> Upsert(int? id)
        {
            Assets asset = new Assets();

            if (id == null)
            {
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
        public async Task<IActionResult> Upsert(Assets asset)
        {
            if (ModelState.IsValid)
            {
                if (asset.Id == 0)
                {
                    await _context.Assets.AddAsync(asset);
                }
                else
                {
                    _context.Assets.Update(asset);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(asset);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
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
