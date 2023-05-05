using Cancer_Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Cancer_Hospital.Controllers
{
    public class PatientLoginController : Controller
    {
        private readonly IConfiguration _configuration;
        public IActionResult Index()
        {
            return View();
        }
        public PatientLoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(PatientLoginViewModel model)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "SELECT p_id,p_fname, p_email,p_Password FROM [Patient] WHERE p_email=@Email AND p_password=@Password";

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
                        var currentUserID = (int)reader["p_id"];
                        var first = reader["p_fname"].ToString();
                        return RedirectToAction("Index", "Patient", new { id = currentUserID, fname = first });
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
