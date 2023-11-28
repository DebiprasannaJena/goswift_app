using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Service;
using BusinessLogicLayer.Service;

public partial class ServiceMaster_AddServiceMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                BindDept();
                if (Request.QueryString["Sid"] != "")
                {
                    EditData();
                }
               
              
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Service");
            }
        }
    }
    private void BindDept()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.BindDepartment("DP").ToList();
        ddlDepartment.DataSource = objServicelist;
        ddlDepartment.DataTextField = "strdeptname";
        ddlDepartment.DataValueField = "Deptid";
        ddlDepartment.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlDepartment.Items.Insert(0, list);

    }
    private void EditData()
    {
        ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
        ServiceDetails objservice = new ServiceDetails();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        try
        {
            objservice.strAction = "E";
            objservice.intServiceId = Convert.ToInt32(Request.QueryString["Sid"]);
            objServicelist = objServiceDet.ViewServiceMasterDet(objservice);
            if (objServicelist.Count > 0)
            {
                txtServiceName.Text = objServicelist[0].strServiceName;
                txtORTPSTimeline.Text = objServicelist[0].strExcalationDays.ToString();
                ddlDepartment.SelectedValue = objServicelist[0].intdeptid.ToString();
                ddlServiceCategory.SelectedValue=objServicelist[0].intServiceCategory.ToString();
                txtaliasname.Text=objServicelist[0].strServiceAliasName.ToString();
                txtDescription.Text=objServicelist[0].strRemark.ToString();
                rdbPayment.SelectedValue=objServicelist[0].intPaymentStatus.ToString();
                txtPayment.Text = objServicelist[0].strPaymentAmount.ToString();
                ddlServiceType.SelectedValue = objServicelist[0].Int_ServiceType.ToString();
                txtServiceURL.Text = objServicelist[0].Str_ExtrnalServiceUrl.ToString();
                hdnDeptId.Value= objServicelist[0].intdeptid.ToString();
                hdnServiceName.Value= objServicelist[0].strServiceName;
                rdbExternal.SelectedValue = objServicelist[0].intExternalType.ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
        ServiceDetails objservice = new ServiceDetails();
        string rawURL = "";
        try
        {
            if (Request.QueryString["Sid"] != "" && Request.QueryString["Sid"] != null)
            {
                objservice.strAction = "U";
                rawURL = "ViewServiceMaster.aspx";
                objservice.intServiceId = Convert.ToInt32(Request.QueryString["Sid"]);
                objservice.intdeptid = Convert.ToInt32(hdnDeptId.Value);
                objservice.strServiceName = Convert.ToString(hdnServiceName.Value);
            }
            else { objservice.strAction = "A";
             rawURL = Request.RawUrl;
                objservice.intdeptid = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
                objservice.strServiceName = txtServiceName.Text;
            }
            objservice.strExcalationDays = txtORTPSTimeline.Text;
            objservice.Int_ServiceType = Convert.ToInt32(ddlServiceType.SelectedValue);
            objservice.intServiceCategory = Convert.ToInt32(ddlServiceCategory.SelectedValue);
            objservice.strServiceAliasName =txtaliasname.Text;
            objservice.intPaymentStatus = Convert.ToInt32(rdbPayment.SelectedValue);
            objservice.intExternalType = Convert.ToInt32(rdbExternal.SelectedValue);

            if (rdbPayment.SelectedValue == "0")
            { 
                objservice.strPaymentAmount = txtPayment.Text;
            }
            else
            {
                objservice.strPaymentAmount = "0.00";
            }
               
            if (ddlServiceType.SelectedValue == "1")
            {
                objservice.Str_ExtrnalServiceUrl = txtServiceURL.Text;
            }              
            else
            {
                if(objservice.intExternalType == 1)
                {
                    objservice.Str_ExtrnalServiceUrl = txtServiceURL.Text;
                }
                else
                {
                    objservice.Str_ExtrnalServiceUrl = "";
                }                
            }                   
            objservice.strRemark = txtDescription.Text;
            objservice.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            string strRetVal = objServiceDet.AddServiceMasterDet(objservice);         
            string strShowMsg = Messages.ShowMessage(strRetVal);
            string ff = "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false);

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Sid"] != "" && Request.QueryString["Sid"] != null)
        {
            Response.Redirect("AddServiceMaster.aspx?Sid=" + Request.QueryString["Sid"].ToString());
        }
        else
        {
            Response.Redirect("AddServiceMaster.aspx");
        }
    }
}