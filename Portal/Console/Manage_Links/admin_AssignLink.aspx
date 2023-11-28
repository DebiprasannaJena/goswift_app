<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="admin_AssignLink.aspx.cs"
    Inherits="Admin_Manage_Links_admin_AssignLink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Console/Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc1" %>
<%@ Register Src="~/Console/FillHierarchy.ascx" TagName="FillHierarchy" TagPrefix="uc2" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc2" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="uc1" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Console::Assign Links</title>
    <link href="../style/default.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/1.3.2_jquery.min.js" type="text/javascript"></script>

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/PopulateHiearchyajax.js" type="text/javascript"></script>

    <style type="text/css">
        .style2
        {
        }
        .style4
        {
            color: #FF0000;
        }
        .style7
        {
        }
        .style8
        {
            width: 100%;
        }
    </style>

    <script type="text/javascript">
       
        function validation() {    
        debugger
            if (!DropDownValidation('TabContainer1_TabAssignLink_ddlGlobalLink', 'global link ')) {
                return false;
            }
            if (!DropDownValidation('TabContainer1_TabAssignLink_ddlPrimaryLink', 'primary link ')) {
                return false;
            }
            if(document.getElementById('TabContainer1_TabAssignLink_rbtDesig').checked==true){
                if (document.getElementById('ddlLocationList').selectedIndex==0) {
                    alert('Please Select Location');
                    return false;
                }
                if (document.getElementById('ddlDesignation').selectedIndex==0) {
                   alert('Please Select Designation');
                    return false;
                }
                if (document.getElementById('TabContainer1_TabAssignLink_hidSelectUser').value == null || document.getElementById('TabContainer1_TabAssignLink_hidSelectUser').value == '') {
                        alert('Please select a user ');
                        return false;
                    }
                else {
                    return true;
                }
            } 
           if(document.getElementById('TabContainer1_TabAssignLink_rbtDept').checked==true){
                if (!DropDownValidation('TabContainer1_TabAssignLink_FillLocationHierarchy_sdrplayers0', 'location ')) {
                    return false;
                }
                if (document.getElementById('TabContainer1_TabAssignLink_hidSelectedLevels').value == null || document.getElementById('TabContainer1_TabAssignLink_hidSelectedLevels').value == '') {
                    if (document.getElementById('TabContainer1_TabAssignLink_hidSelectUser').value == null || document.getElementById('TabContainer1_TabAssignLink_hidSelectUser').value == '') {
                        alert('Please select a user or a level data !!');
                        return false;
                    }
                    else {return true;}                                            
                }
                else { return true;}                                   
            }
            if(document.getElementById('TabContainer1_TabAssignLink_rbtLoc').checked==true){
                if (document.getElementById('TabContainer1_TabAssignLink_hidSelectedLevels').value == null || document.getElementById('TabContainer1_TabAssignLink_hidSelectedLevels').value == '') {
                        alert('Please select a location ');
                        return false;
                    }
                else {return true;}                                    
            }            
            return true;
        }
        function CheckConfirm(btnId) {
        debugger;
            if (btnId.value == "Update") {
                if (!validation()) {
                    return false;
                }
                else {
                    return confirm('Do you want to update ?');
                }
            }
            else {
                return confirm('Do you want to Reset ?');
            }
        }
        
      
     function ChangeLabel() {
     debugger;
 
        //var radiolist = document.getElementById('TabContainer1_TabAssignLink_rbtBtnPermitDeny');
         var radioButtons = document.getElementById('TabContainer1_TabAssignLink_rbtBtnPermitDeny');
            var inputs = radioButtons.getElementsByTagName("input");
            var selected;
            for (var i = 0; i < inputs.length; i++) {
                 if (inputs[i].checked) {
                    if(inputs[i].value=="Deny"){
                         document.getElementById('TabContainer1_TabAssignLink_lblPermissionDeny').innerHTML="Deny For";  
                    }else{
                         document.getElementById('TabContainer1_TabAssignLink_lblPermissionDeny').innerHTML="Permission For";  
                    }                      
                     break;
                  }
            }   
        }
   
    </script>

</head>
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
                            <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
                                <cc1:TabPanel runat="server" HeaderText="Assign Link" ID="TabAssignLink">
                                    <HeaderTemplate>
                                        Assign Link
                                    
                                    
                                </HeaderTemplate>
                                    
