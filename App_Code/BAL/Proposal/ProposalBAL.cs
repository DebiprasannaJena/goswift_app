using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcessLayer.Proposal;
using EntityLayer.Proposal;
using System.Data;

namespace BusinessLogicLayer.Proposal
{
    public class ProposalBAL : IProposalBAL
    {
        ProposalDataLayer objDataAccess = new ProposalDataLayer();

        #region Added By Subhasmita Behera on 18-Aug-2017

        public string ProposalPromoterAED(EntityLayer.Proposal.PromoterDet objProposal)
        {
            return objDataAccess.ProposalPromoterAED(objProposal);
        }

        public List<EntityLayer.Proposal.PromoterDet> GetCompanyDetails(EntityLayer.Proposal.PromoterDet objProposal)
        {
            return objDataAccess.GetCompanyDetails(objProposal);
        }

        public List<EntityLayer.Proposal.PromoterDet> GetPromoterNameDetails(EntityLayer.Proposal.PromoterDet objProposal)
        {
            return objDataAccess.GetPromoterNameDetails(objProposal);
        }

        public List<EntityLayer.Proposal.PromoterDet> GetNameDesgDetails(EntityLayer.Proposal.PromoterDet objProposal)
        {
            return objDataAccess.GetNameDesgDetails(objProposal);
        }

        public List<EntityLayer.Proposal.PromoterDet> GetRawMetrialDetails(EntityLayer.Proposal.PromoterDet objProposal)
        {
            return objDataAccess.GetRawMetrialDetails(objProposal);
        }

        public List<EntityLayer.Proposal.PromoterDet> GetEnclosureDetails(EntityLayer.Proposal.PromoterDet objProposal)
        {
            return objDataAccess.GetEnclosureDetails(objProposal);
        }

        public DataTable GetGcNewWorthDetails(EntityLayer.Proposal.ProjectInfo objProposal)
        {
            return objDataAccess.GetGcNewWorthDetails(objProposal);
        }

        public DataTable GetTotalNetWorth(EntityLayer.Proposal.PromoterDet objProposal)
        {
            return objDataAccess.GetTotalNetWorth(objProposal);
        }

        #endregion

        #region Added By Priti

        public string ProjectInfoAED(EntityLayer.Proposal.ProjectInfo objprop)
        {
            return objDataAccess.ProjectInfoAED(objprop);
        }
        public List<EntityLayer.Proposal.ProjectInfo> getProjectLOCDetails(EntityLayer.Proposal.ProjectInfo objprop)
        {
            return objDataAccess.getProjectLOCDetails(objprop);
        }
        public List<EntityLayer.Proposal.ProjectInfo> getOtherUnitlDetails(EntityLayer.Proposal.ProjectInfo objprop)
        {
            return objDataAccess.getOtherUnitlDetails(objprop);
        }
        public List<EntityLayer.Proposal.ProjectInfo> PopulateProjDropdowns(EntityLayer.Proposal.ProjectInfo objprop)
        {
            return objDataAccess.PopulateProjDropdowns(objprop);
        }
        public List<EntityLayer.Proposal.ProposalDet> getProposalDetails(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.getProposalDetails(objProposal);
        }
        //Added by nibedita behera on 17-09-2017for admin view proposal
        public List<EntityLayer.Proposal.ProposalDet> getAdminProposalDetails(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.getAdminProposalDetails(objProposal);
        }

        public string ProposalTakeAction(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.ProposalTakeAction(objProposal);
        }
        public List<EntityLayer.Proposal.ProposalDet> PopulateStatus(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.PopulateStatus(objProposal);
        }
        #region "Raise Query"
        public string ProposalRaiseQuery(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.ProposalRaiseQuery(objProposal);
        }
        public List<EntityLayer.Proposal.ProposalDet> getRaisedQueryDetails(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.getRaisedQueryDetails(objProposal);
        }
        #endregion
        #endregion

        #region Added By Pradeep
        public string ProposalLandAED(EntityLayer.Proposal.LandDet objProposal)
        {
            return objDataAccess.ProposalLandAED(objProposal);
        }
        //Added by nibedita behera on 16-09-2017

