<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDefault.aspx.cs" Inherits="AdminDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="uc1" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Console::Hierarchy Manage</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.02)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.02)" />
    <link href="style/default.css" rel="stylesheet" type="text/css" />
    <link href="style/common.css" rel="stylesheet" type="text/css" />

    <script src="jscript48/Validator.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function ShowHideTrDiv(intCnt) {
            if (intCnt == 0) {
                document.getElementById("divFirst").style.display = "none";
            }
            else {
                document.getElementById("divFirst").style.display = "block";
            }

        }
         function displaylvl(tr, txt) {
            var str = (document.getElementById(txt).value);
            for (var i = 1; i <= 7; i++) {
                document.getElementById(tr + parseInt(i)).style.display = "none";
            }
            for (var i = 1; i <= parseInt(str); i++) {
                document.getElementById(tr + parseInt(i)).style.display = "";
            }
        }
         function chkValid(a) {
             var str = (document.getElementById("txtsubnodeNo").value);
            var strnode = '<%=totalNo %>';

            if (a == 'A') {
                if (parseInt(str) == 0) {
                    alert('Please give total subndode value greater than 0');
                    return false;
                }
                else {
                    return true;
                }
            }
            if (a == 'E') {
                 if (parseInt(str) < parseInt(strnode)) {
                    alert('Can not give less value of total subnode number than previous node');
                    document.getElementById("txtsubnodeNo").value=strnode;
                    return false;
                }
                else {                    
                        return true;                  
                    }
            }


        }

        function CheckExist() {
            var strcheck = (document.getElementById("txtsubnodeNo").value);
            var obj = [];
            var flag;
            for (var i = 1; i <= parseInt(strcheck); i++) {
                obj[i] = UpperCase(document.getElementById('txtNdName' + i).value);

            }
            for (var j = 1; j < obj.length; j++) {

                for (var k = j + 1; k < obj.length; k++) {
                    if (UpperCase(obj[j]) == UpperCase(obj[k])) {
                        flag = 'yes';
                        break;
                    }
                }
            }
            if (flag == 'yes') {
                alert('Can not insert duplicate value');
                return false;
            }
            else {
                return true;
            }
        }
         function chkdel() {

            if ('<%=trChildNode %>' == null) {
                alert('Please select a node to delete');
                return false;
            }
            else {
                if (!confirm('Are you sure to delete !!!!'))
                    return false;
            }
        }

         function IsWhitespace1st(txt, msg) {
            var t = (document.getElementById(txt).value);
            for (var j = 0; j < t.length; j++) {
                var alphaa = t.charAt(j);
                var hh = alphaa.charCodeAt(0);
                if (j == 0) {
                    if (hh == 32 || hh == 9) {
                        alert('1st white space is not allowed for ' + msg);
                        return false;
                    }
                }
                else {
                    return true;
                }
            }
            return true;
        }
         function CheckConfirm(btnId) {
              if (btnId.value == "Add") {
                if (!chkValid('A')) {
                    return false;
                }  
                else{                                              
                    var result = confirm('Do You Want to Add Hierarchy');
                    return result;              
                }
            }
            else if (btnId.value == "Edit") {
                if (!chkValid('E')) {
                    return false;
                }
                else{
                    var result = confirm('Do You Want to Edit Hierarchy');
                    return result;              
                }                                  
            }
        }
        
        function chkBlanknode() {           
            var tbl = document.getElementById("tblnode");
            var rowsc = tbl.rows.length;
            var cnt = 0;
            for (var i = 0; i < rowsc; i++) {
                if ((i+1) % 2 != 0) {
                    if ($.trim((tbl.rows[i].cells[2].children[0]).value) == '') {
                        cnt = parseInt(cnt) + 1;
                    }
                }
            }
            if (parseInt(cnt) > 0) {
                alert('Node name(s) can not be left blank');
                return false;
            }
            else {
                return true;
            }
        }

        function chkExistNodes() {

            var tbl = document.getElementById("tblnode");
            var rowsc = tbl.rows.length;
            var cnt = 0;
            var arr = new Array();
            var arr1 = new Array();
            for (var i = 0; i < rowsc; i++) {
                if ($.trim((tbl.rows[i].cells[1].children[0]).value) != '') {
                    arr[i] = (tbl.rows[i].cells[1].children[0]).value;
                }

            }
            var tot = parseInt(arr.length);
            for (j = 0; j < tot; j++) {
                if ($.inArray(arr[j], arr1) == -1) {
                    arr1[j] = arr[j];

                }
                else {
                    cnt = parseInt(cnt) + 1;
                }

            }
            if (parseInt(cnt) > 0) {
                alert('Dupplicate node name(s) entered');
                return false;
            }
            else {
                return true;
            }
        }
        
      
    </script>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
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
                            <table width="99%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" valign="top">
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td valign="top" align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <div id="divFirst" class="addTable" style="border-top: solid 1px #F0F0F0">
                                            <table border="0" cellpadding="2" cellspacing="0">
                                                <tr id="trRnname">
                                                    <td width="140">
                                                        Root Node Name
                                                    </td>
                                                    <td width="7">
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtrootName" runat="server" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="trRnSum">
                                                    <td>
                                                        Root Node Summary
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtrootSummary" runat="server" TextMode="MultiLine" Width="200px"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Total SubNode No.
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtsubnodeNo" runat="server" Width="200px" onkeyup="CallTextChangedEvt();"
                                                            MaxLength="2" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilterExtender1" TargetControlID="txtsubnodeNo"
                                                            runat="server" FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="padding: 0px;">
                                                        <table border="0" cellpadding="2" cellspacing="0" id="tblnode" runat="server">
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="tr1" style="margin-left: 10px;">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" Font-Bold="True" OnClientClick="return CheckConfirm(this);"
                                                OnClick="btnAdd_Click" Width="61px" />
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" Font-Bold="True" OnClientClick="return CheckConfirm(this);"
                                                OnClick="btnEdit_Click" Width="61px" />
                                            <asp:Button ID="btnClear" runat="server" Text="Reset" Font-Bold="True" OnClientClick="return confirm('Do you want to reset the nodes ?');"
                                                OnClick="btnClear_Click" Width="61px" />
                                            <asp:Button Font-Bold="True" ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                                                OnClientClick="return chkdel();" />
                                            <asp:Button ID="btnshow" runat="server" Text="Show" Visible="false" OnClick="btnshow_Click"
                                                Width="61px" />
                                            <asp:Button ID="Button1" runat="server" Style="display: none" OnClick="Button1_Click"
                                                Text="Button" />
                                        </div>
                                    </td>
                                    <td width="3%">
                                    </td>
                                    <td valign="top" align="left" style="width: 230px;">
                                        <asp:Label ID="Label1" runat="server" Text="Level Hierarchy" Font-Bold="true"></asp:Label>
                                        <asp:Panel ID="pnlTreeMenu" runat="server" Style="border: solid 1px black; overflow: auto;
                                            height: auto; background-color: #99CCCF">
                                            <asp:TreeView ID="treemenu" NodeWrap="true" CssClass="tree" runat="server" OnSelectedNodeChanged="treemenu_SelectedNodeChanged"
                                                ShowLines="True">
                                            </asp:TreeView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--#include file="Includes/footer.aspx" -->
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    var refreshParent = function() {
        if (opener && !opener.closed) {
            opener.location.reload();
        }
    };
     function CallTextChangedEvt() {
      
        setTimeout(CreateTextBox, 500);
    }
    function CreateTextBox() {
         var tbId = document.getElementById('<%= txtsubnodeNo.ClientID %>');

        var tbVal = tbId.value;
        if (tbVal == null || tbVal == "") {
        }
        else {
            var totalNo = '<%=totalNo%>';
            if (parseInt(tbVal) < totalNo) {
                chklessno(tbVal);
            }
            else {
                document.getElementById('<%= Button1.ClientID %>').click();
                setCursorPositionToEnd();
                var len = tbVal.length;
            }
        }

    }
    function chklessno(val) {
        var totalNo = '<%=totalNo%>';
        if (parseInt(val) >= parseInt(totalNo)) {
            setCursorPositionToEnd()
            return true;
        }
        else {
            alert('Can not Give Less Value of Total Subnode number than Previous node');
            document.getElementById('<%= txtsubnodeNo.ClientID %>').value=totalNo;
            return false;
        }

    }
    function setCursorPositionToEnd() {

        var inputField = document.getElementById('<%= txtsubnodeNo.ClientID %>');
        if (inputField != null && inputField.value.length != 0) {
            if (inputField.createTextRange) {
                var FieldRange = inputField.createTextRange();
                FieldRange.moveStart('character', inputField.value.length);
                FieldRange.collapse();
                FieldRange.select();
            } else if (inputField.selectionStart || inputField.selectionStart == '0') {
                var elemLen = inputField.value.length;
                inputField.selectionStart = elemLen;
                inputField.selectionEnd = elemLen;
                inputField.focus();
            }
        } else {
            inputField.focus();
        }
    }
</script>