<ContentTemplate>
                                        <div class="mandatory">
                                            ( * indicates mandatory fields)</div>
                                        <div class="addTable">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="150">
                                                        <span class="style4">*</span> Assign Link By
                                                    </td>
                                                    <td>
                                                        <strong>:</strong>
                                                    </td>
                                                    <td style="padding-left: 4px;">
                                                        <asp:RadioButton ID="rbtDept" Text="Department" AutoPostBack="True" Checked="True"
                                                            GroupName="a" runat="server" OnCheckedChanged="rbtDept_CheckedChanged" />

                                                        <asp:RadioButton ID="rbtDesig" Text="Designation" GroupName="a" runat="server" />

                                                        <asp:RadioButton ID="rbtLoc" runat="server" GroupName="a" Text="Location" />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="150">
                                                        <span class="style4">*</span> Access Type
                                                    </td>
                                                    <td>
                                                        <strong>:</strong>
                                                    </td>
                                                    <td style="padding-left: 0px;">
                                                        <asp:RadioButtonList ID="rbtBtnPermitDeny" runat="server" onclick="ChangeLabel();"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True">Permit &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
<asp:ListItem>Deny</asp:ListItem>
                                                        </asp:RadioButtonList>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="style4">*</span> Global Link
                                                    </td>
                                                    <td>
                                                        <strong>:</strong>
                                                    </td>
                                                    <td style="padding-left: 6px;">
                                                        <asp:DropDownList ID="ddlGlobalLink" onchange="BindPlink();"   runat="server"
                                                            Width="185px" onselectedindexchanged="ddlGlobalLink_SelectedIndexChanged1">
                                                            <asp:ListItem>-Select-</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="style4">*</span> Primary Link
                                                    </td>
                                                    <td>
                                                        <strong>:</strong>
                                                    </td>
                                                    <td style="padding-left: 6px;">
                                                        <asp:DropDownList ID="ddlPrimaryLink" onchange="GetSelected(this,'TabContainer1_TabAssignLink_hidPlinkId');"
                                                            runat="server" Width="185px"><asp:ListItem>-Select-</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="divDept" runat="server" style="display: block">
                                                <uc2:FillHierarchy ID="FillLocationHierarchy" runat="server" />

                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr runat="server" id="lvlTr" style="display: none">
                                                        <td class="style7" colspan="3" runat="server">
                                                            <div>
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td width="150px" rowspan="2">
                                                                            <span class="style4">*</span>
                                                                            <asp:Label ID="lblLevelName" runat="server" Text="Label"></asp:Label>

                                                                        </td>
                                                                        <td rowspan="2">
                                                                            <strong>:</strong>
                                                                        </td>
                                                                        <td rowspan="2" style="padding-left: 6px;">
                                                                            <asp:ListBox ID="lbSelectLevels" onchange="GetSelected(this,'TabContainer1_TabAssignLink_hidGroupDeptId');"
                                                                                runat="server" Height="90px" Width="140px" SelectionMode="Multiple"><asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                            </asp:ListBox>

                                                                        </td>
                                                                        <td align="center" onclick="LstBoxAdd('TabContainer1_TabAssignLink_lbShowLevels','TabContainer1_TabAssignLink_lbSelectLevels','TabContainer1_TabAssignLink_hidSelectedLevels','TabContainer1_TabAssignLink_hidShowLevels');"
                                                                            valign="middle">
                                                                            <img alt="" src="../images/left-down-blue.gif" title="Move To Left" style="height: 13px;
                                                                                width: 15px" />
                                                                        </td>
                                                                        <td rowspan="2">
                                                                            <asp:ListBox ID="lbShowLevels" runat="server" Height="90px" Width="140px" SelectionMode="Multiple"><asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                            </asp:ListBox>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" onclick="LstBoxRemove('TabContainer1_TabAssignLink_lbShowLevels','TabContainer1_TabAssignLink_lbSelectLevels','TabContainer1_TabAssignLink_hidSelectedLevels','TabContainer1_TabAssignLink_hidShowLevels');"
                                                                            valign="middle">
                                                                            <img src="../images/arrow-down-blue.gif" title="Move To Right" width="14" height="13"
                                                                                alt="Laeft Arrow" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
