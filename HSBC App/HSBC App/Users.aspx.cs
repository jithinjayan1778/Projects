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
    public partial class Users : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void btn_AddNewuser_Click(object sender,EventArgs e)
        {

            if (txt_username.Text != "" && txt_password.Text != "")
            {
                SqlConnection con1 = new SqlConnection(constr);
                SqlCommand cmd1 = new SqlCommand("spcheckusername", con1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@username", SqlDbType.VarChar).Value = txt_username.Text;
                con1.Open();
                Int32 usernamecount = (Int32)cmd1.ExecuteScalar();
                if (usernamecount > 0)
                {
                    lbl_errormsg.Text = "User Name already Exist";
                    ModalPopupExtender1.Show();
                    con1.Close();                 
                }
                else
                {
                    SqlConnection con = new SqlConnection(constr);
                    SqlCommand cmd = new SqlCommand("spinsertuser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("Username", txt_username.Text);
                    cmd.Parameters.AddWithValue("password", txt_password.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("Users.aspx");
                }
            }
            else
            {

                lbl_errormsg.Text = "Username or password is empty";
                ModalPopupExtender1.Show();
            }           
        }
        public byte checkusernameavailablity()
        {
            SqlConnection con1 = new SqlConnection(constr);
            SqlCommand cmd1 = new SqlCommand("spcheckusername", con1);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@username", SqlDbType.VarChar).Value = txt_username.Text;
            con1.Open();
            Int32 usernamecount = (Int32)cmd1.ExecuteScalar();                        
            if(usernamecount > 0)
            {
                lbl_errormsg.Text = "User Name already Exist";
                ModalPopupExtender1.Show();
                return 1;
            }
            con1.Close();
            return 0;
        }
    }
}