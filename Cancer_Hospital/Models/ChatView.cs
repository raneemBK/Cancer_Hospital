namespace Cancer_Hospital.Models
{
    public class ChatView
    {   
        public int Pid { get; set; }
        public int Did { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }

        public string DoctorText { get; set; }
        public string PatientText { get; set; }
        public DateTime Date { get; set;}
        

    }
}