</tr>

                                                    <tr runat="server" id="userTr" style="display: none">
                                                        <td class="style7" colspan="3" runat="server">
                                                            <div>
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td width="150px" rowspan="2">
                                                                            <span class="style4">*</span>
                                                                            <asp:Label ID="Label2" runat="server" Text="User"></asp:Label>

                                                                        </td>
                                                                        <td rowspan="2">
                                                                            <strong>:</strong>
                                                                        </td>
                                                                        <td rowspan="2" style="padding-left: 6px;">
                                                                            <asp:ListBox ID="lbSelectUser" runat="server" Height="90px" onchange="GetSelected(this,'TabContainer1_TabAssignLink_hidGroupUserId');"
                                                                                SelectionMode="Multiple" Width="140px"><asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                            </asp:ListBox>

                                                                        </td>
                                                                        <td align="center" onclick="LstBoxAdd('TabContainer1_TabAssignLink_lbShowUser','TabContainer1_TabAssignLink_lbSelectUser','TabContainer1_TabAssignLink_hidSelectUser','TabContainer1_TabAssignLink_hidShowUser');"
                                                                            valign="middle">
                                                                            <img alt="" src="../images/left-down-blue.gif" title="Move To Left" style="height: 13px;
                                                                                width: 15px" />
                                                                        </td>
                                                                        <td rowspan="2">
                                                                            <asp:ListBox ID="lbShowUser" runat="server" Height="90px" Width="140px" SelectionMode="Multiple"><asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                            </asp:ListBox>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" onclick="LstBoxRemove('TabContainer1_TabAssignLink_lbShowUser','TabContainer1_TabAssignLink_lbSelectUser','TabContainer1_TabAssignLink_hidSelectUser','TabContainer1_TabAssignLink_hidShowUser');"
                                                                            valign="middle">
                                                                            <img src="../images/arrow-down-blue.gif" title="Move To Right" width="15" height="13"
                                                                                alt="Laeft Arrow" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
</tr>

                                                </table>
                                            </div>

                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="3">
                                                        <div id="divDesig" runat="server" style="display: none">
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="150">
                                                                        <span class="style4">*</span> Location
                                                                    </td>
                                                                    <td>
                                                                        <strong>:</strong>
                                                                    </td>
                                                                    <td style="padding-left: 5px;">
                                                                        <select id="ddlLocationList" style="width: 185px;">
                                                                        </select>
                                                                        <asp:HiddenField ID="hidLocId" runat="server" />

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="150">
                                                                        <span class="style4">*</span> Designation
                                                                    </td>
                                                                    <td>
                                                                        <strong>:</strong>
                                                                    </td>
                                                                    <td style="padding-left: 5px;">
                                                                        <select id="ddlDesignation" style="width: 185px;">
                                                                        </select>
                                                                        <asp:HiddenField ID="hidDesigId" runat="server" />

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="150">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <strong>:</strong>
                                                                    </td>
                                                                    <td>
                                                                        <table class="style8">
                                                                            <tr>
                                                                                <td rowspan="2">
                                                                                    <asp:ListBox ID="lbLeftUser" onchange="GetSelected(this,'TabContainer1_TabAssignLink_hidUserIds');"
                                                                                        runat="server" Height="90px" Width="140px" SelectionMode="Multiple"><asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                    </asp:ListBox>

                                                                                </td>
                                                                                <td align="center" onclick="LstBoxAdd('TabContainer1_TabAssignLink_lbRightUser','TabContainer1_TabAssignLink_lbLeftUser','TabContainer1_TabAssignLink_hidSelectUser','TabContainer1_TabAssignLink_hidShowUser');"
                                                                                    valign="middle">
                                                                                    <img alt="" src="../images/left-down-blue.gif" title="Add User" style="height: 13px;
                                                                                        width: 15px" />
                                                                                </td>
                                                                                <td rowspan="2">
                                                                                    <asp:ListBox ID="lbRightUser" runat="server" Height="90px" Width="140px" SelectionMode="Multiple"><asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                    </asp:ListBox>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" onclick="LstBoxRemove('TabContainer1_TabAssignLink_lbRightUser','TabContainer1_TabAssignLink_lbLeftUser','TabContainer1_TabAssignLink_hidSelectUser','TabContainer1_TabAssignLink_hidShowUser');"
                                                                                    valign="middle">
                                                                                    <img src="../images/arrow-down-blue.gif" title="Remove User" width="15" height="13"
                                                                                        alt="Laeft Arrow" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <asp:HiddenField ID="hidListText" runat="server" />

                                                                        <asp:HiddenField ID="hidListVal" runat="server" />

                                                                        <asp:HiddenField ID="hidUserIds" runat="server" />

                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <div id="divLoc" runat="server" style="display: none">
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="150">
                                                                        <span class="style4">*</span> Location
                                                                    </td>
                                                                    <td>
                                                                        <strong>:</strong>
                                                                    </td>
                                                                    <td style="padding-left: 5px;">
                                                                        <asp:ListBox ID="lbChooseLoc" runat="server" Height="90px" onchange="BindLocId();"
                                                                            SelectionMode="Multiple" Width="140px">
                                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                        </asp:ListBox>

                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="150">
                                                        <span class="style4">*</span><asp:Label ID="lblPermissionDeny" runat="server" Text="Permission For"></asp:Label>

                                                    </td>
                                                    <td>
                                                        <strong>:</strong>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rbtBtnPtype" runat="server" RepeatDirection="Horizontal"><asp:ListItem Selected="True" Value="1">Add</asp:ListItem>
