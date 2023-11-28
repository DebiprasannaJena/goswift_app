<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminTabmaster.aspx.cs"
    Inherits="KwantifyPortalV3._2_App.Admin.Manage_application.adminTabmaster" %>

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
    <title>Admin Console::Tab</title>
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
                            <%--       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                            <uc2:Navigation ID="Navigation1" runat="server" />
                            <cc1:TabContainer runat="server" ID="TabadminTabMaster" Width="100%" ActiveTabIndex="1"
                                CssClass="ajax__tab_yuitabview-theme" AutoPostBack="true" OnActiveTabChanged="TabadminTabMaster_ActiveTabChanged">
                                <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateTab" TabIndex="0">
                                    <ContentTemplate>
                                        <div class="nodata" align="center">
                                        </div>
                                        <div class="mandatory">
                                            (<font color="#FF0000">*</font> indicates mandatory field)</div>
                                        <div class="addTable">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="130px">
                                                        Function Name
                                                    </td>
                                                    <td width="5" align="center" valign="middle">
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlselFun" runat="server" OnSelectedIndexChanged="ddlselFun_SelectedIndexChanged"
                                                            AutoPostBack="True" Width="210px">
                                                        </asp:DropDownList>
                                                        <font color="#FF0000">*</font>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Button Name
                                                    </td>
                                                    <td align="center" valign="middle">
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlselbtn" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlselbtn_SelectedIndexChanged"
                                                            Width="210px">
                                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <font color="#FF0000">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Tab Name
                                                    </td>
                                                    <td align="center" valign="middle">
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTabname" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="TabName" ValidChars="-&./_(){}[]+ "
                                                            TargetControlID="txtTabname" FilterType="UppercaseLetters,Custom,LowercaseLetters,Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <font color="#FF0000">*</font>
                                                    </td>
                                                </tr>
                                                <tr id="trOL" runat="server" visible="false">
                                                    <td align="left">
                                                        Tab Name&nbsp(In <%=StrOL%>)
                                                    </td>
                                                    <td align="center" valign="middle">
                                                        <strong>:</strong>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTabNameInAmharic" runat="server" AutoCompleteType="Disabled"
                                                            Width="200px" MaxLength="100"></asp:TextBox><font color="#FF0000">&nbsp;*</font>
                                                    </td>
                                                    <td align="left">
                                                        &#160;&#160;
                                                    </td>
                                                    <td align="left">
                                                      
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        File Name
                                                    </td>
                                                    <td align="center" valign="middle">
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFileName" runat="server" Width="200px" MaxLength="100"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="FileName" ValidChars="-/.&_?=" TargetControlID="txtFileName"
                                                            FilterType="UppercaseLetters,Custom,LowercaseLetters,Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <font color="#FF0000">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Description
                                                    </td>
                                                    <td align="center" valign="middle">
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDesc" runat="server" Style="resize: none" TextMode="MultiLine"
                                                            Width="200px" MaxLength="500"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="Description" FilterMode="InvalidChars"
                                                            InvalidChars="<'*>!" TargetControlID="txtDesc" FilterType="UppercaseLetters,Custom,LowercaseLetters,Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                      &nbsp;  Maximum
                                                        <asp:Label ID="lblchar" ForeColor="Red" Text="500" runat="server"></asp:Label>
                                                        Characters Allowed
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Sl No
                                                    </td>
                                                    <td align="center" valign="middle">
                                                        :
                                                    </td>
                                                    <td valign="middle">
                                                        <asp:Label ID="lblSlno" runat="server" Text=""></asp:Label>
                                                        <span style="margin-left: 210px">Highlights the latest Sl. No.</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Tab Available for
                                                    </td>
                                                    <td align="center" valign="middle">
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
                                                    <td align="center" valign="middle">
                                                        :
                                                    </td>
                                                    <td id="Td6" runat="server">
                                                        <asp:RadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="rbtn" Checked="true" />
                                                        <asp:RadioButton ID="rbtnNo" runat="server" Text="No" GroupName="rbtn" />
                                                        <font color="#FF0000">*</font>
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
                                                        <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn" OnClientClick="return dispConfm(this);"
                                                            OnClick="btnsave_Click" />&nbsp;
                                                        <asp:Button ID="btnReset" OnClientClick="return dispConfm(this);" runat="server"
                                                            Text="Reset" CssClass="btn" OnClick="btnReset_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="ACTIVE" ID="TabEditTab" TabIndex="1">
                                    <HeaderTemplate>
                                        ACTIVE
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div class="nodata" align="center">
                                        </div>
                                        <div class="addTable">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="100">
                                                        Function Name
                                                    </td>
                                                    <td width="210">
                                                        <asp:DropDownList ID="ddlActivefun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlActivefun_SelectedIndexChanged"
                                                            Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="80">
                                                        Button Name
                                                    </td>
                                                    <td width="210">
                                                        <asp:DropDownList ID="ddlactivebtn" runat="server" Width="200px">
                                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnview" runat="server" Text="View" OnClick="btnview_Click" OnClientClick="return validateview('TabadminTabMaster_TabEditTab_ddlActivefun','TabadminTabMaster_TabEditTab_ddlactivebtn');" />
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
                                                        <asp:Label ID="lblpage" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="viewTable">
                                            <asp:GridView ID="GridTabActive" runat="server" AutoGenerateColumns="False" DataKeyNames="TabId"
                                                CellPadding="0" ItemStyle-VerticalAlign="Top" PagerStyle-Mode="NumericPages"
                                                PagerStyle-PageButtonCount="10" EmptyDataText="No Records Found" OnRowDataBound="GridTabActive_RowDataBound"
                                                AllowPaging="True" OnPageIndexChanging="GridTabActive_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <input type="checkbox" name="cbAll" value="cbAll" onclick="SelectAll(cbAll,'GridTabActive','form1')" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll,'GridTabActive','form1')" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Sl.No</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtslno" runat="server" Width="40px" Text='<%# DataBinder.Eval(Container.DataItem, "ShotNum") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Tab Name(In English)" DataField="TabName">
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemStyle Width="120px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField  DataField="TabNameinAmharic">
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemStyle Width="120px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="File Name" DataField="URL">
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemStyle Width="150px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Description" DataField="Description">
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemStyle Width="200px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="View" DataField="ViewR">
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemStyle Width="30px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Add" DataField="ADDR">
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemStyle Width="30px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Manage" DataField="ManageR">
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemStyle Width="30px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Edit</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="Hypedit" runat="server" ToolTip="Edit"> <img src="../images/editIcon.gif" border="0" align="absmiddle" /> </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemStyle Width="30px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div style="height: 20px; padding: 5px; font-weight: bold; font-size: medium; color: silver">
                                                        No Active Tab Records Found.
                                                    </div>
                                                </EmptyDataTemplate>
                                                <HeaderStyle Font-Bold="True" />
                                                <PagerStyle CssClass="paging" />
                                            </asp:GridView>
                                        </div>
                                        <div class="deletebtn" style="padding-left: 10px;">
                                            <asp:Button ID="btnmodify" Visible="False" runat="server" Text="Modify Link" OnClick="btnmodify_click"
                                                OnClientClick="return checkSelect(this);" />&nbsp;
                                            <asp:Button ID="btninActive" runat="server" Text="Make InActive" Visible="False"
                                                OnClick="btninActive_Click" OnClientClick="return checkSelect(this);" />
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="INACTIVE" ID="TabActiveTab" TabIndex="2">
                                    <ContentTemplate>
                                        <div class="nodata" align="center">
                                        </div>
                                        <div class="addTable">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="100">
                                                        Function Name
                                                    </td>
                                                    <td width="210">
                                                        <asp:DropDownList ID="ddlinactivefun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlinactivefun_SelectedIndexChanged"
                                                            Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="80">
                                                        Button Name
                                                    </td>
                                                    <td width="210">
                                                        <asp:DropDownList ID="ddlinactivebtn" runat="server" AutoPostBack="True" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btninactiveview" runat="server" OnClick="btninactiveview_Click" OnClientClick="return validateview('TabadminTabMaster_TabActiveTab_ddlinactivefun','TabadminTabMaster_TabActiveTab_ddlinactivebtn');"
                                                            Text="View" />
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
                                                        <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="viewTable">
                                            <asp:GridView ID="GridTabinActive" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                ItemStyle-VerticalAlign="Top" EmptyDataText="No Records Found" PagerStyle-Mode="NumericPages"
                                                PagerStyle-PageButtonCount="10" DataKeyNames="TabId" PageSize="10" AllowPaging="true"
                                                OnPageIndexChanging="GridTabinActive_PageIndexChanging" OnRowDataBound="GridTabInActive_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <input type="checkbox" name="cbAll" value="cbAll" onclick="SelectAll(cbAll,'GridTabinActive','form1')" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll,'GridTabinActive','form1')" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="SL.No" DataField="ShotNum"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Tab Name" DataField="TabName"></asp:BoundField>
                                                    <asp:BoundField  DataField="TabNameinAmharic"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Description" DataField="Description"></asp:BoundField>
                                                    <asp:BoundField HeaderText="View" DataField="ViewR"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Add" DataField="ADDR"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Manage" DataField="ManageR"></asp:BoundField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div style="height: 20px; padding: 5px; font-weight: bold; font-size: medium; color: silver">
                                                        No Inactive Tab Records Found.
                                                    </div>
                                                </EmptyDataTemplate>
                                                <HeaderStyle Font-Bold="True" CssClass="headerbg" />
                                                <PagerStyle CssClass="paging" />
                                            </asp:GridView>
                                        </div>
                                        <div class="deletebtn" style="padding-left: 10px;">
                                            <asp:Button ID="btnactive" runat="server" Text="Make Active" OnClick="btnactive_Click"
                                                Visible="False" OnClientClick="return checkSelect(this);" />
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                            <%--     </ContentTemplate>
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

        if (!DropDownValidation('TabadminTabMaster_TabCreateTab_ddlselFun', 'Function Name')) {
            return false;
        }
        if (!DropDownValidation('TabadminTabMaster_TabCreateTab_ddlselbtn', 'Button Name')) {
            return false;
        }
        if (!blankFieldValidation('TabadminTabMaster_TabCreateTab_txtTabname', 'Tab Name')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabadminTabMaster_TabCreateTab_txtTabname')) {
            return false;
        }
        if (document.getElementById("TabadminTabMaster_TabCreateTab_txtTabNameInAmharic") != null) {
            if (!blankFieldValidation('TabadminTabMaster_TabCreateTab_txtTabNameInAmharic', 'Tab Name (In <%=StrOL%>)')) {
                return false;
            }
            if (!WhiteSpaceValidation1st('TabadminTabMaster_TabCreateTab_txtTabNameInAmharic')) {
                return false;
            }
        }

        if (!blankFieldValidation('TabadminTabMaster_TabCreateTab_txtFileName', 'File Name')) {
            return false;
        }

        if (!WhiteSpaceValidation1st('TabadminTabMaster_TabCreateTab_txtFileName')) {
            return false;
        }

        if (!WhiteSpaceValidation('TabadminTabMaster_TabCreateTab_txtFileName')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabadminTabMaster_TabCreateTab_txtDesc')) {
            return false;
        }

        if (document.getElementById('TabadminTabMaster_TabCreateTab_chkView').checked == false && document.getElementById('TabadminTabMaster_TabCreateTab_chkAdd').checked == false && document.getElementById('TabadminTabMaster_TabCreateTab_chkMng').checked == false) {
            alert('Check atleast one Rights from checkBox');
            return false;
        }
        return true;

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
                else if (btnid.value == "Make InActive") {
                    return confirm('Are You Sure To Make InActive?');
                }
                else {
                    return confirm('Are You Sure To Make Active?');
                }
            }
        }

        return false;
    }
    function dispConfm(btnId) {
        if (btnId.value == "Save") {
            if (!checkvalidation()) {
                return false;
            }
            else {
                return confirm("Are You Sure To Save ?")
            }
        }
        else if (btnId.value == "Update") {
            if (!checkvalidation()) {
                return false;
            }
            else {
                return confirm("Are You Sure To Update ?")
            }
        }



        else if (btnId.value == "Reset") {
            return confirm("Are You Sure To Reset ?");
        }
        else {
            return confirm("Are You Sure To Cancel ?");
        }

    }
    function validateview(A, B) {
        if (!DropDownValidation(A, 'Function Name')) {
            return false;
        }
        if (!DropDownValidation(B, 'Button Name')) {
            return false;
        }
    }
    </script>
<script type="text/javascript">

    function conformation() {
        var btntext = '<%=strbutton%>';

        if (checkvalidation() != "") {
            if (btntext == "Save") {
                if (confirm("Are you sure to Save ?")) {
                    return true;
                }
                else
                    return false;
            }

            if (btntext == "Update") {
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
</script>
