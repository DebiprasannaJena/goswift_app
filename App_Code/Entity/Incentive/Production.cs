using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Created
/// </summary>
public class Production
{
    public int? EPF { get; set; }
    public int? ESI { get; set; }
    public int? PRODUCTIONID { get; set; }
    public int? CONTRACTUALEMP { get; set; }
    public int? CREATEDBY { get; set; }
    public int? CURRENTMANAGERIAL { get; set; }
    public int? CURRENTSEMISKILLED { get; set; }
    public int? CURRENTSKILLED { get; set; }
    public int? CURRENTSUPERVISORY { get; set; }
    public int? CURRENTTOTAL { get; set; }
    public int? CURRENTUNSKILLED { get; set; }
    public int? DIRECTEMP { get; set; }
    public int? INCUNQUEID { get; set; }
    public int? PROPOSEDMANAGERIAL { get; set; }
    public int? PROPOSEDSEMISKILLED { get; set; }
    public int? PROPOSEDSKILLED { get; set; }
    public int? PROPOSEDSUPERVISORY { get; set; }
    public int? PROPOSEDTOTAL { get; set; }
    public int? PROPOSEDUNSKILLED { get; set; }
    public string ESIOREPF { get; set; }
    public string PRODUCTION { get; set; }
    public string PRODUCTIONPROPOSED { get; set; }
    public string COMMENCEDOC { get; set; }
    public string DPRDOC { get; set; }
    public string EMPDOC { get; set; }
    public string ESIOREPFDOC { get; set; }
    public string ESIOREPFREGNO { get; set; }
    public string LOCATION { get; set; }
    public string STATUS { get; set; }
    public int? STATUSFORDFTAPPLY { get; set; }
    public List<ProductionItem> PRODUCTIONITEMS { get; set; }//XML
    public string ESIEPFCOMPDOC { get; set; }
    public string PAYROLLDOC { get; set; }
    public string DOCUMENTINSUPPORT { get; set; }

    public string DELAYREASON { get; set; }

    public string REGATTACHDOC { get; set; }

    //////////----- ADDED ON 13TH Nov 2017------ by GS Chhotray-----------
    public string ESIAUTHNAME { get; set; }
    public string EPFREGNO { get; set; }
    public string EPFREGDATE { get; set; }
    public string EPFREGATTACHDOC { get; set; }
    public string EPFAUTHNAME { get; set; }
}
public class ProductionItem
{ 
    public string   PRODUCTNAME{get; set;}
    public decimal?      QUANTITY   {get; set;}
    public int?      UNITTYPE   {get; set;}
    public decimal?  VALUE      {get; set;}
}

