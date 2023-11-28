<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminPrimaryLink.aspx.cs"
    Inherits="Admin_Manage_Links_adminPrimaryLink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc2" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="uc1" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Console:: Primary Link</title>
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />
    <script src="../jscript48/Validator.js" type="text/javascript"></script>
    
    <style type="text/css">
        .style1
        {
            color: #FF0000;
            border-left-color: #ACA899;
            border-right-color: #C0C0C0;
            border-top-color: #ACA899;
            border-bottom-color: #C0C0C0;
        }
        .overlay
        {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px; /* background-color: #aaa;
            filter: alpha(opacity=50);
            opacity: 0.5;*/
        }
        .overlayContent
        {
            z-index: 99;
            margin: 250px auto;
            width: 90px;
            height: 90px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            color: #000;
        }
        .progress
        {
            background: #FFFFFF;
            font-family: Verdana,Arial, Helvetica;
            color: Black;
            font-size: 11.5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:HiddenField ID="hidfldlbltxt" runat="server"></asp:HiddenField>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="1800" EnablePartialRendering="true"
        runat="server">
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
                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <uc2:Navigation ID="Navigation1" runat="server" />
                                    <cc1:TabContainer runat="server" ID="TabPrimaryLink" Width="100%" ActiveTabIndex="1"
                                        CssClass="ajax__tab_yuitabview-theme" AutoPostBack="true" OnActiveTabChanged="TabPrimaryLink_ActiveTabChanged">
                                        <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreatePlink" TabIndex="0">
                                            <HeaderTemplate>
                                                CREATE
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <div class="nodata" align="center">
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#C00000"></asp:Label>
                                                </div>
                                                <div class="mandatory">
                                                    (<font color="#FF0000">*</font> indicates mandatory field)</div>
                                                <div class="addTable">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td width="230px">
                                                                Global Link Name
                                                            </td>
                                                            <td width="5px">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlselGlink" runat="server" OnSelectedIndexChanged="ddlselGlink_SelectedIndexChanged"
                                                                    AutoPostBack="True" Width="210px" TabIndex="1">
                                                                    <asp:ListItem Value="0">Select Global Link</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="230px">
                                                                Primary Link Name&nbsp(In English)
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPlink" runat="server" MaxLength="50" TabIndex="2" Width="200px"></asp:TextBox>
                                                                <font color="#FF0000">*</font> (Maximum
                                                                <asp:Label ID="lblRemarks" runat="server" ForeColor="Red">50</asp:Label>
                                                                &nbsp;characters allowed)
                                                                <cc1:FilteredTextBoxExtender ID="txtPlinkfilter" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                                    ValidChars="-&./_(){}[]+ " TargetControlID="txtPlink" Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr id="trOL" runat="server" visible="false">
                                                            <td width="230px">
                                                                Primary Link Name&nbsp;(In <%=StrOL%>)
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtPlinkNameInAmharic" runat="server" AutoCompleteType="Disabled"
                                                                    Width="200px" MaxLength="100"></asp:TextBox><font color="#FF0000">&nbsp;*</font>
                                                            </td>
                                                            <td align="left">
                                                                &#160;&#160;
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="230px">
                                                                Link Type
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="rbtIntern" runat="server" Checked="True" GroupName="radLinkType"
                                                                    Text="Internal" TabIndex="3" />
                                                                <asp:RadioButton ID="rbtExtrn" runat="server" GroupName="radLinkType" Text="External"
                                                                    TabIndex="4" />
                                                            </td>
                                                        </tr>
                                                        <tr id="TRfunction" runat="server">
                                                            <td runat="server" width="230px">
                                                                Type of Function
                                                            </td>
                                                            <td runat="server">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td runat="server">
                                                                <asp:DropDownList ID="ddlselFunction" runat="server" Width="210px" TabIndex="5">
                                                                    <asp:ListItem Value="0">Select Function</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr id="TRurl" runat="server" style="display: none">
                                                            <td runat="server" width="230px">
                                                                URL
                                                            </td>
                                                            <td runat="server">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td runat="server">
                                                                <asp:TextBox ID="txtURL" runat="server" Width="200px" TabIndex="6"></asp:TextBox>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <%--  <tr id="TRbrowser" runat="server" style="display: none">
                                                            <td runat="server">
                                                                Open in Browser
                                                            </td>
                                                            <td runat="server">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td id="Td2" runat="server">
                                                                <asp:RadioButton ID="rbtSame" runat="server" GroupName="radBrowser" Text="Same" Checked="True"
                                                                    TabIndex="7" />
                                                                <asp:RadioButton ID="rbtNew" runat="server" GroupName="radBrowser" Text="New" TabIndex="8" />
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td width="230px">
                                                                Sl No Link
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <%--<asp:TextBox ID="txtSlNo" runat="server" ReadOnly="True" MaxLength="2" Width="200px"
                                                                    TabIndex="9"></asp:TextBox>--%>
                                                                <asp:Label ID="lblPSlNo" runat="server" Text="***"></asp:Label>
                                                                <span style="margin-left: 210px">Highlights the latest Sl. No.</span>
                                                            </td>
                                                        </tr>
                                                        <%--  <tr>
                                                            <td>
                                                                <asp:Label ID="lbllfirstlayer" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <asp:ListBox ID="lbLocation" onchange="GetSelected(this);" runat="server" Height="100px"
                                                                    Width="210px" SelectionMode="Multiple"></asp:ListBox>
                                                                <font color="#FF0000">*</font>
                                                                <asp:HiddenField ID="hidVal" runat="server" />
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td width="230px">
                                                                Show on Home Page
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkShowHome" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <%--  <tr id="TRsecondlayer" runat="server" style="display: none">
                                                            <td runat="server">
                                                                <asp:Label ID="lblsecondlayer" runat="server"></asp:Label>
                                                            </td>
                                                            <td runat="server">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td runat="server">
                                                                <asp:ListBox ID="lbDepartment" runat="server" SelectionMode="Multiple" Width="210px"
                                                                    Height="100px" TabIndex="11"></asp:ListBox>
                                                                <font color="#FF0000">*</font> Hold &lt;Ctrl&gt; key to select &nbsp;multiple departments
                                                                Hold &lt;Ctrl&gt; key to select &nbsp;multiple departments
                                                            </td>
                                                        </tr>
                                                        <tr>--%>
                                                        <td>
                                                            &nbsp;
                                                            <%-- <asp:HiddenField ID="hidUserLoc" runat="server" />--%>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Save" TabIndex="12" />
                                                            &nbsp;
                                                            <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Update" Visible="False"
                                                                TabIndex="13" />
                                                            &nbsp;
                                                            <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" TabIndex="14" />
                                                        </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="ACTIVE" ID="TabActivePlink" TabIndex="1">
                                            <ContentTemplate>
                                                <div class="nodata" align="center">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#C00000"></asp:Label>
                                                </div>
                                                <div class="addTable">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="110">
                                                                Select Global Link <strong>:</strong>
                                                            </td>
                                                            <td width="220">
                                                                <asp:DropDownList ID="ddleditGlink" runat="server" Width="210px">
                                                                </asp:DropDownList>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnShow" OnClientClick="return ddlValidation('TabPrimaryLink_TabActivePlink_ddleditGlink');"
                                                                    OnClick="btnShow_Click" runat="server" Text="Show" />
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
                                                    <asp:GridView ID="GridActivePlink" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                        AllowPaging="True" DataKeyNames="PlinkId" ItemStyle-VerticalAlign="Top" PagerStyle-Mode="NumericPages"
                                                        PagerStyle-PageButtonCount="10" OnPageIndexChanging="GridActivePlink_PageIndexChanging"
                                                        OnRowDataBound="GridActivePlink_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <input type="checkbox" name="cbAll" value="cbAll" onclick="SelectAll(cbAll,'GridActivePlink','form1')" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkplink" runat="server" onclick="return deSelectHeader(cbAll,'GridActivePlink','form1')" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15px"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl No.</HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtslno" runat="server" Width="40px" Text='<%# DataBinder.Eval(Container.DataItem, "SlNo") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Primary Link(In English)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrimaryLinkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlinkName") %>'></asp:Label>
                                                                    <asp:Button ID="btnactive" runat="server" CommandName="Activate" Font-Bold="False"
                                                                        Text="Make Active" Visible="False" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField  DataField="PlinkNameinAhmaric">
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Global Link" DataField="GLinkName"></asp:BoundField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Edit
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="hypEdit" ImageUrl="../images/editIcon.gif" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="40px"></ItemStyle>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <div style="height: 20px; padding: 5px; font-weight: bold; font-size: medium; color: silver">
                                                                No Active Primary Link Related Records Found.
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle Font-Bold="True" />
                                                        <PagerStyle CssClass="paging" />
                                                    </asp:GridView>
                                                </div>
                                                <div class="deletebtn" style="padding-left: 10px; margin-top: 5px;">
                                                    <asp:Button ID="btnmodify" runat="server" Text="Modify Link" OnClick="btnmodify_click"
                                                        OnClientClick="return checkSelect('TabPrimaryLink_TabActivePlink_btnmodify');" />
                                                    <asp:Button ID="btninactive" runat="server" Text="Make InActive" OnClick="btninactive_click"
                                                        OnClientClick="return checkSelect('TabPrimaryLink_TabActivePlink_btninactive');" />
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="INACTIVE" ID="TabInActivePlink" TabIndex="2">
                                            <ContentTemplate>
                                                <div class="nodata" align="center">
                                                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="#C00000"></asp:Label>
                                                </div>
                                                <div class="addTable">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="110">
                                                                Select Global Link:
                                                            </td>
                                                            <td width="220">
                                                                <asp:DropDownList ID="ddlinactiveGlink" runat="server" Width="210px">
                                                                </asp:DropDownList>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnInactiveShow" OnClientClick="return ddlValidation('TabPrimaryLink_TabInActivePlink_ddlinactiveGlink');"
                                                                    OnClick="btnInactiveShow_Click" runat="server" Text="Show" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="margin-right: 7px; height: 20px">
                                                    <table border="0" align="right">
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnkInactiveAll" Visible="False" runat="server" Text="All" OnClick="lnkInactiveAll_Click"></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblInactivePaging" runat="server" Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="viewTable">
                                                    <asp:GridView ID="GridInActivePlink" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                        AllowPaging="True" ItemStyle-VerticalAlign="Top" PagerStyle-Mode="NumericPages"
                                                        PagerStyle-PageButtonCount="10" PageSize="10" OnPageIndexChanging="GridInActivePlink_PageIndexChanging"
                                                        OnRowDataBound="GridInActivePlink_RowDataBound" EmptyDataText="No Records Found"
                                                        DataKeyNames="PlinkId">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <input type="checkbox" name="cbAll" value="cbAll" onclick="SelectAll(cbAll,'GridInActivePlink','form1')" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkplink" runat="server" onclick="return deSelectHeader(cbAll,'GridInActivePlink','form1')" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15px"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="SlNo" HeaderText="Sl No.">
                                                                <HeaderStyle Width="35px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PlinkName" HeaderText="PrimaryLink Name(In English)" NullDisplayText="--">
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PlinkNameinAhmaric" 
                                                                NullDisplayText="--"></asp:BoundField>
                                                      
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <div style="height: 20px; padding: 5px; font-weight: bold; font-size: medium; color: silver">
                                                                No Inactive Primary Link Related Records Found.
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle Font-Bold="True" />
                                                        <PagerStyle CssClass="paging" />
                                                    </asp:GridView>
                                                </div>
                                                <div class="deletebtn" style="padding-left: 10px; margin-top: 5px;">
                                                    <asp:Button ID="btnactive" CssClass="deletebtn" runat="server" Text="Make Active"
                                                        OnClientClick="return checkSelect('TabPrimaryLink_TabInActivePlink_btnactive');"
                                                        OnClick="btnactive_Click" Visible="False" />
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                                <ProgressTemplate>
                                    <div class="overlay" />
                                    <div class="overlayContent">
                                        <img src="../images/Loading.gif" alt="Loading" />
                                        Loading...
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--#include file="../Includes/footer.aspx" -->
    </div>
    </form>
</body>
<script type="text/javascript" language="JavaScript">
    function chkValid() {
        var hidval;

        if (!DropDownValidation('TabPrimaryLink_TabCreatePlink_ddlselGlink', 'Global Link')) {
            return false;
        }
        if (!blankFieldValidation('TabPrimaryLink_TabCreatePlink_txtPlink', 'Primary Link')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabPrimaryLink_TabCreatePlink_txtPlink')) {
            return false;
        }
        if (document.getElementById("TabPrimaryLink_TabCreatePlink_txtPlinkNameInAmharic") != null) {
            if (!blankFieldValidation('TabPrimaryLink_TabCreatePlink_txtPlinkNameInAmharic', 'Primary Link Name (In <%=StrOL%>)')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabPrimaryLink_TabCreatePlink_txtPlinkNameInAmharic')) {
            return false;
        }
         }

//       
        if (!SpecialCharcheck('TabPrimaryLink_TabCreatePlink_txtPlink')) {
            return false;
        }
        if (document.getElementById("TabPrimaryLink_TabCreatePlink_rbtIntern").checked == true) {
            if (!DropDownValidation('TabPrimaryLink_TabCreatePlink_ddlselFunction', 'Function')) {
                return false;
            }
        }

        if (document.getElementById("TabPrimaryLink_TabCreatePlink_rbtExtrn").checked == true) {

            if (!blankFieldValidation('TabPrimaryLink_TabCreatePlink_txtURL', 'URL')) {
                return false;
            }

        }

    }

    function HideShow(RbtInternal, RbtExternal) {
        var rbtinter = document.getElementById(RbtInternal);
        var rbtextern = document.getElementById(RbtExternal);
        if (document.getElementById && document.createTextNode) {

            if (rbtinter.checked == true) {

                document.getElementById("TabPrimaryLink_TabCreatePlink_TRfunction").style.display = "";
                document.getElementById("TabPrimaryLink_TabCreatePlink_TRurl").style.display = "none";
                document.getElementById("TabPrimaryLink_TabCreatePlink_TRbrowser").style.display = "none";
            }
            else if (rbtextern.checked == true) {

                document.getElementById("TabPrimaryLink_TabCreatePlink_TRfunction").style.display = "none";
                document.getElementById("TabPrimaryLink_TabCreatePlink_TRurl").style.display = "";
                document.getElementById("TabPrimaryLink_TabCreatePlink_TRbrowser").style.display = "";
                //                    document.getElementById("TabPrimaryLink_TabCreatePlink_txtURL").value="";

            }
        }
    }
    function checkSelect(Buttonid) {

        if (!ConfirmCheck('form1')) {

            return false;
        }
        for (var i = 0; i < document.forms[0].elements.length; i++) {
            if (document.forms[0].elements[i].checked == true) {
                if (Buttonid == "TabPrimaryLink_TabActivePlink_btnDelete") {
                    return confirm('Are You Sure To Delete?');
                }
                if (Buttonid == "TabPrimaryLink_TabActivePlink_btninactive") {
                    return confirm('Are You Sure To Inactive?');
                }
                if (Buttonid == "TabPrimaryLink_TabActivePlink_btnmodify") {
                    return confirm('Are You Sure To Modify?');
                }
                if (Buttonid == "TabPrimaryLink_TabInActivePlink_btnactive") {
                    return confirm('Are You Sure To Active?');
                }

            }
        }

        return false;
    }
    function conformation(btnid) {

        if (chkValid() != "") {
            if (btnid == "TabPrimaryLink_TabCreatePlink_btnAdd") {
                if (confirm("Are you sure to Save ?")) {

                    return true;
                }
                else
                    return false;
            }
            if (btnid == "TabPrimaryLink_TabCreatePlink_btnEdit") {
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
    function ResetFunction(btnid) {
        if (btnid == "TabPrimaryLink_TabCreatePlink_btnReset") {

            var btn = document.getElementById(btnid);

            if (btn.value == "Reset") {

                if (confirm("Are you sure to Reset ?")) {

                    return true;
                }
                else
                    return false;
            }
            else if (btn.value == "Cancel") {
                if (confirm("Are you sure to Cancel ?")) {

                    return true;
                }
                else
                    return false;
            }
        }
    }
    function isUrl(obj) {
        var d = document.getElementById(obj);
        var s = d.value;
        var regexp = /(ftp|http|https):\/{2}([w-w]{3})\.{1}([a-zA-Z0-9_]*)\.{1}([a-zA-Z0-9]*)$/
        if (regexp.test(s)) {
            return true
        }
        else {
            alert('Enter valid url');
            return false
        }

    }

    function GetSelected(listbId) {
        var opt;
        document.getElementById("TabPrimaryLink_TabCreatePlink_hidUserLoc").value = "";
        for (i = 0; i < listbId.length; i++) {
            opt = listbId.options[i];
            if ((opt.selected) && (opt.value != 0)) {
                var selected = opt.value;
                document.getElementById("TabPrimaryLink_TabCreatePlink_hidUserLoc").value = document.getElementById("TabPrimaryLink_TabCreatePlink_hidUserLoc").value + ',' + selected;
            }
        }
    }
    function ddlValidation(ddlId) {
        var ddlToValidate = document.getElementById(ddlId);
        if (ddlToValidate.selectedIndex == 0) {
            alert('Select Global Link !');
            return false;
        }
        else {
            return true;
        }
    }
        
    </script>
</html>
