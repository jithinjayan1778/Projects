using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HSBC_App
{
    public partial class MailSentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["username"] == null) & (Session["password"] == null))
            {
                Response.Redirect("login.aspx");
            }
        }
    }
}