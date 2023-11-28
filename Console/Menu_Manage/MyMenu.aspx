<%@ Page Language="C#" AutoEventWireup="true" Inherits="MyMenu" CodeBehind="MyMenu.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Menu</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            background: url(../images/LeftBG.jpg) repeat-y top right;
        }
        .tree
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #333;
            font-size: 12px;
        }
        .tree td div
        {
            height: 100% !important;
        }
        .rootNode
        {
            font-size: 12px;
            padding: 5px 0px;
            margin: 0px;
            font-weight: bold;
            color: #333;
        }
        .parentNode
        {
            font-size: 12px;
            padding-right: 5px;
            margin-right: 10px;
            margin-bottom: 2px;
            font-weight: bold;
            background: #f3f9fe url(../images/NavigationBG.jpg) repeat-x left bottom;
            color: #000;
            border-bottom: #c3e3f3 solid 1px;
        }
        .parentNode:hover
        {
            background: #f3f9fe url(../images/NavigationBG.jpg) repeat-x left top;
            font-size: 12px;
            font-weight: bold;
            color: #000;
        }
        .leafNode
        {
            font-size: 12px;
            padding-right: 5px;
            margin: 0px;
            font-weight: bold;
            color: #010101;
        }
        .leafNode:hover
        {
            font-size: 12px;
            font-weight: normal;
            color: #147498;
        }
        .selectedNode
        {
            font-size: 12px;
            padding-right: 5px;
            margin: 0px;
            font-weight: bold;
            color: #147498;
        }
    </style>

    <script type="text/javascript">
        function chk() {
            PageMethods.getclear(CallSuccess, CallFailed);
            return true;
        }
        function CallSuccess() {
            return true;
        }
         function CallFailed() {
            return false;
        }
	 
    </script>

</head>
<body>
    <div class="LftPanel">
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td colspan="3" rowspan="3" align="left" style="height: 100% !important;" valign="top"
                    width="100%">
                    <form id="form1" runat="server">
                    <asp:ScriptManager ID="scriptmanager1" runat="server" EnablePageMethods="true">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="updatepanel1" runat="server">
                        <ContentTemplate>
                            <asp:TreeView ID="TreeViewMenu" CssClass="tree" NodeWrap="true" runat="server" NodeIndent="9"
                                Target="mainFrame" ShowLines="True" Font-Bold="False" OnSelectedNodeChanged="TreeViewMenu_SelectedNodeChanged"
                                OnTreeNodeExpanded="TreeViewMenu_TreeNodeExpanded">
                                <RootNodeStyle CssClass="rootNode" Width="200px" />
                                <ParentNodeStyle CssClass="parentNode" Font-Bold="True" Width="165px" />
                                <LeafNodeStyle CssClass="leafNode" />
                                <SelectedNodeStyle CssClass="selectedNode" />
                            </asp:TreeView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
