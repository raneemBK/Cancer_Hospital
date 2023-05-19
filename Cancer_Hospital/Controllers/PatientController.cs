using Cancer_Hospital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Cancer_Hospital.Controllers
{
    public class PatientController : Controller
    {
        private readonly IConfiguration _configuration;
        public PatientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: PatientController
        public ActionResult Index(int id , string fname)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "SELECT Patient.p_id,Patient.p_fname,Patient.p_lname,Patient.p_cancerType,Patient.s_discrption,Patient.cancer_stage,Patient.treatment_plan,patient.laboratory_results,patient.d_doctor,doctor.d_fname FROM [Patient] join [Doctor] on patient.d_doctor=Doctor.d_id where p_id = '" + id + "' and p_fname='" + fname + "' ";
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
                    patient.DoctorName = reader["d_fname"].ToString();
                    patient.DoctorId = (int)reader["d_doctor"];
                    patients.Add(patient);
                }

                reader.Close();

                List<PatientData> Patient;
                return View(patients);
            }
        }
        public ActionResult ChatDisplay(int PId , int DId, string Fname , ChatView chat)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "select chat.pat_id,chat.doc_id, Patient.p_fname,Doctor.d_fname,Chat.doctor_text,Chat.patient_text,chat.chat_date from [Chat] join [Doctor] on chat.doc_id = doctor.d_id join [Patient] on chat.pat_id = patient.p_id where chat.doc_id = '"+DId+"' and chat.pat_id= '"+PId+ "' ORDER BY chat.IDColumn";
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
            reader.Close ();
            ViewBag.Pid = PId;
            ViewBag.Did = DId;
            ViewBag.FirstName = Fname;
            return View(chats);
        }
        [HttpGet]
        public ActionResult ChatSend()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChatSend( int patientID , int doctorID, string text)
        {
            string test = "test";
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string query = "insert into [Chat] (pat_id,doc_id,patient_text,doctor_text,chat_date) values((select p_id from [Patient] where p_id = '" + patientID+"'),(select d_id from [Doctor] where d_id = '"+doctorID+"'),'"+text+"','"+test+"',CONVERT(datetime, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 120))";
            SqlConnection connection = new SqlConnection( connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("ChatDisplay","Patient", new {PId = patientID , DId = doctorID});
        }
    }
}
