<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormPreview.aspx.cs" Inherits="incentives_PatentRegistration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });

            $('.Pioneersec,.attorneysec,.adhardetails').hide();
            $(".applyby").on("click", function () {
                if ($("input:checked").val() == 'Self') {
                    $('.adhardetails').show();
                    $('.attorneysec').hide();
                }
                else {
                    $('.attorneysec').show();
                    $('.adhardetails').hide();
                }
            });
            $(".optradioPriority").on("click", function () {
                if ($("input:checked").val() == 'Yes') {


                    $('.Pioneersec').show();

                }
                else {

                    $('.Pioneersec').hide();
                }
            });
        });
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
                <div class="investrs-tab">
                    <ul class="nav nav-pills">
                        <li class="menudashboard"><a href="javascript:void(0)"><i class="fa fa-tachometer"></i>
                            Dashboard</a> </li>
                        <li class="menuprofile"><a href="../InvesterProfile.aspx"><i class="fa fa-user"></i>
                            Investor Profile</a> </li>
                        <li class="menuproposal"><a href="../Proposals.aspx"><i class="fa fa-briefcase"></i>
                            Proposals</a> </li>
                        <li class="menuservices"><a href="../DepartmentClearance.aspx"><i class="fa fa-wrench">
                        </i>Services</a> </li>
                        <%--  <li><a href="IncentiveCalculator.aspx">Incentive Calculator</a> </li>--%>
                        <li class="menuincentive"><a href="incentiveoffered.aspx"><i class="fa fa-money"></i>
                            Incentive</a> </li>
                    </ul>
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">
                            <div class="innertabs">
                                <ul class="nav nav-pills pull-right">
                                    <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                    <li class="active"><a href="appliedindustrylist.aspx">Apply For incentive</a></li>
                                    <li><a href="javscript:void(0);">View Application Status</a></li>
                                </ul>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-header">
                                <div class="iconsdiv">
                                    <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                        <i class="fa fa-print"></i></a><a href="javascript:void(0);" title="Export to Excel"
                                            id="A1" class="pull-right printbtn"><i class="fa fa-file-excel-o"></i></a>
                                </div>
                                <h2>
                                    Application For Providing assistance on Patent Registration</h2>
                            </div>
                            <div class="form-body">
                                <div class="incentivepreiview">
                                    <div class="preiviewheader text-center">
                                        <img src="../images/barcode.jpg" alt="bar-code" />
                                        <h4>
                                            APPLICATION FOR SANCTION AND DISBURSEMENT OF ASSISTANCE ON PATENT & INTELLECTUAL
                                            PROPERTY RIGHTS UNDER PROVISIONS OF INDUSTRIAL POLICY RESOLUTION 2015</h4>
                                        <p>
                                            Application received after the due date / incomplete in any respect shalt be tiable
                                            for rejection)<br />
                                            (Strike out whichever is not applicable)</p>
                                    </div>
                                    <div class="prieviewdatasec">
                                        <h4>
                                            From</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                M/s :<asp:Label ID="Label1" runat="server" Text="Bikash Kumar Panda"></asp:Label></p>
                                            <p>
                                                At :<asp:Label ID="Label3" runat="server" Text="Khurda"></asp:Label></p>
                                            <p>
                                                PO :<asp:Label ID="Label2" runat="server" Text="Khurda"></asp:Label></p>
                                            <p>
                                                Sub-Division :<asp:Label ID="Label4" runat="server" Text="Jatni"></asp:Label></p>
                                            <p>
                                                Dist.(Location of the lndustrial Unit) :<asp:Label ID="Label5" runat="server" Text="Khurda"></asp:Label></p>
                                        </div>
                                        <h4>
                                            To</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                The General Manager, Regional lndustries Centre / District lndustries Centre
                                                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></p>
                                        </div>
                                        <h4>
                                            Sub :
                                        </h4>
                                        <div class="padding-left-20">
                                            <p>
                                                sanction & disbursement of assistance on Patent & lntellectual property Rights under
                                                provisions of lndustrial Policy Resolution 2015.
                                            </p>
                                        </div>
                                        <h4>
                                            Sir,</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                ln accordance with the provisions laid down in lndustrial policy Resolution - 2015
                                                and operational guidelines, the claim for assistance on patent & intellectual property
                                                rights is submitted herewith with following particulars.
                                            </p>
                                        </div>
                                    </div>
                                    <div class="prievewdynamicdata">
                                        <div class="panel-group padding-20" id="accordion" role="tablist" aria-multiselectable="true">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingOne">
                                                    <h4 class="panel-title">
                                                        <a>Industrial Unit's Details </a>
                                                    </h4>
                                                </div>
                                                <div id="collapseOne" class="panel-collapsein" role="tabpanel" aria-labelledby="headingOne">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Name of Enterprise/Industrial Unit</label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span><asp:Label ID="Label11" CssClass="dataspan" runat="server"
                                                                        Text="JRD Farma &amp; Research"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Organization Type</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label12" CssClass="dataspan" runat="server" Text="Proprietorship"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Name of Applicant</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label13" CssClass="dataspan" runat="server" Text="Bikash kumar Panda"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Application Applying By</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label14" CssClass="dataspan" runat="server" Text="Self"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Address of Industrial Unit</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label15" CssClass="dataspan" runat="server" Text="Khurda"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Unit Category</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label16" CssClass="dataspan" runat="server" Text="Micro"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Unit Type</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label17" CssClass="dataspan" runat="server" Text="Existing E/M/D"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Is Priority</label>
                                                                <div class="col-sm-3 ">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label18" CssClass="dataspan" runat="server" Text="No"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Address of Registered Office of the Industrial Unit</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label19" CssClass="dataspan" runat="server" Text="Khurda"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Name of Managing Partner</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label20" CssClass="dataspan" runat="server" Text="Mr. Bikash Kumar Panda"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    EIN/ IEM/ IL No.</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label21" CssClass="dataspan" runat="server" Text="1234-5678-1234"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Date of EIN/ IEM/ IL Date</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label22" CssClass="dataspan" runat="server" Text="25-07-1990"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    PC No.</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label23" CssClass="dataspan" runat="server" Text="1234-5678-1234"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    PC Issuance Date</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label24" CssClass="dataspan" runat="server" Text="25-07-1990"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3">
                                                                    Date of Commencement of Production</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label25" CssClass="dataspan" runat="server" Text="25-07-1990"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingTwo">
                                                    <h4 class="panel-title">
                                                        <a>Production & Employment Details</a>
                                                    </h4>
                                                </div>
                                                <div id="PromoterInformation" class="panel-collapse in" role="tabpanel" aria-labelledby="headingTwo">
                                                    <div class="panel-body">
                                                        <p class="text-red text-right">
                                                            All Amouts to be Entered in INR(Exact Amount)</p>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th>
                                                                                Sl#
                                                                            </th>
                                                                            <th>
                                                                                Product/Service Name
                                                                            </th>
                                                                            <th>
                                                                                Quantity
                                                                            </th>
                                                                            <th>
                                                                                Units
                                                                            </th>
                                                                            <th>
                                                                                Value
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                1
                                                                            </td>
                                                                            <td>
                                                                                Steel pipes
                                                                            </td>
                                                                            <td>
                                                                                1000
                                                                            </td>
                                                                            <td>
                                                                                Kg.
                                                                            </td>
                                                                            <td>
                                                                                50 Lakhs
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <h4 class="h4-header">
                                                            Employment Generated</h4>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3 ">
                                                                    Direct Empolyment IN NUMBERS<small>(on Company Payroll)</small>
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label26" CssClass="dataspan" runat="server" Text="45"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-3 ">
                                                                    Contractual Employment IN NUMBERS</label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label27" CssClass="dataspan" runat="server" Text="60"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th>
                                                                                Type
                                                                            </th>
                                                                            <th>
                                                                                Current
                                                                            </th>
                                                                            <th>
                                                                                Proposed
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Managerial
                                                                            </td>
                                                                            <td class=" text-right">
                                                                                10
                                                                            </td>
                                                                            <td class=" text-right">
                                                                                10
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Supervisory
                                                                            </td>
                                                                            <td class=" text-right">
                                                                                23
                                                                            </td>
                                                                            <td class=" text-right">
                                                                                23
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Skilled
                                                                            </td>
                                                                            <td class=" text-right">
                                                                                42
                                                                            </td>
                                                                            <td class=" text-right">
                                                                                42
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Semi-Skilled
                                                                            </td>
                                                                            <td class=" text-right">
                                                                                44
                                                                            </td>
                                                                            <td class=" text-right">
                                                                                44
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Unskilled
                                                                            </td>
                                                                            <td class=" text-right">
                                                                                102
                                                                            </td>
                                                                            <td class=" text-right">
                                                                                102
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                TOTAL
                                                                            </td>
                                                                            <td class=" text-right">
                                                                                221
                                                                            </td>
                                                                            <td class="text-right">
                                                                                221
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingThree">
                                                    <h4 class="panel-title">
                                                        <a>Investment Details </a>
                                                    </h4>
                                                </div>
                                                <div id="IndustryDetails" class="panel-collapsein" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-6 ">
                                                                    Date of First Fixed Capital Investment <small>(for land/Building/plant and machinery
                                                                        & Balancing Equipment)</small></label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Label28" CssClass="dataspan" runat="server" Text="03-Jan-2017"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Total Capital Investment</label>
                                                                <div class="col-sm-12">
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th>
                                                                                Sl #
                                                                            </th>
                                                                            <th>
                                                                                Investment Head
                                                                            </th>
                                                                            <th>
                                                                                Interest Amount
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                1
                                                                            </td>
                                                                            <td>
                                                                                Own ancestoral land
                                                                            </td>
                                                                            <td class="text-right">
                                                                                45.6
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                2
                                                                            </td>
                                                                            <td>
                                                                                Building
                                                                            </td>
                                                                            <td class="text-right">
                                                                                45.6
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                3
                                                                            </td>
                                                                            <td>
                                                                                Plant & Machinery
                                                                            </td>
                                                                            <td class="text-right">
                                                                                45.6
                                                                            </td>
                                                                            <tr>
                                                                                <td>
                                                                                    4
                                                                                </td>
                                                                                <td>
                                                                                    Balancing Equipment
                                                                                </td>
                                                                                <td class="text-right">
                                                                                    45.6
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    5
                                                                                </td>
                                                                                <td>
                                                                                    Other Fixed Assests
                                                                                </td>
                                                                                <td class="text-right">
                                                                                    45.6
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <strong>Total</strong>
                                                                                </td>
                                                                                <td class="text-right">
                                                                                    <strong>365.7</strong>
                                                                                </td>
                                                                            </tr>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <h4 class="h4-header">
                                                            MEANS OF FINANCE</h4>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Term Loan Details</label>
                                                                <div class="col-sm-12">
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th rowspan="2" width="40">
                                                                                Sl #
                                                                            </th>
                                                                            <th rowspan="2">
                                                                                Name of Financial Institution
                                                                            </th>
                                                                            <th colspan="2">
                                                                                Location
                                                                            </th>
                                                                            <th rowspan="2">
                                                                                Term Loan Amount
                                                                            </th>
                                                                            <th rowspan="2">
                                                                                Sanction Date
                                                                            </th>
                                                                            <th rowspan="2">
                                                                                Availed Amount
                                                                            </th>
                                                                            <th rowspan="2">
                                                                                Availed Date
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>
                                                                                State
                                                                            </th>
                                                                            <th>
                                                                                City
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                1
                                                                            </td>
                                                                            <td>
                                                                                SBI
                                                                            </td>
                                                                            <td>
                                                                                Bhubaneswar
                                                                            </td>
                                                                            <td>
                                                                                Mancheswar
                                                                            </td>
                                                                            <td>
                                                                                500000
                                                                            </td>
                                                                            <td>
                                                                                12/31/2015
                                                                            </td>
                                                                            <td>
                                                                                300000
                                                                            </td>
                                                                            <td>
                                                                                05/01/2016
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div5">
                                                    <h4 class="panel-title">
                                                        <a></i>Patent Details </a>
                                                    </h4>
                                                </div>
                                                <div id="PatentDetails" class="panel-collapse in" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Patented Items or Processes /Intellectual Property Right Details</label>
                                                                <div class="col-sm-12">
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th>
                                                                                Item/Process/IPR Name
                                                                            </th>
                                                                            <th>
                                                                                Name of Authority
                                                                            </th>
                                                                            <th>
                                                                                Date of Commercial Use
                                                                            </th>
                                                                            <th>
                                                                                Patent/IPR Registration No.
                                                                            </th>
                                                                            <th>
                                                                                Registration Date
                                                                            </th>
                                                                            <th>
                                                                                Expenditure incurred
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                1177666
                                                                            </td>
                                                                            <td>
                                                                                Patients of India
                                                                            </td>
                                                                            <td>
                                                                                05-May-2017
                                                                            </td>
                                                                            <td>
                                                                                05-May-2017
                                                                            </td>
                                                                            <td>
                                                                                05-May-2017
                                                                            </td>
                                                                            <td>
                                                                                90,000/-
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <h4 class="h4-header">
                                                            MEANS OF FINANCE FOR PATENT REGISTRATION</h4>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Loan Details</label>
                                                                <div class="col-sm-12">
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th>
                                                                                Sl #
                                                                            </th>
                                                                            <th>
                                                                                Name of Financial Institution
                                                                            </th>
                                                                            <th>
                                                                                Amount Availed
                                                                            </th>
                                                                            <th>
                                                                                Amount Avaialed Date
                                                                            </th>
                                                                            <th>
                                                                                Loan Number
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                1
                                                                            </td>
                                                                            <td>
                                                                                HDFC,Bhubaneswar
                                                                            </td>
                                                                            <td>
                                                                                60,000/-
                                                                            </td>
                                                                            <td>
                                                                                04-Apr-2017
                                                                            </td>
                                                                            <td>
                                                                                LV-55566
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div4">
                                                    <h4 class="panel-title">
                                                        <a>Availed Details</a>
                                                    </h4>
                                                </div>
                                                <div id="AvailedClaimDetails" class="panel-collapse" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12">
                                                                    Assistance details for Patent/IPR incentives already availed by this enterprise
                                                                </label>
                                                                <div class="col-sm-12  margin-bottom10">
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th>
                                                                                Sl#
                                                                            </th>
                                                                            <th>
                                                                                Body (Pvt, State Govt (Specify State),GoI)
                                                                            </th>
                                                                            <th>
                                                                                Name of Financial Institution
                                                                            </th>
                                                                            <th>
                                                                                Amount Availed
                                                                            </th>
                                                                            <th>
                                                                                Availed Date
                                                                            </th>
                                                                            <th>
                                                                                Sanction Order no.
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                1
                                                                            </td>
                                                                            <td>
                                                                                Govt. Of Jharkhand
                                                                            </td>
                                                                            <td>
                                                                                SBI, Ranchi
                                                                            </td>
                                                                            <td>
                                                                                75,000/-
                                                                            </td>
                                                                            <td>
                                                                                2-Sept-2016
                                                                            </td>
                                                                            <td>
                                                                                SN-25222
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div1">
                                                    <h4 class="panel-title">
                                                        <a class="tect-center">Document CheckList</a>
                                                    </h4>
                                                </div>
                                                <div class="panel-body">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>
                                                                Document Name
                                                            </th>
                                                            <th width="150px">
                                                                View
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Document(s) in support of rehabilitated sick industrial unit treated at par with
                                                                new industrial unit and duly recommended by State Level lnter lnstitutional Committee
                                                                (SLllC) for this incentive
                                                            </td>
                                                            <td>
                                                                <a id="LinkButton10" target="_blank" data-toggle="View Documen" title="Upload" runat="server">
                                                                    View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Documeht(s) in support of lndustrial unit seized under Section 29 of the State Financial
                                                                Corporation Act,1951/ SARFAESI Ac|,2002 and thereafter sold to a new entrepreneur
                                                                on sale of assets basis and treated as new industrial unit forthe purpose of this
                                                                IPR
                                                            </td>
                                                            <td>
                                                                <a id="A2" target="_blank" data-toggle="View Documen" title="Upload" runat="server">
                                                                    View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Certificate of registration under lndian Partnership Act1932 / Societies Registration
                                                                Act- 1860 / Certificate of incorporation (Memorandum of association & Article of
                                                                Association ) under Company Act1956
                                                            </td>
                                                            <td>
                                                                <a target="_blank" data-toggle="View Documen" title="Upload" runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Certificate on Date of Commencement of production
                                                            </td>
                                                            <td>
                                                                <a target="_blank" data-toggle="View Documen" title="Upload" runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Document in Support of Number of EMployyes shown as directly employed (e.g. Certificate
                                                                by DLO)- this certificate has to be taken
                                                            </td>
                                                            <td>
                                                                <a id="A4" target="_blank" data-toggle="View Documen" title="Upload" runat="server">
                                                                    View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Document in support of date of first investment in fixed capital i.e. land i building
                                                                / plant & machinery and balancing equipment
                                                            </td>
                                                            <td>
                                                                <a target="_blank" data-toggle="View Documen" title="Upload" runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Term loan sanction order of Financial lnstitute (s) / Banks
                                                            </td>
                                                            <td>
                                                                <a target="_blank" data-toggle="View Documen" title="Upload" runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Undertaking on non-availment of subsidy earlier on this project
                                                            </td>
                                                            <td>
                                                                <a target="_blank" data-toggle="View Documen" title="Upload" runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Details of assistance sanctioned / availed so far with sanction order no & date
                                                                and other supporting documents from State Govt / Central Govt / Govt. Agencies /
                                                                Financial lnstituttions
                                                            </td>
                                                            <td>
                                                                <a target="_blank" data-toggle="View Documen" title="Upload" runat="server">View Document</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="Div2">
                                                    <h4 class="panel-title">
                                                        <a class="text-center">Undertaking</a>
                                                    </h4>
                                                </div>
                                                <div class="preiviewfooter padding-20">
                                                    <p>
                                                        I ,Sri
                                                        <asp:Label ID="Label7" runat="server" Text="Bikash Kumar Panda"></asp:Label>s/o
                                                        <asp:Label ID="Label8" runat="server" Text="Chittaranjan Panda"></asp:Label>
                                                        at present
                                                        <asp:Label ID="Label9" runat="server" Text="General Manager"></asp:Label>
                                                        (designation) of M/S
                                                        <asp:Label ID="Label10" runat="server" Text="Apparel"></asp:Label>
                                                        (name of the industrial unit) certify that the information furnished as above is
                                                        true and correct to the best of my knowledge and belief.</p>
                                                    <p>
                                                        I hereby undertake to abide by the terms and conditions prescribed under the provisions
                                                        of Odisha lndustrial Policy 2015 and its operational guidelines.</p>
                                                    <p>
                                                        I hereby undertake to repay the assistance amount or any part thereof with penal
                                                        interest as diecided by the authority-</p>
                                                    <ol>
                                                        <li>lf the information furnished is found to be false / incorrect i misleading or misrepresented
                                                            and there has been suppression of facts / materials or disbursed in excess of the
                                                            amount actually admissible for whatsoever reason.</li>
                                                        <li>lf the patent and intellectual property right registered is revoked by the authority
                                                            for any reason within five years of registration.</li>
                                                    </ol>
                                                    <p>
                                                        I hereby certify that this industrial unit has not applied / sanctioned / availed
                                                        any amount of assistance under any other scheme of the State Govt. or the Central
                                                        Govt. or any</p>
                                                    <p>
                                                        Financial lnstitution(s) / Support organization in the country and abroad against
                                                        which the present claim is made.</p>
                                                    <div class="col-sm-4 ">
                                                    </div>
                                                    <div class="col-sm-2 ">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        Signature of the Proprietor / Managing Partner / Managing Director / Authorized
                                                        Signatory in full and behalf of
                                                        <br />
                                                        M/s :<img src="../images/signaturesample.png" /><br />
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-2">
                                                                    Upload</label>
                                                                <div class="col-sm-6">
                                                                    <asp:FileUpload CssClass="form-control" ID="FileUpload1" runat="server" /></div>
                                                            </div>
                                                        </div>
                                                        Date: <b>6-Sept-2017</b>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-footer">
                                <div class="row">
                                    <div class="col-sm-12 text-right">
                                        <div id="myModal2" class="modal fade" role="dialog">
                                            <div class="modal-dialog modal-md">
                                                <!-- Modal content-->
                                                <div class="modal-content">
                                                    <div class="modal-body text-center">
                                                        <div class="form-group">
                                                            <h4 class="text-success">
                                                               Thanks  for submiting your application </h4>
                                                               <p>Your Application no. : <b>App1234567</b></p>
                                                                <p>Expected First response time : <b>7 Days </b></p>
                                                            <p>Maximum eligible incentive : <b><i class="fa fa-inr"></i> 75,000/-</b></p>
                                                            <p class="text-red"><i> * This is an indicative value Disbursement amount may be lesser depending upon application details scrutiny.</i></p>
                                                            <a class="btn btn-success" href="ViewApplicationStatus.aspx">OK</a>

                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <a class="btn btn-warning">Save as Draft</a> <a class="btn btn-success" data-toggle="modal"
                                            data-target="#myModal2">Apply</a>
                                    </div>
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
    <script type="text/javascript">
         $(function () {
             $('.datePicker').datepicker({
                 dateFormat: 'dd:mm:yyyy',
                 separator: ' @ ',
                 minDate: new Date(),autoclose: true,
             });
         });
    </script>
    </form>
</body>
</html>
