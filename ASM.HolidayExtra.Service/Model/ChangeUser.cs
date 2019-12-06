namespace ASM.HolidayExtra.Service.Model
{
    public class ChangeUser
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ChangedBy { get; set; }
    }
}