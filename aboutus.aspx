<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aboutus.aspx.cs" Inherits="aboutus" %>

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

            $('.aboutlink').addClass('active');

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
                    <li>At A Glance</li></ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="content-form-section">
            <div class="col-sm-12">
                <div class="aboutcontent-sec">
                    <h3>
                        At A Glance</h3>
                    <div id="divabout" runat="server">
                    </div>
                    <%-- <p>Odisha is one of the fastest growing states of India and is a land of several opportunities. Odisha achieved GDP growth of 8.78% during 2014-15, 20% higher than India’s GDP growth rate, and is poised to grow at around 12% by year 2020 *.</p>
     <img src="images/g3.jpg"  class="img-left" alt="Demo image"/>
  <p>Odisha is the heartland of India’s mineral deposits, with mineral production of US$3.64 bn, more than the aggregate value of 21 other Indian states. It has around 50% of Aluminium smelting capacity and around 20% of India’s steel making capacity.</p>
   
   <p>Odisha has emerged as the educational hub of East India and houses premier institutes such as IIT, IIM, NISER, IISER, IIIT, XIMB, Institute of Life Sciences, Institute of Minerals and Materials Technology etc. This ensures adequate availability of a large pool of technical graduates. Around 1,26,000 students graduate every year from the 773 technical training institutions in the state. A recent National Employability Report rates engineering graduates from Odisha as the 2nd most employable engineers in the country.</p>
     
   <p>Odisha is a pioneer in the Single Window clearance system. The Orissa Industries (Facilitation) Act 2004 was one of the first legislations for single window clearance system in the country.</p>
    <p>Bhubaneswar, the capital city of Odisha, is among the most business-friendly destinations. It has been ranked by the World Bank, as the 3rd best city in India to do business. It was recently ranked as No.1 in the Smart City challenge, from amongst 97 cities across India.</p>
    
    <ul>
<li>Odisha has launched a new ‘Invest Odisha’</li>
<li>Odisha has launched a new ‘Invest Odisha’</li>
<li>Odisha has launched a new ‘Invest Odisha’</li>
<li>Odisha has launched a new ‘Invest Odisha’</li>
<li><a href="#">Odisha has launched a new ‘Invest Odisha’</a></li>
</ul>
                    --%>
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
