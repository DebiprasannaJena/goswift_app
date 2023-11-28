<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Office_Timing_AdminShiftAssignment"
    EnableEventValidation="false" CodeBehind="AdminShiftAssignment.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../FillHierarchy.ascx" TagName="getUsers" TagPrefix="uc2" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc4" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="hdr" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Console::ShiftAssignment</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.02)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.02)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../jscript48/Validator.js" type="text/javascript"></script>
     <script src="../scripts/ajax.js" type="text/javascript"></script>
    <script src="../scripts/PopulateHiearchyajax.js" type="text/javascript"></script>

   

</head>

<script type="text/javascript">
     function CheckSelect() {

        if (!DropDownValidation('TabShiftAssign_TabViewShiftAssign_ddlEshift', 'Shift')) {
            return false;
        }

        if (!ConfirmCheck('form1')) {
            return false;
        } else {
            return confirm('Do you want to Move shift ?');
        }
    }
     function DeleteSelect() {
        if (!ConfirmCheck('form1')) {
            return false;
        }
        else {
            return confirm('Do you want to delete shift ?');
        }
    }
     function ViewValidation() {
       
    }
     function CheckUpdate() {
    debugger;
        if (!DropDownValidation('TabShiftAssign_TabCreateShiftAssign_ddlLocation', 'Location')) {
            return false;
        }
        else if (!DropDownValidation('TabShiftAssign_TabCreateShiftAssign_ddlShift', 'Shift')) {
            return false;
        }
        else if (!DropDownValidation('TabShiftAssign_TabCreateShiftAssign_getUsers1_sdrplayers0', 'Location')) {
            return false;
        }
        else if(document.getElementById("TabShiftAssign_TabCreateShiftAssign_hiduser").value=="")
        {
            if(document.getElementById("TabShiftAssign_TabCreateShiftAssign_hidRuser").value=="")
            {
                var len=document.getElementById("TabShiftAssign_TabCreateShiftAssign_selUser").options.length;
                if(len>1)
                {
                    return true;
                }
                else{
                    alert("Add or remove a user for shift assignment.");
                    return false;
                }
            }
            else
            {
                var len=document.getElementById("TabShiftAssign_TabCreateShiftAssign_selUser").options.length;
                if (len>1)
                {
                    return true;
                }
                else{
                    alert("Add or remove a user for shift assignment.");
                    return false;
                }
            }
        }                                                       

        else {
            return true;
        }
 
    }
    function CheckConfirm(btnId) {
        var result;
      
        if (btnId.value == "Update") {
            if (!CheckUpdate()) {
                return false;
            }
            else {
                result = confirm('Do you want to Update Shift ?');
                return result;
            }
        }
        else if (btnId.value == "Reset") {
        return confirm('Do you want to Reset ?');
        }
    }
      function ValidationNew() {
            if (!DropDownValidation('TabShiftAssign_TabViewShiftAssign_HierarchyForAllLocation2_sdrplayers0', 'Location')) {
                return false;
            }
            else{            
                return true;
            }
        }
