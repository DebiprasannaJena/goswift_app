using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataAcessLayer.HelpDeskDataLayer;
using System.Data;
/// <summary>
/// Summary description for HelpDeskBusinessLayer
/// </summary>
/// 

namespace BusinessLogicLayer.HelpDesk
{
    public class HelpDeskBusinessLayer : IHelpdeskBusinessLayer
    {
        HelpDeskDataLayer objDataAccess = new HelpDeskDataLayer();

        public List<IssueRegistration> BindCategory(IssueRegistration objDdl)
        {
            return objDataAccess.BindCategory(objDdl);
        }
        public List<IssueRegistration> BindType(IssueRegistration objDdl)
        {
            return objDataAccess.BindType(objDdl);
        }

        public List<IssueRegistration> BindSubCategory(IssueRegistration objDdl)
        {
            return objDataAccess.BindSubCategory(objDdl);
        }


        public List<IssueRegistration> BindDepartment(IssueRegistration objDdl)
        {
            return objDataAccess.BindDepartment(objDdl);
        }


        public List<IssueRegistration> BindUser(IssueRegistration objDdl)
        {
            return objDataAccess.BindUser(objDdl);
        }


        public List<IssueRegistration> BindInvestor(IssueRegistration objDdl)
        {
            return objDataAccess.BindInvestor(objDdl);
        }


        public List<IssueRegistration> AddIssueRegister(IssueRegistration objMasterSector)
        {
            return objDataAccess.AddIssueRegister(objMasterSector);
        }


        public List<IssueRegistration> ViewIssueRegistration(IssueRegistration obj)
        {
            return objDataAccess.ViewIssueRegistration(obj);
        }


        public List<IssueRegistration> ViewFile(IssueRegistration obj)
        {
            return objDataAccess.ViewFile(obj);
        }


        public List<IssueRegistration> HelpdeskCategory_Report(IssueRegistration obj)
        {
            return objDataAccess.HelpdeskCategory_Report(obj);
        }


        public List<IssueRegistration> HelpdeskSubCategory_Report(IssueRegistration obj)
        {
            return objDataAccess.HelpdeskSubCategory_Report(obj);
        }


        public List<IssueRegistration> Helpdesk_Report(IssueRegistration obj)
        {
            return objDataAccess.Helpdesk_Report(obj);
        }


        public List<IssueRegistration> Helpdesk_Status_Report(IssueRegistration obj)
        {
            return objDataAccess.Helpdesk_Status_Report(obj);
        }


        public string AddIssueCategory(IssueRegistration objMaster)
        {
            return objDataAccess.AddIssueCategory(objMaster);
        }





        public List<IssueRegistration> ViewIssueCategory(IssueRegistration obj)
        {
            return objDataAccess.ViewIssueCategory(obj);
        }

        public string AddIssueSubCategory(IssueRegistration objMaster)
        {
            return objDataAccess.AddIssueSubCategory(objMaster);
        }

        public List<IssueRegistration> ViewIssueSubCategory(IssueRegistration obj)
        {
            return objDataAccess.ViewIssueSubCategory(obj);
        }
        public List<IssueRegistration> ViewIssueintimation(IssueRegistration obj)
        {
            return objDataAccess.ViewIssueintimation(obj);
        }
        public List<IssueRegistration> FillAuthority(IssueRegistration obj)
        {
            return objDataAccess.FillAuthority(obj);
        }
        public string AddHDEscalationConfiguration(IssueRegistration obj)
        {
            return objDataAccess.AddHDEscalationConfiguration(obj);
        }
        public int CountEscalationLevel(IssueRegistration objMaster)
        {
            return objDataAccess.CountEscalationLevel(objMaster);
        }
        public List<IssueRegistration> ViewConfigEscalation(IssueRegistration obj)
        {
            return objDataAccess.ViewConfigEscalation(obj);
        }
        public List<IssueRegistration> AutoEscalationProcess(IssueRegistration objMaster)
        {
             return objDataAccess.AutoEscalationProcess(objMaster);
        }
        public List<IssueRegistration> ViewpopConfigEscalation(IssueRegistration obj)
        {
            return objDataAccess.ViewpopConfigEscalation(obj);
        }
        public List<IssueRegistration> EditViewConfigEscalation(IssueRegistration obj)
        {
            return objDataAccess.EditViewConfigEscalation(obj);
        }


        public List<IssueRegistration> ViewEscalationEmailRegistration(IssueRegistration obj)
        {
            return objDataAccess.ViewEscalationEmailRegistration(obj);
        }


        public List<IssueRegistration> ViewIssueRegistrationMIS(IssueRegistration obj)
        {
            return objDataAccess.ViewIssueRegistrationMIS(obj);
        }


        public List<IssueRegistration> GetEmailID(IssueRegistration obj)
        {
            return objDataAccess.GetEmailID(obj);
        }
    }
}