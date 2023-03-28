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
    public class StudentEnrollmentTablesController : Controller
    {
        private readonly AttendenceManagementContext _context;

        public StudentEnrollmentTablesController(AttendenceManagementContext context)
        {
            _context = context;
        }

        // GET: StudentEnrollmentTables
        public async Task<IActionResult> Index()
        {

            //join use is left join
            var attendenceManagementContext = _context.StudentEnrollmentTables.Include(s => s.Co).Include(s => s.Stu);
            return View(await attendenceManagementContext.ToListAsync());
        }

        // GET: StudentEnrollmentTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentEnrollmentTables == null)
            {
                return NotFound();
            }

            var studentEnrollmentTable = await _context.StudentEnrollmentTables
                .Include(s => s.Co)
                .Include(s => s.Stu)
                .FirstOrDefaultAsync(m => m.SeId == id);
            if (studentEnrollmentTable == null)
            {
                return NotFound();
            }

            return View(studentEnrollmentTable);
        }

        // GET: StudentEnrollmentTables/Create
        public IActionResult Create()
        {
            ViewData["CoId"] = new SelectList(_context.CourseOfferedTables, "CoId", "CoId");
            ViewData["StuId"] = new SelectList(_context.StudentTables, "StudentRegId", "StudentRegId");
            return View();
        }

        // POST: StudentEnrollmentTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeId,StuId,CoId")] StudentEnrollmentTable studentEnrollmentTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentEnrollmentTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoId"] = new SelectList(_context.CourseOfferedTables, "CoId", "CoId", studentEnrollmentTable.CoId);
            ViewData["StuId"] = new SelectList(_context.StudentTables, "StudentRegId", "StudentRegId", studentEnrollmentTable.StuId);
            return View(studentEnrollmentTable);
        }

        // GET: StudentEnrollmentTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentEnrollmentTables == null)
            {
                return NotFound();
            }

            var studentEnrollmentTable = await _context.StudentEnrollmentTables.FindAsync(id);
            if (studentEnrollmentTable == null)
            {
                return NotFound();
            }
            ViewData["CoId"] = new SelectList(_context.CourseOfferedTables, "CoId", "CoId", studentEnrollmentTable.CoId);
            ViewData["StuId"] = new SelectList(_context.StudentTables, "StudentRegId", "StudentRegId", studentEnrollmentTable.StuId);
            return View(studentEnrollmentTable);
        }

        // POST: StudentEnrollmentTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeId,StuId,CoId")] StudentEnrollmentTable studentEnrollmentTable)
        {
            if (id != studentEnrollmentTable.SeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentEnrollmentTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentEnrollmentTableExists(studentEnrollmentTable.SeId))
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
            ViewData["CoId"] = new SelectList(_context.CourseOfferedTables, "CoId", "CoId", studentEnrollmentTable.CoId);
            ViewData["StuId"] = new SelectList(_context.StudentTables, "StudentRegId", "StudentRegId", studentEnrollmentTable.StuId);
            return View(studentEnrollmentTable);
        }

        // GET: StudentEnrollmentTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentEnrollmentTables == null)
            {
                return NotFound();
            }

            var studentEnrollmentTable = await _context.StudentEnrollmentTables
                .Include(s => s.Co)
                .Include(s => s.Stu)
                .FirstOrDefaultAsync(m => m.SeId == id);
            if (studentEnrollmentTable == null)
            {
                return NotFound();
            }

            return View(studentEnrollmentTable);
        }

        // POST: StudentEnrollmentTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentEnrollmentTables == null)
            {
                return Problem("Entity set 'AttendenceManagementContext.StudentEnrollmentTables'  is null.");
            }
            var studentEnrollmentTable = await _context.StudentEnrollmentTables.FindAsync(id);
            if (studentEnrollmentTable != null)
            {
                _context.StudentEnrollmentTables.Remove(studentEnrollmentTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentEnrollmentTableExists(int id)
        {
          return (_context.StudentEnrollmentTables?.Any(e => e.SeId == id)).GetValueOrDefault();
        }
    }
}
