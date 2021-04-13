using System;

namespace WebGYM.ViewModels
{
    public class MemberPlotGridModel
    {
        public int PlotId;
        public int MemberId;
        public string PlotNo { get; set; }        
        public string PlotArea { get; set; }
        public decimal RateperMerla { get; set; }
        public decimal PriceToBeDecided { get; set; }
        public decimal Total { get; set; }
        public DateTime? BookingDate  { get; set; }
        public string BookingAuthority { get; set; }
        public string TimeLimit { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }

    }
}