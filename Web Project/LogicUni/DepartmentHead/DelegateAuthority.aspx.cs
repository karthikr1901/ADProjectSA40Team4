    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ADTeam4EF;
    public partial class DelegateAuthority : System.Web.UI.Page
    {
        ADTeam4EF.DelegateAuthorityController delegateController = new ADTeam4EF.DelegateAuthorityController();
        string deparmentid = string.Empty;
        string processResult = string.Empty;
        DateTime fromDate = new DateTime();
        DateTime toDate = new DateTime();
        string employeename = string.Empty;
        string currentEmp = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            loadData();
            lblStatus.Visible = false;
            lblCancelStatus.Visible = false;
            if (!IsPostBack)
            {
                populateDDL();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                compareDate();
                if (processResult == "ok")
                {
                    insertDelegate();
                }

            }
            //ADTeam4EF.Employee emp = delegateController.cancelAuthority(delEmpId);
        }
        public void loadData()
        {

            if (Session["DepartmentID"] != null)
            {

                if (delegateController.populateAuthority(deparmentid) != null)
                {
                    ADTeam4EF.Employee employee = delegateController.populateAuthority(deparmentid);
                    lblCancelEmp.InnerHtml = "Employee Name: " + employee.EmployeeName.ToString();
                    btnDSubmit.Visible = false;
                    assignbox.Visible = false;
                    cancelbox.Visible = true;
                    btnCancel.Visible = true;
                    currentEmp = employee.EmployeeName.ToString();
                }
                else
                {
                    lblCancelEmp.InnerHtml = "<span class='glyphicon glyphicon-warning-sign'></span>&nbsp;" + "There is no delegated employee at this moment!";
                    btnDSubmit.Visible = true;
                    assignbox.Visible = true;
                    btnCancel.Visible = false;
                    //cancelbox.Visible = false;
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx", true);
            }

        }
        public void compareDate()
        {
            DateTime today = System.DateTime.Today;

            fromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);
            toDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
            int fromResult = fromDate.CompareTo(today);
            if (fromResult < 0)
            {
                btnDSubmit.CssClass = "btn btn-danger btn-sm";
                lblStatus.InnerHtml = "<span class='glyphicon glyphicon-warning-sign'></span>&nbsp;" + "Please enter valid data!";
                lblStatus.Visible = true;
                processResult = "error";
                txtFromDate.Text = "";
                txtToDate.Text = "";
            }
            else
            {
                processResult = "ok";
            }
            int toResult = toDate.CompareTo(fromDate);
            if (toResult < 0)
            {
                btnDSubmit.CssClass = "btn btn-danger btn-sm";
                lblStatus.InnerHtml = "<span class='glyphicon glyphicon-warning-sign'></span>&nbsp;" + "Please enter valid data!";
                lblStatus.Visible = true;
                processResult = "error";
                txtFromDate.Text = "";
                txtToDate.Text = "";
            }
            else
            {
                processResult = "ok";
            }
            int betweenResult = toDate.CompareTo(fromDate);
            if (betweenResult <= 0)
            {
                btnDSubmit.CssClass = "btn btn-danger btn-sm";
                lblStatus.InnerHtml = "<span class='glyphicon glyphicon-warning-sign'></span>&nbsp;" + "Please enter valid data!";
                lblStatus.Visible = true;
                processResult = "error";
                txtFromDate.Text = "";
                txtToDate.Text = "";
            }
            else
            {
                processResult = "ok";
            }
        }
        public void insertDelegate()
        {
            employeename = ddlEmployeeName.SelectedValue.ToString();
            //Response.Write("<script>alert('" + processResult + "')</script>");
            if (delegateController.delegateAuthority(employeename, fromDate, toDate))
            {
                string departmentid = Session["DepartmentID"].ToString();
                lblStatus.InnerHtml = "<span class='glyphicon glyphicon-ok-circle'></span>&nbsp;" + "Delegation has been completed!";
                loadData();
                delegateController.sendmail(departmentid,"delegate");
                
            }
            else
            {

                lblStatus.InnerHtml = "<span class='glyphicon glyphicon-warning-sign'></span>&nbsp;" + "Delegation failed!";
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string departmentid = Session["DepartmentID"].ToString();
            if (delegateController.cancelAuthority())
            {
                delegateController.sendmail(departmentid, "cancel");
                lblCancelStatus.InnerHtml = "<br/><span class='glyphicon glyphicon-ok-circle'></span>&nbsp;" + "Cancellation completed!";
                lblCancelStatus.Visible = true;
                btnDSubmit.Visible = true;
                assignbox.Visible = true;
                cancelbox.Visible = true;
                loadData();
                populateDDL();
                txtFromDate.Text = "";
                txtToDate.Text = "";
               
            }
            else
            {
                lblStatus.InnerHtml = "<br/><span class='glyphicon glyphicon-warning-sign'></span>&nbsp;" + "Cancellation failed!";
                lblCancelStatus.Visible = true;
                btnDSubmit.Visible = false;
                assignbox.Visible = false;
                cancelbox.Visible = true;
            }
        }
        public void populateDDL()
        {
            deparmentid = Session["DepartmentID"].ToString();
            List<ADTeam4EF.Employee> employeeList = delegateController.populateEmployeeList(deparmentid);
            ddlEmployeeName.DataSource = employeeList;
            ddlEmployeeName.DataTextField = "EmployeeName";
            ddlEmployeeName.DataBind();
        }
    }
