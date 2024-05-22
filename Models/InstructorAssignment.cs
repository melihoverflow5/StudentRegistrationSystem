namespace StudentRegistrationSystem.Models
{
    public class InstructorAssignment
    {
        public int AssignmentID { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public int AcademicYear { get; set; }

        // Navigation properties
        public AcademicPersonnel Instructor { get; set; }
        public Course Course { get; set; }
    }
}