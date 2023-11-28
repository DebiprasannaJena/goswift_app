using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Created By - Bikash Sahoo
/// Created On - 9th-SEP-2017
/// Description - This Entity Class is used for section Major Operational Activity of the Company.
/// Tables Inserted - T_INCT_MAJOR_OPERATION_COMPANY, T_INCT_MAJOR_OPERATION_ITEMS_DTL
/// Procedure used - USP_INCT_MajorOperationOfCompany
/// </summary>
/// 
namespace EntityLayer.Incentive
{
    public class MajorOperationOfCompany
    {
        public MajorOperationOfCompany()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int intMajorOperationId { get; set; }
        public int INTINCUNQUEID { get; set; }
        public int intDirectEmployment { get; set; }
        public int intContractualEmployment { get; set; }
        public string vchEmployeeSupportDocument { get; set; }
        public int intManagerial_curr { get; set; }
        public int intManagerial_prop { get; set; }
        public int intSupervisory_curr { get; set; }
        public int intSupervisory_prop { get; set; }
        public int intSkilled_curr { get; set; }
        public int intSkilled_prop { get; set; }
        public int intSemiSkilled_curr { get; set; }
        public int intSemiSkilled_prop { get; set; }
        public int intUnSkilled_curr { get; set; }
        public int intUnSkilled_prop { get; set; }
        public int intTotal_curr { get; set; }
        public int intTotal_prop { get; set; }

        public int BITDELETEDFLAG { get; set; }
        public string chrActionCode { get; set; }
        public int intSectionNo { get; set; }
        public string XmlData { get; set; }

        public List<MajorOperationItmDtl> lstMajorOperationItmDtl { get; set; }

    }

    // Properties for detail table
    [Serializable]
    public class MajorOperationItmDtl
    {
        public int INTITEMID { get; set; }
        public string VCHPRODUCTNAME { get; set; }
        public int INTQUANTITY { get; set; }
        public int INTUNITTYPE { get; set; }
        public int INTVALUE { get; set; }
        public int BITDELETEDFLAG { get; set; }
        public int slno { get; set; }
        public string vchUnitName { get; set; }
    }
}