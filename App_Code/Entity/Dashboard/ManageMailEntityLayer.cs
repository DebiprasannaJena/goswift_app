using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ManageMailEntityLayer
/// </summary>
public class ManageMailEntityLayer
{
    public ManageMailEntityLayer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string strAction { get; set; }
    public int intDesgId { get; set; }
    public string strMailStatus { get; set; }
    public string strIds { get; set; }

    public string strCcMailId { get; set; }
    public string strCcEnableStatus { get; set; }
    public string strBccMailId { get; set; }
    public string strBccEnableStatus { get; set; }
    public int intCreatedBy { get; set; }

    public int intSerialNo { get; set; }
    public string strMailId { get; set; }

    public string strSpamMode { get; set; }
    public string strSpamText { get; set; }

    public string strSubAction { get; set; } // ADD ANIL SAHOO

    public int intUserId { get; set; }  // add anil sahoo

}