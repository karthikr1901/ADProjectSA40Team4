using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
public partial class ViewRequestHistory : System.Web.UI.Page
{
    ADTeam4EF.ViewRequestHistoryController viewController = new ADTeam4EF.ViewRequestHistoryController();
    string deparmentid = string.Empty;
    DateTime fromDate = new DateTime();
    DateTime toDate = new DateTime();
    string processResult = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        btnPreview.Visible = false;
        if (!IsPostBack)
        {
            loadData();
        }

    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        int requestno = Convert.ToInt32(ddlRequestNo.SelectedValue);
        if (viewController.getRequestHistory(requestno) != null)
        {
            List<ADTeam4EF.RequestHistoryObj> requestHistoryList = viewController.getRequestHistory(requestno);
            PreviewGridView.DataSource = requestHistoryList;
            PreviewGridView.DataBind();
            lblRequestNo.InnerText = "Request No. " + requestno.ToString();
            ADTeam4EF.Request rStatus = viewController.getRequestStatus(requestno);
            string status = rStatus.RequestStatus.ToString();
            lbRequestStatus.InnerText = "Request Status: " + status;
        }

    }

    public void loadData()
    {
        if (Session["DepartmentID"] != null)
        {
            //deparmentid = Session["DepartmentID"].ToString();
            string departmentid = Session["DepartmentID"].ToString();
            if(viewController.getEmp(departmentid) !=null)
            {
                List<DISTINCTEMPLOYEE_VIEW> employeeList = viewController.getEmp(departmentid).ToList();
                ddlEmployeeName.DataSource = employeeList;
                ddlEmployeeName.DataTextField = "EmployeeName";
                ddlEmployeeName.DataBind();
                btnPreview.Visible = false;
                noRequest.Visible = false;
            }
            else
            {
                noRequest.Visible = true;
                requestbox.Visible = false;
            }
            
        }
        else
        {
            Response.Redirect("~/Login.aspx", true);
        }
    }


    public void getRequest()
    {
        string employeeName = ddlEmployeeName.SelectedValue;
        ADTeam4EF.ViewRequestHistoryController viewRequestHistoryController = new ADTeam4EF.ViewRequestHistoryController();
        if (viewRequestHistoryController.populateRequestNo(employeeName, fromDate, toDate) != null)
        {
            List<ADTeam4EF.Request> requestNoList = viewRequestHistoryController.populateRequestNo(employeeName, fromDate, toDate);
            ddlRequestNo.DataSource = requestNoList;
            ddlRequestNo.DataTextField = "RequestID";
            ddlRequestNo.DataBind();
            btnPreview.Visible = true;
            status.Visible = false;
        }
        else
        {
            status.InnerText = "There is no request with this preferences!";
            btnPreview.Visible = false;
        }
    }


    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            compareDate();
            if (processResult == "ok")
            {
                getRequest();
            }
        }
    }

    public void compareDate()
    {
        DateTime today = System.DateTime.Today;

        fromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);
        toDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
        int toResult = toDate.CompareTo(fromDate);
        int betweenResult = toDate.CompareTo(fromDate);
        if (betweenResult <= 0)
        {
            //btnDSubmit.CssClass = "btn btn-danger btn-sm";
            lblStatus.InnerHtml = "<span class='glyphicon glyphicon-warning-sign'></span>&nbsp;" + "Please enter valid data!";
            lblStatus.Visible = true;
            processResult = "error";
            txtFromDate.Text = "";
            txtToDate.Text = "";
        }
        else
        {
            processResult = "ok";
            lblStatus.Visible = false;
        }
    }
    protected void ddlEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnPreview.Visible = false;
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlRequestNo.Items.Clear();
    }
}