<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListofInspectingAuthorities.aspx.cs" Inherits="APAA" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html>
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet"  />
    <script >
        $(document).ready(function () {
            $('.service,.plApaa').addClass('active');
        });

    </script>
     <title>SWP(Single Window Portal)</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="navigatorheader-div aboutheadernav">
            <div class="col-sm-12">
                <ul class="breadcrumb">
                    <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                    <li>Services</li><li><a href="Department.aspx?deptid=10">Directorate of Factories & Boilers</a></li></ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="content-form-section">
            <div class="col-sm-12">
                <div class="aboutcontent-sec">
                    <h3>Inspecting Authorities</h3>
                    <p>The following are the Inspecting Agencies authorized by the Central Boilers Board, Government of India under Indian Boiler Regulations, to carry out inspections of Boilers in the State of Odisha.</p>
                

                <table class="table table-bordered">
                <tr><th width="40px">Sl #</th><th>Name of the Inspecting Authority</th><th>Address</th></tr>
                <tr><td>1</td><td>Director of Factories & Boilers, Odisha</td><td>Directorate Of Factories & Boilers
Unit-4, Near Ram Mandir, 
Bhubaneswar – 751002, Odisha</td></tr>
                <tr><td>2</td><td>M/s Lloyd’s Register Area</td><td>63-64, Kalpataru Square, 6th Floor
Kondivita Lane, Off. Andheri-Kurla Road
Andheri (East), Mumbai – 400059, Maharashtra</td></tr>
                <tr><td>3</td><td>M/s Bureau Veritas (India) Pvt. Ltd.</td><td>Marwah Center, 6th Floor, 
Opp. Ansa Industrial Estate, K. Marwah Marg
Off. Saki-Vihar Road, Andheri (East),
Mumbai – 400072, Maharashtra</td></tr>
                <tr><td>4</td><td>M/s ABS Industrial Verification (India) Pvt. Limited</td><td>10th Floor, Lakhani’s Centrium,
Sector 15, Plot No. 27, CBD Belapur (East),
Navi Mumbai – 400614, Maharashtra</td></tr>
                <tr><td>5</td><td>M/s TUV India Pvt. Limited (TUV Nord Group)</td><td>801, Raheja Plaza-1, L.B.S. Marg,
Ghatkopar (West), Mumbai – 400086, Maharashtra</td></tr>
                <tr><td>6</td><td>M/s Intertek India Pvt. Limited</td><td>E-20, Block – B1, Mohan Co-operative Industrial Estate, Mathura Road,
New Delhi – 100044</td></tr>
                <tr><td>7</td><td>M/s. TUV SUD South Asia Pvt. Ltd.</td><td>TUV SUD House,
Off Saki Vihar Road, Saki Naka, Andheri (East)
Mumbai – 40007, Maharashtra</td></tr>


                </table>
<div class="pull-right">
<a href="Download/RecognisedInspectingAuthorities31032017.pdf" class="btn btn-primary" target="_blank" > List of all Inspecting Authorities </a>
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
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>