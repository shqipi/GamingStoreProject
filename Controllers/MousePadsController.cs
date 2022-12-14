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
    public class MousePadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MousePadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MousePads
        public async Task<IActionResult> Index(int pg = 1)
        {
            List<MousePad> mousePads = await _context.MousePads.ToListAsync();
            int pageSize = 8;
            if (pg < 1)
                pg = 1;
            int resCount = mousePads.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = mousePads.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            
            return View(data);
        }

        public async Task<IActionResult> Search(string? descriptionString, string? minPrice, string? maxPrice)
        {
            var mousepads = from o in _context.MousePads
                           select o;
            if (String.IsNullOrEmpty(descriptionString) && String.IsNullOrEmpty(minPrice) && String.IsNullOrEmpty(maxPrice))
                return View("Index", await _context.MousePads.ToListAsync());
            else if (!String.IsNullOrEmpty(minPrice) && !String.IsNullOrEmpty(maxPrice) && !String.IsNullOrEmpty(descriptionString))
            {
                mousepads = mousepads.Where(s => s.Description!.Contains(descriptionString) && s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice));
                return View("Index", await mousepads.ToListAsync());
            }
            else if (!String.IsNullOrEmpty(descriptionString))
            {
                mousepads = mousepads.Where(s => s.Description!.Contains(descriptionString));
                return View("Index", await mousepads.ToListAsync());
            }
            else
            {
                mousepads = mousepads.Where(s => s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice));
                return View("Index", await mousepads.ToListAsync());
            }
        }

        // GET: MousePads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mousePad = await _context.MousePads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mousePad == null)
            {
                return NotFound();
            }

            return View(mousePad);
        }

        // GET: MousePads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MousePads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Description,Price")] MousePad mousePad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mousePad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mousePad);
        }

        // GET: MousePads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mousePad = await _context.MousePads.FindAsync(id);
            if (mousePad == null)
            {
                return NotFound();
            }
            return View(mousePad);
        }

        // POST: MousePads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Description,Price")] MousePad mousePad)
        {
            if (id != mousePad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mousePad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MousePadExists(mousePad.Id))
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
            return View(mousePad);
        }

        // GET: MousePads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mousePad = await _context.MousePads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mousePad == null)
            {
                return NotFound();
            }

            return View(mousePad);
        }

        // POST: MousePads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mousePad = await _context.MousePads.FindAsync(id);
            _context.MousePads.Remove(mousePad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MousePadExists(int id)
        {
            return _context.MousePads.Any(e => e.Id == id);
        }
    }
}
