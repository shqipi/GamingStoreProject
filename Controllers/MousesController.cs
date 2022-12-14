using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamingStoreProject.Data;
using GamingStoreProject.Models;

namespace GamingStoreProject.Controllers
{
    public class MousesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MousesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mouses
        public async Task<IActionResult> Index(int pg = 1)
        {
            List<Mouse> mice = await _context.Mouses.ToListAsync();
            int pageSize = 8;
            if (pg < 1)
                pg = 1;
            int resCount = mice.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = mice.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            
            return View(data);
        }

        public async Task<IActionResult> Search(string? descriptionString, string? minPrice, string? maxPrice, string wireless)
        {
            var mouses = from m in _context.Mouses
                            select m;
            if (String.IsNullOrEmpty(descriptionString))
                descriptionString = "";
            if (String.IsNullOrEmpty(minPrice))
                minPrice = "0";
            if (String.IsNullOrEmpty(maxPrice))
                maxPrice = "1000000";
            if (wireless.Equals("--"))
            {
                mouses = mouses.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice));
                return View("Index", await mouses.ToListAsync());
            }
            else
            {
                mouses = mouses.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice) && s.Wireless == bool.Parse(wireless));
                return View("Index", await mouses.ToListAsync());
            }
        }
        // GET: Mouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mouse = await _context.Mouses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mouse == null)
            {
                return NotFound();
            }

            return View(mouse);
        }

        // GET: Mouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Mouse mouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mouse);
        }

        // GET: Mouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mouse = await _context.Mouses.FindAsync(id);
            if (mouse == null)
            {
                return NotFound();
            }
            return View(mouse);
        }

        // POST: Mouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Description,Price,Wireless")] Mouse mouse)
        {
            if (id != mouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MouseExists(mouse.Id))
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
            return View(mouse);
        }

        // GET: Mouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mouse = await _context.Mouses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mouse == null)
            {
                return NotFound();
            }

            return View(mouse);
        }

        // POST: Mouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mouse = await _context.Mouses.FindAsync(id);
            _context.Mouses.Remove(mouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MouseExists(int id)
        {
            return _context.Mouses.Any(e => e.Id == id);
        }
    }
}
