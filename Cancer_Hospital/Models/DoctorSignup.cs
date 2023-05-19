namespace Cancer_Hospital.Models
{
    public class DoctorSignup
    {
        public int d_id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Education { get; set; }   
       /* public string Specialization { get; set; }*/
        public string Country { get; set; }
        public string City { get; set; }
    }
}
