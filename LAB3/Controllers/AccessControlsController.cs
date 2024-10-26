using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB3.Models;

namespace LAB3.Controllers
{
    public class AccessControlsController : Controller
    {
        private readonly VnsContext _context;

        public AccessControlsController(VnsContext context)
        {
            _context = context;
        }

        // GET: AccessControls
        public async Task<IActionResult> Index()
        {
            var vnsContext = _context.AccessControls.Include(a => a.Resource).Include(a => a.User);
            return View(await vnsContext.ToListAsync());
        }

        // GET: AccessControls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessControl = await _context.AccessControls
                .Include(a => a.Resource)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AccessId == id);
            if (accessControl == null)
            {
                return NotFound();
            }

            return View(accessControl);
        }

        // GET: AccessControls/Create
        public IActionResult Create()
        {
            ViewData["ResourceId"] = new SelectList(_context.Resources, "ResourceId", "ResourceId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: AccessControls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccessId,ResourceId,UserId,Role,AccessStart,AccessEnd,Restricted")] AccessControl accessControl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accessControl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResourceId"] = new SelectList(_context.Resources, "ResourceId", "ResourceId", accessControl.ResourceId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", accessControl.UserId);
            return View(accessControl);
        }

        // GET: AccessControls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessControl = await _context.AccessControls.FindAsync(id);
            if (accessControl == null)
            {
                return NotFound();
            }
            ViewData["ResourceId"] = new SelectList(_context.Resources, "ResourceId", "ResourceId", accessControl.ResourceId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", accessControl.UserId);
            return View(accessControl);
        }

        // POST: AccessControls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccessId,ResourceId,UserId,Role,AccessStart,AccessEnd,Restricted")] AccessControl accessControl)
        {
            if (id != accessControl.AccessId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessControl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessControlExists(accessControl.AccessId))
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
            ViewData["ResourceId"] = new SelectList(_context.Resources, "ResourceId", "ResourceId", accessControl.ResourceId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", accessControl.UserId);
            return View(accessControl);
        }

        // GET: AccessControls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessControl = await _context.AccessControls
                .Include(a => a.Resource)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AccessId == id);
            if (accessControl == null)
            {
                return NotFound();
            }

            return View(accessControl);
        }

        // POST: AccessControls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessControl = await _context.AccessControls.FindAsync(id);
            if (accessControl != null)
            {
                _context.AccessControls.Remove(accessControl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessControlExists(int id)
        {
            return _context.AccessControls.Any(e => e.AccessId == id);
        }
    }
}
