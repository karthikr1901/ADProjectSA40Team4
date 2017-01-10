using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
using System.Data;

public partial class ProcessOutstandingRequest : System.Web.UI.Page
{
    ProcessOustandingReportController processOutstandingReportController;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["EmployeeID"] != null)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
        
    }
    void BindGrid()
    {
        processOutstandingReportController = new ProcessOustandingReportController();
        DataTable dt = new DataTable();
        dt = processOutstandingReportController.getOutstandingDataForGrid();
        ViewState["CurrentData"] = dt;
        if (dt.Rows.Count >0) 
        {
            gdvOutstanding.DataSource = (DataTable)ViewState["CurrentData"];
            gdvOutstanding.DataBind();
            btnAllocate.Visible = true;
            noOutstanding.Visible = false;
        }
        else
        {
            noOutstanding.InnerText = "There is no outstanding item";
            noOutstanding.Visible = true;
            //lblMessage.Text = "No Outstanding Item";
            btnAllocate.Visible = false;
        }

    }


    protected void gdvOutstanding_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        processOutstandingReportController = new ProcessOustandingReportController();
        DataTable dt = (DataTable)ViewState["CurrentData"];

        int r = Convert.ToInt16(e.RowIndex);
        int reqID = Convert.ToInt16(dt.Rows[r][0].ToString());
        string ItemID = dt.Rows[r][1].ToString();
        dt.Rows[r].Delete();
        int data = reqID;

        processOutstandingReportController.DeleteOutstanding(reqID, ItemID);
        int count = processOutstandingReportController.getReqDetailCount(data);
        if (count < 1)
        {
            processOutstandingReportController.deleteRequest(reqID);
        }
        gdvOutstanding.DataSource = dt;
        gdvOutstanding.DataBind();
        ViewState["CurrentData"] = dt;
        if (dt.Rows.Count == 0)
        {
            noOutstanding.InnerText = "There is no outstanding item";
            btnAllocate.Visible = false;
            lblMessage.Visible = false;
            noOutstanding.Visible = true;
        }

    }

    void ClearGridView()
    {
        gdvOutstanding.DataSource = null;
        gdvOutstanding.DataBind();
    }
    protected void btnAllot_Click(object sender, EventArgs e)
    {
        processOutstandingReportController = new ProcessOustandingReportController();
        
        int empID = Convert.ToInt16(Session["EmployeeID"]);
        bool result = processOutstandingReportController.OutstandingAllocate(empID);
        if (result == true)
        {
            
            ClearGridView();
            noOutstanding.InnerText = "Successfully allocated. No new outstanding";
            //lblMessage.Text = "Successful";
            btnAllocate.Visible = false;
        }
        else
        {
            lblMessage.Text="Stock is not enough";
            
        }
        BindGrid();

    }

}