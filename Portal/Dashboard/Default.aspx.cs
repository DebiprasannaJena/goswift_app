using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BusinessLogicLayer.Dashboard;
using System.Net;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Configuration;
using System.Globalization;
using BusinessLogicLayer.Service;
using EntityLayer.Service;
public partial class Dashboard_Default : System.Web.UI.Page
{
    SWPDashboard objDashboard = new SWPDashboard();

    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }

        string strDes = Session["desId"].ToString();
        string strDep = Session["DeptId"].ToString();

        if (strDes == "94")
        {
            Response.Redirect("CmDashboard.aspx");
        }
        else if (strDes == "95" || strDes == "124" || strDes == "171") //// Chief Secretary and Development Commissioner
        {
            Response.Redirect("ChiefSecretaryDashboard.aspx");
        }
        else if (strDes == "172") ////Principal Advisor
        {
            Response.Redirect("PrincipalAdvisorDashboard.aspx");
        }
        else if (strDes == "125")
        {
            Response.Redirect("PS(MSME)Dashboard.aspx");
        }
        else if (strDes == "89")
        {
            Response.Redirect("MISIDCODashboard.aspx");
        }
        else if (strDes == "96")
        {
            Response.Redirect("PSIndustriesDashboard.aspx");
        }
        else if (strDes == "97")
        {
            Response.Redirect("PS(MSME)Dashboard.aspx");
        }
        else if (strDes == "98")
        {
            Response.Redirect("PS(Finance)Dashboard.aspx");
        }
        else if (strDes == "99")
        {
            Response.Redirect("CMDIPICOLDashboard.aspx");
        }
        else if (strDes == "128")
        {
            Response.Redirect("CMDIDCODashboard.aspx");
        }
        else if (strDes == "100")
        {
            Response.Redirect("GMDashboard.aspx");
        }
        else if (strDes == "2")
        {
            Response.Redirect("PsTourism.aspx");
        }
        else if (strDes == "10" || strDes == "9")
        {
            Response.Redirect("DICDashboard.aspx");
        }
        else if (strDes == "126")
        {
            Response.Redirect("DICDashboard.aspx");
        }
        else if (strDes == "101")
        {
            Response.Redirect("DIDashboard.aspx");
        }
        else if (strDes == "102")
        {
            Response.Redirect("MinisterDashboard.aspx");
        }
        else if (strDes == "174")
        {
            Response.Redirect("OPTCLDashboard.aspx");
        }
        else if (strDes == "105" || strDes == "106" || strDes == "107" || strDes == "108" || strDes == "109" || strDes == "21" || strDes == "23" || strDes == "110" || strDes == "111") //// Energy Utility
        {
            //// 105-Utility Head
            //// 106-CESU Supertending Engg(SE)
            //// 107-CESU Executive Engineer (Division Head)
            //// 108-Wesco Supertending Engg (SE)
            //// 109-WESCO Executive Engg (Division Head)
            //// 21- SOUTHCO Executive Engineer (Division Head)
            //// 23- Southco Superintending Engineer (SE)
            //// 110-NESCO Supertending Engg (SE)
            //// 111-NESCO Executive Engineer (Division Head)

            Response.Redirect("EnergyUtilityDashboard.aspx");
        }
        else
        {
            if (Session["adminstat"].ToString() == "super")
            {
                Response.Redirect("AdminDashboard.aspx");
            }
            else
            {
                Response.Redirect("DepartmentDashboard.aspx");
            }
        }

        //Chief Minister         94   
        //Minister               
        //Chief Secretary       95    
        //PS (Industries)     96   
        //PS (MSME)            97  
        //PS (Finance)           98
        //CMD (IPICOL)           99
        //GM SLNA                100
        //DIC                    10
        //DI        101
        //Minister 102   
    }
}