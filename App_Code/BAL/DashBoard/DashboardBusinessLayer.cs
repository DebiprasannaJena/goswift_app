using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcessLayer.DashboardDataLayer;
using System.Data;

namespace BusinessLogicLayer.Dashboard
{
    public class DashboardBusinessLayer : IDashboardBusinessLayer
    {
        DashboardDataLayer objDataAccess = new DashboardDataLayer();

        public List<SWPDashboard> GetDashboardProposalDtls(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashboardProposalDtls(objdashboard);
        }
        #region "Added By nibedita behera on 18-07-2017"
        public List<SWPDashboard> GetDashboardAPAAtatus(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashboardAPAAtatus(objdashboard);
        }
        #endregion

        #region "Added By nibedita behera on 19-09-2017"
        public List<SWPDashboard> GetDashboardPEALStatusDtls(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashboardPEALStatusDtls(objdashboard);
        }
        #endregion

        #region "Added By nibedita behera on 20-07-2017"
        public List<SWPDashboard> GetDashboardAPAAGrid(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashboardAPAAGrid(objdashboard);
        }
        #endregion
        #region "Added By nibedita behera on 20-07-2017"
        public List<SWPDashboard> CheckAppastatus(SWPDashboard objdashboard)
        {
            return objDataAccess.CheckAppastatus(objdashboard);
        }
        #endregion

        #region "Added By romalin Panda on 22-09-2017"
        public List<SWPDashboard> GetDashboardServiceStatusDtls(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashboardServiceStatusDtls(objdashboard);
        }
        #endregion
        #region "Added By Romalin Panda on 22-Sep-2017"
        public List<SWPDashboard> GetSPMGDashboardService(SWPDashboard objdashboard)
        {
            return objDataAccess.GetSPMGDashboardService(objdashboard);
        }
        #endregion

        #region "Added By Nibedita Behera on 22-Sep-2017"
        public List<SWPDashboard> GetDashboardCSRDtls(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashboardCSRDtls(objdashboard);
        }
        public List<SWPDashboard> GetDashCSRDtls(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashCSRDtls(objdashboard);
        }
        public List<SWPDashboard> GetDashboardCSRCatDtls(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashboardCSRCatDtls(objdashboard);
        }
        public List<SWPDashboard> GetDashboardPEALDtls(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashboardPEALDtls(objdashboard);
        }
        public List<SWPDashboard> GetDashboardPEALFORMDtls(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashboardPEALFORMDtls(objdashboard);
        }
        public List<SWPDashboard> GetDashboardCICGGrid(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashboardCICGGrid(objdashboard);
        }
        #endregion
        #region "Added By Romalin Panda on 23-Sep-2017"
        public List<SWPDashboard> GetCICGDashboardService(SWPDashboard objdashboard)
        {
            return objDataAccess.GetCICGDashboardService(objdashboard);
        }

        public List<SWPDashboard> GetSPMGDetailService(SWPDashboard objdashboard)
        {
            return objDataAccess.GetSPMGDetailService(objdashboard);
        }
        #endregion
        public List<SWPDashboard> GetServiceQuery(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.GetServiceQuery(objDashboardInfo);
        }
        public List<SWPDashboard> FillFinacialYear(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.FillFinacialYear(objDashboardInfo);
        }
        #region "Added By SUROJ KUMAR PRADHAN FOR INCENTIVE BIND"
        public List<SWPDashboard> GetDashboardServiceIncentiveDtls(SWPDashboard objdashboard)
        {
            return objDataAccess.GetDashboardServiceIncentiveDtls(objdashboard);
        }
        #endregion
        public int GetDepartment(string userid)
        {
            return objDataAccess.GetDepartment(userid);
        }

        public List<SWPDashboard> GetInvestorPealDtls(SWPDashboard objdashboard)
        {
            return objDataAccess.GetInvestorPealDtls(objdashboard);
        }

        //added buy suroj for Agenda Monitoring System
        public List<SWPDashboard> GETAMS(SWPDashboard objdashboard)
        {
            return objDataAccess.GETAMS(objdashboard);
        }

        //ADDED  BY NIBEDITA BEHERA ON 09-NOV-2017 FOR CIF DEPT ID
        public string GetCIFDepartmentid(string userid)
        {
            return objDataAccess.GetCIFDepartmentid(userid);
        }
        public List<SWPDashboard> BindCICGDepartment(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.BindCICGDepartment(objDashboardInfo);
        }

        // added by nibedita behera on 13-12-2017
        public List<SWPDashboard> GETLandDetails(SWPDashboard objdashboard)
        {
            return objDataAccess.GETLandDetails(objdashboard);
        }

        //ADDED BY NIBEDITA BEHERA ON 20-12-2017
        public List<SWPDashboard> GetPEALQuery(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.GetPEALQuery(objDashboardInfo);
        }
        public List<SWPDashboard> GetServicesQuery(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.GetServicesQuery(objDashboardInfo);
        }

        public List<SWPDashboard> GetiNCENTIVEQuery(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.GetiNCENTIVEQuery(objDashboardInfo);
        }

        public DataTable GetPEALQueryDetails(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.GetPEALQueryDetails(objDashboardInfo);
        }
        public DataTable GetServicesQueryDetails(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.GetServicesQueryDetails(objDashboardInfo);
        }
        public int GetSPMGDepartment(string deptid)
        {
            return objDataAccess.GetSPMGDepartment(deptid);
        }
        public List<SWPDashboard> GetSPMGdepartmentwiseStatus(SWPDashboard objdashboard)
        {
            return objDataAccess.GetSPMGdepartmentwiseStatus(objdashboard);
        }

        #region Added by Sushant Jena on Dt.09-Mar-2018

        public string GetDeptName(int deptId)
        {
            return objDataAccess.GetDeptName(deptId);
        }

        #endregion

        #region Added by Sushant Jena

        public DataTable getInvestorChildUnit(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.getInvestorChildUnit(objDashboardInfo);
        }

        #endregion

        public DataTable getInvestorGrievance(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.getInvestorGrievance(objDashboardInfo);
        }
        public DataTable getDepartmentGrievance(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.getDepartmentGrievance(objDashboardInfo);
        }
        public DataTable getGrievanceDetails(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.getGrievanceDetails(objDashboardInfo); 
        }

        public DataTable DepartmentGrievanceDetails(SWPDashboard objDashboardInfo)
        {
            return objDataAccess.DepartmentGrievanceDetails(objDashboardInfo); 
        }
    }
}
