using System;
using System.Collections.Generic;

namespace SchoolManagement.mvc.DataDB
{
    public partial class AttendenceDetail
    {
        public int AdId { get; set; }
        /// <summary>
        /// Attendence ID
        /// </summary>
        public int? AmId { get; set; }
        public int? StudentId { get; set; }
        public short? IsPresent { get; set; }

        public virtual AttendenceTable? Am { get; set; }
        public virtual StudentTable? Student { get; set; }
    }
}
