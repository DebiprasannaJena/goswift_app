<%--'*******************************************************************************************************************
' File Name         : incentiveoffered.aspx
' Description       : Incentive Landing Page,View Application Counts and Validate Certification
' Created by        : Sushant Kumar Jena
' Created On        : 21st Sept 2017
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="incentiveoffered.aspx.cs"
    Inherits="incentives_incentiveoffered" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<%--<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> </title>
    
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css"/>
    <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/jscript">
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });

            boxHght = $('.leftcolm').height();
        });
    
    </script>
    <style type="text/css">
        .manual
        {
            font-size: 14px;
            text-decoration: none;
            color: #1460a0;
        }
    </style>
    <style type="text/css">
        .unitdtl .groupmastreportlet2 .portletdivider
        {
            width: 20%;
        }
        .groupmastreportlet2 .portletdivider span
        {
            font-size: 20px;
            margin-left: 3px;
        }
        ul ol
        {
            margin-left: 35px !important;
        }
        .groupmastreportlet2 h4
        {
            color: #fff !important;
            margin: 0 0 8px !important;
            font-size: 16px !important;
            text-transform: uppercase;
            border-bottom: 1px solid #3e96a2;
            padding-bottom: 4px !important;
            display: block !important;
        }
        .masterportlet h4
        {
            color: #fff !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
     <uc2:header ID="header" runat="server" />
        <div class="container">
            <div class="container wrapper">
                <div class="registration-div investors-bg investboxconain">
                    <div id="exTab1" class="">
                        <div class="investrs-tab">
                            <uc4:investoemenu ID="ineste" runat="server" />
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="form-sec">
                                    <div class="row">
                                        <div class="col-md-9 col-sm-12">
                                            <div class="applicationhistory">
                                                <div class="row">
                                                    <div class="col-sm-8">
                                                        <h1>
                                                            <i class="fa fa-file-text-o"></i>&nbsp; Application History</h1>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="DrpDwn_Investor_Unit" runat="server" CssClass="form-control dropx"
                                                            AutoPostBack="true" OnSelectedIndexChanged="DrpDwn_Investor_Unit_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-9 ">
                                                        <h3>Total Applications <span class="counter "><a id="TagTotalApp" runat="server" href="#"
                                                            title="Total Application">
                                                            <asp:Label ID="Lbl_Total_App" runat="server"></asp:Label>
                                                        </a></span>
                                                        </h3>
                                                        <div class="content-sec">
                                                            <h4>Application Status</h4>
                                                            <div class="datadiv">
                                                                <a id="TagApproved" runat="server" href="#" title="Approved"><span iclass="counter">
                                                                    <asp:Label ID="Lbl_Approved" runat="server"></asp:Label></span> Approved </a>
                                                            </div>
                                                            <div class="datadiv">
                                                                <a id="TagScrutiny" runat="server" href="#" title="Scrutiny in Progress"><span class="counter">
                                                                    <asp:Label ID="Lbl_Scrutiny" runat="server"></asp:Label></span>Scrutiny in Progress
                                                                </a>
                                                            </div>
                                                            <div class="datadiv border-right0">
                                                                <a id="TagRejected" runat="server" href="#" title="Rejected"><span class="counter">
                                                                    <asp:Label ID="Lbl_Rejected" runat="server"></asp:Label></span>Rejected </a>
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 ">
                                                        <div class="drafted-dtls">
                                                            <div class="consec">
                                                                <a id="TagDraft" runat="server" href="#" title="Drafted"><span class="counter">
                                                                    <asp:Label ID="Lbl_Drafted_App" runat="server"></asp:Label></span>Drafted Application
                                                                </a>
                                                            </div>
                                                            <div class="consec border0">
                                                                <a id="TagDisbursed" runat="server" href="#" title="Disbursed"><span class="counter">
                                                                    <asp:Label ID="Lbl_Disbursed" runat="server"></asp:Label></span>Disbursed </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-3">
                                            <div class="innermenulist">
                                                <h1>
                                                    <i class="fa fa-list-alt"></i>&nbsp; Important Links</h1>
                                                <ul>
                                                    <li>
                                                        <asp:LinkButton ID="LnkBtn_Prov_Priority" runat="server" ToolTip="Click Here to Apply Provisional Priority Certificate !!"
                                                            OnClick="LnkBtn_Prov_Priority_Click">Provisional Priority Certificate IPR-2015</asp:LinkButton>
                                                    </li>
                                                    <li><a title="Priority Certificate">Priority Certificate IPR-2015<b class="caret pull-right">
                                                    </b></a>
                                                        <ul class="dropdownlist">
                                                            <li>
                                                                <asp:LinkButton ID="LnkBtn_Priority_Offline" runat="server" ToolTip="Click Here to Apply Offline Priority Certificate !!"
                                                                    OnClick="LnkBtn_Priority_Offline_Click">Offline</asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton ID="LnkBtn_Priority_Online" runat="server" ToolTip="Click Here to Apply Online Priority Certificate !!"
                                                                    OnClick="LnkBtn_Priority_Online_Click">Online</asp:LinkButton>
                                                            </li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="LnkBtn_Pioneer" runat="server" ToolTip="Click Here to Apply Pioneer Certificate !!"
                                                            OnClick="LnkBtn_Pioneer_Click">Pioneer Certificate IPR-2015</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="LnkBtn_Prov_Thrust_Priority" runat="server" ToolTip="Click Here to Apply Provisional Thrust/Priority  Certificate !!"
                                                            OnClick="LnkBtn_Prov_Thrust_Priority_Click">Provisional Thrust/Priority Certificate IPR-2022</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="LnkBtn_Stam_Duty_Exeption" runat="server" OnClick="LnkBtn_Stam_Duty_Exeption_Click" ToolTip="Click Here to Apply Stamp Duty Exemption  Certificate !!">Stamp Duty Exemption IPR-2022</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="LnkBtn_Land_Industrial_Use" runat="server" OnClick="LnkBtn_Land_Industrial_Use_Click" ToolTip="Click Here to Apply Conversion Of Land For Industrial Use  Certificate !!">Conversion Of Land For Industrial Use IPR-2022</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="LnkBtn_Migrated_Unit" runat="server" OnClick="LnkBtn_Migrated_Unit_Click" ToolTip="Click Here to Apply Exercising Option by Migrated Industrial Unit  Certificate !!">Exercising Option by Migrated Industrial Unit IPR-2022</asp:LinkButton>
                                                    </li>

                                                    <li><a title="Priority Certificate">Apply For Incentive <b class="caret pull-right"></b></a>
                                                        <ul class="dropdownlist">
                                                            <li>
                                                                <asp:LinkButton ID="LnkBtn_Inct_Apply" runat="server" ToolTip="Click Here to Apply Incentive Under IPR-2015 !!"
                                                                    OnClick="LnkBtn_Inct_Apply_Click">Under IPR-2015</asp:LinkButton></li>
                                                            <%--<li>
                                                        <asp:LinkButton ID="LnkBtn_Inct_Apply_2022" runat="server" ToolTip="Click Here to Apply Incentive Under IPR-2022 !!"
                                                            OnClick="LnkBtn_Inct_Apply_Click">Under IPR-2022</asp:LinkButton>
                                                    </li>--%>
                                                            <li>
                                                                <asp:LinkButton ID="LnkBtn_Inct_Apply_Sectoral" runat="server" ToolTip="Click Here to Apply Incentive Under Sectoral Policy !!"
                                                                    OnClick="LnkBtn_Inct_Apply_Sectoral_Click">Under Sectoral Policy</asp:LinkButton>
                                                            </li>
                                                        </ul>
                                                    </li>
                                                    <li><a href="ViewApplicationStatus.aspx" title="Click Here to View Application Status !!">View Application Status</a></li>
                                                    <li><a href="View_EC_Application_Status.aspx" title="Click Here to View EC Application Status !!">View EC Application Status</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-header">
                                        <h2>Policy &amp; Incentive Framework for Industrial Units in Odisha</h2>
                                    </div>
                                    <div>
                                        <div class="row">
                                            <div class="col-sm-8">
                                                <div class="details-section leftcolm">
                                                    <%--<h3>Advantage Odisha</h3>--%>
                                                    <h3>
                                                        <span>Advantage Odisha</span> <small>Industrial Policy 2015</small>
                                                    </h3>
                                                    <h1>Land &amp; Infrastructure Development Support
                                                <hr />
                                                    </h1>
                                                    <div class="row">
                                                        <div class="col-sm-5">
                                                            <div class="show-table">
                                                                <h4>
                                                                    <span>
                                                                        <img src="../images/Industrial-Infrastructure.png" width="33" alt="Industrial-Infrastructure" />
                                                                    </span><strong>Industrial Infrastructure Development Fund</strong>
                                                                </h4>
                                                            </div>
                                                            <p>
                                                                An exclusive Industrial Infrastructure Development Fund (IIDF) with an initial corpus
                                                        of Rs.100 crore for development of quality infrastructure
                                                            </p>
                                                            <div class="show-table">
                                                                <h4>
                                                                    <span>
                                                                        <img src="../images/Power.png" width="33" alt="Power" />
                                                                    </span><strong>Power</strong></h4>
                                                            </div>
                                                            <ul class="list-item">
                                                                <li>Committed 24*7 power</li>
                                                                <li>Dedicated industrial feeders</li>
                                                                <li>Reimbursement of Rs. 0.25 – 1.25 per unit for a period of 5 years based employment
                                                            and investment (Category A1-3, B1-3)</li>
                                                            </ul>
                                                            <div class="show-table">
                                                                <h4>
                                                                    <span>
                                                                        <img src="../images/Anchor-Tenant.png" width="33" alt="Power" /></span> <strong>Anchor
                                                                    Tenant</strong></h4>
                                                            </div>
                                                            <ul class="list-item">
                                                                <li>25% subsidy on cost of land</li>
                                                                <li>VAT Reimbursement for additional 2 years subject to the overall limit</li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-sm-6 col-sm-offset-1">
                                                            <div class="show-table">
                                                                <h4>
                                                                    <span>
                                                                        <img src="../images/support-Quality.png" width="42" alt="Power" /></span> <strong>Capital
                                                                    grant to support Quality Infrastructure</strong></h4>
                                                            </div>
                                                            <ul class="list-item">
                                                                <li>50% of the infrastructure cost with a ceiling of Rs.10 crore per green field industrial
                                                            park/cluster</li>
                                                                <li>50% of total cost with a ceiling of Rs.5 crore for up gradation of brown field clusters.</li>
                                                            </ul>
                                                            <div class="show-table">
                                                                <h4>
                                                                    <span>
                                                                        <img src="../images/land.png" width="38" alt="Power" /></span> <strong>Land</strong></h4>
                                                            </div>
                                                            <p>
                                                                10% of the land for large projects subject to an upper limit of 300 Acres shall
                                                        be earmarked for setting up ancillary and downstream industrial park
                                                            </p>
                                                            <div class="show-table">
                                                                <h4>
                                                                    <span>
                                                                        <img src="../images/Economic-Zones.png" width="33" alt="Power" /></span> <strong>Special
                                                                    Economic Zones</strong></h4>
                                                            </div>
                                                            <p>
                                                                Separate policy for SEZs to provide fiscal and non-fiscal incentives
                                                            </p>
                                                            <div class="show-table">
                                                                <h4>
                                                                    <span>
                                                                        <img src="../images/Protection-infrastructure.png" width="33" alt="Power" /></span>
                                                                    <strong>Environmental Protection infrastructure subsidy</strong></h4>
                                                            </div>
                                                            <p>
                                                                20 Lakhs or 20% of capital cost of setting ETP
                                                            </p>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                    <div class="text-center m-t-20">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 ">
                                                <div class="details-section rightcolm">
                                                    <h3 class="m-t-0">
                                                        <span><small>Know your</small> Incentives</span></h3>
                                                    <p class="subhd">
                                                        Incentive Eligibility &amp; Application Status
                                                    </p>
                                                    <ul class="list-item">
                                                        <li class="ulheading">Principal Policies</li>
                                                        <li><a href="http://investodisha.org/download/industrial-policy-resolution-2015.pdf"
                                                            title="IPR 2015" target="_blank">IPR 2015</a> </li>
                                                        <li><a href="https://investodisha.gov.in/download/Industrial_Policy_Resolution_2022_Col.pdf"
                                                            title="IPR 2022" target="_blank">IPR 2022</a> </li>
                                                    </ul>
                                                    <ul class="list-item">
                                                        <li class="ulheading">Sectoral Policies</li>
                                                        <li><a href="http://www.investodisha.org/apparel-policy" title="Apparel Policy" target="_blank">Apparel Policy</a></li>
                                                        <li><a href="http://investodisha.org/biotechnology-policy" title="Biotechnology Policy 2016"
                                                            target="_blank">Biotechnology Policy 2016</a></li>
                                                        <li><a href="http://investodisha.org/health-care-investment-promotion-policy" title="Health Care Investment Promotion Policy 2016"
                                                            target="_blank">Health Care Investment Promotion Policy 2016</a></li>
                                                        <li><a href="http://investodisha.org/ICT-policy" title="ICT Policy, 2014" target="_blank">ICT Policy, 2014</a></li>
                                                        <li><a href="http://investodisha.org/odisha-fisheries-policy" title="Odisha Fisheries Policy 2015"
                                                            target="_blank">Odisha Fisheries Policy 2015</a></li>
                                                        <li><a href="http://investodisha.org/odisha-food-processing-policy" title="Odisha Food Processing Policy, 2016"
                                                            target="_blank">Odisha Food Processing Policy, 2016</a></li>
                                                        <li><a href="http://investodisha.org/tourism-policy" title="Odisha Tourism Policy, 2016"
                                                            target="_blank">Odisha Tourism Policy, 2016</a></li>
                                                        <li><a href="http://investodisha.org/pharmaceuticals-policy" title="Pharmaceuticals Policy 2016"
                                                            target="_blank">Pharmaceuticals Policy 2016</a></li>
                                                        <li><a href="http://investodisha.org/renewable-energy-policy" title="Renewable Energy Policy 2016"
                                                            target="_blank">Renewable Energy Policy 2016</a></li>
                                                    </ul>
                                                    <ul class="list-item">
                                                        <li class="ulheading">Other Policies</li>
                                                        <li><a href="http://investodisha.org/odisha-MSME-policy" title="Odisha MSME Policy, 2016"
                                                            target="_blank">Odisha MSME Policy, 2016</a></li>
                                                        <li><a href="http://investodisha.org/policy-for-special-economic-zones" title="Policy for Special Economic Zones, 2015"
                                                            target="_blank">Policy for Special Economic Zones, 2015</a></li>
                                                        <li><a href="http://investodisha.org/startup-policy" title="Startup Policy 2016"
                                                            target="_blank">Startup Policy 2016</a></li>
                                                    </ul>
                                                    <br />
                                                    <span><a href="Files/UserManual/Incentive_User_Manual.pdf" title="Click Here to View Incentive User Manual"
                                                        class="manual" target="_blank"><i class="fa fa-book"></i>Incentive User Manual</a></span>
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
    
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
