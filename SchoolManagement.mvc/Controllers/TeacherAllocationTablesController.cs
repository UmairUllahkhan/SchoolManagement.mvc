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
    public class TeacherAllocationTablesController : Controller
    {
        private readonly AttendenceManagementContext _context;

        public TeacherAllocationTablesController(AttendenceManagementContext context)
        {
            _context = context;
        }

        // GET: TeacherAllocationTables
        public async Task<IActionResult> Index()
        {
            var attendenceManagementContext = _context.TeacherAllocationTables.Include(t => t.Co).Include(t => t.TIdNavigation);
            return View(await attendenceManagementContext.ToListAsync());
        }

        // GET: TeacherAllocationTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeacherAllocationTables == null)
            {
                return NotFound();
            }

            var teacherAllocationTable = await _context.TeacherAllocationTables
                .Include(t => t.Co)
                .Include(t => t.TIdNavigation)
                .FirstOrDefaultAsync(m => m.TaId == id);
            if (teacherAllocationTable == null)
            {
                return NotFound();
            }

            return View(teacherAllocationTable);
        }

        // GET: TeacherAllocationTables/Create
        public IActionResult Create()
        {
            /// to view course name
            var course = _context.CourseOfferedTables.Include(q => q.Course).Select(q => new
            {
                Coursename = q.Course.CourseName,
                Id = q.CoId
            });
            ViewData["CoId"] = new SelectList(course, "Id", "Coursename");
            //ViewData["CoId"] = new SelectList(_context.CourseOfferedTables, "CoId", "CoId");
            ViewData["TId"] = new SelectList(_context.TeacherTables, "TeacherId", "TeacherName");
            return View();
        }

        // POST: TeacherAllocationTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaId,CoId,TId")] TeacherAllocationTable teacherAllocationTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherAllocationTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var course = _context.CourseOfferedTables.Include(q => q.Course).Select(q => new
            {
                Coursename = q.Course.CourseName,
                Id = q.CoId
            }); 
            ViewData["CoId"] = new SelectList(course, "Id", "Coursename", teacherAllocationTable.CoId);
            //ViewData["CoId"] = new SelectList(_context.CourseOfferedTables, "CoId", "CoId", teacherAllocationTable.CoId);

            //ViewData["TId"] = new SelectList(_context.TeacherTables, "TeacherId", "TeacherName", teacherAllocationTable.TId);
            return View(teacherAllocationTable);
        }

        // GET: TeacherAllocationTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeacherAllocationTables == null)
            {
                return NotFound();
            }

            var teacherAllocationTable = await _context.TeacherAllocationTables.FindAsync(id);
            if (teacherAllocationTable == null)
            {
                return NotFound();
            }
            ViewData["CoId"] = new SelectList(_context.CourseOfferedTables, "CoId", "CoId", teacherAllocationTable.CoId);
            ViewData["TId"] = new SelectList(_context.TeacherTables, "TeacherId", "TeacherId", teacherAllocationTable.TId);
            return View(teacherAllocationTable);
        }

        // POST: TeacherAllocationTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaId,CoId,TId")] TeacherAllocationTable teacherAllocationTable)
        {
            if (id != teacherAllocationTable.TaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherAllocationTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherAllocationTableExists(teacherAllocationTable.TaId))
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
            ViewData["CoId"] = new SelectList(_context.CourseOfferedTables, "CoId", "CoId", teacherAllocationTable.CoId);
            ViewData["TId"] = new SelectList(_context.TeacherTables, "TeacherId", "TeacherId", teacherAllocationTable.TId);
            return View(teacherAllocationTable);
        }

        // GET: TeacherAllocationTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeacherAllocationTables == null)
            {
                return NotFound();
            }

            var teacherAllocationTable = await _context.TeacherAllocationTables
                .Include(t => t.Co)
                .Include(t => t.TIdNavigation)
                .FirstOrDefaultAsync(m => m.TaId == id);
            if (teacherAllocationTable == null)
            {
                return NotFound();
            }

            return View(teacherAllocationTable);
        }

        // POST: TeacherAllocationTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeacherAllocationTables == null)
            {
                return Problem("Entity set 'AttendenceManagementContext.TeacherAllocationTables'  is null.");
            }
            var teacherAllocationTable = await _context.TeacherAllocationTables.FindAsync(id);
            if (teacherAllocationTable != null)
            {
                _context.TeacherAllocationTables.Remove(teacherAllocationTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherAllocationTableExists(int id)
        {
          return (_context.TeacherAllocationTables?.Any(e => e.TaId == id)).GetValueOrDefault();
        }
    }
}
