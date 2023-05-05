using System.Numerics;

namespace Cancer_Hospital.Models
{
    public class PatientDataForDoctor
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string CancerType { get; set; }
        public string FirstName { get; set; }
        public int DoctorID { get; set; }
       /* public int DoctorId { get; set; } // Foreign key property
        public Doctor Doctor { get; set; }*/ // Navigation property
        /* public int DoctorId { get; set; }
         public Doctor Doctor { get; set; }*/
    }
  /*  public class Doctor
    {
        public int Id { get; set; }
        
    }*/
}
