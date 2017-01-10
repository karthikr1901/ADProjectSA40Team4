using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using ADTeam4EF;

public partial class Login : System.Web.UI.Page
{
    ADTeam4EF.LoginController loginCtr = new ADTeam4EF.LoginController();

    protected string email = string.Empty;
    protected string hashedPassword = string.Empty;
    protected string role = "default";
    protected string userName = string.Empty;
    protected int employeeid = 0;
    protected string departmentid = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Cookies.Clear();
        loginCtr.updateExpiredDelegatedEmployee();
    }

    protected void btnSubmitLogin_Click(object sender, EventArgs e)
    {
        email = txtEmail.Value.ToString();
        hashedPassword = GetSHA1HashData(txtPassword.Value.ToString());
        loginUser(email, hashedPassword);
    }
    private void loginUser(string email, string password)
    {
        ADTeam4EF.Employee user = loginCtr.authenticateUser(email, password);
        if (user != null)
        {
            if ((int)user.RoleID == 3)
            {
                employeeid = user.EmployeeID;
                DateTime today = System.DateTime.Today;
                if (loginCtr.checkDelegate(employeeid, today) != null)
                {
                    role = user.Role.RoleName.ToString();
                }
                else
                {
                    role = "Employee";
                }
            }
            else
            {
                role = user.Role.RoleName.ToString();
            }
            userName = user.EmployeeName.ToString();
            departmentid = user.DepartmentID.ToString();
            employeeid = Convert.ToInt32(user.EmployeeID.ToString());
            storeSession(userName, role, employeeid, departmentid);
            if (chkRememberMe.Checked == true)
            {
                Response.Cookies["LogicUniversityUserInfo"]["email"] = email;
                Response.Cookies["LogicUniversityUserInfo"]["identity"] = hashedPassword;
            }
            redirectToPage(Session["role"].ToString());
        }
        else
        {
            lblLoginStatus.Text = "Login Failed!";
            btnSubmitLogin.CssClass = "btn btn-danger btn-block";
            lblLoginStatus.Visible = true;
            Session.Clear();
            Response.Cookies.Clear();
        }
    }

    public string GetSHA1HashData(string data)
    {
        //create new instance of md5
        SHA1 sha1 = SHA1.Create();

        //convert the input text to array of bytes
        byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

        //create new instance of StringBuilder to save hashed data
        StringBuilder returnValue = new StringBuilder();

        //loop for each byte and add it to StringBuilder
        for (int i = 0; i < hashData.Length; i++)
        {
            returnValue.Append(hashData[i].ToString());
        }
        // return hexadecimal string
        return returnValue.ToString();
    }

    protected void storeSession(string userName, string role, int employeeid, string departmentid)
    {
        Session["UserName"] = userName;
        Session["Role"] = role;
        Session["EmployeeID"] = employeeid;
        Session["DepartmentID"] = departmentid;
    }
    protected void redirectToPage(string role)
    {
        if (role.Equals("Department Head"))
        {
            Response.Redirect("~/DepartmentHead/DepartmentHeadHome.aspx", false);
        }
        else if (role.Equals("Department Representative"))
        {
            Response.Redirect("~/DepartmentRepresentative/DepartmentRepresentative.aspx", false);
        }
        else if (role.Equals("Delegated Employee"))
        {
            Response.Redirect("~/DelegatedEmployee/DelicatedEmployeeHome.aspx", false);
        }
        else if (role.Equals("Employee"))
        {
            Response.Redirect("~/Employee/EmployeeHome.aspx", false);
        }
        else if (role.Equals("Store Clerk"))
        {
            Response.Redirect("~/StoreClerk/StoreClerkHome.aspx", false);
        }
        else if (role.Equals("Store Manager"))
        {
            Response.Redirect("~/StoreManager/StoreManager.aspx", false);
        }
        else if (role.Equals("Store Supervisor"))
        {
            Response.Redirect("~/StoreSupervisor/StoreSupervisorHome.aspx",false);
        }
    }
}