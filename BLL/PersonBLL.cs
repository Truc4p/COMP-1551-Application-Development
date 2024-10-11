using System;
using System.Collections.Generic;
using System.Data;

namespace CW
{
    // Business Logic Layer (BLL) class for managing Person entities.
    public class PersonBLL
    {
        static void Main(string[] _)
        {}

        // Base class representing a person with common properties.
        public class Person
        {
            // Unique identifier for the person.
            public int PersonId { get; set; }

            // Full name of the person.
            public string Name { get; set; }

            // Contact telephone number of the person.
            public string Telephone { get; set; }

            // Email address of the person.
            public string Email { get; set; }

            // Role of the person within the organization (e.g., Teacher, Student).
            public string Role { get; set; }
        }

        // Derived class representing a teacher with additional properties.
        public class Teacher : Person
        {
            // Salary of the teacher.
            public int? Salary { get; set; }

            // Primary subject taught by the teacher.
            public string Subject1 { get; set; }

            // Secondary subject taught by the teacher.
            public string Subject2 { get; set; }
        }

        // Derived class representing an admin with additional properties.
        public class Admin : Person
        {
            // Salary of the admin.
            public int? Salary { get; set; }

            // Employment type of the admin (e.g., Full-time, Part-time).
            public string EmploymentType { get; set; }

            // Number of working hours per week for the admin.
            public int? WorkingHours { get; set; }
        }

        // Derived class representing a student with additional properties.
        public class Student : Person
        {
            // Current subject the student is enrolled in (first subject).
            public string CurrentSubject1 { get; set; }

            // Current subject the student is enrolled in (second subject).
            public string CurrentSubject2 { get; set; }

            // Previous subject the student was enrolled in (first subject).
            public string PreviousSubject1 { get; set; }

            // Previous subject the student was enrolled in (second subject).
            public string PreviousSubject2 { get; set; }
        }

        // Method to retrieve all persons from the database.
        public DataTable GetPersons()
        {
            try
            {
                PersonDAL objdal = new PersonDAL(); // DAL instance.
                return objdal.Read(); // Fetch and return all persons.
            }
            catch
            {
                // Rethrow the exception for the calling method to handle.
                throw;
            }
        }

        // Method to retrieve a person by ID.
        public DataTable GetPerson(Int32 ID)
        {
            try
            {
                PersonDAL objdal = new PersonDAL(); // DAL instance.
                return objdal.Read(ID); // Fetch and return person by ID.
            }
            catch
            {
                // Rethrow the exception for the calling method to handle.
                throw;
            }
        }

        // Method to retrieve persons by their name.
        public DataTable GetPersons(String NAME)
        {
            try
            {
                PersonDAL objdal = new PersonDAL(); // DAL instance.
                return objdal.ReadByName(NAME); // Fetch and return persons by name.
            }
            catch
            {
                // Rethrow the exception for the calling method to handle.
                throw;
            }
        }

        // Method to retrieve specific person information based on ID and role.
        public DataTable GetPersonInfo(object personId, object personRole)
        {
            try
            {
                PersonDAL objdal = new PersonDAL(); // DAL instance.
                return objdal.ReadPersonInfo(personId, personRole); // Fetch and return person info by ID and role.
            }
            catch
            {
                // Rethrow the exception for the calling method to handle.
                throw;
            }
        }

        // Method to retrieve a list of teachers from the data access layer
        public List<Teacher> GetTeachers()
        {
            try
            {
                // Instantiate the data access layer (DAL) to interact with the database.
                PersonDAL objdal = new PersonDAL();

                // Use the DAL to read teacher records from the database.
                DataTable teacherTable = objdal.ReadTeachers();

                // Initialize a list to hold the teacher objects.
                List<Teacher> teachers = new List<Teacher>();

                // Iterate through each row in the DataTable and create a Teacher object.
                foreach (DataRow row in teacherTable.Rows)
                {
                    Teacher teacher = new Teacher
                    {
                        PersonId = Convert.ToInt32(row["PersonId"]),
                        Name = row["Name"].ToString(),
                        Telephone = row["Telephone"].ToString(),
                        Email = row["Email"].ToString(),
                        Role = row["Role"].ToString(),
                        Salary = Convert.ToInt32(row["Salary"]),
                        Subject1 = row["Subject1"].ToString(),
                        Subject2 = row["Subject2"].ToString()
                    };

                    // Add the Teacher object to the list.
                    teachers.Add(teacher);
                }

                // Return the list of teachers.
                return teachers;
            }
            catch (Exception ex)
            {
                // Catch any exceptions and throw a new exception with a descriptive message.
                throw new Exception("Error retrieving teachers: " + ex.Message);
            }
        }

