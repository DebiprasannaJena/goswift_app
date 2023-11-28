<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreasuryResponseold.aspx.cs" Inherits="TreasuryResponseold" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
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
                                      <div >
                                      <div class="col-sm-6 col-sm-offset-3">

                                       <h4 class="text-red text-center">
                                       <img src="images/cancel-symbol-inside-a-circle.png" alt="Failure img" id="Img2" runat="server"/><br />
                                       
                                       <img src="images/checked.png" alt="success img" id="Img1" runat="server"/><br />
                                           <asp:Label ID="lblPaymentStatus" runat="server" Text="Label"></asp:Label></h4>
                                      <table class="table table-bordered">
                                      <tr><th width="50%">Challan Amount</th><td><asp:Label ID="lblchallanAmt" runat="server"
            Text="Label"></asp:Label></td></tr>

            <tr><th>Bank Transaction Id</th><td><asp:Label ID="lblTrancId" runat="server" Text="Label"></asp:Label></td></tr>
            <tr><th>Challan Ref Id</th><td><asp:Label ID="lblchallanrefid" runat="server" Text="Label"></asp:Label></td></tr>
                                      </table>

    

    </div>
    
   
    
    </div>
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
