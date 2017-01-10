using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using ADTeam4EF;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    protected string email = string.Empty;
    protected string hashedPassword = string.Empty;
    protected string role = "default";
    protected string userName = string.Empty;
    protected void Page_Init(object sender, EventArgs e)
    {

        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Same methods as in Login
        if (!IsPostBack)
        {
            Session.Clear();
            if (Request.Cookies["LogicUniversityUserInfo"] != null)
            {
                if (Request.Cookies["LogicUniversityUserInfo"]["email"] != null)
                {
                    if (Request.Cookies["LogicUniversityUserInfo"]["identity"] != null)
                    {
                        email = Request.Cookies["LogicUniversityUserInfo"]["email"];
                        hashedPassword = Request.Cookies["LogicUniversityUserInfo"]["identity"];
                        loginUserAutomatically(email, hashedPassword);
                    }

                }
            }
        }
    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }
    private void loginUserAutomatically(string email, string hashedpassword)
    {
        ADTeam4EF.LoginController loginCtr = new ADTeam4EF.LoginController();
        ADTeam4EF.Employee user = loginCtr.authenticateUser(email, hashedpassword);
        if (user != null)
        {
            {
                userName = user.EmployeeName.ToString();
                role = user.Role.RoleName.ToString();
                storeSession(userName, role);
                redirectToPage(Session["role"].ToString());
            }
        }
    }
    private string GetSHA1HashData(string data)
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
    protected void storeSession(string userName, string role)
    {
        Session["UserName"] = userName;
        Session["Role"] = role;
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
            Response.Redirect("~/StoreSupervisor/StoreSupervisorHome.aspx", false);
        }
    }
}

   
