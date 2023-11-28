<%--'*******************************************************************************************************************
' File Name         : DICDashboard.aspx
' Description       : Show the Portal related to DIC Login Dashboard
' Created by        : Pranay Kumar
' Created On        : 04-OCT-2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="DICDashboard.aspx.cs" Inherits="Portal_Dashboard_DICDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function pageLoad() {
            var yr1 = $('#ContentPlaceHolder1_ddlPealYear').val();
            $('#ContentPlaceHolder1_lbl1').html(yr1);
            $('#ContentPlaceHolder1_lbldet1').html(yr1);
            $('#btnPealsubmit').click(function () {
                yr1 = $('#ContentPlaceHolder1_ddlPealYear').val();
                $('#ContentPlaceHolder1_lbl1').html(yr1);
                $('#ContentPlaceHolder1_lbldet1').html(yr1);
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

            var yr10 = $('#ContentPlaceHolder1_ddlyearquery option:selected').text();
            $('#ContentPlaceHolder1_lbl10').html(yr10);
            $('#ContentPlaceHolder1_lbldet12').html(yr10);
            $('#btnQuery').click(function () {
                yr10 = $('#ContentPlaceHolder1_ddlyearquery option:selected').text();
                $('#ContentPlaceHolder1_lbl10').html(yr10);
                $('#ContentPlaceHolder1_lbldet12').html(yr10);
            });

            var yr9 = $('#ContentPlaceHolder1_ddlLandFinYear option:selected').text();
            $('#ContentPlaceHolder1_lbl9').html(yr9);
            $('#ContentPlaceHolder1_lbldet10').html(yr9);
            $('#btnLandSubmit').click(function () {
                yr9 = $('#ContentPlaceHolder1_ddlLandFinYear option:selected').text();
                $('#ContentPlaceHolder1_lbl9').html(yr9);
                $('#ContentPlaceHolder1_lbldet10').html(yr9);
            });

            var yr94 = $('#ContentPlaceHolder1_ddlserviceyear option:selected').text();
            $('#ContentPlaceHolder1_lbl44').html(yr94);
            $('#ContentPlaceHolder1_lbl44det').html(yr94);
            $('#btnStatusOfApproval').click(function () {
                yr94 = $('#ContentPlaceHolder1_ddlserviceyear option:selected').text();
                $('#ContentPlaceHolder1_lbl44').html(yr94);
                $('#ContentPlaceHolder1_lbl44det').html(yr94);
            });

            var yr4 = $('#ContentPlaceHolder1_ddlspmgyear option:selected').text();
            $('#ContentPlaceHolder1_lbl4').html(yr4);
            $('#ContentPlaceHolder1_lblDet2').html(yr4);
            $('#btnspmg').click(function () {
                yr4 = $('#ContentPlaceHolder1_ddlspmgyear option:selected').text();
                $('#ContentPlaceHolder1_lbl4').html(yr4);
                $('#ContentPlaceHolder1_lblDet2').html(yr4);
            });

            var yr12 = $('#ContentPlaceHolder1_ddlgyear option:selected').text();
            $('#ContentPlaceHolder1_lb15').html(yr12);
            $('#ContentPlaceHolder1_lblDet15').html(yr12);
            $('#btnGSearch').click(function () {
                yr12 = $('#ContentPlaceHolder1_ddlgyear option:selected').text();
                $('#ContentPlaceHolder1_lb15').html(yr12);
                $('#ContentPlaceHolder1_lblDet15').html(yr12);
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

            $('#INCENTIVEfilter').click(function () {
                $('#INCENTIVEsearch').slideToggle();
            });

            $('#pealfilter').click(function () {
                $('#pealsearch').slideToggle();
            });

            $('#AMSfilter').click(function () {
                $('#AMSsearch').slideToggle();
            });

            $('#linkSpmg').click(function () {
                $('#SpmgContent').slideToggle();
            });

            //added by ritika lath for incentive
            $('#ancIncentiveApplicationSearch').click(function () {
                $('#divIncentiveAppSearch').slideToggle();
            });

            $('#Queryfilter').click(function () {
                $('#Querysearch').slideToggle();
            });

            $('#landFilter').click(function () {
                $('#landSearch').slideToggle();
            });

            $('#GRIEVANCEfilter').click(function () {
                $('#GRIEVANCEsearch').slideToggle();
            });

            ShowIncentiveApplicationDetails();
            //added by ritika lath for incentive
        });

        function Show(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlPealQuarter");
            var PealType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPEALDistrict");
            var Pealdistrict = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPealYear");
            var Pealyear = e.options[e.selectedIndex].value;
            //           var Pealdistrict = '<%= Session["Pealuserid"] %>';
            document.getElementById('ContentPlaceHolder1_myIframe').src = "FramePealDaysStatus.aspx?Status=" + Status + "&PealType=" + 0 + "&Pealdistrict=" + Pealdistrict + "&PealYear=" + Pealyear + "&PealQuarter=" + 0 + "&PealUserStatus=" + 1;
        }

        //ADDED BY SUROJ KUMAR PRADHAN FOR INCENTIVE
        function ShowIncentive(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlIncentive");
            var IncentiveType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlIncentiveYear");
            var IncentiveYear = e.options[e.selectedIndex].value;
            var Userid = '<%= Session["Userid"] %>';
            var Pealdistrict = '<%= Session["Pealuserid"] %>';
            document.getElementById('ContentPlaceHolder1_IncentiveIframe').src = "IncentiveStatus.aspx?Action=" + Status + "&IncentiveType=" + IncentiveType + "&IncentiveYear=" + IncentiveYear + "&Userid=" + 0 + "&Distid=" + Pealdistrict;
        }
        //ENDED BY SUROJ
        function openpoupwin(ctrl) {

            $.ajax({
                type: "POST",
                url: "CmDashboard.aspx/PealDetailsBind",
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
                url: "CmDashboard.aspx/EmployeementPealDetailsBind",

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
                url: "CmDashboard.aspx/EmployeementCapitalPealDetailsBind",

                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {

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
            document.getElementById('ContentPlaceHolder1_iFrameIncentiveApplication').src = "../Incentive/IncentiveReport.aspx?unitType=" + unitType + "&IncentiveYear=" + IncentiveYear + "&Distid=" + Pealdistrict + "&policy=" + policy;
        }

        function RefreshIncentiveAppIFrame() {
            var ifr = document.getElementById('ContentPlaceHolder1_iFrameIncentiveApplication');
            ShowIncentiveApplicationDetails();
            $('#divIncentiveAppSearch').slideToggle();
        }
        //added by ritika lath for incentive
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

                            $('#INCENTIVEfilter').click(function () {
                                $('#INCENTIVEsearch').slideToggle();
                            });

                            $('#pealfilter').click(function () {
                                $('#pealsearch').slideToggle();
                            });

                            $('#AMSfilter').click(function () {
                                $('#AMSsearch').slideToggle();
                            });

                            $('#ancIncentiveApplicationSearch').click(function () {
                                $('#divIncentiveAppSearch').slideToggle();
                            });

                            $('#Queryfilter').click(function () {
                                $('#Querysearch').slideToggle();
                            });

                            $('#landFilter').click(function () {
                                $('#landSearch').slideToggle();
                            });

                            $('#linkSpmg').click(function () {
                                $('#SpmgContent').slideToggle();
                            });

                            $('#GRIEVANCEfilter').click(function () {
                                $('#GRIEVANCEsearch').slideToggle();
                            });
                        }
                    });
                };

                function ShowSearchpanel() {
                    //dvservce
                    $('[id*=DepartmentServices]').css("display", "block");
                    return false;
                }
            </script>
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <div class="header-icon">
                        <i class="fa fa-tachometer"></i>
                    </div>
                    <div class="header-title">
                        <h1 runat="server" id="h1Dashuser"></h1>
                        <%-- <h1>DIC DashBoard</h1>--%>
                        <ul class="breadcrumb">
                            <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                            <li><a id="adashuser" runat="server"></a></li>
                        </ul>
                        <%--  <a>DIC DashBoard</a></li><li><a></a></li></ul>--%>
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
                                <h4>Master Tracker <small>For
                                    <asp:DropDownList CssClass="masterdropdown" ID="ddlFinacialYear" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFinacialYear_SelectedIndexChanged" runat="server">
                                        <%--<asp:ListItem>2017-18</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </small>
                                </h4>
                                <div class="row">
                                    <div class="col-sm-6 col-md-4">
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
                                            <h4>Pending Approvals
                                            </h4>
                                            <p>
                                                Applications past ORTPSA timelines<span id="spanapprove" runat="server"></span>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="masterportletsec">
                                            <h4>Incentive Details</h4>
                                            <p>
                                                Pending incentives <span>
                                                    <asp:Literal ID="lblIncpendingdtls" runat="server"></asp:Literal></span>
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
                                    <div class="col-sm-6  col-md-4">
                                        <div class="masterportletsec">
                                            <h4>State Project Monitoring Group
                                            </h4>
                                            <p>
                                                Issues pending<span id="spSpmgpnd" runat="server"></span>
                                            </p>
                                        </div>
                                    </div>
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
                                    <%--PEAL form details--%>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Single Window Application Status (<asp:Label ID="lbl1" runat="server" Text="reerre"></asp:Label>)
                                            <a class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="pealfilter">
                                                <i class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                    <ul>
                                                        <li><a href="#Div1" data-toggle="modal" data-target="#Div1" title="Applied" onclick="Show(1);" style="color:blue;font-weight:bold;">Applied<span>
                                                            <asp:Label ID="lblPealApplied" runat="server" Text=""></asp:Label></span></a>
                                                        </li>
                                                        <li><a href="#Div1" data-toggle="modal" data-target="#Div1" title="Approved" onclick="Show(2);" style="color:green;font-weight:bold;">Approved<span>
                                                            <asp:Label ID="lblPealApproved" runat="server" Text=""></asp:Label></span></a>
                                                        </li>
                                                        <li><a href="#Div1" data-toggle="modal" data-target="#Div1" title="Rejected" onclick="Show(3);" style="color:red;font-weight:bold;">Rejected<span>
                                                            <asp:Label ID="lblPealRejected" runat="server" Text=""></asp:Label></span></a>
                                                        </li>
                                                        <li><a href="#Div1" data-toggle="modal" data-target="#Div1" title="Deferred" onclick="Show(7);" style="color:violet;font-weight:bold;">Deferred<span>
                                                            <asp:Label ID="lblPealDeferred" runat="server" Text=""></asp:Label></span></a>
                                                        </li>
                                                        <li><a href="#Div1" data-toggle="modal" data-target="#Div1" title="Query In Progress"
                                                            onclick="Show(8);" style="color:dodgerblue;font-weight:bold;">Query In Progress<span>
                                                                <asp:Label ID="lblQueryInprogress" runat="server" Text=""></asp:Label></span></a>
                                                        </li>
                                                        <li><a href="#Div1" data-toggle="modal" data-target="#Div1" title="Under Evalution"
                                                            onclick="Show(6);" style="color:black;font-weight:bold;">Under Evalution<span>
                                                                <asp:Label ID="lblPealUnderEvalution" runat="server" Text=""></asp:Label></span></a>
                                                        </li>
                                                        <li><a href="#Div1" data-toggle="modal" data-target="#Div1" title=" Application pending since last 30 days"
                                                            onclick="Show(9);" style="color:orange;font-weight:bold;">Application pending since last 30 days<span>
                                                                <asp:Label ID="Lbl_Peal_ORTPSA" runat="server" Text=""></asp:Label></span></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="portletsearch" id="pealsearch">
                                                <div class="form-group">
                                                    <div class="row" style="display: none">
                                                        <label class="col-sm-4">
                                                            Quarterwise</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlPealQuarter" runat="server">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Q1" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Q2" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Q3" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Q4" Value="4"></asp:ListItem>
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
                                                                <%--<asp:ListItem Text="--Select--" Value="0"></asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="display: none">
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
                                    <%-- Approval status--%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Department Wise Approvals (<asp:Label ID="lbl44" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="linkDepartmentServices"><i
                                                    class="fa fa-search"></i></a></h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                <ul>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('R');"
                                                        title="Application Received" style="color:blue;font-weight:bold;">Application Received<span id="hdApplied" runat="server"><asp:Literal
                                                            ID="ltlServiceApplied" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('A');"
                                                        title="Total Approvals Provided" style="color:green;font-weight:bold;">Total Approvals Provided<span id="hdApprove" runat="server"><asp:Literal
                                                            ID="ltlApprove" runat="server"></asp:Literal></span></a></li>
                                                    <%--<li>Approval Pending<span  id="hdPending" runat="server"><asp:Literal ID="ltlServicepending" runat="server"></asp:Literal></span></li>--%>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('PK');"
                                                        title="Approval Pending" style="color:dodgerblue;font-weight:bold;">Query In Progress<span id="hdnqueryRaised" runat="server"><asp:Literal
                                                            ID="Literal1" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('RN');"
                                                        title="Approval Pending" style="color:violet;font-weight:bold;">Approval Pending<span id="hdPending" runat="server"></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('P');"
                                                        title="Approval Pending" style="color:red;font-weight:bold;">Rejected<span id="hdReject" runat="server"></span></a></li>
                                                    
                                                </ul>
                                                    </div>
                                            </div>
                                            <div class="portletsearch" id="DepartmentServices">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Year</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlserviceyear" runat="server">
                                                                <%--<asp:ListItem>--Select--</asp:ListItem>--%>
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
                                                            <asp:DropDownList CssClass="form-control" ID="ddlServcMonth" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="display: none">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Districts</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlServcDistrict" runat="server">
                                                                <asp:ListItem>--Select--</asp:ListItem>
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
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec">
                                            <h4>STATE PROJECT MONITORING GROUP (<asp:Label ID="lbl4" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="linkSpmg"><i
                                                    class="fa fa-search"></i></a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                <ul>
                                                    <li><a title="Issues Received" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal"
                                                        onclick="ShowSPMG('received');" style="color:blue;font-weight:bold;">Issues Received<span id="spmgraised" runat="server"></span></a></li>
                                                    <li><a title="Issues Resolved" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal"
                                                        onclick="ShowSPMG('resolved');" style="color:green;font-weight:bold;">Issues Resolved<span id="spmgresolved" runat="server"></span></a></li>
                                                    <li><a title="Issues Pending" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal"
                                                        onclick="ShowSPMG('pending');" style="color:violet;font-weight:bold;">Issues Pending<span id="spmgpending" runat="server"></span></a></li>
                                                    <li><a title="Issues Pending > 30 days" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal"
                                                        onclick="ShowSPMG('delayed');" style="color:orange;font-weight:bold;">Issues Pending > 30 days <span id="spmg30pending"
                                                            runat="server"></span></a></li>
                                                </ul>
                                                    </div>
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

                                    <%--Incentive status--%>
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
                                                        onclick="ShowIncentive('F');" style="color:red;font-weight:bold;" >Rejected<span><asp:Label ID="lblIncrejected" runat="server"
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
                                                                Quarterwise</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlIncentive" runat="server">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    <%--<asp:ListItem Text=">50 Cr." Value="1"></asp:ListItem>
                <asp:ListItem Text="<50 Cr." Value="2"></asp:ListItem>--%>
                                                                    <asp:ListItem Text="Q1" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Q2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Q3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="Q4" Value="4"></asp:ListItem>
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
                                                                <%--<asp:ListItem Text="--Select--" Value="0"></asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="display: none">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            District</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlIncentiveDistrict" runat="server">
                                                                <%-- <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>--%>
                                                                <%--<asp:ListItem Text=">50 Cr." Value="1"></asp:ListItem>
                <asp:ListItem Text="<50 Cr." Value="2"></asp:ListItem>--%>
                                                                <asp:ListItem Text="Q1" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Q2" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Q3" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Q4" Value="4"></asp:ListItem>
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
                                    <%-- IDCO POST ALLOTMENT APPLICATIONS Status--%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>IDCO POST ALLOTMENT APPLICATIONS (<asp:Label ID="lbl6" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right spmgfilter" id="APAAfilter" data-toggle="tooltip" title="Search"><i
                                                    class="fa fa-search"></i></a></h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                <ul>
                                                    <li><a title="Received" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('A');" style="color:blue;font-weight:bold;">Change requests received<span id="spchngrqstApplied" runat="server"></span></a></li>
                                                    <li><a title="Processed" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('B');" style="color:green;font-weight:bold;">Change requests processed<span id="spchngreqdispose" runat="server"></span></a></li>
                                                    <li><a title="Disposed" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('D');" style="color:dodgerblue;font-weight:bold;">Change requests pending at IDCO<span id="spchngreqPendAtIDCO"
                                                            runat="server"></span></a></li>
                                                    <li><a title="Disposed" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('C');" style="color:violet;font-weight:bold;">Change requests pending at Industry <span id="spnPendingatUnit"
                                                            runat="server"></span></a></li>
                                                    <li><a title="Approvals" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('E');" style="color:orange;font-weight:bold;">Change requests pending > 30 days<span class="bgdisbursed"
                                                            id="spchngReqCrossThirty" style="color:orange;font-weight:bold;" runat="server"></span></a></li>
                                                </ul>
                                                    </div>
                                            </div>
                                            <div class="portletsearch" id="APAAsearch">
                                                <div class="form-group" style="display: none">
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
                                                               <%-- <asp:ListItem Value="0">--Select--</asp:ListItem>--%>
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
                                    <%--QUERY--%>
                                    <div class="col-sm-6 col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Query Monitoring (<asp:Label ID="lbl10" runat="server" Text=""></asp:Label>)<a class="pull-right spmgfilter"
                                                id="Queryfilter"><i class="fa fa-search"></i></a></h4>
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
                                                            <td style="color:blue;font-weight:bold;">Queries Raised
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
                                                            <td style="color:green;font-weight:bold;">Queries Responded
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
                                                            <td style="color:violet;font-weight:bold;">Response Pending
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
                                                            <td style="color:dodgerblue;font-weight:bold;">Response not received within Timeline
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
                                                            <td style="color:orange;font-weight:bold;">Average Day Taken to Raise Query
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
                                    <%--LAND ALLOTMENT DETAILS--%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>Land Allotment Details (<asp:Label ID="lbl9" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right landfilter" data-toggle="tooltip" title="Search" id="landFilter"><i
                                                    class="fa fa-search"></i></a></h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <div class="scroll-prtlet">
                                                    <ul>
                                                        <li><a href="#LandModal" data-toggle="modal" data-target="#LandModal" title="Land assessments"
                                                            onclick="ShowLandDetails(1);" style="color:blue;font-weight:bold;">No. of Projects Requiring Land<span id="spLandAssesment"
                                                                runat="server"></span></a></li>
                                                        <li><a href="#LandModal" data-toggle="modal" data-target="#LandModal" title="proposals sent to IDCO for Land Allotment"
                                                            onclick="ShowLandDetails(2);" style="color:brown;font-weight:bold;">No. of proposals sent to
                                                    <br />
                                                            IDCO for Land Allotment<span id="spPropIDCO" runat="server"></span></a></li>
                                                        <li><a href="#LandModal" data-toggle="modal" data-target="#LandModal" title="No. of proposal for which land allotted by IDCO"
                                                            onclick="ShowLandDetails(5);" style="color:green;font-weight:bold;">No. of proposal for which
                                                    <br />
                                                            land allotted by IDCO<span id="spLandAllotByIDCO" runat="server"></span></a></li>
                                                        <li><a href="#LandModal" data-toggle="modal" data-target="#LandModal" title="Land allotted by IDCO (in Acres)"
                                                            onclick="ShowLandDetails(3);" style="color:dodgerblue;font-weight:bold;">Area of Land allotted
                                                    <br />
                                                            by IDCO (in Acres)<span id="spLandAllot" runat="server"></span></a></li>
                                                        <li><a href="#LandModal" data-toggle="modal" data-target="#LandModal" title="No. of Applications where ORTPSA Timelines have exceeded"
                                                            onclick="ShowLandDetails(4);" style="color:orange;font-weight:bold;">No. of Applications where ORTPSA
                                                    <br />
                                                            Timelines have exceeded<span id="spORTPSALAnd" runat="server"></span></a></li>
                                                    </ul>
                                                </div>
                                                <div class="portletsearch" id="landSearch" style="top: -5px;">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Year</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList CssClass="form-control" ID="ddlLandFinYear" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-8 col-sm-offset-4">
                                                                <asp:Button ID="btnLandSubmit" CssClass="btn btn-success" runat="server" Text="Submit"
                                                                    OnClick="btnLandSubmit_Click"></asp:Button>
                                                            </div>
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
                                                            onclick="ShowGRV('0');" style="color:blue;font-weight:bold;"
>Received
                                                             <asp:Label ID="lblGapplied" runat="server" Text=""></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a title="Resolved" href="#GRVModal" data-toggle="modal" data-target="#GRVModal"
                                                            onclick="ShowGRV('1');" style="color:green;font-weight:bold;"
>Resolved
                                                             <asp:Label ID="lblGrsolved" runat="server" Text=""></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a title="Pending" href="#GRVModal" data-toggle="modal" data-target="#GRVModal"
                                                            onclick="ShowGRV('2');" style="color:violet;font-weight:bold;"
>Pending
                                                             <asp:Label ID="lblGpending" runat="server" Text=""></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a title="Rejected" href="#GRVModal" data-toggle="modal" data-target="#GRVModal"
                                                            onclick="ShowGRV('3');" style="color:red;font-weight:bold;"
>Rejected
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
                                                            <asp:Button ID="btnGSearch" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnGSearch_Click"></asp:Button>
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
                                <h4 class="modal-title">Department Wise Approvals Details in (<asp:Label ID="lbl44det" runat="server" Text=""></asp:Label>)</h4>
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
                                <h4 class="modal-title">Department Wise Approvals Details</h4>
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
                <!--lAND Modal Popups -->
                <div id="LandModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Land Allotment Details in
                                    <asp:Label ID="lbldet10" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframeLand" id="myIframeLand" width="100%" style="height: 298px"
                                    runat="server"></iframe>
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
                <%--Query--%>
                <div id="Div2" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title" id="hQuery"></h4>
                                <h4 class="modal-title">
                                    <%--   Query Details in 
                        <asp:Label ID="lbldet12" runat="server" Text=""></asp:Label>--%></h4>
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
                <div id="SPMGModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
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
                                <iframe name="GRVIframe" id="GRVIframe" width="100%" style="height: 298px" runat="server"></iframe>
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
    <script type="text/javascript">

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

        function ShowService(serviceStatus) {
            var Year = $('#ContentPlaceHolder1_ddlserviceyear').val();
            var Month = $('#ContentPlaceHolder1_ddlServcMonth').val();
            var District = $('#ContentPlaceHolder1_ddlServcDistrict').val();
            document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimelineGM.aspx?Dist=" + District + "&Year=" + Year + "&Month=" + Month + "&serviceStatus=" + serviceStatus;
        }

        function ShowAPAA(APAAStatus) {

            var deptid = '<%= Session["deptid"] %>';
            var month = $('#ContentPlaceHolder1_ddlAppaMonth').val();
            var dist = $('#ContentPlaceHolder1_ddlAPAADistrict').val();
            var year = $('#ContentPlaceHolder1_ddlAppaYear').val();
            //            if('<%= Session["desId"] %>'=="126")
            //            {
            deptid = '<%= Session["deptid"] %>';
            //            }
            //            else
            //            {
            //            deptid="0";
            //            }
            document.getElementById('ContentPlaceHolder1_myAPAAIframe').src = "../../APAARequestsGrid.aspx?month=" + month + "&dist=" + dist + "&year=" + year + "&deptid=" + deptid + "&Type=" + 2 + "&APAAStatus=" + APAAStatus;
        }

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
            var District = '<%= Session["Pealuserid"] %>';
            document.getElementById('ContentPlaceHolder1_IframeQuery').src = "PealQueryDetails.aspx?Status=" + Status + "&Year=" + Year + "&District=" + District;
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
            var District = '<%= Session["Pealuserid"] %>';
            document.getElementById('ContentPlaceHolder1_IframeQuery').src = "ServiceQueryDetails.aspx?Status=" + Status + "&Year=" + Year + "&District=" + District + "&deptId=" + 0;
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
            var District = '<%= Session["Pealuserid"] %>';
            document.getElementById('ContentPlaceHolder1_IframeQuery').src = "IncentiveQueryDetails.aspx?Status=" + Status + "&Year=" + Year + "&District=" + District;
        }

        function ShowLandDetails(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlLandFinYear");
            var Year = e.options[e.selectedIndex].value;
            var Type = "";
            var Dist = '<%= Session["Pealuserid"] %>';
            document.getElementById('ContentPlaceHolder1_myIframeLand').src = "DashboardLandDetails.aspx?Status=" + Status + "&Year=" + Year + "&Type=" + 0 + "&Dist=" + Dist;
        }

        function ShowSPMG(SPMGStatus) {
            var Year = $('#ContentPlaceHolder1_ddlspmgyear').val();
            document.getElementById('ContentPlaceHolder1_SPMGIframe').src = "SPMGGridStatus.aspx?Year=" + Year + "&Level=" + 2 + "&SPMGStatus=" + SPMGStatus;
        }

        function ShowGRV(Status) {
            var Year = $("#ContentPlaceHolder1_ddlgyear option:selected").text();
            var dist = $('#ContentPlaceHolder1_ddlgdist option:selected').val();
            document.getElementById('ContentPlaceHolder1_GRVIframe').src = "GrievanceGridStatus.aspx?Year=" + Year + "&dist=" + dist + "&Status=" + Status;
        }

    </script>
</asp:Content>
