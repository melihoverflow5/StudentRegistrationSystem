namespace StudentRegistrationSystem.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }

        public override string ToString()
        {
            return CourseName;
        }
    }
}