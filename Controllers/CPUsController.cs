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
    public class CPUsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CPUsController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: CPUs
        public async Task<IActionResult> Index(int pg = 1)
        {
            List<CPU> cPUs = await _context.CPUs.ToListAsync();
            int pageSize = 8;
            if (pg < 1)
                pg = 1;
            int resCount = cPUs.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = cPUs.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            
            return View(data);
        }

        public async Task<IActionResult> Search(string? descriptionString, string? minPrice, string? maxPrice, string warranty)
        {
            var cpus = from c in _context.CPUs
                           select c;
            if (String.IsNullOrEmpty(descriptionString))
                descriptionString = "";
            if (String.IsNullOrEmpty(minPrice))
                minPrice = "0";
            if (String.IsNullOrEmpty(maxPrice))
                maxPrice = "1000000";
            if (warranty.Equals("--"))
            {
                cpus = cpus.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice));
                return View("Index", await cpus.ToListAsync());
            }
            else
            {
                cpus = cpus.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice) && s.Warranty == bool.Parse(warranty));
                return View("Index", await cpus.ToListAsync());
            }
        }

        // GET: CPUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPU = await _context.CPUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPU == null)
            {
                return NotFound();
            }

            return View(cPU);
        }

        // GET: CPUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CPUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Description,Price,Warranty")] CPU cPU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cPU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cPU);
        }

        // GET: CPUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPU = await _context.CPUs.FindAsync(id);
            if (cPU == null)
            {
                return NotFound();
            }
            return View(cPU);
        }

        // POST: CPUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Description,Price,Warranty")] CPU cPU)
        {
            if (id != cPU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cPU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CPUExists(cPU.Id))
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
            return View(cPU);
        }

        // GET: CPUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPU = await _context.CPUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPU == null)
            {
                return NotFound();
            }

            return View(cPU);
        }

        // POST: CPUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cPU = await _context.CPUs.FindAsync(id);
            _context.CPUs.Remove(cPU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CPUExists(int id)
        {
            return _context.CPUs.Any(e => e.Id == id);
        }
    }
}
