namespace Cancer_Hospital.Models
{
    public class PatientSignup
    {
        public int P_id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Cancertype { get; set; }
        public DateTime BirthDate { get; set; }
        public int D_id { get; set; }

    }
}
