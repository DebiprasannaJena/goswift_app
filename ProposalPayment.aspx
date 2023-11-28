<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProposalPayment.aspx.cs"
    Inherits="ProposalPayment" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.menuproposal').addClass('active');


        });
  
 

    </script>
    <style>
        .payment-sec
        {
            padding: 35px 10px;
            border: 1px solid #eae8e8;
            background: #f7f7f7;
        }
    </style>
</head>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:investoemenu ID="ineste" runat="server" />
                    </div>
                    <div class="tab-content clearfix">
                        <div class="tab-pane active" id="1a">
                            <div class="form-sec">
                                <div class="form-header">
                                    <h2>
                                        Proceed for Payment</h2>
                                </div>
                                <div class="form-body ">
                                    <div class="payment-sec">
                                        <div class="col-sm-8 col-sm-offset-2">
                                            <div class="form-group ">
                                                <div class="row">
                                                    <div class="col-sm-12 text-center">
                                                        Payment No. :
                                                        <asp:Label ID="lblPaymentNo" runat="server" Text=""></asp:Label></div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12 text-center">
                                                        Amount. :
                                                        <asp:Label ID="lblpaymentAmount" runat="server" Text="Rs."></asp:Label></div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-12 text-center">
                                                        Your are Proceeding to pay above amount for
                                                        <b><asp:Label ID="lblDesc" runat="server" Text=""></asp:Label></b> head
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12 text-center">
                                                        <asp:Button ID="btmPay" CssClass="btn btn-success" runat="server" Text="Pay"
                                                            OnClick="btmPay_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
          <p class="text-red ">
                           <strong> N.B: </strong>

Please do not close the browser window of payment gateway until the process is fully complete. Wait for the time duration as guided in the payment gateway portal.
All the payments made will be realised within 3 working days.
1. Your payment is being submitted. Please do not close this window or click the Back button on your browser until the process is fully complete. Wait for the time duration as guided in the payment gateway portal.
2. All the payments made will be realized within 3 working days

</p>
                                        <div class="clearfix">
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
