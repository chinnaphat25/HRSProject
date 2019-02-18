using HRSProject.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRSProject.Manpower
{
    public partial class index : System.Web.UI.Page
    {
        DBScript dBScript = new DBScript();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                string sql_year = "SELECT * FROM tbl_year";
                GetDownList(txtYear, sql_year, "year", "year");
                getMampowerData(dBScript.getBudgetYear());
                getMampowerFullData(dBScript.getBudgetYear());
                GetDatailManpower(dBScript.getBudgetYear());
                Title = "อัตรากำลัง ปีงบประมาณ " + dBScript.getBudgetYear();
                //rowShow.InnerHtml += "<div class='col-md'>55555</ div > ";
            }

            //if (GridViewDetail.InnerHtml.ToString() == "")
            //{
                GetDatailManpower(dBScript.getBudgetYear());
            //}
        }
        public void GetDownList(DropDownList list, string sql, string text, string value)
        {
            MySqlDataReader rs = dBScript.selectSQL(sql);
            using (var reader = dBScript.selectSQL(sql))
            {
                if (reader.HasRows)
                {
                    list.DataSource = reader;
                    list.DataValueField = value;
                    list.DataTextField = text;
                    list.DataBind();
                }
            }
        }

        public string[] getAffManpower(string year)
        {
            string sql = "SELECT manpower_pos_id,affi_name FROM tbl_manpower LEFT JOIN tbl_emp_profile ON manpower_cpoint_id = emp_cpoint_id AND emp_staus_working = 1 LEFT JOIN tbl_cpoint ON manpower_cpoint_id = cpoint_id LEFT JOIN tbl_affiliation ON affi_id = manpower_pos_id WHERE manpower_year = '" + year + "' GROUP BY manpower_pos_id ORDER BY affi_name";
            int count = 0;
            string[] data = new string[100];
            MySqlDataReader rs = dBScript.selectSQL(sql);
            while (rs.Read())
            {
                data[count] = rs.GetString("manpower_pos_id");
                count++;
            }
            rs.Close();
            return data;
        }

        public void getMampowerData(string year)
        {
            string sql = "SELECT pos_id,pos_name, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '701' THEN tbl_emp_profile.emp_id END ) AS LB, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '702' THEN tbl_emp_profile.emp_id END ) AS BB, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '703' THEN tbl_emp_profile.emp_id END ) AS BK, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '704' THEN tbl_emp_profile.emp_id END ) AS PN, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '706' THEN tbl_emp_profile.emp_id END ) AS BG, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '707' THEN tbl_emp_profile.emp_id END ) AS BP, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '708' THEN tbl_emp_profile.emp_id END ) AS NK, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '709' THEN tbl_emp_profile.emp_id END ) AS PO, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '710' THEN tbl_emp_profile.emp_id END ) AS PY, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '902' THEN tbl_emp_profile.emp_id END ) AS TC1, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '903' THEN tbl_emp_profile.emp_id END ) AS TC2, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '904' THEN tbl_emp_profile.emp_id END ) AS TY1, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '905' THEN tbl_emp_profile.emp_id END ) AS TY2, COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id LIKE '60%' THEN tbl_emp_profile.emp_id END ) AS Center, ( COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '701' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '702' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '703' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '704' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '706' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '707' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '708' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '709' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '710' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '902' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '903' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '904' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id = '905' THEN tbl_emp_profile.emp_id END ) + COUNT( CASE WHEN tbl_emp_profile.emp_cpoint_id LIKE '60%' THEN tbl_emp_profile.emp_id END ) ) AS total FROM tbl_manpower LEFT JOIN tbl_emp_profile ON manpower_cpoint_id = emp_cpoint_id AND manpower_pos_id = emp_pos_id AND emp_staus_working = 1 LEFT JOIN tbl_cpoint ON manpower_cpoint_id = cpoint_id LEFT JOIN tbl_pos ON pos_id = manpower_pos_id WHERE manpower_year = '" + year + "' AND pos_name IS NOT NULL GROUP BY manpower_pos_id ORDER BY pos_priorty ASC";
            //MySqlDataReader rs = dBScript.selectSQL(sql);

            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ManpowerSubGridView.DataSource = dt;
            ManpowerSubGridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getMampowerData(txtYear.SelectedValue);
            getMampowerFullData(txtYear.SelectedValue);
            Title = "อัตรากำลัง ปีงบประมาณ " + txtYear.SelectedValue;
        }

        public void getMampowerFullData(string year)
        {
            string sql = "SELECT T1.pos_name, T1.manpower_full, IF(T2.num IS NULL, 0, T2.num) AS num, ( T1.manpower_full - IF(T2.num IS NULL, 0, T2.num) ) AS dif FROM (SELECT pos_id, pos_name, SUM(manpower_full) AS manpower_full,pos_priorty FROM tbl_manpower JOIN tbl_pos ON pos_id = manpower_pos_id WHERE manpower_year = '" + year + "' GROUP BY pos_id) T1 LEFT JOIN (SELECT pos_id, pos_name, COUNT(emp_id) AS num FROM tbl_emp_profile JOIN tbl_pos ON pos_id = emp_pos_id WHERE emp_staus_working = 1 GROUP BY emp_pos_id) T2 ON T1.pos_id = T2.pos_id ORDER BY T1.pos_priorty asc";
            //MySqlDataReader rs = dBScript.selectSQL(sql);

            MySqlDataAdapter da = dBScript.getDataSelect(sql);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ManpowerSumGridView.DataSource = dt;
            ManpowerSumGridView.DataBind();
        }

        public string getColorNum(string num)
        {
            int number = int.Parse(num);
            if (number < 0) { return "<td class='badge badge-warning'>&nbsp" + num + "&nbsp</td>"; }
            if (number > 0) { return "<td class='badge badge-danger'>&nbsp" + num + "&nbsp</td>"; }
            return "<td class='badge badge-success'>&nbsp" + num + "&nbsp</td>";
        }
        decimal total = 0;
        decimal total1 = 0;
        decimal total2 = 0;
        protected void ManpowerSumGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                total = total + Convert.ToDecimal(e.Row.Cells[1].Text);
                total1 = total1 + Convert.ToDecimal(e.Row.Cells[2].Text);
                total2 = total2 + Convert.ToDecimal(e.Row.Cells[3].Text);

                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "<b>รวม</b>";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = total.ToString("0");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Text = total1.ToString("0");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].Text = total2.ToString("0");
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        decimal totalLB = 0;
        decimal totalBB = 0;
        decimal totalBK = 0;
        decimal totalPN = 0;
        decimal totalBG = 0;
        decimal totalBP = 0;
        decimal totalNK = 0;
        decimal totalPO = 0;
        decimal totalPY = 0;
        decimal totalTC1 = 0;
        decimal totalTC2 = 0;
        decimal totalTY1 = 0;
        decimal totalTY2 = 0;
        decimal totalCenter = 0;
        protected void ManpowerSubGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton lableLB = (LinkButton)(e.Row.FindControl("lableLB"));
            if (lableLB != null)
            {
                lableLB.Text = DataBinder.Eval(e.Row.DataItem, "LB").ToString();
                lableLB.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString()+"#701";
            }
            LinkButton lableBB = (LinkButton)(e.Row.FindControl("lableBB"));
            if (lableBB != null)
            {
                lableBB.Text = DataBinder.Eval(e.Row.DataItem, "BB").ToString();
                lableBB.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#702";
            }
            LinkButton lableBK = (LinkButton)(e.Row.FindControl("lableBK"));
            if (lableBK != null)
            {
                lableBK.Text = DataBinder.Eval(e.Row.DataItem, "BK").ToString();
                lableBK.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#703";
            }
            LinkButton lablePN = (LinkButton)(e.Row.FindControl("lablePN"));
            if (lablePN != null)
            {
                lablePN.Text = DataBinder.Eval(e.Row.DataItem, "PN").ToString();
                lablePN.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#704";
            }
            LinkButton lableBG = (LinkButton)(e.Row.FindControl("lableBG"));
            if (lableBG != null)
            {
                lableBG.Text = DataBinder.Eval(e.Row.DataItem, "BG").ToString();
                lableBG.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#706";
            }
            LinkButton lableBP = (LinkButton)(e.Row.FindControl("lableBP"));
            if (lableBP != null)
            {
                lableBP.Text = DataBinder.Eval(e.Row.DataItem, "BP").ToString();
                lableBP.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#707";
            }
            LinkButton lableNK = (LinkButton)(e.Row.FindControl("lableNK"));
            if (lableNK != null)
            {
                lableNK.Text = DataBinder.Eval(e.Row.DataItem, "NK").ToString();
                lableNK.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#708";
            }
            LinkButton lablePO = (LinkButton)(e.Row.FindControl("lablePO"));
            if (lablePO != null)
            {
                lablePO.Text = DataBinder.Eval(e.Row.DataItem, "PO").ToString();
                lablePO.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#709";
            }
            LinkButton lablePY = (LinkButton)(e.Row.FindControl("lablePY"));
            if (lablePY != null)
            {
                lablePY.Text = DataBinder.Eval(e.Row.DataItem, "PY").ToString();
                lablePY.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#710";
            }
            LinkButton lableTC1 = (LinkButton)(e.Row.FindControl("lableTC1"));
            if (lableTC1 != null)
            {
                lableTC1.Text = DataBinder.Eval(e.Row.DataItem, "TC1").ToString();
                lableTC1.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#902";
            }
            LinkButton lableTC2 = (LinkButton)(e.Row.FindControl("lableTC2"));
            if (lableTC2 != null)
            {
                lableTC2.Text = DataBinder.Eval(e.Row.DataItem, "TC2").ToString();
                lableTC2.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#903";
            }
            LinkButton lableTY1 = (LinkButton)(e.Row.FindControl("lableTY1"));
            if (lableTY1 != null)
            {
                lableTY1.Text = DataBinder.Eval(e.Row.DataItem, "TY1").ToString();
                lableTY1.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#904";
            }
            LinkButton lableTY2 = (LinkButton)(e.Row.FindControl("lableTY2"));
            if (lableTY2 != null)
            {
                lableTY2.Text = DataBinder.Eval(e.Row.DataItem, "TY2").ToString();
                lableTY2.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#905";
            }
            LinkButton lableCenter = (LinkButton)(e.Row.FindControl("lableCenter"));
            if (lableCenter != null)
            {
                lableCenter.Text = DataBinder.Eval(e.Row.DataItem, "Center").ToString();
                lableCenter.CommandName = DataBinder.Eval(e.Row.DataItem, "pos_id").ToString() + "#60";
            }
            Label lableTotalAll = (Label)(e.Row.FindControl("lableTotalAll"));
            if (lableTotalAll != null)
            {
                lableTotalAll.Text = DataBinder.Eval(e.Row.DataItem, "total").ToString();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalLB = totalLB + Convert.ToDecimal(int.Parse(lableLB.Text));
                totalBB = totalBB + Convert.ToDecimal(int.Parse(lableBB.Text));
                totalBK = totalBK + Convert.ToDecimal(int.Parse(lableBK.Text));
                totalPN = totalPN + Convert.ToDecimal(int.Parse(lablePN.Text));
                totalBG = totalBG + Convert.ToDecimal(int.Parse(lableBG.Text));
                totalBP = totalBP + Convert.ToDecimal(int.Parse(lableBP.Text));
                totalNK = totalNK + Convert.ToDecimal(int.Parse(lableNK.Text));
                totalPO = totalPO + Convert.ToDecimal(int.Parse(lablePO.Text));
                totalPY = totalPY + Convert.ToDecimal(int.Parse(lablePY.Text));
                totalTC1 = totalTC1 + Convert.ToDecimal(int.Parse(lableTC1.Text));
                totalTC2 = totalTC2 + Convert.ToDecimal(int.Parse(lableTC2.Text));
                totalTY1 = totalTY1 + Convert.ToDecimal(int.Parse(lableTY1.Text));
                totalTY2 = totalTY2 + Convert.ToDecimal(int.Parse(lableTY2.Text));
                totalCenter = totalCenter + Convert.ToDecimal(int.Parse(lableCenter.Text));

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[15].BackColor = ColorTranslator.FromHtml("#990000");
                e.Row.Cells[15].ForeColor = Color.White;
                e.Row.Cells[15].Font.Bold = true;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "<b>รวม</b>";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = totalLB.ToString("0");
                e.Row.Cells[2].Text = totalBB.ToString("0");
                e.Row.Cells[3].Text = totalBK.ToString("0");

                e.Row.Cells[4].Text = totalPN.ToString("0");
                e.Row.Cells[5].Text = totalBG.ToString("0");
                e.Row.Cells[6].Text = totalBP.ToString("0");
                e.Row.Cells[7].Text = totalNK.ToString("0");
                e.Row.Cells[8].Text = totalPO.ToString("0");
                e.Row.Cells[9].Text = totalPY.ToString("0");
                e.Row.Cells[10].Text = totalTC1.ToString("0");
                e.Row.Cells[11].Text = totalTC2.ToString("0");
                e.Row.Cells[12].Text = totalTY1.ToString("0");
                e.Row.Cells[13].Text = totalTY2.ToString("0");
                e.Row.Cells[14].Text = totalCenter.ToString("0");
                e.Row.Cells[15].Text = (totalLB + totalBB + totalBK + totalPN + totalBG + totalBP + totalNK + totalPO + totalPY + totalTC1 + totalTC2 + totalTY1 + totalTY2 + totalCenter).ToString("0");

            }
        }

        decimal totalFull = 0;
        decimal totalCurrent = 0;
        decimal totalDiff = 0;
        private void GetDatailManpower(string year)
        {
            string sql_select = "SELECT pos.pos_id,pos.pos_name FROM tbl_manpower mp JOIN tbl_pos pos ON pos.pos_id = mp.manpower_pos_id GROUP BY pos.pos_id ORDER BY pos.pos_priorty";
            MySqlDataAdapter da = dBScript.getDataSelect(sql_select);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds);
            dt = ds.Tables[0];
            //GridViewDetail.Controls.Add(new LiteralControl("<div class='row'>"));
            foreach (DataRow dr in dt.Rows)
            {
                totalFull = 0;
                totalCurrent = 0;
                totalDiff = 0;

                GridView gridView = new GridView() { ShowFooter = true };
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableHeaderCell cell = new TableHeaderCell();
                cell.Text = "อัตรากำลัง" + dr[1];
                cell.ColumnSpan = 4;
                row.Controls.Add(cell);
                row.BackColor = Color.DarkRed;


                string sql_Grid = "SELECT cpoint_name AS 'ด่าน', manpower_full AS 'อัตราเต็ม', COUNT(emp_id) AS 'อัตราปัจจุบัน', (manpower_full - COUNT(emp_id)) AS 'อัตราขาด/เกิน' FROM tbl_manpower LEFT JOIN tbl_emp_profile ON manpower_cpoint_id = emp_cpoint_id AND manpower_pos_id = emp_pos_id AND emp_staus_working = 1 LEFT JOIN tbl_cpoint ON manpower_cpoint_id = cpoint_id LEFT JOIN tbl_pos ON pos_id = manpower_pos_id WHERE manpower_pos_id = '" + dr[0] + "' AND manpower_year = '" + year + "'   GROUP BY manpower_pos_id, manpower_cpoint_id ORDER BY manpower_cpoint_id";
                MySqlDataAdapter daGrid = dBScript.getDataSelect(sql_Grid);
                DataTable dtGrid = new DataTable();
                //dtGrid.Columns.Add(new DataColumn("อัตรากำลัง"+dr[1]));
                daGrid.Fill(dtGrid);

                gridView.RowDataBound += new GridViewRowEventHandler(gdv_RowDataBound);
                gridView.DataSource = dtGrid;
                gridView.DataBind();
                try { gridView.HeaderRow.Parent.Controls.AddAt(0, row); } catch { }

                gridView.CssClass = "table table-sm";

                if (gridView.DataSource != null)
                {
                    GridViewDetail.Controls.Add(new LiteralControl("<div class='col-md-4'>"));

                    StyleGridView(gridView, HorizontalAlign.Center, HorizontalAlign.Center);
                    GridViewDetail.Controls.Add(gridView);

                    GridViewDetail.Controls.Add(new LiteralControl("</div>"));
                    GridViewDetail.Controls.Add(new LiteralControl("&nbsp"));
                }
            }
            //GridViewDetail.Controls.Add(new LiteralControl("</div>"));

        }

        private void StyleGridView(GridView gridView, HorizontalAlign H_align, HorizontalAlign R_align)
        {
            gridView.HeaderStyle.Font.Bold = true;
            gridView.HeaderStyle.HorizontalAlign = H_align;
            gridView.HeaderStyle.BackColor = Color.DarkRed;
            gridView.HeaderStyle.ForeColor = Color.White;

            gridView.RowStyle.HorizontalAlign = R_align;
            gridView.RowStyle.BackColor = Color.LightYellow;

            gridView.FooterStyle.HorizontalAlign = R_align;
            gridView.FooterStyle.Font.Bold = true;
            gridView.FooterStyle.BackColor = Color.DarkRed;
            gridView.FooterStyle.ForeColor = Color.White;
        }


        protected void gdv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.Cells[0].Text = "Date";
                e.Row.Cells[0].Text = "ด่านฯ";
                e.Row.Cells[1].Text = "อัตราเต็ม";
                e.Row.Cells[2].Text = "อัตราปัจุบัน";
                e.Row.Cells[3].Text = "อัตราขาด/เกิน";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalFull = totalFull + Convert.ToDecimal(e.Row.Cells[1].Text);
                totalCurrent = totalCurrent + Convert.ToDecimal(e.Row.Cells[2].Text);
                totalDiff = totalDiff + Convert.ToDecimal(e.Row.Cells[3].Text);
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].ForeColor = Color.Red;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "<b>รวม</b>";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = totalFull.ToString("0");
                e.Row.Cells[2].Text = totalCurrent.ToString("0");
                e.Row.Cells[3].Text = totalDiff.ToString("0");
            }
        }

        protected void lableLB_Command(object sender, CommandEventArgs e)
        {
            string pos_id = e.CommandName.ToString().Split('#')[0];
            string cpoint_id = e.CommandName.ToString().Split('#')[1];
            //Response.Redirect("/Profile/empViwe?pos=" + pos_id + "&" + "cpoint=" + cpoint_id);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Profile/empViwe?pos=" + pos_id + "&" + "cpoint=" + cpoint_id+"','_newtab');", true);
        }
    }
}