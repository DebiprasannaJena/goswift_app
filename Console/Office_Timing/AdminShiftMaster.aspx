<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Office_Timing_AdminShiftMaster"
    EnableEventValidation="false" CodeBehind="AdminShiftMaster.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc4" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="hdr" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Console::ShftMaster</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.02)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.02)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

</head>

<script type="text/javascript" language="javascript">


    function CallReset() {
        if (document.getElementById("TabShiftMaster_TabCreateShift_btnReset").value == "Reset") {
            var strCtrls = "TabShiftMaster_TabCreateShift_ddlLocation,TabShiftMaster_TabCreateShift_txtShiftName,TabShiftMaster_TabCreateShift_txtDesc,TabShiftMaster_TabCreateShift_cbShiftType";
            var objArr = strCtrls.split(",");

            for (var i = 0; i < objArr.length; i++) {
             
                ResetValue(objArr[i]);
            }

        } 
        else{        
            history.back();
         }
        return true;
    }

    function checkAddvalidation() {
        if (!DropDownValidation('TabShiftMaster_TabCreateShift_ddlLocation', 'Location')) {
            return false;
        }
        if (!blankFieldValidation('TabShiftMaster_TabCreateShift_txtShiftName', 'Shift Name')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabShiftMaster_TabCreateShift_txtShiftName')) {
            return false;
        }
         if (!SpecialCharcheckValidate('TabShiftMaster_TabCreateShift_txtShiftName', ",'^#%~`@$!\"\<>:;?|]}[{")) {

            return false;
        }
 
        if (!blankFieldValidation('TabShiftMaster_TabCreateShift_txtDesc', 'Description')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabShiftMaster_TabCreateShift_txtDesc')) {
            return false;
        }
        if (!SpecialCharcheckValidate('TabShiftMaster_TabCreateShift_txtDesc', ",'^#%~`@$!\"\<>:;?|]}[{")) {

            return false;
        }

        if (!MaxlengthValidation('TabShiftMaster_TabCreateShift_txtDesc', 'Description', 100)) {
            return false;
        }
        return true;
    }
     function CheckSelect() {
    debugger;
        if (!ConfirmCheck('form1')) {

            return false;
        }
        for (var i = 0; i < document.forms[0].elements.length; i++) {
            if (document.forms[0].elements[i].checked == true) {
                return confirm('Are You Sure To Delete?');

            }
        }

        return false;
    }
     function conformation(btnid) {
    debugger;
         if (checkAddvalidation()) {
            if (btnid.value == "Save") {
                if (confirm("Are you sure to Save ?")) {
                    return true;
                }
                else
                    return false;
            }  

            if (btnid.value == "Update") {
                if (confirm("Are You Sure To Update?")) {
                    return true;
                }
                else
                    return false;
            }  
        }
        else
            return false;
    }  
    function TextCounter(ctlTxtName, lblCouter, numTextSize) {
            var txtName = document.getElementById(ctlTxtName).value;
            var txtNameLength = txtName.length;
            if (parseInt(txtNameLength) > parseInt(numTextSize)) {
                var txtMaxTextSize = txtName.substr(0, numTextSize)
                document.getElementById(ctlTxtName).innerHTML = txtMaxTextSize;
                alert("Entered Text Exceeds '" + numTextSize + "' Characters.");
                document.getElementById(lblCouter).innerHTML = 0;
                return false;
            }
            else {
                document.getElementById(lblCouter).innerHTML = parseInt(numTextSize) - parseInt(txtNameLength);
                return true;
            }
        }
        function AllowEnter() {
            if (window.event.keyCode == 13) {
                event.returnValue = true;
                event.cancel = false;
            }
        }
</script>

