using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer.Incentive
{
    /// <summary>
    /// Summary description for InvestmentDetails
    /// </summary>
    public class InvestmentDetails
    {
        public int?         INT_Section_No               { get; set; }
        public int?         INCUNQUEID                   { get; set; }
        public int?         INDUSTRAILUNIT               { get; set; }
        public int?         INT_IND_Investment_Details   { get; set; }
        public DateTime?    DTM_IND_Date_of_First_Fixed  { get; set; }
        public string      Document_in_support          { get; set; }
        public string      LAND_TYPE                    { get; set; }
        public decimal?     LAND_TYPE_AMOUNT             { get; set; }
        public string      Building                     { get; set; }
        public decimal?     Building_AMOUNT              { get; set; }
        public string      Plant_Machinery              { get; set; }
        public decimal?     Plant_Machinery_AMOUNTR      { get; set; }
        public string      Balancing_Equipment          { get; set; }
        public decimal?     Balancing_Equipment_AMOUNT   { get; set; }
        public string      Other_Fixed_Assests          { get; set; }
        public decimal?     Other_Fixed_Assests_AMOUNT   { get; set; }
        public string      Electric_install             { get; set; }
        public decimal?     Electric_install_AMOUNT      { get; set; }
        public string      Loading                      { get; set; }
        public decimal?     LoadingAmount                { get; set; }
        public string      IDCOShed                     { get; set; }
        public decimal?     IDCOShedAmount               { get; set; }       
        public string      PROJECTDOC                   { get; set; }
        public string      MACHINERYDOC                 { get; set; }
        public string       LAB	                        { get; set; }
        public decimal?      LAB_AMOUNT                  { get; set; }
        public decimal?      LAND_TYPE_EMD	            { get; set; }
        public decimal?      Building_EMD	            { get; set; }
        public decimal?      Plant_Machinery_EMD	        { get; set; }
        public decimal?      Balancing_Equipment_EMD	     { get; set; }
        public decimal?      Other_Fixed_Assests_EMD	     { get; set; }
        public decimal?      ELECTRIC_INSTALL_EMD	     { get; set; }
        public decimal?      LOADING_EMD	                 { get; set; }
        public decimal?      IDCOSHED_EMD	             { get; set; }
        public decimal?      LAB_EMD	                     { get; set; }
        public DateTime?     IND_Date_of_First_Fixed_EMD { get; set; }
        public decimal?      Total                        { get; set; }
        //New 
        public string vchNewBillDoc { get; set; }
        public string vchSecHandDoc { get; set; }
        public string vchSecHandBill { get; set; }


    }
}