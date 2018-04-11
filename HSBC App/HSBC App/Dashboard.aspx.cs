using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace HSBC_App
{
    public partial class Home : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["username"] == null) & (Session["password"] == null))
            {
                Response.Redirect("login.aspx");
            }
            getnoofcustomer();
            getmailsenttoday();
            getmailssentthismonth();

        }
        public void getnoofcustomer()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("spgetnoofcust", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            lbl_noofcustomers.Text = dr["count"].ToString();
            dr.Close();
            con.Close();
        }
        public void getmailsenttoday()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("spmailssenttoday", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();            
            dr.Read();
            lbl_NoOfMailssenttoday.Text = dr["count"].ToString();
            dr.Close();
            con.Close();
        }
        public void getmailssentthismonth()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("spmailssentcurrentmonth", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            lbl_mailssentthismonth.Text = dr["count"].ToString();
            dr.Close();
            con.Close();
        }
    }
}