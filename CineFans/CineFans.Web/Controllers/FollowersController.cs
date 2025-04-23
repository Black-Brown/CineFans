using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineFans.Domain.Entities;

namespace CineFans.Web.Controllers
{
    public class FollowersController : Controller
    {
        private readonly CineFansDbContext _context;

        public FollowersController(CineFansDbContext context)
        {
            _context = context;
        }

        // GET: Followers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Followers.ToListAsync());
        }

        // GET: Followers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followers = await _context.Followers
                .FirstOrDefaultAsync(m => m.FollowerId == id);
            if (followers == null)
            {
                return NotFound();
            }

            return View(followers);
        }

        // GET: Followers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Followers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FollowerId,FollowedId")] Follower followers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(followers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(followers);
        }

        // GET: Followers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followers = await _context.Followers.FindAsync(id);
            if (followers == null)
            {
                return NotFound();
            }
            return View(followers);
        }

        // POST: Followers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FollowerId,FollowedId")] Follower followers)
        {
            if (id != followers.FollowerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(followers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowersExists(followers.FollowerId))
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
            return View(followers);
        }

        // GET: Followers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followers = await _context.Followers
                .FirstOrDefaultAsync(m => m.FollowerId == id);
            if (followers == null)
            {
                return NotFound();
            }

            return View(followers);
        }

        // POST: Followers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var followers = await _context.Followers.FindAsync(id);
            if (followers != null)
            {
                _context.Followers.Remove(followers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowersExists(int id)
        {
            return _context.Followers.Any(e => e.FollowerId == id);
        }
    }
}
