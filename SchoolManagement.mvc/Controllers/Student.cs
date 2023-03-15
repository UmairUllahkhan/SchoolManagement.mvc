using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.mvc.DataDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace SchoolManagement.mvc.Controllers
{
    public class Student : Controller
    {
        private readonly AttendenceManagementContext _context;

        public Student(AttendenceManagementContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context != null ? View(await _context.StudentTables.ToListAsync()) : Problem("Table is Null");
            
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentName","Email","DateOfBirth","MobileNo")] StudentTable studentTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(studentTable);
        }
    }

}
