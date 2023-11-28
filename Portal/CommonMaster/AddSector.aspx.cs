using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Master;
//using BusinessLogicLayer.Master;
using System.Data;
using System.IO;
using BusinessLogicLayer.MasterSector;

//public partial class Master_AddSector : System.Web.UI.Page
//{
//    MasterDetails objentity = null;
//    MasterBusinessLayer objService = new MasterBusinessLayer();
//    string output = null;
//    protected void Page_Load(object sender, EventArgs e)
//    {

//        if (!IsPostBack)
//        {
//            bindcountry();
//        }

//    }
//    protected void btnSubmit_Click(object sender, EventArgs e)
//    {
//        try
//        {
//            objentity = new MasterDetails();
//            if (ViewState["id"] != null)
//                objentity.strAction = "U";
//            else
//                objentity.strAction = "A";
//            objentity.SectorCode = Convert.ToInt32(txtSectorCode.Text.Trim());
//            objentity.strSectorName = txtSectorName.Text.Trim();
//            objentity.strSectorDescription = txtDescription.Text.Trim();
//            objentity.SectorPriority = (chkSector.Checked) ? 1 : 0;
//            objentity.intPolicyReference = Convert.ToInt32(ddlPolicyReferences.SelectedValue);
//            objentity.Policyreference = Convert.ToDateTime(txtPolicydate.Text.ToString());
//            output = objService.SectorData(objentity);
//            if (output == "1")
//            {
//                if (ViewState["id"] != null)
//                    Response.Write("<script>alert('Successfully Updated');</script>");
//                else
//                    Response.Write("<script>alert('Successfully added');</script>");

                
//            }

//        }
//        catch (Exception)
//        {
//            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ADDED", "alert('invalid found')", true);
//        }

//    }
//    protected void btnCancel_Click(object sender, EventArgs e)
//    {

//    }

//    public void bindcountry()
//    {
//        try
//        {
//            objentity = new MasterDetails();
//            objentity.strAction = "S";
//            DataSet ds = objService.BindDDl(objentity);
//            ddlPolicyReferences.DataSource = ds.Tables[0];
//            ddlPolicyReferences.DataTextField = "VCH_POLICY_NAME";
//            ddlPolicyReferences.DataValueField = "INT_POLICY_ID";
//            ddlPolicyReferences.DataBind();


//        }
//        catch (Exception)
//        {
    
//        }
//    }
//}

public partial class Master_AddSector : System.Web.UI.Page
{

    string str_Retvalue = "";
    int retval = 0;
     protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            btnSubmit.Text = "Submit";
            bindDdl();
            if (Request.QueryString["id"] != null)
            {
                editData(Convert.ToInt32(Request.QueryString["id"]));
            }
        }

    }
     public void editData(int id)
     {
         try
         {
             MasterSectorBusinesslayer objService = new MasterSectorBusinesslayer();
             EntityLayer.Mastersector.MasterSectorDetails objdata= new EntityLayer.Mastersector.MasterSectorDetails();
             objdata = objService.EditData(id);
             if (objdata != null)
             {
                 txtDescription.Text = objdata.strSectorDescription;
                 txtPolicydate.Text = objdata.Policyreference.ToString("dd/MMM/yyyy");
                 txtSectorCode.Text = objdata.SectorCode.ToString ();
                 txtSectorName.Text = objdata.strSectorName;
                 ddlPolicyReferences.SelectedValue = objdata.intPolicyReference.ToString ();
                 if (objdata.SectorPriority == 1)
                 {
                     chkSector.Checked = true;
                 }
                 else
                 {
                     chkSector.Checked = false;
                 }
                 ViewState["id"] = id;
                 btnSubmit.Text = "Update";
             }
         }
         catch (Exception ex)
         {
             
             throw;
         }
     }
     protected void btnSubmit_Click(object sender, EventArgs e)
     {
         MasterSectorBusinesslayer objService = new MasterSectorBusinesslayer();
         EntityLayer.Mastersector.MasterSectorDetails objProperty=new EntityLayer.Mastersector.MasterSectorDetails();
        try
        {
            if (btnSubmit.Text == "Update")
            {
                objProperty.strAction = "U";
                objProperty.SectorId = Convert.ToInt32(ViewState["id"].ToString());
            }
            else
            {
                objProperty.strAction = "A";
            }
            objProperty.SectorCode = Convert.ToInt32(txtSectorCode.Text.Trim());
            objProperty.strSectorName = txtSectorName.Text.Trim();
            objProperty.strSectorDescription = txtDescription.Text.Trim();
            objProperty.SectorPriority = (chkSector.Checked) ? 1 : 0;
            objProperty.intPolicyReference = Convert.ToInt32(ddlPolicyReferences.SelectedValue);
            objProperty.Policyreference = Convert.ToDateTime(txtPolicydate.Text.ToString());
            str_Retvalue = objService.SectorData(objProperty);

            if (str_Retvalue == "1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Data Save successfully!')",true);
                reset();
            }
            if (str_Retvalue == "2")
                
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Data update successfully!');window.location='ViewSector.aspx';</script>'");
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ADDED", "alert('invalid found')", true);
        }

    
     }
    public void bindDdl()
     {
         MasterSectorBusinesslayer objService = new MasterSectorBusinesslayer();
         //EntityLayer.Mastersector.MasterDdl objdropdown = new EntityLayer.Mastersector.MasterDdl();
        List<EntityLayer.Mastersector.MasterDdl> objProperty=new List<EntityLayer.Mastersector.MasterDdl>();
        EntityLayer.Mastersector.MasterDdl objinput=new EntityLayer.Mastersector.MasterDdl();
         try
         {
             //objMaster = new MasterDetails();
             //objMaster.strAction = "S";
             //DataSet ds = objService.BindDDl(objMaster);
             //ddlPolicyReferences.DataSource = ds.Tables[0];
             //ddlPolicyReferences.DataTextField = "VCH_POLICY_NAME";
             //ddlPolicyReferences.DataValueField = "INT_POLICY_ID";
             //ddlPolicyReferences.DataBind();
             objinput.strAction="S";
             objProperty = objService.BindDDl(objinput).ToList() ;

             ddlPolicyReferences.DataTextField = "strpolicyname";
             ddlPolicyReferences.DataValueField = "intPolicyId";
             ddlPolicyReferences.DataSource = objProperty;
             ddlPolicyReferences.DataBind();
             ddlPolicyReferences.Items.Insert(0, new ListItem("-Select-", "0"));

         }
         catch (Exception)
         {

         }
     }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Update")
        {
            Response.Redirect("ViewSector.aspx");
           
        }
        else
        {
            reset();
        }
    }
    public void reset()
    {
        txtSectorCode.Text = "";
        txtSectorName.Text = "";
        txtDescription.Text = "";
        txtPolicydate.Text = "";
        ddlPolicyReferences.SelectedValue = "";
    }

}