using Cancer_Hospital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;

namespace Cancer_Hospital.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IConfiguration _configuration;
        public DoctorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: DoctorController
        public ActionResult Index(int id , string fname)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // var doctorId = currentUserId;
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "SELECT Patient.p_id,Patient.p_fname,Patient.p_lname,Patient.p_cancerType,Doctor.d_fname,Doctor.d_id FROM [Patient] inner join [Doctor] on Patient.d_doctor = '"+id+"' and Doctor.d_fname='"+fname+"' ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                // command.Parameters.AddWithValue("@userId", doc.ID);
                // command.Parameters.AddWithValue("@doctor_id", currentUserId);
               /* command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;*/

                SqlDataReader reader = command.ExecuteReader();
                List<PatientDataForDoctor> patients = new List<PatientDataForDoctor>();

                while (reader.Read())
                {
                    PatientDataForDoctor patient = new PatientDataForDoctor();
                    patient.Id = (int)reader["p_id"];
                    patient.Fname = reader["p_fname"].ToString();
                    patient.Lname = reader["p_lname"].ToString();
                    patient.CancerType = reader["p_cancerType"].ToString();
                   // patient.FirstName = reader["d_fname"].ToString();
                    patient.DoctorID = (int)reader["d_id"];
                    patients.Add(patient);
                }

                reader.Close();

                List<PatientDataForDoctor> Patient;
                //ViewBag.DoctorId = id;
                ViewBag.FirstName = fname;
                return View(patients);
            }
        }
        public ActionResult Display(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "SELECT * FROM [Patient] where p_id = '" + id + "' ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();
                List<PatientData> patients = new List<PatientData>();

                while (reader.Read())
                {
                    PatientData patient = new PatientData();
                    patient.Id = (int)reader["p_id"];
                    patient.Fname = reader["p_fname"].ToString();
                    patient.Lname = reader["p_lname"].ToString();
                    patient.CancerType = reader["p_cancerType"].ToString();
                    patient.Discrption = reader["s_discrption"].ToString();
                    patient.CancerStage = reader["cancer_stage"].ToString();
                    patient.TreatmentPlan = reader["treatment_plan"].ToString();
                    patient.LAboratoryResults = reader["laboratory_results"].ToString();
                    patient.DoctorId = (int)reader["d_doctor"];
                    patients.Add(patient);
                }

                reader.Close();

                List<PatientData> Patient;
                return View(patients);
            }
        }
        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(int Id , UpdateDataPatient data)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "update [Patient] set s_discrption= '" + data.Discrption + "', cancer_stage= '" + data.CancerStage + "', treatment_plan='" + data.TreatmentPlan + "', laboratory_results='" + data.LAboratoryResults + "' where p_id= '" + Id + "' ";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Display", "Doctor", new { id = Id });
        }
        public ActionResult ChatDisplay(int PId, int DId, ChatView chat)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "select chat.pat_id,chat.doc_id, Patient.p_fname,Doctor.d_fname,Chat.doctor_text,Chat.patient_text,chat.chat_date from [Chat] join [Doctor] on chat.doc_id = doctor.d_id join [Patient] on chat.pat_id = patient.p_id where chat.doc_id = '" + DId + "' and chat.pat_id= '" + PId + "'";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<ChatView> chats = new List<ChatView>();
            while (reader.Read())
            {
                ChatView ch = new ChatView();
                ch.DoctorName = reader["d_fname"].ToString();
                ch.PatientName = reader["p_fname"].ToString();
                ch.PatientText = reader["patient_text"].ToString();
                ch.DoctorText = reader["doctor_text"].ToString();
                ch.Pid = (int)reader["pat_id"];
                ch.Did = (int)reader["doc_id"];
                //  ch.Date = (DateTime)reader["chat_date"]; 
                chats.Add(ch);
            }
            reader.Close();
            ViewBag.Pid = PId;
            ViewBag.Did = DId;

            return View(chats);
        }
        [HttpGet]
        public ActionResult ChatSend()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChatSend(int patientID, int doctorID, string doctortext)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "insert into [Chat] (pat_id,doc_id,doctor_text,chat_date) values((select p_id from [Patient] where p_id = '" + patientID + "'),(select d_id from [Doctor] where d_id = '" + doctorID + "'),'" + doctortext + "',CONVERT(datetime, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 120))";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("ChatDisplay", "Doctor", new { PId = patientID, DId = doctorID });
        }

    }
}
