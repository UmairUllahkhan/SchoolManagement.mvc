using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.mvc.DataDB;

namespace SchoolManagement.mvc.Controllers
{
    public class CourseTablesController : Controller
    {
        private readonly AttendenceManagementContext _context;

        public CourseTablesController(AttendenceManagementContext context)
        {
            _context = context;
        }

        // GET: CourseTables
        public async Task<IActionResult> Index()
        {
            return _context.CourseTables != null ? View(await _context.CourseTables.ToListAsync()) : Problem("is Null");
              //return View(await _context.CourseTables.ToListAsync());
        }

        // GET: CourseTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CourseTables == null)
            {
                return NotFound();
            }

            var courseTable = await _context.CourseTables
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (courseTable == null)
            {
                return NotFound();
            }

            return View(courseTable);
        }

        // GET: CourseTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,ShortName")] CourseTable courseTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseTable);
        }

        // GET: CourseTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CourseTables == null)
            {
                return NotFound();
            }

            var courseTable = await _context.CourseTables.FindAsync(id);
            if (courseTable == null)
            {
                return NotFound();
            }
            return View(courseTable);
        }

        // POST: CourseTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseName,ShortName")] CourseTable courseTable)
        {
            if (id != courseTable.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseTableExists(courseTable.CourseId))
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
            return View(courseTable);
        }

        // GET: CourseTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CourseTables == null)
            {
                return NotFound();
            }

            var courseTable = await _context.CourseTables
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (courseTable == null)
            {
                return NotFound();
            }

            return View(courseTable);
        }

        // POST: CourseTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CourseTables == null)
            {
                return Problem("Entity set 'AttendenceManagementContext.CourseTables'  is null.");
            }
            var courseTable = await _context.CourseTables.FindAsync(id);
            if (courseTable != null)
            {
                _context.CourseTables.Remove(courseTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseTableExists(int id)
        {
          return _context.CourseTables.Any(e => e.CourseId == id);
        }
    }
}
