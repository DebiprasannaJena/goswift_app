using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class incentives_IncentiveFeedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //////-----500 for incentive
            /////------501 for PC
            ////-------502 for Certfication(Priority/pioneer/provisional Priority)
            hdServiceId.Value = Request.QueryString["ServiceId"];
            hdApplicationUniqueID.Value = Request.QueryString["InctUniqueNo"];
        }
    }
}