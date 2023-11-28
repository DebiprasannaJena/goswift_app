<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestSMS.aspx.cs" Inherits="TestSMS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td colspan="3">GOSWIFT Message Testing
                </td>
            </tr>
            <tr>
                <td width="10%">Mobile No
                </td>
                <td width="2%">:
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Height="24px" Width="211px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>SMS Content
                </td>
                <td width="2%">:
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Height="67px" TextMode="MultiLine" Width="215px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Send SMS" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <br />
        <table width="100%">
            <tr>
                <td width="10%">Main URL
                </td>
                <td width="2%">:
                </td>
                <td>
                    <asp:TextBox ID="Txt_Main_Url" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>User Name
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="Txt_User_Name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>PIN
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="Txt_PIN" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Message
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="Txt_Msg" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Mobile Number
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="Txt_Mobile_No" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Signature
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="Txt_Signature" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>DLT Entity Id
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="Txt_DLT_Entity_Id" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>DLT Template Id
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="Txt_DLT_Template_Id" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="Btn_Msg_Configure" runat="server" Text="SUBMIT" OnClick="Btn_Msg_Configure_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Label ID="Lbl_Msg_Configure" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Lbl_Response_1" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <br />
        <table width="100%">
            <tr>
                <td width="10%">Full URL
                </td>
                <td width="2%">:
                </td>
                <td>
                    <asp:TextBox ID="Txt_Full_Url" runat="server" TextMode="MultiLine" Width="70%" Height="50px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="Btn_Msg_2" runat="server" Text="SUBMIT" OnClick="Btn_Msg_2_Click" />
                    <asp:Label ID="Lbl_Response_2" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>

        <asp:DropDownList ID="DrpDwn_Year" runat="server"></asp:DropDownList>


        <asp:DropDownList ID="DrpDwn_Vehicle_Type" runat="server">
            <asp:ListItem Value="1">Two Wheeler</asp:ListItem>
            <asp:ListItem Value="2">Three Wheeler</asp:ListItem>
            <asp:ListItem Value="3">Four Wheeler</asp:ListItem>
        </asp:DropDownList>

    </form>
</body>
</html>
