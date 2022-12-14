using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamingStoreProject.Data;
using GamingStoreProject.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GamingStoreProject.Controllers
{
    public class PCsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PCsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: PCs
        public async Task<IActionResult> Index(int pg = 1)
        {
            List<PC> pCs = await _context.PCs.ToListAsync();
            int pageSize = 8;
            if (pg < 1)
                pg = 1;
            int resCount = pCs.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = pCs.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            //return View(graphicCards);
            return View(data);
        }

        public async Task<IActionResult> Search(string? descriptionString, string? minPrice, string? maxPrice, string warranty)
        {
            var pcs = from p in _context.PCs
                           select p;
            if (String.IsNullOrEmpty(descriptionString))
                descriptionString = "";
            if (String.IsNullOrEmpty(minPrice))
                minPrice = "0";
            if (String.IsNullOrEmpty(maxPrice))
                maxPrice = "1000000";
            if (warranty.Equals("--"))
            {
                pcs = pcs.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice));
                return View("Index", await pcs.ToListAsync());
            }
            else
            {
                pcs = pcs.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice) && s.Warranty == bool.Parse(warranty));
                return View("Index", await pcs.ToListAsync());
            }
        }

        // GET: PCs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pC = await _context.PCs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pC == null)
            {
                return NotFound();
            }

            return View(pC);
        }

        // GET: PCs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PC pC)
        {
            if (ModelState.IsValid)
            {
                


               // string imageName = "noimage.png";
                //if(pC.ImageUpload != null)
                //{

                    //string wwwRootPath = _hostEnvironment.WebRootPath;
                    //string fileName = Path.GetFileNameWithoutExtension(pC.ImageUpload.FileName);
                    //string extension = Path.GetExtension(pC.ImageUpload.FileName);
                    //pC.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    //string path = Path.Combine(wwwRootPath + "/ProductImages/", fileName);
                    //using (var fileStream = new FileStream(path, FileMode.Create))
                    //{
                    //    await pC.ImageUpload.CopyToAsync(fileStream);       
                    //}

                    
                    //string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "ProductImages");
                    // imageName = Guid.NewGuid().ToString() + "_" + pC.ImageUpload.FileName;
                    //string filePath = Path.Combine(uploadsDir, imageName);
                    // FileStream fs = new FileStream(filePath, FileMode.Create);
                    // await pC.ImageUpload.CopyToAsync(fs);
                    // fs.Close();
                //}
                //pC.Image = imageName;
                _context.Add(pC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(pC);
        }

        // GET: PCs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pC = await _context.PCs.FindAsync(id);
            if (pC == null)
            {
                return NotFound();
            }
            return View(pC);
        }

        // POST: PCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Description,Price,Warranty")] PC pC)
        {
            if (id != pC.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PCExists(pC.Id))
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
            return View(pC);
        }

        // GET: PCs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pC = await _context.PCs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pC == null)
            {
                return NotFound();
            }

            return View(pC);
        }

        // POST: PCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pC = await _context.PCs.FindAsync(id);
            _context.PCs.Remove(pC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PCExists(int id)
        {
            return _context.PCs.Any(e => e.Id == id);
        }
    }
}
