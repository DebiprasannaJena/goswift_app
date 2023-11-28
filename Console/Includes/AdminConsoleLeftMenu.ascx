<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminConsoleLeftMenu.ascx.cs"
    Inherits="AdminConsoleLeftMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 
<table width="100%" cellpadding="0" cellspacing="0" border="0" class="leftmenutreeview_css">
    <tr>
        <td colspan="3" rowspan="3" align="left" style="height: auto" valign="top" width="100%">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:TreeView ID="TreeViewMenu" CssClass="tree" NodeWrap="true" runat="server" NodeIndent="9"
                        Target="_top" ShowLines="True" Font-Bold="False" OnSelectedNodeChanged="TreeViewMenu_SelectedNodeChanged"
                        OnTreeNodeExpanded="TreeViewMenu_TreeNodeExpanded">
                        <RootNodeStyle CssClass="rootNode" Width="200px" />
                        <ParentNodeStyle CssClass="parentNode" Font-Bold="True" Width="165px" />
                        <LeafNodeStyle CssClass="leafNode" />
                        <SelectedNodeStyle CssClass="selectedNode" />
                    </asp:TreeView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
