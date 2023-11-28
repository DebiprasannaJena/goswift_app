using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EntityLayer.Master;
using DataAcessLayer.Master;
using System.Data;

namespace BusinessLogicLayer.Master
{
    public class MasterBusinessLayer:IMasterBusinessLayer
    {
        MasterDatalayer objDataAccess = new MasterDatalayer();
        public string SectorData(EntityLayer.Master.MasterDetails objSector)
        {
            return objDataAccess.SectorData(objSector);
        }
        public DataTable ViewSectorDetails(string Id, string action)
        {
            return objDataAccess.ViewSectorDetails(Id, action);
        }
        public DataSet BindDDl(MasterDetails objSector)
        {
            
            return objDataAccess.BindDDl(objSector);
        }

        //public string InvestorData(SectorDetails objInvestor)
        //{
        //    throw new NotImplementedException();
        //}

        //public DataTable ViewInvestorDetails(string Id, string action)
        //{
        //    throw new NotImplementedException();
        //}
    }
}