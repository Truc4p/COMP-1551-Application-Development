using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CW.PersonDAL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CW
{
    public partial class Form1 : Form
    {
        // Boolean flags to track changes
        private bool isNameChanged = false;
        private bool isTelephoneChanged = false;
        private bool isEmailChanged = false;
        private bool isRoleChanged = false;

        private bool isSalaryChanged = false;
        private bool isSubject1Changed = false;
        private bool isSubject2Changed = false;

        private bool isSalaryAChanged = false;
        private bool isEmploymentTypeChanged = false;
        private bool isWorkingHoursChanged = false;

        private bool isCurrentSubject1Changed = false;
        private bool isCurrentSubject2Changed = false;
        private bool isPreviousSubject1Changed = false;
        private bool isPreviousSubject2Changed = false;

        public Form1()
        {
            InitializeComponent();
            this.PersonDGV.SelectionChanged += new System.EventHandler(this.PersonDGV_SelectionChanged);

            // Attach event handlers to TextChanged events
            nameTB.TextChanged += nameTB_TextChanged;
            telephoneTB.TextChanged += telephoneTB_TextChanged;
            emailTB.TextChanged += emailTB_TextChanged;

            teacherLb.TextChanged += teacherLb_TextChanged;
            adminLb.TextChanged += teacherLb_TextChanged;
            stulb.TextChanged += teacherLb_TextChanged;

            salaryTB.TextChanged += salaryTB_TextChanged;
            subject1TB.TextChanged += subject1TB_TextChanged;
            subject2TB.TextChanged += subject2TB_TextChanged;

            salaryAtb.TextChanged += salaryAtb_TextChanged;
            emptyptb.TextChanged += emptyptb_TextChanged;
            worhoutb.TextChanged += worhoutb_TextChanged;

            currentsubject1tb.TextChanged += currentsubject1tb_TextChanged;
            currentsubject2tb.TextChanged += currentsubject2tb_TextChanged;
            previoussubject1tb.TextChanged += previoussubject1tb_TextChanged;
            previoussubject2tb.TextChanged += previoussubject2tb_TextChanged;
        }

        // Event handler methods
        private void nameTB_TextChanged(object sender, EventArgs e) => isNameChanged = true;
        private void telephoneTB_TextChanged(object sender, EventArgs e) => isTelephoneChanged = true;
        private void emailTB_TextChanged(object sender, EventArgs e) => isEmailChanged = true;

        private void teacherLb_TextChanged(object sender, EventArgs e) => isRoleChanged = true;
        private void adminLb_TextChanged(object sender, EventArgs e) => isRoleChanged = true;
        private void studentLb_TextChanged(object sender, EventArgs e) => isRoleChanged = true;


        private void salaryTB_TextChanged(object sender, EventArgs e) => isSalaryChanged = true;
        private void subject1TB_TextChanged(object sender, EventArgs e) => isSubject1Changed = true;
        private void subject2TB_TextChanged(object sender, EventArgs e) => isSubject2Changed = true;

        private void salaryAtb_TextChanged(object sender, EventArgs e) => isSalaryChanged = true;
        private void emptyptb_TextChanged(object sender, EventArgs e) => isEmploymentTypeChanged = true;
        private void workhoutb_TextChanged(object sender, EventArgs e) => isWorkingHoursChanged = true;

        private void currentsubject1tb_TextChanged(object sender, EventArgs e) => isCurrentSubject1Changed = true;
        private void currentsubject2tb_TextChanged(object sender, EventArgs e) => isCurrentSubject2Changed = true;
        private void previoussubject1tb_TextChanged(object sender, EventArgs e) => isPreviousSubject1Changed = true;
        private void previoussubject2tb_TextChanged(object sender, EventArgs e) => isPreviousSubject2Changed = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                PersonBLL p = new PersonBLL();
                this.PersonDGV.DataSource = p.GetPersons();

            }
            catch
            {
                MessageBox.Show("Error Occurred");
            }
        }

        private void PersonDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (PersonDGV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = PersonDGV.SelectedRows[0];

                // Access the data in the selected row
                var personId = selectedRow.Cells["PersonId"].Value;
                var personRole = selectedRow.Cells["Role"].Value;

                PersonBLL p = new PersonBLL();
                this.PersonDGV.DataSource = p.GetPersonInfo(personId, personRole);
            }
        }

        


        

        private void LoadDataInfoBtn_Click(object sender, EventArgs e)
        {
            try
            {
                PersonBLL p = new PersonBLL();
     
                if (this.LoadDataInfoTxtBox.Text.All(Char.IsDigit))
                    { 

                    this.PersonDGV.DataSource = p.GetPerson(Convert.ToInt16(this.LoadDataInfoTxtBox.Text)); // search ID 
                }
                else
                {
                    this.PersonDGV.DataSource = p.GetPersons(this.LoadDataInfoTxtBox.Text); // search by name
                }
            }
            catch
            {
                MessageBox.Show("Error Occurred");
            }
        }

        private void ViewAllDataBtn_Click(object sender, EventArgs e)
        {
            try
            {
                PersonBLL p = new PersonBLL();
                this.PersonDGV.DataSource = p.GetPersons();
            }
            catch
            {
                MessageBox.Show("Error Occurred");
            }
        }

        private void ViewTeacherBtn_Click(object sender, EventArgs e)
        {
            try
            {
                PersonBLL p = new PersonBLL();
                this.PersonDGV.DataSource = p.GetTeachers();
            }
            catch
            {
                MessageBox.Show("Error Occurred");
            }
        }

        private void ViewAdminBtn_Click(object sender, EventArgs e)
        {
            try
            {
                PersonBLL p = new PersonBLL();
                this.PersonDGV.DataSource = p.GetAdmins();
            }
            catch
            {
                MessageBox.Show("Error Occurred");
            }
        }

        private void ViewStudentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                PersonBLL p = new PersonBLL();
                this.PersonDGV.DataSource = p.GetStudents();
            }
            catch
            {
                MessageBox.Show("Error Occurred");
            }
        }

        

        private void AddTeacherBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string name = nameTB.Text;
                string telephone = telephoneTB.Text;
                string email = emailTB.Text;
                string role = teacherLb.Text;
                decimal salary = Convert.ToDecimal(salaryTB.Text);
                string subject1 = subject1TB.Text;
                string subject2 = subject2TB.Text;

                PersonBLL p = new PersonBLL();
                int personId = p.AddPerson(name, telephone, email, role); // Add person and get the PersonId
                p.AddTeacher(personId, salary, subject1, subject2); // Add teacher with the PersonId

                this.PersonDGV.DataSource = p.GetTeachers();

                MessageBox.Show("Teacher added successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }
        
        private void AddAdminBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string name = nameTB.Text;
                string telephone = telephoneTB.Text;
                string email = emailTB.Text;
                string role = adminLb.Text;
                decimal salary = Convert.ToDecimal(salaryAtb.Text);
                string employmenttype = emptyptb.Text;
                decimal workinghours = Convert.ToDecimal(worhoutb.Text);

                PersonBLL p = new PersonBLL();
                int personId = p.AddPerson(name, telephone, email, role); // Add person and get the PersonId
                p.AddAdmin(personId, salary, employmenttype, workinghours); // Add admin with the PersonId

                this.PersonDGV.DataSource = p.GetAdmins();

                MessageBox.Show("Admin added successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }

        private void addStudentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string name = nameTB.Text;
                string telephone = telephoneTB.Text;
                string email = emailTB.Text;
                string role = stulb.Text;
                string currentsubject1 = currentsubject1tb.Text;
                string currentsubject2 = currentsubject2tb.Text;
                string previoussubject1 = previoussubject1tb.Text;
                string previoussubject2 = previoussubject2tb.Text;

                PersonBLL p = new PersonBLL();
                int personId = p.AddPerson(name, telephone, email, role); // Add person and get the PersonId
                p.AddStudent(personId, currentsubject1, currentsubject2, previoussubject1, previoussubject2); // Add teacher with the PersonId

                this.PersonDGV.DataSource = p.GetStudents();

                MessageBox.Show("Student added successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PersonBLL p = new PersonBLL();
                this.PersonDGV.DataSource = p.GetPersons();
            }
            catch
            {
                MessageBox.Show("Error Occurred");
            }
        }

        private void LoadDataInfoTxtBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkGroupInfo_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void GroupDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void viewAfterAddDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void viewGroupDGV_CellContentClick(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void PersonDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void viewGroupDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void adminLb_Click(object sender, EventArgs e)
        {

        }

        private void worhoutb_TextChanged(object sender, EventArgs e)
        {

        }

        private void editIDtb_TextChanged(object sender, EventArgs e)
        {

        }

        private void editTeacherBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if editIDtb is empty or contains non-numeric characters
                if (string.IsNullOrWhiteSpace(editIDtb.Text) || !int.TryParse(editIDtb.Text, out int personId))
                {
                    MessageBox.Show("Please enter a valid PersonId.");
                    return;
                }

                string name = isNameChanged ? nameTB.Text : null;
                string telephone = isTelephoneChanged ? telephoneTB.Text : null;
                string email = isEmailChanged ? emailTB.Text : null;
                string role = isRoleChanged ? teacherLb.Text : null;
                decimal? salary = isSalaryChanged ? (decimal?)Convert.ToDecimal(salaryTB.Text) : null;
                string subject1 = isSubject1Changed ? subject1TB.Text : null;
                string subject2 = isSubject2Changed ? subject2TB.Text : null;

                PersonBLL p = new PersonBLL();
                p.EditPerson(personId, name, telephone, email, role); // Edit person details

                p.EditTeacher(personId, salary, subject1, subject2); // Edit teacher details

                // Check if at least one field is provided for updating
                if (name == null && telephone == null && email == null && role == null && salary == null && subject1 == null && subject2 == null)
                {
                    MessageBox.Show("No fields to update.");
                    return;
                }

                this.PersonDGV.DataSource = p.GetTeachers();

                MessageBox.Show("Teacher edited successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }

        private void editAdminBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if editIdAdminTb is empty or contains non-numeric characters
                if (string.IsNullOrWhiteSpace(editIdAdminTb.Text) || !int.TryParse(editIdAdminTb.Text, out int personId))
                {
                    MessageBox.Show("Please enter a valid PersonId.");
                    return;
                }

                string name = isNameChanged ? nameTB.Text : null;
                string telephone = isTelephoneChanged ? telephoneTB.Text : null;
                string email = isEmailChanged ? emailTB.Text : null;
                string role = isRoleChanged ? adminLb.Text : null;
                decimal? salary = isSalaryAChanged ? (decimal?)Convert.ToDecimal(salaryAtb.Text) : null;
                string employmenttype = isEmploymentTypeChanged ? emptyptb.Text : null;
                decimal? workinghours = isWorkingHoursChanged ? (decimal?)Convert.ToDecimal(worhoutb.Text) : null;

                PersonBLL p = new PersonBLL();
                p.EditPerson(personId, name, telephone, email, role); // Edit person details

                p.EditAdmin(personId, salary, employmenttype, workinghours); // Edit Admin details

                // Check if at least one field is provided for updating
                if (name == null && telephone == null && email == null && role == null && salary == null && employmenttype == null && workinghours == null)
                {
                    MessageBox.Show("No fields to update.");
                    return;
                }

                this.PersonDGV.DataSource = p.GetAdmins();

                MessageBox.Show("Admin edited successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }

        private void editStudentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if editStuIdTb is empty or contains non-numeric characters
                if (string.IsNullOrWhiteSpace(editStuIdTb.Text) || !int.TryParse(editStuIdTb.Text, out int personId))
                {
                    MessageBox.Show("Please enter a valid PersonId.");
                    return;
                }

                string name = isNameChanged ? nameTB.Text : null;
                string telephone = isTelephoneChanged ? telephoneTB.Text : null;
                string email = isEmailChanged ? emailTB.Text : null;
                string role = isRoleChanged ? stulb.Text : null;
                string currentsubject1 = isCurrentSubject1Changed ? currentsubject1tb.Text : null;
                string currentsubject2 = isCurrentSubject2Changed ? currentsubject2tb.Text : null;
                string previoussubject1 = isPreviousSubject1Changed ? previoussubject1tb.Text : null;
                string previoussubject2 = isPreviousSubject2Changed ? previoussubject2tb.Text : null;

                PersonBLL p = new PersonBLL();
                p.EditPerson(personId, name, telephone, email, role); // Edit person details

                p.EditStudent(personId, currentsubject1, currentsubject2, previoussubject1, previoussubject2); // Edit Student details

                // Check if at least one field is provided for updating
                if (name == null && telephone == null && email == null && role == null && currentsubject1 == null && currentsubject2 == null && previoussubject1 == null && previoussubject2 == null)
                {
                    MessageBox.Show("No fields to update.");
                    return;
                }

                this.PersonDGV.DataSource = p.GetStudents();

                MessageBox.Show("Student edited successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }
    }
}