        public string UpdateLandApproval(EntityLayer.Proposal.LandDet objProposal)
        {
            return objDataAccess.UpdateLandApproval(objProposal);
        }

        public List<EntityLayer.Proposal.LandDet> ViewLandDetails(EntityLayer.Proposal.LandDet objProposal)
        {
            return objDataAccess.ViewLandDetails(objProposal);
        }
        #endregion

        public string Declartion(EntityLayer.Proposal.Declartion objdec)
        {
            return objDataAccess.Declartion(objdec);
        }

        public List<EntityLayer.Proposal.Declartion> GetDeclartionData(EntityLayer.Proposal.Declartion objDec)
        {
            return objDataAccess.GetDeclartionData(objDec);
        }

        public List<EntityLayer.Proposal.PromoterDet> ViewCompanyInformation(EntityLayer.Proposal.PromoterDet objCompInfo)
        {
            return objDataAccess.ViewCompanyInformation(objCompInfo);
        }

        public List<EntityLayer.Proposal.LandDet> ViewLandUtility(EntityLayer.Proposal.LandDet objLand)
        {
            return objDataAccess.ViewLandUtility(objLand);
        }

        public List<EntityLayer.Proposal.LandDet> Industrial(EntityLayer.Proposal.LandDet objprop)
        {
            return objDataAccess.Industrial(objprop);
        }

        public List<EntityLayer.Proposal.PromoterDet> Intermidiate(EntityLayer.Proposal.PromoterDet objprop)
        {
            return objDataAccess.Intermidiate(objprop);
        }

        #region "Raise Query Service"

        public string ServiceProposalRaiseQuery(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.ServiceProposalRaiseQuery(objProposal);
        }
        public List<EntityLayer.Proposal.ProposalDet> ServicegetRaisedQueryDetails(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.ServicegetRaisedQueryDetails(objProposal);
        }

        #endregion

        public List<EntityLayer.Proposal.CNET> GetCNETCompanyDetails(EntityLayer.Proposal.CNET objProposal)
        {
            return objDataAccess.GetCNETCompanyDetails(objProposal);
        }
        public List<EntityLayer.Proposal.ProjectInfo> getProjectInfoDetails(EntityLayer.Proposal.ProjectInfo objprop)
        {
            return objDataAccess.getProjectInfoDetails(objprop);
        }
        public List<EntityLayer.Proposal.ProjectInfo> GetProposalDtls(EntityLayer.Proposal.ProjectInfo objprop)
        {

            return objDataAccess.GetProposalDtls(objprop);
        }
        public string AddProposalDtls(EntityLayer.Proposal.ProjectInfo objprop)
        {
            return objDataAccess.AddProposalDtls(objprop);
        }

        public string ProposalCNETData(EntityLayer.Proposal.PromoterDet objProposal)
        {
            return objDataAccess.ProposalCNETData(objProposal);
        }

        #region "Added By Pranay Kumar on 10-Sept-2017"
        #region "Check Raise Query Date withing Date Limit"
        public int CheckRaiseQStatus(EntityLayer.Proposal.ProposalDet objprop)
        {
            return objDataAccess.CheckRaiseQStatus(objprop);
        }
        #endregion
        #region "WHEN USER WANTS TO EXTEND THE QUERY DATE"
        public int intExtendDate(string strAction, int intProposalNo)
        {
            return objDataAccess.intExtendDate(strAction, intProposalNo);
        }
        #endregion
        #endregion


        public List<EntityLayer.Proposal.LandDet> GETMobileNo(EntityLayer.Proposal.LandDet objprop)
        {
            return objDataAccess.GETMobileNo(objprop);
        }

        public List<EntityLayer.Proposal.LandDet> GETApplicationNoByOrderNo(EntityLayer.Proposal.LandDet objprop)
        {
            return objDataAccess.GETApplicationNoByOrderNo(objprop);
        }

        public List<EntityLayer.Proposal.LandDet> GETMobileNoOfUser(EntityLayer.Proposal.LandDet objprop)
        {
            return objDataAccess.GETMobileNoOfUser(objprop);
        }

        public List<EntityLayer.Proposal.ProposalDet> getProposalDetailsMIS(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.getProposalDetailsMIS(objProposal);
        }

