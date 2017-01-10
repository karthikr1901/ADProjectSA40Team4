using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;
using System.Data;
using System.Transactions;
public partial class ReceivedOrderForm : System.Web.UI.Page
{
    ReceivedOrderController receivedOrderController;
    bool approve;
    DataTable udt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Visible = false;
            lblOrdDateDec.Visible = false;
            lblOrdDate.Visible = false;
            lblSupplierDes.Visible = false;
            lblSupplierName.Visible = false;
            lblDONo.Visible = false;
            txtDeliveryOrdNo.Visible = false;
            BindDropDownList();
            btnShow.Visible = false;
        }

        //ddlOrderNo_SelectedIndexChanged(sender, e);
    }
    void BindDropDownList()
    {
        receivedOrderController = new ReceivedOrderController();

        if (receivedOrderController.getPurchaseOrderID() != null)
        {
            ddlOrderNo.DataSource = receivedOrderController.getPurchaseOrderID();
            ddlOrderNo.DataValueField = "PurchaseOrderID";
            ddlOrderNo.DataTextField = "PurchaseOrderID";
            ddlOrderNo.DataBind();
            ddlOrderNo.Items.Insert(0, "-- Select PO Number --");
            noOrder.Visible = false;
            ReceiveForm.Visible = true;
        }
        else
        {
            noOrder.Visible = true;
            ReceiveForm.Visible = false;
        }
    }
    protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrderNo.SelectedIndex != 0)
        {
            receivedOrderController = new ReceivedOrderController();
            int poID = Convert.ToInt32(ddlOrderNo.SelectedValue.ToString());
            string supName = receivedOrderController.getSupplierbyPOID(poID);
            DateTime date = receivedOrderController.getOrderDate(poID);
            string orderDate = date.ToString("dd/MM/yyyy");

            lblOrdDateDec.Visible = true;
            lblOrdDate.Visible = true;
            lblSupplierDes.Visible = true;
            lblSupplierName.Visible = true;
            lblDONo.Visible = true;
            txtDeliveryOrdNo.Visible = true;
            lblSupplierName.Text = supName;
            lblOrdDate.Text = orderDate;
            //GdvReceivedOrder.DataSource = receivedOrderController.getListDataTable(poID);
            //GdvReceivedOrder.DataBind();
            //ViewState["CurrentData"] = receivedOrderController.getListDataTable(poID);
            //GdvReceivedOrder.DataBind();
            btnSave.Visible = false;
            btnShow.Visible = false;

        }
        else
        {
            ClearGridView();
            btnSave.Visible = false;
            btnShow.Visible = false;

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (ddlOrderNo.SelectedIndex != 0)
            {

                receivedOrderController = new ReceivedOrderController();
                int POID = Convert.ToInt32(ddlOrderNo.SelectedValue.ToString());

                string name = Session["UserName"].ToString();
                string DVNo = txtDeliveryOrdNo.Text;
                DataTable dt = new DataTable();
                dt = (DataTable)Session["MyData"];
                approve = receivedOrderController.updatePurchaseOrder(POID, name, DVNo, dt);
                if (approve == true)
                {
                    Page_Load(sender, e);
                    txtDeliveryOrdNo.Text = "";

                }
            }

            BindDropDownList();

            ddlOrderNo.SelectedIndex = -1;
            ClearGridView();
            btnSave.Visible = false;

        }
        else
        {
            btnSave.Visible = true;
            lblOrdDateDec.Visible = true;
            lblOrdDate.Visible = true;
            lblSupplierDes.Visible = true;
            lblSupplierName.Visible = true;
            lblDONo.Visible = true;
            txtDeliveryOrdNo.Visible = true;
        }

    }

    void ClearGridView()
    {
        GdvReceivedOrder.DataSource = null;
        GdvReceivedOrder.DataBind();
    }
    public void loadData()
    {
        GdvReceivedOrder.DataSource = ViewState["CurrentData"];
        GdvReceivedOrder.DataBind();
    }
    protected void GdvReceivedOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GdvReceivedOrder.EditIndex = e.NewEditIndex;
        loadData();
    }
    protected void GdvReceivedOrder_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GdvReceivedOrder.EditIndex = -1;
        loadData();
    }
    protected void GdvReceivedOrder_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        udt = (DataTable)ViewState["CurrentData"];
        GridViewRow editedRow = GdvReceivedOrder.Rows[e.RowIndex];

        string itemID = GdvReceivedOrder.DataKeys[e.RowIndex].Value.ToString();
        int receivedQty = Convert.ToInt32(((TextBox)(editedRow.Cells[4].Controls[0])).Text);
        TextBox txtremark = (TextBox)editedRow.Cells[0].FindControl("txtRemark");
        string remark = txtremark.Text;

        foreach (DataRow dr in udt.Rows)
        {
            if (dr["ItemID"].ToString() == itemID)
            {
                dr["ReceivedQty"] = receivedQty;

            }
        }
        GdvReceivedOrder.EditIndex = -1;
        GdvReceivedOrder.DataSource = udt;
        GdvReceivedOrder.DataBind();
        ViewState["CurrentData"] = udt;

        Session["MyData"] = udt;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            receivedOrderController = new ReceivedOrderController();
            int poID = Convert.ToInt32(ddlOrderNo.SelectedValue.ToString());
            string supName = receivedOrderController.getSupplierbyPOID(poID);
            DateTime date = receivedOrderController.getOrderDate(poID);
            string orderDate = date.ToString("dd/MM/yyyy");

            lblOrdDateDec.Visible = true;
            lblOrdDate.Visible = true;
            lblSupplierDes.Visible = true;
            lblSupplierName.Visible = true;
            lblDONo.Visible = true;
            txtDeliveryOrdNo.Visible = true;
            btnSave.Visible = true;


            lblSupplierName.Text = supName;
            lblOrdDate.Text = orderDate;
            GdvReceivedOrder.DataSource = receivedOrderController.getListDataTable(poID);
            GdvReceivedOrder.DataBind();
            ViewState["CurrentData"] = receivedOrderController.getListDataTable(poID);
            GdvReceivedOrder.DataBind();
            btnSave.Visible = true;
        }
    }
    protected void txtDeliveryOrdNo_TextChanged(object sender, EventArgs e)
    {
        if (txtDeliveryOrdNo.Text.Equals(""))
        {
            btnShow.Visible = false;
        }
        else
            btnShow.Visible = true;
    }
}