<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <uc4:Navigation ID="Navigation1" runat="server" />
                                    <cc1:TabContainer ID="TabShiftMaster" runat="server" Width="100%" ActiveTabIndex="1"
                                        CssClass="ajax__tab_yuitabview-theme" AutoPostBack="true" OnActiveTabChanged="TabShiftMaster_ActiveTabChanged">
                                        <div class="Menubar">
                                            <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateShift" TabIndex="0">
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
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="FormBorder">
                                                                        <tr>
                                                                            <td width="150">
                                                                                Location Name
                                                                            </td>
                                                                            <td width="5">
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlLocation" runat="server" Width="160px" AutoPostBack="true"
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
                                                                            <td>
                                                                                <span></span>
                                                                                <asp:TextBox ID="txtShiftName" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Description
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDesc" runat="server" MaxLength="100" Height="53px" TextMode="MultiLine"
                                                                                    Width="300px"></asp:TextBox>
                                                                                <font color="#FF0000">*</font> Maximum
                                                                                <asp:Label ID="lblMaxCounter" Text="100" ForeColor="Red" runat="server"></asp:Label>&nbsp;
                                                                                Characters Allowed
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Shift Type
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="cbShiftType" runat="server" Text="Cross Shift" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Set as Default
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkView" runat="server" Text="Default Shift" />
                                                                                <asp:Label ID="lblDefaultMsg" runat="server" Visible="false" Text="">                                                </asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <span>&nbsp;<asp:Button ID="btnAdd" runat="server" Text="Save" OnClick="btnAdd_Click"
                                                                                    OnClientClick="return conformation(this);" />
                                                                                    &nbsp;&nbsp;
                                                                                    <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" Text="Reset" />
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel runat="server" HeaderText="EDIT" ID="TabEditShift" TabIndex="1">
                                                <ContentTemplate>
                                                    <div class="nodata" align="center">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#C00000"></asp:Label>
                                                    </div>
                                                    <div class="addTable">
                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="FormBorder">
                                                            <tr>
                                                                <td width="128">
                                                                    <strong>Location Name</strong>
                                                                </td>
                                                                <td width="5">
                                                                    <strong>:</strong>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlLoc" runat="server" Width="160px" OnSelectedIndexChanged="ddlLoc_SelectedIndexChanged"
                                                                        AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div style="margin-right: 7px; height: 20px">
                                                        <table border="0" align="right">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkBtnAll" Visible="False" runat="server" Text="All" OnClick="lnkBtnAll_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPaging" Visible="False" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="viewTable">
                                                        <asp:GridView ID="GVShift" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                            CellSpacing="0" HeaderStyle-Font-Bold="True" ItemStyle-VerticalAlign="Top" PagerStyle-HorizontalAlign="Right"
                                                            PagerStyle-Mode="NumericPages" PagerStyle-PageButtonCount="10" Width="100%" PageSize="10"
                                                            DataKeyNames="ShiftID,DefaultShift,ShiftType" AllowPaging="true" OnPageIndexChanging="GVShift_PageIndexChanging"
                                                            OnRowDataBound="GVShift_RowDataBound">
                                                            <EmptyDataTemplate>
                                                                <div id="divCentr" style="text-align: center;">
                                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="LightGray"
                                                                        Text="No Shift Master Record Found ..."></asp:Label>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="25px">
                                                                    <HeaderTemplate>
                                                                        <input type="checkbox" name="cbAll" value="cbAll" onclick="SelectAll(cbAll,'GVShift','form1');" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll,'GVShift','form1');" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="SL. No" ItemStyle-Width="40px"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Location" DataField="LocationName" ItemStyle-Width="125px" />
                                                                <asp:BoundField HeaderText="Shift" DataField="ShiftName" ItemStyle-Width="125px" />
                                                                <asp:BoundField HeaderText="Description" DataField="ShiftDescription" />
                                                                <asp:TemplateField ItemStyle-Width="50px">
                                                                    <HeaderTemplate>
                                                                        Edit
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href="AdminShiftMaster.aspx?SID=<%#DataBinder.Eval(Container.DataItem,"ShiftID") %>">
                                                                            <img alt="" src="../images/editIcon.gif" border="0" align="absmiddle" />
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle Font-Bold="True" />
                                                            <PagerStyle CssClass="paging" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="deletebtn" style="padding-left: 10px">
                                                        <asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btndelete_Click"
                                                            OnClientClick="return CheckSelect();" />
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                        </div>
                                    </cc1:TabContainer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
