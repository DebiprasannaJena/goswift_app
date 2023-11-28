using System.Collections.Generic;
using System.ServiceModel;
using EntityLayer.CMS;
using System.Data;

namespace BusinessLogicLayer.CMS
{
    [ServiceContract]
    public interface ICmsBusinesslayer
    {
        [OperationContract]
        IList<EntityLayer.CMS.CMSDetails> PopulateMenu();

        [OperationContract]
        string ManageCMS(CMSDetails objCMS);

        [OperationContract]
        List<CMSDetails> ViewCMS(CMSDetails objectCMS);

        [OperationContract]
        CMSDetails EditCMS(int CmsId);           
        [OperationContract]
              DataTable ChkCMSData(string action, int IntMenuId);
        [OperationContract]
        DataTable GetHeadContent(string action, int IntMenuId);
        [OperationContract]
        string AddContact(CMSDetails objCMS);
        [OperationContract]
        string AddNews(CMSDetails objCMS);
        [OperationContract]
        DataTable BindNewsEventData(string action, string strtype);
        [OperationContract]
        DataTable BindCondData(string action,int id);

        [OperationContract]
        IList<EntityLayer.CMS.CMSDetails> GetServices();

        [OperationContract]
        string ManageServiceCMS(CMSDetails objCMS);

        [OperationContract]
        IList<EntityLayer.CMS.CMSDetails> GetCMSData(CMSDetails objCMS);
        [OperationContract]
        DataTable BindDepartment(string action);
        [OperationContract]
        DataTable BindServiceData(string action,int deptid);

        [OperationContract]
        IList<EntityLayer.CMS.CMSDetails> GetWebsiteProposalDetails(CMSDetails objCMS);

        [OperationContract]
        IList<EntityLayer.CMS.CMSDetails> GetServiceManual();
        [OperationContract]
        DataTable ChkProposal(string action, int investorid);
        [OperationContract]
        DataTable GetServiceDetails(string action, int serviceid);
        [OperationContract]
        string DeleteContentData(CMSDetails objCMS);
        [OperationContract]
        string AddTemplateDetails(CMSDetails objCMS);
        [OperationContract]
        List<CMSDetails> ViewPageDetails(CMSDetails obj);
        [OperationContract]
        List<CMSDetails> GetContentDetails(CMSDetails obj);
        [OperationContract]
        string AddGlinkDetails(CMSDetails objCMS);

        [OperationContract]
        List<CMSDetails> ViewGlinkDetails(CMSDetails obj);

        [OperationContract]
        string AddPlinkDetails(CMSDetails objCMS);
        [OperationContract]
        List<CMSDetails> BindPageList(CMSDetails obj);

        [OperationContract]
        List<CMSDetails> GlinkList(CMSDetails obj);
        [OperationContract]
        List<CMSDetails> ViewPlinkDetails(CMSDetails obj);
        [OperationContract]
        DataTable BindPlinkDetails(string action);
        [OperationContract]
        DataTable BindGlinkMenuDetails(string action);
        [OperationContract]
        DataTable BindGlinkSubMenuDetails(string action, int GlinkId);
        [OperationContract]
        List<CMSDetails> GetMenuLinkDetails(CMSDetails obj);
        [OperationContract]
        string AddUseFulinkDetails(CMSDetails objCMS);
        [OperationContract]
        List<CMSDetails> UseFulLinkDetails(CMSDetails obj);
        [OperationContract]
        DataTable BindUsefulLinkDetails(string action);

        [OperationContract]
        List<CMSDetails> Dynamicheaderfooterview(CMSDetails obj);
    }
}
