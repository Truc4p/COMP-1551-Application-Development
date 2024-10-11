using System;
using System.Linq;
using System.Windows.Forms;

namespace CW
{
    public partial class Form1 : Form
    {
        // Boolean flags to track if specific fields have been changed by the user
        private bool isNameChanged = false;
        private bool isTelephoneChanged = false;
        private bool isEmailChanged = false;

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

        // Constructor initializes the form and attaches event handlers
        public Form1()
        {
            InitializeComponent();

            // Event handler for when the selected row in the DataGridView changes
            this.PersonDGV.SelectionChanged += new System.EventHandler(this.PersonDGV_SelectionChanged);

            // Attach event handlers to TextBox TextChanged events to track changes
            nameTB.TextChanged += nameTB_TextChanged;
            telephoneTB.TextChanged += telephoneTB_TextChanged;
            emailTB.TextChanged += emailTB_TextChanged;

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

        // Event handler methods that set the corresponding flag to true when a TextBox value changes
        private void nameTB_TextChanged(object sender, EventArgs e) => isNameChanged = !string.IsNullOrEmpty(nameTB.Text);
        private void telephoneTB_TextChanged(object sender, EventArgs e) => isTelephoneChanged = !string.IsNullOrEmpty(telephoneTB.Text);
        private void emailTB_TextChanged(object sender, EventArgs e) => isEmailChanged = !string.IsNullOrEmpty(emailTB.Text);
        
        private void salaryTB_TextChanged(object sender, EventArgs e) => isSalaryChanged = !string.IsNullOrEmpty(salaryTB.Text);
        private void subject1TB_TextChanged(object sender, EventArgs e) => isSubject1Changed = !string.IsNullOrEmpty(subject1TB.Text);
        private void subject2TB_TextChanged(object sender, EventArgs e) => isSubject2Changed = !string.IsNullOrEmpty(subject2TB.Text);

        private void salaryAtb_TextChanged(object sender, EventArgs e) => isSalaryAChanged = !string.IsNullOrEmpty(salaryAtb.Text);
        private void emptyptb_TextChanged(object sender, EventArgs e) => isEmploymentTypeChanged = !string.IsNullOrEmpty(emptyptb.Text);
        private void worhoutb_TextChanged(object sender, EventArgs e) => isWorkingHoursChanged = !string.IsNullOrEmpty(worhoutb.Text);

        private void currentsubject1tb_TextChanged(object sender, EventArgs e) => isCurrentSubject1Changed = !string.IsNullOrEmpty(currentsubject1tb.Text);
        private void currentsubject2tb_TextChanged(object sender, EventArgs e) => isCurrentSubject2Changed = !string.IsNullOrEmpty(currentsubject2tb.Text);
        private void previoussubject1tb_TextChanged(object sender, EventArgs e) => isPreviousSubject1Changed = !string.IsNullOrEmpty(previoussubject1tb.Text);
        private void previoussubject2tb_TextChanged(object sender, EventArgs e) => isPreviousSubject2Changed = !string.IsNullOrEmpty(previoussubject2tb.Text);       

        // Method that runs when the form loads, setting up the DataGridView with data
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the business logic layer and set the data source of the DataGridView
                PersonBLL p = new PersonBLL();
                this.PersonDGV.DataSource = p.GetPersons();
            }
            catch
            {
                // Display an error message if something goes wrong during data loading
                MessageBox.Show("Error Occurred");
            }
        }

        // This method handles the SelectionChanged event of the PersonDGV DataGridView.
        // It triggers when the selection of rows changes in the DataGridView.
        private void PersonDGV_SelectionChanged(object sender, EventArgs e)
        {
            // Check if at least one row is selected
            if (PersonDGV.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = PersonDGV.SelectedRows[0];

                // Retrieve the PersonId and Role values from the selected row
                var personId = selectedRow.Cells["PersonId"].Value;
                var personRole = selectedRow.Cells["Role"].Value;

                // Create an instance of the PersonBLL class to interact with the business logic layer
                PersonBLL p = new PersonBLL();

                // Set the DataSource of PersonDGV to the data returned by GetPersonInfo method
                this.PersonDGV.DataSource = p.GetPersonInfo(personId, personRole);
            }
        }

