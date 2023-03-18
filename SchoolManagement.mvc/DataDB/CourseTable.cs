using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.mvc.DataDB
{
    public partial class CourseTable
    {
        public CourseTable()
        {
            CourseOfferedTables = new HashSet<CourseOfferedTable>();
        }

        
        public int CourseId { get; set; }
        [Display(Name ="Course Name")]
        public string CourseName { get; set; } = null!;
        [Display(Name = "Course Code")]
        public string? ShortName { get; set; }

        public virtual ICollection<CourseOfferedTable> CourseOfferedTables { get; set; }
    }
}
