using System;

namespace WebGYM.ViewModels
{
    public class MemberRegistrationGridModel
    {
        public int Id;
        public string MemberName { get; set; }
        public DateTime? Dob { get; set; }
        public string Contactno { get; set; }
        public string EmailId { get; set; }
        
    }
}