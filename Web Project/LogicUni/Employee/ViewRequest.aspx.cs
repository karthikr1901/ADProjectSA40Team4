using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;

public partial class EmpHistory : System.Web.UI.Page
{
    ADTeam4EF.EditRequest edr = new EditRequest();
    int empid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmployeeID"] != null)
        {
            empid = Convert.ToInt32(Session["EmployeeID"]);
            if (!IsPostBack)
            {
                GridView1.Columns[4].Visible = false;
                DropDownLoad();
                Label3.Visible = Label5.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", true);
        }           
    }

    public void DropDownLoad()
    {
        if (edr.HistDropReqNo(empid) != null)
        {
            DropDownList1.DataSource = edr.HistDropReqNo(empid);
            DropDownList1.DataTextField = "RequestID";
            DropDownList1.DataValueField = "RequestID";
            DropDownList1.DataBind();
            RequestForm.Visible = true;
            noRequest.Visible = false;
        }
        else
        {
            RequestForm.Visible = false;
            noRequest.Visible = true;
        }
            
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue != "")
        {
            Label3.Visible = Label5.Visible = true;
            int requid = Convert.ToInt32(DropDownList1.SelectedValue);
            var labellist = edr.HistLabelGen(requid);
            Label3.InnerText = "Status: " + labellist[0].RequestStatus;
            Label5.InnerText = "Remark: " + labellist[0].Remark;
            if (Convert.ToString(labellist[0].Remark) == "" || Convert.ToString(labellist[0].Remark) == null)
            { Label5.Visible = false; }
            if (Convert.ToString(labellist[0].RequestStatus) == "Disbursed" || Convert.ToString(labellist[0].RequestStatus) == "Alloted")
                GridView1.Columns[4].Visible = true;
            else
                GridView1.Columns[4].Visible = false;
            GridView1.DataSource = edr.HistGridViewGen(requid);
            GridView1.DataBind();
        }
    }
}