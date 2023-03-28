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
    public class TeacherTablesController : Controller
    {
        private readonly AttendenceManagementContext _context;

        public TeacherTablesController(AttendenceManagementContext context)
        {
            _context = context;
        }

        // GET: TeacherTables
        public async Task<IActionResult> Index()
        {
              return _context.TeacherTables != null ? 
                          View(await _context.TeacherTables.ToListAsync()) :
                          Problem("Entity set 'AttendenceManagementContext.TeacherTables'  is null.");
        }

        // GET: TeacherTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeacherTables == null)
            {
                return NotFound();
            }

            var teacherTable = await _context.TeacherTables
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacherTable == null)
            {
                return NotFound();
            }

            return View(teacherTable);
        }

        // GET: TeacherTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeacherTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherId,TeacherName,Email,DateOfBirth,MobileNo,JoiningDate")] TeacherTable teacherTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacherTable);
        }

        // GET: TeacherTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeacherTables == null)
            {
                return NotFound();
            }

            var teacherTable = await _context.TeacherTables.FindAsync(id);
            if (teacherTable == null)
            {
                return NotFound();
            }
            return View(teacherTable);
        }

        // POST: TeacherTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherId,TeacherName,Email,DateOfBirth,MobileNo,JoiningDate")] TeacherTable teacherTable)
        {
            if (id != teacherTable.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherTableExists(teacherTable.TeacherId))
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
            return View(teacherTable);
        }

        // GET: TeacherTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeacherTables == null)
            {
                return NotFound();
            }

            var teacherTable = await _context.TeacherTables
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacherTable == null)
            {
                return NotFound();
            }

            return View(teacherTable);
        }

        // POST: TeacherTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeacherTables == null)
            {
                return Problem("Entity set 'AttendenceManagementContext.TeacherTables'  is null.");
            }
            var teacherTable = await _context.TeacherTables.FindAsync(id);
            if (teacherTable != null)
            {
                _context.TeacherTables.Remove(teacherTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherTableExists(int id)
        {
          return (_context.TeacherTables?.Any(e => e.TeacherId == id)).GetValueOrDefault();
        }
    }
}
