using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CW
{
    public class PersonDAL
    {
        // Connection string to connect to the SQL Server database
        private readonly string ConString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=EduCen;Persist Security Info=True;User ID=sa;Password=zxc;TrustServerCertificate=True";

        // SqlConnection object to manage the connection to the database
        private readonly SqlConnection con = new SqlConnection();

        // DataTable to hold the results of database queries
        private readonly DataTable dt = new DataTable();

        static void Main(string[] _)
        {}

        // Method to retrieve all records from the Person table, ordered by PersonId in descending order
        public DataTable Read()
        {
            // Set the connection string for the SqlConnection object
            con.ConnectionString = ConString;

            // Open the connection if it is closed
            if (ConnectionState.Closed == con.State)
                con.Open();

            // Define the SQL query to select all persons
            SqlCommand cmd = new SqlCommand("SELECT * FROM Person ORDER BY PersonId DESC", con);
            try
            {
                // Execute the query and load the results into the DataTable
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                return dt;
            }
            catch
            {
                // Rethrow the exception for further handling
                throw;
            }
        }

        // Method to retrieve a specific person based on their PersonId
        public DataTable Read(Int32 id)
        {
            // Set the connection string for the SqlConnection object
            con.ConnectionString = ConString;

            // Open the connection if it is closed
            if (ConnectionState.Closed == con.State)
                con.Open();

            // Define the SQL query to select a person by their PersonId
            SqlCommand cmd = new SqlCommand("select * from Person where PersonId= " + id, con);
            try
            {
                // Execute the query and load the results into the DataTable
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                return dt;
            }
            catch
            {
                // Rethrow the exception for further handling
                throw;
            }
        }

        // Method to retrieve persons based on their name, using a LIKE query for partial matches
        public DataTable ReadByName(String Name)
        {
            // Set the connection string for the SqlConnection object
            con.ConnectionString = ConString;

            // Open the connection if it is closed
            if (ConnectionState.Closed == con.State)
                con.Open();

            // Define the SQL query to select persons where the name matches the parameter
            SqlCommand cmd = new SqlCommand("SELECT * FROM Person WHERE Name LIKE @Name", con);

            // Add the Name parameter with wildcard characters for partial matching
            cmd.Parameters.AddWithValue("@Name", "%" + Name + "%");

            try
            {
                // Execute the query and load the results into a new DataTable
                SqlDataReader rd = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rd);
                return dt;
            }
            catch (Exception ex)
            {
                // Handle or log the exception and provide a custom error message
                throw new Exception("Error reading person data by name with LIKE.", ex);
            }
        }

        // Method to retrieve all records from the Teacher table, joining with the Person table
        public DataTable ReadTeachers()
        {
            // Set the connection string for the SqlConnection object
            con.ConnectionString = ConString;

            // Open the connection if it is closed
            if (ConnectionState.Closed == con.State)
                con.Open();

            // Define the SQL query to join Teacher and Person tables
            string query = @"
                SELECT 
                    Teacher.TeacherId,
                    Teacher.PersonId,
                    Person.Name,
                    Person.Telephone,
                    Person.Email,
                    Person.Role,
                    Teacher.Salary,
                    Teacher.Subject1,
                    Teacher.Subject2
                FROM 
                    Teacher
                INNER JOIN 
                    Person ON Teacher.PersonId = Person.PersonId
                ORDER BY PersonId DESC";

            // Create a SqlCommand object with the query
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                // Execute the query and load the results into the DataTable
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                return dt;
            }
            catch
            {
                // Rethrow the exception for further handling
                throw;
            }
        }

        /// <summary>
        /// Reads information about a person based on their ID and role. 
        /// The role determines which table to query (e.g., Teacher, Admin, Student).
        /// </summary>
        /// <param name="personId">The ID of the person whose information is to be retrieved.</param>
        /// <param name="personRole">The role of the person (e.g., Teacher, Admin, Student).</param>
        /// <returns>A DataTable containing the person's information.</returns>
        public DataTable ReadPersonInfo(object personId, object personRole)
        {
            // Set the connection string for the SqlConnection object
            con.ConnectionString = ConString;

            // Open the connection if it is closed
            if (ConnectionState.Closed == con.State)
                con.Open();

            // Validate and sanitize the personRole parameter to prevent SQL injection
            string[] allowedRoles = { "Teacher", "Admin", "Student" };
            if (!allowedRoles.Contains(personRole.ToString()))
            {
                throw new ArgumentException("Invalid role specified.");
            }

            // Define the query string based on the personRole
            string query;
            if (personRole.ToString() == "Teacher")
            {
                query = @"
                    SELECT P.PersonId, P.Name, P.Telephone, P.Email, 'Teacher' AS Role, 
                            T.Salary, T.Subject1, T.Subject2
                    FROM Person AS P
                    INNER JOIN Teacher AS T ON P.PersonId = T.PersonId
                    WHERE P.PersonId = @PersonId";
            }
            else if (personRole.ToString() == "Admin")
            {
                query = @"
                    SELECT P.PersonId, P.Name, P.Telephone, P.Email, 'Admin' AS Role, 
                           A.Salary, A.EmploymentType, A.WorkingHours
                    FROM Person AS P
                    INNER JOIN Admin AS A ON P.PersonId = A.PersonId
                    WHERE P.PersonId = @PersonId";
            }
            else if (personRole.ToString() == "Student")
            {
                query = @"
                    SELECT P.PersonId, P.Name, P.Telephone, P.Email, 'Student' AS Role, 
                           S.CurrentSubject1, S.CurrentSubject2, 
                           S.PreviousSubject1, S.PreviousSubject2
                    FROM Person AS P
                    INNER JOIN Student AS S ON P.PersonId = S.PersonId
                    WHERE P.PersonId = @PersonId";
            }
            else
            {
                throw new ArgumentException("Role not supported.");
            }

            // Use the constructed query in the SqlCommand
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Add the personId parameter to the SQL query
                cmd.Parameters.AddWithValue("@PersonId", personId);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    // Fill the DataTable with the results of the query
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// Retrieves a list of all admins along with their personal and employment details.
        /// </summary>
        /// <returns>A DataTable containing information about admins.</returns>
        public DataTable ReadAdmins()
        {
            // Set the connection string for the database connection
            con.ConnectionString = ConString;

            // Open the connection if it's closed
            if (ConnectionState.Closed == con.State)
                con.Open();

            // SQL query to get details of all admins joined with personal information
            string query = @"
            SELECT 
                Admin.AdminId,
                Admin.PersonId,
                Person.Name,
                Person.Telephone,
                Person.Email,
                Person.Role,
                Admin.Salary,
                Admin.EmploymentType,
                Admin.WorkingHours
            FROM 
                Admin
            INNER JOIN 
                Person ON Admin.PersonId = Person.PersonId
            ORDER BY PersonId DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                SqlDataReader rd = cmd.ExecuteReader();
                // Load the query results into the DataTable
                dt.Load(rd);
                return dt;
            }
            catch
            {
                // Rethrow any exceptions that occur
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of all students along with their personal and academic details.
        /// </summary>
        /// <returns>A DataTable containing information about students.</returns>
        public DataTable ReadStudents()
        {
            // Set the connection string for the database connection
            con.ConnectionString = ConString;

            // Open the connection if it's closed
            if (ConnectionState.Closed == con.State)
                con.Open();

            // SQL query to get details of all students joined with personal information
            string query = @"
            SELECT 
                Student.StudentId,
                Student.PersonId,
                Person.Name,
                Person.Telephone,
                Person.Email,
                Person.Role,
                Student.CurrentSubject1,
                Student.CurrentSubject2,
                Student.PreviousSubject1,
                Student.PreviousSubject2
            FROM 
                Student
            INNER JOIN 
                Person ON Student.PersonId = Person.PersonId
            ORDER BY PersonId DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                SqlDataReader rd = cmd.ExecuteReader();
                // Load the query results into the DataTable
                dt.Load(rd);
                return dt;
            }
            catch
            {
                // Rethrow any exceptions that occur
                throw;
            }
        }

        /// <summary>
        /// Creates a new person in the database and returns the newly created person's ID.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        /// <param name="telephone">The telephone number of the person.</param>
        /// <param name="email">The email address of the person.</param>
        /// <param name="role">The role of the person (e.g., teacher, student).</param>
        /// <returns>The ID of the newly created person.</returns>
        public int CreatePerson(string name, string telephone, string email, string role)
        {
            // Set the connection string and open the connection if it is closed
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();

            // SQL query to insert a new person and return the generated PersonId
            string query = "INSERT INTO Person (Name, Telephone, Email, Role) OUTPUT INSERTED.PersonId VALUES (@Name, @Telephone, @Email, @Role)";
            SqlCommand cmd = new SqlCommand(query, con);

            // Add parameters to prevent SQL injection
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Telephone", telephone);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Role", role);

            try
            {
                // Execute the query and return the new person's ID
                int personId = (int)cmd.ExecuteScalar();
                return personId;
            }
            catch (Exception ex)
            {
                // Handle and rethrow exceptions that occur during execution
                throw new Exception("Error creating person", ex);
            }
            finally
            {
                // Ensure the connection is closed
                con.Close();
            }
        }

        /// <summary>
        /// Creates a new teacher record in the database based on the given person ID.
        /// </summary>
        /// <param name="personId">The ID of the person who will be a teacher.</param>
        /// <param name="salary">The salary of the teacher.</param>
        /// <param name="subject1">The first subject the teacher teaches.</param>
        /// <param name="subject2">The second subject the teacher teaches.</param>
        public void CreateTeacher(int personId, int salary, string subject1, string subject2)
        {
            // Set the connection string and open the connection if it is closed
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();

            // SQL query to insert a new teacher record
            string query = "INSERT INTO Teacher (PersonId, Salary, Subject1, Subject2) VALUES (@PersonId, @Salary, @Subject1, @Subject2)";
            SqlCommand cmd = new SqlCommand(query, con);

            // Add parameters to prevent SQL injection
            cmd.Parameters.AddWithValue("@PersonId", personId);
            cmd.Parameters.AddWithValue("@Salary", salary);
            cmd.Parameters.AddWithValue("@Subject1", subject1);
            cmd.Parameters.AddWithValue("@Subject2", subject2);

            try
            {
                // Execute the query without returning any results
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle and rethrow exceptions that occur during execution
                throw new Exception("Error creating teacher", ex);
            }
            finally
            {
                // Ensure the connection is closed
                con.Close();
            }
        }

        // Method to create a new Admin record in the database
        public void CreateAdmin(int personId, int salary, string employmenttype, int workinghours)
        {
            con.ConnectionString = ConString;
            // Open connection if it's not already open
            if (ConnectionState.Closed == con.State)
                con.Open();

            // SQL query to insert a new Admin record
            string query = "INSERT INTO Admin (PersonId, Salary, EmploymentType, WorkingHours) VALUES (@PersonId, @Salary, @EmploymentType, @WorkingHours)";
            SqlCommand cmd = new SqlCommand(query, con);
            // Add parameters to SQL command
            cmd.Parameters.AddWithValue("@PersonId", personId);
            cmd.Parameters.AddWithValue("@Salary", salary);
            cmd.Parameters.AddWithValue("@EmploymentType", employmenttype);
            cmd.Parameters.AddWithValue("@WorkingHours", workinghours);

            try
            {
                // Execute the query to create a new Admin record
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Throw a detailed exception if something goes wrong
                throw new Exception("Error creating Admin", ex);
            }
            finally
            {
                // Ensure the connection is closed
                con.Close();
            }
        }

        // Method to create a new Student record in the database
        public void CreateStudent(int personId, string currentsubject1, string currentsubject2, string previoussubject1, string previoussubject2)
        {
            con.ConnectionString = ConString;
            // Open connection if it's not already open
            if (ConnectionState.Closed == con.State)
                con.Open();

            // SQL query to insert a new Student record
            string query = "INSERT INTO Student (PersonId, CurrentSubject1, CurrentSubject2, PreviousSubject1, PreviousSubject2) VALUES (@PersonId, @CurrentSubject1, @CurrentSubject2, @PreviousSubject1, @PreviousSubject2)";
            SqlCommand cmd = new SqlCommand(query, con);
            // Add parameters to SQL command
            cmd.Parameters.AddWithValue("@PersonId", personId);
            cmd.Parameters.AddWithValue("@CurrentSubject1", currentsubject1);
            cmd.Parameters.AddWithValue("@CurrentSubject2", currentsubject2);
            cmd.Parameters.AddWithValue("@PreviousSubject1", previoussubject1);
            cmd.Parameters.AddWithValue("@PreviousSubject2", previoussubject2);

            try
            {
                // Execute the query to create a new Student record
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Throw a detailed exception if something goes wrong
                throw new Exception("Error creating Student", ex);
            }
            finally
            {
                // Ensure the connection is closed
                con.Close();
            }
        }

        /// <summary>
        /// Updates the details of an existing person based on the provided person ID.
        /// </summary>
        /// <param name="personId">The ID of the person to update.</param>
        /// <param name="name">The new name of the person (optional).</param>
        /// <param name="telephone">The new telephone number of the person (optional).</param>
        /// <param name="email">The new email address of the person (optional).</param>
        /// <param name="role">The new role of the person (optional).</param>
        /// <returns>The number of rows affected by the update.</returns>
        public int UpdatePerson(int personId, string name = null, string telephone = null, string email = null, string role = null)
        {
            // Set the connection string and open the connection if it is closed
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();

            // Build the update query dynamically based on provided parameters
            List<string> updateFields = new List<string>();
            if (!string.IsNullOrEmpty(name)) updateFields.Add("Name = @Name");
            if (!string.IsNullOrEmpty(telephone)) updateFields.Add("Telephone = @Telephone");
            if (!string.IsNullOrEmpty(email)) updateFields.Add("Email = @Email");
            if (!string.IsNullOrEmpty(role)) updateFields.Add("Role = @Role");

            // Handle case where no fields are provided for update
            if (updateFields.Count == 0)
            {
                return 0; // No rows updated
            }

            // SQL query to update the person record
            string query = $"UPDATE Person SET {string.Join(", ", updateFields)} WHERE PersonId = @PersonId";
            SqlCommand cmd = new SqlCommand(query, con);

            // Add parameters to prevent SQL injection
            cmd.Parameters.AddWithValue("@PersonId", personId);
            if (!string.IsNullOrEmpty(name)) cmd.Parameters.AddWithValue("@Name", name);
            if (!string.IsNullOrEmpty(telephone)) cmd.Parameters.AddWithValue("@Telephone", telephone);
            if (!string.IsNullOrEmpty(email)) cmd.Parameters.AddWithValue("@Email", email);
            if (!string.IsNullOrEmpty(role)) cmd.Parameters.AddWithValue("@Role", role);

            try
            {
                // Execute the update query and return the number of affected rows
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Handle and rethrow exceptions that occur during execution
                throw new Exception("Error updating person", ex);
            }
            finally
            {
                // Ensure the connection is closed
                con.Close();
            }
        }

        /// <summary>
        /// Updates the details of an existing teacher based on the provided person ID.
        /// </summary>
        /// <param name="personId">The ID of the teacher to update.</param>
        /// <param name="salary">The new salary of the teacher (optional).</param>
        /// <param name="subject1">The new first subject the teacher teaches (optional).</param>
        /// <param name="subject2">The new second subject the teacher teaches (optional).</param>
        public void UpdateTeacher(int personId, int? salary = null, string subject1 = null, string subject2 = null)
        {
            // Set the connection string and open the connection if it is closed
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();

            // Build the update query dynamically based on provided parameters
            List<string> updateFields = new List<string>();
            if (salary.HasValue) updateFields.Add("Salary = @Salary");
            if (!string.IsNullOrEmpty(subject1)) updateFields.Add("Subject1 = @Subject1");
            if (!string.IsNullOrEmpty(subject2)) updateFields.Add("Subject2 = @Subject2");

            // Handle case where no fields are provided for update
            if (updateFields.Count == 0)
            {
                return; // No fields to update
            }

            // SQL query to update the teacher record
            string query = $"UPDATE Teacher SET {string.Join(", ", updateFields)} WHERE PersonId = @PersonId";
            SqlCommand cmd = new SqlCommand(query, con);

            // Add parameters to prevent SQL injection
            cmd.Parameters.AddWithValue("@PersonId", personId);
            if (salary.HasValue) cmd.Parameters.AddWithValue("@Salary", salary.Value);
            if (!string.IsNullOrEmpty(subject1)) cmd.Parameters.AddWithValue("@Subject1", subject1);
            if (!string.IsNullOrEmpty(subject2)) cmd.Parameters.AddWithValue("@Subject2", subject2);

            try
            {
                // Execute the update query without returning any results
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle and rethrow exceptions that occur during execution
                throw new Exception("Error updating teacher: " + ex.Message, ex);
            }
            finally
            {
                // Ensure the connection is closed
                con.Close();
            }
        }

        // Method to update the details of an Admin in the database
        public void UpdateAdmin(int personId, int? salary = null, string employmenttype = null, int? workinghours = null)
        {
            con.ConnectionString = ConString;
            // Open connection if it's not already open
            if (ConnectionState.Closed == con.State)
                con.Open();

            List<string> updateFields = new List<string>();
            // Add fields to be updated based on provided parameters
            if (salary.HasValue) updateFields.Add("Salary = @Salary");
            if (!string.IsNullOrEmpty(employmenttype)) updateFields.Add("EmploymentType = @EmploymentType");
            if (workinghours.HasValue) updateFields.Add("WorkingHours = @WorkingHours");

            // Handle case when no fields are provided
            if (updateFields.Count == 0)
            {
                return; // No fields to update
            }

            // Construct SQL query with dynamic fields
            string query = $"UPDATE Admin SET {string.Join(", ", updateFields)} WHERE PersonId = @PersonId";
            SqlCommand cmd = new SqlCommand(query, con);
            // Add parameters to SQL command
            cmd.Parameters.AddWithValue("@PersonId", personId);
            if (salary.HasValue) cmd.Parameters.AddWithValue("@Salary", salary.Value);
            if (!string.IsNullOrEmpty(employmenttype)) cmd.Parameters.AddWithValue("@EmploymentType", employmenttype);
            if (workinghours.HasValue) cmd.Parameters.AddWithValue("@WorkingHours", workinghours.Value);

            try
            {
                // Execute the query to update the Admin
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Throw a detailed exception if something goes wrong
                throw new Exception("Error updating Admin: " + ex.Message, ex);
            }
            finally
            {
                // Ensure the connection is closed
                con.Close();
            }
        }

        // Method to update the details of a Student in the database
        public void UpdateStudent(int personId, string currentSubject1 = null, string currentSubject2 = null, string previousSubject1 = null, string previousSubject2 = null)
        {
            con.ConnectionString = ConString;
            // Open connection if it's not already open
            if (ConnectionState.Closed == con.State)
                con.Open();

            List<string> updateFields = new List<string>();
            // Add fields to be updated based on provided parameters
            if (!string.IsNullOrEmpty(currentSubject1)) updateFields.Add("CurrentSubject1 = @CurrentSubject1");
            if (!string.IsNullOrEmpty(currentSubject2)) updateFields.Add("CurrentSubject2 = @CurrentSubject2");
            if (!string.IsNullOrEmpty(previousSubject1)) updateFields.Add("PreviousSubject1 = @PreviousSubject1");
            if (!string.IsNullOrEmpty(previousSubject2)) updateFields.Add("PreviousSubject2 = @PreviousSubject2");

            // Handle case when no fields are provided
            if (updateFields.Count == 0)
            {
                return; // No fields to update
            }

            // Construct SQL query with dynamic fields
            string query = $"UPDATE Student SET {string.Join(", ", updateFields)} WHERE PersonId = @PersonId";
            SqlCommand cmd = new SqlCommand(query, con);
            // Add parameters to SQL command
            cmd.Parameters.AddWithValue("@PersonId", personId);
            if (!string.IsNullOrEmpty(currentSubject1)) cmd.Parameters.AddWithValue("@CurrentSubject1", currentSubject1);
            if (!string.IsNullOrEmpty(currentSubject2)) cmd.Parameters.AddWithValue("@CurrentSubject2", currentSubject2);
            if (!string.IsNullOrEmpty(previousSubject1)) cmd.Parameters.AddWithValue("@PreviousSubject1", previousSubject1);
            if (!string.IsNullOrEmpty(previousSubject2)) cmd.Parameters.AddWithValue("@PreviousSubject2", previousSubject2);

            try
            {
                // Execute the query to update the Student
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Throw a detailed exception if something goes wrong
                throw new Exception("Error updating student: " + ex.Message, ex);
            }
            finally
            {
                // Ensure the connection is closed
                con.Close();
            }
        }     

        // Method to delete a person from the database based on their unique identifier.
        public void DeletePerson(int personId)
        {
            // Set the connection string for the SQL connection.
            con.ConnectionString = ConString;

            // Open the connection if it is currently closed.
            if (ConnectionState.Closed == con.State)
                con.Open();

            // SQL query to delete a person record where the PersonId matches the provided value.
            string query = "DELETE FROM Person WHERE PersonId = @PersonId";

            // Create a SqlCommand object with the query and connection.
            SqlCommand cmd = new SqlCommand(query, con);

            // Add the personId parameter to the SQL command to avoid SQL injection.
            cmd.Parameters.AddWithValue("@PersonId", personId);

            try
            {
                // Execute the SQL command to delete the person record.
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the execution of the SQL command.
                throw new Exception("Error deleting person", ex);
            }
            finally
            {
                // Ensure the SQL connection is closed even if an error occurs.
                con.Close();
            }
        }
    }
}
