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



        public DataTable GetTeachers()
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                return objdal.ReadTeachers();
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAdmins()
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                return objdal.ReadAdmins();
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStudents()
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                return objdal.ReadStudents();
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

        public int EditPerson(int personId, string name, string telephone, string email, string role)
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

        public void AddTeacher(int personId, decimal salary, string subject1, string subject2)
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                objdal.CreateTeacher(personId, salary, subject1, subject2);
            }
            catch
            {
                throw;
            }
        }

        public void EditTeacher(int personId, decimal salary, string subject1, string subject2)
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                objdal.UpdateTeacher(personId, salary, subject1, subject2);
            }
            catch
            {
                throw;
            }
        }

        public void AddAdmin(int personId, decimal salary, string employmenttype, decimal workinghours)
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                objdal.CreateAdmin(personId, salary, employmenttype, workinghours);
            }
            catch
            {
                throw;
            }
        }

        public void AddStudent(int personId, string currentsubject1, string currentsubject2, string previoussubject1, string previoussubject2)
        {
            try
            {
                PersonDAL objdal = new PersonDAL();
                objdal.CreateStudent(personId, currentsubject1, currentsubject2, previoussubject1, previoussubject2);
            }
            catch
            {
                throw;
            }
        }
    }
}
