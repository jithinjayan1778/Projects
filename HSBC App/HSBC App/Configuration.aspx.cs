using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace HSBC_App
{
    public partial class Configuration : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["username"] == null) & (Session["password"] == null))
            {
                Response.Redirect("login.aspx");
            }
            txt_EmailID.Enabled = false;
            txt_FTPURLDestination.Enabled = false;
            txt_FTPURLSource.Enabled = false;
            txt_password.Enabled = false;
            txt_SMTPPort.Enabled = false;
            txt_SMTPserver.Enabled = false;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("spconfiguration",con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            txt_EmailID.Text = dr["EmailID"].ToString();
            txt_FTPURLDestination.Text = dr["FTPURLdestination"].ToString();
            txt_FTPURLSource.Text = dr["FTPURLSource"].ToString();
            txt_password.Text = dr["password"].ToString();
            txt_SMTPPort.Text = dr["SMTP_Port"].ToString();
            txt_SMTPserver.Text = dr["SMTp_Server"].ToString();
            con.Close();
            
         }

        protected void btn_Edit_Click(object sender, EventArgs e)
        {
            txt_EmailID.Enabled = true;
            txt_FTPURLDestination.Enabled = true;
            txt_FTPURLSource.Enabled = true;
            txt_password.Enabled = true;
            txt_SMTPPort.Enabled = true;
            txt_SMTPserver.Enabled = true;
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("SPUpdateConfig",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SMTPserver",SqlDbType.VarChar).Value= txt_SMTPserver.Text;
            cmd.Parameters.Add("@SMTPPort", SqlDbType.VarChar).Value = txt_SMTPPort.Text;
            cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = txt_password.Text;
            cmd.Parameters.Add("@FTPURLSource", SqlDbType.VarChar).Value = txt_FTPURLSource.Text;
            cmd.Parameters.Add("@FTPURLDestination", SqlDbType.VarChar).Value = txt_FTPURLDestination.Text;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = txt_EmailID.Text;            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}