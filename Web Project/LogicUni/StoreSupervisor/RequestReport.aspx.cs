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
public partial class Store_Supervisor_GenerateOrderReport : System.Web.UI.Page
{
    ADTeam4EF.RequestReportController rController = new ADTeam4EF.RequestReportController();
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
            populateDepartment();
            populateCategory();
        }
    }


    public void populateDepartment()
    {
        List<Department> departmentList = rController.getAllDepartment();
        ddlDepartment.DataSource = departmentList;
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, "All");
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
            string department = string.Empty;
            string category = string.Empty;
            if (rdPrice.Checked)
            {
                type = "Price";
            }
            else
            {
                type = "Quantity";
            }
            if (ddlDepartment.SelectedItem.ToString() == "All")
            {
                category = ddlCategory.SelectedItem.ToString();
                addColumn();
                List<ADTeam4EF.RequestReport_View> firstRequestList = rController.getFirstMonth(category, ff, fl);
                List<ADTeam4EF.RequestReport_View> secondRequestList = rController.getSecondMonth(category, sf, sl);
                List<ADTeam4EF.RequestReport_View> thirdRequestList = rController.getThirdMonth(category, tf, tl);
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
                    reportDoc.Load(Server.MapPath("~/Report/RequestQtyReportForAllDepartmentOneCategory.rpt"));
                    reportDoc.SetDataSource(ds);
                    RequestReportViewer.ReportSource = reportDoc;
                    RequestReportViewer.Visible = true;
                    lblStatus.Visible = false;
                }
            else
            {
                department = ddlDepartment.SelectedItem.ToString();
                category = ddlCategory.SelectedItem.ToString();
                if (category == "All")
                {
                    addColumn();
                    List<ADTeam4EF.RequestReport_View> firstRequestList = rController.getFirstMonthOneDepartmentAllCategory(department, ff, fl);
                    List<ADTeam4EF.RequestReport_View> secondRequestList = rController.getSecondMonthOneDepartmentAllCategory(department, sf, sl);
                    List<ADTeam4EF.RequestReport_View> thirdRequestList = rController.getThirdMonthOneDepartmentAllCategory(department, tf, tl);
                    if (firstRequestList != null)
                    {
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
                    else if (firstRequestList == null && secondRequestList == null && thirdRequestList == null)
                    {
                        lblStatus.Visible = true;
                        RequestReportViewer.Visible = false;
                    }
                    ds.Tables[0].Merge(dt);
                    reportDoc.Load(Server.MapPath("~/Report/RequestQtyReportForAllDepartmentOneCategory.rpt"));
                    reportDoc.SetDataSource(ds);
                    RequestReportViewer.ReportSource = reportDoc;
                    RequestReportViewer.Visible = true;
                    lblStatus.Visible = false;
                }
                else
                {
                    addColumn();
                    department = ddlDepartment.SelectedItem.ToString();
                    category = ddlCategory.SelectedItem.ToString();
                    List<ADTeam4EF.RequestReport_View> firstRequestList = rController.getFirstMonthOneDepartmentOneCategory(department, category, ff, fl);
                    List<ADTeam4EF.RequestReport_View> secondRequestList = rController.getSecondMonthOneDepartmentOneCategory(department, category, sf, sl);
                    List<ADTeam4EF.RequestReport_View> thirdRequestList = rController.getThirdMonthOneDepartmentOneCategory(department, category, tf, tl);
                    if (firstRequestList != null)
                    {
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
                    else if (firstRequestList == null && secondRequestList == null && thirdRequestList == null)
                    {
                        lblStatus.Visible = true;
                        RequestReportViewer.Visible = false;
                    }
                    ds.Tables[0].Merge(dt);
                    reportDoc.Load(Server.MapPath("~/Report/RequestQtyReportForAllDepartmentOneCategory.rpt"));
                    reportDoc.SetDataSource(ds);
                    RequestReportViewer.ReportSource = reportDoc;
                    RequestReportViewer.Visible = true;
                    lblStatus.Visible = false;
                }
            }
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.Items[0].Selected)
        {
            if (ddlCategory.Items[0].Value == "All")
            {
                ddlCategory.Items.Remove(ddlCategory.Items.FindByValue("All"));
            }
        }
        else
        {
            if (ddlCategory.Items[0].Value != "All")
            {
                ddlCategory.Items.Insert(0, "All");
                ddlCategory.SelectedIndex = 0;
            }   
        }
    }
    public  DataTable ConvertListToDataTable(List<ADTeam4EF.RequestReport_View> reqList,string month)
    {
        try
        {
            foreach (var req in reqList)
            {
                DataRow newRow = dt.NewRow();
                newRow["DepartmentName"] = req.DepartmentName;
                newRow["CategoryName"] = req.CategoryName;
                newRow[month] = req.Total_Quantity;  
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
    public DataTable ConvertListToDataTablePrice(List<ADTeam4EF.RequestReport_View> reqList, string month)
    {
        try
        {
            foreach (var req in reqList)
            {
                DataRow newRow = dt.NewRow();
                newRow["DepartmentName"] = req.DepartmentName;
                newRow["CategoryName"] = req.CategoryName;
                newRow[month] = req.Total_Price;
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
        ddlDepartment.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        rdQty.Checked = true;
        txtFirst.Text = "";
        txtSecond.Text = "";
        txtThird.Text = "";
        dt.Clear();
    }
    public void addColumn()
    {
        dt.Columns.Add("DepartmentName");
        dt.Columns.Add("CategoryName");
        dt.Columns.Add("FirstMonth", typeof(int));
        dt.Columns.Add("SecondMonth", typeof(int));
        dt.Columns.Add("ThirdMonth", typeof(int));
    }
}