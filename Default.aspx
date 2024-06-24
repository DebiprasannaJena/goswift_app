<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="website_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc4" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <title>GOSWIFT||Single Window Portal</title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .imglinksdet
        {
            display: block;
            margin-bottom: 7px;
            margin-right: 0;
                height: 71px;
                background: #f5f5f591;
            cursor: pointer;
                border: 1px solid #edededd1;
            text-decoration: none;
        }
       .imglinksdet img{
width:100%;
object-fit: cover;
        }
        .imglinksdet img:hover{
            transition: all 0.2s ease-out;
    box-shadow: 0px 4px 8px rgba(38, 38, 38, 0.2);
        }
       .map-sec h4 {
    margin: 0 0 26px;
}
    </style>
    <%--<script>
        $(function () {
            document.addEventListener('contextmenu', function(e) {
                   e.preventDefault();
                });
                document.onkeydown = function(e) {
                      if(event.keyCode == 123) {
                         return false;
                    }  
                    else if ((event.ctrlKey && event.shiftKey && event.keyCode == 73) || (event.ctrlKey && event.shiftKey && event.keyCode == 74)) {
                          return false;
                    }
                }
        });
    </script>--%>
</head>
<body class="home">
    <form id="form1" runat="server">
        <div>
            <%--   <uc2:header ID="header" runat="server" />--%>
            <!--Section for Header Menu-->
            
            <section class="topbar">
                <div class="container">
                    <div class="row" style="width: 1172px;">
                        <div class="helpline">
                            <i class="fa fa-phone"></i>
                            <h3>Toll Free Helpline - <span>1800 345 7157</span><br />
                                Help Desk Contact No  -<span>91-8895889513</span>
                                <small>(Timing 10.00 AM to 6.00 PM on working days)</small></h3>
                            <h4><i class="fa fa-envelope"></i>support[dot]investodisha[at]nic[dot]in</h4>
                        </div>

                        <ul class="nav navbar-nav nav-links">
                            <li><a class="scrdr" href="http://www.nvda-project.org/" target="_blank"><i class="fa fa-fax"></i></a></li>
                            <li><a href="#" class="font-plus">T+</a></li>
                            <li><a href="#" class="font-normal active">T</a></li>
                            <li><a href="#" class="font-minus">T-</a></li>
                            <%--<li><a href="https://www.youtube.com/watch?v=jqXi-AhpDg0&t=0s&index=2&list=PLxlD_G5mrgNItIPSMkbNSj-wDKCCR8MiH" target="_blank">Walkthrough Videos</a></li>--%>
                            <li class="dropdown">
                                <a class="dropdown-toggle" data-toggle="dropdown" href="#">User Manual<span class="caret"></span></a>
                                <ul class="dropdown-menu" style="min-width: 93px !important; background-color: black;">
                                    <li><a href="#" style="position: static;">REGISTRATION</a></li>
                                    <li><a href="images/UserManual.pdf" target="_blank" style="position: static;">PEAL</a></li>
                                </ul>
                            </li>
                            <li>
                                <a href="https://investodisha.gov.in/goswift/contactus.aspx" target="_blank">Contact Us</a>
                                <%--<a href="contactus.aspx?enc=UgkSQR+edDnLp3fXL8+9Bw==" target="_blank">Contact Us</a>--%>
                            </li>
                        </ul>
                    </div>
                </div>
            </section>
            <section class="header">
                
                <div class="container">
                    <div class="row">
                        <div class="col-sm-6">
                            <h1 class="logo">
                                <img src="images/govtlogo.png" />
                                <img src="images/gologo.png" />
                            </h1>
                        </div>
                        <div class="col-sm-6">
                            <%--<div class="searchbar">
                <div class="input-group">
                    <input type="text" class="form-control" name="email" placeholder="Search Term">
                    <span class="input-group-addon"><i class="fa fa-search"></i></span>
                  </div>
            </div>--%>
                        </div>
                    </div>
                </div>
            </section>
            <section class="menubar">
                <div class="container">
                    <div class="row">
                        <nav class="navbar navbar-default">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                </button>

                            </div>
                            <div class="collapse navbar-collapse" runat="server" id="myNavbar">
                            </div>

                            <ul class="implinks">
                                <li>
                                    <a href="javascript:void(0)"><i class="fa fa-sign-in"></i>
                                        <label>Login</label></a>
                                    <ul>
                                        <li><a href="Login.aspx" title="Investor Login">Investor Login</a></li>
                                        <li><a href="portal" target="_blank" title="Department Login">Department Login</a></li>
                                    </ul>
                                </li>


                            </ul>

                            <div class="clearfix"></div>
                        </nav>
                    </div>



                </div>
            </section>
            <!--End Header-->
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <!--// Slider //-->
        </div>
        <div class="container wrapper">
            <div runat="server" visible="true" id="divScrollingText" style="color: #0d3fd1; font-size: 14px; font-family: Verdana; font-style:italic; font-weight: 600; padding-top: 5px;">
            </div>
             <%--<marquee style=" overflow: hidden; position: relative; background: #fefefe;
                    color: Red; font-style:italic; ">Due to maintenance activity by IT secretariat team,  GO-SWIFT portal will not be available to users till 11:59 PM on January 10, 2023.Inconvenience caused is regretted.</marquee>--%>
            <section class="gowelcome">
                <div class="row">
                    <div class="col-md-8 col-sm-7">
                        <div class="welcomemsg welcomehgt">
                            <h2>Welcome to <span>Go-Swift</span></h2>
                            <p style="text-align: justify">
                                The Government of Odisha has developed the online Single Window portal, GO SWIFT
                        i.e. Government of Odisha – Single Window for Investor Facilitation and Tracking,
                        to transform the B2G interface through the entire investment lifecycle. GO SWIFT
                        is a key business reform undertaken by the state government with the objective to
                        provide all requisite information/clearances to investors in a hassle-free and paper-less
                        manner. The portal is a “One-stop Solution” for information on clearances required;
                        land banks available; application, payment, tracking & approval of G2B services;
                        risk-based synchronized inspection by regulatory agencies; incentive administration;
                        post land allotment services; grievance redressal; and dovetailing CSR activities
                        with the developmental goals of the State.
                            </p>
                            <%-- <p>With a vision to promote conducive business environment by bringing in transparency, improving efficiency and assuring time-bound clearances to the investors, the Industries Department of the Govt. of Odisha has developed a new Single Window Portal, called GO-SWIFT i.e. Government of Odisha’s Single Window for Investor Facilitation & Tracking.</p> 
                    
                    <p>GO-SWIFT is a big boost for Ease of Doing Business in the state as it assists investors in obtaining the requisite clearances / approvals to establish and operate the proposed enterprise in a hassle-free and paper-less manner. The Portal is a ‘one-stop solution’ for information, registration, approvals, payment and application tracking for clearances/approvals without any need for a physical touch point.</p>
                            --%>
                            <a href="aboutus.aspx?id=1" class="rmore">Read More</a>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-5">
                        <div class="subscription welcomehgt">
                            <span class="pull-right">
                                <asp:DropDownList CssClass="form-control" TabIndex="16" ID="drpYear" runat="server"
                                    OnSelectedIndexChanged="drpYear_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </span>
                            <h2>
                                <span>APPROVED </span>PROPOSALS</h2>
                            <div class="proposal-portlets">
                                <div class="propodaldtls bglight-orange">
                                    <h3>
                                        <i class="fa fa-file-text-o"></i>&nbsp;<span class="counter" id="hdApplRec" runat="server">
                                        </span>
                                    </h3>
                                    <h4>Application
                        <br />
                                        Recieved</h4>
                                </div>
                                <div class="propodaldtls bglight-green  margin-right0">
                                    <h3>
                                        <i class="fa fa-file-text-o"></i>&nbsp;<span class="counter" id="hdApproveProp" runat="server">
                                        </span>
                                    </h3>
                                    <h4>Approved
                        <br />
                                        Proposals
                                    </h4>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="propodaldtls  bglight-metal">
                                    <h3>
                                        <i class="fa fa-inr"></i>&nbsp;<span class="counter" id="hdPropInv" runat="server"></span>
                                        <small>
                                            <br />
                                            <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label></small>
                                    </h3>
                                    <h4>Proposed Investment</h4>
                                </div>
                                <div class="propodaldtls  bglight-yellow margin-right0">
                                    <h3>
                                        <i class="fa fa-users"></i>&nbsp;<span class="counter" id="hdPropEmp" runat="server"></span>
                                    </h3>
                                    <h4>Proposed
                        <br />
                                        Employment</h4>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <p>
                                Download
                            </p>
                            <div class="sublinks">
                                <a href="images/QuickStartGuide.pdf" class="redlink" target="_blank"><i class="fa fa-file-code-o"></i>
                                    <label>
                                        QUICK START<small>Guide</small></label>
                                </a><a href="images/Odisha-Investors-Guide_1.pdf" class="blacklink" target="_blank">
                                    <i class="fa fa-file-text-o"></i>
                                    <label>
                                        INVESTOR'S<small>Guide</small></label>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!--// Slider //-->
            <!--// Notification //-->
            <div class="ticker-container" style="display: none;">
                <div class="ticker-caption">
                    <p>
                        Notifications
                    </p>
                </div>
                <div id="news-container1">
                    <ul>
                        <asp:Repeater ID="RepNotification" runat="server">
                            <ItemTemplate>
                                <li><a href="javascript:void(0)" title='<%#Eval("VCH_HEADING")%>'><span>
                                    <%#Eval("VCH_CONTENT")%>
                                </span></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <!--// Notification //-->
            <!--// About Section //-->
            <div class="about-sec" style="display: none;">
                <div class="col-sm-8">
                    <h2 style="text-transform: inherit">At a Glance
                    </h2>
                    <div id="divabout" runat="server">
                    </div>
                    <a class="ReadMore" href="aboutus.aspx?id=1" title="">Read More</a>
                </div>
                <div class="col-sm-4">
                </div>
                <div class="col-sm-4 separator" style="display: none">
                    <div class="whats-newdata">
                        <h2>News <a href="News.aspx" title="View All">View All</a>
                        </h2>
                        <ul style="display: none">
                            <asp:Repeater ID="RepNews" runat="server">
                                <ItemTemplate>
                                    <li><a href="News.aspx?annid=<%#Eval("INT_ID")%>"><span>
                                        <img src="CMSImageGallery/<%#Eval("VCH_IMAGE")%>" class="img-sec" alt="<%#Eval("VCH_HEADING")%>" /></span>
                                        <%#Eval("VCH_HEADING")%>
                                        <div class="clearfix">
                                        </div>
                                    </a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <section class="goinvestor">

                <div class="row">
                    <div class="col-sm-12">
                        <h1>First-of-its kind single window portal in india to ensure investor delight</h1>
                        <%-- <p>With a vision to promote a conducive business environment in the State by bringing in transparency and ensuring time-bound clearances, Government of Odisha has developed the GO-SWIFT Portal</p>--%>
                    </div>
                    <div class="col-sm-12">
                        <ul class="golist">
                            <li>
                                <a href="https://investodisha.gov.in/info-wizard/" target="_blank">
                                    <img src="images/wizardlogo.png" />
                                    <span class="goarrow"></span>
                                    <div class="gotext">
                                        <h3>Info Wiz</h3>
                                        <p>Access customized information about investment opportunities, incentives and requisite approvals</p>
                                    </div>
                                </a>
                            </li>
                            <li><a href="http://gis.investodisha.org/" target="_blank">
                                <img src="images/goplus.png" />
                                <span class="goarrow"></span>
                                <div class="gotext">
                                    <h3>GO PLUS</h3>
                                    <p>Select suitable industrial land on a Geographic Information System (GIS) based platform</p>
                                </div>

                            </a></li>
                            <li><a href="javascript:void(0);" target="_blank">
                                <img src="images/mainApp.png" />
                                <span class="goarrow"></span>
                                <div class="gotext">
                                    <h3>Single Window Clearance</h3>
                                    <p>Get online approval of Single Window Authority and allotment of Land from IDCO</p>
                                </div>
                            </a></li>
                            <li><a href="javascript:void(0);">
                                <img src="images/appr.png" />
                                <span class="goarrow"></span>
                                <div class="gotext">
                                    <h3>Approval & Clearances</h3>
                                    <p>Apply, e-Pay, Track and obtain approval for 32 G2B services from 15 Departments</p>
                                </div>

                            </a></li>
                            <li><a href="http://cicg.investodisha.org/iimsweb/Default.aspx" target="_blank">
                                <img src="images/gosmile.png" />
                                <span class="goarrow"></span>
                                <div class="gotext">
                                    <h3>GO SMILE</h3>
                                    <p>Avail Risk-based synchronized inspections from regulatory agencies</p>
                                </div>

                            </a></li>
                            <li><a href="javascript:void(0);">
                                <img src="images/money.png" />
                                <span class="goarrow"></span>
                                <div class="gotext">
                                    <h3>Incentive Administration</h3>
                                    <p>Apply for incentives and get sanctions under Industrial Policy Resolution (IPR) 2015</p>
                                </div>
                            </a></li>
                            <li><a href="https://esuvidha.gov.in/odisha/index.php" target="_blank">
                                <img src="images/esubidha.png" />
                                <span class="goarrow"></span>
                                <div class="gotext">
                                    <h3>SPMG Portal</h3>
                                    <p>Resolve issues with Government Departments</p>
                                </div>

                            </a></li>
                            <li><a href="https://portal.idco.in/EntrepreneurLogin.aspx" target="_blank">
                                <img src="images/goipass.png" />
                                <span class="goarrow"></span>
                                <div class="gotext">
                                    <h3>GO iPAS</h3>
                                    <p>Government of Odisha IDCO Post Allotment Services</p>
                                </div>

                            </a></li>
                            <li><a href="http://csr.odisha.gov.in/" target="_blank">
                                <img src="images/gocare.png" />
                                <span class="goarrow"></span>
                                <div class="gotext">
                                    <h3>GO CARE</h3>
                                    <p>Dovetail CSR activities with developmental goals of the state</p>
                                </div>
                            </a></li>
                        </ul>

                    </div>
                </div>

            </section>
            <section class="golinks">

                <div class="row">
                    <div class="col-sm-4">

                        <div class="goportal useflink gohgt" id="dvUlink" runat="server">

                            <%--
                <a  style="height:75px;" class="imglink"  href="http://services.ebiz.gov.in/content/services/filing_of_industrial_entrepreneurs_memorandum" target="_blank">
                    <h3> <small style="margin-top:23px;text-align:right;color:#fff">Registration for IEM</small></h3>
                </a>
                <a style="height:75px;" class="imglink" href="https://udyogaadhaar.gov.in/UA/UAM_Registration.aspx" target="_blank">
                <h3></h3>
                
                </a>
                <a style="height:75px;" class="imglink"  href="https://investodisha.gov.in/" target="_blank">
                   
                   <h4 style=" margin-top: -10px;padding-left:20px; text-align:right;" >&nbsp;</h4>
                </a>--%>
                        </div>
                    </div>
                    <div class="col-sm-8">
                        <div class="goportal landbank gohgt">
                            <div class="map-sec">
                                <h4>District Land Bank</h4>
                                <div id="pop-up" class="info-tooltip">
                                    <%--class="map_area_txt"--%>
                                    <div style="text-align: right; padding-right: 10px;">( Area in Acres )</div>
                                    <h3>ODISHA</h3>
                                    <div class="data-sec">
                                        <label id="categorya">
                                            <a style="cursor: help; color: Black" title="Land immediately available with IDCO .">Category A :</a><span>24899.121</span></label>
                                        <label id="categoryb">
                                            <a style="cursor: help; color: Black" title="Land reserved by District Collectors for industrial use.">Category B :</a><span>24899.121</span></label>
                                    </div>
                                </div>
                                <?xml version="1.0" ?>
                                <!-- Generator: Adobe Illustrator 20.0.0, SVG Export Plug-In . SVG Version: 6.00 Build 0)  -->
                                <svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"
                                    x="0px" y="0px" viewbox="-40 -10 680 480" style="enable-background: new 0 0 605 476;"
                                    xml:space="preserve">
                                    <style type="text/css">
                                        .st0, .st1, .st2 {
                                            stroke: #fff;
                                            stroke-miterlimit: 17;
                                        }

                                        .st4 {
                                            font-size: 10.0514px;
                                        }
                                    </style>
                                    <path class="st0 dist-block" id="19" d="M434.4,215.6h-0.9v0.9h-1.6v0.9h-0.8v0.8l-1.6,1.6v1.6l-0.8,0.8v0.9l-0.9,0.9l-1.6-0.1l0.1,0.9l-0.8,0.8h-0.9
	l0.1,1.7h0.9v0.9l-3.4-0.1l-0.8-0.9H419l-1.7-0.9h-1.6l-0.9-0.8H414l-0.8,0.8h-2.4L410,227h-1.6l-1.6,1.6l0.1,0.8h-1.6l-0.9,1
	l-2.4-0.2v-0.9l-0.9-0.8v-0.8h-1.6l-1.6,1.6h-2.4l-0.9-0.8l0.1,4l-0.8,0.8v0.9H393v0.8h-4v0.8h-5.8v0.9l-0.7,0.9v0.8l-0.8,1.6
	l0.9,0.9l0.1,4.8h3.3v-0.8h0.8l1.6-1.7l1.6-0.8h0.9l0.8-0.8h1.6v-0.8l1-0.9l0.9,0.2l0.7-0.9h0.8V238h2.5v1.8l-0.9,0.8l0.1,2.4h0.9
	l0.9-0.9l0.7,0.9v0.9h2.5v0.8h0.8l0.2,1.3l-0.8,0.7l0.1,1.6h-2.4l-1.6,1.8h0.8v0.8l0.7,1l0.4,0.8l0.9,0.7l1.4,0.8l0.8,0.2l0.1,0.8
	l2.6,2.3v0.9l0.8,0.8l0.1,2.6h-0.8l0.1,0.8h-0.9l-1.7,1.8l-1.6-0.3l-1.6-0.6l-0.9-0.6l-0.7,0.1l-0.8,0.2l-0.8,0.4l-2.2,1.1l-2.1,1.1
	h-4.1l-0.9,0.8h-4.1l-1.1,0.9l-1.3-0.1l-0.9-0.8H381l-1.6-1.7h-4.1l-1.6,0.9l-1.8,0.8l-0.6,0.9H367h-4.7l-0.9-0.9l-1.6-0.1l-0.8,0.8
	l-0.6,1.1l2.2,2.3l2.8,2.8l0.4,1.2l1.2,1.1l2.4-0.4l1.6,0.8l0.8,0.2l1,0.5l0.9,1.7v0.5l0.1,1.1h0.8l0.7,0.7l1,0.2l0.9,0.8l1.6,0.1
	l0.9,0.8v0.9l0.9,1.7l2.4,1.5l0.8,0.9v0.8l-0.1,0.9l1,0.7l2-0.9l1.6-1.6V286l0.8-0.8l1.4-3.1l2.5-2.9h1.6l1.6-0.8h3.3l4.9-4.9
	l1.6-0.7l3.1-3.5h0.8v-0.8l1.9-0.1l0.7,0.8h1.8l1.6,0.9h0.8l0.9,0.9h1.6l0.8-0.8h1.4l-0.4-5.7h-3.3v-0.9h0.8v-0.8l-2.5,0.1l0.1-1.1
	l0.8-0.7v-0.9h1.7l0.8,0.9l1.6,0.2v0.8h0.9l-0.1-2.4l0.8-1l-1.1-0.6l0.2-1.1l1.6-1.6l-0.1-0.8h0.8l-0.1,0.7l1.1,0.9l2.3-2.3h-0.8
	l-0.1-2.5h0.9l-0.1-2.6l0.9-0.6l-0.1-0.6l0.6-0.4l0.1,0.5l0.9,1.3h2.6l-0.2-2.3l0.8-0.2l-1-1.2l0.3-1.4l-1.6-1.6l0.8-0.8h1.6v-0.8
	h1.8v0.8h0.8l0.1,0.8l0.8,0.8l0.8-0.8h2.4l0.9,0.9h0.8l-0.2-3.9l1.6-0.2l0.9,0.9l1.8,0.1l-0.1-0.8l0.8-0.9h5.7l0.8,0.9l0.1,0.8
	l-1.6,1.6l0.1,1.6l0.9,0.9l2.4,0.1l0.9-0.9l1.6-0.1l0.1,2.4l1.6,0.2V245h0.9l0.6-0.8h0.9l0.9,0.8h4l0.8,0.9l1.7-0.9h3.3l-1.7-1.6
	h-2.5v-0.9l-0.9-0.9v-0.8l0.8-0.8v-1.6h-2.3v-0.9h-1.1L462,236h-0.8v-1.6h-0.8l-0.9-0.8h-1.6l-0.7-0.9l-0.1-1.6l-0.8-0.8v-0.9
	l-1.8-1.6l-1.6-0.9H452l-0.1-1.6l2.5-0.1l-0.1-1.6l-0.8-0.8V222h-0.9l-0.9-1.6h-0.9l-0.8-0.9h-0.8l-0.8,0.9l-1.8-1.8l-3.2-1.6
	l-0.1-2.4h-0.8l-0.1-0.8l-1.6-0.1l-1.7-0.8h-0.8v0.9h-0.8l0.2,3.2H436L434.4,215.6z" />
                                    <path class="st0 dist-block" id="16" d="M287.9,298v-2.8l0.8-0.9h0.8l0.8-0.8v-0.8l0.8-0.9h-1.6l-0.9-0.9l-0.9,0.1l-1.6-1.6v-1.6l-1.7-0.2v-0.9
	l-0.9-0.8h0.9v-0.8l-0.9-0.8h-0.8l-1.6,0.8l-0.1-2.5l-0.9-0.7V281l-0.8-0.9v-0.9l-0.8-0.8v-0.8h0.8l0.6-0.8l0.2,0.8h0.8l0.8-0.9
	l-0.8-0.9V275l0.8-0.8h0.8l0.8-0.9h0.5l0.9-0.8l-0.9-0.1l-0.9-0.8V270l6.6,0.1l1.6-0.9h0.8v-0.8h0.8l0.9-0.9v-0.8h0.9l0.8-0.9h0.9
	l0.1,0.9h0.8l0.1,3.3h1.6l1.6-0.8h0.9l2.5,2.9h2.5v0.8l0.8-0.8l0.8,0.8h1.8l-0.2-5.7l1.5-1.6v-0.9l-0.9-0.7l-0.1-2.5h-4.9v-0.9
	l-1.6-1h-1.6l-0.9-0.8h-0.9h-2.4l-0.8-0.8l0.8-0.8l-0.1-0.9l2.4-2.4h0.9l0.8-0.8h2.3l1.8-0.8l-1-0.8v-0.9H304v-2.5l0.7-0.8v-0.9
	v-0.9l0.8-0.8v-0.8h0.8V243l0.8-0.9v-0.8l-0.8-0.8l-0.1-2.5l0.9-0.8H307v-0.9l0.8-0.1v0.9l0.8,0.1v0.5l0.8,0.9l0.1,0.9l0.8,0.8v0.8
	h0.8l0.9,0.8l1.6,0.1l0.7-0.8v-0.8l1.6-0.1v0.8l1,0.8h0.9l-0.1-0.8l0.9,0.1v-1.6h1.6l1-0.6l0.9-0.8l-0.9-0.9l-0.9-1.6l-0.8-0.9V234
	l-0.9-0.9l-0.1-0.8h-0.8l-0.1-4.1l0.8-0.9l-0.1-3.4l-0.9-0.8h-0.9l-0.8,0.8l-0.8-0.6l-2.4-0.2l-0.1-0.8h-0.9v-0.1h0.9l1.5-1.6v-0.8
	l-3.3,0.8V219l-0.9-0.9l-0.2-4.9l-3.2-3.3l-0.1-2.5l-1.6-1.6h-0.9l-2.4-2.6l-0.1-0.8l0.9-0.9l-0.1-2.4l-0.9-0.9v-1.6l-4.4-4.1h-0.8
	v-0.9h-0.8l-0.9-0.9h-0.9l-0.8,0.9h-0.9l-0.8,0.9l0.2,0.8h-0.9v0.9l0.9,0.9l-0.8,0.8v0.9l-0.8-0.1v1.6h-0.8l-0.8,0.9h-1.5l-0.9,0.9
	h-0.9l-0.7,0.7h-1.6l-1.7-1.6l-0.9-0.1l-1.6,1.6h-2.5l-1.6-0.8l-2.6,0.1l-1.5-0.9h-0.9L268,200h-0.8l-0.8,0.7h-0.9l-0.9,0.8
	l-1.5,0.1l-1.7-0.9l-0.9-0.7h-3.3v-0.9h-0.8l-0.5-1.1h-0.8v1.7l-1.6,0.8h-4.1v0.8l-1.6,1.7H247l-0.8,0.7l-0.8,1.7l-0.8,0.8v0.9
	l-0.8,1l0.1,0.7l0.8,0.8v1.6l0.8,0.8h0.9l0.8,0.9l-0.8-0.1v0.8l-3.3,0.1v1.6l-0.7,0.9l-1-1h-0.8l-0.8-0.9H239l-0.9,0.9H234l-0.8-0.9
	h-1.8l0.2,1.7l-0.9,0.8l0.1,1.7l0.9,0.8l1.6,0.8l1.6,1.6v0.9l1.7,1.6l0.1,3.3l0.9,0.9l0.2,7.4h0.9v0.8l4.9,0.1l0.2,2.6l-0.9,0.8v0.8
	h-0.8l-0.8,0.7H239v1.7h-0.9l0.2,1.6l-0.8,1.8v0.8l-0.9-0.2v0.9h-0.8v-0.9l-4.1-0.1l-0.8,1h-0.8l-0.9-0.9l-1.6-0.8l-2.5-0.1v-0.8
	l-1.7,0.1l0.2,0.8l-0.8-0.1l-0.9,0.8v0.9l-0.6,1.1v0.8l-0.9,0.8l0.1,1.6h-0.9l0.9,0.8l0.1,2.5h-0.9v-0.8h-0.9l-2.3,3.2l-0.8,0.9
	l0.8,0.8h1.7l0.8,1h0.9l1.8,1.6l0.1,1.6h-0.9l0.1,2.5l-1.9,1.6l0.2,3.3h0.9l1.6-0.9l1.6,0.1l0.8,0.9l0.9-0.1l1.7,1.7v1.6l0.9,0.8
	v1.7l-0.8,0.7v0.9h0.8l0.9,1.7l0.1,1.6l0.8,0.9v0.9l0.8,0.8v0.8l0.9,0.8l0.2,3.3h0.9l0.7-0.8v-1.7h0.9l0.8-0.8l2.4,2.5l0.1,0.8
	l0.9,0.9h0.8v-0.9l1.6-2.6l2.4-2.3h1.6v-0.9h4.1l0.8,0.9h0.8v0.8h0.9l0.1,1.6l1.7,1.6l2.4-0.1l0.8-0.8l2.5,0.1v0.8l0.9,0.9l0.1,2.5
	h2.3v0.8l0.9,0.9h2.5v-0.9l0.8,0.9l1.6-1.7h0.9l-0.1-0.9l2.5,0.1v0.9h2.6l0.7,0.8l0.9-0.1l0.9-0.7h2.5v0.8h0.9v1.6l0.8,0.9v0.7
	l4.9,0.1l0.9,0.8l1.6,0.9h0.8" />
                                    <path class="st0 dist-block" id="22" d="M542.9,49.2l-1.7-0.9h-5.7v-0.9l-0.9-0.8h-0.8l-0.8-0.8v-0.9l-0.9-0.8l-0.1-1.6l-7.4,1.6l-0.1-4.9h0.9l-0.1-0.9
	l-0.9-0.8h-1.6l-0.8-0.9l-0.1-0.9l-0.9-0.8l-1.6-0.9H517v0.9l-0.9-0.9l-0.1-1.5l0.9-0.9H516l-0.9-0.9h-1.6l-2.5-2.4h-0.9l-1.6-0.9
	l-3.2-0.2v-0.8l-1.8-1.6l-1.6-0.9h-3.2l-0.9,0.8l-0.8,1.7h-0.8l-0.8,0.8h-1.6l-2.5-2.5l-0.9,0.1l-1.6-1.6h-0.9l-0.1-0.9H487v-0.8
	l-1.6-0.9l-0.9-0.8l-0.9-1.6l-3.3-3.2l-0.9-0.1l-1.6-0.9l-1.6,0.2l-0.9-0.8l-3.3-1.8h-1.6l-1.7-0.9h-4l-0.2-0.8h-0.8L462.8,9V8.4
	l-0.9-0.7V6.8l-1.6-1.7l-1.7-0.9h-0.7l-0.8-0.8l-0.8,0.9l-0.9-0.1l-0.7,0.9l0.9,0.8v0.8l-0.8,0.9h-6.6l-0.7,0.8v0.9l0.8,0.9l0.8-0.1
	V11h1.7l0.9,0.8h-0.9l0.1,3.2l0.9,0.1V16h0.8v0.9h0.9l2.5,2.3v0.9l-0.6,0.8v0.8l-0.9,0.8l0.1,2.6l0.8,0.9l0.1,0.8h-0.9l0.9,0.8
	l1.6-0.8h0.8v0.8l-1.6,1.7h-1.6l-1.6,1.6v0.9l-0.7,0.9l0.1,2.3l0.7,0.8l0.1,0.9l0.8,0.8l1.8,0.9l0.1,2.5l-2.6,2.4l0.1,2.4l0.9,0.8
	l0.1,2.6l-0.9,0.9l-0.7,1.6l-1.6,0.8l-0.9,0.9l0.1,0.9l-1.6,0.9l-0.9,0.8l-0.8,1.6l-0.8,0.8l-0.7,1.6l0.1,2.6l-0.9-0.9h-4.1
	l-1.6-0.7h-5.9l-0.8-0.9l-0.1-0.9h-0.8v-0.8l0.8-0.8v-0.9l0.8-0.8v-3.3l1.6-1.6h0.8l0.9,0.7l1.6,0.1l-0.1-1.6l-0.8-0.1l-0.9-0.8
	v-0.9l-1-1.1h-1.6l-0.9,0.8l-1.7,0.8v1.6l-0.8,0.8l-1.6,0.9h-3.2v-0.9h-0.9v0.9l-0.9,0.8l0.1,0.9H423v2.4h1.6l0.9,0.9v1.6h-0.9
	l0.1,0.9h3.3l1.6,1.6h0.9l0.9,0.8l0.8,0.2l1.8,1.7v0.4v0.2l0.2,2.6h0.8V68l0.8,0.8l0.1,4.1l0.8-0.1l0.2,4.1l2.5,0.1l1.6,1.6h0.8
	l0.9,0.9l1.6-0.3l0.8,0.9l0.9,1.6l0.8,0.9h3.3l0.8,0.8l4.1,0.2l0.9,0.9h0.8l0.8,0.8l0.1,2.5h0.9v0.8h1.6l0.8,0.7l0.8,0.1l1,1.6
	l0.1,1.7l0.9,0.9v0.6l-0.8,0.9v1.6H462l0.1,0.8l-0.9,0.9l-0.8-0.1l-0.8,0.8l-0.8,1.6l-0.8,0.9h-0.8v0.8h-0.8v1.7h0.8l0.1,0.9
	l1.6,1.6l1.7,0.8l0.8,0.9h1.7l1.6,1.6h1.6l1.6,1.6h0.9v0.9l0.9,0.7l0.9,0.1v2.6l-0.8,0.7l-3.1,5.1l-0.8,0.8v0.8l-0.9,0.9l0.1,2.3
	l0.9,0.9v0.9h0.9v1.6l0.8,0.9h0.8l-0.1-0.9l2.5,0.1l0.9,0.8h0.9l0.8,0.8h0.9l2.3-2.3v-1l0.9-0.9v-1.6l3.9-4.1l-0.1-1.7l0.8-0.9h1.6
	l0.9,0.9h0.7l1.8,1.6h0.8l1.7,1.6h0.9v0.8l0.9,0.9l0.1,0.8l-0.9,0.8v0.9l4.1-0.9l1.6-0.8l0.8-1.7v-0.7h0.8l3.2-1.6l2.4-2.5l1.6-0.7
	l1.6-2.5l0.8-1.7l2.5-2.4v-1.7l4.1-4.1h2.3l0.9-0.8l-0.1-0.9l2.3-2.4V101l1.6-1.7l3.4,0.1l1.6-1.6h9.9l0.8,0.9l2.5,0.1l0.9-0.9
	l-0.2-3.2h0.8l2.5,2.3l0.1,0.9l1.6,1.6l6.6,0.2l0.8-0.8V98l0.7-0.9l-0.7-0.8v-0.8h-0.9l-0.2-0.9l1.6-1.7l-0.9-0.8v-1.6l0.7-0.9v-0.7
	h0.9v0.7l0.9,0.9l-0.1-0.9l0.9-0.1v-0.7l0.8,0.1V88h0.9l-0.9-0.9v-1.6l-0.9-0.9l-0.2-1l-0.8,0.1v-1.6L556,82l0.8,0.9l0.1,1.8
	l0.9,0.8v0.8h1.6l0.8-0.8v-2.6l0.8-0.8h2.4v-0.9l-0.9-0.9l-0.8,0.1l-0.8-0.9l-0.1-0.8h2.4l0.1,0.8l0.8,0.8h0.7l0.9,0.9h0.9v-0.9h0.9
	l0.8-0.9v-0.8l-0.9-0.8l1.6-1.7v-0.9l-0.9,0.1l-0.9-0.7h-0.8l-0.1-0.9l-2.4-0.1l-2.6-2.3l-0.8-0.1v-0.9h-0.9l-0.8,0.9h-1.6l-0.9-0.9
	l-0.9-0.1l-2.5-2.3h-0.6v-1.6l-0.8-0.9l-0.1-1.6h0.8v-1.6l0.7-1.6l0.9-0.9l-0.1-0.9l0.8-0.2l-0.8-0.8l-0.1-3.9l-0.9-0.9l-1.6-0.1
	l-4.3-4.1L542.9,49.2z" />
                                    <path class="st0 dist-block" id="2" d="M566.9,61.8l-1.6,1.6l0.1,2.4l-0.9-0.9H563l-0.1-0.8h-0.8l-2.4,2.6l0.1,1.6l0.8,0.8l-0.8,0.8v0.9h0.9v0.9
	l0.8,0.1l2.6,2.3l2.4,0.1l0.1,0.9h0.8l0.9,0.7l0.9-0.1v0.9l-1.6,1.7l0.9,0.8v0.8l-0.8,0.9h-0.9v0.9h-0.9l-0.9-0.9h-0.7l-0.8-0.8
	l-0.1-0.8H561l0.1,0.8l0.8,0.9l0.8-0.1l0.9,0.9v0.9h-2.4l-0.8,0.8V86l-0.8,0.8H558V86l-0.9-0.8l-0.1-1.8l-0.8-0.9l-1.7,0.1v1.6
	l0.8-0.1l0.2,1l0.9,0.9v1.2l0.9,0.9h-0.9V89l-0.8-0.1v0.7l-0.9,0.1l0.1,0.9l-0.9-0.9V89H553v0.7l-0.7,0.9v1.6l0.9,0.8l-1.6,1.7
	l0.2,0.9h0.9v0.8l0.7,0.8l-0.7,0.9V99l-0.8,0.8l-6.6-0.2l-1.6-1.6l-0.1-0.9l-2.5-2.3h-0.8l0.2,3.2l-0.9,0.9l-2.5-0.1l-0.8-0.9h-9.9
	l-1.6,1.6l-3.4-0.1l-1.6,1.7v0.9l-2.3,2.4l0.1,0.9l-0.9,0.8h-2.3l-4.1,4.1v1.7l-2.5,2.4L507,116l-1.6,2.5l-1.6,0.7l-2.4,2.5
	l-3.2,1.6h-0.8v0.8l-0.8,1.7l-1.6,0.8l-4.1,0.9l0.1,2.4h0.9l0.1,0.8l0.7,0.9v0.9h0.9l-0.8,0.7h-0.7v0.9l1.6,1.6h2.3l0.9,0.9h0.9v0.8
	h0.8l1.6-1.6l0.9-0.1v0.9h0.8l-0.8,0.8v0.9l-0.8,0.1v0.9h-0.9l0.9,0.9v2.3l1,0.8v0.8h1.6v-0.8h0.9l0.9,0.8h0.8l0.8,0.9h0.8v-0.9
	l0.8-0.6v-0.9h4.1l0.9,0.9v1.6l0.8,0.8h0.9v0.9l0.9,0.7v0.9h1.6v1.7l0.8,0.8l0.9,0.1l0.9,0.9v0.8h1.6v-0.8l1.7-0.1l-0.1-0.9
	l-0.8-0.7v-0.9l0.6-1.8v-0.8l-0.8-0.8v-0.8l0.8-0.9v-1l0.8-0.8h0.8l0.8-0.9v-0.8l0.8-0.8h1.6l1,0.8h5.8l0.8-0.6v-0.9l-0.9-0.9
	l-0.1-3.3h1.6v-0.9l0.7-0.1v0.9h0.9V134l0.9,0.1l1.7,1.6l-0.1-0.8l0.9-0.9v-0.9l0.8-1.6l0.7-0.8v-1.6l1.6-1.8l0.9-1.6l0.8-0.8
	c0.5-0.3,1-0.6,1.6-0.9l1.6-3.2l9.6-10l0.8-1.6v-0.8h1.6v-0.8h0.9l0.8-0.8h0.8l1.6-1.7h1.6l0.9-0.8h0.8l0.9-0.9h0.8v-0.9h1.8
	l0.8-0.9h1.5l1.6-0.9h4.9l1.7-0.8h5.6v0.9h2.7l0.8-0.8l-0.9-0.9v-0.7h0.8v-0.8l0.9-0.1l0.7-0.8h1.6l0.9-0.9h1.6l0,0h1.6l0.8-0.8
	l1.7,0.2l-0.1-1.7h-0.9v-1.7h-0.8l-0.9-0.8v-2.8l0.7-0.9v-2.5L598,84v-0.8l-0.9,0.1l-0.8-1.7v-0.9l-0.8-1.6L588,79v-0.8h-3.3
	l-1.6-0.8l-0.9-0.8l-1.6-0.9h-0.9v0.9l-3.3-3.2l-0.2-4.9l1.6-1.6L577,66v-0.9l-0.9-0.7l-1.8-0.2l2.5-2.4H576V61h-0.9v0.8h-2.4V61
	l-3.2-0.1l-0.8-0.9l-1,0.1l-0.9-0.9L566.9,61.8z" />
                                    <path class="st0 dist-block" id="4" d="M499.8,135.6l-1.5,1.6h-0.8v-0.8h-0.9l-0.9-0.9h-2.3l-1.6-1.6h-0.9l0.1,1.6l0.9,0.9H491l0.1,1.7l-1.7,1.6h-0.8
	l0.1,0.8h-4.2v0.8h-0.8v0.9l-0.8,0.9v0.7h0.9l3.3,3.3v0.9h0.9l0.1,0.8l0.9,0.9v1.6h-0.9v0.8l-0.8,0.9l0.1,0.8l-0.9,0.1v0.8h-0.7
	l-0.9,0.8l0.9,0.9v0.8l0.7,0.9v0.8l-0.6,1.6l-0.8,0.9l0.1,4.1l0.8,0.8l2.5-0.1v0.9l1.6,0.8l0.1,3.4h0.9l0.8,0.9l1.6-0.9h0.8l1.7-0.8
	h0.8l0.9,0.8h1.6l1.6,0.9l1.8-0.1l0.7-0.9l3.2-1.6h3.3l0.9,0.9h4.9l0.9,0.7h0.9v0.9l0.8,0.9l0.1,0.9l0.9,0.8l0.8,1.7v0.7h4.2
	l1.6,0.8l0.8,0.9h0.9l0.9,0.9h5.1l0.7,0.8v0.8l0.9,0.8h0.9l0.8-0.8h2.4l0.9-0.8h0.8l0.8-0.8l1.6-0.8l0.8-0.9h0.9l1.6-0.8h4.1
	l1.7,0.8h0.8l0.8,0.9h0.8l-0.8-0.9l-0.1-4.1h-0.8V173l-0.9-0.8l-0.9-1.7l-0.9-0.8l-0.8-1.6v-1.6l-0.9-0.8l-0.9-1.6l-2.5-2.5
	l-0.1-0.9l-1.8-3.4v-0.8l-0.9-0.8l-0.7-1.7l-0.1-0.7l-0.9-1.8l-0.1-0.8l-1.6-2.5l-1.7-4.1l-0.9-0.7H535l-0.1-0.8l2.4-0.1v-0.9
	l0.9-0.8l-0.1-2.4l0.8-0.9v-0.8l0.8-0.8l-1.7-1.6l-0.9-0.1v0.9h-0.9v-0.9l-0.7,0.1v0.9h-1.6l0.1,3.3l0.9,0.9v0.9l-0.8,0.6h-5.8
	l-1-0.8h-1.6l-0.8,0.8v0.8l-0.8,0.9h-0.8l-1,0.6v0.6l-0.8,0.9v0.8l0.8,0.8v0.8l-0.6,1.8v0.9l0.8,0.7l0.1,0.9l-1.7,0.1v0.8h-1.6v-0.8
	l-0.9-0.9l-0.9-0.1l-0.8-0.8V148h-1.6v-0.9l-0.9-0.7v-0.9h-0.9l-0.8-0.8v-1.5l-0.9-0.9h-4.1v0.9l-0.8,0.6v0.9h-0.8l-0.8-0.9h-0.8
	l-0.9-0.8h-0.9v0.8h-1.6V143l-1-0.8v-2.3L499,139h0.9v-0.8l0.8-0.1v-0.9l0.8-0.8h-0.8v-0.9L499.8,135.6z" />
                                    <path class="st0 dist-block" id="13" d="M483.9,155.3l-1.6-1.6H479l-0.9-0.8h-2.5L474,152h-0.8l-0.9-0.8h-1.7l-0.9-1h-0.9l-0.7,0.9h-0.8l-2.4,2.5v1.6
	H464v-0.8h-0.8l-0.9-0.9h-0.8l-1.6-1.7v-3.3h0.8v-0.9l0.8,0.1v-1.5h0.8l0.8-0.8v-0.9h0.8v-3.2l-3.3-0.1l-0.9,0.9h-0.9l-0.7,0.9
	l-1.7,0.7l-1.5-0.8v0.7H454l-0.9,0.9l-1.6,0.1l-0.8,0.8h-0.9v0.9l-0.9,0.8v0.8h-0.8l0.1,0.9h-4.9l-0.9-0.9l-1.6-0.1l-0.9-0.8h-4.6
	v-0.8l-1.6-1.7v-0.9h-0.9l-0.9-0.7v0.6l-2.4,0.1v-0.7l-0.8,0.7l-1.6-0.1v0.9l-0.8,0.9l0.1,3.3l3.3,3.4l1,1.6v4.1l1.6,1.6h0.9
	l1.8,0.8l2.3,0.1l0.9,0.9l1.6-0.2v0.9l1.7,1.6h0.8l0.9,0.9l2.4-0.2l1.6,0.9h2.4l0.1,2.4l-0.7,0.9v0.9l-0.8,1.6v1.7h0.8l0.9,0.9
	l1.7,0.1l1.6-0.9l0.1,0.9h0.9l0.9,0.8v0.9l1.6,0.1l0.9,0.9h1.7l1.6,1.5H460l0.2,5.9l0.8,0.8h-0.8l-9.8,2.5v1.5h0.8l1.8,0.9l1.6-0.1
	l1.7,1.7l1.6,0.9l3.4,3.4v0.8h0.8l0.1,0.9l0.7,0.8v0.8l0.9,0.8l-0.1-0.8h1.7l0.9-0.7l-0.1-1.6l0.8-0.9l2.4-0.1l2.4-2.4h0.9l0.8-0.9
	h1.6l0.9,0.9l0.8-0.9h2.5v0.9h-1.6v2.5l2.4,0.1l0.9,0.8l0.1,0.9l1.6-0.1l0.9,0.9l2.4,0.1v0.8l0.9,0.8h0.7h2.6v-0.8h0.8l-0.1-4.1h1.7
	v0.8h0.9v0.8l0.8,0.9h4.1l2.3-2.4h0.9l-0.1-0.8l0.9-0.9h2.4l-0.1-1.8h0.8l-0.8-0.7l-0.8-0.1v-1.3h0.8v-0.8h3.1l0.9-0.7l0.9-0.1v-0.8
	h3.3v-0.9l0.8-0.7h0.8v-0.9h0.9l-0.1-0.8l-0.8-0.8v-0.9l-0.9-0.9l0.8-0.7l2.6-0.2l3.1-3.3v-0.5l-0.8-1.7l-0.9-0.8l-0.1-0.9l-0.8-0.9
	V171h-0.9l-0.9-0.7h-4.9l-0.9-0.9h-3.3l-3.2,1.6l-0.7,0.9l-1.8,0.1l-1.6-0.9h-1.6l-0.9-0.8H496l-1.7,0.8h-0.8l-1.6,0.9l-0.8-0.9
	h-0.9l-0.1-3.4l-1.6-0.8V166l-2.5,0.1l-0.8-0.8l-0.1-4.1l0.8-0.9l0.6-1.6v-0.8l-0.7-0.9v-0.8l-0.9-0.9H483.9z" />
                                    <path class="st0 dist-block" id="9" d="M430.2,142v-0.8h-1.6v-0.9l-2.5-0.1l-0.9,0.9h-0.7l-0.8,0.9l-3.4-0.1l0.1,0.9l-0.9,0.6h-0.9l-0.7,0.8l-1.6-0.8
	h-1.7l-0.8-0.6l-2.4-0.1v0.7h-0.8v0.8l-0.8,0.9v0.9l-0.9,0.8l-2.5-0.1l-0.8-0.8h-0.9l-3.4-3.2h-0.7l-1.7-1.7l-6.6-0.1l-0.9,0.9h-0.8
	l-0.8-0.9h-3.3v0.9h-0.8v0.9h-0.8v2.3l-0.8,0.8v2.5l-0.8,0.8v0.8h-0.8v0.9h-4.9v0.9l-0.8,0.8h0.8l0.1,0.8l1.8,0.9l0.8,0.9h0.7
	l1.7,0.8l1.6,1.6l0.1,0.9h0.9v0.8l-0.9,0.8l0.1,0.9l-0.8,0.8l0.1,3.2l0.8,1.7v0.8l0.9,0.8l0.2,2.5l-0.9,0.8l0.2,2.6l0.9,0.9v0.9
	l0.9,0.8v0.8h0.9v0.8h-3.3l-0.8,0.8l-1.7,0.1v0.8l1.7,1.6l0.1,2.5l-1.6,0.9l-0.7,1.6v0.8l-0.8,0.8l0.1,1.7l-0.9-0.1l-0.8-0.9
	l-0.1-0.9l-0.8-0.8l-1.8-3.3v0.9l-1.6,1.6v0.8l-1.6,1.6v0.9l0.8,0.8v0.9l1,1.7l0.1,3.4h1.6l1.7,1.6h1.6l0.1,1.7l0.9,0.8l0.1,1.6
	l0.9,0.8v0.9l0.8,0.8h0.8v0.9h1.7l1.6,0.9h4.9l0.8,0.8l2.6,0.1v0.8h5.8v-0.8h6.4l0.8-1.6h0.8l1.7-1.6h4.9v0.8h1.6v-0.8h0.9l0.8-0.9
	h4v-0.9l1.7,0.1l0.8-0.8l0.8-0.1v-0.8l0.8-0.9l3.2-1.6h0.8v-0.8l1.6-1.6V195l1.7-0.8h1.6l-0.1-2.5l0.7-0.9h9.8l0.9,1h0.8l1.6-1.7
	v-1.7h0.8v-1.5l9.8-2.5h0.8l-0.8-0.8l-0.2-5.9h0.9l-1.6-1.5h-1.7l-0.9-0.9l-1.6-0.1v-1l-0.9-0.8h-0.9l-0.1-0.9l-1.6,0.9l-1.7-0.1
	l-0.9-0.9h-0.8v-1.7l0.8-1.6v-0.9l0.7-0.9l-0.1-2.4h-2.4l-1.6-0.9l-2.4,0.2l-0.9-0.9h-0.8l-1.7-1.6v-0.9l-1.6,0.2l-0.9-0.9l-2.3-0.1
	l-1.8-0.8h-0.8l-1.6-1.6v-4.1l-1-1.6l-3.3-3.4l-0.1-3.3l0.8-0.9v-0.9l1.6,0.1l0.8-0.7l-0.1-0.8L430.2,142L430.2,142z" />
                                    <path class="st0 dist-block" id="7" d="M456.2,190.7l-1.7-1.7l-1.6,0.1l-1.8-0.9h-0.8h-0.8v1.7l-1.6,1.7h-0.8l-0.9-1h-9.8l-0.7,0.9l0.1,2.5h-1.6
	l-1.7,0.8v0.9l-1.6,1.6v0.8h-0.8l-3.2,1.6l-0.8,0.9v0.8l-0.8,0.1l-0.8,0.8l-1.7-0.1v0.9h-4L418,204h-0.9v0.8h-1.6V204h-4.9l-1.7,1.6
	h-0.8l-0.8,1.6h-6.4v0.8h-5.8v-0.8l-2.6-0.1l-0.8-0.8h-4.9l-1.6-0.9h-1.7v-0.9h-0.8l-0.8-0.8v-0.9L381,202l-0.1-1.6l-0.9-0.8
	l-0.1-1.7h-1.6l-1.7-1.6H375h-3.4l-1.7-1l-0.8,0.2l-0.7,0.9v0.8l-0.9,0.8l0.1,0.8H361l-0.9,0.8h-0.8l-0.9,0.8h-1.6l-0.9-0.8h-4.1
	l-0.8-0.9h-1.7v0.9h-0.9l-0.7,1.6l-2.5,3.3l2.7,2.6l1.6,0.8h4.1l1.7,0.9h1.6l0.9,0.8h0.8l0.9,0.8l1.6,0.9l1.7,1.6l5,2.6h0.8l0.8,0.9
	h0.8l0.9,0.8h1.6l0.8,0.8h0.9v0.9l0.9,0.9h0.8l0.8,0.8h2.6v-0.8h5.7l1.6,0.9l0.9,0.8h10.6l-1.6,3.3v0.8l-0.8,1.8l-1.6,1.6l0.9,0.8
	h2.4l1.6-1.6h1.6v0.8l0.9,0.8v0.9l2.4,0.2l0.9-1h1.6l-0.1-0.8l1.6-1.6h1.6l0.8-0.9h2.4l0.8-0.8h0.9l0.9,0.8h1.6l1.7,0.9h2.4l0.8,0.9
	l3.4,0.1v-0.9h-0.9l-0.1-1.7h0.9l0.8-0.8l-0.1-0.9l1.6,0.1l0.9-0.9v-0.9l0.8-0.8v-1.6l1.6-1.6v-0.8h0.8v-1.5h1.6v-0.9h0.9l1.6,1.8
	h1.7l-0.2-3.3h0.8v-0.9h0.8l1.7,0.8l1.6,0.1l0.1,0.8h0.8l0.1,2.5l3.2,1.6l1.8,1.8l0.8-0.9h0.8l0.8,0.9h0.9l0.9,1.6h0.9v0.8l0.8,0.8
	l0.1,1.6l-2.5,0.1l0.1,1.6h0.9l1.6,0.9l1.8,1.6v0.9l0.8,0.8l0.1,1.6l0.7,0.9h1.6l0.9,0.8h0.8v1.6h0.8l0.2,1.7h0.9v0.9h2.3v1.6
	l-0.8,0.8v0.8l0.9,0.9v0.9h2.5l2.2,2.1l-0.4,2.8l0.8,0.9h2.6v0.9h0.9v0.9l0.8,0.9h3.2l0.9-0.9h0.8l-1.7-3.4l-0.1-2.6l-1.6-1.6
	l-0.1-1.6h-0.8v-0.9h1.6l0.7-0.9h0.9l-0.1-0.8h-0.8l-0.8-0.9h-0.9l-1.6-1.6h0.8l-0.1-0.9h0.8v-0.8l-4.1-1.7l-1.7-0.9h-0.8v-0.8h-0.9
	v-0.7l-0.9,0.1v-0.8l-2.5-0.1v-0.9H464l0.8-0.9l-0.9-0.8h-0.8v-0.9l0.8-0.8v-0.9h-0.8l0.3-1.6l-1.1-1.6l-1.9-1.6l0.5-0.7l0.8-0.6
	l-0.2-1.1l1.1,0.2l0.4-0.9l3.7,0.3l1.6,1.5h1.6v-0.9l1.7,0.1l1.2,1.3l7.4,0.1l0.9,0.9l2.4-0.1l0.8-0.8h2.5l-0.1-5l0.6-0.8V213
	l0.9-0.9h-0.9l-0.1-0.8l-1.6-1.7l-0.1-1.6h0.9l0.7-0.8l-0.7-0.9l-0.1-0.8l0.7-0.9v-0.8h-2.4l-0.1-0.9l0.9-0.8l0.8-2.5l0.7-0.8h-0.7
	l-0.9-0.8v-0.8l-2.4-0.1l-0.9-0.9l-1.6,0.1l-0.1-0.9l-0.9-0.8l-2.4-0.1V192h1.6v-0.9h-2.5l-0.8,0.9l-0.9-0.9h-1.6L472,192h-0.9
	l-2.4,2.4l-2.4,0.1l-0.8,0.9l0.1,1.6l-0.3,1.2h-1.7l0.1,0.8l-0.9-0.8v-0.8l-0.7-0.8l-0.1-0.9h-0.8v-0.8l-3.4-3.4L456.2,190.7z" />
                                    <path class="st0 dist-block" id="12" d="M460.9,219.7v0.8l1.9,1.6l0.8,1.6l-0.5,1.6h0.9l-0.1,0.8l-0.8,0.8v0.9h0.8l1,0.8l-0.9,0.9h0.9v0.9l2.5,0.1v0.8
	h0.9v0.6h0.9v0.8l1.3,0.2l1.7,0.9l4.1,1.7v0.8h-1l0.1,0.9h-0.8l1.6,1.6h0.9l0.8,0.9h0.8l0.1,0.8h-0.9l-0.7,0.9h-1.6v0.8h0.8l0.1,1.6
	l1.6,1.6l0.1,2.6l1.7,3.4l7.4,0.1l0.9,0.9l0.2,0.8l0.6,0.8h0.9l0.9,0.8h2.5l0.9,0.9l0.9,0.1v3.3l0.7,0.8h5.1v-0.8l-0.9-0.9h-0.9
	l0.9-0.8l1.6-0.8v-0.8l0.6-0.9v-0.8h0.9l0.9,0.7V254c0.5-0.2,1-0.5,1.6-0.8l0.8-0.8l0.8-1.6l0.8-0.9l0.7-1.6l2.6-2.6l2.3-4.8
	l3.2-3.4h0.9l0.7-1.6l11.3-6.4h0.9l0.8-0.9h0.7l1.6-1.6h1.6l0.9-0.9h0.8l-0.1-1.6h-1.6l-1.6,1.6h-0.9l-0.9,0.8l-3.2,0.1v-0.9
	l-0.9-0.7l-4.9-2.7l-5.7-0.1v0.9h-0.9l-0.9,0.9l-2.3-0.2v-0.8H504l-1.8-1.6h-0.9l-1.6-0.8h-0.8l-0.9,0.8h-1.6l-0.8-0.8h-4.9
	l-0.9-0.9l-2.9-0.4h-2.6l-0.7,0.8l-2.5,0.1l-0.9-0.9l-7.4-0.1l-1.2-1.3l-1.7-0.1v0.9h-1.6l-1.6-1.5l-3.8-0.3l-0.3,0.9l-1.1-0.2
	l0.2,1.1L460.9,219.7z" />
                                    <path class="st0 dist-block" id="17" d="M524,176.9h-4.2l-3.1,3.3l-2.6,0.2l-0.8,0.7l0.9,0.9v0.9l0.8,0.8l0.1,0.8h-0.9v0.9h-0.8l-0.8,0.7v0.9h-3.3v0.8
	l-0.9,0.1l-0.9,0.7h-3.1v0.8h-0.8v0.8l0.8,0.1l0.8,0.7h-0.8l0.1,1.8h-2.4l-0.9,0.9l0.1,0.8h-0.9l-2.3,2.4H494l-0.8-0.9v-0.8h-0.9
	v-0.8h-1.7l0.1,4.1h-0.8v0.8h-2.6l-0.7,0.8l-0.8,2.5l-0.9,0.8l0.1,0.9h2.4v0.8l-0.7,0.9l0.1,0.8l0.7,0.9l-0.7,0.8h-0.9l0.1,1.6
	l1.6,1.7l0.1,0.8h0.9l-0.9,0.9v0.9l-0.6,0.8l0.1,5h2.4l0.9,0.9h4.9l0.8,0.8h1.6l0.9-0.8h0.8l1.6,0.8h0.9l1.8,1.6h9.1v0.8l2.3,0.2
	l0.9-0.9h0.9v-1.4l5.7,0.1l4.9,2.7l0.9,0.7v0.9l3.2-0.1l0.9-0.8h0.9l1.6-1.6h1.6v-0.9h-0.8v-0.9l3.1-3.2v-0.8l0.8-0.8V215l0.7-0.8
	v-1.7h-0.8l-0.1-0.9h-0.9l0.1,0.9l-0.9,0.8l0.2,5h-0.7v0.9l-0.8,0.8h-0.9v-0.9l0.8-0.8v-0.8h-0.8v0.8H534v0.8h-0.9l-0.8,0.9
	l-0.8-0.1v-0.8h0.8v-0.8l-0.8-0.9h-0.8v-0.9l0.6-0.8v-0.9l0.8-0.8l-0.1-4.1l3.3-3.1l-0.1-0.9l0.9-0.8h0.9l3.1-3.4l2.3-1.6l1.6-1.6
	l3.3-1.6l14.6-9.9l-0.2-0.9h0.9v-1.6l-0.9-0.7V182h-0.8l-0.1-0.9l-0.8-0.9h-1.6l-0.9-0.8h-1.6l-1.8-0.8h-0.8l-0.8-0.9h-0.8l-1.7-0.8
	h-4.1l-1.6,0.8h-0.9l-0.8,0.9l-1.6,0.8l-0.8,0.8h-0.8l-0.9,0.8h-2.4l-0.8,0.8h-0.9l-0.9-0.8v-0.8l-0.7-0.8h-5.1l-0.9-0.9h-0.9
	l-0.8-0.9L524,176.9z" />
                                    <path class="st0 dist-block" id="14" d="M295.8,57.2l-0.9-0.8h-0.6l-0.9-0.9h-8.2l0.2,3.3l-0.9,0.8l-1.6,0.9l-0.8,0.1l-0.8,0.9h-0.8l-1.6-1.8l-1.7-0.8
	h-0.9l-0.9-0.9h-0.9v-0.8h-1.6l-1.6,1.7h-0.8l-0.9,0.8h-0.8l-0.9,0.9l0.2,2.5l-0.7,0.8v0.9l-2.4-0.1v-0.8h-1.7l-0.9-0.8l0.1,0.8
	l-0.9,0.8l0.2,2.5h-4.9v-0.9l-0.9-0.9v-0.8h-5.7l-0.9,0.8h-0.8l0.1,0.8l-1.6,0.1v0.9l-0.9,0.7l0.1,0.9h-1.7v0.8h-0.8l-1.6,1.7v0.8
	h-0.9l-0.8,0.9h-1.8l-0.8,0.9l-0.8,1.6l-1.6,0.8l-0.8,1.6l-1.6,1.7v0.9l-2.5-0.1l-0.8-0.9v-0.9l-0.9-0.8h-3.3v-0.9l-0.9-0.7v-0.9
	l-0.8-0.9v-0.7h-0.6l-0.8,0.6v0.9l-0.8,0.9h-0.9l-1.9-0.8l-3.1-0.1v1.6l0.9,0.8l0.1,1.6h-1.7l-3.2-3.2v0.8l-0.9,0.8l0.2,0.8
	l-0.8,0.9l0.1,1.7l0.8,0.8l-0.8,0.9v1.6l0.8,0.8l-1.6,1.6l-0.9,1.6l-0.8,0.8h-1.5l-0.9,0.9v0.9l0.9,0.6h0.8v0.9l0.8,0.8l1.7,0.1
	l1.6,0.9l0.8-1.7h1.7v-0.8l1.5-1.6v-0.9l0.8-1.5v-0.9l0.8-0.9v-0.8h0.8l0.8-0.8h1.8l0.8,0.8h2.5l3.2,1.8h0.8l1.7,0.8h0.9l1.6,1.6
	v1.6l0.9,0.9v0.8l5.1,5.1h0.8l3.4,3.3l0.1,1.6l0.8,0.8l0.9-0.8v-0.9l0.6-0.8l10.6,0.1l0.9-0.9l1.6,0.1l0.9-0.9l-0.2-1.1l0.9-0.7
	l-0.1-1.7l-0.8-0.8l-0.2-4.2l-0.8-0.8v-0.9l-0.9-1.5l-1.6-1.7v-0.8l0.9-0.2l0.8-0.6v-0.9l0.8-0.9v-0.8l1.6-0.8h2.5l1.6,1.6h1.7v-0.8
	l0.8-0.8h2.5l0.7,0.8h2.5l1.6-1.6l7.4,0.1l0.9,0.9l2.3-0.1l0.9,0.8l0.8,0.1v-0.8h2.4l0.8-0.9l-0.1-2.4l0.8-0.9v-0.8l3.2-3.2
	l-0.1-3.4l0.8-0.8l0.8-1.6l0.9-0.8l-0.1-0.8l0.8-1.7l-0.1-2.5l0.7-1.6L295.8,57.2L295.8,57.2z" />
                                    <polygon class="st0 dist-block" id="30" points="393.9,68.3 393.8,65.2 392,63.4 391.2,63.4 390.4,62.6 383,62.5 379.8,59.2 379.7,58.4 380.5,57.5 
	380.5,56.7 379.7,56.7 378.6,55.7 378,55.1 378,53.5 377.1,52.7 376.2,52.7 375.4,51.8 374.6,51.7 373.7,51 372.9,51.7 372.9,52.6 
	372.1,51.8 372.1,51 371.2,50.2 368.8,50.2 368,49.3 364.6,46.9 360.5,46.7 358.9,48.5 358.9,49.2 358.1,50.2 355.7,50.1 
	355.7,49.2 354.8,49.2 355.7,48.5 357.2,47.7 358,46.9 358.9,46.7 359.7,45 359.7,43.4 360.4,42.6 360.4,40.9 361.9,39.5 
	362.6,37.7 362.6,36.9 364.3,33.5 364.2,31.2 365,30.4 365.8,30.4 367.4,28.7 367.3,26.2 365.7,24.4 365.7,23.7 364.8,23.7 
	364.6,16.4 363.9,16.4 363.9,15.5 362.9,14.6 362.9,9.7 361.9,8.9 360.3,8.9 360.3,9.7 359.6,9.7 359.6,10.5 358.8,11.3 357.1,11.3 
	356.4,12.2 354.6,12.1 353.1,13.7 349.8,13.7 348.2,14.6 346.6,16.1 346.6,15.4 345.7,15.4 345.7,14.5 343.9,14.5 343.9,13.6 
	343.1,13.7 342.3,14.6 334.9,14.5 333.4,16.1 332.6,16.1 331.7,16.9 329.2,16.9 327.7,16.1 324.3,16.1 321.9,15.3 319.4,15.3 
	317.8,16.1 315.3,16.1 306.3,17.7 306.3,18.5 307.1,19.4 305.5,20.2 304.7,20.2 301.6,23.4 296.6,23.3 295,24.2 288.5,24.1 
	286.7,22.6 286.7,21.6 285.1,21.6 284.3,20.8 280.2,20.8 279.4,20 277.7,20 277.7,19.2 276.8,19.2 274.2,16.7 274.1,13.3 
	271.7,10.9 270,10.9 268.4,9.3 266.6,8.6 262.6,7.6 260.8,6.8 260.8,5.9 259.3,5.9 258.5,6.8 258.5,7.6 257.6,8.5 255.9,8.4 
	255.9,9.3 257.7,10.9 258.7,10.9 259.4,10.1 260.9,9.3 261,10.9 263.5,10.9 263.6,15 262.8,15.9 262,17.5 262,18.3 261.2,19.2 
	261.2,20.8 260.5,21.5 259.7,21.5 258,23.1 258,24 257.2,24.9 256.3,24.9 255.7,25.7 253.2,25.8 252.3,24.9 251.5,24.9 249.9,26.5 
	248.2,27.3 245.9,27.3 245.1,28.2 244.2,28.2 243.4,29.1 242.6,29.1 242.6,29.8 241.8,29.8 241.8,30.6 241.1,30.6 239.5,32.3 
	239.5,33 237.8,34.8 236.9,34.7 235.4,35.5 230.3,35.5 227.2,37 225.6,37.1 223.2,39.7 222.5,41.2 222.5,42 221.7,42.9 221.7,44.5 
	220.1,46.2 220.1,47 218.4,47.8 218.5,50.3 219.4,50.3 221.1,52 221.9,52 222.8,52.8 222.8,53.6 216.2,53.6 215.3,54.3 215.4,56.8 
	216.3,57.7 216.3,60.2 215.5,60.2 215.5,61.8 217.2,63.5 217.2,65.3 221.3,65.3 223.1,66.1 223.1,66.8 222.3,67.6 220.6,67.6 
	219.8,68.4 219.8,70.1 219,71 219,73.5 220,74.1 219,75.1 220.9,75.8 221.7,75.8 222.5,75 222.5,74.1 223.3,73.5 224.1,73.5 
	224.1,74.2 224.9,75.1 224.9,75.9 225.8,76.6 225.8,77.5 229.1,77.5 229.9,78.3 229.9,79.1 230.7,80 233.2,80.1 233.2,79.2 
	234.8,77.5 235.6,75.9 237.1,75.1 237.9,73.6 238.7,72.7 240.5,72.7 241.3,71.8 242.1,71.8 242.1,71.1 243.6,69.3 244.4,69.3 
	244.4,68.6 246.1,68.6 246.1,67.7 246.9,67 246.9,66.1 248.5,66.1 248.4,65.3 249.2,65.3 250,64.5 255.8,64.5 255.8,65.3 
	256.7,66.1 256.7,67 261.6,67 261.5,64.5 262.3,63.7 262.3,62.9 263.2,63.7 264.9,63.7 264.9,64.5 267.3,64.6 267.3,63.7 268,62.9 
	267.9,60.4 268.8,59.6 269.6,59.6 270.5,58.8 271.2,58.8 272.9,57.1 274.5,57.1 274.5,57.8 275.3,57.8 276.2,58.8 277,58.8 
	278.8,59.6 280.4,61.4 281.2,61.4 282,60.5 282.7,60.4 284.4,59.6 285.3,58.8 285.1,55.5 285,49.8 285.8,49 285.8,48.1 286.6,47.3 
	290.7,47.3 292.3,46.5 292.3,45.7 293.9,44.9 293.9,43.9 294.6,43.9 297.1,41.5 297.9,41.5 301.1,39.9 301.9,39.1 304.3,39.1 
	305.2,39.9 306.1,41.6 306.1,42.5 307.1,43.3 307.1,43.9 307.9,45.7 307.9,49.9 308.9,50.8 308.9,52.4 309.7,53.2 309.8,56.4 
	309,57.2 309,62.2 310,61.4 309.9,60.6 310.7,60.6 311.5,61.4 310.8,62.1 310.9,64.8 310.1,65.7 308.3,65.6 308.3,66.4 309.2,67.2 
	309.2,68 310.1,68 310.2,69.7 311,70.4 311,71.3 311.8,71.3 311.8,72.2 312.6,73 312.6,73.8 311.8,73.8 311.1,75.4 311.2,76.2 
	311.9,77 311.9,77.8 313.6,77.9 314.4,78.7 314.4,79.5 315.2,79.5 315.3,82 316.2,82.9 320.3,82.9 321.2,83.7 321.2,82.9 
	322.6,82.9 324.4,83 325.3,83.7 326.1,83.7 326.1,84.5 327,86.2 327,87.1 327.8,88 327.8,88.7 329.5,88.7 329.5,88 330.2,88 
	332.7,90.3 332.8,93.7 333.7,93.7 333.8,94.5 334.5,95.3 334.5,96.2 337,96.2 337,95.3 337.9,95.4 337.8,92.8 338.7,92.8 338.7,92 
	341.9,92 342.6,91.2 344.3,91.2 345.3,92 346,91.9 347.7,91.2 349.2,91.2 349.2,90.4 352.5,90.5 352.5,91.2 353.3,90.4 355,90.4 
	355.8,91.2 359.1,91.2 360,92 360,92.8 360.9,93.8 361,95.4 361.8,95.4 361,96.3 361.1,97 361.8,97.9 361.8,98.7 363.4,98.7 
	364.2,97.9 372.5,98.1 372.5,97.1 373.3,97.1 373.2,96.3 374.8,94.6 375.8,94.7 375.6,92.1 376.4,92.1 375.5,91.3 375.5,90.5 
	376.4,89.7 376.3,88.1 375.5,87.3 376.2,86.4 377.1,86.4 377.9,85.6 377.9,84.1 377.1,84.1 376.4,83.3 376.2,83.2 376.2,82.3 
	377,80.6 376.9,77.3 377.8,77.3 377.8,76.5 378.6,75.7 378.6,76.5 379.3,76.5 379.3,77.3 380.1,78.2 381.8,78.2 381.8,77.3 
	383.4,77.3 383.4,76.5 384.3,76.5 384.2,75.7 385.1,75.8 385.4,76.2 385.9,76.6 386.7,76.6 386.7,77.4 387.6,77.3 388.4,76.5 
	390.8,76.6 390.8,75.8 391.6,75.8 391.6,74.2 392.3,73.3 392.3,71.7 393.1,71.7 393.1,69.2 " />
                                    <path class="st0 dist-block" id="3" d="M219,84.1l-0.8,0.8h-0.8v0.8l-0.8,0.9v0.9l-0.8,1.5v0.9l-1.5,1.6v0.8h-1.7l-0.8,1.7l0.9,0.7v0.9h-2.6l0.2,0.9
	l-0.9,0.8h-0.9v-0.9l-0.9-0.9v-1.6h-0.8l-1.6,0.8h-2.5l-1.6,1.6l-0.7,1.7l0.1,3.2l-3.2,7.4h0.9v2.5h0.9v2.4l0.8,0.9h0.9l0.7,0.8
	l0.1,1.6l1.6,1.6l0.2,3.3l0.9,1.7l-0.9-0.9h-4.9l-1.7-1.6h-0.9l-3.2-1.6l-3.1,5.6v0.8l-0.8,0.9l0.8,0.9l1.7,0.1v1.6l-0.8,0.9
	l-3.3-0.1l-0.9,0.9l0.1,4.9l-0.8,1.7l0.1,1.6l-0.8,0.8v0.9l-0.8,0.8v0.8l-0.9,0.8h-1.6l0.1,0.9h-0.8l-1.7,1.6h-0.8v0.8h-3.3
	l-0.9-0.9l-1.7,0.2l-0.7-0.8h-0.9l-0.8-0.9h-1.6l-0.9-0.8l-1.7-0.9h-0.8l-0.8,0.9l-0.9-0.9v-0.7h0.9v-1.6h-1.7l-0.1-1.7l-0.7,0.8
	h-0.9l-0.1-0.8h-1.6l-0.7,0.8l-1.6,0.9l-3.4-0.2l-0.9,0.8h-3.2l-0.9-0.8h-3.3l-0.8,0.8h-2.5l-0.9-0.8h-4.1l-1.7-0.9h-0.8l-0.2,0.1
	c1.2,1.3,1.8,2.6,1.8,3.9c0.2,2.7,0.2,4.1,0.3,4.3c0.5,1.8,1.3,3.4,2.4,4.7c0.9,1.1,1.3,2.3,1.5,3.7c0.1,1.3,1,3.8,2.7,7.5
	c1.5,3.3,2.3,6.2,2.3,8.8l0.7-0.8l0.8,0.1l2.4-2.5h0.9l1.6-1.6l0.8-0.1l0.8-0.8h1.7l0.8,0.8l1.7,0.1v0.9l3.1-0.1v0.9l1,0.9v1.7h0.9
	l0.1,0.8h4l0.8-0.8h8.2l0.8-0.8h0.9l0.6-0.9h1.7l-0.2-0.9h1.8l1.6-1.6l1.6-0.9v-0.8l0.9,0.2v-0.9h1.6v-0.8l0.8-0.1l0.1,0.8h0.9v0.9
	l0.8,0.2l0.2,3.2l0.9,0.8l0.8,1.8l0.9,0.9h1.6l2.3-2.7v-0.8h5.9l0.6-0.9h0.9l0.8-0.8h0.8v-0.8l-1.7-1.7h-0.8l-0.1-0.8l-0.7-1.6
	l-0.2-4.1l-0.8-0.8l-1.9-0.9l-0.8-0.1l-0.8,0.8h-1.6l-0.8-0.8v-0.9l-0.9-0.8l-0.1-0.9l3.2-3.2h5.7l0.9,0.9l0.1,0.8h0.9v0.8h3.3v-0.8
	h0.8l-0.1-0.8h1.6l0.2,0.8h0.8l0.8,0.8l1.6-1.6h1.6l0.8-0.9h0.9l1.6-1.6h0.9l4.1-4.1h0.8l0.9-0.9h0.8l0.8,0.9h2.3l2,0.6l0.6,0.2
	l0.8,0.9h0.8l1.6-1.6v-0.9l1.6-0.1l0.8-0.8l1.6-0.8v-0.7h1.7v0.7l0.8,0.8v2.5h1.7v0.9h1.6l0.8,0.8h0.8l0.8-0.7h0.9V146h0.8v-0.8
	l0.7-1.8v-0.8l1.5-1.6v-0.9l0.9-0.8l-0.2-1.7l0.8-0.8v-4.9l-0.9-0.8v-0.8l-0.9-1H250l-0.1-0.8H249l-0.8-0.7l-0.1-4.1l0.8-0.9
	l-0.1-3.2l-0.8-0.8h-0.2v-0.6l-0.8-0.1l-1.7-1.6v-1.8h1.6l0.9-0.8h0.8v-1.7h-0.9l-0.1-5.6l-0.9-0.1l-1.6-1.7h-0.8l-0.8-0.8l-0.2-0.9
	l-0.8-0.8l-0.1-1.6l-3.4-3.3h-0.8l-5.1-5.1v-0.8l-0.9-0.9V89l-1.6-1.6h-0.8l-1.7-0.8h-0.8l-3.2-1.8h-2.5l-0.8-0.8H219V84.1z" />
                                    <path class="st0 dist-block" id="29" d="M240.7,142H239v0.7l-1.6,0.8l-0.8,0.8l-1.6,0.1v0.9l-1.6,1.6h-0.8l-0.8-0.9l-0.6-0.2l-2-0.6h-2.3l-0.8-0.9h-0.8
	l-0.9,0.9h-0.8l-4.1,4.1h-0.9l-1.6,1.6h-0.9l-0.8,0.9h-1.6l-1.6,1.6l-0.8-0.8h-0.8l-0.2-0.8h-1.6l0.1,0.8H208l0.8,1.6l5.1,5h0.8
	l1.6,0.8l2.6,0.1l0.9,0.9l2.4-0.1l0.8-0.8h4.1v1.6h-0.8l0.1,0.8h-0.8l-0.9,0.9l0.2,4l-2.3,2.4l-0.9,0.1l-0.8,0.9l0.1,1.7l2.3,2.4
	l-0.8,0.8v1.6l1.7,0.1v-0.9h0.8l1.7,1.6l0.1,2.5l0.8,0.2l-0.1-1l0.9,0.1l0.9,0.9h-0.9v0.8l0.9,0.8l0.1,1.6l0.8,0.8l-0.7,1.7v0.8
	l-0.8,0.8l-0.9,1.7l-1.5,1.6v0.8l-0.8,0.9v0.8h-0.9l-0.8,0.8l0.1,1.7h-0.8l0.2,9l0.8-0.9l3.3-1.6l1.6,0.1l0.8-0.9l3.3-1.6l3.1-3.3
	l-0.2-4.9l0.9-0.9v-0.9l0.7-1.6l0.9-0.9l-0.1-0.8l1.6-1.6l-0.2-4.9l0.8-1.7l1.6-1.6h4.9l1.6-0.9h3.3l0.9-0.8l1.6-0.8l0.8-0.9
	l5.7,0.1l1.7,0.9l2.5-0.1l0.8,0.9h1.6l4.9-2.5h1.6l0.9-0.7l2.3-1.6l0.9-1h0.9l1.6-0.7l0.9-0.9h6.4l0.8-0.8v-0.9H288l-0.8-1.6
	l-0.9-0.8v-0.8h-0.9l-0.8-0.9l-0.2-0.8l-1.6-1.7H282l-0.9-0.8l-2.3-0.2v-0.8l-1-1.6l-0.9-0.8v-0.8l-0.9-0.8v-0.8l-1.6-0.1l-0.9-0.9
	h-1.7l-0.8-0.8l-7.4-0.2l-1.6-1.6v-0.8l-0.9-0.8v-1.7l-0.9-0.9v-0.8l-1.7-1.7l-7.6-3.3v0.9l-1.5,1.6v0.8l-0.7,1.8v0.8h-0.8v0.9H247
	l-0.8,0.7h-0.8l-0.8-0.8H243v-0.9h-1.7v-2.5l-0.8-0.8V142H240.7z" />
                                    <path class="st0 dist-block" id="5" d="M145.2,172.1l-1.6,1.7l-0.8,1.6l-2.4,2.3v0.9l-0.8,1.6v0.9l-0.7,0.9v0.8h-2.5l-0.8,0.9v3.2l0.9,0.9l0.8,0.1
	l0.8,0.8v0.8l0.9-0.1l0.1,2.5h0.8v2.5h-0.8l0.1,2.5l-2.4,2.4l0.1,4.9l-0.7,0.9v1.6l-0.8,0.9l0.1,4.9h0.9l0.1,1.6l0.8,1v0.8l0.9,0.8
	v1.7l0.8,0.8h-0.8v0.8l0.9,0.8l0.8-0.8h1.6l0.9,0.8h0.8h2.4v0.9l1.7,2.5v-0.9l2.5,2.6l0.7,1.6l0.1,0.9l0.9,0.1v4.1h1.8v1.6l-0.9,0.9
	h-0.9l-1.6,1.6v0.8l-0.8,0.9v0.8h-0.8v0.9h7.4v0.8l1.6,0.1l0.8-0.8v-1.7h6.6l0.9,0.9h1.6l1.6-0.9l1.6-1.6h3.2l2.6-0.8v0.8h3.1
	l0.9,0.9h0.8l0.9,0.8h2.4v-0.9l0.8,0.1l0.9-0.8h0.8l0.8-0.9h0.9l0.8-1.6v-0.8l0.9-0.9v-0.9l1.5-1.6h0.8v-0.8l0.9-0.8h0.8l1.6-1.7
	l1.6-0.8l0.7-0.9l0.9,0.9h0.9l1.5-1.7v-1.6h0.8v-0.8l0.9-0.1l1.5-1.6h0.9v-1.6l0.8-0.8v-0.9l1.6-1.7h2.4v-0.8l1.6-1.6v-0.9l1.6,0.1
	l0.1,3.3l0.9,0.9l2.4-0.2l0.8,0.9l4.3,0.1l0.8,0.8h0.9v0.9l0.8,0.8v2.3h1.7l0.9-0.8h2.4l-0.1-0.8h0.8v-1.6h0.8l-0.1-1.7l0.9-0.8
	l-0.2-1.7H230l-0.9-0.8h-0.9l-0.8-0.8v-0.9l-2.6-2.5h-1.6v-0.9h-0.9v-2.4l0.8-0.9l-0.2-9h0.8l-0.1-1.7l0.8-0.8h0.9v-0.8l0.8-0.9
	v-0.8l1.5-1.6l0.9-1.7l0.8-0.8v-0.8l0.7-1.7l-0.8-0.8l-0.1-1.6l-0.9-0.8v-0.8h0.9l-0.9-0.9l-0.9-0.1l0.1,1l-0.8-0.2l-0.1-2.5
	l-1.7-1.6H224v0.9l-1.7-0.1v-1.6l0.8-0.8l-2.3-2.4l-0.1-1.7l0.8-0.9l0.9-0.1l2.3-2.4l-0.2-4l0.9-0.9h0.8l-0.1-0.8h0.8v-1.6h-4.1
	l-0.8,0.8l-2.4,0.1l-0.9-0.9l-2.6-0.1l-1.6-0.8h-0.8l-5.1-5l-0.8-1.6v0.8h-3.3v-0.8h-0.9l-0.1-0.8l-0.9-0.9h-5.7l-3.2,3.2l0.1,0.9
	l0.9,0.8v0.9l0.8,0.8h1.6l0.8-0.8l0.8,0.1l1.9,0.9l0.8,0.8l0.2,4.1l0.7,1.6l0.1,0.8h0.8l1.7,1.7v0.8h-0.7l-0.8,0.8h-0.9l-0.6,0.9
	H196v0.8l-2.3,2.7h-1.6l-0.9-0.9l-0.8-1.8l-0.9-0.8l-0.2-3.2l-0.8-0.2v-0.9h-0.9l-0.1-0.8l-0.8,0.1v0.8h-1.5v0.9l-0.9-0.2v0.8
	l-1.6,0.9l-1.6,1.6h-1.8l0.2,0.9h-1.7l-0.6,0.9h-0.9l-0.8,0.8h-8.2l-0.8,0.8h-4l-0.1-0.8h-0.9v-1.7l-1-0.9v-0.9l-3.1,0.1v-0.9
	l-1.7-0.1l-0.8-0.8h-1.7l-0.8,0.8l-0.8,0.1l-1.6,1.6h-0.9l-2.4,2.5l-0.8-0.1L145.2,172.1" />
                                    <path class="st0 dist-block" id="15" d="M156,240.5v-0.8h-7.4v0.8l-0.7,0.8v0.9l-0.8,0.9l0.1,0.7l-1.7,1.7h-0.8l-1.6,0.8h-1.8l-1.6,1.6h-0.8v0.9
	l0.8,0.9v0.8l-0.8,1.7l-0.8,0.7v2.4l0.2,6.7l-0.7,1.6v0.9l0.8,0.8l0.1,0.8l0.9,0.9v0.8l-0.9,0.8l0.1,0.9l-0.8,0.8v0.9l-1.6,0.7h-4.1
	l-0.9,0.9l-0.6,1.6l-4.9,2.4l0.8,0.9l0.1,2.4l-0.9,0.9h-0.8l-0.7,0.8l0.2,6.5l-1.6,1.7l-0.9-0.1v0.9h-0.8l0.1,3.3l-0.9,0.1v1.7
	l0.9,0.7l3.4,0.1v0.9l-0.8,0.8v0.8h0.8l0.8,0.9h1.6v0.9h0.9l2.5,2.4h0.9v0.9h-0.9l0.1,1.6h0.9v0.9h0.7l0.1,1.6l0.9,0.8l0.1,0.9h0.9
	l2.4,2.4h0.9l1.6,1.8l-0.8,0.8v0.8l0.8,0.8l0.2,2.6l0.9,0.8h1.6l-0.1-0.8l0.9,0.8v0.8h0.8v0.9h0.9l1.6-1.6l0.8,0.8l0.8,0.1l0.8,0.9
	l1.6-0.1l0.9,0.9h0.8l0.2,3.4l-0.9,0.7l0.1,0.9h-0.8l-0.8,0.9l0.1,3.2l1.5-0.1l0.9,0.9h0.9l1.7,1.6v0.8l0.9,0.9h0.8h0.8v-1.6h1.6
	l0.8-0.9v-0.8l0.7-0.9h-0.7v-0.9h-0.8l-1-0.9v-0.7h0.9l-0.2-2.5h0.9l0.8-0.8v-0.9l1.6-1.6v-2.4h0.8v-0.9l0.7-0.8l-0.1-2.5l-0.7-1.6
	l-1-0.9v-1.2l-0.8-0.2v-0.8h-0.9l-0.1-0.8l-0.8-0.8l0.8-0.8l-0.1-0.9h0.9l0.9-0.7h1.6l0.8-0.9l2.6,0.1l2.3-2.4l-0.1-0.8l0.8-0.9
	v-0.8l0.8-0.9v-1.7l0.8-0.8v-0.8l0.8,0.8h0.8l0.9,0.9v0.8l0.9,0.9l0.1,0.8l1.7,1.7v0.8l0.8,0.9l2.5-0.1l0.8,0.9l0.1,2.4h0.9l1.7,1.6
	l1.6-0.1v0.9l0.9,0.2l-0.1-2.6h2.4v-0.8l0.8-0.8v-0.8l2.3-2.4l0.8-0.1l0.9-0.8v-0.9l0.8-0.8l-0.2-4.1l0.8-0.8V296h5.8l0.9-0.8h0.8
	l0.8-0.8l0.8,0.8h0.9v0.8l0.8,0.1l-0.2-5l0.9-1.7l-0.2-0.8l1.6-0.8l0.9-0.8h0.9l1.6-1.6v-0.8h0.8v-0.9l0.8-1.8l0.7-0.8v-1.6
	l-0.7-0.9v-0.8l0.7-0.9l-0.1-1.7h-0.8l-1.8-1.4v-1.7l-0.9-1.6l-0.1-3.3l-0.8-0.9l-0.1-3.1l0.8-0.8h1.6v-0.8l0.8-0.9v-2.6h2.3
	l0.9,0.9h1.6l0.8-0.9l2.3-3.2h0.9v0.8h0.9l-0.1-2.5l-0.9-0.8h0.9l-0.1-1.6l0.9-0.8v-0.8l0.6-0.9v-0.9l0.9-0.8l0.8,0.1l-0.2-0.8
	l1.7-0.1v0.8l2.5,0.1l1.6,0.8l0.9,0.9h0.8l0.8-1l4.1,0.1v0.9h0.8v-0.9l0.9,0.2v-0.8l0.8-1.8l-0.2-1.6h0.9v-1.7h2.3l0.8-0.7h0.8v-0.9
	l0.9-0.8l-0.2-2.6l-4.9-0.1v-0.8h-0.9l-0.2-7.4l-0.9-0.9l-0.1-3.3l-1.7-1.6v-0.9l-1.6-1.6l-1.6-0.8l-0.9-0.8H230v1.6h-0.8l0.1,0.8
	h-2.4l-0.9,0.8h-1.7v-2.3l-0.8-0.8v-0.9h-0.9l-0.8-0.8l-4.3-0.1l-0.8-0.9l-2.4,0.2l-0.9-0.9l-0.1-3.3l-1.6-0.1v0.9l-1.6,1.6v0.8
	h-2.4l-1.6,1.7v0.9l-0.8,0.8v1.6h-0.9l-1.5,1.6l-0.9,0.1v0.8h-0.8v1.6l-1.5,1.7h-0.9l-0.9-0.9l-0.7,0.9l-1.6,0.8l-1.6,1.7h-0.8
	l-0.9,0.8v0.8h-0.8l-1.5,1.6v0.9l-0.9,0.9v0.8l-0.8,1.6h-0.9l-0.8,0.9h-0.3l-0.9,0.8l-0.8-0.1v0.9h-2.4l-0.9-0.8h-0.8l-0.9-0.9h-3.1
	v-0.8l-2.6,0.8h-3.2l-1.6,1.6l-1.6,0.9h-1.6l-0.9-0.9h-6.6v1.7l-0.8,0.8L156,240.5z" />
                                    <polygon class="st0 dist-block" id="6" points="310.8,220.7 312.9,220.2 312.9,219.9 312.8,217.4 313.7,217.4 315.2,215.7 316.4,215.7 316.4,215.7 
	317.8,215.7 319.4,214.1 319.4,215 319.7,215 320.6,214.1 320.6,214.9 321,214.9 322.6,214.1 323.5,214.1 323.6,214.2 323.8,214.1 
	324.7,214.1 325.6,215 328.5,215 329.3,214.1 330.2,214.1 331,213.3 331.9,213.3 331,211.7 330.1,210.8 330.9,210 331.7,207.5 
	332.5,205.9 332.5,205.1 333.2,204.2 333.2,203.9 333.8,203.9 334.7,203 335.5,203 336,203.5 336.2,203.3 337,203.3 337.3,203.6 
	337.3,203.1 338.2,203.1 339.7,201.4 339.7,200.6 341.3,199 342.1,199 342.5,199.7 342.8,199.3 341,197.7 339.4,196.9 338.5,196.9 
	336.8,196 334.3,196 332.7,195.1 327.8,195.1 326.9,194.2 325.3,194.2 325.3,193.4 324.4,193.4 324.4,192.6 322.8,191.7 
	322.8,190.8 321.2,190 319.4,188.3 317.3,188.3 317.2,187.4 315.5,187.4 314.6,186.6 313,186.6 313,185.8 312.1,185.8 310.5,184.2 
	309.6,184.2 308,183.3 303.9,183.3 303,182.5 301.4,181.6 300.6,181.6 298.8,180 298,179.8 298,178.2 297.1,177.4 297,174.2 
	294.4,171.8 293.6,171.7 291.9,169.9 291.1,170 290.2,169.2 289.4,169.2 288.5,168.3 287.7,168.3 281.3,168.3 280.4,169.2 
	279.1,170 278.2,170 277.3,171 275,172.6 274.1,173.3 272.5,173.3 267.6,175.8 266,175.8 265.2,174.9 262.7,175 261,174.1 
	255.3,174 254.5,174.9 252.9,175.7 252,176.5 248.7,176.5 247.1,177.4 242.2,177.4 240.6,179 239.8,180.7 240,185.6 238.4,187.2 
	238.5,188 237.6,188.9 236.9,190.5 236.9,191.4 236,192.3 236.2,197.2 233.1,200.5 229.8,202.1 229,203 227.4,202.9 224.1,204.5 
	223.3,205.4 222.5,206.3 222.5,208.7 223.4,208.7 223.4,209.6 225,209.6 227.6,212.1 227.6,213 228.4,213.8 229.3,213.8 
	230.2,214.6 231.8,214.6 233.6,214.6 234.4,215.5 238.5,215.5 239.4,214.6 240.2,214.6 241,215.5 241.8,215.5 242.8,216.5 
	243.5,215.6 243.5,214 246.8,213.9 246.8,213.1 247.6,213.2 246.8,212.3 245.9,212.3 245.1,211.5 245.1,209.9 244.3,209.1 
	244.2,208.4 245,207.4 245,206.5 245.8,205.7 246.6,204 247.4,203.3 248.2,203.3 249.8,201.6 249.8,200.8 253.9,200.8 255.5,200 
	255.5,198.3 256.3,198.3 257.2,199.2 258,199.2 258,200.1 261.3,200.1 262.2,200.8 263.9,201.7 265.4,201.6 266.3,200.8 
	267.2,200.8 268,200.1 268.8,200.1 269.7,199.2 270.6,199.2 272.1,200.1 274.7,200 276.3,200.8 278.8,200.8 280.4,199.2 
	281.3,199.3 283,200.9 284.6,200.9 285.3,200.2 286.2,200.2 287.1,199.3 288,199.3 288.8,198.4 289.6,198.4 289.6,196.8 
	290.4,196.9 290.4,196 291.2,195.2 290.3,194.3 290.3,193.4 291.2,193.4 291,192.6 291.8,191.7 292.7,191.7 293.5,190.8 
	294.4,190.8 295.3,191.7 296.1,191.7 296.1,192.6 296.9,192.6 301.3,196.7 301.3,198.3 302.2,199.2 302.3,201.6 301.4,202.5 
	301.5,203.3 303.9,205.9 304.8,205.9 306.4,207.5 306.5,210 309.7,213.3 309.9,218.2 310.8,219.1 " />
                                    <path class="st0 dist-block" id="25" d="M102.3,166h-0.9v-0.8l-0.8,0.8l0.2,3.4l-0.9,0.9l0.9,1.7l0.3,13.8l1.7,1.7l0.1,6.7l-0.8,0.9l0.1,2.3h-0.8
	l-0.9,0.9h-0.8l-0.8,1.6v0.8l0.8-0.8h0.9l-0.1-0.8h0.9l0.1,0.8l1.7,0.8v1.7l0.9,0.8l0.8-0.1l0.2,5.9h0.8v0.8h0.8V209h0.9l-0.2-1.6
	h0.9v1.6l0.8,0.9l0.2,0.9l0.8,1.7l-0.7,0.7v0.9h-0.9v0.8l-0.9,0.8v1.7l-0.8,0.9l0.1,4l2.7,2.7l0.1,2.4l-1.7-0.1v0.9l-0.8,0.9
	l0.9,1.6l0.1,4.9l-1.6,1.6l0.1,1.7l0.8,0.9l0.2,4.1l-0.7,5.8l0.8,0.9h1.7l0.9,0.8h4v0.9h0.9l0.9,0.8h0.8l0.9,0.8l6.6,0.1l2.6,2.4
	h1.6l0.8-0.8h1.6l0.8-0.9h2.4l0.9,0.9h3.1v-2.4l0.8-0.7l0.8-1.7V250l-0.8-0.9v-0.9h0.8l1.6-1.6h1.8l1.6-0.8h0.8l1.7-1.7l-0.1-0.7
	l0.8-0.9v-0.9l0.7-0.8V240v-0.9h0.8v-0.8l0.8-0.9v-0.8l1.6-1.6h0.9l0.9-0.9v-1.6H152v-4.1l-0.9-0.1l-0.1-0.9l-3.6-4.2l-1.7-2.6h-2.4
	h-0.8l-0.9-0.8H140l-0.8,0.8l-0.9-0.8V219h0.8l-0.8-0.8v-1.7l-0.9-0.8v-0.8l-0.8-1l-0.1-1.6h-0.9l-0.1-4.9l0.8-0.9v-1.6l0.7-0.9
	l-0.1-4.9l2.4-2.4l-0.1-2.5h0.8v-2.5h-0.8l-0.1-2.5l-0.9,0.1v-0.8l-0.8-0.8l-0.8-0.1l-0.9-0.9v-3.2l0.8-0.9h2.5v-0.8l0.7-0.9v-0.7
	l0.8-1.6v-0.9l2.4-2.3l0.8-1.6l1.6-1.7c-0.1-2.6-0.9-5.5-2.3-8.8c-1.6-3.8-2.6-6.3-2.7-7.5c-0.2-1.4-0.6-2.6-1.5-3.7
	c-1.2-1.3-2-2.9-2.4-4.7c-0.1-0.2-0.2-1.6-0.3-4.3c0-1.3-0.6-2.6-1.8-3.9l-5.5,1.6l0.1,0.8l-0.9,0.8l1.7,0.8v0.9h0.9v1.6h-0.9v0.8
	l-0.8,0.9l-2.3-0.1l0.1,3.4h0.8l0.8,0.9l-0.6,1.6l-3.4,3v0.9l-0.8,0.9v0.8l-0.8,0.8v0.9l-1.6,0.9h-1.8l-1.5,1.6l-0.9,0.1l-0.9,0.8
	l0.1,0.8l-0.8,0.9l0.1,2.5l-0.8,0.8h-0.9l-0.8,0.8v0.9l-0.8,0.8l0.9,0.9l-0.9,0.8H109v-0.8l-0.9-0.9v-0.8l-0.9-0.9v-0.9h-0.9v-0.9
	h-1.6l-0.8,0.9l-1.7,0.1L102.3,166z" />
                                    <path class="st0 dist-block" id="23" d="M63.2,244.4h-2.3v0.8H60l-1.6,1.6l-0.8,1.7l-0.9,0.8h-1.4l-0.9,0.9l0.1,2.5l-0.8,0.8v1.6L53,256v1.6l-0.8,0.9
	v0.9h0.9v3.3h0.8l0.1,0.7l0.9,0.9h0.9l1.6,1.6l7.5,1.6l0.1,4.1l0.9,0.1v0.9l0.8,0.8l0.1,0.8h4.8l2.5,0.8l0.1,2.7h-0.8l0.1,1.6
	l-0.9,0.9l0.1,2.5l-0.8,1.6v0.8l0.9,0.8l0.1,5l-0.8,0.8l0.2,2.5l0.8,0.9l0.1,3.3l-0.8,0.8v0.8l-0.8,0.9v0.9l-0.8,0.8h5.5l0.8-0.8
	l0.9,0.8v1.6l1.8,5.7l0.9,0.9l4.8,0.2l1.8,1.5l0.1,2.6h-0.9v0.8h-0.9l-0.8,0.9l-1.7-0.1l0.1,1.8l0.9,0.8l0.1,0.8h1.6v0.8h0.9
	l0.8,0.8h-0.8v0.9l-0.8,0.8v1.7l-0.8,0.9v0.7l-0.8,0.8l0.2,5.8l0.9,0.9l0.9,2.5l0.9-0.2v1.7l-2.4,2.5l0.9,0.8l1.6-0.1l0.9,0.9h0.9
	l0.1,0.8l0.7,0.1l0.8-0.8v-0.9l1.6-1.6h2.4l0.8-0.8h1.6l0.9-0.8h0.9l-0.1-0.9l0.8,0.1l0.9-0.9h0.9v0.8h0.8l1.6-0.8h1.7v0.9h0.7
	l0.9-0.9h1.7v0.8l0.8,0.1l0.8-0.9h0.8v0.9h1.7v-0.9l0.8,0.1l0.8-0.8l1.6-0.1l0.8,0.8h0.8l1.7-1.6h4.1l0.8-0.8h3.2v0.8l4.3,4.1
	l0.9,0.1v-0.9h0.7l-0.1-0.9l0.9-0.9v-0.8h3.3l0.9,0.8l1.6,0.9l0.8,0.9h0.9v0.9h5l0.8-0.8h0.9l1.6-1.7l0.8-0.1l0.8-0.8l3.4,0.1
	l0.9,0.8h0.8v-0.8h-0.8L155,335v-0.8l-1.7-1.6h-0.9l-0.9-0.9l-1.5,0.1l-0.1-3.2l0.8-0.9h0.8l-0.1-0.9l0.9-0.7l-0.2-3.4h-0.8
	l-0.9-0.9l-1.6,0.1L148,321l-0.8-0.1l-0.8-0.8l0,0l-1.6,1.6h-1.5v-0.9h-0.8V320l-0.9-0.8l0.1,0.8h-1.6l-0.9-0.8l-0.2-2.6l-0.8-0.8
	V315l0.8-0.8l-1.6-1.8h-0.9l-2.4-2.4h-0.9l-0.1-0.9l-0.9-0.8l-0.1-1.6h-0.7v-0.9h-0.9l-0.1-1.6h0.9v-0.9h-0.9l-2.5-2.4H127V300h-1.6
	l-0.8-0.9h-0.8v-0.9l0.8-0.8v-0.9l-3.4-0.1l-0.9-0.7V294l0.9-0.1l-0.1-3.3h0.8v-0.9l0.9,0.1l1.6-1.7l-0.2-6.5l0.7-0.8h0.8l0.9-0.9
	l-0.1-2.4l-0.8-0.9h-0.1l-0.1-4.9l-0.9-0.8V270l0.8-0.9v-1.6h-2.3l-1-0.9h-2.4l-0.9-0.8H118l-1.6-1.7h-1.7l-0.8-0.9h-3.1l-1.8,0.9
	l-1.6-0.1l-0.9,0.8l0.2,0.9l-1.6,1.7v0.8l-0.8,0.9l-1.6-0.1l-0.9,0.9h-0.9l-1.6-1.7v-0.8l-1.6-1.6l-0.1-0.9l-1.7-3.1v-0.9l-0.9-0.8
	l-0.1-2.6l-0.9-0.9h-0.8v-0.9l-1.7-1.6h-2.4v0.8h-0.8v0.9l-0.8,0.9h-0.8l-0.8,0.8v-0.8H85l-0.1-3.3h-1.6l-0.9-0.9H80l-1.6-1.6
	l-0.1-0.8l-0.9-0.9v-0.9l-0.9-0.9h-0.8l-0.9-0.8l-0.8,0.8l0.1,2.6h-0.8l0.1,0.8h-0.8v2.4h-2.4L70,250l-0.8-0.9v-0.9h-2.4v-0.8
	L63.2,244.4z" />
                                    <path class="st0 dist-block" id="20" d="M89.7,342.5l-0.7-0.1l0.7,0.9l0.1,2.5l-0.7,0.8l2.4,5.1l0.2,7.4h0.9l0.1,3.2H91l-1.5-0.8h-1.7l-1.6,0.8
	l-0.8,0.8v0.9l-0.9,0.8l0.1,0.9l0.9,1.8l0.1,1.6l-0.9,0.7l0.1,0.8l-0.9,0.9l0.1,2.4l-1.6,1.7h-0.9l-0.8,0.9l-3.3-0.1l0.1,1.6
	l0.8,0.9l1.6,0.8l0.8,0.1l0.1,0.8l0.9,0.8l0.1,2.5l0.9,0.8h1.6l0.9,0.9v0.9h0.9v0.8h0.8l0.8-0.8l0.9-0.1l0.8-0.9l1.6,0.1l0.9,0.9
	l0.1,1.6l0.8,1.8l2.5,2.4l0.8-0.1l0.8,0.9h1.4l0.1,0.8l1.6,0.1l0.7-0.8l1.7-0.1v-0.9l1.6,0.1v-0.8h0.8l0.9-0.9l-0.1-0.8h2.4l0.2,0.8
	l0.8,0.1l0.8,0.8h0.9l0.8-0.9h0.9v0.9h0.9v2.4h-0.8l-0.9,0.9H110l-0.8,0.9l-0.9-0.1l0.1,0.9l-0.9,0.8v0.9l1,0.8v0.9l2.5,2.4h1.6
	l0.9,0.9l0.8-0.8l0.3-0.7h0.8l1.3,0.2c0,0.1,0.1,0.2,0.2,0.2c-0.2,0.2-0.2,0.5-0.2,0.6c0,1.1,0.5,1.8,1.6,2.2l2,0.2l0.8,4.1
	c0.4,1.6,1,2.7,2,3.1l-0.1,0.1v0.7l-0.7,0.9c-0.5,0.5-0.7,0.9-0.8,1.4v2.4c1.6-0.2,3.1,0.1,4.5,0.9c1.3,0.6,2.1,1.6,2.3,2.7
	l-0.2,0.1l-0.8-0.1v1.7l0.9,0.9l-0.3,0.4h-0.4c-0.5,0-0.9,0.2-1.2,0.8c-0.2,0.4-0.2,0.8-0.2,1.1l0.4,1.4l-0.3,1.4
	c0,0.4,0.4,1.1,1.2,2l1.2,1.5c0.3,0.2,0.8,0.5,1.7,0.9c0.8,0.4,1.4,0.5,1.8,0.5c1.3,0,2-0.5,2.3-1.7c0.2-2,0.5-3.1,0.8-3.5
	c0.2-0.3,1-0.7,2.3-1.2c0.5-0.2,0.9-0.7,1.1-1.4c0.2-0.6,0.6-0.9,1.3-0.9l4-0.4c0.9-0.2,1.7-0.6,2.2-1.3c0.5-0.5,0.9-1.3,1.1-2.2
	l0.4-1.9l1-1.2v-2.4h-1.3v-1.7l0.3-0.2l0.5-0.5h0.8l1.7-0.2c0.3,0,1,0.5,1.9,1.6c0.9,1,1.9,1.6,2.9,1.6c1.2,0,2.1,0.5,2.5,1.6
	c0.2,0.4,0.4,1.5,0.5,3.1l6.4,0.3c0.9,0,1.8-0.4,3.1-1.1c1.2-0.7,2.4-1.1,3.8-1.1c0.7,0,1.1,0.2,1.3,0.3h1.1l0.5-3.6l-0.2-0.9
	l0.9-0.7l-0.1-1.6l0.8-0.9l-0.1-1.6l-1.6-0.1v-0.9h-0.7c0.2-0.5,0.5-1.1,1.3-1.6l0.2-0.1l0,0c1.2-0.9,1.9-1.7,2-2.7
	c0.1-1.6,0.3-2.7,0.7-3.2l0.1-0.5l0.2-0.2v-1.6l0.8-0.9v-0.9l0.8-0.9v-0.9l-0.9-0.7l-0.1-1.6c0.5-0.6,1.6-1.3,2.8-1.7
	c2.4-1,3.7-2.2,3.7-3.5c0-0.8-0.2-1.1-0.7-1.1c-0.3,0-0.9,0.3-1.7,1.1c-0.8,0.6-1.3,0.9-1.7,0.9c-0.7,0-1.3-0.4-2-1.1l-0.3-0.5h3.2
	l0.9-0.7h0.8l-0.2-4.1h-0.8l0.1,0.9l-1.6,1.6v-1.1h-0.9v-0.8H177v-0.9l3.1-3.3h0.9l0.8-0.9l-0.1-1.6h1.6l0.9-0.8h0.8v1.6l3.3-0.1
	v0.8l-0.8,0.9h1.6v0.9h0.9l0.2,0.8h1.6v-1.6H191l-0.2-1.7h0.8l0.9-0.8l1.7,0.7l1.6,0.1v-0.8l0.9-0.9l-0.1-0.8h0.9v-0.9h2.4l0.9-0.8
	h0.8l-0.1-0.8h0.8v-0.9h0.9v-0.8l2.4,0.1v0.9h0.9l0.9-0.9h0.7l0.8-0.8v-0.8h0.8l0.9-0.9l-0.9-0.8v-0.8h-0.9l-0.9-0.9l-1.6,0.1v-0.9
	l0.8-1h0.8v-0.8h-2.4l0.7-0.7v-0.8l-0.9-0.9v-1.6h-1v-1.6h-0.8l-0.9,0.8v1.6l-0.8,0.8v1.6h-1.6v-4.1l0.8-0.8l-0.8-0.9h-0.9l-1.7-1.6
	h1.7l0.9-0.8h1.5l0.9-0.9v-0.7h0.9l-0.2-0.5h-0.9l-0.1-2.6l-0.8-0.8l-0.2-4.1h-0.8l-1.7-1.8h-3.3v0.9h-0.9v0.9l-0.7,0.7l0.8,0.9v0.8
	l-0.7,0.8v0.9h-0.7l-1.7,1.8v1.6l-0.8,0.9l0.2,2.3h-1.7l-0.8-0.9l-0.1-3.2l-0.9-1.7v-0.9l0.8-0.9V347h-0.8v-1.6l-0.9-0.8l0.8-0.9
	v-0.8h0.8l1.6-0.8h1.6v-0.8l0.9-0.1l-0.1-1.6l0.9-0.8V338h-0.9l-0.9-0.8h-0.8v-0.9h-0.8v0.9l-1.6-0.1l-0.7,0.9v0.8l-2.4,2.4h-0.9
	v0.9h-0.8v-0.9l-0.9-0.8H180v0.8l-0.9-0.1v0.9l-0.8,0.8l-0.1-0.8l-0.8,0.1l-0.8,0.8v0.8l-0.8,0.9v0.8H175l-0.8,0.7v0.9l-0.8,0.9
	h-0.8v0.8h-1.7l-0.1-4.1l-0.8-0.8h-0.8l-0.9-1.6v-1h-0.9v-0.8l-1.6-1.6v-0.9l-0.9-0.1l-0.9-0.9l-0.9,0.1l-0.7,0.9l-4.2-0.1l-0.9-0.9
	h-0.8l-0.8-0.9h-0.8l-0.9-0.8l-3.4-0.1l-0.8,0.8L149,336l-1.6,1.7h-0.9l-0.8,0.8h-5v-0.9h-0.9l-0.8-0.9l-1.6-0.9l-0.9-0.8h-3.3v0.8
	l-0.9,0.9l0.1,0.9h-0.1v0.9l-0.9-0.1l-4.3-4.1v-0.8h-3.2l-0.8,0.8H119l-1.7,1.6h-0.8l-0.8-0.8l-1.6,0.1l-0.8,0.8l-0.8-0.1v0.9h-1.7
	v-0.9H110l-0.8,0.9l-0.8-0.1v-0.8h-1.7l-0.9,0.9h-0.7v-0.9h-1.7l-1.6,0.8H101v-0.8h-0.9l-0.9,0.9l-0.8-0.1l0.1,0.9h-0.9l-0.9,0.8
	h-1.4l-0.8,0.8h-2.4l-1.6,1.6v0.9L89.7,342.5z" />
                                    <path class="st0 dist-block" id="21" d="M59.1,397.5l0.8,0.9l0.1,0.8h-1.6l-0.8,0.9h-0.8l-0.9,0.6h-0.8v1.8l-0.8,0.8v0.9h-0.7l-3.3,3.3l-1.6,0.8
	l-1.6,1.7l-0.7,1.6v0.9l-1.6,4.1v0.9h-7.4l-1.6,0.6l-1.6,1.7l-1.6,0.1c-0.6,0.3-1.2,0.5-1.8,0.8H30l-2.3,2.4
	c-0.5,0.3-1.1,0.6-1.6,0.9l-0.9,0.8h-1.6l-0.8,0.8v0.9l-0.8,1.6v0.8l-0.8,0.8v0.9h-0.8l0.1,1.6l0.8,1.7l0.2,5.7l-0.8,0.9l0.2,1.6
	l-1.8,1.8l-0.8,1.6v0.8l-0.5,0.8l0.1,5.8l-1.6,1.6v0.8l-0.8,0.8h-0.8l0.1,5.9H9.9l0.2,5l-1.6,1.6l0.1,1.6l-0.8,1.7h0.6v-0.8l0.9-0.8
	h3.2l0.8-0.9v-0.8H15l0.8-0.8h0.9l2.4,2.6l3.3-0.1l-0.1-0.9l0.9,0.2l0.8-0.8h0.8l1.7-0.9H29l0.8,0.9v0.8h2.4l0.9-0.7l-0.1-1.7h0.9
	v-1.3h0.9l0.8-0.8l-0.1-0.9l1.6-1.7l3.2,0.1v0.9l0.9,0.8l1.7-0.8l1.6-1.6h0.8v-0.8h2.5l0.8-0.9l-0.1-0.9l0.8-0.8v-0.8l1.6-1.7
	l3.3,0.1l1.5-0.9l2.6,0.1v-0.9h0.8l0.8-0.9h0.9l0.6-0.6l0.8-0.1l0.8,0.8l0.9-0.8l2.5,0.1l1.6-1.6v-0.8h0.8l0.9-0.9h0.7v-1.7l0.8-0.8
	h5.7l-0.1-0.8h0.9l0.8,0.8l4.1,0.1l1.7,1.6l0.9,0.1v-0.8h1.6v0.8h0.9v0.9h0.7v0.9h0.8l0.8,0.8h1.8v0.8l0.8,0.9h3.2l1.6-1.6v-0.8
	l-0.9,0.1l-0.1-4.1l0.9-0.1v-0.9h0.8l0.8,0.9v2.4l0.9,0.1v-0.8h0.9v-0.9h0.9v0.9l0.8,0.8v0.9l1.7-1.6L104,446h0.7v-1.1l-0.8-0.9
	l-0.2-5.1l0.8,0.1l0.9,0.9h1.6v-3.3h-0.9l-0.8-0.9h-4.1l-0.1-2.5l0.8-0.8h0.8v-0.9l0.9-0.1l-0.1-3.2l0.8-0.7v-0.9l0.7-0.9h0.9
	l0.8-0.9h0.9l0.8-0.8l-0.2-2.5H105l-0.1-0.8l0.8-0.9l1.6-3.3h0.8l-0.2-3.3l-0.8-0.9h-0.9v-0.8l0.8-0.8v-0.8l2.5-2.5l0.8-0.1l0.8-0.8
	v-0.9l2.3-2.4l-0.9-0.9h-1.6l-2.5-2.4V399l-1-0.8v-0.9l0.9-0.8l-0.1-0.9l0.9,0.1l0.8-0.9h1.6l0.9-0.9h0.8v-2.4h-0.8v-0.9h-0.9
	l-0.8,0.9h-0.9l-0.8-0.8l-0.8-0.1l-0.2-0.8h-2.4l0.1,0.8l-0.9,0.9H104v0.8l-1.6-0.1v0.9l-1.7,0.1L100,394l-1.6-0.1l-0.1-0.8h-1.7
	l-0.8-0.9l-0.8,0.1l-2.5-2.4l-0.8-1.8l-0.1-1.6l-0.9-0.9l-1.6-0.1l-0.8,0.9l-0.9,0.1l-0.8,0.8h-0.8v-0.8h-0.9v-0.9l-0.9-0.9h-1.6
	l-0.9-0.8l-0.1-2.5l-0.9-0.8l-0.1-0.8l-0.8-0.1l-1.6-0.8l-0.8-0.9v1.6l-0.8,0.8l-6.5-0.1l-0.8,0.9h-0.9l-1.7,1.8l-1.5-0.1v0.9
	l-0.9,0.7l0.2,3.4h-2.6l-0.9-0.9h-2.3v1.6h1.6v0.8l0.9,0.9v0.9l0.9,0.9l1.6,0.8l1,0.8h0.8l-0.8,0.8v1.6h-3.4l0.3-0.2h-1.6v0.9
	L59.1,397.5z" />
                                    <path class="st0 dist-block" id="27" d="M221.6,262.3l-1.8-1.6h-0.9l-0.8-1h-1.7l-0.8-0.8H214l-0.9-0.9h-2.3v2.6l-0.8,0.9v0.8h-1.7l-0.8,0.8l0.1,3.1
	l0.8,0.9l0.1,3.3l0.9,1.6v1.7l1.8,1.4h0.8l0.1,1.7l-0.7,0.9v0.8l0.7,0.9v1.6l-0.7,0.8l-0.8,1.8v0.9h-0.8v0.8l-1.6,1.6h-0.9l-0.9,0.8
	l-1.6,0.8l0.2,0.8l-0.9,1.7l0.2,5l-0.8-0.1v-0.7h-0.9l-0.8-0.8l-0.8,0.8h-0.8l-0.9,0.8h-5.8v0.8l-0.8,0.8l0.2,4.1l-0.8,0.8v0.9
	l-0.9,0.8l-0.8,0.1l-2.3,2.4v0.8l-0.8,0.8v0.8h-2.4l0.1,2.6l-0.9-0.2v-0.9l-1.6,0.1l-1.7-1.6h-0.9l-0.1-2.4l-0.8-0.9l-2.5,0.1
	l-0.8-0.9v-0.8l-1.7-1.7l-0.1-0.8l-0.9-0.9V300l-0.9-0.9h-0.8l-0.8-0.8v0.8l-0.8,0.8v1.7l-0.8,0.9v0.8l-0.6,0.9l0.1,0.8l-2.3,2.4
	l-2.6-0.1l-0.8,0.9h-1.6l-0.9,0.7h-0.9l0.1,0.9l-0.8,0.8l0.8,0.8l0.1,0.8h0.9v0.9l0.8,0.2v0.9l1,0.9l0.7,1.6l0.1,2.5l-0.7,0.8v0.9
	h-0.8v2.4l-1.6,1.6v0.9l-0.8,0.8h-0.9l0.2,2.5h-1.1v0.7l1,0.9h0.8v0.9h0.7l-0.7,0.9v0.8l-0.8,0.9h-1.6v1.6h-0.8v0.8l0.8,0.9h0.8
	l0.9,0.9l4.2,0.1l0.7-0.9l0.9-0.1l0.9,0.9l0.9,0.1v0.9l1.6,1.6v0.8h0.9v0.9l0.9,1.6h0.8l0.8,0.8l0.1,4.1h1.7v-0.8h0.8l0.8-0.9v-0.9
	l0.8-0.7h0.8v-0.8l0.8-0.9v-0.8l0.8-0.8l0.8-0.1l0.1,0.8l0.8-0.8v-1.3l0.9,0.1v-0.8h2.4l0.9,0.8v0.9h0.8v-0.9h0.9l2.4-2.4v-0.8
	l0.7-0.9l1.6,0.1v-0.9h0.8v0.9h0.8l0.9,0.8h0.9v0.8l-0.9,0.8l0.1,1.6l-0.9,0.1v0.8H190l-1.6,0.8h-0.8v0.8l-0.8,0.9l0.9,0.8v1.6h0.8
	v0.8l-0.8,0.9v0.9l0.9,1.7l0.1,3.2l0.8,0.9h1.7l-0.2-2.3l0.8-0.9v-1.6l1.7-1.8h0.8V348l0.7-0.8v-0.8l-0.8-0.9l0.7-0.7v-0.9h0.9V343
	h3.3l1.7,1.8h0.8l0.2,4.1l0.8,0.8l0.1,2.6h0.9l0.1,0.9h0.8v0.8h0.8v0.9l1.7-0.1v-0.9l2.4,0.1l0.9,0.9l0.1,1.7l2.5,2.4h1.6l-0.2-4.1
	l-0.7-0.9v-0.7h1.5l0.9,0.8l-0.1-3.3h-0.8l-0.1-1.6l-0.7-0.9l-0.2-0.9h0.8v-0.9l0.8-1.6l0.1,0.8h0.9v2.5l0.9,0.9l1.6-0.8l0.2,5.8
	l2.4-0.1l0.9-0.8l-0.1-3.3l0.8-0.8v-1.9h0.8v0.9l0.8,0.9v0.9h0.9V349l1.6-1.8v-0.9l0.8,0.1l0.9-0.8l-0.1-0.9l-1.6-1.6v-0.8l0.8-0.8
	h0.8v-0.8h0.9l-0.1-0.9h0.9v-0.6l0.8,0.7l0.1,0.9l-0.8,0.8h0.8v0.9h1.6l0.2,2.4l0.9,0.9l-0.8,0.8v0.9l0.9,0.9l0.8-0.1l0.9,0.9h0.8
	l0.1,1.6h0.9v1.6l0.9,0.9h0.8v2.6l1.7,1.6l0.2,0.8l0.9,0.8v1.6l0.8,1v0.8h0.9l1.6,1.6h0.8l0.8-0.8l-3.3-3.3v-0.9l-0.8-0.9l-0.1-0.8
	l0.8-0.8v-0.8h0.9l1.5-1.8h0.9v-0.8l0.8-0.7l1.6-0.9v0.8l0.9,0.8h0.9v0.8l0.8,0.9l0.8,0.1l1.7,1.6l2.5-0.1l-0.1-2.6l0.8-0.6h1.6
	l0.9,0.8h0.9l0.9,0.9v0.9h0.8l0.8-0.9h0.8l1.7-1.6l-0.2-0.9h0.9v-0.8l-0.9-0.9h-0.8l-0.9-0.8v-1.8h1.7l1.7,0.9h0.8l0.8,0.9l0.8-0.9
	h0.8v-1.6h-0.9v-0.9l-0.8-0.7v-1.7l0.8-0.9l-0.1-1.6l-0.8-0.7h-0.7l-1-0.9v-1.6l0.8-1.7v-0.7h-0.8v-0.9l-2.6-2.6v-0.8h0.8l0.7-1.7
	v-0.7l1.7-1.6l-0.1-0.9l0.8-0.8l-0.1-1.6H268v-1.6l1.6-0.1l-0.1-0.9l0.9-0.8v-0.9l-0.9-0.8h-2.2v-0.8l-0.8-0.8l-0.8-0.2l-0.2-0.9
	l-0.8,0.9h-1.6l-1.6-1.6h-0.9v-0.8l-0.8-0.9l-0.2-2.4l0.8-0.9v-0.8l0.8-1.6h0.8v-2.4l-0.8-0.8l-0.1-0.9h0.8v-0.9h0.8v-0.8h0.8V302
	l-2.5-2.6h-0.9v-0.8l-0.7-0.8v-0.9l-0.9-0.8l-0.1-2.4l0.9-0.9v-0.6l-0.1-2.5l-0.9-0.9V288l-2.5-0.1l-0.8,0.8l-2.4,0.1l-1.7-1.6
	l-0.1-1.6h-0.9v-0.8h-0.8l-0.8-0.9h-4.1v0.9h-1.6l-2.4,2.3l-1.6,2.6v0.9h-0.8l-0.9-0.9l-0.1-0.8l-2.4-2.5l-0.8,0.8h-0.9v1.7
	l-0.7,0.8h-0.9l-0.2-3.3l-0.9-0.8v-0.8l-0.8-0.8v-0.9l-0.8-0.9l-0.1-1.6l-0.9-1.7h-0.8V278l0.8-0.7v-1.7l-0.9-0.8v-1.6l-1.7-1.7
	l-0.9,0.1l-0.8-0.9l-1.6-0.1l-1.6,0.9H219l-0.2-3.3l1.9-1.6l-0.1-2.5h0.9L221.6,262.3z" />
                                    <g>
                                        <g>
                                            <g>
                                                <path class="st0 dist-block" id="28" d="M288.7,167.8h-0.8l-0.8-1.7l-0.9-0.8v-0.8h-0.9l-0.8-0.9l-0.2-0.8l-1.6-1.7h-0.8l-0.9-0.8l-2.4-0.2v-0.8
				l-1-1.7l-0.9-0.8V156l-0.9-0.8v-0.8l-1.6-0.1l-0.9-0.9h-1.7l-0.8-0.8l-7.5-0.2l-1.6-1.7v-0.8l-0.9-0.8v-1.7l-0.9-0.9v-0.3
				l-1.7-1.7l-7.6-3.3l0.9-0.8l-0.2-1.7l0.8-0.8v-5l-0.9-0.8v-0.8l-0.9-1h-0.8l-0.1-0.8h-0.9l-0.8-0.7l-0.1-4.2l0.8-0.9l-0.1-3.2
				l-0.8-0.8h-0.9v-0.8l-0.8-0.1l-1.7-1.7v-1.8h1.6l0.9-0.8h0.8v-1.7h-0.9l-0.1-5.7l-0.9-0.1l-1.7-1.7H243l-0.8-0.8l-0.2-0.9
				l0.9-0.8v-0.9l0.6-0.8l10.7,0.1l0.9-0.9l1.6,0.1l0.9-0.9l-0.1-0.9l0.9-0.7l-0.1-1.7l-0.8-0.8l-0.2-4.3l-0.8-0.8v-0.9l-0.9-1.5
				l-1.7-1.7v-0.8l0.9-0.2l0.8-0.6v-0.9l0.8-0.9v-0.6l1.7-0.8h2.5l1.6,1.6h1.7v-0.8l0.8-0.8h2.5l1.1,0.8h2.5l1.6-1.7l7.5,0.1
				l0.9,0.9l2.4-0.1l0.9,0.8l0.8,0.1v-0.8h2.4l0.8-0.9l-0.1-2.4l0.8-0.9v-0.8l3.2-3.2l-0.1-3.4l0.8-0.8l0.8-1.7l0.9-0.8l-0.1-0.8
				l0.8-1.7l-0.1-2.5l0.7-1.6v-2.6l-0.9-0.8h-0.5l-0.9-0.9h-8.3L285,50l0.8-0.8v-0.9l0.8-0.9h4.2l1.6-0.8v-0.8l1.6-0.9V44h0.8
				l2.5-2.4h0.8l3.2-1.6l0.8-0.8h2.4l0.9,0.8l0.9,1.7v0.9l1,0.8V44l0.8,1.8v4.4l1,0.9v1.7l0.9,0.8l0.1,3.2l-0.9,0.8l0.1,5l0.9-0.9
				l-0.1-0.8h0.8l0.8,0.8l-0.6,0.8l0.1,2.7l-0.9,0.9l-1.8-0.1l0.1,0.8l0.9,0.9v0.8h0.9l0.1,1.7l0.8,0.8v0.9h0.8v0.9l0.8,0.9l0.1,0.8
				h-0.9l-0.7,1.6l0.2,0.9l0.6,0.8v0.4l1.7,0.1l0.9,0.8V80h0.8l0.1,2.5l0.9,0.9h4.2l0.9,0.9v-0.5l0.7,1l-1.1,0.4l0.9,1.3l-0.5,1
				l-1.3,0.3c0,0-1,0.6-1.1,0.6s-4.7,0-4.7,0l-1.7,1.7l-4.1,0.1l-0.8-0.9v0.8l-1.2,0.1l0.9,0.5l-0.5,1h-0.9v0.8l0.9,0.9v0.9h1.6
				l0.9,0.8h1.4l1.1,1.1c0,0,0.5-0.1,0.7,0c0.3,0.1,0.1,1.1,0.1,1.1s-0.2,0.4,0.4,0.4c0.5,0,1.3,0.8,1.3,0.8l0.1,0.8h-4l-0.9,1.7
				H309l0.2,0.8l-0.9,0.9l0.2,5.8l-0.8,0.7v0.8l0.8,0.9h0.9l0.1,0.9l-0.9,0.8l-0.9-0.2v0.9l-0.8,0.9v2.4c0,0,1,0.5,1,0.9
				s0,0.9,0,0.9l0.8,0.8l0.1,0.9l4.1-0.1l0.8,0.9l0.4,1.3l0.6,1.1c0,0,0.2,1.2,0.3,1.4c0.1,0.2,1.4,2,1.4,2l0.2,5.7h1.4l1.4-1
				l-0.5-1.5l2.6,0.9l1.1-0.8l0.7-0.4l1.3-2.1c0,0,0.7-0.7,1.1-1.2c0.5-0.5,1.3-0.5,1.3-0.5l1.3-0.6h1.3l0.7,1.3l0.1,1.1h1.7
				l0.5,1.2l1.2,0.4l0.6,0.5l0.4,1.2l0.5,1.3l0.4,0.3v0.9h-0.9l-0.8-0.9h-1.1l-0.8,0.2l-0.6,0.7l0.8,0.9l-0.8,0.9v1.1l0.4,1.6
				l0.6,1.5l0.4,1.1l-1.3,1.3v1.1l-2.1-1.6l-1.2,0.6l0.1,0.9l-0.9,1.6l-0.6,1.6c0,0,0,0.8,0,1.6s0.9,1,0.9,1l0.9,0.8l0.9,1.7h-0.9
				l-0.8,0.9l-0.5-0.5l-1.2-1.2h-1.9l-3.1,0.5l-2.3,1.2l-2.5-1.7c0,0-2,0-2.3,0s-2.7,0-2.7,0l-0.8,0.8c0,0-1.5,0.8-1.9,1.1
				c-0.3,0.3-2.1,1.4-2.1,1.4l-1.6,0.9l-1.8,0.8h-0.8l0.8,2.2l0.1,1.3l0.1,2.8l-0.8,1.3h-3h-2.8l-2.4,1.1l-1.7,0.4L288.7,167.8z" />
                                                <polygon class="st0" points="321.7,83.3 321.3,83.3 321.4,83.5 " />
                                            </g>
                                        </g>
                                    </g>
                                    <path class="st0 dist-block" id="8" d="M321.6,84.4l-0.8,0.4l0.9,1.3l-0.5,1l-1.3,0.3c0,0-1,0.6-1.1,0.6s-4.7,0-4.7,0l-1.7,1.7l-4.1,0.1l-0.8-0.9v0.8
	l-1.2,0.1l1,0.5l-0.5,1.1h-0.9v0.8l0.9,0.9V94h1.6l1,0.8h1.4l1.1,1.1c0,0,0.5-0.1,0.7,0c0.3,0.1,0.1,1.1,0.1,1.1s-0.2,0.4,0.4,0.4
	c0.5,0,1.3,0.8,1.3,0.8l0.1,0.8h-4l-0.9,1.7h-1l0.2,0.8l-0.9,0.9l0.2,5.9l-0.8,0.7v0.8l0.8,0.9h0.9l0.1,1l-0.9,0.8l-0.9-0.2v1
	l-0.8,0.9v2.5c0,0,1,0.5,1,0.9s0,1,0,1l0.8,0.8l0.1,0.9l4.1-0.1l0.8,0.9l0.4,1.3l0.6,1.1c0,0,0.2,1.3,0.3,1.4c0.1,0.2,1.4,2,1.4,2
	l0.2,5.7h1.4l1.4-1l-0.5-1.5l2.6,0.9l1.1-0.8l0.7-0.4l1.4-2.1c0,0,0.7-0.7,1.1-1.2c0.5-0.5,1.3-0.5,1.3-0.5l1.4-0.6h1.3l0.7,1.3
	l0.1,1.1h1.7l0.5,1.2l1.2,0.4l0.6,0.5l0.4,1.2l0.5,1.3l0.4,0.3v0.9h-0.9l-0.8-0.9h-1.1l-0.8,0.2l-0.6,0.7l0.8,0.9l-0.8,0.9v1.1
	l0.4,1.7l0.6,1.5l0.4,1.2l0.5,1.3l2.5,1c0,0,1.3,0.6,1.8,0.7c0.5,0.2,2.5,0.8,2.5,0.8l0.8-1.6l-1-2.1l1-0.4l3.1,0.3l1.4,0.3l1.5,0.8
	l1.2,0.1l1.7,1c0,0,0.6,0.2,1.4,0.2s1.4-0.8,1.4-0.8l-0.1-0.9l-1.4-1.2l2,0.5h0.4l0.8,2.1v1.3l0.8-1.7l-0.1-2.7l0.7-0.8l0.4-1.2
	l-0.5-2.2l-3.3-2.2l1-0.9l0.7-2.5l3.3-2.6l1,1.8l1.6-1h2.5l-0.1-2.5l-1.9-3.5l-0.2-1.9l-1.5-3.3l-0.1-2.3l1.6-0.8l2.1-1.4
	c0,0,1,1.4,0.7,1.4s2.1,0,2.1,0v-0.9l1.8-0.7h5.7l1.6-0.9l0.6-1.4l3.5-3.5v-1.7h0.9l-1.1-2.1l-0.7-2.6l-2.6-1.3l-1.7-1.6l-1.2,0.9
	l-7.2-0.1l-0.8,0.8h-1.6v-0.8l-0.8-0.9l0.7-1.7h-0.8l-1-2.6l-1-1.6h-1.9H356l-0.8-0.8h-1.9l-0.9,1.4l-0.6-1.3l-2.6-0.1v0.8h-1.6
	l-1.7,0.7l-1.7-0.7h-1.7l-0.7,0.8h-3.2V93h-0.9l0.1,2.6l-0.9-0.1v0.9h-2.6v-0.9l-0.8-0.8l-0.1-0.9h-0.9l-0.1-3.4l-2.5-2.4h-0.8v0.2
	h-1.7v-0.7l-0.8-0.9l-0.1-1l-0.9-1.7l-0.8-0.8l-0.9-0.8l-1.7-0.1l-1.4,0.9L321.6,84.4z" />
                                    <path class="st0 dist-block" id="11" d="M288.5,298l1.1,0.1v0.8v0.8h0.8h1.6l0.8-0.8v-0.8l1.6-1.6v-0.8l0.9-0.9v0.8l0.9,0.8l0.1,0.8h5.8v0.8h0.8
	l-0.8,0.9v1.6l0.2,6.8l0.9-0.1l0.9,1.8v1.7l-0.8,1.5l1.6,0.2l0.8,0.8l-0.8,0.8h-1.6v2.6l1.8,2.5h3.2v0.8l-0.8,0.8h-0.8l-0.8,0.9v0.9
	l-0.8,0.8v0.9l0.1,0.8l0.8,0.9l0.1,1.6l-0.8,0.9v1.5h0.6l-0.7,0.9v1.7l-0.7,0.8h-1.7l-0.9,0.9h-0.8v0.8l0.9,0.8v0.1h0.1l0.8,0.8
	l-0.9,0.8l0.9,0.9l0.9,0.9v0.8h1.6l-0.8,0.9h-0.7v0.8v0.8l0.8,0.8v0.8h-0.8h-0.8v0.9v1.6l-2.4,0.8l0.1,1.7v0.8l0.9,1.7l1.7,2.4
	l0.9,0.9l2.4,0.8v0.9l1.6,0.8l0.1,0.9l0.9,0.8h1.2h0.8v-0.9l1.5-0.9l-0.7-0.8h-0.8l-1-1.6l1.1-0.9l1.3,0.9l0.1,0.8l1.6,0.1v0.9h0.9
	l0.8-0.9l-0.9-0.9v-0.8l0.8-0.8l6.5-3.3l0.9,0.1l0.9-0.9l0.8-0.1v3.3h1.6l0.9-1.6l0.8-0.7l4.1-0.1v-0.9l0.8,0.1v-0.9l-0.8-0.2
	l-0.1-0.8h-2.5l-0.6,0.9l-0.2-1.7l-2.5-2.5l1.6,0.1l5.7-0.9v-2.1l0.7-0.8h1.7l0.1,0.8h0.8v0.8l0.8-0.8h2.5v0.8l-0.9,0.8h-0.8
	l0.1,0.8l0.8,0.9l-0.8,0.9H338l5,3.4l0.8-0.9l0.8-2.5l1.6-1.7v-0.8l8.8-9.1l0.8-1.6l1.7-0.8l3.2-3.3l4.9-2.4l2.3-0.9l1.6-1.6
	l3.2-1.6v-1.7l0.8-0.8v-0.8h0.8v-0.9l0.8-0.9h0.8v-0.8l0.9-0.8h0.8l1.7-1.7h0.8l2.3-2.5l-1.7-1.6v-0.9l-0.9-0.8l-0.8-1.6l-0.1-1.7
	l1.6-1.6h0.9l0.8-0.9l-0.1-1.6h0.9l-0.9-0.8l-0.2-7.4l0.9-0.8v-0.9l-0.9-0.8v-1.6l-0.8-0.9l-1.6-0.9l-0.8-0.6l-0.9-1.7v-0.9
	l-0.9-0.8l-1.6-0.1l-0.9-0.8h-0.8l-0.9-0.9h-0.8l-0.1-1.6l-0.8-0.9l-0.1-0.8l-0.8-0.2l-1.6-0.8l-3.4-0.1l-1.6-1.5v-0.8l-5-5.1v-1.7
	l-0.9-0.7l-2.5-0.1l-0.9-0.8v-0.9l1.8-1.6l-0.9-0.9l-0.1-2.4l-0.9-0.9v-1.6l-0.8-0.9l-0.8,0.9h-3.4l-0.9-0.9v-2.4h-0.9v-1.2
	l-0.8-0.8h-0.9l-1.6-1.7v-0.9h-0.9l-0.1-2.3l0.9-0.9v-0.9l0.8-0.9l-0.2-5.6l0.9-0.8l-0.2-1.7l-0.9-0.9l-1.6,0.1l-0.8-0.9H341
	l-0.1-0.8h-1.6v-1h-3.4v-0.8l-0.9-0.8v-0.8h-0.8V229h-1.9l-0.2-0.9h-0.8l-3.2,3.3v1.7l-0.7,0.8v1.6l-2.4,2.4h-0.7l-0.9,0.9h-2.3
	l-0.9,0.8h-1.6v1.6l-0.9-0.1l0.1,0.8h-0.9l-1-0.8v-0.8l-1.6,0.1v0.8l-0.7,0.8l-1.6-0.1l-0.9-0.8h-0.8v-0.8l-0.8-0.8l-0.1-0.9
	l-0.8-0.9v-0.5l-0.8-0.1v-0.9l-0.8,0.1v0.9h0.1l-0.9,0.8l0.1,2.5l0.8,0.8v0.8l-0.8,0.9v0.9h-0.8v0.8l-0.8,0.8v0.9v0.9l-0.7,0.8v2.5
	h0.8v0.9l1,0.8l-1.8,0.8h-2.3l-0.8,0.8H300l-2.4,2.4l0.1,0.9l-0.8,0.8l0.8,0.8h2.4h0.9l0.9,0.8h1.6l1.6,1v0.9h4.9l0.1,2.5l0.9,0.7
	v0.9l-1.5,1.6l0.2,5.7h-1.8l-0.8-0.8l-0.8,0.8v-0.8h-2.5l-2.5-2.9h-0.9l-1.6,0.8h-1.6l-0.1-3.3h-0.8l-0.1-0.9h-0.9l-0.8,0.9h-0.9
	v0.8l-0.9,0.9h-0.8v0.8h-0.8l-1.6,0.9l-6.6-0.1v1.6l0.9,0.8l0.9,0.1l-0.9,0.8h-0.5l-0.8,0.9h-0.8l-0.8,0.8v0.8l0.8,0.9l-0.8,0.9
	h-0.8l-0.2-0.8l-0.6,0.8h-0.8v0.8l0.8,0.8v0.9l0.8,0.9v0.9l0.9,0.7l0.1,2.5l1.6-0.8h0.8l0.9,0.8v0.8h-0.9l0.9,0.8v0.9l1.7,0.2v1.6
	l1.6,1.6l0.9-0.1l0.9,0.9h1.6l-0.8,0.9v0.8l-0.8,0.8h-0.8l-0.8,0.9v2.8L288.5,298L288.5,298z" />
                                    <path class="st0 dist-block" id="10" d="M244.3,356.4l2.1-2.4h0.9v-0.8l0.8-0.7l1.6-0.9v0.8l0.9,0.8h0.9v0.8l0.8,0.9l0.8,0.1l1.7,1.6l2.5-0.1l-0.1-2.6
	l0.8-0.6h1.6l0.9,0.8h0.9l0.9,0.9v0.9h0.8l0.8-0.9h0.8l1.7-1.6l-0.2-0.9h0.9v-0.8l-0.9-0.9h-0.8l-0.9-0.8v-1.8h1.7l1.7,0.9h0.8
	l0.8,0.9l0.8-0.9h0.8v-1.6h-0.9v-0.9l-0.8-0.7v-1.7l0.8-0.9l-0.1-1.6l-0.8-0.7h-0.7l-1-0.9v-1.6l0.8-1.7v-0.7h-0.8v-0.9l-2.6-2.6
	v-0.8h0.8l0.7-1.7v-0.7l1.7-1.6l-0.1-0.9l0.8-0.8l-0.1-1.6H268v-1.6l1.6-0.1l-0.1-0.9l0.9-0.8v-0.9l-0.9-0.8h-2.2v-0.8l-0.8-0.8
	l-0.8-0.2l-0.2-0.9l-0.8,0.9h-1.6l-1.6-1.6h-0.9v-0.8l-0.8-0.9l-0.2-2.4l0.8-0.9v-0.8l0.8-1.6h0.8v-2.4l-0.8-0.8l-0.1-0.9h0.8v-0.9
	h0.8v-0.8h0.8V302l-2.5-2.6h-0.9v-0.8l-0.7-0.8v-0.9l-0.9-0.8l-0.1-2.4l0.9-0.9v-0.6h2.2v0.7l0.9,0.9h2.5v-0.9l0.8,0.9l1.6-1.7h0.7
	l-0.1-0.9l2.5,0.1v0.9h2.6l0.7,0.8l0.9-0.1l0.9-0.7h2.5v0.9h0.9v1.6l0.8,0.9v0.7l4.9,0.1l0.9,0.8l1.6,0.9h0.8h1.7v0.8v0.8h0.8h1.6
	l0.8-0.8v-0.8l1.6-1.6v-0.8l0.9-0.9v0.8l0.9,0.8l0.1,0.8h5.8v0.8h0.8l-0.8,0.9v1.6l0.2,6.8l0.9-0.1l0.9,1.8v1.7l-0.8,1.5l1.6,0.2
	l0.8,0.8l-0.8,0.8h-1.6v2.6l1.8,2.5h3.2v0.8l-0.8,0.8h-0.8l-0.8,0.9v0.9l-0.8,0.8v0.9l0.1,0.8l0.8,0.9l0.1,1.6l-0.8,0.9v1.5h0.6
	l-0.7,0.9v1.7l-0.7,0.8h-1.7l-0.9,0.9h-0.8v0.8l0.9,0.8v0.1h0.1l0.8,0.8l-0.9,0.8l0.9,0.9l0.9,0.9v0.8h1.6l-0.8,0.9h-0.7v0.8v0.8
	l0.8,0.8v0.8h-0.8h-0.8v0.9v1.6l-2.4,0.8l0.1,1.7v0.8l0.9,1.7l1.7,2.4l0.9,0.9l2.4,0.8v0.9l1.6,0.8l0.1,0.9l0.9,0.8h1.3v2.5l0.8-0.1
	v1.6l-2.4,1l-1.7,0.7l-3.1,2.5l0.9,1.6l-2.5,0.8l-0.6,4.8h-0.9v1.6h-0.8l-0.7,0.9l-0.8-0.1l-1-0.9l-1.7,0.1l0.1,1.7h-0.8l-0.9,0.7
	h-0.8v-0.8h-2.4l-2.4,0.8h-2.6l-0.7-0.8h-3.3l0.1,2.4h-0.8h-0.8l-3.5-0.9h-1.6l-0.8-0.9v-1.6H274l-0.2-0.8l-1.6-0.8h-4.9l-0.9-0.8
	l-0.9-0.2l-0.9-0.8h-0.9l0.1,0.8L263,375l-0.8,0.2l-0.8-0.8l-1.7-0.2l-0.9-0.8h-1.6l-0.8-0.9v-0.9l-0.9-0.9l-0.8,0.1l-0.8-0.8v-0.9
	H253l-0.1-0.9H252v-0.9l-1.7-1.6h-0.8v-0.8h-0.8l-0.2-7.4h-0.8l-2.2-1L244.3,356.4z" />
                                    <path class="st0 dist-block" id="26" d="M468,245.2l-1.3,0.1l-1.7,0.9l-0.8-0.9h-4l-0.9-0.8h-0.9l-0.6,0.8h-0.9v0.9l-1.6-0.2l-0.1-2.5l-1.6,0.1
	l-0.9,0.9l-2.4-0.1l-0.9-0.9l-0.1-1.6l1.6-1.6l-0.1-0.8l-0.8-0.9h-5.7l-0.8,0.9l0.1,0.8l-1.8-0.1l-0.9-0.9l-1.6,0.2l0.2,4h-0.8
	l-0.9-0.9h-2.4l-0.8,0.8l-0.8-0.8l-0.1-0.8h-0.8V241h-1.8v0.8h-1.6l-0.8,0.8l1.6,1.6l-0.3,1.4l0.8,0.9H430l0.2,2.5h-2.6l-0.7-0.9
	l-0.1-0.9l-0.8,0.1l0.1,0.9l-0.9,0.8l0.1,2.4h-0.9l0.1,2.5h0.8l-2.3,2.3l-0.9-0.8l-0.1-0.8h-0.8l0.1,0.8l-1.6,1.6l0.1,1.7h0.8
	l-0.8,0.8l0.1,2.6H419v-0.8l-1.6-0.2l-0.8-0.9h-1.7v0.9l-0.8,0.9l0.2,0.8h2.3v0.8h-0.8v0.9h3.3l0.2,4.8l0.3,1.1h-0.9l-0.8,0.8h-1.6
	l-0.9-0.9h-0.8l-1.6-0.9h-1.8l-0.7-0.8l-2.5-0.1v0.8h-0.8l-3.1,3.5l-1.6,0.7l-4.9,4.9h-3.3l-1.6,0.8h-1.6l-2.3,2.6l-1.6,3.4
	l-0.8,0.8v1.6l-1.6,1.6l-1.5,0.8h-1v0.9l-0.9,0.8l0.2,7.4l0.9,0.8h-0.9l0.1,1.6l-0.8,0.9H380l-1.6,1.6l0.1,1.7l0.8,1.6l0.9,0.8v0.9
	l1.7,1.6l1.6-1.6l1.6-0.8l2.5-1.6l1.6-1.7l1.6-0.7l2.3-1.6l1.7-1.7l2.3-0.8l1.6-1.6l1.7-0.9l3.1-0.6l1.6-1.6l3.2-1.7l4.9-1.6
	l8.1-4.3l2.4-1.6h1.6v-0.7h1.7l-0.2-0.9h1.6l3.4-1.7l0.9,0.2l1.6-0.9l2.4-0.8l1.7-0.9l2.4-0.8c0.5-0.3,1.1-0.5,1.6-0.8h0.9l0.9-0.9
	l6.5-2.4l1.6,0.1l2.5-0.8h2.5l5.6-0.9h0.8l0.8-0.9v-0.8l2.4,0.1l0.2,0.8h0.8v-0.8l1.6-0.1l1.7-0.8h2.4l3.2-1.6h0.9l1.6-0.8l0.9,0.1
	l4.1-1.8l0.7-0.7h0.8v-0.8h0.9l13.1-5.7h0.8v-0.9h-5.1l-0.7-0.8v-3.3l-0.9-0.1l-0.9-0.9h-2.5l-0.9-0.8h-0.9l-0.6-0.8l-0.2-0.8
	l-0.9-0.9l-7.4-0.1h-0.8l-0.9,0.9h-3.2l-0.8-0.9v-0.6H473v-0.9h-2.6l-0.8-0.9l0.4-2.6L468,245.2z" />
                                    <path class="st0 dist-block" id="24" d="M333.2,204.2l-0.7,0.9v0.8l-0.8,1.6l-0.8,2.5l-0.8,0.8l0.9,0.9l0.9,1.6H331l-0.8,0.8h-0.9l-0.8,0.9h-4.1
	l-0.9-0.9h-0.9l-1.6,0.8l-1.6,0.1v-0.9l-1.6,1.6h-2.6l-1.5,1.7h-0.9l0.1,2.5v0.8l-1.5,1.6h-0.9v0.9h0.9l0.1,0.8l2.4,0.2l0.8,0.6
	l0.8-0.8h0.9l0.9,0.8l0.1,3.4l-0.8,0.9l0.1,4.1h0.8l0.1,0.8l0.9,0.9v0.8l0.8,0.9l0.9,1.6l0.9,0.9h2.3l0.9-0.9h0.8l2.4-2.4v-1.6
	l0.7-0.8v-1.7l3.2-3.3h0.8l0.2,0.9h1.6v0.8h0.8v0.8l0.9,0.7v0.1v0.8h3.4v0.9h1.6l0.1,0.8h0.9l0.8,0.9l1.6-0.1l0.9,0.9l0.2,1.7
	l-0.9,0.8l0.2,5.6l-0.8,0.9v0.9l-0.9,0.9l0.1,2.3h0.9v0.9l1.6,1.7h0.9l0.8,0.8v1.6h0.9v2.4l0.9,0.9h3.4l0.8-0.9l0.8,0.9v1.6l0.9,0.9
	l0.1,2.4l0.9,0.9l-1.8,1.6v0.9l0.9,0.8l2.5,0.1l0.9,0.7h0.8l0.8-0.8l1.6,0.1l0.9,0.9h9l0.6-0.9h0.9l0.9-0.8l1.6-0.9h4.1l1.6,1.7h0.9
	l0.9,0.8h1.6l0.8-0.8h4.1l0.9-0.8h4.1l3.1-1.6l1.2-0.6l1.2-0.6l1.1-0.1l0.9,0.6l0.8-0.1l0.8,0.8l1.6,0.2l1.7-1.8h0.9l-0.1-0.8h0.8
	l-0.1-2.6l-0.8-0.8v-0.9l-2.6-2.3l-0.1-0.8l-0.8-0.2l-0.8-0.8h-0.8l-0.9-0.9l-0.9-1.6V250h-0.8l1.6-1.8h2.4l-0.1-1.6l0.8-0.7v-1.3
	l-1-0.3v-0.5H401l-0.1-0.5l-0.7-0.9l-0.9,0.9h-0.9l-0.1-2.4l0.9-0.8v-1.9h-2.5v0.9h-0.8l-0.7,0.9l-0.9-0.2l-1,0.9v0.8h-1.6l-0.8,0.8
	H390l-1.6,0.8l-1.6,1.7H386v0.8h-3.3l-0.1-4.9l-0.9-0.9l0.8-1.6v-0.8l0.7-0.9v-0.9h5.8v-0.8h4v-0.6h0.9v-0.9l0.8-0.8l-0.1-4.1
	l1.6-1.6l0.8-1.8v-0.8l1.6-3.3H388l-0.9-0.8l-1.6-0.9h-5.7v0.8h-2.6l-0.8-0.8h-0.8l-0.9-0.9v-0.9h-0.9l-0.8-0.8h-1.6l-0.9-0.8h-0.8
	l-0.8-0.9H368l-5-2.6l-1.7-1.6l-1.6-0.9l-0.9-0.8H358l-0.9-0.8h-1.6l-1.7-0.9h-4.1l-1.6-0.8l-2.7-2.6v-0.8l-2.4-2.5l-0.9-1.6h-0.8
	l-1.6,1.6v0.8l-1.5,1.7h-0.9v0.8l-0.9-0.1l-0.9-0.8h-0.8l-0.9,0.9h-0.6V204.2z" />
                                    <path class="st1 dist-block block-kj" id="18" d="M491.7,133.9V133h0.7l0.8-0.7h-0.9v-0.8l-0.7-0.9v-0.8h-0.9l-0.1-2.4v-0.9l0.9-0.8l-0.1-0.8l-0.9-1v-0.7h-0.8
	l-1.7-1.6h-0.8l-1.8-1.6h-0.7l-0.9-1h-1.6l-0.8,0.9l0.1,1.7l-3.9,4.1v1.6l-0.9,0.9v0.8l-2.4,2.4h-0.8l-0.8-0.8h-0.9l-0.9-0.8
	l-2.5-0.1l0.1,0.9h-0.8l-0.8-0.9v-1.6H466v-0.9l-0.9-0.9l-0.1-2.4l0.9-0.8v-0.8l0.8-0.8l3.1-5.1l0.8-0.7v-2.5l-0.9-0.1l-0.8-0.7
	v-0.9h-1l-1.5-1.6h-1.7l-1.6-1.6h-1.7l-0.8-0.9l-1.7-0.8l-1.6-1.6l-0.1-0.8h-0.8v-1.8h0.8v-0.7h0.8l0.8-1l0.8-1.6l0.7-0.8l0.8,0.1
	l0.9-0.9l-0.1-0.8h0.9v-1.6l0.7-0.9v-0.8l-0.8-0.8l-0.1-1.7l-1-1.7l-0.8-0.1l-0.8-0.7h-1.5v-0.7h-0.9l-0.1-2.6l-0.8-0.7H456l-1-0.9
	l-4.1-0.2l-0.8-0.7h-3.3l-0.8-0.9l-0.9-1.6l-0.8-0.9l-1.7,0.1l-0.9-0.9h-0.8l-1.6-1.6l-2.5-0.1l-0.2-4.1l-0.7,0.1l-0.1-4.1l-0.8-0.8
	v-0.9h-0.8l-0.1-2.6v-0.2v-0.4l-1.8-1.7l-0.8-0.2l-0.9-0.7h-0.9l-1.5-1.7h-3.3l-0.1-0.9h0.9V57l-1-0.9h-1.6v-2.4h0.8l-0.1-0.9
	l0.9-0.7v-0.9h-0.9l-0.9-0.8h-2.4l-1.6-0.8l-7.5-0.1l-1.8-1.7h-0.7l-1.7-1.7h-1.5l-0.9-0.8l-3.3-1.6l-1.6,0.1l-1-1h-1.6l-1.6-0.8
	h-0.8v0.8l-0.9-0.1l0.2,0.8l-1,0.8v0.9l-0.8,0.9h-1.6L390,47l-2.4-0.1l-0.9,0.8l-0.8,0.1l-0.7,0.8v0.9l-0.8-0.1v0.8l-0.8,0.8v0.8
	l-0.8,1.7l-2.3,2.4v0.8v0.8l-0.8,0.9l0.1,0.8l3.2,3.3l7.4,0.1l0.8,0.8h0.8l1.8,1.8l0.1,3.1l-0.8,0.9v2.5h-0.8v1.6l-0.7,0.9v1.6h-0.8
	v0.8l-2.4-0.1l-0.8,0.8l-0.9,0.1v-0.8h-0.8l-0.5-0.4l-0.3-0.4l-0.9-0.1l0.1,0.8h-0.9v0.8h-1.6v0.9h-1.7l-0.8-0.9v-0.8h-0.7v-0.8
	l-0.8,0.8v0.8h-0.9l0.1,3.3l-0.8,1.7v0.9l0.2,0.1l0.7,0.8h0.8v1.5l-0.8,0.8h-0.9l-0.7,0.9l0.8,0.8l0.1,1.6l-0.9,0.8l0.6,1.4l0.1-0.1
	l0.2-0.2l1.5-1.4h1.3l0.7,0.3l1,0.4l0.2,0.1h1.6l2.1,0.4l-1.8,1.9l-0.2,0.2l0.9,1.6l1.1,1.1l3.1,3.1l0.2,3.3l-1.5,0.2l0.7,0.7
	l0.8,0.8h0.8l0.9,3.3l0.1,0.2l0.2,0.3l0.6,1.1l-0.4,0.9l-0.3,0.5l-0.1,0.2l-1.5,0.2l-4.1,0.6l0.2,0.4l0.6,1.2l0.9,0.9l3.2,1.6
	l0.9,2.6l0.1,0.1l0.8,0.7v0.3l0.1,3.8l-0.4,0.4l-0.4,0.4l-0.1,0.1l-0.7,0.3l0,0l-0.9,0.4l-0.8,0.9l-0.7,0.8l-0.1,0.1l-0.5,0.3h-0.1
	l-1,0.6l0.2,0.3l0.7,1.3l0.1,1.6l2.4,0.8c0,0,2.6-0.1,3.4,0.8c0.2,0.2,0.4,0.4,0.6,0.5c0,0,0,0,0.1,0.1c0.2,0.2,0.4,0.3,0.7,0.5
	c0.6,0.4,1.1,0.7,1.1,0.7l0.3,0.3l1.5,1.3v0.1l0.1,1.6l1.6,1.7l0.9,1.6h-1l2.8,2h0.7l3.4,3.2h0.8l0.8,0.8l2.5,0.1l0.9-0.8v-0.8
	l0.8-1v-0.8h0.7v-0.7l2.5,0.1l0.8,0.6h1.7l1.6,0.8l0.7-0.8h0.9l0.8-0.6v-0.9l3.3,0.1l0.8-0.9h0.7l0.9-0.9l2.5,0.1v0.9h1.6v0.8h-0.8
	v0.8v0.7l2.5-0.1v-0.6l0.9,0.7h0.9v0.9l1.6,1.7v0.8h4.9l0.9,0.8h1.6l0.8,0.9h4.9v-0.9h0.8V147l0.8-0.8v-0.9h0.9l0.8-0.8l1.6-0.1
	l0.9-0.8h0.8v-0.7l1.5,0.8l1.7-0.7l0.7-0.9h0.9l0.9-0.9l3.2,0.1v3.2h-0.7v0.9l-0.8,0.8h-0.8v1.6l-0.8-0.1v0.9H460v3.3l1.7,1.7h0.8
	l0.9,0.9h0.8v0.7h0.8v-1.6l2.5-2.5h0.7l0.7-0.9h0.9l0.9,1.1h1.7l0.8,0.7h0.8l1.6,0.9h2.5l0.9,0.8h3.3l1.6,1.6h0.8l0.9-0.8h0.7v-0.7
	l0.9-0.1l-0.1-0.8l0.8-0.9v-0.7h0.8v-1.7l-0.8-0.9l-0.1-0.8h-0.9V147l-3.2-3.3h-0.9V143l0.8-0.8v-0.9h0.8v-0.8h4.2l-0.1-0.7h0.8
	l1.7-1.7l-0.1-1.7h0.9l-0.9-0.9v-1.6L491.7,133.9L491.7,133.9z" />
                                    <path class="st2 dist-block" id="1" d="M398.1,139.1l-1.6-1.7l-0.1-1.6l0,0v-0.1l-1.5-1.3l0,0l-0.3-0.3c0,0-0.5-0.3-1.1-0.7l0,0l0,0
	c-0.2-0.2-0.4-0.3-0.7-0.5c0,0,0,0-0.1-0.1c-0.2-0.2-0.4-0.4-0.6-0.5c-0.8-0.9-3.4-0.8-3.4-0.8l-2.4-0.8l-0.1-1.6l-0.7-1.3l0,0
	l-0.2-0.3l1-0.6h0.1l0,0l0.5-0.3l0.1-0.1l0,0l0.7-0.8l0.8-0.9l0.9-0.4l0,0l0,0l0.7-0.3l0.1-0.1l0.4-0.4l0.4-0.4l-0.1-3.8l0,0v-0.3
	l-0.8-0.7l0,0l-0.1-0.1l-0.9-2.6l-3.2-1.6l-0.9-0.9l-0.6-1.2l0,0l-0.2-0.4l4.1-0.6l1.5-0.2l0.1-0.2l0.3-0.5l0.4-0.9l-0.6-1.1l0,0
	l-0.2-0.3l0,0l-0.1-0.2l-0.9-3.3H388l-0.8-0.8l-0.7-0.7l1.5-0.2l-0.2-3.3l-3.1-3.1l-1.1-1.1l-0.9-1.6l0.2-0.2l0,0l1.8-1.9l-2.1-0.4
	H381l-0.2-0.1l0,0l-1-0.4l0,0l-0.7-0.3h-1.3l-1.5,1.4l0,0l-0.2,0.2l-0.7,1.6l0.1,1l-0.9-0.1l-0.9,1.4l-1.4,1.1l1.7,1.6l2.5,0.9
	l0.6,1.9l0.3,1.7l0.9,1.5h-0.9v1.6l-1.6,1.6l-1.9,1.8l-0.6,1.4l-1.6,0.9c0,0-0.7,0-1.1,0s-2.8,0-2.8,0l-3.6,0.7v0.9h-2.1l-0.6-1.4
	l-2.1,1.4l-1.6,0.8l0.1,2.2l0.8,2l0.9,1.6v1.5l0.9,1.6l0.9,1.8l0.1,2.4c0,0-1.6,0-2,0s-0.5,0-0.5,0l-1.6,0.9l-1-1.8l-1.9,1.6l-1.4,1
	l-0.7,2.5h-0.8l0.1,1.6l3.4,1.6l0.2,2.8c0,0-0.1,0.6-1,0.6s0,3.3,0,3.3l-0.8,0.9v-1.1l-0.8-1.3H351l0.8,1.3l-0.6,1.2h-0.8
	c0,0-1.8,0-2.7-0.9c-0.8-0.9-1.4-0.4-1.4-0.4l-1.9-0.6l-1.6-0.8H342h-1.5l-0.8,0.8l0.8,1.7l-0.8,1.6l-2.4-0.8l-4.2-1.7l-0.5-1.3
	l-1.2,1.3v1l-2.1-1.6l-1.2,0.6l0.1,0.9l-0.9,1.6l-0.6,1.6c0,0,0,0.8,0,1.6s0.9,1,0.9,1l0.9,0.8l0.9,1.6h-0.9l-0.8,0.9l-0.5-0.5
	l-1.2-1.2h-1.9L321,150l-2.3,1.2l-2.5-1.7c0,0-2,0-2.3,0s-2.6,0-2.6,0l-0.8,0.8c0,0-1.5,0.8-1.8,1.1s-2.1,1.4-2.1,1.4l-1.5,0.9
	l-1.8,0.8h-0.8l0.8,2.2l0.1,1.3l0.1,2.8l-0.8,1.3h-3h-2.8l-2.4,1.1l-1.7,0.4l-4.1,4l5.9,4.3l1.2,1.1l1.4,1.3v0.6v1.9l-0.3,1.8l2,1.5
	l0.5,0.5l1.3,1.1l3.3,1.7l3.1,0.2l1-0.2l2.5,0.9l0.1,1.8l2.4-0.1l4.2,2.4h0.6l1.6,1.1l1.3,1.2l2.2,2l2.5,1.6l0.9,0.9
	c0,0,3.7,0,4.9,0s4.1,0.9,4.1,0.9l2.6,0.9l3.4,2.4l4.1,4.9l1.8-2.4l1.4-2.5l2.6-0.9l0.8,0.9l2.5-0.2l2.5,1h1.6l2.5-1.6h5l2.4-1.6
	l0.7-1.6l2.5,0.9h3.4l0.5-1.7l-1.6-3.4l-0.8-2.5l0.5-0.5l1-1.9l1.6-2.5v0.9l2.6,3.2l1.7,1.8l1.1-4.5l2-1.4l-0.1-2.5l-1.7-1.6v-0.8
	l2.5-0.9h3.3l-0.9-1.6l-0.9-0.8l-0.9-1.7l0.9-1.9l-0.1-1.4l-0.2-2.5l-1.6-3.3l0.7-4l0.8-1.7l-0.9-1.7l-3.4-2.4l-1.5-0.9l-2.7-1.6
	l1.5-1.6l4.2-0.2l0.8-0.9c0,0,0.8-1.6,0.8-2.6s0.8-2.3,0.8-2.3v-2.3l2.6-1.4h3.5l1.1-0.6l6.6,0.1L398.1,139.1z" />
                                    <g>
                                    </g>
                                </svg>
                            </div>
                        </div>
                    </div>
                </div>

            </section>
            <!--// About Section //-->
            <!--// Portlet Section //-->
            <div class="portlet-sec" style="display: none">
                <div class="col-sm-4 ">
                    <div class="announcement-sec">
                        <h2>Gallery <a href="gallery.aspx" title="View All">View All</a></h2>
                        <div class="footer-gallery">
                            <div class="img-container">
                                <a href="https://youtu.be/Fb5txS4iciQ" class="html5lightbox">
                                    <figure class="effect-img">
                                        <img src="images/goswift-thumb.jpg" alt="video IMAGE" style="width: 100%" />
                                        <figcaption>
                                            <h4>Zoom<i class="fa fa-play-circle-o"></i></h4>
                                        </figcaption>
                                    </figure>
                                </a>
                            </div>
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="img-container">
                                        <a href="Portal/ImageGallery/<%#Eval("vchImage") %>" class="b-link-stripe b-animate-go  thickbox">
                                            <%--<a href="../Portal/ImageGallery/1106_Passport.jpg"  class="b-link-stripe b-animate-go  thickbox"> --%>
                                            <figure class="effect-img">
                                                <asp:HiddenField runat="server" ID="hid1" Value='<%#Eval("vchImage") %>' />
                                                <asp:Image ID="Image1" runat="server" alt="" />
                                                <figcaption>
                                                    <h4>Zoom<i class="fa fa-expand"></i></h4>
                                                </figcaption>
                                            </figure>
                                        </a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="clearfix">
                            </div>
                        </div>
                        <ul style="display: none">
                            <asp:Repeater ID="RepAnnouncement" runat="server">
                                <ItemTemplate>
                                    <li><a href="Announcements.aspx?annid=<%#Eval("INT_ID")%>">
                                        <div class="date-sec">
                                            <%#Eval("DtmCreatedON")%>
                                        </div>
                                        <span class="listheader">
                                            <%#Eval("VCH_HEADING")%></span> <span class="listdata">
                                                <%#Eval("VCH_CONTENT")%></span> <span class="clearfix"></span></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="proposal-sec">
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="infowizard-sec">
                        <h4>info wizard
                        </h4>
                        <p>
                            Info Wizard is an interactive tool to provide customized information on investment
                        opportunities and guidance on doing business in Odisha.This Info Wizard presents
                        an illustrative list of incentives applicable...
                        </p>
                        <a class="infowizard" href="http://investodisha.gov.in/info-wizard" target="_blank"
                            title="Info Wizard">Enter</a>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="guideline-sec">
                        <a href="feedback.aspx" title="Feedback">Feedback</a>
                    </div>
                    <div class="feedback-sec" style="display: none">
                        <h2>Feedback</h2>
                        <div class="feedback-homesec">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12 twocontrols">
                                        <asp:TextBox CssClass="form-control" placeholder="First Name" ID="txtFirstName" runat="server"
                                            MaxLength="50"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtFirstName_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="txtFirstName" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                            InvalidChars="&quot;<>&;">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:TextBox CssClass="form-control" placeholder="Last Name" ID="txtLastName" runat="server"
                                            MaxLength="50"></asp:TextBox>
                                    </div>
                                    <cc1:FilteredTextBoxExtender ID="txtLastName_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" TargetControlID="txtLastName" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                        InvalidChars="&quot;<>&;">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group twocontrols">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:TextBox CssClass="form-control" placeholder="Email" ID="txtEmail" runat="server"
                                            MaxLength="100"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtEmail_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="txtEmail" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                            InvalidChars="&quot;<>&;">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:TextBox CssClass="form-control" placeholder="Mobile Number" ID="txtMobileNumber"
                                            runat="server" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtMobileNo" runat="server"
                                        Enabled="True" FilterType="Numbers" TargetControlID="txtMobileNumber" />
                                    <cc1:FilteredTextBoxExtender ID="txtMobileNumber_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" TargetControlID="txtMobileNumber" FilterMode="InvalidChars" InvalidChars="&quot;<>&;">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:TextBox CssClass="form-control" ID="txtSubject" placeholder="Subject" runat="server"
                                            MaxLength="100"></asp:TextBox>
                                    </div>
                                    <cc1:FilteredTextBoxExtender ID="txtSubject_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" TargetControlID="txtSubject" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                        InvalidChars="&quot;<>&;">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:TextBox CssClass="form-control" placeholder="Feedback" TextMode="MultiLine"
                                            ID="txtFeedback" runat="server" MaxLength="250"></asp:TextBox>
                                    </div>
                                    <cc1:FilteredTextBoxExtender ID="txtFeedback_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" TargetControlID="txtFeedback" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                        InvalidChars="&quot;<>&;">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12 col-md-6">
                                        <asp:TextBox ID="txtCaptcha" runat="server" placeholder="Enter Captcha" CssClass="form-control upper-case"
                                            MaxLength="6" autocomplete="off"></asp:TextBox>
                                        <a data-toggle="tooltip" class="fieldinfo" title="Enter the Characters shown in Image!">
                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                            TargetControlID="txtCaptcha" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                            InvalidChars="&quot;'<>&;">
                                        </cc1:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span>
                                    </div>
                                    <div class="col-sm-12 col-md-6">
                                        <div class="input-group">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <cc4:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                        CaptchaMinTimeout="5" CaptchaMaxTimeout="240" CssClass="homecaptchaimg" NoiseColor="#B1B1B1" />
                                                    <div class="refresh">
                                                        <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false">  <span class="fa fa-refresh homerefreshbtn input-group-addon" style="cursor: pointer;" onclick="return RefreshCaptcha();"
                                                                                aria-hidden="true"></span></asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </div>
                                    <div class="clear20">
                                    </div>
                                </div>
                            </div>
                            <asp:Button CssClass="ReadMore" ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                OnClientClick="return Validate();" />
                        </div>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <!--// Portlet Section //-->
        </div>
        <uc3:footer ID="footer" runat="server" />

        <%--Used for CMS Modal (Dynamically added at Server Side)--%>
        <div id="divModal" runat="server"></div>

    </form>
    <script src="js/jquery.js" type="text/javascript"></script>
    <link href="css/jquery.mCustomScrollbar.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.mCustomScrollbar.concat.min.js" type="text/javascript"></script>
    <%--<script src="js/jquery.counterup.min.js" type="text/javascript"></script>--%>
    <script src="js/jquery.countup.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/waypoints.min.js"></script>
    <script src="js/districtdetails.js" type="text/javascript"></script>
    <script src="js/vticker-min.js" type="text/javascript"></script>
    <script src="js/html5lightbox.js" type="text/javascript"></script>
    <script>

        $(document).ready(function () {
            for (i = 0; i < districtdetails.length; i++) {
                $('#' + districtdetails[i].district_Id).data('district', districtdetails[i]);
                //  alert('#' + districtdetails[i].district_Id);

                $('#' + districtdetails[i].district_Id).click(function () {
                    window.open("http://gis.investodisha.org/landbank.aspx");
                });
            }
            jQuery(".html5lightbox").html5lightbox();
            hovertooltip();

            function hovertooltip() {
                $(function () {

                    $('.dist-block').hover(function (e) {

                        var did = $(this).data('district');
                        $('div#pop-up h3').text(did.district_name);
                        $('#categorya span').text(did.category_a);
                        $('#categoryb span').text(did.category_b);
                        $('div#pop-up').show();
                    }, function () {
                        $('div#pop-up h3').text('ODISHA');
                        $('#categorya span').text('24899.121');
                        $('#categoryb span').text('98857.474');
                    });

                });
            }

            $('.homelink').addClass('active');
            //$('.counter').counterUp({ delay: 10, time: 2000 });
            $('.counter').countUp();
            (function ($) {
                var windowWidth = $(window).width();

                $(".prosal-data").mCustomScrollbar({
                    setHeight: 270,
                    theme: "inset-2-dark"
                });



                $(".whats-newdata ul").mCustomScrollbar({
                    setHeight: 240,
                    theme: "inset-2-dark"
                });


            })(jQuery);
            $(function () {
                $('#news-container1').vTicker({
                    speed: 700,
                    pause: 4000,
                    animation: 'fade',
                    mousePause: false,
                    showItems: 1
                });

            });
        })
    </script>
    <script>
        function Validate() {
            debugger;
            if (document.getElementById("txtFirstName").value == "") {
                jAlert("First Name can not be left blank !");
                document.getElementById("txtFirstName").focus();
                return false;
            }
            if (document.getElementById("txtLastName").value == "") {
                jAlert("Last Name can not be left blank !");
                document.getElementById("txtLastName").focus();
                return false;
            }
            if (document.getElementById("txtEmail").value == "") {
                jAlert("Email ID can not be left blank !");
                document.getElementById("txtEmail").focus();
                return false;

            }
            if (document.getElementById("txtEmail").value != "") {

                if (!emailCheck()) {
                    document.getElementById('txtEmail').focus();
                    return false;
                }
            }
            function emailCheck() {
                debugger;
                var email = document.getElementById('txtEmail');
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!filter.test(email.value) || txtEmail.value == "") {
                    jAlert('Invalid Email Id !');
                    return false;
                }
                return true;
            }
            if (document.getElementById("txtMobileNumber").value == "") {
                jAlert("Mobile Number can not be left blank !");
                document.getElementById('txtMobileNumber').focus();
                return false;
            }
            var val = ($("txtMobileNumber").val());
            if (($("#txtMobileNumber").val()).substring(0, 1) == '0') {
                jAlert('Mobile Number should not be start with zero !');
                $("#txtMobileNumber").val('');
                $("#txtMobileNumber").focus();
                return false;
            }
            if (($("#txtMobileNumber").val().length < 10) && ($("#txtMobileNumber").val().length > 0)) { jAlert('Mobile No. can not be less then 10 characters !'); $("#txtMobileNumber").focus(); return false; }

            if (document.getElementById("txtSubject").value == "") {
                jAlert("Subject can not be left blank !");
                document.getElementById('txtSubject').focus();
                return false;
            }
            if (document.getElementById("txtFeedback").value == "") {
                jAlert("Feedback can not be left blank !");
                document.getElementById('txtFeedback').focus();
                return false;
            }
            return true;
        }

    </script>
    <style>
        .map_area_txt {
            text-align: center;
        }
    </style>
</body>
</html>