</script>

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
                            <cc1:TabContainer ID="TabShiftAssign" runat="server" Width="100%" ActiveTabIndex="1"
                                CssClass="ajax__tab_yuitabview-theme" OnActiveTabChanged="TabShiftAssign_ActiveTabChanged"
                                AutoPostBack="True">
                                <div class="Menubar">
                                    <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateShiftAssign" TabIndex="0">
                                        <ContentTemplate>
                                            <div class="mandatory">
                                                (* indicates mandatory fields)
                                            </div>
                                            <div class="nodata">
                                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                            </div>
                                            <div class="addTable">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="middle">
                                                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                                <ContentTemplate>
                                                                    <table border="0" cellpadding="0" cellspacing="0" class="FormBorder">
                                                                        <tr>
                                                                            <td width="150">
                                                                                Location Name
                                                                            </td>
                                                                            <td width="5">
                                                                                :
                                                                            </td>
                                                                            <td colspan="3" style="padding-left: 6px;">
                                                                                <asp:DropDownList ID="ddlLocation" runat="server" Width="185px" AutoPostBack="true"
                                                                                    OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Shift Name
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td colspan="3" style="padding-left: 6px;">
                                                                                <asp:DropDownList ID="ddlShift" runat="server" Width="185px" AutoPostBack="true"
                                                                                    OnSelectedIndexChanged="ddlShift_SelectedIndexChanged">
                                                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <uc2:getUsers ID="getUsers1" runat="server" />
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="150">
                                                                                Select User:
                                                                            </td>
                                                                            <td width="5">
                                                                                :
                                                                            </td>
                                                                            <td style="padding-left: 5px;" width="210">
                                                                                <asp:ListBox ID="selUser" runat="server" AppendDataBoundItems="True" SelectionMode="Multiple"
                                                                                    Width="185px" Height="100px">
                                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                </asp:ListBox>
                                                                            </td>
                                                                            <td width="25px">
                                                                                <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td onclick="LstBoxAdd('TabShiftAssign_TabCreateShiftAssign_selRUser','TabShiftAssign_TabCreateShiftAssign_selUser','TabShiftAssign_TabCreateShiftAssign_hiduser','TabShiftAssign_TabCreateShiftAssign_hidRuser');"
                                                                                            onmouseover="this.style.cursor='hand';" style="width: 98px">
                                                                                            <img src="../images/leftArrow2.gif" alt="" title="Select" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td onclick="LstBoxRemove('TabShiftAssign_TabCreateShiftAssign_selRUser','TabShiftAssign_TabCreateShiftAssign_selUser','TabShiftAssign_TabCreateShiftAssign_hiduser','TabShiftAssign_TabCreateShiftAssign_hidRuser');"
                                                                                            onmouseover="this.style.cursor='hand'" style="width: 98px">
                                                                                            <img src="../images/rightArrow1.gif" alt="" title="Remove" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <asp:HiddenField ID="hiduser" runat="server" />
                                                                                    <asp:HiddenField ID="hidRuser" runat="server" />
                                                                                </table>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <asp:ListBox ID="selRUser" runat="server" SelectionMode="Multiple" Width="180px"
                                                                                    Height="100px">
                                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                </asp:ListBox>
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <span>&nbsp;
                                                                                    <asp:Button ID="btnAdd" runat="server" Text="Update" OnClick="btnAdd_Click" OnClientClick="return CheckConfirm(this);" />
                                                                                    &nbsp;&nbsp;
                                                                                    <asp:Button ID="btnReset" OnClientClick="return CheckConfirm(this);" OnClick="btnReset_Click"
                                                                                        runat="server" Text="Reset" />
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabViewShiftAssign" runat="server" HeaderText="VIEW" TabIndex="1">
                                        <ContentTemplate>
                                            <div class="addTable">
                                                <uc2:getUsers ID="HierarchyForAllLocation2" runat="server" />
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="148px" align="left">
                                                            Search By Employee
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td style="width: 210px; padding-left: 9px;">
                                                            <asp:TextBox ID="txtsearch" runat="server" Width="175px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="148px" align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 210px; padding-left: 9px;">
                                                            <asp:Button ID="btnSearch" OnClientClick="return ValidationNew();" runat="server"
                                                                Text="Search" OnClick="btnSearch_Click" />
                                                            &nbsp;
                                                            <asp:Button ID="btnSearchReset" runat="server" Text="Reset" OnClick="btnSearchReset_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <div style="margin-right: 7px; height: 20px">
                                                        <table border="0" align="right">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="LnkbtnAllin" Visible="False" runat="server" Text="All" OnClick="LnkbtnAllin_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblpage" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="viewTable">
                                                        <asp:GridView ID="GrdShiftAssign" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                            ItemStyle-VerticalAlign="Top" AllowPaging="true" PagerStyle-Mode="NumericPages"
                                                            PagerStyle-PageButtonCount="5" Width="100%" PageSize="10" DataKeyNames="UserId"
                                                            OnRowDataBound="GrdShiftAssign_RowDataBound" OnPageIndexChanging="GrdShiftAssign_PageIndexChanging" EmptyDataText="No Records Found !">
                                                            <EmptyDataTemplate>
                                                                <div id="divCentr" style="text-align: center;">
                                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="LightGray"
                                                                        Text="No Shift Assignment Record Found ..."></asp:Label>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <input type="checkbox" name="cbAll" value="cbAll" onclick="SelectAll(cbAll,'GrdShiftAssign','form1')" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll,'GrdShiftAssign','form1')" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="" HeaderText="SlNo."></asp:BoundField>
                                                                <asp:BoundField DataField="Fullname" HeaderText="Employee Name"></asp:BoundField>
                                                                <asp:BoundField DataField="DesignationName" HeaderText="Designation"></asp:BoundField>
                                                                <asp:BoundField DataField="LocationName" HeaderText="Location"></asp:BoundField>
                                                                <asp:BoundField DataField="ShiftName" HeaderText="Shift Name"></asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle Font-Bold="True" />
                                                            <PagerStyle CssClass="paging" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="deletebtn">
                                                        <span>&nbsp;
                                                            <asp:Label ID="lblSelectShift" Visible="false" runat="server" Text="Select Shift"></asp:Label>
                                                            &nbsp;&nbsp;
                                                            <asp:DropDownList ID="ddlEshift" runat="server" Width="100px">
                                                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            &nbsp;&nbsp;
                                                            <asp:Button ID="btnMove" runat="server" Text="Move" OnClick="btnMove_Click" OnClientClick="return CheckSelect();" />
                                                            &nbsp;&nbsp;
                                                            <asp:Button ID="btnDelete" runat="server" Text="Remove Shift" OnClick="btnDelete_Click"
                                                                OnClientClick="return DeleteSelect();" />
                                                        </span>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnSearchReset" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </div>
                            </cc1:TabContainer>
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
