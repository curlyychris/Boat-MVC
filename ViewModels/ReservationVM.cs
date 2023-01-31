using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab8.ViewModels
{
    public class ReservationVM
    {
        public int Cust_Res_ID { get; set; }
        public int BoatID { get; set; }
        public int CustomerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}