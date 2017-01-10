using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
public partial class InventoryStatusReport : System.Web.UI.Page
{
    ADTeam4EF.InventoryStatusController isc = new ADTeam4EF.InventoryStatusController();

    protected void Page_Load(object sender, EventArgs e)
    {
        loadData();
    }

    protected void GridInventoryReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridInventoryReport.PageIndex = e.NewPageIndex;
        loadData();
    }
    public void loadData()
    {
        List<DisplayLowLevelStock_View> lowStock = isc.displayLowLevelStock();
        if (lowStock.Count() > 0)
        {
            GridInventoryReport.DataSource = isc.displayLowLevelStock();
            GridInventoryReport.DataBind();
        }
    }
}