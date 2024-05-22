namespace StudentRegistrationSystem.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using StudentRegistrationSystem.Models;
    using StudentRegistrationSystem.Data;

    public class ManagePersonnelCoursesForm : Form
    {
        private int personnelId;
        private TextBox txtName = new TextBox();
        private DataGridView dgvCourses = new DataGridView();
        private CheckedListBox clbCourses = new CheckedListBox();
        private NumericUpDown nudAcademicYear = new NumericUpDown();
        private Button btnSave = new Button();
        private Button btnCancel = new Button();

        public ManagePersonnelCoursesForm(int personnelId)
        {
            this.personnelId = personnelId;
            InitializeComponent();
            LoadPersonnelDetails();
            LoadCourses();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Manage Personnel Courses";

            Label lblName = new Label { Text = "Name:", Location = new Point(20, 20), Size = new Size(100, 20) };
            txtName.Location = new Point(130, 20);
            txtName.Size = new Size(200, 20);
            txtName.ReadOnly = true;

            Label lblCourses = new Label { Text = "Assigned Courses:", Location = new Point(20, 60), Size = new Size(120, 20) };
            dgvCourses.Location = new Point(20, 90);
            dgvCourses.Size = new Size(740, 200);
            dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Label lblAssignCourses = new Label { Text = "Assign Courses:", Location = new Point(20, 310), Size = new Size(120, 20) };
            clbCourses.Location = new Point(20, 340);
            clbCourses.Size = new Size(340, 150);

            Label lblAcademicYear = new Label { Text = "Academic Year:", Location = new Point(400, 340), Size = new Size(100, 20) };
            nudAcademicYear.Location = new Point(500, 340);
            nudAcademicYear.Minimum = 2024;
            nudAcademicYear.Maximum = DateTime.Now.Year + 10;

            btnSave.Text = "Save";
            btnSave.Location = new Point(500, 400);
            btnSave.Size = new Size(100, 30);
            btnSave.Click += btnSave_Click;

            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(620, 400);
            btnCancel.Size = new Size(100, 30);
            btnCancel.Click += (sender, e) => { this.Close(); };

            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblCourses);
            this.Controls.Add(dgvCourses);
            this.Controls.Add(lblAssignCourses);
            this.Controls.Add(clbCourses);
            this.Controls.Add(lblAcademicYear);
            this.Controls.Add(nudAcademicYear);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void LoadPersonnelDetails()
        {
            // Fetch personnel details and display the name
            List<AcademicPersonnel> personnelList = DatabaseHelper.GetAcademicPersonnel();
            AcademicPersonnel personnel = personnelList.Find(p => p.InstructorID == personnelId);
            if (personnel != null)
            {
                txtName.Text = personnel.Name;
            }
        }

        private void LoadCourses()
        {
            List<Course> allCourses = DatabaseHelper.GetCourses();
            List<Course> assignedCourses = DatabaseHelper.GetCoursesByInstructor(personnelId);

            clbCourses.Items.Clear();
            dgvCourses.DataSource = assignedCourses;

            foreach (var course in allCourses)
            {
                clbCourses.Items.Add(course, assignedCourses.Exists(ac => ac.CourseID == course.CourseID));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<int> selectedCourseIds = new List<int>();
            foreach (Course course in clbCourses.CheckedItems)
            {
                selectedCourseIds.Add(course.CourseID);
            }

            int academicYear = (int)nudAcademicYear.Value;

            DatabaseHelper.AssignCoursesToInstructor(personnelId, selectedCourseIds, academicYear);
            LoadCourses(); // Refresh the assigned courses list
        }
    }
}
