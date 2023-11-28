using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PowerTariff
/// </summary>
public class PowerTariff
{


    public int SectionId { get; set; }

    public Decimal NewInvestment_SchematicProvisions { get; set; }
    public Decimal NewInvestment_TillDateOfCommencementOfProduction { get; set; }
    public string NewInvestment_reasons { get; set; }

    public Decimal Land_SchematicProvisions { get; set; }
    public Decimal Land_TillDateOfCommencementOfProduction { get; set; }
    public string Land_reasons { get; set; }

    public Decimal Building_SchematicProvisions { get; set; }
    public Decimal Building_TillDateOfCommencementOfProduction { get; set; }
    public string Building_reasons { get; set; }

    public Decimal PlantMachinery_SchematicProvisions { get; set; }
    public Decimal PlantMachinery_TillDateOfCommencementOfProduction { get; set; }
    public string PlantMachinery_reasons { get; set; }

    public Decimal OtherFixedAssets_SchematicProvisions { get; set; }
    public Decimal OtherFixedAssets_TillDateOfCommencementOfProduction { get; set; }
    public string OtherFixedAssets_reasons { get; set; }

    public Decimal ElectricalInstallations_SchematicProvisions { get; set; }
    public Decimal ElectricalInstallations_TillDateOfCommencementOfProduction { get; set; }
    public string ElectricalInstallations_reasons { get; set; }

    public string JustificationForExcessInvestment { get; set; }

    public string TotalUnitConsumed { get; set; }
    public Decimal AmountPaid { get; set; }
    public int Refid { get; set; }
    public string MoneyReceipt { get; set; }
}
