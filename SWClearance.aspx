<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWClearance.aspx.cs" Inherits="SWClearance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.service,.plSWClearance').addClass('active');
        });

    </script>
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
                    <h3>Single Window Clearance</h3>
                    <p>Odisha government is dedicated to encourage Ease of Doing Business by enabling entrepreneurs to set up and operate their business in the State. The Government of Odisha has introduced the Single Window Portal which acts as is a single window facilitation mechanism for investors concerning various services required throughout the lifecycle of projects.</p>
<p>The portal provides a single point interface for investors to know about various State government Acts & Rules for applicable approvals/clearances, application submission, payment and tracking. <strong>Once approved, the user can obtain the approval or registration certificate online through the portal. Further, any other third party has an option to verify online about the authenticity of the approval or registration granted by each agency.</strong></p>
                </div>
            </div>
           <%-- <div class="col-sm-4">
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
