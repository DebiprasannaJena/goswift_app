using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.ServiceModel;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Web.Script.Serialization;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public partial class LandAllocated : System.Web.UI.Page
{
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();
    PromoterDet objprom = new PromoterDet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ProposalNo"]))
            {
                try
                {
                    IntegrationPEALWithIDCO();
                }
                catch (NullReferenceException ex) { throw ex; }
                catch (Exception ex)
                { throw ex; }
                finally {  }
            }
        }
    }
    private void IntegrationPEALWithIDCO()
    {
        try
        {
            CNET objCnet = new CNET();
            ProposalBAL objService = new ProposalBAL();
            objCnet.vchProposalNo = Request.QueryString["ProposalNo"];
            List<CNET> objProposalList1 = objService.GetCNETCompanyDetails(objCnet).ToList();
            string inputJson = (new JavaScriptSerializer()).Serialize(objProposalList1);
            inputJson = inputJson.TrimStart('[').TrimEnd(']');
            string serviceUrl = "http://erp.idco.in/rest/sendDataFromSWPtoERP";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                //initiate the request
                var resToWrite = inputJson;
                streamWriter.Write(resToWrite);
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    if (strResult.IndexOf("failure_message") != -1)
                    {
                        objprom.vch_unique_application_id_from_swp = Request.QueryString["ProposalNo"];
                        objprom.vch_Error_Msg = JObject.Parse(strResult)["failure_message"].ToString();
                        objprom.vch_validation_Msg = JObject.Parse(strResult)["validation_message"].ToString();
                        objprom.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
                        objprom.vch_Input_String = inputJson;
                        string strRetVal = objService.ProposalCNETData(objprom);
                    }
                    else
                    {
                        objprom.vch_oas_cafno = JObject.Parse(strResult)["cafno"].ToString();
                        objprom.vch_unique_application_id_from_swp = JObject.Parse(strResult)["unique_application_id_from_swp"].ToString();
                        objprom.vch_industry_code = JObject.Parse(strResult)["industry_code"].ToString();
                        objprom.vch_success_message = JObject.Parse(strResult)["success_message"].ToString();
                        objprom.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
                        objprom.vch_Input_String = inputJson;
                        string strRetVal = objService.ProposalCNETData(objprom);
                    }
                }
            }
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { }

    }
    //private void GetIntegrationStatusPEALWithIDCO(string oas_cafno)
    //{
    //    string serviceUrl = "http://erp.idco.in/" + "sendStatusOfApplicationFromERPtoSWP?oas_cafno=" + oas_cafno;
    //    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
    //    httpRequest.Accept = "application/json";
    //    httpRequest.ContentType = "application/json";
    //    httpRequest.Method = "GET";
    //    using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
    //    {
    //        using (Stream stream = httpResponse.GetResponseStream())
    //        {
    //            string strResult = (new StreamReader(stream)).ReadToEnd();
    //        }
    //    }
    //}
}