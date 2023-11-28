using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLayer.Service;
using DataAcessLayer.Service;
using System.Data;

namespace BusinessLogicLayer.Service
{
    public class ServiceBusinessLayer : IServiceBusinessLayer
    {
        ServiceDataLayer objDataAccess = new ServiceDataLayer();

        public List<EntityLayer.Service.ServiceDetails> BindService(string strAction, int Deptid)
        {
            return objDataAccess.BindService(strAction, Deptid);
        }
        public List<EntityLayer.Service.ServiceDetails> BindServiceOnlyForward(string strAction, int Deptid, int? userid, int intserviceid)
        {
            return objDataAccess.BindServiceOnlyForward(strAction, Deptid, userid, intserviceid);
        }
        public List<EntityLayer.Service.ServiceDetails> BindPanel(string strAction, int FormId)
        {
            return objDataAccess.BindPanel(strAction, FormId);
        }
        public List<EntityLayer.Service.ServiceDetails> BindEmployeeName(string strAction, string strFullname)
        {
            return objDataAccess.BindEmployeeName(strAction, strFullname);
        }


        public List<EntityLayer.Service.ServiceDetails> BindDepartment(string strAction)
        {
            return objDataAccess.BindDepartment(strAction);
        }
        public List<EntityLayer.Service.ServiceDetails> FindUserDepartment(string strAction, string strUserid)
        {
            return objDataAccess.FindUserDepartment(strAction, strUserid);
        }
        public List<EntityLayer.Service.ServiceDetails> BindLineDepartment(string strAction, int Deptid)
        {
            return objDataAccess.BindLineDepartment(strAction, Deptid);
        }

        public List<EntityLayer.Service.ServiceDetails> BindOffice(string strAction, int LinedeptIdDeptid)
        {
            return objDataAccess.BindOffice(strAction, LinedeptIdDeptid);
        }

        public List<EntityLayer.Service.ServiceDetails> BindLocation(string strAction)
        {
            return objDataAccess.BindLocation(strAction);
        }

        public List<EntityLayer.Service.ServiceDetails> BindAllDepartment(string strAction, int ParentId)
        {
            return objDataAccess.BindAllDepartment(strAction, ParentId);
        }

        public List<EntityLayer.Service.ServiceDetails> BindDirectorate(string strAction, int ParentId)
        {
            return objDataAccess.BindDirectorate(strAction, ParentId);
        }

        public List<EntityLayer.Service.ServiceDetails> BindDivision(string strAction, int ParentId)
        {
            return objDataAccess.BindDivision(strAction, ParentId);
        }

        public List<EntityLayer.Service.ServiceDetails> BindDistrict(string strAction, int ParentId)
        {
            return objDataAccess.BindDistrict(strAction, ParentId);
        }

        public List<EntityLayer.Service.ServiceDetails> BindUser(string strAction, int LevelDetailsId)
        {
            return objDataAccess.BindUser(strAction, LevelDetailsId);
        }

        public string MappingConfigurationData(ServiceDetails objService)
        {
            return objDataAccess.MappingConfigurationData(objService);
        }

        public List<EntityLayer.Service.ServiceDetails> BindAllUser(string strAction)
        {
            return objDataAccess.BindAllUser(strAction);
        }

        public string ServiceConfigurationData(ServiceDetails objService)
        {
            return objDataAccess.ServiceConfigurationData(objService);
        }

        public string UpdateServiceDet(ServiceDetails objService)
        {
            return objDataAccess.UpdateServiceDet(objService);
        }

        public List<ServiceDetails> ViewSErviceTakeActionDetails(ServiceDetails objService)
        {
            return objDataAccess.ViewSErviceTakeActionDetails(objService);
        }

        public List<ServiceDetails> ViewServiceConfigurationData(ServiceDetails objService)
        {
            return objDataAccess.ViewServiceConfigurationData(objService);
        }
        public List<EntityLayer.Service.ServiceDetails> ViewDepartmentWiseServiceDetails(ServiceDetails objService)
        {
            return objDataAccess.ViewDepartmentWiseServiceDetails(objService);
        }
        //public string ViewChngPwd(string name)
        //{
        //    return "Hi ........................" + name;
        //}

        public IList<ServiceDetails> PopulateDistrict()
        {
            return objDataAccess.PopulateDistrict();
        }

        public IList<ServiceDetails> PopulateULB(int DistrictId)
        {
            return objDataAccess.PopulateULB(DistrictId);
        }