<asp:ListItem Value="2">View</asp:ListItem>
<asp:ListItem Value="3">Manage</asp:ListItem>
                                                        </asp:RadioButtonList>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="150">
                                                        <asp:HiddenField ID="hidPlinkId" runat="server" />

                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnUpdate" OnClientClick="return CheckConfirm(this);" runat="server"
                                                            OnClick="btnUpdate_Click" Text="Update" />

                                                        &#160;<asp:Button ID="btnReset" OnClientClick="return CheckConfirm(this);" runat="server"
                                                            OnClick="btnReset_Click" Text="Reset" />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="150">
                                                        <asp:HiddenField ID="hidShowLevels" runat="server" />

                                                    </td>
                                                    <td>
                                                        &#160;&#160;
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="hidSelectedLevels" runat="server" />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="150">
                                                        <asp:HiddenField ID="hidShowUser" runat="server" />

                                                    </td>
                                                    <td>
                                                        &#160;&#160;
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="hidSelectUser" runat="server" />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="150">
                                                        <asp:HiddenField ID="hidGroupDeptId" runat="server" />

                                                    </td>
                                                    <td>
                                                        &#160;&#160;
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="hidGroupUserId" runat="server" />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="150">
                                                        &#160;&#160;
                                                    </td>
                                                    <td>
                                                        &#160;&#160;
                                                    </td>
                                                    <td>
                                                        &#160;&#160;
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    
                                    
                                </ContentTemplate>
                                
</cc1:TabPanel>
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

