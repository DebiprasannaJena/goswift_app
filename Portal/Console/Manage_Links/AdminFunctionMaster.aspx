<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Manage_Links_AdminFunctionMaster"
    CodeBehind="AdminFunctionMaster.aspx.cs" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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
    <title>Admin Console::Function Master</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.02)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.02)" />
  

    <script src="../scripts/1.4.2_jquery.min.js" type="text/javascript"></script>

    <script src="../scripts/1.8.1_jquery-ui.min.js" type="text/javascript"></script>

    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .WaterMarkedTextBox
        {
            height: 16px;
            width: 200px;
            padding: 2px 2 2 2px;
            border: 1px solid #BEBEBE;
            background-color: #F0F8FF;
            color: gray;
            font-size: 9pt;
         }
        .serchTxtBox
        {
        	height: 16px;
            width: 200px;
            padding: 2px 2 2 2px;
            border: 1px solid #BEBEBE;
            background-color: #F0F8FF;
            color: black;
             
        }
    </style>
</head>

<script type="text/javascript" language="javascript">
    function checkAddvalidation() {
    debugger;
        if (!blankFieldValidation('TabFunctionDetails_TabCreateFunction_txtFunName', 'Function Name')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabFunctionDetails_TabCreateFunction_txtFunName')) {
            return false;
        }
        if (!blankFieldValidation('TabFunctionDetails_TabCreateFunction_txtFileName', 'File Name')) {
            return false;
        }

        if (!blankFieldValidation('TabFunctionDetails_TabCreateFunction_txtDesc', 'Description')) {
            return false;
        }
        if (!WhiteSpaceValidation1st('TabFunctionDetails_TabCreateFunction_txtDesc')) {
            return false;
        }
        if (!MaxlengthValidation('TabFunctionDetails_TabCreateFunction_txtDesc', 'Description Name', 200)) {
            return false;
        }  
       if(document.getElementById('TabFunctionDetails_TabCreateFunction_chkAdd').checked!=true){
                if(document.getElementById('TabFunctionDetails_TabCreateFunction_chkView').checked!=true){
                    if(document.getElementById('TabFunctionDetails_TabCreateFunction_chkMng').checked!=true){
                        alert('Choose at least one permission.');
                        return false;
                    }
                }
        }                     
//        if (document.getElementById('TabFunctionDetails_TabCreateFunction_FileUpload1').value != "") {
//            var fileextension = new Array('.png', '.jpg', '.pjpeg', '.jpeg', '.gif');
//            if (isValidFile(document.getElementById('TabFunctionDetails_TabCreateFunction_FileUpload1'), fileextension, "document") == false)
//                return false;
//            else
//                return true;
//        }        
         return true;
    }


     function CheckSelect(btnid) {
        if (!ConfirmCheck('form1')) {
            return false;
        }

        if (btnid == 'TabFunctionDetails_TabEditFunction_btnInActive') {
                return confirm('Are You Sure To Inactive It ?');
            }
        if (btnid == 'TabFunctionDetails_TabInActive_btnActive') {
            return confirm('Are You Sure To Activate It ?');
        }
    }
     function conformation(btnId) {

        var btntext = btnId.value;
        if (btntext.toLowerCase() == "save") {
            if (!checkAddvalidation()) {
                return false;
            }
            else {
                return confirm('Do you want to save ?');
            }
        }
        else if (btntext.toLowerCase() == "update") {
            if (!checkAddvalidation()) {
                return false;
            }
            else {
                return confirm('Do you want to Update ?');
            }
        }
        else if (btntext.toLowerCase() == "reset") {
            return confirm('Do you want to Reset ?');
        }
        else {
            return confirm('Do you want to Cancel ?');
        }
    }



     function CheckAdd(tbId) {

         var txt = tbId.value;

        if (txt != "") {
            if (txt.toLowerCase() != "add") {
                alert('Text Add is only valid');
                tbId.value = "";
                tbId.focus();
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }

    }
     function CheckView(tbId) {
         var txt = tbId.value;
        if (txt != "") {
            if (txt.toLowerCase() != "view") {
                alert('Text View is only valid');
                tbId.value = "";
                tbId.focus();
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }
    }
     function CheckManage(tbId) {
         var txt = tbId.value;

        if (txt != "") {
            if (txt.toLowerCase() != "manage") {
                alert('Text Manage is only valid');
                tbId.value = "";
                tbId.focus();
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return false;
        }
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
        
    function SearchText() {
          if(document.getElementById('TabFunctionDetails_TabEditFunction_txtSearchText').value==""){
            document.getElementById("TabFunctionDetails_TabEditFunction_Button2").click();
            document.getElementById('TabFunctionDetails_TabEditFunction_txtSearchText').focus();
        }
        document.getElementById("TabFunctionDetails_TabEditFunction_Button3").click();
 }

 function setCursorPositionToEnd() {
     
      var elementRef = document.getElementById('TabFunctionDetails_TabEditFunction_txtSearchText');
     var cursorPosition = document.getElementById('TabFunctionDetails_TabEditFunction_txtSearchText').value.length;
     cursorPosition = cursorPosition+1
     if ( elementRef != null ) 
     {
      if ( elementRef.createTextRange ) 
          {
               var textRange = elementRef.createTextRange();
               textRange.move('character', cursorPosition);
               textRange.select();
          }
          else 
          {
               if ( elementRef.selectionStart ) 
               {
                elementRef.focus();
                elementRef.setSelectionRange(cursorPosition, cursorPosition);
               }
               else
               {
                elementRef.focus();
                elementRef.setSelectionRange(cursorPosition, cursorPosition);
               }
         }
     }
}
function pageLoad() {
    $(document).ready(function () {
        $('#TabFunctionDetails_TabCreateFunction_txtFunName').focus();
    });
}
</script>

<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
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
                            <uc2:Navigation ID="Navigation1" runat="server" />
                            <cc1:TabContainer ID="TabFunctionDetails" runat="server" Width="100%" ActiveTabIndex="1"
                                CssClass="ajax__tab_yuitabview-theme" AutoPostBack="True" 
                                OnActiveTabChanged="TabFunctionDetails_ActiveTabChanged" 
                                meta:resourcekey="TabFunctionDetailsResource1">
                                <div class="Menubar">
                                    <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateFunction">
                                        <ContentTemplate>
                                            <div class="mandatory">
                                                (* indicates mandatory fields)
                                            </div>
                                            <div class="nodata">
                                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                            </div>
                                            <asp:UpdatePanel ID="upFuncManage" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <div class="addTable">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td valign="middle">
                                                                    <table border="0" cellpadding="0" cellspacing="0" class="FormBorder">
                                                                        <tr>
                                                                            <td width="150px">
                                                                                Function Name
                                                                            </td>
                                                                            <td width="5" align="center" valign="middle">
                                                                               <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFunName" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                <font color="#FF0000">*</font>
                                                                                <cc1:FilteredTextBoxExtender ID="FilterFunname" runat="server" TargetControlID="txtFunName"
                                                                                    FilterType="LowercaseLetters,UppercaseLetters,Custom,Numbers" ValidChars="_/&-+.[]{}() ">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td rowspan="3">
                                                                                <br />
                                                                                <table align="center" class="style1">
                                                                                    <tr>
                                                                                        <td align="center">
                                                                                            <asp:Image ID="imgInner" ImageUrl=" " runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                                                                BorderColor="Black" Height="85px" Visible="false" Width="92px" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center">
                                                                                            <asp:Label ID="lblImage" ForeColor="BlueViolet" runat="server" Visible="false"></asp:Label>
                                                                                            <asp:HiddenField ID="hidImgUrl" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center">
                                                                                            <asp:ImageButton ID="imgBtnDel" Visible="false" OnClientClick="return confirm('Are you sure to delete the image ?');"
                                                                                                OnClick="imgBtnDel_Click" ImageUrl="~/Console/images/DeleteIcn.gif" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 19px">
                                                                                File Name
                                                                            </td>
                                                                            <td align="center" valign="middle" style="height: 19px">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td style="height: 19px">
                                                                                <asp:TextBox ID="txtFileName" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                <font color="#FF0000">*</font>
                                                                                <cc1:FilteredTextBoxExtender ID="filename" runat="server" TargetControlID="txtFileName"
                                                                                    FilterType="LowercaseLetters,UppercaseLetters,Custom,Numbers" ValidChars="_/.(){}[]+-">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 59px">
                                                                                Description
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                               <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDesc" runat="server" Height="53px" MaxLength="200" TextMode="MultiLine"
                                                                                    Width="200px"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDesc"
                                                                                    FilterType="LowercaseLetters,UppercaseLetters,Custom,Numbers"  ValidChars=" _/.(){}[]+-">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <font color="#FF0000">*</font> &nbsp Maximum <span class="mandatory">
                                                                                    <asp:Label ID="lblMaxcounter" Text="200" runat="server"></asp:Label>
                                                                                </span>Characters Allowed.
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="trFunc">
                                                                            <td style="height: 22px">
                                                                                Permission
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                 <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkAdd" Text="Add" runat="server" />
                                                                                <asp:CheckBox ID="chkView" Text="View" runat="server" />
                                                                                <asp:CheckBox ID="chkMng" Text="Manage" runat="server" /><font color="#FF0000">*</font>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                External Mail Required
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td valign="middle">
                                                                                <asp:RadioButton ID="rdYesM" runat="server" GroupName="mail" Text="Yes" />
                                                                                &nbsp;
                                                                                <asp:RadioButton ID="rdNoM" runat="server" GroupName="mail" Text="No" />
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                            <td valign="middle">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        <tr>
                                                                            <td>
                                                                                Freebees
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                               <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                <asp:RadioButton ID="rdYesF" runat="server" Checked="True" GroupName="a" Text="Yes" />
                                                                                &nbsp;
                                                                                <asp:RadioButton ID="rdNoF" runat="server" GroupName="a" Text="No" />
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Portlet File Name
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPortlet" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="portelet" runat="server" TargetControlID="txtPortlet"
                                                                                    FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars="_/.-+{}()[]">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="6">
                                                                            <td>
                                                                                Active
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                 <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                <asp:RadioButton ID="rdYesA" runat="server" Checked="True" GroupName="c" Text="Yes" />
                                                                                &nbsp;&nbsp;
                                                                                <asp:RadioButton ID="rdNoA" runat="server" GroupName="c" Text="No" />
                                                                                <font color="#FF0000">*</font><span class="noteText" style="color: #ff0000"></span>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Re-Create Xml
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="cbReCXml" runat="server" />
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
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
                                                                                <asp:Button ID="btnAdd" runat="server" Text="Save" OnClick="btnAdd_Click" OnClientClick="return conformation(this);" />
                                                                                <asp:Button ID="btnReset" OnClientClick="return conformation(this);" runat="server"
                                                                                    Text="Reset" OnClick="btnReset_Click" />
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnAdd" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="ACTIVE" ID="TabEditFunction" TabIndex="1">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="nodata" align="center">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#C00000"></asp:Label>
                                                    </div>
                                                    <div style="margin-right: 7px; height: 20px">
                                                        <table border="0" align="right">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lbtnAll" Visible="false" runat="server" Text="All" OnClick="lbtnAll_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellpadding="0" cellspacing="0" style="margin-bottom: 3px">
                                                        <tr>
                                                            <td width="160px:">
                                                                <span style="padding: 0 0 5px 10px; font-family: Arial, Helvetica, sans-serif; font-weight: bold;
                                                                    font-size: small; color: #61617b">Type Function Name</span>
                                                            </td>
                                                            <td width="8px:">
                                                               <strong>:</strong>
                                                            </td>
                                                            <td width="210px:">
                                                                <asp:TextBox ID="txtSearchText" CssClass="serchTxtBox" Font-Bold="true" onkeyup="SearchText()" Width="150px" class="autosuggest"
                                                                    runat="server"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="txtSearchText_TextBoxWatermarkExtender" runat="server"
                                                                    Enabled="True" TargetControlID="txtSearchText" WatermarkText="Type Search Text....."
                                                                    WatermarkCssClass="WaterMarkedTextBox">
                                                                </cc1:TextBoxWatermarkExtender>
                                                                <asp:Button ID="Button1" Style="display: none" runat="server" OnClick="Button1_Click"
                                                                    Text="Button" />
                                                                <asp:Button ID="Button2" Style="display: none" runat="server" OnClick="Button2_Click"
                                                                    Text="Button" />
                                                                <asp:Button ID="Button3" Style="display: none" runat="server" OnClick="Button3_Click"
                                                                    Text="Button" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div class="viewTable">
                                                        <asp:HiddenField ID="hidFuncId" runat="server" />
                                                        <asp:GridView ID="GVFunction" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                            CellSpacing="0" HeaderStyle-Font-Bold="True" ItemStyle-VerticalAlign="Top" PagerStyle-HorizontalAlign="Right"
                                                            PagerStyle-Mode="NumericPages" PagerStyle-PageButtonCount="10" PageSize="10"
                                                            AllowPaging="true" OnPageIndexChanging="GVFunction_PageIndexChanging" DataKeyNames="FunctionId"
                                                            OnRowDataBound="GVFunction_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <input type="checkbox" name="cbAll1" value="cbAll1" onclick="SelectAll(cbAll1,'GVFunction','form1')" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll1,'GVFunction','form1')" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="15"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Sl No." HeaderStyle-Width="35"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Function Name" DataField="FunctionName" />
                                                                <asp:BoundField HeaderText="File Name" DataField="FileName" />
                                                                <asp:BoundField HeaderText="Description" DataField="Description" />
                                                                <asp:BoundField HeaderText="Add" DataField="FAdd" />
                                                                <asp:BoundField HeaderText="View" DataField="FView" />
                                                                <asp:BoundField HeaderText="Manage" DataField="FManage" />
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Edit
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="hypEdit" ImageUrl="../images/editIcon.gif" runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                               <EmptyDataTemplate>
                                                                <div style="height:20px; padding:5px; font-weight:bold; font-size:medium; color:silver">
                                                                No Active Function Related Records Found.
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <PagerStyle CssClass="paging" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="deletebtn" style="padding-left: 10px; margin-top: 5px">
                                                        <asp:HiddenField ID="hidval" runat="server" />
                                                        <asp:Button ID="btnInActive" runat="server" Text="Make InActive" OnClick="btnInActive_Click"
                                                            OnClientClick="return CheckSelect('TabFunctionDetails_TabEditFunction_btnInActive');" />
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="INACTIVE" ID="TabInActive" TabIndex="2">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="nodata" align="center">
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#C00000"></asp:Label>
                                                    </div>
                                                    <div style="margin-right: 7px; height: 20px">
                                                        <table border="0" align="right">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="LnkbtnAllin" Visible="false" OnClick="LnkbtnAllin_Click" runat="server"
                                                                        Text="All"></asp:LinkButton>
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
                                                        <asp:GridView ID="GvInActive" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                            CellSpacing="0" HeaderStyle-Font-Bold="True" ItemStyle-VerticalAlign="Top" PagerStyle-HorizontalAlign="Right"
                                                            PagerStyle-Mode="NumericPages" PagerStyle-PageButtonCount="10" PageSize="10"
                                                            AllowPaging="true" OnPageIndexChanging="GvInActive_PageIndexChanging" DataKeyNames="FunctionId"
                                                            EmptyDataText="No Records" OnRowDataBound="GvInActive_RowDataBound" PagerStyle-CssClass="paging">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <input type="checkbox" name="cbAll2" value="cbAll2" onclick="SelectAll(cbAll2,'GvInActive','form1')" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll2,'GvInActive','form1')" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="15"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Serial No" HeaderStyle-Width="55"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Function Name" DataField="FunctionName" HeaderStyle-Width="120" />
                                                                <asp:BoundField HeaderText="File Name" DataField="FileName" HeaderStyle-Width="150" />
                                                                <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-Width="120" ItemStyle-Width="20" ItemStyle-Wrap="true"/>
                                                                <asp:BoundField HeaderText="Add" DataField="FAdd" HeaderStyle-Width="40" />
                                                                <asp:BoundField HeaderText="View" DataField="FView" HeaderStyle-Width="40" />
                                                                <asp:BoundField HeaderText="Manage" DataField="FManage" />
                                                            </Columns>
                                                             <EmptyDataTemplate>
                                                                <div style="height:20px; padding:5px; font-weight:bold; font-size:medium; color:silver">
                                                                No InActive Function Related Records Found.
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <PagerStyle CssClass="paging" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="deletebtn" style="padding-left: 10px; margin-top: 5px">
                                                        <asp:Button ID="btnActive" runat="server" Text="Make Active" OnClick="btnActive_Click"
                                                            OnClientClick="return CheckSelect('TabFunctionDetails_TabInActive_btnActive');" />
                                                    </div>
                                                </ContentTemplate>
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
