using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGYM.ViewModels
{
    public class AttachPlotViewModel
    {        
        public int PlotId { get; set; }
        public int MemberId { get; set; }

        [DisplayName("PlotNo")]
        [Required(ErrorMessage = "Please enter Plot Number")]
        public string PlotNo { get; set; }

        [DisplayName("PlotArea")]
        [Required(ErrorMessage = "Please enter Plot Area")]
        public string PlotArea { get; set; }


        [DisplayName("Rate per Merla")]
        [Required(ErrorMessage = "Please enter Rate per Merla")]
        public decimal RateperMerla { get; set; }
        
        [DisplayName("Price To Be Decided")]
        
        public decimal PriceToBeDecided { get; set; }

        [DisplayName("Total Amount")]

        public decimal TotalAmount { get; set; }

        
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Booking Date")]
        
        public DateTime? BookingDate { get; set; }

        
        public string BookingAuthority { get; set; }

        public string TimeLimit { get; set; }                
        
    }
}
