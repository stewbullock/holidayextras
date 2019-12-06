using ASM.Core;
using System;

namespace ASM.HolidayExtra.Core.Model
{
    class User : Identity
    {
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Changed { get; set; }
        public string ChangedBy { get; set; }
    }
}