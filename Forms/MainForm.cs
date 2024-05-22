using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentRegistrationSystem.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "University Registration System";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(600, 400);

            // FlowLayoutPanel for Buttons
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(20),
                AutoSize = true,
                WrapContents = false
            };
            this.Controls.Add(buttonPanel);

            // Manage Students Button
            Button btnManageStudents = new Button
            {
                Text = "Manage Students",
                Size = new Size(200, 50),
                Margin = new Padding(20)
            };
            btnManageStudents.Click += btnManageStudents_Click;
            buttonPanel.Controls.Add(btnManageStudents);

            // Manage Courses Button
            Button btnManageCourses = new Button
            {
                Text = "Manage Courses",
                Size = new Size(200, 50),
                Margin = new Padding(20)
            };
            btnManageCourses.Click += btnManageCourses_Click;
            buttonPanel.Controls.Add(btnManageCourses);

            // Manage Personnel Button
            Button btnManagePersonnel = new Button
            {
                Text = "Manage Academic Personnel",
                Size = new Size(200, 50),
                Margin = new Padding(20)
            };
            btnManagePersonnel.Click += btnManagePersonnel_Click;
            buttonPanel.Controls.Add(btnManagePersonnel);
        }

        private void btnManageStudents_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = new StudentForm();
            studentForm.Show();
        }

        private void btnManageCourses_Click(object sender, EventArgs e)
        {
            CourseForm courseForm = new CourseForm();
            courseForm.Show();
        }

        private void btnManagePersonnel_Click(object sender, EventArgs e)
        {
            PersonnelForm personnelForm = new PersonnelForm();
            personnelForm.Show();
        }
    }
}
