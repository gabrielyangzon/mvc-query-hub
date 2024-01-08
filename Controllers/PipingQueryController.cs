using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using mvc_query_hub.Data;
using mvc_query_hub.Models;

namespace mvc_query_hub.Controllers
{
    public class PipingQueryController : Controller
    {
        private readonly HubContext _context;

        public PipingQueryController(HubContext context)
        {
            _context = context;
        }

        // GET: PipingQuery
        //[OutputCache(Duration = 10)]
        public async Task<IActionResult> Index()
        {


            return _context.Queries != null ?
                        View(await _context.Queries.ToListAsync()) :
                        Problem("Entity set 'HubContext.Queries'  is null.");
        }




        // GET: PipingQuery/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Queries == null)
            {
                return NotFound();
            }

            var pipingQuery = await _context.Queries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pipingQuery == null)
            {
                return NotFound();
            }


            var comments = await _context.Comments
               .Where(m => m.QueryId == pipingQuery.Id).ToListAsync();


            ViewData["Comments"] = comments;


            return View(pipingQuery);
        }

        // GET: PipingQuery/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PipingQuery/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,From,To,Tags,Image")] PipingQuery pipingQuery)
        {
            pipingQuery.PostDate = DateTime.Now;
            pipingQuery.EditDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(pipingQuery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pipingQuery);
        }

        // GET: PipingQuery/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Queries == null)
            {
                return NotFound();
            }

            var pipingQuery = await _context.Queries.FindAsync(id);
            if (pipingQuery == null)
            {
                return NotFound();
            }
            return View(pipingQuery);
        }

        // POST: PipingQuery/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,From,To,Tags,PostDate,Image")] PipingQuery pipingQuery)
        {
            if (id != pipingQuery.Id)
            {
                return NotFound();
            }
            pipingQuery.EditDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pipingQuery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PipingQueryExists(pipingQuery.Id))
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
            return View(pipingQuery);
        }

        // GET: PipingQuery/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Queries == null)
            {
                return NotFound();
            }

            var pipingQuery = await _context.Queries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pipingQuery == null)
            {
                return NotFound();
            }

            return View(pipingQuery);
        }

        // POST: PipingQuery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Queries == null)
            {
                return Problem("Entity set 'HubContext.Queries'  is null.");
            }
            var pipingQuery = await _context.Queries.FindAsync(id);
            if (pipingQuery != null)
            {
                _context.Queries.Remove(pipingQuery);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PipingQueryExists(int id)
        {
            return (_context.Queries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
