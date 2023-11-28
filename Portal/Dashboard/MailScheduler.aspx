<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="MailScheduler.aspx.cs" Inherits="Portal_Dashboard_MailScheduler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
<%--        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    View Weekly Mail Contents</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Incentive Policy</a></li><li><a>View OG</a></li></ul>
            </div>
        </div>--%>
        <div class="content">
            <div class="row">
                <%--<div style="padding-left: 280px;padding-right:5px;">--%>
                <%-- <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
        </asp:DropDownList>--%>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="Btn_Send_Mail" runat="server" Text="View Mail Content" OnClick="Btn_Send_Mail_Click"
                                CssClass="btn btn-primary" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <%--  <asp:Button ID="Btn_CC_BCC" runat="server" Text="Test CC BCC Mail" OnClick="Btn_CC_BCC_Click" />--%>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Lbl_Msg" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <table border="1" cellpadding="1" cellspacing="1" style="width: 100%; border-color: Maroon;">
                    <tr>
                        <td align="center">
                            <h4 style="color: Blue; font-weight: 700;">
                                Mail Body Content
                            </h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px;">
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
