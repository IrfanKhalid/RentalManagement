using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGYM.Models
{
    [Table("Member")]
    public class MemberRegistration
    {

        [Key]
        public int Id { get; set; }
        
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter First Name")]
        public string MemberFName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter Last Name")]
        public string MemberLName { get; set; }
        [DisplayName("Middle Name")]
        
        public string MemberMName { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Birth Date")]
        public DateTime? Dob { get; set; }

        public string Age { get; set; }

        [Required(ErrorMessage = "Please enter Contactno")]
        public string Contactno { get; set; }

        public string EmailId { get; set; }

        public string Gender { get; set; }

        public int Createdby { get; set; }

        public int ModifiedBy { get; set; }


        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }

    }
}