        // Method to retrieve a list of admins from the data access layer
        public List<Admin> GetAdmins()
        {
            try
            {
                // Instantiate the data access layer (DAL) to interact with the database.
                PersonDAL objdal = new PersonDAL();

                // Use the DAL to read admin records from the database.
                DataTable adminTable = objdal.ReadAdmins();

                // Initialize a list to hold the admin objects.
                List<Admin> admins = new List<Admin>();

                // Iterate through each row in the DataTable and create an Admin object.
                foreach (DataRow row in adminTable.Rows)
                {
                    Admin admin = new Admin
                    {
                        PersonId = Convert.ToInt32(row["PersonId"]),
                        Name = row["Name"].ToString(),
                        Telephone = row["Telephone"].ToString(),
                        Email = row["Email"].ToString(),
                        Role = row["Role"].ToString(),
                        Salary = Convert.ToInt32(row["Salary"]),
                        EmploymentType = row["EmploymentType"].ToString(),
                        WorkingHours = Convert.ToInt32(row["WorkingHours"])
                    };

                    // Add the Admin object to the list.
                    admins.Add(admin);
                }

                // Return the list of admins.
                return admins;
            }
            catch (Exception ex)
            {
                // Catch any exceptions and throw a new exception with a descriptive message.
                throw new Exception("Error retrieving admins: " + ex.Message);
            }
        }

        // Method to retrieve a list of students from the data access layer
        public List<Student> GetStudents()
        {
            try
            {
                // Create an instance of the data access layer class to interact with the database
                PersonDAL objdal = new PersonDAL();

                // Retrieve the data table containing student information from the database
                DataTable studentTable = objdal.ReadStudents();

                // Create a list to hold the student objects
                List<Student> students = new List<Student>();

                // Iterate through each row in the data table
                foreach (DataRow row in studentTable.Rows)
                {
                    // Create a new student object and populate it with data from the current row
                    Student student = new Student
                    {
                        PersonId = Convert.ToInt32(row["PersonId"]), // Convert PersonId to integer
                        Name = row["Name"].ToString(),               // Get student name
                        Telephone = row["Telephone"].ToString(),     // Get student telephone
                        Email = row["Email"].ToString(),             // Get student email
                        Role = row["Role"].ToString(),               // Get student role
                        CurrentSubject1 = row["CurrentSubject1"].ToString(), // Get current subject 1
                        CurrentSubject2 = row["CurrentSubject2"].ToString(), // Get current subject 2
                        PreviousSubject1 = row["PreviousSubject1"].ToString(), // Get previous subject 1
                        PreviousSubject2 = row["PreviousSubject2"].ToString()  // Get previous subject 2
                    };

                    // Add the newly created student object to the list
                    students.Add(student);
                }

                // Return the list of students
                return students;
            }
            catch (Exception ex)
            {
                // Catch any exceptions and throw a new exception with a custom message
                throw new Exception("Error retrieving students: " + ex.Message);
            }
        }

        // Method to add a new person to the database.
        public int AddPerson(string name, string telephone, string email, string role)
        {
            try
            {
                PersonDAL objdal = new PersonDAL(); // DAL instance.
                return objdal.CreatePerson(name, telephone, email, role); // Add a new person and return the new person ID.
            }
            catch
            {
                // Rethrow the exception for the calling method to handle.
                throw;
            }
        }


        // Method to add a new Teacher to the system
        public void AddTeacher(int personId, int salary, string subject1, string subject2)
        {
            try
            {
                // Initialize a new Teacher object with provided details
                Teacher teacher = new Teacher
                {
                    PersonId = personId,
                    Salary = salary,
                    Subject1 = subject1,
                    Subject2 = subject2
                };

                // Instantiate the data access layer object
                PersonDAL objdal = new PersonDAL();

                // Call the method to create a new Teacher record in the database
                objdal.CreateTeacher(personId, salary, subject1, subject2);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process and throw a custom exception message
                throw new Exception("Error adding teacher: " + ex.Message);
            }
        }

