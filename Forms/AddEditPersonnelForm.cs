namespace StudentRegistrationSystem.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using StudentRegistrationSystem.Models;
    using StudentRegistrationSystem.Data;

    public class AddEditPersonnelForm : Form
    {
        TextBox txtName = new TextBox();
        ComboBox cmbDepartment = new ComboBox();
        TextBox txtSalary = new TextBox();
        Button btnSave = new Button();
        Button btnCancel = new Button();

        private AcademicPersonnel personnel;

        public AddEditPersonnelForm(AcademicPersonnel personnel = null)
        {
            this.personnel = personnel ?? new AcademicPersonnel();
            InitializeComponent();
            if (personnel != null)
            {
                LoadPersonnelDetails();
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
            Label lblName = new Label { Text = "Name", Location = new Point(10, 20), Size = new Size(80, 20) };
            txtName.Location = new Point(100, 20);
            txtName.Size = new Size(170, 20);

            Label lblDepartment = new Label { Text = "Department", Location = new Point(10, 50), Size = new Size(80, 20) };
            cmbDepartment.Location = new Point(100, 50);
            cmbDepartment.Size = new Size(170, 20);
            LoadDepartments();

            Label lblSalary = new Label { Text = "Salary", Location = new Point(10, 80), Size = new Size(80, 20) };
            txtSalary.Location = new Point(100, 80);
            txtSalary.Size = new Size(170, 20);

            btnSave.Text = "Save";
            btnSave.Location = new Point(30, 130);
            btnSave.Size = new Size(100, 30);
            btnSave.Click += btnSave_Click;

            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(160, 130);
            btnCancel.Size = new Size(100, 30);
            btnCancel.Click += (sender, e) => { this.Close(); };

            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblDepartment);
            Controls.Add(cmbDepartment);
            Controls.Add(lblSalary);
            Controls.Add(txtSalary);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
        }

        private void LoadDepartments()
        {
            // Example departments, you may load this from a table or config file
            string[] departments = { "Computer Science", "Mathematics", "Physics", "Chemistry", "Biology", "Engineering" };
            cmbDepartment.Items.AddRange(departments);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            personnel.Name = txtName.Text;
            personnel.Department = cmbDepartment.SelectedItem.ToString();
            personnel.Salary = decimal.Parse(txtSalary.Text);

            DatabaseHelper.SaveAcademicPersonnel(personnel);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LoadPersonnelDetails()
        {
            txtName.Text = personnel.Name;
            cmbDepartment.SelectedItem = personnel.Department;
            txtSalary.Text = personnel.Salary.ToString();
        }
    }
}
