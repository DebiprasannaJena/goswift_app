using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Agenda
{
    public class AgendaDet
    {
        public string strAction { get; set; }
        public int  strStatus { get; set; }
        public string vchProposalNo { get; set; }
        public string ProjectTitle { get; set; }
        public string CompanyName { get; set; }
        public int NodId { get; set; }
        public int sector { get; set; }
        public string DateofApplication { get; set; }
        public string Type { get; set; }
        public string EnvironmentCategory { get; set; }
        public string BriefProposal { get; set; }
        public string Land { get; set; }
        public string Water { get; set; }
        public string Power { get; set; }
        public int Source { get; set; }
        public int DirectEmployment { get; set; }
        public int ContractualEmployment { get; set; }
        public string ImplementationPeriod { get; set; }
        public string FinancingDescription { get; set; }
        public string Remark { get; set; }
        public string URL { get; set; }
        //Location details
        public int District { get; set; }
        public string Location { get; set; }

        public string Product { get; set; }
        public string Capacity { get; set; }

        public string DirectorsName { get; set; }

        public string BusinessIntrest { get; set; }

        public int Description { get; set; }
        //public string Description { get; set; }
        public string Cost { get; set; }


        public string Amount { get; set; }
        public string Percentage { get; set; }


        public string Particulars { get; set; }
        public string TurnOver { get; set; }
        public string ProfitAfterTax { get; set; }
        public string NetWorth { get; set; }
        public int FinancialInfo { get; set; }
        public string document_name { get; set; }
        public string document_status { get; set; }
        public string document_Link { get; set; }
       
        public string strNodalOffcrNM { get; set; }
        public string ReserveSurplus { get; set; }

    }

}
