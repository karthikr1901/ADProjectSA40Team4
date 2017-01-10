using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcessRequisition : System.Web.UI.Page
{
    ADTeam4EF.ProcessReqSC prsc = new ADTeam4EF.ProcessReqSC();
    int eid1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmployeeID"] != null)
        {
            eid1 = Convert.ToInt32(Session["EmployeeID"]);
            if (!IsPostBack)
            {
                int av = 1;
                GridView1.DataSource = prsc.GridDisp(av);
                GridView1.DataBind();
                int asd = prsc.GetCount();
                if (asd <= 0)
                {
                    btnAllocate.Visible = false;
                    noEdit.Visible = true;
                }
                else
                {
                    noEdit.Visible = false;
                }
                   
            }
        }
        else
            Response.Redirect("~/Login.aspx");
    }
    public static void displayGridView(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (i > 1)
                {
                    if (row.Cells[1].Text == previousRow.Cells[1].Text)
                    {
                        if (row.Cells[i].Text == previousRow.Cells[i].Text)
                        {

                            {
                                row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                                       previousRow.Cells[i].RowSpan + 1;
                                previousRow.Cells[i].Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {

                        {
                            row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                                   previousRow.Cells[i].RowSpan + 1;
                            previousRow.Cells[i].Visible = false;
                        }
                    }
                }
            }
        }
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        displayGridView(GridView1);
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        bool getoutstand = prsc.GetOutstandingAllocate();
        if (getoutstand == false)
        {
            int av = 2;
            GridView1.DataSource = prsc.ProcessReqGrid(av);
            GridView1.DataBind();
            btnAllocate.Enabled = false;
        }

    }
}