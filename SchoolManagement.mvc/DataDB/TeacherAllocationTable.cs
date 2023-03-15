using System;
using System.Collections.Generic;

namespace SchoolManagement.mvc.DataDB
{
    public partial class TeacherAllocationTable
    {
        public int TaId { get; set; }
        /// <summary>
        /// Course Offered ID
        /// </summary>
        public int? CoId { get; set; }
        /// <summary>
        /// Teacher ID
        /// </summary>
        public int? TId { get; set; }

        public virtual CourseOfferedTable? Co { get; set; }
        public virtual TeacherTable? TIdNavigation { get; set; }
    }
}
