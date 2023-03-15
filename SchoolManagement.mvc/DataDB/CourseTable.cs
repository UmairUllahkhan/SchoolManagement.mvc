using System;
using System.Collections.Generic;

namespace SchoolManagement.mvc.DataDB
{
    public partial class CourseTable
    {
        public CourseTable()
        {
            CourseOfferedTables = new HashSet<CourseOfferedTable>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string? ShortName { get; set; }

        public virtual ICollection<CourseOfferedTable> CourseOfferedTables { get; set; }
    }
}
