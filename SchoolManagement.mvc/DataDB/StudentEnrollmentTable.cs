using System;
using System.Collections.Generic;

namespace SchoolManagement.mvc.DataDB
{
    public partial class StudentEnrollmentTable
    {
        public int SeId { get; set; }
        /// <summary>
        /// Student Id
        /// </summary>
        public int? StuId { get; set; }
        /// <summary>
        /// Course Offer ID
        /// </summary>
        public int? CoId { get; set; }

        public virtual CourseOfferedTable? Co { get; set; }
        public virtual StudentTable? Stu { get; set; }
    }
}
