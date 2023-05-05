using Cancer_Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Cancer_Hospital.Controllers
{
    public class PatientRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        public PatientRegistrationController(IConfiguration configuration)
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
        public IActionResult Signup(PatientSignup model)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "Insert into [Patient] (p_id,p_fname,p_lname,p_password,p_email,p_gender,p_birthdate,p_phone,p_cancerType,p_country,p_city,d_doctor) values('" + model.P_id + "','" + model.Fname + "','" + model.Lname + "','" +model.Password+ "', '" +model.Email+ "', '" +model.Gender+ "', '" +model.BirthDate+ "', '" +model.Phone+ "', '" +model.Cancertype+ "' , '" +model.Country+ "', '" +model.City+ "', '" +model.D_id+ "') ";
           
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
