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
    public class MediaItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MediaItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MediaItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MediaItem.Include(m => m.Collection);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MediaItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaItem = await _context.MediaItem
                .Include(m => m.Collection)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mediaItem == null)
            {
                return NotFound();
            }

            return View(mediaItem);
        }

        // GET: MediaItems/Create
        public IActionResult Create()
        {
            ViewData["CollectionId"] = new SelectList(_context.MediaCollection, "Id", "Id");
            return View();
        }

        // POST: MediaItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,FileType,CollectionId")] MediaItem mediaItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediaItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollectionId"] = new SelectList(_context.MediaCollection, "Id", "Id", mediaItem.CollectionId);
            return View(mediaItem);
        }

        // GET: MediaItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaItem = await _context.MediaItem.SingleOrDefaultAsync(m => m.Id == id);
            if (mediaItem == null)
            {
                return NotFound();
            }
            ViewData["CollectionId"] = new SelectList(_context.MediaCollection, "Id", "Id", mediaItem.CollectionId);
            return View(mediaItem);
        }

        // POST: MediaItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,FileType,CollectionId")] MediaItem mediaItem)
        {
            if (id != mediaItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaItemExists(mediaItem.Id))
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
            ViewData["CollectionId"] = new SelectList(_context.MediaCollection, "Id", "Id", mediaItem.CollectionId);
            return View(mediaItem);
        }

        // GET: MediaItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaItem = await _context.MediaItem
                .Include(m => m.Collection)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mediaItem == null)
            {
                return NotFound();
            }

            return View(mediaItem);
        }

        // POST: MediaItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaItem = await _context.MediaItem.SingleOrDefaultAsync(m => m.Id == id);
            _context.MediaItem.Remove(mediaItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaItemExists(int id)
        {
            return _context.MediaItem.Any(e => e.Id == id);
        }
    }
}
