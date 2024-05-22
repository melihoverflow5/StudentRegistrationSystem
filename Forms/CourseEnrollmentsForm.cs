namespace StudentRegistrationSystem.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using StudentRegistrationSystem.Models;
    using StudentRegistrationSystem.Data;

    public class CourseEnrollmentsForm : Form
    {
        private int courseId;
        private DataGridView dataGridView1 = new DataGridView();

        public CourseEnrollmentsForm(int courseId)
        {
            this.courseId = courseId;
            InitializeComponent();
            LoadEnrollments();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(800, 450);
            this.Text = "Course Enrollments";
            this.StartPosition = FormStartPosition.CenterParent;

            dataGridView1.Location = new Point(20, 20);
            dataGridView1.Size = new Size(740, 380);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            this.Controls.Add(dataGridView1);
        }

        private void LoadEnrollments()
        {
            List<Student> students = DatabaseHelper.GetStudentsByCourse(courseId);
            dataGridView1.DataSource = students;
        }
    }
}
