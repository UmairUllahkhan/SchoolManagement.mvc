using System;
using System.Collections.Generic;

namespace SchoolManagement.mvc.DataDB
{
    public partial class StudentTable
    {
        public StudentTable()
        {
            AttendenceDetails = new HashSet<AttendenceDetail>();
            StudentEnrollmentTables = new HashSet<StudentEnrollmentTable>();
        }

        public int StudentRegId { get; set; }
        public string? StudentName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MobileNo { get; set; } = null!;

        public virtual ICollection<AttendenceDetail> AttendenceDetails { get; set; }
        public virtual ICollection<StudentEnrollmentTable> StudentEnrollmentTables { get; set; }
    }
}
