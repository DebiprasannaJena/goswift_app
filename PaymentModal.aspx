<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentModal.aspx.cs" Inherits="PaymentModal" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
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
    <style type="text/css">
        .payment-sec
        {
            padding: 35px 10px;
            border: 1px solid #eae8e8;
            background: #f7f7f7;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    <div class="modal-content modal-primary ">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">
                &times;</button>
            <h4 class="modal-title text-center">
                Payment Details</h4>
        </div>
        <div class="modal-body">
            <div class="old-querydetails">
                <div class="form-group" id="div2">
                </div>
                                <div class="row">
                    <div class="col-sm-12 text-center">
                        <p>
                            You are proceeding to pay</p>
                        <h4>
                            <asp:Label ID="lblDesc" runat="server" ForeColor="Red" CssClass="clsDesc" Text=""></asp:Label></h4>
                        <p>
                            for the request No.</p>
                        <h4>
                            <asp:Label ID="lblRequestNo" runat="server" Text=""></asp:Label></h4>
                        <p>
                            of amount</p>
                        <h4>
                            Rs:<i class="fa fa-inr"></i>
                            <asp:Label ID="lblAmount" runat="server" CssClass="clsAmount"></asp:Label>
                            /-</h4>
                        <asp:Button ID="btnModalSubmit" runat="server" Text="Pay Now" 
                            CssClass="btn btn-success btn-sm clssave" onclick="btnModalSubmit_Click"
                             />
                        <%--<asp:Button ID="Button2" runat="server" Text="Payment Confirmation" PostBackUrl="PaymentConfirmation.aspx"
                            CssClass="btn btn-success btn-sm" />
                        <asp:Button ID="Button3" runat="server" Text="Cancel" PostBackUrl="Proposals.aspx"
                            data-toggle="modal" CssClass="btn btn-success btn-sm" />--%>
                        <br />
                        <br />
                        <p class="text-red ">
                           <strong> N.B: </strong>

Please do not close the browser window of payment gateway until the process is fully complete. Wait for the time duration as guided in the payment gateway portal.
All the payments made will be realised within 3 working days.
1. Your payment is being submitted. Please do not close this window or click the Back button on your browser until the process is fully complete. Wait for the time duration as guided in the payment gateway portal.
2. All the payments made will be realized within 3 working days

</p>
                    </div>
                </div>
                </div>
            </div>
        </div>
        <%-- <div class="modal-footer">
                                                                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                                                                Close</button>
                                                                        </div>--%>
    </div>
   
    </form>
</body>
</html>
