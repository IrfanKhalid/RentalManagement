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
    [Table("Expense")]
    public class Expense
    {
        [Key]

        [DisplayName("Id")]
        public int Id { get; set; }
        
        [DisplayName("AccountHead")]
        public string AccountHead { get; set; }
        
        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [DisplayName("PaymentDate")]
        public DateTime PaymentDate { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }

}
