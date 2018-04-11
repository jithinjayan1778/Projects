using System;
using System.Linq;
using System.Text;
using Aspose.Pdf.Text;
using Aspose.Pdf;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace DDSconsole
{
    class Program
    {
        static string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        static string newfilename;
        static string custEmail;
        static string ccemail;
        static String source;
        static string destination;
        static string CustName;
        static string Emailbody;
        static string emailsubject;
        static void Main(string[] args)
        {
            //License license = new Aspose.Pdf.License();
            //license.SetLicense("Aspose.Pdf.lic");
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("spconfiguration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            source = dr["FTPURLSource"].ToString();
            destination = dr["FTPURLDestination"].ToString();
            dr.Close();
            con.Close();
            string mydir = source + @"\";
            string[] myfiles = Directory.GetFiles(mydir + "").Select(Path.GetFileName).ToArray();
            foreach (var i in myfiles)
            {
                if (i.Contains("HSBC"))
                {
                    string sub = "HSBC SA " + i.Split(' ', '.')[2];
                    string sub1 = i.Split(' ', '.')[2];
                    //string sub = str.Substring(8, 6);
                    //string sub1 = rowString.Substring(0, 3);
                    //string sub = rowString.Replace('"', ' ').Trim();
                    if (sub.Contains("DPM"))
                    {
                        Document pdfDocument = new Document(mydir + i);
                        //create TextAbsorber object to find all instances of the input search    phrase
                        TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(sub);
                        //accept the absorber for all the pages
                        pdfDocument.Pages.Accept(textFragmentAbsorber);

                        SqlCommand com1 = new SqlCommand("spGetMaster", con);
                        con.Open();
                        com1.CommandType = CommandType.StoredProcedure;
                        com1.Parameters.Add("@masked", SqlDbType.VarChar).Value = sub1;
                        com1.ExecuteNonQuery();
                        SqlDataReader reader = com1.ExecuteReader();
                        reader.Read();
                        if (reader.HasRows)
                        {
                            //update text and other properties
                            CustName = reader["de_masked"].ToString();
                            reader.Close();
                            //get the extracted text fragments
                            TextFragmentCollection textFragmentCollection = textFragmentAbsorber.TextFragments;
                            //loop through the fragments             
                            foreach (TextFragment textFragment in textFragmentCollection)
                            {
                                //string str = "select de_masked from master where Masked='" + sub.ToString() + "'";
                                SqlCommand com = new SqlCommand("spGetMaster", con);
                                com.CommandType = CommandType.StoredProcedure;
                                com.Parameters.Add("@masked", SqlDbType.VarChar).Value = sub1;
                                com.ExecuteNonQuery();
                                SqlDataReader reader1 = com.ExecuteReader();
                                reader1.Read();
                                //update text and other properties
                                string CustName1 = reader1["de_masked"].ToString();
                                textFragment.Text = CustName1;
                                reader1.Close();
                            }
                            //setting password to pdf
                            SqlCommand cmd1 = new SqlCommand("spGetMaster", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add("masked", SqlDbType.VarChar).Value = sub1;
                            cmd1.ExecuteNonQuery();
                            SqlDataReader rd = cmd1.ExecuteReader();
                            rd.Read();
                            string pass = rd["pdfpassword"].ToString();
                            custEmail = rd["Email"].ToString();
                            ccemail = rd["ccemail"].ToString();
                            pdfDocument.Encrypt(pass, "", 0, CryptoAlgorithm.RC4x128);
                            rd.Close();
                            newfilename = i + ".pdf";
                            pdfDocument.Save(destination + @"\" + newfilename);
                            sendEmail();
                            insertdata();
                            //setpass();
                            string oldfile = mydir + i;
                            File.Delete(oldfile);
                        }
                        con.Close();
                    }
                }
            }

        }
        public static void sendEmail()
        {
            getemailbody();
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
            MailMessage msg = new MailMessage(userName,custEmail);
            try
            {
                msg.CC.Add(ccemail);
            }
            catch(Exception ex)
            {

            }
            msg.Subject = emailsubject;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Emailbody);
            //sb.AppendLine("Drop Downlist Name:" + ddllinksource.SelectedValue.ToString());
            msg.Body = sb.ToString();
            System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment((destination + @"\" + newfilename));
            msg.Attachments.Add(attach);
            SmtpClient SmtpClient = new SmtpClient();
            SmtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
            SmtpClient.Host = host;
            SmtpClient.Port = port;
            SmtpClient.EnableSsl = true;
            SmtpClient.Send(msg);
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.Delay;
        
        }
        public static void insertdata()
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
        public static void getemailbody()
        {
            try
            {
                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand("spgetemailbody", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                emailsubject = dr["subject"].ToString();
                Emailbody = dr["emailbody"].ToString();
                dr.Close();
                con.Close();
            }
            catch
            {

            }
        }

    }
}



