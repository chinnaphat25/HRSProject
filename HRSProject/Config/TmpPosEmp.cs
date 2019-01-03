using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRSProject.Config
{
    public class TmpPosEmp
    {
        private string tmp_pos_id;
        private string tmp_pos_emp_id;
        private string tmp_pos_pos_old_id;
        private string tmp_pos_aff_old_id;
        private string tmp_pos_emp_type_old_id;
        private string tmp_pos_pos_id;
        private string tmp_pos_aff_id;
        private string tmp_pos_emp_type_id;
        private string tmp_pos_date;
        private string tmp_pos_status;

        public TmpPosEmp(string tmp_pos_id, string tmp_pos_emp_id, string tmp_pos_pos_old_id, string tmp_pos_aff_old_id, string tmp_pos_emp_type_old_id, string tmp_pos_pos_id, string tmp_pos_aff_id, string tmp_pos_emp_type_id, string tmp_pos_date, string tmp_pos_status)
        {
            this.tmp_pos_id = tmp_pos_id;
            this.tmp_pos_emp_id = tmp_pos_emp_id;
            this.tmp_pos_pos_old_id = tmp_pos_pos_old_id;
            this.tmp_pos_aff_old_id = tmp_pos_aff_old_id;
            this.tmp_pos_emp_type_old_id = tmp_pos_emp_type_old_id;
            this.tmp_pos_pos_id = tmp_pos_pos_id;
            this.tmp_pos_aff_id = tmp_pos_aff_id;
            this.tmp_pos_emp_type_id = tmp_pos_emp_type_id;
            this.tmp_pos_date = tmp_pos_date;
            this.tmp_pos_status = tmp_pos_status;
        }

        public string Tmp_pos_id { get => tmp_pos_id; set => tmp_pos_id = value; }
        public string Tmp_pos_emp_id { get => tmp_pos_emp_id; set => tmp_pos_emp_id = value; }
        public string Tmp_pos_pos_old_id { get => tmp_pos_pos_old_id; set => tmp_pos_pos_old_id = value; }
        public string Tmp_pos_aff_old_id { get => tmp_pos_aff_old_id; set => tmp_pos_aff_old_id = value; }
        public string Tmp_pos_emp_type_old_id { get => tmp_pos_emp_type_old_id; set => tmp_pos_emp_type_old_id = value; }
        public string Tmp_pos_pos_id { get => tmp_pos_pos_id; set => tmp_pos_pos_id = value; }
        public string Tmp_pos_aff_id { get => tmp_pos_aff_id; set => tmp_pos_aff_id = value; }
        public string Tmp_pos_emp_type_id { get => tmp_pos_emp_type_id; set => tmp_pos_emp_type_id = value; }
        public string Tmp_pos_date { get => tmp_pos_date; set => tmp_pos_date = value; }
        public string Tmp_pos_status { get => tmp_pos_status; set => tmp_pos_status = value; }
    }
}