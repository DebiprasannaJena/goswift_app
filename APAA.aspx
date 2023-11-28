<%@ Page Language="C#" AutoEventWireup="true" CodeFile="APAA.aspx.cs" Inherits="APAA" %>

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
            $('.service,.plApaa').addClass('active');
        });
    </script>
     <title>SWP(Single Window Portal)</title>
</head>
<body>
    <form id="form1" runat="server">
    <div >
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
                   <h3>Government of Odisha IDCO Post Allotment Services (GO iPAS)</h3>
                    <p>The State has implemented Government of Odisha IDCO Post Allotment Services (GO iPAS) for smooth management of existing MSME business units associated with IDCO. This portal has been developed to facilitate online registrations, applications for any post allotment matters, online payments, application tracking and processing activities. GO iPAS enables to download the sanctioned letters, removing physical interface between the units and IDCO, thus reducing the burden on both.</p>
                
<div class="text-left">
<a href="https://portal.idco.in/EntrepreneurLogin.aspx" class="btn btn-info" target="_blank"> More Information <i class="fa fa-angle-right"></i></a>
</div>                 
                </div>
            </div>
            <%--<div class="col-sm-4">
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