        // Method to add a new Admin to the system
        public void AddAdmin(int personId, int salary, string employmenttype, int workinghours)
        {
            try
            {
                // Initialize a new Admin object with provided details
                Admin admin = new Admin
                {
                    PersonId = personId,
                    Salary = salary,
                    EmploymentType = employmenttype,
                    WorkingHours = workinghours
                };

                // Instantiate the data access layer object
                PersonDAL objdal = new PersonDAL();

                // Call the method to create a new Admin record in the database
                objdal.CreateAdmin(personId, salary, employmenttype, workinghours);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process and throw a custom exception message
                throw new Exception("Error adding admin: " + ex.Message);
            }
        }

        // Adds a new student to the system with the given details.
        public void AddStudent(int personId, string currentsubject1, string currentsubject2, string previoussubject1, string previoussubject2)
        {
            try
            {
                // Create a new Student object with the provided details.
                Student student = new Student
                {
                    PersonId = personId,
                    CurrentSubject1 = currentsubject1,
                    CurrentSubject2 = currentsubject2,
                    PreviousSubject1 = previoussubject1,
                    PreviousSubject2 = previoussubject2
                };

                // Instantiate the data access layer (DAL) to interact with the database.
                PersonDAL objdal = new PersonDAL();

                // Use the DAL to create a new student record in the database.
                objdal.CreateStudent(personId, currentsubject1, currentsubject2, previoussubject1, previoussubject2);
            }
            catch (Exception ex)
            {
                // Catch any exceptions and throw a new exception with a descriptive message.
                throw new Exception("Error adding student: " + ex.Message);
            }
        }

        // Method to edit the details of a person based on their ID
        public int EditPerson(int personId, string name = null, string telephone = null, string email = null, string role = null)
        {
            try
            {
                // Create an instance of the data access layer class to interact with the database
                PersonDAL objdal = new PersonDAL();

                // Update the person's details and return the result of the operation
                return objdal.UpdatePerson(personId, name, telephone, email, role);
            }
            catch
            {
                // Catch any exceptions and rethrow them
                throw;
            }
        }

        // Method to edit an existing Teacher's information
        public void EditTeacher(int personId, int? salary, string subject1, string subject2)
        {
            try
            {
                // Initialize a Teacher object with updated details
                Teacher teacher = new Teacher
                {
                    PersonId = personId,
                    Salary = salary,
                    Subject1 = subject1,
                    Subject2 = subject2
                };

                // Instantiate the data access layer object
                PersonDAL objdal = new PersonDAL();

                // Call the method to update the Teacher's record in the database
                objdal.UpdateTeacher(personId, salary, subject1, subject2);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process and throw a custom exception message
                throw new Exception("Error editing teacher: " + ex.Message);
            }
        }

        // Method to edit an existing Admin's information
        public void EditAdmin(int personId, int? salary, string employmenttype, int? workinghours)
        {
            try
            {
                // Initialize an Admin object with updated details
                Admin admin = new Admin
                {
                    PersonId = personId,
                    Salary = salary,
                    EmploymentType = employmenttype,
                    WorkingHours = workinghours
                };

                // Instantiate the data access layer object
                PersonDAL objdal = new PersonDAL();

                // Call the method to update the Admin's record in the database
                objdal.UpdateAdmin(personId, salary, employmenttype, workinghours);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process and throw a custom exception message
                throw new Exception("Error editing admin: " + ex.Message);
            }
        }

        // Method to edit an existing Student's information
        public void EditStudent(int personId, string currentsubject1, string currentsubject2, string previoussubject1, string previoussubject2)
        {
            try
            {
                // Initialize a Student object with updated details
                Student student = new Student
                {
                    PersonId = personId,
                    CurrentSubject1 = currentsubject1,
                    CurrentSubject2 = currentsubject2,
                    PreviousSubject1 = previoussubject1,
                    PreviousSubject2 = previoussubject2
                };

                // Instantiate the data access layer object
                PersonDAL objdal = new PersonDAL();

                // Call the method to update the Student's record in the database
                objdal.UpdateStudent(personId, currentsubject1, currentsubject2, previoussubject1, previoussubject2);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process and throw a custom exception message
                throw new Exception("Error editing student: " + ex.Message);
            }
        }

        // Method to delete a person based on their ID
        public void DeletePerson(int personId)
        {
            try
            {
                // Create an instance of the data access layer class to interact with the database
                PersonDAL objdal = new PersonDAL();

                // Delete the person and handle any necessary operations
                objdal.DeletePerson(personId);
            }
            catch
            {
                // Catch any exceptions and rethrow them
                throw;
            }
        }
    }
}
