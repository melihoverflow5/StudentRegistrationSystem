using System;
using System.Data;
using System.Data.SqlClient;
using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem.Data{
    public static class DatabaseHelper
    {
        private static string connectionString = "Server=.\\SQLEXPRESS; Database=UniversityDatabase; Trusted_Connection=True;";

        public static DataTable ExecuteQuery(string sql)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        public static int ExecuteNonQuery(string sql)
        {
            int affectedRows = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    affectedRows = cmd.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        public static object ExecuteScalar(string sql)
        {
            object result = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    result = cmd.ExecuteScalar();
                }
            }
            return result;
        }

         public static List<AcademicPersonnel> GetAcademicPersonnel(string filter = "")
        {
            List<AcademicPersonnel> personnelList = new List<AcademicPersonnel>();
            string sql = "SELECT * FROM AcademicPersonnel";
            if (!string.IsNullOrEmpty(filter))
            {
                sql += $" WHERE Name LIKE '%{filter}%' OR Department LIKE '%{filter}%'";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    personnelList.Add(new AcademicPersonnel
                    {
                        InstructorID = reader.GetInt32(reader.GetOrdinal("InstructorID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Department = reader.GetString(reader.GetOrdinal("Department")),
                        Salary = reader.GetDecimal(reader.GetOrdinal("Salary"))
                    });
                }
            }
            return personnelList;
        }

        public static void SaveAcademicPersonnel(AcademicPersonnel personnel)
        {
            string sql;
            if (personnel.InstructorID == 0) // New record
            {
                sql = "INSERT INTO AcademicPersonnel (Name, Department, Salary) VALUES (@Name, @Department, @Salary)";
            }
            else // Update existing record
            {
                sql = "UPDATE AcademicPersonnel SET Name = @Name, Department = @Department, Salary = @Salary WHERE InstructorID = @InstructorID";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", personnel.Name);
                cmd.Parameters.AddWithValue("@Department", personnel.Department);
                cmd.Parameters.AddWithValue("@Salary", personnel.Salary);
                if (personnel.InstructorID != 0)
                {
                    cmd.Parameters.AddWithValue("@InstructorID", personnel.InstructorID);
                }
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteAcademicPersonnel(int instructorId)
        {
            string sql = "DELETE FROM AcademicPersonnel WHERE InstructorID = @InstructorID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@InstructorID", instructorId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Student> GetStudents(string filter = "")
        {
            List<Student> studentList = new List<Student>();
            string sql = "SELECT * FROM Students";
            if (!string.IsNullOrEmpty(filter))
            {
                sql += $" WHERE Name LIKE '%{filter}%' OR CAST(StudentID AS VARCHAR) LIKE '%{filter}%'";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    studentList.Add(new Student
                    {
                        StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                        BloodType = reader.GetString(reader.GetOrdinal("BloodType")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gender = reader.GetString(reader.GetOrdinal("Gender")),
                        Department = reader.GetString(reader.GetOrdinal("Department")), // Load department
                        AdvisorID = reader.GetInt32(reader.GetOrdinal("AdvisorID"))
                    });
                }
            }
            return studentList;
        }

        public static void SaveStudent(Student student)
        {
            string sql;
            if (student.StudentID == 0) // New record
            {
                sql = "INSERT INTO Students (Name, Address, PhoneNumber, BloodType, DateOfBirth, Gender, Department, AdvisorID) VALUES (@Name, @Address, @PhoneNumber, @BloodType, @DateOfBirth, @Gender, @Department, @AdvisorID)";
            }
            else // Update existing record
            {
                sql = "UPDATE Students SET Name = @Name, Address = @Address, PhoneNumber = @PhoneNumber, BloodType = @BloodType, DateOfBirth = @DateOfBirth, Gender = @Gender, Department = @Department, AdvisorID = @AdvisorID WHERE StudentID = @StudentID";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                cmd.Parameters.AddWithValue("@BloodType", student.BloodType);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@Department", student.Department); // Save department
                cmd.Parameters.AddWithValue("@AdvisorID", student.AdvisorID);
                if (student.StudentID != 0)
                {
                    cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
                }
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteStudent(int studentId)
        {
            string sql = "DELETE FROM Students WHERE StudentID = @StudentID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Student> GetStudentsByAdvisor(int advisorId)
        {
            List<Student> studentList = new List<Student>();
            string sql = "SELECT * FROM Students WHERE AdvisorID = @AdvisorID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AdvisorID", advisorId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    studentList.Add(new Student
                    {
                        StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                        BloodType = reader.GetString(reader.GetOrdinal("BloodType")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gender = reader.GetString(reader.GetOrdinal("Gender")),
                        AdvisorID = reader.GetInt32(reader.GetOrdinal("AdvisorID"))
                    });
                }
            }
            return studentList;
        }

        public static List<Student> GetStudentsByCourse(int courseId)
        {
            List<Student> studentList = new List<Student>();
            string sql = "SELECT s.* FROM Students s INNER JOIN Enrollments e ON s.StudentID = e.StudentID WHERE e.CourseID = @CourseID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    studentList.Add(new Student
                    {
                        StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                        BloodType = reader.GetString(reader.GetOrdinal("BloodType")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gender = reader.GetString(reader.GetOrdinal("Gender")),
                        Department = reader.GetString(reader.GetOrdinal("Department")), // Load department
                        AdvisorID = reader.GetInt32(reader.GetOrdinal("AdvisorID"))
                    });
                }
            }
            return studentList;
        }

         public static List<Course> GetCourses(string filter = "")
        {
            List<Course> courseList = new List<Course>();
            string sql = "SELECT * FROM Courses";
            if (!string.IsNullOrEmpty(filter))
            {
                sql += $" WHERE CourseName LIKE '%{filter}%' OR CAST(CourseID AS VARCHAR) LIKE '%{filter}%'";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courseList.Add(new Course
                    {
                        CourseID = reader.GetInt32(reader.GetOrdinal("CourseID")),
                        CourseName = reader.GetString(reader.GetOrdinal("CourseName")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        CreditHours = reader.GetInt32(reader.GetOrdinal("CreditHours"))
                    });
                }
            }
            return courseList;
        }

        public static void SaveCourse(Course course)
        {
            string sql;
            if (course.CourseID == 0) // New record
            {
                sql = "INSERT INTO Courses (CourseName, Description, CreditHours) VALUES (@CourseName, @Description, @CreditHours)";
            }
            else // Update existing record
            {
                sql = "UPDATE Courses SET CourseName = @CourseName, Description = @Description, CreditHours = @CreditHours WHERE CourseID = @CourseID";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                cmd.Parameters.AddWithValue("@Description", course.Description);
                cmd.Parameters.AddWithValue("@CreditHours", course.CreditHours);
                if (course.CourseID != 0)
                {
                    cmd.Parameters.AddWithValue("@CourseID", course.CourseID);
                }
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteCourse(int courseId)
        {
            string sql = "DELETE FROM Courses WHERE CourseID = @CourseID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Course> GetCoursesByInstructor(int instructorId)
        {
            List<Course> courseList = new List<Course>();
            string sql = "SELECT c.* FROM Courses c INNER JOIN InstructorAssignments ia ON c.CourseID = ia.CourseID WHERE ia.InstructorID = @InstructorID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@InstructorID", instructorId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courseList.Add(new Course
                    {
                        CourseID = reader.GetInt32(reader.GetOrdinal("CourseID")),
                        CourseName = reader.GetString(reader.GetOrdinal("CourseName")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        CreditHours = reader.GetInt32(reader.GetOrdinal("CreditHours"))
                    });
                }
            }
            return courseList;
        }

        public static void AssignCoursesToInstructor(int instructorId, List<int> courseIds, int academicYear)
        {
            string deleteSql = "DELETE FROM InstructorAssignments WHERE InstructorID = @InstructorID";
            string insertSql = "INSERT INTO InstructorAssignments (InstructorID, CourseID, AcademicYear) VALUES (@InstructorID, @CourseID, @AcademicYear)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand deleteCmd = new SqlCommand(deleteSql, conn))
                {
                    deleteCmd.Parameters.AddWithValue("@InstructorID", instructorId);
                    deleteCmd.ExecuteNonQuery();
                }

                foreach (int courseId in courseIds)
                {
                    using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@InstructorID", instructorId);
                        insertCmd.Parameters.AddWithValue("@CourseID", courseId);
                        insertCmd.Parameters.AddWithValue("@AcademicYear", academicYear);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public static List<Course> GetCoursesByStudent(int studentId)
        {
            List<Course> courseList = new List<Course>();
            string sql = "SELECT c.* FROM Courses c INNER JOIN Enrollments e ON c.CourseID = e.CourseID WHERE e.StudentID = @StudentID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courseList.Add(new Course
                    {
                        CourseID = reader.GetInt32(reader.GetOrdinal("CourseID")),
                        CourseName = reader.GetString(reader.GetOrdinal("CourseName")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        CreditHours = reader.GetInt32(reader.GetOrdinal("CreditHours"))
                    });
                }
            }
            return courseList;
        }
        public static void RegisterCoursesForStudent(int studentId, List<int> courseIds)
        {
            string deleteSql = "DELETE FROM Enrollments WHERE StudentID = @StudentID";
            string insertSql = "INSERT INTO Enrollments (StudentID, CourseID, EnrollmentDate) VALUES (@StudentID, @CourseID, @EnrollmentDate)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand deleteCmd = new SqlCommand(deleteSql, conn))
                {
                    deleteCmd.Parameters.AddWithValue("@StudentID", studentId);
                    deleteCmd.ExecuteNonQuery();
                }

                foreach (int courseId in courseIds)
                {
                    using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@StudentID", studentId);
                        insertCmd.Parameters.AddWithValue("@CourseID", courseId);
                        insertCmd.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static List<Enrollment> GetEnrollmentsByStudent(int studentId)
        {
            List<Enrollment> enrollmentList = new List<Enrollment>();
            string sql = "SELECT * FROM Enrollments WHERE StudentID = @StudentID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    enrollmentList.Add(new Enrollment
                    {
                        EnrollmentID = reader.GetInt32(reader.GetOrdinal("EnrollmentID")),
                        StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                        CourseID = reader.GetInt32(reader.GetOrdinal("CourseID")),
                        EnrollmentDate = reader.GetDateTime(reader.GetOrdinal("EnrollmentDate")),
                        Grade = reader.IsDBNull(reader.GetOrdinal("Grade")) ? null : reader.GetString(reader.GetOrdinal("Grade"))
                    });
                }
            }
            return enrollmentList;
        }

        public static List<AcademicPersonnel> GetInstructorsByCourse(int courseId)
        {
            List<AcademicPersonnel> instructorList = new List<AcademicPersonnel>();
            string sql = "SELECT ap.* FROM AcademicPersonnel ap INNER JOIN InstructorAssignments ia ON ap.InstructorID = ia.InstructorID WHERE ia.CourseID = @CourseID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    instructorList.Add(new AcademicPersonnel
                    {
                        InstructorID = reader.GetInt32(reader.GetOrdinal("InstructorID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Department = reader.GetString(reader.GetOrdinal("Department")),
                        Salary = reader.GetDecimal(reader.GetOrdinal("Salary"))
                    });
                }
            }
            return instructorList;
        }

    }
}