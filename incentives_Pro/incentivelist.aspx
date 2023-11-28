<%@ Page Language="C#" AutoEventWireup="true" CodeFile="incentivelist.aspx.cs"
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
                                                                    <a href="InterestSubsidy.aspx">Apply</a>
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
                                                                    <a href="Enterpreneurship_Subsidy.aspx">Apply</a>
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
                                                                    Apparel Unit
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
                                                                  Non-Fiscal
                                                                </td>
                                                                <td>
                                                                    <a href="Apparel_Policy.aspx">Apply</a>
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
                                                        </table>
                                                    </div>
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
  
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
