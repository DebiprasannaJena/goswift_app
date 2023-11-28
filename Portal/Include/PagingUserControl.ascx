<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagingUserControl.ascx.cs" Inherits="includes_PagingUserControl" %>
<div class="pagingSection" id="pagingSection" runat="server">
<asp:LinkButton ID="lnkPrevious" OnClientClick="window.scrollTo(0,0)" runat="server" CssClass='page-numbers prev' Visible="false" OnClick="LinkButton_Click">Prev</asp:LinkButton>
<asp:LinkButton ID="lnkFirst" OnClientClick="window.scrollTo(0,0)" runat="server" CssClass='page-numbers' Visible="false"  OnClick="LinkButton_Click">1</asp:LinkButton>
<asp:Label runat="server" ID="lblFirstDots" CssClass="page-numbers prev" Visible="false"  Text="..."></asp:Label>
<asp:PlaceHolder ID="plhDynamicLink" runat="server"></asp:PlaceHolder>
<asp:Label runat="server" ID="lblSecondDots" Visible="false" CssClass="page-numbers prev" Text="..."></asp:Label>
<asp:LinkButton ID="lnkLast"  OnClientClick="window.scrollTo(0,0)" runat="server" CssClass='page-numbers' Visible="false" OnClick="LinkButton_Click">Last</asp:LinkButton>
<asp:LinkButton ID="lnkNext" OnClientClick="window.scrollTo(0,0)" runat="server" CssClass='page-numbers next' Visible="false" OnClick="LinkButton_Click">Next</asp:LinkButton>
 </div>