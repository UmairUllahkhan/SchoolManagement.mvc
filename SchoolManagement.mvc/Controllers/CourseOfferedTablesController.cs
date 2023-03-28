using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.mvc.DataDB;
using SchoolManagement.mvc.Models;

namespace SchoolManagement.mvc.Controllers
{
    public class CourseOfferedTablesController : Controller
    {
        private readonly AttendenceManagementContext _context;

        public CourseOfferedTablesController(AttendenceManagementContext context)
        {
            _context = context;
        }

        // GET: CourseOfferedTables
        public async Task<IActionResult> Index()
        {
            var attendenceManagementContext = _context.CourseOfferedTables.Include(c => c.Course);
            return View(await attendenceManagementContext.ToListAsync());
        }

        // GET: CourseOfferedTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CourseOfferedTables == null)
            {
                return NotFound();
            }

            var courseOfferedTable = await _context.CourseOfferedTables
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.CoId == id);
            if (courseOfferedTable == null)
            {
                return NotFound();
            }

            return View(courseOfferedTable);
        }

        // GET: CourseOfferedTables/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.CourseTables, "CourseId", "CourseName");
            return View();
        }

        // POST: CourseOfferedTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoId,CourseId,TsId")] CourseOfferedTable courseOfferedTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseOfferedTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.CourseTables, "CourseId", "CourseName", courseOfferedTable.CourseId);
            return View(courseOfferedTable);
        }

        // GET: CourseOfferedTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CourseOfferedTables == null)
            {
                return NotFound();
            }

            var courseOfferedTable = await _context.CourseOfferedTables.FindAsync(id);
            if (courseOfferedTable == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.CourseTables, "CourseId", "CourseId", courseOfferedTable.CourseId);
            return View(courseOfferedTable);
        }

        // POST: CourseOfferedTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoId,CourseId,TsId")] CourseOfferedTable courseOfferedTable)
        {
            if (id != courseOfferedTable.CoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseOfferedTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseOfferedTableExists(courseOfferedTable.CoId))
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
            ViewData["CourseId"] = new SelectList(_context.CourseTables, "CourseId", "CourseId", courseOfferedTable.CourseId);
            return View(courseOfferedTable);
        }

        // GET: CourseOfferedTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CourseOfferedTables == null)
            {
                return NotFound();
            }

            var courseOfferedTable = await _context.CourseOfferedTables
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.CoId == id);
            if (courseOfferedTable == null)
            {
                return NotFound();
            }

            return View(courseOfferedTable);
        }

        // POST: CourseOfferedTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CourseOfferedTables == null)
            {
                return Problem("Entity set 'AttendenceManagementContext.CourseOfferedTables'  is null.");
            }
            var courseOfferedTable = await _context.CourseOfferedTables.FindAsync(id);
            if (courseOfferedTable != null)
            {
                _context.CourseOfferedTables.Remove(courseOfferedTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> AssignTeacher(int id)
        {
            var course = await _context.CourseOfferedTables
                .Include(c => c.Course)

                .Include(c => c.TeacherAllocationTables)
                    .ThenInclude(q => q.TIdNavigation)
                .FirstOrDefaultAsync(m => m.CoId == id);
            var teacher = await _context.TeacherTables.ToListAsync();
            var model = new AssignTeacherViewModel();
            model.CourseOfferedTable = course;
            foreach (var teach in teacher)
            {
                model.AssignTeacher.Add(new Assign
                {
                    Id = teach.TeacherId,
                    Name = teach.TeacherName,
                    Email = teach.Email,
                    IsAssign = (course?.TeacherAllocationTables?.Any(q => q.TId == teach.TeacherId)).GetValueOrDefault()
                  
                });


            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> AssignCourse(int CourseId,int TeacherId,bool ShouldAssign)
        {
            var assign = new TeacherAllocationTable();
            if(ShouldAssign == true)
            {
                assign.CoId = CourseId;
                assign.TId = TeacherId;
                await _context.TeacherAllocationTables.AddAsync(assign);
            }
            else
            {
                assign = await _context.TeacherAllocationTables.FirstOrDefaultAsync(
                    q => q.CoId == CourseId && q.TId == TeacherId);
                if (assign != null)
                {
                    _context.Remove(assign);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseOfferedTableExists(int id)
        {
          return (_context.CourseOfferedTables?.Any(e => e.CoId == id)).GetValueOrDefault();
        }
    }
}
