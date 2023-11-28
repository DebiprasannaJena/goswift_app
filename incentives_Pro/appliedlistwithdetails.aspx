<%@ Page Language="C#" AutoEventWireup="true" CodeFile="appliedlistwithdetails.aspx.cs"
    Inherits="incentives_incentiveoffered" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
            $('#checkfalse').click(function () {
                alert("hii");
            });
            $('#checktrue').click(function () {
                alert("hii");
            })
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
                              <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                            </ul>
                            <div class="clearfix">
                            </div>
                        </div>
                        <div class="form-header">
                            <div class="iconsdiv">
                                <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                    <i class="fa fa-print"></i></a><a href="javascript:void(0);" title="Export to Excel"
                                        id="A1" class="pull-right printbtn"><i class="fa fa-file-excel-o"></i></a>
                                <a href="javascript:void(0);" title="Delete" id="A2" class="pull-right printbtn"><i
                                    class="fa fa-trash-o"></i></a>
                            </div>
                            <h2>
                                Your Units
                            </h2>
                        </div>
                        <div class="form-body">
                           
                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingTwo">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#PromoterInformation" aria-expanded="true" aria-controls="collapseTwo"><i class="more-less fa  fa-minus">
                                                </i>Eligible Summary </a>
                                        </h4>
                                    </div>
                                    <div id="PromoterInformation" class="panel-collapse collapse in" role="tabpanel"
                                        aria-labelledby="headingTwo">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Industry Code</label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="Label1" runat="server" CssClass="dataspan" Text="Label">12-56-23-56-99999</asp:Label>
                                                       
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Unit/Enterprise Name</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                         <asp:Label ID="Label2" runat="server" CssClass="dataspan" Text="Label">JRD Farma &amp; Research</asp:Label>
                                                      </div>
                                                </div>
                                            </div>
                                            <div>
                                            <div class="criteriadiv">
                                            <h4>Criteria Provided</h4>
                                             <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-3">
                                                       Sector </label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="Label3" runat="server" CssClass="dataspan" Text="Label">Apparel</asp:Label>
                                                       
                                                    </div>
                                                    <label class="col-sm-3">
                                                         Date of FFCI </label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                         <asp:Label ID="Label5" runat="server" CssClass="dataspan" Text="Label">15-Aug-2016</asp:Label>
                                                      </div>
                                                </div>
                                            </div>
                                             <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-3">
                                                        Date of production Commencement </label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="Label8" runat="server" CssClass="dataspan" Text="Label">15-Aug-2016</asp:Label>
                                                       
                                                    </div>
                                                    <label class="col-sm-3">
                                                        Total Capital Investment</label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                         <asp:Label ID="Label9" runat="server" CssClass="dataspan" Text="Label">10 Cr.</asp:Label>
                                                      </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-3">
                                                        Investment In Palnt Machinery</label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="Label10" runat="server" CssClass="dataspan" Text="Label">6 Cr.</asp:Label>
                                                       
                                                    </div>
                                                   
                                                </div>
                                            </div>
                                            </div>
                                            <div class="criteriadiv derivecon ">
                                            <h4> Derived Unit Details</h4>
                                         <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-3">
                                                       Unit Type </label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="Label6" runat="server" CssClass="dataspan" Text="Label">Existibg (E/M/D)</asp:Label>
                                                       
                                                    </div>
                                                    <label class="col-sm-3">
                                                         District category </label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                         <asp:Label ID="Label7" runat="server" CssClass="dataspan" Text="Label">B</asp:Label>
                                                      </div>
                                                </div>
                                            </div>
                                      
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-3">
                                                         Employment & Investment Rating</label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="Label13" runat="server" CssClass="dataspan" Text="Label">B2</asp:Label>
                                                       
                                                    </div>
                                                   
                                                </div>
                                            </div>
                                           

                                            </div>
                                             <div class="criteriadiv derivecon derivecon2">

                                            <h4> Incentives Eligible for Under Ploicies</h4>
                                         <table class="table table-bordered">
                                         <tr><th width="30%">Eligible For</th><th width="70%">Not Eligible For</th></tr>
                                         <tr><td rowspan="7">
                                         <ul>
                                         <li>IPR 2015</li>
                                         <li>Apparel Policy 2016</li>
                                         </ul>
                                         </td>
                                         
                                         <td>
                                        <table class="table table-bordered">
                                        <tr><th>Policy Name</th><th>Reason for Ineligibility</th></tr>
                                        <tr><td><p class="notdone">MSME</p></td><td> You ara a large unit</td></tr>
                                       
                                        <tr><td><p class="notdone">Biotechnology Policy 2016</p></td><td> Sector Mismatch</td></tr>
                                        <tr><td><p class="notdone"> Health Care Investment Promotion</p></td><td>Sector Mismatch</td></tr>
                                        <tr><td><p class="notdone"> Odisha Fisheries Policy 2015</p></td><td>Sector Mismatch</td></tr>
                                        <tr><td><p class="notdone">Odisha Food Processing Policy, 2016</p></td><td> Sector Mismatch</td></tr>
                                        <tr><td><p class="notdone">Odisha Startup Policy, 2016</p></td><td>You ara a large unit</td></tr>
                                        <tr><td><p class="notdone"> Pharmaceuticals Policy 2016</p></td><td>Sector Mismatch</td></tr>
                                      
                                        </table>
                                         </td>
                                         
                                         </tr>
                                         </table>

                                            </div>
                                             
                                            </div>
                                            <div>
                                            </div>
                                    
                                        </div>
                                    </div>
                                
                                    
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingThree">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#IndustryDetails" aria-expanded="true" aria-controls="collapseTwo"><i class="more-less fa  fa-minus">
                                                </i>List of Eligibility Incentives </a>
                                        </h4>
                                    </div>
                                    <div id="IndustryDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="details-section">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                    Incentive
                                                                </th>
                                                                <th>
                                                                    Policy Name
                                                                </th>
                                                                <th>
                                                                    Incentive Disbursement type
                                                                </th>
                                                                <th>
                                                                    Incentive Availment type
                                                                </th>
                                                                <th>
                                                                    Incentive Nature
                                                                </th>
                                                                <th>
                                                                    Action
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Interest subsidy
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    New
                                                                </td>
                                                                <td>
                                                                    Pre Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a data-toggle="modal" data-target="#myModal">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Patent Registration
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    New
                                                                </td>
                                                                <td>
                                                                    Pre Production
                                                                </td>
                                                                 <td>
                                                                  Non-Fiscal
                                                                </td>
                                                                <td>
                                                                   <a href="PatentRegistration.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Subsidy for Plant &amp; Machinery
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="Subsidy_Plant_MC.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Stamp Duty Exemption
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    New
                                                                </td>
                                                                <td>
                                                                    Pre Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="Stamp_Duty_Exemption.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Technical Know How
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    New
                                                                </td>
                                                                <td>
                                                                    Pre Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="TechnicalKnowhow.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Employment Cost Subsidy
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="EmployeementCostSubsidy.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Enterpreneurship Subsidy
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    New
                                                                </td>
                                                                <td>
                                                                    Pre Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                             <%--    <a href="Enterpreneurship_Subsidy.aspx">Apply</a>--%>
                                                                    <p data-toggle="tooltip" class="margin-bottom0" title="Please update Course Duration & institute name in the additional details  profile sector.">Missins Information !</p><a href="unitdetails.aspx" class="text-red"> Upload Profile</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    One time reimbursement of energy audit cost under IPR 2015
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    New
                                                                </td>
                                                                <td>
                                                                    Pre Production
                                                                </td>
                                                                 <td>
                                                                  Non-Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="Reimbursementofenergyaudit.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Exemption Premium Land Conversion
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="Exemption_Premium_Land_Conversion.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Quality certification
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="Quality_Certification.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <%--<tr><td>Employement Rating</td><td>Existing</td><td>Post Production</td><td><a href="EmploymentRating.aspx">Apply</a></td></tr>
                                   <tr><td>Category of Districts</td><td>Existing</td><td>Post Production</td><td><a href="District.aspx">Apply</a></td></tr>
                                   <tr><td>Classification of Industry</td><td>Existing</td><td>Post Production</td><td><a href="ClassificationofIndustry.aspx">Apply</a></td></tr>--%>
                                                            <tr>
                                                                <td>
                                                                    Power
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a data-toggle="modal" data-target="#Power_Modal_1">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Training Subsidy
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="TrainingSubsidy.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Land for Workers Hostels
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="LandforWorkersHostels.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <%--     <tr>
                                                                <td>
                                                                    Application For Capital Grant To Support Quality Infrastructure
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                <td>
                                                                    <a href="CapitalgranttosupportQinf.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Application For Plant and Machinery
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                <td>
                                                                    <a href="PlantandMachinery.aspx">Apply</a>
                                                                </td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td>
                                                                    Pioneer units under IPR 2015
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="PioneerUnit.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Migrated lndustrial Unit
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="MigratedlndustrialUnit.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Capital Invest Subsidy
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="Capitalinvestsubsidy.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Marketing Assistance
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                    Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="MarketingAssistance.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                          
                                                            <tr>
                                                                <td>
                                                                    Health care promotion policy
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                 Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td> <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="Health_Care_Promotion.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Anchor Tenant
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                 Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td> <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="AnchorTenant.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Exemption of Electricity Duty
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">IPR 2015</a>
                                                                </td>
                                                                <td>
                                                                 Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                <td>
                                                                  Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="Exemption_Electricity_Duty.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td>
                                                                    Apparel Unit
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="../images/industrialpol-2015.pdf">Apparel Policy 2016</a>
                                                                </td>
                                                                <td>
                                                                 Existing
                                                                </td>
                                                                <td>
                                                                    Post Production
                                                                </td>
                                                                 <td>
                                                                  Non-Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="Apparel_Policy.aspx">Apply</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="myModal" class="modal fade" role="dialog">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-header bg-gray">
                                                <button type="button" class="close" data-dismiss="modal">
                                                    &times;</button>
                                                <h4 class="modal-title">
                                                    We want to make Sure your Application is Eligible, Please provide us / Confirm a
                                                    few More details before Proceeding</h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-3">
                                                            Total No. of Male Workers</label>
                                                        <div class="col-sm-3">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                        </div>
                                                        <label class="col-sm-3">
                                                            Total No. of Female Workers</label>
                                                        <div class="col-sm-3">
                                                            <span class="colon">:</span><asp:TextBox ID="TextBox4" CssClass="form-control" runat="server"
                                                                Text=""></asp:TextBox></div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <h4 class="h4-header">
                                                        We Found that your an Existing Unit</h4>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-9">
                                                            Please mentioned Amount of additional investment on fixed assets for Expansion /
                                                            Mordernization / Diversification</label>
                                                        <div class="col-sm-3">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" Text="14,00,00,00"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 text-right">
                                                        <a class="btn btn-danger" data-toggle="modal" data-target="#myModal3">Proceed to Check
                                                            if false</a> <a class="btn btn-success" data-toggle="modal" data-target="#myModal2">
                                                                Proceed to Check if true</a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                                    Close</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="myModal2" class="modal fade" role="dialog">
                                    <div class="modal-dialog modal-sm">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-body text-center">
                                                <div class="form-group">
                                                    <h4 class="text-success">
                                                        Congratulations You are Eligible</h4>
                                                    <br />
                                                    <a class="btn btn-success" href="InterestSubsidy.aspx">Proceed to Apply</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="myModal3" class="modal fade" role="dialog">
                                    <div class="modal-dialog modal-sm">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-body text-center">
                                                <div class="form-group">
                                                    <h4>
                                                        Sorry You are not Eligible</h4>
                                                    <hr>
                                                    <label>
                                                        Reason for Rejection</label>
                                                    <p>
                                                        You are not eligible as thia amount is less than 50% book value of your fixed assets
                                                        !</p>
                                                    <br />
                                                    <a class="btn btn-danger" href="appliedlistwithdetails.aspx">Check eligibilty for another
                                                        incentive</a>
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal5" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body text-center">
                    <div class="form-group">
                        <label>
                            Redirect to Portal for Application for PC</label>
                        <button type="button" class="btn btn-primary" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal4" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body text-center">
                    <div class="form-group">
                        <label>
                            Redirect to Portal for Application for EIN/IEM</label>
                        <button type="button" class="btn btn-primary" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingipr2015" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part the negative unit
                        list under <strong>IPR 2015</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Units engaged in manufacturing and/or servicing activity belonging to following
                            categories.</li>
                        <li>Industries listed under the first schedule of the industries Department and regulation
                            Act,1951 and manufacturing enterprise classified under MSME.</li>
                        <li>Industries falling within the purview of the following Boards and public Agencies.
                            <ol>
                                <li>Coir Board </li>
                                <li>Silk Board</li>
                                <li>All India handloom and Handicraft Board</li>
                                <li>Khadi and village industries Commission/Board</li>
                                <li>Any other Agency con situation by Government for industrial department.</li>
                            </ol>
                        </li>
                        <li>Infrastructure projects only for the purpose of determining applicable land rate
                        </li>
                        <li>Service sector projects under priority sector.</li>
                        <li>Service sector other than priority sector only for the purpose of applicable land
                            rate.</li>
                        <li>Industrial unit will not include non-manufacturing/servicing industries except:
                            <ol>
                                <li>General workshops including repair workshops having investment in plant & machinery
                                    of Rs.50 Lakhs and above and running with power.</li>
                                <li>Cold storage and seafood freezing units having investment of Rs. 25.00 Lakhs and
                                    above.</li>
                                <li>Electronics repair and maintenance units for professional grade equipment and computer
                                    software ,ITES/BPO and related services with investment of Rs. 25,00 Lakhs and above.</li>
                                <li>Technology Development Laboratory/prototype Development centre/Research & Development
                                    with investment of Rs.25.00 Lakhs and above.</li>
                                <li>Printing press with investment in plant and machinery of Rs. 50.00 Lakhs and above.</li>
                                <li>Laundry/Dry Cleaning with investment in plant and machinery/equipment of Rs. 25.00
                                    Lakhs and above. </li>
                            </ol>
                        </li>
                        <li>The following units shall neither be eligible for fiscal incentives specified under
                            this IPR nor for allotment of land at concessional rates in the state, but shall
                            be eligible for investment facilitation ,allotment of land under normal rules at
                            benchmark value/market rate and recommendation to the financial institutions for
                            term loan and working capital and for recommendation, if necessary to the power
                            Distribution companies:
                            <ol>
                                <li>Hullers and Rice mills with investment in plant and machinery of less than Rs. 25
                                    Lakhs for industrially backward districts and less than Rs. 1 crore for other districts</li>
                                <li>Four mills including manufacture of besan, Pulse mills and chuda mills except investment
                                    in plant & machinery of more than 25 lakhs for industrially backward districts and
                                    less than Rs. 1 crore for other districts(excluding Roller Flour mills)
                                    <ol>
                                        <li>Processing of Spices with investment in plant & machinery with less than Rs.10 Lakhs
                                            for industrially backward districts and less than two crore rupees for other districts</li>
                                        <li>
                                        Units without Spice-mark or Agmark
                                    </ol>
                                </li>
                                <li>Confectionary with investment in plant and machinery with less than Rs.10 Lakhs
                                    for industrially backward districts and less than two crore rupees for other districts.</li>
                                <li>Oil mills with expellers including oil processing, filtering , de-coloring ,coloring
                                    ,refining of edible oils and hydro-generation thereof except investment in plant
                                    and machinery of RS. 10 Lakhs in industrial backward areas.</li>
                                <li>Preparation of sweets and savories etc</li>
                                <li>Bread making(excluding mechanized bakery)</li>
                                <li>Mixture.Bhujia and chanachur preparation units</li>
                                <li>Manufacture of ice candy</li>
                                <li>Manufacture and processing of betel nuts</li>
                                <li>Hacheries,Piggeries,rabbit or Broiler farming </li>
                                <li>Standalone sponge iron plants</li>
                                <li>Iron and steel processors, such as cutting of sheets,bars,angles,coils,M.S. sheets,
                                    recoiling, straightening,corrugating,drophammer units etc with low value addition</li>
                                <li>Cracker-making units</li>
                                <li>Tyre retreading units with investment in plant and machinery of less Rs.20 Lakhs</li>
                                <li>Stone crushing units</li>
                                <li>Coal/coke screening coal /coke Briquetting.</li>
                                <li>Production of firewood and charcoal.</li>
                                <li>Painting and spray-painting units with investment in plant and machinery of less
                                    than Rs. 20 Lakhs.</li>
                                <li>Units for physical mixing of fertilizers.</li>
                                <li>Brick- making units (except units making refractory bricks and those making bricks
                                    from flyash, red mud and similar industrial waste not less than 25% as base martial).</li>
                                <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                                    of less than Rs. 20 Lakhs.</li>
                                <li>Saw mills, sawing of timber.</li>
                                <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                                    cluster of at least 20 units.</li>
                                <li>Drilling rigs ,Bore-wells and Tube-wells</li>
                                <li>Units for mixing or blending/packaging of tea.</li>
                                <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purpose and Gudakhu
                                    manufacturing units.</li>
                                <li>Units for bottling of medicines.</li>
                                <li>Bookbinding/Rubber stamp making/making notebooks, exercise notebook s and envelopes.</li>
                                <li>Distilled water units</li>
                                <li>Tailoring (other than readymade garment manufacturing units )</li>
                                <li>Repacking /stitching/printing of woven sacks out of woven fabrics.</li>
                                <li>Pre-Processing of oil seeds-Decorticating, expelling, crushing, parching and frying.</li>
                                <li>Aerated water and soft drinks units</li>
                                <li>Bottling units or any activity in respect of IMFL or liquor of any kind.</li>
                                <li>Size reducing/size separating units/ Grinding / mixing units with investment in
                                    plant and machinery of less than ten crore rupees except manufacturing of cement
                                    with clinker.</li>
                                <li>Polythene less than 40 micron in thickness /recycling of plastic materials.</li>
                                <li>Thermal power plants.</li>
                                <li>Repackaging units.</li>
                            </ol>
                        </li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided informatin will be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingfoodprocessing">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingfoodprocessing" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part the negative unit
                        list under <strong>Food Processing</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).
                        </li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward districts </li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jaggery for chewing purposes and guddkhu
                            manufacturing units.</li>
                        <li>Pre-processing of oil seeds: decorticating, expelling, crushing, parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingMSME">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingMSME" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part the negative unit
                        list under <strong>MSME Policy</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>All service enterprises.</li>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).</li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward district.</li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Standalone Sponge iron plants.</li>
                        <li>“iron and steel processors” such as cutting of sheets, bars, angles, coils, M.S.
                            sheets, recoiling straightening , corrugating , drop hammer units etc. with low
                            value addition.</li>
                        <li>Cracker-making units.</li>
                        <li>Tyre retreading units with investment in plant and machinery of less than Rs.20
                            lakhs.</li>
                        <li>Stone crushing units.</li>
                        <li>Coal/coke screening. Coal washing ,coal/coke briquetting.</li>
                        <li>Production of firewood and charcoal.</li>
                        <li>Painting and spray-painting units with investment in plant and machinery of less
                            than Rs.20 lakhs.</li>
                        <li>Units for physical mixing of fertilizer.</li>
                        <li>Brick-making units (except units making refractory bricks and those making bricks
                            from fly ash, red mud and industrial waste not less than 25% as base material).</li>
                        <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                            of less than Rs. 20 lakh.</li>
                        <li>Saw mill, sawing of timber.</li>
                        <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                            cluster of at least 20 units.</li>
                        <li>Drilling rigs, Bore-wells and tube wells.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purposes and gudakhu
                            manufacturing units.</li>
                        <li>Units for bottling of medicines.</li>
                        <li>Bookbinding / Rubber stamp making /making notebooks, exercise notebooks and envelopes.</li>
                        <li>Distilled water units.</li>
                        <li>Tailoring (other than readymade garment manufacturing units).</li>
                        <li>Repacking /stitching /printing of woven sacks out of woven fabrics.</li>
                        <li>Pre-processing of oil seeds-decorticating , expelling ,crushing ,parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Size reducing/size separating units /grinding/mixing units with investment in plant
                            and machinery of less than 10 crore except manufacturing of cement with clinker.</li>
                        <li>Polythene less than 40 micron in thickness/recycling of plastic materials</li>
                        <li>Thermal power plant.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a href="unitdetails.aspx" class="btn btn-success" title="Submit">Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingipr20152" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>IPR 2015</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Units engaged in manufacturing and/or servicing activity belonging to following
                            categories.</li>
                        <li>Industries listed under the first schedule of the industries Department and regulation
                            Act,1951 and manufacturing enterprise classified under MSME.</li>
                        <li>Industries falling within the purview of the following Boards and public Agencies.
                            <ol>
                                <li>Coir Board </li>
                                <li>Silk Board</li>
                                <li>All India handloom and Handicraft Board</li>
                                <li>Khadi and village industries Commission/Board</li>
                                <li>Any other Agency con situation by Government for industrial department.</li>
                            </ol>
                        </li>
                        <li>Infrastructure projects only for the purpose of determining applicable land rate
                        </li>
                        <li>Service sector projects under priority sector.</li>
                        <li>Service sector other than priority sector only for the purpose of applicable land
                            rate.</li>
                        <li>Industrial unit will not include non-manufacturing/servicing industries except:
                            <ol>
                                <li>General workshops including repair workshops having investment in plant & machinery
                                    of Rs.50 Lakhs and above and running with power.</li>
                                <li>Cold storage and seafood freezing units having investment of Rs. 25.00 Lakhs and
                                    above.</li>
                                <li>Electronics repair and maintenance units for professional grade equipment and computer
                                    software ,ITES/BPO and related services with investment of Rs. 25,00 Lakhs and above.</li>
                                <li>Technology Development Laboratory/prototype Development centre/Research & Development
                                    with investment of Rs.25.00 Lakhs and above.</li>
                                <li>Printing press with investment in plant and machinery of Rs. 50.00 Lakhs and above.</li>
                                <li>Laundry/Dry Cleaning with investment in plant and machinery/equipment of Rs. 25.00
                                    Lakhs and above. </li>
                            </ol>
                        </li>
                        <li>The following units shall neither be eligible for fiscal incentives specified under
                            this IPR nor for allotment of land at concessional rates in the state, but shall
                            be eligible for investment facilitation ,allotment of land under normal rules at
                            benchmark value/market rate and recommendation to the financial institutions for
                            term loan and working capital and for recommendation, if necessary to the power
                            Distribution companies:
                            <ol>
                                <li>Hullers and Rice mills with investment in plant and machinery of less than Rs. 25
                                    Lakhs for industrially backward districts and less than Rs. 1 crore for other districts</li>
                                <li>Four mills including manufacture of besan, Pulse mills and chuda mills except investment
                                    in plant & machinery of more than 25 lakhs for industrially backward districts and
                                    less than Rs. 1 crore for other districts(excluding Roller Flour mills)
                                    <ol>
                                        <li>Processing of Spices with investment in plant & machinery with less than Rs.10 Lakhs
                                            for industrially backward districts and less than two crore rupees for other districts</li>
                                        <li>
                                        Units without Spice-mark or Agmark
                                    </ol>
                                </li>
                                <li>Confectionary with investment in plant and machinery with less than Rs.10 Lakhs
                                    for industrially backward districts and less than two crore rupees for other districts.</li>
                                <li>Oil mills with expellers including oil processing, filtering , de-coloring ,coloring
                                    ,refining of edible oils and hydro-generation thereof except investment in plant
                                    and machinery of RS. 10 Lakhs in industrial backward areas.</li>
                                <li>Preparation of sweets and savories etc</li>
                                <li>Bread making(excluding mechanized bakery)</li>
                                <li>Mixture.Bhujia and chanachur preparation units</li>
                                <li>Manufacture of ice candy</li>
                                <li>Manufacture and processing of betel nuts</li>
                                <li>Hacheries,Piggeries,rabbit or Broiler farming </li>
                                <li>Standalone sponge iron plants</li>
                                <li>Iron and steel processors, such as cutting of sheets,bars,angles,coils,M.S. sheets,
                                    recoiling, straightening,corrugating,drophammer units etc with low value addition</li>
                                <li>Cracker-making units</li>
                                <li>Tyre retreading units with investment in plant and machinery of less Rs.20 Lakhs</li>
                                <li>Stone crushing units</li>
                                <li>Coal/coke screening coal /coke Briquetting.</li>
                                <li>Production of firewood and charcoal.</li>
                                <li>Painting and spray-painting units with investment in plant and machinery of less
                                    than Rs. 20 Lakhs.</li>
                                <li>Units for physical mixing of fertilizers.</li>
                                <li>Brick- making units (except units making refractory bricks and those making bricks
                                    from flyash, red mud and similar industrial waste not less than 25% as base martial).</li>
                                <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                                    of less than Rs. 20 Lakhs.</li>
                                <li>Saw mills, sawing of timber.</li>
                                <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                                    cluster of at least 20 units.</li>
                                <li>Drilling rigs ,Bore-wells and Tube-wells</li>
                                <li>Units for mixing or blending/packaging of tea.</li>
                                <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purpose and Gudakhu
                                    manufacturing units.</li>
                                <li>Units for bottling of medicines.</li>
                                <li>Bookbinding/Rubber stamp making/making notebooks, exercise notebook s and envelopes.</li>
                                <li>Distilled water units</li>
                                <li>Tailoring (other than readymade garment manufacturing units )</li>
                                <li>Repacking /stitching/printing of woven sacks out of woven fabrics.</li>
                                <li>Pre-Processing of oil seeds-Decorticating, expelling, crushing, parching and frying.</li>
                                <li>Aerated water and soft drinks units</li>
                                <li>Bottling units or any activity in respect of IMFL or liquor of any kind.</li>
                                <li>Size reducing/size separating units/ Grinding / mixing units with investment in
                                    plant and machinery of less than ten crore rupees except manufacturing of cement
                                    with clinker.</li>
                                <li>Polythene less than 40 micron in thickness /recycling of plastic materials.</li>
                                <li>Thermal power plants.</li>
                                <li>Repackaging units.</li>
                            </ol>
                        </li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided informatin will be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingfoodprocessing2">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingfoodprocessing2" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>Food Processing</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).
                        </li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward districts </li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jaggery for chewing purposes and guddkhu
                            manufacturing units.</li>
                        <li>Pre-processing of oil seeds: decorticating, expelling, crushing, parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingMSME2">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingMSME2" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>MSME Policy</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>All service enterprises.</li>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).</li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward district.</li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Standalone Sponge iron plants.</li>
                        <li>“iron and steel processors” such as cutting of sheets, bars, angles, coils, M.S.
                            sheets, recoiling straightening , corrugating , drop hammer units etc. with low
                            value addition.</li>
                        <li>Cracker-making units.</li>
                        <li>Tyre retreading units with investment in plant and machinery of less than Rs.20
                            lakhs.</li>
                        <li>Stone crushing units.</li>
                        <li>Coal/coke screening. Coal washing ,coal/coke briquetting.</li>
                        <li>Production of firewood and charcoal.</li>
                        <li>Painting and spray-painting units with investment in plant and machinery of less
                            than Rs.20 lakhs.</li>
                        <li>Units for physical mixing of fertilizer.</li>
                        <li>Brick-making units (except units making refractory bricks and those making bricks
                            from fly ash, red mud and industrial waste not less than 25% as base material).</li>
                        <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                            of less than Rs. 20 lakh.</li>
                        <li>Saw mill, sawing of timber.</li>
                        <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                            cluster of at least 20 units.</li>
                        <li>Drilling rigs, Bore-wells and tube wells.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purposes and gudakhu
                            manufacturing units.</li>
                        <li>Units for bottling of medicines.</li>
                        <li>Bookbinding / Rubber stamp making /making notebooks, exercise notebooks and envelopes.</li>
                        <li>Distilled water units.</li>
                        <li>Tailoring (other than readymade garment manufacturing units).</li>
                        <li>Repacking /stitching /printing of woven sacks out of woven fabrics.</li>
                        <li>Pre-processing of oil seeds-decorticating , expelling ,crushing ,parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Size reducing/size separating units /grinding/mixing units with investment in plant
                            and machinery of less than 10 crore except manufacturing of cement with clinker.</li>
                        <li>Polythene less than 40 micron in thickness/recycling of plastic materials</li>
                        <li>Thermal power plant.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a href="unitdetailsforpccleared.aspx" class="btn btn-success" title="Submit">Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingipr20153" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>IPR 2015</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Units engaged in manufacturing and/or servicing activity belonging to following
                            categories.</li>
                        <li>Industries listed under the first schedule of the industries Department and regulation
                            Act,1951 and manufacturing enterprise classified under MSME.</li>
                        <li>Industries falling within the purview of the following Boards and public Agencies.
                            <ol>
                                <li>Coir Board </li>
                                <li>Silk Board</li>
                                <li>All India handloom and Handicraft Board</li>
                                <li>Khadi and village industries Commission/Board</li>
                                <li>Any other Agency con situation by Government for industrial department.</li>
                            </ol>
                        </li>
                        <li>Infrastructure projects only for the purpose of determining applicable land rate
                        </li>
                        <li>Service sector projects under priority sector.</li>
                        <li>Service sector other than priority sector only for the purpose of applicable land
                            rate.</li>
                        <li>Industrial unit will not include non-manufacturing/servicing industries except:
                            <ol>
                                <li>General workshops including repair workshops having investment in plant & machinery
                                    of Rs.50 Lakhs and above and running with power.</li>
                                <li>Cold storage and seafood freezing units having investment of Rs. 25.00 Lakhs and
                                    above.</li>
                                <li>Electronics repair and maintenance units for professional grade equipment and computer
                                    software ,ITES/BPO and related services with investment of Rs. 25,00 Lakhs and above.</li>
                                <li>Technology Development Laboratory/prototype Development centre/Research & Development
                                    with investment of Rs.25.00 Lakhs and above.</li>
                                <li>Printing press with investment in plant and machinery of Rs. 50.00 Lakhs and above.</li>
                                <li>Laundry/Dry Cleaning with investment in plant and machinery/equipment of Rs. 25.00
                                    Lakhs and above. </li>
                            </ol>
                        </li>
                        <li>The following units shall neither be eligible for fiscal incentives specified under
                            this IPR nor for allotment of land at concessional rates in the state, but shall
                            be eligible for investment facilitation ,allotment of land under normal rules at
                            benchmark value/market rate and recommendation to the financial institutions for
                            term loan and working capital and for recommendation, if necessary to the power
                            Distribution companies:
                            <ol>
                                <li>Hullers and Rice mills with investment in plant and machinery of less than Rs. 25
                                    Lakhs for industrially backward districts and less than Rs. 1 crore for other districts</li>
                                <li>Four mills including manufacture of besan, Pulse mills and chuda mills except investment
                                    in plant & machinery of more than 25 lakhs for industrially backward districts and
                                    less than Rs. 1 crore for other districts(excluding Roller Flour mills)
                                    <ol>
                                        <li>Processing of Spices with investment in plant & machinery with less than Rs.10 Lakhs
                                            for industrially backward districts and less than two crore rupees for other districts</li>
                                        <li>
                                        Units without Spice-mark or Agmark
                                    </ol>
                                </li>
                                <li>Confectionary with investment in plant and machinery with less than Rs.10 Lakhs
                                    for industrially backward districts and less than two crore rupees for other districts.</li>
                                <li>Oil mills with expellers including oil processing, filtering , de-coloring ,coloring
                                    ,refining of edible oils and hydro-generation thereof except investment in plant
                                    and machinery of RS. 10 Lakhs in industrial backward areas.</li>
                                <li>Preparation of sweets and savories etc</li>
                                <li>Bread making(excluding mechanized bakery)</li>
                                <li>Mixture.Bhujia and chanachur preparation units</li>
                                <li>Manufacture of ice candy</li>
                                <li>Manufacture and processing of betel nuts</li>
                                <li>Hacheries,Piggeries,rabbit or Broiler farming </li>
                                <li>Standalone sponge iron plants</li>
                                <li>Iron and steel processors, such as cutting of sheets,bars,angles,coils,M.S. sheets,
                                    recoiling, straightening,corrugating,drophammer units etc with low value addition</li>
                                <li>Cracker-making units</li>
                                <li>Tyre retreading units with investment in plant and machinery of less Rs.20 Lakhs</li>
                                <li>Stone crushing units</li>
                                <li>Coal/coke screening coal /coke Briquetting.</li>
                                <li>Production of firewood and charcoal.</li>
                                <li>Painting and spray-painting units with investment in plant and machinery of less
                                    than Rs. 20 Lakhs.</li>
                                <li>Units for physical mixing of fertilizers.</li>
                                <li>Brick- making units (except units making refractory bricks and those making bricks
                                    from flyash, red mud and similar industrial waste not less than 25% as base martial).</li>
                                <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                                    of less than Rs. 20 Lakhs.</li>
                                <li>Saw mills, sawing of timber.</li>
                                <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                                    cluster of at least 20 units.</li>
                                <li>Drilling rigs ,Bore-wells and Tube-wells</li>
                                <li>Units for mixing or blending/packaging of tea.</li>
                                <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purpose and Gudakhu
                                    manufacturing units.</li>
                                <li>Units for bottling of medicines.</li>
                                <li>Bookbinding/Rubber stamp making/making notebooks, exercise notebook s and envelopes.</li>
                                <li>Distilled water units</li>
                                <li>Tailoring (other than readymade garment manufacturing units )</li>
                                <li>Repacking /stitching/printing of woven sacks out of woven fabrics.</li>
                                <li>Pre-Processing of oil seeds-Decorticating, expelling, crushing, parching and frying.</li>
                                <li>Aerated water and soft drinks units</li>
                                <li>Bottling units or any activity in respect of IMFL or liquor of any kind.</li>
                                <li>Size reducing/size separating units/ Grinding / mixing units with investment in
                                    plant and machinery of less than ten crore rupees except manufacturing of cement
                                    with clinker.</li>
                                <li>Polythene less than 40 micron in thickness /recycling of plastic materials.</li>
                                <li>Thermal power plants.</li>
                                <li>Repackaging units.</li>
                            </ol>
                        </li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided informatin will be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingfoodprocessing3">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingfoodprocessing3" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>Food Processing</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).
                        </li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward districts </li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jaggery for chewing purposes and guddkhu
                            manufacturing units.</li>
                        <li>Pre-processing of oil seeds: decorticating, expelling, crushing, parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a data-toggle="modal" class="btn btn-success" data-dismiss="modal" data-target="#undertakingMSME3">
                        Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="undertakingMSME3" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Please provide undertakeing stating that your unit is not a part thre negative unit
                        list under <strong>MSME Policy</strong></h4>
                </div>
                <div class="modal-body">
                    <h4>
                        UNDERTAKING</h4>
                    <p>
                        I hereby declare that my unit/Enterprise does not belong to the below listed specific
                        unit types which fall in the negative/ineligible unit.</p>
                    <p>
                        In my application, I will produce required documents for the same.</p>
                    <h4>
                        List of ineligible Unit Types</h4>
                    <ol>
                        <li>All service enterprises.</li>
                        <li>Hullers and rice mills with investment in plant and machinery of less than Rs. 25
                            lakhs for industrially backward districts and less than 1 crore for other.</li>
                        <li>Flour mills including manufacture of besan , pulse mills and chuda mills except
                            investment in plant and machinery of less than Rs.25 lakhs far industrially backward
                            districts and less than Rs. 1 crore for other areas (excluding rolling mills).</li>
                        <li>
                            <ol>
                                <li>Processing of spices with investment in plant and machinery with less than Rs. 10
                                    lakh for industrially backward districts and less than Rs.2 crore for other areas.</li>
                                <li>Units without spice-mark or Ag-mark.</li>
                            </ol>
                        </li>
                        <li>Confectionary with investment in plant and machinery with less than Rs.10 lakhs
                            for industrially backward districts and less than Rs. 2 crore for other areas.
                        </li>
                        <li>Oil mills with expellers including oil processing , filtering, decoloring , coloring
                            refining of edible oils and hydrogenation thereof except investment in plant and
                            machinery more than Rs.10 lakhs in industrially backward district.</li>
                        <li>Preparation of sweets and savories excepts investment in plant and machinery of
                            less than Rs.10 lakhs for industrially backward districts and less than Rs.50 lakhs
                            for other areas.</li>
                        <li>Bread-making (excluding mechanized bakery)</li>
                        <li>Mixture, bhujia and chanachur preparation units.</li>
                        <li>Manufacture of ice candy</li>
                        <li>Manufacture and processing of betel units.</li>
                        <li>Hatcheries, piggeries, Rabbit or Broiler farming.</li>
                        <li>Standalone Sponge iron plants.</li>
                        <li>“iron and steel processors” such as cutting of sheets, bars, angles, coils, M.S.
                            sheets, recoiling straightening , corrugating , drop hammer units etc. with low
                            value addition.</li>
                        <li>Cracker-making units.</li>
                        <li>Tyre retreading units with investment in plant and machinery of less than Rs.20
                            lakhs.</li>
                        <li>Stone crushing units.</li>
                        <li>Coal/coke screening. Coal washing ,coal/coke briquetting.</li>
                        <li>Production of firewood and charcoal.</li>
                        <li>Painting and spray-painting units with investment in plant and machinery of less
                            than Rs.20 lakhs.</li>
                        <li>Units for physical mixing of fertilizer.</li>
                        <li>Brick-making units (except units making refractory bricks and those making bricks
                            from fly ash, red mud and industrial waste not less than 25% as base material).</li>
                        <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                            of less than Rs. 20 lakh.</li>
                        <li>Saw mill, sawing of timber.</li>
                        <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                            cluster of at least 20 units.</li>
                        <li>Drilling rigs, Bore-wells and tube wells.</li>
                        <li>Units for mixing or blending /packaging of tea.</li>
                        <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purposes and gudakhu
                            manufacturing units.</li>
                        <li>Units for bottling of medicines.</li>
                        <li>Bookbinding / Rubber stamp making /making notebooks, exercise notebooks and envelopes.</li>
                        <li>Distilled water units.</li>
                        <li>Tailoring (other than readymade garment manufacturing units).</li>
                        <li>Repacking /stitching /printing of woven sacks out of woven fabrics.</li>
                        <li>Pre-processing of oil seeds-decorticating , expelling ,crushing ,parching and frying.</li>
                        <li>Aerated water and soft drink units.</li>
                        <li>Bottling units or any activity in respect to IMFL or liquor of any kind.</li>
                        <li>Size reducing/size separating units /grinding/mixing units with investment in plant
                            and machinery of less than 10 crore except manufacturing of cement with clinker.</li>
                        <li>Polythene less than 40 micron in thickness/recycling of plastic materials</li>
                        <li>Thermal power plant.</li>
                        <li>Re-packaging units.</li>
                    </ol>
                    <p>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="">I agree that provided information is be correct.</label>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <a href="unitdetailsforEINIEM.aspx" class="btn btn-success" title="Submit">Submit</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="Power_Modal_1" class="modal fade" role="dialog">
        <div class="modal-dialog modal-md">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Warning</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <h4 class="h4-header text-center">
                            You can Apply for the Previous Financial Year i.e. Apr'2016-Mar'2017</h4>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 text-right">
                            <a class="btn btn-success" data-toggle="modal" data-target="#Power_Modal_2">Proceed</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="Power_Modal_2" class="modal fade" role="dialog">
        <div class="modal-dialog modal-md">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Warning</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <h4 class="h4-header text-center">
                            You can't Apply for the Previous Financial Year i.e. Apr'2016-Mar'2017 as you have
                            crossed 6 Months after Financial Year End</h4>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 text-right">
                            <a class="btn btn-danger" data-toggle="modal" data-target="#Power_Modal_3">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="Power_Modal_3" class="modal fade" role="dialog">
        <div class="modal-dialog modal-md">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Warning</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <h4 class="h4-header text-center">
                            You have Already applied for the eligible no. of years.</h4>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 text-right">
                            <a class="btn btn-danger" href="Power.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
