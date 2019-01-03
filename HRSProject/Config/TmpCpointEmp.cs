using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRSProject.Config
{
    public class TmpCpointEmp
    {
        private string tmp_cpoint_id;
        private string tmp_cpoint_emp_id;
        private string tmp_cpoint_cpoint_id;
        private string tmp_cpoint_date;
        private string tmp_cpoint_status;
        private string tmp_cpoint_emp_pos;
        private string tmp_cpoint_emp_aff;
        private string tmp_cpoint_cpoint_old_id;

        public string Tmp_cpoint_id { get => tmp_cpoint_id; set => tmp_cpoint_id = value; }
        public string Tmp_cpoint_emp_id { get => tmp_cpoint_emp_id; set => tmp_cpoint_emp_id = value; }
        public string Tmp_cpoint_cpoint_id { get => tmp_cpoint_cpoint_id; set => tmp_cpoint_cpoint_id = value; }
        public string Tmp_cpoint_date { get => tmp_cpoint_date; set => tmp_cpoint_date = value; }
        public string Tmp_cpoint_status { get => tmp_cpoint_status; set => tmp_cpoint_status = value; }
        public string Tmp_cpoint_emp_pos { get => tmp_cpoint_emp_pos; set => tmp_cpoint_emp_pos = value; }
        public string Tmp_cpoint_emp_aff { get => tmp_cpoint_emp_aff; set => tmp_cpoint_emp_aff = value; }
        public string Tmp_cpoint_cpoint_old_id { get => tmp_cpoint_cpoint_old_id; set => tmp_cpoint_cpoint_old_id = value; }

        public TmpCpointEmp (string id,string emp_id,string cpoint_id,string date,string status,string pos,string aff,string cpoint_old)
        {
            Tmp_cpoint_cpoint_id = id;
            Tmp_cpoint_emp_id = emp_id;
            Tmp_cpoint_cpoint_id = cpoint_id;
            Tmp_cpoint_date = date;
            Tmp_cpoint_status = status;
            Tmp_cpoint_emp_pos = pos;
            Tmp_cpoint_emp_aff = aff;
            Tmp_cpoint_cpoint_old_id = cpoint_old;
        }
    }
}