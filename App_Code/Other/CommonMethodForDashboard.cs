using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using BusinessLogicLayer.Service;
using EntityLayer.Proposal;
using EntityLayer.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CommonMethodForDashboard
/// </summary>
public class CommonMethodForDashboard
{
    public CommonMethodForDashboard()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    SWPDashboard objSWP;
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    ProposalBAL objService = new ProposalBAL();
    ProjectInfo objProp;
    #region FillYearDropDownList

    /// <summary>
    /// This function is used to bind dropdownlist with financial year.
    /// Here both value and text field for Dropdownlist is same i.e. financial year.
    /// Financial Year Format Like :- 2016-17,2017-18 and so on.
    
    public void FillFinancialYear(DropDownList DrpDwnYear)
    {
        try
        {
            StringBuilder FinalcealYr = new StringBuilder();

            int MONTH = Convert.ToInt32(DateTime.Now.ToString("MM"));

            if(MONTH >3)
            {
            
               int datediff = Convert.ToInt32(DateTime.Now.Year.ToString()) - Convert.ToInt32(2016);
                 DrpDwnYear.Items.Clear();
                for (int i = 0; i <= datediff; i++)
                {
                    string FY = Convert.ToString(Convert.ToInt32(DateTime.Now.Year.ToString()) - i) + "-" + Convert.ToString((Convert.ToInt32(DateTime.Now.ToString("yy")) - i) + 1);
                
                    ListItem list1 = new ListItem();
                    list1.Text = FY;
                    list1.Value = FY;
                
                    DrpDwnYear.Items.Add(list1);
                }

            }
            else
            {
                int datediff = Convert.ToInt32(DateTime.Now.Year.ToString()) - Convert.ToInt32(2016) - 1;
                DrpDwnYear.Items.Clear();
                for (int i = 0; i <= datediff; i++)
                {
                    string FY = Convert.ToString(Convert.ToInt32(DateTime.Now.Year.ToString()) - i - 1) + "-" + Convert.ToString((Convert.ToInt32(DateTime.Now.ToString("yy")) - i));
                    ListItem list1 = new ListItem();
                    list1.Text = FY;
                    list1.Value = FY;

                    DrpDwnYear.Items.Add(list1);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// This function is used to bind dropdownlist with financial year as text field and year as value field.
    /// Here text field for dropdownlist is financial year (2016-17) and the value field is the only year (2016).   
    
    public void FillFinancialYearWithYear(DropDownList DrpDwnYear)
    {
        try
        {
            StringBuilder FinalcealYr = new StringBuilder();
            int MONTH = Convert.ToInt32(DateTime.Now.ToString("MM"));
            if(MONTH > 3) // check month is april  then add new financial year 
            {
                int datediff = Convert.ToInt32(DateTime.Now.Year.ToString()) - Convert.ToInt32(2016);
                DrpDwnYear.Items.Clear();
                for (int i = 0; i <= datediff; i++)
                {
                    string TextFY = Convert.ToString(Convert.ToInt32(DateTime.Now.Year.ToString()) - i) + "-" + Convert.ToString((Convert.ToInt32(DateTime.Now.ToString("yy")) - i) + 1);
                    string ValueYr = Convert.ToString(Convert.ToInt32(DateTime.Now.Year.ToString()) - i);

                    ListItem list1 = new ListItem
                    {
                        Text = TextFY,
                        Value = ValueYr
                    };

                    DrpDwnYear.Items.Add(list1);
                }
            }
            else
            {
                int datediff = Convert.ToInt32(DateTime.Now.Year.ToString()) - Convert.ToInt32(2016) -1;
                DrpDwnYear.Items.Clear();
                for (int i = 0; i <= datediff; i++)
                {
                    string TextFY = Convert.ToString(Convert.ToInt32(DateTime.Now.Year.ToString()) - i-1) + "-" + Convert.ToString((Convert.ToInt32(DateTime.Now.ToString("yy")) - i));
                    string ValueYr = Convert.ToString(Convert.ToInt32(DateTime.Now.Year.ToString()) - i-1);

                    ListItem list1 = new ListItem
                    {
                        Text = TextFY,
                        Value = ValueYr
                    };

                    DrpDwnYear.Items.Add(list1);
                }

            }



            
           
            
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    #endregion
    #region FillDistDropDown
    /// <summary>
    /// This function is used for fill data in  district dropdown
    /// </summary>   
    public void FillDist(DropDownList ddlDist)
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();

        ddlDist.DataSource = objProjList;
        ddlDist.DataTextField = "vchDistName";
        ddlDist.DataValueField = "intDistId";
        ddlDist.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlDist.Items.Insert(0, list);
    }

    /// <summary>
    /// This function is used for fill data in  district checkboxlist
    /// </summary> 
    public void FillDist(CheckBoxList ChkDist)
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();
        for (int i = 0; i < objProjList.Count; i++)
        {
            ListItem item = new ListItem();
            item.Text = objProjList[i].vchDistName;
            item.Value = objProjList[i].intDistId.ToString();
            ChkDist.Items.Add(item);
        }
        foreach (ListItem lstitem in ChkDist.Items)
        {
            lstitem.Selected = true;
        }
    }
    

    #endregion

    #region Fill Department
    /// <summary>
    /// This function is used for fill data in  department dropdown
    /// </summary>   
    public void FillDepartment(DropDownList ddlDept)
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.BindDepartment("DP").ToList();
        ddlDept.DataSource = objServicelist;
        ddlDept.DataTextField = "strdeptname";
        ddlDept.DataValueField = "Deptid";
        ddlDept.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlDept.Items.Insert(0, list);
    }

    /// <summary>
    /// This function is used for  fill data in CIF  department dropdown
    /// </summary>

    public void FillDepartmentForCIF (DropDownList ddlDistCIF)
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        objSWP.strAction = "CDP";
        List<SWPDashboard> objServiceStatus = objserviceDashboard.BindCICGDepartment(objSWP).ToList();
        ddlDistCIF.DataSource = objServiceStatus;
        ddlDistCIF.DataTextField = "VCH_DEPT_NAME";
        ddlDistCIF.DataValueField = "intDeptId";
        ddlDistCIF.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlDistCIF.Items.Insert(0, list);
    }

    #endregion

    #region Fill Service 
    /// <summary>
    /// This function is used for fill data in  Searvice  dropdown
    /// </summary>

    public void FillService(DropDownList ddlDept , DropDownList ddlService)
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.BindService("S", int.Parse(ddlDept.SelectedValue)).ToList();
        ddlService.DataSource = objServicelist;
        ddlService.DataTextField = "strServiceName";
        ddlService.DataValueField = "intServiceId";
        ddlService.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlService.Items.Insert(0, list);
    }

    /// <summary>
    /// This function is used for fill data in  Searvice  dropdown for Energy Utility department
    /// </summary>

    public void FillServiceEnergy(DropDownList ddlDept, DropDownList ddlService)
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.BindService("S", int.Parse(ddlDept.SelectedValue)).ToList();
        List<ServiceDetails> tlistFiltered = objServicelist.Where(item => item.intServiceId == 16).ToList();
        ddlService.DataSource = tlistFiltered;
        ddlService.DataTextField = "strServiceName";
        ddlService.DataValueField = "intServiceId";
        ddlService.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlService.Items.Insert(0, list);
    }




