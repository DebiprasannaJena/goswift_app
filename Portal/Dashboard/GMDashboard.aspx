<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="GMDashboard.aspx.cs" Inherits="Portal_Dashboard_GMDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function pageLoad() {
            //MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
           
            $("#<%=chkCSRDistrct.ClientID %>").find('input[type="checkbox"]').click(function () {
                var cbl = document.getElementById('<%=chkCSRDistrct.ClientID %>').getElementsByTagName("input");
                chkCnt = 0;
                for (i = 0; i < cbl.length; i++) {
                    if (cbl[i].checked == true) {
                        chkCnt = chkCnt + 1;
                    }
                }
                if (cbl.length == chkCnt) {
                    $("#ContentPlaceHolder1_chkCSRDistrctheader").prop('checked', true);
                } else {
                    $("#ContentPlaceHolder1_chkCSRDistrctheader").prop('checked', false);
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
            var yr8 = $('#ContentPlaceHolder1_ddlIncentiveYear option:selected').text();
            $('#ContentPlaceHolder1_lbl8').html(yr8);
            $('#ContentPlaceHolder1_lblDet6').html(yr8);
            $('#btnIncentiveSubmit').click(function () {
                yr8 = $('#ContentPlaceHolder1_ddlIncentiveYear option:selected').text();
                $('#ContentPlaceHolder1_lbl8').html(yr8);
                $('#ContentPlaceHolder1_lblDet6').html(yr8);
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
            var yr7 = $('#ContentPlaceHolder1_ddlCSRYear option:selected').text();
            $('#ContentPlaceHolder1_lbl7').html(yr7);
            $('#ContentPlaceHolder1_lblDet5').html(yr7);
            $('#btnCSRStatus').click(function () {
                yr7 = $('#ContentPlaceHolder1_ddlCSRYear option:selected').text();
                $('#ContentPlaceHolder1_lbl7').html(yr7);
                $('#ContentPlaceHolder1_lblDet5').html(yr7);
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
            $('#INCENTIVEfilter').click(function () {
                $('#INCENTIVEsearch').slideToggle();
            });
            $('#pealfilter').click(function () {
                $('#pealsearch').slideToggle();
            }); $('#AMSfilter').click(function () {
                $('#AMSsearch').slideToggle();
            });
            $('#landFilter').click(function () {
                $('#landSearch').slideToggle();
            }); 
        });
        function Show(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlPealQuarter");
            var PealType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPEALDistrict");
            var Pealdistrict = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPealYear");
            var Pealyear = e.options[e.selectedIndex].value;
            //            document.getElementById('ContentPlaceHolder1_myIframe').src = "FramePealStatus.aspx?Status=" + Status + "&PealType=" + PealType + "&Pealdistrict=" + Pealdistrict;
            document.getElementById('ContentPlaceHolder1_myIframe').src = "FramepealStatusDistrict.aspx?Status=" + Status + "&PealType=" + 0 + "&Pealdistrict=" + Pealdistrict + "&PealYear=" + Pealyear + "&PealQuarter=" + 0 + "&PealUserStatus=" + 1;
        }
        //ADDED BY SUROJ KUMAR PRADHAN ON 05-12-17 TO DRILL DOWN PEAL DISTWISE AND STATEWISE DATA
        function ShowPealstatewise(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlPealQuarter");
            var PealType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPEALDistrict");
            var Pealdistrict = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPealYear");
            var Pealyear = e.options[e.selectedIndex].value;
            document.getElementById('ContentPlaceHolder1_myIframe').src = "FramepealStatusDistrict.aspx?Status=" + Status + "&PealType=" + 0 + "&Pealdistrict=" + Pealdistrict + "&PealYear=" + Pealyear + "&PealQuarter=" + 0 + "&PealUserStatus=" + 1 + "&stateid=" + 11;
        }
        function ShowPealdistwise(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlPealQuarter");
            var PealType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPEALDistrict");
            var Pealdistrict = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlPealYear");
            var Pealyear = e.options[e.selectedIndex].value;
            document.getElementById('ContentPlaceHolder1_myIframe').src = "FramepealStatusDistrict.aspx?Status=" + Status + "&PealType=" + 0 + "&Pealdistrict=" + Pealdistrict + "&PealYear=" + Pealyear + "&PealQuarter=" + 0 + "&PealUserStatus=" + 1 + "&stateid=" + 12;
        }
        //ENDED BY SUROJ KUMAR PRADHAN
        //ADDED BY SUROJ KUMAR PRADHAN FOR INCENTIVE
        function ShowIncentive(Status) {


            var e = document.getElementById("ContentPlaceHolder1_ddlIncentive");
            var IncentiveType = e.options[e.selectedIndex].value;
            var e = document.getElementById("ContentPlaceHolder1_ddlIncentiveYear");
            var IncentiveYear = e.options[e.selectedIndex].value;

            //document.getElementById('ContentPlaceHolder1_IncentiveIframe').src = "IncentiveStatus.aspx?Action=" + Status + "&IncentiveType=" + 0 + "&IncentiveYear=" + 0;
            document.getElementById('ContentPlaceHolder1_IncentiveIframe').src = "IncentiveStatus.aspx?Action=" + Status + "&IncentiveType=" + IncentiveType + "&IncentiveYear=" + IncentiveYear + "&Userid=" + 0 + "&Distid=" + 0;
            //document.getElementById('ContentPlaceHolder1_IncentiveIframe').src = "IncentiveStatus.aspx?Action=" + Status + "&IncentiveType=" + 0 + "&IncentiveYear=" + IncentiveYear + "&Userid=" + 0 + "&Distid=" + Pealdistrict;

        }
        //added by nibedita behera on 01-11-2017 for multiple district
        function CSRDist() {
            var val = [];
            $('#ContentPlaceHolder1_chkCSRDistrct').find('input[type=checkbox]:checked').each(function () {
                val.push($(this).val());
            })
            $('#ContentPlaceHolder1_hdnCSRDistrct').val(val.join(','));

        }
        //ENDED BY SUROJ
        function openpoupwin(ctrl) {

            $.ajax({
                type: "POST",
                url: "GMDashboard.aspx/PealDetailsBind",

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
                url: "GMDashboard.aspx/EmployeementPealDetailsBind",

                data: '{"Pealyear":"' + $('#ContentPlaceHolder1_ddlYearEmployement').val() + '","Pealdistrict":"' + $('#ContentPlaceHolder1_ddlDistrictEmployeement').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#ContentPlaceHolder1_lblemp').html($('#ContentPlaceHolder1_ddlYearEmployement').val());
                    tempHTML = "";
                    $("#tblEmployemnt").html('')
                    tempHTML += '<thead><tr class="persist-header">'
                    tempHTML += '<th rowspan="1" valign="middle" width="20px" bgcolor="#e4e4e4">Sl#</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Company Name</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Direct Employee</th>'
                    tempHTML += '<th rowspan="1" valign="middle"  bgcolor="#e4e4e4">Contractual Employee</th>'
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
                            tempHTML += '<td >' + value.intDirectEmployee + '</td>';
                            tempHTML += '<td >' + value.intContractualEmployee + '</td>';
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
                url: "GMDashboard.aspx/EmployeementCapitalPealDetailsBind",

                data: '{"Pealyear":"' + $('#ContentPlaceHolder1_ddlYearInvest').val() + '","Pealdistrict":"' + $('#ContentPlaceHolder1_ddlYDistrictInvest').val() + '"}',
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
    <style type="text/css">
        .portletcontainer.cmdashbordportlet
        {
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
                            $('#INCENTIVEfilter').click(function () {
                                $('#INCENTIVEsearch').slideToggle();
                            });
                            $('#pealfilter').click(function () {
                                $('#pealsearch').slideToggle();
                            }); $('#AMSfilter').click(function () {
                                $('#AMSsearch').slideToggle();
                            });
                            $('#Queryfilter').click(function () {
                                $('#Querysearch').slideToggle();
                            });
                            $('#landFilter').click(function () {
                                $('#landSearch').slideToggle();
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
              <%-- <div class="header-icon">
                  <img src="../images/naveen-patnaik.jpg" style="height: 53px;" class="img img-responsive img-thumbnail" />
               </div>--%>
               <div class="header-icon">
                  <i class="fa fa-tachometer"></i>
               </div>
               <div class="header-title">
                  <h1>GM DashBoard</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>GM DashBoard</a></li></ul>
               </div>
            </section>
                <!-- Main content -->
                <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                      <div class="Mastertracker">
                       <p class="pull-right">Last updated on <span id="spLastUpdate" runat="server"></span> 
                     <asp:LinkButton ID="LinkButton1" CssClass="refresh" runat="server" OnClick="LinkButton1_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
                       </p>
              <h4>Master Tracker <small>
             <asp:DropDownList  ID="ddlFinacialYear" runat="server" CssClass="masterdropdown" OnSelectedIndexChanged="ddlFinacialYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
             </small></h4>
<div class="row">
           <div class="col-sm-6  col-md-4">
             <div class="masterportletsec">
             <h4>Single Window Application Status </h4>
             <div class="width33">
              <p>Application Received <span id="lblProposalRecieved" runat="server"></span></p>
             </div>
          <div class="width33"> <p>Application Approved <span id="lblProposalapproved" runat="server"></span></p></div>
           <div class="width33 margin-right0">  <p>Application Pending <span id="lblTrackerEvalution" runat="server"></span></p></div>   
               
             <div class="clearfix"></div>
             </div>
             </div>
             <div class="col-sm-6  col-md-4">
             <div class="masterportletsec">
             <h4>Pending approvals </h4>
             <p>Applications past ORTPSA timelines<span id="spanapprove" runat="server"></span></p>
             
             </div>
             </div>
             <div class="col-sm-6  col-md-4">
             <div class="masterportletsec">
             <h4>CSR Spend </h4>
             <p>Total Spending <span> <i class="fa fa-rupee"></i>&nbsp;<label id="SPNetSpent" runat="server"></label><small>&nbsp;Cr.</small></span></p>
            

             </div>
             </div>
            <%-- <div class="col-sm-4">
             <div class="masterportletsec">
             <h4>Investment </h4>
             <p>Total Proposed Investment<span>22234.56 
             
             <medium>Cr.</medium></span></p>
            

             </div>
             </div>--%>
             <%-- <div class="col-sm-4">
             <div class="masterportletsec">
             <h4>Employment</h4>
             <p>Total Proposed Employment <span>1235</span></p>
             

             </div>
             </div>--%>
             <div class="col-sm-6  col-md-4">
             <div class="masterportletsec">
             <h4>Incentive Details</h4>
             <p>Pending incentives <span><asp:Literal ID="lblIncpendingdtls" runat="server"></asp:Literal></span></p>
             

             </div>
             </div>
             
              <div class="col-sm-6  col-md-4">
             <div class="masterportletsec">
             <h4>Central Inspection Framework</h4>
             <p>Pending inspections <span id="SPcicgpending" runat="server"></span></p>
             
             </div>
             </div>
             <%-- <div class="col-sm-3">
             <div class="masterportletsec">
             <h4>IDCO POST ALLOTMENT APPLICATIONS</h4>
             <p>Pending issues <span id="spAPAAPending" runat="server"></span></p>
             
             </div>
             </div>--%>
             <%-- <div class="col-sm-3">
             <div class="masterportletsec">
             <h4>SPMG</h4>
             <p>Issues pending<span id="spSpmgpnd" runat="server"></span></p>
             
             </div>
             </div>--%>
             </div>
                  </div>

                <div class="grphs-sec">
                  <div class="text-left text-red"><h4>For detailed information, select search option <i class="fa fa-search"></i> shown in the menu bar for each field</h4></div>
                  <div class="row">
                <%--  PEAL form status--%>
                    <div class="col-md-8 col-sm-8">
                                        <div class="investordashboard-sec incentive-sec ">
                                            <h4>
                                                Single Window Application Status (<asp:Label ID="lbl1" runat="server" Text="reerre"></asp:Label>)<a class="pull-right spmgfilter" data-toggle="tooltip"
                                                    title="Search" id="pealfilter"><i class="fa fa-search"></i></a>
                                            </h4>

                                            <div class="portletcontainer cmdashbordportlet"><div class="scroll-prtlet">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th rowspan="2" style="vertical-align:middle!important;">
                                                            Status
                                                        </th>
                                                        <th width="130px" style="vertical-align:middle!important;"  rowspan="2">
                                                            State Level
                                                        </th>
                                                        <th width="130px" rowspan="2" style="vertical-align:middle!important;">
                                                            District Level
                                                        </th>
                                                          <th  width="260px"  style="text-align:center!important;" colspan="2">
                                                            SPL. Single Window
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                          <th  width="130px" colspan="1">
                                                              IT</th>
                                                               <th  width="130px" colspan="1">
                                                              Tourism</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Applied
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
                                                        <td>
                                                            Approved
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
                                                        <td>
                                                            Rejected
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
                                                        <td>
                                                            Deferred
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
                                                        <td>
                                                           Query In Progress
                                                        </td>
                                                        <td align="right">
                                                            <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Query In Progress" onclick="ShowPealstatewise(8);">
                                                                <asp:Label ID="lblPealQueryRaise" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Query In Progress" onclick="ShowPealdistwise(8);">
                                                                <asp:Label ID="lblPealdistQueryRaise" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                         <td align="right">
                                                            <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Query In Progress" onclick="ShowITpeal(8);">
                                                                <asp:Label ID="lblPealITQueryRaise" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                         <td align="right">
                                                            <a href="#Div1" data-toggle="modal" data-target="#Div1" title="Query In Progress" onclick="Showtourismpeal(8);">
                                                                <asp:Label ID="lblPealTourismQueryRaise" runat="server" Text=""></asp:Label></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Under Evalution
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
                                                </table></div>
                                              
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

                                    
                        <%--  Department Wise Approvals--%>
                  <div class="col-sm-6  col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>Department Wise Approvals<a class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="linkDepartmentServices"><i class="fa fa-search"></i></a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                     <ul>
                     <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('R');"  title="Application Received">Application Received<span  id="hdApplied" runat="server"><asp:Literal ID="ltlServiceApplied" runat="server"></asp:Literal></span></a></li>
                      <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('A');"  title="Total Approvals Provided">Total Approvals Provided<span  id="hdApprove" runat="server"><asp:Literal ID="ltlApprove" runat="server"></asp:Literal></span></a></li>                      
                      <%--<li>Approval Pending<span  id="hdPending" runat="server"><asp:Literal ID="ltlServicepending" runat="server"></asp:Literal></span></li>--%>
                      <%--<li>Total Rejected Provided<span  id="hdReject" runat="server"><asp:Literal ID="ltlServiceReject" runat="server"></asp:Literal></span></li>--%>
                        <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('PK');"
                                                        title="Approval Pending">Query In Progress<span id="hdnqueryRaised" runat="server"><asp:Literal
                                                            ID="Literal1" runat="server"></asp:Literal></span></a></li>
                      <li><a href="#DsModal" data-toggle="modal" data-target="#DsModal" onclick="ShowService('P');"  title="Approval Pending">Approval Pending<span  id="hdPending" runat="server"></span></a></li>
                      </ul>
                     </div>
                          
                          <div class="portletsearch" id="DepartmentServices">
                                           <div class="form-group">
                  <div class="row">
                  <label class="col-sm-4">Year</label>
                   <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlserviceyear" runat="server">
                <asp:ListItem>--Select--</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  </div>
                               <div class="form-group">
                  <div class="row">
                  <label class="col-sm-4">Month</label>
                   <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlServcMonth" runat="server">
                <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  
                  </div>
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Districts</label>
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
                  <asp:Button ID="btnStatusOfApproval" CssClass="btn btn-success" runat="server" Text="Submit"  OnClick="btnStatusOfApproval_Click"></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
             
                </div>
                </div>
                  <%-- Incentive Details--%>
                     <div class="col-sm-6  col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>Incentive Details (<asp:Label ID="lbl8" runat="server" Text=""></asp:Label>)<a class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="INCENTIVEfilter"><i class="fa fa-search"></i></a> </h4>
                       <div class="portletcontainer cmdashbordportlet">
                      <ul>
                        <li>                             
                       <%--<span  id="ltlincSanctioned" runat="server">Approved</span>--%>
                       
                      <a href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal" title="Sanctioned"  onclick="ShowIncentive('D');" >Sanctioned<span><asp:Label ID="lblIncsanctioed" runat="server" Text=""></asp:Label></span></a>
                    </li> 
                    
                       <%-- <span  id="ltlincPending" runat="server">Scrutiny</span>--%>
                      
                    
                      <li> <a href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal" title="Scrutiny"  onclick="ShowIncentive('E');" >Pending
                       <span> <asp:Label ID="lblIncpending" runat="server" Text=""></asp:Label></span></a>
                      </li>
                     
                       <%--<span class="bgrejected" id="ltlincRejected" runat="server">Rejected</span>--%>
                      
                      <li> <a href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal" title="Rejected"  onclick="ShowIncentive('F');" >Rejected<span><asp:Label ID="lblIncrejected" runat="server" Text=""></asp:Label></span></a>
                    <%--   <a title="" href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal">45</a></td><td><a title="" href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal">355</a>--%>
                     </li>
                     
                      <%-- <span  id="ltlincApplied" runat="server">Applied</span>--%>
                       
                  
                      <li><a href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal" title="Disbursed"  onclick="ShowIncentive('C');" >Disbursed
                       <span><asp:Label ID="lblIncApplied" runat="server" Text=""></asp:Label></span></a>
                      </li>
                      <%-- <tr><td><span class="bgdisbursed" id="ltlincDistrubed" runat="server">Disbursed</span></td><td>0
                       <%--<a title="" href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal">45</a></td><td><a title="" href="#IncentiveModal" data-toggle="modal" data-target="#IncentiveModal">355</a>
                       </td><td>0</td></tr>--%>
                       </ul>
                       </div>
                         <div class="portletsearch" id="INCENTIVEsearch">
                               <div class="form-group">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Quarterwise</label>
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
                 <label class="col-sm-4">Year </label>
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
                  <asp:Button ID="btnIncentiveSubmit" CssClass="btn btn-success" runat="server" Text="Submit"  onclick="btnIncentiveSubmit_Click" ></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
                       </div></div>
                           <%--Agenda Management System--%>
                                                              <div class="col-sm-6  col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>Agenda Monitoring System <a class="pull-right spmgfilter" data-toggle="tooltip" title="Search" id="AMSfilter"><i class="fa fa-search"></i></a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                     <div class="scroll-prtlet">
                       <div class="table-responsive">
                       <%--<table class="table table-bordered tablefixheight">
                       <tr>
                       <th>Sl#.</th><th>Name of the unit</th><th width="150px">Nodal Person </th><th>Pending Since (Days)</th></tr>
                    <tr><td rowspan="2">1</td><td rowspan="2">ABC</td ><td >Mr./Ms. LMN</td><td>0</td></tr>
                    <tr><td>Mr./Ms. ABC</td><td>0</td></tr>
                    <tr><td rowspan="2">2</td><td rowspan="2">XYZ</td ><td >Mr./Ms. LMN</td><td>0</td></tr>
                    <tr><td>Mr./Ms. ABC</td><td>0</td></tr>
                       </table>--%>
                       
                       <asp:GridView ID="gvAMS" runat="server" CssClass="table table-bordered" AllowPaging="false" OnPreRender="gvAMS_PreRender"
                         AutoGenerateColumns="False" onpageindexchanging="gvAMS_PageIndexChanging"  EmptyDataText="No Record(s) Found" GridLines="None" onrowdatabound="gvAMS_RowDataBound" >
                        <AlternatingRowStyle />
                        <Columns>
                        <asp:TemplateField HeaderText="Sl#">
                    <%--<ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name of the Unit">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("strUnitname") %>'></asp:Label>
                                
                                <asp:HiddenField ID="HiddenField1" Value='<%# Eval("IntProposeid")%>' runat="server"></asp:HiddenField>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nodal Person">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("strNodalPersonName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pending Since(Days)">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("intPendingdays") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
             <PagerStyle CssClass="pagination-grid no-print" HorizontalAlign="Right" /> 
        </asp:GridView>
                      
            </div>
            </div>
           
               
                       </div>
                        <div class="portletsearch" id="AMSsearch">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Range</label>
                  <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlAMS" runat="server">
                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                <asp:ListItem Value="1" Text="0-15 days"></asp:ListItem>
                <asp:ListItem Value="2" Text="15-30 days"></asp:ListItem>
                <asp:ListItem Value="3" Text=">30 days"></asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  </div>
                    
           
                    <div class="form-group">
                  <div class="row">
                 <div class="col-sm-8 col-sm-offset-4">
                  <asp:Button ID="btnAmsSubmit" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnAmsSubmit_Click"></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
                       </div></div>
                      <%--QUERY--%>
                        <div class="col-sm-6 col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4> Query Monitoring (<asp:Label ID="lbl10" runat="server" Text=""></asp:Label>)<a class="pull-right spmgfilter" id="Queryfilter"><i class="fa fa-search"></i></a></h4>
                      <div class="portletcontainer cmdashbordportlet">
                      <div class="scroll-prtlet">
                        <div class="table-responsive">
                       
                       <table class="table table-bordered">
                        <tr><th>Status</th><th>PEAL </th> <th>Services</th> <th>Incentive</th></tr>
                        <tr>
                            <td>Queries Raised</td>
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
                            <td>
                                Queries Responded
                            </td>
                            <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Queries Responded" onclick="ShowPealQuery(2);">
                                    <asp:Label ID="spResolvedpeal" runat="server" Text=""></asp:Label></a>
                            </td>
                            <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Queries Responded" onclick="ShowServiceQuery(2);">
                                    <asp:Label ID="spTotalQueryResponse" runat="server"></asp:Label></a>
                            </td>
                            <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Queries Responded" onclick="ShowIncentiveQuery(2);">
                                    <asp:Label ID="spResolvedIncentive" runat="server"></asp:Label></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Response Pending
                            </td>
                            <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response Pending" onclick="ShowPealQuery(3);">
                                    <asp:Label ID="spPendingpeal" runat="server" Text=""></asp:Label></a>
                            </td>
                            <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response Pending" onclick="ShowServiceQuery(3);">
                                    <asp:Label ID="spTotalQueryPendings" runat="server"></asp:Label></a>
                            </td>
                              <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response Pending" onclick="ShowIncentiveQuery(3);">
                                    <asp:Label ID="spPendingIncentive" runat="server"></asp:Label></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Response not received within Timeline
                            </td>
                            <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response not received within Timeline"  onclick="ShowPealQuery(4);">
                                    <asp:Label ID="spResponcenotRecPeal" runat="server" Text=""></asp:Label></a>
                            </td>
                            <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response not received within Timeline" onclick="ShowServiceQuery(4);">
                                    <asp:Label ID="spNotResponse" runat="server"></asp:Label></a>
                            </td>
                               <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Response not received within Timeline" onclick="ShowIncentiveQuery(4);">
                                    <asp:Label ID="spResponcenotResIncentive" runat="server"></asp:Label></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               Average Day Taken to Raise Query
                            </td>
                            <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Average Time  Taken to Raise Query" onclick="ShowPealQuery(5);">
                                    <asp:Label ID="spAvgTimeQuerypeal" runat="server" Text=""></asp:Label></a>
                            </td>
                            <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Average Time  Taken to Raise Query" onclick="ShowServiceQuery(5);">
                                    <asp:Label ID="spAvgTimeTaken" runat="server"></asp:Label></a>
                            </td>
                            <td align="right">
                                <a href="#Div2" data-toggle="modal" data-target="#Div2" title="Average Time  Taken to Raise Query" onclick="ShowIncentiveQuery(5);">
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
                 <label class="col-sm-4">Year </label>
                  <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlyearquery" runat="server">
              <%--  <asp:ListItem Text="--Select--" Value="0"></asp:ListItem> 
               --%>
                </asp:DropDownList>
                  </div>
                  </div>
                 </div>
                 <div class="form-group" style="display:none">
                   <div class="row">
                 <label class="col-sm-4">Month </label>
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
                  <asp:Button ID="btnQuery" CssClass="btn btn-success" runat="server" Text="Submit"  onclick="btnQuery_Click" ></asp:Button>
                  </div>
                  </div>
                  </div>
                  </div>
                       </div>
                       </div></div>

                
                     <%--Central Inspection Framework--%>
                       <div class="col-sm-6  col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>Central Inspection Framework<a class="pull-right spmgfilter" data-toggle="tooltip" title="Search"  id="CifFilter"><i class="fa fa-search"></i></a></h4>
                     <div class="portletcontainer cmdashbordportlet">
                     <ul>
                     <li><a title="Inspections Completed" href="#CIFModal" data-toggle="modal" data-target="#CIFModal" onclick="ShowCICG('C');">Inspections Completed<span  id="SPcicgcompleted" runat="server"></span></a></li>
                     <li><a title="Inspections Scheduled" href="#CIFModal" data-toggle="modal" data-target="#CIFModal" onclick="ShowCICG('A');">Inspections Scheduled<span  id="SPcicgapplied" runat="server"></span></a></li>
                      
                       <li><a title="Unattended Inspections" href="#CIFModal" data-toggle="modal" data-target="#CIFModal" onclick="ShowCICG('B');">Unattended Inspections<span   id="SPunattInsdash" runat="server"></span></a></li>
                       <li><a title="Inspection reports not uploaded within 48 hours" href="#CIFModal1" data-toggle="modal" data-target="#CIFModal1" onclick="ShowCICG1();">Inspection reports not uploaded within 48 hours<span class="bgdisbursed" id="SPReprtNotUploaded" runat="server"></span></a></li>
                     </ul>
                     </div>
                          <div class="portletsearch" id="CifSearch">
                           <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Department</label>
                  <div class="col-sm-8">
                   <span class="colon">:</span>
                <asp:DropDownList ID="ddldeptCIF" runat="server" CssClass="form-control dpt" >
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
                <asp:DropDownList ID="ddlYearCICG" runat="server" CssClass="form-control dpt" >
                <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  </div>
                  <div class="form-group">
                  <div class="row">
                  <label class="col-sm-4">Month</label>
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
                  <asp:Button ID="btnCICGStatus" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnCICGStatus_Click"></asp:Button>
                  </div>
               
                  </div>
                  </div>
                  </div>
                       </div>
                       </div>
                    

                        <%--LAND ALLOTMENT DETAILS--%>

                          <div class="col-sm-6  col-md-4">
                         <div class="investordashboard-sec incentive-sec ">
                       <h4>Land Allotment Details (<asp:Label ID="lbl9" runat="server" Text=""></asp:Label>)<a class="pull-right landfilter" data-toggle="tooltip" title="Search"  id="landFilter"><i class="fa fa-search"></i></a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                     <ul>
                      <li><a href="#LandModal" data-toggle="modal" data-target="#LandModal" title="Land assessments" onclick="ShowLandDetails(1);">No. of Projects Requiring Land<span  id="spLandAssesment" runat="server"></span></a></li>
                      <li><a href="#LandModal" data-toggle="modal" data-target="#LandModal" title="proposals sent to IDCO for Land Allotment" onclick="ShowLandDetails(2);">Number of proposals sent to <br /> IDCO for Land Allotment<span   id="spPropIDCO" runat="server"></span></a></li>
                       <li><a href="#LandModal" data-toggle="modal" data-target="#LandModal" title="No. of proposal for which land allotted by IDCO" onclick="ShowLandDetails(5);">No. of proposal for which <br /> land allotted by IDCO<span   id="spLandAllotByIDCO" runat="server"></span></a></li>
                      <li><a href="#LandModal" data-toggle="modal" data-target="#LandModal" title="Land allotted by IDCO (in Acres)" onclick="ShowLandDetails(3);">Area of Land allotted <br /> by IDCO (in Acres)<span   id="spLandAllot" runat="server"></span></a></li>
                      <li><a href="#LandModal" data-toggle="modal" data-target="#LandModal" title="No. of Applications where ORTPSA Timelines have exceeded" onclick="ShowLandDetails(4);">No. of Applications where ORTPSA <br /> Timelines have exceeded<span  id="spORTPSALAnd" runat="server" ></span></a></li>
                     </ul>
               <div class="portletsearch" id="landSearch" style="top: -5px;">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Year</label>
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
                  <asp:Button ID="btnLandSubmit" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnLandSubmit_Click"></asp:Button>
                  </div>
                  </div>
                  </div>
                  </div>
                       </div>
                       </div></div>
                        <%--  <div class="col-sm-6  col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>IDCO POST ALLOTMENT APPLICATIONS<a class="pull-right spmgfilter" id="APAAfilter"><i class="fa fa-search"></i></a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                     <ul>
                     <li>Change requests received<span  id="spchngrqstApplied" runat="server"></span></li>
                     <li>Change requests processed<span  id="spchngreqdispose" runat="server"></span></li>
                     <li>Change requests pending to be disposed<span   id="spchngreqPendAtIDCO" runat="server"></span></li>
                     <li><a title="Approvals" href="#APAAModal" data-toggle="modal" data-target="#APAAModal" onclick="ShowAPAA();">Change requests which have crossed >30 days<span class="bgdisbursed" id="spchngReqCrossThirty" runat="server"></span></a></li>
                     </ul>
                     </div>
                          <div class="portletsearch" id="APAAsearch">
                               <div class="form-group">
                  <div class="row">
                  <label class="col-sm-4">Month</label>
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
                 <label class="col-sm-4">Districts</label>
                  <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlAPAADistrict" runat="server">
                <asp:ListItem>--Select--</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  </div>
                 <div class="form-group">
                  <div class="row">
                  <label class="col-sm-4">Year</label>
                   <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlAppaYear" runat="server">
                <asp:ListItem>--Select--</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                  </div>
           
                    <div class="form-group">
                  <div class="row">
                 <div class="col-sm-8 col-sm-offset-4">
                  <asp:Button ID="Button9" CssClass="btn btn-success" runat="server" Text="Submit"></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
             
               
                       </div>
                       </div>--%>
                       <%--   <div class="col-sm-6  col-md-4">
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
                       </div>--%>
                    <%-- <div class="col-sm-6  col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>Year wise investment <a class="pull-right " id="investmentfilter"><i class="fa fa-search"></i></a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                       <div >
                       <a title="Total Proposed Investment" href="#investmentModal" data-toggle="modal" data-target="#investmentModal" onclick="openpoupwincapiatal('HiddenField3');">
                       <h3><i class="fa fa-inr"></i><span><asp:Label ID="lblCapital" runat="server" Text=""></asp:Label></span><medium>Cr.</medium> 
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
                       <%--CSR ACTIVITIES--%>
                    <div class="col-sm-6  col-md-4">
                <div class="investordashboard-sec incentive-sec ">
                <h4>CSR ACTIVITIES (<asp:Label ID="lbl7" runat="server" Text=""></asp:Label>)
                <a class="pull-right spmgfilter" id="linkCSRActivities" data-toggle="tooltip" title="Search"><i class="fa fa-search"></i></a></h4>
                <div class="portletcontainer cmdashbordportlet">
                <ul>
                     <li>Total Project<a href="#csrModal" data-toggle="modal" data-target="#csrModal" title="Projects taken up" onclick="ShowCSR();"><span  id="SPProject" runat="server"></span></a></li>
                    <li>Recommended by Council<span >0</span></li>
                      <li>Total Spending<span ><i class="fa fa-rupee"></i>&nbsp;<label id="SPSpent" runat="server"></label>&nbsp; Cr.</span></li> 
                     </ul>
                 <div class="portletsearch" id="CSRActivities" style="top: -5px;">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">District</label>
                  <div class="col-sm-8">
                    <span class="colon">:</span>
               <%-- <asp:DropDownList CssClass="form-control" ID="ddlDistrict" runat="server">
                <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>--%>
                <div class="listcontrol checkbox form-control">
                 <asp:CheckBox ID="chkCSRDistrctheader" runat="server" Checked="true" onclick="setCBLCSRDist(this)" Text="Check All"></asp:CheckBox>
                               <asp:CheckBoxList ID="chkCSRDistrct" CssClass="" runat="server" >
                    </asp:CheckBoxList>
                    <asp:HiddenField ID="hdnCSRDistrct" runat="server"></asp:HiddenField>
                    </div>
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
                  <asp:Button ID="btnCSRStatus" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnCSRStatus_Click" OnClientClick="return CSRDist();" ></asp:Button>
                  </div>
               
                  </div>
                  
                  </div>
                  </div>
               
                       </div>
                       </div>
                       </div>
                        <%-- <div class="col-sm-6  col-md-4">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>SPMG <a class="pull-right spmgfilter" id="linkSpmg"><i class="fa fa-search"></i></a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                  <ul>
                      <li>Issues Received<span  id="spmgraised" runat="server"></span></li>
                      <li>Issues Resolved<span   id="spmgresolved" runat="server"></span></li>
                       <li>Issues Pending<span   id="spmgpending" runat="server"></span></li>
                         <li><a title="Total Proposed Employment" href="#SPMGModal" data-toggle="modal" data-target="#SPMGModal">Issues Pending > 30 days <span  id="spmg30pending" runat="server" ></span></a></li>
                     </ul>
               <div class="portletsearch" id="SpmgContent" style="top: -5px;">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Year</label>
                  <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlspmgyear" runat="server">
                <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                       </div></div>--%>
                   
                 <div class="col-sm-6  col-md-4" style="display:none">
                       <div class="investordashboard-sec incentive-sec ">
                       <h4>Single Window Application Status<a class="pull-right ppfilter" id="ppfilter" data-toggle="tooltip" title="Search" ><i class="fa fa-search"></i></a></h4>
                       <div class="portletcontainer cmdashbordportlet">
                     <ul>
                     <li><%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending ">--%>Applied<span >0</span><%--</a>--%></li>
                     <li><%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending ">--%>Approved<span >0</span><%--</a>--%></li>
                     <li><%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending ">--%>Approved, yet to commence operations<span >0</span><%--</a>--%></li>
                     <li><%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending ">--%>Deferred<span class="bgdisbursed">0</span><%--</a>--%></li>
                     <li><%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending" >--%>Rejected<span class="bgrejected">0</span><%--</a>--%></li>  
                     <li><%--<a href="#PpModal" data-toggle="modal" data-target="#PpModal"  title="Issues Pending" >--%>Under Evaluation<span >0</span><%--</a>--%></li>  
                     </ul>
                     </div>
                          <div class="portletsearch" id="ppsearch">
                  <div class="form-group">
                  <div class="row">
                 <label class="col-sm-4">Year</label>
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
                  <label class="col-sm-4">Value</label>
                   <div class="col-sm-8">
                  <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="DropDownList2" runat="server">
                <asp:ListItem>--Select--</asp:ListItem> <asp:ListItem> < 50 crore</asp:ListItem><asp:ListItem> > 50 crore </asp:ListItem>
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
                     </div>
                     </div>
                     

                  
                  </div>
               </div>
            </section>
                <!-- /.content -->
                <!--Modal Popups -->
                 <div id="LandModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Land Allotment Details in <asp:Label ID="lbldet10" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                  <iframe name="myIframeLand" id="myIframeLand" width="100%" style="height: 298px" runat="server">
                                </iframe>
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
                                   Department wise approval details</h4>
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
                <div id="investmentModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Year wise investment Details in
                                    <asp:Label ID="lblYearinv" runat="server" Text=""></asp:Label></h4>
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
                                    Year wise employment Details  in  <asp:Label ID="lblemp" runat="server" Text=""></asp:Label></h4>
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
                                    Department Wise Approvals Details</h4>
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
                                    CSR Details in 
                                    <asp:Label ID="lblDet5" runat="server" Text=""></asp:Label></h4>
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
                <div id="SPMGModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    STATE PROJECT MONITORING GROUP Details</h4>
                            </div>
                            <div class="modal-body">
                                <%--<table class="table table-bordered">
                                    <tr>
                                        <th width="40px">
                                            Sl#.
                                        </th>
                                        <th>
                                            Name of the unit
                                        </th>
                                        <th>
                                            Department
                                        </th>
                                        <th>
                                            Type of the issues
                                        </th>
                                        <th>
                                            Since the receipt of the issue(in days)
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
                                            acb
                                        </td>
                                        <td>
                                            Type of issue
                                        </td>
                                        <td>
                                            10
                                        </td>
                                    </tr>
                                </table>--%>
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
                                <h4 class="modal-title">
                                    Incentive Details in <asp:Label ID="lblDet6" runat="server" Text=""></asp:Label></h4>
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
                                    Single Window Application Status Details in 
                                    <asp:Label ID="lbldet1" runat="server" Text=""></asp:Label></h4>
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
                   <iframe name="IframeQueryService" id="IframeQuery" width="100%" style="height: 298px" runat="server">
                        </iframe>
                      </div> 
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
        function ShowService(serviceStatus) {
            var Year = $('#ContentPlaceHolder1_ddlserviceyear').val();
            var Month = $('#ContentPlaceHolder1_ddlServcMonth').val();
            var District = $('#ContentPlaceHolder1_ddlServcDistrict').val();
            document.getElementById('ContentPlaceHolder1_myservcIframe').src = "ApprovalTimelineGM.aspx?Dist=" + District + "&Year=" + Year + "&Month=" + Month + "&serviceStatus=" + serviceStatus;
        }
        //modifiy by nibedita behera for multiple district on 01-11-2017
        function ShowCSR() {
            var dist = "";
            if ($('#ContentPlaceHolder1_hdnCSRDistrct').val() != "") {
                dist = $('#ContentPlaceHolder1_hdnCSRDistrct').val();
            }
            else {
                dist = "0";
            }
            var year = $('#ContentPlaceHolder1_ddlCSRYear').val();
            document.getElementById('ContentPlaceHolder1_myCSRIframe').src = "../../CSRGridStatus.aspx?dist=" + dist + "&year=" + year;
        }
        //added by nibedita behera on 01-11-17 for multiple district
        function setCBLCSRDist(sender) {
            var cbl = document.getElementById('<%=chkCSRDistrct.ClientID %>').getElementsByTagName("input");
            for (i = 0; i < cbl.length; i++) cbl[i].checked = sender.checked;
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
//        function ShowQuery() {
//            var Dept = $('#ContentPlaceHolder1_ddlMonthQuery').val();
//            document.getElementById('ContentPlaceHolder1_IframeQuery').src = "QueryPendingStatus.aspx?Month=" + Dept;
        //        }

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
        function ShowLandDetails(Status) {
            var e = document.getElementById("ContentPlaceHolder1_ddlLandFinYear");
            var Year = e.options[e.selectedIndex].value;
            var Type = "";
            var Dist = "";
            document.getElementById('ContentPlaceHolder1_myIframeLand').src = "DashboardLandDetails.aspx?Status=" + Status + "&Year=" + Year + "&Type=" + 1 + "&Dist=" + 0;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
