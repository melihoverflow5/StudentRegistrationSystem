namespace StudentRegistrationSystem.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using StudentRegistrationSystem.Models;
    using StudentRegistrationSystem.Data;

    public class AddEditStudentForm : Form
    {
        TextBox txtName = new TextBox();
        TextBox txtAddress = new TextBox();
        TextBox txtPhoneNumber = new TextBox();
        ComboBox cmbBloodType = new ComboBox();
        DateTimePicker datePickerDOB = new DateTimePicker();
        ComboBox cmbGender = new ComboBox();
        ComboBox cmbDepartment = new ComboBox(); // Add ComboBox for Department
        ComboBox cmbAdvisor = new ComboBox();
        Button btnSave = new Button();
        Button btnCancel = new Button();

        private Student student;

        public AddEditStudentForm(Student student = null)
        {
            this.student = student ?? new Student();
            InitializeComponent();
            if (student != null)
            {
                LoadStudentDetails();
            }
        }

        private void InitializeComponent()
        {
            this.Size = new Size(300, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            SetupFormControls();
        }

        private void SetupFormControls()
        {
            Label lblName = new Label { Text = "Name", Location = new Point(10, 20), Size = new Size(80, 20) };
            txtName.Location = new Point(100, 20);
            txtName.Size = new Size(170, 20);

            Label lblAddress = new Label { Text = "Address", Location = new Point(10, 50), Size = new Size(80, 20) };
            txtAddress.Location = new Point(100, 50);
            txtAddress.Size = new Size(170, 20);

            Label lblPhoneNumber = new Label { Text = "Phone", Location = new Point(10, 80), Size = new Size(80, 20) };
            txtPhoneNumber.Location = new Point(100, 80);
            txtPhoneNumber.Size = new Size(170, 20);

            Label lblBloodType = new Label { Text = "Blood Type", Location = new Point(10, 110), Size = new Size(80, 20) };
            cmbBloodType.Location = new Point(100, 110);
            cmbBloodType.Size = new Size(170, 20);
            cmbBloodType.Items.AddRange(new string[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" });

            Label lblDOB = new Label { Text = "Date of Birth", Location = new Point(10, 140), Size = new Size(80, 20) };
            datePickerDOB.Location = new Point(100, 140);
            datePickerDOB.Size = new Size(170, 20);

            Label lblGender = new Label { Text = "Gender", Location = new Point(10, 170), Size = new Size(80, 20) };
            cmbGender.Location = new Point(100, 170);
            cmbGender.Size = new Size(170, 20);
            cmbGender.Items.AddRange(new string[] { "M", "F", "Other" });

            Label lblDepartment = new Label { Text = "Department", Location = new Point(10, 200), Size = new Size(80, 20) };
            cmbDepartment.Location = new Point(100, 200);
            cmbDepartment.Size = new Size(170, 20);
            cmbDepartment.Items.AddRange(new string[] { "Computer Science", "Mathematics", "Physics", "Chemistry", "Biology" }); // Example departments

            Label lblAdvisor = new Label { Text = "Advisor", Location = new Point(10, 230), Size = new Size(80, 20) };
            cmbAdvisor.Location = new Point(100, 230);
            cmbAdvisor.Size = new Size(170, 20);
            LoadAdvisors();

            btnSave.Text = "Save";
            btnSave.Location = new Point(30, 270);
            btnSave.Size = new Size(100, 30);
            btnSave.Click += btnSave_Click;

            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(160, 270);
            btnCancel.Size = new Size(100, 30);
            btnCancel.Click += (sender, e) => { this.Close(); };

            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblAddress);
            Controls.Add(txtAddress);
            Controls.Add(lblPhoneNumber);
            Controls.Add(txtPhoneNumber);
            Controls.Add(lblBloodType);
            Controls.Add(cmbBloodType);
            Controls.Add(lblDOB);
            Controls.Add(datePickerDOB);
            Controls.Add(lblGender);
            Controls.Add(cmbGender);
            Controls.Add(lblDepartment);
            Controls.Add(cmbDepartment);
            Controls.Add(lblAdvisor);
            Controls.Add(cmbAdvisor);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
        }

        private void LoadAdvisors()
        {
            List<AcademicPersonnel> advisors = DatabaseHelper.GetAcademicPersonnel();
            cmbAdvisor.DataSource = advisors;
            cmbAdvisor.DisplayMember = "Name";
            cmbAdvisor.ValueMember = "InstructorID";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            student.Name = txtName.Text;
            student.Address = txtAddress.Text;
            student.PhoneNumber = txtPhoneNumber.Text;
            student.BloodType = cmbBloodType.SelectedItem.ToString();
            student.DateOfBirth = datePickerDOB.Value;
            student.Gender = cmbGender.SelectedItem.ToString();
            student.Department = cmbDepartment.SelectedItem.ToString(); // Save department
            student.AdvisorID = (int)cmbAdvisor.SelectedValue;

            DatabaseHelper.SaveStudent(student);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LoadStudentDetails()
        {
            txtName.Text = student.Name;
            txtAddress.Text = student.Address;
            txtPhoneNumber.Text = student.PhoneNumber;
            cmbBloodType.SelectedItem = student.BloodType;
            datePickerDOB.Value = student.DateOfBirth;
            cmbGender.SelectedItem = student.Gender;
            cmbDepartment.SelectedItem = student.Department; // Load department
            cmbAdvisor.SelectedValue = student.AdvisorID;
        }
    }
}
