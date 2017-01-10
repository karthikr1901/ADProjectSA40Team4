using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditRequisition : System.Web.UI.Page
{
    ADTeam4EF.EditRequest edr = new ADTeam4EF.EditRequest();
    int empid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmployeeID"] != null)
        {
            if (!IsPostBack)
            {
                empid = Convert.ToInt32(Session["EmployeeID"]);
                edr.ChangeSt(empid);
                Button5.Visible = false;
                DropReqNo();
                DropCateg(); Button3.Visible = false;
                lblCategory.Visible = lblDescription.Visible = lblQty.Visible = false;
                GridView1.Visible = Button3.Visible = Button4.Visible = DropDownList2.Visible = DropDownList3.Visible = Label1.Visible = TextBox1.Visible = false;
                GridView1.Columns[4].Visible = false;
                Button1.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", true);
        }            
    }

    public void DropReqNo()
    {
        empid = Convert.ToInt32(Session["EmployeeID"]);
        var dd = edr.DropReqNo(empid);
        if (dd != null)
        {
            DropDownList1.DataTextField = "RequestID";
            DropDownList1.DataValueField = "RequestID";
            DropDownList1.DataSource = dd;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "--Select Request Number--");
            editForm.Visible = true;
            noEdit.Visible = false;
        }
        else
        {
            editForm.Visible = false;
            noEdit.Visible = true;
        }
       
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex != 0)
        {
            Button5.Visible = true;
            Session["transidedit"] = DropDownList1.SelectedItem.Value;
            GridView1.Visible = true;
            GridViewGen();
            Button1.Visible = true;
        }
        else
        {
            Button1.Visible = false;
        }
    }

    public void GridViewGen()
    {
        int tran = Convert.ToInt32(Session["transidedit"]);
        var gg = edr.GridViewGen(tran);// (from ffg in f.TransactionDetails join fgj in f.Catalogues on ffg.ItemID equals fgj.ItemID where ffg.TransID == tran select new {ffg.ItemID, ffg.TransID,ffg.Quantity,fgj.Description}).ToList();
        if (!gg.Equals(null))
        {
            GridView1.DataSource = gg;
            GridView1.DataBind();
            GridView1.Columns[0].Visible = false;
        }
            //object a ;
        
        else 
        {
            Button3.Visible = false;
            lblCategory.Visible = lblDescription.Visible = lblQty.Visible = false;
            GridView1.Visible = Button3.Visible = Button4.Visible = DropDownList2.Visible = DropDownList3.Visible = Label1.Visible = TextBox1.Visible = false;
            GridView1.Columns[4].Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (DropDownList1.SelectedIndex != 0)
            {
                Button1.Enabled = false;
                lblCategory.Visible = lblDescription.Visible = lblQty.Visible = true;
                int tran = Convert.ToInt32(Session["transidedit"]);
                DropDownList1.Enabled = DropDownList3.Enabled = Button3.Enabled = false;
                Button4.Visible = Button3.Visible = DropDownList2.Visible = DropDownList3.Visible = Label1.Visible = TextBox1.Visible = true;
                Button3.Enabled = false;
                GridView1.Columns[4].Visible = true;
                edr.UpdReqStat(tran);
                DropCateg();
            }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Button3.Visible = false; Button5.Visible = false; Label1.Text = "";
        lblCategory.Visible = lblDescription.Visible = lblQty.Visible = false;
        GridView1.Visible = Button3.Visible = Button4.Visible = DropDownList2.Visible = DropDownList3.Visible = Label1.Visible = TextBox1.Visible = false;
        GridView1.Columns[4].Visible = false;
        int tran = Convert.ToInt32(Session["transidedit"]);
        edr.UpdReqStatAfterEdit(tran);
        DropDownList1.Enabled = true;
        Button1.Enabled = true;
        if (DropDownList3.Equals(null))
        {
            DropDownList3.SelectedIndex = 0; 

        }
        TextBox1.Text = "";
        DropReqNo();
        DropCateg();
    }
    public void DropCateg()
    {
            DropDownList2.DataSource = edr.DropCategory();
            DropDownList2.DataTextField = "CategoryName";
            DropDownList2.DataValueField = "CategoryID";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "--Select Category--");        
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        int qty = Convert.ToInt32(TextBox1.Text);
        if (qty > 0)
        {
            DropDownList3.Enabled = Button3.Enabled = false;
            int tran = Convert.ToInt32(Session["transidedit"]);
            string itm = DropDownList3.SelectedItem.Value.ToString();
            edr.AddItem(tran, itm, qty);
            GridViewGen();
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedIndex != 0)
        {
            DropDownList3.Enabled = true;
            var ddd = edr.DropItem(Convert.ToInt32(DropDownList2.SelectedValue));
            DropDownList3.DataSource = ddd;
            DropDownList3.DataTextField = "Description";
            DropDownList3.DataValueField = "ItemID";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, "--Select Item--");
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList3.SelectedIndex != 0)
        {
            Button3.Enabled = true;
            var dd = edr.UnitMeasure(DropDownList3.SelectedItem.Value);
            Label1.Text = "Unit of Measurement:" + dd[0].ToString();
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int tran = Convert.ToInt32(Session["transidedit"]);
        string lbl3 = GridView1.DataKeys[e.RowIndex].Value.ToString();
        bool averify = edr.DeleteItem(lbl3, tran);
        if (averify == true)
        {
            Button3.Visible = false; Label1.Text = "";
            lblCategory.Visible = lblDescription.Visible = lblQty.Visible = false;
            GridView1.Visible = Button3.Visible = Button4.Visible = DropDownList2.Visible = DropDownList3.Visible = Label1.Visible = TextBox1.Visible = false;
            GridView1.Columns[4].Visible = false;
            Button5.Visible = false; Button1.Enabled = DropDownList1.Enabled = true;
            DropReqNo();
            DropCateg();
            DropDownList3.SelectedIndex = 0; TextBox1.Text = "";
        }
        else 
        {
            GridViewGen();
        }
    }

    void DeleteReqNo()
    {
        int tran = Convert.ToInt32(Session["transidedit"]);
        edr.DeleteReqNo(tran);
        GridViewGen();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Button1.Enabled = DropDownList1.Enabled = true;
        Button5.Visible = false; Label1.Text = "";
        DeleteReqNo(); Button3.Visible = false;
        lblCategory.Visible = lblDescription.Visible = lblQty.Visible = false;
        GridView1.Visible = Button3.Visible = Button4.Visible = DropDownList2.Visible = DropDownList3.Visible = Label1.Visible = TextBox1.Visible = false;
        GridView1.Columns[4].Visible = false;
        DropReqNo();
        DropCateg();
        if (DropDownList3.Equals(null))
        {
            DropDownList3.SelectedIndex = 0;

        } 
        TextBox1.Text = "";
    }
}
