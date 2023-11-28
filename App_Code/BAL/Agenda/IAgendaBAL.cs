using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EntityLayer.Agenda;
namespace BusinessLogicLayer.Agenda
{
    [ServiceContract]
    public interface IAgendaBAL
    {
         [OperationContract]
        string AddAgenda(int Status, string vchProposalNo, string Remark, string URL);
         [OperationContract]
         List<AgendaDet> GetAgendaDet(AgendaDet objLand);
         [OperationContract]
         string AddAgendaDetails(int Status, string vchProposalNo, string Remark, string URL);
    }
}
