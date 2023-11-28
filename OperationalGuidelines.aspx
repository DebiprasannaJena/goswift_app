<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperationalGuidelines.aspx.cs" Inherits="OperationalGuidelines" %>

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
            $('.incentives,.plOGuidelines').addClass('active');
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
              <li>Operational Guidelines</li>
            </ul>
          </div>
          <div class="clearfix"> </div>
        </div>
        <div class="content-form-section">
          <div class="col-sm-12">
            <div class="aboutcontent-sec">
              <h3> Operational Guidelines</h3>
              <table class="table table-bordered table-striped">
  <tbody>
    <tr>
      <th width="35">Sl#</th>
      <th>Subject</th>
      <th width="100">Download</th>
    </tr>
<tr>
  <td>1</td>
  <td>Rice Technology Park, Bhadrak</td>
  <td class="text-center"><a href="http://investodisha.org/MIO/download/AF-125.pdf" title="download" target="_blank"><i class="fa fa-download"></i></a></td>
</tr>
<tr>
                  <td>2</td>
                  <td>Sea Food Park at Deras</td>
                  <td class="text-center"><a href="http://investodisha.org/MIO/download/AF-126.pdf" title="download" target="_blank"><i class="fa fa-download"></i></a></td>
                </tr>
<tr>
                  <td>3</td>
                  <td>MITS Food Park at Rayagada</td>
                  <td class="text-center"><a href="http://investodisha.org/MIO/download/AF-127.pdf" title="download" target="_blank"><i class="fa fa-download"></i></a></td>
                </tr>
<tr>
                  <td>4</td>
                  <td>Development of Mega and Medium Food Park - Ganjam</td>
                  <td class="text-center"><a href="http://investodisha.org/MIO/download/AF-128.pdf" title="download" target="_blank"><i class="fa fa-download"></i></a></td>
                </tr>
<tr>
                  <td>5</td>
                  <td>Development of Mega and Medium Food Park - Kalahandi</td>
                  <td class="text-center"><a href="http://investodisha.org/MIO/download/AF-129.pdf" title="download" target="_blank"><i class="fa fa-download"></i></a></td>
                </tr>
  </tbody>
</table>

            </div>
          </div>
        <%--  <div class="col-sm-4">
            <uc4:rightpanel ID="rightpanel" runat="server" />
          </div>--%>
          <div class="clearfix"> </div>
        </div>
      </div>
      <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>