<script type="text/javascript">
   
    function CallWMtoGetPrimaryLinks(ddlGlobalLinkId) {

        var ddlval = ddlGlobalLinkId.value;
        if (ddlGlobalLinkId.selectedIndex > 0) {
            PageMethods.BindPrimaryLinks(ddlval, getpPrimaryLinkData);
        }
        else {
            PageMethods.BindPrimaryLinks("0", getpPrimaryLinkData);
        }
    }

    function getpPrimaryLinkData(returnString) {
        try {
             var ddlDistId = document.getElementById("TabContainer1_TabAssignLink_ddlPrimaryLink");
             var distNames = returnString.split(',');
             while (ddlDistId.options.length > 0) {
                ddlDistId.options.remove(0);
            }
            for (var i = 0; i < distNames.length; i = i + 2) {
                var opt = document.createElement("option");
                opt.text = distNames[i];
                opt.value = distNames[i + 1];
                ddlDistId.options.add(opt);
            }
        }
        catch (e) {
            alert(e);
        }
    }

   
    function GetSelected(ctrlId, hidId) {
        var opt;
        document.getElementById(hidId).value = "";
        for (i = 0; i < ctrlId.length; i++) {
            opt = ctrlId.options[i];
            if ((opt.selected) && (opt.value != 0)) {
                var selected = opt.value;
                document.getElementById(hidId).value = document.getElementById(hidId).value + ',' + selected;
            }
        }
    }
    
    $('#<%=rbtLoc.ClientID %>').click(function()
    {  
    debugger;
         $('#TabContainer1_TabAssignLink_divLoc').show(); 
         $('#TabContainer1_TabAssignLink_divDept').hide();
         $('#TabContainer1_TabAssignLink_divDesig').hide();
         fillLoc();
         var listItems = []; 
         listItems.push('<option value="0">--Select--</option>');
                
         var lbShow = $('select[id$=lbShowUser]'); 
         lbShow.empty();
         $(lbShow).append(listItems.join('')); 
         
         var lbSelec = $('select[id$=lbSelectUser]'); 
         lbSelec.empty();
         $(lbSelec).append(listItems.join('')); 
         
         var levelshow = $('select[id$=lbShowLevels]'); 
         levelshow.empty();
         $(levelshow).append(listItems.join('')); 
         
         var levelselect = $('select[id$=lbSelectLevels]'); 
         levelselect.empty();
         $(levelselect).append(listItems.join('')); 
    }); 
    
    $('#<%=rbtDept.ClientID %>').click(function()
    { 
         $('#TabContainer1_TabAssignLink_divDept').show();

         $('#TabContainer1_TabAssignLink_divDesig').hide(); 
         
         $('#TabContainer1_TabAssignLink_divLoc').hide(); 
         document.getElementById('TabContainer1_TabAssignLink_hidSelectedLevels').value="";
         var listItems = []; 
         listItems.push('<option value="0">--Select--</option>');
                
         var lbShow = $('select[id$=lbShowUser]'); 
         lbShow.empty();
         $(lbShow).append(listItems.join('')); 
         
         var lbSelec = $('select[id$=lbSelectUser]'); 
         lbSelec.empty();
          $(lbSelec).append(listItems.join('')); 
         
         var levelshow = $('select[id$=lbShowLevels]'); 
         levelshow.empty();
          $(levelshow).append(listItems.join('')); 
         
         var levelselect = $('select[id$=lbSelectLevels]'); 
         levelselect.empty();
         $(levelselect).append(listItems.join('')); 
         var lbChoose = $('select[id$=lbChooseLoc]'); 
         lbChoose.empty();
         $(lbChoose).append(listItems.join(''));     
               
      });     
 
    $('#<%=rbtDesig.ClientID %>').click(function()
    {        
    debugger;
        document.getElementById('TabContainer1_TabAssignLink_hidSelectedLevels').value="";
        fillLocationCombo();
        $('#ddlDesignation').empty(); 
        $('#ddlDesignation').append($("<option value='0'>-Select-</option>"));
        var listItems = [];                 
        var lBox = $('select[id$=lbLeftUser]'); 
        lBox.empty();
        listItems.push('<option value="0">--Select--</option>');
        $(lBox).append(listItems.join('')); 
        var rBox = $('select[id$=lbRightUser]'); 
        rBox.empty();
        $(rBox).append(listItems.join(''));
        var lbChoose = $('select[id$=lbChooseLoc]'); 
        lbChoose.empty();
        $(lbChoose).append(listItems.join(''));                   
        $('#TabContainer1_TabAssignLink_divDesig').show();         
        $('#TabContainer1_TabAssignLink_divDept').hide(); 
        $('#TabContainer1_TabAssignLink_divLoc').hide(); 
                          
    }); 
 
   function fillLocationCombo() {
        var userId='<%=Session["UserId"]%>';
        var adminStat='<%=Session["UserId"]%>';
      $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "admin_AssignLink.aspx/FillLocationCombo",
        data: '{"strUserId":"'+userId+'"}',
        dataType: "json",
        success: function(msg) {
            fillLocationData(msg.d);
        },
        error: function(msg) {
            AjaxFailed;
        }
    });
}
function BindPlink() {
    debugger;
    var val = document.getElementById('TabContainer1_TabAssignLink_ddlGlobalLink').value;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "admin_AssignLink.aspx/GetPlink",
        data: '{"glink":"' + val + '"}',
        dataType: "json",
        success: function (msg) {
            var ddl = document.getElementById('TabContainer1_TabAssignLink_ddlPrimaryLink');
            $(ddl).empty();
            $(ddl).prepend("<option value='0'>--Select---</option>");
            var gender = msg.d;
            var listItems = [];
            if (gender.length > 0) {

                for (var key in gender) {
                    listItems.push('<option value="' + gender[key].PlinkId + '">' + gender[key].PlinkName + '</option>');
                }
                $(ddl).append(listItems.join(''));
            }
            else {
               
            }
        },
        error: function (msg) {
            AjaxFailed;
        }
    });
}
//Fill Location to while assign links by location
function fillLoc() {
        var userId='<%=Session["UserId"]%>';
        var adminStat='<%=Session["UserId"]%>';
      $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "admin_AssignLink.aspx/FillLocationCombo",
        data: '{"strUserId":"'+userId+'"}',
        dataType: "json",
        success: function(msg) {
            var lBox = $('select[id$=lbChooseLoc]'); 
            lBox.empty();
            var gender = msg.d;
            var listItems = [];
              if (gender.length > 0) {
//                     listItems.push('<option  value="All">Select All</option>');
                                            
                    for (var key in gender) {
                        listItems.push('<option value="' + gender[key].LocationId + '">' + gender[key].LocationName + '</option>');  
                     }
                    $(lBox).append(listItems.join(''));
                }
                else {
                    listItems.push('<option disabled="disabled" value="0">--Select--</option>');
                     $(lBox).append(listItems.join(''));
                }
        },
        error: function(msg) {
            AjaxFailed;
        }
    });
}
//Location Combo Fill Success Event
function fillLocationData(data) {
debugger;
    $('#ddlLocationList').empty(); 
    $.each(data, function(index, value) {
        $('#ddlLocationList').append($("<option value='" + value.LocationId + "'>" + value.LocationName + "</option>"));
    })
    //$('#ddlLocationList').val(locId);
    //locId = data[0].ID
  
    $("#ddlLocationList").bind("change", function(e) {
        //locId = $(this).attr("value");
       debugger
        $(document).trigger("optionChange");
        $(document).trigger("eventDetail");
        //catId = $("#cmbCategory").val();
        catId="0"
        locId = $("#ddlLocationList").val();
        //console.log(locId)
        document.getElementById("TabContainer1_TabAssignLink_hidLocId").value=locId;
        $("#hidLocId").val(locId)
        fillDesignation(locId);
        $("ddlLocationList").text($(this).text());
    })
 }

