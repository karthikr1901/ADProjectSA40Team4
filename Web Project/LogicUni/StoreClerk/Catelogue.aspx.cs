using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Catelogue : System.Web.UI.Page
{
    ADTeam4EF.CatalogueController cateController = new ADTeam4EF.CatalogueController();
    string itemNo = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //loadData();
        if (!IsPostBack)
        {
            if (Session["role"] != null)
            {
                if (!Session["role"].Equals("Store Clerk"))
                {
                    Response.Redirect("~/Login.aspx", true);
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx", true);
            }

            ddlCategory.DataSource = cateController.populateCategory();
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataBind();
            //ddlCategory.Items.Insert(0, "--Choose Category Name --");
            loadData();
            //DataTable dt = (DataTable)ViewState["CurrentData"];
        }
        else
        {
          //  CatagoryGridView.DataSource = (DataTable)ViewState["CurrentData"];
          //  CatagoryGridView.DataBind();
        }
            
    }

    protected void btnAdd_Click(object sender, EventArgs e)
        {
        //TO GRIDVIEW FROM DROPDOWN 
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();

        string category = ddlCategory.SelectedItem.ToString();
        string description = txtDescription.Text;
        int reorderLevel = Convert.ToInt32(txtReorderLevel.Text);
        int reorderQty = Convert.ToInt32(txtReorderQty.Text);
        string unitOfMeasurement = txtUOM.Text;
        int balanceQty = Convert.ToInt32(txtBalance.Text);

        string prefix = category.Substring(0, 1);
        string s_lastNo = cateController.getLastRow(category);
        int number = Convert.ToInt32(s_lastNo.Substring(1, 3));
        number++;
        if (number < 10)
        {
            itemNo = string.Concat(prefix, 0,0, number);
        }
        else if (number < 100)
        {
            itemNo = string.Concat(prefix, 0, number);
        }
        else
        {
            itemNo = string.Concat(prefix, number);
        }

        //same with database column name
        dt1.Columns.Add("ItemID");
        dt1.Columns.Add("CategoryName");
        dt1.Columns.Add("Description");
        dt1.Columns.Add("ReorderLevel");
        dt1.Columns.Add("ReorderQuantity");
        dt1.Columns.Add("UnitOfMeasurement");
        dt1.Columns.Add("Balance");

        DataRow dr = dt1.NewRow();
        dr["ItemID"] = itemNo;
        dr["CategoryName"] = category;
        dr["Description"] = description;
        dr["ReorderLevel"] = reorderLevel;
        dr["ReorderQuantity"] = reorderQty;
        dr["UnitOfMeasurement"] = unitOfMeasurement;
        dr["Balance"] = balanceQty;
        dt1.Rows.Add(dr);
        CatagoryGridView.DataSource = dt1;
        CatagoryGridView.DataBind();

        if (ViewState["CurrentData"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentData"];
            int count = dt.Rows.Count;
            BindGrid(count);
        }
        else
        {
            BindGrid(1);
        }


        txtDescription.Text = string.Empty;
        txtReorderLevel.Text = string.Empty;
        txtReorderQty.Text = string.Empty;
        txtUOM.Text = string.Empty;
        txtBalance.Text = string.Empty;

        if (cateController.insertAllCategory(itemNo, category, description, reorderLevel, reorderQty, unitOfMeasurement, balanceQty))
        {
   //           Response.Write("<script>alert('Ok')</script>");
            lblCatelogueStatus.InnerHtml = "<span class='glyphicon glyphicon-ok-circle'></span>&nbsp;" + "Successfully added!";
            loadData();
        }
        else
        {
            btnAdd.CssClass = "btn btn-danger btn-block";
            lblCatelogueStatus.InnerHtml = "<span class='glyphicon glyphicon-warning-sign'></span>&nbsp;" + "Adding Failed! Please try again later!";
            //Response.Write("<script>alert('no')</script>");
            loadData();
        }

    }
    private void BindGrid(int rowcount)
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new System.Data.DataColumn("ItemID", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("CategoryName", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Description", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("ReorderLevel", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("ReorderQuantity", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("UnitOfMeasurement", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Balance", typeof(String)));

        if (ViewState["CurrentData"] != null)
        {
            for (int i = 0; i < rowcount + 1; i++)
            {
                dt = (DataTable)ViewState["CurrentData"];
                if (dt.Rows.Count > 0)
                {
                    dr = dt.NewRow();
                    dr[0] = dt.Rows[0][0].ToString();

                }
            }
            dr = dt.NewRow();
            dr[1] = ddlCategory.SelectedValue;
            dr[2] = txtDescription.Text;
            dr[3] = txtReorderLevel.Text;
            dr[4] = txtReorderQty.Text;
            dr[5] = txtUOM.Text;
            dr[6] = txtBalance.Text;
            dt.Rows.Add(dr);

        }
        else
        {
            dr = dt.NewRow();
            dr[1] = ddlCategory.Text;
            dr[2] = txtDescription.Text;
            dr[3] = txtReorderLevel.Text;
            dr[4] = txtReorderQty.Text;
            dr[5] = txtUOM.Text;
            dr[6] = txtBalance.Text;
            dt.Rows.Add(dr);

        }

        // If ViewState has a data then use the value as the DataSource
        if (ViewState["CurrentData"] != null)
        {
            CatagoryGridView.DataSource = (DataTable)ViewState["CurrentData"];
            CatagoryGridView.DataBind();
        }
        else
        {
            // Bind GridView with the initial data assocaited in the DataTable
            CatagoryGridView.DataSource = dt;
            CatagoryGridView.DataBind();

        }
        // Store the DataTable in ViewState to retain the values
        ViewState["CurrentData"] = dt;

    }


    protected void CatagoryGridView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        //grid view column name
        dt.Columns.Add("Item Number");
        dt.Columns.Add("Category");
        dt.Columns.Add("Description");
        dt.Columns.Add("Reorder Level");
        dt.Columns.Add("Reorder Qty");
        dt.Columns.Add("Unit of Measurement");
        dt.Columns.Add("Balance");
        return dt;
    }
    protected void CatagoryGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CatagoryGridView.EditIndex = e.NewEditIndex;
        bindData();
       
    }
    protected void CatagoryGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["CurrentData"];
        int r = Convert.ToInt16(e.RowIndex);
        string itNo = dt.Rows[r][0].ToString();
        cateController.deleteItem(itNo);
        dt.Rows[r].Delete();
        CatagoryGridView.DataSource = dt;
        CatagoryGridView.DataBind();
        ViewState["CurrentData"] = dt;
    }
    public void loadData()
    {
        CatagoryGridView.DataSource = cateController.getAllCategory();
        CatagoryGridView.DataBind();
        ViewState["CurrentData"] = cateController.getAllCategory();
    }
    protected void CatagoryGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        CatagoryGridView.EditIndex = -1;
        loadData();
    }
    protected void CatagoryGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {          
        GridViewRow editedRow = CatagoryGridView.Rows[e.RowIndex];
        itemNo = CatagoryGridView.DataKeys[e.RowIndex].Value.ToString();
        string eCat = CatagoryGridView.DataKeys[e.RowIndex][1].ToString();
        string eDescription = CatagoryGridView.DataKeys[e.RowIndex][2].ToString();
        string eRLvl = ((TextBox)(editedRow.Cells[3].Controls[0])).Text; 
        string eRQty = ((TextBox)(editedRow.Cells[4].Controls[0])).Text;
        string eUOM = ((TextBox)(editedRow.Cells[5].Controls[0])).Text;
        string eBal = ((TextBox)(editedRow.Cells[6].Controls[0])).Text;

        int newRLevel = Convert.ToInt32(eRLvl);
        int newRQty = Convert.ToInt32(eRQty);
        int newBal = Convert.ToInt32(eBal);

        if (cateController.checkCategory(eCat))
        {
            if (cateController.updateItem(itemNo, eCat, eDescription, newRLevel, newRQty, eUOM, newBal))
            {
                //Response.Write("<script>alert('OK')</script>");
                loadData();
            }
            else
            {
                //Response.Write("<script>alert('No')</script>");
            }
            CatagoryGridView.EditIndex = -1;
            bindData();
        }
        else
        {
            Response.Write("<script>alert('Please choose the existing category only!')</script>");
        }
    }
    public void bindData()
    {
        CatagoryGridView.DataSource = ViewState["CurrentData"];
        CatagoryGridView.DataBind();
    }
    protected void CatagoryGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CatagoryGridView.DataSource = ViewState["CurrentData"];
        CatagoryGridView.PageIndex = e.NewPageIndex;
        CatagoryGridView.DataBind();
    }
}