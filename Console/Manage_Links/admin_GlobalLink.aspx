<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Manage_Links_admin_GlobalLink" CodeBehind="admin_GlobalLink.aspx.cs" %>
    

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
    <title>Admin Console::Global Link</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.02)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.02)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/ajax.js" type="text/javascript"></script>

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../scripts/md5.js"></script>

    <script type="text/javascript" language="JavaScript"></script>

    <style type="text/css">
        .hiddencol
        {
            display: none;
        }
    </style>

    

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <uc2:Navigation ID="Navigation1" runat="server" />
                                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme"
                                        AutoPostBack="true" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                        <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateGlink" TabIndex="0">
                                            <HeaderTemplate>
                                                CREATE
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <div class="mandatory">
                                                    (* indicates mandatory field)</div>
                                                <div class="addTable">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                Global Link Name&nbsp(In English)
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGlinkName" onkeyup="isSpecialChar1st('TabContainer1_TabCreateGlink_txtGlinkName')"
                                                                    runat="server" MaxLength="50" TabIndex="1" Width="200px"></asp:TextBox>
                                                             <%--   <font color="#FF0000">*</font> Maximum <span style="color: Red">50</span> characters--%>
                                                                <cc1:FilteredTextBoxExtender ID="txtGlinkNamefilter" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                                    ValidChars="&_-(){}[]+ " TargetControlID="txtGlinkName" Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                                  <font color="#FF0000">*</font> &nbsp Maximum <span class="mandatory">
                                                                                    <asp:Label ID="lblMaxcounter" Text="50" runat="server"></asp:Label>
                                                                                </span>Characters Allowed.

                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                         <tr id="trOL" runat="server" visible="false">
                                                            <td align="left">
                                                                 Global Link Name&nbsp(In <%=StrOL%>)
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGlinkNameInAmharic" runat="server" AutoCompleteType="Disabled" Width="200px"
                                                                    MaxLength="100"></asp:TextBox><font color="#FF0000">&nbsp;*</font>
                                                            </td>
                                                            <td align="left">
                                                                &#160;&#160;
                                                            </td>
                                                            <td align="left">
                                                               
                                                            </td>
                                                        </tr>
                                                       <%-- <tr>
                                                            <td>
                                                                Location
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <asp:ListBox ID="lbUserLoc" onchange="GetSelected(this);" Height="100px" runat="server"
                                                                    SelectionMode="Multiple" Width="210px"></asp:ListBox>
                                                                <font color="#FF0000">*</font> Hold &lt;Ctrl&gt; key to select &nbsp;multiple departments
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td>
                                                                Sl. No.
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <%--  <asp:TextBox ID="txtGSlNo" runat="server" MaxLength="2" ReadOnly="True" TabIndex="2"
                                                                        Width="200px"></asp:TextBox>--%>
                                                                <asp:Label ID="lblGSlno" runat="server" Text=""></asp:Label>
                                                               
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                         
                                                      <%--  <tr>
                                                            <td>
                                                                Global Link Image
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <asp:FileUpload ID="fuGlinkImg" onchange="previewFile();" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Image ID="imgGlink" Visible="true" Width="50px" Height="50px" runat="server" />
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td>
                                                                Show on Home Page
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkShowHome" runat="server" />
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                          <tr  id="tr1" runat="server">
                                                            <td>
                                                                Re-Create Xml
                                                            </td>
                                                            <td align="center" valign="middle" >
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="cbReCXml" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               <%-- <asp:HiddenField ID="hidUserLoc" runat="server" />
                                                                <asp:HiddenField ID="hidImgUrl" runat="server" />--%>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnGAdd" runat="server" OnClick="btnGAdd_Click" TabIndex="3" Text="Save" />
                                                                &nbsp;
                                                                <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" OnClientClick="return dispConfm('TabContainer1_TabCreateGlink_btnReset');"
                                                                    TabIndex="4" Text="Reset" />
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="ACTIVE" ID="TabActiveGlink" TabIndex="1">
                                            <HeaderTemplate>
                                                ACTIVE
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <div class="nodata" align="center">
                                                    <div style="margin-right: 7px; height: 20px">
                                                        <table align="right" border="0">
                                                            <tr>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lbtnAll" runat="server" OnClick="lbtnAll_Click" Text="All" Visible="False"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                </div>
                                                <div class="viewTable">
                                                    <asp:GridView ID="grdActiveGLink" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        CellPadding="0" DataKeyNames="IntGlobalLinkID" ItemStyle-VerticalAlign="Top"
                                                        PageSize="10" OnPageIndexChanging="grdActiveGLink_PageIndexChanging" OnRowDataBound="grdActiveGLink_RowDataBound"
                                                        PagerStyle-Mode="NumericPages" PagerStyle-PageButtonCount="10">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <input name="cbAll" onclick="SelectAll(cbAll,'grdActiveGLink','form1')" type="checkbox"
                                                                        value="cbAll" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkGLink" runat="server" onclick="return deSelectHeader(cbAll,'grdActiveGLink','form1')" />
                                                                </ItemTemplate>
                                                                <ControlStyle CssClass="NOPRINT" />
                                                                <HeaderStyle CssClass="NOPRINT" Width="20px" />
                                                                <ItemStyle CssClass="NOPRINT" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSlNo" runat="server" Columns="2" Text='<%# Eval("SLNO") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Global Link(In English)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                 <asp:Label ID="lblGblName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GlobalLinkName") %>'> </asp:Label>
                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="GlobalLinkNameinAhmaric"/>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Link Status
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("DeletedStatus") %>'> </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
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
                                                                No Active Global Link Related Records Found.
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle Font-Bold="True" />
                                                        <PagerStyle CssClass="paging" />
                                                    </asp:GridView>
                                                </div>
                                                <div class="deletebtn" style="padding-left: 10px; margin-top: 5px;">
                                                    <asp:Button ID="btnModify" runat="server" OnClick="btnModify_Click" Text="Modify Links" />
                                                  <%--  <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete Links" />--%>
                                                    <asp:Button ID="btninactive" runat="server" OnClick="btninactive_Click" Text="Make InActive" />
                                                    <br />
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="INACTIVE" ID="TabInActiveGlink" TabIndex="2">
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
                                                    <asp:GridView ID="grdInActiveGlobalLink" DataKeyNames="IntGlobalLinkID" runat="server"
                                                        AutoGenerateColumns="False" CellPadding="0" ItemStyle-VerticalAlign="Top" PagerStyle-Mode="NumericPages"
                                                        PagerStyle-PageButtonCount="10" Width="100%" AllowPaging="True" OnPageIndexChanging="grdInActiveGlobalLink_PageIndexChanging"
                                                        EmptyDataText="No Records Found"  OnRowDataBound="grdInActiveGLink_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <input name="cbInactive" onclick="SelectAll(cbInactive,'grdInActiveGlobalLink','form1')"
                                                                        type="checkbox" value="cbInactive" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkInactiveGLink" runat="server" onclick="return deSelectHeader(cbInactive,'grdInActiveGlobalLink','form1')" />
                                                                </ItemTemplate>
                                                                <ControlStyle CssClass="NOPRINT" />
                                                                <HeaderStyle CssClass="NOPRINT" Width="20px" />
                                                                <ItemStyle CssClass="NOPRINT" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="SLNO" HeaderText="Sl No.">
                                                                <HeaderStyle Width="35px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="GlobalLinkName" HeaderText="Global Link Name"></asp:BoundField>
                                                              <asp:BoundField DataField="GlobalLinkNameinAhmaric" />
                                                            <asp:BoundField HeaderText="Admin" Visible="False"></asp:BoundField>
                                                              
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <div style="height: 20px; padding: 5px; font-weight: bold; font-size: medium; color: silver">
                                                                No Inactive Global Link Related Records Found.
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle CssClass="headerbg" Font-Bold="True" />
                                                        <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                                                    </asp:GridView>
                                                </div>
                                                <div class="deletebtn" style="padding-left: 10px; margin-top: 5px;">
                                                    <asp:Button ID="btnActivate" runat="server" OnClick="btnActivate_Click" Text="Make Active" />
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="TabContainer1$TabCreateGlink$btnGAdd" />
                                </Triggers>
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
<script language="javascript" type="text/javascript">
    function checkSelect(btnid) {
        if (!ConfirmCheck('form1')) {
            return false;
        }

        //            if (btnid == 'TabContainer1_TabActiveGlink_btnDelete') {
        //                return confirm('Are You Sure To Delete?');
        //            }
        if (btnid == 'TabContainer1_TabActiveGlink_btnActivate') {
            return confirm('Are You Sure To Active?');
        }
        if (btnid == 'TabContainer1_TabActiveGlink_btninactive') {
            return confirm('Are You Sure To InActive?');
        }
        if (btnid == 'TabContainer1_TabActiveGlink_btnModify') {
            return confirm('Are You Sure To Modify?');
        }

    }

    function Validation() {
        if (!blankFieldValidation('TabContainer1_TabCreateGlink_txtGlinkName', 'Global Link Name (In English)')) {
            return false;
        }
        if (!SpecialCharcheck('TabContainer1_TabCreateGlink_txtGlinkName')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabContainer1_TabCreateGlink_txtGlinkName')) {
            return false;
        }

        if (!blankFieldValidation('TabContainer1_TabCreateGlink_txtGlinkNameInAmharic', 'Global Link Name (In <%=StrOL%>)')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabContainer1_TabCreateGlink_txtGlinkNameInAmharic')) {
            return false;
        }
        //            if (!ListBoxValidation('TabContainer1_TabCreateGlink_lbUserLoc', 'Location')) {
        //                return false;
        //            }
        //            if (document.getElementById('TabContainer1_TabCreateGlink_fuGlinkImg').value != "") {
        //                var fileextension = new Array('.png');
        //                if (isValidFile(document.getElementById('TabContainer1_TabCreateGlink_fuGlinkImg'), fileextension, "document") == false)
        //                    return false;
        //                else
        //                    return true;
        //            }   
    }


    function dispConfm(btn) {

        var v = document.getElementById(btn);

        if (v.value == "Save") {

            if (Validation() != "") {
                if (confirm("Are You Sure To Save ?")) {
                    return true;
                }
                else
                    return false;
            }
            else {
                return false;
            }
        }
        else if (v.value == "Update") {
            if (Validation() != "") {
                if (confirm("Are You Sure To Update ?")) {
                    return true;
                }
                else
                    return false;
            }
            else {
                return false;
            }
        }
        else if (v.value == "Reset") {
            return confirm("Are You Sure To Reset ?");
        }
        else {
            return confirm("Are You Sure To Cancel ?");
        }
    }

    function isSpecialChar1st(tb) {

        var txt = document.getElementById(tb);
        var str = txt.value;

        var iChars = "*|, \":<>[]{}`\'&;()@&$#%!^-_=+./?";

        if (iChars.indexOf(str.charAt(0)) != -1) {

            txt.value = '';
            txt.focus();
            return false;
        }
    }



    function GetSelected(listbId) {
        var opt;
        document.getElementById("TabContainer1_TabCreateGlink_hidUserLoc").value = "";
        for (i = 0; i < listbId.length; i++) {
            opt = listbId.options[i];
            if ((opt.selected) && (opt.value != 0)) {
                var selected = opt.value;
                document.getElementById("TabContainer1_TabCreateGlink_hidUserLoc").value = document.getElementById("TabContainer1_TabCreateGlink_hidUserLoc").value + ',' + selected;
            }

        }
    }
 
 
 
    </script>
</html>
