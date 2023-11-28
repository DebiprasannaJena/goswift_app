<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="true"
    Inherits="Admin_Manage_User_AdminAddUser" CodeBehind="AdminAddUser.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Console/FillUserHierarchy.ascx" TagName="FillHierarchy" TagPrefix="uc2" %>
<%@ Register Src="~/Console/FillHierarchy.ascx" TagName="HierarchyForAllLocation" TagPrefix="uc3" %>
   
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation" TagPrefix="uc4" %>
   
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header" TagPrefix="hdr" %>
    
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu" TagPrefix="lm" %>
   
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Console::AddUser</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.02)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.02)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../jscript48/calendar.css" rel="stylesheet" type="text/css" />
    <link href="../style/tooltipster.css" rel="stylesheet" type="text/css" />

     <script src="../scripts/ProgressBar/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script src="../scripts/1.4.2_jquery.min.js" type="text/javascript"></script>

    <script src="../scripts/1.8.1_jquery-ui.min.js" type="text/javascript"></script>

    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <link href="../style/default.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/ajax.js" type="text/javascript"></script>

    <script src="../scripts/ajaxHierarchy.js" type="text/javascript"></script>

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

    <style type="text/css">
        .divheading
        {
            font-size: small;
            font-weight: bold;
        }
        .divcontent
        {
        }
        .divcontent3
        {
        }
        .divcontent3_div1
        {
            color: Teal;
            font-size: 12px;
            text-decoration: underline;
            border: solid 1px silver;
            border-bottom: 0;
            background-color: #F0F0F0;
            text-align: left;
            border-top: 0;
            margin-left: 200px;
            height: 25px;
            padding-top: 5px;
            padding-left: 5px;
            width: 69.5%;
        }
        .divheading3
        {
            font-size: 14px;
            font-weight: bold;
            border: solid 1px silver;
            text-align: left;
            margin-left: 200px;
            width: 70%;
            background-color: #F0F0F0;
            cursor: pointer;
            margin-top: 2px;
            height: 20px;
            background: url(../images/botBg.png) repeat-x left top;
        }
        .modalBackground
        {
            background-color: Silver;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .DuplicateCss
        {
            font-family: Verdana;
            font-size: x-small;
            font-weight: bold;
            color: Red;
        }
        .AvailableCss
        {
            font-family: Verdana;
            font-size: x-small;
            font-weight: bold;
            color: green;
        }
        .VeryPoorStrength
        {
            background: Red;
            color: White;
            font-weight: bold;
        }
        .WeakStrength
        {
            background: Gray;
            color: White;
            font-weight: bold;
        }
        .AverageStrength
        {
            background: orange;
            color: black;
            font-weight: bold;
        }
        .GoodStrength
        {
            background: blue;
            color: White;
            font-weight: bold;
        }
        .ExcellentStrength
        {
            background: Green;
            color: White;
            font-weight: bold;
        }
        .BarBorder
        {
            border-style: solid;
            border-width: 1px;
            width: 180px;
            padding: 2px;
        }
        .TextColor
        {
            color: Red;
        }
       
    </style>

    <script type="text/javascript">

  function pageLoad() {    
    $(document).ready(function() {
        $('#TabContainer1_TabAddUser_txtfullname').focus();
		});
	}
        
        function changeImage() {
          var img= document.getElementById("TabContainer1_TabAddUser_imgCollapse");
          var status= document.getElementById("TabContainer1_TabAddUser_divCont").style.display;
            if(status=="none"){
                document.getElementById("TabContainer1_TabAddUser_divCont").style.display="block";
                img.src="../images/expand.gif";
            }else{
                document.getElementById("TabContainer1_TabAddUser_divCont").style.display="none";
                img.src="../images/collapse.gif";
            }
          var s=img.src;               
        }
  /*
    Created By : Dilip Kumar Tripathy
    Created On : 30-Oct-2013
    Description: To check the Duplicate username, domain name and email address
  */      
        
function CheckDuplicateValue(actionCode,tboxId,lblId,imgTick){

   if(tboxId.value!="")
    {           
         $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "AdminAddUser.aspx/DuplicateValueCheck",
            data: '{"strActionCode":"' + actionCode + '","strDuplicVal":"' + tboxId.value + '","strUserId":"'+userId+'"}',
            dataType: "json",
            success: function(msg) {
            var result=msg.d        
                          
             if(result!="AVAILABLE"){                   
                document.getElementById("TabContainer1_TabAddUser_"+lblId).className="DuplicateCss";
                document.getElementById("TabContainer1_TabAddUser_"+lblId).style.display="block";                 
                document.getElementById("TabContainer1_TabAddUser_"+lblId).innerHTML=result;
                document.getElementById(imgTick).style.display="none"; 
                tboxId.focus();
              }else{
                  document.getElementById("TabContainer1_TabAddUser_"+lblId).innerHTML="";
                  document.getElementById(imgTick).style.display="block"; 
              }              
            },
            error: function(XMLHttpRequest,textStatus, errorThrown) {
              //  alert(textStatus);
            }
        });
      
            if(tboxId=="TabContainer1_TabAddUser_txtEmail"){
                if(document.getElementById("TabContainer1_TabAddUser_btnAdd")!=null){
                    Validation(document.getElementById("TabContainer1_TabAddUser_btnAdd"));
                }
                if(document.getElementById("TabContainer1_TabAddUser_btnUpdate")!=null){
                    Validation(document.getElementById("TabContainer1_TabAddUser_btnUpdate"));
                }
            }

   }else{
            document.getElementById("TabContainer1_TabAddUser_"+lblId).innerHTML=""
            document.getElementById(imgTick).style.display="none";
            if(tboxId=="TabContainer1_TabAddUser_txtEmail"){
                if(document.getElementById("TabContainer1_TabAddUser_btnAdd")!=null){
                    Validation(document.getElementById("TabContainer1_TabAddUser_btnAdd"));
                }
                if(document.getElementById("TabContainer1_TabAddUser_btnUpdate")!=null){
                    Validation(document.getElementById("TabContainer1_TabAddUser_btnUpdate"));
                }
            }
    }   
}
 

    </script>

