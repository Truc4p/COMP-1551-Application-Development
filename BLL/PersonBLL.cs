using System;
using System.Collections.Generic;
using System.Data;
using Person = CW.PersonDAL.Person;
using Teacher = CW.PersonDAL.Teacher;
using Admin = CW.PersonDAL.Admin;
using Student = CW.PersonDAL.Student;
using static CW.PersonDAL;
using System.Data.SqlClient;

namespace CW
{
    public class PersonBLL
    {
        private List<Person> persons = new List<Person>();
        private PersonDAL personDAL = new PersonDAL();

        static void Main(string[] args)
        {
            // Example usage of PersonBLL
            PersonBLL personBLL = new PersonBLL();
            DataTable allPersons = personBLL.GetPersons();
            Console.WriteLine("All persons data loaded.");

            DataTable personById = personBLL.GetPerson(1);
            Console.WriteLine("Person with ID 1 data loaded.");

            DataTable personByName = personBLL.GetPersons("Tom");
            Console.WriteLine("Person with Name Tom data loaded.");
        }

        public DataTable GetPersons()
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                return objdal.Read();
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPerson(Int16 ID)
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                return objdal.Read(ID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPersons(String NAME)
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                return objdal.ReadByName(NAME);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPersonInfo(object personId, object personRole)
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                return objdal.ReadPersonInfo(personId, personRole);
            }
            catch
            {
                throw;
            }
        }

        public int AddPerson(string name, string telephone, string email, string role)
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                return objdal.CreatePerson(name, telephone, email, role);
            }
            catch
            {
                throw;
            }
        }

        public void AddTeacher(int personId, decimal salary, string subject1, string subject2)
        {
            try
            {
                Teacher teacher = new Teacher
                {
                    PersonId = personId,
                    Salary = salary,
                    Subject1 = subject1,
                    Subject2 = subject2
                };

                PersonDAL objdal = new PersonDAL();
                objdal.CreateTeacher(personId, salary, subject1, subject2);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding teacher: " + ex.Message);
            }
        }

        public void EditTeacher(int personId, decimal salary, string subject1, string subject2)
        {
            try
            {
                Teacher teacher = new Teacher
                {
                    PersonId = personId,
                    Salary = salary,
                    Subject1 = subject1,
                    Subject2 = subject2
                };

                PersonDAL objdal = new PersonDAL();
                objdal.UpdateTeacher(personId, salary, subject1, subject2);
            }
            catch (Exception ex)
            {
                throw new Exception("Error editing teacher: " + ex.Message);
            }
        }

        public void EditAdmin(int personId, decimal salary, string employmenttype, decimal workinghours)
        {
            try
            {
                Admin admin = new Admin
                {
                    PersonId = personId,
                    Salary = salary,
                    EmploymentType = employmenttype,
                    WorkingHours = workinghours
                };

                PersonDAL objdal = new PersonDAL();
                objdal.UpdateAdmin(personId, salary, employmenttype, workinghours);
            }
            catch (Exception ex)
            {
                throw new Exception("Error editing admin: " + ex.Message);
            }
        }

        public void EditStudent(int personId, string currentsubject1, string currentsubject2, string previoussubject1, string previoussubject2)
        {
            try
            {
                Student student = new Student
                {
                    PersonId = personId,
                    CurrentSubject1 = currentsubject1,
                    CurrentSubject2 = currentsubject2,
                    PreviousSubject1 = previoussubject1,
                    PreviousSubject2 = previoussubject2
                };

                PersonDAL objdal = new PersonDAL();
                objdal.UpdateStudent(personId, currentsubject1, currentsubject2, previoussubject1, previoussubject2);
            }
            catch (Exception ex)
            {
                throw new Exception("Error editing student: " + ex.Message);
            }
        }

        public void AddAdmin(int personId, decimal salary, string employmenttype, decimal workinghours)
        {
            try
            {
                Admin admin = new Admin
                {
                    PersonId = personId,
                    Salary = salary,
                    EmploymentType = employmenttype,
                    WorkingHours = workinghours
                };

                PersonDAL objdal = new PersonDAL();
                objdal.CreateAdmin(personId, salary, employmenttype, workinghours);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding admin: " + ex.Message);
            }
        }

        public void AddStudent(int personId, string currentsubject1, string currentsubject2, string previoussubject1, string previoussubject2)
        {
            try
            {
                Student student = new Student
                {
                    PersonId = personId,
                    CurrentSubject1 = currentsubject1,
                    CurrentSubject2 = currentsubject2,
                    PreviousSubject1 = previoussubject1,
                    PreviousSubject2 = previoussubject2
                };

                PersonDAL objdal = new PersonDAL();
                objdal.CreateStudent(personId, currentsubject1, currentsubject2, previoussubject1, previoussubject2);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding student: " + ex.Message);
            }
        }

        public List<Teacher> GetTeachers()
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                DataTable teacherTable = objdal.ReadTeachers();

                List<Teacher> teachers = new List<Teacher>();
                foreach (DataRow row in teacherTable.Rows)
                {
                    Teacher teacher = new Teacher
                    {
                        PersonId = Convert.ToInt32(row["PersonId"]),
                        Name = row["Name"].ToString(),
                        Telephone = row["Telephone"].ToString(),
                        Email = row["Email"].ToString(),
                        Role = row["Role"].ToString(),
                        Salary = Convert.ToDecimal(row["Salary"]),
                        Subject1 = row["Subject1"].ToString(),
                        Subject2 = row["Subject2"].ToString()
                    };
                    teachers.Add(teacher);
                }

                return teachers;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving teachers: " + ex.Message);
            }
        }

        public List<Admin> GetAdmins()
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                DataTable adminTable = objdal.ReadAdmins();

                List<Admin> admins = new List<Admin>();
                foreach (DataRow row in adminTable.Rows)
                {
                    Admin admin = new Admin
                    {
                        PersonId = Convert.ToInt32(row["PersonId"]),
                        Name = row["Name"].ToString(),
                        Telephone = row["Telephone"].ToString(),
                        Email = row["Email"].ToString(),
                        Role = row["Role"].ToString(),
                        Salary = Convert.ToDecimal(row["Salary"]),
                        EmploymentType = row["EmploymentType"].ToString(),
                        WorkingHours = Convert.ToDecimal(row["WorkingHours"])
                    };
                    admins.Add(admin);
                }

                return admins;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving admins: " + ex.Message);
            }
        }

        public List<Student> GetStudents()
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                DataTable studentTable = objdal.ReadStudents();

                List<Student> students = new List<Student>();
                foreach (DataRow row in studentTable.Rows)
                {
                    Student student = new Student
                    {
                        PersonId = Convert.ToInt32(row["PersonId"]),
                        Name = row["Name"].ToString(),
                        Telephone = row["Telephone"].ToString(),
                        Email = row["Email"].ToString(),
                        Role = row["Role"].ToString(),
                        CurrentSubject1 = row["CurrentSubject1"].ToString(),
                        CurrentSubject2 = row["CurrentSubject2"].ToString(),
                        PreviousSubject1 = row["PreviousSubject1"].ToString(),
                        PreviousSubject2 = row["PreviousSubject2"].ToString()
                    };
                    students.Add(student);
                }

                return students;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving students: " + ex.Message);
            }
        }

        public int EditPerson(int personId, string name = null, string telephone = null, string email = null, string role = null)
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                return objdal.UpdatePerson(personId, name, telephone, email, role);
            }
            catch
            {
                throw;
            }
        }

        public void DeletePerson(int personId)
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                objdal.DeletePerson(personId);
            }
            catch
            {
                throw;
            }
        }

    }
}
