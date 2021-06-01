using Swashbuckle.AspNetCore.Annotations;
using System;

namespace WebGYM.ViewModels
{
    [SwaggerSchema(Required = new[] { "Description" })]
    public class ExpenseViewModel
    {
        public  int ID { get; set; }
        public string AccountHead { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }


        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        //public long PaymentID { get; set; }
        //public string PlanName { get; set; }
        //public string SchemeName { get; set; }
        //public DateTime? PaymentFromdt { get; set; }
        //public DateTime? PaymentTodt { get; set; }
        //public decimal? PaymentAmount { get; set; }
        //public DateTime? NextRenwalDate { get; set; }
        //public string RecStatus { get; set; }
        //public string MemberName { get; set; }
        //public string MemberNo { get; set; }
    }
}