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
    public class KeyboardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KeyboardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Keyboards
        public async Task<IActionResult> Index(int pg = 1)
        {
            List<Keyboard> keyboards = await _context.Keyboards.ToListAsync();
            int pageSize = 8;
            if (pg < 1)
                pg = 1;
            int resCount = keyboards.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = keyboards.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            //return View(graphicCards);
            return View(data);
        }

        public async Task<IActionResult> Search(string? descriptionString, string? minPrice, string? maxPrice, string wireless)
        {
            var keyboards = from k in _context.Keyboards
                           select k;
            if (String.IsNullOrEmpty(descriptionString))
                descriptionString = "";
            if (String.IsNullOrEmpty(minPrice))
                minPrice = "0";
            if (String.IsNullOrEmpty(maxPrice))
                maxPrice = "1000000";
            if (wireless.Equals("--"))
            {
                keyboards = keyboards.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice));
                return View("Index", await keyboards.ToListAsync());
            }
            else
            {
                keyboards = keyboards.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice) && s.Wireless == bool.Parse(wireless));
                return View("Index", await keyboards.ToListAsync());
            }
        }

        // GET: Keyboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyboard = await _context.Keyboards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keyboard == null)
            {
                return NotFound();
            }

            return View(keyboard);
        }

        // GET: Keyboards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Keyboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Keyboard keyboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keyboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(keyboard);
        }

        // GET: Keyboards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyboard = await _context.Keyboards.FindAsync(id);
            if (keyboard == null)
            {
                return NotFound();
            }
            return View(keyboard);
        }

        // POST: Keyboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Description,Price,Wireless")] Keyboard keyboard)
        {
            if (id != keyboard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keyboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeyboardExists(keyboard.Id))
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
            return View(keyboard);
        }

        // GET: Keyboards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyboard = await _context.Keyboards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keyboard == null)
            {
                return NotFound();
            }

            return View(keyboard);
        }

        // POST: Keyboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keyboard = await _context.Keyboards.FindAsync(id);
            _context.Keyboards.Remove(keyboard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeyboardExists(int id)
        {
            return _context.Keyboards.Any(e => e.Id == id);
        }
    }
}
