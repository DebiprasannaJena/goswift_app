<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="EnergyUtilityDashboard.aspx.cs" Inherits="Portal_Dashboard_EnergyUtilityDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function pageLoad() {
            var yr4 = $('#ContentPlaceHolder1_ddlspmgyear option:selected').text();
            $('#ContentPlaceHolder1_lbl4').html(yr4);
            $('#ContentPlaceHolder1_lblDet2').html(yr4);
            $('#btnspmg').click(function () {
                yr4 = $('#ContentPlaceHolder1_ddlspmgyear option:selected').text();
                $('#ContentPlaceHolder1_lbl4').html(yr4);
                $('#ContentPlaceHolder1_lblDet2').html(yr4);
            });
            var yr10 = $('#ContentPlaceHolder1_ddlyearquery option:selected').text();
            $('#ContentPlaceHolder1_lbl10').html(yr10);
            $('#ContentPlaceHolder1_lbldet12').html(yr10);
            $('#btnQuery').click(function () {
                yr10 = $('#ContentPlaceHolder1_ddlyearquery option:selected').text();
                $('#ContentPlaceHolder1_lbl10').html(yr10);
                $('#ContentPlaceHolder1_lbldet12').html(yr10);
            });
             var yr100 = $('#ContentPlaceHolder1_ddlsyear option:selected').text();
            $('#ContentPlaceHolder1_lbl20').html(yr100);
            $('#ContentPlaceHolder1_lbldet122').html(yr100);
            $('#ContentPlaceHolder1_lbldet1222').html(yr100);
            $('#btnQuery').click(function () {
                yr100 = $('#ContentPlaceHolder1_ddlsyear option:selected').text();
                $('#ContentPlaceHolder1_lbl20').html(yr100);
                $('#ContentPlaceHolder1_lbldet122').html(yr100);
                 $('#ContentPlaceHolder1_lbldet1222').html(yr100);
            });
            var yr4 = $('#ContentPlaceHolder1_ddlspmgyear option:selected').text();
            $('#ContentPlaceHolder1_lbl4').html(yr4);
            $('#ContentPlaceHolder1_lblDet2').html(yr4);
            $('#btnspmg').click(function () {
                yr4 = $('#ContentPlaceHolder1_ddlspmgyear option:selected').text();
                $('#ContentPlaceHolder1_lbl4').html(yr4);
                $('#ContentPlaceHolder1_lblDet2').html(yr4);
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

            //            $('#filterIncentivesearh').click(function () {
            //                $('#Incentivesearh').slideToggle();

            //            });
            $('#linkDepartmentServices').click(function () {
                $('#DepartmentServices').slideToggle();

            });

            //            $('#linkAPAAStatus').click(function () {
            //                $('#APAAStatus').slideToggle();

            //            });

            //            $('#linkCSRActivities').click(function () {
            //                $('#CSRActivities').slideToggle();

            //            });

            //            $('#linkCIF').click(function () {
            //                $('#CIF').slideToggle();

            //            });

            //            $('#linkUnitREg').click(function () {
            //                $('#UnitREg').slideToggle();
            //            });

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

            $('#Queryfilter').click(function () {
                $('#Querysearch').slideToggle();
            });

            $('#pealfilter').click(function () {
                $('#pealsearch').slideToggle();
            });

            $('#AMSfilter').click(function () {
                $('#AMSsearch').slideToggle();
            });


        });
        function ShowSearchpanel() {
            //dvservce

            $('[id*=DepartmentServices]').css("display", "block");
            return false;
        }
        function ShowSPMG(SPMGStatus) {

            var Year = $('#ContentPlaceHolder1_ddlspmgyear').val();
            document.getElementById('ContentPlaceHolder1_SPMGIframe').src = "SPMGDeptwiseStatusdtls.aspx?Year=" + Year + "&SPMGStatus=" + SPMGStatus;
        }
        //        function Show(Status) {
        //            document.getElementById('ContentPlaceHolder1_myIframe').src = "FramePealStatus.aspx?Status=" + Status;
        //        }
        function openpoupwin(ctrl) {

            $.ajax({
                type: "POST",
                url: "DepartmentDashboard.aspx/PealDetailsBind",

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

        function openpoupwinEmploeement(ctrl) {

            $.ajax({
                type: "POST",
                url: "DepartmentDashboard.aspx/EmployeementPealDetailsBind",

                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    tempHTML = "";
                    $("#tblEmployemnt").html('')
                    tempHTML += '<thead><tr class="persist-header">'
                    tempHTML += '<th rowspan="1" valign="middle" width="20px" bgcolor="#e4e4e4">Sl#</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Company Name</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Employees</th>'
                    tempHTML += '</tr></thead><tbody>'
                    $('#tblEmployemnt').append(tempHTML);
                    tempHTML = "";
                    var serialNo = 0;
                    $.each(r.d, function (index, value) {
                        serialNo++;
                        if (r.d.length > 0) {
                            tempHTML += '<tr>';
                            tempHTML += '<td align="left">' + serialNo + '</td>';
                            tempHTML += '<td >' + value.strComapnyName + '</td>';
                            tempHTML += '<td >' + value.intEmployeement + '</td>';
                            tempHTML += '</tr>';
                        }
                    });
                    if (r.d.length == 0) {
                        $("#tblEmployemnt").html('')
                        tempHTML += '<tr><td>No Records Found...</td></tr>';
                        $("#tblEmployemnt").append(tempHTML);
                    }
                    else {
                        $("#tblEmployemnt").append(tempHTML); //APPEND THE DYNAMIC VALUE IN ROW
                        $("#tblEmployemnt").append("</tbody>");
                    }
                },
                error: function (response) {

                    var msg = jQuery.parseJSON(response.responseText);
                    alert("Message: " + msg.Message + "<br /> StackTrace:" + msg.StackTrace + "<br /> ExceptionType:" + msg.ExceptionType);
                }
            });
        }

        function openpoupwincapiatal(ctrl) {

            $.ajax({
                type: "POST",
                url: "DepartmentDashboard.aspx/EmployeementCapitalPealDetailsBind",

                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    tempHTML = "";
                    $("#lblCapital").html('')
                    tempHTML += '<thead><tr class="persist-header">'
                    tempHTML += '<th rowspan="1" valign="middle" width="20px" bgcolor="#e4e4e4">Sl#</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Company Name</th>'
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
                            tempHTML += '<td >' + value.intEmployeement1 + '</td>';
                            tempHTML += '</tr>';
                        }
                    });
                    if (r.d.length == 0) {
                        $("#lblCapital").html('')
                        tempHTML += '<tr><td>No Records Found...</td></tr>';
                        $("#lblCapital").append(tempHTML);
                    }
                    else {
                        $("#lblCapital").append(tempHTML); //APPEND THE DYNAMIC VALUE IN ROW
                        $("#lblCapital").append("</tbody>");
                    }
                },
                error: function (response) {

                    var msg = jQuery.parseJSON(response.responseText);
                    alert("Message: " + msg.Message + "<br /> StackTrace:" + msg.StackTrace + "<br /> ExceptionType:" + msg.ExceptionType);
                }
            });
        }
       

       
    </script>
    <style>
        .portletcontainer.cmdashbordportlet {
            min-height: 242px;
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

                            }); $('#linkAPAAStatus').click(function () {
                                $('#APAAStatus').slideToggle();

                            }); $('#linkCSRActivities').click(function () {
                                $('#CSRActivities').slideToggle();

                            }); $('#linkCIF').click(function () {
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
                            }); $('#CifFilter').click(function () {
                                $('#CifSearch').slideToggle();
                            });
                            $('#APAAfilter').click(function () {
                                $('#APAAsearch').slideToggle();
                            });
                            $('#pealfilter').click(function () {
                                $('#pealsearch').slideToggle();
                            }); $('#AMSfilter').click(function () {
                                $('#AMSsearch').slideToggle();
                            });
                            $('#Queryfilter').click(function () {
                                $('#Querysearch').slideToggle();
                            });
                            $('#ancIncentiveApplicationSearch').click(function () {
                                $('#divIncentiveAppSearch').slideToggle();
                            });
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
                        <h1>Energy Utility DashBoard</h1>
                        <ul class="breadcrumb">
                            <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                            <li><a>Energy Utility DashBoard</a></li>
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
                                    <div class="col-sm-6 col-md-4">
                                        <div class="masterportletsec">
                                            <h4>Pending approval
                                            </h4>
                                            <p>
                                                Applications past ORTPSA timelines<span id="spanapprove" runat="server"></span>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  col-md-4" style="display: none;">
                                        <div class="masterportletsec">
                                            <h4>State Project Monitoring Group
                                            </h4>
                                            <p>
                                                Issues pending<span id="spSpmgpnd" runat="server"></span>
                                            </p>
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

                                    <div class="col-sm-6  col-md-6">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Department/ Agency Wise Approvals(<asp:Label ID="lbl20" runat="server" Text=""></asp:Label>)<a class="pull-right spmgfilter" data-toggle="tooltip"
                                                title="Search" id="linkDepartmentServices"><i class="fa fa-search"></i></a></h4>
                                            <div class="portletcontainer cmdashbordportlet" id="dvservce">
                                                <div class="scroll-prtlet">
                                                <ul>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" title="Application Received"
                                                        onclick="Show('R');" style="color:blue;font-weight:bold;">Application Received<span id="hdApplied" runat="server"><asp:Literal
                                                            ID="ltlServiceApplied" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" title="Total Approvals Provided"
                                                        onclick="Show('A');" style="color:green;font-weight:bold;">Total Approvals Provided<span id="hdApprove" runat="server"><asp:Literal
                                                            ID="ltlApprove" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="Show('PK');"
                                                        title="Approval Pending" style="color:dodgerblue;font-weight:bold;">Query In Progress<span id="hdnqueryRaised" runat="server"><asp:Literal
                                                            ID="Literal1" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" title="Approval Pending"
                                                        onclick="Show('P');" style="color:violet;font-weight:bold;">Approval Pending<span id="hdPending" runat="server"><asp:Literal
                                                            ID="ltlServicepending" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" title="Total Rejected"
                                                        onclick="Show('RJ');" style="color:red;font-weight:bold;">Total Rejected <span id="hdReject" runat="server">
                                                            <asp:Literal ID="ltlServiceReject" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal1" data-toggle="modal" data-target="#DsModal1" onclick="Show1();"
                                                        title="Applications past ORTPSA timelines" style="color:orange;font-weight:bold;">Applications past ORTPSA timelines<span
                                                            class="bgdisbursed" style="color:orange;font-weight:bold;" id="hdExceed" runat="server"></span></a></li>
                                                </ul>
                                                    </div>
                                            </div>
                                            <div class="portletsearch" id="DepartmentServices">
                                                <div class="form-group" style="display: none">
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
                                                            Year</label>
                                                        <div class="col-sm-8" style="display: none;">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlService" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="ddlsyear" runat="server" CssClass="form-control">
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

                                    <%--  SPMG--%>
                                    <div class="col-sm-6  col-md-4" style="display: none;">
                                        <div class="investordashboard-sec incentive-sec">
                                            <h4>STATE PROJECT MONITORING GROUP (<asp:Label ID="lbl4" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="linkSpmg"><i
                                                    class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <ul>
                                                    <li><a title="Issues Received" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal"
                                                        onclick="ShowSPMG('received');">Issues Received<span id="spmgraised" runat="server"></span></a></li>
                                                    <li><a title="Issues Resolved" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal"
                                                        onclick="ShowSPMG('resolved');">Issues Resolved<span id="spmgresolved" runat="server"></span></a></li>
                                                    <li><a title="Issues Pending" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal"
                                                        onclick="ShowSPMG('pending');">Issues Pending<span id="spmgpending" runat="server"></span></a></li>
                                                    <li><a title="Issues Pending > 30 days" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal"
                                                        onclick="ShowSPMG('delayed');">Issues Pending > 30 days <span id="spmg30pending"
                                                            runat="server"></span></a></li>
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
                                                                <asp:Button ID="btnspmg" CssClass="btn btn-success" runat="server" Text="Submit"
                                                                    OnClick="btnspmg_Click"></asp:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6 col-md-4">
                                        <div class="investordashboard-sec incentive-sec">
                                            <h4>Query (<asp:Label ID="lbl10" runat="server" Text=""></asp:Label>)<a class="pull-right spmgfilter" id="Queryfilter"><i class="fa fa-search"></i>
                                            </a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                <ul>
                                                    <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal" title="Queries Raised" onclick="ShowQuery(1);" style="color:blue;font-weight:bold;" >Queries Raised<span id="spRaised" runat="server"></span></a></li>
                                                    <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal" title="Queries Responded" onclick="ShowQuery(2);" style="color:green;font-weight:bold;">Queries Responded<span id="spRevert" runat="server"></span></a></li>
                                                    <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal" title="Response Pending" onclick="ShowQuery(3);" style="color:violet;font-weight:bold;">Response Pending<span id="spPending" runat="server"></span></a></li>
                                                    <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal" onclick="ShowQuery(4);"
                                                        title="Response not received within Timeline" style="color:dodgerblue;font-weight:bold;">Response not received within Timeline<span class="bgdisbursed"
                                                            id="spResponseTimeline" runat="server"></span></a></li>
                                                    <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal" title="Queries Raised" onclick="ShowQuery(5);" style="color:orange;font-weight:bold;">Average Day Taken to Raise Query<span id="spAvgTime" runat="server"></span></a></li>
                                                </ul>
                                                 </div>
                                                <div class="portletsearch" id="Querysearch" style="top: -0px;">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-2">
                                                                Year</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlyearquery" runat="server">
                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:Button ID="Button10" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="Button10_Click"></asp:Button>
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
                    </div>
                </section>
                <!-- /.content -->
                <!--Modal Popups -->
                <div id="DsModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Department Wise Approvals Details in 
                                    <asp:Label ID="lbldet122" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myservcIframe" width="100%" style="height: 298px" runat="server"></iframe>
                                <asp:HiddenField ID="hdnsrvc" runat="server" />
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
                                <h4 class="modal-title">Applications past ORTPSA timelines Details in 
                                    <asp:Label ID="lbldet1222" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myservcIframe1" width="100%" style="height: 298px" runat="server"></iframe>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="QueryModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Query Details in
                                    <asp:Label ID="lbldet12" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="IframeQuery" width="100%" style="height: 298px" runat="server"></iframe>
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
                        <div class="modal-content modal-primary">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">State Project Monitoring Group Details in
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="SPMGIframe" id="SPMGIframe" width="100%" style="height: 298px" runat="server"></iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
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



        });

        function Show1() {

            var Dept = $('#ContentPlaceHolder1_hdnsrvc').val();
            var Servc = $('#ContentPlaceHolder1_ddlService').val();
            var FIYear = $('#ContentPlaceHolder1_ddlsyear').val();
            //document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimeline.aspx?Dept=0&servc=0";
            document.getElementById('ContentPlaceHolder1_myservcIframe1').src = "ApprovalTimeline.aspx?Dept=" + Dept + "&FIYear=" + FIYear + "&Servc=" + Servc;
        }
        function Show(ServiceStatus) {
            var Dept = $('#ContentPlaceHolder1_hdnsrvc').val();
            var Servc = $('#ContentPlaceHolder1_ddlService').val();
            var FIYear = $('#ContentPlaceHolder1_ddlsyear').val();
            //document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimeline.aspx?Dept=0&servc=0";
            document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimelineDashboard.aspx?Dept=" + Dept + "&FIYear=" + FIYear + "&Servc=" + Servc + "&ServiceStatus=" + ServiceStatus;
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
        function ShowQuery(Status) {
            debugger;
            var e = document.getElementById("ContentPlaceHolder1_ddlyearquery");
            var Year = e.options[e.selectedIndex].value;
            var District = "0";
            var deptId = $('#ContentPlaceHolder1_hdnsrvc').val();
            // document.getElementById('ContentPlaceHolder1_IframeQuery').src = "ServiceQueryDetails.aspx?Status=" + Status + "&Year=" + Year + "&District=" + District;
            document.getElementById('ContentPlaceHolder1_IframeQuery').src = "ServiceQueryDetails.aspx?Status=" + Status + "&Year=" + Year + "&District=" + 0 + "&deptId=" + deptId;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
