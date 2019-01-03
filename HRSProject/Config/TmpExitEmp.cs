using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRSProject.Config
{
    public class TmpExitEmp
    {
        private string tmp_ex_id;
        private string tmp_ex_emp;
        private string tmp_ex_status;
        private string tmp_ex_date;
        private string tmp_ex_note;
        private string tmp_ex_working_status;

        public string Tmp_ex_id { get => tmp_ex_id; set => tmp_ex_id = value; }
        public string Tmp_ex_emp { get => tmp_ex_emp; set => tmp_ex_emp = value; }
        public string Tmp_ex_status { get => tmp_ex_status; set => tmp_ex_status = value; }
        public string Tmp_ex_date { get => tmp_ex_date; set => tmp_ex_date = value; }
        public string Tmp_ex_note { get => tmp_ex_note; set => tmp_ex_note = value; }
        public string Tmp_ex_working_status { get => tmp_ex_working_status; set => tmp_ex_working_status = value; }

        public TmpExitEmp(string id,string emp,string status,string date,string note,string workS)
        {
            tmp_ex_id = id;
            tmp_ex_emp = emp;
            tmp_ex_status = status;
            tmp_ex_date = date;
            tmp_ex_note = note;
            tmp_ex_working_status = workS;
        }
    }
}