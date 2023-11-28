using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using EntityLayer.Mastersector;
using DataAcessLayer.MasterSector;

namespace BusinessLogicLayer.MasterSector
{
   public  class MasterSectorBusinesslayer:IMasterSectorBusinessLayer
    {
       MasterSectorDataLayer objdata = new MasterSectorDataLayer();
       public string SectorData(MasterSectorDetails objSector)
        {
            return objdata.MasterSectorData(objSector);
        }
       public List<MasterDdl> BindDDl(MasterDdl objBindDdl)
       {
           return objdata.BindDDl(objBindDdl);
       }
       public List<MasterGrid> BindDropdown(MasterGrid objgrid)
       {
           return objdata.BindDropdown (objgrid);
       }
       public List<Gridviewgrd> BindDropdowngrd(Gridviewgrd objgrid)
       {
           return objdata.BindDropdowngrd(objgrid);
       }
       public MasterSectorDetails EditData(int sectorId)
       {
           return objdata.EditData(sectorId);
       }
       //public string test(string a)
       //{
       //    return "Hi  " + a;
       //}
        //public DataTable ViewSectorDetails(string Id, string action)
        //{
        //    return objdata.ViewMasterSectorDetails(Id, action);
        //}
       //public DataSet BindDDl(EntityLayer.Master.MasterDetails objSector);
       //{

       //    return objdata.BindDDl(objSector);
       //}

    }

}
