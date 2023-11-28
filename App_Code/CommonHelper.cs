using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

/// <summary>
/// Summary description for ExtensionHelper
/// </summary>
public static class CommonHelper
{
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute),
            false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }
    public static string SerializeToXMLString<T>(this T toSerialize)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
        StringWriter textWriter = new StringWriter();

        xmlSerializer.Serialize(textWriter, toSerialize);
        return textWriter.ToString();
    }


    public static void MergeRows(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Text == previousRow.Cells[i].Text)
                {
                    row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                           previousRow.Cells[i].RowSpan + 1;
                    previousRow.Cells[i].Visible = false;
                }
            }
        }
    }
    public static string SessionUserKey { get { return "UserId"; } }
    public static string SessionDeptIdKey { get { return "DeptId"; } }
    public static string SessionDeptNameKey { get { return "Department"; } }
    public static string SessionUserNameKey { get { return "userName"; } }
    public static string SessionLevelIDKey { get { return "LevelID"; } }
    public static string SessionDesigIDKey { get { return "DesigID"; } }
    public static string SessionLocIDKey { get { return "DeptId"; } }
    public static string SessionLanguage { get { return "E"; } }
    public static int StateId { get { return 27; } }

    /// <summary>
    /// Added by Abhijit Ojha to check a control raising Postback
    /// </summary>
    /// <param name="page"></param>
    /// <param name="ControlList"></param>
    /// <returns></returns>
    public static Boolean GetPostBackControlId(System.Web.UI.Page page, string ControlList)
    {
        if (!page.IsPostBack)
            return true;
        System.Web.UI.Control control = null;
        string controlName = page.Request.Params["__EVENTTARGET"];
        if (!String.IsNullOrEmpty(controlName))
        {
            control = page.FindControl(controlName);
        }
        else
        {
            string controlId;
            System.Web.UI.Control foundControl;
            foreach (string ctl in page.Request.Form)
            {
                if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                {
                    controlId = ctl.Substring(0, ctl.Length - 2);
                    foundControl = page.FindControl(controlId);
                }
                else
                {
                    foundControl = page.FindControl(ctl);
                }
                if (!(foundControl is Button || foundControl is ImageButton)) continue;
                control = foundControl;
                break;
            }
        }
        return Array.Exists(ControlList.Split(','), element => element.Equals((control == null ? String.Empty : control.ID), StringComparison.OrdinalIgnoreCase));
    }
    /// <summary>
    /// Added by Abhijit Ojha to get All Querystring Name,Value Pairs
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public static string GetQueryStrings(System.Web.UI.Page page)
    {
        string[] queryStr = null;
        string AllQueryString = string.Empty;
        try
        {
            if (page.Request.QueryString.HasKeys())
            {
                queryStr = page.Request.QueryString.AllKeys;
                for (int i = 0; i < queryStr.Length; i++)
                {
                    if (i == 0)
                    {
                        AllQueryString = "?" + queryStr[i] + "=" + page.Request.QueryString[queryStr[i]];
                    }
                    else
                    {
                        AllQueryString = AllQueryString + "&" + queryStr[i] + "=" + page.Request.QueryString[queryStr[i]];
                    }
                }
            }
        }
        catch { AllQueryString = ""; }
        finally { queryStr = null; }
        return AllQueryString;
    }

    public enum WSItemcategory
    {
        Controlled = 1,
        Subsidized = 2,
        NonControlled = 3,
        Others = 4
    }
    public enum WSStockSaleTransfer
    {
        Godown = 1,
        FPS = 2,
        SubWholesale = 3,
        Institute = 4,
        PrivateFPS = 5,   
        SendSample = 6
    }
    public enum WSStockReceipt
    {
        StockTransfer = 1,
        Subsidized = 2,
        NonControlled = 3,
        Consignment = 4,
        FCI = 5,
        Miller = 6,
        Supplier = 7,
        RetailTransfer = 8
    }
    public enum AccountActivity
    {
        HeadOffice = 34,
        RegionalOffice = 48,
        Warehouse=37
    }
    public enum CommonUnit
    {
        KG = 2,
    }
    public static string UnitConversion(string Qty, string Unit, string Price, string UnitOld)
    {
        string TotalUnit=string.Empty;
        string TotalPrice = string.Empty;
        TotalUnit = Qty;
        TotalPrice = Price;
       
        #region Unit      
        if (Unit != "")
        {
            if (Unit == "1")
            {
                TotalUnit = Convert.ToString(Convert.ToDouble(Qty) * 100);
            }          
            if ((Unit == "2") || (Unit == "3") || (Unit == "5") || (Unit == "4") || (Unit == "6") || (Unit == "8") || (Unit == "9"))
            {
                TotalUnit = Qty;
            }
            if (Unit == "7")
            {
                TotalUnit = Convert.ToString(Convert.ToDouble(Qty) * 0.01);
            }
           
        }
        #endregion

        #region Price
        if (Price != "")
        {
            if (UnitOld == "1")//QTL
            {
                if (Unit == "1")//QTL
                {
                    TotalPrice = Price;
                }
                if (Unit == "2")//KG
                {
                    TotalPrice = Convert.ToString(Convert.ToDouble(Price) / 100);
                }
                if (Unit == "3")//NUMBER
                {
                    TotalPrice = Price;
                }
                if (Unit == "4")//PACK
                {
                    TotalPrice = Price;
                }
                if (Unit == "5")//LTR
                {
                    TotalPrice = Price;
                }
                if (Unit == "6")//MTR
                {
                    TotalPrice = Price;
                }
                if (Unit == "7")//GM
                {
                    TotalPrice = Convert.ToString(Convert.ToDouble(Price) / 100000);
                }
                if (Unit == "8")//Mtr
                {
                    TotalPrice = Price;
                }
                if (Unit == "9")//GUNNY BAG
                {
                    TotalPrice = Price;
                }
               
            }
            if (UnitOld == "2")//KG
            {
                if (Unit == "1")//QTL
                {
                    TotalPrice = Convert.ToString(Convert.ToDouble(Price) * 100);
                }
                if (Unit == "2")//KG
                {
                    TotalPrice = Price;
                }
                if (Unit == "3")//NUMBER
                {
                    TotalPrice = Price;
                }
                if (Unit == "4")//PACK
                {
                    TotalPrice = Price;
                }
                if (Unit == "5")//LTR
                {
                    TotalPrice = Price;
                }
                if (Unit == "6")//MTR
                {
                    TotalPrice = Price;
                }
                if (Unit == "7")//GM
                {
                    TotalPrice = Convert.ToString(Convert.ToDouble(Price) / 1000);
                }
                if (Unit == "8")//Mtr
                {
                    TotalPrice = Price;
                }
                if (Unit == "9")//GUNNY BAG
                {
                    TotalPrice = Price;
                }
                
            }
            if (UnitOld == "7")//GM
            {
                if (Unit == "1")//QTL
                {
                    TotalPrice = Convert.ToString(Convert.ToDouble(Price) * 100000);
                }
                if (Unit == "2")//KG
                {
                    TotalPrice = Convert.ToString(Convert.ToDouble(Price) / 1000);
                }
                if (Unit == "3")//NUMBER
                {
                    TotalPrice = Price;
                }
                if (Unit == "4")//PACK
                {
                    TotalPrice = Price;
                }
                if (Unit == "5")//LTR
                {
                    TotalPrice = Price;
                }
                if (Unit == "6")//MTR
                {
                    TotalPrice = Price;
                }
                if (Unit == "7")//GM
                {
                    TotalPrice = Price;
                }
                if (Unit == "8")//Mtr
                {
                    TotalPrice = Price;
                }
                if (Unit == "9")//GUNNY BAG
                {
                    TotalPrice = Price;
                }
                else
                {
                    TotalPrice = Price;
                }
            }
            if ((UnitOld == "3") || (UnitOld == "4") || (UnitOld == "5") || (UnitOld == "6") || (UnitOld == "9") || (UnitOld == "8"))//NUMBER
            {
                TotalPrice = Price;
            }
           
        }
        #endregion

        return TotalUnit + "~" + TotalPrice;
    }
    #region "For File Upload"
    public static string UploadPath()
    {
        string strResult = string.Empty;
        try
        {        
                strResult = System.Configuration.ConfigurationManager.AppSettings["PhotoPathAdd"].ToString();         
                
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return strResult;
    }
    public static string ViewPath()
    {
        string strResult = string.Empty;
        try
        {            
                strResult = System.Configuration.ConfigurationManager.AppSettings["photopathView"].ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return strResult;
    }
    #endregion
}
