<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentThankYou1.aspx.cs"
    Inherits="PaymentThankYou" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="includes/Feedback.ascx" TagName="Feedback" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="container">
        <div class="registration-div">
            <div class="container">
                <div id="exTab1">
                    <div class="wizard">
                        <div class="wizard-inner margin-top15">
                            <div class="connecting-line">
                            </div>
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation" class="backactive"><a href="javasxript:void(0)" data-toggle="tab"
                                    aria-controls="Profile Creation" role="tab" title="Form Registration"><span class="round-tab">
                                        <i class="fa fa-file-text-o"></i></span><small>Form Registration</small> </a>
                                </li>
                                <li role="presentation" class="backactive"><a href="#step3" data-toggle="tab" aria-controls="Payment Details"
                                    role="tab" title="Payment Details"><span class="round-tab"><i class="fa fa-credit-card">
                                    </i></span><small>Payment Details</small> </a></li>
                                <li role="presentation" class="backactive"><a href="#complete" data-toggle="tab"
                                    aria-controls="Success" role="tab" title="Complete"><span class="round-tab"><i class="glyphicon glyphicon-ok">
                                    </i></span><small>Success</small> </a></li>
                            </ul>
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="form-body ">
                                    <div class="payment-sec">
                                        <div>
                                            <div class="col-sm-6 col-sm-offset-3">
                                                <h4 class="text-red text-center">
                                                    <img src="images/cancel-symbol-inside-a-circle.png" alt="Failure img" id="Img2" runat="server" /><br />
                                                    <img src="images/checked.png" alt="success img" id="Img1" runat="server" /><br />
                                                    <asp:Label ID="lblPaymentStatus" runat="server" Text="Label"></asp:Label></h4>
                                                <p class="text-center">
                                                    <asp:Label ID="lblReference" runat="server"></asp:Label></p>
                                                <div id="test" runat="server">
                                                    <table runat="server" id="tblAmount" class="table table-bordered">
                                                        <tr>
                                                            <th width="50%">
                                                                Challan Amount
                                                            </th>
                                                            <td>
                                                                <asp:Label ID="lblchallanAmt" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                Bank Transaction Id
                                                            </th>
                                                            <td>
                                                                <asp:Label ID="lblTrancId" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                Challan Ref Id
                                                            </th>
                                                            <td>
                                                                <asp:Label ID="lblchallanrefid" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="text-align: center">
                                                    <asp:Label ID="lblReferenceId" runat="server" ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div style="text-align: center">
                                            <br />
                                            <p>
                                                <asp:HiddenField ID="hdServiceId" runat="server" />
                                                <asp:HiddenField ID="hdApplicationUniqueID" runat="server" />
                                                <asp:Label ID="lblNote" runat="server"><span style="color:Red"><strong>Note:</strong> Your application shall be processed after receipt of the Original Stability Certificate by post to the Director,Factories & Boilers , Odisha , Bhubaneswar</span></asp:Label></p>
                                            <p>
                                                <asp:Label ID="lblNote35" runat="server"><span style="color:Red"><strong>Note:</strong> Your Application shall be processed after receipt of the factory drawings to the Director of Factories & Boiler, Odisha , Bhubaneswar</span></asp:Label>
                                            </p>
                                        </div>
                                        <div>
                                            <uc4:Feedback ID="Feedback1" runat="server" />
                                            <center>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="DepartmentClearance.aspx"
                                                    CssClass="btn btn-sent">Back to home</asp:HyperLink>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