        public List<ServiceDetails> DeleteServiceConfigurationData(ServiceDetails objService)
        {
            return objDataAccess.DeleteServiceConfigurationData(objService);
        }

        public List<ServiceDetails> GetAllApplicationDetails(ServiceDetails objServiceEntity)
        {
            return objDataAccess.GetAllApplicationDetails(objServiceEntity);
        }
        public List<ServiceDetails> GetAllDraftedApplicationDetails(string UserName)
        {
            return objDataAccess.GetAllDraftedApplicationDetails(UserName);
        }
        public string UploadJs(ServiceDetails objService)
        {
            return objDataAccess.UploadJs(objService);
        }

        public List<ServiceDetails> ViewUploadJS(ServiceDetails objService)
        {
            return objDataAccess.ViewUploadJS(objService);
        }
        public string GetULBCode(string ApplicationNo)
        {
            return objDataAccess.GetULBCode(ApplicationNo);
        }
        public List<EntityLayer.Service.ServiceDetails> FillProposalId(int InvestorId)
        {
            return objDataAccess.FillProposalId(InvestorId);
        }

        public List<EntityLayer.Service.ServiceDetails> GetParticularApplicationDetails(ServiceDetails objService)
        {
            return objDataAccess.GetParticularApplicationDetails(objService);
        }
        public List<EntityLayer.Service.ServiceDetails> TrakingDetailsOfTakeAction(ServiceDetails objService)
        {
            return objDataAccess.TrakingDetailsOfTakeAction(objService);
        }
        #region "WHEN USER WANTS TO EXTEND THE QUERY DATE(Added By Pranay Kumar on 12-Sept-2017)"
        public int ExtendServiceQDate(string strAction, string strApplicationNo)
        {
            return objDataAccess.ExtendServiceQDate(strAction, strApplicationNo);
        }
        #endregion

        public string[] UpdateExistingServiceStatus(ServiceDetails objService)
        {
            return objDataAccess.UpdateExistingServiceStatus(objService);
        }
        public List<ServiceDetails> GetDepartmentofParticularUser(ServiceDetails objService)
        {
            return objDataAccess.GetDepartmentofParticularUser(objService);
        }
        public List<ServiceDetails> GetAppliedApplicationNoofParticularUser(ServiceDetails objService)
        {
            return objDataAccess.GetAppliedApplicationNoofParticularUser(objService);
        }
        public string UpdateApplicationStatus(ServiceDetails objService)
        {
            return objDataAccess.UpdateApplicationStatus(objService);
        }

        public List<ServiceDetails> GetApplicationNoofParticularUserDepartmentWise(ServiceDetails objService)
        {
            return objDataAccess.GetApplicationNoofParticularUserDepartmentWise(objService);
        }
        public List<ServiceDetails> GetFilterApplicationDetails(ServiceDetails objService)
        {
            return objDataAccess.GetFilterApplicationDetails(objService);
        }

        public List<ServiceDetails> BindDepartmentWise(ServiceDetails obj)
        {
            return objDataAccess.BindDepartmentWise(obj);
        }


        public List<ServiceDetails> DepartmentWise_Reporty(ServiceDetails obj)
        {
            return objDataAccess.DepartmentWise_Reporty(obj);
        }


        public List<ServiceDetails> ServiceWise_Report(ServiceDetails obj)
        {
            return objDataAccess.ServiceWise_Report(obj);
        }


        public List<ServiceDetails> ApplicationWise_Report(ServiceDetails obj)
        {
            return objDataAccess.ApplicationWise_Report(obj);
        }


