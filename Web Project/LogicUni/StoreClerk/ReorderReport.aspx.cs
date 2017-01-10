using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class StoreSupervisor_ReorderReport : System.Web.UI.Page
{
    ADTeam4EF.ReorderReportController rController = new ADTeam4EF.ReorderReportController();
    ReportDocument reportDoc = new ReportDocument();
    DataSet ds = new ADTeam4DS();
    DataTable dt = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblStatus.InnerText = "There is no request with this preferences!";
        lblStatus.Visible = false;
        RequestReportViewer.Visible = false;
        if (!IsPostBack)
        {
            //dt = new DataTable();
            rdQty.Checked = true;
            populateCategory();
        }
    }
    public void populateCategory()
    {
        List<Category> categoryList = rController.getAllCategory();
        ddlCategory.DataSource = categoryList;
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataBind();
        ddlCategory.SelectedIndex = 0;
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DateTime firstDate = Convert.ToDateTime(txtFirst.Text);
            DateTime ff = new DateTime(firstDate.Year, firstDate.Month, 1);
            DateTime fl = ff.AddMonths(1).AddDays(-1);


            DateTime secondDate = Convert.ToDateTime(txtSecond.Text);
            DateTime sf = new DateTime(secondDate.Year, secondDate.Month, 1);
            DateTime sl = sf.AddMonths(1).AddDays(-1);


            DateTime thirdDate = Convert.ToDateTime(txtSecond.Text);
            DateTime tf = new DateTime(thirdDate.Year, secondDate.Month, 1);
            DateTime tl = tf.AddMonths(1).AddDays(-1);


            string type = string.Empty;
            string category = string.Empty;
            if (rdPrice.Checked)
            {
                type = "Price";
            }
            else
            {
                type = "Quantity";
            }
                category = ddlCategory.SelectedItem.ToString();
                addColumn();
                List<ADTeam4EF.ReorderReport_View> firstRequestList = rController.getFirstMonth(category, ff, fl);
                List<ADTeam4EF.ReorderReport_View> secondRequestList = rController.getSecondMonth(category, sf, sl);
                List<ADTeam4EF.ReorderReport_View> thirdRequestList = rController.getThirdMonth(category, tf, tl);
                if (firstRequestList != null){
                    if (type == "Quantity")
                    {
                        dt = ConvertListToDataTable(firstRequestList, "FirstMonth");
                    }
                    else
                    {
                        dt = ConvertListToDataTablePrice(firstRequestList, "FirstMonth");
                    }
                
                }
                if (secondRequestList != null)
                {
                    if (type == "Quantity")
                    {
                        dt = ConvertListToDataTable(secondRequestList, "SecondMonth");
                    }
                    else
                    {
                        dt = ConvertListToDataTablePrice(secondRequestList, "SecondMonth");
                    }
                }
                if (thirdRequestList != null)
                {
                    if (type == "Quantity")
                    {
                        dt = ConvertListToDataTable(thirdRequestList, "ThirdMonth");
                    }
                    else
                    {
                        dt = ConvertListToDataTablePrice(thirdRequestList, "ThirdMonth");
                    }
                }
                 else if(firstRequestList==null && secondRequestList==null && thirdRequestList ==null)
                {
                    lblStatus.Visible = true;
                    RequestReportViewer.Visible = false;
                }
                    ds.Tables[0].Merge(dt);
                    //reportDoc.Load(Server.MapPath("~/Report/ReorderCrystalReport.rpt"));
                    //reportDoc.SetDataSource(ds);
                    //RequestReportViewer.ReportSource = reportDoc;
                    //RequestReportViewer.Visible = true;
                    lblStatus.Visible = false;
                }
        RequestGridView.DataSource = dt;
        RequestGridView.DataBind();
    }
    public DataTable ConvertListToDataTable(List<ADTeam4EF.ReorderReport_View> reqList, string month)
    {
        try
        {
            foreach (var req in reqList)
            {
                DataRow newRow = dt.NewRow();
                newRow["ItemID"] = req.ItemID;
                newRow["CategoryName"] = req.CategoryName;
                newRow["Description"] = req.Description;
                newRow[month] = req.TotalQty;
                //newRow["SecondMonth"] = req.SecondMonth;
                //newRow["ThirdMonth"] = req.ThirdMonth;
                dt.Rows.Add(newRow);
            }
            return dt;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
    public DataTable ConvertListToDataTablePrice(List<ADTeam4EF.ReorderReport_View> reqList, string month)
    {
        try
        {
            foreach (var req in reqList)
            {
                DataRow newRow = dt.NewRow();
                newRow["CategoryName"] = req.CategoryName;
                newRow[month] = req.Price;
                //newRow["SecondMonth"] = req.SecondMonth;
                //newRow["ThirdMonth"] = req.ThirdMonth;
                dt.Rows.Add(newRow);
            }
            return dt;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
    public void clearAll()
    {
        ddlCategory.SelectedIndex = 0;
        rdQty.Checked = true;
        txtFirst.Text = "";
        txtSecond.Text = "";
        txtThird.Text = "";
        dt.Clear();
    }
    public void addColumn()
    {
        dt.Columns.Add("ItemID");
        dt.Columns.Add("CategoryName");
        dt.Columns.Add("Description");
        dt.Columns.Add("FirstMonth", typeof(int));
        dt.Columns.Add("SecondMonth", typeof(int));
        dt.Columns.Add("ThirdMonth", typeof(int));
    }
}