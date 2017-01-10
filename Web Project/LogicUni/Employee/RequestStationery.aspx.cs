using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;

public partial class RequestStationery : System.Web.UI.Page
{
    ADTeam4EF.EmpNewRequest enr = new EmpNewRequest();
    string departid = "";
    int empid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmployeeID"] != null)
        {
            empid = Convert.ToInt32(Session["EmployeeID"]);
            departid = Convert.ToString(Session["DepartmentID"]);
            if (!IsPostBack)
            {
                Button2.Visible = false;
                Button1.Enabled = Button2.Enabled = DropDownList2.Enabled = false;
                DropCateg();
                Label2.Visible = Label3.Visible = false;
                enr.ReqNo(empid);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", true);
        }             
    }


    public void GridViewReqNew(int requestno)
    {
        GridView1.DataSource = enr.GridViewEmpNewReq(requestno);
        GridView1.DataBind();
        GridView1.Columns[0].Visible = false;
    }


    public void GetRequestNumber()
    {
        List<ADTeam4EF.Request> tt = enr.GetReqNo(departid, empid);
        Label3.Text = Convert.ToString(tt[0].RequestID);
    }

    public void DropCateg()
    {
        DropDownList1.DataSource = enr.DropCategory();
        DropDownList1.DataTextField = "CategoryName";
        DropDownList1.DataValueField = "CategoryID";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "--Select Category--");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int qty = Convert.ToInt32(TextBox1.Text);
            if (qty > 0)
            {
                GetRequestNumber();
                Label2.Visible = Label3.Visible = true;
                GridView1.Visible = true;
                int req = Convert.ToInt32(Label3.Text);
                string itm = DropDownList2.SelectedItem.Value.ToString();
                enr.AddItem(req, itm, qty);
                GridViewReqNew(req);
                Button1.Enabled = false; Button2.Enabled = true; DropDownList2.Enabled = false;
                Button2.Visible = true;
            }
       
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Button2.Visible = false;
        Button2.Enabled = false;
        DropDownList2.SelectedIndex = 0;
        int req = Convert.ToInt32(Label3.Text);
        enr.AddReqNo(req);
        Label2.Visible = Label3.Visible = false;
        Label1.Text = "";
        TextBox1.Text = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.Visible = false; DropDownList2.Enabled = false;
        //mail();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int req = Convert.ToInt32(Label3.Text);
        string te = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string abc = enr.DeleteItem(te, req);
        if (abc == "false")
            GridViewReqNew(req);
        else 
        {
            GridView1.Visible = false;
            Button2.Visible = false;
            Button1.Enabled = Button2.Enabled = DropDownList2.Enabled = false;
            DropCateg();
            DropDownList2.SelectedIndex = 0;
            Label1.Text = "";
            TextBox1.Text = "";
            Label2.Visible = Label3.Visible = false;
        }
    }

    public void mail()
    {
        int req = Convert.ToInt32(Label3.Text);
        enr.sendmail(req, departid, empid);
        GridViewReqNew(req);
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex == 0)
        {
            Button1.Enabled = false;
        }
        else
        {
            int a = Convert.ToInt32(DropDownList1.SelectedItem.Value);
            DropDownList2.DataSource = enr.DropItem(a);
            DropDownList2.DataTextField = "Description";
            DropDownList2.DataValueField = "ItemID";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "--Select Item--");
            Button1.Enabled = false; DropDownList2.Enabled = true;
        }
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedIndex == 0)
        {
            Button1.Enabled = false;
        }
        else
        {
            var dd = enr.UnitMeasure(DropDownList2.SelectedItem.Value);
            Label1.Text = "Unit of measurement: " + dd[0].ToString();
            Button1.Enabled = true;
        }
    }
}
