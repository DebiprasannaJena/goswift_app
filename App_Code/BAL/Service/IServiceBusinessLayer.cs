using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EntityLayer.Service;

namespace BusinessLogicLayer.Service
{
    [ServiceContract]
    public interface IServiceBusinessLayer
    {
        //[OperationContract]
        //string ViewChngPwd(string name);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindService(string strAction, int Deptid);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindServiceOnlyForward(string strAction, int Deptid, int? userid,int intserviceid);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindPanel(string strAction, int FormId);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindEmployeeName(string strAction, string fullName);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindDepartment(string strAction);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> FindUserDepartment(string strAction, string strUserid);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindLineDepartment(string strAction, int Deptid);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindOffice(string strAction, int LinedeptIdDeptid);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindLocation(string strAction);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindAllDepartment(string strAction, int ParentId);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindDirectorate(string strAction, int ParentId);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindDivision(string strAction, int ParentId);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindDistrict(string strAction, int ParentId);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindUser(string strAction, int ParentId);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindAllUser(string strAction);

        [OperationContract]
        string ServiceConfigurationData(ServiceDetails objService);

        [OperationContract]
        string UpdateServiceDet(ServiceDetails objService);

        [OperationContract]
        List<ServiceDetails> ViewSErviceTakeActionDetails(ServiceDetails objService);

        [OperationContract]
        List<ServiceDetails> ViewServiceConfigurationData(ServiceDetails objService);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> ViewDepartmentWiseServiceDetails(ServiceDetails objService);
        [OperationContract]
        IList<EntityLayer.Service.ServiceDetails> PopulateDistrict();
        [OperationContract]
        IList<EntityLayer.Service.ServiceDetails> PopulateULB(int DistrictId);

        [OperationContract]
        List<ServiceDetails> DeleteServiceConfigurationData(ServiceDetails objService);

        [OperationContract]
        //List<ServiceDetails> GetAllApplicationDetails(string UserName);
        List<ServiceDetails> GetAllApplicationDetails(ServiceDetails objServiceEntity);
        // List<ServiceDetails> DeleteServiceConfigurationData(ServiceDetails objService);

        [OperationContract]
        string UploadJs(ServiceDetails objService);

        [OperationContract]
        List<ServiceDetails> ViewUploadJS(ServiceDetails objService);

        [OperationContract]
        string GetULBCode(string ApplicationNo);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> FillProposalId(int InvestorId);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> GetParticularApplicationDetails(ServiceDetails objService);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> TrakingDetailsOfTakeAction(ServiceDetails objService);

        #region "WHEN USER WANTS TO EXTEND THE QUERY DATE(Added By Pranay Kumar on 12-Sept-2017)"
        [OperationContract]
        int ExtendServiceQDate(string strAction, string strApplicationNo);
        #endregion

        [OperationContract]
        string[] UpdateExistingServiceStatus(ServiceDetails objService);

        [OperationContract]
        List<ServiceDetails> GetDepartmentofParticularUser(ServiceDetails objService);

        [OperationContract]
        List<ServiceDetails> GetAppliedApplicationNoofParticularUser(ServiceDetails objService);

        [OperationContract]
        string UpdateApplicationStatus(ServiceDetails objService);

        [OperationContract]
        List<ServiceDetails> GetApplicationNoofParticularUserDepartmentWise(ServiceDetails objService);

        [OperationContract]
        List<ServiceDetails> GetFilterApplicationDetails(ServiceDetails objService);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindDepartmentWise(ServiceDetails obj);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> DepartmentWise_Reporty(ServiceDetails obj);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> ServiceWise_Report(ServiceDetails obj);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> ApplicationWise_Report(ServiceDetails obj);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> ApplicationStatus_Report(ServiceDetails obj);
        //[OperationContract]
        //List<EntityLayer.Service.ServiceDetails> BindDepartmentWise(ServiceDetails obj);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> GetEmailAndMobile(string strAction, string vchApplicationNo);

        [OperationContract]
        List<ServiceDetails> ORTPS_SMSConfiguration(ServiceDetails objService);
        [OperationContract]
        List<EntityLayer.Service.SMSAndMAILCls> SMSAndMailStatusReport(SMSAndMAILCls objSMSClsObj);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> ViewDynSrviceEditView(ServiceDetails objService);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> DynSrviceEditUpdate(ServiceDetails objService);
        [OperationContract]
        int DynamicSrviceUpdate(ServiceDetails objService);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindAllTable();
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> BindAllColumn(string tableName);
        [OperationContract]
        int ViewDynSrviceDelete(ServiceDetails objService);
        [OperationContract]
        int DynamicSrviceInsert(ServiceDetails objService);
        [OperationContract]
        int newPanelInsert(ServiceDetails objService);
        [OperationContract]
        string AddServiceMasterDet(ServiceDetails objService);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> ViewServiceMasterDet(ServiceDetails objService);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> StausReport(ServiceDetails obj);

        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> PanelView(ServiceDetails obj);
        [OperationContract]
        List<EntityLayer.Service.ServiceDetails> PanelEdit(ServiceDetails obj);
        [OperationContract]
        List<ServiceDetails> PaymentOrderDetails(ServiceDetails objService);
        [OperationContract]
        List<ServiceDetails> FillApplicationNo();
        [OperationContract]
        List<ServiceDetails> StatusSchedule(ServiceDetails obj);
        [OperationContract]
        string AddServiceChallan(string Action, string strChallanxml, string ApplicationNo, int CretedBy, string vchOrderNo, string AmtPaid, string Overdue);
        [OperationContract]
        List<ServiceDetails> GetAllChalanDetails(ServiceDetails objService);
        [OperationContract]
        List<fileCheckCls> AllFileView(fileCheckCls obj);

        [OperationContract]
         List<ServiceDetails> PCBAutoUpdate(ServiceDetails objService);

        DataSet TrackServiceAppliactionDetail(TrackService objService); // add anil sahoo

        DataSet ViewServiceMasterByDepartMentID(ServiceDetails objService); //Added New Service 14-04-22
        int UpdateServiceByServiceId(ServiceDetails objService); //Added New Service 14-04-22
        //Add by Debiprasanna
        DataTable ViewQueryDateUpdate(ServiceDetails objService);
        //Add by Debiprasanna
        string ServiceUpdateQueryDate(ServiceDetails objService); 
    }
}
