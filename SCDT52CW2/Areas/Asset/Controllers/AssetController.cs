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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Assets.ToListAsync());
        }

        // GET: Asset/Asset/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assets = await _context.Assets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assets == null)
            {
                return NotFound();
            }

            return View(assets);
        }

        // GET: Asset/Asset/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Asset/Asset/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssetID,Desc,Location")] Assets assets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assets);
        }

        // GET: Asset/Asset/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assets = await _context.Assets.FindAsync(id);
            if (assets == null)
            {
                return NotFound();
            }
            return View(assets);
        }

        // POST: Asset/Asset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssetID,Desc,Location")] Assets assets)
        {
            if (id != assets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetsExists(assets.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(assets);
        }

        // GET: Asset/Asset/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assets = await _context.Assets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assets == null)
            {
                return NotFound();
            }

            return View(assets);
        }

        // POST: Asset/Asset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assets = await _context.Assets.FindAsync(id);
            _context.Assets.Remove(assets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetsExists(int id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }
    }
}
