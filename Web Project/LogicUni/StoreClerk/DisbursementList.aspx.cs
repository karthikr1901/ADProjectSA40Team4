using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;

public partial class DisbursementList : System.Web.UI.Page
{
    ADTeam4EF.ViewDisbursementController vdc = new ViewDisbursementController();
    ADTeam4EF.Disbursement dclist = new Disbursement();
    int empid;
    protected void Page_Load(object sender, EventArgs e)
    {
        empid = Convert.ToInt32(Session["EmployeeID"]);
        if (!IsPostBack)
            ddlDepartmentLoad();
    }

    protected void ddlDepartmentLoad()
    {
        ddlDepartment.DataSource = vdc.GetAllDepartment(empid);
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataValueField = "DepartmentID";
        ddlDepartment.DataBind();
    }
    protected void btnSearch_Click1(object sender, EventArgs e)
    {
        string cptid = ddlDepartment.SelectedValue;
        List<Disbursementclass> hhList = new List<Disbursementclass>();
        List<String> lobj = new List<String>();
        lobj = dclist.DisburseDepartLabel(cptid);
        
        if ((lobj != null))// && (!lobj.Any()))
        {
            lblCollectpoint.InnerText= "Collection Point: "+lobj[2]; 
            lblRep.InnerText = "Representative Name: "+lobj[1];
            hhList = dclist.DisburseDepartGrid(cptid);
            GridView1.DataSource = hhList;
            GridView1.DataBind();        
        }
        else//if (lobj.Count <= 0)// is.Equals(null))
        {
            lblCollectpoint.Visible = false;
            lblRep.Visible = false;
            lblCollectpoint.InnerText = ""; lblRep.InnerText = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }
}