using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using System.Linq;
namespace HSBC_App
{
    public partial class Customer_Details : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["username"] == null) & (Session["password"] == null))
            {
                Response.Redirect("login.aspx");
            }

        }

        public void btn_AddNewCustClick(object sender, EventArgs e)
        {
            string masked = txt_masked.Text;
            string eidtmasked = masked.Substring(0, 3);
            if (txt_masked.Text != "" || txt_Email_ID.Text != "" || txt_Name.Text != "")
            {
                if (eidtmasked == "DPM")

                {
                    SqlConnection con1 = new SqlConnection(constr);
                    SqlCommand cmd1 = new SqlCommand("spcheckcustomer", con1);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@masked", SqlDbType.VarChar).Value = txt_masked.Text;
                    con1.Open();
                    Int32 usernamecount = (Int32)cmd1.ExecuteScalar();
                    if (usernamecount > 0)
                    {
                        lbl_errormsg.Text = "Customer details already Exist";
                        ModalPopupExtender1.Show();
                        con1.Close();
                    }
                    else
                    {
                        SqlConnection con = new SqlConnection(constr);
                        SqlCommand cmd = new SqlCommand("addnewcustomer", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("masked", txt_masked.Text);
                        cmd.Parameters.AddWithValue("name", txt_Name.Text);
                        cmd.Parameters.AddWithValue("email", txt_Email_ID.Text);
                        cmd.Parameters.AddWithValue("cc", txt_ccemail.Text);
                        cmd.Parameters.AddWithValue("pdfpass", txt_pdf_password.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("CustomerDetails.aspx");
                    }
                }
                else
                {
                    lbl_errormsg.Text = "Only Insert Masked name example DPMXXXX";
                    ModalPopupExtender1.Show();
                }
            }
            else
            {
                lbl_errormsg.Text = "All Fields are mandatory";
                ModalPopupExtender1.Show();
            }
        }

        protected void btn_download_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Customer Details.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                GridView1.Columns[4].Visible = false;
                GridView1.Columns[5].Visible = false;

                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                if (GridView1.HeaderRow != null)
                {
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView1.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void uploadexcel_click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (Path.GetExtension(FileUpload1.FileName) == ".xlsx")
                {
                    ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
                    GridView2.DataSource = package.ToDataTable();
                    GridView2.DataBind();
                    Bulk_Insert();
                }
            }
        }
        protected void Bulk_Insert()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Masked", typeof(string)),
                        new DataColumn("de_Masked", typeof(string)),
                        new DataColumn("Email",typeof(string)),
            new DataColumn("PDFPassword", typeof(string)) });
            foreach (GridViewRow row in GridView2.Rows)
            {
                string Masked = row.Cells[0].Text;
                string de_Masked = row.Cells[1].Text;
                string Email = row.Cells[2].Text;
                string PDFPassword = row.Cells[3].Text;
                dt.Rows.Add(Masked, de_Masked, Email, PDFPassword);
            }
            if (dt.Rows.Count > 0)
            {

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.master";

                        //[OPTIONAL]: Map the DataTable columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Masked", "Masked");
                        sqlBulkCopy.ColumnMappings.Add("De_Masked", "De_Masked");
                        sqlBulkCopy.ColumnMappings.Add("Email", "Email");
                        sqlBulkCopy.ColumnMappings.Add("PDFPassword", "PDFPassword");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                        Response.Redirect("/CustomerDetails.aspx");
                    }
                }
            }
        }
        public void btn_edit_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {

                int index = row.RowIndex; //gets the row index selected
                
                using (GridViewRow row1 = (GridViewRow)((LinkButton)sender).Parent.Parent)
                {
                    
                    txt_Editmasked.ReadOnly = true;
                    txt_Editmasked.Text = row1.Cells[0].Text;
                    txt_editcustname.Text = row1.Cells[1].Text;
                    txt_editcustemail.Text = row1.Cells[2].Text;
                    txt_editcustccemail.Text = row1.Cells[3].Text.Replace("&nbsp;", "");
                    SqlConnection con = new SqlConnection(constr);
                    SqlCommand cmd = new SqlCommand("spGetPDFPass", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@masked", txt_Editmasked.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    txt_editpdfpass.Text = dr["pdfpassword"].ToString();
                    dr.Close();
                    con.Close();
                    ModalPopupExtender3.Show();
                }
            }
        }
        public void btn_updateCustClick(object sender, EventArgs e)
        {
            insertintoaudit();
            updatecustomerdetails();
            Response.Redirect("/CustomerDetails.aspx");
        }
        public void insertintoaudit()
        {
            //enter in audit log file
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("spaeditcustaudit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", Session["username"]);
            cmd.Parameters.AddWithValue("@custmasked", txt_Editmasked.Text);
            cmd.Parameters.AddWithValue("@custname", txt_editcustname.Text);
            cmd.Parameters.AddWithValue("@custemail", txt_editcustemail.Text);
            cmd.Parameters.AddWithValue("@custccemail", txt_editcustccemail.Text);
            cmd.Parameters.AddWithValue("@custpdfpass", txt_editpdfpass.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void updatecustomerdetails()
        {
            //update customerdetails
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("spupdatecustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@custmasked", txt_Editmasked.Text);
            cmd.Parameters.AddWithValue("@custname", txt_editcustname.Text);
            cmd.Parameters.AddWithValue("@custemail", txt_editcustemail.Text);
            cmd.Parameters.AddWithValue("@custccemail", txt_editcustccemail.Text);
            cmd.Parameters.AddWithValue("@custpdfpass", txt_editpdfpass.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void btn_delete_Click(object sender, EventArgs e)
        {
           LinkButton lb = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {

                int index = row.RowIndex; //gets the row index selected
                //SqlCommand cmd = new SqlCommand("select * from exceptions where id='"+index+"'", con);
                using (GridViewRow row1 = (GridViewRow)((LinkButton)sender).Parent.Parent)
                {                   
                  string masked = row1.Cells[0].Text;
                    SqlConnection con = new SqlConnection(constr);
                    SqlCommand cmd = new SqlCommand("spdeletecustomerdetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@masked", masked);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("/customerdetails.aspx");
                }
            }
        }
    }
}