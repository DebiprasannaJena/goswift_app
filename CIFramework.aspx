<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CIFramework.aspx.cs" Inherits="CIFramework" %>

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
            $('.service,.plCIFramework').addClass('active');
        });

    </script> <title>SWP(Single Window Portal)</title>
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
                    <h3>Central Inspection Framework</h3>
                    
<p>Odisha has under taken several inspection related reforms, primary one being the setting up of a Central Inspection Framework (CIF) which is also recognized as a best practice by the Government of India in BRAP 2016. The CIF has been formed to address the common complaints about ambiguity, duplication and overlapping mandates between inspection authorities, and perceived lack of co-operation and co-ordination. The CIF eliminates the process of multiple visits to the same enterprises by the inspectors and synchronizes various inspections by different regulatory authorities. </p>
<br />
<h5>The key functions of the CIF are as follows:</h5>
<ul class="list-inline">
  <li>Develop transparent checklists and procedures for inspections</li>
  <li>Ensure synchronized inspections of Factories &amp; Boilers, Labour Department and Odisha State Pollution Control Board (OSPCB)</li>
  <li>Establish the framework for risk based inspections</li>
  <li>Develop modalities for conducting surprise inspections</li>
  <li>Empanel third party inspectors, wherever applicable</li>
</ul>
<div class="text-left">
<a href="http://cicg.investodisha.org/iimsweb/Default.aspx" class="btn btn-info" target="_blank"> More Information <i class="fa fa-angle-right"></i></a>
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