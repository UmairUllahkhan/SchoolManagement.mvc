using System;
using System.Collections.Generic;

namespace SchoolManagement.mvc.DataDB
{
    public partial class AttendenceTable
    {
        public AttendenceTable()
        {
            AttendenceDetails = new HashSet<AttendenceDetail>();
        }

        public int AmId { get; set; }
        /// <summary>
        /// Course Offered ID
        /// </summary>
        public int CoId { get; set; }
        /// <summary>
        /// DD/MM/YYYY
        /// </summary>
        public DateTime Date { get; set; }

        public virtual CourseOfferedTable Co { get; set; } = null!;
        public virtual ICollection<AttendenceDetail> AttendenceDetails { get; set; }
    }
}
