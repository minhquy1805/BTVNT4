using BTVNT4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace BTVNT4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Student student)
        {
            getStudent(student);
            List<Student> students = getAll();
            return View(students);
        }

        private void getStudent(Student student)
        {
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO tblStudent(StudentName, Region, Adress, Telephone) VALUES (@StudentName, @Region, @Address, @Telephone)"
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
                    cmd.Parameters.AddWithValue("@Region", student.Region);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@Telephone", student.Telephone);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if(rowsAffected > 0)
                    {
                        string message = "Created the record successfully";
                        ViewBag.Message = message;
                    }
                    else
                    {
                        string errorMessage = "Failed to create the record";
                        ViewBag.ErrorMessage = errorMessage;
                    }
                }
            }
        }
        private List<Student> getAll()
        {
            List<Student> listStudent = new List<Student>();
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "SELECT StudentName, Region, Address, Telephone FROM tblStudent";
                using(SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            string studentName = reader["StudentName"].ToString();
                            string region = reader["Region"].ToString();
                            string address = reader["Address"].ToString();
                            double telephone = Convert.ToDouble(reader["Telephone"]);
                            Student student = new Student
                            {
                                StudentName = studentName,
                                Region = region,
                                Address = address,
                                Telephone = telephone
                            };
                            listStudent.Add(student);
                        }                 
                    }
                }
            }
            return listStudent;
        }
    }
}
