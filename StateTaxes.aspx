<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StateTaxes.aspx.cs" Inherits="UserManual" %>

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
            $('.dbodisha,.LIstatetaxs').addClass('active');
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="navigatorheader-div aboutheadernav">
            <div class="col-sm-12">
                <ul class="breadcrumb">
                    <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                    <li>List of State Taxes</li>
                </ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="content-form-section">
            <div class="col-sm-12">
                <div class="aboutcontent-sec">
                    <h3>
                        List of State Taxes</h3>
                    <table class="table table-bordered table-striped">
                        <tbody>
                            <tr>
                                <th width="35">
                                    Sl#
                                </th>
                                <th>
                                    Name
                                </th>
                                <th width="100">
                                    Download
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    1
                                </td>
                                <td>
                                    State Excise
                                </td>
                                <td class="text-center">
                                    <a href="Document/Excise_Duty_Levies_Fees.pdf" title="download" target="_blank"><i
                                        class="fa fa-download"></i></a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    2
                                </td>
                                <td>
                                    Profession Tax
                                </td>
                                <td class="text-center">
                                    <a href="Document/Profession.pdf" title="download" target="_blank"><i class="fa fa-download">
                                    </i></a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    3
                                </td>
                                <td>
                                    Receipts Under Motor Vehicles Taxation Act
                                </td>
                                <td class="text-center">
                                    <a href="Document/Motor_Vehicle_Taxes.pdf" title="download" target="_blank"><i class="fa fa-download">
                                    </i></a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    4
                                </td>
                                <td>
                                    Stamps and Registration Fees
                                </td>
                                <td class="text-center">
                                    <a href="Document/Stamp_Duties.pdf" title="download" target="_blank"><i class="fa fa-download">
                                    </i></a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    5
                                </td>
                                <td>
                                    Taxes and Duties on Electricity
                                </td>
                                <td class="text-center">
                                    <a href="Document/Electricity_Duty.pdf" title="download" target="_blank"><i class="fa fa-download">
                                    </i></a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    6
                                </td>
                                <td>
                                    Levies at the Municipal Level
                                </td>
                                <td class="text-center">
                                    <a href="https://www.ulbodisha.gov.in/or/emun/others" title="download" target="_blank">
                                        <i class="fa fa-download"></i></a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <%--  <div class="col-sm-4">
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
