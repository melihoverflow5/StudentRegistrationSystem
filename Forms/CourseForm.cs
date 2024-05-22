namespace StudentRegistrationSystem.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using StudentRegistrationSystem.Data;
    using StudentRegistrationSystem.Models;

    public class CourseForm : Form
    {
        DataGridView dataGridView1 = new DataGridView();
        TextBox txtSearch = new TextBox();
        Button btnAdd = new Button();
        Button btnEdit = new Button();
        Button btnDelete = new Button();
        Button btnViewEnrollments = new Button(); // Add View Enrollments button
        Button btnViewInstructors = new Button(); // Add View Instructors button
        Label lblSearch = new Label();

        public CourseForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Form properties
            this.Size = new Size(800, 450);
            this.Text = "Course Management";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Label for search
            lblSearch.Text = "Search:";
            lblSearch.Location = new Point(20, 20);
            lblSearch.Size = new Size(50, 20);

            // Search TextBox
            txtSearch.Location = new Point(80, 20);
            txtSearch.Size = new Size(200, 20);
            txtSearch.TextChanged += txtSearch_TextChanged;

            // DataGridView setup
            dataGridView1.Location = new Point(20, 50);
            dataGridView1.Size = new Size(740, 300);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            // Add Button
            btnAdd.Text = "Add";
            btnAdd.Location = new Point(20, 360);
            btnAdd.Size = new Size(100, 30);
            btnAdd.Click += btnAdd_Click;

            // Edit Button
            btnEdit.Text = "Edit";
            btnEdit.Location = new Point(130, 360);
            btnEdit.Size = new Size(100, 30);
            btnEdit.Click += btnEdit_Click;

            // Delete Button
            btnDelete.Text = "Delete";
            btnDelete.Location = new Point(240, 360);
            btnDelete.Size = new Size(100, 30);
            btnDelete.Click += btnDelete_Click;

            // View Enrollments Button
            btnViewEnrollments.Text = "View Enrollments";
            btnViewEnrollments.Location = new Point(350, 360);
            btnViewEnrollments.Size = new Size(150, 30);
            btnViewEnrollments.Click += btnViewEnrollments_Click;

            // View Instructors Button
            btnViewInstructors.Text = "View Instructors";
            btnViewInstructors.Location = new Point(510, 360);
            btnViewInstructors.Size = new Size(150, 30);
            btnViewInstructors.Click += btnViewInstructors_Click;

            // Adding controls to the form
            this.Controls.Add(lblSearch);
            this.Controls.Add(txtSearch);
            this.Controls.Add(dataGridView1);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnViewEnrollments);
            this.Controls.Add(btnViewInstructors);

            LoadData(); // Load initial data
        }

        private void LoadData(string filter = "")
        {
            List<Course> courseList = DatabaseHelper.GetCourses(filter);
            dataGridView1.DataSource = courseList;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditCourseForm addForm = new AddEditCourseForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // Refresh the list
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Course selectedCourse = (Course)dataGridView1.CurrentRow.DataBoundItem;
                AddEditCourseForm editForm = new AddEditCourseForm(selectedCourse);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh the list
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Course selectedCourse = (Course)dataGridView1.CurrentRow.DataBoundItem;
                DatabaseHelper.DeleteCourse(selectedCourse.CourseID);
                LoadData(); // Refresh the list
            }
        }

        private void btnViewEnrollments_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Course selectedCourse = (Course)dataGridView1.CurrentRow.DataBoundItem;
                CourseEnrollmentsForm enrollmentsForm = new CourseEnrollmentsForm(selectedCourse.CourseID);
                enrollmentsForm.ShowDialog();
            }
        }

        private void btnViewInstructors_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Course selectedCourse = (Course)dataGridView1.CurrentRow.DataBoundItem;
                CourseInstructorsForm instructorsForm = new CourseInstructorsForm(selectedCourse.CourseID);
                instructorsForm.ShowDialog();
            }
        }
    }
}
