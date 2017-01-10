using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
public partial class StoreClerkHome : System.Web.UI.Page
{
    ADTeam4EF.StoreClerkHomeController storeClerkHomeController = new ADTeam4EF.StoreClerkHomeController();
    ADTeam4EF.EmpNotify eny = new ADTeam4EF.EmpNotify();
    int empid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["role"] != null)
            {
                if (!Session["role"].Equals("Store Clerk"))
                {
                    Response.Redirect("~/Login.aspx", true);
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx", true);
            }
            loadData();
        }
    }
    protected override void OnInit(EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.MinValue);

        base.OnInit(e);
    }
    public void loadData()
    {
        List<DisplayLowLevelStock_View> lowStockList = storeClerkHomeController.displayLowLevelStock();
        if (lowStockList != null)
        {
            int noti = lowStockList.Count();
            if (noti > 0)
            {
                notification.InnerHtml = @"You have <a href=""RaisePurchaseOrder.aspx"">" + noti.ToString() + "</a> low-stock notifications";
            }
        }
        else
        {
            notification.InnerHtml = "You have no new low-stock notifications";
        }
    }
}