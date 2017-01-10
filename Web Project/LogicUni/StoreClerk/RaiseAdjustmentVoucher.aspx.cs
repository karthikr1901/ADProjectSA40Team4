using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
using System.Data;
using System.Transactions;

public partial class RaiseAdjustmentVoucher : System.Web.UI.Page
{
    RaiseAdjustmentController raiseAdjustmentController;
    public int categoryID;
    public decimal tP = 0;
    public decimal totalPrice;
    int empID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session != null)
        {
            empID = Convert.ToInt16(Session["EmployeeID"]);
            raiseAdjustmentController = new RaiseAdjustmentController();
            int count = raiseAdjustmentController.checkAdjustCount(empID);
            if (ViewState["CurrentTotalPrice"] != null)  //Add New
            {
                totalPrice = (decimal)ViewState["CurrentTotalPrice"];
            }

            if (count > 0)
            {
                if (!IsPostBack)
                {
                    //ddlCategory.Visible = false;
                    BindGridView();
                    ddlCategory.DataSource = raiseAdjustmentController.getCategories();           
                    btnSubmit.Visible = true;
                    adjustmentform.Visible = false;
                    txtPending.Visible = true;
                }
            }
            else
            {
                if (!IsPostBack)
                {
                    txtPending.Visible = false;
                    raiseAdjustmentController = new RaiseAdjustmentController();
                    ddlItemID.Visible = false;
                    lblDescription.Visible = false;
                    btnSubmit.Visible = false;
                    adjustmentform.Visible = true;
                    ddlCategory.DataSource = raiseAdjustmentController.getCategories();
                    ddlCategory.DataValueField = "CategoryID";
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, "--Choose Category Name --");

                    GridView1.DataSource = CreateDataTable();
                    GridView1.DataBind();
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    void BindGridView()
    {
        raiseAdjustmentController = new RaiseAdjustmentController();
        int count = raiseAdjustmentController.checkAdjustCount(empID);
        DataTable dt = new DataTable();
        if (count > 0)
        {
            int adjID = raiseAdjustmentController.getAdjustID(empID);
            dt = raiseAdjustmentController.addNewItemtoGrid(adjID);
            ViewState["Currentdata"] = dt;
            GridView1.DataSource = ViewState["Currentdata"];
            GridView1.DataBind();
        }
        else
        {
            Adjustment adj = new Adjustment();
            dt = raiseAdjustmentController.addNewItemtoGrid(adj.AdjustmentID);
            ViewState["Currentdata"] = dt;
            GridView1.DataSource = ViewState["Currentdata"];
            GridView1.DataBind();
        }

        if (ViewState["CurrentData"] != null)
        {
            GridView1.DataSource = (DataTable)ViewState["CurrentData"];
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        ViewState["CurrentData"] = dt;
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCategory.SelectedIndex > 0)
        {
            ddlItemID.Visible = true;
            lblDescription.Visible = true;
            raiseAdjustmentController = new RaiseAdjustmentController();
            //string cateName = ddlCategory.SelectedValue;
            //int cateid = raiseAdjustmentController.getCategoryID(cateName);
            int cateid = ddlCategory.SelectedIndex;
            if (raiseAdjustmentController.getItemsByCategoryId(cateid) != null)
            {
                List<ADTeam4EF.Item> itemList = raiseAdjustmentController.getItemsByCategoryId(cateid);
                ddlItemID.DataSource = itemList;
                ddlItemID.DataValueField = "ItemID";
                ddlItemID.DataTextField = "Description";
                ddlItemID.DataBind();
                ddlItemID.Items.Insert(0, "--Choose Description --");
            }

        }
        else
            ddlItemID.Items.Clear();

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (ddlCategory.SelectedIndex != 0)
            {
                raiseAdjustmentController = new RaiseAdjustmentController();
                string ItemID = ddlItemID.SelectedValue;
                int qty = 0;
                qty = Convert.ToInt32(txtQty.Text);
                string freegift = "Free gift in offer pack";
                string specialgift = "Special gift";
                if (ddlRemark.SelectedValue.Equals(freegift) || ddlRemark.SelectedValue.Equals(specialgift))
                {
                    if (ViewState["CurrentData"] != null)
                    {
                        DataTable dt1 = (DataTable)ViewState["CurrentData"];
                        int count = dt1.Rows.Count;
                        BindGrid(count);
                    }
                    else
                    {
                        BindGrid(1);
                    }
                    ddlCategory.Focus();
                    lblMessage.Visible = false;
                    btnSubmit.Visible = true;
                }
                else
                {
                    if (raiseAdjustmentController.checkQuantity(ItemID, qty) >= qty)
                    {
                        if (ViewState["CurrentData"] != null)
                        {
                            DataTable dt1 = (DataTable)ViewState["CurrentData"];
                            int count = dt1.Rows.Count;
                            BindGrid(count);
                        }
                        else
                        {
                            BindGrid(1);
                        }
                        ddlCategory.Focus();
                        lblMessage.Visible = false;
                        btnSubmit.Visible = true;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Your Quantity is more than Quantity on Hand";
                    }
                   
                }
            }

            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please choose a category!";
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        raiseAdjustmentController = new RaiseAdjustmentController();
        DataTable dt = (DataTable)ViewState["CurrentData"];
        string name = Session["userName"].ToString();
        int count = raiseAdjustmentController.checkAdjustCount(empID);
        if (count > 0)
        {
            int adjID = raiseAdjustmentController.getAdjustID(empID);
            bool raise = raiseAdjustmentController.updateDataToAdjustment(adjID, dt);
            if (raise == true)
            {
                ClearGridView();
                btnSubmit.Visible = false;
                ddlCategory.Visible = true;
                ddlCategory.DataSource = raiseAdjustmentController.getCategories();
                ddlCategory.DataValueField = "CategoryID";
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, "--Choose Category Name --");
                ddlItemID.Visible = false;
                txtQty = null;
                txtPending.Visible = false;
                adjustmentform.Visible = true;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Your Quantity is more than Quantity on Hand";
                adjustmentform.Visible = true;
            }
        }
        else
        {
            bool raise = raiseAdjustmentController.addDataToAdjustment(dt, name, totalPrice);
            if (raise == true)
            {
                ClearGridView();
                lblDescription.Visible = false;
                btnSubmit.Visible = false;
                ddlCategory.SelectedIndex = 0;
                ddlItemID.Visible = false;
                txtQty.Text = "";
            }
            //else
            //{
            //    lblMessage.Visible = true;
            //    lblMessage.Text= "Your Quantity is more than Quantity on Hand";
            //}
        }
    }

    DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Description");
        dt.Columns.Add("Quantity");
        dt.Columns.Add("UnitOfMeasurement");
        dt.Columns.Add("Price");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Remark");

        return dt;
    }

    private void BindGrid(int rowcount)
    {

        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataRow dr;
        raiseAdjustmentController = new RaiseAdjustmentController();
        string ItemID = ddlItemID.SelectedValue;
        dt2 = raiseAdjustmentController.addItemtoAdjustmentDetail(ItemID);
        string aa = txtQty.ToString();
        int a = Convert.ToInt32(txtQty.Text);
        decimal b = Convert.ToDecimal(dt2.Rows[0]["Price"]);
        decimal c = a * b;
        dt1.Columns.Add("ItemID");
        dt1.Columns.Add("Description");
        dt1.Columns.Add("Quantity");
        dt1.Columns.Add("UnitOfMeasurement");
        dt1.Columns.Add("Price");
        dt1.Columns.Add("Amount");
        dt1.Columns.Add("AdjustmentRemark");

        if (ViewState["CurrentData"] != null)
        {
            for (int i = 0; i < rowcount + 1; i++)
            {
                dt1 = (DataTable)ViewState["CurrentData"];
                if (dt1.Rows.Count > 0)
                {
                    dr = dt1.NewRow();
                    dr[0] = dt1.Rows[0][0].ToString();

                }
            }
            if ((ddlRemark.SelectedValue == "Free gift in offer pack") || (ddlRemark.SelectedValue == "Special gift"))
            {
                dr = dt1.NewRow();
                dr["ItemID"] = ItemID;
                dr["Description"] = dt2.Rows[0]["Description"];
                dr["Quantity"] = txtQty.Text;
                dr["UnitOfMeasurement"] = dt2.Rows[0]["UOM"];
                dr["Price"] = 0;
                dr["Amount"] = 0;
                dr["AdjustmentRemark"] = ddlRemark.SelectedValue;
                dt1.Rows.Add(dr);
                tP = tP + Convert.ToDecimal(dr["Amount"]);


            }
            else
            {
                dr = dt1.NewRow();
                dr["ItemID"] = ItemID;
                dr["Description"] = dt2.Rows[0]["Description"];
                dr["Quantity"] = txtQty.Text;
                dr["UnitOfMeasurement"] = dt2.Rows[0]["UOM"];
                dr["Price"] = dt2.Rows[0]["Price"];
                dr["Amount"] = c;
                dr["AdjustmentRemark"] = ddlRemark.SelectedValue;
                dt1.Rows.Add(dr);
                tP = tP + Convert.ToDecimal(dr["Amount"]);
            }

        }
        else
        {
            if ((ddlRemark.SelectedValue == "Free gift in offer pack") || (ddlRemark.SelectedValue == "Special gift"))
            {
                dr = dt1.NewRow();
                dr["ItemID"] = ItemID;
                dr["Description"] = dt2.Rows[0]["Description"];
                dr["Quantity"] = txtQty.Text;
                dr["UnitOfMeasurement"] = dt2.Rows[0]["UOM"];
                dr["Price"] = 0;
                dr["Amount"] = 0;
                dr["AdjustmentRemark"] = ddlRemark.SelectedValue;
                dt1.Rows.Add(dr);
                tP = tP + Convert.ToDecimal(dr["Amount"]);
            }
            else
            {
                dr = dt1.NewRow();
                dr["ItemID"] = ItemID;
                dr["Description"] = dt2.Rows[0]["Description"];
                dr["Quantity"] = txtQty.Text;
                dr["UnitOfMeasurement"] = dt2.Rows[0]["UOM"];
                dr["Price"] = dt2.Rows[0]["Price"];
                dr["Amount"] = c;
                dr["AdjustmentRemark"] = ddlRemark.SelectedValue;
                dt1.Rows.Add(dr);
                tP = tP + Convert.ToDecimal(dr["Amount"]);

            }
        }
        totalPrice += tP;
        ViewState["CurrentTotalPrice"] = totalPrice;

        if (ViewState["CurrentData"] != null)
        {
            GridView1.DataSource = (DataTable)ViewState["CurrentData"];
            GridView1.DataBind();
        }
        else
        {
            // Bind GridView with the initial data assocaited in the DataTable
            GridView1.DataSource = dt1;
            GridView1.DataBind();

        }
        // Store the DataTable in ViewState to retain the values
        ViewState["CurrentData"] = dt1;



    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["CurrentData"];
        int r = Convert.ToInt16(e.RowIndex);
        totalPrice -= Convert.ToDecimal(dt.Rows[r]["Amount"]);
        dt.Rows[r].Delete();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        ViewState["CurrentData"] = dt;
        if (dt.Rows.Count == 0)
        {
            btnSubmit.Visible = false;
        }

    }

    void ClearGridView()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        ViewState["CurrentData"] = null;
    }
}


