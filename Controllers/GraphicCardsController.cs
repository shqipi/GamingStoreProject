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
    public class GraphicCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GraphicCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GraphicCards
        public async Task<IActionResult> Index(int pg =1)
        {
            List<GraphicCard> graphicCards =await _context.GraphicCards.ToListAsync();
            int pageSize = 8;
            if (pg < 1)
                pg = 1;
            int resCount = graphicCards.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = graphicCards.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            //return View(graphicCards);
            return View(data);
        }

        public async Task<IActionResult> Search(string? descriptionString, string? minPrice, string? maxPrice, string warranty)
        {
            var graphicCards = from c in _context.GraphicCards
                           select c;
            if (String.IsNullOrEmpty(descriptionString))
                descriptionString = "";
            if (String.IsNullOrEmpty(minPrice))
                minPrice = "0";
            if (String.IsNullOrEmpty(maxPrice))
                maxPrice = "1000000";
            if (warranty.Equals("--"))
            {
                graphicCards = graphicCards.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice));
                return View("Index", await graphicCards.ToListAsync());
            }
            else
            {
                graphicCards = graphicCards.Where(s => s.Description!.Contains(descriptionString) &&
                s.Price > Double.Parse(minPrice) && s.Price < Double.Parse(maxPrice) && s.Warranty == bool.Parse(warranty));
                return View("Index", await graphicCards.ToListAsync());
            }
        }

        // GET: GraphicCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graphicCard = await _context.GraphicCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (graphicCard == null)
            {
                return NotFound();
            }

            return View(graphicCard);
        }

        // GET: GraphicCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GraphicCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GraphicCard graphicCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(graphicCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(graphicCard);
        }

        // GET: GraphicCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graphicCard = await _context.GraphicCards.FindAsync(id);
            if (graphicCard == null)
            {
                return NotFound();
            }
            return View(graphicCard);
        }

        // POST: GraphicCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Description,Price,Warranty")] GraphicCard graphicCard)
        {
            if (id != graphicCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(graphicCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GraphicCardExists(graphicCard.Id))
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
            return View(graphicCard);
        }

        // GET: GraphicCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graphicCard = await _context.GraphicCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (graphicCard == null)
            {
                return NotFound();
            }

            return View(graphicCard);
        }

        // POST: GraphicCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var graphicCard = await _context.GraphicCards.FindAsync(id);
            _context.GraphicCards.Remove(graphicCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GraphicCardExists(int id)
        {
            return _context.GraphicCards.Any(e => e.Id == id);
        }
    }
}
