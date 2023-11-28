<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentConfirmation.aspx.cs"
    Inherits="PaymentConfirmation" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.menuproposal').addClass('active');
            $("#BtnSave").click(function (e) {

                if ($('#txtPaymentNo').val() == "") {
                    alert('Payment No Can not be left blank!');
                    $('#txtPaymentNo').focus();
                    return false;

                }
                if ($('#txtChallanAmt').val() == "") {
                    alert('Challan Amount Can not be left blank!');
                    $('#txtChallanAmt').focus();
                    return false;
                   
                }
                if ($('#TxtTransactionID').val() == "") {
                    alert('Bank Transaction Id Can not be left blank!');
                    $('#TxtTransactionID').focus();
                    return false;
                   
                }
                if ($('#txtChallanRefNo').val() == "") {
                    alert('Challan Ref. Id Can not be left blank!');
                    $('#txtChallanRefNo').focus();
                    return false;
                    
                }
            });

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
                                <div class="form-body ">
                                    <div class="payment-sec">
                                        <div>
                                            <div class="col-sm-6 col-sm-offset-3">
                                                <table class="table table-bordered">
                                                <tr>
                                                        <th width="50%">
                                                            Payment No
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtPaymentNo" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th width="50%">
                                                            Challan Amount
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtChallanAmt" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            Bank Transaction Id
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="TxtTransactionID" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            Challan Ref Id
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtChallanRefNo" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                        </th>
                                                        <td>
                                                            <asp:Button ID="BtnSave" runat="server" Text="Submit" OnClick="BtnSave_Click" CssClass="btn btn-success btn-sm clssave" />
                                                        </td>
                                                    </tr>
                                                </table>
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
    </form>
</body>
</html>
