<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncentiveFeedback.aspx.cs"
    Inherits="incentives_IncentiveFeedback" %>

<%@ Register Src="~/includes/Feedback.ascx" TagName="Feedback" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="PMenu" TagPrefix="uc5" %>
<%@ Register Src="~/incentives/WUC_Inct_FY.ascx" TagPrefix="ucfy" TagName="FY_Load" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Employee Cost Subsidy</title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        var msgTitle = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'; //  'Incentive';

        $(document).ready(function () {

        });
    </script>
    <style type="text/css">
        .fieldinfo2
        {
            float: right;
            margin-right: 8px;
            font-size: 17px;
            margin-top: 1px;
            color: #3abffb;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
                <div class="investrs-tab">
                    <uc5:PMenu ID="PMenu" runat="server" />
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">
                            <div class="innertabs">
                                <ul class="nav nav-pills pull-right">
                                    <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                    <li class="active"><a href="appliedlistwithdetails.aspx">Apply For incentive</a></li>
                                    <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                                </ul>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-header">
                                <div class="iconsdiv">
                                </div>
                                <h2>
                                    <asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
                            </div>
                            <div class="form-body">
                                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                    <asp:HiddenField ID="hdServiceId" runat="server" />
                                    <asp:HiddenField ID="hdApplicationUniqueID" runat="server" />
                                    <uc4:Feedback ID="Feedback1" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    </form>
</body>
</html>