        // This method handles the Click event of the LoadDataInfoBtn button.
        // It is used to load person data based on the input provided in the LoadDataInfoTxtBox.
        private void LoadDataInfoBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate that the input textbox is not empty or whitespace
                if (string.IsNullOrWhiteSpace(this.LoadDataInfoTxtBox.Text))
                {
                    // Display a message if the input is invalid
                    MessageBox.Show("Please enter a valid PersonId or Name.");
                    return;
                }

                // Create an instance of the PersonBLL class to interact with the business logic layer
                PersonBLL p = new PersonBLL();

                // Check if the input is numeric (i.e., a Person ID)
                if (this.LoadDataInfoTxtBox.Text.All(Char.IsDigit))
                {
                    // Load data based on Person ID
                    this.PersonDGV.DataSource = p.GetPerson(Convert.ToInt16(this.LoadDataInfoTxtBox.Text));
                }
                else
                {
                    // Load data based on Person Name
                    this.PersonDGV.DataSource = p.GetPersons(this.LoadDataInfoTxtBox.Text);
                }
            }
            catch
            {
                // Display an error message if an exception occurs
                MessageBox.Show("Error Occurred");
            }
        }

        // This method handles the Click event of the ViewAllDataBtn button.
        // It loads all person data into the DataGridView.
        private void ViewAllDataBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the PersonBLL class to interact with the business logic layer
                PersonBLL p = new PersonBLL();

                // Load all person data
                this.PersonDGV.DataSource = p.GetPersons();

            }
            catch
            {
                // Display an error message if an exception occurs
                MessageBox.Show("Error Occurred");
            }
        }

        // This method handles the Click event of the ViewTeacherBtn button.
        // It loads all teacher data into the DataGridView.
        private void ViewTeacherBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the PersonBLL class to interact with the business logic layer
                PersonBLL p = new PersonBLL();

                // Load teacher data
                this.PersonDGV.DataSource = p.GetTeachers();
            }
            catch
            {
                // Display an error message if an exception occurs
                MessageBox.Show("Error Occurred");
            }
        }

        // This method handles the Click event of the ViewAdminBtn button.
        // It loads all administrator data into the DataGridView.
        private void ViewAdminBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the PersonBLL class to interact with the business logic layer
                PersonBLL p = new PersonBLL();

                // Load administrator data
                this.PersonDGV.DataSource = p.GetAdmins();
            }
            catch
            {
                // Display an error message if an exception occurs
                MessageBox.Show("Error Occurred");
            }
        }
        // Event handler for the "View Student" button click
        private void ViewStudentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the PersonBLL class
                PersonBLL p = new PersonBLL();

                // Retrieve the list of students and bind it to the DataGridView
                this.PersonDGV.DataSource = p.GetStudents();
            }
            catch
            {
                // Show an error message if something goes wrong
                MessageBox.Show("Error Occurred");
            }
        }

        // Event handler for the "Add Teacher" button click
        private void AddTeacherBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Collect input data from TextBoxes and other UI elements
                string name = nameTB.Text;
                string telephone = telephoneTB.Text;
                string email = emailTB.Text;
                string role = teacherLb.Text;
                string salaryText = salaryTB.Text;
                string subject1 = subject1TB.Text;
                string subject2 = subject2TB.Text;

                // Validate that all required fields are filled
                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(telephone) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(role) ||
                    string.IsNullOrWhiteSpace(salaryText) ||
                    string.IsNullOrWhiteSpace(subject1) ||
                    string.IsNullOrWhiteSpace(subject2))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return;
                }

                // Validate that salary is a valid integer
                if (!int.TryParse(salaryText, out int salary))
                {
                    MessageBox.Show("Please enter a valid salary.");
                    return;
                }               

                // Create an instance of the PersonBLL class
                PersonBLL p = new PersonBLL();

                // Add the person to the database and get the generated PersonId
                int personId = p.AddPerson(name, telephone, email, role);

                // Use the PersonId to add the teacher-specific details
                p.AddTeacher(personId, salary, subject1, subject2);

                // Refresh the DataGridView to display the updated list of teachers
                this.PersonDGV.DataSource = p.GetTeachers();

                // Clear the TextBoxes after successfully adding the teacher
                nameTB.Clear();
                telephoneTB.Clear();
                emailTB.Clear();
                salaryTB.Clear();
                subject1TB.Clear();
                subject2TB.Clear();

                // Show a success message upon completion
                MessageBox.Show("Teacher added successfully");
            }
            catch (Exception ex)
            {
                // Show an error message if something goes wrong, including the exception message
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }

        // Event handler for the "Add Admin" button click
        private void AddAdminBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Collect input data from TextBoxes and other UI elements
                string name = nameTB.Text;
                string telephone = telephoneTB.Text;
                string email = emailTB.Text;
                string role = adminLb.Text;
                string salaryText = salaryAtb.Text;
                string employmenttype = emptyptb.Text;
                string workinghoursText = worhoutb.Text;

                // Validate that all required fields are filled
                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(telephone) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(role) ||
                    string.IsNullOrWhiteSpace(salaryText) ||
                    string.IsNullOrWhiteSpace(employmenttype) ||
                    string.IsNullOrWhiteSpace(workinghoursText))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return;
                }

                // Validate that salary is a valid integer
                if (!int.TryParse(salaryText, out int salary))
                {
                    MessageBox.Show("Please enter a valid salary.");
                    return;
                }

                // Validate that workinghours is a valid integer
                if (!int.TryParse(workinghoursText, out int workinghours))
                {
                    MessageBox.Show("Please enter a valid workinghours.");
                    return;
                }

                // Create an instance of the PersonBLL class
                PersonBLL p = new PersonBLL();

                // Add the person to the database and get the generated PersonId
                int personId = p.AddPerson(name, telephone, email, role);

                // Use the PersonId to add the admin-specific details
                p.AddAdmin(personId, salary, employmenttype, workinghours);

                // Refresh the DataGridView to display the updated list of admins
                this.PersonDGV.DataSource = p.GetAdmins();

                // Clear the TextBoxes after successfully adding the admin
                nameTB.Clear();
                telephoneTB.Clear();
                emailTB.Clear();
                salaryAtb.Clear();
                emptyptb.Clear();
                worhoutb.Clear();

                // Show a success message upon completion
                MessageBox.Show("Admin added successfully");
            }
            catch (Exception ex)
            {
                // Show an error message if something goes wrong, including the exception message
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }
        // Event handler for adding a student when the "Add Student" button is clicked
        private void addStudentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve input data from text boxes
                string name = nameTB.Text;
                string telephone = telephoneTB.Text;
                string email = emailTB.Text;
                string role = stulb.Text;
                string currentsubject1 = currentsubject1tb.Text;
                string currentsubject2 = currentsubject2tb.Text;
                string previoussubject1 = previoussubject1tb.Text;
                string previoussubject2 = previoussubject2tb.Text;

                // Validate that all required fields are filled
                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(telephone) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(role) ||
                    string.IsNullOrWhiteSpace(currentsubject1) ||
                    string.IsNullOrWhiteSpace(currentsubject2) ||
                    string.IsNullOrWhiteSpace(previoussubject1) ||
                    string.IsNullOrWhiteSpace(previoussubject2))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return;
                }

                // Create a new PersonBLL object to interact with the business logic layer
                PersonBLL p = new PersonBLL();

                // Add the person to the database and retrieve the generated PersonId
                int personId = p.AddPerson(name, telephone, email, role);

                // Add the student with the provided PersonId and subject information
                p.AddStudent(personId, currentsubject1, currentsubject2, previoussubject1, previoussubject2);

                // Update the DataGridView to display the list of students
                this.PersonDGV.DataSource = p.GetStudents();

                // Clear the TextBoxes after successfully adding the student
                nameTB.Clear();
                telephoneTB.Clear();
                emailTB.Clear();
                currentsubject1tb.Clear();
                currentsubject2tb.Clear();
                previoussubject1tb.Clear();
                previoussubject2tb.Clear();

                // Display a success message to the user
                MessageBox.Show("Student added successfully");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the process and display an error message
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }

        // Event handler for editing a teacher when the "Edit Teacher" button is clicked
        private void editTeacherBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate that the PersonId is provided and is numeric
                if (string.IsNullOrWhiteSpace(editIDtb.Text) || !int.TryParse(editIDtb.Text, out int personId))
                {
                    MessageBox.Show("Please enter a valid PersonId.");
                    return;
                }

                // Retrieve updated data from text boxes based on whether fields have been changed
                string name = isNameChanged ? nameTB.Text : null;
                string telephone = isTelephoneChanged ? telephoneTB.Text : null;
                string email = isEmailChanged ? emailTB.Text : null;
                string role = teacherLb.Text;
                int? salary = isSalaryChanged ? Convert.ToInt32(salaryTB.Text) : (int?)null;
                string subject1 = isSubject1Changed ? subject1TB.Text : null;
                string subject2 = isSubject2Changed ? subject2TB.Text : null;

                // Create a new PersonBLL object to interact with the business logic layer
                PersonBLL p = new PersonBLL();

                // Update the person's basic details in the database
                p.EditPerson(personId, name, telephone, email, role);

                // Update the teacher's specific details, including salary and subjects
                p.EditTeacher(personId, salary, subject1, subject2);

                // Ensure at least one field is provided for updating, otherwise notify the user
                if (!isNameChanged && !isTelephoneChanged && !isEmailChanged && !isSalaryChanged && !isSubject1Changed && !isSubject2Changed)
                {
                    MessageBox.Show("No fields to update.");
                    return;
                }

                // Update the DataGridView to display the list of teachers
                this.PersonDGV.DataSource = p.GetPerson(personId);

                // Clear the TextBoxes after successfully adding the teacher
                nameTB.Clear();
                telephoneTB.Clear();
                emailTB.Clear();
                salaryTB.Clear();
                subject1TB.Clear();
                subject2TB.Clear();

                // Display a success message to the user
                MessageBox.Show("Teacher edited successfully");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the process and display an error message
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }

        private void editAdminBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate the input for the admin's PersonId, ensuring it's not empty and contains only numeric characters.
                if (string.IsNullOrWhiteSpace(editIdAdminTb.Text) || !int.TryParse(editIdAdminTb.Text, out int personId))
                {
                    MessageBox.Show("Please enter a valid PersonId.");
                    return; // Exit if validation fails.
                }

                // Initialize variables with input values if they are marked as changed, otherwise set to null.
                string name = isNameChanged ? nameTB.Text : null;
                string telephone = isTelephoneChanged ? telephoneTB.Text : null;
                string email = isEmailChanged ? emailTB.Text : null;
                string role = adminLb.Text;
                int? salary = isSalaryAChanged ? Convert.ToInt32(salaryAtb.Text) : (int?)null;
                string employmenttype = isEmploymentTypeChanged ? emptyptb.Text : null;
                int? workinghours = isWorkingHoursChanged ? Convert.ToInt32(worhoutb.Text) : (int?)null;

                // Create an instance of PersonBLL to handle business logic.
                PersonBLL p = new PersonBLL();

                // Call method to edit general person details.
                p.EditPerson(personId, name, telephone, email, role);

                // Call method to edit specific admin details.
                p.EditAdmin(personId, salary, employmenttype, workinghours);

                // Ensure at least one field is provided for updating, otherwise notify the user
                if (!isNameChanged && !isTelephoneChanged && !isEmailChanged && !isSalaryAChanged && !isEmploymentTypeChanged && !isWorkingHoursChanged)
                {
                    MessageBox.Show("No fields to update.");
                    return;
                }

                // Update the DataGridView to reflect changes to the list of admins.
                this.PersonDGV.DataSource = p.GetPerson(personId);

                // Clear the TextBoxes after successfully adding the admin
                nameTB.Clear();
                telephoneTB.Clear();
                emailTB.Clear();
                salaryAtb.Clear();
                emptyptb.Clear();
                worhoutb.Clear();

                MessageBox.Show("Admin edited successfully"); // Notify the user of success.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred: " + ex.Message); // Display any exceptions that occur.
            }
        }

        private void editStudentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate the input for the student's PersonId, ensuring it's not empty and contains only numeric characters.
                if (string.IsNullOrWhiteSpace(editStuIdTb.Text) || !int.TryParse(editStuIdTb.Text, out int personId))
                {
                    MessageBox.Show("Please enter a valid PersonId.");
                    return; // Exit if validation fails.
                }

                // Initialize variables with input values if they are marked as changed, otherwise set to null.
                string name = isNameChanged ? nameTB.Text : null;
                string telephone = isTelephoneChanged ? telephoneTB.Text : null;
                string email = isEmailChanged ? emailTB.Text : null;
                string role = stulb.Text;
                string currentsubject1 = isCurrentSubject1Changed ? currentsubject1tb.Text : null;
                string currentsubject2 = isCurrentSubject2Changed ? currentsubject2tb.Text : null;
                string previoussubject1 = isPreviousSubject1Changed ? previoussubject1tb.Text : null;
                string previoussubject2 = isPreviousSubject2Changed ? previoussubject2tb.Text : null;


                // Create an instance of PersonBLL to handle business logic.
                PersonBLL p = new PersonBLL();

                // Call method to edit general person details.
                p.EditPerson(personId, name, telephone, email, role);

                // Call method to edit specific student details.
                p.EditStudent(personId, currentsubject1, currentsubject2, previoussubject1, previoussubject2);

                // Ensure at least one field is provided for updating, otherwise notify the user
                if (!isNameChanged && !isTelephoneChanged && !isEmailChanged && !isCurrentSubject1Changed && !isCurrentSubject2Changed && !isPreviousSubject1Changed && !isPreviousSubject2Changed)
                {
                    MessageBox.Show("No fields to update.");
                    return;
                }

                // Update the DataGridView to reflect changes to the list of students.
                this.PersonDGV.DataSource = p.GetPerson(personId);

                // Clear the TextBoxes after successfully adding the student
                nameTB.Clear();
                telephoneTB.Clear();
                emailTB.Clear();
                currentsubject1tb.Clear();
                currentsubject2tb.Clear();
                previoussubject1tb.Clear();
                previoussubject2tb.Clear();

                MessageBox.Show("Student edited successfully"); // Notify the user of success.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred: " + ex.Message); // Display any exceptions that occur.
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate the input for the PersonId to be deleted, ensuring it's not empty and contains only numeric characters.
                if (string.IsNullOrWhiteSpace(deleteTb.Text) || !int.TryParse(deleteTb.Text, out int personId))
                {
                    MessageBox.Show("Please enter a valid PersonId.");
                    return; // Exit if validation fails.
                }

                // Create an instance of PersonBLL to handle business logic.
                PersonBLL p = new PersonBLL();

                // Call method to delete the person from the system.
                p.DeletePerson(personId);

                // Update the DataGridView to reflect changes to the list of persons.
                this.PersonDGV.DataSource = p.GetPersons();

                MessageBox.Show("Person deleted successfully"); // Notify the user of success.
            }
            catch
            {
                MessageBox.Show("Error Occurred"); // Display a general error message in case of exceptions.
            }
        }
    }
}


