using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Incentive;
using Common.Persistence.Data;
using System.Data.SqlClient;
using System.Data;
using CommonDataExtensionHelper;
/// <summary>
/// Summary description for IncentiveProvider
/// </summary>
public class IncentiveProvider : IIncentiveProvider
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    public string CreateIncentive(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 
                                "@SectionNo_1", sectionList[1],
                                "@SectionNo_2", sectionList[2], 
                                "@SectionNo_3", sectionList[3],
                                "@SectionNo_4", sectionList[4],
                                "@SectionNo_5", sectionList[5],
                                "@SectionNo_6", sectionList[6],
                                "@SectionNo_7", sectionList[7],
                                "@SectionNo_8", sectionList[8],
                                "@SectionNo_9", sectionList[9],
                                "@SectionNo_10", sectionList[10],
                                "@SectionNo_11", sectionList[11],
                                "@SectionNo_12", sectionList[12],
                                "@SectionNo_13", sectionList[13],
                                "@SectionNo_14", sectionList[14],
                                "@SectionNo_15", sectionList[15],
                                "@SectionNo_16", sectionList[16],
                                "@SectionNo_17", sectionList[17],
                                "@SectionNo_18", sectionList[18],
                                "@SectionNo_19", sectionList[19],
                                "@SectionNo_20", sectionList[20],
                                "@SectionNo_21", sectionList[21],
                                "@SectionNo_22", sectionList[22],
                                "@SectionNo_23", sectionList[23],
                                "@SectionNo_24", sectionList[24],
                                "@SectionNo_25", sectionList[25],
                                "@SectionNo_26", sectionList[26],


                                //Parameters for Industrial Unit's Details",,
                                "@P_INTINDUSTRAILUNIT",incentive.IndsutUnitMstDet.INDUSTRAILUNIT_IND==null ? 0 : incentive.IndsutUnitMstDet.INDUSTRAILUNIT_IND,
                                "@P_VCHENTERPRISENAME",incentive.IndsutUnitMstDet.ENTERPRISENAME_IND==null ? "" : incentive.IndsutUnitMstDet.ENTERPRISENAME_IND,
                                "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? 0 : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,

                                "@P_INTORGANISATIONTYPE",incentive.IndsutUnitMstDet.ORGANISATIONTYPE_IND==null ? 0 : incentive.IndsutUnitMstDet.ORGANISATIONTYPE_IND,

                                "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,
                                "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ? 0 : incentive.IndsutUnitMstDet.APPLYBY_IND,
                                "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? 0 : incentive.IndsutUnitMstDet.AADHAARNO_IND,
                                "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? 0 : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                                "@P_VCHINDUSTRYADDRESS",incentive.IndsutUnitMstDet.INDUSTRYADDRESS_IND==null ? 0 : incentive.IndsutUnitMstDet.INDUSTRYADDRESS_IND,
                                "@P_INTDISTID",incentive.IndsutUnitMstDet.DISTID_IND==null ? 0 : incentive.IndsutUnitMstDet.DISTID_IND,
                                "@P_VCHREGISTEREDOFCADDRESS",incentive.IndsutUnitMstDet.REGISTEREDOFCADDRESS_IND==null ? 0 : incentive.IndsutUnitMstDet.REGISTEREDOFCADDRESS_IND,
                                "@P_VCHREGISTRATIONFILENAME",incentive.IndsutUnitMstDet.REGISTRATIONFILENAME_IND==null ? 0 : incentive.IndsutUnitMstDet.REGISTRATIONFILENAME_IND,
                                "@P_VCHCOMPANYDATE",incentive.IndsutUnitMstDet.COMPANYDATE_IND==null ? "1900/01/01" : incentive.IndsutUnitMstDet.COMPANYDATE_IND,
                                "@P_VCHCOMPANYPLACE",incentive.IndsutUnitMstDet.COMPANYPLACE_IND==null ? 0 : incentive.IndsutUnitMstDet.COMPANYPLACE_IND,
                                "@P_VCHREGISTRATIONNO",incentive.IndsutUnitMstDet.REGISTRATIONNO_IND==null ? 0 : incentive.IndsutUnitMstDet.REGISTRATIONNO_IND,
                                "@P_VCHLICENSENO",incentive.IndsutUnitMstDet.LICENSENO_IND==null ? 0 : incentive.IndsutUnitMstDet.LICENSENO_IND,
                                "@P_VCHCOMPANYCIN",incentive.IndsutUnitMstDet.COMPANYCIN_IND==null ? 0 : incentive.IndsutUnitMstDet.COMPANYCIN_IND,
                                "@P_VCHCOMPANYPAN",incentive.IndsutUnitMstDet.COMPANYPAN_IND==null ? 0 : incentive.IndsutUnitMstDet.COMPANYPAN_IND,
                                "@P_VCHTINVAT",incentive.IndsutUnitMstDet.TINVAT_IND==null ? 0 : incentive.IndsutUnitMstDet.TINVAT_IND,

                                "@P_INTCATAGORYUNIT",incentive.IndsutUnitMstDet.CATAGORYUNIT_IND==null ? 0 : incentive.IndsutUnitMstDet.CATAGORYUNIT_IND,
                                "@P_INTUNITTYPE",incentive.IndsutUnitMstDet.UNITTYPE_IND==null ? 0 : incentive.IndsutUnitMstDet.UNITTYPE_IND,
                                "@P_VCHREHABILITATEDDOCUMENT",incentive.IndsutUnitMstDet.REHABILITATEDDOCUMENT_IND==null ? 0 : incentive.IndsutUnitMstDet.REHABILITATEDDOCUMENT_IND,
                                "@P_VCHINDUSTRIALDOCUMENT",incentive.IndsutUnitMstDet.INDUSTRIALDOCUMENT_IND==null ? 0 : incentive.IndsutUnitMstDet.INDUSTRIALDOCUMENT_IND,
                                "@P_INTPRIORITY",incentive.IndsutUnitMstDet.PRIORITY_IND==null ? 0 : incentive.IndsutUnitMstDet.PRIORITY_IND,

                                "@P_INTPIONEER",incentive.IndsutUnitMstDet.PIONEER_IND==null ? 0 : incentive.IndsutUnitMstDet.PIONEER_IND,
                                "@P_VCHADDRESSREGISTEROFFICE",incentive.IndsutUnitMstDet.ADDRESSREGISTEROFFICE_IND==null ? 0 : incentive.IndsutUnitMstDet.ADDRESSREGISTEROFFICE_IND,
                                "@P_VCHPIONEERCERTIFICATE",incentive.IndsutUnitMstDet.PIONEERCERTIFICATE_IND==null ? 0 : incentive.IndsutUnitMstDet.PIONEERCERTIFICATE_IND,

                                "@P_VCHMANAGINGPARTNERNAME",incentive.IndsutUnitMstDet.MANAGINGPARTNERNAME_IND==null ? 0 : incentive.IndsutUnitMstDet.MANAGINGPARTNERNAME_IND,
                                "@P_VCHMANAGINGPARTNERGENDER",incentive.IndsutUnitMstDet.MANAGINGPARTNERGENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.MANAGINGPARTNERGENDER_IND,
                                "@P_VCHCERTIFICATEOFREGISTRATION",incentive.IndsutUnitMstDet.CERTIFICATEOFREGISTRATION_IND==null ? 0 : incentive.IndsutUnitMstDet.CERTIFICATEOFREGISTRATION_IND,
                                "@P_VCHEINNO",incentive.IndsutUnitMstDet.EINNO_IND==null ? 0 : incentive.IndsutUnitMstDet.EINNO_IND,
                                "@P_DTMEIN",incentive.IndsutUnitMstDet.EINDATE_IND==null ? "1900/01/01" : incentive.IndsutUnitMstDet.EINDATE_IND,
                                "@P_VCHPCNO_INDUSTRIAL",incentive.IndsutUnitMstDet.PCNO_INDUSTRIAL_IND==null ? 0 : incentive.IndsutUnitMstDet.PCNO_INDUSTRIAL_IND,
                                "@P_DTMPCISSUANCE",incentive.IndsutUnitMstDet.PCISSUANCE_IND==null ? "1900/01/01" : incentive.IndsutUnitMstDet.PCISSUANCE_IND,
                                "@P_DTMCOMMENCEMENT",incentive.IndsutUnitMstDet.COMMENCEMENT_IND==null ? "1900/01/01" : incentive.IndsutUnitMstDet.COMMENCEMENT_IND,
                                "@P_VCHCOMMENCEMENTCERTIFICATE",incentive.IndsutUnitMstDet.COMMENCEMENTCERTIFICATE_IND==null ? 0 : incentive.IndsutUnitMstDet.COMMENCEMENTCERTIFICATE_IND,
                                "@P_VCHTINNO",incentive.IndsutUnitMstDet.TINNO_IND==null ? "" : incentive.IndsutUnitMstDet.TINNO_IND,
                                "@P_VCHTINDATE",incentive.IndsutUnitMstDet.TINDATE_IND==null ? "1900/01/01" : incentive.IndsutUnitMstDet.TINDATE_IND,
                                "@P_VCHTINDOCUMENT",incentive.IndsutUnitMstDet.TINDOCUMENT_IND==null ? "" : incentive.IndsutUnitMstDet.TINDOCUMENT_IND,

                                ////Parameters for Production & Employment Details",,


                                "@P_INTDIRECTEMP",incentive.ProdEmpDet.DIRECTEMP==null ? 0 : incentive.ProdEmpDet.DIRECTEMP,
                                "@P_INTCONTRACTUALEMP",incentive.ProdEmpDet.CONTRACTUALEMP==null ? 0 : incentive.ProdEmpDet.CONTRACTUALEMP,
                                "@P_VCHEMPDOC",incentive.ProdEmpDet.EMPDOC==null ? "" : incentive.ProdEmpDet.EMPDOC,
                                "@P_INTCURRENTMANAGERIAL",incentive.ProdEmpDet.CURRENTMANAGERIAL==null ? 0 : incentive.ProdEmpDet.CURRENTMANAGERIAL,
                                "@P_INTPROPOSEDMANAGERIAL",incentive.ProdEmpDet.PROPOSEDMANAGERIAL==null ? 0 : incentive.ProdEmpDet.PROPOSEDMANAGERIAL,
                                "@P_INTCURRENTSUPERVISORY",incentive.ProdEmpDet.CURRENTSUPERVISORY==null ? 0 : incentive.ProdEmpDet.CURRENTSUPERVISORY,
                                "@P_INTPROPOSEDSUPERVISORY",incentive.ProdEmpDet.PROPOSEDSUPERVISORY==null ? 0 : incentive.ProdEmpDet.PROPOSEDSUPERVISORY,
                                "@P_INTCURRENTSKILLED",incentive.ProdEmpDet.CURRENTSKILLED==null ? 0 : incentive.ProdEmpDet.CURRENTSKILLED,
                                "@P_INTPROPOSEDSKILLED",incentive.ProdEmpDet.PROPOSEDSKILLED==null ? 0 : incentive.ProdEmpDet.PROPOSEDSKILLED,
                                "@P_INTCURRENTSEMISKILLED",incentive.ProdEmpDet.CURRENTSEMISKILLED==null ? 0 : incentive.ProdEmpDet.CURRENTSEMISKILLED,
                                "@P_INTPROPOSEDSEMISKILLED",incentive.ProdEmpDet.PROPOSEDSEMISKILLED==null ? 0 : incentive.ProdEmpDet.PROPOSEDSEMISKILLED,
                                "@P_INTCURRENTUNSKILLED",incentive.ProdEmpDet.CURRENTUNSKILLED==null ? 0 : incentive.ProdEmpDet.CURRENTUNSKILLED,
                                "@P_INTPROPOSEDUNSKILLED",incentive.ProdEmpDet.PROPOSEDUNSKILLED==null ? 0 : incentive.ProdEmpDet.PROPOSEDUNSKILLED,
                                "@P_INTCURRENTTOTAL",incentive.ProdEmpDet.CURRENTTOTAL==null ? 0 : incentive.ProdEmpDet.CURRENTTOTAL,
                                "@P_INTPROPOSEDTOTAL",incentive.ProdEmpDet.PROPOSEDTOTAL==null ? 0 : incentive.ProdEmpDet.PROPOSEDTOTAL,
                                "@P_DTMPRODUCTION",incentive.ProdEmpDet.PRODUCTION==null ? "" : incentive.ProdEmpDet.PRODUCTION,
                                "@P_VCHLOCATION",incentive.ProdEmpDet.LOCATION==null ? "" : incentive.ProdEmpDet.LOCATION,
                                "@P_VCHSTATUS",incentive.ProdEmpDet.STATUS==null ? "" : incentive.ProdEmpDet.STATUS,
                                "@P_VCHCOMMENCEDOC",incentive.ProdEmpDet.COMMENCEDOC==null ? "" : incentive.ProdEmpDet.COMMENCEDOC,
                                "@P_DTMPRODUCTIONPROPOSED",incentive.ProdEmpDet.PRODUCTIONPROPOSED==null ? "" : incentive.ProdEmpDet.PRODUCTIONPROPOSED,
                                "@P_VCHDPRDOC",incentive.ProdEmpDet.DPRDOC==null ? "" : incentive.ProdEmpDet.DPRDOC,
                                "@P_VCHESIOREPFREGNO",incentive.ProdEmpDet.ESIOREPFREGNO==null ? "" : incentive.ProdEmpDet.ESIOREPFREGNO,
                                "@P_DTMESIOREPF",incentive.ProdEmpDet.ESIOREPF==null ? "" : incentive.ProdEmpDet.ESIOREPF,
                                "@P_VCHESIOREPFDOC",incentive.ProdEmpDet.ESIOREPFDOC==null ? "" : incentive.ProdEmpDet.ESIOREPFDOC,

                                "@P_BITESI",incentive.ProdEmpDet.ESI==null ? 0 : incentive.ProdEmpDet.ESI,
                                "@P_BITEPF",incentive.ProdEmpDet.EPF==null ? 0 : incentive.ProdEmpDet.EPF,
                                "@P_INT_STATUS",incentive.ProdEmpDet.STATUSFORDFTAPPLY==null ? 0 : incentive.ProdEmpDet.STATUSFORDFTAPPLY,
                                "@P_XMLINNER",incentive.ProdEmpDet.PRODUCTIONITEMS==null ? "" : incentive.ProdEmpDet.PRODUCTIONITEMS.SerializeToXMLString(),



                                //////Parameters for Investment Details",,

                                "@P_INT_IND_Investment_Details",incentive.InvestmentDet.INT_IND_Investment_Details==null ? 0 : incentive.InvestmentDet.INT_IND_Investment_Details,
                                "@P_DTM_IND_Date_of_First_Fixed",incentive.InvestmentDet.DTM_IND_Date_of_First_Fixed==null ? Convert.ToDateTime("1/1/1900") : incentive.InvestmentDet.DTM_IND_Date_of_First_Fixed,
                                "@P_VCH_Document_in_support",incentive.InvestmentDet.Document_in_support==null ? "" : incentive.InvestmentDet.Document_in_support,
                                "@P_VCH_LAND_TYPE",incentive.InvestmentDet.LAND_TYPE==null ? "" : incentive.InvestmentDet.LAND_TYPE,
                                "@P_VCH_Other_Fixed_Assests",incentive.InvestmentDet.Other_Fixed_Assests==null ? "" : incentive.InvestmentDet.Other_Fixed_Assests,
                                "@P_VCH_Building",incentive.InvestmentDet.Building==null ? "" : incentive.InvestmentDet.Building,
                                "@P_DEC_LAND_TYPE_AMOUNT",incentive.InvestmentDet.LAND_TYPE_AMOUNT==null ? 0 : incentive.InvestmentDet.LAND_TYPE_AMOUNT,
                                "@P_DEC_Building",incentive.InvestmentDet.Building_AMOUNT==null ? 0 : incentive.InvestmentDet.Building_AMOUNT,
                                "@P_VCH_Plant_Machinery",incentive.InvestmentDet.Plant_Machinery==null ? "" : incentive.InvestmentDet.Plant_Machinery,
                                "@P_DEC_Plant_Machinery",incentive.InvestmentDet.Plant_Machinery_AMOUNTR==null ? 0 : incentive.InvestmentDet.Plant_Machinery_AMOUNTR,
                                "@P_VCH_Balancing_Equipment",incentive.InvestmentDet.Balancing_Equipment==null ? "" : incentive.InvestmentDet.Balancing_Equipment,
                                "@P_DEC_Balancing_Equipment",incentive.InvestmentDet.Balancing_Equipment_AMOUNT==null ? 0 : incentive.InvestmentDet.Balancing_Equipment_AMOUNT,
                                "@P_DEC_Other_Fixed_Assests",incentive.InvestmentDet.Other_Fixed_Assests_AMOUNT==null ? 0 : incentive.InvestmentDet.Other_Fixed_Assests_AMOUNT,
                                "@P_DEC_Total",incentive.InvestmentDet.Total==null ? 0 : incentive.InvestmentDet.Total,
                                "@P_INT_INDUSTRAILUNIT",incentive.InvestmentDet.INDUSTRAILUNIT==null ? 0 : incentive.InvestmentDet.INDUSTRAILUNIT,
                                "@P_VCH_ELECTRIC_INSTALL",incentive.InvestmentDet.Electric_install ==null ?"":incentive.InvestmentDet.Electric_install,
                                "@P_DEC_ELECTRIC_INSTALL",incentive.InvestmentDet.Electric_install_AMOUNT  ==null ?0:incentive.InvestmentDet.Electric_install_AMOUNT,
                                "@P_VCH_LOADING",incentive.InvestmentDet.Loading==null ?"":incentive.InvestmentDet.Loading,
                                "@P_DEC_LOADING",incentive.InvestmentDet.LoadingAmount==null ?0:incentive.InvestmentDet.LoadingAmount,
                                "@P_VCH_IDCOSHED",incentive.InvestmentDet.IDCOShed==null ?"":incentive.InvestmentDet.IDCOShed,
                                "@P_DEC_IDCOSHED",incentive.InvestmentDet.IDCOShedAmount==null ?0:incentive.InvestmentDet.IDCOShedAmount,
                                "@P_VCH_LAB",incentive.InvestmentDet.LAB==null?"":incentive.InvestmentDet.LAB,							
                                "@P_DEC_LAB",incentive.InvestmentDet.LAB_AMOUNT ==null?0:incentive.InvestmentDet.LAB_AMOUNT,							
                                "@P_DEC_LAND_TYPE_EMD",incentive.InvestmentDet.LAND_TYPE_EMD==null?0:incentive.InvestmentDet.LAND_TYPE_EMD,				
                                "@P_DEC_Building_EMD",incentive.InvestmentDet.Building_EMD  ==null?0:incentive.InvestmentDet.Building_EMD,				
                                "@P_DEC_Plant_Machinery_EMD",incentive.InvestmentDet.Plant_Machinery_EMD ==null?0:incentive.InvestmentDet.Plant_Machinery_EMD,
                                "@P_DEC_Balancing_Equipment_EMD",incentive.InvestmentDet.Balancing_Equipment_EMD==null?0:incentive.InvestmentDet.Balancing_Equipment_EMD,			
                                "@P_DEC_Other_Fixed_Assests_EMD",incentive.InvestmentDet.Other_Fixed_Assests_EMD ==null?0:incentive.InvestmentDet.Other_Fixed_Assests_EMD,		
                                "@P_DEC_ELECTRIC_INSTALL_EMD",incentive.InvestmentDet.ELECTRIC_INSTALL_EMD==null?0:incentive.InvestmentDet.ELECTRIC_INSTALL_EMD,			
                                "@P_DEC_LOADING_EMD",incentive.InvestmentDet.LAND_TYPE_EMD ==null?0:incentive.InvestmentDet.LAND_TYPE_EMD,				
                                "@P_DEC_IDCOSHED_EMD",incentive.InvestmentDet.IDCOSHED_EMD==null?0:incentive.InvestmentDet.IDCOSHED_EMD,					
                                "@P_DEC_LAB_EMD",incentive.InvestmentDet.LAND_TYPE_EMD ==null?0:incentive.InvestmentDet.LAND_TYPE_EMD,						
                                "@P_DTM_IND_Date_of_First_Fixed_EMD",incentive.InvestmentDet.IND_Date_of_First_Fixed_EMD ==null?Convert.ToDateTime("1/1/1900"):incentive.InvestmentDet.IND_Date_of_First_Fixed_EMD, 
                                "@P_VCH_PROJECTDOC",incentive.InvestmentDet.PROJECTDOC==null ?"":incentive.InvestmentDet.PROJECTDOC,
                                "@P_VCH_MACHINERYDOC",incentive.InvestmentDet.MACHINERYDOC==null ?"":incentive.InvestmentDet.MACHINERYDOC,


                                //////Parameters for erest Subsidy Details",,
                                ////"@DEC_ERESTSUBSIDY",incentive.IntrstSubsidyDet.decInterestSubsidy==null ? 0 : incentive.IntrstSubsidyDet.decInterestSubsidy,
                                ////"@_PERIODCLAIM",incentive.IntrstSubsidyDet.intPeriodClaim==null ? 0 : incentive.IntrstSubsidyDet.intPeriodClaim,
                                ////"@DEC_DIFFERAMTCLAIM",incentive.IntrstSubsidyDet.decDifferAmtClaim==null ? 0 : incentive.IntrstSubsidyDet.decDifferAmtClaim,
                                ////"@VCH_DOC_DIFFERAMTCLAI",incentive.IntrstSubsidyDet.DocDifferAmtClaim==null ? "" : incentive.IntrstSubsidyDet.DocDifferAmtClaim,
                                ////"@DEC_REIMBURSEMENT",incentive.IntrstSubsidyDet.decReImbursement==null ? 0 : incentive.IntrstSubsidyDet.decReImbursement,
                                ////"@VCH_SUPPOTIMPLEMENETDOC",incentive.IntrstSubsidyDet.SuppotImplemenetDoc==null ? "" : incentive.IntrstSubsidyDet.SuppotImplemenetDoc,

                                ////"@VCH_LSTERESTLOAN",incentive.IntrstSubsidyDet.lstInterestLoan==null ? "" : incentive.IntrstSubsidyDet.lstInterestLoan.SerializeToXMLString(),
                                ////"@VCH_LSTSANCTIONSUBSIDY",incentive.IntrstSubsidyDet.lstSanctionSubsidy==null ? "" : incentive.IntrstSubsidyDet.lstSanctionSubsidy.SerializeToXMLString(),
                                ////"@VCH_LSTSTATUTORYCLEARENCES",incentive.IntrstSubsidyDet.lstStatutoryClearences==null ? "" : incentive.IntrstSubsidyDet.lstStatutoryClearences.SerializeToXMLString(),

                                                            
                                ////Parameters for Priority Sector Details",,
                                "@P_INT_CERTAVAIL",incentive.PrioritySector.intAvailPriorityCertf==null ? 0 : incentive.PrioritySector.intAvailPriorityCertf,
                                "@P_VCH_SECTORCERT",incentive.PrioritySector.strPrioritycCertf2015==null ? "" : incentive.PrioritySector.strPrioritycCertf2015,
                                "@P_VCH_ACKNOW",incentive.PrioritySector.strAppcnAcknow==null ? "" : incentive.PrioritySector.strAppcnAcknow,


                                //Parameters for Additional Documents",,

                                "@P_INT_OSPCBNOC",incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC,
                                "@P_INT_OSPCBCONSENT",incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent,
                                "@P_INT_OSPCBEXCISE",incentive.AdditionalDocument.intStatutoryCleanCentralExec==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanCentralExec,
                                "@P_INT_OSPCBFSHGSCD",incentive.AdditionalDocument.intStatutoryCleanFSHGSCD==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanFSHGSCD,
                                "@P_INT_OSPCBEXPOLSIVE",incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC,
                                "@P_VCH_STATUTORYCLEARANCE",incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB==null ? "" : incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB,
                                "@P_VCHSTCLEANCONSENTOSPCB",incentive.AdditionalDocument.strStCleanConsentOSPCB==null ? "" : incentive.AdditionalDocument.strStCleanConsentOSPCB,
                                "@P_VCHCLEARANCECETIFTOSPCB",incentive.AdditionalDocument.strClearanceCetiftOSPCB==null ? "" : incentive.AdditionalDocument.strClearanceCetiftOSPCB,
                                "@P_VCHVALIDSATUTORYGREENCATEGORY",incentive.AdditionalDocument.strValidSatutoryGreenCategory==null ? "" : incentive.AdditionalDocument.strValidSatutoryGreenCategory,
                                "@P_VCHCONDODOCUMENTATIONDELAY",incentive.AdditionalDocument.strCondoDocumentationDelay==null ? "" : incentive.AdditionalDocument.strCondoDocumentationDelay,
                                "@P_XML_FILES",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(), 

                                //////Parameters for Patent Details",,
                                ////"@PatentId",incentive.PatentDet.intPatentId==null ? 0 : incentive.PatentDet.intPatentId,
                                ////"@itemsDetails",incentive.PatentDet.lstitemsDetails==null ? "" : incentive.PatentDet.lstitemsDetails.SerializeToXMLString(),


                                //////Parameters for Availed Details",,

                                ////"@UndertakingSubsidyDoc",incentive.AvailDet.UndertakingSubsidyDoc==null ? "" : incentive.AvailDet.UndertakingSubsidyDoc,
                                ////"@SubsidyAvailed",incentive.AvailDet.SubsidyAvailed==null ? 0 : incentive.AvailDet.SubsidyAvailed,
                                ////"@SupportingDocs",incentive.AvailDet.SupportingDocs==null ? "" : incentive.AvailDet.SupportingDocs,
                                ////"@ClaimtExempted",incentive.AvailDet.ClaimtExempted==null ? 0 : incentive.AvailDet.ClaimtExempted,
                                ////"@ClaimReimbursement",incentive.AvailDet.ClaimReimbursement==null ? 0 : incentive.AvailDet.ClaimReimbursement,
                                ////"@AssistanceDetails",incentive.AvailDet.AssistanceDetails==null ? "" : incentive.AvailDet.AssistanceDetails.SerializeToXMLString(),
                                ////"@IncentiveAvailed",incentive.AvailDet.IncentiveAvailed==null ? "" : incentive.AvailDet.IncentiveAvailed.SerializeToXMLString(),

                                //////Parameters for Availed Claim Details",,

                                ////"@P__AVAIL_CLAIM_ID",incentive.AvailClaimDet.AVAIL_CLAIM_ID==null ? 0 : incentive.AvailClaimDet.AVAIL_CLAIM_ID,
                                ////"@P_DEC_DIFF_AMOUNT_CLAIM",incentive.AvailClaimDet.DIFF_AMOUNT_CLAIM==null ? 0 : incentive.AvailClaimDet.DIFF_AMOUNT_CLAIM,
                                ////"@P_VCH_SANCTION_FILE",incentive.AvailClaimDet.SANCTION_FILE==null ? "" : incentive.AvailClaimDet.SANCTION_FILE,
                                ////"@P__DATA",incentive.AvailClaimDet.ListAvailedClaimDetailsInfo==null ? "" : incentive.AvailClaimDet.ListAvailedClaimDetailsInfo.SerializeToXMLString(),

                                //////Parameters for Bank Details",,

                                "@P_INTBANKID",incentive.BankDet.BankId==null ? 0 : incentive.BankDet.BankId,
                                "@P_VCHACCOUNTNO",incentive.BankDet.AccountNo==null ? "" : incentive.BankDet.AccountNo,
                                "@P_VCHBANKNAME",incentive.BankDet.BankName==null ? "" : incentive.BankDet.BankName,
                                "@P_VCHBRANCHNAME",incentive.BankDet.BranchName==null ? "" : incentive.BankDet.BranchName,
                                "@P_VCHIFSCNO",incentive.BankDet.IFSCNo==null ? "" : incentive.BankDet.IFSCNo,
                                "@P_VCHMICR",incentive.BankDet.MICRNo==null ? "" : incentive.BankDet.MICRNo,


                                //////Parameters for Stamp Duty Exemption",,
                                ////"@VCHTypeofDeed",incentive.StampDutyDet.TypeofDeed==null ? "" : incentive.StampDutyDet.TypeofDeed,
                                ////"@VCHOriginalDeedDoc",incentive.StampDutyDet.OriginalDeedDoc==null ? "" : incentive.StampDutyDet.OriginalDeedDoc,


                                //////Parameters for Technical Know How Claim Details",,
                                ////"@P__TECHNICAL_CLAIM",incentive.TechnicalKnowDet.INT_TECHNICAL_CLAIM==null ? 0 : incentive.TechnicalKnowDet.INT_TECHNICAL_CLAIM,
                                ////"@P_VCH_BRIEF_ON_TECHNICA",incentive.TechnicalKnowDet.STR_BRIEF_ON_TECHNICAL==null ? "" : incentive.TechnicalKnowDet.STR_BRIEF_ON_TECHNICAL,
                                ////"@P_TechnicalKnowHowClaim_",incentive.TechnicalKnowDet.Technicalknowdetails==null ? "" : incentive.TechnicalKnowDet.Technicalknowdetails.SerializeToXMLString(),

                                //////Parameters for Course Details",,

                                ////"@P__CD_CourseDetails",incentive.CourseDet.IntCourseDetails==null ? 0 : incentive.CourseDet.IntCourseDetails,
                                ////"@P_VCH_CD_Institution_Name",incentive.CourseDet.InstitutionName==null ? "" : incentive.CourseDet.InstitutionName,
                                ////"@P_VCH_CD_Institution_Address",incentive.CourseDet.InstitutionAddress==null ? "" : incentive.CourseDet.InstitutionAddress,
                                ////"@P_VCH_CD_Course_Duratio",incentive.CourseDet.CourseDuratio==null ? "" : incentive.CourseDet.CourseDuratio,
                                ////"@P_DEC_CD_Course_Amount",incentive.CourseDet.CourseAmount==null ? 0 : incentive.CourseDet.CourseAmount,
                                ////"@P_VCH_CD_Course_Attachment",incentive.CourseDet.CourseAttachment==null ? "" : incentive.CourseDet.CourseAttachment,
                                ////"@P_DTM_CD__of_selection",incentive.CourseDet.Dateofselection==null ? Convert.ToDateTime("1/1/1900") : incentive.CourseDet.Dateofselection,
                                ////"@P_VCH_CD_Copy_of_letterofselection",incentive.CourseDet.Copyofletterofselection==null ? "" : incentive.CourseDet.Copyofletterofselection,
                                ////"@P_DTM_CD_Excepted_of_course",incentive.CourseDet.Excepteddateofcourse==null ? Convert.ToDateTime("1/1/1900") : incentive.CourseDet.Excepteddateofcourse,
                                ////"@P__CD_INDUSTRAILUNIT",incentive.CourseDet.INTINDUSTRAILUNIT==null ? 0 : incentive.CourseDet.INTINDUSTRAILUNIT,

                                //////Parameters for Documents to be Submitted after Completion of Course",,

                                ////"@P_VCH_PROV_SAC_LETTER",incentive.DocSubAftCompDet.ProvSacLetter==null ? "" : incentive.DocSubAftCompDet.ProvSacLetter,
                                ////"@P_VCH_MANG_DEV_LETTER",incentive.DocSubAftCompDet.ManagementDevSuceLetter==null ? "" : incentive.DocSubAftCompDet.ManagementDevSuceLetter,

                                //////Parameters for Means of Finance",,
                                "@P_VCH_TERM_LOAN_SAC",incentive.MeanOfFinanceDet.TermLoanSaction==null ? "" : incentive.MeanOfFinanceDet.TermLoanSaction,
                                "@P_VCH_INTERNAL_SOURCE",incentive.MeanOfFinanceDet.InternalSource==null ? "" : incentive.MeanOfFinanceDet.InternalSource,
                                "@P_VCH_APPROVED_DOC",incentive.MeanOfFinanceDet.ApprovedDocdocument==null ? "" : incentive.MeanOfFinanceDet.ApprovedDocdocument,
                                "@P_VCH_LOAN_DOC",incentive.MeanOfFinanceDet.SactionOrderOfLoanDoc==null ? "" : incentive.MeanOfFinanceDet.SactionOrderOfLoanDoc,
                                "@P_DEC_AMT_PER_WORKER",incentive.MeanOfFinanceDet.AmtClaimPerWorker==null ? 0 : incentive.MeanOfFinanceDet.AmtClaimPerWorker,
                                "@P_XMLTBL_LOAN",incentive.MeanOfFinanceDet.LoanList==null ? "" : incentive.MeanOfFinanceDet.LoanList.SerializeToXMLString(),
                                "@P_XMLTBL_REPAYMENT",incentive.MeanOfFinanceDet.RepaymentList==null ? "" : incentive.MeanOfFinanceDet.RepaymentList.SerializeToXMLString(),
                                "@P_XMLTBL_WORKING_LOAN",incentive.MeanOfFinanceDet.WorkingLoanList==null ? "" : incentive.MeanOfFinanceDet.WorkingLoanList.SerializeToXMLString(),
                                "@P_VCH_ASSESSMENT_REPORT",incentive.MeanOfFinanceDet.AssesesmentReport==null ? "" : incentive.MeanOfFinanceDet.AssesesmentReport,
                                "@P_INT_MEANS_FINANCE_ID",incentive.MeanOfFinanceDet.MOFID==null ? 0 : incentive.MeanOfFinanceDet.MOFID,


                                //////Parameters for Land Details",,
                                ////"@P_VCHCOSTOFPROJECT",incentive.LandDet.CostofProject==null ? "" : incentive.LandDet.CostofProject,
                                ////"@P_VCHLANDAREAPERPROJECT",incentive.LandDet.LandRequiredAsperReport==null ? "" : incentive.LandDet.LandRequiredAsperReport,
                                ////"@P_VCHLANDAREA",incentive.LandDet.LandRequired==null ? "" : incentive.LandDet.LandRequired,
                                ////"@P_VCHLANDDOCUMENT",incentive.LandDet.LandDocument==null ? "" : incentive.LandDet.LandDocument,
                                ////"@P_LANDDETAILS",incentive.LandDet.Landconverted==null ? "" : incentive.LandDet.Landconverted.SerializeToXMLString(),

                                //////Parameters for Electricity Consumption / Load Details",,

                                ////"@P_CONSUMEID",incentive.ConsumLoadDet.INTCONSUMEID==null ? "" : incentive.ConsumLoadDet.INTCONSUMEID,
                                ////"@P_DTMSUPPLY",incentive.ConsumLoadDet.stringSUPPLYDATE==null ? "" : incentive.ConsumLoadDet.stringSUPPLYDATE,
                                ////"@P_VCHCONSUMENUMBER",incentive.ConsumLoadDet.stringCONSUMENUMBER==null ? "" : incentive.ConsumLoadDet.stringCONSUMENUMBER,
                                ////"@P_VCHCONNECTEDLOAD",incentive.ConsumLoadDet.stringCONNECTEDLOAD==null ? "" : incentive.ConsumLoadDet.stringCONNECTEDLOAD,
                                ////"@P_ELECTRICITYAVIL",incentive.ConsumLoadDet.INTELECTRICITYAVIL==null ? "" : incentive.ConsumLoadDet.INTELECTRICITYAVIL,
                                ////"@P_STATEDETAIL",incentive.ConsumLoadDet.TstrSTATEDETAIL==null ? "" : incentive.ConsumLoadDet.TstrSTATEDETAIL.SerializeToXMLString(),

                                //////Parameters for Contract Demand & Connected Load Details",,
                                ////"@P_CONLOADID",incentive.ContractLoadDet.intconctdemamdid==null ? 0 : incentive.ContractLoadDet.intconctdemamdid,
                                ////"@P_VCHDEMANDFILE",incentive.ContractLoadDet.strcdemandfile==null ? "" : incentive.ContractLoadDet.strcdemandfile,
                                ////"@P_XMLLOANDETAIL",incentive.ContractLoadDet.ContractLoanDet==null ? "" : incentive.ContractLoadDet.ContractLoanDet.SerializeToXMLString(),

                                //////Parameters for Energy Audit Details",,
                                ////"@EnergyAuditorName",incentive.EnergyAuditDet.strEnergyAuditorName== null ? "" :incentive.EnergyAuditDet.strEnergyAuditorName,
                                ////"@EnergyAuditorDocName",incentive.EnergyAuditDet.strEnergyAuditorDocName== null ? "" :incentive.EnergyAuditDet.strEnergyAuditorDocName,
                                ////"@EnergyAuditorAddress",incentive.EnergyAuditDet.strEnergyAuditorAddress== null ? "" :incentive.EnergyAuditDet.strEnergyAuditorAddress,
                                ////"@EnergyAuditorAccreditation",incentive.EnergyAuditDet.strEnergyAuditorAccreditation== null ? "" :incentive.EnergyAuditDet.strEnergyAuditorAccreditation,
                                ////"@EnergyAuditorAccreditationDoc",incentive.EnergyAuditDet.strEnergyAuditorAccreditationDoc== null ? "" :incentive.EnergyAuditDet.strEnergyAuditorAccreditationDoc,
                                ////"@Expenditureincurred",incentive.EnergyAuditDet.strExpenditureincurred== null ? "" :incentive.EnergyAuditDet.strExpenditureincurred,
                                ////"@ExpenditureincurredDoc",incentive.EnergyAuditDet.strExpenditureincurredDoc== null ? "" :incentive.EnergyAuditDet.strExpenditureincurredDoc,
                                ////"@SuccessfulcompletionAudit",incentive.EnergyAuditDet.dtmSuccessfulcompletionAuditDate== null ? Convert.ToDateTime("1/1/1900") :incentive.EnergyAuditDet.dtmSuccessfulcompletionAuditDate,
                                ////"@SupportofimplementationofEnergyDoc",incentive.EnergyAuditDet.strSupportofimplementationofEnergyDOC== null ? "" :incentive.EnergyAuditDet.strSupportofimplementationofEnergyDOC,
                                ////"@SuccessfulcompletionAuditDoc",incentive.EnergyAuditDet.strSuccessfulcompletionAuditDOC== null ? "" :incentive.EnergyAuditDet.strSuccessfulcompletionAuditDOC,
                                ////"@EnergyConsumptionAfter",incentive.EnergyAuditDet.dtmEnergyConsumptionAfter== null ? 0 :incentive.EnergyAuditDet.dtmEnergyConsumptionAfter,
                                ////"@EnergyConsumptionBefore",incentive.EnergyAuditDet.strEnergyConsumptionBefore== null ? 0 :incentive.EnergyAuditDet.strEnergyConsumptionBefore,
                                ////"@ReductionOfEnergyDoc",incentive.EnergyAuditDet.strReductionOfEnergyDoc== null ? "" :incentive.EnergyAuditDet.strReductionOfEnergyDoc,

                                //////Parameters for Quality Cettificate Details",,
                                ////"@CertificationID",incentive.QualityCertDet.QualityCertificationActivitiesDetails== null ? "" :incentive.QualityCertDet.QualityCertificationActivitiesDetails.SerializeToXMLString(),
                                ////"@Total",incentive.QualityCertDet.intCertificationTotal== null ? 0 :incentive.QualityCertDet.intCertificationTotal,


                                //////Parameters for Details for Reimbursement of Power Traffic",,
                                ////"@decNewInvestment_SchematicProvisions",incentive.PowerTariffDet.NewInvestment_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.NewInvestment_SchematicProvisions,
                                ////"@decNewInvestment_TillOfCommencementOfProduction",incentive.PowerTariffDet.NewInvestment_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.NewInvestment_TillDateOfCommencementOfProduction,
                                ////"@vchNewInvestment_reasons",incentive.PowerTariffDet.NewInvestment_reasons== null ? "" :incentive.PowerTariffDet.NewInvestment_reasons,
                                ////"@decLand_SchematicProvisions",incentive.PowerTariffDet.Land_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.Land_SchematicProvisions,
                                ////"@decLand_TillOfCommencementOfProduction",incentive.PowerTariffDet.Land_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.Land_TillDateOfCommencementOfProduction,
                                ////"@vchLand_reasons",incentive.PowerTariffDet.Land_reasons== null ? "" :incentive.PowerTariffDet.Land_reasons,
                                ////"@decBuilding_SchematicProvisions",incentive.PowerTariffDet.Building_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.Building_SchematicProvisions,
                                ////"@decBuilding_TillOfCommencementOfProduction",incentive.PowerTariffDet.Building_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.Building_TillDateOfCommencementOfProduction,
                                ////"@vchBuilding_reasons",incentive.PowerTariffDet.Building_reasons== null ? "" :incentive.PowerTariffDet.Building_reasons,
                                ////"@decPlantMachinery_SchematicProvisions",incentive.PowerTariffDet.PlantMachinery_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.PlantMachinery_SchematicProvisions,
                                ////"@decPlantMachinery_TillOfCommencementOfProduction",incentive.PowerTariffDet.PlantMachinery_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.PlantMachinery_TillDateOfCommencementOfProduction,
                                ////"@vchPlantMachinery_reasons",incentive.PowerTariffDet.PlantMachinery_reasons== null ? "" :incentive.PowerTariffDet.PlantMachinery_reasons,
                                ////"@decOtherFixedAssets_SchematicProvisions",incentive.PowerTariffDet.OtherFixedAssets_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.OtherFixedAssets_SchematicProvisions,
                                ////"@decOtherFixedAssets_TillOfCommencementOfProduction",incentive.PowerTariffDet.OtherFixedAssets_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.OtherFixedAssets_TillDateOfCommencementOfProduction,
                                ////"@vchOtherFixedAssets_reasons",incentive.PowerTariffDet.OtherFixedAssets_reasons== null ? "" :incentive.PowerTariffDet.OtherFixedAssets_reasons,
                                ////"@decElectricalInstallations_SchematicProvisions",incentive.PowerTariffDet.ElectricalInstallations_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.ElectricalInstallations_SchematicProvisions,
                                ////"@decElectricalInstallations_TillOfCommencementOfProduction",incentive.PowerTariffDet.ElectricalInstallations_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.ElectricalInstallations_TillDateOfCommencementOfProduction,
                                ////"@vchElectricalInstallations_reasons",incentive.PowerTariffDet.ElectricalInstallations_reasons== null ? "" :incentive.PowerTariffDet.ElectricalInstallations_reasons,
                                ////"@vchJustificationForExcessInvestment",incentive.PowerTariffDet.JustificationForExcessInvestment== null ? "" :incentive.PowerTariffDet.JustificationForExcessInvestment,
                                ////"@vchTotalUnitConsumed",incentive.PowerTariffDet.TotalUnitConsumed== null ? "" :incentive.PowerTariffDet.TotalUnitConsumed,
                                ////"@decAmountPaid",incentive.PowerTariffDet.AmountPaid== null ? 0 :incentive.PowerTariffDet.AmountPaid,
                                ////"@_ReferenceID",incentive.PowerTariffDet.Refid== null ? 0 :incentive.PowerTariffDet.Refid,
                                ////"@vch_MoneyReceipt",incentive.PowerTariffDet.MoneyReceipt== null ? "" :incentive.PowerTariffDet.MoneyReceipt,

                                //Parameters for Training Details",,

                                "@intNewlyrecruited_NoOfTrainees",incentive.TrainingDetail.Newlyrecruited_NoOfTrainees== null ? 0 :incentive.TrainingDetail.Newlyrecruited_NoOfTrainees,
                                "@intNewlyrecruited_NoOfDays",incentive.TrainingDetail.Newlyrecruited_NoOfDays== null ? 0 :incentive.TrainingDetail.Newlyrecruited_NoOfDays,
                                "@vchNewlyrecruited_InHouseOrOutSide",incentive.TrainingDetail.Newlyrecruited_InHouseOrOutSide== null ? "" :incentive.TrainingDetail.Newlyrecruited_InHouseOrOutSide,
                                "@vchNewlyrecruited_NameOfInstitute",incentive.TrainingDetail.Newlyrecruited_NameOfInstitute== null ? "" :incentive.TrainingDetail.Newlyrecruited_NameOfInstitute,
                                "@intSkillupgradation_NoOfTrainees",incentive.TrainingDetail.Skillupgradation_NoOfTrainees== null ? 0 :incentive.TrainingDetail.Skillupgradation_NoOfTrainees,
                                "@vchSkillupgradation_NoOfDays",incentive.TrainingDetail.Skillupgradation_NoOfDays== null ? 0 :incentive.TrainingDetail.Skillupgradation_NoOfDays,
                                "@vchSkillupgradation_InHouseOrOutSide",incentive.TrainingDetail.Skillupgradation_InHouseOrOutSide== null ? "" :incentive.TrainingDetail.Skillupgradation_InHouseOrOutSide,
                                "@vchSkillupgradation_NameOfInstitute",incentive.TrainingDetail.Skillupgradation_NameOfInstitute== null ? "" :incentive.TrainingDetail.Skillupgradation_NameOfInstitute,
                                "@vchTotalUnitConsumedTdet",incentive.TrainingDetail.TotalUnitConsumed== null ? "" :incentive.TrainingDetail.TotalUnitConsumed,
                                "@decAmountPaidTdet",incentive.TrainingDetail.AmountPaid== null ? 0 :incentive.TrainingDetail.AmountPaid,
                                "@vch_MoneyReceiptFileTdet",incentive.TrainingDetail.MoneyReceipt== null ? "" :incentive.TrainingDetail.MoneyReceipt,
                                "@vch_TraineeDetailsTdet",incentive.TrainingDetail.TraineeDetails== null ? "" :incentive.TrainingDetail.TraineeDetails,



                                //////Parameters for Major Operational Activity of the Company",,
                                ////"@P_intMajorOperationId",incentive.MajorOperationOfComp.intMajorOperationId== null ? 0 :incentive.MajorOperationOfComp.intMajorOperationId,
                                ////"@P_intDirectEmployment",incentive.MajorOperationOfComp.intDirectEmployment== null ? 0 :incentive.MajorOperationOfComp.intDirectEmployment,
                                ////"@P_intContractualEmployment",incentive.MajorOperationOfComp.intContractualEmployment== null ? 0 :incentive.MajorOperationOfComp.intContractualEmployment,
                                ////"@P_vchEmployeeSupportDocument",incentive.MajorOperationOfComp.vchEmployeeSupportDocument== null ? "" :incentive.MajorOperationOfComp.vchEmployeeSupportDocument,
                                ////"@P_intManagerial_curr",incentive.MajorOperationOfComp.intManagerial_curr== null ? 0 :incentive.MajorOperationOfComp.intManagerial_curr,
                                ////"@P_intManagerial_prop",incentive.MajorOperationOfComp.intManagerial_prop== null ? 0 :incentive.MajorOperationOfComp.intManagerial_prop,
                                ////"@P_intSupervisory_curr",incentive.MajorOperationOfComp.intSupervisory_curr== null ? 0 :incentive.MajorOperationOfComp.intSupervisory_curr,
                                ////"@P_intSupervisory_prop",incentive.MajorOperationOfComp.intSupervisory_prop== null ? 0 :incentive.MajorOperationOfComp.intSupervisory_prop,
                                ////"@P_intSkilled_curr",incentive.MajorOperationOfComp.intSkilled_curr== null ? 0 :incentive.MajorOperationOfComp.intSkilled_curr,
                                ////"@P_intSkilled_prop",incentive.MajorOperationOfComp.intSkilled_prop== null ? 0 :incentive.MajorOperationOfComp.intSkilled_prop,
                                ////"@P_intSemiSkilled_curr",incentive.MajorOperationOfComp.intSemiSkilled_curr== null ? 0 :incentive.MajorOperationOfComp.intSemiSkilled_curr,
                                ////"@P_intSemiSkilled_prop",incentive.MajorOperationOfComp.intSemiSkilled_prop== null ? 0 :incentive.MajorOperationOfComp.intSemiSkilled_prop,
                                ////"@P_intUnSkilled_curr",incentive.MajorOperationOfComp.intUnSkilled_curr== null ? 0 :incentive.MajorOperationOfComp.intUnSkilled_curr,
                                ////"@P_intUnSkilled_prop",incentive.MajorOperationOfComp.intUnSkilled_prop== null ? 0 :incentive.MajorOperationOfComp.intUnSkilled_prop,
                                ////"@P_intTotal_curr",incentive.MajorOperationOfComp.intTotal_curr== null ? 0 :incentive.MajorOperationOfComp.intTotal_curr,
                                ////"@P_intTotal_prop",incentive.MajorOperationOfComp.intTotal_prop== null ? 0 :incentive.MajorOperationOfComp.intTotal_prop,
                                ////"@P_XmlData",incentive.MajorOperationOfComp.XmlData== null ? "" :incentive.MajorOperationOfComp.XmlData,
                                ////"@P_XmlData",incentive.MajorOperationOfComp.lstMajorOperationItmDtl== null ? "" :incentive.MajorOperationOfComp.lstMajorOperationItmDtl.SerializeToXMLString(),

                                //////Parameters for Brief Details of Proposed Activity",,

                                ////"@P_BrfDetailPropActivity",incentive.BriefDtlPropActvy.intBrfDetailPropActivity== null ? 0 :incentive.BriefDtlPropActvy.intBrfDetailPropActivity,
                                ////"@P_vchBriefDtlProposed",incentive.BriefDtlPropActvy.vchBriefDtlProposed== null ? "" :incentive.BriefDtlPropActvy.vchBriefDtlProposed,
                                ////"@P_vchProsDwnStrm",incentive.BriefDtlPropActvy.vchProsDwnStrm== null ? "" :incentive.BriefDtlPropActvy.vchProsDwnStrm,
                                ////"@P_vchProsAncillary",incentive.BriefDtlPropActvy.vchProsAncillary== null ? "" :incentive.BriefDtlPropActvy.vchProsAncillary,
                                ////"@P_vchDevelopUtility",incentive.BriefDtlPropActvy.vchDevelopUtility== null ? "" :incentive.BriefDtlPropActvy.vchDevelopUtility,
                                ////"@P_vchExternalities",incentive.BriefDtlPropActvy.vchExternalities== null ? "" :incentive.BriefDtlPropActvy.vchExternalities,
                                ////"@P_vchProposedCFC",incentive.BriefDtlPropActvy.vchProposedCFC== null ? "" :incentive.BriefDtlPropActvy.vchProposedCFC,
                                ////"@P_vchAnyOthers",incentive.BriefDtlPropActvy.vchAnyOthers== null ? "" :incentive.BriefDtlPropActvy.vchAnyOthers,
                                ////"@P_vchDtlOfSecondTnt",incentive.BriefDtlPropActvy.vchDtlOfSecondTnt== null ? "" :incentive.BriefDtlPropActvy.vchDtlOfSecondTnt,
                                ////"@P_vchdtlAttractSecndTnt",incentive.BriefDtlPropActvy.vchdtlAttractSecndTnt== null ? "" :incentive.BriefDtlPropActvy.vchdtlAttractSecndTnt,
                                ////"@P_vchConsetSecndTnt",incentive.BriefDtlPropActvy.vchConsetSecndTnt== null ? "" :incentive.BriefDtlPropActvy.vchConsetSecndTnt,
                                ////"@P_XmlData",incentive.BriefDtlPropActvy.XmlData== null ? "" :incentive.BriefDtlPropActvy.XmlData,
                                ////"@P_XmlData",incentive.BriefDtlPropActvy.lstProposedCommonFacility== null ? "" :incentive.BriefDtlPropActvy.lstProposedCommonFacility.SerializeToXMLString(),

                                //////Parameters for DLSWCA/ SLSWCA/ HLCA Approval Details",,

                                ////"@P_DTM_APPROVAL_",incentive.DLSWCAApprovalDet.dtmApprovalDate== null ? null :incentive.DLSWCAApprovalDet.dtmApprovalDate,
                                ////"@P_DCM_LAND_REQ",incentive.DLSWCAApprovalDet.dcmLandRequired== null ? 0 :incentive.DLSWCAApprovalDet.dcmLandRequired,
                                ////"@P_DCM_LAND_COST",incentive.DLSWCAApprovalDet.dcmCostOfLand== null ? 0 :incentive.DLSWCAApprovalDet.dcmCostOfLand,
                                ////"@P_DCM_SUBSIDY_AMT",incentive.DLSWCAApprovalDet.dcmSubsidyAmount== null ? 0 :incentive.DLSWCAApprovalDet.dcmSubsidyAmount,
                                ////"@P_DCM_APPROVAL_DOC",incentive.DLSWCAApprovalDet.strDLSWCAApprovalDoc== null ? "" :incentive.DLSWCAApprovalDet.strDLSWCAApprovalDoc,
                                ////"@P_DCM_SUBSTANTIATE_DOC",incentive.DLSWCAApprovalDet.strsubstantitateDoc== null ? "" :incentive.DLSWCAApprovalDet.strsubstantitateDoc,


                                //////Parameters for Investment in PollutionControl Equipment",,
                                ////"@P__InvestmentID",incentive.InvestPolutionDet.intInvestmentID== null ? 0 :incentive.InvestPolutionDet.intInvestmentID,
                                ////"@P__PollutionDetails",incentive.InvestPolutionDet.lstInvestPollution== null ? "" :incentive.InvestPolutionDet.lstInvestPollution.SerializeToXMLString(),

                                ////Common Attributes",,
                                "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                                "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                                "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                                "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                                "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                                "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                                "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                                "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                                "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,                               
                                "@P_INCTTYPE", incentive.incentivetype== null ? 0 :incentive.incentivetype,


                                };

        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms", out ss, sqlParam);
        return ss.ToString();
    }

    public string CreateIncentivePioneer(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 
                      "@SectionNo_1", sectionList[1],
                      "@SectionNo_5", sectionList[5],
                      "@SectionNo_6", sectionList[6],
                     
                   
                      ////Parameters for Industrial Unit's Details",
		               "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                      "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                      "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                      "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                      "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                      "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,
                                        

                    ////Parameters for Priority Sector Details",,
                    "@P_INT_CERTAVAIL",incentive.PrioritySector.intAvailPriorityCertf==null ? 0 : incentive.PrioritySector.intAvailPriorityCertf,
                    "@P_VCH_SECTORCERT",incentive.PrioritySector.strPrioritycCertf2015==null ? "" : incentive.PrioritySector.strPrioritycCertf2015,
                    "@P_VCH_ACKNOW",incentive.PrioritySector.strAppcnAcknow==null ? "" : incentive.PrioritySector.strAppcnAcknow,

                    ////Parameters for Additional Documents",,

                    "@P_INT_OSPCBNOC",incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC,
                    "@P_INT_OSPCBCONSENT",incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent,
                    "@P_INT_OSPCBEXCISE",incentive.AdditionalDocument.intStatutoryCleanCentralExec==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanCentralExec,
                    "@P_INT_OSPCBFSHGSCD",incentive.AdditionalDocument.intStatutoryCleanFSHGSCD==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanFSHGSCD,
                    "@P_INT_OSPCBEXPOLSIVE",incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC,
                    "@P_VCH_STATUTORYCLEARANCE",incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB==null ? "" : incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB,
                    "@P_VCHSTCLEANCONSENTOSPCB",incentive.AdditionalDocument.strStCleanConsentOSPCB==null ? "" : incentive.AdditionalDocument.strStCleanConsentOSPCB,
                    "@P_VCHCLEARANCECETIFTOSPCB",incentive.AdditionalDocument.strClearanceCetiftOSPCB==null ? "" : incentive.AdditionalDocument.strClearanceCetiftOSPCB,
                    "@P_VCHVALIDSATUTORYGREENCATEGORY",incentive.AdditionalDocument.strValidSatutoryGreenCategory==null ? "" : incentive.AdditionalDocument.strValidSatutoryGreenCategory,
                    "@P_VCHCONDODOCUMENTATIONDELAY",incentive.AdditionalDocument.strCondoDocumentationDelay==null ? "" : incentive.AdditionalDocument.strCondoDocumentationDelay,
                    "@P_XML_FILES",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(),  



                    //Common Attributes",,
                    "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                    "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                    "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                    "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                    "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                    "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                    "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                    "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                    "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                    "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),


                    };

        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_Pioneer", out ss, sqlParam);
        return ss.ToString();
    }

    public string CreateIncentiveCapitalInvst(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();
        object[] sqlParam = { 
                "@SectionNo_1", sectionList[1],
                "@SectionNo_10", sectionList[10],
                "@SectionNo_26", sectionList[26],

                ////Parameters for Industrial Unit's Details",
		        "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,

                ////Parameters for Bank Details",,
                "@P_INTBANKID",incentive.BankDet.BankId==null ? 0 : incentive.BankDet.BankId,
                "@P_VCHACCOUNTNO",incentive.BankDet.AccountNo==null ? "" : incentive.BankDet.AccountNo,
                "@P_VCHBANKNAME",incentive.BankDet.BankName==null ? "" : incentive.BankDet.BankName,
                "@P_VCHBRANCHNAME",incentive.BankDet.BranchName==null ? "" : incentive.BankDet.BranchName,
                "@P_VCHIFSCNO",incentive.BankDet.IFSCNo==null ? "" : incentive.BankDet.IFSCNo,
                "@P_VCHMICR",incentive.BankDet.MICRNo==null ? "" : incentive.BankDet.MICRNo,
                "@P_BankDoc",incentive.BankDet.BankDoc ==null?"":incentive.BankDet.BankDoc,

                //Parameters for Investment in PollutionControl Equipment",,
                "@P_int_InvestmentID",incentive.InvestPolutionDet.intInvestmentID== null ? 0 :incentive.InvestPolutionDet.intInvestmentID,
                "@P_VCHOPERATIONALIZATION",incentive.InvestPolutionDet.operationalizationDOC == null ?"":incentive.InvestPolutionDet.operationalizationDOC,
                "@P_DTMOPERATIONALIZATION",incentive.InvestPolutionDet.operationalizationDate == null ? "" :incentive.InvestPolutionDet.operationalizationDate,
                "@P_xml_PollutionDetails",incentive.InvestPolutionDet.lstInvestPollution== null ? "" :incentive.InvestPolutionDet.lstInvestPollution.SerializeToXMLString(),

                //Common Attributes",,
                "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),
                };
        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_CapitalInvetment", out ss, sqlParam);
        return ss.ToString();

    }

    public string CreateIncentiveAnchorTenant(Incentive incentive)
    {

        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");

        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 
                    "@SectionNo_1", sectionList[1],
                   
                    "@SectionNo_10", sectionList[10],
                 
                    "@SectionNo_24", sectionList[24],
                    "@SectionNo_25", sectionList[25],
               

                    
                    ////Parameters for Industrial Unit's Details",
		
                    "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                    "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                    "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                    "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                    "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                    "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,


                    ////Parameters for Brief Details of Proposed Activity",,

                    "@P_intBrfDetailPropActivity",incentive.BriefDtlPropActvy.intBrfDetailPropActivity== null ? 0 :incentive.BriefDtlPropActvy.intBrfDetailPropActivity,
                    "@P_vchBriefDtlProposed",incentive.BriefDtlPropActvy.vchBriefDtlProposed== null ? "" :incentive.BriefDtlPropActvy.vchBriefDtlProposed,
                    "@P_vchProsDwnStrm",incentive.BriefDtlPropActvy.vchProsDwnStrm== null ? "" :incentive.BriefDtlPropActvy.vchProsDwnStrm,
                    "@P_vchProsAncillary",incentive.BriefDtlPropActvy.vchProsAncillary== null ? "" :incentive.BriefDtlPropActvy.vchProsAncillary,
                    "@P_vchDevelopUtility",incentive.BriefDtlPropActvy.vchDevelopUtility== null ? "" :incentive.BriefDtlPropActvy.vchDevelopUtility,
                    "@P_vchExternalities",incentive.BriefDtlPropActvy.vchExternalities== null ? "" :incentive.BriefDtlPropActvy.vchExternalities,
                    "@P_vchProposedCFC",incentive.BriefDtlPropActvy.vchProposedCFC== null ? "" :incentive.BriefDtlPropActvy.vchProposedCFC,
                    "@P_vchAnyOthers",incentive.BriefDtlPropActvy.vchAnyOthers== null ? "" :incentive.BriefDtlPropActvy.vchAnyOthers,
                    "@P_vchDtlOfSecondTnt",incentive.BriefDtlPropActvy.vchDtlOfSecondTnt== null ? "" :incentive.BriefDtlPropActvy.vchDtlOfSecondTnt,
                    "@P_vchdtlAttractSecndTnt",incentive.BriefDtlPropActvy.vchdtlAttractSecndTnt== null ? "" :incentive.BriefDtlPropActvy.vchdtlAttractSecndTnt,
                    "@P_vchConsetSecndTnt",incentive.BriefDtlPropActvy.vchConsetSecndTnt== null ? "" :incentive.BriefDtlPropActvy.vchConsetSecndTnt,
                    "@P_XmlData_BriefDtlProposed",incentive.BriefDtlPropActvy.lstProposedCommonFacility== null ? "" :incentive.BriefDtlPropActvy.lstProposedCommonFacility.SerializeToXMLString(),

                    ////Parameters for DLSWCA/ SLSWCA/ HLCA Approval Details",,

                    "@P_DTM_APPROVAL_DATE",incentive.DLSWCAApprovalDet.dtmApprovalDate== null ? null :incentive.DLSWCAApprovalDet.dtmApprovalDate,
                    "@P_DCM_LAND_REQ",incentive.DLSWCAApprovalDet.dcmLandRequired== null ? 0 :incentive.DLSWCAApprovalDet.dcmLandRequired,
                    "@P_DCM_LAND_COST",incentive.DLSWCAApprovalDet.dcmCostOfLand== null ? 0 :incentive.DLSWCAApprovalDet.dcmCostOfLand,
                    "@P_DCM_SUBSIDY_AMT",incentive.DLSWCAApprovalDet.dcmSubsidyAmount== null ? 0 :incentive.DLSWCAApprovalDet.dcmSubsidyAmount,
                    "@P_DCM_APPROVAL_DOC",incentive.DLSWCAApprovalDet.strDLSWCAApprovalDoc== null ? "" :incentive.DLSWCAApprovalDet.strDLSWCAApprovalDoc,
                    "@P_DCM_SUBSTANTIATE_DOC",incentive.DLSWCAApprovalDet.strsubstantitateDoc== null ? "" :incentive.DLSWCAApprovalDet.strsubstantitateDoc,



                    ////Parameters for Bank Details",,


                    "@P_INTBANKID",incentive.BankDet.BankId==null ? 0 : incentive.BankDet.BankId,
                    "@P_VCHACCOUNTNO",incentive.BankDet.AccountNo==null ? "" : incentive.BankDet.AccountNo,
                    "@P_VCHBANKNAME",incentive.BankDet.BankName==null ? "" : incentive.BankDet.BankName,
                    "@P_VCHBRANCHNAME",incentive.BankDet.BranchName==null ? "" : incentive.BankDet.BranchName,
                    "@P_VCHIFSCNO",incentive.BankDet.IFSCNo==null ? "" : incentive.BankDet.IFSCNo,
                    "@P_VCHMICR",incentive.BankDet.MICRNo==null ? "" : incentive.BankDet.MICRNo,
                     "@P_BankDoc",incentive.BankDet.BankDoc==null ? "" : incentive.BankDet.BankDoc,

                     //Common Attributes",,
                    "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                    "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                    "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                    "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                    "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                    "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                    "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                    "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                    "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                    "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),

                    };

        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_AnchorTenant", out ss, sqlParam);
        return ss.ToString();

    }

    public string CreateIncentiveInetersSubsidy(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 

"@SectionNo_1", sectionList[1],
"@SectionNo_4", sectionList[4],



////Parameters for Industrial Unit's Details",		
"@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
"@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
"@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
"@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
"@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
"@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,


//interest subsidy detail",
"@P_INT_IS_ID",incentive.TermLoanDetails.INT_IS_ID ==null?0:incentive.TermLoanDetails.INT_IS_ID
,"@P_vchFinancialInstitution",incentive.TermLoanDetails.vchFinancialInstitution ==null ?"":incentive.TermLoanDetails.vchFinancialInstitution
,"@P_intYear",incentive.TermLoanDetails.intYear==null ?0:  incentive.TermLoanDetails.intYear
,"@P_dtmLoanStartDate",incentive.TermLoanDetails.dtmLoanStartDate ==null? Convert.ToDateTime("1900/01/01") :incentive.TermLoanDetails.dtmLoanStartDate
,"@P_dtmLoanMaturitydate",incentive.TermLoanDetails.dtmLoanMaturitydate ==null? Convert.ToDateTime("1900/01/01") :incentive.TermLoanDetails.dtmLoanMaturitydate   
,"@P_decSanctionAmount",incentive.TermLoanDetails.decSanctionAmount ==null?0:incentive.TermLoanDetails.decSanctionAmount
,"@P_vchSanctionOrderDoc",incentive.TermLoanDetails.vchFinancialInstitution==null ?"": incentive.TermLoanDetails.vchFinancialInstitution
,"@P_decReinursementAmount1",incentive.TermLoanDetails.decReinursementAmount1 ==null ?0:incentive.TermLoanDetails.decReinursementAmount1
,"@P_decReinursementAmount2",incentive.TermLoanDetails.decReinursementAmount2 ==null ?0:incentive.TermLoanDetails.decReinursementAmount2
,"@P_decReinursementAmount3",incentive.TermLoanDetails.decReinursementAmount3 ==null ?0:incentive.TermLoanDetails.decReinursementAmount3
,"@P_decReinursementAmount4",incentive.TermLoanDetails.decReinursementAmount3 ==null ?0:incentive.TermLoanDetails.decReinursementAmount4
,"@P_PlannedDisbursal",incentive.TermLoanDetails.lstPlannedDisbursal ==null?"":incentive.TermLoanDetails.lstPlannedDisbursal.SerializeToXMLString()
,"@P_RepaymentSchedule",incentive.TermLoanDetails.lstRepaymentSchedule ==null ?"":incentive.TermLoanDetails.lstRepaymentSchedule.SerializeToXMLString()
,"@P_PreviousSanction",incentive.TermLoanDetails.listPreviousSanction ==null ?"":incentive.TermLoanDetails.listPreviousSanction.SerializeToXMLString()
,"@P_VCHTermLoanDOC",incentive.TermLoanDetails.VCHTermLoanDOC ==null ?"":incentive.TermLoanDetails.VCHTermLoanDOC
,"@P_VCHBankDetailDOC",incentive.TermLoanDetails.VCHBankDetailDOC ==null?"":incentive.TermLoanDetails.VCHBankDetailDOC,

////Common Attributes",,
"@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
"@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
"@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
"@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
"@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
"@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
"@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
"@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
"@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
"@P_F_YEAR",incentive.FYear== null ? 0 :incentive.FYear,


};

        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_InterestSubsidy", out ss, sqlParam);
        return ss.ToString();
    }

    public string CreateIncentiveTrainingSubsidy(Incentive incentive)
    {

        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 
                    "@SectionNo_1", sectionList[1],
                    "@SectionNo_6", sectionList[6],
                    "@SectionNo_10", sectionList[10],
                    "@SectionNo_22", sectionList[22],
                  

                    ////Parameters for Industrial Unit's Details",
		
                    "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                    "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                    "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                    "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                    "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                    "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,


                   ////Parameters for Additional Documents",,

                    "@P_INT_OSPCBNOC",incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC,
                    "@P_INT_OSPCBCONSENT",incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent,
                    "@P_INT_OSPCBEXCISE",incentive.AdditionalDocument.intStatutoryCleanCentralExec==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanCentralExec,
                    "@P_INT_OSPCBFSHGSCD",incentive.AdditionalDocument.intStatutoryCleanFSHGSCD==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanFSHGSCD,
                    "@P_INT_OSPCBEXPOLSIVE",incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC,
                    "@P_VCH_STATUTORYCLEARANCE",incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB==null ? "" : incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB,
                    "@P_VCHSTCLEANCONSENTOSPCB",incentive.AdditionalDocument.strStCleanConsentOSPCB==null ? "" : incentive.AdditionalDocument.strStCleanConsentOSPCB,
                    "@P_VCHCLEARANCECETIFTOSPCB",incentive.AdditionalDocument.strClearanceCetiftOSPCB==null ? "" : incentive.AdditionalDocument.strClearanceCetiftOSPCB,
                    "@P_VCHVALIDSATUTORYGREENCATEGORY",incentive.AdditionalDocument.strValidSatutoryGreenCategory==null ? "" : incentive.AdditionalDocument.strValidSatutoryGreenCategory,
                    "@P_VCHCONDODOCUMENTATIONDELAY",incentive.AdditionalDocument.strCondoDocumentationDelay==null ? "" : incentive.AdditionalDocument.strCondoDocumentationDelay,
                    "@P_XML_FILES",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(), 

                    
                     ////Parameters for Training Details",,

                    "@P_XML_NEWRECRUIT",incentive.TrainingDetail.NewlyRecruited == null ? "" : incentive.TrainingDetail.NewlyRecruited.SerializeToXMLString(),
                    "@P_XML_SKILLUPGRADE",incentive.TrainingDetail.SkillUpgradation == null ? "" : incentive.TrainingDetail.SkillUpgradation.SerializeToXMLString(),
                    "@decAmountPaidTdet",incentive.TrainingDetail.AmountPaid== null ? 0 :incentive.TrainingDetail.AmountPaid,
                    "@vch_MoneyReceiptFileTdet",incentive.TrainingDetail.MoneyReceipt== null ? "" :incentive.TrainingDetail.MoneyReceipt,
                    "@vch_TraineeDetailsTdet",incentive.TrainingDetail.TraineeDetails== null ? "" :incentive.TrainingDetail.TraineeDetails,
                    "@vchFyear",incentive.TrainingDetail.vchFyear==null ? "" : incentive.TrainingDetail.vchFyear,


                 
                ////Parameters for Bank Details",,
                    "@P_INTBANKID",incentive.BankDet.BankId==null ? 0 : incentive.BankDet.BankId,
                    "@P_VCHACCOUNTNO",incentive.BankDet.AccountNo==null ? "" : incentive.BankDet.AccountNo,
                    "@P_VCHBANKNAME",incentive.BankDet.BankName==null ? "" : incentive.BankDet.BankName,
                    "@P_VCHBRANCHNAME",incentive.BankDet.BranchName==null ? "" : incentive.BankDet.BranchName,
                    "@P_VCHIFSCNO",incentive.BankDet.IFSCNo==null ? "" : incentive.BankDet.IFSCNo,
                    "@P_VCHMICR",incentive.BankDet.MICRNo==null ? "" : incentive.BankDet.MICRNo,
                    "@P_BankDoc",incentive.BankDet.BankDoc ==null?"":incentive.BankDet.BankDoc,

                

                    //Common Attributes",,
                    "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                    "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                    "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                    "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                    "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                    "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                    "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                    "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                    "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                    "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),
                    "@P_F_YEAR",incentive.FYear== null ? 0 :incentive.FYear,


                    };

        ////SqlHelper.ExecuteNonQuery(conn, "USP_Insentive_Jeevan", sqlParam);
        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_TrainingSubsidy", out ss, sqlParam);
        return ss.ToString();

    }

    public DataSet GetIncentiveEDD(Incentive incentive)
    {
        DataSet ds = new DataSet();
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = {  
                            "@CHRACTIONCODE",sectionList[0],
                            "@P_INTID_A",incentive.GetVwPrmtrs.Param1ID==null?"":incentive.GetVwPrmtrs.Param1ID,
                            "@P_INTID_B",incentive.GetVwPrmtrs.Param2ID==null?"":incentive.GetVwPrmtrs.Param2ID, 
                            "@P_INTID_C",incentive.GetVwPrmtrs.Param3ID==null?"":incentive.GetVwPrmtrs.Param3ID,
                            "@P_INTID_D",incentive.GetVwPrmtrs.Param4ID==null?"":incentive.GetVwPrmtrs.Param4ID, 
                            "@P_VCHID_E" ,incentive.GetVwPrmtrs.Param5==null?"":incentive.GetVwPrmtrs.Param5,
                            "@P_VCHID_F" ,incentive.GetVwPrmtrs.Param6==null?"":incentive.GetVwPrmtrs.Param6,
                            "@P_VCHID_G",incentive.GetVwPrmtrs.Param7==null?"":incentive.GetVwPrmtrs.Param7,
                            "@P_DTMFROM",incentive.GetVwPrmtrs.FrmDate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.FrmDate,
                            "@P_DTMTO",incentive.GetVwPrmtrs.Todate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.Todate,
                            "@P_INCTTYPE",incentive.GetVwPrmtrs.InctType==null?0:incentive.GetVwPrmtrs.InctType,
                            "@P_INCTUNQID",incentive.UnqIncentiveId==null?0:incentive.UnqIncentiveId,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_FormsViews_EDD", sqlParam);

        return ds;
    }

    public DataSet GetIncentive(Incentive incentive)
    {

        DataSet ds = new DataSet();
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = {  "@CHRACTIONCODE",sectionList[0],
	                           "@P_INTID_A",incentive.GetVwPrmtrs.Param1ID==null?"":incentive.GetVwPrmtrs.Param1ID,
	                           "@P_INTID_B",incentive.GetVwPrmtrs.Param2ID==null?"":incentive.GetVwPrmtrs.Param2ID, 
	                           "@P_INTID_C",incentive.GetVwPrmtrs.Param3ID==null?"":incentive.GetVwPrmtrs.Param3ID,
	                           "@P_INTID_D",incentive.GetVwPrmtrs.Param4ID==null?"":incentive.GetVwPrmtrs.Param4ID, 
	                           "@P_VCHID_E" ,incentive.GetVwPrmtrs.Param5==null?"":incentive.GetVwPrmtrs.Param5,
	                           "@P_VCHID_F" ,incentive.GetVwPrmtrs.Param6==null?"":incentive.GetVwPrmtrs.Param6,
	                           "@P_VCHID_G",incentive.GetVwPrmtrs.Param7==null?"":incentive.GetVwPrmtrs.Param7,
	                           "@P_DTMFROM",incentive.GetVwPrmtrs.FrmDate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.FrmDate,
	                           "@P_DTMTO",incentive.GetVwPrmtrs.Todate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.Todate,
                               "@P_INCTTYPE",incentive.GetVwPrmtrs.InctType==null?0:incentive.GetVwPrmtrs.InctType,
                               "@P_INCTUNQID",incentive.UnqIncentiveId==null?0:incentive.UnqIncentiveId,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_FormsViews", sqlParam);

        return ds;
    }

    public int UpdateSignature(Incentive incentive)
    {

        object[] sqlParam = { 

                            "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                            "@P_APPVACTION",incentive.ApprovalAction== null ? "" :incentive.ApprovalAction,
                            "@P_SIGNATURE", incentive.Signature== null ? "" :incentive.Signature,

                            };


        SqlHelper.ExecuteNonQuery(conn, "USP_Insentive_Forms_Update", sqlParam);

        return 0;
    }

    #region PatentDetail

    public string CreateIncentivePatent(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");

        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 
                            "@SectionNo_1", sectionList[1],
                            "@SectionNo_7", sectionList[7],
                            "@SectionNo_8", sectionList[8],

                            ////Parameters for Industrial Unit's Details",		
                            "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                            "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                            "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                            "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                            "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                            "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,

                            //Parameters for Patent Details",,
                            "@intPatentId",incentive.PatentDet.intPatentId==null ? 0 : incentive.PatentDet.intPatentId,
                            "@P_vchAgencyName",incentive.PatentDet.AgencyName==null ? "" : incentive.PatentDet.AgencyName,
                            "@P_vchAgencyAddress",incentive.PatentDet.AgencyAddress==null ? "" : incentive.PatentDet.AgencyAddress,
                            "@itemsDetails",incentive.PatentDet.lstitemsDetails==null ? "" : incentive.PatentDet.lstitemsDetails.SerializeToXMLString(),
                            "@P_XMLTBL_PATENT_LOAN",incentive.PatentDet.lstPatLoanDetails==null ? "" : incentive.PatentDet.lstPatLoanDetails.SerializeToXMLString(),

                            //Parameters for Availed Details",,
                            "@NeverAvailedPrior",incentive.AvailDet.NeverAvailedPrior==null ? 0 : incentive.AvailDet.NeverAvailedPrior,
                            "@UndertakingSubsidyDoc",incentive.AvailDet.UndertakingSubsidyDoc==null ? "" : incentive.AvailDet.UndertakingSubsidyDoc,
                            "@SubsidyAvailed",incentive.AvailDet.SubsidyAvailed==null ? 0 : incentive.AvailDet.SubsidyAvailed,
                            "@SupportingDocs",incentive.AvailDet.SanctionOrderDoc==null ? "" : incentive.AvailDet.SanctionOrderDoc,
                            "@ClaimtExempted",incentive.AvailDet.ClaimtExempted==null ? 0 : incentive.AvailDet.ClaimtExempted,
                            "@ClaimReimbursement",incentive.AvailDet.ClaimReimbursement==null ? 0 : incentive.AvailDet.ClaimReimbursement,
                            "@AssistanceDetails",incentive.AvailDet.AssistanceDetails==null ? "" : incentive.AvailDet.AssistanceDetails.SerializeToXMLString(),
                            "@IncentiveAvailed",incentive.AvailDet.IncentiveAvailed==null ? "" : incentive.AvailDet.IncentiveAvailed.SerializeToXMLString(),

                            ////Common Attributes",,
                            "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                            "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                            "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                            "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                            "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                            "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                            "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                            "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                            "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                            };
        object ss = "";
        SqlHelper.ExecuteNonQuery(conn, "USP_Insentive_Forms_PatentReg", out ss, sqlParam);

        return ss.ToString();
    }
    public DataSet patentView(int intUNQINCID)
    {
        DataSet ds = new DataSet();
        object[] sqlParam = {  
                             "@P_IntIncUnqueId",intUNQINCID,	                           
                            };

        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_Forms_PatentReg_view", sqlParam);

        return ds;
    }

    #endregion

    public DataSet PrepopulateData(int userid)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  "@CHRACTIONCODE","V",
	                           "@P_INTUSERID",userid==null?0:userid,
	                        };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_VIEW_COMMON_PREPOPULATE_IPIM", sqlParam);

        return ds;
    }

    public DataSet PostpopulateData(int userid)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  "@CHRACTIONCODE","V",
	                           "@P_INTUNQINCID",userid==null?0:userid,
	                        };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_VIEW_COMMON_POSTPOPULATE_IPIM", sqlParam);

        return ds;
    }

    public DataSet dynamic_name_doc_bind()
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataSet objds = new DataSet();
        try
        {
            objCommand.CommandText = "USP_INCT_DOC_BIND";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;
            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objCommand = null;
        }
        return objds;
    }

    public DataSet GetSubsidyClaim(Incentive incentive)
    {

        DataSet ds = new DataSet();
        object[] sqlParam = { 
	                       
                               "@P_INTUSERID",incentive.Userid==null ? 0:incentive.Userid,
                             
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_GETTRAININGSUBSIDY_CLAIM", sqlParam);

        return ds;
    }

    public DataSet GetIncentiveMaster(Incentive incentive)
    {

        DataSet ds = new DataSet();
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        object[] sqlParam = {  
                               "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                          "@P_VCHINCTID",incentive.IncentiveNum ==null? "":incentive.IncentiveNum,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_Master", sqlParam);

        return ds;
    }

    public DataSet PrepopulateFile(Incentive obj)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  
	                           "@P_INDUSTRYCODE",obj.UnitCode==null?"":obj.UnitCode,
	                        };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_PREFILE_BIND", sqlParam);

        return ds;
    }

    public DataSet BindDropdown(string strActionCode)
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataSet objds = new DataSet();
        try
        {
            objCommand.CommandText = "USP_INCT_FillDropDown";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;
            objCommand.Parameters.AddWithValue("@P_ACTION", strActionCode);
            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { objCommand = null; }
        return objds;
    }

    public string CreateIncentiveEmpCostSubsidy(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();
        object[] sqlParam = { 
                            "@SectionNo_1", sectionList[1],
                            "@SectionNo_2", sectionList[2],                               
                            "@SectionNo_6", sectionList[6],
                               
                            "@SectionNo_8", sectionList[8],                               
                            "@SectionNo_10", sectionList[10], 

                            //Parameters for Industrial Unit's Details",, 
                            "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? 0 : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,
                            "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,
                            "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ? 0 : incentive.IndsutUnitMstDet.APPLYBY_IND,
                            "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? 0 : incentive.IndsutUnitMstDet.AADHAARNO_IND,
                            "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? 0 : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                            "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? 0 : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND, 
                                 
                            //Parameters for Production & Employment Details",,                                
                            "@P_VCHDPRDOC",incentive.ProdEmpDet.DPRDOC==null ? "" : incentive.ProdEmpDet.DPRDOC,
                            "@P_VCHESIOREPFREGNO",incentive.ProdEmpDet.ESIOREPFREGNO==null ? "" : incentive.ProdEmpDet.ESIOREPFREGNO,
                            "@P_DTMESIOREPF",incentive.ProdEmpDet.ESIOREPF==null ? "" : incentive.ProdEmpDet.ESIOREPF,                               

                            "@P_BITESI",incentive.ProdEmpDet.ESI==null ? 0 : incentive.ProdEmpDet.ESI,
                            "@P_BITEPF",incentive.ProdEmpDet.EPF==null ? 0 : incentive.ProdEmpDet.EPF,
                            "@P_VCHESIOREPFDOC",incentive.ProdEmpDet.ESIOREPFDOC==null ? "" : incentive.ProdEmpDet.ESIOREPFDOC,
                            "@P_VCHESIEPFCOMPDOC",incentive.ProdEmpDet.ESIEPFCOMPDOC==null ? "" : incentive.ProdEmpDet.ESIEPFCOMPDOC,
                            "@P_VCHPAYROLLDOC",incentive.ProdEmpDet.PAYROLLDOC==null ? "" : incentive.ProdEmpDet.PAYROLLDOC,
                            "@P_VCHDOCUMENTINSUPPORT",incentive.ProdEmpDet.DOCUMENTINSUPPORT==null ? "" : incentive.ProdEmpDet.DOCUMENTINSUPPORT,
                            "@P_VCHDELAYREASON",incentive.ProdEmpDet.DELAYREASON==null ? "" : incentive.ProdEmpDet.DELAYREASON,
                            "@P_VCHREGATTACHDOC",incentive.ProdEmpDet.REGATTACHDOC==null ? "" : incentive.ProdEmpDet.REGATTACHDOC,  

                            //////////----- ADDED ON 13TH Nov 2017------ by GS Chhotray-----------
                            "@P_VCHESIAUTHNAME",incentive.ProdEmpDet.ESIAUTHNAME==null ? "" : incentive.ProdEmpDet.ESIAUTHNAME,
                            "@P_VCHEPFREGNO",incentive.ProdEmpDet.EPFREGNO==null ? "" : incentive.ProdEmpDet.EPFREGNO,
                            "@P_DTMEPFREGDATE",incentive.ProdEmpDet.EPFREGDATE==null ? "" : incentive.ProdEmpDet.EPFREGDATE,
                            "@P_VCHEPFREGATTACHDOC",incentive.ProdEmpDet.EPFREGATTACHDOC==null ? "" : incentive.ProdEmpDet.EPFREGATTACHDOC,
                            "@P_VCHEPFAUTHNAME",incentive.ProdEmpDet.EPFAUTHNAME==null ? "" : incentive.ProdEmpDet.EPFAUTHNAME,

                            //Parameters for Availed Details",,
                            "@NeverAvailedPrior",incentive.AvailDet.NeverAvailedPrior==null ? 0 : incentive.AvailDet.NeverAvailedPrior,
                            "@UndertakingSubsidyDoc",incentive.AvailDet.UndertakingSubsidyDoc==null ? "" : incentive.AvailDet.UndertakingSubsidyDoc,
                            "@SubsidyAvailed",incentive.AvailDet.SubsidyAvailed==null ? 0 : incentive.AvailDet.SubsidyAvailed,
                            "@SupportingDocs",incentive.AvailDet.SupportingDocs==null ? "" : incentive.AvailDet.SupportingDocs,
                            "@ClaimtExempted",incentive.AvailDet.ClaimtExempted==null ? 0 : incentive.AvailDet.ClaimtExempted,
                            "@ClaimReimbursement",incentive.AvailDet.ClaimReimbursement==null ? 0 : incentive.AvailDet.ClaimReimbursement,
                            "@AssistanceDetails",incentive.AvailDet.AssistanceDetails==null ? "" : incentive.AvailDet.AssistanceDetails.SerializeToXMLString(),
                            "@IncentiveAvailed",incentive.AvailDet.IncentiveAvailed==null ? "" : incentive.AvailDet.IncentiveAvailed.SerializeToXMLString(),
                            "@P_DecDiffAmtClaim",incentive.AvailDet.DecDiffAmtClaim==null ? 0 : incentive.AvailDet.DecDiffAmtClaim,
                            "@P_VchSanctionDoc",incentive.AvailDet.SanctionOrderDoc==null ? "" : incentive.AvailDet.SanctionOrderDoc,
                            "@P_decClaimReimbursementEPF",incentive.AvailDet.decClaimReimbursementEPF==null ? 0 : incentive.AvailDet.decClaimReimbursementEPF,
                                
                            ////Parameters for Additional Documents",,
                            "@P_INT_OSPCBNOC",incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC,
                            "@P_INT_OSPCBCONSENT",incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent,
                            "@P_INT_OSPCBEXCISE",incentive.AdditionalDocument.intStatutoryCleanCentralExec==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanCentralExec,
                            "@P_INT_OSPCBFSHGSCD",incentive.AdditionalDocument.intStatutoryCleanFSHGSCD==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanFSHGSCD,
                            "@P_INT_OSPCBEXPOLSIVE",incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC,
                            "@P_VCH_STATUTORYCLEARANCE",incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB==null ? "" : incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB,/////------ factory
                            "@P_VCHSTCLEANCONSENTOSPCB",incentive.AdditionalDocument.strStCleanConsentOSPCB==null ? "" : incentive.AdditionalDocument.strStCleanConsentOSPCB,
                            "@P_VCHCLEARANCECETIFTOSPCB",incentive.AdditionalDocument.strClearanceCetiftOSPCB==null ? "" : incentive.AdditionalDocument.strClearanceCetiftOSPCB,
                            "@P_VCHVALIDSATUTORYGREENCATEGORY",incentive.AdditionalDocument.strValidSatutoryGreenCategory==null ? "" : incentive.AdditionalDocument.strValidSatutoryGreenCategory,
                            "@P_VCHCONDODOCUMENTATIONDELAY",incentive.AdditionalDocument.strCondoDocumentationDelay==null ? "" : incentive.AdditionalDocument.strCondoDocumentationDelay,
                            "@P_VCHFILE",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(),
                            "@P_VCHDOCUMENTNAME",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(),
                            "@P_XML_FILES",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(),

                            ////Parameters for Bank Details",,                                
                            "@P_INTBANKID",incentive.BankDet.BankId==null ? 0 : incentive.BankDet.BankId,
                            "@P_VCHACCOUNTNO",incentive.BankDet.AccountNo==null ? "" : incentive.BankDet.AccountNo,
                            "@P_VCHBANKNAME",incentive.BankDet.BankName==null ? "" : incentive.BankDet.BankName,
                            "@P_VCHBRANCHNAME",incentive.BankDet.BranchName==null ? "" : incentive.BankDet.BranchName,
                            "@P_VCHIFSCNO",incentive.BankDet.IFSCNo==null ? "" : incentive.BankDet.IFSCNo,
                            "@P_VCHMICR",incentive.BankDet.MICRNo==null ? "" : incentive.BankDet.MICRNo,
                            "@P_BankDoc",incentive.BankDet.BankDoc==null ? "" : incentive.BankDet.BankDoc,

                            //Common Attributes",,
                            "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                            "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                            "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                            "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                            "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                            "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                            "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                            "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                            "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                            "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),
                            "@P_F_YEAR",incentive.FYear== null ? 0 :incentive.FYear,
                            };
        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_Emp_CostSubsidy", out ss, sqlParam);///
        return ss.ToString();
    }

    public DataSet PostpopulateDataCostSubsidy(int userid)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  "@CHRACTIONCODE","V",
	                           "@P_INTUNQINCID",userid==null?0:userid,
	                        };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_VIEW_COMMON_POSTPOPULATE_IPIM_EMP_COST_SUBSIDY", sqlParam);  //////////////USP_INCT_VIEW_COMMON_POSTPOPULATE_IPIM

        return ds;
    }

    public DataSet PostpopulateCapSubsidy(Incentive incentive)
    {

        DataSet ds = new DataSet();
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = {  "@CHRACTIONCODE",sectionList[0],
	                           "@P_INTID_A",incentive.GetVwPrmtrs.Param1ID==null?"":incentive.GetVwPrmtrs.Param1ID,
	                           "@P_INTID_B",incentive.GetVwPrmtrs.Param2ID==null?"":incentive.GetVwPrmtrs.Param2ID, 
	                           "@P_INTID_C",incentive.GetVwPrmtrs.Param3ID==null?"":incentive.GetVwPrmtrs.Param3ID,
	                           "@P_INTID_D",incentive.GetVwPrmtrs.Param4ID==null?"":incentive.GetVwPrmtrs.Param4ID, 
	                           "@P_VCHID_E" ,incentive.GetVwPrmtrs.Param5==null?"":incentive.GetVwPrmtrs.Param5,
	                           "@P_VCHID_F" ,incentive.GetVwPrmtrs.Param6==null?"":incentive.GetVwPrmtrs.Param6,
	                           "@P_VCHID_G",incentive.GetVwPrmtrs.Param7==null?"":incentive.GetVwPrmtrs.Param7,
	                           "@P_DTMFROM",incentive.GetVwPrmtrs.FrmDate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.FrmDate,
	                           "@P_DTMTO",incentive.GetVwPrmtrs.Todate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.Todate,
                               "@P_INCTTYPE",incentive.GetVwPrmtrs.InctType==null?0:incentive.GetVwPrmtrs.InctType,
                               "@P_INCTUNQID",incentive.UnqIncentiveId==null?0:incentive.UnqIncentiveId,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_FormsViews_CapInvst", sqlParam);
        return ds;
    }

    public DataSet getPreEDDdataExistance(Incentive incentive)
    {
        DataSet ds = new DataSet();
        object[] sqlParam = {  
	                           "@P_INTUSERID",incentive.Userid==null? 0 :incentive.Userid,
	                           "@P_VCHINCENTIVENO", incentive.IncentiveNum==null? "" :incentive.IncentiveNum,
                            };

        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_CHECK_EARLY_EDD", sqlParam);

        return ds;
    }

    public string CreateIncentiveElectricity(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 
                        "@SectionNo_1", sectionList[1],
                        "@SectionNo_10", sectionList[10],                        
                        "@SectionNo_17", sectionList[17],
                        "@SectionNo_8", sectionList[8],                        

                        ////Parameters for Industrial Unit's Details",		
                        "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                        "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                        "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                        "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                        "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                        "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,
                        
                        //Parameters for Electricity Consumption / Load Details",,
                        "@P_INTCONSUMEID",incentive.ConsumLoadDet.INTCONSUMEID==null ? "0" : incentive.ConsumLoadDet.INTCONSUMEID,
                        "@P_DTMSUPPLYDATE",incentive.ConsumLoadDet.stringSUPPLYDATE==null ? "1900/01/01" : incentive.ConsumLoadDet.stringSUPPLYDATE,
                        "@P_VCHCONSUMENUMBER",incentive.ConsumLoadDet.stringCONSUMENUMBER==null ? "" : incentive.ConsumLoadDet.stringCONSUMENUMBER,
                        "@P_VCHCONNECTEDLOAD",incentive.ConsumLoadDet.stringCONNECTEDLOAD==null ? "" : incentive.ConsumLoadDet.stringCONNECTEDLOAD,
                        "@P_INTELECTRICITYAVIL",incentive.ConsumLoadDet.INTELECTRICITYAVIL==null ? "0" : incentive.ConsumLoadDet.INTELECTRICITYAVIL,
                        "@P_STATEDETAIL_XML",incentive.ConsumLoadDet.TstrSTATEDETAIL==null ? "" : incentive.ConsumLoadDet.TstrSTATEDETAIL.SerializeToXMLString(),
                        "@P_VCHPMTBILLDOC",incentive.ConsumLoadDet.strbillpmtvoucher==null ? "" : incentive.ConsumLoadDet.strbillpmtvoucher,
                        "@P_VCHEXEMPELECDUTY",incentive.ConsumLoadDet.strelecconsumption==null ? "" : incentive.ConsumLoadDet.strelecconsumption,
                        "@P_VCHELECDETAILS",incentive.ConsumLoadDet.strElecDetails==null? "" : incentive.ConsumLoadDet.strElecDetails,
                        "@P_VCHDOPSDOC", incentive.ConsumLoadDet.strdpsdocument==null? "" : incentive.ConsumLoadDet.strdpsdocument,
                        "@P_VCHCONDOC", incentive.ConsumLoadDet.strcondocument==null? "" : incentive.ConsumLoadDet.strcondocument,

                        //Parameters for Bank Details",,
                        "@P_INTBANKID",incentive.BankDet.BankId==null ? 0 : incentive.BankDet.BankId,
                        "@P_VCHACCOUNTNO",incentive.BankDet.AccountNo==null ? "" : incentive.BankDet.AccountNo,
                        "@P_VCHBANKNAME",incentive.BankDet.BankName==null ? "" : incentive.BankDet.BankName,
                        "@P_VCHBRANCHNAME",incentive.BankDet.BranchName==null ? "" : incentive.BankDet.BranchName,
                        "@P_VCHIFSCNO",incentive.BankDet.IFSCNo==null ? "" : incentive.BankDet.IFSCNo,
                        "@P_VCHMICR",incentive.BankDet.MICRNo==null ? "" : incentive.BankDet.MICRNo,
                        "@P_BANKDOC",incentive.BankDet.BankDoc==null? "" : incentive.BankDet.BankDoc,

                        //Parameters for Availed Details",,
                        "@NeverAvailedPrior",incentive.AvailDet.NeverAvailedPrior==null ? 0 : incentive.AvailDet.NeverAvailedPrior,
                        "@UndertakingSubsidyDoc",incentive.AvailDet.UndertakingSubsidyDoc==null ? "" : incentive.AvailDet.UndertakingSubsidyDoc,
                        "@SubsidyAvailed",incentive.AvailDet.SubsidyAvailed==null ? 0 : incentive.AvailDet.SubsidyAvailed,
                        "@SupportingDocs",incentive.AvailDet.SanctionOrderDoc==null ? "" : incentive.AvailDet.SanctionOrderDoc,
                        "@ClaimtExempted",incentive.AvailDet.ClaimtExempted==null ? 0 : incentive.AvailDet.ClaimtExempted,
                        "@ClaimReimbursement",incentive.AvailDet.ClaimReimbursement==null ? 0 : incentive.AvailDet.ClaimReimbursement,
                        "@AssistanceDetails",incentive.AvailDet.AssistanceDetails==null ? "" : incentive.AvailDet.AssistanceDetails.SerializeToXMLString(),
                        "@IncentiveAvailed",incentive.AvailDet.IncentiveAvailed==null ? "" : incentive.AvailDet.IncentiveAvailed.SerializeToXMLString(),

                        //Common Attributes",,
                        "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                        "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                        "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                        "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                        "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                        "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                        "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                        "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                        "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                        "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),
                        "@P_F_YEAR",incentive.FYear== null ? 0 :incentive.FYear,
                    };

        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_Electrical", out ss, sqlParam);
        return ss.ToString();
    }

    public string CreateIncentiveTechKHW(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");

        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 
                            "@SectionNo_1", sectionList[1],
                            "@SectionNo_6", sectionList[6],
                            "@SectionNo_8", sectionList[8],
                            "@SectionNo_12", sectionList[12],               
                    
                            ////Parameters for Industrial Unit's Details",
                            "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                            "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                            "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                            "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                            "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                            "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,
                    
                            //Parameters for Availed Details",,-----------------------------------------------------------------
                            "@NeverAvailedPrior",incentive.AvailDet.NeverAvailedPrior==null ? 0 : incentive.AvailDet.NeverAvailedPrior,
                            "@UndertakingSubsidyDoc",incentive.AvailDet.UndertakingSubsidyDoc==null ? "" : incentive.AvailDet.UndertakingSubsidyDoc,
                            "@SubsidyAvailed",incentive.AvailDet.SubsidyAvailed==null ? 0 : incentive.AvailDet.SubsidyAvailed,
                            "@SupportingDocs",incentive.AvailDet.SanctionOrderDoc==null ? "" : incentive.AvailDet.SanctionOrderDoc,
                            "@ClaimtExempted",incentive.AvailDet.ClaimtExempted==null ? 0 : incentive.AvailDet.ClaimtExempted,
                            "@ClaimReimbursement",incentive.AvailDet.ClaimReimbursement==null ? 0 : incentive.AvailDet.ClaimReimbursement,
                            "@AssistanceDetails",incentive.AvailDet.AssistanceDetails==null ? "" : incentive.AvailDet.AssistanceDetails.SerializeToXMLString(),
                            "@IncentiveAvailed",incentive.AvailDet.IncentiveAvailed==null ? "" : incentive.AvailDet.IncentiveAvailed.SerializeToXMLString(),
                            
                            ////Parameters for Additional Documents",,------------------------------------------------------------------------
                            "@P_INT_OSPCBNOC",incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC,
                            "@P_INT_OSPCBCONSENT",incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent,
                            "@P_INT_OSPCBEXCISE",incentive.AdditionalDocument.intStatutoryCleanCentralExec==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanCentralExec,
                            "@P_INT_OSPCBFSHGSCD",incentive.AdditionalDocument.intStatutoryCleanFSHGSCD==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanFSHGSCD,
                            "@P_INT_OSPCBEXPOLSIVE",incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC,
                            "@P_VCH_STATUTORYCLEARANCE",incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB==null ? "" : incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB,
                            "@P_VCHSTCLEANCONSENTOSPCB",incentive.AdditionalDocument.strStCleanConsentOSPCB==null ? "" : incentive.AdditionalDocument.strStCleanConsentOSPCB,
                            "@P_VCHCLEARANCECETIFTOSPCB",incentive.AdditionalDocument.strClearanceCetiftOSPCB==null ? "" : incentive.AdditionalDocument.strClearanceCetiftOSPCB,
                            "@P_VCHVALIDSATUTORYGREENCATEGORY",incentive.AdditionalDocument.strValidSatutoryGreenCategory==null ? "" : incentive.AdditionalDocument.strValidSatutoryGreenCategory,
                            "@P_VCHCONDODOCUMENTATIONDELAY",incentive.AdditionalDocument.strCondoDocumentationDelay==null ? "" : incentive.AdditionalDocument.strCondoDocumentationDelay,
                            "@P_XML_FILES",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(),  
                            
                            ////Parameters for Technical Know How Claim Details",,-----------
                            "@P_INT_TECHNICAL_CLAIM",incentive.TechnicalKnowDet.INT_TECHNICAL_CLAIM==null ? 0 : incentive.TechnicalKnowDet.INT_TECHNICAL_CLAIM,
                            "@P_VCH_BRIEF_ON_TECHNICAL",incentive.TechnicalKnowDet.STR_BRIEF_ON_TECHNICAL==null ? "" : incentive.TechnicalKnowDet.STR_BRIEF_ON_TECHNICAL,
                            "@P_TechnicalKnowHowClaim_XML",incentive.TechnicalKnowDet.Technicalknowdetails==null ? "" : incentive.TechnicalKnowDet.Technicalknowdetails.SerializeToXMLString(),
                            
                            //Common Attributes",,
                            "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                            "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                            "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                            "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                            "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                            "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                            "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                            "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                            "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                            "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),
                        };
        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_TECHKHW", out ss, sqlParam);
        return ss.ToString();
    }

    public string CreateIncentiveLandSubsidy(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();
        object[] sqlParam = { 
                                "@SectionNo_1", sectionList[1],
                                "@SectionNo_6", sectionList[6],
                                "@SectionNo_16", sectionList[16],
                                
                                //Industrial
                                "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? 0 : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,
                                "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,
                                "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ? 0 : incentive.IndsutUnitMstDet.APPLYBY_IND,
                                "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? 0 : incentive.IndsutUnitMstDet.AADHAARNO_IND,
                                "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? 0 : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                                "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? 0 : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,
 
                                //Parameters for Additional Documents",

                                 "@P_INT_OSPCBNOC",incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC,
                                "@P_INT_OSPCBCONSENT",incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent,
                                "@P_INT_OSPCBEXCISE",incentive.AdditionalDocument.intStatutoryCleanCentralExec==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanCentralExec,
                                "@P_INT_OSPCBFSHGSCD",incentive.AdditionalDocument.intStatutoryCleanFSHGSCD==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanFSHGSCD,
                                "@P_INT_OSPCBEXPOLSIVE",incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC,
                                "@P_VCH_STATUTORYCLEARANCE",incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB==null ? "" : incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB,
                                "@P_VCHSTCLEANCONSENTOSPCB",incentive.AdditionalDocument.strStCleanConsentOSPCB==null ? "" : incentive.AdditionalDocument.strStCleanConsentOSPCB,
                                "@P_VCHCLEARANCECETIFTOSPCB",incentive.AdditionalDocument.strClearanceCetiftOSPCB==null ? "" : incentive.AdditionalDocument.strClearanceCetiftOSPCB,
                                "@P_VCHVALIDSATUTORYGREENCATEGORY",incentive.AdditionalDocument.strValidSatutoryGreenCategory==null ? "" : incentive.AdditionalDocument.strValidSatutoryGreenCategory,
                                "@P_VCHCONDODOCUMENTATIONDELAY",incentive.AdditionalDocument.strCondoDocumentationDelay==null ? "" : incentive.AdditionalDocument.strCondoDocumentationDelay,
                                "@P_XML_FILES",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(), 

                              


                                //Parameters for Land Details",,
                                "@P_VCHCOSTOFPROJECT",incentive.LandDet.CostofProject==null ? "" : incentive.LandDet.CostofProject,
                                "@P_VCHLANDAREAPERPROJECT",incentive.LandDet.LandRequiredAsperReport==null ? "" : incentive.LandDet.LandRequiredAsperReport,
                                "@P_VCHLANDAREA",incentive.LandDet.LandRequired==null ? "" : incentive.LandDet.LandRequired,
                                "@P_VCHLANDDOCUMENT",incentive.LandDet.LandDocument==null ? "" : incentive.LandDet.LandDocument,
                                "@P_VCHLANDUNDERTAKINGDOC",incentive.LandDet.LANDUNDERTAKINGDOC==null ? "" : incentive.LandDet.LANDUNDERTAKINGDOC,
                                "@P_XMLLANDDETAILS",incentive.LandDet.Landconverted==null ? "" : incentive.LandDet.Landconverted.SerializeToXMLString(),
                                //"@P_INT_STATUS",incentive.LandDet.StausOfLandApply==null ? 0 : incentive.LandDet.StausOfLandApply,
                               


                               ////Common Attributes",,
                                "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                                "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                                "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                                "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                                "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                                "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                                "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                                "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                                "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                                "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),

                                };
        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_LandSubsidy", out ss, sqlParam);
        return ss.ToString();


    }

    public DataSet PostpopulateDataSPM(int userid)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  "@CHRACTIONCODE","V",
	                           "@P_INTUNQINCID",userid==null?0:userid,
	                        };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_VIEW_COMMON_POSTPOPULATE_SPM", sqlParam);

        return ds;
    }

    public string CreateIncentiveSubsidyPlant_MAchinery(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");

        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();
        object[] sqlParam = { 
                            "@SectionNo_1", sectionList[1],
                            "@SectionNo_3", sectionList[3],
                            "@SectionNo_8", sectionList[8],
                            "@SectionNo_10", sectionList[10],

                            ////Parameters for Industrial Unit's Details",,
                            ////--------------
                            "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? 0 : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,
                            "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,
                            "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ? 0 : incentive.IndsutUnitMstDet.APPLYBY_IND,
                            "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? 0 : incentive.IndsutUnitMstDet.AADHAARNO_IND,
                            "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? 0 : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                            "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? 0 : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,

                            //Parameters for Investment Details",,
                            "@P_VCH_Document_in_support",incentive.InvestmentDet.Document_in_support==null ? "" : incentive.InvestmentDet.Document_in_support,
                            "@P_vchNewBillDoc",incentive.InvestmentDet.vchNewBillDoc==null ? "" : incentive.InvestmentDet.vchNewBillDoc,
                            "@P_vchSecHandDoc",incentive.InvestmentDet.vchSecHandDoc==null ? "" : incentive.InvestmentDet.vchSecHandDoc,
                            "@P_vchSecHandBill",incentive.InvestmentDet.vchSecHandBill==null ? "" : incentive.InvestmentDet.vchSecHandBill,

                            //Parameters for Availed Details",,
                            "@NeverAvailedPrior",incentive.AvailDet.NeverAvailedPrior==null ? 0 : incentive.AvailDet.NeverAvailedPrior,
                            "@UndertakingSubsidyDoc",incentive.AvailDet.UndertakingSubsidyDoc==null ? "" : incentive.AvailDet.UndertakingSubsidyDoc,
                            "@SubsidyAvailed",incentive.AvailDet.SubsidyAvailed==null ? 0 : incentive.AvailDet.SubsidyAvailed,
                            "@SupportingDocs",incentive.AvailDet.SanctionOrderDoc==null ? "" : incentive.AvailDet.SanctionOrderDoc,
                            "@ClaimtExempted",incentive.AvailDet.ClaimtExempted==null ? 0 : incentive.AvailDet.ClaimtExempted,
                            "@ClaimReimbursement",incentive.AvailDet.ClaimReimbursement==null ? 0 : incentive.AvailDet.ClaimReimbursement,
                            "@AssistanceDetails",incentive.AvailDet.AssistanceDetails==null ? "" : incentive.AvailDet.AssistanceDetails.SerializeToXMLString(),
                            "@IncentiveAvailed",incentive.AvailDet.IncentiveAvailed==null ? "" : incentive.AvailDet.IncentiveAvailed.SerializeToXMLString(),

                            //Parameters for Bank Details",,
                            "@P_INTBANKID",incentive.BankDet.BankId==null ? 0 : incentive.BankDet.BankId,
                            "@P_VCHACCOUNTNO",incentive.BankDet.AccountNo==null ? "" : incentive.BankDet.AccountNo,
                            "@P_VCHBANKNAME",incentive.BankDet.BankName==null ? "" : incentive.BankDet.BankName,
                            "@P_VCHBRANCHNAME",incentive.BankDet.BranchName==null ? "" : incentive.BankDet.BranchName,
                            "@P_VCHIFSCNO",incentive.BankDet.IFSCNo==null ? "" : incentive.BankDet.IFSCNo,
                            "@P_VCHMICR",incentive.BankDet.MICRNo==null ? "" : incentive.BankDet.MICRNo,
                            "@P_BankDoc",incentive.BankDet.BankDoc==null ? "" : incentive.BankDet.BankDoc,

                            ////Common Attributes",,
                            "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                            "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                            "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                            "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                            "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                            "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                            "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                            "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                            "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                            "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),
                            };
        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_SPM", out ss, sqlParam);
        return ss.ToString();
    }

    public DataSet PostpopulateDataLand(int userid)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  "@CHRACTIONCODE","V",
	                           "@P_INTUNQINCID",userid==null?0:userid,
	                        };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_VIEW_COMMON_POSTPOPULATE_LAND", sqlParam);

        return ds;
    }

    public DataSet GetIncentiveAnchorTenant(Incentive incentive)
    {

        DataSet ds = new DataSet();
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = {  "@CHRACTIONCODE",sectionList[0],
	                           "@P_INTID_A",incentive.GetVwPrmtrs.Param1ID==null?"":incentive.GetVwPrmtrs.Param1ID,
	                           "@P_INTID_B",incentive.GetVwPrmtrs.Param2ID==null?"":incentive.GetVwPrmtrs.Param2ID, 
	                           "@P_INTID_C",incentive.GetVwPrmtrs.Param3ID==null?"":incentive.GetVwPrmtrs.Param3ID,
	                           "@P_INTID_D",incentive.GetVwPrmtrs.Param4ID==null?"":incentive.GetVwPrmtrs.Param4ID, 
	                           "@P_VCHID_E" ,incentive.GetVwPrmtrs.Param5==null?"":incentive.GetVwPrmtrs.Param5,
	                           "@P_VCHID_F" ,incentive.GetVwPrmtrs.Param6==null?"":incentive.GetVwPrmtrs.Param6,
	                           "@P_VCHID_G",incentive.GetVwPrmtrs.Param7==null?"":incentive.GetVwPrmtrs.Param7,
	                           "@P_DTMFROM",incentive.GetVwPrmtrs.FrmDate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.FrmDate,
	                           "@P_DTMTO",incentive.GetVwPrmtrs.Todate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.Todate,
                               "@P_INCTTYPE",incentive.GetVwPrmtrs.InctType==null?0:incentive.GetVwPrmtrs.InctType,
                               "@P_INCTUNQID",incentive.UnqIncentiveId==null?0:incentive.UnqIncentiveId,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_FormsViews_AtntAndTrnSubsidy", sqlParam);

        return ds;
    }

    public DataSet GetIncentiveOneTmReim(Incentive incentive)
    {
        DataSet ds = new DataSet();
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = {  "@CHRACTIONCODE",sectionList[0],
	                           "@P_INTID_A",incentive.GetVwPrmtrs.Param1ID==null?"":incentive.GetVwPrmtrs.Param1ID,
	                           "@P_INTID_B",incentive.GetVwPrmtrs.Param2ID==null?"":incentive.GetVwPrmtrs.Param2ID, 
	                           "@P_INTID_C",incentive.GetVwPrmtrs.Param3ID==null?"":incentive.GetVwPrmtrs.Param3ID,
	                           "@P_INTID_D",incentive.GetVwPrmtrs.Param4ID==null?"":incentive.GetVwPrmtrs.Param4ID, 
	                           "@P_VCHID_E" ,incentive.GetVwPrmtrs.Param5==null?"":incentive.GetVwPrmtrs.Param5,
	                           "@P_VCHID_F" ,incentive.GetVwPrmtrs.Param6==null?"":incentive.GetVwPrmtrs.Param6,
	                           "@P_VCHID_G",incentive.GetVwPrmtrs.Param7==null?"":incentive.GetVwPrmtrs.Param7,
	                           "@P_DTMFROM",incentive.GetVwPrmtrs.FrmDate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.FrmDate,
	                           "@P_DTMTO",incentive.GetVwPrmtrs.Todate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.Todate,
                               "@P_INCTTYPE",incentive.GetVwPrmtrs.InctType==null?0:incentive.GetVwPrmtrs.InctType,
                               "@P_INCTUNQID",incentive.UnqIncentiveId==null?0:incentive.UnqIncentiveId,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_FormsViews_OneTmReim", sqlParam);

        return ds;
    }

    public string CreateIncentive_OneTimeReimbursement(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");

        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 
                "@SectionNo_1", sectionList[1],
                "@SectionNo_6", sectionList[6],
                "@SectionNo_8", sectionList[8],
                "@SectionNo_17", sectionList[17],
                "@SectionNo_18", sectionList[18],
                "@SectionNo_19", sectionList[19],
              
                //Parameters for Industrial Unit's Details",,		
                "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,

                //////////Parameters for Additional Documents",,
                "@P_INT_OSPCBNOC",incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC,
                "@P_INT_OSPCBCONSENT",incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent,
                "@P_INT_OSPCBEXCISE",incentive.AdditionalDocument.intStatutoryCleanCentralExec==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanCentralExec,
                "@P_INT_OSPCBFSHGSCD",incentive.AdditionalDocument.intStatutoryCleanFSHGSCD==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanFSHGSCD,
                "@P_INT_OSPCBEXPOLSIVE",incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC,
                "@P_VCH_STATUTORYCLEARANCE",incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB==null ? "" : incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB,
                "@P_VCHSTCLEANCONSENTOSPCB",incentive.AdditionalDocument.strStCleanConsentOSPCB==null ? "" : incentive.AdditionalDocument.strStCleanConsentOSPCB,
                "@P_VCHCLEARANCECETIFTOSPCB",incentive.AdditionalDocument.strClearanceCetiftOSPCB==null ? "" : incentive.AdditionalDocument.strClearanceCetiftOSPCB,
                "@P_VCHVALIDSATUTORYGREENCATEGORY",incentive.AdditionalDocument.strValidSatutoryGreenCategory==null ? "" : incentive.AdditionalDocument.strValidSatutoryGreenCategory,
                "@P_VCHCONDODOCUMENTATIONDELAY",incentive.AdditionalDocument.strCondoDocumentationDelay==null ? "" : incentive.AdditionalDocument.strCondoDocumentationDelay,
                "@P_XML_FILES",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(),  
                
                ////////Parameters for Availed Details",,
                "@NeverAvailedPrior",incentive.AvailDet.NeverAvailedPrior==null ? 0 : incentive.AvailDet.NeverAvailedPrior,
                "@UndertakingSubsidyDoc",incentive.AvailDet.UndertakingSubsidyDoc==null ? "" : incentive.AvailDet.UndertakingSubsidyDoc,
                "@SubsidyAvailed",incentive.AvailDet.SubsidyAvailed==null ? 0 : incentive.AvailDet.SubsidyAvailed,
                "@SupportingDocs",incentive.AvailDet.SanctionOrderDoc==null ? "" : incentive.AvailDet.SanctionOrderDoc,
                "@ClaimtExempted",incentive.AvailDet.ClaimtExempted==null ? 0 : incentive.AvailDet.ClaimtExempted,
                "@ClaimReimbursement",incentive.AvailDet.ClaimReimbursement==null ? 0 : incentive.AvailDet.ClaimReimbursement,
                "@AssistanceDetails",incentive.AvailDet.AssistanceDetails==null ? "" : incentive.AvailDet.AssistanceDetails.SerializeToXMLString(),
                "@IncentiveAvailed",incentive.AvailDet.IncentiveAvailed==null ? "" : incentive.AvailDet.IncentiveAvailed.SerializeToXMLString(),

                //////Parameters for Electricity Consumption / Load Details",,
                "@P_INTCONSUMEID",incentive.ConsumLoadDet.INTCONSUMEID==null ? "0" : incentive.ConsumLoadDet.INTCONSUMEID,
                "@P_DTMSUPPLYDATE",incentive.ConsumLoadDet.stringSUPPLYDATE==null ? "1900/01/01" : incentive.ConsumLoadDet.stringSUPPLYDATE,
                "@P_VCHCONSUMENUMBER",incentive.ConsumLoadDet.stringCONSUMENUMBER==null ? "" : incentive.ConsumLoadDet.stringCONSUMENUMBER,
                "@P_VCHCONNECTEDLOAD",incentive.ConsumLoadDet.stringCONNECTEDLOAD==null ? "" : incentive.ConsumLoadDet.stringCONNECTEDLOAD,
                "@P_INTELECTRICITYAVIL",incentive.ConsumLoadDet.INTELECTRICITYAVIL==null ? "0" : incentive.ConsumLoadDet.INTELECTRICITYAVIL,
                "@P_STATEDETAIL_XML",incentive.ConsumLoadDet.TstrSTATEDETAIL==null ? "" : incentive.ConsumLoadDet.TstrSTATEDETAIL.SerializeToXMLString(),
                "@P_VCHPMTBILLDOC",incentive.ConsumLoadDet.strbillpmtvoucher==null ? "" : incentive.ConsumLoadDet.strbillpmtvoucher,
                "@P_VCHEXEMPELECDUTY",incentive.ConsumLoadDet.strelecconsumption==null ? "" : incentive.ConsumLoadDet.strelecconsumption,
                "@P_VCHELECDETAILS",incentive.ConsumLoadDet.strElecDetails==null? "" : incentive.ConsumLoadDet.strElecDetails,

                //////Parameters for Contract Demand & Connected Load Details",,
                "@P_INTCONLOADID",incentive.ContractLoadDet.intconctdemamdid==null ? 0 : incentive.ContractLoadDet.intconctdemamdid,
                "@P_XMLLOANDETAIL",incentive.ContractLoadDet.ContractLoanDet==null ? "" : incentive.ContractLoadDet.ContractLoanDet.SerializeToXMLString(),
                "@P_VCHDEMANDFILE",incentive.ContractLoadDet.strcdemandfile==null ? "" : incentive.ContractLoadDet.strcdemandfile,

                //////Parameters for Energy Audit Details",,
                "@EnergyAuditID",incentive.EnergyAuditDet.intEnergyAuditID== null ? 0 :incentive.EnergyAuditDet.intEnergyAuditID,
                "@EnergyAuditorName",incentive.EnergyAuditDet.strEnergyAuditorName== null ? "" :incentive.EnergyAuditDet.strEnergyAuditorName,
                "@EnergyAuditorDocName",incentive.EnergyAuditDet.strEnergyAuditorDocName== null ? "" :incentive.EnergyAuditDet.strEnergyAuditorDocName,
                "@EnergyAuditorAddress",incentive.EnergyAuditDet.strEnergyAuditorAddress== null ? "" :incentive.EnergyAuditDet.strEnergyAuditorAddress,
                "@EnergyAuditorAccreditation",incentive.EnergyAuditDet.strEnergyAuditorAccreditation== null ? "" :incentive.EnergyAuditDet.strEnergyAuditorAccreditation,
                "@EnergyAuditorAccreditationDoc",incentive.EnergyAuditDet.strEnergyAuditorAccreditationDoc== null ? "" :incentive.EnergyAuditDet.strEnergyAuditorAccreditationDoc,
                "@Expenditureincurred",incentive.EnergyAuditDet.strExpenditureincurred== null ? 0 : incentive.EnergyAuditDet.strExpenditureincurred,
                "@ExpenditureincurredDoc",incentive.EnergyAuditDet.strExpenditureincurredDoc== null ? "" :incentive.EnergyAuditDet.strExpenditureincurredDoc,
                "@SuccessfulcompletionAuditDate",incentive.EnergyAuditDet.dtmSuccessfulcompletionAuditDate== null ? Convert.ToDateTime("1/1/1900") :incentive.EnergyAuditDet.dtmSuccessfulcompletionAuditDate,
                "@SupportofimplementationofEnergyDoc",incentive.EnergyAuditDet.strSupportofimplementationofEnergyDOC== null ? "" :incentive.EnergyAuditDet.strSupportofimplementationofEnergyDOC,
                "@SuccessfulcompletionAuditDoc",incentive.EnergyAuditDet.strSuccessfulcompletionAuditDOC== null ? "" :incentive.EnergyAuditDet.strSuccessfulcompletionAuditDOC,
                "@EnergyConsumptionAfter",incentive.EnergyAuditDet.dtmEnergyConsumptionAfter== null ? 0 :incentive.EnergyAuditDet.dtmEnergyConsumptionAfter,
                "@EnergyConsumptionBefore",incentive.EnergyAuditDet.strEnergyConsumptionBefore== null ? 0 :incentive.EnergyAuditDet.strEnergyConsumptionBefore,
                "@ReductionOfEnergyDoc",incentive.EnergyAuditDet.strReductionOfEnergyDoc== null ? "" :incentive.EnergyAuditDet.strReductionOfEnergyDoc,
                "@EnergyEfficiencyCertificate",incentive.EnergyAuditDet.strEnergyEfficiencyCertificate== null ? "" :incentive.EnergyAuditDet.strEnergyEfficiencyCertificate,
                "@strCarbonFootprintDoc",incentive.EnergyAuditDet.strCarbonFootprintDoc== null ? "" :incentive.EnergyAuditDet.strCarbonFootprintDoc,

                //Common Attributes",,
                "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),
                };

        object ss = "";
        SqlHelper.ExecuteNonQuery(conn, "USP_Insentive_Forms_OneTmReim", out ss, sqlParam);
        return ss.ToString();
    }

    public DataSet GetIncentivePioneer(Incentive incentive)
    {

        DataSet ds = new DataSet();
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = {  "@CHRACTIONCODE",sectionList[0],
	                           "@P_INTID_A",incentive.GetVwPrmtrs.Param1ID==null?"":incentive.GetVwPrmtrs.Param1ID,
	                           "@P_INTID_B",incentive.GetVwPrmtrs.Param2ID==null?"":incentive.GetVwPrmtrs.Param2ID, 
	                           "@P_INTID_C",incentive.GetVwPrmtrs.Param3ID==null?"":incentive.GetVwPrmtrs.Param3ID,
	                           "@P_INTID_D",incentive.GetVwPrmtrs.Param4ID==null?"":incentive.GetVwPrmtrs.Param4ID, 
	                           "@P_VCHID_E" ,incentive.GetVwPrmtrs.Param5==null?"":incentive.GetVwPrmtrs.Param5,
	                           "@P_VCHID_F" ,incentive.GetVwPrmtrs.Param6==null?"":incentive.GetVwPrmtrs.Param6,
	                           "@P_VCHID_G",incentive.GetVwPrmtrs.Param7==null?"":incentive.GetVwPrmtrs.Param7,
	                           "@P_DTMFROM",incentive.GetVwPrmtrs.FrmDate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.FrmDate,
	                           "@P_DTMTO",incentive.GetVwPrmtrs.Todate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.Todate,
                               "@P_INCTTYPE",incentive.GetVwPrmtrs.InctType==null?0:incentive.GetVwPrmtrs.InctType,
                               "@P_INCTUNQID",incentive.UnqIncentiveId==null?0:incentive.UnqIncentiveId,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_FormsViews_Pioneer", sqlParam);

        return ds;
    }

    public DataTable GetFile_FromDocMaster_UnitCodeWise(string UnitCode)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  
                               "@UnitCode",UnitCode
	                        };

        ds = SqlHelper.ExecuteDataset(conn, "usp_getFile_Document_Master_UnitCodeWise", sqlParam);



        return ds.Tables[0];
    }

    public string CreateIncentiveQualityCertificate(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");

        object[] sqlParam = { 
                //Parameters for Industrial Unit's Details",,
                "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,
                
                //Parameters for Quality Certificate Details",,
                "@CertificationID",incentive.QualityCertDet.intCertificationID== null ? 0 :incentive.QualityCertDet.intCertificationID,
                "@P_XmlQuality",incentive.QualityCertDet.QualityCertificationActivitiesDetails== null ? "" : incentive.QualityCertDet.QualityCertificationActivitiesDetails.SerializeToXMLString(),
                "@Total",incentive.QualityCertDet.decCertificationTotal== null ? 0 :incentive.QualityCertDet.decCertificationTotal,
                    
                ////////Parameters for Availed Details",,
                "@NeverAvailedPrior",incentive.AvailDet.NeverAvailedPrior==null ? 0 : incentive.AvailDet.NeverAvailedPrior,
                "@UndertakingSubsidyDoc",incentive.AvailDet.UndertakingSubsidyDoc==null ? "" : incentive.AvailDet.UndertakingSubsidyDoc,
                "@SubsidyAvailed",incentive.AvailDet.SubsidyAvailed==null ? 0 : incentive.AvailDet.SubsidyAvailed,
                "@SupportingDocs",incentive.AvailDet.SanctionOrderDoc==null ? "" : incentive.AvailDet.SanctionOrderDoc,
                "@ClaimtExempted",incentive.AvailDet.ClaimtExempted==null ? 0 : incentive.AvailDet.ClaimtExempted,
                "@ClaimReimbursement",incentive.AvailDet.ClaimReimbursement==null ? 0 : incentive.AvailDet.ClaimReimbursement,
                "@AssistanceDetails",incentive.AvailDet.AssistanceDetails==null ? "" : incentive.AvailDet.AssistanceDetails.SerializeToXMLString(),
                "@IncentiveAvailed",incentive.AvailDet.IncentiveAvailed==null ? "" : incentive.AvailDet.IncentiveAvailed.SerializeToXMLString(),

                //Parameters for Additional Documents"
                "@P_INT_OSPCBNOC",incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC,
                "@P_INT_OSPCBCONSENT",incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent,
                "@P_INT_OSPCBEXCISE",incentive.AdditionalDocument.intStatutoryCleanCentralExec==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanCentralExec,
                "@P_INT_OSPCBFSHGSCD",incentive.AdditionalDocument.intStatutoryCleanFSHGSCD==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanFSHGSCD,
                "@P_INT_OSPCBEXPOLSIVE",incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC,
                "@P_VCH_STATUTORYCLEARANCE",incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB==null ? "" : incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB,
                "@P_VCHSTCLEANCONSENTOSPCB",incentive.AdditionalDocument.strStCleanConsentOSPCB==null ? "" : incentive.AdditionalDocument.strStCleanConsentOSPCB,
                "@P_VCHCLEARANCECETIFTOSPCB",incentive.AdditionalDocument.strClearanceCetiftOSPCB==null ? "" : incentive.AdditionalDocument.strClearanceCetiftOSPCB,
                "@P_VCHVALIDSATUTORYGREENCATEGORY",incentive.AdditionalDocument.strValidSatutoryGreenCategory==null ? "" : incentive.AdditionalDocument.strValidSatutoryGreenCategory,
                "@P_VCHCONDODOCUMENTATIONDELAY",incentive.AdditionalDocument.strCondoDocumentationDelay==null ? "" : incentive.AdditionalDocument.strCondoDocumentationDelay,
                "@P_XML_FILES",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(), 
                                        
                //Common Attributes",,
                "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                "@xmlFiles",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),
                "@P_F_YEAR",incentive.FYear== null ? 0 :incentive.FYear,
              };

        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_QualityCertificate", out ss, sqlParam);
        return ss.ToString();
    }

    public DataSet GetIncentiveQuality(Incentive incentive)
    {

        DataSet ds = new DataSet();
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = {  
                               "@CHRACTIONCODE",sectionList[0],
	                           "@P_INTID_A",incentive.GetVwPrmtrs.Param1ID==null?"":incentive.GetVwPrmtrs.Param1ID,
	                           "@P_INTID_B",incentive.GetVwPrmtrs.Param2ID==null?"":incentive.GetVwPrmtrs.Param2ID, 
	                           "@P_INTID_C",incentive.GetVwPrmtrs.Param3ID==null?"":incentive.GetVwPrmtrs.Param3ID,
	                           "@P_INTID_D",incentive.GetVwPrmtrs.Param4ID==null?"":incentive.GetVwPrmtrs.Param4ID, 
	                           "@P_VCHID_E" ,incentive.GetVwPrmtrs.Param5==null?"":incentive.GetVwPrmtrs.Param5,
	                           "@P_VCHID_F" ,incentive.GetVwPrmtrs.Param6==null?"":incentive.GetVwPrmtrs.Param6,
	                           "@P_VCHID_G",incentive.GetVwPrmtrs.Param7==null?"":incentive.GetVwPrmtrs.Param7,
	                           "@P_DTMFROM",incentive.GetVwPrmtrs.FrmDate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.FrmDate,
	                           "@P_DTMTO",incentive.GetVwPrmtrs.Todate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.Todate,
                               "@P_INCTTYPE",incentive.GetVwPrmtrs.InctType==null?0:incentive.GetVwPrmtrs.InctType,
                               "@P_INCTUNQID",incentive.UnqIncentiveId==null?0:incentive.UnqIncentiveId,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_FormsViews_QualityCertification", sqlParam);

        return ds;
    }

    public DataSet GetIncentivePower(Incentive incentive)
    {

        DataSet ds = new DataSet();
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = {  
                               "@P_INCTUNQID",incentive.UnqIncentiveId==null?0:incentive.UnqIncentiveId,
                            };

        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_FormsViews_power", sqlParam);
        return ds;
    }

    public string CreateIncentivePowerTariff(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 



                    //Parameters for Industrial Unit's Details",,
                    "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                    "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                    "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                    "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                    "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                    "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,

                    //Parameters for Details for Reimbursement of Power Traffic",,
                    "@decNewInvestment_SchematicProvisions",incentive.PowerTariffDet.NewInvestment_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.NewInvestment_SchematicProvisions,
                    "@decNewInvestment_TillDateOfCommencementOfProduction",incentive.PowerTariffDet.NewInvestment_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.NewInvestment_TillDateOfCommencementOfProduction,
                    "@vchNewInvestment_reasons",incentive.PowerTariffDet.NewInvestment_reasons== null ? "" :incentive.PowerTariffDet.NewInvestment_reasons,
                    "@decLand_SchematicProvisions",incentive.PowerTariffDet.Land_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.Land_SchematicProvisions,
                    "@decLand_TillDateOfCommencementOfProduction",incentive.PowerTariffDet.Land_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.Land_TillDateOfCommencementOfProduction,
                    "@vchLand_reasons",incentive.PowerTariffDet.Land_reasons== null ? "" :incentive.PowerTariffDet.Land_reasons,
                    "@decBuilding_SchematicProvisions",incentive.PowerTariffDet.Building_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.Building_SchematicProvisions,
                    "@decBuilding_TillDateOfCommencementOfProduction",incentive.PowerTariffDet.Building_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.Building_TillDateOfCommencementOfProduction,
                    "@vchBuilding_reasons",incentive.PowerTariffDet.Building_reasons== null ? "" :incentive.PowerTariffDet.Building_reasons,
                    "@decPlantMachinery_SchematicProvisions",incentive.PowerTariffDet.PlantMachinery_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.PlantMachinery_SchematicProvisions,
                    "@decPlantMachinery_TillDateOfCommencementOfProduction",incentive.PowerTariffDet.PlantMachinery_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.PlantMachinery_TillDateOfCommencementOfProduction,
                    "@vchPlantMachinery_reasons",incentive.PowerTariffDet.PlantMachinery_reasons== null ? "" :incentive.PowerTariffDet.PlantMachinery_reasons,
                    "@decOtherFixedAssets_SchematicProvisions",incentive.PowerTariffDet.OtherFixedAssets_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.OtherFixedAssets_SchematicProvisions,
                    "@decOtherFixedAssets_TillDateOfCommencementOfProduction",incentive.PowerTariffDet.OtherFixedAssets_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.OtherFixedAssets_TillDateOfCommencementOfProduction,
                    "@vchOtherFixedAssets_reasons",incentive.PowerTariffDet.OtherFixedAssets_reasons== null ? "" :incentive.PowerTariffDet.OtherFixedAssets_reasons,
                    "@decElectricalInstallations_SchematicProvisions",incentive.PowerTariffDet.ElectricalInstallations_SchematicProvisions== null ? 0 :incentive.PowerTariffDet.ElectricalInstallations_SchematicProvisions,
                    "@decElectricalInstallations_TillDateOfCommencementOfProduction",incentive.PowerTariffDet.ElectricalInstallations_TillDateOfCommencementOfProduction== null ? 0 :incentive.PowerTariffDet.ElectricalInstallations_TillDateOfCommencementOfProduction,
                    "@vchElectricalInstallations_reasons",incentive.PowerTariffDet.ElectricalInstallations_reasons== null ? "" :incentive.PowerTariffDet.ElectricalInstallations_reasons,
                    "@vchJustificationForExcessInvestment",incentive.PowerTariffDet.JustificationForExcessInvestment== null ? "" :incentive.PowerTariffDet.JustificationForExcessInvestment,
                    "@vchTotalUnitConsumed",incentive.PowerTariffDet.TotalUnitConsumed== null ? "" :incentive.PowerTariffDet.TotalUnitConsumed,
                    "@decAmountPaid",incentive.PowerTariffDet.AmountPaid== null ? 0 :incentive.PowerTariffDet.AmountPaid,
                    "@vch_MoneyReceiptFile",incentive.PowerTariffDet.MoneyReceipt== null ? "" :incentive.PowerTariffDet.MoneyReceipt,

                    //Parameters for Bank Details",,
                    "@P_INTBANKID",incentive.BankDet.BankId==null ? 0 : incentive.BankDet.BankId,
                    "@P_VCHACCOUNTNO",incentive.BankDet.AccountNo==null ? "" : incentive.BankDet.AccountNo,
                    "@P_VCHBANKNAME",incentive.BankDet.BankName==null ? "" : incentive.BankDet.BankName,
                    "@P_VCHBRANCHNAME",incentive.BankDet.BranchName==null ? "" : incentive.BankDet.BranchName,
                    "@P_VCHIFSCNO",incentive.BankDet.IFSCNo==null ? "" : incentive.BankDet.IFSCNo,
                    "@P_VCHMICR",incentive.BankDet.MICRNo==null ? "" : incentive.BankDet.MICRNo,
                    "@P_BankDoc",incentive.BankDet.BankDoc==null ? "" : incentive.BankDet.BankDoc,

                    ////Parameters for Additional Documents",,
                    "@P_INT_OSPCBNOC",incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC,
                    "@P_INT_OSPCBCONSENT",incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent,
                    "@P_INT_OSPCBEXCISE",incentive.AdditionalDocument.intStatutoryCleanCentralExec==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanCentralExec,
                    "@P_INT_OSPCBFSHGSCD",incentive.AdditionalDocument.intStatutoryCleanFSHGSCD==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanFSHGSCD,
                    "@P_INT_OSPCBEXPOLSIVE",incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC,
                    "@P_VCHSTCLEANCONSENTOSPCB",incentive.AdditionalDocument.strStCleanConsentOSPCB==null ? "" : incentive.AdditionalDocument.strStCleanConsentOSPCB,
                    "@P_VCH_STATUTORYCLEARANCE",incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB==null ? "" : incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB,
                    "@P_VCHCLEARANCECETIFTOSPCB",incentive.AdditionalDocument.strClearanceCetiftOSPCB==null ? "" : incentive.AdditionalDocument.strClearanceCetiftOSPCB,
                    "@P_VCHVALIDSATUTORYGREENCATEGORY",incentive.AdditionalDocument.strValidSatutoryGreenCategory==null ? "" : incentive.AdditionalDocument.strValidSatutoryGreenCategory,
                    "@P_VCHCONDODOCUMENTATIONDELAY",incentive.AdditionalDocument.strCondoDocumentationDelay==null ? "" : incentive.AdditionalDocument.strCondoDocumentationDelay,
                    "@P_VCHFILE",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(),
                    "@P_VCHDOCUMENTNAME",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(),
                    "@P_XML_FILES",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(),




                    //Common Attributes",,
                    "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                    "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                    "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                    "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                    "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                    "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                    "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                    "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                    "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                    "@xmlFiles",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),
                    "@P_F_YEAR",incentive.FYear== null ? 0 :incentive.FYear,

            };



        ////SqlHelper.ExecuteNonQuery(conn, "USP_Insentive_Jeevan", sqlParam);
        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_Powertariff", out ss, sqlParam);
        return ss.ToString();
    }

    public string CreateGrantPriority(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 
                      "@SectionNo_1", sectionList[1],
                      "@SectionNo_5", sectionList[5],
                      "@SectionNo_6", sectionList[6],
                      "@SectionNo_25", sectionList[25],
                      "@SectionNo_27", sectionList[27],



                      ////Parameters for Industrial Unit's Details",
		              "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                      "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                      "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                      "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                      "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                      "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,
                                        

                  
                      
                   //   ////Parameters for Priority Sector Details",,
 
                        "@P_INT_CERTAVAIL",incentive.PrioritySector.intAvailPriorityCertf==null ? 0 : incentive.PrioritySector.intAvailPriorityCertf,
                        "@P_VCH_SECTORCERT",incentive.PrioritySector.strPrioritycCertf2015==null ? "" : incentive.PrioritySector.strPrioritycCertf2015,
                        "@P_VCH_ACKNOW",incentive.PrioritySector.strAppcnAcknow==null ? "" : incentive.PrioritySector.strAppcnAcknow,                        
                        "@P_INT_SECTORID",incentive.PrioritySector.intSectorId == null ? 0 : incentive.PrioritySector.intSectorId,
                        "@P_INT_SUBSECTORID", incentive.PrioritySector.intSubSectorId == null ? 0 : incentive.PrioritySector.intSubSectorId,
                        "@P_VCH_DERIVEDSECTOR" ,incentive.PrioritySector.strDerivedSector == null ? "" : incentive.PrioritySector.strDerivedSector,
                        "@P_INT_LIESPRIORITY",incentive.PrioritySector.intLiesSector == null ? 0 : incentive.PrioritySector.intLiesSector,
                        "@P_INT_ACTIVITY", incentive.PrioritySector.intSpecificActivity == null ? 0 :incentive.PrioritySector.intSpecificActivity,
                        "@P_VCH_VCHACTIVITY",incentive.PrioritySector.strSpecificActivity == null ? "" :incentive.PrioritySector.strSpecificActivity,
                        "@P_VCH_PRESENTNOTESTAGE",incentive.PrioritySector.strNote == null ? "" :incentive.PrioritySector.strNote,
                        "@P_VCH_SUPPORTCERTDOC",incentive.PrioritySector.strSupportingDoc == null ? "" :incentive.PrioritySector.strSupportingDoc,



                        
                   //////Parameters for DLSWCA/ SLSWCA/ HLCA Approval Details",,

                    "@P_DTM_APPROVAL_DATE",incentive.DLSWCAApprovalDet.dtmApprovalDate== null ? null :incentive.DLSWCAApprovalDet.dtmApprovalDate,
                    "@P_DCM_LAND_REQ",incentive.DLSWCAApprovalDet.dcmLandRequired== null ? 0 :incentive.DLSWCAApprovalDet.dcmLandRequired,
                    "@P_DCM_LAND_COST",incentive.DLSWCAApprovalDet.dcmCostOfLand== null ? 0 :incentive.DLSWCAApprovalDet.dcmCostOfLand,
                    "@P_DCM_SUBSIDY_AMT",incentive.DLSWCAApprovalDet.dcmSubsidyAmount== null ? 0 :incentive.DLSWCAApprovalDet.dcmSubsidyAmount,
                    "@P_DCM_APPROVAL_DOC",incentive.DLSWCAApprovalDet.strDLSWCAApprovalDoc== null ? "" :incentive.DLSWCAApprovalDet.strDLSWCAApprovalDoc,
                    "@P_DCM_SUBSTANTIATE_DOC",incentive.DLSWCAApprovalDet.strsubstantitateDoc== null ? "" :incentive.DLSWCAApprovalDet.strsubstantitateDoc,
                    
                   // /////Availed incentive Earlier
                     
                      
                      "@P_XmlQuality",incentive.AvailedEarlier.ListAvailedEarlierDetails == null ? "" : incentive.AvailedEarlier.ListAvailedEarlierDetails.SerializeToXMLString(),
                                       
                   // ////Parameters for Additional Documents",,

                    "@P_INT_OSPCBNOC",incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC,
                    "@P_INT_OSPCBCONSENT",incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent,
                    "@P_INT_OSPCBEXCISE",incentive.AdditionalDocument.intStatutoryCleanCentralExec==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanCentralExec,
                    "@P_INT_OSPCBFSHGSCD",incentive.AdditionalDocument.intStatutoryCleanFSHGSCD==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanFSHGSCD,
                    "@P_INT_OSPCBEXPOLSIVE",incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC==null ? 0 : incentive.AdditionalDocument.intStatutoryCleanExplosive_NOC,
                    "@P_VCH_STATUTORYCLEARANCE",incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB==null ? "" : incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB,
                    "@P_VCHSTCLEANCONSENTOSPCB",incentive.AdditionalDocument.strStCleanConsentOSPCB==null ? "" : incentive.AdditionalDocument.strStCleanConsentOSPCB,
                    "@P_VCHCLEARANCECETIFTOSPCB",incentive.AdditionalDocument.strClearanceCetiftOSPCB==null ? "" : incentive.AdditionalDocument.strClearanceCetiftOSPCB,
                    "@P_VCHVALIDSATUTORYGREENCATEGORY",incentive.AdditionalDocument.strValidSatutoryGreenCategory==null ? "" : incentive.AdditionalDocument.strValidSatutoryGreenCategory,
                    "@P_VCHCONDODOCUMENTATIONDELAY",incentive.AdditionalDocument.strCondoDocumentationDelay==null ? "" : incentive.AdditionalDocument.strCondoDocumentationDelay,
                    "@P_XML_FILES",incentive.AdditionalDocument.AdditionalDetails==null ? "" : incentive.AdditionalDocument.AdditionalDetails.SerializeToXMLString(),  


                    //Common Attributes",,
                    "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                    "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                    "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                    "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                    "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                    "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                    "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                    "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                    "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                    "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),
                    "@P_INTMODE", incentive.IndsutUnitMstDet.Int_Mode == null?0:incentive.IndsutUnitMstDet.Int_Mode,
                    "@P_VCHPRIORITYDOC", incentive.IndsutUnitMstDet.Vch_Priorityfile == null ? "" : incentive.IndsutUnitMstDet.Vch_Priorityfile,
                    "@P_VCHSIGNATURE",incentive.IndsutUnitMstDet.vch_signfile == null ? "" : incentive.IndsutUnitMstDet.vch_signfile,

                    };

        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_Grantpriority", out ss, sqlParam);
        return ss.ToString();
    }

    public DataSet GetGrantPriority(Incentive incentive)
    {

        DataSet ds = new DataSet();
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = {  "@CHRACTIONCODE",sectionList[0],
	                           "@P_INTID_A",incentive.GetVwPrmtrs.Param1ID==null?"":incentive.GetVwPrmtrs.Param1ID,
	                           "@P_INTID_B",incentive.GetVwPrmtrs.Param2ID==null?"":incentive.GetVwPrmtrs.Param2ID, 
	                           "@P_INTID_C",incentive.GetVwPrmtrs.Param3ID==null?"":incentive.GetVwPrmtrs.Param3ID,
	                           "@P_INTID_D",incentive.GetVwPrmtrs.Param4ID==null?"":incentive.GetVwPrmtrs.Param4ID, 
	                           "@P_VCHID_E" ,incentive.GetVwPrmtrs.Param5==null?"":incentive.GetVwPrmtrs.Param5,
	                           "@P_VCHID_F" ,incentive.GetVwPrmtrs.Param6==null?"":incentive.GetVwPrmtrs.Param6,
	                           "@P_VCHID_G",incentive.GetVwPrmtrs.Param7==null?"":incentive.GetVwPrmtrs.Param7,
	                           "@P_DTMFROM",incentive.GetVwPrmtrs.FrmDate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.FrmDate,
	                           "@P_DTMTO",incentive.GetVwPrmtrs.Todate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.Todate,
                               "@P_INCTTYPE",incentive.GetVwPrmtrs.InctType==null?0:incentive.GetVwPrmtrs.InctType,
                               "@P_INCTUNQID",incentive.UnqIncentiveId==null?0:incentive.UnqIncentiveId,
                            };



        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_FormsViews_GrantPriority", sqlParam);

        return ds;
    }

    public DataSet InetersSubsidyView(int intUNQINCID)
    {

        DataSet ds = new DataSet();
        object[] sqlParam = {  "@P_IntIncUnqueId",intUNQINCID,
	                           
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_Forms_InterestSubsidy_view", sqlParam);

        return ds;
    }

    public DataSet PostpopulateDataPLUS(int userid)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  "@CHRACTIONCODE","V",
	                           "@P_INTUSERID",userid==null?0:userid,
	                        };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_VIEW_COMMON_PREPOPULATE_IPIM_PLUS", sqlParam);

        return ds;
    }

    public DataSet PostpopulateEnterpreneurshipSubsidy(Incentive incentive)
    {

        DataSet ds = new DataSet();
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = {  "@CHRACTIONCODE",sectionList[0],
	                           "@P_INTID_A",incentive.GetVwPrmtrs.Param1ID==null?"":incentive.GetVwPrmtrs.Param1ID,
	                           "@P_INTID_B",incentive.GetVwPrmtrs.Param2ID==null?"":incentive.GetVwPrmtrs.Param2ID, 
	                           "@P_INTID_C",incentive.GetVwPrmtrs.Param3ID==null?"":incentive.GetVwPrmtrs.Param3ID,
	                           "@P_INTID_D",incentive.GetVwPrmtrs.Param4ID==null?"":incentive.GetVwPrmtrs.Param4ID, 
	                           "@P_VCHID_E" ,incentive.GetVwPrmtrs.Param5==null?"":incentive.GetVwPrmtrs.Param5,
	                           "@P_VCHID_F" ,incentive.GetVwPrmtrs.Param6==null?"":incentive.GetVwPrmtrs.Param6,
	                           "@P_VCHID_G",incentive.GetVwPrmtrs.Param7==null?"":incentive.GetVwPrmtrs.Param7,
	                           "@P_DTMFROM",incentive.GetVwPrmtrs.FrmDate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.FrmDate,
	                           "@P_DTMTO",incentive.GetVwPrmtrs.Todate==null?Convert.ToDateTime("1/1/1900"):incentive.GetVwPrmtrs.Todate,
                               "@P_INCTTYPE",incentive.GetVwPrmtrs.InctType==null?0:incentive.GetVwPrmtrs.InctType,
                               "@P_INCTUNQID",incentive.UnqIncentiveId==null?0:incentive.UnqIncentiveId,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_Insentive_FormsViews_Ent_Subsidy", sqlParam);
        return ds;
    }

    public string CreateIncentiveEntSubsidy(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormType is not set.");


        ParamManager param = new ParamManager(incentive.FormType);
        int[] sectionList = param.GetSectionParam();

        object[] sqlParam = { 
                        "@SectionNo_1", sectionList[1],
                        "@SectionNo_8", sectionList[8],
                        "@SectionNo_10", sectionList[10],
                        "@SectionNo_13", sectionList[13],
                        "@SectionNo_14", sectionList[14],
                                             

                        ////Parameters for Industrial Unit's Details", 1
                        "@P_VCHAPPLICANTNAME",incentive.IndsutUnitMstDet.APPLICANTNAME_IND==null ? "" : incentive.IndsutUnitMstDet.APPLICANTNAME_IND,		
                        "@P_INTGENDER",incentive.IndsutUnitMstDet.GENDER_IND==null ? 0 : incentive.IndsutUnitMstDet.GENDER_IND,			
                        "@P_INTAPPLYBY",incentive.IndsutUnitMstDet.APPLYBY_IND==null ?0 : incentive.IndsutUnitMstDet.APPLYBY_IND,			
                        "@P_VCHAADHAARNO",incentive.IndsutUnitMstDet.AADHAARNO_IND==null ? "" : incentive.IndsutUnitMstDet.AADHAARNO_IND,			
                        "@P_VCHAUTHORIZEDFILENAME",incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND,
                        "@P_VCHAUTHORIZEDFILECODE",incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND==null ? "" : incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND,

                        ////Parameters for Bank Details",,
                        "@P_INTBANKID",incentive.BankDet.BankId==null ? 0 : incentive.BankDet.BankId,
                        "@P_VCHACCOUNTNO",incentive.BankDet.AccountNo==null ? "" : incentive.BankDet.AccountNo,
                        "@P_VCHBANKNAME",incentive.BankDet.BankName==null ? "" : incentive.BankDet.BankName,
                        "@P_VCHBRANCHNAME",incentive.BankDet.BranchName==null ? "" : incentive.BankDet.BranchName,
                        "@P_VCHIFSCNO",incentive.BankDet.IFSCNo==null ? "" : incentive.BankDet.IFSCNo,
                        "@P_VCHMICR",incentive.BankDet.MICRNo==null ? "" : incentive.BankDet.MICRNo,
                        "@P_BankDoc",incentive.BankDet.BankDoc ==null?"":incentive.BankDet.BankDoc,

                        ////Parameters for Course Details",, 13
                        "@P_Int_CD_CourseDetails",incentive.CourseDet.IntCourseDetails==null ? 0 : incentive.CourseDet.IntCourseDetails,
                        "@P_Int_CD_Institution_Name",incentive.CourseDet.InstitutionName==null ? 0 : incentive.CourseDet.InstitutionName,
                        "@P_VCH_CD_Other_Institution",incentive.CourseDet.OtherInstitutionName==null?"":incentive.CourseDet.OtherInstitutionName,
                        "@P_Int_CD_Location_Institute",incentive.CourseDet.InstitutionLocation==null ? 0 : incentive.CourseDet.InstitutionLocation,
                        "@P_VCH_CD_Institution_Address",incentive.CourseDet.InstitutionAddress==null ? "" : incentive.CourseDet.InstitutionAddress,
                        "@P_VCH_CD_Course_Duratio",incentive.CourseDet.CourseDuratio==null ? "" : incentive.CourseDet.CourseDuratio,
                        "@P_DEC_CD_Course_Amount",incentive.CourseDet.CourseAmount==null ? 0 : incentive.CourseDet.CourseAmount,
                        "@P_VCH_CD_Course_Attachment",incentive.CourseDet.CourseAttachment==null ? "" : incentive.CourseDet.CourseAttachment,
                        "@P_DTM_CD_Date_of_selection",incentive.CourseDet.Dateofselection == null ? "" :incentive.CourseDet.Dateofselection,
                        "@P_VCH_CD_Copy_of_letterofselection",incentive.CourseDet.Copyofletterofselection==null ? "" : incentive.CourseDet.Copyofletterofselection,
                        "@P_INT_CD_INDUSTRAILUNIT",incentive.CourseDet.INTINDUSTRAILUNIT==null ? 0 : incentive.CourseDet.INTINDUSTRAILUNIT,
                        "@P_VCH_PROV_SAC_LETTER",incentive.CourseDet.ProvSacLetter==null ? "" : incentive.CourseDet.ProvSacLetter,
                        "@P_DTM_CD_Date_of_Saction_Letter",incentive.CourseDet.DateofSanction == null ? "" :incentive.CourseDet.DateofSanction,
                        "@P_VCH_CD_Saction_Letter_No",incentive.CourseDet.SanctionNo==null?"":incentive.CourseDet.SanctionNo,
                        //////Parameters for Availed Details",,

                        "@NeverAvailedPrior",incentive.AvailDet.NeverAvailedPrior==null ? 0 : incentive.AvailDet.NeverAvailedPrior,
                        "@UndertakingSubsidyDoc",incentive.AvailDet.UndertakingSubsidyDoc==null ? "" : incentive.AvailDet.UndertakingSubsidyDoc,
                        "@SubsidyAvailed",incentive.AvailDet.SubsidyAvailed==null ? 0 : incentive.AvailDet.SubsidyAvailed,
                        "@SupportingDocs",incentive.AvailDet.SupportingDocs==null ? "" : incentive.AvailDet.SupportingDocs,
                        "@ClaimtExempted",incentive.AvailDet.ClaimtExempted==null ? 0 : incentive.AvailDet.ClaimtExempted,
                        "@ClaimReimbursement",incentive.AvailDet.ClaimReimbursement==null ? 0 : incentive.AvailDet.ClaimReimbursement,
                        "@AssistanceDetails",incentive.AvailDet.AssistanceDetails==null ? "" : incentive.AvailDet.AssistanceDetails.SerializeToXMLString(),
                        "@IncentiveAvailed",incentive.AvailDet.IncentiveAvailed==null ? "" : incentive.AvailDet.IncentiveAvailed.SerializeToXMLString(),

                        ////Parameters for Documents to be Submitted after Completion of Course",,
                        "@P_DTM_CD_Excepteddate_of_course",incentive.DocSubAftCompDet.Excepteddateofcourse==null ? "": incentive.DocSubAftCompDet.Excepteddateofcourse,
                        "@P_VCH_MANG_DEV_LETTER",incentive.DocSubAftCompDet.ManagementDevSuceLetter==null ? "" : incentive.DocSubAftCompDet.ManagementDevSuceLetter,

                         ////Common Attributes",,
                        "@CHRACTIONCODE",incentive.strcActioncode== null ? "" :incentive.strcActioncode,
                        "@P_VCHINCENTIVENO",incentive.IncentiveNum== null ? "" :incentive.IncentiveNum,
                        "@P_VCHPEALNO",incentive.PealNum== null ? "" :incentive.PealNum,
                        "@P_VCHPCNO",incentive.PCNum== null ? "" :incentive.PCNum,
                        "@P_VCHUNITCODE",incentive.UnitCode== null ? "" :incentive.UnitCode,
                        "@P_VCHPROPOSALNO",incentive.ProposalNum== null ? "" :incentive.ProposalNum,
                        "@P_INTCREATEDBY",incentive.Createdby== null ? 0 :incentive.Createdby,
                        "@P_INTUSERID",incentive.Userid== null ? 0 :incentive.Userid,
                        "@P_INTINCUNQUEID",incentive.UnqIncentiveId== null ? 0 :incentive.UnqIncentiveId,
                        "@P_XML_FILEUPLOAD",incentive.FileUploadDetails == null ? "" : incentive.FileUploadDetails.SerializeToXMLString(),
                        };
        object ss = "";
        SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_Insentive_Forms_EntSubsidy", out ss, sqlParam);
        return ss.ToString();

    }

    public DataSet FillAllActivities(PrioritySectorDetails objPriority)
    {
        DataSet dtActs = new DataSet();
        object[] sqlParam = {  "@P_INTSECTORID", objPriority.intSectorId,
	                           "@P_INTSUBSECTORID",objPriority.intSubSectorId,
                               "@P_SEGACT",objPriority.intLiesSector,
                               "@P_ACTID",objPriority.intActivityid
	                        };
        dtActs = SqlHelper.ExecuteDataset(conn, "USP_INCT_FILLACTIVITIES", sqlParam);

        return dtActs;

    }

    public string IsPriorityApp(int IntUserId, int IntAction)
    {
        string retval = "0";
        string StrQuery = "select dbo.UDF_INCT_CHKPRIORITYSTATUS(" + IntUserId + ", " + IntAction + ")";
        retval = Convert.ToString(SqlHelper.ExecuteScalar(conn, CommandType.Text, StrQuery));
        return retval.Trim();
    }

    public string IsProvisionalCertificate(int IntUserId, string strIncentiveNumber)
    {
        String retval = "0";
        string StrQuery = "SELECT DBO.UDF_INCT_ISPROVISIONALCERT(" + IntUserId + ",'" + strIncentiveNumber + "')";
        retval = Convert.ToString(SqlHelper.ExecuteScalar(conn, CommandType.Text, StrQuery));
        return retval.Trim();
    }

    public DataSet ProvisionalThrustsectorpopulateData(int userid)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  "@CHRACTIONCODE","V",
                               "@P_INTUNQINCID",userid==null?0:userid,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_VIEW_Thrust_Priority_Statu_ipr_2022", sqlParam);

        return ds;
    }

    public DataSet ProvisionalThrustsectorpopulateDatainDraft(int userid)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  "@CHRACTIONCODE","DV",
                               "@P_INTUNQINCID",userid==null?0:userid,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_VIEW_Thrust_Priority_Statu_ipr_2022", sqlParam);

        return ds;
    }

    public DataSet StampDutyExemptionpopulateData(int userid)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  "@CHRACTIONCODE","VW",
                               "@P_INTUNQINCID",userid==null?0:userid,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_VIEW_StampDutyExemption_IPR_2022", sqlParam);

        return ds;
    }
    public DataSet StampDutyExemptionpopulateDatainDraft(int userid)
    {
        DataSet ds = new DataSet();

        object[] sqlParam = {  "@CHRACTIONCODE","DVW",
                               "@P_INTUNQINCID",userid==null?0:userid,
                            };
        ds = SqlHelper.ExecuteDataset(conn, "USP_INCT_VIEW_StampDutyExemption_IPR_2022", sqlParam);

        return ds;
    }


}
