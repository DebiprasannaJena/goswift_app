using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
using EntityLayer.Service;
using System.IO;
using BusinessLogicLayer.Service;
using System.Collections.Specialized;

public partial class Portal_Service_AddChallanDtls : System.Web.UI.Page
{
    #region Variables
    string FilePath = "";
    ServiceDetails objService1 = new ServiceDetails();
    public string strManageRight = "";
    public int intLevelDetailId;
    //string strUserId, strPassword, strRandomPassword;
    DataTable objdt;
    #endregion
    decimal sum = 0;
    DataTable dtable;
    DataSet ds = new DataSet();
    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindDept();
                AutoselectDept();
                CreateDataTableRWM();
                BindApplicationNo();
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
        ddldept.DataSource = objServicelist;
        ddldept.DataTextField = "strdeptname";
        ddldept.DataValueField = "Deptid";
        ddldept.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddldept.Items.Insert(0, list);

    }
    private void AutoselectDept()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.FindUserDepartment("FD", Session["UserId"].ToString()).ToList();
        if (objServicelist.Count > 0)
        {
            if (objServicelist[0].Deptid.ToString() != "0")
            {
                ddldept.SelectedValue = objServicelist[0].Deptid.ToString();
                ddldept.Enabled = false;

            }
        }
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindApplicationNo();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ApplicationNo");
        }
    }
    private void BindApplicationNo()
    {
        try
        {
            ServiceBusinessLayer objService = new ServiceBusinessLayer();
            List<ServiceDetails> objServicelist = new List<ServiceDetails>();
            objServicelist = objService.BindService("DM", int.Parse(ddldept.SelectedValue)).ToList();
            ddlApplicationNo.DataSource = objServicelist;
            ddlApplicationNo.DataTextField = "strServiceName";
            ddlApplicationNo.DataValueField = "intServiceId";
            ddlApplicationNo.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlApplicationNo.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ApplicationNo");
        }

    }
    protected void ddlApplicationNo_SelectedIndexChanged(object sender, EventArgs e)
    {
         try
        {
            ServiceBusinessLayer objService = new ServiceBusinessLayer();
            List<ServiceDetails> objServicelist = new List<ServiceDetails>();
            objServicelist = objService.BindEmployeeName("DK",Convert.ToString(ddlApplicationNo.SelectedItem.Text)).ToList();
           
            if (objServicelist.Count > 0)
            {
                decimal penalty = 0;
                decimal overdue = 0;
                txtAmount.Text = objServicelist[0].strfullname.Split('_')[0];
                txtDemandAmount.Text = objServicelist[0].strfullname.Split('_')[1];
                if (txtPenaltyAmount.Text != "")
                {
                     penalty = Convert.ToDecimal(txtPenaltyAmount.Text);
                }
                else
                {
                     penalty = 0;
                }
                if (txtPaymentOverdue.Text != "")
                {
                     overdue = Convert.ToDecimal(txtPaymentOverdue.Text);
                }
                else
                {
                     overdue = 0;
                }
                decimal total = (Convert.ToDecimal(txtAmount.Text) + Convert.ToDecimal(txtDemandAmount.Text) + penalty) - (overdue);
                txtTotalAmt.Text = total.ToString();
                objService1.str_ApplicationNo = ddlApplicationNo.SelectedItem.Text;
                objServicelist = objService.GetAllChalanDetails(objService1).ToList();
                if (objServicelist.Count > 0)
                {
                    GrvChallan.DataSource = objServicelist;
                    GrvChallan.DataBind();
                    DataTable dt2 = CreateDataTableRWM();
                    for (int i = 0; i <= GrvChallan.Rows.Count - 1; i++)
                    {
                        HiddenField hdpid2 = (HiddenField)GrvChallan.Rows[i].FindControl("hdpid2");
                        HiddenField hdnTransMode = (HiddenField)GrvChallan.Rows[i].FindControl("hdnTransMode");
                        DataRow dr = dt2.NewRow();
                        dr["intProId2"] = hdpid2.Value;
                        dr["vchChallanNo"] = GrvChallan.Rows[i].Cells[1].Text;
                        dr["vchTranscationNo"] = GrvChallan.Rows[i].Cells[2].Text;
                        dr["vchAmount"] = GrvChallan.Rows[i].Cells[3].Text;
                        dr["vchChallanFile"] = GrvChallan.Rows[i].Cells[4].Text;
                        dr["vchChallanDate"] = GrvChallan.Rows[i].Cells[5].Text;
                        dr["TransMode"] = hdnTransMode.Value;
                        dt2.Rows.Add(dr);
                        dt2.AcceptChanges();
                    }
                    ViewState["ChallanTB"] = dt2;
                }

            }
            objServicelist = objService.BindEmployeeName("DJ", Convert.ToString(ddlApplicationNo.SelectedItem.Text)).ToList();
            if (objServicelist.Count > 0)
            {
                txtPaymentOverdue.Text = objServicelist[0].strfullname;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ApplicationNo");
        }
    }
    private DataTable CreateDataTableRWM()
    {
        DataTable dtTrans = new DataTable();
        DataColumn intProId2 = new DataColumn("intProId2");
        intProId2.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(intProId2);

        DataColumn vchChallanNo = new DataColumn("vchChallanNo");
        vchChallanNo.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(vchChallanNo);

        DataColumn vchTranscationNo = new DataColumn("vchTranscationNo");
        vchTranscationNo.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(vchTranscationNo);

        DataColumn vchAmount = new DataColumn("vchAmount");
        vchAmount.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(vchAmount);

        DataColumn vchChallanFile = new DataColumn("vchChallanFile");
        vchChallanFile.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(vchChallanFile);

        DataColumn vchChallanDate = new DataColumn("vchChallanDate");
        vchChallanDate.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(vchChallanDate);

        DataColumn TransMode = new DataColumn("TransMode");
        TransMode.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(TransMode);

        ViewState["ChallanTB"] = dtTrans;
        return dtTrans;
    }
    protected void btnAddMoreRWM_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt1 = new DataTable();
            dt1 = ViewState["ChallanTB"] as DataTable;
            DataTable dts = new DataTable();
            DataRow dr = null;
            dt1 = (ViewState["ChallanTB"] as DataTable);
            int flag1 = 0;
           
            //for (int z = 0; z < dt1.Rows.Count; z++)
            //{
            //    //if (dt1.Rows[z]["vchChallanNo"].ToString() == txtChallanNo.Text.ToString())
            //    //{
            //    //    flag1 = 1;
            //    //    break;
            //    //}
            //    sum = sum + Convert.ToDecimal(dt1.Rows[z]["vchAmount"].ToString()) + Convert.ToDecimal(txtChallanAmount.Text.Trim());
            //    decimal tlAmt = Convert.ToDecimal(txtTotalAmt.Text.Trim());
            //    if (sum < tlAmt)
            //    {
            //        flag1 = 2;
            //        break;
            //    }
            //}
          
            if (flag1 == 0)
            {
                IsFileValidChallan(fldChallan);
                dr = dt1.NewRow();
                dr["intProId2"] = (dt1.Rows.Count + 1).ToString();
                dr["vchChallanNo"] = txtChallanNo.Text.TrimEnd();
                dr["vchTranscationNo"] = txtBankTransctionNo.Text.TrimEnd();
                dr["vchAmount"] = txtChallanAmount.Text.TrimEnd();
                dr["vchChallanFile"] = fldChallan.FileName;
                dr["vchChallanDate"] = txtChallanDate.Text.Trim();
                dr["TransMode"] = "1";
                dt1.Rows.Add(dr);
                dr = null;
                    GrvChallan.DataSource = dt1;
                    GrvChallan.DataBind();
                    dt1.TableName = "ChallanTB";
                    ViewState["ChallanTB"] = dt1;
                    ClearValue2();
               
            }
           
            //if (flag1 == 1)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Challan number should not be duplicate.', '" + Messages.TitleOfProject + "'); </script>", false);
            //}
            //if (flag1 == 2)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Challan Amount should not be less than total amount.', '" + Messages.TitleOfProject + "'); </script>", false);
            //}
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Challan Add More");
        }
    }
    private bool IsFileValidChallan(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("../ChallanFiles"));

        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(fldChallan.FileName) != ".pdf") && (Path.GetExtension(fldChallan.FileName) != ".png") && (Path.GetExtension(fldChallan.FileName) != ".jpg") && (Path.GetExtension(fldChallan.FileName) != ".jpeg"))
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }
                int fileSize = fldChallan.PostedFile.ContentLength;
               
                    //FilePath = Convert.ToInt32(Session["UserId"]) + string.Format("{0:yyyyMMddhhmmss}" + "ChallanFile" + Path.GetExtension(fldChallan.FileName), DateTime.Now);
                    FilePath = fldChallan.FileName;
               
                if (!string.IsNullOrEmpty(fldChallan.FileName))
                {
                    if (dir.Exists)
                    {
                        fldChallan.SaveAs(Server.MapPath("../ChallanFiles/" + FilePath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("../ChallanFiles"));
                        fldChallan.SaveAs(Server.MapPath("../ChallanFiles/" + FilePath));
                    }

                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private void ClearValue2()
    {
        txtChallanNo.Text = string.Empty;
        txtBankTransctionNo.Text = string.Empty;
        txtChallanAmount.Text = string.Empty;
        txtChallanDate.Text = string.Empty;
    }
    protected void imgbtnDelete2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt2 = new DataTable();
            dt2 = ViewState["ChallanTB"] as DataTable;
            ImageButton imgbtn = (ImageButton)sender;
            HiddenField hdnid = (HiddenField)imgbtn.FindControl("hdpid2");
            DataRow[] dr1 = null;
            dr1 = dt2.Select("intProId2='" + hdnid.Value + "'");
            for (int i = 0; i < dr1.Length; i++)
            {
                dr1[i].Delete();
            }
            dt2.AcceptChanges();
            GrvChallan.DataSource = dt2;
            GrvChallan.DataBind();
            ViewState["ChallanTB"] = dt2;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Challan Delete");
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        objdt = (DataTable)ViewState["ChallanTB"];
        objdt.TableName = "ChallanTB";
        string xmlData = GetSTRXMLResult(objdt);
        string vchOrderNo = "ES" + ddlApplicationNo.SelectedItem.Text + "-" + DateTime.Now.ToString("ddmmyyhhmmss");
        decimal penalty = 0;
        decimal overdue = 0;
        decimal totlamt = 0;
        decimal demandAmt = 0;
        if (txtDemandAmount.Text != "")
        {
            demandAmt = Convert.ToDecimal(txtDemandAmount.Text);
        }
        else
        {
            demandAmt = 0;
        }
        if (txtAmount.Text != "")
        {
            totlamt = Convert.ToDecimal(txtAmount.Text);
        }
        else
        {
            totlamt = 0;
        }
        if (txtPenaltyAmount.Text != "")
        {
            penalty = Convert.ToDecimal(txtPenaltyAmount.Text);
        }
        else
        {
            penalty = 0;
        }
        if (txtPaymentOverdue.Text != "")
        {
            overdue = Convert.ToDecimal(txtPaymentOverdue.Text);
        }
        else
        {
            overdue = 0;
        }
        decimal total = (totlamt + demandAmt + penalty) - (overdue);
        string overDueAmt = "0";
        if (total < 0)
        {
            overDueAmt = total.ToString();
        }
        else
        {
            overDueAmt = "0";
        }

        string AmtPaid = total.ToString();
        string strRetVal = objService.AddServiceChallan("A", xmlData, ddlApplicationNo.SelectedItem.Text, Convert.ToInt32(Session["UserId"].ToString()), vchOrderNo, AmtPaid, overDueAmt);
        if (strRetVal == "1")
        {
            string rawURL = Request.RawUrl;
            string strShowMsg = "Data Save Successfully!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false);
        }
    }
    public static string GetSTRXMLResult(DataTable dtTable)
    {
        string strXMLResult = "";
        if ((dtTable != null))
        {
            if (dtTable.Rows.Count > 0)
            {
                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw);
                strXMLResult = sw.ToString();
                sw.Close();
                sw.Dispose();
            }
        }
        return strXMLResult;
    }
    protected void GrvChallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnTransMode = (HiddenField)e.Row.FindControl("hdnTransMode");
            ImageButton imgbtnDelete2 = (ImageButton)e.Row.FindControl("imgbtnDelete2");
            if (hdnTransMode.Value == "1")
            {
                imgbtnDelete2.Visible = true;
            }
            else
            {
                imgbtnDelete2.Visible = false;
            }
        }
    }
   
}