using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADTeam4EF;
public partial class ApproveRejectRequest : System.Web.UI.Page
{
    ADTeam4EF.ApproveRejectRequestController approveRejectRequestController = new ADTeam4EF.ApproveRejectRequestController();
    string departmentid = string.Empty;
    int approvedByEmp = 0;
    string status = string.Empty;
    string remark = string.Empty;
    int requestID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblResult.Visible = false;
            populateDDL();
        }
    }
    protected void ddlEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        remark = txtReason.Text;
        if (Session["EmployeeID"] != null)
        {
            approvedByEmp = Convert.ToInt32(Session["EmployeeID"].ToString());
            status = "Approved";
            requestID = Convert.ToInt32(Session["RequestID"].ToString());
            string result = approveRejectRequestController.updateRequestStatusToApprove(requestID, status, approvedByEmp, remark);
            if (result == "SUCCESS")
            {
                //lblResult.InnerText = "Approve Successful!";
                lblResult.Visible = true;
                txtReason.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;
                populateDDL();
            }
            else
            {
                lblResult.InnerText = "Approve Failed!";
                lblResult.Visible = true;
            }
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        remark = txtReason.Text;
        if (Session["EmployeeID"] != null)
        {
            approvedByEmp = Convert.ToInt32(Session["EmployeeID"].ToString());
            status = "Rejected";
            requestID = Convert.ToInt32(Session["RequestID"].ToString());
            string result = approveRejectRequestController.updateRequestStatusToApprove(requestID, status, approvedByEmp, remark);
            if (result == "SUCCESS")
            {
               
                //lblResult.InnerText = "Reject Successful!";
                lblResult.Visible = true;
                txtReason.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;
                populateDDL();
            }
            else
            {
                lblResult.InnerText = "Reject Failed!";
                lblResult.Visible = true;

            }
        }
    }
    public void populateDDL()
    {
        if (Session["DepartmentID"] != null)
        {
            departmentid = Session["DepartmentID"].ToString();
            if (approveRejectRequestController.getRequestID(departmentid) != null)
            {
                ddlRequest.DataSource = approveRejectRequestController.getRequestID(departmentid);
                ddlRequest.DataTextField = "RequestID";
                ddlRequest.DataBind();
                ddlRequest.Items.Insert(0, "--Choose Request--");
                ddlRequest.Visible = true;
                lblRequestEmp.Visible = true;
                txtReason.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;
                noRequest.Visible = false;
                RequestForm.Visible = true;
            }
            else
            {
                //lblEmployeeStatus.InnerHtml = "There is no pending requests!";
                noRequest.Visible = true;
                ddlRequest.Visible = true;
                lblRequestEmp.Visible = true;
                txtReason.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;
                PreviewGridView.Visible = false;
                RequestForm.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", true);
        }
    }
    protected void ddlRequest_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRequest.SelectedIndex == 0)
        {
            PreviewGridView.Visible = false;
            txtReason.Visible = false;
            btnApprove.Visible = false;
            btnReject.Visible = false;
        }
        else if (ddlRequest.SelectedIndex > 0)
        {
            int requestID = Convert.ToInt32(ddlRequest.SelectedValue.ToString());
            requestID =Convert.ToInt32(ddlRequest.SelectedItem.Value);
            Session["RequestID"] = requestID;
                    if (approveRejectRequestController.getRequestItem(requestID) != null)
                    {
                        List<ApproveRejectRequestObject> requestDetailList = approveRejectRequestController.getRequestItem(requestID);
                        if (requestDetailList.Count() > 0)
                        {
                            PreviewGridView.DataSource = requestDetailList;
                            PreviewGridView.DataBind();
                            PreviewGridView.Visible = true;
                            txtReason.Visible = true;
                            btnApprove.Visible = true;
                            btnReject.Visible = true;
                        }
                    }
                }
    }
}