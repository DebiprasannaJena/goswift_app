<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Manage_User_DesignationMaster"
    CodeBehind="DesignationMaster.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc4" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="hdr" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

 

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Console::Designation Master</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/AjaxScript.js" type="text/javascript"></script>

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function checkvalidation() {
        if (!ListBoxValidation('TabadminDesignationMaster_TabCreateDesignation_lbLocation', 'Location')) {
                return false;
            }
            var opt;
            var count=0;
            var type='';
            var listbId = document.getElementById('TabadminDesignationMaster_TabCreateDesignation_lbLocation');
            for (i = 0; i < listbId.length; i++) {
                opt = listbId.options[i];
                if (opt.selected) {
                    count=count+1;
                    if((opt.value == 'All'))
                    {
                    type='All';
                    }
                }
                
                if((type == 'All')&& (count>1))
                {
                alert('Cannot select multiple location while All is selected');
                return false;
                }
                
            }
            if (!blankFieldValidation('TabadminDesignationMaster_TabCreateDesignation_txtdesignation', 'Designation Name')) {
                return false;
            }           
            if (!WhiteSpaceValidation1st('TabadminDesignationMaster_TabCreateDesignation_txtdesignation')) {
                return false;
            }           
            if (!blankFieldValidation('TabadminDesignationMaster_TabCreateDesignation_txtalias', 'Alias Name')) {
                return false;
            }
            if (!WhiteSpaceValidation1st('TabadminDesignationMaster_TabCreateDesignation_txtalias')) {
                return false;
            }
            var strAlias = document.getElementById('TabadminDesignationMaster_TabCreateDesignation_txtalias').value;
            if (strAlias.charAt(0) == '.') {
                alert("Dot symbe should not be used at initial position");
                document.getElementById('TabadminDesignationMaster_TabCreateDesignation_txtalias').focus();
                return false;
            }           
            if (!DropDownValidation('TabadminDesignationMaster_TabCreateDesignation_ddlUsertype', 'User Type')) {
                return false;
            }            
            return true;
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
         function dispConfm(btnId) {
            if (btnId.value == "Save") {
                if (!checkvalidation()) {
                    return false;
                }
                else {
                    return confirm("Are You Sure Want To Save ?")
                }
            }
            else if (btnId.value == "Update") {
                if (!checkvalidation()) {
                    return false;
                }
                else {
                    return confirm("Are You Sure Want To Update ?")
                }
            }
            else if (btnId.value == "Reset") {
                return confirm("Are You Sure Want To Reset ?")
            }
            else {
                return confirm("Are You Sure Want To Cancel ?")
            }

        }
         function GetSelected(listbId) {
            var opt;
            document.getElementById("TabadminDesignationMaster_TabCreateDesignation_hidUserLoc").value = "";
            for (i = 0; i < listbId.length; i++) {
                opt = listbId.options[i];
                if ((opt.selected) && (opt.value != 0)) {
                    var selected = opt.value;
                    document.getElementById("TabadminDesignationMaster_TabCreateDesignation_hidUserLoc").value = document.getElementById("TabadminDesignationMaster_TabCreateDesignation_hidUserLoc").value + ',' + selected;
                }
            }
        }
        function BindLocation() {
            var listbId = document.getElementById("TabadminDesignationMaster_TabCreateDesignation_lbLocation");

            var str = document.getElementById("TabadminDesignationMaster_TabCreateDesignation_hidUserLoc").value;

            var locIds = str.split(/[\s,]+/);

            for (i = 0; i < listbId.length; i++) {
                opt = listbId.options[i];


                for (j = 0; j < locIds.length; j++) {
                    if (locIds[j] == opt.value) {

                        opt.selected = true;
                    }  
                }  

            } 
        }
    </script>

    <script src="../scripts/jquery-1.4.3.min.js" type="text/javascript"></script>

    <script src="../scripts/jquery.tablesorter.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        jQuery(document).ready(function() {
        $("#TabadminDesignationMaster_TabViewDesignation_GridDesignation").tablesorter({ debug: false, widgets: ['zebra'], sortList: [[0, 0]] });
        }); 
    </script>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:HiddenField ID="HiddenField1" runat="server" />
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
                                    <cc1:TabContainer runat="server" ID="TabadminDesignationMaster" Width="100%" ActiveTabIndex="0"
                                        CssClass="ajax__tab_yuitabview-theme" OnActiveTabChanged="TabadminDesignationMaster_ActiveTabChanged"
                                        AutoPostBack="true">
                                        <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateDesignation" TabIndex="0">
                                            <HeaderTemplate>
                                                CREATE
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <div class="nodata" align="center">
                                                </div>
                                                <div class="mandatory">
                                                    (<font color="#FF0000">*</font> indicates mandatory field)</div>
                                                <div class="addTable">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td width="150">
                                                                Location
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:ListBox ID="lbLocation" onchange="GetSelected(this);" runat="server" SelectionMode="Multiple"
                                                                    Width="160px" Height="80px"></asp:ListBox>
                                                                &nbsp;<font color="#FF0000">&nbsp; *&nbsp;&nbsp;&nbsp; </font>
                                                                <asp:Label ID="Label1" runat="server" Text="Press alt+ctrl to select multiple"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Designation Name
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtdesignation" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender runat="server" ID="designaiton" ValidChars=".() " TargetControlID="txtdesignation"
                                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters" Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                                &nbsp; <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Alias Name
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtalias" runat="server" MaxLength="10" Width="150px"></asp:TextBox>
                                                                &nbsp; <font color="#FF0000">*</font>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextAlias" runat="server" Enabled="True"
                                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txtalias"
                                                                    ValidChars=". ">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Type
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlUsertype" runat="server" Width="160px">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                    <asp:ListItem Value="P">Permanent</asp:ListItem>
                                                                    <asp:ListItem Value="T">Temporary</asp:ListItem>
                                                                </asp:DropDownList>
                                                                &nbsp; <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:HiddenField ID="hidUserLoc" runat="server" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" OnClientClick="return dispConfm(this);"
                                                                    Text="Save" />
                                                                &nbsp;&nbsp;
                                                                <asp:Button ID="btncancel" runat="server" OnClientClick="return dispConfm(this);"
                                                                    OnClick="btncancel_Click" Text="Reset" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="VIEW" ID="TabViewDesignation" TabIndex="1">
                                            <HeaderTemplate>
                                                VIEW
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <div style="text-align: right; height: 20px">
                                                    <table border="0" align="right">
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="LnkbtnAllin" runat="server" OnClick="LnkbtnAllin_Click1" Text="All"
                                                                    Visible="False"></asp:LinkButton>
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
                                                    <asp:GridView ID="GridDesignation" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        CellPadding="0" DataKeyNames="DesignationID" EmptyDataText="No Records Found"
                                                        ItemStyle-VerticalAlign="Top" PageSize="10" OnPageIndexChanging="GridDesignation_PageIndexChanging"
                                                        OnRowDataBound="GridDesignation_RowDataBound" PagerStyle-Mode="NumericPages"
                                                        PagerStyle-PageButtonCount="10">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <input name="cbAll" onclick="SelectAll(cbAll,'GridDesignation','form1')" type="checkbox"
                                                                        value="cbAll" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll,'GridDesignation','form1')" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="25px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sl No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSlno" runat="server" Text=""></asp:Label>
                                                                
                                                                </ItemTemplate>
                                                                <ItemStyle Width="40px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Designation" DataField="Designame" NullDisplayText="N/A" />
                                                            <asp:BoundField HeaderText="Location" DataField="LocId" NullDisplayText="All" />
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Edit
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="Hypedit" runat="server" ToolTip="Edit"> <img src="../images/editIcon.gif" border="0" align="absmiddle" /> </asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="40px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle Font-Bold="True" />
                                                        <PagerStyle CssClass="paging" />
                                                    </asp:GridView>
                                                </div>
                                                <div class="deletebtn" style="padding-left: 10px">
                                                    <asp:Button ID="btninDel" runat="server" Text="Delete" Width="65px" OnClick="btninDel_Click"
                                                        OnClientClick="return checkSelect();" />
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
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
