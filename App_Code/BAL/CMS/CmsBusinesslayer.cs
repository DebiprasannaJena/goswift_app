using System.Collections.Generic;
using EntityLayer.CMS;
using DataAcessLayer.CMS;
using System.Data;

namespace BusinessLogicLayer.CMS
{
    public class CmsBusinesslayer:ICmsBusinesslayer
    {
        CMSDataLayer objDataAccess = new CMSDataLayer();



        public IList<EntityLayer.CMS.CMSDetails> PopulateMenu()
        {
           return objDataAccess.PopulateMenu();
        }


        public string ManageCMS(CMSDetails objCMS)
        {
            return objDataAccess.ManageCMS(objCMS);
        }


        public List<CMSDetails> ViewCMS(CMSDetails objectCMS)
        {
            return objDataAccess.ViewCMS(objectCMS);
        }


        public CMSDetails EditCMS(int CmsId)
        {
            return objDataAccess.EditCMS(CmsId);
        }
        public DataTable ChkCMSData(string action, int IntMenuId)
        {
            return objDataAccess.ChkCMSData(action, IntMenuId);
        }
        public DataTable GetHeadContent(string action, int IntMenuId)
        {
            return objDataAccess.GetHeadContent(action, IntMenuId);
        }
        public string AddContact(CMSDetails objCMS)
        {
            return objDataAccess.AddContact(objCMS);
        }
        public string AddNews(CMSDetails objCMS)
        {
            return objDataAccess.AddNews(objCMS);
        }
             public DataTable BindNewsEventData(string action, string strtype)
        {
            return objDataAccess.BindNewsEventData(action, strtype);
        }
        public DataTable BindCondData(string action, int id)
        {
            return objDataAccess.BindCondData(action, id);
        }
        public IList<EntityLayer.CMS.CMSDetails> GetServices()
        {
            return objDataAccess.GetServices();
        }
        public string ManageServiceCMS(CMSDetails objCMS)
        {
            return objDataAccess.ManageServiceCMS(objCMS);
        }
        public IList<EntityLayer.CMS.CMSDetails> GetCMSData(CMSDetails objCMS)
        {
            return objDataAccess.GetCMSData(objCMS);
        }
        public DataTable BindDepartment(string action)
        {
            return objDataAccess.BindDepartment(action);
        }
        public DataTable BindServiceData(string action, int deptid)
        {
            return objDataAccess.BindServiceData(action, deptid);
        }
        public IList<EntityLayer.CMS.CMSDetails> GetWebsiteProposalDetails(CMSDetails objCMS)
        {
            return objDataAccess.GetWebsiteProposalDetails(objCMS);
        }
        public IList<EntityLayer.CMS.CMSDetails> GetServiceManual()
        {
            return objDataAccess.GetServiceManual();
        }
        public DataTable ChkProposal(string action, int investorid)
        {
            return objDataAccess.ChkProposal(action, investorid);
        }
        public DataTable GetServiceDetails(string action, int serviceid)
        {
            return objDataAccess.GetServiceDetails(action, serviceid);
        }
        public string DeleteContentData(CMSDetails objCMS)
        {
            return objDataAccess.DeleteContentData(objCMS);
        }


        public string AddTemplateDetails(CMSDetails objCMS)
        {
            return objDataAccess.AddTemplateDetails(objCMS);
        }

        public List<CMSDetails> ViewPageDetails(CMSDetails obj)
        {
            return objDataAccess.ViewPageDetails(obj);
        }


        public List<CMSDetails> GetContentDetails(CMSDetails obj)
        {
            return objDataAccess.GetContentDetails(obj);

        }


        public string AddGlinkDetails(CMSDetails objCMS)
        {
            return objDataAccess.AddGlinkDetails(objCMS);
        }

        public List<CMSDetails> ViewGlinkDetails(CMSDetails obj)
        {
            return objDataAccess.ViewGlinkDetails(obj);
        }


        public string AddPlinkDetails(CMSDetails objCMS)
        {
            return objDataAccess.AddPlinkDetails(objCMS);
        }

        public List<CMSDetails> BindPageList(CMSDetails obj)
        {
            return objDataAccess.BindPageList(obj);
        }

        public List<CMSDetails> GlinkList(CMSDetails obj)
        {
            return objDataAccess.GlinkList(obj);
        }

        public List<CMSDetails> ViewPlinkDetails(CMSDetails obj)
        {
            return objDataAccess.ViewPlinkDetails(obj);
        }



        public DataTable BindPlinkDetails(string action)
        {
            return objDataAccess.BindPlinkDetails(action);
        }


        public DataTable BindGlinkMenuDetails(string action)
        {
            return objDataAccess.BindGlinkMenuDetails(action);
        }
        public DataTable BindGlinkSubMenuDetails(string action, int GlinkId)
        {
            return objDataAccess.BindGlinkSubMenuDetails(action, GlinkId);
        }


        public List<CMSDetails> GetMenuLinkDetails(CMSDetails obj)
        {
            return objDataAccess.GetMenuLinkDetails(obj);
        }


        public string AddUseFulinkDetails(CMSDetails objCMS)
        {
            return objDataAccess.AddUseFulinkDetails(objCMS);
        }

        public List<CMSDetails> UseFulLinkDetails(CMSDetails obj)
        {
            return objDataAccess.UseFulLinkDetails(obj);
        }


        public DataTable BindUsefulLinkDetails(string action)
        {
            return objDataAccess.BindUsefulLinkDetails(action);
        }
        public List<TemplateDetails> TemplateContentDetails(TemplateDetails obj)
        {
            return objDataAccess.TemplateContentDetails(obj);
        }
        public string UpdateTemplateDetails(TemplateDetails obj)
        {
            return objDataAccess.UpdateTemplateDetails(obj);
        }
        public string TemplateContentCount(TemplateDetails obj)
        {
            return objDataAccess.TemplateContentCount(obj);
        }
        public string PrimaryLinkName(TemplateDetails obj)
        {
            return objDataAccess.PrimaryLinkName(obj);
        }
        public List<CMSDetails> Dynamicheaderfooterview(CMSDetails obj)
        {
            return objDataAccess.Dynamicheaderfooterview(obj);
        }
        public string AddHeaderDetails(CMSDetails objHeader)
        {
            return objDataAccess.AddHeaderDetails(objHeader);
        }
        public string AddFooterDetails(CMSDetails objFooter)
        {
            return objDataAccess.AddFooterDetails(objFooter);
        }
        public DataTable DynamicHeaderview(CMSDetails obj)
        {
            return objDataAccess.DynamicHeaderview(obj);
        }
        public DataTable DynamicFooterview(CMSDetails obj)
        {
            return objDataAccess.DynamicFooterview(obj);
        }
        public List<CMSDetails> ContactusDetails(CMSDetails obj)
        {
            return objDataAccess.ContactusDetails(obj);
        }
    }
}
