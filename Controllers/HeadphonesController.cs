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
    public class HeadphonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HeadphonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Headphones
        public async Task<IActionResult> Index(int pg = 1)
        {
            List<Headphone> headphones = await _context.Headphones.ToListAsync();
            int pageSize = 8;
            if (pg < 1)
                pg = 1;
            int resCount = headphones.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = headphones.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
           
            return View(data);
        }
        public async Task<IActionResult> Search(string? descriptionString, string? minPrice, string? maxPrice, string warranty)
        {
            var headphones = from h in _context.Headphones
                           select h;
            if (String.IsNullOrEmpty(descriptionString))
                descriptionString = "";
            if (String.IsNullOrEmpty(minPrice))
                minPrice = "0";
            if (String.IsNullOrEmpty(maxPrice))
                maxPrice = "1000000";
            if (warranty.Equals("--"))
            {
                headphones = headphones.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice));
                return View("Index", await headphones.ToListAsync());
            }
            else
            {
                headphones = headphones.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice) && s.Warranty == bool.Parse(warranty));
                return View("Index", await headphones.ToListAsync());
            }
        }
    

        // GET: Headphones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var headphone = await _context.Headphones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (headphone == null)
            {
                return NotFound();
            }

            return View(headphone);
        }

        // GET: Headphones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Headphones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Headphone headphone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(headphone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(headphone);
        }

        // GET: Headphones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var headphone = await _context.Headphones.FindAsync(id);
            if (headphone == null)
            {
                return NotFound();
            }
            return View(headphone);
        }

        // POST: Headphones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Description,Price,Warranty")] Headphone headphone)
        {
            if (id != headphone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(headphone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeadphoneExists(headphone.Id))
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
            return View(headphone);
        }

        // GET: Headphones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var headphone = await _context.Headphones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (headphone == null)
            {
                return NotFound();
            }

            return View(headphone);
        }

        // POST: Headphones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var headphone = await _context.Headphones.FindAsync(id);
            _context.Headphones.Remove(headphone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeadphoneExists(int id)
        {
            return _context.Headphones.Any(e => e.Id == id);
        }
    }
}
