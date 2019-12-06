using System;

namespace ASM.HolidayExtra.Service.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Changed { get; set; }
        public string ChangedBy { get; set; }
    }
}