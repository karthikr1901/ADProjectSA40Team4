using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StoreSupervisor : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["role"] != null)
        {
            if (!Session["role"].Equals("Store Supervisor"))
            {
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                //empName.InnerText = Session["UserName"].ToString();
                empName.InnerHtml = "<span class='glyphicon glyphicon-log-out'></span>&nbsp;" + Session["UserName"].ToString() + "&nbsp;";
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", true);
        }

    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["LogicUniversityUserInfo"] != null)
        {
            HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["LogicUniversityUserInfo"];
            HttpContext.Current.Response.Cookies.Remove("LogicUniversityUserInfo");
            currentUserCookie.Expires = DateTime.Now.AddDays(-10);
            currentUserCookie.Value = null;
            HttpContext.Current.Response.SetCookie(currentUserCookie);
        }
        Session.Clear();
        Response.Redirect("~/Default.aspx", false);
    }
    protected override void OnInit(EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.MinValue);

        base.OnInit(e);
    }
    
}
