<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs" Inherits="AdminApp_UI_Console_Manage_Password" EnableEventValidation="false" %>

<%@ Register Src="~/Console/Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc1" %>
<%@ Register Src="~/Console/Includes/printpage.ascx" TagName="printpage" TagPrefix="uc2" %>
<%@ Register Src="~/Console/FillHierarchy.ascx" TagName="FillHierarchy" TagPrefix="uc3" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="hdr" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Console::Location User Report</title>
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/Validator.js" type="text/javascript"></script>

    <script src="../scripts/PopulateHiearchyajax.js" type="text/javascript"></script>
        <script type="text/javascript" language="javascript" src="../scripts/md5.js"></script>
    <script src="../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <style>
        .modalBackground
        {
            background-color: Silver;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
    </style>
    <script type="text/javascript">
        function CheckSelect() {
            var count = 0;
            for (var i = 0; i < document.forms[0].elements.length; i++) {
                if (document.forms[0].elements[i].checked == true && document.forms[0].elements[i].type == "radio") {
                   count = count + 1;
                 }
            }
         if(count==1) {
             return confirm('Are You Sure To Reset Password?');
         }
            else {
                alert('Please Select A user');
                return false;

            }
        }
        function onload() {

            var login = document.getElementById("hidmsg").value;
            if (login != "") {
                alert("Invalid User Name or Password");
            }
            document.getElementById("txtusr").focus();
        }
        function randomtext() {

            var the_number = Math.floor(Math.random() * 500);
            return (the_number)
        }
        function Validation() {
            var str2;
            var slt;

            slt = randomtext();
            document.getElementById("hidSlt").value = slt;
            str2 = hex_md5(document.getElementById("txtpwd").value).toUpperCase() + slt;
            document.getElementById("txtpwd").value = hex_md5(str2).toUpperCase();
            if (!blankFieldValidation('txtusr', 'UserId')) {
                document.getElementById("txtusr").focus();
                return false;
            }
         
            if (!blankFieldValidation('txtpwd', 'Password')) {
                document.getElementById("txtpwd").focus();
                return false;
            }
          
            if (!DropDownValidation('HierarchyForAllLocation2_sdrplayers0', 'Location')) {

                return false;
            }
            
        }
        function CheckOtherIsCheckedByGVID(spanChk) {

            var IsChecked = spanChk.checked;



            var CurrentRdbID = spanChk.id;

            var Chk = spanChk;

            Parent = document.getElementById("<%=grdUsersInfo.ClientID%>");

            var items = Parent.getElementsByTagName('input');

            for (i = 0; i < items.length; i++) {

                if (items[i].id != CurrentRdbID && items[i].type == "radio") {

                    if (items[i].checked) {

                        items[i].checked = false;


                    }

                }

            }

        }
    </script>

</head>
<body onload="onload()">
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
                            <uc1:Navigation ID="Navigation1" runat="server" />
                            <div align="right" class="back">
                                &nbsp;</div>
                            <div class="addTable">
                            
                                  
                                <table cellpadding="0" cellspacing="0" border="0">
                              
                                 <tr>
                                                                        
                                                                            <td align="left" width="150">
                                                                                User Name
                                                                            </td>
                                                                            <td align="left" width="5px">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                                  <asp:TextBox  
                                            ID="txtusr" runat="server"  AutoCompleteType="disabled" onPaste="return false"
                                            Width="175px"></asp:TextBox>  <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr> 
                                    <tr>
                                                                           
                                                                            <td align="left">
                                                                                Password
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                                 <asp:TextBox 
                                            ID="txtpwd" runat="server" AutoCompleteType="disabled" onPaste="return false"
                                            TextMode="Password" Width="175px"></asp:TextBox>  <font color="#FF0000">*</font>
                                                                                <asp:HiddenField ID="hidSlt" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                          <tr>
                                <td colspan="3">
                                    <uc3:FillHierarchy ID="HierarchyForAllLocation2" runat="server"></uc3:FillHierarchy>
                                </td>
                                </tr>
                                  <tr>
                                                        <td width="150">
                                                            Search By Employee
                                                        </td>
                                                        <td>
                                                           <strong>:</strong>
                                                        </td>
                                                        <td style="padding-left: 8px;">
                                                            <asp:TextBox ID="txtSearch" runat="server" MaxLength="100" Width="175px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                    <tr>
                                        <td width="150">
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="padding-left: 8px;">
                                            <asp:Button ID="btnShow" OnClientClick="return Validation();" runat="server" Text="Show"
                                                OnClick="btnShow_Click" />
                                                 <input name="hidden" type="hidden" id="hidmsg" runat="server" />
                                        </td>
                                       <td style="width: 100px;" align="left">
                                          
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
                                                    <asp:LinkButton ID="LnkbtnAllin" Visible="False" runat="server" Text="All" Font-Bold="False"
                                                        Font-Size="Smaller" OnClick="LnkbtnAllin_Click"></asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblpage" runat="server" Visible="False" Font-Size="Smaller"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="viewTable" class="viewTable">
                                        <div style="margin-left: 10px; margin-bottom: 5px;">
                                            <asp:Label ID="lblLoc" Visible="false" runat="server" Text="Location : "></asp:Label>
                                            <asp:Label ID="lblLocatioName" Visible="false" runat="server" Text="Label"></asp:Label></div>
                                        <asp:GridView ID="grdUsersInfo" OnPageIndexChanging="grdUsersInfo_PageIndexChanging"
                                            runat="server" EmptyDataText="No User Data Available" BorderWidth="1px" AutoGenerateColumns="False"
                                            Width="100%" DataKeyNames="GetID,UserName" AllowPaging="True" OnRowDataBound="grdUsersInfo_RowDataBound">
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" NextPageText="Next" FirstPageText="First"
                                                LastPageText="Last" PreviousPageText="Prev" Position="TopAndBottom" />
                                            <Columns>
                                              <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                       
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                       <asp:RadioButton ID="rbtChk" runat="server"   
                                                  onclick="javascript:CheckOtherIsCheckedByGVID(this);"/>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="15px" />
                                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="SlNo.">
                                                    <HeaderStyle Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Employee Name" DataField="FullName">
                                                    <HeaderStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="User Name" DataField="UserName">
                                                    <HeaderStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Designation" DataField="DesignationID">
                                                    <HeaderStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Department" DataField="DepartmentID">
                                                    <HeaderStyle Width="120px" />
                                                </asp:BoundField>
                                              
                                            </Columns>
                                            <HeaderStyle Font-Bold="True" />
                                            <PagerStyle CssClass="paging noPrint" />
                                        </asp:GridView>
                                    </div>
                                     <asp:Button ID="btnReset"  runat="server" Visible="false"
                            Text="Reset Password" onclick="btnReset_Click"  OnClientClick="return CheckSelect();"   />&nbsp;
                                    <asp:Label ID="lblAlert" runat="server" Text="Alert Type:" Visible="false"></asp:Label> 
                                    <asp:CheckBox ID="chkEmail" runat="server" Text="Mail"  Visible="false" Checked="true" /> 
                                    <asp:CheckBox ID="chkSMS" runat="server" Text=" SMS"  Visible="false"/>
                                 
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnShow" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                               
                        </div>
                        <asp:Panel ID="pnlPreview" runat="server" HorizontalAlign="Center" Style="display: none;
                                                            width: 40%">
                                                            <div class="divheading3">
                                                              
                                                                <div style="float: right; width: 9%; text-align: right;">
                                                                    <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Console/images/delete_img.png" />
                                                                </div>
                                                            </div>
                                                            <div class="divcontent3">
                                                                <div class="divcontent3_div1">
                                                                 
                                                                    <div  style="float:left; width:60%; text-align:left">
                                                                        <asp:Label ID="lblMsgModal" runat="server" Font-Bold="true" ForeColor="Green" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div id="div1" class="addTable" style="border: solid 1px silver; border-top: 0; background-color: #F0F0F0;
                                                                    text-align: left; margin-left: 150px; width: 69%;Height:100px">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="border: solid 0px silver">
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                       
                    </td>
                </tr>
            </table>
        </div>
        <!--#include file="../Includes/footer.aspx" -->
    </div>
    </form>
</body>
