<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NSWSMessage.aspx.cs" Inherits="NSWSMessage" %>

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
                            <li>NSWS</li>
                        </ul>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="content-form-section">
                    <div class="col-sm-12">
                        <div class="aboutcontent-sec">
                            <h3>National Single Window System (NSWS)</h3>
                            <p>The National Single Window System has access to over 100 Central level approvals and State Single Window Systems of 14 States/UTs with one user id and password. </p>
                            <br />
                            <div class="text-left">
                                <a href="https://www.nsws.gov.in/" class="btn btn-info" target="_blank" title="Click here to get more information on NSWS.">Visit Website <i class="fa fa-angle-right"></i></a>
                            </div>
                             
                        </div>
                    </div>                    
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>
        <uc3:footer ID="footer" runat="server" />


          

    </form>
</body>
</html>
