using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EntityLayer.Mastersector;
using DataAcessLayer.MasterSector;
using System.Data;


namespace BusinessLogicLayer.MasterSector
{
    [ServiceContract]
    public interface IMasterSectorBusinessLayer
    {
        [OperationContract]
        string SectorData(MasterSectorDetails objSector);
        //[OperationContract]
        //string test(string a);

        //[OperationContract]
        //List<InvestorDetails> ViewInvestorDetails(InvestorDetails objInvestor);
        //[OperationContract]
        //DataTable ViewMasterSectorDetails(string Id, string action);
        //[OperationContract]
        //DataSet BindDDl(EntityLayer.Master.MasterDetails objSector);
        [OperationContract]
        List<MasterDdl> BindDDl(MasterDdl objBindDdl);
        [OperationContract]
        List<MasterGrid> BindDropdown(MasterGrid objgrid);
        [OperationContract]
        List<Gridviewgrd> BindDropdowngrd(Gridviewgrd objgrid);
        [OperationContract]
        MasterSectorDetails EditData(int sectorId);
    }
}
