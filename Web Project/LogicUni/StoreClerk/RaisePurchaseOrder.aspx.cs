using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
using System.Data;

public partial class RaisePurchaseOrder : System.Web.UI.Page
{
    DataTable dt;
    int empid=0;
    ADTeam4EF.PurchaseControlOrder pco = new PurchaseControlOrder();
    int a = 0, b = 0, c = 0, i = 0;
    string a1 = "", a2 = "", itmsid1 = "", itmsid2 = "", itmsid3 = "";
    int i1 = 0, i2 = 0, i3 = 0, i4 = 0, i5 = 0, i6 = 0, i7 = 0;
    decimal d1 = 0, d2 = 0, d3 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        empid = Convert.ToInt32(Session["EmployeeID"]);
        VisibleCtrl(false);
        if (!IsPostBack)
        {
            loaddatatable();
            gridload();
        }
        
    }

    protected void loaddatatable()
    {
        dt = new DataTable();
        dt.Columns.Add("itemid"); dt.Columns.Add("description"); dt.Columns.Add("balance"); dt.Columns.Add("reorderlevel");
        dt.Columns.Add("reorderqty"); dt.Columns.Add("suggestedqty"); dt.Columns.Add("suppliername");
        dt.Columns.Add("supplierid"); dt.Columns.Add("price"); dt.Columns.Add("totalcost");

    }
    protected void VisibleCtrl(bool a)
    {
        pv.Visible = a;
        Label3.Visible = Label4.Visible = Label5.Visible = Label6.Visible = Label8.Visible = Label11.Visible = a;
        RadioButtonList1.Visible = DropDownList1.Visible = DropDownList2.Visible = a;
        lblItemID.Visible = lblDescript.Visible = lblsgqty1.Visible = lblsgqty2.Visible = lblsgqty3.Visible = a;
        Number1.Visible = Number2.Visible = Number3.Visible = Button2.Visible = a;
        Label12.Visible = Label13.Visible = Label14.Visible = a;
    }

    protected void VisibleCtrl2(bool a)
    {
        Label12.Visible = Label13.Visible = Label14.Visible = a;
        DropDownList1.Visible = DropDownList2.Visible =  a;
        Number1.Visible = Number2.Visible = Number3.Visible = Button2.Visible = lblsgqty1.Visible = lblsgqty2.Visible = lblsgqty3.Visible = a;
        Label6.Visible = Label8.Visible = Label11.Visible = a;
    }
    protected void gridload()
    {
        if (pco.PageLoadGrid() != null)
        {
            dt = pco.PageLoadGrid();
            ViewState["purchasedatatable"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            noLowLevel.Visible = false;
            txtDate.Visible = true;
            Button1.Visible = true;
            GridView1.Visible = true;
        }
        else
        {
            GridView1.Visible = false;
            Button1.Visible = false;
            txtDate.Visible = false;
            noLowLevel.Visible = true;
        }
     
    }

    protected void lblNo_Click(object sender, EventArgs e)
    {
        RequiredFieldValidator1.Visible = false;
        txtDate.Visible = false;
        Button1.Visible = false;
        LinkButton lbl = new LinkButton();
        lbl = (LinkButton)sender;
        FillDetails(lbl.Text);
        Session["lbl"] = lbl.Text;
        //btnSubmit.Enabled = true;

    }
    private void FillDetails(string ItemID)
    {
        VisibleCtrl(true);
        VisibleCtrl2(false);
        lblItemID.Text = ItemID;
        List<PurchaseClassOrder> pclo = new List<PurchaseClassOrder>();
        pclo = pco.ItemOne(ItemID);
        lblDescript.Text = pclo[0].Description;
        int sw = 0;
        dt = (DataTable)ViewState["purchasedatatable"];
        for (int rfc = 0; rfc < dt.Rows.Count; rfc++)
        {
            if (Convert.ToString(dt.Rows[rfc]["itemid"]) == ItemID)
            {
                sw += Convert.ToInt32(dt.Rows[rfc]["suggestedqty"]);
                Session["suggestedqty1"] = sw;
            }
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button2.Visible = true;
            if (RadioButtonList1.SelectedValue == "1")
                Supplier1();
            else if (RadioButtonList1.SelectedValue == "2")
                Supplier2();
            else if (RadioButtonList1.SelectedValue == "3")
                Supplier3();
    }

    protected void Supplier1()
    {
        VisibleCtrl(true);
        VisibleCtrl2(false);
        string ItemID = Convert.ToString(Session["lbl"]);
        Label6.Visible = DropDownList1.Visible = Number1.Visible = lblsgqty1.Visible = true;
        Button2.Visible = true;
        DropDownList1.DataSource = pco.ItemOne(ItemID);
        DropDownList1.DataTextField = "SupplierName";
        DropDownList1.DataValueField = "SupplierID";
        DropDownList1.DataBind();
        List<PurchaseClassOrder> pclo = new List<PurchaseClassOrder>();
        pclo = pco.ItemOne(ItemID);
        lblsgqty1.Text = Convert.ToString(Session["suggestedqty1"]);
    }
    protected void Supplier2()
    {
        VisibleCtrl(true);
        VisibleCtrl2(false);
        string ItemID = Convert.ToString(Session["lbl"]);
        Label6.Visible = DropDownList1.Visible = Number1.Visible = lblsgqty1.Visible = true;
        Label8.Visible = DropDownList2.Visible = Number2.Visible = lblsgqty2.Visible = true;
        Button2.Visible = true;
        DropDownList1.DataSource = pco.ItemOne(ItemID);
        DropDownList1.DataTextField = "SupplierName";
        DropDownList1.DataValueField = "SupplierID";
        DropDownList1.DataBind();
        DropDownList2.DataSource = pco.ItemOne(ItemID);
        DropDownList2.DataTextField = "SupplierName";
        DropDownList2.DataValueField = "SupplierID";
        DropDownList2.DataBind();
        List<PurchaseClassOrder> pclo = new List<PurchaseClassOrder>();
        pclo = pco.ItemOne(ItemID);
        int asqty = Convert.ToInt32(Session["suggestedqty1"]);
        if (asqty % 2 == 0)
        { a = b = pclo[0].SuggestedQty / 2; }
        else
        { a = (asqty / 2) + 1; b = asqty / 2; }
        lblsgqty1.Text = Convert.ToString(a);
        lblsgqty2.Text = Convert.ToString(b);
    }
    protected void Supplier3()
    {
        VisibleCtrl(true);
        VisibleCtrl2(false);
        string ItemID = Convert.ToString(Session["lbl"]);
        Label6.Visible = Label12.Visible = Number1.Visible = lblsgqty1.Visible = true;
        Label8.Visible = Label13.Visible = Number2.Visible = lblsgqty2.Visible = true;
        Label11.Visible = Label14.Visible = Number3.Visible = lblsgqty3.Visible = true;
        Button2.Visible = true;
        List<PurchaseClassOrder> pclo = new List<PurchaseClassOrder>();
        pclo = pco.ItemOne(ItemID);
        int asqty = Convert.ToInt32(Session["suggestedqty1"]);
        Label12.Text = pclo[0].SupplierName;
        Label13.Text = pclo[1].SupplierName;
        Label14.Text = pclo[2].SupplierName;
        if (asqty % 3 == 0)
        { a = b = c = asqty / 3; }
        else if (asqty % 3 == 1)
        { a = (asqty / 3) + 1; b = asqty / 3; c = asqty / 3; }
        else
        { a = (asqty / 3) + 1; b = (asqty / 3) + 1; c = asqty / 3; }
        lblsgqty1.Text = Convert.ToString(a);
        lblsgqty2.Text = Convert.ToString(b);
        lblsgqty3.Text = Convert.ToString(c);
    }
    

    protected void gridload2()
    {
        DataTable dt3 = (DataTable)ViewState["purchasedatatable"];
        GridView1.DataSource = dt3;
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        RequiredFieldValidator1.Visible = true;
        txtDate.Visible = true;
        Button1.Visible = true;
        dt = (DataTable)ViewState["purchasedatatable"];
        string select = RadioButtonList1.SelectedValue;
        string ItemID = Convert.ToString(Session["lbl"]);
        DataTable d23t = new DataTable();
        d23t.Columns.Add("itemid"); d23t.Columns.Add("description"); d23t.Columns.Add("balance"); d23t.Columns.Add("reorderlevel");
        d23t.Columns.Add("reorderqty"); d23t.Columns.Add("suggestedqty"); d23t.Columns.Add("suppliername");
        d23t.Columns.Add("supplierid"); d23t.Columns.Add("price"); d23t.Columns.Add("totalcost");
        
        DataRow dr, d2r, d3r, d4r, d22r, d23r, d24r; int ccount = 0;
       // d3r = d23t.NewRow(); d4r = d23t.NewRow();
        for (i = 0; i < dt.Rows.Count; i++)
        {
            dr = dt.Rows[i];
            if (Convert.ToString(dr["itemid"]) == ItemID)
            {
                ccount++;
                if(ccount==1)
                { 
                    d2r = d23t.NewRow();
                    d2r["itemid"] = dt.Rows[i]["itemid"]; d2r["description"] = dt.Rows[i]["description"]; 
                    d2r["balance"] = dt.Rows[i]["balance"]; d2r["reorderlevel"] = dt.Rows[i]["reorderlevel"];
                    d2r["reorderqty"] = dt.Rows[i]["reorderqty"]; d2r["suggestedqty"] = dt.Rows[i]["suggestedqty"];
                    d2r["suppliername"] = dt.Rows[i]["suppliername"]; d2r["supplierid"] = dt.Rows[i]["supplierid"];
                    d2r["price"] = dt.Rows[i]["price"]; d2r["totalcost"] = dt.Rows[i]["totalcost"];                
                    d23t.Rows.Add(d2r); ViewState["drow1"] = d23t; 
                }                    
                else if (ccount == 2)
                { 
                    d3r = d23t.NewRow();
                    d3r["itemid"] = dt.Rows[i]["itemid"]; d3r["description"] = dt.Rows[i]["description"];
                    d3r["balance"] = dt.Rows[i]["balance"]; d3r["reorderlevel"] = dt.Rows[i]["reorderlevel"];
                    d3r["reorderqty"] = dt.Rows[i]["reorderqty"]; d3r["suggestedqty"] = dt.Rows[i]["suggestedqty"];
                    d3r["suppliername"] = dt.Rows[i]["suppliername"]; d3r["supplierid"] = dt.Rows[i]["supplierid"];
                    d3r["price"] = dt.Rows[i]["price"]; d3r["totalcost"] = dt.Rows[i]["totalcost"];   
                    d23t.Rows.Add(d3r); ViewState["drow2"] = d23t; 
                }                    
                else if (ccount == 3)
                { 
                    d4r = d23t.NewRow();
                    d4r["itemid"] = dt.Rows[i]["itemid"]; d4r["description"] = dt.Rows[i]["description"];
                    d4r["balance"] = dt.Rows[i]["balance"]; d4r["reorderlevel"] = dt.Rows[i]["reorderlevel"];
                    d4r["reorderqty"] = dt.Rows[i]["reorderqty"]; d4r["suggestedqty"] = dt.Rows[i]["suggestedqty"];
                    d4r["suppliername"] = dt.Rows[i]["suppliername"]; d4r["supplierid"] = dt.Rows[i]["supplierid"];
                    d4r["price"] = dt.Rows[i]["price"]; d4r["totalcost"] = dt.Rows[i]["totalcost"];                       
                    d23t.Rows.Add(d4r); ViewState["drow3"] = d23t; 
                }                    
                a1 = Convert.ToString(dr["itemid"]); a2 = Convert.ToString(dr["description"]); i4 += Convert.ToInt32(dr["suggestedqty"]);
                i1 = Convert.ToInt32(dr["balance"]); i2 = Convert.ToInt32(dr["reorderlevel"]); i3 = Convert.ToInt32(dr["reorderqty"]);
                dr.Delete();
                i--;
            }
        }
        DataRow dr2,dr3,dr4;
        DataTable dt2 = new DataTable();
        if (select == "1")
        {
            itmsid1 = DropDownList1.SelectedValue;
            if (Number1.Text == "" || Number1.Text == null)  i5 = 0; 
            else
            {
                int j1 = Convert.ToInt32(Number1.Text);
                if (j1 >= 0)
                    i5 = Convert.ToInt32(Number1.Text);                
                else  i5 = 0; 
            }
            i5 = Convert.ToInt32(Number1.Text);
            if (i5 >= i4)
            {
                dt2 = pco.ItemOnePriority(ItemID, itmsid1);
                dr2 = dt.NewRow();
                dr2["itemid"] = a1; dr2["description"] = a2; dr2["balance"] = i1; dr2["reorderlevel"] = i2; dr2["reorderqty"] = i3;
                dr2["suggestedqty"] = i5; dr2["suppliername"] = dt2.Rows[0]["suppliername"]; dr2["supplierid"] = dt2.Rows[0]["supplierid"];
                dr2["price"] = dt2.Rows[0]["price"];
                d1 = i5 * Convert.ToDecimal(dt2.Rows[0]["price"]);
                dr2["totalcost"] = d1;
                dt.Rows.Add(dr2);
                ViewState["purchasedatatable"] = dt;
                gridload2();
            }
            else 
            {
                if (ccount == 1)
                {
                    d22r = dt.NewRow();
                    d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"];
                    d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"];
                    d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"];
                    d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"];
                    d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"];                
                    dt.Rows.Add(d22r);
                    ViewState["purchasedatatable"] = dt;
                    gridload2();
                }
                else if (ccount == 2)
                {
                    d22r = dt.NewRow(); d23r = dt.NewRow();
                    d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"];             d23r["itemid"] = d23t.Rows[1]["itemid"]; d23r["description"] = d23t.Rows[1]["description"];
                    d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"];         d23r["balance"] = d23t.Rows[1]["balance"]; d23r["reorderlevel"] = d23t.Rows[1]["reorderlevel"];
                    d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"];   d23r["reorderqty"] = d23t.Rows[1]["reorderqty"]; d23r["suggestedqty"] = d23t.Rows[1]["suggestedqty"];
                    d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"];   d23r["suppliername"] = d23t.Rows[1]["suppliername"]; d23r["supplierid"] = d23t.Rows[1]["supplierid"];
                    d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"];                   d23r["price"] = d23t.Rows[1]["price"]; d23r["totalcost"] = d23t.Rows[1]["totalcost"];     
                    dt.Rows.Add(d22r); dt.Rows.Add(d23r);
                    ViewState["purchasedatatable"] = dt;
                    gridload2();
                }
                else if (ccount == 3)
                {
                    d22r = dt.NewRow(); d23r = dt.NewRow();
                    d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"];             d23r["itemid"] = d23t.Rows[1]["itemid"]; d23r["description"] = d23t.Rows[1]["description"];
                    d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"];         d23r["balance"] = d23t.Rows[1]["balance"]; d23r["reorderlevel"] = d23t.Rows[1]["reorderlevel"];
                    d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"];   d23r["reorderqty"] = d23t.Rows[1]["reorderqty"]; d23r["suggestedqty"] = d23t.Rows[1]["suggestedqty"];
                    d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"];   d23r["suppliername"] = d23t.Rows[1]["suppliername"]; d23r["supplierid"] = d23t.Rows[1]["supplierid"];
                    d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"];                   d23r["price"] = d23t.Rows[1]["price"]; d23r["totalcost"] = d23t.Rows[1]["totalcost"];
                    d24r = dt.NewRow();
                    d24r["itemid"] = d23t.Rows[2]["itemid"]; d24r["description"] = d23t.Rows[2]["description"];
                    d24r["balance"] = d23t.Rows[2]["balance"]; d24r["reorderlevel"] = d23t.Rows[2]["reorderlevel"];
                    d24r["reorderqty"] = d23t.Rows[2]["reorderqty"]; d24r["suggestedqty"] = d23t.Rows[2]["suggestedqty"];
                    d24r["suppliername"] = d23t.Rows[2]["suppliername"]; d24r["supplierid"] = d23t.Rows[2]["supplierid"];
                    d24r["price"] = d23t.Rows[2]["price"]; d24r["totalcost"] = d23t.Rows[2]["totalcost"];    
                    
                    dt.Rows.Add(d22r); dt.Rows.Add(d23r); dt.Rows.Add(d24r);
                    ViewState["purchasedatatable"] = dt;
                    gridload2();
                }
            }
            
        }
        else if (select == "2")
        {
            itmsid1 = DropDownList1.SelectedValue;
            itmsid2 = DropDownList2.SelectedValue;
            if (Number1.Text == "" || Number2.Text == "" || Number1.Text == null || Number2.Text == null) { i5 = i6 = 0; }
            else
            {
                int j1 = Convert.ToInt32(Number1.Text);
                int j2 = Convert.ToInt32(Number2.Text);
                if ((j1 >= 0) && (j2 >= 0))
                {
                    i5 = Convert.ToInt32(Number1.Text);
                    i6 = Convert.ToInt32(Number2.Text);
                }
                else { i5 = i6 = 0; }
            }
            if (itmsid1 == itmsid2)
            {
                if (ccount == 1)
                {
                    d22r = dt.NewRow();
                    d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"];
                    d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"];
                    d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"];
                    d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"];
                    d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"];
                    dt.Rows.Add(d22r);
                    ViewState["purchasedatatable"] = dt;
                    gridload2();
                }
                else if (ccount == 2)
                {
                    d22r = dt.NewRow(); d23r = dt.NewRow();
                    d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"]; d23r["itemid"] = d23t.Rows[1]["itemid"]; d23r["description"] = d23t.Rows[1]["description"];
                    d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"]; d23r["balance"] = d23t.Rows[1]["balance"]; d23r["reorderlevel"] = d23t.Rows[1]["reorderlevel"];
                    d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"]; d23r["reorderqty"] = d23t.Rows[1]["reorderqty"]; d23r["suggestedqty"] = d23t.Rows[1]["suggestedqty"];
                    d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"]; d23r["suppliername"] = d23t.Rows[1]["suppliername"]; d23r["supplierid"] = d23t.Rows[1]["supplierid"];
                    d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"]; d23r["price"] = d23t.Rows[1]["price"]; d23r["totalcost"] = d23t.Rows[1]["totalcost"];
                    dt.Rows.Add(d22r); dt.Rows.Add(d23r);
                    ViewState["purchasedatatable"] = dt;
                    gridload2();
                }
                else if (ccount == 3)
                {
                    d22r = dt.NewRow(); d23r = dt.NewRow();
                    d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"]; d23r["itemid"] = d23t.Rows[1]["itemid"]; d23r["description"] = d23t.Rows[1]["description"];
                    d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"]; d23r["balance"] = d23t.Rows[1]["balance"]; d23r["reorderlevel"] = d23t.Rows[1]["reorderlevel"];
                    d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"]; d23r["reorderqty"] = d23t.Rows[1]["reorderqty"]; d23r["suggestedqty"] = d23t.Rows[1]["suggestedqty"];
                    d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"]; d23r["suppliername"] = d23t.Rows[1]["suppliername"]; d23r["supplierid"] = d23t.Rows[1]["supplierid"];
                    d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"]; d23r["price"] = d23t.Rows[1]["price"]; d23r["totalcost"] = d23t.Rows[1]["totalcost"];
                    d24r = dt.NewRow();
                    d24r["itemid"] = d23t.Rows[2]["itemid"]; d24r["description"] = d23t.Rows[2]["description"];
                    d24r["balance"] = d23t.Rows[2]["balance"]; d24r["reorderlevel"] = d23t.Rows[2]["reorderlevel"];
                    d24r["reorderqty"] = d23t.Rows[2]["reorderqty"]; d24r["suggestedqty"] = d23t.Rows[2]["suggestedqty"];
                    d24r["suppliername"] = d23t.Rows[2]["suppliername"]; d24r["supplierid"] = d23t.Rows[2]["supplierid"];
                    d24r["price"] = d23t.Rows[2]["price"]; d24r["totalcost"] = d23t.Rows[2]["totalcost"];

                    dt.Rows.Add(d22r); dt.Rows.Add(d23r); dt.Rows.Add(d24r);
                    ViewState["purchasedatatable"] = dt;
                    gridload2();
                }
            }
            else
            {
                if ((i5 + i6) >= i4)
                {
                    dt2 = pco.ItemOnePriority(ItemID, itmsid1);
                    dr2 = dt.NewRow();
                    dr2["itemid"] = a1; dr2["description"] = a2; dr2["balance"] = i1; dr2["reorderlevel"] = i2; dr2["reorderqty"] = i3;
                    dr2["suggestedqty"] = i5; dr2["suppliername"] = dt2.Rows[0]["suppliername"]; dr2["supplierid"] = dt2.Rows[0]["supplierid"];
                    dr2["price"] = dt2.Rows[0]["price"];
                    d1 = i5 * Convert.ToDecimal(dt2.Rows[0]["price"]);
                    dr2["totalcost"] = d1;
                    dt.Rows.Add(dr2);
                    dt2 = pco.ItemOnePriority(ItemID, itmsid2);
                    dr3 = dt.NewRow();
                    dr3["itemid"] = a1; dr3["description"] = a2; dr3["balance"] = i1; dr3["reorderlevel"] = i2; dr3["reorderqty"] = i3;
                    dr3["suggestedqty"] = i6; dr3["suppliername"] = dt2.Rows[0]["suppliername"]; dr3["supplierid"] = dt2.Rows[0]["supplierid"];
                    dr3["price"] = dt2.Rows[0]["price"];
                    d2 = i5 * Convert.ToDecimal(dt2.Rows[0]["price"]);
                    dr3["totalcost"] = d2;
                    dt.Rows.Add(dr3);
                    ViewState["purchasedatatable"] = dt; ;
                    gridload2();
                }
                else 
                {
                    if (ccount == 1)
                    {
                        d22r = dt.NewRow();
                        d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"];
                        d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"];
                        d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"];
                        d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"];
                        d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"];
                        dt.Rows.Add(d22r);
                        ViewState["purchasedatatable"] = dt;
                        gridload2();
                    }
                    else if (ccount == 2)
                    {
                        d22r = dt.NewRow(); d23r = dt.NewRow();
                        d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"]; d23r["itemid"] = d23t.Rows[1]["itemid"]; d23r["description"] = d23t.Rows[1]["description"];
                        d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"]; d23r["balance"] = d23t.Rows[1]["balance"]; d23r["reorderlevel"] = d23t.Rows[1]["reorderlevel"];
                        d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"]; d23r["reorderqty"] = d23t.Rows[1]["reorderqty"]; d23r["suggestedqty"] = d23t.Rows[1]["suggestedqty"];
                        d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"]; d23r["suppliername"] = d23t.Rows[1]["suppliername"]; d23r["supplierid"] = d23t.Rows[1]["supplierid"];
                        d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"]; d23r["price"] = d23t.Rows[1]["price"]; d23r["totalcost"] = d23t.Rows[1]["totalcost"];
                        dt.Rows.Add(d22r); dt.Rows.Add(d23r);
                        ViewState["purchasedatatable"] = dt;
                        gridload2();
                    }
                    else if (ccount == 3)
                    {
                        d22r = dt.NewRow(); d23r = dt.NewRow();
                        d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"]; d23r["itemid"] = d23t.Rows[1]["itemid"]; d23r["description"] = d23t.Rows[1]["description"];
                        d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"]; d23r["balance"] = d23t.Rows[1]["balance"]; d23r["reorderlevel"] = d23t.Rows[1]["reorderlevel"];
                        d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"]; d23r["reorderqty"] = d23t.Rows[1]["reorderqty"]; d23r["suggestedqty"] = d23t.Rows[1]["suggestedqty"];
                        d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"]; d23r["suppliername"] = d23t.Rows[1]["suppliername"]; d23r["supplierid"] = d23t.Rows[1]["supplierid"];
                        d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"]; d23r["price"] = d23t.Rows[1]["price"]; d23r["totalcost"] = d23t.Rows[1]["totalcost"];
                        d24r = dt.NewRow();
                        d24r["itemid"] = d23t.Rows[2]["itemid"]; d24r["description"] = d23t.Rows[2]["description"];
                        d24r["balance"] = d23t.Rows[2]["balance"]; d24r["reorderlevel"] = d23t.Rows[2]["reorderlevel"];
                        d24r["reorderqty"] = d23t.Rows[2]["reorderqty"]; d24r["suggestedqty"] = d23t.Rows[2]["suggestedqty"];
                        d24r["suppliername"] = d23t.Rows[2]["suppliername"]; d24r["supplierid"] = d23t.Rows[2]["supplierid"];
                        d24r["price"] = d23t.Rows[2]["price"]; d24r["totalcost"] = d23t.Rows[2]["totalcost"];

                        dt.Rows.Add(d22r); dt.Rows.Add(d23r); dt.Rows.Add(d24r);
                        ViewState["purchasedatatable"] = dt;
                        gridload2();
                    }
                }
            }
        }
        else if (select == "3")
        {
            if (Number1.Text == "" || Number2.Text == "" || Number3.Text == "" || Number1.Text == null || Number2.Text == null || Number3.Text == null) { i5 = i6 = i7 = 0; }
            else
            {
                int j1 = Convert.ToInt32(Number1.Text);
                int j2 = Convert.ToInt32(Number2.Text);
                int j3 = Convert.ToInt32(Number3.Text);
                if ((j1 >= 0) && (j2 >= 0) && (j3 >= 0))
                {
                    i5 = Convert.ToInt32(Number1.Text);
                    i6 = Convert.ToInt32(Number2.Text);
                    i7 = Convert.ToInt32(Number3.Text);
                }
                else { i5 = i6 = i7 = 0; }
            }
            List<PurchaseClassOrder> pclo = new List<PurchaseClassOrder>();
            pclo = pco.ItemOne(ItemID);
            itmsid1 = Convert.ToString(pclo[0].SupplierID);
            itmsid2 = Convert.ToString(pclo[1].SupplierID);
            itmsid3 = Convert.ToString(pclo[2].SupplierID);
            if ((i5 + i6 + i7) >= i4)
            {
                dt2 = pco.ItemOnePriority(ItemID, itmsid1);
                dr2 = dt.NewRow();
                dr2["itemid"] = a1; dr2["description"] = a2; dr2["balance"] = i1; dr2["reorderlevel"] = i2; dr2["reorderqty"] = i3;
                dr2["suggestedqty"] = i5; dr2["suppliername"] = dt2.Rows[0]["suppliername"]; dr2["supplierid"] = dt2.Rows[0]["supplierid"];
                dr2["price"] = dt2.Rows[0]["price"];
                d1 = i5 * Convert.ToDecimal(dt2.Rows[0]["price"]);
                dr2["totalcost"] = d1;
                dt.Rows.Add(dr2);
                dt2 = pco.ItemOnePriority(ItemID, itmsid2);
                dr3 = dt.NewRow();
                dr3["itemid"] = a1; dr3["description"] = a2; dr3["balance"] = i1; dr3["reorderlevel"] = i2; dr3["reorderqty"] = i3;
                dr3["suggestedqty"] = i6; dr3["suppliername"] = dt2.Rows[0]["suppliername"]; dr3["supplierid"] = dt2.Rows[0]["supplierid"];
                dr3["price"] = dt2.Rows[0]["price"];
                d2 = i5 * Convert.ToDecimal(dt2.Rows[0]["price"]);
                dr3["totalcost"] = d2;
                dt.Rows.Add(dr3);
                dt2 = pco.ItemOnePriority(ItemID, itmsid3);
                dr4 = dt.NewRow();
                dr4["itemid"] = a1; dr4["description"] = a2; dr4["balance"] = i1; dr4["reorderlevel"] = i2; dr4["reorderqty"] = i3;
                dr4["suggestedqty"] = i7; dr4["suppliername"] = dt2.Rows[0]["suppliername"]; dr4["supplierid"] = dt2.Rows[0]["supplierid"];
                dr4["price"] = dt2.Rows[0]["price"];
                d3 = i5 * Convert.ToDecimal(dt2.Rows[0]["price"]);
                dr4["totalcost"] = d3;
                dt.Rows.Add(dr4);
                ViewState["purchasedatatable"] = dt; ;
                gridload2();
            }
            else 
            {
                if (ccount == 1)
                {
                    d22r = dt.NewRow();
                    d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"];
                    d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"];
                    d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"];
                    d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"];
                    d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"];
                    dt.Rows.Add(d22r);
                    ViewState["purchasedatatable"] = dt;
                    gridload2();
                }
                else if (ccount == 2)
                {
                    d22r = dt.NewRow(); d23r = dt.NewRow();
                    d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"]; d23r["itemid"] = d23t.Rows[1]["itemid"]; d23r["description"] = d23t.Rows[1]["description"];
                    d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"]; d23r["balance"] = d23t.Rows[1]["balance"]; d23r["reorderlevel"] = d23t.Rows[1]["reorderlevel"];
                    d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"]; d23r["reorderqty"] = d23t.Rows[1]["reorderqty"]; d23r["suggestedqty"] = d23t.Rows[1]["suggestedqty"];
                    d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"]; d23r["suppliername"] = d23t.Rows[1]["suppliername"]; d23r["supplierid"] = d23t.Rows[1]["supplierid"];
                    d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"]; d23r["price"] = d23t.Rows[1]["price"]; d23r["totalcost"] = d23t.Rows[1]["totalcost"];
                    dt.Rows.Add(d22r); dt.Rows.Add(d23r);
                    ViewState["purchasedatatable"] = dt;
                    gridload2();
                }
                else if (ccount == 3)
                {
                    d22r = dt.NewRow(); d23r = dt.NewRow();
                    d22r["itemid"] = d23t.Rows[0]["itemid"]; d22r["description"] = d23t.Rows[0]["description"]; d23r["itemid"] = d23t.Rows[1]["itemid"]; d23r["description"] = d23t.Rows[1]["description"];
                    d22r["balance"] = d23t.Rows[0]["balance"]; d22r["reorderlevel"] = d23t.Rows[0]["reorderlevel"]; d23r["balance"] = d23t.Rows[1]["balance"]; d23r["reorderlevel"] = d23t.Rows[1]["reorderlevel"];
                    d22r["reorderqty"] = d23t.Rows[0]["reorderqty"]; d22r["suggestedqty"] = d23t.Rows[0]["suggestedqty"]; d23r["reorderqty"] = d23t.Rows[1]["reorderqty"]; d23r["suggestedqty"] = d23t.Rows[1]["suggestedqty"];
                    d22r["suppliername"] = d23t.Rows[0]["suppliername"]; d22r["supplierid"] = d23t.Rows[0]["supplierid"]; d23r["suppliername"] = d23t.Rows[1]["suppliername"]; d23r["supplierid"] = d23t.Rows[1]["supplierid"];
                    d22r["price"] = d23t.Rows[0]["price"]; d22r["totalcost"] = d23t.Rows[0]["totalcost"]; d23r["price"] = d23t.Rows[1]["price"]; d23r["totalcost"] = d23t.Rows[1]["totalcost"];
                    d24r = dt.NewRow();
                    d24r["itemid"] = d23t.Rows[2]["itemid"]; d24r["description"] = d23t.Rows[2]["description"];
                    d24r["balance"] = d23t.Rows[2]["balance"]; d24r["reorderlevel"] = d23t.Rows[2]["reorderlevel"];
                    d24r["reorderqty"] = d23t.Rows[2]["reorderqty"]; d24r["suggestedqty"] = d23t.Rows[2]["suggestedqty"];
                    d24r["suppliername"] = d23t.Rows[2]["suppliername"]; d24r["supplierid"] = d23t.Rows[2]["supplierid"];
                    d24r["price"] = d23t.Rows[2]["price"]; d24r["totalcost"] = d23t.Rows[2]["totalcost"];

                    dt.Rows.Add(d22r); dt.Rows.Add(d23r); dt.Rows.Add(d24r);
                    ViewState["purchasedatatable"] = dt;
                    gridload2();
                }
            }
        }
        
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime deliveryDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null);
        dt = (DataTable)ViewState["purchasedatatable"];
        pco.RaisePurchaseControlOrder(dt, empid, deliveryDate);
        gridload();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RequiredFieldValidator1.Visible = true;
        txtDate.Visible = true;
        Button1.Visible = true;
        GridView1.PageIndex = e.NewPageIndex;
        gridload();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("../StoreClerk/RaisePurchaseOrder.aspx", false);
    }
}