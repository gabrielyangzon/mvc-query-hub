using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_query_hub.Data;
using mvc_query_hub.Models;

namespace mvc_query_hub.Controllers
{
    public class QueryCommentsController : Controller
    {
        private readonly HubContext _context;

        public QueryCommentsController(HubContext context)
        {
            _context = context;
        }





        // GET: QueryComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var queryComments = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (queryComments == null)
            {
                return NotFound();
            }

            return View(queryComments);
        }

        // GET: QueryComments/Create
        [Route("QueryComments/Create/{queryId}")]
        public IActionResult Create(int queryId)
        {
            ViewData["queryId"] = queryId;
            return View();
        }

        // POST: QueryComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("QueryComments/Create/{queryId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Message,QueryId")] QueryComments queryComments)
        {
            queryComments.PostDate = DateTime.Now;
            queryComments.EditDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(queryComments);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "PipingQuery", new { id = queryComments.QueryId });
            }
            return View(queryComments);
        }

        // GET: QueryComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var queryComments = await _context.Comments.FindAsync(id);
            if (queryComments == null)
            {
                return NotFound();
            }
            return View(queryComments);
        }

        // POST: QueryComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QueryId,Message,UserId,DateTime")] QueryComments queryComments)
        {
            if (id != queryComments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(queryComments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueryCommentsExists(queryComments.Id))
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
            return View(queryComments);
        }

        // GET: QueryComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var queryComments = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (queryComments == null)
            {
                return NotFound();
            }

            return View(queryComments);
        }

        // POST: QueryComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set 'HubContext.Comments'  is null.");
            }
            var queryComments = await _context.Comments.FindAsync(id);
            if (queryComments != null)
            {
                _context.Comments.Remove(queryComments);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueryCommentsExists(int id)
        {
            return (_context.Comments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
