namespace StudentRegistrationSystem.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using StudentRegistrationSystem.Data;
    using StudentRegistrationSystem.Models;

    public class StudentForm : Form
    {
        DataGridView dataGridView1 = new DataGridView();
        TextBox txtSearch = new TextBox();
        Button btnAdd = new Button();
        Button btnEdit = new Button();
        Button btnDelete = new Button();
        Button btnRegisterCourses = new Button(); // Add Register Courses button
        Label lblSearch = new Label();

        public StudentForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Form properties
            this.Size = new Size(800, 450);
            this.Text = "Student Management";
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

            // Register Courses Button
            btnRegisterCourses.Text = "Register Courses";
            btnRegisterCourses.Location = new Point(350, 360);
            btnRegisterCourses.Size = new Size(150, 30);
            btnRegisterCourses.Click += btnRegisterCourses_Click;

            // Adding controls to the form
            this.Controls.Add(lblSearch);
            this.Controls.Add(txtSearch);
            this.Controls.Add(dataGridView1);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnRegisterCourses);

            LoadData(); // Load initial data
        }

        private void LoadData(string filter = "")
        {
            List<Student> studentList = DatabaseHelper.GetStudents(filter);
            dataGridView1.DataSource = studentList;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditStudentForm addForm = new AddEditStudentForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // Refresh the list
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Student selectedStudent = (Student)dataGridView1.CurrentRow.DataBoundItem;
                AddEditStudentForm editForm = new AddEditStudentForm(selectedStudent);
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
                Student selectedStudent = (Student)dataGridView1.CurrentRow.DataBoundItem;
                DatabaseHelper.DeleteStudent(selectedStudent.StudentID);
                LoadData(); // Refresh the list
            }
        }

        private void btnRegisterCourses_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Student selectedStudent = (Student)dataGridView1.CurrentRow.DataBoundItem;
                CourseRegistrationForm registrationForm = new CourseRegistrationForm(selectedStudent.StudentID);
                registrationForm.ShowDialog();
            }
        }
    }
}
