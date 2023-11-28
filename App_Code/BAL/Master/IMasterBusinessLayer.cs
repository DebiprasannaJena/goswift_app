using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EntityLayer.Master;
using System.Data;

namespace BusinessLogicLayer.Master
{
    [ServiceContract]
    public interface IMasterBusinessLayer
    {
        [OperationContract]
        string SectorData(EntityLayer.Master.MasterDetails objSector);

        //[OperationContract]
        //List<InvestorDetails> ViewInvestorDetails(InvestorDetails objInvestor);
        [OperationContract]
        DataTable ViewSectorDetails(string Id, string action);
        [OperationContract]
        DataSet BindDDl(EntityLayer.Master.MasterDetails objSector);
    }
}