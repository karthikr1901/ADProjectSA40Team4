using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADTeam4EF;

public partial class DRCollectionPt : System.Web.UI.Page
{
    ADTeam4EF.ChangeCollectionPt ccpt = new ADTeam4EF.ChangeCollectionPt();
    string deptid = ""; int cptid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DepartmentID"] != null)
        {
            deptid = Convert.ToString(Session["DepartmentID"]);
            if (!IsPostBack)
            {
                LoadLabel();
                LoadDrop();
                Button2.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", true);
        }
      
    }

    protected void LoadLabel()
    {
         List<CollectionPoint> lcp = new List<CollectionPoint>();
         lcp = ccpt.CurrentCollectionPt(deptid);
         currentCollectionPoint.InnerText ="Current Collection Point: " + Convert.ToString(lcp[0].Place) + " " + Convert.ToString(lcp[0].Time);
         //lblcollpt.Text = Convert.ToString(lcp[0].Place) + " " + Convert.ToString(lcp[0].Time);
    }

    protected void LoadDrop()
    {
        DropDownList2.DataSource = ccpt.DropCollectionPt();
        DropDownList2.DataTextField = "Place";
        DropDownList2.DataValueField = "CollectionPointID";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "--Select Collection Point--");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedIndex != 0)
        {
            cptid = Convert.ToInt32(DropDownList2.SelectedValue);
            ccpt.UpdateCollectionPt(deptid, cptid);
            LoadLabel();
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (DropDownList2.SelectedIndex != 0)
        {
            cptid = Convert.ToInt32(DropDownList2.SelectedValue);
            lblTime.Text = "Collection Time: " + Convert.ToString(ccpt.DisplayTime(cptid));
            Button2.Visible = true;
        }
        else
        {
            lblTime.Text = "";
            Button2.Visible = false;
        }
    }
}