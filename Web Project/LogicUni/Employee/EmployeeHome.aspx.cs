using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
public partial class EmployeeHome : System.Web.UI.Page
{
    ADTeam4EF.EmpNotify eny = new ADTeam4EF.EmpNotify();
    int empid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["role"] != null)
            {
                if (!Session["role"].Equals("Employee"))
                {
                    Response.Redirect("~/Login.aspx", true);
                }
                else
                {
                    empid = Convert.ToInt32(Session["EmployeeID"]);
                    List<Request> lr = eny.Notify(empid);
                    int i = lr.Count();
                    if (i > 0)
                    {
                        notification.InnerHtml = @"You have <a href=""../Employee/ViewRequest.aspx"">" + i.ToString() + "</a> notifications"; 
                    }
                    else
                    {
                        notification.InnerHtml = "You have no new notifications";
                    }
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx", true);
            }
        }
    }
    protected override void OnInit(EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.MinValue);

        base.OnInit(e);
    }
    public void GridNotify()
    {
        //List<Request> lr = eny.Notify(empid);
        //int i = lr.Count();
        //if (i > 0)
        //{
        //    if (i == 1)
        //        Label1.Text = "You have 1 new notification";
        //    else
        //        Label1.Text = "You have " + i + " new notifications";
        //    GridView1.DataSource = lr;
        //    GridView1.DataBind();
        //}
        //else
        //{
        //    Label1.Text = "You have no notification";
        //    GridView1.DataSource = null;
        //    GridView1.DataBind();
        //}
    }
}