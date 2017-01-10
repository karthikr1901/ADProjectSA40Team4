using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
using System.Net;
using System.Net.Mail;
public partial class test : System.Web.UI.Page
{
    ADTeam4EF.EmailControl rr = new ADTeam4EF.EmailControl();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        rr.sendEmail();
    }
}
