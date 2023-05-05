using Cancer_Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Data.SqlClient;

namespace Cancer_Hospital.Controllers
{
    public class DoctorLoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public IActionResult Index()
        {
            return View();
        }
        public DoctorLoginController(IConfiguration configuration)
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
            string query = "SELECT d_id,d_fname, d_email,d_Password FROM [Doctor] WHERE  d_email=@Email AND d_password=@Password";

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
                        var currentUserID = (int)reader["d_id"];
                        var first = reader["d_fname"].ToString();
                        // Login successful
                        return RedirectToAction("Index", "Doctor", new { id = currentUserID, fname = first });
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
