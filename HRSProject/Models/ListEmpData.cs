using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRSProject.Models
{
    public class ListEmpData
    {
        public string profix { get; set; }
        public string name { get; set; }
        public string lname { get; set; }
        public string cpoint_name { get; set; }
        public string pos_name { get; set; }
        public string emp_id { get; set; }
        public string note { get; set; }
        public DateTime dateNote { get; set; }
        public string pos_row { get; set; }
        public string cpoint_row { get; set; }
    }
}