        public List<ServiceDetails> ApplicationStatus_Report(ServiceDetails obj)
        {
            return objDataAccess.ApplicationStatus_Report(obj);
        }
        //public List<ServiceDetails> BindDepartmentWise(ServiceDetails obj)
        //{
        //    return objDataAccess.BindDepartmentWise(obj);
        //}
        public List<EntityLayer.Service.ServiceDetails> GetEmailAndMobile(string strAction, string vchApplicationNo)
        {
            return objDataAccess.GetEmailAndMobile(strAction, vchApplicationNo);
        }
        public List<ServiceDetails> ORTPS_SMSConfiguration(ServiceDetails objService)
        {
            return objDataAccess.ORTPS_SMSConfiguration(objService);
        }
        public List<EntityLayer.Service.SMSAndMAILCls> SMSAndMailStatusReport(SMSAndMAILCls objSMSClsObj)
        {
            return objDataAccess.SMSAndMailStatusReport(objSMSClsObj);
        }
        public List<EntityLayer.Service.ServiceDetails> ViewDynSrviceEditView(ServiceDetails objService)
        {
            return objDataAccess.ViewDynSrviceEditView(objService);
        }
        public List<EntityLayer.Service.ServiceDetails> DynSrviceEditUpdate(ServiceDetails objService)
        {
            return objDataAccess.DynSrviceEditUpdate(objService);
        }
        public int DynamicSrviceUpdate(ServiceDetails objService)
        {
            return objDataAccess.DynamicSrviceUpdate(objService);
        }
        public List<EntityLayer.Service.ServiceDetails> BindAllTable()
        {
            return objDataAccess.BindAllTable();
        }
        public List<EntityLayer.Service.ServiceDetails> BindAllColumn(string tableName)
        {
            return objDataAccess.BindAllColumn(tableName);
        }
        public int ViewDynSrviceDelete(ServiceDetails objService)
        {
            return objDataAccess.ViewDynSrviceDelete(objService);
        }
        public int DynamicSrviceInsert(ServiceDetails objService)
        {
            return objDataAccess.DynamicSrviceInsert(objService);
        }

        public List<EntityLayer.Service.ServiceDetails> ViewServiceMasterDet(ServiceDetails objService)
        {
            return objDataAccess.ViewServiceMasterDet(objService);
        }

        public string AddServiceMasterDet(ServiceDetails objService)
        {
            return objDataAccess.AddServiceMasterDet(objService);
        }

        public int newPanelInsert(ServiceDetails objService)
        {
            return objDataAccess.newPanelInsert(objService);
        }
        public List<ServiceDetails> StausReport(ServiceDetails obj)
        {
            return objDataAccess.StausReport(obj);
        }
        public List<ServiceDetails> StatusSchedule(ServiceDetails obj)
        {
            return objDataAccess.StatusSchedule(obj);
        }
        public List<EntityLayer.Service.ServiceDetails> PanelView(ServiceDetails obj)
        {
            return objDataAccess.PanelView(obj);
        }
        public List<EntityLayer.Service.ServiceDetails> PanelEdit(ServiceDetails obj)
        {
            return objDataAccess.PanelEdit(obj);
        }
        public List<ServiceDetails> PaymentOrderDetails(ServiceDetails objService)
        {
            return objDataAccess.PaymentOrderDetails(objService);
        }
        public List<ServiceDetails> FillApplicationNo()
        {
            return objDataAccess.FillApplicationNo();
        }


        public string AddServiceChallan(string Action, string strChallanxml, string ApplicationNo, int CretedBy, string vchOrderNo, string AmtPaid, string Overdue)
        {
            return objDataAccess.AddServiceChallan(Action, strChallanxml, ApplicationNo, CretedBy, vchOrderNo, AmtPaid, Overdue);
        }


        public List<ServiceDetails> GetAllChalanDetails(ServiceDetails objService)
        {
            return objDataAccess.GetAllChalanDetails(objService);
        }
        public List<fileCheckCls> AllFileView(fileCheckCls obj)
        {
            return objDataAccess.AllFileView(obj);
        }
        public List<ServiceDetails> PCBAutoUpdate(ServiceDetails objService)
        {
            return objDataAccess.PCBAutoUpdate(objService);
        }

        #region Add anil sahoo
        public DataSet TrackServiceAppliactionDetail(TrackService objService)   //  For Application Track Service 
        {
            return objDataAccess.TrackServiceAppliactionDetail(objService);
        }
        #endregion

        public DataTable GetApplicationByTrackingId(string strTrackingId)
        {
            return objDataAccess.GetApplicationByTrackingId(strTrackingId);
        }

        public DataSet ViewServiceMasterByDepartMentID(ServiceDetails objService) //Added New Service 14-04-22
        {

            try
            {
                return objDataAccess.ViewServiceMasterByDepartMentID(objService);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateServiceByServiceId(ServiceDetails objService) //Added New Service 14-04-22
        {
            try
            {
                return objDataAccess.UpdateServiceByServiceId(objService);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Add by Debiprasanna
        public DataTable ViewQueryDateUpdate(ServiceDetails objService)
        {
            try
            {
                return objDataAccess.ViewQueryDateUpdate(objService);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Add by Debiprasanna
        public string ServiceUpdateQueryDate(ServiceDetails objService) 
        {
            try
            {
                return objDataAccess.ServiceUpdateQueryDate(objService);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}