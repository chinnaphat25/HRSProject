using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;

namespace HRSProject.Config
{
    public class DBScript
    {
        MySqlConnection Conn;
        int ArraySize = 300;
        //MySqlCommand cmd;
        public string strConnString = "Server=10.6.3.201;User Id=adminhr; Password=admin25;charset=tis620; Database=hrsystem; Pooling=false";  //Depoly Server
        //public string strConnString = "Server=10.6.3.201;User Id=adminhrs; Password=admin25;charset=tis620; Database=hrs_db; Pooling=false";  //Depoly Test
        //public string strConnString = "Server=10.6.3.175;User Id=adminhrs; Password=admin25; Database=hrs_test; Pooling=false";  //Test
        public DBScript()
        {
            Conn = new MySqlConnection(strConnString);
        }

        public void Set_Max_Connection()
        {
            try
            {
                string sql = "SET global max_connections = 1000000";
                Conn.Open();
                MySqlCommand comm = Conn.CreateCommand();
                comm.CommandText = sql;
                comm.ExecuteNonQuery();
                Conn.Close();
            }
            catch { Conn.Close(); }
        }
        private bool openConn()
        {
            //Set_Max_Connection();
            if (Conn != null && Conn.State == ConnectionState.Closed)//to check if conn is already open or not
            {
                Conn.Open();
            }
            else
            {
                Conn.Close();
                Conn.Open();
            }
            return true;
        }

        public void CloseConnection()
        {
            Conn.Close();
        }

        private bool closeConn()
        {
            if (Conn != null && Conn.State == ConnectionState.Open)//to check if conn is already open or not
            {
                Conn.Close();
            }
            return true;
        }

        public MySqlDataReader selectSQL(string sql)
        {
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = sql;
            openConn();
            MySqlDataReader result = cmd.ExecuteReader();
            return result;
        }

        public string GetSelectData(string sql)
        {
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = sql;
            openConn();
            MySqlDataReader result = cmd.ExecuteReader();
            string data = "";
            if (result.Read())
            {
                data = result.GetString(0);
            }
            result.Close();
            closeConn();
            return data;
        }

