using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
using System.Data;

public partial class ApproveAdjustmentVoucher : System.Web.UI.Page
{
    ApproveAdjustmentController approveAdjustmentController;
    //ADTeam4EF.ADProjectSA40Team4Entities ctx = new ADTeam4EF.ADProjectSA40Team4Entities();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        btnApprove.Visible = false;
       
        if (!IsPostBack)
        {
            BindDropDownList();
        } 
    }
    private void BindGrid()
    {

        DataTable dt = new DataTable();
        approveAdjustmentController = new ApproveAdjustmentController();
        int AdjustID = Convert.ToInt32(ddlVoucherNo.SelectedValue.ToString());
        dt = (DataTable)approveAdjustmentController.getAdjustmentList(AdjustID);


        DataTable dt1 = new DataTable();
        dt1.Columns.Add("Description");
        dt1.Columns.Add("Quantity");
        dt1.Columns.Add("UnitOfMeasurement");
        dt1.Columns.Add("Price");
        dt1.Columns.Add("Amount");
        dt1.Columns.Add("AdjustmentRemark");
        int i = 0;

        foreach (DataRow r in dt.Rows)
        {
            if (((dt.Rows[i][5].ToString()) == "Free gift in offer pack") || ((dt.Rows[i][5].ToString()) == "Special gift"))
            {
                dt1.Rows.Add(dt.Rows[i][0].ToString(), Convert.ToInt16(dt.Rows[i][1].ToString()), dt.Rows[i][2].ToString(), Convert.ToDecimal(0), Convert.ToDecimal(0), dt.Rows[i][5].ToString());
            }
            else
            {
                dt1.Rows.Add(dt.Rows[i][0].ToString(), Convert.ToInt16(dt.Rows[i][1].ToString()), dt.Rows[i][2].ToString(), Convert.ToDecimal(dt.Rows[i][3].ToString()), Convert.ToDecimal(dt.Rows[i][4].ToString()), dt.Rows[i][5].ToString());
            }
            i++;

        }

        gdvAdjustmentDetail.DataSource = dt1;
        gdvAdjustmentDetail.DataBind();


    }
    void BindDropDownList()
    {
        approveAdjustmentController = new ApproveAdjustmentController();

        //ddlVoucherNo.Items.Add(new ListItem("--Select One--", "0", true));
        string username = string.Empty;
        if (Session["UserName"] != null)
        {
             username = Session["UserName"].ToString();

        }
        else
        {
            Response.Redirect("~/Login.aspx", true);
        }
        if (approveAdjustmentController.getVoucherNumber(username) != null)
        {
            ddlVoucherNo.DataSource = approveAdjustmentController.getVoucherNumber(username);
            ddlVoucherNo.DataValueField = "AdjustmentID";
            ddlVoucherNo.DataTextField = "AdjustmentID";
            ddlVoucherNo.DataBind();
            voucherbox.Visible = true;
            ddlVoucherNo.Items.Insert(0, "--Select One--");
            noAdjustment.Visible = false;
        }
        else
        {
            voucherbox.Visible = false;
            noAdjustment.Visible = true;
        }
        
    }

   protected void ddlVoucherNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVoucherNo.SelectedIndex != 0)
        {
            approveAdjustmentController = new ApproveAdjustmentController();
            int AdjustID = Convert.ToInt32(ddlVoucherNo.SelectedValue.ToString());
            btnApprove.Visible = true;
            BindGrid();
        }
        else 
        {
            ClearGridView();
        }

   }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (ddlVoucherNo.SelectedIndex!=0)
        {
            approveAdjustmentController = new ApproveAdjustmentController();
            int AdjustID = Convert.ToInt32(ddlVoucherNo.SelectedValue.ToString());
            bool approve;
            string name= Session["userName"].ToString();

            approve = approveAdjustmentController.approvedAdjustmentList(AdjustID, name);
            if (approve == true)
            {
                Page_Load(sender, e);
                
            }
        }

        BindDropDownList();

        ddlVoucherNo.SelectedIndex = -1;
        ClearGridView();
    }


    void ClearGridView()
    {
        gdvAdjustmentDetail.DataSource = null;
        gdvAdjustmentDetail.DataBind();
    }

    }
