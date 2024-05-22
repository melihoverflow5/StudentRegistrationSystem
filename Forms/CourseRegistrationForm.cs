namespace StudentRegistrationSystem.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using StudentRegistrationSystem.Models;
    using StudentRegistrationSystem.Data;

    public class CourseRegistrationForm : Form
    {
        private int studentId;
        private TextBox txtName = new TextBox();
        private DataGridView dgvRegisteredCourses = new DataGridView();
        private CheckedListBox clbAvailableCourses = new CheckedListBox();
        private Button btnSave = new Button();
        private Button btnCancel = new Button();

        public CourseRegistrationForm(int studentId)
        {
            this.studentId = studentId;
            InitializeComponent();
            LoadStudentDetails();
            LoadCourses();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Course Registration";

            Label lblName = new Label { Text = "Name:", Location = new Point(20, 20), Size = new Size(100, 20) };
            txtName.Location = new Point(130, 20);
            txtName.Size = new Size(200, 20);
            txtName.ReadOnly = true;

            Label lblRegisteredCourses = new Label { Text = "Registered Courses:", Location = new Point(20, 60), Size = new Size(140, 20) };
            dgvRegisteredCourses.Location = new Point(20, 90);
            dgvRegisteredCourses.Size = new Size(740, 200);
            dgvRegisteredCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Label lblAvailableCourses = new Label { Text = "Available Courses:", Location = new Point(20, 310), Size = new Size(140, 20) };
            clbAvailableCourses.Location = new Point(20, 340);
            clbAvailableCourses.Size = new Size(340, 150);

            // Set the DisplayMember to CourseName
            clbAvailableCourses.DisplayMember = "CourseName";

            btnSave.Text = "Save";
            btnSave.Location = new Point(500, 500);
            btnSave.Size = new Size(100, 30);
            btnSave.Click += btnSave_Click;

            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(620, 500);
            btnCancel.Size = new Size(100, 30);
            btnCancel.Click += (sender, e) => { this.Close(); };

            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblRegisteredCourses);
            this.Controls.Add(dgvRegisteredCourses);
            this.Controls.Add(lblAvailableCourses);
            this.Controls.Add(clbAvailableCourses);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void LoadStudentDetails()
        {
            List<Student> students = DatabaseHelper.GetStudents();
            Student student = students.Find(s => s.StudentID == studentId);
            if (student != null)
            {
                txtName.Text = student.Name;
            }
        }

        private void LoadCourses()
        {
            List<Course> allCourses = DatabaseHelper.GetCourses();
            List<Course> registeredCourses = DatabaseHelper.GetCoursesByStudent(studentId);

            clbAvailableCourses.Items.Clear();
            dgvRegisteredCourses.DataSource = registeredCourses;

            foreach (var course in allCourses)
            {
                clbAvailableCourses.Items.Add(course, registeredCourses.Exists(rc => rc.CourseID == course.CourseID));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<int> selectedCourseIds = new List<int>();
            foreach (Course course in clbAvailableCourses.CheckedItems)
            {
                selectedCourseIds.Add(course.CourseID);
            }

            DatabaseHelper.RegisterCoursesForStudent(studentId, selectedCourseIds);
            LoadCourses(); // Refresh the registered courses list
        }
    }
}
