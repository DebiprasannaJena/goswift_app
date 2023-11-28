<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminUserProfile.aspx.cs"
    EnableEventValidation="false" Inherits="Admin_Manage_User_AdminUserProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc4" %>
<%@ Register Src="~/Console/FillHierarchy.ascx" TagName="FillHierarchy" TagPrefix="uc3" %>
<%@ Register Src="~/Console/FillUserHierarchy.ascx" TagName="FillUserHierarchy" TagPrefix="uc2" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="hdr" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Admin Console::User Profile</title>
    <script src="../scripts/ProgressBar/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script src="../scripts/ProgressBar/jquery.blockUI.js" type="text/javascript"></script>

    <script src="../scripts/ProgressBar/common.js" type="text/javascript"></script>

    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.02)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.02)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/ajax.js" type="text/javascript"></script>

    <script src="../scripts/PopulateHiearchyajax.js" type="text/javascript"></script>

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../scripts/md5.js"></script>

    <script src="../scripts/jquery.tablesorter.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="JavaScript">
        jQuery(document).ready(function() {
            $("#TabContainer1_TabNew_grdNewEmp").tablesorter({ debug: false, widgets: ['zebra'], sortList: [[0, 0]] });
        });

        function CheckSelect() {
            if (!ConfirmCheck('form1')) {
                return false;
            }
        }
        function ValidationAct() {
            if (!DropDownValidation('TabContainer1_TabActive_HierarchyForAllLocation1_sdrplayers0', 'Location')) {
                return false;
            }
        }
        function ValidationNew() {
            if (!DropDownValidation('TabContainer1_TabNew_HierarchyForAllLocation2_sdrplayers0', 'Location')) {
                return false;
            }
        }
        function ValidationIna() {
            if (!DropDownValidation('TabContainer1_TabInactive_HierarchyForAllLocation3_sdrplayers0', 'Location')) {
                return false;
            }
        }

        function checkSelection(buttonId) {

            if (!ConfirmCheck('form1')) {

                return false;
            }
            else {
                if (buttonId == "TabContainer1_TabNew_btnDelete") {
                    return confirm('Are You Sure To Delete?');
                }
                else if (buttonId == "TabContainer1_TabInactive_btnActive") {
                    return confirm('Are You Sure To Make Active Employee?');
                }
                else if (buttonId == "TabContainer1_TabNew_btnEditInactive") {
                    return confirm('Are You Sure To Make Ex-Employee?');
                }
                else {
                    return false;
                }
            }
        }

        function ShowRows() {

            document.getElementById("TabContainer1_TabNew_HierarchyForAllLocation2_tr2").style.display = "block";
            document.getElementById("TabContainer1_TabNew_HierarchyForAllLocation2_tr3").style.display = "block";
        }
    </script>

    <style type="text/css">
        .style2
        {
            width: 150px;
        }
        .style1
        {
            width: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="MainArea">
        <hdr:Header ID="header1" runat="server" />
        <div id="MidArea">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td valign="top" id="LeftPannel">
                        <lm:LeftMenu ID="lm1" runat="server" />
                    </td>
                    <td valign="top" class="midRightArea">
                        <div id="container">
                            <uc4:Navigation ID="Navigation1" runat="server" />
                            <asp:HiddenField ID="hidTabindex" runat="server" />
                            <div>
                                <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                    AutoPostBack="true" OnActiveTabChanged="TabContainer1_ActiveTabChanged" ActiveTabIndex="1">
                                    <cc1:TabPanel ID="TabActive" HeaderText="All" TabIndex="0" runat="server">
                                        <HeaderTemplate>
                                            All
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div class="mandatory">
                                                (<font color="#FF0000">*</font> indicates mandatory field)</div>
                                            <div class="addTable">
                                                <uc3:FillHierarchy ID="HierarchyForAllLocation1" runat="server" />
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="150">
                                                            Search By Employee
                                                        </td>
                                                        <td>
                                                            <strong>:</strong>
                                                        </td>
                                                        <td style="padding-left: 8px;">
                                                            <asp:TextBox ID="txtEmpName" runat="server" Width="175px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilterFunname" runat="server" TargetControlID="txtEmpName"
                                                                FilterType="Custom, UppercaseLetters, LowercaseLetters" ValidChars=" " Enabled="True">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td style="padding-left: 8px;">
                                                            <asp:Button ID="btnSearchActive" runat="server" Text="Search" OnClientClick="return ValidationAct();"
                                                                OnClick="btnSearchActive_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:UpdatePanel ID="upActiveTab" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <div style="margin-right: 7px; height: 20px;">
                                                        <table border="0" align="right" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkActiveAll" Visible="False" runat="server" Text="All" OnClick="lnkActiveAll_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblActivePaging" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="viewTable">
                                                        <asp:GridView ID="grdActiveUser" runat="server" PageSize="10" AutoGenerateColumns="False"
                                                            DataKeyNames="GetID" EmptyDataText="No Records Found" AllowPaging="True" OnRowDataBound="grdActiveUser_RowDataBound"
                                                            OnPageIndexChanging="grdActiveUser_PageIndexChanging1">
                                                            <PagerSettings PageButtonCount="5" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <input name="cbAll" value="cbAll" type="checkbox" onclick="SelectAll(cbAll,'grdActiveUser','form1')" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkUser" runat="server" onclick="return deSelectHeader(cbAll,'grdActiveUser','form1')" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="15px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        SlNo.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSlno2" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="15px" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Employee Name" DataField="FullName">
                                                                    <HeaderStyle Width="150px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="User Name" DataField="UserName">
                                                                    <HeaderStyle Width="150px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Designation" DataField="DesignationID">
                                                                    <HeaderStyle Width="120px" />
                                                                </asp:BoundField>
                                                              <%--  <asp:BoundField HeaderText="Department" DataField="DepartmentID">
                                                                    <HeaderStyle Width="120px" />
                                                                </asp:BoundField>--%>
                                                              <%--  <asp:BoundField HeaderText="Grade" DataField="GradeName" NullDisplayText="--" />--%>
                                                                <asp:BoundField HeaderText="Status" DataField="status" NullDisplayText="--" />
                                                            </Columns>
                                                            <HeaderStyle Font-Bold="True" />
                                                            <PagerStyle CssClass="paging" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div style="margin-left: 10px;">
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSearchActive" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabNew" HeaderText="Active" TabIndex="1" runat="server">
                                        <HeaderTemplate>
                                            Active
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div class="mandatory">
                                                (<font color="#FF0000">*</font> indicates mandatory field)</div>
                                            <div class="addTable">
                                                <uc3:FillHierarchy ID="HierarchyForAllLocation2" runat="server" />
                                                <table border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="150">
                                                            Search By Employee
                                                        </td>
                                                        <td>
                                                             <strong>:</strong>
                                                        </td>
                                                        <td style="padding-left: 8px;">
                                                            <asp:TextBox ID="txtSearchNew" runat="server" MaxLength="100" Width="175px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredUserName" runat="server" Enabled="True"
                                                                FilterType="Custom,UppercaseLetters, LowercaseLetters" TargetControlID="txtSearchNew"
                                                                ValidChars=" ">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td style="padding-left: 8px;">
                                                            <asp:Button ID="btnSearchNew" runat="server" OnClick="btnSearchNew_Click" Text="Search"
                                                                OnClientClick="return ValidationNew();" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:UpdatePanel ID="upEditTab" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <div style="margin-right: 7px; height: 20px">
                                                        <table border="0" align="right">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LnkbtnAllin" Visible="False" runat="server" Text="All" OnClick="LnkbtnAllin_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblpage" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="viewTable">
                                                        <asp:GridView ID="grdNewEmp" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            DataKeyNames="GetID" EmptyDataText="No Records Found" AllowPaging="True" OnPageIndexChanging="grdNewEmp_PageIndexChanging"
                                                            PageSize="10" OnRowDataBound="grdNewEmp_RowDataBound">
                                                            <PagerSettings PageButtonCount="5" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <input name="cbAll" value="cbAll" type="checkbox" onclick="SelectAll(cbAll,'grdNewEmp','form1')" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkUser" runat="server" onclick="return deSelectHeader(cbAll,'grdNewEmp','form1')" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="15px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        SlNo.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSlno" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="15px" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Employee Name" DataField="FullName">
                                                                    <HeaderStyle Width="150px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="User Name" DataField="UserName">
                                                                    <HeaderStyle Width="150px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Designation" DataField="DesignationID">
                                                                    <HeaderStyle Width="120px" />
                                                                </asp:BoundField>
                                                           <%--     <asp:BoundField HeaderText="Department" DataField="DepartmentID">
                                                                    <HeaderStyle Width="120px" />
                                                                </asp:BoundField>--%>
                                                               <%-- <asp:BoundField HeaderText="Grade" DataField="GradeName" NullDisplayText="--" />--%>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Edit</HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="hypEdit" ImageUrl="~/Console/images/editIcon.gif" runat="server"></asp:HyperLink>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle Font-Bold="True" />
                                                            <PagerStyle CssClass="paging" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div style="margin-left: 10px; margin-top: 5px">
                                                        <asp:Button ID="btnDelete" Visible="false" runat="server" Text="Delete User" OnClick="btnDelete_Click"
                                                            OnClientClick="return checkSelection('TabContainer1_TabNew_btnDelete');" />
                                                        <asp:Button ID="btnEditInactive" Visible="False" runat="server" OnClick="btnEditInactive_Click1"
                                                            OnClientClick="return checkSelection('TabContainer1_TabNew_btnEditInactive');"
                                                            Text="Make Ex-Employee" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSearchNew" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabInactive" HeaderText="Ex-Employee" TabIndex="2" runat="server">
                                        <HeaderTemplate>
                                            Ex-Employee
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div class="mandatory">
                                                (<font color="#FF0000">*</font> indicates mandatory field)</div>
                                            <div class="addTable">
                                                <uc3:FillHierarchy ID="HierarchyForAllLocation3" runat="server" />
                                                <table border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="150">
                                                            Search By Employee
                                                        </td>
                                                        <td>
                                                           <strong>:</strong>
                                                        </td>
                                                        <td style="padding-left: 8px;">
                                                            <asp:TextBox ID="txtSearchInactive" runat="server" MaxLength="100" Width="175px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td style="padding-left: 8px;">
                                                            <asp:Button ID="btnSearchInactive" runat="server" OnClick="btnSearchInactive_Click"
                                                                Text="Search" OnClientClick="return ValidationIna();" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:UpdatePanel ID="upTabInactive" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <div style="margin-right: 7px; height: 20px">
                                                        <table border="0" align="right">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lbtnAll" Visible="False" runat="server" Text="All" OnClick="lbtnAll_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPaging" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="viewTable">
                                                        <asp:GridView ID="grdInactiveUser" runat="server" PageSize="5" AutoGenerateColumns="False"
                                                            DataKeyNames="GetID" EmptyDataText="No Records Found" AllowPaging="True" OnRowDataBound="grdInactiveUser_RowDataBound"
                                                            OnPageIndexChanging="grdInactiveUser_PageIndexChanging">
                                                            <PagerSettings PageButtonCount="5" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <input name="cbAll" value="cbAll" type="checkbox" onclick="SelectAll(cbAll,'grdInactiveUser','form1')" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkInactiveUser" runat="server" onclick="return deSelectHeader(cbAll,'grdInactiveUser','form1')" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="15px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        SlNo.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSlno3" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="15px" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Employee Name" DataField="FullName">
                                                                    <HeaderStyle Width="150px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="User Name" DataField="UserName">
                                                                    <HeaderStyle Width="150px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Designation" DataField="DesignationID">
                                                                    <HeaderStyle Width="120px" />
                                                                </asp:BoundField>
                                                             <%--   <asp:BoundField HeaderText="Department" DataField="DepartmentID">
                                                                    <HeaderStyle Width="120px" />
                                                                </asp:BoundField>--%>
                                                                <asp:BoundField HeaderText="Grade" DataField="GradeName" NullDisplayText="--" />
                                                            </Columns>
                                                            <HeaderStyle Font-Bold="True" />
                                                            <PagerStyle CssClass="paging" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div style="margin-left: 10px; margin-top: 10px;">
                                                        <asp:Button ID="btnActive" runat="server" Text="Make Active" OnClientClick="return checkSelection('TabContainer1_TabInactive_btnActive');"
                                                            OnClick="btnActive_Click" Visible="False" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSearchInactive" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </div>
                            <div style="display: none" id="divBlock">
                                <asp:Image runat="server" ID="Image1" ImageUrl="~/Console/images/Loading.gif" AlternateText="Loading.." />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--#include file="../Includes/footer.aspx" -->
    </div>
    </form>
</body>
</html>
