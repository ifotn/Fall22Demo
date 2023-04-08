using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fall22Demo.Data;
using Fall22Demo.Models;

namespace Fall22Demo.Controllers
{
    public class ChildrenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChildrenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Children
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Children.Include(c => c.Parent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Children == null)
            {
                return NotFound();
            }

            var child = await _context.Children
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.ChildId == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // GET: Children/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "Name");
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChildId,Name,ParentId")] Child child)
        {
            if (ModelState.IsValid)
            {
                _context.Add(child);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "Name", child.ParentId);
            return View(child);
        }

        // GET: Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Children == null)
            {
                return NotFound();
            }

            var child = await _context.Children.FindAsync(id);
            if (child == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "Name", child.ParentId);
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChildId,Name,ParentId")] Child child)
        {
            if (id != child.ChildId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(child);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildExists(child.ChildId))
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
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "Name", child.ParentId);
            return View(child);
        }

        // GET: Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Children == null)
            {
                return NotFound();
            }

            var child = await _context.Children
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.ChildId == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Children == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Children'  is null.");
            }
            var child = await _context.Children.FindAsync(id);
            if (child != null)
            {
                _context.Children.Remove(child);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildExists(int id)
        {
          return _context.Children.Any(e => e.ChildId == id);
        }
    }
}