</head>
<body bgcolor="#ff3300">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <script type="text/javascript">var userId='<%=strUid%>'   
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="MainArea">
        <hdr:Header ID="header1" runat="server" />
        <asp:HiddenField ID="hdnLevelid" runat="server" />
        <div id="MidArea">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td valign="top" id="LeftPannel">
                        <lm:LeftMenu ID="lm1" runat="server" />
                    </td>
                    <td valign="top" class="midRightArea">
                        <div id="container">
                            <uc4:Navigation ID="Navigation1" runat="server" />
                            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme">
                                <cc1:TabPanel runat="server" HeaderText="Add User" ID="TabAddUser">
                                    <HeaderTemplate>
                                        User
                                    
</HeaderTemplate>
                                    

<ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                                                <div class="mandatory">
                                                    <asp:Button ID="btnMngCtrl" runat="server" Text="Manage Page Fields" />( * indicates
                                                    mandatory fields)</div>
                                                <div class="addTable">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="left" width="150">
                                                                Name
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtfullname" runat="server" onkeyup="isSpecialChar1st('TabContainer1_TabAddUser_txtfullname');"
                                                                    AutoCompleteType="Disabled" Width="175px" MaxLength="100"></asp:TextBox><font color="#FF0000">*</font>
                                                                <asp:HiddenField ID="hdnAction" runat="server" />
                                                                <cc1:FilteredTextBoxExtender ID="FilterFunname" runat="server" TargetControlID="txtfullname"
                                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters" ValidChars=" ." Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                            <td width="250px">
                                                            </td>
                                                            <td width="250px" rowspan="3">
                                                                <asp:Image ID="ShowUserImage" runat="server" Height="70px" ImageUrl="User_Image/DefaultImage.jpg"
                                                                    Width="70px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                User Name
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtUserName" runat="server" onblur="CheckDuplicateValue('U',this,'lblDupUserMsg','imgDupUser')"
                                                                    AutoCompleteType="Disabled" MaxLength="20" onkeyup="isSpecialChar1st('TabContainer1_TabAddUser_txtUserName');"
                                                                    onPaste="return false" Width="175px"></asp:TextBox>
                                                                <font color="#FF0000">*</font>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredUserName" runat="server" Enabled="True"
                                                                    FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txtUserName"
                                                                    ValidChars="-_.+(){}[]">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                            <td align="left">
                                                                <div style="float: left; vertical-align: middle">
                                                                    <asp:Label ID="lblDupUserMsg" Style="display: none" runat="server" Text="Label"></asp:Label></div>
                                                                <div style="float: left; vertical-align: middle">
                                                                    <img id="imgDupUser" src="../images/tickmark.jpg" alt="a" style="display: none; height: 20px;
                                                                        width: 20px" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr style="display:none">
                                                            <td align="left">
                                                                Father Name(In English)
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtFatherNameInEnglish" runat="server" Width="175px" MaxLength="100"></asp:TextBox>
                                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFatherNameInEnglish"
                                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters" ValidChars=" ." Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                                <font
                                                                    color="#FF0000">*</font>
                                                            </td>
                                                            <td align="left">
                                                                &#160;&#160;
                                                            </td>
                                                        </tr>
                                                        <tr style="display:none">
                                                            <td align="left">
                                                                Father Name(In Amharic)
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtFatherNameInAmharic" runat="server" AutoCompleteType="Disabled"
                                                                    Width="175px" MaxLength="100"></asp:TextBox>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                            <td align="left">
                                                                &#160;&#160;
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr style="display:none">
                                                            <td align="left">
                                                                Grand Father Name&nbsp;&nbsp;&nbsp;&nbsp; (In English)
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtGrandFatherNameInEnglish" runat="server" AutoCompleteType="Disabled"
                                                                    Width="175px" MaxLength="100"></asp:TextBox>
                                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtGrandFatherNameInEnglish"
                                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters" ValidChars=" ." Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                            <td align="left">
                                                                &#160;&#160;
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr style="display:none">
                                                            <td align="left">
                                                                Grand Father Name&nbsp;(In Amharic)
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtGrandFatherNameInAmharic" runat="server" AutoCompleteType="Disabled"
                                                                    Width="175px" MaxLength="100"></asp:TextBox>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                            <td align="left">
                                                                &#160;&#160;
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                User Image
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" style="padding-left: 8px;">
                                                                <asp:FileUpload ID="FileUploadImage" Width="175px" runat="server" onchange="previewFile()" />
                                                                <font
                                                                    color="#FF0000">*</font>
                                                            </td>
                                                            <td align="left" colspan="2">
                                                                &nbsp;&nbsp;&nbsp;
                                                                <br />
                                                                (Only jpg,png file with size < 1mb.)&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr id="trPass" runat="server">
                                                            <td id="Td1" align="left" runat="server">
                                                                Password
                                                            </td>
                                                            <td id="Td2" align="left" runat="server">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td id="Td3" align="left" runat="server" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtPassword" onkeypress="return isWhiteSpace(event);" runat="server"
                                                                    AutoCompleteType="Disabled" MaxLength="10"
                                                                    onPaste="return false" TextMode="Password" Wrap="False" Width="175px" 
                                                                    ></asp:TextBox>
                                                                <font color="#FF0000"> *</font></span>
                                                            </td>
                                                          
                                                             <td align="left" colspan="2">
                                                              (Password must have 1 digit,1 special character,1 upper & lower case letter.[Min 8 Char])
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr id="trCPass" runat="server">
                                                            <td id="Td4" align="left" runat="server">
                                                                Confirm Password
                                                            </td>
                                                            <td id="Td5" align="left" runat="server">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td id="Td6" align="left" runat="server" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtConPassword" onkeyup="return isSpecialChar('TabContainer1_TabAddUser_txtConPassword')"
                                                                    onkeypress="return isWhiteSpace(event);" runat="server" AutoCompleteType="Disabled"
                                                                    onPaste="return false" TextMode="Password" Width="175px" MaxLength="10"></asp:TextBox>
                                                                <font
                                                                        color="#FF0000">*</font>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr id="trDomainUser" runat="server">
                                                            <td align="left">
                                                                Domain Username
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtDomainName" runat="server" onblur="CheckDuplicateValue('D',this,'lblDupDomainMsg','imgDupDomain')"
                                                                    AutoCompleteType="Disabled" MaxLength="50" Width="175px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDupDomainMsg" Style="display: none" runat="server" Text="Label"></asp:Label>
                                                                <img
                                                                    id="imgDupDomain" src="../images/tickmark.jpg" alt="a" style="display: none;
                                                                    width: 20px; height: 20px" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr align="left">
                                                            <td align="left" colspan="4" valign="top">
                                                                <strong style="font-size: 12px">Department of the user </strong>
                                                            </td>
                                                            <td align="left" valign="top">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <uc2:FillHierarchy ID="HierarchyForAllLocation1" runat="server" />
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr id="Tr3">
                                                            <td align="left" width="150">
                                                                Physical Location
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td colspan="2" style="padding-left: 8px;">
                                                                <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" Width="185px">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                      
                                                        <tr id="trOfficType" runat="server">
                                                            <td align="left">
                                                                Office Type
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" colspan="2" style="padding-left: 8px;">
                                                                <asp:DropDownList ID="ddlOfficeType" runat="server" Width="185px">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Date Of Joining
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" valign="middle" colspan="2" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtDOJ" runat="server" Width="175px" class="inputCalendar"></asp:TextBox>
                                                                <cc1:CalendarExtender
                                                                    ID="txtDOJ_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy"
                                                                    TargetControlID="txtDOJ" CssClass="cal_Theme1">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr id="trProbaComp" runat="server">
                                                            <td align="left">
                                                                Probation Completion Date
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" valign="middle" colspan="2" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtPCD" runat="server" class="inputCalendar" Width="175px"></asp:TextBox>
                                                                <cc1:CalendarExtender
                                                                    ID="txtPCD_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True"
                                                                    Format="dd-MMM-yyyy" TargetControlID="txtPCD">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Designation
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" valign="middle" colspan="2" style="padding-left: 8px;">
                                                                <asp:DropDownList ID="ddlDesg" onchange="GetSelected(this);" runat="server" Width="185px">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr id="trGrade" runat="server">
                                                            <td align="left">
                                                                Grade
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" valign="middle" colspan="2" style="padding-left: 8px;">
                                                                <asp:DropDownList ID="ddlGrade" onchange="GetSelectedGradeId(this);" runat="server"
                                                                    Width="185px">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr id="rAdminprevil" runat="server">
                                                            <td id="Td7" width="150" runat="server">
                                                               Super Admin Privilege
                                                            </td>
                                                            <td id="Td8" runat="server">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td id="Td9" runat="server" align="left">
                                                                <asp:CheckBox ID="chkSupAdminPrevil" runat="server" Text="Enable Super Admin Privilege" />
                                                            </td>
                                                        </tr>
                                                        <tr id="trAttendance" runat="server">
                                                            <td>
                                                               Attendance
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left">
                                                                <asp:CheckBox ID="chkAttendance" runat="server" Text="Enable Attendance " />
                                                            </td>
                                                        </tr>
                                                        <tr id="trPayroll" runat="server">
                                                            <td>
                                                                Option 3
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left">
                                                                <asp:CheckBox ID="chkPayroll" runat="server"  Text="Put In Payroll">
                                                                </asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trEpf" runat="server">
                                                            <td>
                                                                EPF
                                                            </td>
                                                            <td>
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left">
                                                                <asp:RadioButtonList ID="rbtnLstEPF" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Gender
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" colspan="2">
                                                                <asp:RadioButtonList ID="rbtGender" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Male" Value="M" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Date Of Birth
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" valign="middle" colspan="2" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtDOB" runat="server" class="inputCalendar" Width="175px"></asp:TextBox>
                                                                <cc1:CalendarExtender
                                                                    ID="txtDOB_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True"
                                                                    Format="dd-MMM-yyyy" TargetControlID="txtDOB">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                E-Mail
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" colspan="2" style="padding-left: 8px;">
                                                                <div style="float: left; vertical-align: middle">
                                                                    <asp:TextBox ID="txtEmail" runat="server" AutoCompleteType="Disabled" MaxLength="50"
                                                                        Width="175px" onblur="CheckDuplicateValue('E',this,'lblDupEmailMsg','imgDupEmail')"></asp:TextBox>
                                                                    <font
                                                                            color="#FF0000">*</font></div>
                                                                <div style="float: left; vertical-align: middle">
                                                                    <asp:Label ID="lblDupEmailMsg" Style="display: none" runat="server" Text=""></asp:Label>
                                                                    <img
                                                                        src="../images/tickmark.jpg" alt="a" id="imgDupEmail" style="display: none; float: left;
                                                                        height: 20px; width: 20px" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trTelephone" runat="server">
                                                            <td align="left">
                                                                Office Telephone
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" colspan="2" style="padding-left: 8px;">
                                                                <div style="width: 50px; float: left;">
                                                                    <asp:TextBox ID="txtCountryCode" runat="server" MaxLength="6" Width="37px" ToolTip="Country Code"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender
                                                                        ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers"
                                                                        TargetControlID="txtCountryCode">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </div>
                                                                <div id="divHifen1" style="width: 10px; float: left; margin-top: 5px; text-align: center;"
                                                                    runat="server">
                                                                    <span>-</span></div>
                                                                <div style="width: 55px; float: left">
                                                                    <asp:TextBox ID="txtStdCode" runat="server" Width="37px" MaxLength="6" ToolTip="Area Code"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender
                                                                        ID="txtStdCode_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers"
                                                                        TargetControlID="txtStdCode">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </div>
                                                                <div id="divHifen2" style="width: 8px; float: left; margin-top: 5px;" runat="server">
                                                                    <span>-</span></div>
                                                                <div style="width: 53px; float: left">
                                                                    <asp:TextBox ID="txtOfficeTel" runat="server" Width="53px" Wrap="False" MaxLength="10"
                                                                        ToolTip="Telephone Number"> </asp:TextBox><cc1:FilteredTextBoxExtender ID="txtOfficeTel_FilteredTextBoxExtender"
                                                                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtOfficeTel">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                &#160;&#160;
                                                            </td>
                                                            <td align="left">
                                                                &#160;&#160;
                                                            </td>
                                                            <td align="left" colspan="2" style="padding-left: 8px;">
                                                                <asp:Label ID="Label1" runat="server" ForeColor="#FF3300" Text="CountryCode - AreaCode - TelephoneNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="trMobile" runat="server">
                                                            <td align="left">
                                                                Mobile
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" colspan="2" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtMob" runat="server" Width="175px" Wrap="False" MaxLength="15"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender
                                                                    ID="txtMob_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Custom,Numbers"
                                                                    TargetControlID="txtMob" ValidChars="+- ">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr id="trPaddr" runat="server">
                                                            <td align="left">
                                                                Present Address
                                                            </td>
                                                            <td align="left">
                                                                <strong>:</strong>
                                                            </td>
                                                            <td align="left" colspan="2" style="padding-left: 8px;">
                                                                <asp:TextBox ID="txtPaddress" Style="resize: none" runat="server" onkeyup="isSpecialChar1st('TabContainer1_TabAddUser_txtPaddress');TextCounter('TabContainer1_TabAddUser_txtPaddress','TabContainer1_TabAddUser_lblMaxcounter',500);"
                                                                    Height="50px" TextMode="MultiLine" Width="175px" MaxLength="500"></asp:TextBox>
                                                               
                                                                &#160;Maximum <span class="mandatory">
                                                                    <asp:Label ID="lblMaxcounter" Text="500" runat="server"></asp:Label></span>characters
                                                                are allowed.
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div id="divRA" runat="server">
                                                        <div class="divheading">
                                                            Reporting Authority
                                                            <asp:Image ID="imgCollapse" onclick="changeImage();" ImageUrl="~/Console/images/collapse.gif"
                                                                runat="server" /></div>
                                                        <div class="divcontent" id="divCont" runat="server">
                                                           <uc3:HierarchyForAllLocation ID="getUsers1" runat="server" />
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="left" valign="middle" width="150">
                                                                        Primary Authority
                                                                    </td>
                                                                    <td valign="middle" align="left">
                                                                        <strong>:</strong>
                                                                    </td>
                                                                    <td valign="middle" width="240px" style="padding-left: 8px;">
                                                                        <asp:TextBox ID="txtPAuthor" runat="server" AutoCompleteType="Disabled" onPaste="return false"
                                                                            ReadOnly="True" Width="175px"></asp:TextBox>
                                                                        <asp:RadioButton ID="radPAuthor" runat="server"
                                                                                GroupName="Priority" Text="1"></asp:RadioButton>
                                                                        <img src="../images/DeleteIcon.png"
                                                                                    onclick="RemoveAuthority('TabContainer1_TabAddUser_txtPAuthor','Primary Authority','TabContainer1_TabAddUser_hidPriority1');"
                                                                                    title="Click to remove Primary Authority" alt="" />
                                                                    </td>
                                                                    <td rowspan="3">
                                                                        User Name <strong>:</strong>
                                                                    </td>
                                                                    <td rowspan="3">
                                                                        <asp:ListBox ID="ListRUser" runat="server" ondblClick="AddUserList();" SelectionMode="Multiple"
                                                                            Width="185px" Height="80px">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        </asp:ListBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle">
                                                                        Secondary Authority
                                                                    </td>
                                                                    <td valign="middle" align="left">
                                                                        <strong>:</strong>
                                                                    </td>
                                                                    <td valign="middle" style="padding-left: 8px;">
                                                                        <asp:TextBox ID="txtSAuthor" runat="server" AutoCompleteType="Disabled" onPaste="return false"
                                                                            ReadOnly="True" Width="175px"></asp:TextBox>
                                                                        <asp:RadioButton ID="radSAuthor" runat="server"
                                                                                GroupName="Priority" Text="2"></asp:RadioButton>
                                                                        <img src="../images/DeleteIcon.png"
                                                                                    onclick="RemoveAuthority('TabContainer1_TabAddUser_txtSAuthor','Secondary Authority','TabContainer1_TabAddUser_hidPriority2');"
                                                                                    title="Click to remove Secondary Authority" alt="" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle">
                                                                        Optional Authority
                                                                    </td>
                                                                    <td align="left" valign="middle">
                                                                        <strong>:</strong>
                                                                    </td>
                                                                    <td valign="middle" style="padding-left: 8px;">
                                                                        <asp:TextBox ID="txtOAuthor" runat="server" AutoCompleteType="Disabled" onPaste="return false"
                                                                            ReadOnly="True" Width="175px"></asp:TextBox>
                                                                        <asp:RadioButton ID="radOAuthor" runat="server"
                                                                                GroupName="Priority" Text="3"></asp:RadioButton>
                                                                        <img src="../images/DeleteIcon.png"
                                                                                    onclick="RemoveAuthority('TabContainer1_TabAddUser_txtOAuthor','Optional Authority','TabContainer1_TabAddUser_hidPriority3');"
                                                                                    title="Click to remove Optional Authority" alt="" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="4">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="4">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr id="Tr4">
                                                            <td align="left" valign="middle" width="150">
                                                                <asp:HiddenField ID="hdnUser" runat="server" Value="0" />
                                                            </td>
                                                            <td align="left">
                                                                &#160;&#160;
                                                            </td>
                                                            <td align="left" style="padding-left: 8px;">
                                                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Save" OnClientClick="return Validation(this);" />&#160;&nbsp;<asp:Button
                                                                    ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return Validation(this);" />&nbsp;
                                                                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClientClick="return ConfirmResetCancel(this);"
                                                                    OnClick="btnReset_Click" />
                                                            </td>
                                                            <td align="left">
                                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                                    <ProgressTemplate>
                                                                        <asp:Image ID="Image1" ImageUrl="~/Console/images/load.gif" runat="server" /></ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div>
                                                        <asp:HiddenField ID="hidDOA" runat="server" />
                                                        <asp:HiddenField ID="hidpassword" runat="server" />
                                                        <asp:HiddenField ID="HidNo" runat="server" />
                                                        <asp:HiddenField ID="hidoldempid" runat="server" />
                                                        <asp:HiddenField ID="hidMob" runat="server" />
                                                        <asp:HiddenField ID="hidFilename" runat="server" />
                                                        <asp:HiddenField ID="hidPriority1" runat="server" />
                                                        <asp:HiddenField ID="hidPriority2" runat="server" />
                                                        <asp:HiddenField ID="hidPriority3" runat="server" />
                                                        <asp:HiddenField ID="hidUID" runat="server" />
                                                        <asp:HiddenField ID="hidDesigId" runat="server" />
                                                        <asp:HiddenField ID="hidGradeId" runat="server" />
                                                        <asp:HiddenField ID="hidUserImage" runat="server" />
                                                        <asp:HiddenField ID="hidPageLink" runat="server" />
                                                        <asp:Panel ID="pnlPreview" runat="server" HorizontalAlign="Center" Style="display: none;
                                                            width: 80%">
                                                            <div class="divheading3">
                                                                <div style="float: left; width: 90%">
                                                                  
                                                                    User Add Page Configuration</div>
                                                                <div style="float: right; width: 9%; text-align: right;">
                                                                    <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Console/images/delete_img.png" />
                                                                </div>
                                                            </div>
                                                            <div class="divcontent3">
                                                                <div class="divcontent3_div1">
                                                                    <div style="float:left; width:40%">
                                                                        Optional Controls of AddUser page</div>
                                                                    <div  style="float:left; width:60%; text-align:left">
                                                                        <asp:Label ID="lblMsgModal" runat="server" Font-Bold="true" ForeColor="Green" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div id="div1" class="addTable" style="border: solid 1px silver; border-top: 0; background-color: #F0F0F0;
                                                                    text-align: left; margin-left: 200px; width: 69%">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="border: solid 0px silver">
                                                                        <tr>
                                                                            <td align="left" width="230px">
                                                                                <strong>Domain User Name </strong>
                                                                            </td>
                                                                            <td width="5px">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td align="left" width="300px">
                                                                                <asp:CheckBox ID="chkDomainUser" runat="server" />
                                                                            </td>
                                                                            <td align="left" width="200px">
                                                                                <strong>Super Admin Privilege</strong>
                                                                            </td>
                                                                            <td align="left" width="5px">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td width="270px">
                                                                                &nbsp;
                                                                                <asp:CheckBox ID="chkSuperPrevil" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <strong>Office Type </strong>
                                                                            </td>
                                                                            <td>
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:CheckBox ID="chkOfficeType" runat="server" />
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>Enable Attendance </strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                                <asp:CheckBox ID="chkAttend" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <strong>Probation Completion Date </strong>
                                                                            </td>
                                                                            <td>
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:CheckBox ID="chkProbComp" runat="server" />
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>Put In Payroll </strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                                <asp:CheckBox ID="chkProll" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <strong>Grade </strong>
                                                                            </td>
                                                                            <td>
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:CheckBox ID="chkGrade" runat="server" />
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>EPF </strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                                <asp:CheckBox ID="chkEpf" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <strong>Office Telephone </strong>
                                                                            </td>
                                                                            <td>
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:CheckBox ID="chkTelephone" runat="server" />
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>Mobile </strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                                <asp:CheckBox ID="chkMobile" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <strong>Permanent Adddress </strong>
                                                                            </td>
                                                                            <td>
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:CheckBox ID="chkPAddr" runat="server" />
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>Reporting Authority</strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>:</strong>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                                <asp:CheckBox ID="chkRA" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="400px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td width="5px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left" width="200px">
                                                                                &nbsp;
                                                                                <asp:Button ID="btnSubmitModal" runat="server" OnClientClick="return CheckConfirm(this);"
                                                                                    Text="Submit" Width="60px" OnClick="btnSubmitModal_Click" />
                                                                                &nbsp;<asp:Button ID="btnResetModal" runat="server" Text="Reset" Width="60px" OnClick="btnResetModal_Click" />
                                                                            </td>
                                                                            <td align="left" width="150px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <input id="Button1" style="display: none" type="button" value="button" />
                                                <cc1:ModalPopupExtender ID="pnlPreview_ModalPopupExtender" runat="server" DynamicServicePath=""
                                                    Enabled="True" CancelControlID="imgBtnClose" BackgroundCssClass="modalBackground"
                                                    PopupControlID="pnlPreview" TargetControlID="btnMngCtrl">
                                                </cc1:ModalPopupExtender>
                                            
</ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="btnAdd" />
<asp:PostBackTrigger ControlID="btnUpdate" />
</Triggers>
</asp:UpdatePanel>


                                    
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

<script language="javascript" type="text/javascript">
    window.onload = function() {
        //ShowHideEPF();
       // ShowHideSuperAdmin();
    }
    function previewFile() {
            var preview = document.querySelector('#<%=ShowUserImage.ClientID %>');
          
            var file = document.querySelector('#<%=FileUploadImage.ClientID %>').files[0];
            var reader = new FileReader();
            
            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    function RemoveAuthority(txtid,msg,hidId)
    {       
        if( document.getElementById(txtid).value!=""){ 
            var result=confirm('Are you sure to remove '+msg+'.')
            if(result==true)
             { 
                 if(txtid=="TabContainer1_TabAddUser_txtPAuthor")
                 {
                    if(document.getElementById("TabContainer1_TabAddUser_txtSAuthor").value=="" && document.getElementById("TabContainer1_TabAddUser_txtOAuthor").value=="")
                    {
                        document.getElementById(txtid).value="";
                        document.getElementById(hidId).value=""; 
                    }
                    else{
                        alert('First remove the Secondary Authority and Optional Authority in order to remoce Primary Authority');
                    }
                 }
                 else{
                    document.getElementById(txtid).value="";
                    document.getElementById(hidId).value=""; 
                 }
             }                        
         }
        else{
        alert('No '+msg+' is selected to remove.');
        }             
    }
    function AddUserList() {
        if (document.getElementById("TabContainer1_TabAddUser_radPAuthor").checked == false && document.getElementById("TabContainer1_TabAddUser_radSAuthor").checked == false && document.getElementById("TabContainer1_TabAddUser_radOAuthor").checked == false) {
            alert("Please select any one authority from left side options");
            return false;
        }
        var selUser = document.getElementById("TabContainer1_TabAddUser_ListRUser");
        if (selUser.options[selUser.options.selectedIndex].value == document.getElementById("TabContainer1_TabAddUser_hdnUser").value) {
            alert('Please select different user for authority');
            return false;
        }
        if ((document.getElementById("TabContainer1_TabAddUser_txtPAuthor").value == "") && (document.getElementById("TabContainer1_TabAddUser_radSAuthor").checked == true || document.getElementById("TabContainer1_TabAddUser_radOAuthor").checked == true)) {
            alert("Please select an user for primary auhority");
            return false;
        }
        if (document.getElementById("TabContainer1_TabAddUser_txtSAuthor").value == "" && document.getElementById("TabContainer1_TabAddUser_radOAuthor").checked == true) {
            alert("Please select an user for secondary authority");
            return false;
        }
        var selUserId = selUser.options[selUser.options.selectedIndex].value;
        var selUserText = selUser.options[selUser.options.selectedIndex].text;

        if (selUserId == 0) {
            if (document.getElementById("TabContainer1_TabAddUser_radPAuthor").checked == true) {
                document.getElementById("TabContainer1_TabAddUser_hidPriority1").value = "";
                document.getElementById("TabContainer1_TabAddUser_txtPAuthor").value = "";
            }
            if (document.getElementById("TabContainer1_TabAddUser_radSAuthor").checked == true) {
                document.getElementById("TabContainer1_TabAddUser_hidPriority2").value = "";
                document.getElementById("TabContainer1_TabAddUser_txtSAuthor").value = "";
            }
            if (document.getElementById("TabContainer1_TabAddUser_radOAuthor").checked == true) {
                document.getElementById("TabContainer1_TabAddUser_hidPriority3").value = "";
                document.getElementById("TabContainer1_TabAddUser_txtOAuthor").value = "";
            }
            return false;
        }
        if (document.getElementById("TabContainer1_TabAddUser_txtPAuthor").value == selUserId || document.getElementById("TabContainer1_TabAddUser_txtSAuthor").value == selUserId || document.getElementById("TabContainer1_TabAddUser_txtOAuthor").value == selUserId) {
            alert(selUserText + " has already been assigned as an authority");
            return false;
        }

        if (document.getElementById("TabContainer1_TabAddUser_radPAuthor").checked == true) {
            document.getElementById("TabContainer1_TabAddUser_txtPAuthor").value = selUser.options[selUser.options.selectedIndex].text;
            document.getElementById("TabContainer1_TabAddUser_hidPriority1").value = selUser.options[selUser.options.selectedIndex].value;
        }
        if (document.getElementById("TabContainer1_TabAddUser_radSAuthor").checked == true) {
            document.getElementById("TabContainer1_TabAddUser_txtSAuthor").value = selUser.options[selUser.options.selectedIndex].text;
            document.getElementById("TabContainer1_TabAddUser_hidPriority2").value = selUser.options[selUser.options.selectedIndex].value;
        }
        if (document.getElementById("TabContainer1_TabAddUser_radOAuthor").checked == true) {
            document.getElementById("TabContainer1_TabAddUser_txtOAuthor").value = selUser.options[selUser.options.selectedIndex].text;
            document.getElementById("TabContainer1_TabAddUser_hidPriority3").value = selUser.options[selUser.options.selectedIndex].value;
        }
    }
    function Checkfiles(fileName) {
                var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
                if (ext == "png" || ext == "PNG" || ext == "jpg" || ext == "JPG") {
                    return true;
                }
                else {
                    return false;
                }
            }

    function Validation(btnId) {
    
        var strUID = document.getElementById("<%=hidUID.ClientID %>").value;
        if (!blankFieldValidation('TabContainer1_TabAddUser_txtfullname', 'Full Name')) {
            return false;
        }
      
        if (!blankFieldValidation('TabContainer1_TabAddUser_txtUserName', 'User Name')) {
            return false;
        }
//        if (!WhiteSpaceValidation1st('TabContainer1_TabAddUser_txtFatherNameInEnglish')) {
//            return false;
//        }
//        if (!blankFieldValidation('TabContainer1_TabAddUser_txtFatherNameInEnglish', 'Father Name(In English)'))
//        {
//        return false;
//    }
//    if (!WhiteSpaceValidation1st('TabContainer1_TabAddUser_txtFatherNameInAmharic')) {
//        return false;
//    }
//        if(!blankFieldValidation('TabContainer1_TabAddUser_txtFatherNameInAmharic','Father Name(In Amharic)'))
//        {
//        return false;
//    }
//    if (!WhiteSpaceValidation1st('TabContainer1_TabAddUser_txtGrandFatherNameInEnglish')) {
//        return false;
//    }
//        if(!blankFieldValidation('TabContainer1_TabAddUser_txtGrandFatherNameInEnglish','Grand Father Name(In English)'))
//        {
//        return false;
//    }
//    if (!WhiteSpaceValidation1st('TabContainer1_TabAddUser_txtGrandFatherNameInAmharic')) {
//        return false;
//    }
//        if(!blankFieldValidation('TabContainer1_TabAddUser_txtGrandFatherNameInAmharic','Grand Father Name(In Amharic)'))
//        {
//        return false;
//        }
        if (document.getElementById('TabContainer1_TabAddUser_hdnAction').value == "A") {
//            if (!selFileToUpload('TabContainer1_TabAddUser_FileUploadImage', 'Image')) {
//                return false;
//            }
            var filename = TabContainer1_TabAddUser_FileUploadImage.value;
            if (filename != "") {
                if (!Checkfiles(filename)) {
                    alert("Please upload JPG,PNG files only");
                    document.getElementById("TabContainer1_TabAddUser_FileUploadImage").focus();
                    return false;
                }
                var file = document.getElementById('TabContainer1_TabAddUser_FileUploadImage');
                if (file.files[0].size >= 1048576) {
                    alert('File size should be less than or Equal to 1 MB');
                    document.getElementById("TabContainer1_TabAddUser_FileUploadImage").focus();
                    return false;
                }
            }
        }
        
        if (document.getElementById('TabContainer1_TabAddUser_hdnAction').value == "A") {
            if (!blankFieldValidation('TabContainer1_TabAddUser_txtPassword', 'Password')) {
                return false;
            }
            if (!isPasswordValidation('TabContainer1_TabAddUser_txtPassword', 'Enter valid password')) {
                return false;
            }
            if (!MinlengthValidation('TabContainer1_TabAddUser_txtPassword', 'Password', 8)) {
                return false;
            }
            if (!checkPassword()) {
                return false;
            }
            if (!MaxlengthValidation('TabContainer1_TabAddUser_txtPassword', 'Password', 12)) {
                return false;
            }
            if (!blankFieldValidation('TabContainer1_TabAddUser_txtConPassword', 'Confirm Password')) {
                return false;
            }
            if (!PasswordValidation('TabContainer1_TabAddUser_txtPassword', 'TabContainer1_TabAddUser_txtConPassword', 'Password', 'Confirm Password')) {
                return false;
            }
        }
        if(document.getElementById("TabContainer1_TabAddUser_txtDomainName")!=null){        
            if (!isAlphaNumericValidation('TabContainer1_TabAddUser_txtDomainName', 'Accepts only alpha numeric value')) {
                return false;
            }
        }
        var dropdownlabel = document.getElementById('TabContainer1_TabAddUser_HierarchyForAllLocation1_Labels1').innerHTML;
        if (!DropDownValidation('TabContainer1_TabAddUser_HierarchyForAllLocation1_sdrplayers0', 'Location')) {
            return false;
        }

        if (!DropDownValidation('TabContainer1_TabAddUser_ddlLocation', 'Physical Location')) {
            return false;
        }

        //********Commented by Amrit till this region********

//        if (!DropDownValidation('TabContainer1_TabAddUser_ddlType', 'Employee Type')) {
//            return false;
//        }
        if (!DropDownValidation('TabContainer1_TabAddUser_ddlDesg', 'Designation')) {
            return false;
        }
//        if(document.getElementById("TabContainer1_TabAddUser_ddlGrade")!=null){        
//            if (!DropDownValidation('TabContainer1_TabAddUser_ddlGrade', 'Grade')) {
//                return false;
//            }
//        }
        if(document.getElementById("TabContainer1_TabAddUser_txtPCD")!=null){        
//            if ((document.getElementById('TabContainer1_TabAddUser_txtDOJ').value != "") && (document.getElementById('TabContainer1_TabAddUser_txtPCD').value != "")) {
//                if (!Comparedatetime('TabContainer1_TabAddUser_txtPCD', 'TabContainer1_TabAddUser_txtDOJ', 'Probation Completion Date', 'Date Of Joining')) {
//                    return false;
//                }
//            }
        }
        if(document.getElementById("TabContainer1_TabAddUser_txtPCD")!=null){        
            if (strUID == null) {        
                if (document.getElementById('TabContainer1_TabAddUser_txtPCD').value != "") {
                    if (!GreaterCurrentDateValidator('TabContainer1_TabAddUser_txtPCD', 'Probation Completion Date')) {
                        return false;
                    }
                }
            }
        }
        if (!DateValidation()) {
            return false;
        }
        if (!blankFieldValidation('TabContainer1_TabAddUser_txtEmail', 'E-Mail')) {
            return false;
        }
        if (!EmailValidation('TabContainer1_TabAddUser_txtEmail')) {
            return false;
        }
        if (!isValidEmail('TabContainer1_TabAddUser_txtEmail')) {
            return false;
        }
        var dropdownlabel = document.getElementById('TabContainer1_TabAddUser_HierarchyForAllLocation1_Labels1').innerHTML;
        if (!DropDownValidation('TabContainer1_TabAddUser_HierarchyForAllLocation1_sdrplayers0', 'Location')) {
            return false;
        }
        if(document.getElementById("TabContainer1_TabAddUser_lblDupUserMsg").innerHTML=="User Name Already Exists"){
             return false;
        }
        if(document.getElementById("TabContainer1_TabAddUser_lblDupDomainMsg")!=null){
            if(document.getElementById("TabContainer1_TabAddUser_lblDupDomainMsg").innerHTML=="Domain User Name Already Exists"){
                 return false;
            }
        }
        if(document.getElementById("TabContainer1_TabAddUser_lblDupEmailMsg").innerHTML=="Email Already Exists"){
             return false;
        }
       
        
           
               
        if (btnId.value == "Save") {
            return confirm("Are You Sure You Want to Save?");
        }
        else {
            return confirm("Are You Sure You Want to Update?");
        }
     }
    function CheckSelect() {
        if (!ConfirmCheck('form1')) {
            return false;
        }
    }
    function password() {
        if (document.getElementById("TabContainer1_TabAddUser_hidpassword").value != '') {
            document.getElementById("TabContainer1_TabAddUser_txtPassword").value = document.getElementById("TabContainer1_TabAddUser_hidpassword").value;
            document.getElementById("TabContainer1_TabAddUser_txtConPassword").value = document.getElementById("TabContainer1_TabAddUser_hidpassword").value;
        }
    }    
    
    /*Add Method th check password and show message in aspan*/
    function CheckPasswordStrength() {
   if(document.getElementById("TabContainer1_TabAddUser_lblDupUserMsg").innerHTML!="User Name Already Exists")
   {
        var txtPwd = document.getElementById("TabContainer1_TabAddUser_txtPassword");
        var error = "";
        var pwdVal=txtPwd.value;
        var re =/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/
        if (!re.test(pwdVal)) {
                     document.getElementById("TabContainer1_TabAddUser_trPwdmsg").style.display="block";
                    document.getElementById("MsgSpan1").innerHTML="Password must have 1 digit,1 special character,1 upper & lower case letter."
                    document.getElementById('TabContainer1_TabAddUser_txtPassword').style.border = "solid 1px red";
           }
        else{
                document.getElementById("TabContainer1_TabAddUser_trPwdmsg").style.display="none";
                document.getElementById("MsgSpan1").innerHTML="";
                document.getElementById('TabContainer1_TabAddUser_txtPassword').style.border = "solid 1px #8b9096";
         }
     }
     else{
        document.getElementById("TabContainer1_TabAddUser_txtUserName").focus();
     }
  }
    /*------------------------------------------------------*/
  function checkPassword() {
   if(document.getElementById("TabContainer1_TabAddUser_lblDupUserMsg").innerHTML!="User Name Already Exists")
   {
        var txtPwd = document.getElementById("TabContainer1_TabAddUser_txtPassword");
        var error = "";
        var pwdVal=txtPwd.value;
        var illegalChars = /[\W_]g/; 
       // var re = /^\w*(?=\w*\d)(?=\w*[a-zA-Z]\w*$)/
        var re =/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/
        if (pwdVal == "") {
        if(document.getElementById("TabContainer1_TabAddUser_txtfullname").value!="" && document.getElementById("TabContainer1_TabAddUser_txtUserName").value!=""){
             alert("Enter a password.");
             txtPwd.focus();
             return false;   
            }                             
        }
       
        if ((pwdVal.length < 8) || (pwdVal.length > 10)) {
           if(document.getElementById("TabContainer1_TabAddUser_txtfullname").value!="" && document.getElementById("TabContainer1_TabAddUser_txtUserName").value!=""){
             alert("The password should contain 8 characters.");
             txtPwd.focus();
             return false;
            }
        }
        else if (illegalChars.test(pwdVal)) {
          if(document.getElementById("TabContainer1_TabAddUser_txtfullname").value!="" && document.getElementById("TabContainer1_TabAddUser_txtUserName").value!=""){
             alert("The password contains illegal characters.");
             txtPwd.focus();
             return false;
            }
        }       
        else if (!re.test(pwdVal)) {
         if(document.getElementById("TabContainer1_TabAddUser_txtfullname").value!="" && document.getElementById("TabContainer1_TabAddUser_txtUserName").value!=""){
            alert("The password must contain at least one uppercase letter,one lowercase letter,one numeral and one special character.");
            txtPwd.focus();
            return false;
           }
        }
        else{
          return true;  
        }
     }
     else{
        document.getElementById("TabContainer1_TabAddUser_txtUserName").focus();
     }
  }
    function ShowHideEPF() {
        if (document.getElementById("TabContainer1_TabAddUser_chkProll").checked == true) {
            document.getElementById("trEpf").style.display = '';
        }
        else {
            document.getElementById("trEpf").style.display = 'none';
        }
    }
    function isSpecialChar1st(tb) {
         var txt = document.getElementById(tb);
        var str = txt.value;
        var iChars = "*|, \":<>[]{}`\'&;()@&$#%!^-_=+./?";
        if(str!=""){
        if (iChars.indexOf(str.charAt(0)) != -1) {
            txt.value = '';
            txt.focus();
            alert('Special characters including space are not allowed as first character.');
        }
       }
    }
    function isWhiteSpace(evt) {
        if (evt.keyCode == 32) {
            alert("Space not allowed");
            return false;
        }
        return true;
    }
    
    function isSpecialChar(tb) {
        var txt = document.getElementById(tb);
        var str = txt.value;
        var iChars = "*|, \":<>[]{}`\'&;()&$%!^-=+./?";
        for (var i = 0; i < str.length; i++) {
            if (iChars.indexOf(str.charAt(i)) != -1) {
                txt.value = str.substr(0, i);
                txt.focus();
                alert("Only @#_ is allowed as Special Character");
                return false;
            }
        }
    }
    function isValidEmail(txt) {
        var s = document.getElementById(txt).value;
        var regexp = /^([a-zA-Z0-9_.-]*)\@{1}([a-zA-Z0-9_-]*)\.{1}([a-zA-Z0-9.]*)$/
        if (regexp.test(s)) {
            return true
        }
        else {
            alert("enter valid Email-Id");
            return false
        }
    }
    function ShowHideSuperAdmin() {
        var status = '<%=strUserAttribute%>';
        if (status == 'super') {
            document.getElementById('TabContainer1_TabAddUser_rAdminprevil').style.display = "";
        }
        else {
            document.getElementById('TabContainer1_TabAddUser_rAdminprevil').style.display = "none";
        }
    }
    function GetSelected(ddlId) {
        document.getElementById("TabContainer1_TabAddUser_hidDesigId").value = "";
        document.getElementById("TabContainer1_TabAddUser_hidDesigId").value = ddlId.value;
    }
    function GetSelectedGradeId(ddlId) {

        document.getElementById("TabContainer1_TabAddUser_hidGradeId").value = "";
        document.getElementById("TabContainer1_TabAddUser_hidGradeId").value = ddlId.value;
    }
    function DateValidation() {
        var monthsArr = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
        var objDOB = document.getElementById("TabContainer1_TabAddUser_txtDOB").value;
        var dtDOB = objDOB.split('-');
        for (var i = 0; i < monthsArr.length; i++) {
            if (dtDOB[1] == monthsArr[i]) {
                dtDOB[1] = i + 1;
                break;
            }
        }
        objDOB = dtDOB[1] + '/' + dtDOB[0] + '/' + dtDOB[2];
        var objDOJ = document.getElementById("TabContainer1_TabAddUser_txtDOJ").value;
        var dtDOJ = objDOJ.split('-');
        for (var i = 0; i < monthsArr.length; i++) {
            if (dtDOJ[1] == monthsArr[i]) {
                dtDOJ[1] = i + 1;
                break;
            }
        }
        objDOJ = dtDOJ[1] + '/' + dtDOJ[0] + '/' + dtDOJ[2];
        if(document.getElementById("TabContainer1_TabAddUser_txtPCD")!=null){
            var objProbation = document.getElementById("TabContainer1_TabAddUser_txtPCD").value;
            var dtProbation = objProbation.split('-');
            for (var i = 0; i < monthsArr.length; i++) {
                if (dtProbation[1] == monthsArr[i]) {
                    dtProbation[1] = i + 1;
                    break;
                }
            }
       
            objProbation = dtProbation[1] + '/' + dtProbation[0] + '/' + dtProbation[2];
            var dateProbation = new Date(objProbation);   
        }        
        var dateDob = new Date(objDOB);      
        var dateDoj = new Date(objDOJ);
        var currentDate = new Date();
        if(document.getElementById("TabContainer1_TabAddUser_txtPCD")!=null){
//            if (dateDoj > dateProbation) {
//                alert("Date-Of-Joining should be less than Probation Complete Date");
//                return false;
//            }
//            if (dateDob > dateProbation) {
//                alert("Date-Of-Birth should be less than  Probation Complete Date");
//                return false;
//            }
        }
        
        if (dateDob > dateDoj) {
            alert("Date-Of-Birth should be less than Date-Of-Joining");
            return false;
        }        
        if (dateDob > currentDate) {
            alert("Date Of Birth should be less than CurrentDate");
            return false;
        }
        var date2 = new Date(objDOB);
        var y1 = currentDate.getFullYear(); //getting current year       
        var y2 = date2.getFullYear(); //getting dob year
        var age = y1 - y2;
        if (age < 18) {
            alert("Please select valid Date of Birth (User must be atleast of 18 years)");
            return false;
        }
        if (dateDoj > currentDate) {
            alert("Date-Of-Joining should be less than CurrentDate");
            return false;
        }
        
      
      return true;      
       
    }
    function ConfirmResetCancel(btnId) {
        if (btnId.value == "Reset") {
            return confirm('Do you want to reset?');
        }
        else {
            return confirm('Do you want to cancel?');
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
        
    
  
</script>

</html>
