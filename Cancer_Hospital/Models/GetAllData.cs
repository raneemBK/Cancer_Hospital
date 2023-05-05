namespace Cancer_Hospital.Models
{
    public class GetAllData
    {
        public int? D_id { get; set; }
        public string? D_fname { get; set; }
        public string? D_lname { get; set; }
        public string? D_email { get; set;}
        public string? D_password { get; set;}
        public string? D_phone { get; set;}
        public string? D_gender { get; set;}
        public DateTime? D_birthdate { get; set;}
        public string? D_educations { get; set;}
        public string? D_country { get; set;}
        public string? D_city { get; set;}

        public int? P_id { get; set; }
        public string? P_fname { get; set; }
        public string? P_lname { get; set; }
        public string? P_email { get; set; }
        public string? P_password { get; set; }
        public string? P_phone { get; set; }
        public string? P_gender { get; set; }
        public DateTime? P_birthdate { get; set; }
        public string? P_cancerType { get; set; }
        public string? P_country { get; set; }
        public string? P_city { get; set; }
        public int? D_doctor { get; set; }

    }
}
