using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProfessionalTax;
using Newtonsoft.Json;
using EntityLayer.Service;
using System.Configuration;
using BPAS;
using System.IO;
using System.Data;
using System.Net;
using System.Web.Script.Serialization;


public partial class ServiceApplicationDetails : SessionCheck
{
   static string ApplicationNo = "";
   static string ServiceId = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ApplicationNo = Request.QueryString["ApplicationNo"].ToString();
            ServiceId = Request.QueryString["ServiceId"].ToString();
            BindGrid();
        }
    }
    public void BindGrid()
    {
        try
        {
            if (ServiceId == "10")
            {
                WebServiceSoapClient obj = new WebServiceSoapClient();
                string str = obj.PTRegistrationStatus(ConfigurationManager.AppSettings["PTKEY"].ToString(), ApplicationNo, "");

                List<CPT> list = JsonConvert.DeserializeObject<List<CPT>>(str);
                if (list.Count > 0)
                {
                    grvDetails.DataSource = list;
                    grvDetails.DataBind();
                }
            }
            else if (ServiceId == "20")
            {


                string RestServiceURL = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
                // string RedirectionURL = "http://164.100.141.98/BDALogin";
                try
                {
                    using (var client = new WebClient()) //WebClient  
                    {
                        //Variable given by User
                        string _uniquecode = ApplicationNo; string _SWPCode = Session["InvestorId"].ToString();


                        string _msg = string.Empty;
                        client.Headers.Add("Content-Type:application/json"); //Content-Type  
                        client.Headers.Add("Accept:application/json");
                        var result = client.DownloadString(RestServiceURL + "/FetchApplicationsByUniqueCode?_uniquecode=" + _uniquecode + "&_SWPCode=" + _SWPCode + "");
                        string reslt = result;
                        JavaScriptSerializer serializer = new JavaScriptSerializer();

                        object a = serializer.Deserialize(result, typeof(object));
                        string str = a.ToString();
                        if (str == "No data found.")
                        {
                            grvDetails.DataSource = null;
                            grvDetails.DataBind();                           
                        }
                        else
                        {
                            DataTable DynTable = (DataTable)JsonConvert.DeserializeObject(str, (typeof(DataTable)));
                            grvDetails.DataSource = DynTable;
                            grvDetails.DataBind();
                          
                        }

                    }


                }
                catch (Exception)
                {


                }

            }
        }
        catch(Exception ex)
        {

        }
    }
}