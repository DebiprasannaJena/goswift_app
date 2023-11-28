﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="PS(MSME)Dashboard.aspx.cs" Inherits="Portal_Dashboard_PS__MSME_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function pageLoad() {
            //MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
            $("#<%=CheckBoxList1.ClientID %>").find('input[type="checkbox"]').click(function () {
                var cbl = document.getElementById('<%=CheckBoxList1.ClientID %>').getElementsByTagName("input");
                chkCnt = 0;
                for (i = 0; i < cbl.length; i++) {
                    if (cbl[i].checked == true) {
                        chkCnt = chkCnt + 1;
                    }
                }
                if (cbl.length == chkCnt) {
                    $("#ContentPlaceHolder1_chkAll").prop('checked', true);
                } else {
                    $("#ContentPlaceHolder1_chkAll").prop('checked', false);
                }
            });
            $("#<%=chkYearwise.ClientID %>").find('input[type="checkbox"]').click(function () {
                var cbl = document.getElementById('<%=chkYearwise.ClientID %>').getElementsByTagName("input");
                chkCnt = 0;
                for (i = 0; i < cbl.length; i++) {
                    if (cbl[i].checked == true) {
                        chkCnt = chkCnt + 1;
                    }
                }
                if (cbl.length == chkCnt) {
                    $("#ContentPlaceHolder1_chkYearwiseHeader").prop('checked', true);
                } else {
                    $("#ContentPlaceHolder1_chkYearwiseHeader").prop('checked', false);
                }
            });
            var yr1 = $('#ContentPlaceHolder1_ddlPealYear').val();
            $('#ContentPlaceHolder1_lbl1').html(yr1);
            $('#ContentPlaceHolder1_lbldet1').html(yr1);
            $('#btnPealsubmit').click(function () {
                yr1 = $('#ContentPlaceHolder1_ddlPealYear').val();
                $('#ContentPlaceHolder1_lbl1').html(yr1);
                $('#ContentPlaceHolder1_lbldet1').html(yr1);
            });
            var yr2 = $('#ContentPlaceHolder1_ddlYearInvest').val();
            $('#ContentPlaceHolder1_lbl2').html(yr2);
            $('#btnPealcapitalsubmit').click(function () {
                yr2 = $('#ContentPlaceHolder1_ddlYearInvest').val();
                $('#ContentPlaceHolder1_lbl2').html(yr2);
            });

            var yr3 = $('#ContentPlaceHolder1_ddlYearEmployement').val();
            $('#ContentPlaceHolder1_lbl3').html(yr3);
            $('#btnPealEmployement').click(function () {
                yr3 = $('#ContentPlaceHolder1_ddlYearEmployement').val();
                $('#ContentPlaceHolder1_lbl3').html(yr3);
            });

            var yr8 = $('#ContentPlaceHolder1_ddlIncentiveYear option:selected').text();
            $('#ContentPlaceHolder1_lbl8').html(yr8);
            $('#ContentPlaceHolder1_lblDet6').html(yr8);
            $('#btnIncentiveSubmit').click(function () {
                yr8 = $('#ContentPlaceHolder1_ddlIncentiveYear option:selected').text();
                $('#ContentPlaceHolder1_lbl8').html(yr8);
                $('#ContentPlaceHolder1_lblDet6').html(yr8);
            });

            var yr6 = $('#ContentPlaceHolder1_ddlAppaYear option:selected').text();
            $('#ContentPlaceHolder1_lbl6').html(yr6);
            $('#ContentPlaceHolder1_lblDet4').html(yr6);
            $('#btnAPAASubmit').click(function () {
                yr6 = $('#ContentPlaceHolder1_ddlAppaYear option:selected').text();
                $('#ContentPlaceHolder1_lbl6').html(yr6);
                $('#ContentPlaceHolder1_lblDet4').html(yr6);
            });

            var yr11 = $('#ContentPlaceHolder1_ddlgyear option:selected').text();
            $('#ContentPlaceHolder1_lb15').html(yr11);
            $('#ContentPlaceHolder1_lblDet15').html(yr11);
            $('#btnGSearch').click(function () {
                yr11 = $('#ContentPlaceHolder1_ddlgyear option:selected').text();
                $('#ContentPlaceHolder1_lb15').html(yr11);
                $('#ContentPlaceHolder1_lblDet15').html(yr11);
            });
        }


        $(document).ready(function (e) {
            //  $('.counter').counterUp({ delay: 10, time: 2000 });
            $('.searchfilter,.spmgfilter').click(function () {
                $(this).find(".fa").toggleClass('fa-search fa-times-circle');
            });

            $('#filterpealsearch').click(function () {
                $('#pealsearch').slideToggle();
            });

            $('#filterIncentivesearh').click(function () {
                $('#Incentivesearh').slideToggle();
            });

            $('#linkDepartmentServices').click(function () {
                $('#DepartmentServices').slideToggle();
            });

            $('#linkAPAAStatus').click(function () {
                $('#APAAStatus').slideToggle();
            });

            $('#linkCSRActivities').click(function () {
                $('#CSRActivities').slideToggle();
            });

            $('#linkCIF').click(function () {
                $('#CIF').slideToggle();
            });

            $('#linkUnitREg').click(function () {
                $('#UnitREg').slideToggle();
            });

            $('#linkSpmg').click(function () {
                $('#SpmgContent').slideToggle();
            });

            $('#ppfilter').click(function () {
                $('#ppsearch').slideToggle();
            });

            $('#investmentfilter').click(function () {
                $('#investmentsearch').slideToggle();
            });

            $('#employmentfilter').click(function () {
                $('#employmentsearch').slideToggle();
            });

            $('#CifFilter').click(function () {
                $('#CifSearch').slideToggle();
            });

            $('#APAAfilter').click(function () {
                $('#APAAsearch').slideToggle();
            });

            $('#pealfilter').click(function () {
                $('#pealsearch').slideToggle();
            });

            $('#AMSfilter').click(function () {
                $('#AMSsearch').slideToggle();
            });

            $('#INCENTIVEfilter').click(function () {
                $('#INCENTIVEsearch').slideToggle();
            });

            $('#Queryfilter').click(function () {
                $('#Querysearch').slideToggle();
            });

            //added by ritika lath for incentive
            $('#ancIncentiveApplicationSearch').click(function () {
                $('#divIncentiveAppSearch').slideToggle();
            });

            $('#GRIEVANCEfilter').click(function () {
                $('#GRIEVANCEsearch').slideToggle();
            });

            ShowIncentiveApplicationDetails();
            //added by ritika lath for incentive
        });

        //added by ritika lath for incentive
        function ShowIncentiveApplicationDetails(Status) {
            var IncentiveType = "";
            var e = document.getElementById("ContentPlaceHolder1_drpInctYear");
            var IncentiveYear = e.options[e.selectedIndex].value;
            var Userid = "";
            var e = document.getElementById("ContentPlaceHolder1_drpInctAppDistrict");
            var Pealdistrict = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_drpInctUnitType");
            var unitType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_drpPolicy");
            var policy = e.options[e.selectedIndex].value;
            document.getElementById('ContentPlaceHolder1_iFrameIncentiveApplication').src = "../Incentive/IncentiveReport.aspx?unitType=" + unitType + "&IncentiveYear=" + IncentiveYear + "&Distid=" + Pealdistrict + "&pc_msme=1&policy=" + policy;
        }

        function RefreshIncentiveAppIFrame() {
            var ifr = document.getElementById('ContentPlaceHolder1_iFrameIncentiveApplication');
            ShowIncentiveApplicationDetails();
            $('#divIncentiveAppSearch').slideToggle();
        }

        //added by ritika lath for incentive

        //MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
        function test() {
            var val = [];
            $('#ContentPlaceHolder1_CheckBoxList1').find('input[type=checkbox]:checked').each(function () {
                val.push($(this).val());
            })
            $('#ContentPlaceHolder1_hddnValue').val(val.join(','));
        }

        function test1() {
            var val = [];
            $('#ContentPlaceHolder1_chkYearwise').find('input[type=checkbox]:checked').each(function () {
                val.push($(this).val());
            })
            $('#ContentPlaceHolder1_hddnValue1').val(val.join(','));
        }

        //ENDED BY SUROJ KUMAR PRADHAN ON 24-10-2017
        function Show(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlPealQuarter");
            var PealType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPEALDistrict");
            var Pealdistrict = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPealYear");
            var Pealyear = e.options[e.selectedIndex].value;
            //            document.getElementById('ContentPlaceHolder1_myIframe').src = "FramePealStatus.aspx?Status=" + Status + "&PealType=" + PealType + "&Pealdistrict=" + Pealdistrict;
            document.getElementById('ContentPlaceHolder1_myIframe').src = "FramePealStatus.aspx?Status=" + Status + "&PealType=" + 0 + "&Pealdistrict=" + Pealdistrict + "&PealYear=" + Pealyear + "&PealQuarter=" + 0;
        }

        //ADDED BY SUROJ KUMAR PRADHAN ON 05-12-17 TO DRILL DOWN PEAL DISTWISE AND STATEWISE DATA
        function ShowPealstatewise(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlPealQuarter");
            var PealType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPEALDistrict");
            var Pealdistrict = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPealYear");
            var Pealyear = e.options[e.selectedIndex].value;
            document.getElementById('ContentPlaceHolder1_myIframe').src = "FramePealStatus.aspx?Status=" + Status + "&PealType=" + 0 + "&Pealdistrict=" + Pealdistrict + "&PealYear=" + Pealyear + "&PealQuarter=" + 0 + "&stateid=" + 11;
        }

        function ShowPealdistwise(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlPealQuarter");
            var PealType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPEALDistrict");
            var Pealdistrict = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPealYear");
            var Pealyear = e.options[e.selectedIndex].value;
            document.getElementById('ContentPlaceHolder1_myIframe').src = "FramePealStatus.aspx?Status=" + Status + "&PealType=" + 0 + "&Pealdistrict=" + Pealdistrict + "&PealYear=" + Pealyear + "&PealQuarter=" + 0 + "&stateid=" + 12;
        }

        //ENDED BY SUROJ KUMAR PRADHAN
        //ADDED BY SUROJ KUMAR PRADHAN FOR INCENTIVE
        function ShowIncentive(Status) {
            // modified by nibedita behera on 4-Dec-2017 for fiter
            var IncentiveType = "";
            var e = document.getElementById("ContentPlaceHolder1_ddlIncentiveYear");
            var IncentiveYear = e.options[e.selectedIndex].value;
            var Userid = "";
            var e = document.getElementById("ContentPlaceHolder1_ddlIncentiveDistrict");
            var Pealdistrict = e.options[e.selectedIndex].value;

            document.getElementById('ContentPlaceHolder1_IncentiveIframe').src = "IncentiveStatus.aspx?Action=" + Status + "&IncentiveType=" + 0 + "&IncentiveYear=" + IncentiveYear + "&Userid=" + 0 + "&Distid=" + Pealdistrict;
        }
        //ENDED BY SUROJ

        function openpoupwin(ctrl) {

            $.ajax({
                type: "POST",
                url: "PS(MSME)Dashboard.aspx/PealDetailsBind",

                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    tempHTML = "";
                    $("#tblPeal").html('')
                    tempHTML += '<thead><tr class="persist-header">'
                    tempHTML += '<th rowspan="1" valign="middle" width="20px" bgcolor="#e4e4e4">Sl#</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Company Name</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Fee</th>'
                    tempHTML += '</tr></thead><tbody>'
                    $('#tblPeal').append(tempHTML);
                    tempHTML = "";
                    var serialNo = 0;
                    $.each(r.d, function (index, value) {
                        serialNo++;
                        if (r.d.length > 0) {
                            tempHTML += '<tr>';
                            tempHTML += '<td align="left">' + serialNo + '</td>';
                            tempHTML += '<td >' + value.strComapnyName + '</td>';
                            tempHTML += '<td >' + value.decFee + '</td>';
                            tempHTML += '</tr>';
                        }
                    });
                    if (r.d.length == 0) {
                        $("#tblPeal").html('')
                        tempHTML += '<tr><td>No Records Found...</td></tr>';
                        $("#tblPeal").append(tempHTML);
                    }
                    else {
                        $("#tblPeal").append(tempHTML); //APPEND THE DYNAMIC VALUE IN ROW
                        $("#tblPeal").append("</tbody>");
                    }
                },
                error: function (response) {

                    var msg = jQuery.parseJSON(response.responseText);
                    alert("Message: " + msg.Message + "<br /> StackTrace:" + msg.StackTrace + "<br /> ExceptionType:" + msg.ExceptionType);
                }
            });
        }

        function ShowWiseInvdet() {

            //            var Dept = $('#ContentPlaceHolder1_ddldept').val();
            //            var Servc = $('#ContentPlaceHolder1_ddlService').val();
            //document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimeline.aspx?Dept=0&servc=0";
            $('#ContentPlaceHolder1_lblemp').html($('#ContentPlaceHolder1_ddlYearEmployement').val());
            var Pealyear = $('#ContentPlaceHolder1_ddlYearEmployement').val();
            var Pealdistrictdtls = $('#ContentPlaceHolder1_hddnValue').val();
            document.getElementById('ContentPlaceHolder1_Iframe2').src = "YearWiseEmployeementDetails.aspx?Pealyear=" + Pealyear + "&Pealdistrictdtls=" + Pealdistrictdtls;
        }

        function openpoupwinEmploeement(ctrl) {
            var total = 0;
            var ExistingTotal = 0; //ADDED BY SUROJ FOR EXHISTING EMPLOYEE
            $.ajax({
                type: "POST",
                url: "PS(MSME)Dashboard.aspx/EmployeementPealDetailsBind",

                data: '{"Pealyear":"' + $('#ContentPlaceHolder1_ddlYearEmployement').val() + '","Pealdistrictdtls":"' + $('#ContentPlaceHolder1_hddnValue').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#ContentPlaceHolder1_lblemp').html($('#ContentPlaceHolder1_ddlYearEmployement').val());
                    tempHTML = "";
                    $("#tblEmployemnt").html('')
                    tempHTML += '<thead><tr class="persist-header">'
                    tempHTML += '<th rowspan="1" valign="middle" width="20px" bgcolor="#e4e4e4">Sl#</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Company Name</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">District Name</th>'//ADDED BY SUROJ ON 21-10-17
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Direct Employee</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Contractual Employee</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Employees</th>'
                    tempHTML += '<th rowspan="1" valign="middle"   bgcolor="#e4e4e4">Existing Employees</th>'//ADDED BY SUROJ FOR EXHISTING EMPLOYEE
                    tempHTML += '</tr></thead><tbody>'
                    $('#tblEmployemnt').append(tempHTML);
                    tempHTML = "";
                    var serialNo = 0;
                    $.each(r.d, function (index, value) {
                        serialNo++;
                        if (r.d.length > 0) {
                            tempHTML += '<tr>';
                            tempHTML += '<td align="right">' + serialNo + '</td>';
                            tempHTML += '<td >' + value.strComapnyName + '</td>';
                            tempHTML += '<td>' + value.strDistrictName + '</td>'; //ADDED BY SUROJ ON 21-10-17
                            tempHTML += '<td align="right">' + value.intDirectEmployee + '</td>';
                            tempHTML += '<td align="right">' + value.intContractualEmployee + '</td>';
                            tempHTML += '<td align="right">' + value.intEmployeement + '</td>';
                            tempHTML += '<td align="right">' + value.intExhistingemployee + '</td>'; //ADDED BY SUROJ FOR EXHISTING EMPLOYEE
                            tempHTML += '</tr>';
                            total += value.intEmployeement;
                            ExistingTotal += value.intExhistingemployee; //ADDED BY SUROJ FOR EXHISTING EMPLOYEE
                        }
                    });
                    if (r.d.length == 0) {
                        $("#tblEmployemnt").html('')
                        tempHTML += '<tr><td>No Records Found...</td></tr>';
                        $("#tblEmployemnt").append(tempHTML);
                    }
                    else {

                        $("#tblEmployemnt").append(tempHTML); //APPEND THE DYNAMIC VALUE IN ROW
                        $('#tblEmployemnt').append('<tr><td></td><td></td><td></td><td></td><td bgcolor="#e4e4e4">Total</td><td align="right">' + total + '</td><td align="right">' + ExistingTotal + '</td></tr>'); //ADDED BY SUROJ ON 21-10-17
                        $("#tblEmployemnt").append("</tbody>");
                    }
                },
                error: function (response) {

                    var msg = jQuery.parseJSON(response.responseText);
                    alert("Message: " + msg.Message + "<br /> StackTrace:" + msg.StackTrace + "<br /> ExceptionType:" + msg.ExceptionType);
                }
            });
        }

        function ShowWiseInv() {

            //            var Dept = $('#ContentPlaceHolder1_ddldept').val();
            //            var Servc = $('#ContentPlaceHolder1_ddlService').val();
            //document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimeline.aspx?Dept=0&servc=0";
            $('#ContentPlaceHolder1_lblYearinv').html($('#ContentPlaceHolder1_ddlYearInvest').val());
            var Pealyear = $('#ContentPlaceHolder1_ddlYearInvest').val();
            var Pealdistrictdtls = $('#ContentPlaceHolder1_hddnValue1').val();
            document.getElementById('ContentPlaceHolder1_Iframe1').src = "FrameYearWiseInvestment.aspx?Pealyear=" + Pealyear + "&Pealdistrictdtls=" + Pealdistrictdtls;
        }

        function openpoupwincapiatal(ctrl) {
            var total = 0;
            $.ajax({
                type: "POST",
                url: "PS(MSME)Dashboard.aspx/EmployeementCapitalPealDetailsBind",

                data: '{"Pealyear":"' + $('#ContentPlaceHolder1_ddlYearInvest').val() + '","Pealdistrictdtls":"' + $('#ContentPlaceHolder1_hddnValue1').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#ContentPlaceHolder1_lblYearinv').html($('#ContentPlaceHolder1_ddlYearInvest').val());
                    tempHTML = "";
                    $("#lblCapital").html('')
                    tempHTML += '<thead><tr class="persist-header">'
                    tempHTML += '<th rowspan="1" valign="middle" width="20px" bgcolor="#e4e4e4">Sl#</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Company Name</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">District Name</th>'//ADDED BY SUROJ ON 21-10-17
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Amount in cr</th>'
                    tempHTML += '</tr></thead><tbody>'
                    $('#lblCapital').append(tempHTML);
                    tempHTML = "";
                    var serialNo = 0;
                    $.each(r.d, function (index, value) {
                        serialNo++;
                        if (r.d.length > 0) {
                            tempHTML += '<tr>';
                            tempHTML += '<td align="left">' + serialNo + '</td>';
                            tempHTML += '<td >' + value.strComapnyName + '</td>';
                            tempHTML += '<td>' + value.strDistrictName + '</td>'; //ADDED BY SUROJ ON 21-10-17
                            tempHTML += '<td  align="right">' + value.intEmployeement1 + '</td>';
                            tempHTML += '</tr>';
                            total += value.intEmployeement1;
                        }
                    });
                    if (r.d.length == 0) {
                        $("#lblCapital").html('')
                        tempHTML += '<tr><td>No Records Found...</td></tr>';
                        $("#lblCapital").append(tempHTML);
                    }
                    else {
                        $("#lblCapital").append(tempHTML); //APPEND THE DYNAMIC VALUE IN ROW
                        $('#lblCapital').append('<tr><td></td><td></td><td bgcolor="#e4e4e4">Total</td><td  align="right">' + total.toFixed(2) + '</td></tr>'); //ADDED BY SUROJ ON 21-10-17
                        $("#lblCapital").append("</tbody>");
                    }
                },
                error: function (response) {

                    var msg = jQuery.parseJSON(response.responseText);
                    alert("Message: " + msg.Message + "<br /> StackTrace:" + msg.StackTrace + "<br /> ExceptionType:" + msg.ExceptionType);
                }
            });
        }

        function ShowSearchpanel() {
            //dvservce

            $('[id*=DepartmentServices]').css("display", "block");
            return false;
        }

    </script>
    <style type="text/css">
        .portletcontainer.cmdashbordportlet {
            min-height: 242px;
        }

        .portletsearch .listcontrol {
            height: 30px;
            padding: 4px 28px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            $('.searchfilter,.spmgfilter').click(function () {
                                $(this).find(".fa").toggleClass('fa-search fa-times-circle');
                            });

                            $('#filterpealsearch').click(function () {
                                $('#pealsearch').slideToggle();
                            });

                            $('#filterIncentivesearh').click(function () {
                                $('#Incentivesearh').slideToggle();
                            });

                            $('#linkDepartmentServices').click(function () {
                                $('#DepartmentServices').slideToggle();
                            });

                            $('#linkAPAAStatus').click(function () {
                                $('#APAAStatus').slideToggle();
                            });

                            $('#linkCSRActivities').click(function () {
                                $('#CSRActivities').slideToggle();
                            });

                            $('#linkCIF').click(function () {
                                $('#CIF').slideToggle();
                            });

                            $('#linkUnitREg').click(function () {
                                $('#UnitREg').slideToggle();
                            });

                            $('#linkSpmg').click(function () {
                                $('#SpmgContent').slideToggle();
                            });

                            $('#ppfilter').click(function () {
                                $('#ppsearch').slideToggle();
                            });

                            $('#investmentfilter').click(function () {
                                $('#investmentsearch').slideToggle();
                            });

                            $('#employmentfilter').click(function () {
                                $('#employmentsearch').slideToggle();
                            });

                            $('#CifFilter').click(function () {
                                $('#CifSearch').slideToggle();
                            });

                            $('#APAAfilter').click(function () {
                                $('#APAAsearch').slideToggle();
                            });

                            $('#pealfilter').click(function () {
                                $('#pealsearch').slideToggle();
                            });

                            $('#AMSfilter').click(function () {
                                $('#AMSsearch').slideToggle();
                            });

                            $('#INCENTIVEfilter').click(function () {
                                $('#INCENTIVEsearch').slideToggle();
                            });

                            $('#Queryfilter').click(function () {
                                $('#Querysearch').slideToggle();
                            });

                            //added by ritika lath for incentive
                            $('#ancIncentiveApplicationSearch').click(function () {
                                $('#divIncentiveAppSearch').slideToggle();
                            });

                            $('#GRIEVANCEfilter').click(function () {
                                $('#GRIEVANCEsearch').slideToggle();
                            });

                            ShowIncentiveApplicationDetails();
                            //added by ritika lath for incentive
                        }
                    });
                };

            </script>
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <div class="header-icon">
                        <i class="fa fa-tachometer"></i>
                    </div>
                    <div class="header-title">
                        <h1 runat="server" id="h1Dashuser"></h1>
                        <ul class="breadcrumb">
                            <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                            <li><a id="adashuser" runat="server"></a></li>
                        </ul>
                    </div>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="row">
                        <!-- Form controls -->
                        <div class="col-sm-12">
                            <div class="Mastertracker">
                                <p class="pull-right">
                                    Last updated on <span id="spLastUpdate" runat="server"></span>
                                    <asp:LinkButton ID="LinkButton1" CssClass="refresh" runat="server" OnClick="LinkButton1_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
                                </p>
                                <h4>Master Tracker <small>
                                    <asp:DropDownList ID="ddlFinacialYear" runat="server" CssClass="masterdropdown" OnSelectedIndexChanged="ddlFinacialYear_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </small>
                                </h4>
                                <div class="row">
                                    <div class="col-sm-6  col-md-4">
                                        <div class="masterportletsec">
                                            <h4>Single Window Application Status
                                            </h4>
                                            <div class="width33">
                                                <p>
                                                    Application Received <span id="lblProposalRecieved" runat="server"></span>
                                                </p>
                                            </div>
                                            <div class="width33">
                                                <p>
                                                    Application Approved <span id="lblProposalapproved" runat="server"></span>
                                                </p>
                                            </div>
                                            <div class="width33 margin-right0">
                                                <p>
                                                    Application Pending <span id="lblTrackerEvalution" runat="server"></span>
                                                </p>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="masterportletsec">
                                            <h4>Pending approvals
                                            </h4>
                                            <p>
                                                Applications past ORTPSA timelines<span id="spanapprove" runat="server"></span>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="masterportletsec" style="display: none;">
                                            <h4>CSR Spend
                                            </h4>
                                            <p>
                                                Total Spending <span><i class="fa fa-rupee"></i>&nbsp;<label id="SPNetSpent" runat="server"></label><small>&nbsp;Cr.</small></span>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="masterportletsec">
                                            <h4>Investment
                                            </h4>
                                            <p>
                                                Total Proposed Investment<span>
                                                    <%-- <asp:Label ID="lblmastinv" runat="server" Text=""></asp:Label>--%>
                                                    <i class="fa fa-inr"></i>
                                                    <asp:Literal ID="lblmastinv" runat="server"></asp:Literal>
                                                    <small>Cr.</small></span>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="masterportletsec">
                                            <h4>Employment</h4>
                                            <p>
                                                Total Proposed Employment <span id="SPEmpGen" runat="server"></span>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="masterportletsec">
                                            <h4>Incentive Details</h4>
                                            <p>
                                                Pending incentives <span id="spIncPending" runat="server"></span>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="masterportletsec">
                                            <h4>Central Inspection Framework</h4>
                                            <p>
                                                Pending inspections <span id="SPcicgpending" runat="server"></span>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="masterportletsec">
                                            <h4>IDCO Post Allotment Applications</h4>
                                            <p>
                                                Pending issues <span id="spAPAAPending" runat="server"></span>
                                            </p>
                                        </div>
                                    </div>
                                    <%--<div class="col-sm-6  col-md-4">
                <div class="masterportletsec" style="display: none;">
                    <h4>
                        State Project Monitoring Group
                    </h4>
                    <p>
                        Issues pending<span id="spSpmgpnd" runat="server"></span></p>
                </div>
            </div>--%>

                                    <div class="col-sm-6  col-md-4">
                                        <div class="masterportletsec">
                                            <h4>Grievance Status
                                            </h4>
                                            <div>
                                                <p>
                                                    Pending <span id="Spangpending" runat="server"></span>
                                                </p>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="grphs-sec">
                                <div class="text-left text-red">
                                    <h4>For detailed information, select search option <i class="fa fa-search"></i>shown
                        in the menu bar for each field</h4>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6" style="display: none;">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Project Proposals <a class="pull-right ppfilter" data-toggle="tooltip" title="Search"
                                                id="ppfilter"><i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <ul>
                                                    <li>
                                                        <%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending ">--%>Applied<span>0</span><%--</a>--%></li>
                                                    <li>
                                                        <%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending ">--%>Approved<span>0</span><%--</a>--%></li>
                                                    <li>
                                                        <%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending ">--%>Approved,
                                        yet to commence operations<span>0</span><%--</a>--%></li>
                                                    <li>
                                                        <%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending ">--%>Deferred<span
                                                            class="bgdisbursed">0</span><%--</a>--%></li>
                                                    <li>
                                                        <%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending" >--%>Rejected<span
                                                            class="bgrejected">0</span><%--</a>--%></li>
                                                    <li>
                                                        <%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending" >--%>Under
                                        Evaluation<span>0</span><%--</a>--%></li>
                                                </ul>
                                            </div>
                                            <div class="portletsearch" id="ppsearch">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="DropDownList1" runat="server">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Value</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="DropDownList2" runat="server">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                                <asp:ListItem> < 50 crore</asp:ListItem>
                                                                <asp:ListItem> > 50 crore </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="Button1" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Project Proposals--%>
                                    <div class="col-md-8 col-sm-8">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Single Window Application Status (<asp:Label ID="lbl1" runat="server" Text="reerre"></asp:Label>)<a
                                                class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="pealfilter"><i
                                                    class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th rowspan="2" style="vertical-align: middle!important;">Status
                                                            </th>
                                                            <th width="130px" style="vertical-align: middle!important;" rowspan="2">State Level
                                                            </th>
                                                            <th width="130px" rowspan="2" style="vertical-align: middle!important;">District Level
                                                            </th>
                                                            <th width="260px" style="text-align: center!important;" colspan="2">SPL. Single Window
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <th width="130px" colspan="1">IT
                                                            </th>
                                                            <th width="130px" colspan="1">Tourism
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td style="color:blue;font-weight:bold;">Applied
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Applied" onclick="ShowPealstatewise(1);">
                                                                    <asp:Label ID="lblPealApplied" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Applied" onclick="ShowPealdistwise(1);">
                                                                    <asp:Label ID="lblPealdistApplied" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Applied" onclick="ShowITpeal(1);">
                                                                    <asp:Label ID="lblPealITApplied" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Applied" onclick="Showtourismpeal(1);">
                                                                    <asp:Label ID="lblPealTourismApplied" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color:green;font-weight:bold;">Approved
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Approved" onclick="ShowPealstatewise(2);">
                                                                    <asp:Label ID="lblPealApproved" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Approved" onclick="ShowPealdistwise(2);">
                                                                    <asp:Label ID="lblPealdistApproved" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Approved" onclick="ShowITpeal(2);">
                                                                    <asp:Label ID="lblPealITApproved" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Approved" onclick="Showtourismpeal(2);">
                                                                    <asp:Label ID="lblPealTourismApproved" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color:red;font-weight:bold;">Rejected
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Rejected" onclick="ShowPealstatewise(3);">
                                                                    <asp:Label ID="lblPealRejected" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Rejected" onclick="ShowPealdistwise(3);">
                                                                    <asp:Label ID="lblPealdistRejected" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Rejected" onclick="ShowITpeal(3);">
                                                                    <asp:Label ID="lblPealITRejected" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Rejected" onclick="Showtourismpeal(3);">
                                                                    <asp:Label ID="lblPealTourismRejected" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color:violet;font-weight:bold;">Deferred
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Deferred" onclick="ShowPealstatewise(7);">
                                                                    <asp:Label ID="lblPealDeferred" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Deferred" onclick="ShowPealdistwise(7);">
                                                                    <asp:Label ID="lblPealdistDeferred" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Deferred" onclick="ShowITpeal(7);">
                                                                    <asp:Label ID="lblPealITDeferred" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Deferred" onclick="Showtourismpeal(7);">
                                                                    <asp:Label ID="lblPealTourismDeferred" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color:dodgerblue;font-weight:bold;">Query In Progress
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Query In Progress"
                                                                    onclick="ShowPealstatewise(8);">
                                                                    <asp:Label ID="lblPealQueryRaise" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Query In Progress"
                                                                    onclick="ShowPealdistwise(8);">
                                                                    <asp:Label ID="lblPealdistQueryRaise" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Query In Progress"
                                                                    onclick="ShowITpeal(8);">
                                                                    <asp:Label ID="lblPealITQueryRaise" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Query In Progress"
                                                                    onclick="Showtourismpeal(8);">
                                                                    <asp:Label ID="lblPealTourismQueryRaise" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color:black;font-weight:bold;">Under Evalution
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Under Evalution" onclick="ShowPealstatewise(6);">
                                                                    <asp:Label ID="lblPealUnderEvalution" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Under Evalution" onclick="ShowPealdistwise(6);">
                                                                    <asp:Label ID="lblPealdistUnderEvalution" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Under Evalution" onclick="ShowITpeal(6);">
                                                                    <asp:Label ID="lblPealITUnderEvalution" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Under Evalution" onclick="Showtourismpeal(6);">
                                                                    <asp:Label ID="lblPealTourismUnderEvalution" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color:orange;font-weight:bold;">Application pending since last 30 days
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Application pending since last 30 days"
                                                                    onclick="ShowPealstatewise(9);">
                                                                    <asp:Label ID="Lbl_Peal_ORTPSA_State" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Application pending since last 30 days"
                                                                    onclick="ShowPealdistwise(9);">
                                                                    <asp:Label ID="Lbl_Peal_ORTPSA_Dist" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Application pending since last 30 days"
                                                                    onclick="ShowITpeal(9);">
                                                                    <asp:Label ID="Lbl_Peal_ORTPSA_IT" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                            <td align="right">
                                                                <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Application pending since last 30 days"
                                                                    onclick="Showtourismpeal(9);">
                                                                    <asp:Label ID="Lbl_Peal_ORTPSA_Tourism" runat="server" Text=""></asp:Label></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="portletsearch" id="pealsearch">
                                                <div class="form-group">
                                                    <div class="row" style="display: none">
                                                        <label class="col-sm-4">
                                                            Amount
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlPealQuarter" runat="server">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text=">50 Cr." Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="<50 Cr." Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlPealYear" runat="server">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            District</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlPEALDistrict" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="btnPealsubmit" CssClass="btn btn-success" runat="server" Text="Submit"
                                                                OnClick="btnPealsubmit_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- YEAR WISE INVESTMENT--%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Year wise investment (<asp:Label ID="lbl2" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right " data-toggle="tooltip" title="Search" id="investmentfilter"><i
                                                    class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                    <div class="singlevaluecintainer">
                                                        <%-- <a title="Total Proposed Investment" href="#investmentModal" data-toggle="modal"
                                                        data-target="#investmentModal" onclick="openpoupwincapiatal('HiddenField3');">--%>
                                                        <a href="#investmentModal" data-toggle="modal" data-target="#investmentModal" onclick="ShowWiseInv();"
                                                            title="Search" style="color:blue;font-weight:bold;">
                                                            <h3>
                                                                <i class="fa fa-inr"></i><span>
                                                                    <asp:Label ID="lblEmpInvestment" runat="server" Text=""></asp:Label></span><medium> Cr.</medium>
                                                                <asp:HiddenField ID="HiddenField4" Value="3" runat="server" />
                                                            </h3>
                                                            Total Proposed Investment<br />
                                                            <small>(For the Year
                                            <asp:Label ID="lblYearInvestment" runat="server" Text=""></asp:Label>) </small>
                                                        </a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="portletsearch" id="investmentsearch">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlYearInvest" runat="server">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            District</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <%-- <asp:DropDownList CssClass="form-control" ID="ddlYDistrictInvest" runat="server">
                <asp:ListItem>--Select--</asp:ListItem> 
                </asp:DropDownList>--%>
                                                            <div class="listcontrol checkbox form-control">
                                                                <asp:CheckBox ID="chkYearwiseHeader" runat="server" Checked="true" onclick="setCBL1(this)"
                                                                    Text="Check All"></asp:CheckBox>
                                                                <asp:CheckBoxList ID="chkYearwise" CssClass="" runat="server">
                                                                    <%--  <asp:ListItem Text="" Value=""></asp:ListItem>--%>
                                                                </asp:CheckBoxList>
                                                                <asp:HiddenField ID="hddnValue" runat="server"></asp:HiddenField>
                                                                <asp:HiddenField ID="hddnValue1" runat="server"></asp:HiddenField>
                                                                <asp:HiddenField ID="hdnFinacial" runat="server"></asp:HiddenField>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="btnPealcapitalsubmit" CssClass="btn btn-success" OnClientClick="return test1();"
                                                                runat="server" Text="Submit" OnClick="btnPealcapitalsubmit_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Year wise employment--%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Year wise employment (<asp:Label ID="lbl3" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right " data-toggle="tooltip" title="Search" id="employmentfilter"><i
                                                    class="fa fa-search"></i></a></h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                    <div class="scroll-prtlet">
                                                <div class="singlevaluecintainer">
                                                    <%-- <a title="Total Proposed Employment" href="#EmploymentModal" data-toggle="modal"
                                                        data-target="#EmploymentModal" onclick="openpoupwinEmploeement('HiddenField3');">--%>
                                                    <a href="#EmploymentModal" data-toggle="modal" data-target="#EmploymentModal" onclick="ShowWiseInvdet();"
                                                        title="" style="color:orange;font-weight:bold;">
                                                        <h3>
                                                            <i class="fa fa-users"></i><span>
                                                                <asp:Label ID="lblPealEmployeemnet" runat="server" Text=""></asp:Label></span>
                                                            <asp:HiddenField ID="HiddenField3" Value="3" runat="server" />
                                                        </h3>
                                                        Total Proposed Employment
                                        <br />
                                                        <small>(For the Year
                                            <asp:Label ID="lblYearEmployement" runat="server" Text=""></asp:Label>) </small>
                                                    </a>
                                                </div>
                                                        </div>
                                            </div>
                                            <div class="portletsearch" id="employmentsearch">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlYearEmployement" runat="server">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            District</label>
                                                        <div id="divtest" class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <div class="listcontrol checkbox form-control ">
                                                                <asp:CheckBox ID="chkAll" runat="server" Checked="true" onclick="setCBL(this)" Text="Check All"></asp:CheckBox>
                                                                <asp:CheckBoxList ID="CheckBoxList1" CssClass="" runat="server">
                                                                    <%--  <asp:ListItem Text="" Value=""></asp:ListItem>--%>
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="btnPealEmployement" CssClass="btn btn-success test" OnClientClick="return test();"
                                                                runat="server" Text="Submit" OnClick="btnPealEmployement_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--  Department Wise Approvals--%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Department Wise Approvals<a class="pull-right spmgfilter" data-toggle="tooltip" title="Search"
                                                id="linkDepartmentServices"><i class="fa fa-search"></i></a></h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                <ul>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('R');"
                                                        title="Application Received" style="color:blue;font-weight:bold;">Application Received<span id="hdApplied" runat="server"><asp:Literal
                                                            ID="ltlServiceApplied" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('A');"
                                                        title="Total Approvals Provided" style="color:green;font-weight:bold;">Total Approvals Provided<span id="hdApprove" runat="server"><asp:Literal
                                                            ID="ltlApprove" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('PK');"
                                                        title="Approval Pending" style="color:dodgerblue;font-weight:bold;">Query In Progress<span id="hdnqueryRaised" runat="server"><asp:Literal
                                                            ID="Literal1" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('P');"
                                                        title="Approval Pending" style="color:violet;font-weight:bold;">Approval Pending<span id="hdPending" runat="server"><asp:Literal
                                                            ID="ltlServicepending" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('RJ');"
                                                        title="Total Rejected " style="color:red;font-weight:bold;">Total Rejected <span id="hdReject" runat="server">
                                                            <asp:Literal ID="ltlServiceReject" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal1" data-toggle="modal" data-target="#DsModal1" onclick="ShowService1();"
                                                        title="Applications past ORTPSA timelines" style="color:orange;font-weight:bold;">Applications past ORTPSA timelines<span
                                                            class="bgdisbursed" style="color:orange;font-weight:bold;" id="hdExceed" runat="server"></span></a></li>
                                                </ul>
                                                    </div>
                                            </div>
                                            <div class="portletsearch" id="DepartmentServices">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Department</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control dpt" OnSelectedIndexChanged="ddldept_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Service</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlService" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="btnStatusOfApproval" CssClass="btn btn-success" runat="server" Text="Submit"
                                                                OnClick="btnStatusOfApproval_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--  Central Inspection Framework--%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Central Inspection Framework<a class="pull-right spmgfilter" data-toggle="tooltip"
                                                title="Search" id="CifFilter"><i class="fa fa-search"></i></a></h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                <ul>
                                                    <li><a title="Inspections Completed" href="#CIFModal" data-toggle="modal" data-target="#CIFModal"
                                                        onclick="ShowCICG('C');" style="color:green;font-weight:bold;">Inspections Completed<span id="SPcicgcompleted" runat="server"></span></a></li>
                                                    <li><a title="Inspections Scheduled" href="#CIFModal" data-toggle="modal" data-target="#CIFModal"
                                                        onclick="ShowCICG('A');" style="color:blue;font-weight:bold;">Inspections Scheduled<span id="SPcicgapplied" runat="server"></span></a></li>
                                                    <li><a title="Unattended Inspections" href="#CIFModal" data-toggle="modal" data-target="#CIFModal"
                                                        onclick="ShowCICG('B');" style="color:violet;font-weight:bold;">Unattended Inspections<span id="SPunattInsdash" runat="server"></span></a></li>
                                                    <li><a title="Inspection reports not uploaded within 48 hours" href="#CIFModal1"
                                                        data-toggle="modal" data-target="#CIFModal1" onclick="ShowCICG1();" style="color:orange;font-weight:bold;">Inspection reports
                                        not uploaded within 48 hours<span class="bgdisbursed" style="color:orange;font-weight:bold;" id="SPReprtNotUploaded" runat="server"></span></a></li>
                                                </ul>
                                                    </div>
                                            </div>
                                            <div class="portletsearch" id="CifSearch">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Department</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="ddldeptCIF" runat="server" CssClass="form-control dpt">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="ddlYearCICG" runat="server" CssClass="form-control dpt">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                                                            <asp:DropDownList CssClass="form-control" ID="ddlCICGMonth" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                                        </div>
                                    </div>
                                    <%--CSR ACTIVITIES--%>
                                    <div class="col-sm-6  col-md-4" style="display: none;">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>CSR ACTIVITIES <a class="pull-right spmgfilter" id="linkCSRActivities " data-toggle="tooltip"
                                                title="Search"><i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <ul>
                                                    <li>No. of recommended CSR Projects undertaken by corporates<span><label id="lblRcdTkn" runat="server"></label></span></li>
                                                    <li>Total Project<a href="#csrModal" data-toggle="modal" data-target="#csrModal"
                                                        title="Projects taken up" onclick="ShowCSR();"><span id="SPProject" runat="server"></span></a></li>
                                                    <li>Recommended by Council<span id="SPRecommendedCouncil" runat="server"></span></li>
                                                    <li>Total Spending<span><i class="fa fa-rupee"></i>&nbsp;<label id="SPSpent" runat="server"></label>&nbsp;
                                        Cr.</span></li>
                                                </ul>
                                                <div class="portletsearch" id="CSRActivities" style="top: -5px;">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                District</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlDistrict" runat="server">
                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Year</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlCSRYear" runat="server">
                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-8 col-sm-offset-4">
                                                                <asp:Button ID="btnCSRStatus" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Incentive Details--%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Incentive Details (<asp:Label ID="lbl8" runat="server" Text=""></asp:Label>)<a class="pull-right spmgfilter"
                                                data-toggle="tooltip" title="Search" id="INCENTIVEfilter"><i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                <ul>
                                                    <li>
                                                        <%--<span  id="ltlincSanctioned" runat="server">Approved</span>--%>
                                                        <a href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal" title="Sanctioned"
                                                            onclick="ShowIncentive('D');" style="color:green;font-weight:bold;">Sanctioned<span><asp:Label ID="lblIncsanctioed" runat="server"
                                                                Text=""></asp:Label></span></a> </li>
                                                    <%-- <span  id="ltlincPending" runat="server">Scrutiny</span>--%>
                                                    <li><a href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal" title="Scrutiny"
                                                        onclick="ShowIncentive('E');" style="color:violet;font-weight:bold;">Pending <span>
                                                            <asp:Label ID="lblIncpending" runat="server" Text=""></asp:Label></span></a>
                                                    </li>
                                                    <%--<span class="bgrejected" id="ltlincRejected" runat="server">Rejected</span>--%>
                                                    <li><a href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal" title="Rejected"
                                                        onclick="ShowIncentive('F');" style="color:red;font-weight:bold;">Rejected<span><asp:Label ID="lblIncrejected" runat="server"
                                                            Text=""></asp:Label></span></a>
                                                        <%--   <a title="" href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal">45</a></td><td><a title="" href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal">355</a>--%>
                                                    </li>
                                                    <%-- <span  id="ltlincApplied" runat="server">Applied</span>--%>
                                                    <li><a href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal" title="Disbursed"
                                                        onclick="ShowIncentive('C');" style="color:blue;font-weight:bold;">Disbursed <span>
                                                            <asp:Label ID="lblIncApplied" runat="server" Text=""></asp:Label></span></a>
                                                    </li>
                                                    <%-- <tr><td><span class="bgdisbursed" id="ltlincDistrubed" runat="server">Disbursed</span></td><td>0
                       <%--<a title="" href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal">45</a></td><td><a title="" href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal">355</a>
                       </td><td>0</td></tr>--%>
                                                </ul>
                                                    </div>
                                            </div>
                                            <div class="portletsearch" id="INCENTIVEsearch">
                                                <div class="form-group">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                District</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlIncentiveDistrict" runat="server">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    <%--<asp:ListItem Text=">50 Cr." Value="1"></asp:ListItem>
                <asp:ListItem Text="<50 Cr." Value="2"></asp:ListItem>--%>
                                                                    <%-- <asp:ListItem Text="Q1" Value="1"></asp:ListItem>
                <asp:ListItem Text="Q2" Value="2"></asp:ListItem>
                <asp:ListItem Text="Q3" Value="3"></asp:ListItem>
                <asp:ListItem Text="Q4" Value="4"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlIncentiveYear" runat="server">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="btnIncentiveSubmit" CssClass="btn btn-success" runat="server" Text="Submit"
                                                                OnClick="btnIncentiveSubmit_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--IDCO POST ALLOTMENT APPLICATIONS--%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>IDCO POST ALLOTMENT APPLICATIONS (<asp:Label ID="lbl6" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="APAAfilter"><i
                                                    class="fa fa-search"></i></a></h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                <ul>
                                                    <li><a title="Received" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('A');" style="color:blue;font-weight:bold;">Change requests received<span id="spchngrqstApplied" runat="server"></span></a></li>
                                                    <li><a title="Processed" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('B');" style="color:green;font-weight:bold;">Change requests processed<span id="spchngreqdispose" runat="server"></span></a></li>
                                                    <li><a title="Disposed" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('D');" style="color:dodgerblue;font-weight:bold;">Change requests pending to be disposed<span id="spchngreqPendAtIDCO"
                                                            runat="server"></span></a></li>
                                                    <li><a title="Disposed" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('C');" style="color:violet;font-weight:bold;">Change requests pending at Unit<span id="spnPendingatUnit"
                                                            runat="server"></span></a></li>
                                                    <li><a title="Approvals" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('E');" style="color:orange;font-weight:bold;">Change requests which have crossed 30 days<span class="bgdisbursed"
                                                            id="spchngReqCrossThirty" style="color:orange;font-weight:bold;" runat="server"></span></a></li>
                                                </ul>
                                                    </div>
                                            </div>
                                            <div class="portletsearch" id="APAAsearch">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Districts</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlAPAADistrict" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlAppaYear" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                                        </div>
                                    </div>
                                    <%--SPMG--%>
                                    <div class="col-sm-6  col-md-4" style="display: none;">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>STATE PROJECT MONITORING GROUP <a class="pull-right spmgfilter" id="linkSpmg" data-toggle="tooltip"
                                                title="Search"><i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <ul>
                                                    <li>Issues Received<span id="spmgraised" runat="server"></span></li>
                                                    <li>Issues Resolved<span id="spmgresolved" runat="server"></span></li>
                                                    <li>Issues Pending<span id="spmgpending" runat="server"></span></li>
                                                    <li><a title="Total Proposed Employment" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal">Issues Pending > 30 days <span id="spmg30pending" runat="server"></span></a>
                                                    </li>
                                                </ul>
                                                <div class="portletsearch" id="SpmgContent" style="top: -5px;">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Year</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlspmgyear" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-8 col-sm-offset-4">
                                                                <asp:Button ID="Button6" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--QUERY--%>
                                    <div class="col-sm-6 col-md-4" id="DVqUERY" runat="server">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Query Monitoring <a class="pull-right spmgfilter" id="Queryfilter"><i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                    <div class="table-responsive">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>Status
                                                                </th>
                                                                <th>PEAL
                                                                </th>
                                                                <th>Services
                                                                </th>
                                                                <th>Incentive
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>Queries Raised
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Queries Raised" onclick="ShowPealQuery(1);">
                                                                        <asp:Label ID="spRaisedpeal" runat="server" Text=""></asp:Label></a>
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Queries Raised" onclick="ShowServiceQuery(1);">
                                                                        <asp:Label ID="spTotalQueryRaised" runat="server"></asp:Label></a>
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Queries Raised" onclick="ShowIncentiveQuery(1);">
                                                                        <asp:Label ID="spRaisedIncentive" runat="server"></asp:Label></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Queries Responded
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Queries Responded"
                                                                        onclick="ShowPealQuery(2);">
                                                                        <asp:Label ID="spResolvedpeal" runat="server" Text=""></asp:Label></a>
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Queries Responded"
                                                                        onclick="ShowServiceQuery(2);">
                                                                        <asp:Label ID="spTotalQueryResponse" runat="server"></asp:Label></a>
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Queries Responded"
                                                                        onclick="ShowIncentiveQuery(2);">
                                                                        <asp:Label ID="spResolvedIncentive" runat="server"></asp:Label></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Response Pending
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response Pending"
                                                                        onclick="ShowPealQuery(3);">
                                                                        <asp:Label ID="spPendingpeal" runat="server" Text=""></asp:Label></a>
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response Pending"
                                                                        onclick="ShowServiceQuery(3);">
                                                                        <asp:Label ID="spTotalQueryPendings" runat="server"></asp:Label></a>
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response Pending"
                                                                        onclick="ShowIncentiveQuery(3);">
                                                                        <asp:Label ID="spPendingIncentive" runat="server"></asp:Label></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Response not received within Timeline
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response not received within Timeline"
                                                                        onclick="ShowPealQuery(4);">
                                                                        <asp:Label ID="spResponcenotRecPeal" runat="server" Text=""></asp:Label></a>
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response not received within Timeline"
                                                                        onclick="ShowServiceQuery(4);">
                                                                        <asp:Label ID="spNotResponse" runat="server"></asp:Label></a>
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response not received within Timeline"
                                                                        onclick="ShowIncentiveQuery(4);">
                                                                        <asp:Label ID="spResponcenotResIncentive" runat="server"></asp:Label></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Average Day Taken to Raise Query
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Average Time  Taken to Raise Query"
                                                                        onclick="ShowPealQuery(5);">
                                                                        <asp:Label ID="spAvgTimeQuerypeal" runat="server" Text=""></asp:Label></a>
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Average Time  Taken to Raise Query"
                                                                        onclick="ShowServiceQuery(5);">
                                                                        <asp:Label ID="spAvgTimeTaken" runat="server"></asp:Label></a>
                                                                </td>
                                                                <td align="right">
                                                                    <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Average Time  Taken to Raise Query"
                                                                        onclick="ShowIncentiveQuery(5);">
                                                                        <asp:Label ID="spAvgTimeQueryIncentive" runat="server"></asp:Label></a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <%-- <ul>
                       <li>Queries Raised<span  id="spTotalQueryRaised" runat="server"></span></li>
                        <li>Queries Responded<span  id="spTotalQueryResponse" runat="server"></span></li>
                         <li>Response not received within Timeline<span  id="spNotResponse" runat="server"></span></li>
                         <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal" onclick="ShowQuery();"  title="Response Pending">Response Pending<span class="bgdisbursed" id="spTotalQueryPendings" runat="server"></span></a></li>
                           <li>Average Time  Taken to Raise Query<span  id="spAvgTimeTaken" runat="server"></span></li>
                     </ul>--%>
                                                <div class="portletsearch" id="Querysearch" style="top: -0px;">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Year
                                                            </label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlyearquery" runat="server">
                                                                    <%--  <asp:ListItem Text="--Select--" Value="0"></asp:ListItem> 
                                                                    --%>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" style="display: none">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Month
                                                            </label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlMonthQuery" runat="server">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-8 col-sm-offset-4">
                                                                <asp:Button ID="btnQuery" CssClass="btn btn-success" runat="server" Text="Submit"
                                                                    OnClick="btnQuery_Click"></asp:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Agenda Management System--%>
                                    <div class="col-sm-6  col-md-4" style="display: none;">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Agenda Management System <a class="pull-right spmgfilter" data-toggle="tooltip" title="Search"
                                                id="AMSfilter"><i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="table-responsive">
                                                    <table class="table table-bordered tablefixheight">
                                                        <tr>
                                                            <th>Sl#.
                                                            </th>
                                                            <th>Name of the unit
                                                            </th>
                                                            <th width="150px">Nodal Person
                                                            </th>
                                                            <th>Count of days since PEAL clearance is pending
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td rowspan="2">1
                                                            </td>
                                                            <td rowspan="2">ABC
                                                            </td>
                                                            <td>Mr./Ms. LMN
                                                            </td>
                                                            <td>10
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Mr./Ms. ABC
                                                            </td>
                                                            <td>15
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td rowspan="2">2
                                                            </td>
                                                            <td rowspan="2">XYZ
                                                            </td>
                                                            <td>Mr./Ms. LMN
                                                            </td>
                                                            <td>12
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Mr./Ms. ABC
                                                            </td>
                                                            <td>35
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="portletsearch" id="AMSsearch">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Range</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="DropDownList18" runat="server">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="Button10" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Incentive Application Details added by ritika lath --%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec">
                                            <h4>Incentive Application Details<a class="pull-right spmgfilter" data-toggle="tooltip"
                                                title="Search" id="ancIncentiveApplicationSearch"><i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <iframe name="myIframe" id="iFrameIncentiveApplication" width="100%" style="height: 215px"
                                                    runat="server"></iframe>
                                            </div>
                                            <div class="portletsearch" id="divIncentiveAppSearch">
                                                <div class="form-group">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                District</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="drpInctAppDistrict" runat="server">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="drpInctYear" runat="server">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Unit Type
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="drpInctUnitType" runat="server">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Policy
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="drpPolicy" runat="server">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <input type="button" id="btnIncentiveAppSubmit" class="btn btn-success" onclick="RefreshIncentiveAppIFrame();"
                                                                value="Submit" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <%-- Grievance Details--%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec Grievance-sec">
                                            <h4>Grievance Status (<asp:Label ID="lb15" runat="server" Text=""></asp:Label>)
                                            <a class="pull-right spmgfilter"
                                                data-toggle="tooltip" title="Search" id="GRIEVANCEfilter"><i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                <ul>
                                                    <li>
                                                        <a title="Received" href="#GRVModal" data-toggle="modal" data-target="#GRVModal"
                                                            onclick="ShowGRV('0');" style="color:blue;font-weight:bold;">Received
                                                             <asp:Label ID="lblGapplied" runat="server" Text=""></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a title="Resolved" href="#GRVModal" data-toggle="modal" data-target="#GRVModal"
                                                            onclick="ShowGRV('1');" style="color:green;font-weight:bold;">Resolved
                                                             <asp:Label ID="lblGrsolved" runat="server" Text=""></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a title="Pending" href="#GRVModal" data-toggle="modal" data-target="#GRVModal"
                                                            onclick="ShowGRV('2');" style="color:violet;font-weight:bold;">Pending
                                                             <asp:Label ID="lblGpending" runat="server" Text=""></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a title="Rejected" href="#GRVModal" data-toggle="modal" data-target="#GRVModal"
                                                            onclick="ShowGRV('3');" style="color:red;font-weight:bold;">Rejected
                                                             <asp:Label ID="lblGrejected" runat="server" Text=""></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                                    </div>
                                            </div>

                                            <div class="portletsearch" id="GRIEVANCEsearch">
                                                <div class="form-group">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                District</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlgdist" runat="server">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlgyear" runat="server">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-4">
                                                            <asp:Button ID="btnGSearch" CssClass="btn btn-success" runat="server" Text="Submit"
                                                                OnClick="btnGSearch_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
                <!--Modal Popups -->
                <div id="PpModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Project Proposals Details</h4>
                            </div>
                            <div class="modal-body">
                                <table class="table table-bordered">
                                    <tr>
                                        <th width="40px">Sl#.
                                        </th>
                                        <th>State
                                        </th>
                                        <th>District
                                        </th>
                                        <th>Name of the company
                                        </th>
                                    </tr>
                                    <tr>
                                        <td rowspan="2">1
                                        </td>
                                        <td rowspan="2">Odisha
                                        </td>
                                        <td>Khurda
                                        </td>
                                        <td>Tata Steel
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Khurda
                                        </td>
                                        <td>Coca cola
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2">2
                                        </td>
                                        <td rowspan="2">Odisha
                                        </td>
                                        <td>Khurda
                                        </td>
                                        <td>Tata Steel
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Khurda
                                        </td>
                                        <td>Coca cola
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="DsModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Department Wise Approvals Details</h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myservcIframe" width="100%" style="height: 298px" runat="server"></iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="DsModal1" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Applications past ORTPSA timelines Details</h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myservcIframe1" width="100%" style="height: 298px" runat="server"></iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="investmentModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Year wise investment Details in
                                    <asp:Label ID="lblYearinv" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="Iframe1" width="100%" style="height: 298px" runat="server"></iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                            <%--<div style="width: 885px; height: 500px; overflow: auto;">
                                <table class="table table-bordered" id="lblCapital">
                                </table>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        Close</button>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
                <div id="EmploymentModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Year wise employment Details in
                                    <asp:Label ID="lblemp" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <%--<div style="width: 885px; height: 500px; overflow: auto;">
                                <table class="table table-bordered" id="tblEmployemnt">
                                </table>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        Close</button>
                                </div>
                            </div>--%>
                            <div class="modal-body">
                                <div class="table-responsive">
                                    <iframe name="IframeQueryServicedet" id="Iframe2" width="100%" style="height: 298px"
                                        runat="server"></iframe>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                            <%-- <div class="modal-body">
                               
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>--%>
                        </div>
                    </div>
                </div>
                <div id="ApprovalsModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Department Wise Approvals Details</h4>
                            </div>
                            <div class="modal-body">
                                <table class="table table-bordered">
                                    <tr>
                                        <th width="40px">Sl#.
                                        </th>
                                        <th>Name of the company
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>1
                                        </td>
                                        <td>Xyz
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>2
                                        </td>
                                        <td>abc
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="CIFModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Central Inspection Framework Details</h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myCICGIframe" width="100%" style="height: 298px" runat="server"></iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="CIFModal1" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Central Inspection Framework Details</h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myCICGIframe1" width="100%" style="height: 298px" runat="server"></iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="APAAModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">IDCO POST ALLOTMENT APPLICATIONS Details in
                                    <asp:Label ID="lblDet4" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myAPAAIframe" width="100%" style="height: 298px" runat="server"></iframe>
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
                                <h4 class="modal-title">CSR Details</h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myCSRIframe" width="100%" style="height: 298px" runat="server"></iframe>
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
                                <h4 class="modal-title">STA Details</h4>
                            </div>
                            <div class="modal-body">
                                <asp:GridView ID="grdSPMGDtl" runat="server" CssClass="table table-bordered" AllowPaging="true"
                                    PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                                    CellPadding="4" GridLines="None">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name of the unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("VCH_DEPT_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type of the issues">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIssues" runat="server" Text='<%# Eval("VCH_ISSUE_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Since the receipt of the issue(in Days)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDays" runat="server" Text='<%# Eval("VCH_DAYS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-grid no-print" />
                                </asp:GridView>
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
                                <h4 class="modal-title">Incentive Details in
                                    <asp:Label ID="lblDet6" runat="server" Text=""></asp:Label></h4>
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
                <div id="Pealmodal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Single Window Application Status Details</h4>
                            </div>
                            <div class="modal-body">
                                <div style="width: 870px; height: 400px; overflow: auto;">
                                    <table class="table table-bordered" id="tblPeal">
                                    </table>
                                </div>
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
                                <h4 class="modal-title">Single Window Application Status Details in
                                    <asp:Label ID="lbldet1" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myIframe" width="100%" style="height: 298px" runat="server"></iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <%--Query--%>
                <div id="Div2" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Query Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="table-responsive">
                                    <iframe name="IframeQueryService" id="IframeQuery" width="100%" style="height: 298px"
                                        runat="server"></iframe>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="GRVModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Grievance Details in
                                    <asp:Label ID="lblDet15" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="GRVIframe" id="GRVIframe" width="100%" style="height: 350px" runat="server"></iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!--Modal Popups -->
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LinkButton1" />
        </Triggers>
    </asp:UpdatePanel>
    <script src="../js/highcharts.js" type="text/javascript"></script>
    <script src="../js/exporting.js" type="text/javascript"></script>
    <script src="../js/data.js" type="text/javascript"></script>
    <script src="../js/drilldown.js" type="text/javascript"></script>
    <script>

        var location_detl_hdr = 'Proposal Details';
        var location_detl_body_cnt = 'ProposalPupUp.aspx';
        var frm_hit = 450;
        var location_detl_ftr = '<button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Cancel</button>';
        $('.projectdtls').click(function () {
            openPageModal(location_detl_hdr, location_detl_body_cnt, location_detl_ftr, frm_hit);
        });
        $(document).ready(function (e) {
            $("[id*=ddldept]").change(function () {
                return false;
            })

            //         function ShowSearchpanel() {
            //           
            //            $("[id*=DepartmentServices]").show();
            //        }	

        });

        //         function ShowSearchpanel() {
        //           
        //            $("[id*=DepartmentServices]").show();
        //        }		

        function ShowService1() {
            var Dept = $('#ContentPlaceHolder1_ddldept').val();
            var Servc = $('#ContentPlaceHolder1_ddlService').val();
            //document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimeline.aspx?Dept=0&servc=0";
            document.getElementById('ContentPlaceHolder1_myservcIframe1').src = "ApprovalTimeline.aspx?Dept=" + Dept + "&Servc=" + Servc;
        }

        function ShowService(ServiceStatus) {
            var Dept = $('#ContentPlaceHolder1_ddldept').val();
            var Servc = $('#ContentPlaceHolder1_ddlService').val();
            //document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimeline.aspx?Dept=0&servc=0";
            document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimelineDashboard.aspx?Dept=" + Dept + "&Servc=" + Servc + "&ServiceStatus=" + ServiceStatus;
        }

        function ShowAPAA(APAAStatus) {
            var month = $('#ContentPlaceHolder1_ddlAppaMonth').val();
            var dist = $('#ContentPlaceHolder1_ddlAPAADistrict').val();
            var year = $('#ContentPlaceHolder1_ddlAppaYear').val();
            var deptid = '0';
            document.getElementById('ContentPlaceHolder1_myAPAAIframe').src = "../../APAARequestsGrid.aspx?month=" + month + "&dist=" + dist + "&year=" + year + "&deptid=" + deptid + "&Type=" + 0 + "&APAAStatus=" + APAAStatus;
        }

        function ShowCICG1() {
            var Dept = $('#ContentPlaceHolder1_ddldeptCIF').val();
            var Year = $('#ContentPlaceHolder1_ddlYearCICG').val();
            var month = $('#ContentPlaceHolder1_ddlCICGMonth').val();
            document.getElementById('ContentPlaceHolder1_myCICGIframe1').src = "../../CICGGrid.aspx?Dept=" + Dept + "&month=" + month + "&Year=" + Year;
        }

        function ShowCICG(CICGStatus) {
            var Dept = $('#ContentPlaceHolder1_ddldeptCIF').val();
            var Year = $('#ContentPlaceHolder1_ddlYearCICG').val();
            var month = $('#ContentPlaceHolder1_ddlCICGMonth').val();
            document.getElementById('ContentPlaceHolder1_myCICGIframe').src = "CICGStatusGrid.aspx?Dept=" + Dept + "&month=" + month + "&Year=" + Year + "&CICGStatus=" + CICGStatus;
        }

        //MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
        function setCBL(sender) {
            var cbl = document.getElementById('<%=CheckBoxList1.ClientID %>').getElementsByTagName("input");
            for (i = 0; i < cbl.length; i++) cbl[i].checked = sender.checked;
        }

        function setCBL1(sender) {
            var cbl = document.getElementById('<%=chkYearwise.ClientID %>').getElementsByTagName("input");
            for (i = 0; i < cbl.length; i++) cbl[i].checked = sender.checked;
        }
        //ENDED BY SUROJ KUMAR PRADHAN ON 24-10-2017

        //added by nibedita behera on 28-12-2017 dor Query monitoring system
        function ShowPealQuery(Status) {
            var yeartext = $('#ContentPlaceHolder1_ddlyearquery').val();

            if (Status == "1") {

                $('#hQuery').text("PEAL - Query Raised Details in " + yeartext + "");
            }
            else if (Status == "2") {
                $('#hQuery').text(" PEAL - Query Responded Details in " + yeartext + "");
            }
            else if (Status == "3") {
                $('#hQuery').text(" PEAL - Response Pending Details in " + yeartext + ")");
            }
            else if (Status == "4") {
                $('#hQuery').text(" PEAL - Response not received within Timeline Details  in " + yeartext + "");
            }
            else if (Status == "5") {
                $('#hQuery').text(" PEAL - Average Day Taken to Raise Query Details in " + yeartext + "");
            }
            var e = document.getElementById("ContentPlaceHolder1_ddlyearquery");
            var Year = e.options[e.selectedIndex].value;
            document.getElementById('ContentPlaceHolder1_IframeQuery').src = "PealQueryDetails.aspx?Status=" + Status + "&Year=" + Year + "&District=" + 0;
        }

        function ShowServiceQuery(Status) {
            var yeartext = $('#ContentPlaceHolder1_ddlyearquery').val();

            if (Status == "1") {
                $('#hQuery').text(" Services - Query Raised Details in " + yeartext + "");
            }
            else if (Status == "2") {
                $('#hQuery').text(" Services - Query Responded Details in " + yeartext + "");
            }
            else if (Status == "3") {
                $('#hQuery').text(" Services - Response Pending Details in " + yeartext + "");
            }
            else if (Status == "4") {
                $('#hQuery').text(" Services - Response not received within Timeline Details in " + yeartext + "");
            }
            else if (Status == "5") {
                $('#hQuery').text(" Services - Average Day Taken to Raise Query Details in " + yeartext + "");
            }
            var e = document.getElementById("ContentPlaceHolder1_ddlyearquery");
            var Year = e.options[e.selectedIndex].value;
            document.getElementById('ContentPlaceHolder1_IframeQuery').src = "ServiceQueryDetails.aspx?Status=" + Status + "&Year=" + Year + "&District=" + 0 + "&deptId=" + 0;
        }

        function ShowIncentiveQuery(Status) {
            var yeartext = $('#ContentPlaceHolder1_ddlyearquery').val();

            if (Status == "1") {
                $('#hQuery').text(" Incentive - Query Raised Details in " + yeartext + "");
            }
            else if (Status == "2") {
                $('#hQuery').text(" Incentive - Query Responded Details in " + yeartext + "");
            }
            else if (Status == "3") {
                $('#hQuery').text(" Incentive - Response Pending Details in " + yeartext + "");
            }
            else if (Status == "4") {
                $('#hQuery').text(" Incentive - Response not received within Timeline Details in " + yeartext + "");
            }
            else if (Status == "5") {
                $('#hQuery').text(" Incentive - Average Day Taken to Raise Query Details in " + yeartext + "");
            }
            var e = document.getElementById("ContentPlaceHolder1_ddlyearquery");
            var Year = e.options[e.selectedIndex].value;
            document.getElementById('ContentPlaceHolder1_IframeQuery').src = "IncentiveQueryDetails.aspx?Status=" + Status + "&Year=" + Year + "&District=" + 0;
        }

        function ShowITpeal(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlPealQuarter");
            var PealType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPEALDistrict");
            var Pealdistrict = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPealYear");
            var Pealyear = e.options[e.selectedIndex].value;
            var PealSectorUser = 1;
            document.getElementById('ContentPlaceHolder1_myIframe').src = "FRAMEPEALSECTOR.aspx?Status=" + Status + "&PealType=" + 0 + "&Pealdistrict=" + Pealdistrict + "&PealYear=" + Pealyear + "&PealQuarter=" + 0 + "&PealSectorUser=" + PealSectorUser + "&PealUserStatus=" + 2;
        }

        function Showtourismpeal(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlPealQuarter");
            var PealType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPEALDistrict");
            var Pealdistrict = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPealYear");
            var Pealyear = e.options[e.selectedIndex].value;
            var PealSectorUser = 2;
            document.getElementById('ContentPlaceHolder1_myIframe').src = "FRAMEPEALSECTOR.aspx?Status=" + Status + "&PealType=" + 0 + "&Pealdistrict=" + Pealdistrict + "&PealYear=" + Pealyear + "&PealQuarter=" + 0 + "&PealSectorUser=" + PealSectorUser + "&PealUserStatus=" + 2;
        }

        function ShowGRV(Status) {
            var Year = $("#ContentPlaceHolder1_ddlgyear option:selected").text();
            var dist = $('#ContentPlaceHolder1_ddlgdist option:selected').val();
            document.getElementById('ContentPlaceHolder1_GRVIframe').src = "GrievanceGridStatus.aspx?Year=" + Year + "&dist=" + dist + "&Status=" + Status;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
