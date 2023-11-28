<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvesterDashboardNonIndustry.aspx.cs" Inherits="InvesterDashboardNonIndustry" %>


<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/NonIndustryHeader.ascx" TagName="NonIndustryheader" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/NonIndustryMenu.ascx" TagName="NonIndustryinvestoemenu" TagPrefix="uc4" %>
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
		.investordashboard-sec ul li h3 { width:auto;  text-align: center; font-size: 20px; color: #fff; padding: 6px 12px;  border-radius: 4px;}
		.masterportletsec h2 {
    margin: 0 0 0px;
    font-size: 20px;
    line-height: 27px;
}.masterportletsec {
    padding: 15px 15px;
    
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
            <uc2:NonIndustryheader ID="header" runat="server" />
            <div class="container wrapper">
                <div class="registration-div investors-bg">
                    <div id="exTab1" class="">
                        <div class="investrs-tab">
                            <div class="row">
                                <div class="col-sm-12">
                                    <uc4:NonIndustryinvestoemenu ID="ineste" runat="server" />
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
                                        <div class="col-sm-4">
                                            <h2>
                                                Master Tracker
                                                <%--<small>(For 2017-18) </small>--%>
                                            </h2>
                                        </div>
                                        <div class="col-sm-4">
										<strong class="collon">:</strong>
                                            <asp:DropDownList ID="DrpDwn_Investor_Unit" runat="server" CssClass="form-control dropx"
                                                AutoPostBack="true" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                  
                                </div>
                                <div class="row">
                                    <%-- GRIEVANCE PORTLET--%>
                                    
									
									
                                      <div class="groupmastreportlet2" style="display:none;">
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
                                       
                                    <div class="col-sm-12">
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
            document.getElementById('myIframeGRV').src = "GrievanceNonInvestorDetails.aspx?GRVStatus=" + GRVStatus + "&InvestorId=" + InvestorId;
        }

    </script>
</body>
</html>
