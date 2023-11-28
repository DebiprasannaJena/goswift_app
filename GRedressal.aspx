<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GRedressal.aspx.cs" Inherits="GRedressal" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html>
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.service,.plGRedressal').addClass('active');
        });

    </script>
     <title>SWP(Single Window Portal)</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:header ID="header" runat="server" />
            <div class="container wrapper">
        <div class="navigatorheader-div aboutheadernav">
            <div class="col-sm-12">
                <ul class="breadcrumb">
                    <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                    <li>Services</li></ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="content-form-section">
            <div class="col-sm-12">
                <div class="aboutcontent-sec">
                    <h3>Grievance Redressal</h3>
                    <p>There is a structured and institutionalized mechanism for resolution of any investor grievances. As a first port of call, the investors can approach <strong>State Level Facilitation Cell (SLFC)</strong>. SLFC has representation of nodal officers from various concerned Departments and meets every week to resolve investor grievances. Furthermore, in order to ensure redressal of issues on priority and in a time bound manner, an online <strong>State Project Monitoring Group (SPMG)</strong> has been set up under the chairpersonship of Chief Secretary to resolve queries of projects with investments greater than Rs. 50 crore. Investors can upload grievance concerning any Government department on the online portal. The SPMG meets every month.  Similarly, the issues of project proponent under MSME sector with investment upto Rs 50 crore are resolved through a committee under the Chairmanship of Secretary, MSME. </p>
<p>The grievances/issues of project proponents are addressed within a time limit of maximum 45 days from the date of receipt.</p>

<div class="text-left">
<a href="https://esuvidha.gov.in/" class="btn btn-info" target="_blank"> More Information <i class="fa fa-angle-right"></i></a>
</div>
                </div>
            </div>
          <%--  <div class="col-sm-4">
                <uc4:rightpanel ID="rightpanel" runat="server" />
            </div>--%>
            <div class="clearfix">
            </div>
        </div>
    </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>