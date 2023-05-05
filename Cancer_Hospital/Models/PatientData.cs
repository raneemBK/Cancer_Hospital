namespace Cancer_Hospital.Models
{
    public class PatientData
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string CancerType { get; set; }
        public string DoctorName { get; set; }
        public int DoctorId { get; set; }
        public string Discrption { get; set; }
        public string CancerStage { get; set; }
        public string TreatmentPlan { get; set; }
        public string LAboratoryResults { get; set; }
    }
}
