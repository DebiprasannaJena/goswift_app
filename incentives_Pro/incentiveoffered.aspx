<%@ Page Language="C#" AutoEventWireup="true" CodeFile="incentiveoffered.aspx.cs"
    Inherits="incentives_incentiveoffered" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
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
                                <li class="active"><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                <li><a href="appliedindustrylist.aspx">Apply For incentive</a></li>
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
                            </div>
                            <h2>
                                Policy &amp; Incentive Framework for Industrial Units in Odisha</h2>
                        </div>
                        <div class="form-body">
                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="details-section">
                                        <h4>
                                            Advantage Odisha</h4>
                                        <h1>
                                            <span>Industrial Policy 2015 </span>
                                            <br />
                                            Land &amp; Infrastructure Development Support</h1>
                                        <table class="table table-bordered bg-white">
                                            <tr>
                                                <th>
                                                    Industrial Infrastructure Development Fund
                                                </th>
                                                <td>
                                                An exclusive Industrial Infrastructure Development Fund (IIDF) with an initial corpus of Rs.100 crore for development of quality infrastructure  
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Capital grant to support Quality Infrastructure
                                                </th>
                                                <td>
                                                <ul>
                                                <li>50% of the infrastructure cost with a ceiling of Rs.10 crore per green field industrial park/cluster</li>
                                                <li>50% of total cost with a ceiling of Rs.5 crore for up gradation of brown field clusters.</li>
                                                </ul>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Power
                                                </th>
                                                <td>
                                                <ul>
                                                <li>Committed 24*7 power</li>  
                                                <li>Dedicated industrial feeders</li>  
                                                <li>Reimbursement of Rs. 0.25 – 1.25 per unit for a period of 5 years based employment and investment (Category A1-3, B1-3)</li>
                                                


                                                </ul>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    land
                                                </th>
                                                <td>
                                                10% of the land for large projects subject to an upper limit of 300 Acres shall be earmarked for setting up ancillary and downstream industrial park
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Special Economic Zones
                                                </th>
                                                <td>
                                                Separate policy for SEZs to provide fiscal and non-fiscal incentives
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Anchor Tenant
                                                </th>
                                                <td>
                                                <ul>
                                                <li>25% subsidy on cost of land</li>
                                                <li>VAT Reimbursement for additional 2 years subject to the overall limit</li>
                                                

                                                </ul>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Envioramental Protection infrastructure subsidy
                                                </th>
                                                <td>
                                                20 Lakhs or 20% of capital cost of setting ETP
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="table table-bordered" style="display:none;">
                                            <tr>
                                                <th>
                                                    Incentive Page links
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <a href="InterestSubsidy.aspx">Interest subsidy</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <a href="PatentRegistration.aspx">Patent Registration</a>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <a href="Subsidy_Plant_MC.aspx">Subsidy for Plant &amp; MC</a>
                                                </td>
                                            </tr> 
                                            
                                            <tr>
                                                <td>
                                                    <a href="Stamp_Duty_Exemption.aspx">Stamp Duty Exemption</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <a href="TechnicalKnowhow.aspx">Technical Know How</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <a href="EmployeementCostSubsidy.aspx">Employment Cost Subsidy</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <a href="Enterpreneurship_Subsidy.aspx">Enterpreneurship Subsidy</a>
                                                </td>
                                            </tr>  <tr>
                                                <td>
                                                    <a href="Reimbursementofenergyaudit.aspx">One time reimbursement of energy audit cost under IPR 2015</a>
                                                </td>
                                            </tr> 
                                            
                                            <tr>
                                                <td>
                                                    <a href="Exemption_Premium_Land_Conversion.aspx">Exemption Premium Land Conversion</a>
                                                </td>
                                            </tr> <tr>
                                                <td>
                                                    <a href="Quality_Certification.aspx">Quality certification</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-sm-4 bg-drgray">
                                    <div class="details-section ">
                                        <h4>
                                            Know your Incentives
                                            <br />
                                            <span>Incentive Eligibility &amp; Application Status</span>
                                        </h4>
                                        <ul>
                                            <li class="ulheading">Principal Policies</li>
                                            <li><a href="#" title="IPR 2015">IPR 2015</a> </li>
                                        </ul>
                                        <ul>
                                            <li class="ulheading">Sectoral Policies</li>
                                            <li><a href="http://www.investodisha.org/apparel-policy" title="Apparel Policy">Apparel
                                                Policy</a></li>
                                            <li><a href="http://investodisha.org/biotechnology-policy" title="Biotechnology Policy 2016">
                                                Biotechnology Policy 2016</a></li>
                                            <li><a href="http://investodisha.org/health-care-investment-promotion-policy" title="Health Care Investment Promotion Policy 2016">
                                                Health Care Investment Promotion Policy 2016</a></li>
                                            <li><a href="http://investodisha.org/ICT-policy" title="ICT Policy, 2014">ICT Policy,
                                                2014</a></li>
                                            <li><a href="http://investodisha.org/odisha-fisheries-policy" title="Odisha Fisheries Policy 2015">
                                                Odisha Fisheries Policy 2015</a></li>
                                            <li><a href="http://investodisha.org/odisha-food-processing-policy" title="Odisha Food Processing Policy, 2016">
                                                Odisha Food Processing Policy, 2016</a></li>
                                            <li><a href="http://investodisha.org/tourism-policy" title="Odisha Tourism Policy, 2016">
                                                Odisha Tourism Policy, 2016</a></li>
                                            <li><a href="http://investodisha.org/pharmaceuticals-policy" title="Pharmaceuticals Policy 2016">
                                                Pharmaceuticals Policy 2016</a></li>
                                            <li><a href="http://investodisha.org/renewable-energy-policy" title="Renewable Energy Policy 2016">
                                                Renewable Energy Policy 2016</a></li>
                                        </ul>
                                        <ul>
                                            <li class="ulheading">Other Policies</li>
                                            <li><a href="http://investodisha.org/odisha-MSME-policy" title="Odisha MSME Policy, 2016">
                                                Odisha MSME Policy, 2016</a></li>
                                            <li><a href="http://investodisha.org/policy-for-special-economic-zones" title="Policy for Special Economic Zones, 2015">
                                                Policy for Special Economic Zones, 2015</a></li>
                                            <li><a href="http://investodisha.org/startup-policy" title="Startup Policy 2016">Startup
                                                Policy 2016</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-footer">
                            <div class="row">
                                <div class="col-sm-12 text-right">
                                 
                                    <a Class="btn btn-success"  data-toggle="modal" data-target="#undertakingipr2015"  title="Apply">Apply</a>
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
      <div id="undertakingipr2015" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-gray">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                       Procced Detilas</strong></h4>
                </div>
                <div class="modal-body">
                   <table class="table table-bordered"><tr><td><a href="unitdetails.aspx">Apply with PC No</a></td></tr>
                   <tr><td><a href="unitdetailswithoutpcno.aspx">Apply with out PC No</a></td></tr>
                   <tr><td><a href="incentivelist.aspx">Incentive Form List</a></td></tr></table>
                </div>
                <div class="modal-footer">
                   
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
