using System;
using System.Collections.Generic;

namespace SchoolManagement.mvc.DataDB
{
    public partial class CourseOfferedTable
    {
        public CourseOfferedTable()
        {
            AttendenceTables = new HashSet<AttendenceTable>();
            StudentEnrollmentTables = new HashSet<StudentEnrollmentTable>();
            TeacherAllocationTables = new HashSet<TeacherAllocationTable>();
        }

        public int CoId { get; set; }
        public int? CourseId { get; set; }
        /// <summary>
        /// Time Slot ID
        /// </summary>
        public int? TsId { get; set; }

        public virtual CourseTable? Course { get; set; }
        public virtual ICollection<AttendenceTable> AttendenceTables { get; set; }
        public virtual ICollection<StudentEnrollmentTable> StudentEnrollmentTables { get; set; }
        public virtual ICollection<TeacherAllocationTable> TeacherAllocationTables { get; set; }
    }
}
