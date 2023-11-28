<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InspectionConstruction.aspx.cs"
    Inherits="InspectionConstruction" %>

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
        strong.step
        {
            color: #e30d2b;
            font-size: 15px;
            text-decoration: underline;
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
                        <div id="divid" runat="server">
                        </div>
                        <h3>
                            Inspection Procedure for obtaining Construction Permit</h3>
                        <%-- <strong>Procedures</strong></h4>--%>
                        <div>
                            <h5>
                                General Instructions</h5>
                            <h6>
                                For Building Other than Low Risk Buildings</h6>
                            <p>
                                <strong class="step">Step 1:</strong> On receipt of Common Application Form for
                                building operations, the concerned Verifying Officer will do the scrutiny of uploaded
                                documents for its correctness and completeness. After Scrutiny of the documents,
                                the Verifying Officer will refer the same to the concerned public agencies for obtaining
                                No-objection certificate before granting or refusing permission to the applicant.</p>
                            <p>
                                <strong class="step">Step 2:</strong> The concerned Verifying Officer of the Authority
                                will generate a site inspection notice specifying a date and time to the public
                                agencies for conducting a common Inspection Programme. Any Public Agency which needs
                                to conduct field visit and inspection for giving No-Objection Certificate, shall
                                conduct the same as part of such notified common inspection programme. The date
                                provided by concerned Verifying Officer of the Authority shall be ten days after
                                the receipt of Common Application Form by public agencies but it shall not exceed
                                twenty days of receipt.</p>
                            <p>
                                <strong class="step">Step 3:</strong> The concerned nodal officer of public agency
                                will be intimated with SMS and email for common Inspection programme. The common
                                inspection programme shall be conducted on the said date and time mentioned in the
                                site inspection notice.</p>
                            <p>
                                <strong class="step">Step 4:</strong> Inspection reports are to be uploaded by assigned
                                nodal officers of public agencies within 48 hours of conduct of site inspection.
                                In case, if any objection arises or further clarifications are required, the same
                                shall be mentioned in the report.</p>
                            <p>
                                <strong class="step">Step 5:</strong> After receipt of Inspection report, if the
                                information and document provided by the applicant has been complied and is to the
                                satisfaction of the nodal officer of the public agency, No objection certificate
                                shall be issued.</p>
                            <p>
                                <strong class="step">Step 6:</strong> The nodal officers of public agencies shall
                                upload the NOC/Recommendation within 24 hours of uploading the inspection report.
                                If the NOC/Recommendation is not provided within 3 days of site inspection, it shall
                                be deemed that NOC has been issued by Concerned Public Agency.</p>
                            <h6>
                                For Low Risk Buildings</h6>
                            <p>
                                After receipt of the common Application Form, Authority shall consider the planning
                                and building standard regulations without referring to the public agency. It shall
                                then issue the NOC/recommendation for construction permit.</p>
                            <h5>
                                Checklist of Documents</h5>
                            <p>
                                i)&nbsp; &nbsp; Building Plan Approval</p>
                            <p>
                                ii)&nbsp; &nbsp;Filled up online Common Application Form</p>
                            <p>
                                iii)&nbsp; &nbsp;Self-Signed Xerox copies of ownership Documents</p>
                            <p>
                                iv)&nbsp; &nbsp;Copy of certificate of registration of Architect/Engineers/Builder-Developer</p>
                            <p>
                                v)&nbsp; &nbsp;Affidavit in original</p>
                            <p>
                                vi)&nbsp; &nbsp;Scrutiny fee copy</p>
                            <p>
                                vii)&nbsp; &nbsp;Two Photographs of site.</p>
                        </div>
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
