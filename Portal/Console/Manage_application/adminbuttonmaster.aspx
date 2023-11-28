<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Manage_application_adminbuttonmaster"
    CodeBehind="adminbuttonmaster.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc2" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="uc1" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Console::Button</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/AjaxScript.js" type="text/javascript"></script>

    <script src="../jscript48/Validator.js" type="text/javascript"></script>
    

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="MainArea">
        <uc1:Header ID="header1" runat="server" />
        <div id="MidArea">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td valign="top" id="LeftPannel">
                        <lm:LeftMenu ID="lm1" runat="server" />
                    </td>
                    <td valign="top" class="midRightArea">
                        <div id="container">
                           <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                                    <uc2:Navigation ID="Navigation1" runat="server" />
                                    <cc1:TabContainer runat="server" ID="TabadminButtonMaster" Width="100%" ActiveTabIndex="0"
                                        CssClass="ajax__tab_yuitabview-theme" AutoPostBack="True" 
                                        OnActiveTabChanged="TabadminButtonMaster_ActiveTabChanged">
                                        <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateButton" TabIndex="0">
                                            <ContentTemplate>
                                                <div class="nodata" align="center">
                                                </div>
                                                <div class="mandatory">
                                                    (<font color="#FF0000">*</font> indicates mandatory field)</div>
                                                <div class="addTable">
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td width="130">
                                                                Function Name
                                                            </td>
                                                            <td width="5">
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlselFun" runat="server" OnSelectedIndexChanged="ddlselFun_SelectedIndexChanged"
                                                                    AutoPostBack="True" Width="210px">
                                                                    <asp:ListItem Value="0">Select Function</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Button Name (In English) 
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBtnname" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <font color="#FF0000">*</font>&nbsp;
                                                                <cc1:FilteredTextBoxExtender ID="FilterBtnname" runat="server" TargetControlID="txtBtnname"
                                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" ValidChars="-&./()_ +"
                                                                    Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr id="trOL" runat="server" visible="false">
                                                            <td>
                                                                Button Name (In <%=StrOL%>) 
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBtnnameOL" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <font color="#FF0000">*</font>&nbsp;
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr id="TRurl" runat="server">
                                                            <td id="Td1" runat="server">
                                                                File Name
                                                            </td>
                                                            <td runat="server">
                                                                :
                                                            </td>
                                                            <td id="Td2" runat="server">
                                                                <asp:TextBox ID="txtFilename" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                                                                <font color="#FF0000">*</font>(Maximum
                                                                <asp:Label ID="lblRemarks" runat="server" ForeColor="Red">50</asp:Label>
                                                                &nbsp;characters allowed)
                                                                <cc1:FilteredTextBoxExtender ID="FilteredFilename" runat="server" TargetControlID="txtFilename"
                                                                    FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars="-/.&_?=0123456789"
                                                                    Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                              
                                                            </td>
                                                        </tr>
                                                        <tr id="TRbrowser" runat="server">
                                                            <td id="Td3" runat="server">
                                                                Description
                                                            </td>
                                                            <td runat="server">
                                                                :
                                                            </td>
                                                            <td id="Td4" runat="server">
                                                                <asp:TextBox ID="txtDesc" runat="server" Style="resize: none" TextMode="MultiLine"
                                                                    Width="200px" MaxLength="500"></asp:TextBox>
                                                                <font color="#FF0000">*</font>
                                                                <cc1:FilteredTextBoxExtender runat="server" ID="Description" FilterMode="InvalidChars"
                                                                    InvalidChars="!,<,>,#,%,$,^,&,<>'" TargetControlID="txtDesc" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                                    Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                                Maximum
                                                                <asp:Label ID="lblchar" Text="500" runat="server" ForeColor="#FF3300"></asp:Label>
                                                                &nbsp;Characters Allowed.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Sl No
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSlno" runat="server" Text=""></asp:Label>
                                                                <span style="margin-left: 210px">Highlights the latest Sl. No.</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Tab Available
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="rbtTabyes" runat="server" Checked="True" Text="Yes" GroupName="tab" />
                                                                <asp:RadioButton ID="rbtTabNo" runat="server" Text="No" GroupName="tab" />
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Button Available for
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkView" runat="server" Text="View Rights" />
                                                                <asp:CheckBox ID="chkAdd" runat="server" Text="Add Rights" />
                                                                <asp:CheckBox ID="chkMng" runat="server" Text="Manage Rights" />
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td id="Td5" runat="server">
                                                                Active
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td id="Td6" runat="server">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="1">
                                                                    <tr>
                                                                        <td width="50%">
                                                                            <asp:RadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="rbtn" Checked="True" />
                                                                            <asp:RadioButton ID="rbtnNo" runat="server" Text="No" GroupName="rbtn" />
                                                                            <font color="#FF0000">*</font>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Re-Create Xml
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="cbReCXml" runat="server" />
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
                                                                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn" OnClick="btnsave_Click"
                                                                    OnClientClick="return  conformation(this);" />&nbsp;
                                                                <asp:Button ID="btnReset" runat="server" OnClientClick="return  conformation(this);"
                                                                    Text="Reset" CssClass="btn" OnClick="btnReset_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="ACTIVE" ID="TabActiveButton" TabIndex="1">
                                            <ContentTemplate>
                                                <div class="nodata" align="center">
                                                </div>
                                                <div class="addTable">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="130">
                                                                Select Function Name
                                                            </td>
                                                            <td width="220">
                                                                <asp:DropDownList ID="ddlActivefun" runat="server" Width="210px" OnSelectedIndexChanged="ddlActivefun_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnActiveView" runat="server" Text="View" OnClick="btnActiveView_Click"
                                                                    OnClientClick="return validateview('TabadminButtonMaster_TabActiveButton_ddlActivefun');" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
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
                                                    <asp:GridView ID="GridBtnActive" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                        DataKeyNames="ButtonId" ItemStyle-VerticalAlign="Top" PagerStyle-Mode="NumericPages"
                                                        PagerStyle-PageButtonCount="10" OnRowDataBound="GridBtnActive_RowDataBound"
                                                        AllowPaging="True" OnPageIndexChanging="GridBtnActive_PageIndexChanging" 
                                                        EnableModelValidation="True">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <input type="checkbox" name="cbAll" value="cbAll" onclick="SelectAll(cbAll,'GridBtnActive','form1')" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll,'GridBtnActive','form1')" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl.No</HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtslno" runat="server" Width="40px" Text='<%# DataBinder.Eval(Container.DataItem, "ShotNum") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Button Name(In English)" DataField="ButtonName"></asp:BoundField>
                                                             <asp:BoundField  DataField="ButtonNameOL"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Description" DataField="Description"></asp:BoundField>
                                                            <asp:BoundField HeaderText="View" DataField="ViewR"></asp:BoundField>
                                                            <asp:BoundField HeaderText="ADD" DataField="ADDR"></asp:BoundField>
                                                            <asp:BoundField HeaderText="MANAGE" DataField="ManageR"></asp:BoundField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Edit</HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="Hypedit" runat="server" ToolTip="Edit" ImageUrl="~/Console/images/editIcon.gif"> <img src="../images/editIcon.gif" border="0px" align="absmiddle" /> </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <div style="height: 20px; padding: 5px; font-weight: bold; font-size: medium; color: silver">
                                                                No Active Button Records Found.
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle Font-Bold="True" />
                                                        <PagerStyle CssClass="paging" />
                                                    </asp:GridView>
                                                </div>
                                                <div class="deletebtn" style="padding-left: 10px; margin-top: 5px">
                                                    <asp:Button ID="btnmodify" runat="server" Text="Modify Link" OnClick="btnmodify_click"
                                                        OnClientClick="return checkSelect(this);"
                                                        Visible="False" />&nbsp;
                                                    <asp:Button ID="btninActive" runat="server" Text="Make InActive" OnClick="btninActive_Click"
                                                        Visible="False" OnClientClick="return checkSelect(this);" />
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="INACTIVE" ID="TabInActiveButton" TabIndex="2">
                                            <ContentTemplate>
                                                <div class="nodata" align="center">
                                                </div>
                                                <div class="addTable">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="130">
                                                                Select Function Name
                                                            </td>
                                                            <td width="220">
                                                                <asp:DropDownList ID="ddlinactivefun" runat="server" Width="210px" OnSelectedIndexChanged="ddlinactivefun_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btninactiveview" runat="server" Text="View" OnClick="btninactiveview_Click"
                                                                    OnClientClick="return validateview('TabadminButtonMaster_TabInActiveButton_ddlinactivefun');" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="margin-right: 7px; height: 20px">
                                                    <table border="0" align="right">
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lbtnAll" Visible="False" runat="server" Text="All" OnClick="lbtnAll_Click"></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPaging" runat="server" Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="viewTable">
                                                    <asp:GridView ID="GridinActivebutton" runat="server" AutoGenerateColumns="False"
                                                        CellPadding="0" ItemStyle-VerticalAlign="Top" PagerStyle-Mode="NumericPages"
                                                        PagerStyle-PageButtonCount="10" DataKeyNames="ButtonId" PageSize="10" AllowPaging="True"
                                                        OnPageIndexChanging="GridinActivebutton_PageIndexChanging" OnRowDataBound="GridBtnInActive_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <input type="checkbox" name="cbAll" value="cbAll" onclick="SelectAll(cbAll,'GridinActivebutton','form1')" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll,'GridinActivebutton','form1')" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Sl.No" DataField="ShotNum"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Button Name(In English)" DataField="ButtonName"></asp:BoundField>
                                                             <asp:BoundField  DataField="ButtonNameOL"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Description" DataField="Description"></asp:BoundField>
                                                            <asp:BoundField HeaderText="View" DataField="ViewR"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Add" DataField="ADDR"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Manage" DataField="ManageR"></asp:BoundField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <div style="height: 20px; padding: 5px; font-weight: bold; font-size: medium; color: silver">
                                                                No Inactive Button Records Found.
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle Font-Bold="True" CssClass="headerbg" />
                                                        <PagerStyle CssClass="paging" />
                                                    </asp:GridView>
                                                </div>
                                                <div class="deletebtn" style="padding-left: 10px;">
                                                    <asp:Button ID="btnActive" runat="server" Text="Make Active" OnClick="btnActive_Click"
                                                        Visible="False" OnClientClick="return checkSelect(this)" />
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                              <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>
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
<script type="text/javascript" language="javascript">
    function checkvalidation() {
        if (!WhiteSpaceValidation1st('TabadminButtonMaster_TabCreateButton_txtBtnname')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabadminButtonMaster_TabCreateButton_txtFilename')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabadminButtonMaster_TabCreateButton_txtDesc')) {
            return false;
        }
        if (!DropDownValidation('TabadminButtonMaster_TabCreateButton_ddlselFun', 'Function')) {
            return false;
        }
        if (!blankFieldValidation('TabadminButtonMaster_TabCreateButton_txtBtnname', 'Button Name In English')) {
            return false;
        }
        if (document.getElementById("TabadminButtonMaster_TabCreateButton_txtBtnnameOL") != null) {
            if (!blankFieldValidation('TabadminButtonMaster_TabCreateButton_txtBtnnameOL', 'Button Name In <%=StrOL%>')) {
                return false;
            }
            if (!WhiteSpaceValidation1st('TabadminButtonMaster_TabCreateButton_txtBtnnameOL')) {
                return false;
            }
        }
        if (!blankFieldValidation('TabadminButtonMaster_TabCreateButton_txtFilename', 'File Name')) {
            return false;
        }
        if (!blankFieldValidation('TabadminButtonMaster_TabCreateButton_txtDesc', 'Description')) {
            return false;
        }
        if (document.getElementById('TabadminButtonMaster_TabCreateButton_chkView').checked == false && document.getElementById('TabadminButtonMaster_TabCreateButton_chkAdd').checked == false && document.getElementById('TabadminButtonMaster_TabCreateButton_chkMng').checked == false) {
            alert('Check atleast one right for which button is available.');
            return false;
        }
        return true;

    }
    function TextCounterArea(ctlTxtName, lblCouter, numTextSize) {
        var txtName = document.getElementById(ctlTxtName).value;
        var txtNameLength = txtName.length;
        if (parseInt(txtNameLength) > parseInt(numTextSize)) {
            var txtMaxTextSize = txtName.substr(0, numTextSize)
            document.getElementById(ctlTxtName).value = txtMaxTextSize;
            alert("Entered Text Exceeds '" + numTextSize + "' Characters.");
            document.getElementById(lblCouter).innerHTML = 0;
            return false;
        }
        else {
            document.getElementById(lblCouter).innerHTML = parseInt(numTextSize) - parseInt(txtNameLength);
            return true;
        }
    }
    function checkSelect(btnid) {
        if (!ConfirmCheck('form1')) {
            return false;
        }
        for (var i = 0; i < document.forms[0].elements.length; i++) {
            if (document.forms[0].elements[i].checked == true) {

                if (btnid.value == "Modify Link") {
                    return confirm('Are You Sure To Modify Link?');
                }
                if (btnid.value == "Make InActive") {
                    return confirm('Are You Sure To Make Inactive?');
                }
                else {
                    return confirm('Are You Sure To Make Active?');
                }

            }
        }

        return false;
    }
    function validateview(Button) {
        if (!DropDownValidation(Button, 'Function Name')) {
            return false;
        }
    }
         
    </script>
<script type="text/javascript">
    function conformation(btnId) {
        if (btnId.value.toLowerCase() == "save") {
            if (!checkvalidation()) {
                return false;
            }
            else {
                return confirm("Are you sure to Save?")
            }
        }
        if (btnId.value.toLowerCase() == "update") {
            if (!checkvalidation()) {
                return false;
            }
            else {
                return confirm("Are you sure to Update?")
            }
        }
        if (btnId.value.toLowerCase() == "reset") {
            return confirm("Are you sure to Reset?")
        }
        if (btnId.value.toLowerCase() == "cancel") {
            return confirm("Are you sure to Cancel?")
        }
    }
</script>

