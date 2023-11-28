<%@ Control Language="C#" AutoEventWireup="true" Inherits="Admin_PopulateHierarchy"
    CodeBehind="PopulateHierarchy.ascx.cs" %>
<style type="text/css">
    .tdStyle
    {
        width: 145px;
        float: left;
    }
</style>
<table border="0" cellpadding="0" cellspacing="0">
    <tr id="tr1" runat="server">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdStyle">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="drpLocation" runat="server" Width="133px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <span style="color: red">*</span>
                        <asp:HiddenField ID="hidID0" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr2" runat="server">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdStyle">
                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="drplayer1" runat="server" Width="133px" Height="16px">
                        </asp:DropDownList>
                        <span style="color: red">* </span>
                        <asp:HiddenField ID="hidID1" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr3" runat="server">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdStyle">
                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="drplayer2" runat="server" Width="133px">
                        </asp:DropDownList>
                        <span style="color: red">* </span>
                        <asp:HiddenField ID="hidID2" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr4" runat="server">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdStyle">
                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="drplayer3" runat="server" Width="133px">
                        </asp:DropDownList>
                        <span style="color: red">* </span>
                        <asp:HiddenField ID="hidID3" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr5" runat="server">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdStyle">
                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="drplayer4" runat="server" Width="133px">
                        </asp:DropDownList>
                        <span style="color: red">* </span>
                        <asp:HiddenField ID="hidID4" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr6" runat="server">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdStyle">
                        <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="drplayer5" runat="server" Width="133px">
                        </asp:DropDownList>
                        <span style="color: red">* </span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr7" runat="server">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdStyle">
                        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="drplayer6" runat="server" Width="133px">
                        </asp:DropDownList>
                        <span style="color: red">* </span>
                        <asp:HiddenField ID="hidID5" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr8" runat="server">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdStyle">
                        <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="drplayer7" runat="server" Width="133px">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <span style="color: red">* </span>
                        <asp:HiddenField ID="hidID6" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr9" runat="server">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdStyle">
                        <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="drplayer8" runat="server" Width="133px">
                        </asp:DropDownList>
                        <span style="color: red">* </span>
                        <asp:HiddenField ID="hidID7" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr10" runat="server">
        <td>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdStyle">
                        <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="drplayer9" runat="server" Width="133px">
                        </asp:DropDownList>
                        <span style="color: red">* </span>
                        <asp:HiddenField ID="hidID8" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
