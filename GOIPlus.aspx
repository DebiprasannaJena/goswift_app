<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GOIPlus.aspx.cs" Inherits="GOIPlus" %>

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
            $('.service,.plGOIplus').addClass('active');
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
                    <h3>GOiPLUS</h3>
<p>GO<span class="text-danger">i</span>PLUS (Govt. of Odisha Industrial portal for Land use and Services) is a GIS based industrial land use and infrastructure information system developed by the Government of Odisha.</p>
<p>The Government of Odisha has created a land bank of 1,00,000 acres for industrial use. GO iPLUS is a web enabled platform to display real time information with regards to all the industrial land available in the State.</p>
<br />
<h5>Salient Features of the <strong>GO<span class="text-danger">i</span>PLUS</strong> include:</h5>
<ul>
  <li>It provides  detailed information with regards to availability of industrial plots based on  location specific attributes in terms of connectivity, rail and road linkages  and other physical, health and educational infrastructure available in the  vicinity of the selected industrial land. </li>
  <li>It enables  prospective investors to identify suitable industrial land in Odisha from the  comfort of their offices. A prospective investor can define preferred  parameters such as the district, size of land required, facilities available in  the vicinity, etc. based on which the portal identifies and returns information  regarding the suitable and available land parcels in the State. </li>
  <li>It provides  information on zoning of the industrial land in terms of environmental  categories i.e. Green, Orange and Red to enable an investor decide on suitable  location for investment based on the proposed business activities.</li>

<li>It  enables a prospective investor to get detailed information about the key  attributes of existing industries operational in a particular cluster such as  sector of operation, products, capacity, employment, raw material linkages etc.</li> 
</ul>   

<div class="text-left">
<a href="http://gis.investodisha.org/" class="btn btn-info" target="_blank"> More Information <i class="fa fa-angle-right"></i></a>
</div>                  
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