    #endregion

    #region MasterTrackerSection
    /// <summary>
    /// Method for binding data from  Peal recived data on master Tracker dashbord  .
    /// </summary>
    /// <param name="strFinacialYear"></param>
    /// <returns></returns>
    public List<SWPDashboard> BindPealRecieved(string strFinacialYear) 
    {
        try
        {
            objSWP = new SWPDashboard
            {
                strAction = "RP",
                strFinacialYear = strFinacialYear
            };
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for binding data from  Peal recived ,approved ,Pending   data on master Tracker of IT dashbord  .
    /// </summary>
    public List<SWPDashboard> FillProposalMasterDetails (string strFinacialYear ,int intUserid ,int intSecId)
    {
        try
        {
            objSWP = new SWPDashboard
            {
                strAction = "PT",
                intQuarter=0,
                intDistrictid=0,
                strFinacialYear = strFinacialYear,
                intUserid= intUserid,
                intSecId= intSecId,
                intType=2

            };
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for binding data from  Peal recived data on master Tracker DI dashbord  .
    /// </summary>
    public List<SWPDashboard> BindPealRecieved(string strFinacialYear ,string intUserid)
    {
        try
        {
            objSWP = new SWPDashboard
            {
                strAction = "RP",
                strFinacialYear = strFinacialYear,
                intUserid=Convert.ToInt32( intUserid)
            };
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Method for binding data from  Peal recived data on master Tracker DIC dashbord  .
    /// </summary>
    public List<SWPDashboard> BindPealRecieved(string strFinacialYear, string intUserid ,int intDistrictid)
    {
        try
        {
            objSWP = new SWPDashboard
            {
                strAction = "RP",
                strFinacialYear = strFinacialYear,
                intUserid = Convert.ToInt32(intUserid),
                intDistrictid = intDistrictid
            };
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Method for binding data from  Peal Approved data on master Tracker dashbord  .
    /// </summary>
    /// <param name="strFinacialYear"></param>
    /// <returns></returns>
    public List<SWPDashboard> BindPealApproved(string strFinacialYear) 
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "RA";
            objSWP.strFinacialYear = strFinacialYear;
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for binding data from  Peal Approved data on master Tracker DI dashbord  .
    /// </summary>
    
    public List<SWPDashboard> BindPealApproved(string strFinacialYear ,string intUserid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "RA";
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intUserid = Convert.ToInt32(intUserid);
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for binding data from  Peal Approved data on master Tracker DIC dashbord  .
    /// </summary>

    public List<SWPDashboard> BindPealApproved(string strFinacialYear, string intUserid ,int intDistrictid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "RA";
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intUserid = Convert.ToInt32(intUserid);
            objSWP.intDistrictid = intDistrictid;
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Method for binding data from  Peal pending data on master Tracker dashbord  .
    /// </summary>
    /// <param name="strFinacialYear"></param>
    /// <returns></returns>
    public List<SWPDashboard> BindUnderEvalution(string strFinacialYear)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "UE";
            objSWP.strFinacialYear = strFinacialYear;
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// <summary>
    /// Method for binding data from  Peal pending data on master Tracker of DI  dashbord  .
    /// </summary>

    public List<SWPDashboard> BindUnderEvalution(string strFinacialYear ,string intUserid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "UE";
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intUserid = Convert.ToInt32(intUserid);
            List <SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// <summary>
    /// Method for binding data from  Peal pending data on master Tracker of DIC  dashbord  .
    /// </summary>
    public List<SWPDashboard> BindUnderEvalution(string strFinacialYear, string intUserid ,int intDistrictid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "UE";
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intUserid = Convert.ToInt32(intUserid);
            objSWP.intDistrictid = intDistrictid;
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// <summary>
    /// Method for  bind Incetive data 
    /// </summary>
    /// <param name="strFinacialYear"></param>
    /// <returns></returns>
    public List<SWPDashboard> IncentiveMasterBind(string strFinacialYear) 
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "B";
            objSWP.intQuarter = 0;
            objSWP.intYearId = 0;
            objSWP.strFinacialYear = strFinacialYear;//Added by suroj on 26-10-17 to check finacial yr
            objSWP.intUserid = 0;// Convert.ToInt32(Session["Userid"]);
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Method for  bind Incetive data  for DIC dashboard
    /// </summary>
    public List<SWPDashboard> IncentiveMasterBind(string strFinacialYear ,int IncentiveDist)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "B";
            objSWP.intQuarter = 0;
            objSWP.intYearId = 0;
            objSWP.intDistrictid = IncentiveDist;
            objSWP.strFinacialYear = strFinacialYear;//Added by suroj on 26-10-17 to check finacial yr
            objSWP.intUserid = 0;// Convert.ToInt32(Session["Userid"]);
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            return objswpDashboardList = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for  bind SPMG  on master tracker
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="intYearId"></param>
    /// <returns></returns>
    public List<SWPDashboard> ViewSPMGMasterData(int userid, int intYearId) 
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.intUserid = userid;
            objSWP.strAction = "SPMGALL";
            objSWP.intYearId = intYearId;
            return objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Method for  bind SPMG  on master tracker of department dashboard 
    /// </summary>

    public List<SWPDashboard> ViewSPMGMasterDataDept(int intDeptId, int intYearId)
    {
        try
        {
            objSWP = new SWPDashboard();
            
            objSWP.strAction = "ALL";
            objSWP.intDeptId = intDeptId;
            objSWP.intYearId = intYearId;
            return objserviceDashboard.GetSPMGdepartmentwiseStatus(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for  bind Pending approval  on master tracker
    /// </summary>
    /// <param name="strFinacialYear"></param>
    /// <returns></returns>
    public List<SWPDashboard> ViewServiceMaster(string strFinacialYear) 
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "DM";
            objSWP.intDeptId = 0;
            objSWP.intServiceId = 0;
            objSWP.strFinacialYear = strFinacialYear;
            return objserviceDashboard.GetDashboardServiceStatusDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for  bind Pending approval  on master tracker of department dashboard 
    /// </summary>
    public List<SWPDashboard> ViewServiceMasterDept(int intDeptId,int intUserid,  string strFinacialYear)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "DM";
            objSWP.intDeptId = intDeptId;
            objSWP.intUserid = intUserid;
            objSWP.intServiceId = 0;
            objSWP.strFinacialYear = strFinacialYear;
            return objserviceDashboard.GetDashboardServiceStatusDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Method for  bind Pending approval  on master tracker of CESU dashboard 
    /// </summary>
    public List<SWPDashboard> ViewServiceMasterDept(int intDeptId, int intUserid ,int intServiceId, string strFinacialYear)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "MEUC";
            objSWP.intDeptId = intDeptId;
            objSWP.intUserid = intUserid;
            objSWP.intServiceId = intServiceId;
            objSWP.strFinacialYear = strFinacialYear;
            return objserviceDashboard.GetDashboardServiceStatusDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for bind Investment on master tracker
    /// </summary>
    /// <param name="intUserid"></param>
    /// <param name="strFinacialYear"></param>
    /// <returns></returns>
    public List<SWPDashboard> FillTrackerInvestment(int intUserid, string strFinacialYear) 
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "PB";
            objSWP.intUserid = intUserid;
            objSWP.strFinacialYear = strFinacialYear;
            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for bind Investment on master tracker OF DI dashboard
    /// </summary>
    public List<SWPDashboard> FillTrackerInvestment(int intUserid, string strFinacialYear ,int intType)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "PB";
            objSWP.intUserid = intUserid;
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intType = intType;
            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for bind Employment on master tracker
    /// </summary>
    /// <param name="intUserid"></param>
    /// <param name="strFinacialYear"></param>
    /// <returns></returns>
    public List<SWPDashboard> FillTrackerEmployement(int intUserid, string strFinacialYear) 
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "PB";
            objSWP.intUserid = intUserid;
            objSWP.strFinacialYear = strFinacialYear;
            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for bind Employment on master tracker DI dashboard
    /// </summary>

    public List<SWPDashboard> FillTrackerEmployement(int intUserid, string strFinacialYear ,int intType)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "PB";
            objSWP.intUserid = intUserid;
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intType = intType;
            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for bind Grievance Status on master tracker
    /// </summary>
    /// <param name="intUserid"></param>
    /// <param name="strFinacialYear"></param>
    /// <returns></returns>
    public DataTable BindMasterGrievanceportlet(int intUserid, string strFinacialYear) 
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "DGD";
            objSWP.intUserid = intUserid;
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intDistrictId = 0;
            return objserviceDashboard.getDepartmentGrievance(objSWP);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Method for bind Grievance Status on master tracker
    /// </summary>

    public DataTable BindMasterGrievanceportlet(int intUserid, string strFinacialYear ,int intDistrictId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "DGD";
            objSWP.intUserid = intUserid;
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intDistrictId = intDistrictId;
            return objserviceDashboard.getDepartmentGrievance(objSWP);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for bind Central Inspection Framework on master tracker
    /// </summary>
    /// <param name="intUserid"></param>
    /// <returns></returns>
    public List<SWPDashboard> ViewCICGMasterData(int intUserid) 
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "VCI";
            objSWP.intUserid = intUserid;
            return objserviceDashboard.GetCICGDashboardService(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for bind CSR Spend on master tracker
    /// </summary>
    /// <param name="Year"></param>
    /// <returns></returns>
    public DataTable InsertCSRMaster(string Year) 
    {
        try
        {
            string Type = "1";
            string UniqueKey = "0";
            string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetTotalAmount/" + UniqueKey + "/" + Type + "/" + 0 + "/" + Year;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    return DashboradCommon.JsonStringToDataTable(strResult);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for bind IDCO Post Allotment Applications on master tracker
    /// </summary>
    /// <param name="Year"></param>
    /// <param name="intUserid"></param>
    /// <returns></returns>
    public DataTable InsertAppa(string Year, int intUserid)
    {
        string Type = string.Empty;
        try
        {
            objSWP = new SWPDashboard();
            string Deptid = "0";
            string UniqueKey = "0";
            objSWP.intUserid = intUserid;

            List<SWPDashboard> objlist = objserviceDashboard.CheckAppastatus(objSWP);
            if (objlist.Count > 0)
            {
                if (objlist[0].intStatus == 1 && objlist[0].intDistrictid == 0)//Admin
                {
                    Type = "0";
                }
                else if (objlist[0].intStatus != 1 && objlist[0].intDistrictid == 0)//IPICOL Largescale
                {
                    Type = "1";
                }
                else//MSME
                {
                    Type = "2";
                }
            }
            string serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + "0" + "/" + Deptid + "/" + Type + "/" + Year + "/" + "0";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    return DashboradCommon.JsonStringToDataTable(strResult);

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for bind IDCO Post Allotment Applications for DIC Dashboard on master tracker
    /// </summary>

    public DataTable InsertAppa(string Year, int intUserid ,int Pealuserid)
    {
        string Type = string.Empty;
        try
        {
            objSWP = new SWPDashboard();
            string Deptid = "0";
            string UniqueKey = "0";
            objSWP.intUserid = intUserid;

            List<SWPDashboard> objlist = objserviceDashboard.CheckAppastatus(objSWP);
            if (objlist.Count > 0)
            {
                if (objlist[0].intStatus == 1 && objlist[0].intDistrictid == 0)//Admin
                {
                    Type = "0";
                }
                else if (objlist[0].intStatus != 1 && objlist[0].intDistrictid == 0)//IPICOL Largescale
                {
                    Type = "1";
                }
                else//MSME
                {
                    Type = "2";
                }
            }
            string serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + Pealuserid + "/" + Deptid + "/" + Type + "/" + Year + "/" + "0";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    return DashboradCommon.JsonStringToDataTable(strResult);

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Method for bind QUERY on master tracker
    /// </summary>
    /// <param name="intUserid"></param>
    /// <returns></returns>
    public List<SWPDashboard> ViewQueryMasterService(string Year ,int intSecId ,int intUserid ,string strFinacialYear) 
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "QPT";
            objSWP.Year = Year;
            objSWP.intSecId = intSecId;
            objSWP.intType = 2;
            objSWP.intUserid = intUserid;
            //objSWP.strFinacialYear = strFinacialYear;
            return objserviceDashboard.GetPEALQuery(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    #endregion

    #region ForPortletSection


    /// <summary>
    /// Method for view data for Proposal details on noramal platform
    /// </summary>  
    public List<SWPDashboard> FillProposalDetails(int intType, string strFinacialYear, int intDistrictid, int intUserid) 
    {
        try
        {
            objSWP = new SWPDashboard
            {
                strAction = "PB",
                intType = intType,
                strFinacialYear = strFinacialYear,
                intDistrictid = intDistrictid,
                intUserid = intUserid
            };
            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Method for view data for Proposal details on noramal platform for PsTourism Dashboard
    /// </summary>  
    public List<SWPDashboard> FillProposalDetails(int intType, string strFinacialYear, int intDistrictid, int intUserid ,int intSecId)
    {
        try
        {
            objSWP = new SWPDashboard
            {
                strAction = "PT",
                intQuarter = 0,
                intType = intType,
                strFinacialYear = strFinacialYear,
                intDistrictid = intDistrictid,
                intUserid = intUserid,
                intSecId= intSecId
                

            };
            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    ///Function for view proposal dist  wise on normal platform
    /// </summary>  
    public List<SWPDashboard> FillProposalDistwiseDetails(int intType, string strFinacialYear, int intDistrictid, int intUserid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "PDD";
            objSWP.intType = intType;
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intDistrictid = intDistrictid;
            objSWP.intUserid = intUserid;
            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    /// <summary>
    /// view proposal detail of IT department on normal platform
    /// </summary>    
    public List<SWPDashboard> FillProposalDetailsIT(string strFinacialYear, int intDistrictid, int intSecId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "PSE";
            objSWP.intQuarter = 0;
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intDistrictid = intDistrictid;
            objSWP.intUserid = 0;//Convert.ToInt32(Session["Userid"]);
            objSWP.intSecId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdIT"]);
            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();


        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    /// <summary>
    /// view proposal detail of Tourism department on normal platform
    /// </summary>
    public List<SWPDashboard> FillProposalDetailsTourism(string strFinacialYear, int intDistrictid, int intSecId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "PSE";
            objSWP.intQuarter = 0;
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intDistrictid = intDistrictid;
            objSWP.intUserid = 0;// Convert.ToInt32(Session["Userid"]);

            objSWP.intSecId = intSecId;
            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    /// <summary>
    /// view year wise investment on normal platfrom
    /// </summary>
    public List<SWPDashboard> FillProposalCapital(int intUserid, string strDistrictDtl, string strFinacialYear)  
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "PCI";
            objSWP.intUserid = intUserid;
            objSWP.strDistrictDtl = strDistrictDtl;
            objSWP.strFinacialYear = strFinacialYear;

            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    /// <summary>
    /// view year wise investment on normal platfrom for DI Dashboard
    /// </summary>
    public List<SWPDashboard> FillProposalCapital(int intUserid, string strDistrictDtl, string strFinacialYear ,int intType)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "PB";
            objSWP.intUserid = intUserid;
            objSWP.strDistrictDtl = strDistrictDtl;
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intType = intType;

            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    /// <summary>
    /// view year wise employeement on normal platfrom
    /// </summary>   
    public List<SWPDashboard> FillProposalEmployement(int intUserid, string strDistrictDtl, string strFinacialYear)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "PD";
            objSWP.intUserid = intUserid;
            objSWP.strDistrictDtl = strDistrictDtl;
            objSWP.strFinacialYear = strFinacialYear;
            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }


    /// <summary>
    /// view year wise employeement on normal platfrom for DI Dashboard
    /// </summary>   
    public List<SWPDashboard> FillProposalEmployement(int intUserid, string strDistrictDtl, string strFinacialYear , int intType)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "PB";
            objSWP.intUserid = intUserid;
            objSWP.strDistrictDtl = strDistrictDtl;
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intType = intType;
            return objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    /// <summary>
    /// STATE PROJECT MONITORING GROUP
    /// </summary>   
    public List<SWPDashboard> ViewStatelevelSPMGData(int intYearId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "SP";
            objSWP.intYearId = intYearId;
            return objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }


    /// <summary>
    /// STATE PROJECT MONITORING GROUP department
    /// </summary>   
    public List<SWPDashboard> ViewStatelevelSPMGDataDept(int intYearId ,int intDeptId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "DS";
            objSWP.intYearId = intYearId;
            objSWP.intDeptId = intDeptId;
            // return objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();
            return objserviceDashboard.GetSPMGdepartmentwiseStatus(objSWP).ToList();

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    /// <summary>
    /// STATE PROJECT MONITORING GROUP
    /// </summary>
    public DataTable InsertViewStatelevelSPMGData(int intUserid, int spmgyear, string recived, string resolved, string pending, string pendinglast)
    {
        DataTable PnlDt = new DataTable();
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("USP_SPMG_DASHBOARD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@INTID", SqlDbType.Int).Value = intUserid;
                    cmd.Parameters.Add("@INTYEAR", SqlDbType.Int).Value = spmgyear;
                    cmd.Parameters.Add("@INTRECIVED", SqlDbType.Int).Value = Convert.ToInt32(recived);
                    cmd.Parameters.Add("@INTRESOLVED", SqlDbType.Int).Value = Convert.ToInt32(resolved);
                    cmd.Parameters.Add("@INTPENDING", SqlDbType.Int).Value = Convert.ToInt32(pending);
                    cmd.Parameters.Add("@INTPENDING30", SqlDbType.Int).Value = Convert.ToInt32(pendinglast);
                    cmd.Parameters.Add("@VCHTYPE", SqlDbType.VarChar).Value = "1";//FOR ALL STATE LEVEL IS 1.
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }
            }
            return PnlDt;
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }



    /// <summary>
    /// STATE PROJECT MONITORING GROUP  department
    /// </summary>
    public DataTable InsertViewStatelevelSPMGDataDept(int intUserid, int spmgyear, string recived, string resolved, string pending, string pendinglast ,int strdeptid)
    {
        DataTable PnlDt = new DataTable();
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("USP_SPMG_DASHBOARD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@INTID", SqlDbType.Int).Value = intUserid;
                    cmd.Parameters.Add("@INTYEAR", SqlDbType.Int).Value = spmgyear;
                    cmd.Parameters.Add("@INTRECIVED", SqlDbType.Int).Value = Convert.ToInt32(recived);
                    cmd.Parameters.Add("@INTRESOLVED", SqlDbType.Int).Value = Convert.ToInt32(resolved);
                    cmd.Parameters.Add("@INTPENDING", SqlDbType.Int).Value = Convert.ToInt32(pending);
                    cmd.Parameters.Add("@INTPENDING30", SqlDbType.Int).Value = Convert.ToInt32(pendinglast);
                    cmd.Parameters.Add("@INTDEPT", SqlDbType.Int).Value = Convert.ToInt32(strdeptid);
                    cmd.Parameters.Add("@VCHTYPE", SqlDbType.VarChar).Value = "1D";//FOR ALL STATE LEVEL IS 1.
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }
            }
            return PnlDt;
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    /// <summary>
    /// STATE PROJECT MONITORING GROUP
    /// </summary>   
    public List<SWPDashboard> ViewDistrictlevelSPMGData(int intYearId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "SPMGDIC";
            objSWP.intYearId = intYearId;
            return objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();
           
        }
        catch (Exception ex)
        {
            throw ex;

        }


    }

    /// <summary>
    /// STATE PROJECT MONITORING GROUP for DIC dashboard master tracker
    /// </summary>

    public List<SWPDashboard> ViewDistrictlevelSPMGData( int intUserid , int intYearId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.intUserid = intUserid;
            objSWP.strAction = "SPMGDIC";
            objSWP.intYearId = intYearId;
            return objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();

        }
        catch (Exception ex)
        {
            throw ex;

        }


    }

    /// <summary>
    /// STATE PROJECT MONITORING GROUP department
    /// </summary>   
    public List<SWPDashboard> ViewDistrictlevelSPMGDataDept(int intYearId ,int intDeptId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "DD";
            objSWP.intYearId = intYearId;
            objSWP.intDeptId = intDeptId;
           // return objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();
            return objserviceDashboard.GetSPMGdepartmentwiseStatus(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;

        }


    }
    /// <summary>
    /// STATE PROJECT MONITORING GROUP
    /// </summary>    
    public DataTable InsertViewDistrictlevelSPMGData(int intUserid, int spmgyear, string recived, string resolved, string pending, string pendinglast)
    {
        DataTable PnlDt = new DataTable();
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("USP_SPMG_DASHBOARD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@INTID", SqlDbType.Int).Value = intUserid;
                    cmd.Parameters.Add("@INTYEAR", SqlDbType.Int).Value = spmgyear;
                    cmd.Parameters.Add("@INTRECIVED", SqlDbType.Int).Value = Convert.ToInt32(recived);
                    cmd.Parameters.Add("@INTRESOLVED", SqlDbType.Int).Value = Convert.ToInt32(resolved);
                    cmd.Parameters.Add("@INTPENDING", SqlDbType.Int).Value = Convert.ToInt32(pending);
                    cmd.Parameters.Add("@INTPENDING30", SqlDbType.Int).Value = Convert.ToInt32(pendinglast);
                    cmd.Parameters.Add("@VCHTYPE", SqlDbType.VarChar).Value = "2";//For ALL DIC LEVEL IS 2
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }
            }
            return PnlDt;
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }



    /// <summary>
    /// STATE PROJECT MONITORING GROUP department
    /// </summary>    
    public DataTable InsertViewDistrictlevelSPMGDataDept(int intUserid, int spmgyear, string recived, string resolved, string pending, string pendinglast , string strdeptid)
    {
        DataTable PnlDt = new DataTable();
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("USP_SPMG_DASHBOARD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@INTID", SqlDbType.Int).Value = intUserid;
                    cmd.Parameters.Add("@INTYEAR", SqlDbType.Int).Value = spmgyear;
                    cmd.Parameters.Add("@INTRECIVED", SqlDbType.Int).Value = Convert.ToInt32(recived);
                    cmd.Parameters.Add("@INTRESOLVED", SqlDbType.Int).Value = Convert.ToInt32(resolved);
                    cmd.Parameters.Add("@INTPENDING", SqlDbType.Int).Value = Convert.ToInt32(pending);
                    cmd.Parameters.Add("@INTPENDING30", SqlDbType.Int).Value = Convert.ToInt32(pendinglast);
                    cmd.Parameters.Add("@INTDEPT", SqlDbType.Int).Value = Convert.ToInt32(strdeptid);
                    cmd.Parameters.Add("@VCHTYPE", SqlDbType.VarChar).Value = "2D";//For ALL DIC LEVEL IS 2
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }
            }
            return PnlDt;
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    /// <summary>
    /// view Department Wise Approvals on normal platfrom
    /// </summary>   
    public List<SWPDashboard> ViewServiceStatus(int intDeptId, int intServiceId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "D";
            objSWP.intDeptId = intDeptId;
            objSWP.intServiceId = intServiceId;
            return objserviceDashboard.GetDashboardServiceStatusDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// view Department Wise Approvals on normal platfrom
    /// </summary>   
    public List<SWPDashboard> ViewServiceStatus(int intDeptId, int intServiceId  ,string strFinacialYear)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "EUC";
            objSWP.intDeptId = intDeptId;
            objSWP.intServiceId = intServiceId;
            objSWP.strFinacialYear = strFinacialYear;
            return objserviceDashboard.GetDashboardServiceStatusDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// view Department Wise Approvals on normal platfrom for dashboard
    /// </summary>   
    public List<SWPDashboard> ViewServiceStatus(int intDistrictid, string strFinacialYear ,int intMonthId ,int intUserid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "DGM";
            objSWP.intDistrictid = intDistrictid;
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intMonthId = intMonthId;
            objSWP.intUserid = intUserid;
            return objserviceDashboard.GetDashboardServiceStatusDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    /// <summary>
    /// view CENTRAL INSPECTION FRAMEWORK on normal platfrom
    /// </summary>   
    public List<SWPDashboard> ViewCICGData(int intUserid, int intDeptId, int intMonthId, int intYearId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "VCI";
            objSWP.intUserid = intUserid;
            objSWP.intDeptId = intDeptId;
            objSWP.intMonthId = intMonthId;
            objSWP.intYearId = intYearId;
            return objserviceDashboard.GetCICGDashboardService(objSWP).ToList();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// view IDCO POST ALLOTMENT APPLICATIONS on normal platfrom
    /// </summary>    
    public List<SWPDashboard> ViewApaaStatus(int intUserid, string Year, int intMonthId, int intDistrictid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "AP";
            objSWP.intUserid = intUserid;
            objSWP.Year = Year;
            objSWP.intMonthId = intMonthId;
            objSWP.intDistrictid = intDistrictid;
            return objserviceDashboard.GetDashboardAPAAtatus(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// view IDCO POST ALLOTMENT APPLICATIONS on normal platfrom for Dic dashboard
    /// </summary>    
    public List<SWPDashboard> ViewApaaStatusDic (int intUserid, string Year, int intMonthId, int intDistrictid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "DIC";
            objSWP.intUserid = intUserid;
            objSWP.Year = Year;
            objSWP.intMonthId = intMonthId;
            objSWP.intDistrictid = intDistrictid;
            return objserviceDashboard.GetDashboardAPAAtatus(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// view  CSR ACTIVITIES on normal platfrom
    /// </summary>    
    public DataTable CSRPortletSatus(string strYear, string strDistrict)
    {
        try
        {
            string strType = "1";
            string strUniqueKey = "0";
            string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetTotalAmount/" + strUniqueKey + "/" + strType + "/" + strDistrict + "/" + strYear;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    return DashboradCommon.JsonStringToDataTable(strResult);

                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// view INCENTIVE DETAILS on normal platfrom
    /// </summary>  
    public List<SWPDashboard> IncentiveBind(string strFinacialYear, int intDistrictid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "B";

            objSWP.intQuarter = 0;

            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intDistrictid = intDistrictid;
            objSWP.intUserid = 0;
            return objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// view INCENTIVE DETAILS on normal platfrom for DIC dashboard
    /// </summary>  
    public List<SWPDashboard> IncentiveBind(string strFinacialYear , int intDistrictid , int intQuarter )
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "B";

            objSWP.intQuarter = intQuarter;

            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intDistrictid = intDistrictid;
            objSWP.intUserid = 0;

            return objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    /// <summary>
    /// view INCENTIVE DETAILS on normal platfrom for DI dashboard
    /// </summary>  
    public List<SWPDashboard> IncentiveBind(string strFinacialYear)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "B";

            objSWP.intQuarter = 0;

            objSWP.strFinacialYear = strFinacialYear;

            objSWP.intUserid = 0;

            return objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// view LAND ALLOTMENT DETAILS on normal platfrom
    /// </summary>    
    public List<SWPDashboard> LandServiceBind(string Year)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "LANDV";
            objSWP.intUserid = 0;//Convert.ToInt32(Session["Userid"]);
            objSWP.Year = Year;
            return objserviceDashboard.GETLandDetails(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// view LAND ALLOTMENT DETAILS on normal platfrom for DIC dashboard
    /// </summary>    
    public List<SWPDashboard> LandServiceBind(string Year ,int intDistrictid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "LANDV";
            objSWP.intUserid = 0;//Convert.ToInt32(Session["Userid"]);
            objSWP.intDeptId = 0;
            objSWP.Year = Year;
            objSWP.intDistrictid = intDistrictid;
            return objserviceDashboard.GETLandDetails(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// view QUERY MONITORING on normal platfrom
    /// </summary>    
    public List<SWPDashboard> ViewQueryServicepeal(string Year)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "QPV";
            objSWP.Year = Year;

            return objserviceDashboard.GetPEALQuery(objSWP).ToList();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// view QUERY MONITORING on normal platfrom for DI Dashboard
    /// </summary>    
    public List<SWPDashboard> ViewQueryServicepeal(int intUserid , string Year)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "QPV";
            objSWP.intUserid = intUserid;
            objSWP.Year = Year;

            return objserviceDashboard.GetPEALQuery(objSWP).ToList();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// view QUERY MONITORING on normal platfrom for DI and DIC Dashboard
    /// </summary>    
    public List<SWPDashboard> ViewQueryServicepeal (string intDistrictid, string Year)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "QPV";
            objSWP.intDistrictid = Convert.ToInt32(intDistrictid);
            objSWP.Year = Year;

            return objserviceDashboard.GetPEALQuery(objSWP).ToList();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// view QUERY MONITORING on normal platfrom
    /// </summary>    
    public List<SWPDashboard> ViewQueryService(string Year)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "QV";
            objSWP.Year = Year;
            return objserviceDashboard.GetServicesQuery(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// view QUERY MONITORING on normal platfrom
    /// </summary>    
    public List<SWPDashboard> ViewQueryService(string Year ,int intDistrictid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "QV";
            objSWP.Year = Year;
            objSWP.intDistrictid = intDistrictid;
            return objserviceDashboard.GetServicesQuery(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// view QUERY MONITORING on normal platfrom of Department Dashboard 
    /// </summary>    

    public List<SWPDashboard> ViewQueryServiceDept(string Year,int intDeptId )
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "QV";
            objSWP.Year = Year;
            objSWP.intDeptId = intDeptId;
            return objserviceDashboard.GetServicesQuery(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// view QUERY MONITORING on normal platfrom of Energy Utility Dashboard 
    /// </summary>    

    public List<SWPDashboard> ViewQueryService(string Year, string intDeptId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "EQV";
            objSWP.Year = Year;
            objSWP.intDeptId =  Convert.ToInt32( intDeptId );
            return objserviceDashboard.GetServicesQuery(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// view QUERY MONITORING on normal platfrom
    /// </summary>    
    public List<SWPDashboard> ViewQueryServiceIncentive(string Year)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "IQV";
            objSWP.Year = Year;
            return objserviceDashboard.GetiNCENTIVEQuery(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// view QUERY MONITORING on normal platfrom for DIC Dashboard 
    /// </summary>    
    public List<SWPDashboard> ViewQueryServiceIncentive(string Year ,int intDistrictid)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "IQV";
            objSWP.Year = Year;
            objSWP.intDistrictid = intDistrictid;
            return objserviceDashboard.GetiNCENTIVEQuery(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for  GRIEVANCE STATUS on normal platfrom
    /// </summary>    
    public DataTable BindGrievanceportlet(int intUserid, string strFinacialYear, int intDistrictId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "DGD";
            objSWP.intUserid = intUserid;
            objSWP.strFinacialYear = strFinacialYear;
            objSWP.intDistrictId = intDistrictId;
            return objserviceDashboard.getDepartmentGrievance(objSWP);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for  AGENDA MONITORING SYSTEM on normal platfrom
    /// </summary> 
    public List<SWPDashboard> AMSBIND (string strAMSOption)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "AMS";
            objSWP.strOption = strAMSOption;
            return objserviceDashboard.GETAMS(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Method for  QUERY on normal platfrom
    /// </summary> 

    public List<SWPDashboard> ViewQueryServicepeal (string Year, int intSecId)
    {
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "QPT";
            objSWP.Year = Year;
            objSWP.intSecId = intSecId;
            objSWP.intType = 2;
            //objSWP.intUserid = intUserid;
           // objSWP.strFinacialYear = strFinacialYear;
            return objserviceDashboard.GetPEALQuery(objSWP).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }





    #endregion
}