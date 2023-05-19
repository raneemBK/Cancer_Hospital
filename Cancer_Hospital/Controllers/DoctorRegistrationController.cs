using Cancer_Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Cancer_Hospital.Controllers
{
    public class DoctorRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        public DoctorRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(DoctorSignup model)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "Insert into [Doctor] (d_id,d_fname,d_lname,d_email,d_password,d_phone,d_gender,d_birthdate,d_educations,d_country,d_city) values('"+model.d_id+"','"+model.Fname+"','"+model.Lname+"','"+model.Email+"','"+model.Password+"','"+model.Phone+"','"+model.Gender+"','"+model.Birthdate+"','"+model.Education+"','"+model.Country+"','"+model.City+"') ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
                return RedirectToAction("Index", "Admin");
            }
        }
    }
}
