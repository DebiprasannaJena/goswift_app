<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceDetails.aspx.cs" Inherits="ServiceDetails" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $('.department,.plLesi').addClass('active');
            $('#printbtn').click(function () {
                window.print();
            })
        });

    </script>
    <style>
        .deptlist li a span
        {
            float: none !important;
            margin-right: 12px;
        }
        .aboutcontent-sec h2
        {
            font-size: 1.2em;
            color: #3c3c3c;
            margin: 0;
            padding: .3em 0em;
            border-bottom: 2px solid #c09e46;
        }
        .deptlist
        {
            min-height: 420px !important;
            overflow: hidden;
            overflow-y: scroll;
        }
        .rightnews ul li.active a
        {
            color: #f00;
        }
        .backbtn
        {
            padding: 4px 6px;
            color: #cd1c24;
            border: 1px solid #cd1c24;
            border-radius: 2px;
        }
        .backbtn:hover, .backbtn:focus
        {
            color: #fff;
            background: #cd1c24;
        }
        .links
        {
            position: fixed;
            bottom: 15px;
            right: 60px;
        }
        .links .btn
        {
            background-color: #cd1c24;
            border-color: #cd1c24;
            color: #fff;
            margin-left: 5px;
            font-size: 14px;
            padding: 4px 10px;
            display: inline-block;
        }
        .links .btn:hover, .links .btn:focus
        {
            background: #af0b13;
            color: #fff;
        }
        .applicationdtls
        {
            padding: 4px 30px;
            margin-top: 5px;
            display: block;
            background: #cd1c24;
            text-align: center;
            margin-left: 11px;
            color: #fff;
            float: right;
            margin-top: 5px;
            right: 14px;
        }
        .applicationdtls:hover, .applicationdtls:focus
        {
            text-decoration: none;
            color: #fff;
        }
        .applicationdtls span
        {
            font-size: 24px;
            line-height: 28px;
            color: #f9cd5d;
        }
        .applicationdtls .fa
        {
            color: #f9cd5d;
            margin-right: 8px;
        }
        a.link
        {
            font-size: 15px;
            display: block;
            margin: 7px 0px;
        }
        a.link:hover
        {
            text-decoration: none;
        }
        
        @media print
        {
            .rightnews, .aboutheadernav, .links, .applicationdtls
            {
                display: none;
            }
            .aboutcontent-sec table
            {
                width: 100% !important;
            }
            .aboutcontent-sec h3
            {
                text-align: left;
            }
            .aboutcontent-sec h3 span
            {
                width: 100%;
                text-align: left;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <div class="navigatorheader-div aboutheadernav">
                <div class="col-sm-10">
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                        <%--  <li>Labour &amp; ESI</li></ul>--%>
                        <li runat="server" id="lihid">
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </li>
                    </ul>
                </div>
                <div class="col-sm-2 text-right">
                    <a href="javascript:void(0);" class="backbtn pull-right " id="printbtn" style="margin-left: 5px;"
                        title="Print" data-toggle="tooltip"><i class="fa fa-print"></i></a><a href="javascript:history.back()"
                            class="backbtn pull-right " title="Back" data-toggle="tooltip"><i class="fa fa-arrow-left">
                            </i></a>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="content-form-section">
                <div class="col-sm-4">
                    <div class="rightpanel laboutesi">
                        <div class="rightnews bglight-gray deptservice-list ">
                            <h2>
                                Services</h2>
                            <ul id="oldeptid" class="deptlist" runat="server">
                                <%--  <li class="plLesi"><a href="LabourESI.aspx" title="Labour & ESI">Labour &amp; ESI</a></li>
			<li class="plDIndustries"><a href="http://industries.odisha.gov.in/" target="_blank"
				title="Department of Industries">Department of Industries</a></li>
			<li class="plOPCBoard"><a href="http://ospcboard.org/" target="_blank" title="Odisha Pollution Control Board">
				Odisha Pollution Control Board</a></li>
			<li class="plCCTax"><a href="https://odishatax.gov.in/" target="_blank" title="Commissionerate Of Commercial Tax">
				Commissionerate of Commercial Tax</a></li>--%>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-sm-8">
                    <div class="aboutcontent-sec">
                        <a class="applicationdtls"><span><i class="fa fa-line-chart"></i>
                            <asp:Literal ID="lblCount" runat="server"></asp:Literal>
                        </span>
                            <br />
                            Total Applications</a>
                        <h3 runat="server" id="hservid">
                        </h3>
                        <%-- <strong>Procedures</strong></h4>--%>
                        <div id="divid" runat="server">
                        </div>
                        <%--<asp:HyperLink ID="HyperLink1" runat="server">Inspection for Construction Permit</asp:HyperLink>
                        <br />
                        <asp:HyperLink ID="HyperLink2" runat="server">Inspection for Occupancy Certificate</asp:HyperLink>--%>
                        <%--  <a href="#" title="Inspection for Construction Permit" runat="server" id="lnk1" class="link"></a>--%>
                        <%--  <a href="OccupancyCertificate.aspx" title="Inspection for Occupancy Certificate" class="link"></a>--%>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
        <div class="links">
            <asp:HyperLink class="btn pull-right " ID="HyprLnk" Target="_blank" runat="server"
                title="Download User Manual"><i class="fa fa-download"></i>&nbsp;User Manual</asp:HyperLink>
            <asp:Button ID="btnapply" class="btn pull-right" runat="server" Text="Apply Now"
                OnClick="btnapply_Click" />
            <div class="clearfix">
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