        public string ForwardLandToIDCO(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.ForwardLandToIDCO(objProposal);
        }
        public string ProposalAMStatusUpdate(EntityLayer.Proposal.PromoterDet objProposal)
        {
            return objDataAccess.ProposalAMStatusUpdate(objProposal);
        }
        public string ProposalIDCOtatusUpdate(EntityLayer.Proposal.PromoterDet objProposal)
        {
            return objDataAccess.ProposalIDCOtatusUpdate(objProposal);
        }
        public System.Data.DataTable GetParentData(string Query)
        {
            return objDataAccess.GetParentData(Query);
        }
        public System.Data.DataTable GetChildData(string Query, int val)
        {
            return objDataAccess.GetChildData(Query, val);
        }
        public List<EntityLayer.Proposal.ProposalDet> GetMISAllStatus(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.GetMISAllStatus(objProposal);
        }

        public string ProposalEnclosurUpdate(EntityLayer.Proposal.PromoterDet objProposal)
        {
            return objDataAccess.ProposalEnclosurUpdate(objProposal);
        }
        public List<EntityLayer.Proposal.ProposalDet> GetMISAllDetailsStatus(EntityLayer.Proposal.ProposalDet objProposal)
        {
            return objDataAccess.GetMISAllDetailsStatus(objProposal);
        }

        public string ProposalESIGNPromoterAED(PromoterDet objProposal)
        {
            return objDataAccess.ProposalESIGNPromoterAED(objProposal);
        }

        public List<LandDet> GetAMSUserId(LandDet objLandDet)
        {
            return objDataAccess.GetAMSUserId(objLandDet);
        }

        public List<ProjectInfo> getProductNameDetails(ProjectInfo objprop)
        {
            return objDataAccess.getProductNameDetails(objprop);
        }
        public string PEALServiceOrderUpdate(PromoterDet objProposal)
        {
            return objDataAccess.PEALServiceOrderUpdate(objProposal);
        }

        public List<PromoterDet> GetIDCOMISDetailsRPT(PromoterDet objProposal)
        {
            return objDataAccess.GetIDCOMISDetailsRPT(objProposal);
        }

        public string IDCOBtntatusUpdate(PromoterDet objProposal)
        {
            return objDataAccess.IDCOBtntatusUpdate(objProposal);
        }

        public List<PromoterDet> GetIDCOEmailDetails()
        {
            return objDataAccess.GetIDCOEmailDetails();
        }

        #region Added By Satya On 16-04-2019 for Industry List

        public DataTable IndustryListDetails(ProposalDet objProposal)
        {
            return objDataAccess.IndustryListDetails(objProposal);
        }

        #endregion
        #region Added By MANOJ On 08-05-2019 for Industry List

        public DataTable ALLSERVICEDETAILS(ProposalDet objProposal)
        {
            return objDataAccess.ALLSERVICEDETAILS(objProposal);
        }

        #endregion

        #region Added By MANOJ On 22-05-2019 for UPDATE INDYSTRY INFO
        public DataTable INVESTORINFODISPLAY(ProposalDet objProposal)
        {
            return objDataAccess.INVESTORINFODISPLAY(objProposal);
        }
        public string INVESTORINFOUPDATE(ProposalDet objProposal)
        {
            return objDataAccess.INVESTORINFOUPDATE(objProposal);
        }
        #endregion

        public DataTable GetCAFDetailsNSWS(PromoterDet objProposal)
        {
            return objDataAccess.GetCAFDetailsNSWS(objProposal);
        }

        #region anil sahoo
        public DataSet GetProposalTrackDetails(ProposalDet objProposal)
        {
            return objDataAccess.GetProposalTrackDetails(objProposal);
        }
        #endregion
        #region Debiprasanna
        //Add By Debiprasnna
        public DataTable QueryDateUpdate(ProposalDet objProposal)
        {
            return objDataAccess.QueryDateUpdate(objProposal);
        }
        #endregion
        //Add By Debiprasnna
        public string UpdateQueryDate(EntityLayer.Proposal.ProposalDet objProposal) 
        {
            return objDataAccess.UpdateQueryDate(objProposal);
        }
    }
}
