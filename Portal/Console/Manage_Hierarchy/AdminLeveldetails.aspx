<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Inherits="Admin_Manage_Hierarchy_AdminLeveldetails"
    CodeBehind="AdminLeveldetails.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Console/PopulateHierarchy.ascx" TagName="PopulateHirarchy" TagPrefix="uc1" %>
<%@ Register Src="~/Console/Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc2" %>
<%@ Register Src="~/Console/FillHierarchy.ascx" TagName="FillHierarchy" TagPrefix="uc3" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="hdr" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Console::Hierarchy Level</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.01)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.01)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/ajax.js" type="text/javascript"></script>

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

</head>

<script type="text/javascript" language="JavaScript">
    var Position = '<%=Manage_Usercontrol_Property.PopulateHierarchyProperty.PositionId%>';
     
    var name = '<%=strName%>' + ' Name';
     var count;
    var count1;
     function validation() {
        var lvlVal = '<%=strNodePID%>';
        if (parseInt(lvlVal) == 3) {
            if (!DropDownValidation('TabLevelDetails_TabCreateLevel_PopulateHirarchys_drplayer1', 'Levels')) {
                return false;
            }
        }
        if (parseInt(lvlVal) == 4) {
            if (!DropDownValidation('TabLevelDetails_TabCreateLevel_PopulateHirarchys_drplayer1', 'Levels')) {
                return false;
            }
            if (!DropDownValidation('TabLevelDetails_TabCreateLevel_PopulateHirarchys_drplayer2', 'Levels')) {
                return false;
            }
        }
        if (parseInt(lvlVal) == 5) {
            if (!DropDownValidation('TabLevelDetails_TabCreateLevel_PopulateHirarchys_drplayer1', 'Levels')) {
                return false;
            }
            if (!DropDownValidation('TabLevelDetails_TabCreateLevel_PopulateHirarchys_drplayer2', 'Levels')) {
                return false;
            }
            if (!DropDownValidation('TabLevelDetails_TabCreateLevel_PopulateHirarchys_drplayer3', 'Levels')) {
                return false;
            }
        }
        if (!blankFieldValidation('TabLevelDetails_TabCreateLevel_txtName', name)) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabLevelDetails_TabCreateLevel_txtName')) {
            return false;
        }
        if (!chkSingleQuote('TabLevelDetails_TabCreateLevel_txtName')) {
            return false;
        }

        if (!WhiteSpaceValidation1st('TabLevelDetails_TabCreateLevel_txtAddress1')) {
            return false;
        }
        if (!isCharfirst('TabLevelDetails_TabCreateLevel_txtName')) {
            return false;
        }
        
        return true;
    }

    function CheckConfirm(btnId) {
       
        if (btnId.value == 'Save') {
            if (!validation()) {
                return false;
            }
            else {
                return confirm('Do you want to save the data ?');
            }
        }
        else if (btnId.value == 'Reset') {
            return confirm('Do you want to Reset the data ?');
        }
        else if (btnId.value == 'Cancel') {
            var result = confirm('Do you want to Cancel the data ?');
        }
        else if (btnId.value == 'Update') {
            if (!validation()) {
                return false;
            }
            else {
                return confirm('Do you want to Update the data ?');
            }
        }
    }
     function showLevel() {
        for (var i = 3; i <= parseInt(Position); i++) {

            count = parseInt(i - 2);
            count1 = parseInt(i - 1);
            var viewdropdownlabel = document.getElementById('TabLevelDetails_TabEditLevel_PopulateHirarchy2_Label' + count1).innerHTML;
            if (!DropDownValidation('TabLevelDetails_TabEditLevel_PopulateHirarchy2_drplayer' + (i - 2), viewdropdownlabel)) {
                return false;
            }
        }
    }
     function checkSelect() {
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
     function IndCheckTelNo()  
    {
        var phoneNo = document.getElementById("TabLevelDetails_TabCreateLevel_txtTel").value;
        if (phoneNo === '' || phoneNo === null) {
            return true;
        }
        else {
             var phoneRE = /^([0-9()-+]*)$/;
            if (phoneNo.match(phoneRE)) {
                return true;
            }
            else {
                alert("Invalid Telephone Number!"); return false;
            }
        }
    }
     function RestrictSpecialChar() {
        var enetredValue = String.fromCharCode(window.event.keyCode)
        var objAllowedChar = new RegExp("[-_,/&'().0-9a-zA-Z \r]", "g");
        if (enetredValue.match(objAllowedChar) == null) {
            window.event.keyCode = 0
            return false;
        }
    }
     function isSpecialChar1st(txt) {
        var str = txt.value;
        var iChars = "*|, \":<>[]{}`\';()@&$#%!^-_=+./?";
        if (iChars.indexOf(str.charAt(0)) != -1) {
            txt.value = '';
            txt.focus();
            return false;
        }
        else {
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <uc2:Navigation ID="Navigation1" runat="server" />
                                    <cc1:TabContainer ID="TabLevelDetails" runat="server" Width="100%" ActiveTabIndex="1"
                                        CssClass="ajax__tab_yuitabview-theme" AutoPostBack="True" OnActiveTabChanged="TabLevelDetails_ActiveTabChanged">
                                        <div class="Menubar">
                                            <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateLevel" TabIndex="0">
                                                <ContentTemplate>
                                                    <div class="mandatory">
                                                        (* indicates mandatory fields)</div>
                                                    <div class="nodata">
                                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="addTable">
                                                        <uc1:PopulateHirarchy ID="PopulateHirarchys" runat="server" />
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="146">
                                                                    <%=strName%>
                                                                    Name
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtName" onkeyup="isSpecialChar1st(this);" runat="server" MaxLength="50"></asp:TextBox>
                                                                    &nbsp;<span style="color: red">* </span>(Maximum <span style="color: red">50 </span>
                                                                    Characters)
                                                                    <cc1:FilteredTextBoxExtender ID="FiltertxtName" runat="server" TargetControlID="txtName"
                                                                        FilterType="LowercaseLetters,UppercaseLetters,Custom,Numbers" ValidChars="+(){}[]_/.&- ">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td>
                                                                    Hierarchy Code
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtHierarchyCode" runat="server" MaxLength="50"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td>
                                                                    Address
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAddress1" onkeyup="isSpecialChar1st(this);" onkeypress="javascript:return RestrictSpecialChar();"
                                                                        runat="server" MaxLength="70"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAddress2" onkeyup="isSpecialChar1st(this);" onkeypress="javascript:return RestrictSpecialChar();"
                                                                        runat="server" MaxLength="70"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAddress3" onkeyup="isSpecialChar1st(this);" onkeypress="javascript:return RestrictSpecialChar();"
                                                                        runat="server" MaxLength="60"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td>
                                                                    Telephone
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTel" runat="server" MaxLength="20"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtTel_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txtTel" ValidChars="0123456789-()+">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td>
                                                                    Fax
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFax" runat="server" MaxLength="12"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                        TargetControlID="txtFax" ValidChars="0123456789">
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
                                                                <td>
                                                                    <span class="bodyText">
                                                                        <asp:Button ID="btnAdd" OnClientClick="return CheckConfirm(this);" runat="server"
                                                                            Text="Save" Width="61px" OnClick="btnAdd_Click" />
                                                                        &nbsp;
                                                                        <asp:Button ID="btnReset" OnClientClick="return CheckConfirm(this);" runat="server"
                                                                            Text="Reset" OnClick="btnReset_Click" Width="61px" />
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel runat="server" HeaderText="VIEW" ID="TabEditLevel" TabIndex="1">
                                                <ContentTemplate>
                                                    <div class="nodata" align="center">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#C00000"></asp:Label>
                                                    </div>
                                                    <div class="addTable">
                                                        <uc1:PopulateHirarchy ID="PopulateHirarchy2" runat="server" />
                                                        <asp:HiddenField ID="hidEdit" runat="server" />
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="148">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" OnClientClick="return showLevel();" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div style="margin-right: 7px; height: 20px">
                                                        <table border="0" align="right" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:LinkButton ID="lbtnAll" Visible="false" runat="server" Text="All" OnClick="lbtnAll_Click"></asp:LinkButton>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <div style="margin-left: 10px;">
                                                                        <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="viewTable">
                                                        <asp:GridView ID="GVLvldetails" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                            ItemStyle-VerticalAlign="Top" PagerStyle-Mode="NumericPages" PagerStyle-PageButtonCount="10"
                                                            AllowPaging="True" OnPageIndexChanging="GVLvldetails_PageIndexChanging" DataKeyNames="LeveldetailsID"
                                                            OnRowDataBound="GVLvldetails_RowDataBound" EmptyDataText="No Records Found">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <input type="checkbox" name="cbAll1" value="cbAll1" onclick="SelectAll(cbAll1,'GVLvldetails','form1')" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbItem" runat="server" onclick="deSelectHeader(cbAll1,'GVLvldetails','form1')" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="15px"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Sl No.">
                                                                    <ItemStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Name
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ToolTip="Edit" ID="hypName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LevelName")%>'
                                                                            NavigateUrl='<%# "AdminLeveldetails.aspx?LvlID=" + DataBinder.Eval(Container.DataItem,"LeveldetailsID")%>'></asp:HyperLink>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                           
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <div style="height:20px; padding:5px; font-weight:bold; font-size:medium; color:silver">
                                                                No Hierarchy Level Records Found.
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <HeaderStyle Font-Bold="True" />
                                                            <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="deletebtn" style="padding-left: 10px;">
                                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="false" OnClick="btnDelete_Click"
                                                            OnClientClick="return checkSelect();" />
                                                        <asp:HiddenField ID="hidAtt" runat="server" />
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
