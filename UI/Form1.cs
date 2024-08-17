using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.PersonDGV.SelectionChanged += new System.EventHandler(this.PersonDGV_SelectionChanged);
            this.viewGroupDGV.SelectionChanged += new System.EventHandler(this.viewGroupDGV_SelectionChanged);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                PersonBLL p = new PersonBLL();
                this.PersonDGV.DataSource = p.GetPersons();
                this.viewGroupDGV.DataSource = p.GetTeachers();
                this.viewAfterAddDGV.DataSource = p.GetTeachers(); 
                this.addAdminDgv.DataSource = p.GetAdmins();
                this.addStudentDgv.DataSource = p.GetStudents();

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

        private void viewGroupDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (viewGroupDGV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = viewGroupDGV.SelectedRows[0];

                // Access the data in the selected row
                var personId = selectedRow.Cells["PersonId"].Value;
                var personRole = selectedRow.Cells["Role"].Value;

                PersonBLL p = new PersonBLL();
                this.viewGroupDGV.DataSource = p.GetPersonInfo(personId, personRole);
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

        private void loadGroupInfoBtn_Click(object sender, EventArgs e)
        {
            try
            {
                PersonBLL p = new PersonBLL();

                if (this.loadGroupInfoTxtBox.Text.All(Char.IsDigit))
                {
                    this.viewGroupDGV.DataSource = p.GetPerson(Convert.ToInt16(this.loadGroupInfoTxtBox.Text)); // search ID 
                }
                else
                {
                    this.viewGroupDGV.DataSource = p.GetPersons(this.loadGroupInfoTxtBox.Text); // search by name
                }
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
                string name = name1lb.Text;
                string telephone = telep1lb.Text;
                string email = email1lb.Text;
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
                string name = namlb.Text;
                string telephone = tellb.Text;
                string email = emalb.Text;
                string role = stulb.Text;
                string currentsubject1 = cuj1lb.Text;
                string currentsubject2 = cuj2lb.Text;
                string previoussubject1 = prj1lb.Text;
                string previoussubject2 = prj2lb.Text;

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

        private void label1_Click_1(object sender, EventArgs e)
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

        private void label15_Click_1(object sender, EventArgs e)
        {

        }

        private void PersonDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void viewGroupDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void editTeacherBtn_Click(object sender, EventArgs e)
        {

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
    }
}

