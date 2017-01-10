using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
            Session.Clear();
            if (Request.Cookies["LogicUniversityUserInfo"] != null)
            {
                HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["LogicUniversityUserInfo"];
                HttpContext.Current.Response.Cookies.Remove("LogicUniversityUserInfo");
                currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                currentUserCookie.Value = null;
                HttpContext.Current.Response.SetCookie(currentUserCookie);
            }
            //Response.Redirect("~/Default.aspx", false);
    }
}
