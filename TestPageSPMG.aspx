<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPageSPMG.aspx.cs" Inherits="TestPageSPMG" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <title></title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function hh() {

            var str = "WBLLO WORLD"; //// Pass textbox value here
            var res1 = str.charAt(0);
            var res2 = str.charAt(1);

            alert(res1 + "-----" + res2);

            if (res1 + res2 != 'WB') {
                alert("Invalid");
            }
            else {

                alert("Valid");
            }

            if (str.startsWith("WB")) {
                alert("Valid");
            }
            else {
                alert("Invalid");
            }

        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container wrapper">
        <div class="panel-body">
            <div class="row">
                <table width="50%" cellpadding="0" cellspacing="0" border="0" style="display: none;">
                    <tr>
                        <td colspan="3">
                            <h3>
                                Test Payment Order</h3>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            Order No
                        </td>
                        <td width="2%">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_Order_No" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Challan Amount
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_Challan_Amount" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <%-- <asp:Button ID="Btn_Payment_Service_Test" runat="server" Text="Test Payment Service"
                        OnClick="Btn_Payment_Service_Test_Click" CssClass="btn btn-success" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Lbl_Payment_Service_Response" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <hr />
                <br />
                <div class="form-group" style="display: block;">
                    <div class="row">
                        <label for="Email" class="col-sm-2">
                            AIM Integration Testing
                        </label>
                        <div class="col-sm-3">
                            <asp:Button ID="Button1" runat="server" Text="AIM Document Scheduler" OnClick="Button1_Click"
                                CssClass="btn btn-success" />
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="Btn_AIM_MIS_Report" runat="server" Text="AIM MIS Report Test" OnClick="Btn_AIM_MIS_Report_Click"
                                CssClass="btn btn-success" />
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="Btn_AIM_Test" runat="server" Text="AIM First lave Approve Insert Record" OnClick="Btn_AIM_Test_Click"
                                CssClass="btn btn-success" />
                             <asp:Label ID="Lbk_Msg_AIM" runat="server"></asp:Label>
                        </div>
                    </div>
                    <br />
                    
                    <br />
                    <div class="row">
                        <label for="Email" class="col-sm-2">
                            AIM Integration Testing
                        </label>
                        <div class="col-sm-3">
                            <asp:Button ID="Btn_AIM_Test_Update" runat="server" Text="AIM Second lave Approve Insert Record" OnClick="Btn_AIM_Test_Update_Click"
                                CssClass="btn btn-success" />
                             <asp:Label ID="Lbk_Msg_AIM_update" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="Btn_AIM_Test_CheckEIN" runat="server" Text="Check EIN Number Exist or New by Unique Key" OnClick="Btn_AIM_Test_CheckEIN_Click"
                                CssClass="btn btn-success" />
                             <asp:Label ID="Lbk_Msg_CheckEIN" runat="server"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div  runat="server" id="DIVNO" visible="false" >
                    <br />
                    <div class="row">
                        <label for="Email" class="col-sm-2">
                            Count Incentive
                        </label>
                        <div class="col-sm-3">
                            <asp:GridView ID="GrdCountIncentive" runat="server"></asp:GridView>
                        </div>
                    </div>
                    <br />
                    
                    <br />
                    <div class="row">
                        <label for="Email" class="col-sm-2">
                            Status Wise Incentive Details status2
                        </label>
                        <div class="col-sm-3">
                            <asp:GridView ID="GrdStatusWiseIncentive_2" runat="server"></asp:GridView>
                        </div>
                    </div>
                    <br />
                    
                    <br />
                    <div class="row">
                        <label for="Email" class="col-sm-2">
                           DIC wise count Incentive 
                        </label>
                        <div class="col-sm-3">
                            <asp:GridView ID="GrdDICWisecountIncentive" runat="server"></asp:GridView>
                        </div>
                    </div>

                    <br />
                    
                    <br />
                    <div class="row">
                        <label for="Email" class="col-sm-2">
                            Status Wise Incentive Details status1
                        </label>
                        <div class="col-sm-3">
                            <asp:GridView ID="GrdStatusWiseIncentive_1" runat="server"></asp:GridView>
                        </div>
                    </div>

                    <br />
                <hr />
                <br />
                <div class="form-group">
                    <div class="row">
                        <label for="Email" class="col-sm-2">
                            Click to Run CMS Scheduler
                        </label>
                        <div class="col-sm-3">
                            <asp:Button ID="Btn_CMS" runat="server" Text="CMS ORTPSA Posting" OnClick="Btn_CMS_Click"
                                CssClass="btn btn-warning" OnClientClick="return confirm('Are You Sure ?')" />
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="Btn_CMS_1st_Time_Push" runat="server" Text="CMS ORTPSA Posting(For 1st Time)"
                                OnClick="Btn_CMS_1st_Time_Push_Click" CssClass="btn btn-success" OnClientClick="return confirm('Are You Sure ?')" />
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                    </div>
                    
                </div>
                
            </div>
            <div>
               <span>Application Number</span> <asp:TextBox runat="server" ID="TxtApplicationno"></asp:TextBox>
                 <span>Time Out</span> <asp:TextBox runat="server" ID="TxtTimeout"></asp:TextBox>
                 <asp:Button class="button button-block" ID="btnSub" runat="server" Text="Login"
                    OnClick="btnSub_Click" />
                <asp:Label runat="server" ID="lblid"></asp:Label>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
