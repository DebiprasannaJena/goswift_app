using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;

public partial class WaterAllotmentForm : SessionCheck
{
    int IENameCnt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                txtUnitName.Text = Session["IndustryName"].ToString();

                WaterService objWaterAllotment = new WaterService();
                WaterAllotmentDetails objWater = new WaterAllotmentDetails();
                DataTable dt = new DataTable();

                objWater.Action = "C";
                objWater.strProposalId = Convert.ToString(Request.QueryString["ProposalNo"]);
                dt = objWaterAllotment.IEName(objWater);
                dt.PrimaryKey = new[] { dt.Columns["IEIACode"], dt.Columns["IEIAName"] };

                if (dt.Rows.Count > 0)
                {
                    ddlIE.DataSource = dt;
                    ddlIE.DataTextField = "IEIAName";
                    ddlIE.DataValueField = "IEIACode";
                    ddlIE.DataBind();
                }
                else
                {
                    ddlIE.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
                objWater = null;
                dt = null;
                objWater = new WaterAllotmentDetails();
                dt = new DataTable();
                objWater.Action = "E";
                objWater.strProposalId = Convert.ToString(Request.QueryString["ProposalNo"]);
                dt = objWaterAllotment.IEName(objWater);
                if (dt.Rows.Count > 0)
                {
                    txtCorName.Text = dt.Rows[0]["vchContactPerson"].ToString();
                    txtCorEmail.Text = dt.Rows[0]["vchCorEmail"].ToString();
                    txtCorMobile.Text = dt.Rows[0]["vchCorMobileNo"].ToString();
                    txtCorAddress.Text = dt.Rows[0]["vchCorAdd"].ToString();
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Water");
            }
        }
    }
    public void ResetFunc()
    {
        txtUnitName.Text = "";
        ddlIE.SelectedValue = "0";
        txtPlotNo.Text = "";
        ddlPurpose.SelectedValue = "0";
        txtQuantity.Text = "";
        txtSize.Text = "";
        txtModel.Text = "";
        txtSerialNo.Text = "";
        txtTankSize.Text = "";
        txtTankNo.Text = "";
        txtSumpSize.Text = "";
        txtSumpNo.Text = "";
        txtCorName.Text = "";
        txtCorEmail.Text = "";
        txtCorMobile.Text = "";
        txtCorAddress.Text = "";
        txtPlumberName.Text = "";
        txtPlumberEmail.Text = "";
        txtPlumberMobile.Text = "";
        txtPlumberAddress.Text = "";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string water_ref_no = "0";
        string PaymentUrl = "";
        WaterService objWaterAllotment = new WaterService();
        WaterAllotmentDetails objWater = new WaterAllotmentDetails();
        string strVal;
        try
        {
            objWater.Action = "A";
            objWater.strInvestorName = Convert.ToString(Session["UserName"]);
            objWater.strProposalId = Convert.ToString(Request.QueryString["ProposalNo"]);
            objWater.intServiceId = Convert.ToInt32(Request.QueryString["ServiceID"]);
            objWater.strIndustryCode = Convert.ToString(hdnUnitCode.Value);
            objWater.strUnitName = txtUnitName.Text;
            objWater.intIEId = Convert.ToInt32(ddlIE.SelectedValue);
            objWater.strPlotShedNo = txtPlotNo.Text;
            objWater.intPupose = Convert.ToInt32(ddlPurpose.SelectedValue);
            objWater.strQuantity = txtQuantity.Text;
            objWater.strFlowMeterSize = txtSize.Text;
            objWater.strMakeModel = txtModel.Text;
            objWater.strManfSerialNo = txtSerialNo.Text;
            objWater.strOHTankSize = txtTankSize.Text;
            objWater.strOHTankNo = txtTankNo.Text;
            objWater.strSumpVatSize = txtSumpSize.Text;
            objWater.strSumpVatNo = txtSumpNo.Text;
            objWater.strContactName = txtCorName.Text;
            objWater.strContactEmail = txtCorEmail.Text;
            objWater.strContactMobile = txtCorMobile.Text;
            objWater.strContactAddress = txtCorAddress.Text;
            objWater.strPlumberName = txtPlumberName.Text;
            objWater.strPlumberEmail = txtPlumberEmail.Text;
            objWater.strPlumberMobile = txtPlumberMobile.Text;
            objWater.strPlumberAddress = txtPlumberAddress.Text;
            objWater.intCreatedBy = Convert.ToInt32(Session["InvestorId"].ToString());

            strVal = objWaterAllotment.AddWaterAllotmentDetails(objWater);
            string[] strArr = strVal.Split('_');
            if (strArr[0] == "1")
            {
                try
                {
                    string serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPIEIAName/" + Session["UID"].ToString();
                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
                    httpRequest.Accept = "application/json";
                    httpRequest.ContentType = "application/json";
                    httpRequest.Method = "GET";
                    using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                    {
                        using (Stream stream = httpResponse.GetResponseStream())
                        {
                            string strResult = (new StreamReader(stream)).ReadToEnd();
                            JObject o = JObject.Parse(strResult);
                            string strRes = o["getSWPIEIANameResult"].ToString();
                            string strFinalRes = strRes.Remove(strRes.Trim().Length - 3).Trim().Substring(3);
                            JObject oj = JObject.Parse(strFinalRes);
                            string str = oj["objIEIADetail"].ToString();
                            DataTable DynTable = (DataTable)JsonConvert.DeserializeObject(str, (typeof(DataTable)));

                            if (DynTable.Rows.Count > 0)
                            {
                                foreach (DataRow dr in DynTable.Rows)
                                {
                                    if (dr["IEIAName"].ToString() == Convert.ToString(ddlIE.SelectedItem))
                                    {
                                        try
                                        {
                                            hdnProjectCode.Value = dr["IEIACode"].ToString();
                                            serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getPartyCode/" + Session["UID"].ToString();
                                            httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
                                            httpRequest.Accept = "application/json";
                                            httpRequest.ContentType = "application/json";
                                            httpRequest.Method = "GET";
                                            using (HttpWebResponse httpResponse2 = (HttpWebResponse)httpRequest.GetResponse())
                                            {
                                                using (Stream stream2 = httpResponse2.GetResponseStream())
                                                {
                                                    string strResult2 = (new StreamReader(stream2)).ReadToEnd();
                                                    JObject o2 = JObject.Parse(strResult2);
                                                    string strRes2 = o2["getPartyCodeResult"].ToString();
                                                    string strFinalRes2 = strRes2.Remove(strRes2.Trim().Length - 3).Trim().Substring(3);
                                                    JObject oj2 = JObject.Parse(strFinalRes2);
                                                    string str2 = oj2["objUnitDetails"].ToString();
                                                    DataTable DynTable1 = (DataTable)JsonConvert.DeserializeObject(str2, (typeof(DataTable)));

                                                    if (DynTable1.Rows.Count > 0)
                                                    {
                                                        hdnUnitCode.Value = DynTable1.Rows[0]["PartyCode"].ToString();
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Util.LogError(ex, "Water");
                                            ScriptManager.RegisterStartupScript(Page, GetType(), "msg", "<script>alert('" + ex.Message + "')</script>", true);
                                        }
                                        finally
                                        {
                                        }

                                        string json = "{" + "\"swp_application_id\" : " + "\"" + strArr[1] + "\","
                                            + "\"party_details\" : " + "\"" + hdnUnitCode.Value + "\","
                                            + "\"industrial_estate_area\" : " + "\"" + Convert.ToString(hdnProjectCode.Value) + "\","
                                            + "\"plot_shed_no\" : " + "\"" + txtPlotNo.Text + "\","
                                            + "\"proposeOf_water_connection\" : " + "\"" + Convert.ToString(ddlPurpose.SelectedItem) + "\","
                                            + "\"water_required_per_day\" : " + "\"" + txtQuantity.Text + "\","
                                            + "\"flow_meter_size\" : " + "\"" + txtSize.Text + "\","
                                            + "\"make_model\" : " + "\"" + txtModel.Text + "\","
                                            + "\"manufacture_serial_no\" : " + "\"" + txtSerialNo.Text + "\","
                                            + "\"oh_tank_size\" : " + "\"" + txtTankSize.Text + "\","
                                            + "\"oh_tank_no\" : " + "\"" + txtTankNo.Text + "\","
                                            + "\"sump_size\" : " + "\"" + txtSumpSize.Text + "\","
                                            + "\"sump_no\" : " + "\"" + txtSumpNo.Text + "\","
                                            + "\"contact_person_name\" : " + "\"" + txtCorName.Text + "\","
                                            + "\"contact_address\" : " + "\"" + txtCorAddress.Text + "\","
                                            + "\"contact_email\" : " + "\"" + txtCorEmail.Text + "\","
                                            + "\"contact_mobile\" : " + "\"" + txtCorMobile.Text + "\","
                                            + "\"plumber_name\" : " + "\"" + txtPlumberName.Text + "\","
                                            + "\"plumber_address\" : " + "\"" + txtPlumberAddress.Text + "\","
                                            + "\"plumber_email\" : " + "\"" + txtPlumberEmail.Text + "\","
                                            + "\"plumber_mobile\" : " + "\"" + txtPlumberMobile.Text + "\","
                                            + "\"industry_code\" : " + "\"" + hdnUnitCode.Value + "\""
                                         + "}" ;

                                        try
                                        {
                                            string serviceUrl1 = "http://erp.idco.in/rest/waterApplicationFromSWPtoERP";
                                            HttpWebRequest httpRequest1 = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl1));
                                            httpRequest1.Accept = "application/json";
                                            httpRequest1.ContentType = "application/json";
                                            httpRequest1.Method = "POST";
                                            using (var streamWriter = new StreamWriter(httpRequest1.GetRequestStream()))
                                            {
                                                //initiate the request
                                                var resToWrite = json;
                                                streamWriter.Write(resToWrite);
                                                streamWriter.Flush();
                                                streamWriter.Close();
                                            }
                                            using (HttpWebResponse httpResponse1 = (HttpWebResponse)httpRequest1.GetResponse())
                                            {
                                                using (Stream stream1 = httpResponse1.GetResponseStream())
                                                {
                                                    string strResult1 = (new StreamReader(stream1)).ReadToEnd();
                                                    string Message = JObject.Parse(strResult1)["success_message"].ToString();
                                                    string swp_application_id = JObject.Parse(strResult1)["swp_application_id"].ToString();
                                                    string industry_code = JObject.Parse(strResult1)["industry_code"].ToString();
                                                    if (Message == "Application received and validated successfully")//Success
                                                    {
                                                        water_ref_no = JObject.Parse(strResult1)["water_appl_ref_no"].ToString();
                                                        PaymentUrl = JObject.Parse(strResult1)["payment_gateway_url"].ToString();
                                                    }
                                                }
                                                if (strVal == "1")
                                                {
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Water Allotment Added Successfully.', '" + Messages.TitleOfProject + "', function () {location.href = 'ApplicationDetails.aspx';}); </script>", false);
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Util.LogError(ex, "Water");
                                            ScriptManager.RegisterStartupScript(Page, GetType(), "msg", "<script>alert('" + ex.Message + "')</script>", true);
                                        }
                                        finally
                                        {
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Water Allotment Added Successfully.', '" + Messages.TitleOfProject + "', function () {location.href = 'ApplicationDetails.aspx';}); </script>", false);
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Water Allotment Added Successfully.', '" + Messages.TitleOfProject + "', function () {location.href = 'ApplicationDetails.aspx';}); </script>", false);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "Water");
                    ScriptManager.RegisterStartupScript(Page, GetType(), "msg", "<script>alert('" + ex.Message + "')</script>", true);
                }
                finally
                {
                    
                }
                objWater.Action = "U";
                objWater.strApplicationId = strArr[1];
                objWater.PaymentUrl = PaymentUrl;
                objWater.strRefNo = strArr[1];
                if (water_ref_no == "0")
                {
                    objWater.intStatus = 0;
                }
                else
                {
                    objWater.intStatus = 1;
                }
                strVal = objWaterAllotment.UpdateStatus(objWater);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Water Allotment Added Successfully.', '" + Messages.TitleOfProject + "', function () {location.href = 'ApplicationDetails.aspx';}); </script>", false);
            }
            ResetFunc();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Water");
            ScriptManager.RegisterStartupScript(Page, GetType(), "msg", "<script>alert('" + ex.Message + "')</script>", true);
        }
        finally
        {
            objWater = null;
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbWatr.SelectedValue == "0")
        { 
        
        }
        else if (rdbWatr.SelectedValue == "1")
        {
            if (Request.QueryString["ProposalNo"] != "" || Request.QueryString["ProposalNo"] != null)
            {
                Response.Redirect("FormView.aspx?FormId=41&ProposalNo="+Request.QueryString["ProposalNo"].ToString()+"");
            }
            else
            {
                Response.Redirect("FormView.aspx?FormId=41&ProposalNo=0");
            }
        }
    }
}