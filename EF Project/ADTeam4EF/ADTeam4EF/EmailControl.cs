using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ADTeam4EF
{
    public class EmailControl
    {
        ADProjectSA40Team4Entities ad = new ADProjectSA40Team4Entities();

        public bool sendEmail()
        {
            NetworkCredential loginInfo = new NetworkCredential("smsmarartt@gmail.com", "ttraramsms1");
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("smsmarartt@gmail.com");

            msg.To.Add(new MailAddress("dev.minko@gmail.com"));

            msg.Subject = "hi";

            msg.Body = "My name is Karthik";

            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.Timeout = 2;

            client.EnableSsl = true;

            client.UseDefaultCredentials = false;

            client.Credentials = loginInfo;

            client.Send(msg);

            return true;
        }
    }
}
