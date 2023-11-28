<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="EnergyDepartmentDashboard.aspx.cs" Inherits="Portal_Dashboard_EnergyDepartmentDashboard" %>

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

            $('#Queryfilter').click(function () {
                $('#Querysearch').slideToggle();
            });
            $('#pealfilter').click(function () {
                $('#pealsearch').slideToggle();
            }); $('#AMSfilter').click(function () {
                $('#AMSsearch').slideToggle();
            });
            //added by ritika lath for incentive
            $('#ancIncentiveApplicationSearch').click(function () {
                $('#divIncentiveAppSearch').slideToggle();
            });
            ShowIncentiveApplicationDetails();
            //added by ritika lath for incentive
        });
        function ShowSearchpanel() {
            //dvservce

            $('[id*=DepartmentServices]').css("display", "block");
            return false;
        }
        function ShowSPMG(SPMGStatus) {
            var Year = $('#ContentPlaceHolder1_ddlspmgyear').val();
            document.getElementById('ContentPlaceHolder1_SPMGIframe').src = "SPMGDeptwiseStatusdtls.aspx?Year=" + Year + "&Level="+1+"&SPMGStatus=" + SPMGStatus;
        }

        function ShowdistSPMG(SPMGStatus) {
            var Year = $('#ContentPlaceHolder1_ddlspmgyear').val();
            document.getElementById('ContentPlaceHolder1_SPMGIframe').src = "SPMGDeptwiseStatusdtls.aspx?Year=" + Year + "&Level="+2+"&SPMGStatus=" + SPMGStatus;
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
        .portletcontainer.cmdashbordportlet
        {
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
                        <h1>
                            Department DashBoard</h1>
                        <ul class="breadcrumb">
                            <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                            <li><a>Department DashBoard</a></li></ul>
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
                                <h4>
                                    Master Tracker <small>
                                        <asp:DropDownList ID="ddlFinacialYear" runat="server" CssClass="masterdropdown" OnSelectedIndexChanged="ddlFinacialYear_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </small>
                                </h4>
                                <div class="row">
                                    <%--  <div class="col-sm-6  col-md-4">
             <div class="masterportletsec minheight-170">
             <h4 class="text-center">Project Proposals </h4>

             <div class="two-sectons">
             <p><span><asp:Label ID="lblTrackerEvalution" runat="server" Text=""></asp:Label></span> Under <br>Evaluation </p>
             </div>
             <div class="two-sectons magin-right0">
             <p><span><asp:Label ID="lblTrackerApproved" runat="server" Text=""></asp:Label></span> Approved,<small> yet to commence operations </small></p>
             </div>

             
              

             </div>
             </div>--%>
                                    <div class="col-sm-6 col-md-4">
                                        <div class="masterportletsec">
                                            <h4>
                                                Pending approval
                                            </h4>
                                            <p>
                                                Applications past ORTPSA timelines<span id="spanapprove" runat="server"></span></p>
                                        </div>
                                    </div>
                                    <%-- <div class="col-sm-4">
             <div class="masterportletsec">
             <h4>CSR Spend </h4>
             <p>Total Spending <span> <i class="fa fa-rupee"></i>&nbsp;<label id="SPNetSpent" runat="server"></label><small>Cr.</small></span></p>
            

             </div>
             </div>--%>
                                    <%-- <div class="col-sm-4">
             <div class="masterportletsec">
             <h4>Investment </h4>
             <p>Total Proposed Investment<span>22234.56 <small>Cr.</small></span></p>
            

             </div>
             </div>--%>
                                    <%--  <div class="col-sm-4">
             <div class="masterportletsec">
             <h4>Employment</h4>
             <p>Total Proposed Employment <span>1235</span></p>
             

             </div>
             </div>--%>
                                    <%-- <div class="col-sm-3">
             <div class="masterportletsec">
             <h4>Incentive Details</h4>
             <p>Pending incentives <span>0</span></p>
             

             </div>
             </div>--%>
                                    <div class="col-sm-6 col-md-4" id="dvCICGMast" runat="server" style="display: none">
                                        <div class="masterportletsec">
                                            <h4>
                                                Central Inspection Framework</h4>
                                            <p>
                                                Pending inspections <span id="SPcicgpending" runat="server"></span>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-md-4" id="dvAPAAMast" runat="server" style="display: none">
                                        <div class="masterportletsec">
                                            <h4>
                                                IDCO POST ALLOTMENT APPLICATIONS</h4>
                                            <p>
                                                Pending issues <span id="spAPAAPending" runat="server"></span>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-sm-4" style="display:none;">
                                        <div class="masterportletsec">
                                            <h4>
                                                State Project Monitoring Group
                                            </h4>
                                            <p>
                                                Issues pending<span id="spSpmgpnd" runat="server"></span></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="grphs-sec">
                                <div class="text-left text-red">
                                    <h4>
                                        For detailed information, select search option <i class="fa fa-search"></i>shown
                                        in the menu bar for each field</h4>
                                </div>
                                <div class="row">
                                    <%-- <div class="col-md-4 col-sm-6">
                       <div class="investordashboard-sec incentive-sec ">
                     <h4>Single Window Application Status <a class="pull-right spmgfilter" id="pealfilter"><i class="fa fa-search"></i></a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                     <ul>
                     <li>
                     
                     <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Applied"  onclick="Show(1);" >Applications Applied<span ><asp:Label ID="lblPealApplied" runat="server" Text=""></asp:Label></span></a>
                  
                    
                     </li>
                      <li>
                      <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Approved"  onclick="Show(2);" >Applications Approved<span ><asp:Label ID="lblPealApproved" runat="server" Text=""></asp:Label></span></a>
                      </li>
                      <li>
                      <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Approved(yet to commence)" onclick="Show(8);" >Applications Approved(yet to commence)<span ><asp:Label ID="lblPealCommence" runat="server" Text=""></asp:Label></span></a>
                      </li>
                      <li>
                      <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Rejected"  onclick="Show(3);" >Applications Rejected<span ><asp:Label ID="lblPealRejected" runat="server" Text=""></asp:Label></span></a>
                      </li>
                        <li>
                      <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Deferred"  onclick="Show(7);" >Applications Deferred<span ><asp:Label ID="lblPealDeferred" runat="server" Text=""></asp:Label></span></a>
                      </li>
                     
                         
                     </ul>
                     </div>
                          <div class="portletsearch" id="pealsearch">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Quarter </label>
                  <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="DropDownList16" runat="server">
                <asp:ListItem>--Select--</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                 <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  </div>
                     <div class="form-group">
                  <div class="row">
                  <label class="col-sm-4">District</label>
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
                  <asp:Button ID="Button7" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
             
               
                       </div>
                       </div>--%>
                                    <div class="col-sm-6  col-md-4" id="dvCICG" runat="server" style="display: none;">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>
                                                Central Inspection Framework<a class="pull-right spmgfilter" data-toggle="tooltip"
                                                    title="Search" id="CifFilter"><i class="fa fa-search"></i></a></h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <ul>
                                                 <li><a title="Inspections Completed" href="#CIFModal" data-toggle="modal" data-target="#CIFModal"
                                                        onclick="ShowCICG('C');">Inspections Completed<span id="SPcicgcompleted" runat="server"></span></a></li>
                                                    <li><a title="Inspections Scheduled" href="#CIFModal" data-toggle="modal" data-target="#CIFModal"
                                                        onclick="ShowCICG('A');">Inspections Scheduled<span id="SPcicgapplied" runat="server"></span></a></li>
                                                   
                                                    <li><a title="Unattended Inspections" href="#CIFModal" data-toggle="modal" data-target="#CIFModal"
                                                        onclick="ShowCICG('B');">Unattended Inspections<span id="SPunattInsdash" runat="server"></span></a></li>
                                                    <li><a title="Inspection reports not uploaded within 48 hours" href="#CIFModal1"
                                                        data-toggle="modal" data-target="#CIFModal1" onclick="ShowCICG1();">Inspection reports
                                                        not uploaded within 48 hours<span class="bgdisbursed" id="SPReprtNotUploaded" runat="server"></span></a></li>
                                                </ul>
                                            </div>
                                            <div class="portletsearch" id="CifSearch">
                                                <div class="form-group">
                                                    <div class="row" style="display: none">
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
                                    <div class="col-sm-6  col-md-4" id="dvapaa" runat="server" style="display: none">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>
                                                IDCO POST ALLOTMENT APPLICATIONS<a class="pull-right spmgfilter" data-toggle="tooltip"
                                                    title="Search" id="APAAfilter"><i class="fa fa-search"></i></a></h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <ul>
                                                    <li><a title="Received" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('A');">Change requests received<span id="spchngrqstApplied" runat="server"></span></a></li>
                                                    <li><a title="Processed" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('B');">Change requests processed<span id="spchngreqdispose" runat="server"></span></a></li>
                                                    <li><a title="Disposed" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('D');">Change requests pending to be disposed<span id="spchngreqPendAtIDCO"
                                                            runat="server"></span></a></li>
                                                            <li><a title="Disposed" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('C');">Change requests pending at Unit<span id="spnPendingatUnit"
                                                            runat="server"></span></a></li>
                                                    <li><a title="Approvals" href="#APAAModal" data-toggle="modal" data-target="#APAAModal"
                                                        onclick="ShowAPAA('E');">Change requests which have crossed 30 days<span class="bgdisbursed"
                                                            id="spchngReqCrossThirty" runat="server"></span></a></li>
                                                </ul>
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
                                    <%-- <div class="col-sm-6  col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>Year wise employment <a class="pull-right " id="employmentfilter"><i class="fa fa-search"></i></a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                       <div >
                       <a title="Total Proposed Employment" href="#EmploymentModal" data-toggle="modal" data-target="#EmploymentModal" onclick="openpoupwinEmploeement('HiddenField3');">
                       <h3><i class="fa fa-users"></i><span><asp:Label ID="lblPealEmployeemnet" runat="server" Text=""></asp:Label></span>
                       <asp:HiddenField ID="HiddenField3" Value="3" runat="server" />
                       </h3>
                      Total Proposed Employment
                       </a>
                    </div>
                     </div>
                          <div class="portletsearch" id="employmentsearch">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Year</label>
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
                  <label class="col-sm-4">District</label>
                   <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlDistrictEmployeement" runat="server">
                <asp:ListItem>--Select--</asp:ListItem> 
                </asp:DropDownList>
                  </div>
                  </div>
                  
                  </div>
           
                    <div class="form-group">
                  <div class="row">
                 <div class="col-sm-8 col-sm-offset-4">
                  <asp:Button ID="Button4" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
             
               
                       </div>
                       </div>
                     <div class="col-sm-6  col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>Year wise investment <a class="pull-right " id="investmentfilter"><i class="fa fa-search"></i></a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                       <div >
                       <a title="Total Proposed Investment" href="#investmentModal" data-toggle="modal" data-target="#investmentModal" onclick="openpoupwincapiatal('HiddenField3');">
                       <h3><i class="fa fa-inr"></i><span><asp:Label ID="lblCapital" runat="server" Text=""></asp:Label></span><small>Cr.</small> 
                        <asp:HiddenField ID="HiddenField4" Value="3" runat="server" />
                       </h3>
                    Total Proposed Investment
                       </a>


                      

                    </div>
                     </div>
                          <div class="portletsearch" id="investmentsearch">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Year</label>
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
                  <label class="col-sm-4">District</label>
                   <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlYDistrictInvest" runat="server">
                <asp:ListItem>--Select--</asp:ListItem> 
                </asp:DropDownList>
                  </div>
                  </div>
                  
                  </div>
           
                    <div class="form-group">
                  <div class="row">
                 <div class="col-sm-8 col-sm-offset-4">
                  <asp:Button ID="Button2" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
             
               
                       </div>
                       </div>--%>
                                    <div class="col-sm-6  col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>
                                                Department/ Agency Wise Approvals<a class="pull-right spmgfilter" data-toggle="tooltip"
                                                    title="Search" id="linkDepartmentServices"><i class="fa fa-search"></i></a></h4>
                                            <div class="portletcontainer cmdashbordportlet" id="dvservce">
                                                <ul>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" title="Application Received"
                                                        onclick="Show('R');">Application Received<span id="hdApplied" runat="server"><asp:Literal
                                                            ID="ltlServiceApplied" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" title="Total Approvals Provided"
                                                        onclick="Show('A');">Total Approvals Provided<span id="hdApprove" runat="server"><asp:Literal
                                                            ID="ltlApprove" runat="server"></asp:Literal></span></a></li>
                                                      <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="Show('PK');"
                                                        title="Approval Pending">Query In Progress<span id="hdnqueryRaised" runat="server"><asp:Literal
                                                            ID="Literal1" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" title="Approval Pending"
                                                        onclick="Show('P');">Approval Pending<span id="hdPending" runat="server"><asp:Literal
                                                            ID="ltlServicepending" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" title="Total Rejected"
                                                        onclick="Show('RJ');">Total Rejected <span id="hdReject" runat="server">
                                                            <asp:Literal ID="ltlServiceReject" runat="server"></asp:Literal></span></a></li>
                                                    <li><a href="#DsModal1" data-toggle="modal" data-target="#DsModal1" onclick="Show1();"
                                                        title="Applications past ORTPSA timelines">Applications past ORTPSA timelines<span
                                                            class="bgdisbursed" id="hdExceed" runat="server"></span></a></li>
                                                </ul>
                                            </div>
                                            <div class="portletsearch" id="DepartmentServices">
                                                <div class="form-group" style="display: none">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            Department</label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>--%>
                                                            <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control dpt" OnSelectedIndexChanged="ddldept_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%-- </ContentTemplate>
                                  
                </asp:UpdatePanel>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                   <ContentTemplate>--%>
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
                                                <%--</ContentTemplate>
                  <triggers>
            
                                  
                </triggers>
                </asp:UpdatePanel>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <div class="col-sm-6  col-md-4">
                <div class="investordashboard-sec incentive-sec ">
                <h4>CSR ACTIVITIES
                <a class="pull-right spmgfilter" id="linkCSRActivities"><i class="fa fa-search"></i></a></h4>
                <div class="portletcontainer cmdashbordportlet">
                <ul>
                     <li>Total Project<a href="#csrModal" data-toggle="modal" data-target="#csrModal" title="Projects taken up" onclick="ShowCSR();"><span  id="SPProject" runat="server"></span></a></li>
                    <%--<li>Recommended by Council<span >49</span></li>
                      <li>Total Spending<span ><i class="fa fa-rupee"></i>&nbsp;<label id="SPSpent" runat="server"></label>&nbsp; Cr.</span></li> 
                     </ul>
                 <div class="portletsearch" id="CSRActivities" style="top: -5px;">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">District</label>
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
                  <label class="col-sm-4">Year</label>
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
                       </div>--%>
                                    <%--spmg part commented by Romalin Panda--%>
                                   <div class="col-sm-6  col-md-4" style="display:none;">
                                    <div class="investordashboard-sec incentive-sec ">
                                        <h4>
                                            STATE PROJECT MONITORING GROUP (<asp:Label ID="lbl4" runat="server" Text=""></asp:Label>)<a
                                                class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="linkSpmg"><i
                                                    class="fa fa-search"></i></a>
                                        </h4>
                                        <div class="portletcontainer cmdashbordportlet">
                                            <div class="scroll-prtlet">
                                                <table class="table table-bordered" style="height:215px;">
                                                    <tr>
                                                        <th style="vertical-align: middle!important;">
                                                            Status
                                                        </th>
                                                        <th width="130px" style="vertical-align: middle!important;">
                                                            State Level
                                                        </th>
                                                        <th width="130px" style="vertical-align: middle!important;">
                                                            District Level
                                                        </th>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Issues Received
                                                        </td>
                                                        <td align="right">
                                                         <a title="Issues Received" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" onclick="ShowSPMG('received');">
                                                             <span id="spmgraised" runat="server"></span>
                                                         </a>
                                                        </td>
                                                        <td align="right">
                                                            <a title="Issues Received" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" onclick="ShowdistSPMG('received');">
                                                             <span id="spmgdistraised" runat="server"></span>
                                                            </a>
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Issues Resolved
                                                        </td>
                                                        <td align="right">
                                                         <a title="Issues Resolved" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" onclick="ShowSPMG('resolved');">
                                                             <span id="spmgresolved" runat="server"></span>
                                                         </a>
                                                        </td>
                                                        <td align="right">
                                                        <a title="Issues Resolved" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" onclick="ShowdistSPMG('resolved');">
                                                            <span id="spmgdistresolved" runat="server"></span>
                                                        </a>
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Issues Pending
                                                        </td>
                                                        <td align="right">
                                                         <a title="Issues Pending" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" onclick="ShowSPMG('pending');">
                                                             <span id="spmgpending" runat="server"></span>
                                                         </a>
                                                        </td>
                                                        <td align="right">
                                                        <a title="Issues Pending" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" onclick="ShowdistSPMG('pending');">
                                                            <span id="spmgdistpending" runat="server"></span>
                                                        </a>
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           Issues Pending > 30 days
                                                        </td>
                                                        <td align="right">
                                                         <a title="Issues Pending > 30 days" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" onclick="ShowSPMG('delayed');">
                                                             <span id="spmg30pending" runat="server"></span>
                                                         </a>
                                                        </td>
                                                        <td align="right">
                                                        <a title="Issues Pending > 30 days" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal" onclick="ShowdistSPMG('delayed');">
                                                            <span id="spmg30distpending" runat="server"></span>
                                                        </a>
                                                        </td>                                                        
                                                    </tr>
                                                </table>
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


                                    <div class="col-sm-6 col-md-4">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>
                                                Query (<asp:Label ID="lbl10" runat="server" Text=""></asp:Label>)<a class="pull-right spmgfilter" id="Queryfilter"><i class="fa fa-search"></i>
                                                </a>
                                            </h4>
                                            <div class="portletcontainer cmdashbordportlet">
                                                <ul>
                                                    <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal" title="Queries Raised" onclick="ShowQuery(1);">Queries Raised<span id="spRaised" runat="server"></span></a></li>
                                                    <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal" title="Queries Responded" onclick="ShowQuery(2);">Queries Responded<span id="spRevert" runat="server"></span></a></li>
                                                    <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal" title="Response Pending" onclick="ShowQuery(3);">Response Pending<span id="spPending" runat="server"></span></a></li>
                                                    <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal" onclick="ShowQuery(4);"
                                                        title="Response not received within Timeline">Response not received within Timeline<span class="bgdisbursed"
                                                            id="spResponseTimeline" runat="server"></span></a></li>
                                                    <li><a href="#QueryModal" data-toggle="modal" data-target="#QueryModal" title="Queries Raised" onclick="ShowQuery(5);">Average Day Taken to Raise Query<span id="spAvgTime" runat="server"></span></a></li>
                                                </ul>
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
                                                                <asp:Button ID="Button10" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="Button10_Click">
                                                                </asp:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Incentive Application Details added by ritika lath --%>
                                    <div class="col-sm-6  col-md-4" style="display:none">
                                        <div class="investordashboard-sec incentive-sec">
                                            <h4>
                                                Incentive Application Details<a class="pull-right spmgfilter" data-toggle="tooltip"
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
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
                <!--Modal Popups -->
                <div id="SPMGModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    State Project Monitoring Group Details in
                                    <asp:Label ID="lblDet2" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="SPMGIframe" id="SPMGIframe" width="100%" style="height: 298px" runat="server">
                                </iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="PpModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Project Proposals Details</h4>
                            </div>
                            <div class="modal-body">
                                <table class="table table-bordered">
                                    <tr>
                                        <th width="40px">
                                            Sl#.
                                        </th>
                                        <th>
                                            State
                                        </th>
                                        <th>
                                            District
                                        </th>
                                        <th>
                                            Name of the company
                                        </th>
                                    </tr>
                                    <tr>
                                        <td rowspan="2">
                                            1
                                        </td>
                                        <td rowspan="2">
                                            Odisha
                                        </td>
                                        <td>
                                            Khurda
                                        </td>
                                        <td>
                                            Tata Steel
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Khurda
                                        </td>
                                        <td>
                                            Coca cola
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2">
                                            2
                                        </td>
                                        <td rowspan="2">
                                            Odisha
                                        </td>
                                        <td>
                                            Khurda
                                        </td>
                                        <td>
                                            Tata Steel
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Khurda
                                        </td>
                                        <td>
                                            Coca cola
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
                                <h4 class="modal-title">
                                    Department Wise Approvals Details</h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myservcIframe" width="100%" style="height: 298px" runat="server">
                                </iframe>
                                <asp:HiddenField ID="hdnsrvc" runat="server" />
                                 <asp:HiddenField ID="hdnsrvc1" runat="server" />
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
                                <h4 class="modal-title">
                                    Applications past ORTPSA timelines Details</h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myservcIframe1" width="100%" style="height: 298px" runat="server">
                                </iframe>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
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
                                <h4 class="modal-title">
                                    Year wise investment Details</h4>
                            </div>
                            <div style="width: 885px; height: 500px; overflow: auto;">
                                <table class="table table-bordered" id="lblCapital">
                                </table>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        Close</button>
                                </div>
                            </div>
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
                                <h4 class="modal-title">
                                    Year wise employment Details</h4>
                            </div>
                            <div style="width: 885px; height: 500px; overflow: auto;">
                                <table class="table table-bordered" id="tblEmployemnt">
                                </table>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        Close</button>
                                </div>
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
                                <h4 class="modal-title">
                                    Department/ Agency Wise Approvals Details</h4>
                            </div>
                            <div class="modal-body">
                                <table class="table table-bordered">
                                    <tr>
                                        <th width="40px">
                                            Sl#.
                                        </th>
                                        <th>
                                            Name of the company
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Xyz
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            abc
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
                                <h4 class="modal-title">
                                    Central Inspection Framework Details</h4>
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
                <div id="CIFModal1" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Central Inspection Framework Details</h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myCICGIframe1" width="100%" style="height: 298px" runat="server">
                                </iframe>
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
                                <h4 class="modal-title">
                                    IDCO POST ALLOTMENT APPLICATIONS Details</h4>
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
                <div id="IncentiveModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Incentive Details</h4>
                            </div>
                            <div class="modal-body">
                                <table class="table table-bordered">
                                    <tr>
                                        <th width="40px">
                                            Sl#.
                                        </th>
                                        <th>
                                            Name of the Company
                                        </th>
                                        <th>
                                            Amount
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            Xyz
                                        </td>
                                        <td>
                                            10
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
                <div id="Pealmodal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Single Window Application Status Details</h4>
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
                                <h4 class="modal-title">
                                    Single Window Application Status Details</h4>
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
                <div id="QueryModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Query Details in
                                    <asp:Label ID="lbldet12" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="IframeQuery" width="100%" style="height: 298px" runat="server">
                                </iframe>
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
        function Show1() {

            var Dept = $('#ContentPlaceHolder1_hdnsrvc1').val();
            var Servc = $('#ContentPlaceHolder1_ddlService').val();
            //document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimeline.aspx?Dept=0&servc=0";
            //document.getElementById('ContentPlaceHolder1_myservcIframe1').src = "ApprovalTimeline.aspx?Dept=" + Dept + "&Servc=" + Servc;
            document.getElementById('ContentPlaceHolder1_myservcIframe1').src = "EnergyApprovalTimeline.aspx?Dept=" + Dept + "&Servc=" + Servc;
        }
        function Show(ServiceStatus) {            
            var Dept = $('#ContentPlaceHolder1_hdnsrvc1').val();
            var Servc = $('#ContentPlaceHolder1_ddlService').val();
            //document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimeline.aspx?Dept=0&servc=0";
            //document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimelineDashboard.aspx?Dept=" + Dept + "&Servc=" + Servc + "&ServiceStatus=" + ServiceStatus;
            document.getElementById('ContentPlaceHolder1_myservcIframe').src = "EnergyApprovalTimelineDashboard.aspx?Dept=" + Dept + "&Servc=" + Servc + "&ServiceStatus=" + ServiceStatus;
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
