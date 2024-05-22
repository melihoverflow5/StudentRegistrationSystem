namespace StudentRegistrationSystem.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using StudentRegistrationSystem.Data;
    using StudentRegistrationSystem.Models;

    public class PersonnelForm : Form
    {
        DataGridView dataGridView1 = new DataGridView();
        TextBox txtSearch = new TextBox();
        Button btnAdd = new Button();
        Button btnEdit = new Button();
        Button btnDelete = new Button();
        Button btnStudents = new Button();
        Button btnAssignCourses = new Button(); // Add Assign Courses button
        Label lblSearch = new Label();

        public PersonnelForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Form properties
            this.Size = new Size(800, 450);
            this.Text = "Personnel Management";
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

            // Students Button
            btnStudents.Text = "Students";
            btnStudents.Location = new Point(350, 360);
            btnStudents.Size = new Size(100, 30);
            btnStudents.Click += btnStudents_Click;

            // Assign Courses Button
            btnAssignCourses.Text = "Assign Courses";
            btnAssignCourses.Location = new Point(460, 360);
            btnAssignCourses.Size = new Size(120, 30);
            btnAssignCourses.Click += btnAssignCourses_Click;

            // Adding controls to the form
            this.Controls.Add(lblSearch);
            this.Controls.Add(txtSearch);
            this.Controls.Add(dataGridView1);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnStudents);
            this.Controls.Add(btnAssignCourses);

            LoadData(); // Load initial data
        }

        private void LoadData(string filter = "")
        {
            List<AcademicPersonnel> personnelList = DatabaseHelper.GetAcademicPersonnel(filter);
            dataGridView1.DataSource = personnelList;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditPersonnelForm addForm = new AddEditPersonnelForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // Refresh the list
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                AcademicPersonnel selectedPersonnel = (AcademicPersonnel)dataGridView1.CurrentRow.DataBoundItem;
                AddEditPersonnelForm editForm = new AddEditPersonnelForm(selectedPersonnel);
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
                AcademicPersonnel selectedPersonnel = (AcademicPersonnel)dataGridView1.CurrentRow.DataBoundItem;
                DatabaseHelper.DeleteAcademicPersonnel(selectedPersonnel.InstructorID);
                LoadData(); // Refresh the list
            }
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                AcademicPersonnel selectedPersonnel = (AcademicPersonnel)dataGridView1.CurrentRow.DataBoundItem;
                AdvisorStudentsForm studentsForm = new AdvisorStudentsForm(selectedPersonnel.InstructorID);
                studentsForm.ShowDialog();
            }
        }

        private void btnAssignCourses_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                AcademicPersonnel selectedPersonnel = (AcademicPersonnel)dataGridView1.CurrentRow.DataBoundItem;
                ManagePersonnelCoursesForm coursesForm = new ManagePersonnelCoursesForm(selectedPersonnel.InstructorID);
                coursesForm.ShowDialog();
            }
        }
    }
}
