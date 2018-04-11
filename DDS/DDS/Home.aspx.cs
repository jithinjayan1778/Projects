using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Pdf.Text;
using Aspose.Pdf;
using System.IO;
using System.Net.Mail;
using System.Text;
using Aspose.Pdf.Facades;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDS
{
    public partial class Home : System.Web.UI.Page 
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        string newfilename;
        string custEmail;
        String source;
        string destination;
        string CustName;
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();            
            SqlCommand cmd = new SqlCommand("spconfiguration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            source = dr["FTPURLSource"].ToString();
            destination = dr["FTPURLDestination"].ToString();
            dr.Close();
            string mydir = source+@"\";
            string[] myfiles = Directory.GetFiles(mydir + "").Select(Path.GetFileName).ToArray();
            foreach (var i in myfiles)
            {
                string rowString = i.Substring(8, 6);
                string sub = rowString.Replace('"', ' ').Trim();                
                Document pdfDocument = new Document(mydir + i);
                //create TextAbsorber object to find all instances of the input search    phrase
                TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(sub);
                //accept the absorber for all the pages
                pdfDocument.Pages.Accept(textFragmentAbsorber);
                

                //get the extracted text fragments
                TextFragmentCollection textFragmentCollection = textFragmentAbsorber.TextFragments;
                //loop through the fragments             
                foreach (TextFragment textFragment in textFragmentCollection)
                {
                    //string str = "select de_masked from master where Masked='" + sub.ToString() + "'";
                    SqlCommand com = new SqlCommand("spGetMaster", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@masked", SqlDbType.VarChar).Value =sub;
                    com.ExecuteNonQuery();
                    SqlDataReader reader = com.ExecuteReader();
                    reader.Read();
                    //update text and other properties
                    CustName = reader["de_masked"].ToString();
                    textFragment.Text = CustName;
                    reader.Close();
                    

                }
                //setting password to pdf
                
                SqlCommand cmd1 = new SqlCommand("spGetMaster", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("masked", SqlDbType.VarChar).Value = sub;
                cmd1.ExecuteNonQuery();
                SqlDataReader rd = cmd1.ExecuteReader();
                rd.Read();
                string pass = rd["pdfpassword"].ToString();
                custEmail = rd["Email"].ToString();
                pdfDocument.Encrypt(pass, "", 0, CryptoAlgorithm.RC4x128);
                rd.Close();
                newfilename = i + ".pdf";
                pdfDocument.Save(destination+@"\"+ newfilename);
                //insertdata();        
                sendEmail();
                insertdata();
                //setpass();
                string oldfile = mydir + i;
                File.Delete(oldfile);
                 
            }
        }
        public void sendEmail()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("spconfiguration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            //host name 
            string host = rd["SMTP_Server"].ToString();
            //port
            string sport = rd["SMTP_Port"].ToString();
            int port = Convert.ToInt32(Convert.ToString(sport));
            String userName = rd["emailid"].ToString();
            String password = rd["password"].ToString();
            //source = rd["FTPURLSource"].ToString();
           // destination = rd["FTPURLDestination"].ToString();
            // mail msg (from , to)
            MailMessage msg = new MailMessage(userName, userName);
            msg.Subject = "Account Statement";
            StringBuilder sb = new StringBuilder();            
            sb.AppendLine("Dear Customer; ");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Email:");
            //sb.AppendLine("Drop Downlist Name:" + ddllinksource.SelectedValue.ToString());
            msg.Body = sb.ToString();
            Attachment attach = new Attachment((destination+@"\"+ newfilename));
            msg.Attachments.Add(attach);
            SmtpClient SmtpClient = new SmtpClient();
            SmtpClient.Credentials = new System.Net.NetworkCredential(userName,password);
            SmtpClient.Host = host;
            SmtpClient.Port = port;
            SmtpClient.EnableSsl = true;
            SmtpClient.Send(msg);
        }
        public void insertdata()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("spinsetmailsent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("username", CustName);
            cmd.Parameters.AddWithValue("Email", custEmail);
            cmd.Parameters.AddWithValue("ReportLocation", destination);
            cmd.Parameters.AddWithValue("Status", "Sent");
            cmd.Parameters.AddWithValue("date", DateTime.Now);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        
    }
}