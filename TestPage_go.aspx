<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPage_go.aspx.cs" Inherits="TestPage_go" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <title></title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container wrapper">
        <div class="panel-body">
            <div class="row">
                <br />
                <%--<table width="100%">
                    <tr>
                        <td width="50%" valign="top">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td colspan="3">
                                        <h3 style="color: Green;">
                                            Test Payment Order (Without Restful Service)</h3>
                                        <br />
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
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="Btn_Payment_Service_Test" runat="server" Text="Test Payment Service"
                                            OnClick="Btn_Payment_Service_Test_Click" CssClass="btn btn-success" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="Lbl_Payment_Service_Response" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" valign="top">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td colspan="3">
                                        <h3 style="color: Red;">
                                            Test Payment Order (Restful Service)</h3>
                                        <br />
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
                                        <asp:TextBox ID="Txt_Order_No_REST" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="Btn_Pay_REST" runat="server" Text="Test Payment Service (Restful)"
                                            OnClick="Btn_Pay_REST_Click" CssClass="btn btn-danger" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="Lbl_Msg_Restful" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr style="border: 1px solid #15CCD9" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" valign="top">
                            <table width="80%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td colspan="3">
                                        <h3 style="color: Blue;">
                                            Test CRM Service (Restful)</h3>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        Unique SSO ID
                                    </td>
                                    <td width="2%">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt_SSO_Id" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="Btn_CRM_Service" runat="server" Text="Test CRM (Service)" OnClick="Btn_CRM_Service_Click"
                                            CssClass="btn btn-success" />
                                        <asp:Button ID="Btn_CRM_PEAL" runat="server" Text="Test CRM (PEAL)" OnClick="Btn_CRM_PEAL_Click"
                                            CssClass="btn btn-warning" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Label ID="Lbl_CRM_Response" runat="server"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr style="border: 1px solid #15CCD9" />
                            <br />
                        </td>
                    </tr>
                </table>--%>
            </div>
        </div>
    </div>
       <%-- <asp:Button ID="Button1" runat="server" Text="Button" style="height: 26px" />--%>


    </form>
</body>
</html>
