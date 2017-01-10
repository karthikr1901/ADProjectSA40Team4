using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
public partial class AppointRepresentative : System.Web.UI.Page
{
    ADTeam4EF.AppointRepresentativeController appointRepController = new ADTeam4EF.AppointRepresentativeController();
    string newEmployee = string.Empty;
    string departmentid = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        loadAllData();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        newEmployee = ddlEmployeeName.SelectedValue.ToString();
        //Response.Write("<script language=javascript>alert('" + newEmployee + "');</script>");
        if (appointRepController.appointNewRepresentative(newEmployee, departmentid))
        {
            lblResult.InnerText = "New Representative has been assigned!";
            lblResult.Visible = true;
            loadAllData();
        }
        else
        {
            lblResult.InnerText = "Assigning failed!";
            lblResult.Visible = true;
        }
    }
    public void loadAllData()
    {
        if (Session["DepartmentID"] != null)
        {
                departmentid = Session["DepartmentID"].ToString();
                //Response.Write("<script language=javascript>alert('"+Session["DepartmentID"]+"');</script>");
                if (appointRepController.getRepresentative(departmentid) != null)
                {
                    ADTeam4EF.Employee employee = appointRepController.getRepresentative(departmentid);
                    string employeeName = employee.EmployeeName.ToString();
                    currentRep.InnerText = "Current Representative: " + employeeName;
                    List<ADTeam4EF.Employee> eList = appointRepController.getEmployeeList(departmentid);
                    ddlEmployeeName.DataSource = eList;
                    ddlEmployeeName.DataTextField = "EmployeeName";
                    ddlEmployeeName.DataBind();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx", true);
            }
    }
}