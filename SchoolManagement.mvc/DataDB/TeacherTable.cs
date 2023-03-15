using System;
using System.Collections.Generic;

namespace SchoolManagement.mvc.DataDB
{
    public partial class TeacherTable
    {
        public TeacherTable()
        {
            TeacherAllocationTables = new HashSet<TeacherAllocationTable>();
        }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = null!;
        public string Email { get; set; } = null!;
        /// <summary>
        /// DD/MM/YYYY
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        public string MobileNo { get; set; } = null!;
        /// <summary>
        /// DD/MM/YYYY
        /// </summary>
        public DateTime JoiningDate { get; set; }

        public virtual ICollection<TeacherAllocationTable> TeacherAllocationTables { get; set; }
    }
}
