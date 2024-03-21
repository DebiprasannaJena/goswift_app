<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublicDashBoardQuery.aspx.cs"
    Inherits="PublicDashBoardQuery" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <title>SWP(Single Window Portal)</title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src='//code.jquery.com/jquery-1.8.3.js'></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/css/bootstrap-datepicker3.min.css"/>
    <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
        });

        function setDateValues(strFromDate, strToDate) {
            debugger;
            var appendId = "ContentPlaceHolder1_";
            var fromDate = $("#" + appendId + "txtFromDate").val();
            var toDate = $("#" + appendId + "txtToDate").val();
            $("#" + appendId + "txtFromDate").datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            }).datepicker("setDate", fromDate);
            $("#" + appendId + "txtToDate").datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            }).datepicker("setDate", toDate);
        }


        function ValidatePage() {
            debugger;
            var fDate = $("#txtFromDate").val();
            var tDate = $("#txtToDate").val();
            if (fDate == null || fDate == undefined || fDate == '') {
                jAlert('<strong>Please select from date.</strong>', 'GO-SWIFT');
                return false;
            }
            if (tDate == null || tDate == undefined || tDate == '') {
                jAlert('<strong>Please select to date.</strong>', 'GO-SWIFT');
                return false;
            }
            var dtmFromDate = new Date(GetDate(fDate));
            var dtmToDate = new Date(GetDate(tDate));

            if (dtmFromDate > dtmToDate) {
                jAlert('<strong>From date cannot be greater than to date.</strong>', 'GO-SWIFT');
                return false;
            }
            else {
                return true;
            }

        }

        function GetDate(str) {
            debugger;
            var arr = str.split('-');
            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
            var i = 1;
            for (i; i <= months.length; i++) {
                if (months[i] == arr[1]) {
                    break;
                }
            }
            var formatddate = i + '/' + arr[0] + '/' + arr[2];
            return formatddate;
        }
    </script>
    <style>
        .panel-default > .panel-heading
        {
            padding: 8px 15px !important;
            font-size: 18px;
            text-transform: uppercase;
        }
        
        .note
        {
            border: 1px solid #f0f0f0;
            padding: 15px;
            border-radius: 4px;
            background: #f9f9f9;
        }
        
        .note ol
        {
            margin-bottom: 0;
        }
        
        .note ol li
        {
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="content-form-section">
            <div class="col-sm-12">
                <div class="aboutcontent-sec">
                    <h2 class=" margin-bottom15">
                        Query Dashboard
                    </h2>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Search Details
                        </div>
                        <div class="panel-body">
                            <div class="form-group row NOPRINT">                              
                                 <div class="col-sm-3">
                                    <label for="State">
                                        From Date
                                    </label>
                                    <div class="input-group  date datePicker">
                                        <asp:TextBox runat="server" class="form-control" ID="txtFromDate" name="txtFromDate"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <label for="State">
                                        To Date
                                    </label>
                                    <div class="input-group  date datePicker">
                                        <asp:TextBox runat="server" class="form-control" ID="txtToDate"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="btnSearch" Style="margin-top: 26px" CssClass="btn btn-success" runat="server"
                                        Text="Search" OnClick="btnSearch_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-bordered">
                            <tr>
                                <th>
                                    Status
                                </th>
                                <th style="width: 13%;">
                                    PEAL
                                </th>
                                <th style="width: 13%;">
                                    Service
                                </th>
                                <th style="width: 13%;">
                                    Incentive
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    Time limit prescribed as per the public service guarantee act
                                </td>
                                <td align="right">
                                    <asp:Label ID="spTimelinePeal" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spTimelineService" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spTimelineIncentive" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total number of queries received
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryReceivedPeal" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryReceivedService" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryReceivedIncentive" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total number of queries responded
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryRespondedPeal" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryRespondedService" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryRespondedIncentive" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Average time taken to respond to queries
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryAvgTimePeal" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryAvgTimeService" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryAvgTimeIncentive" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Median time taken to respond to queries
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryMedianTimePeal" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryMedianTimeService" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryMedianTimeIncentive" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Minimum time taken to respond to query
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryMinTimePeal" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryMinTimeService" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryMinTimeIncentive" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Maximum time taken to respond to query
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryMaxTimePeal" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryMaxTimeService" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="spQueryMaxTimeIncentive" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>                  
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