//Fill Designation by location id
 function fillDesignation(locId) {
      $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "admin_AssignLink.aspx/FillDesignation",
        data: '{"strLocId":"'+locId+'"}',
        dataType: "json",
        success: function(msg) {
            fillDesigData(msg.d);
        },
        error: function(msg) {
           // AjaxFailed;
        }
    });
}
//Designation Combo Fill Success Event
function fillDesigData(data) {
debugger;
    $('#ddlDesignation').empty(); 
    $('#ddlDesignation').append($("<option value='0'>-Select-</option>"));
    $.each(data, function(index, value) {
        $('#ddlDesignation').append($("<option value='" + value.DesigId + "'>" + value.DesigName + "</option>"));
    })
    $("#ddlDesignation").bind("change", function(e) {
       debugger
        $(document).trigger("optionChange");
        $(document).trigger("eventDetail");
        catId="0"
        var desigId = $("#ddlDesignation").val();
        var locId = $("#ddlLocationList").val();
        $("#hidDesigId").val(desigId)         
        fillUserListbox(locId,desigId);
        $("ddlLocationList").text($(this).text());
    })
 }
//Bind listbox by designation id
function fillUserListbox(locationId,desigId){
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "admin_AssignLink.aspx/GetUserListByDesig",
        data: '{"strLocId":"' + locationId + '","strDesigId":"' + desigId + '"}',
        dataType: "json",
        success: function(msg) { 
        debugger;
        var lBox = $('select[id$=lbRightUser]'); 
        $("#hidListVal").val("") 
        $("#hidListText").val("") 
        lBox.empty();
        var gender = msg.d;
        var listItems = [];
          if (gender.length > 0) {
                 listItems.push('<option value="0">--Select--</option>');
                                        
                for (var key in gender) {
                    listItems.push('<option value="' + gender[key].UserId + '">' + gender[key].UserName + '</option>');  
                      document.getElementById("TabContainer1_TabAssignLink_hidListVal").value= document.getElementById("TabContainer1_TabAssignLink_hidListVal").value+ gender[key].UNAME+",";
                      document.getElementById("TabContainer1_TabAssignLink_hidListText").value= document.getElementById("TabContainer1_TabAssignLink_hidListText").value+ gender[key].UID+",";                                                      
                }
                $(lBox).append(listItems.join(''));
            }
            else {
                listItems.push('<option value="0">--Select--</option>');
                 $(lBox).append(listItems.join(''));
            }
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}
function BindLocId(){
debugger
    var hidLocIds = document.getElementById('TabContainer1_TabAssignLink_hidSelectedLevels');
    hidLocIds.value="";
    var sel = document.getElementById('TabContainer1_TabAssignLink_lbChooseLoc')
    var isAll=0;
     var listLength = sel.options.length;
     for (var i = 0; i < listLength; i++) {
        if (sel.options[i].selected) {
                if(sel.options[i].value=="All"){
                isAll=1;
            }
            hidLocIds.value=hidLocIds.value+ sel.options[i].text+','+sel.options[i].value+'~'; 
         }
    }
    if(isAll==1){
    hidLocIds.value="";
        for (var i = 0; i < listLength; i++) {
            if(sel.options[i].value!="All"){
                hidLocIds.value=hidLocIds.value+ sel.
                options[i].text+','+sel.options[i].value+'~'; 
            }
        }
    }
 }
</script>