        public Boolean actionSql(string sql)
        {
            openConn();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = sql;
            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    closeConn();
                    return true;
                }
                else
                {
                    closeConn();
                    return false;
                }
            }
            catch
            {
                closeConn();
                return false;
            }
        }

        public string InsertQueryPK(string sql)
        {
            openConn();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = sql;
            string Str_return = "";
            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    cmd.CommandText = "SELECT LAST_INSERT_ID() AS gID";
                    Str_return = cmd.ExecuteScalar().ToString();
                }

                closeConn();
                return Str_return;
            }
            catch
            {
                closeConn();
                return Str_return;
            }
        }

        public string getEmpData(string data, string empId)
        {
            string sql = "SELECT " + data + " FROM tbl_emp_profile JOIN tbl_profix ON emp_profix_id = profix_id JOIN tbl_cpoint ON emp_cpoint_id = cpoint_id JOIN tbl_pos ON emp_pos_id = pos_id JOIN tbl_affiliation ON affi_id = emp_affi_id JOIN tbl_type_emp ON type_emp_id = emp_type_emp_id JOIN tbl_type_add ON type_add_id = emp_add_type WHERE emp_id = '" + empId + "'";
            string detali = "";
            MySqlDataReader rs = selectSQL(sql);
            if (rs.Read())
            {
                detali = rs.GetString(data);
                rs.Close();
                closeConn();
                return detali;
            }
            else { rs.Close(); closeConn(); return ""; }

        }

        public string getEmpDataIDCard(string data, string idcard)
        {
            string sql = "SELECT " + data + " FROM tbl_emp_profile WHERE emp_id_card = '" + idcard + "' AND emp_staus_working = '1' ORDER BY id DESC";
            string detali = "";
            MySqlDataReader rs = selectSQL(sql);
            if (rs.Read())
            {
                detali = rs.GetString(data);
                rs.Close();
                closeConn();
                return detali;
            }
            else { rs.Close(); closeConn(); return ""; }

        }

        public string getEmpIDMD5(string data, string empId)
        {
            string sql = "SELECT " + data + " FROM tbl_emp_profile JOIN tbl_profix ON emp_profix_id = profix_id JOIN tbl_cpoint ON emp_cpoint_id = cpoint_id JOIN tbl_pos ON emp_pos_id = pos_id JOIN tbl_affiliation ON affi_id = emp_affi_id JOIN tbl_type_emp ON type_emp_id = emp_type_emp_id LEFT JOIN tbl_type_add ON type_add_id = emp_add_type WHERE MD5(emp_id) = '" + empId + "' ORDER BY id DESC";
            string detali = "";
            MySqlDataReader rs = selectSQL(sql);
            if (rs.Read())
            {
                detali = rs.GetString(data);
                rs.Close(); closeConn();
                return detali;
            }
            else { rs.Close(); closeConn(); return ""; }

        }

        public string getCpointData(string data, string cpoint)
        {
            string sql = "SELECT " + data + " FROM tbl_cpoint WHERE cpoint_id = '" + cpoint + "'";
            string detali = "";
            MySqlDataReader rs = selectSQL(sql);
            if (rs.Read())
            {
                detali = rs.GetString(data);
                rs.Close(); closeConn();
                return detali;
            }
            else { rs.Close(); closeConn(); return ""; }

        }

        public MySqlDataAdapter getDataSelect(string sql)
        {
            openConn();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = sql;
            try
            {
                //Conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                closeConn();
                return da;
            }
            catch (Exception e)
            {
                closeConn();
                return null;
            }
        }

        public string convertDateShortThai(string dateThai)
        {
            try
            {
                string[] subDate = dateThai.Split('-');
                switch (subDate[1])
                {
                    case "01":
                        subDate[1] = "ม.ค.";
                        break;
                    case "02":
                        subDate[1] = "ก.พ.";
                        break;
                    case "03":
                        subDate[1] = "มี.ค.";
                        break;
                    case "04":
                        subDate[1] = "เม.ย.";
                        break;
                    case "05":
                        subDate[1] = "พ.ค.";
                        break;
                    case "06":
                        subDate[1] = "มิ.ย.";
                        break;
                    case "07":
                        subDate[1] = "ก.ค.";
                        break;
                    case "08":
                        subDate[1] = "ส.ค.";
                        break;
                    case "09":
                        subDate[1] = "ก.ย.";
                        break;
                    case "10":
                        subDate[1] = "ต.ค.";
                        break;
                    case "11":
                        subDate[1] = "พ.ย.";
                        break;
                    case "12":
                        subDate[1] = "ธ.ค.";
                        break;
                    case "00":
                        return "ปัจจุบัน";
                }
                return int.Parse(subDate[0]) + " " + subDate[1] + " " + subDate[2];
            }
            catch
            {
                return "";
            }
        }

        public string convertDatelongThai(string dateThai)
        {
            try
            {
                string[] subDate = dateThai.Split('-');
                switch (subDate[1])
                {
                    case "01":
                        subDate[1] = "มกราคม";
                        break;
                    case "02":
                        subDate[1] = "กุมภาพันธ์";
                        break;
                    case "03":
                        subDate[1] = "มีนาคม";
                        break;
                    case "04":
                        subDate[1] = "เมษายน";
                        break;
                    case "05":
                        subDate[1] = "พฤษภาคม";
                        break;
                    case "06":
                        subDate[1] = "มิถุนายน";
                        break;
                    case "07":
                        subDate[1] = "กรกฎาคม";
                        break;
                    case "08":
                        subDate[1] = "สิงหาคม";
                        break;
                    case "09":
                        subDate[1] = "กันยายน";
                        break;
                    case "10":
                        subDate[1] = "ตุลาคม";
                        break;
                    case "11":
                        subDate[1] = "พฤศจิกายน";
                        break;
                    case "12":
                        subDate[1] = "ธันวาคม";
                        break;
                    case "00":
                        return "ปัจจุบัน";
                }
                return int.Parse(subDate[0]) + " " + subDate[1] + " " + subDate[2];
            }
            catch
            {
                return "";
            }
        }

        public bool getPrivilege(string privilegeId, int privilege)
        {
            if (int.Parse(privilegeId) == privilege)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setEmpStatus(string id, string status)
        {
            string sql_update = "UPDATE tbl_emp_profile SET emp_staus_working = '" + status + "' WHERE id='" + id + "'";
            actionSql(sql_update);
        }

        public string getMd5Hash(string input)
        { // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create(); // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes // and create a string.
            StringBuilder sBuilder = new StringBuilder(); // Loop through each byte of the hashed data // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public string createEmpId(TextBox txtDateStart)
        {
            string empId = "";
            Random random = new Random();
            string[] splitDate = txtDateStart.Text.Split('-');
            empId += splitDate[2].Substring(2, 2) + splitDate[1] + splitDate[0] + random.Next(1000, 9999);
            bool check = true;
            while (check)
            {
                MySqlDataReader rs = selectSQL("SELECT * FROM tbl_emp_profile WHERE emp_id = '" + empId + "'");
                if (!rs.Read())
                {
                    check = false;
                }
                rs.Close();
                closeConn();
            }
            return empId;
        }

        public string getBudgetYear()
        {
            if (DateTime.Now.Month < 10)
            {
                return (DateTime.Now.Year + 543).ToString();
            }
            else
            {
                return (DateTime.Now.Year + 544).ToString();
            }
        }

        public string getBudgetYear(string date)
        {
            string d = date.Split('-')[0] + "-" + date.Split('-')[1] + "-" + (int.Parse(date.Split('-')[2]) - 543);
            DateTime dateTime = DateTime.ParseExact(d, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            if (dateTime.Month < 10)
            {
                return (dateTime.Year + 543).ToString();
            }
            else
            {
                return (dateTime.Year + 544).ToString();
            }
        }

        public void GetDownList(DropDownList list, string sql, string text, string value)
        {
            //MySqlDataReader rs = selectSQL(sql);
            using (var reader = selectSQL(sql))
            {

                if (reader.HasRows)
                {
                    list.DataSource = reader;
                    list.DataValueField = value;
                    list.DataTextField = text;
                    list.DataBind();
                }
                reader.Close();
            }
        }

        public Boolean checkDupicalIDCard(string idcard)
        {
            string sql = "SELECT * FROM tbl_emp_profile WHERE emp_id_card = '" + idcard + "' AND emp_staus_working = '1'";
            MySqlDataReader rs = selectSQL(sql);
            if (rs.Read())
            {
                rs.Close();
                CloseConnection();
                return false;
            }
            else
            {
                rs.Close();
                CloseConnection();
                return true;
            }
            
        }

        public Boolean checkIDCard(String PID)
        {
            string digit = null;

            //ตรวจสอบว่าทุก ๆ ตัวอักษรเป็นตัวเลข

            if (PID.ToCharArray().All(c => char.IsNumber(c)) == false)

                return false;

            //ตรวจสอบว่าข้อมูลมีทั้งหมด 13 ตัวอักษร
            if (PID.Trim().Length != 13)
                return false;
            int sumValue = 0;
            for (int i = 0; i < PID.Length - 1; i++)
                sumValue += int.Parse(PID[i].ToString()) * (13 - i);
            int v = 11 - (sumValue % 11);
            if (v.ToString().Length == 2)
            {
                digit = v.ToString().Substring(1, 1);
            }
            else
            {
                digit = v.ToString();
            }
            return PID[12].ToString() == digit;
        }

        public void userLoginUpdate(string user)
        {
            string sql = "UPDATE tbl_emp_user SET emp_login_time = NOW(), emp_status_login ='1' WHERE emp_user_name = '" + user + "'";
            if (user != "") { actionSql(sql); }
            userLogOutTimeUpdate();
        }

        public void userLogOutTimeUpdate()
        {
            string sql = "UPDATE tbl_emp_user SET emp_status_login = '0' WHERE DATE_ADD(emp_login_time,INTERVAL 15 MINUTE) <= NOW()";
            actionSql(sql);
        }

        public void userLogOutUpdate(string user)
        {
            string sql = "UPDATE tbl_emp_user SET emp_status_login ='0' WHERE emp_user_name = '" + user + "'";
            if (user != "") { actionSql(sql); }
            userLogOutTimeUpdate();
        }

        public void UpdateEmpEx()
        {
            string sql = "SELECT * FROM tbl_tmp_ex WHERE DATE_ADD(STR_TO_DATE(tmp_ex_date, '%d-%m-%Y'),INTERVAL -543 YEAR) <= CURDATE() AND tmp_ex_status = 0 AND tmp_ex_status_approve = 1";
            MySqlDataReader rs = selectSQL(sql);
            int i = 0;
            TmpExitEmp[] tmpExits = new TmpExitEmp[ArraySize];
            while (rs.Read())
            {
                tmpExits[i] = new TmpExitEmp(rs.GetString("tmp_ex_id"), rs.GetString("tmp_ex_emp"), rs.GetString("tmp_ex_status"), rs.GetString("tmp_ex_date"), rs.GetString("tmp_ex_note"), rs.GetString("tmp_ex_working_status"));
                i++;
            }
            rs.Close();
            closeConn();

            foreach (TmpExitEmp tmp in tmpExits)
            {
                if (tmp != null)
                {
                    string sql_update = "UPDATE tbl_emp_profile SET emp_staus_working = '" + tmp.Tmp_ex_working_status + "' WHERE emp_id = '" + tmp.Tmp_ex_emp + "'";
                    actionSql(sql_update);
                    sql_update = "UPDATE tbl_history SET history_status_id = '" + tmp.Tmp_ex_working_status + "',history_date='" + tmp.Tmp_ex_date + "',history_note='" + tmp.Tmp_ex_note + "' WHERE history_emp_id = '" + getEmpData("id", tmp.Tmp_ex_emp) + "'";
                    actionSql(sql_update);
                    sql_update = "UPDATE tbl_tmp_ex SET tmp_ex_status = '1' WHERE tmp_ex_emp = '" + tmp.Tmp_ex_emp + "'";
                    actionSql(sql_update);
                }
            }
        }

        public void UpdateEmpCpoint()
        {
            string sql = "SELECT * FROM tbl_tmp_cpoint WHERE DATE_ADD(STR_TO_DATE(tmp_cpoint_date, '%d-%m-%Y'),INTERVAL -543 YEAR) <= CURDATE() AND tmp_cpoint_status = 0 AND tmp_cpoint_status_approve = 1";
            MySqlDataReader rs = selectSQL(sql);
            int i = 0;
            TmpCpointEmp[] tmpCpoint = new TmpCpointEmp[ArraySize];
            while (rs.Read())
            {
                tmpCpoint[i] = new TmpCpointEmp(rs.GetString("tmp_cpoint_id"), rs.GetString("tmp_cpoint_emp_id"), rs.GetString("tmp_cpoint_cpoint_id"), rs.GetString("tmp_cpoint_date"), rs.GetString("tmp_cpoint_status"), rs.GetString("tmp_cpoint_emp_pos"), rs.GetString("tmp_cpoint_emp_aff"), rs.GetString("tmp_cpoint_cpoint_old_id"));
                i++;
            }
            rs.Close();
            closeConn();

            foreach (TmpCpointEmp tmp in tmpCpoint)
            {
                if (tmp != null)
                {
                    string sql_update = "UPDATE tbl_work_history SET work_history_out = '" + new DBScript().DateCalculation(tmp.Tmp_cpoint_date, -1) + "' where work_history_emp_id = '" + tmp.Tmp_cpoint_emp_id + "' AND work_history_out = '00-00-0000'";
                    actionSql(sql_update);
                    sql_update = "insert into tbl_work_history (work_history_in,work_history_out,work_history_pos,work_history_aff,work_history_cpoint,work_history_emp_id) values ('" + tmp.Tmp_cpoint_date + "','00-00-0000','" + new DBScript().getEmpData("emp_pos_id", tmp.Tmp_cpoint_emp_id) + "','" + new DBScript().getEmpData("emp_affi_id", tmp.Tmp_cpoint_emp_id) + "','" + tmp.Tmp_cpoint_cpoint_id + "','" + tmp.Tmp_cpoint_emp_id + "')";
                    actionSql(sql_update);
                    sql_update = "UPDATE tbl_emp_profile SET emp_cpoint_id = '" + tmp.Tmp_cpoint_cpoint_id + "' WHERE emp_id = '" + tmp.Tmp_cpoint_emp_id + "'";
                    actionSql(sql_update);
                    sql_update = "UPDATE tbl_tmp_cpoint SET tmp_cpoint_status = '1' WHERE tmp_cpoint_emp_id = '" + tmp.Tmp_cpoint_emp_id + "' AND tmp_cpoint_status = '0'";
                    actionSql(sql_update);
                }
            }
        }

        public void UpdateEmpPos()
        {
            string sql = "SELECT * FROM tbl_tmp_pos WHERE DATE_ADD(STR_TO_DATE(tmp_pos_date, '%d-%m-%Y'),INTERVAL -543 YEAR) <= CURDATE() AND tmp_pos_status = 0 AND tmp_pos_status_approve = 1";
            MySqlDataReader rs = selectSQL(sql);
            int i = 0;
            TmpPosEmp[] tmpPos = new TmpPosEmp[ArraySize];
            while (rs.Read())
            {
                tmpPos[i] = new TmpPosEmp(rs.GetString("tmp_pos_pos_id"), rs.GetString("tmp_pos_emp_id"), rs.GetString("tmp_pos_pos_old_id"), rs.GetString("tmp_pos_aff_old_id"), rs.GetString("tmp_pos_emp_type_old_id"), rs.GetString("tmp_pos_pos_id"), rs.GetString("tmp_pos_aff_id"), rs.GetString("tmp_pos_emp_type_id"), rs.GetString("tmp_pos_date"), rs.GetString("tmp_pos_status"));
                i++;
            }
            rs.Close();
            closeConn();

            foreach (TmpPosEmp tmp in tmpPos)
            {
                if (tmp != null)
                {
                    string sql_update = "UPDATE tbl_exp_moterway SET exp_moterway_end = '" + new DBScript().DateCalculation(tmp.Tmp_pos_date, -1) + "' WHERE exp_moterway_emp_id = '" + tmp.Tmp_pos_emp_id + "' AND exp_moterway_end = '00-00-0000'";
                    actionSql(sql_update);
                    sql_update = "insert into tbl_exp_moterway (exp_moterway_start,exp_moterway_end,exp_moterway_pos,exp_moterway_saraly,exp_moterway_emp_id) ";
                    sql_update += " values ('" + tmp.Tmp_pos_date + "','00-00-0000','" + tmp.Tmp_pos_pos_id + "','0','" + tmp.Tmp_pos_emp_id + "')";
                    actionSql(sql_update);
                    sql_update = "UPDATE tbl_emp_profile SET emp_pos_id = '" + tmp.Tmp_pos_pos_id + "',emp_affi_id = '" + tmp.Tmp_pos_aff_id + "',emp_type_emp_id = '" + tmp.Tmp_pos_emp_type_id + "' WHERE emp_id = '" + tmp.Tmp_pos_emp_id + "'";
                    actionSql(sql_update);
                    sql_update = "UPDATE tbl_tmp_pos SET tmp_pos_status = '1' WHERE tmp_pos_emp_id = '" + tmp.Tmp_pos_emp_id + "' AND tmp_pos_status = '0'";
                    actionSql(sql_update);
                }
            }
        }

        public string DateCalculation(string date, int cal)
        {
            DateTime ds = DateTime.ParseExact(date, "dd-MM-" + (int.Parse(date.Split('-')[2])), CultureInfo.InvariantCulture);
            ds.AddDays(cal);
            return ds.ToString("dd-MM-") + (int.Parse(ds.ToString("yyyy")) + 543);
        }

        public DateTime DateCalculationK(string date, int cal)
        {
            string date_txt = date.Split('-')[0] + "-" + date.Split('-')[1] + "-" + (int.Parse(date.Split('-')[2]) - 543);
            DateTime ds = DateTime.ParseExact(date_txt, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            ds.AddDays(cal);
            return ds;
        }

        public void CreateMotowayWorking()
        {
            string sql = "SELECT * FROM tbl_exp_moterway RIGHT JOIN tbl_emp_profile ON emp_id = exp_moterway_emp_id WHERE exp_moterway_start IS NULL";
            //MySqlDataReader rs = selectSQL(sql);
            DataTable table = new DataTable();
            MySqlDataAdapter da = getDataSelect(sql);
            da.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                //TextBox1.Text = row["ImagePath"].ToString();
                string sql_insert = "INSERT INTO tbl_exp_moterway (exp_moterway_start,exp_moterway_end,exp_moterway_pos,exp_moterway_emp_id) VALUES ";
                sql_insert += " ('" + row["emp_start_working"].ToString() + "','00-00-0000','" + row["emp_pos_id"].ToString() + "','" + row["emp_id"].ToString() + "')";
                actionSql(sql_insert);
            }
        }

        public void CreateCpointWorking()
        {
            string sql = "SELECT * FROM tbl_work_history RIGHT JOIN tbl_emp_profile ON emp_id = work_history_emp_id WHERE work_history_in IS NULL";
            //MySqlDataReader rs = selectSQL(sql);
            DataTable table = new DataTable();
            MySqlDataAdapter da = getDataSelect(sql);
            da.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                //TextBox1.Text = row["ImagePath"].ToString();
                string sql_insert = "INSERT INTO tbl_work_history (work_history_in,work_history_out,work_history_pos,work_history_aff,work_history_cpoint,work_history_emp_id) VALUES ";
                sql_insert += " ('" + row["emp_start_working"].ToString() + "','00-00-0000','" + row["emp_pos_id"].ToString() + "','" + row["emp_affi_id"].ToString() + "','" + row["emp_cpoint_id"].ToString() + "','" + row["emp_id"].ToString() + "')";
                actionSql(sql_insert);
            }
        }

        public bool CheckPrivilege(string privilege, string allow)
        {
            switch (allow)
            {
                case "SupperAdmin":
                    if (privilege == "0")
                    {
                        return true;
                    }
                    break;
                case "Admin":
                    if (privilege == "0" || privilege == "1")
                    {
                        return true;
                    }
                    break;
                case "HR":
                    if (privilege == "0" || privilege == "1" || privilege == "2")
                    {
                        return true;
                    }
                    break;
                case "Assistant":
                    if (privilege == "0" || privilege == "1" || privilege == "2" || privilege == "3")
                    {
                        return true;
                    }
                    break;
                case "Accountant":
                    if (privilege == "0" || privilege == "1" || privilege == "2" || privilege == "3" || privilege == "4")
                    {
                        return true;
                    }
                    break;
                case "Employee":
                    if (privilege == "0" || privilege == "1" || privilege == "2" || privilege == "3" || privilege == "4" || privilege == "5")
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        public bool Notallow(string[] privilege, string session)
        {
            foreach (string right in privilege)
            {
                if (right == session)
                {
                    return true;
                }
            }
            return false;
        }

        public int selectCount(string table, string condition, string group)
        {
            int amount = 0;
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) AS num FROM " + table + " WHERE " + condition + " GROUP BY " + group;
            openConn();
            MySqlDataReader result = cmd.ExecuteReader();
            if (result.Read())
            {
                amount = (int)result.GetDecimal("num");
            }
            result.Close();
            closeConn();
            return amount;
        }

    }
}
