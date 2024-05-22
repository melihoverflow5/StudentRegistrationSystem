namespace StudentRegistrationSystem.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using StudentRegistrationSystem.Data;
    using StudentRegistrationSystem.Models;

    public class AdvisorStudentsForm : Form
    {
        DataGridView dataGridView1 = new DataGridView();
        int advisorId;

        public AdvisorStudentsForm(int advisorId)
        {
            this.advisorId = advisorId;
            InitializeComponents();
            LoadData();
        }

        private void InitializeComponents()
        {
            // Form properties
            this.Size = new Size(800, 450);
            this.Text = "Students of Advisor";
            this.StartPosition = FormStartPosition.CenterScreen;

            // DataGridView setup
            dataGridView1.Location = new Point(20, 20);
            dataGridView1.Size = new Size(740, 400);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            // Adding controls to the form
            this.Controls.Add(dataGridView1);
        }

        private void LoadData()
        {
            List<Student> studentList = DatabaseHelper.GetStudentsByAdvisor(advisorId);
            dataGridView1.DataSource = studentList;
        }
    }
}
