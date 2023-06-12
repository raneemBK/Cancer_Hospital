using Cancer_Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;

namespace Cancer_Hospital.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "select d_id ,d_fname,d_lname,d_email ,d_password,d_phone,d_gender,d_birthdate ,d_country,d_city,p_id,p_fname,p_lname ,p_password,p_email,p_gender ,p_birthdate ,p_phone ,p_cancerType ,p_country ,p_city,d_doctor from [Doctor] full join [Patient] on d_id=p_id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query,connection);
                SqlDataReader reader = command.ExecuteReader();

                List<GetAllData> data = new List<GetAllData>();
                while (reader.Read())
                {
                    GetAllData dataItem = new GetAllData();
                    
                    dataItem.D_fname = reader["d_fname"].ToString();
                    dataItem.D_lname = reader["d_lname"].ToString();
                    dataItem.D_email = reader["d_email"].ToString();
                    dataItem.D_password = reader["d_password"].ToString();
                    dataItem.D_phone = reader["d_phone"].ToString();
                    dataItem.D_gender = reader["d_gender"].ToString();
                    
               
                    dataItem.D_country = reader["d_country"].ToString();
                    dataItem.D_city = reader["d_city"].ToString();

                    
                    dataItem.P_fname = reader["p_fname"].ToString();
                    dataItem.P_lname = reader["p_lname"].ToString();
                    dataItem.P_email = reader["p_email"].ToString();
                    dataItem.P_password = reader["p_password"].ToString();
                    dataItem.P_phone = reader["p_phone"].ToString();
                    dataItem.P_gender = reader["p_gender"].ToString();
                    
                    dataItem.P_cancerType = reader["p_cancerType"].ToString();
                    dataItem.P_country = reader["p_country"].ToString();
                    dataItem.P_city = reader["p_city"].ToString();

                    if (reader["d_id"] != DBNull.Value)
                    {
                        dataItem.D_id = (int)reader["d_id"];
                    }
                    else
                    {
                        // Assign a default value or return null, depending on your requirements
                        dataItem.D_id = 0; // or myDateTime = null;
                    }
                    if (reader["d_birthdate"] != DBNull.Value)
                    {
                        dataItem.D_birthdate = (DateTime)reader["d_birthdate"];
                    }
                    else
                    {
                        // Assign a default value or return null, depending on your requirements
                        dataItem.D_birthdate = DateTime.MinValue; // or myDateTime = null;
                    }

                    if (reader["P_birthdate"] != DBNull.Value)
                    {
                        dataItem.P_birthdate = (DateTime)reader["P_birthdate"];
                    }
                    else
                    {
                        // Assign a default value or return null, depending on your requirements
                        dataItem.P_birthdate = DateTime.MinValue; // or myDateTime = null;
                    }
                    if (reader["P_id"] != DBNull.Value)
                    {
                        dataItem.P_id = (int)reader["p_id"];
                    }
                    else
                    {
                        // Assign a default value or return null, depending on your requirements
                        dataItem.P_id = 0; // or myDateTime = null;
                    }
                    if (reader["d_doctor"] != DBNull.Value)
                    {
                        dataItem.D_doctor = (int)reader["d_doctor"];
                    }
                    else
                    {
                        // Assign a default value or return null, depending on your requirements
                        dataItem.D_doctor = 0; // or myDateTime = null;
                    }
                    data.Add(dataItem);

                }
                reader.Close();
                return View(data);
            }
            
        }
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(DoctorLoginViewModel model)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "SELECT a_email,a_Password FROM [AdminLogin] WHERE a_email=@Email AND a_password=@Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Password", model.Password);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Login successful
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        // Login failed
                        ModelState.AddModelError(string.Empty, "Invalid email or password.");
                        return View(model);


                    }
                }
            }
        }
    }
}
