using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sseProject1.Data;
using sseProject1.Models;

namespace sseProject1.Controllers
{
    public class MediaCollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MediaCollectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MediaCollections
        public async Task<IActionResult> Index()
        {
            return View(await _context.MediaCollection.ToListAsync());
        }

        // GET: MediaCollections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaCollection = await _context.MediaCollection
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mediaCollection == null)
            {
                return NotFound();
            }

            return View(mediaCollection);
        }

        // GET: MediaCollections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MediaCollections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BorrowRequests,ApplicationUserId")] MediaCollection mediaCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediaCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mediaCollection);
        }

        // GET: MediaCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaCollection = await _context.MediaCollection.SingleOrDefaultAsync(m => m.Id == id);
            if (mediaCollection == null)
            {
                return NotFound();
            }
            return View(mediaCollection);
        }

        // POST: MediaCollections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BorrowRequests,ApplicationUserId")] MediaCollection mediaCollection)
        {
            if (id != mediaCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaCollectionExists(mediaCollection.Id))
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
            return View(mediaCollection);
        }

        // GET: MediaCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaCollection = await _context.MediaCollection
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mediaCollection == null)
            {
                return NotFound();
            }

            return View(mediaCollection);
        }

        // POST: MediaCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaCollection = await _context.MediaCollection.SingleOrDefaultAsync(m => m.Id == id);
            _context.MediaCollection.Remove(mediaCollection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaCollectionExists(int id)
        {
            return _context.MediaCollection.Any(e => e.Id == id);
        }
    }
}
