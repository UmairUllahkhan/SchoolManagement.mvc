using SchoolManagement.mvc.DataDB;

namespace SchoolManagement.mvc.Models
{
    public class AssignTeacherViewModel
    {
        public CourseOfferedTable? CourseOfferedTable { get; set; }

        public List<Assign> AssignTeacher { get; set; } = new List<Assign>();

    }
}
