namespace StudentRegistrationSystem.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using StudentRegistrationSystem.Models;
    using StudentRegistrationSystem.Data;

    public class AddEditCourseForm : Form
    {
        TextBox txtCourseName = new TextBox();
        TextBox txtDescription = new TextBox();
        NumericUpDown numCreditHours = new NumericUpDown();
        Button btnSave = new Button();
        Button btnCancel = new Button();

        private Course course;

        public AddEditCourseForm(Course course = null)
        {
            this.course = course ?? new Course();
            InitializeComponent();
            if (course != null)
            {
                LoadCourseDetails();
            }
        }

        private void InitializeComponent()
        {
            this.Size = new Size(300, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            SetupFormControls();
        }

        private void SetupFormControls()
        {
            Label lblCourseName = new Label { Text = "Course Name", Location = new Point(10, 20), Size = new Size(80, 20) };
            txtCourseName.Location = new Point(100, 20);
            txtCourseName.Size = new Size(170, 20);

            Label lblDescription = new Label { Text = "Description", Location = new Point(10, 50), Size = new Size(80, 20) };
            txtDescription.Location = new Point(100, 50);
            txtDescription.Size = new Size(170, 20);

            Label lblCreditHours = new Label { Text = "Credit Hours", Location = new Point(10, 80), Size = new Size(80, 20) };
            numCreditHours.Location = new Point(100, 80);
            numCreditHours.Size = new Size(170, 20);
            numCreditHours.Minimum = 1;
            numCreditHours.Maximum = 6;

            btnSave.Text = "Save";
            btnSave.Location = new Point(30, 130);
            btnSave.Size = new Size(100, 30);
            btnSave.Click += btnSave_Click;

            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(160, 130);
            btnCancel.Size = new Size(100, 30);
            btnCancel.Click += (sender, e) => { this.Close(); };

            Controls.Add(lblCourseName);
            Controls.Add(txtCourseName);
            Controls.Add(lblDescription);
            Controls.Add(txtDescription);
            Controls.Add(lblCreditHours);
            Controls.Add(numCreditHours);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            course.CourseName = txtCourseName.Text;
            course.Description = txtDescription.Text;
            course.CreditHours = (int)numCreditHours.Value;

            DatabaseHelper.SaveCourse(course);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LoadCourseDetails()
        {
            txtCourseName.Text = course.CourseName;
            txtDescription.Text = course.Description;
            numCreditHours.Value = course.CreditHours;
        }
    }
}
