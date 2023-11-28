using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Data;


namespace BusinessLogicLayer.Dashboard
{
    public interface IDashboardBusinessLayer
    {


        #region"Added by suroj"
        [OperationContract]
        List<SWPDashboard> GetDashboardProposalDtls(SWPDashboard objProposal);
        #endregion
        #region "Added by nibedita behera on 18-09-2017"
        [OperationContract]
        List<SWPDashboard> GetDashboardAPAAtatus(SWPDashboard objProposal);
        #endregion

        #region "Added by nibedita behera on 19-09-2017"
        [OperationContract]
        List<SWPDashboard> GetDashboardPEALStatusDtls(SWPDashboard objProposal);
        #endregion

        #region "Added by nibedita behera on 20-09-2017"
        [OperationContract]
        List<SWPDashboard> GetDashboardAPAAGrid(SWPDashboard objProposal);
        #endregion

        #region "Added by nibedita behera on 20-09-2017"
        [OperationContract]
        List<SWPDashboard> CheckAppastatus(SWPDashboard objProposal);
        #endregion


        #region "Added by nibedita behera on 21-09-2017"
        [OperationContract]
        List<SWPDashboard> GetDashboardServiceStatusDtls(SWPDashboard objProposal);
        #endregion

        #region "Added By Romalin Panda on 22-Sep-2017"
        [OperationContract]
        List<SWPDashboard> GetSPMGDashboardService(SWPDashboard objProposal);
        #endregion

        #region "Added by nibedita behera on 22-09-2017"
        [OperationContract]
        List<SWPDashboard> GetDashboardCSRDtls(SWPDashboard objProposal);
        [OperationContract]
        List<SWPDashboard> GetDashCSRDtls(SWPDashboard objProposal);
        [OperationContract]
        List<SWPDashboard> GetDashboardCSRCatDtls(SWPDashboard objProposal);
        [OperationContract]
        List<SWPDashboard> GetDashboardPEALDtls(SWPDashboard objProposal);
        [OperationContract]
        List<SWPDashboard> GetDashboardPEALFORMDtls(SWPDashboard objProposal);
        [OperationContract]
        List<SWPDashboard> GetDashboardCICGGrid(SWPDashboard objProposal);
        #endregion
        #region "Added By Romalin Panda on 23-Sep-2017"
        [OperationContract]
        List<SWPDashboard> GetCICGDashboardService(SWPDashboard objProposal);
        [OperationContract]
        List<SWPDashboard> GetSPMGDetailService(SWPDashboard objProposal);
        #endregion
        [OperationContract]
        List<SWPDashboard> GetServiceQuery(SWPDashboard objDashboardInfo);
        [OperationContract]
        List<SWPDashboard> FillFinacialYear(SWPDashboard objDashboardInfo);
        [OperationContract]
        int GetDepartment(string userid);
        [OperationContract]
        List<SWPDashboard> GetInvestorPealDtls(SWPDashboard objProposal);
        [OperationContract]
        List<SWPDashboard> GETAMS(SWPDashboard objProposal);
        //ADDED  BY NIBEDITA BEHERA ON 09-NOV-2017 FOR CIF DEPT ID
        [OperationContract]
        string GetCIFDepartmentid(string userid);

        [OperationContract]
        List<SWPDashboard> BindCICGDepartment(SWPDashboard objDashboardInfo);

        #region added by nibedita behera on 14-12-17 for land details service
        [OperationContract]
        List<SWPDashboard> GETLandDetails(SWPDashboard objProposal);
        #endregion

        #region Added by nibedita behera on 23-12-2017 for Query
        [OperationContract]
        List<SWPDashboard> GetPEALQuery(SWPDashboard objDashboardInfo);

        [OperationContract]
        List<SWPDashboard> GetServicesQuery(SWPDashboard objDashboardInfo);

        [OperationContract]
        List<SWPDashboard> GetiNCENTIVEQuery(SWPDashboard objDashboardInfo);

        [OperationContract]
        DataTable GetPEALQueryDetails(SWPDashboard objDashboardInfo);

        [OperationContract]
        DataTable GetServicesQueryDetails(SWPDashboard objDashboardInfo);
        #endregion
        [OperationContract]
        int GetSPMGDepartment(string deptid);
        [OperationContract]
        List<SWPDashboard> GetSPMGdepartmentwiseStatus(SWPDashboard objProposal);

        #region Added by Sushant Jena on Dt.09-Mar-2018
        string GetDeptName(int deptId);
        #endregion

        DataTable getInvestorChildUnit(SWPDashboard objDashboardInfo);
        DataTable getInvestorGrievance(SWPDashboard objDashboardInfo);
        DataTable getDepartmentGrievance(SWPDashboard objDashboardInfo);
        DataTable getGrievanceDetails(SWPDashboard objDashboardInfo);
        DataTable DepartmentGrievanceDetails(SWPDashboard objDashboardInfo);

    }
}
