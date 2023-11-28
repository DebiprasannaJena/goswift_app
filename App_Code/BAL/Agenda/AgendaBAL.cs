using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcessLayer.Agenda;

namespace BusinessLogicLayer.Agenda
{
    public class AgendaBAL : IAgendaBAL
    {
        AgendaDataLayer objDataAccess = new AgendaDataLayer();

      
        public List<EntityLayer.Agenda.AgendaDet> GetAgendaDet(EntityLayer.Agenda.AgendaDet objLand)
        {
            return objDataAccess.GetAgendaDet(objLand);
        }

        public string AddAgenda(int Status, string vchProposalNo, string Remark, string URL)
        {
            return objDataAccess.AddAgenda(Status, vchProposalNo, Remark, URL);
        }


        public string AddAgendaDetails(int Status, string vchProposalNo, string Remark, string URL)
        {
            return objDataAccess.AddAgendaDetails(Status, vchProposalNo, Remark, URL);
        }
    }
}
