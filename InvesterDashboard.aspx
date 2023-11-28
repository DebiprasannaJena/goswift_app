<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvesterDashboard.aspx.cs"
    Inherits="InvesterProfile" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/jquery.mCustomScrollbar.min.css" rel="stylesheet" type="text/css" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <%--<style></style>--%>
    <style>
        .portletcontainer.cmdashbordportlet
        {
            min-height: 242px;
        }
        .search .listcontrol
        {
            height: 53px;
            padding: 4px 28px;
            overflow: hidden;
            overflow-y: scroll;
            margin-top: 0px;
        }
        .search.checkbox label
        {
            padding-left: 3px;
        }
    </style>
    <script type="text/javascript">

        function pageLoad() {

            $('.menudashboard').addClass('active');
        }

        $(document).ready(function () {
            //added by nibedita behera on 01-11-2017 for multiple district
            //            function CSRDist() {
            //                var val = [];
            //                $('#ContentPlaceHolder1_chkCSRDistrct').find('input[type=checkbox]:checked').each(function () {
            //                    val.push($(this).val());
            //                })
            //                $('#ContentPlaceHolder1_hdnCSRDistrct').val(val.join(','));

            //            }
            $('.menudashboard').addClass('active');

            $('.counter').counterUp({ delay: 10, time: 2000 });
            $('.spmgfilter').click(function () {
                $(this).find(".fa").toggleClass('fa-search fa-times-circle');
                $('#spmgfilter').slideToggle();
            });
            $('.APAAsts').click(function () {
                $(this).find(".fa").toggleClass('fa-search fa-times-circle');
                $('#APAAsts').slideToggle();
            });

            $('.Csrfilter').click(function () {
                $(this).find(".fa").toggleClass('fa-search fa-times-circle');
                $('#csrsearch').slideToggle();
            });
            $('.CICGsts').click(function () {
                $(this).find(".fa").toggleClass('fa-search fa-times-circle');
                $('#CICGsearch').slideToggle();
            });
        });
        //        function EditFunc() {
        //            location.replace("InvestorRegistration.aspx");
        //            return false;
        //        }

        //Added by suroj web method to fetch particular status of INCENTIVE
        function openpoupwin(ctrl) {

            var ctrlname = ctrl;
            var val1 = document.getElementById(ctrlname).value;
            var session_value = '<%=Session["InvestorId"]%>';
            if (val1.trim() != '') {
                $.ajax({
                    type: "POST",
                    url: "InvesterDashboard.aspx/ServiceDetail",
                    data: '{"id":"' + val1 + '","Tid":"' + session_value + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        tempHTML = "";
                        $("#tblincentive").html('')
                        tempHTML += '<thead><tr class="persist-header">'
                        tempHTML += '<th rowspan="1" valign="middle" width="20px" bgcolor="#e4e4e4">Sl#</th>'
                        tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Incentive Type</th>'
                        tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Sector</th>'
                        tempHTML += '<th rowspan="1" valign="middle" bgcolor="#e4e4e4">Status</th>'
                        tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Number of days since the incentive application was received</th>'
                        tempHTML += '</tr></thead><tbody>'
                        $('#tblincentive').append(tempHTML);
                        tempHTML = "";
                        var serialNo = 0;
                        $.each(r.d, function (index, value) {
                            serialNo++;
                            if (r.d.length > 0) {
                                tempHTML += '<tr>';
                                tempHTML += '<td align="left">' + serialNo + '</td>';
                                tempHTML += '<td >' + value.StrincType + '</td>';
                                tempHTML += '<td >' + value.StrincSector + '</td>';
                                tempHTML += '<td >' + value.strincStatus + '</td>';
                                tempHTML += '<td >' + value.intincNoDays + '</td>';
                                tempHTML += '</tr>';
                            }
                        });
                        if (r.d.length == 0) {
                            $("#tblincentive").html('')
                            tempHTML += '<tr><td>No Records Found...</td></tr>';
                            $("#tblincentive").append(tempHTML);
                        }
                        else {
                            $("#tblincentive").append(tempHTML); //APPEND THE DYNAMIC VALUE IN ROW
                            $("#tblincentive").append("</tbody>");
                        }

                    },
                    error: function (response) {
                        var msg = jQuery.parseJSON(response.responseText);
                        alert("Message: " + msg.Message + "<br /> StackTrace:" + msg.StackTrace + "<br /> ExceptionType:" + msg.ExceptionType);
                    }
                });
            }
        }

        function Show(Status) {
            alert('x');
            document.getElementById('ContentPlaceHolder1_myIframe').src = "InvestorPealDtls.aspx?Status=" + Status;
        }
       
    </script>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>
    <style type="text/css">
        .investordashboard-sec ul li h3 span
        {
            float: right;
            text-align: right;
            font-size: 22px;
            line-height: 20px;
            color: #fff;
            padding: 4px;
            margin: 0px;
        }
        .masterportlet .fontsec span
        {
            font-size: 20px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc2:header ID="header" runat="server" />
            <div class="container wrapper">
                <div class="registration-div investors-bg">
                    <div id="exTab1" class="">
                        <div class="investrs-tab">
                            <div class="row">
                                <div class="col-sm-12">
                                    <uc4:investoemenu ID="ineste" runat="server" />
                                </div>
                                <!--div class="col-sm-4">
                            <span class="pull-right" style="margin-top: 10px;"><b>Last Login:</b>
                                <asp:Label ID="lblLastlogin" runat="server" CssClass="lablespan text-primary"></asp:Label>
                            </span>
                        </div-->
                            </div>
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="masterportletsec">
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <h2>
                                                Master Tracker
                                                <%--<small>(For 2017-18) </small>--%>
                                            </h2>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="DrpDwn_Investor_Unit" runat="server" CssClass="form-control dropx"
                                                AutoPostBack="true" OnSelectedIndexChanged="DrpDwn_Investor_Unit_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4 ">
                                            <div class=" groupmastreportlet2">
                                                <h4>
                                                    PEAL Form</h4>
                                                <div class="portletdivider">
                                                    <div class="fontsec">
                                                        Pending <span class="counter" id="SPPENDING" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="portletdivider borderright0">
                                                    <div class="fontsec">
                                                        Rejected<span class="counter" id="SPREJECT" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 ">
                                            <div class=" groupmastreportlet2">
                                                <h4>
                                                    Service Approval</h4>
                                                <div class="portletdivider">
                                                    <div class="fontsec">
                                                        Pending <span class="counter" id="spPendingMaster" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="portletdivider borderright0">
                                                    <div class="fontsec">
                                                        Rejected<span class="counter" id="SPRejectedMaster" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="masterportlet">
                                                <div class="fontsec">
                                                    <h4>
                                                        GO IPAS<small>Change requests Objected</small></h4>
                                                </div>
                                                <div class="countsec" runat="server" id="divApp">
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4" style="display: none">
                                            <div class="masterportlet">
                                                <div class="fontsec">
                                                    <h4>
                                                        PEAL Form <small>Status :
                                                            <label id="spStatus" runat="server">
                                                            </label>
                                                        </small>
                                                    </h4>
                                                </div>
                                                <div class="countsec">
                                                    <small style="font-size: 14px;">Since</small> <span class="counter" id="spIndays"
                                                        runat="server">
                                                        <asp:Label ID="lblIndays" runat="server"></asp:Label>
                                                    </span>&nbsp;<small style="font-size: 14px;">Days</small></div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4" style="display: none;">
                                            <div class="masterportlet">
                                                <div class="fontsec">
                                                    <h4>
                                                        SPMG<small>Issues pending</small></h4>
                                                </div>
                                                <div class="countsec" runat="server" id="divApp1">
                                                    <span class="counter" id="spSpmgpnd" runat="server"></span>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4" style="display: none">
                                            <div class="masterportlet">
                                                <div class="fontsec">
                                                    <h4>
                                                        CSR
                                                    </h4>
                                                </div>
                                                <%--div class="countsec">
                                            <div class="portletcontainer">
                                                <div class="text-left">
                                                    <h2>
                                                        No of Projects : <a href="#csrModal" data-toggle="modal" data-target="#csrModal"
                                                            title="Total No of projects"><span class="counter" id="spNoPrj" runat="server"></span>
                                                        </a>
                                                    </h2>
                                                    <h2>
                                                        CSR Spending : <i class="fa fa-inr" aria-hidden="true"></i><span class="counter"
                                                            id="spSpent" runat="server"></span><small>Cr.</small>
                                                    </h2>
                                                </div>
                                            </div>
                                        </div>--%>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="masterportlet">
                                                <div class="fontsec" style="width: 55%;">
                                                    <h4>
                                                        CSR Spending <small>Amount invested</small></h4>
                                                </div>
                                                <div class="countsec" style="width: 45%;">
                                                    <i class="fa fa-inr" aria-hidden="true"></i>&nbsp;<span class="counter" id="spSpent"
                                                        runat="server"> </span><small>Cr.</small>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="groupmastreportlet2 ">
                                                <h4>
                                                    Incentive Status</h4>
                                                <div class="portletdivider">
                                                    <div class="fontsec">
                                                        Pending<span class="counter" id="SPPendinginc" runat="server"></span></div>
                                                </div>
                                                <div class="portletdivider borderright0">
                                                    <div class="fontsec">
                                                        Rejected <span class="counter" id="SPRejectinc" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 ">
                                            <div class="groupmastreportlet2">
                                                <h4>
                                                    Grievance Status</h4>
                                                <div class="portletdivider">
                                                    <div class="fontsec">
                                                        Pending <span class="counter" id="Spangpending" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="portletdivider borderright0">
                                                    <div class="fontsec">
                                                        Resolved<span class="counter" id="Spangresolved" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="investordashboard-sec apaaportlet">
                                            <h4>
                                                PEAL Status
                                            </h4>
                                            <div class="portletcontainer">
                                                <div class="portletgridcontainer">
                                                    <div id="divGridViewScroll">
                                                        <asp:GridView ID="grdPEALStatus" runat="server" CssClass="table table-bordered" PageSize="10"
                                                            AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" CellPadding="4"
                                                            GridLines="None" OnRowDataBound="grdPEALStatus_RowDataBound" OnRowCommand="grdPEALStatus_RowCommand">
                                                            <AlternatingRowStyle />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl#">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Proposal No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPending" runat="server" Text='<%# Eval("strPending") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Applied Since No. of days">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApply" runat="server" Text='<%# Eval("strApplied") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="HiddenField3" Value='<%# Eval("strPealQuerystatus") %>' runat="server" />
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("StrStatus") %>'></asp:Label>
                                                                        <asp:LinkButton ID="lbtnQuery" Text='<%# Eval("StrStatus") %>' runat="server" class="btn btn-success btn-sm"
                                                                            data-toggle="modal" data-target='<%# "#"+Eval("strPending")%>'></asp:LinkButton>
                                                                        <div id='<%# Eval("strPending")%>' class="modal fade" role="dialog">
                                                                            <div class="modal-dialog modal-lg">
                                                                                <!-- Modal content-->
                                                                                <div class="modal-content modal-primary bg-red ">
                                                                                    <div class="modal-header bg-red">
                                                                                        <button type="button" class="close" data-dismiss="modal">
                                                                                            &times;</button>
                                                                                        <h5 class="modal-title text-white">
                                                                                            PEAL Details</h5>
                                                                                    </div>
                                                                                    <div class="modal-body">
                                                                                        <iframe name="myIframe" id="myservcIframe" width="100%" style="height: 298px" runat="server">
                                                                                        </iframe>
                                                                                    </div>
                                                                                    <div class="modal-footer">
                                                                                        <button type="button" class="btn btn-default" data-dismiss="modal">
                                                                                            Close</button>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle CssClass="pagination-grid no-print" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="investordashboard-sec ">
                                            <h4>
                                                Service Approvals
                                                <%-- <a class="pull-right APAAsts">
                                        <i class="fa fa-search"></i>
                                        </a>--%>
                                            </h4>
                                            <div class="portletcontainer">
                                                <ul>
                                                    <li><span>Applied </span>
                                                        <h3 class="bgapplied" id="hdApplied" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </li>
                                                    <li><span>Approved</span>
                                                        <h3 class="bgsanctioned" id="hdApprove" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </li>
                                                    <li><a href="#serviceModal" data-toggle="modal" data-target="#serviceModal" title="Pending approvals"
                                                        onclick="Show(1);"><span>Pending</span><h3 class="bgpending" id="hdPending" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                    <li><a href="#serviceModal" data-toggle="modal" data-target="#serviceModal" title="Pending approvals"
                                                        onclick="Show(2);"><span>Rejected</span><h3 class="bgrejected" id="hdReject" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>
                                                Incentive Status</h4>
                                            <div class="portletcontainer">
                                                <ul>
                                                    <li><span>Sanctioned</span>
                                                        <h3 class="bgsanctioned" id="ltlincSanctioned" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </li>
                                                    <li><a href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal" onclick="ShowIncentive('E');"
                                                        title="Pending approvals"><span>Pending</span>
                                                        <h3 class="bgpending" id="ltlincPending" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                    <li><a href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal" title="Pending approvals"
                                                        onclick="ShowIncentive('F');"><span>Rejected</span>
                                                        <h3 class="bgrejected" id="ltlincRejected" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                    <li><span>Disbursed</span>
                                                        <h3 class="bgapplied" id="ltlincApplied" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </li>
                                                    <%--<li><span>Disbursed </span>
                                                <h3 class="bgdisbursed" id="ltlincDistrubed" runat="server">
                                                </h3>
                                                <div class="clearfix">
                                                </div>
                                            </li>--%>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <%--SPMG--%>
                                    <div class="col-sm-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>
                                                SPMG <a class="pull-right spmgfilter"><i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer">
                                                <div class="form-group search" id="spmgfilter">
                                                    <div class="row">
                                                        <label class="col-sm-3">
                                                            Year
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlspmgyear" runat="server">
                                                                <asp:ListItem Value="2016">2016-17</asp:ListItem>
                                                                <asp:ListItem Value="2017">2017-18</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-3 padding-left-0">
                                                            <asp:Button ID="btnspmg" CssClass="btn btn-success" runat="server" Text="Submit"
                                                                OnClick="btnspmg_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <ul>
                                                    <li><a href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" title="Received"
                                                        onclick="ShowSPMG('received');"><span>Issues Received </span>
                                                        <h3 class="bgapplied" id="spmgraised" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                    <li><a href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" title="Resolved"
                                                        onclick="ShowSPMG('resolved');"><span>Issues Resolved</span>
                                                        <h3 class="bgsanctioned" id="spmgresolved" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                    <li><a href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" title="Pending"
                                                        onclick="ShowSPMG('pending');"><span>Issues Pending </span>
                                                        <h3 class="bgapplied" id="spmgpending" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                    <li><a href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" title="Pending approvals"
                                                        onclick="ShowSPMG('delayed');"><span>Issues Pending (more than 30 days)</span><h3
                                                            class="bgpending" id="spmgpendingexceed" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <%--apaa pORTLET--%>
                                    <div class="col-sm-4">
                                        <div class="investordashboard-sec apaaportlet ">
                                            <h4>
                                                GO IPAS Status <a class="pull-right APAAsts"><i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer">
                                                <div class="search" id="APAAsts">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Year</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlAppaYear" runat="server">
                                                                    <asp:ListItem>---Select---</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Month</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlAppaMonth" runat="server">
                                                                    <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-8 col-sm-offset-4">
                                                                <asp:Button ID="btnAPAASubmit" CssClass="btn btn-success" runat="server" Text="Submit"
                                                                    OnClick="btnAPAASubmit_Click"></asp:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <ul>
                                                    <li><span>Change requests applied </span>
                                                        <h3 class="bgapplied" id="hdchngrqstApplied" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </li>
                                                    <li><span>Change requests disposed</span>
                                                        <h3 class="bgsanctioned" id="hdchngreqdispose" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </li>
                                                    <li><span>Change requests pending</span><h3 class="bgpending" id="hdhngreqPendAtIDCO"
                                                        runat="server">
                                                    </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </li>
                                                    <li><a href="#ApaaModal" data-toggle="modal" data-target="#ApaaModal" class="projectdtls"
                                                        onclick="ShowAPAA('E');" title="Change requests"><span>Change requests <small>crossed
                                                            >30 days</small></span>
                                                        <h3 class="bgrejected" id="hdchngReqCrossThirty" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <%--CSR PORTLET--%>
                                    <div class="col-sm-4">
                                        <div class="investordashboard-sec apaaportlet ">
                                            <h4>
                                                CSR Spending <a class="pull-right Csrfilter"><i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer">
                                                <div class="form-group search" id="csrsearch">
                                                    <div class="row margin-bottom10">
                                                        <label class="col-sm-3">
                                                            District
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="ddlCSRDistrict" CssClass="form-control" runat="server">
                                                            </asp:DropDownList>
                                                            <%-- <div class="listcontrol checkbox form-control">
                                                     <asp:CheckBox ID="chkCSRDistrctheader" runat="server" Checked="true" onclick="setCBLCSRDist(this)"
                                                            Text="Check All"></asp:CheckBox>
                                                        <asp:CheckBoxList ID="chkCSRDistrct" CssClass="" runat="server">
                                                        </asp:CheckBoxList>
                                                        <asp:HiddenField ID="hdnCSRDistrct" runat="server"></asp:HiddenField>
                                                    </div>--%>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <label class="col-sm-3">
                                                            Year
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="ddlCSRYear" CssClass="form-control" runat="server">
                                                                <%-- <asp:ListItem>2017</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-3 padding-left-0">
                                                            <asp:Button ID="btnCSRStatus" CssClass="btn btn-success" runat="server" Text="Submit"
                                                                OnClick="btnCSRStatus_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <ul>
                                                    <li><span>CSR Spending </span>
                                                        <h3 class="bgapplied" id="hdSpent" runat="server" style="width: 89px;">
                                                            <small class="text-white">Cr.</small></h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </li>
                                                    <li><a href="#csrModal" data-toggle="modal" data-target="#csrModal" title="Projects taken up"
                                                        onclick="ShowCSR();"><span>Projects taken up</span>
                                                        <h3 class="bgsanctioned" id="hdNoPrj" runat="server" style="width: 89px;">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- CICG PORTLET--%>
                                    <div class="col-sm-4">
                                        <div class="investordashboard-sec CICGportlet ">
                                            <h4>
                                                CENTRAL INSPECTION FRAMEWORK <a class="pull-right CICGsts"><i class="fa fa-search"></i>
                                                </a>
                                            </h4>
                                            <div class="portletcontainer">
                                                <div class="search" id="CICGsearch">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Year</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlYearCICG" runat="server">
                                                                    <asp:ListItem>---Select---</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-8 col-sm-offset-4">
                                                                <asp:Button ID="btnCICGStatus" CssClass="btn btn-success" runat="server" Text="Submit"
                                                                    OnClick="btnCICGStatus_Click"></asp:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <ul>
                                                    <li><span>Inspections Scheduled</span>
                                                        <h3 class="bgapplied" id="h3cicgapplied" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </li>
                                                    <li><span>Inspections Completed</span>
                                                        <h3 class="bgsanctioned" id="h3cicgcompleted" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </li>
                                                    <li><a href="#CICGModal" data-toggle="modal" data-target="#CICGModal" class="projectdtls"
                                                        onclick="ShowCICG('C');" title="Inspection reports pending"><span>Unattended Inspection</span><h3
                                                            class="bgpending" id="h3unattInsdash" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                    <li><a href="#CICGModal" data-toggle="modal" data-target="#CICGModal" class="projectdtls"
                                                        onclick="ShowCICG('D');" title="Inspection reports pending"><span>Inspection reports
                                                            pending</span>
                                                        <h3 class="bgrejected" id="h3ReprtNotUploaded" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- GRIEVANCE PORTLET--%>
                                    <div class="col-sm-4">
                                        <div class="investordashboard-sec">
                                            <h4>
                                                Grievance Status
                                            </h4>
                                            <div class="portletcontainer">
                                                <ul>
                                                    <li><a href="#GRVModal" data-toggle="modal" data-target="#GRVModal" title="Applied"
                                                        onclick="ShowGRV('0');"><span>Applied </span>
                                                        <h3 class="bgapplied" id="Gapplied" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                    <li><a href="#GRVModal" data-toggle="modal" data-target="#GRVModal" title="Resolved"
                                                        onclick="ShowGRV('1');"><span>Resolved </span>
                                                        <h3 class="bgsanctioned" id="Gresolved" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                    <li><a href="#GRVModal" data-toggle="modal" data-target="#GRVModal" title="Pending"
                                                        onclick="ShowGRV('2');"><span>Pending </span>
                                                        <h3 class="bgpending" id="Gpending" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                    <li><a href="#GRVModal" data-toggle="modal" data-target="#GRVModal" title="Rejected"
                                                        onclick="ShowGRV('3');"><span>Rejected </span>
                                                        <h3 class="bgrejected" id="Grejected" runat="server">
                                                        </h3>
                                                        <div class="clearfix">
                                                        </div>
                                                    </a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--<div id="QueryModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content modal-primary ">
                    <div class="modal-header bg-red">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">
                            SPMG Details</h4>
                    </div>
                    <div class="modal-body">
                        <iframe name="myIframe" id="mySPMGIframe" width="100%" style="height: 298px" runat="server">
                        </iframe>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>--%>
            <div id="serviceModal" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content modal-primary ">
                        <div class="modal-header bg-red">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                Pending &amp; Rejected Approvals</h4>
                        </div>
                        <div class="modal-body">
                            <iframe name="myIframe" id="myservcIframe" width="100%" style="height: 298px" runat="server">
                            </iframe>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="IncentiveModal" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content modal-primary ">
                        <div class="modal-header bg-red">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                Incentive Pending &amp; Rejected Details</h4>
                        </div>
                        <div class="modal-body">
                            <iframe name="IncentiveIframe" id="IncentiveIframe" width="100%" style="height: 298px"
                                runat="server"></iframe>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="SPMGModal" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content modal-primary ">
                        <div class="modal-header bg-red">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                SPMG Details</h4>
                        </div>
                        <div class="modal-body">
                            <iframe name="myIframeSPMG" id="myIframeSPMG" width="100%" style="height: 298px"
                                runat="server"></iframe>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="ApaaModal" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content modal-primary ">
                        <div class="modal-header bg-red">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                Change requests crossed >30 days</h4>
                        </div>
                        <div class="modal-body">
                            <iframe name="myIframe" id="myAPAAIframe" width="100%" style="height: 298px" runat="server">
                            </iframe>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="Div1" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content modal-primary ">
                        <div class="modal-header bg-red">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                PEAL Details</h4>
                        </div>
                        <div class="modal-body">
                            <iframe name="myIframe" id="myIframe" width="100%" style="height: 298px" runat="server">
                            </iframe>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="csrModal" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content modal-primary ">
                        <div class="modal-header bg-red">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                CSR Details</h4>
                        </div>
                        <div class="modal-body">
                            <iframe name="myIframe" id="myCSRIframe" width="100%" style="height: 298px" runat="server">
                            </iframe>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="CICGModal" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content modal-primary ">
                        <div class="modal-header bg-red">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                CICG Details</h4>
                        </div>
                        <div class="modal-body">
                            <iframe name="myIframe" id="myCICGIframe" width="100%" style="height: 298px" runat="server">
                            </iframe>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="GRVModal" class="modal fade" role="dialog" style="height: 500px;">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content modal-primary">
                        <div class="modal-header bg-red">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                Grievance Details</h4>
                        </div>
                        <div class="modal-body">
                            <iframe name="myIframeGRV" id="myIframeGRV" width="100%" style="height: 298px" runat="server">
                            </iframe>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="LinkButton1" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <uc3:footer ID="footer" runat="server" />
    </form>
    <script type="text/javascript">

        (function ($) {
            $(window).on("load", function () {
                $(".contentbox").mCustomScrollbar();
            });
        })(jQuery);

        // $(document).ready(function (e) {
        //            $('.counter').counterUp({ delay: 10, time: 2000 });
        //            $('.spmgfilter').click(function () {
        //                $(this).find(".fa").toggleClass('fa-search fa-times-circle');
        //                $('#spmgfilter').slideToggle();
        //            });
        //            $('.APAAsts').click(function () {
        //                $(this).find(".fa").toggleClass('fa-search fa-times-circle');
        //                $('#APAAsts').slideToggle();
        //            });

        //            $('.Csrfilter').click(function () {
        //                $(this).find(".fa").toggleClass('fa-search fa-times-circle');
        //                $('#csrsearch').slideToggle();
        //            });
        //            $('.CICGsts').click(function () {
        //                $(this).find(".fa").toggleClass('fa-search fa-times-circle');
        //                $('#CICGsearch').slideToggle();
        //            });
        //});

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('.counter').counterUp({ delay: 10, time: 2000 });
                    $('.spmgfilter').click(function () {
                        $(this).find(".fa").toggleClass('fa-search fa-times-circle');
                        $('#spmgfilter').slideToggle();
                    });
                    $('.APAAsts').click(function () {
                        $(this).find(".fa").toggleClass('fa-search fa-times-circle');
                        $('#APAAsts').slideToggle();
                    });

                    $('.Csrfilter').click(function () {
                        $(this).find(".fa").toggleClass('fa-search fa-times-circle');
                        $('#csrsearch').slideToggle();
                    });
                    $('.CICGsts').click(function () {
                        $(this).find(".fa").toggleClass('fa-search fa-times-circle');
                        $('#CICGsearch').slideToggle();
                    });

                }
            });
        };

        function ShowAPAA(APAAStatus) {
            var month = $('#ddlAppaMonth').val();
            var dist = '0';
            var year = $('#ddlAppaYear').val();
            var deptid = '0';
            document.getElementById('myAPAAIframe').src = "APAAInvestorGrid.aspx?month=" + month + "&dist=" + dist + "&year=" + year + "&deptid=" + deptid + "&Type=" + 0 + "&APAAStatus=" + APAAStatus;
        }

        function ShowCSR() {
            //            var dist = "";
            //            if ($('#hdnCSRDistrct').val() != "") {
            //                dist = $('#hdnCSRDistrct').val();
            //            }
            //            else {
            //                dist = "0";
            //            }
            var dist = $('#ddlCSRDistrict').val();
            var year = $('#ddlCSRYear').val();
            document.getElementById('myCSRIframe').src = "CSRInvestorGrid.aspx?dist=" + dist + "&year=" + year;
        } myCICGIframe
        //ADDED BY SUROJ KUMAR PRADHAN FOR INCENTIVE
        function ShowIncentive(Status) {
            var investorId = $('#DrpDwn_Investor_Unit').val();
            if (investorId > 0) {
                var filterMode = 'I';
            }
            else {
                var filterMode = 'C';
            }
            document.getElementById('IncentiveIframe').src = "IncentiveInvestorStatus.aspx?Action=" + Status + "&FilterMode=" + filterMode + "&InvestorId=" + investorId;
        }
        //ENDED BY SUROJ

        function Show(status) {
            var investorId = $('#DrpDwn_Investor_Unit').val();
            if (investorId > 0) {
                var filterMode = 'I';
            }
            else {
                var filterMode = 'C';
            }

            document.getElementById('myservcIframe').src = "InnerInvestorDashboard.aspx?FilterMode=" + filterMode + "&InvestorId=" + investorId + "&Status=" + status;
        }

        //ADDED BY NIBEDITA BEHERA ON 16-12-2017 FOR CICG DASHBOARD DETAILS
        function ShowCICG(CICGStatus) {
            var year = $('#ddlYearCICG').val();
            document.getElementById('myCICGIframe').src = "CICGInvestorDashDetails.aspx?year=" + year + "&Type=" + 0 + "&CICGStatus=" + CICGStatus;
        }

        function ShowSPMG(SPMGStatus) {
            var year = $('#ddlspmgyear').val();
            var InvestorId = '<%= Session["UID"] %>';
            document.getElementById('myIframeSPMG').src = "SPMGInvestorDetails.aspx?year=" + year + "&SPMGStatus=" + SPMGStatus + "&InvestorId=" + InvestorId;
        }
        function ShowGRV(GRVStatus) {
            var InvestorId = '<%= Session["UID"] %>';
            document.getElementById('myIframeGRV').src = "GrievanceInvestorDetails.aspx?GRVStatus=" + GRVStatus + "&InvestorId=" + InvestorId;
        }

    </script>
</body>
</html>
