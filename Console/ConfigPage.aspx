<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigPage.aspx.cs" Inherits="KwantifyportalV5._1.Console.ConfigPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Console::Configuration</title>
    <link href="style/default.css" rel="stylesheet" type="text/css" />
    <link href="style/tooltipster.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>

    <script src="scripts/jquery.tooltipster.js" type="text/javascript"></script>

    <script src="scripts/ModuleConfig.js" type="text/javascript"></script>

    <style type="text/css">
        body
        {
            background-color: Teal;
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
        #divAdd a:link
        {
            text-decoration: none;
            font-weight: normal;
        }
        #divAdd a:active
        {
            text-decoration: none;
            font-weight: normal;
        }
        #divAdd a:hover
        {
            text-decoration: underline;
            font-weight: bold;
        }
        .overlayContent
        {
            z-index: 50;
            margin: 50px auto;
            width: 50px;
            height: 50px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            color: #000;
        }
        .tooltip
        {
        }
        .divcontent
        {
        }
        .divcontent2 div
        {
        }
        .divcontent3
        {
        }
        .divcontent4
        {
        }
        .divCaution
        {
            margin-left: 280px;
            width: 60%;
            margin-top: 20px;
            font-size: 10px;
            font-weight: bold;
            text-align: right;
            height: 20px;
            font-family: Microsoft Sans Serif;
        }
        .divheading
        {
            font-size: 14px;
            font-weight: bold;
            border: solid 1px silver;
            text-align: left;
            margin-left: 280px;
            width: 60%;
            background-color: #F0F0F0;
            cursor: pointer;
            margin-top: 20px;
            background: url(images/botBg.png) repeat-x left top;
            font-family: Microsoft Sans Serif;
        }
        .divheading2
        {
            font-size: 14px;
            font-weight: bold;
            border: solid 1px silver;
            text-align: left;
            margin-left: 280px;
            width: 60%;
            background-color: #F0F0F0;
            cursor: pointer;
            margin-top: 2px;
            background: url(images/botBg.png) repeat-x left top;
            font-family: Microsoft Sans Serif;
        }
        .divheading3
        {
            font-size: 14px;
            font-weight: bold;
            border: solid 1px silver;
            text-align: left;
            margin-left: 280px;
            width: 60%;
            background-color: #F0F0F0;
            cursor: pointer;
            margin-top: 2px;
            background: url(images/botBg.png) repeat-x left top;
            font-family: Microsoft Sans Serif;
        }
        .divheading4
        {
            font-size: 14px;
            font-weight: bold;
            border: solid 1px silver;
            text-align: left;
            margin-left: 280px;
            width: 60%;
            background-color: #F0F0F0;
            cursor: pointer;
            margin-top: 2px;
            background: url(images/botBg.png) repeat-x left top;
            font-family: Microsoft Sans Serif;
        }
    </style>

    <script type="text/javascript">
      function pageLoad() {    
       jQuery(document).ready(function() {
              jQuery(".divheading").click(function() {                 
                 jQuery(".divcontent").slideToggle(300);
            });
        });
       jQuery(document).ready(function() {
              jQuery(".divcontent2").hide();
              jQuery(".divheading2").click(function() {                 
                 jQuery(".divcontent2").slideToggle(300);
            });
        });
        jQuery(document).ready(function() {
              jQuery(".divcontent3").hide();
              jQuery(".divheading3").click(function() {                 
                 jQuery(".divcontent3").slideToggle(300);
            });
        });
      jQuery(document).ready(function() {
              jQuery(".divcontent4").hide();
              jQuery(".divheading4").click(function() {                 
                 jQuery(".divcontent4").slideToggle(300);
            });
        });
 
    $(document).ready(function() {
			$('.tooltip').tooltipster();
		});
	}
		
    function Validation(){
    debugger
    var home=document.getElementById('txtHomeVal').value;
    var logout=document.getElementById('txtLogoutVal').value
    var menu=document.getElementById('txtMenuName').value
     var rediirectpage=document.getElementById('txtSessionRedirect').value
        if (home=="" ||home=="e.g. ~/Dashboard/Default.aspx")
        {
            alert('Please Type Home Key Value !');
             return false;
        } 
        else if (logout=="" ||logout=="e.g. ~/Default.aspx")
        {
            alert('Please Type Logout Key Value !');
             return false;
        }   
        else if (menu=="" ||menu=="e.g. Kwantify")
        {
            alert('Please Type Hierarchy Menu Name !');
             return false; 
        } 
        else if(rediirectpage =="" || rediirectpage=="e.g. ~/SessionRedirect.aspx"){
             alert('Please Enter Page Name To Redirect While Session Lost !');
             return false;  
        }        
        return true;
    }
    function CheckConfirm(btnid){
    debugger;
        if(btnid.value.toLowerCase()=="submit"){
            if(Validation()){
                return confirm('Do you want to submit ?');               
            }
            else{
                 return false;                 
            }
        }
    }
//function disableSubmit(){           
//   $(function() {
//$('#btnClick').click(function() {
//$(this).attr("disabled", true);
//$(this).val('Please wait Processing');
//// Write your Code
//})
//$('#btnReset').click(function() {
//$('#btnClick').attr("disabled", false);
//$('#btnClick').val('Click');
//})
//})
//} 
 function ChelAllTreeView(){
if(document.getElementById("chkAllMod").checked==true){     
        $("#tvML input:checkbox").attr("checked",true);
        $("#tvMU input:checkbox").attr("checked",true);
        $("#tvOT input:checkbox").attr("checked",true);
        $("#tvRF input:checkbox").attr("checked",true);
        $("#tvReports input:checkbox").attr("checked",true);
        $("#tvPIP input:checkbox").attr("checked",true);
        $("#tvPLS input:checkbox").attr("checked",true);

    }else{
            $("#tvML input:checkbox").attr("checked",false);
            $("#tvMU input:checkbox").attr("checked",false);
            $("#tvOT input:checkbox").attr("checked",false);
            $("#tvRF input:checkbox").attr("checked",false);
            $("#tvReports input:checkbox").attr("checked",false);
            $("#tvPIP input:checkbox").attr("checked",false);
            $("#tvPLS input:checkbox").attr("checked",false);

    }
    
 }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="divCaution">
                <div style="float: left; width: 60%">
                    <img src="images/caution.jpg" width="18px" height="18px" alt="a11" />
                </div>
                <div style="float: left; text-align: right; padding-top: 3px; width: 35%">
                    Check the checkbox to configure particular.
                </div>
            </div>
            <div class="divheading">
                <asp:CheckBox ID="chkGenConfig" Checked="true" runat="server" />
                General Configuration
            </div>
            <div class="divcontent">
                <div id="divAdd" class="addTable" style="border: solid 1px silver; border-top: 0;
                    background-color: #F0F0F0; text-align: left; margin-left: 280px; width: 59.25%">
                    <table border="0" cellpadding="0" cellspacing="0" style="border: solid 0px silver">
                        <tr>
                            <td align="left" width="200px">
                                <span id="spanFlagKey" class="tooltip" title="The '<font color='red'><bold>SetFlag</bold></font>' key is used to insert<br/> record in SectionMaster table. If value is '<font color='red'><bold>Y</bold></font>' <br/>then the same will happen."
                                    style="font-weight: bold; cursor: pointer">SetFlag Key Value</span> <span style="color: red">
                                        *</span>
                            </td>
                            <td>
                                <strong>:</strong>
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rbtSFY" runat="server" GroupName="b" Text="Yes" />
                                <asp:RadioButton ID="rbtSFN" runat="server" Checked="true" GroupName="b" Text="No" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <span id="span2" class="tooltip" title="The '<font color='red'>XmlDel</font>' key is used to modify<br/> user xml after deleting it when auser modify global<br/>  link and primary link name, set permission etc."
                                    style="font-weight: bold; cursor: pointer">XmlDel Key Value</span> <span style="color: red">
                                        *</span>
                            </td>
                            <td>
                                <strong>:</strong>
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rbtXDY" GroupName="c" Checked="true" runat="server" Text="Yes" />
                                <asp:RadioButton ID="rbtXDN" GroupName="c" runat="server" Text="No" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <span id="span3" class="tooltip" title="The '<font color='red'>Home</font>' key is used to set the '<font color='yellow'>Home</font>' <br/> button postback url of Admin Console module."
                                    style="font-weight: bold; cursor: pointer">Home Key value</span> <span style="color: red">
                                        *</span>
                            </td>
                            <td>
                                <strong>:</strong>
                            </td>
                            <td align="left">
                                &nbsp;
                                <asp:TextBox ID="txtHomeVal" runat="server" Width="200px"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="txtHomeVal_TextBoxWatermarkExtender" runat="server"
                                    Enabled="True" TargetControlID="txtHomeVal" WatermarkCssClass="WaterMarkedTextBox"
                                    WatermarkText="e.g. ~/Dashboard/Default.aspx">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <span id="span4" class="tooltip" title="The '<font color='red'>Logout</font>' key is used to set the '<font color='yellow'>Logout</font>'<br/> button postback url of Admin Console module."
                                    style="font-weight: bold; cursor: pointer">Logout Key Value</span> <span style="color: red">
                                        *</span>
                            </td>
                            <td>
                                <strong>:</strong>
                            </td>
                            <td align="left">
                                &nbsp;
                                <asp:TextBox ID="txtLogoutVal" runat="server" Width="200px"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="txtLogoutVal_TextBoxWatermarkExtender" runat="server"
                                    Enabled="True" TargetControlID="txtLogoutVal" WatermarkCssClass="WaterMarkedTextBox"
                                    WatermarkText="e.g. ~/Default.aspx">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <span id="span5" class="tooltip" title="The '<font color='red'>HierMenuName</font>' key is used to change the <br/>Admin Console hierarchy treeview root nood name.<br/><img src='images/menuimage.jpg' width='200px' height='150px'/>"
                                    style="font-weight: bold; cursor: pointer">Hierarchy Menu Name</span> <span style="color: red">
                                        *</span>
                            </td>
                            <td>
                                <strong>:</strong>
                            </td>
                            <td align="left">
                                &nbsp;
                                <asp:TextBox ID="txtMenuName" runat="server" Width="200px"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="txtMenuName_TextBoxWatermarkExtender" runat="server"
                                    Enabled="True" TargetControlID="txtMenuName" WatermarkCssClass="WaterMarkedTextBox"
                                    WatermarkText="e.g. Kwantify">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <span id="span6" class="tooltip" title="The text waht you will enter for this will <br/>show as summary for Root Node Summary.<br/><img src='images/nodeSummary.jpg' width='300px' height='120px' alt='d1' />"
                                    style="font-weight: bold; cursor: pointer">Hierarchy Root Node Summary</span>
                            </td>
                            <td>
                                <strong>:</strong>
                            </td>
                            <td align="left">
                                &nbsp;
                                <asp:TextBox ID="txtRootSummary" runat="server" style="resize:none" Width="200px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <span id="span8" class="tooltip" title="The '<font color='red'>HierMenuName</font>' key is used to change the <br/>Admin Console hierarchy treeview root nood name.<br/><img src='images/menuimage.jpg' width='200px' height='150px'/>"
                                    style="font-weight: bold; cursor: pointer">Session Redirect Page</span> <span style="color: red">
                                        *</span>
                            </td>
                            <td>
                                <strong>:</strong>
                            </td>
                            <td align="left">
                                &nbsp;
                                <asp:TextBox ID="txtSessionRedirect" runat="server" Width="200px"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" Enabled="True"
                                    TargetControlID="txtSessionRedirect" WatermarkCssClass="WaterMarkedTextBox" WatermarkText="e.g. ~/SessionRedirect.aspx">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <%--Database Configuration--%>
            <div class="divheading2">
                <asp:CheckBox ID="chkDbConfig" Checked="true" runat="server" />
                Database Configuration
            </div>
            <div class="divcontent2">
                <div id="div2" class="addTable" style="border: solid 1px silver; border-top: 0; background-color: #F0F0F0;
                    text-align: left; margin-left: 280px; width: 59.25%">
                    <table border="0" cellpadding="0" cellspacing="0" style="border: solid 0px silver">
                        <tr>
                            <td align="left" width="200px">
                                <span id="span1" class="tooltip" title="If you are a existing user of '<font color='red'>Admin Console</strong></font>' <br/>module then choose '<bold><font color='green'>Existing User</font></bold>' <br/>to update enhancements otherwise choose New User."
                                    style="font-weight: bold; cursor: pointer">Integration Status</span> <span style="color: red">
                                        *</span>
                            </td>
                            <td width="5px">
                                <strong>:</strong>
                            </td>
                            <td align="left" width="300px">
                                <asp:RadioButton ID="rbtnNewUser" runat="server" Checked="True" Text="New User" GroupName="a" />
                                <asp:RadioButton ID="rbtnExistingUser" runat="server" GroupName="a" Text="Existing User" />
                            </td>
                            <td width="150px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <%--For AddPage Configuration--%>
            <div class="divheading3">
                <asp:CheckBox ID="chkAddConfig" Checked="true" runat="server" />
                User Add Page Configuration
            </div>
            <div class="divcontent3">
                <div style="color: Teal; font-size: 12px; text-decoration: underline;border: solid 1px silver; border-top: 0; border-bottom:0; background-color: #F0F0F0;
                    text-align: left; margin-left: 280px; height:25px; padding-top:5px; padding-left:5px; width: 59.62%">
                    Optional Controls of AddUser page</div>
                <div id="div1" class="addTable" style="border: solid 1px silver; border-top: 0; background-color: #F0F0F0;
                    text-align: left; margin-left: 280px; width: 59.25%">
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
                                <asp:CheckBox ID="chkAttendance" runat="server" />
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
                                <asp:CheckBox ID="chkPayroll" runat="server" />
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
                        </tr>
                    </table>
                </div>
            </div>
            <%--For Module Configuration--%>
            <div class="divheading4">
                <asp:CheckBox ID="chkModuleConfig" Checked="true" runat="server" />
                Module Configuration
            </div>
            <div class="divcontent4">
                <div id="div3" class="addTable" style="border: solid 1px silver; border-top: 0; background-color: #F0F0F0;
                    text-align: left; margin-left: 280px; width: 59.25%">
                    <table border="0" cellpadding="0" cellspacing="0" style="border: solid 0px silver">
                        <tr>
                            <td align="left" width="200px">
                                <strong>Manage Hierarchy</strong>
                               
                            </td>
                            <td width="5px">
                                &nbsp;
                            </td>
                            <td align="left" width="300px">
                                 <asp:CheckBox ID="chkMhierarchy" runat="server" Checked="True" 
                                    Enabled="False" /><span style="color:Teal">(Mandatory Module)</span></td>
                            <td width="150px" align="right">
                                &nbsp;
                                <asp:CheckBox ID="chkAllMod" runat="server" onclick="ChelAllTreeView()" 
                                    Text="Check All Modules." BorderColor="#666666" BorderStyle="Dotted" 
                                    BorderWidth="1px" Font-Bold="True" ForeColor="Red" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="200px" valign="top">
                                <asp:TreeView ID="tvML" runat="server" CollapseImageUrl="~/Console/images/collapseAll.jpg"
                                    ExpandImageUrl="~/Console/images/expand_all.gif" ShowCheckBoxes="All">
                                </asp:TreeView>
                            </td>
                            <td width="5px">
                                &nbsp;
                            </td>
                            <td align="left" width="300px" valign="top">
                                <asp:TreeView ID="tvMU" runat="server" CollapseImageUrl="~/Console/images/collapseAll.jpg"
                                    ExpandImageUrl="~/Console/images/expand_all.gif" ShowCheckBoxes="All">
                                </asp:TreeView>
                            </td>
                            <td width="150px" align="left" valign="top">
                                <asp:TreeView ID="tvOT" runat="server" CollapseImageUrl="~/Console/images/collapseAll.jpg"
                                    ExpandImageUrl="~/Console/images/expand_all.gif" ShowCheckBoxes="All">
                                </asp:TreeView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="200px" valign="top">
                                <asp:TreeView ID="tvRF" runat="server" CollapseImageUrl="~/Console/images/collapseAll.jpg"
                                    ExpandImageUrl="~/Console/images/expand_all.gif" ShowCheckBoxes="All">
                                </asp:TreeView>
                            </td>
                            <td width="5px">
                                &nbsp;
                            </td>
                            <td align="left" width="300px" valign="top">
                                <asp:TreeView ID="tvReports" runat="server" CollapseImageUrl="~/Console/images/collapseAll.jpg"
                                    ExpandImageUrl="~/Console/images/expand_all.gif" ShowCheckBoxes="All">
                                </asp:TreeView>
                            </td>
                            <td width="150px" align="left" valign="top">
                                <asp:TreeView ID="tvPLS" runat="server" CollapseImageUrl="~/Console/images/collapseAll.jpg"
                                    ExpandImageUrl="~/Console/images/expand_all.gif" ShowCheckBoxes="All">
                                </asp:TreeView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="200px" valign="top">
                                <asp:TreeView ID="tvPIP" runat="server" CollapseImageUrl="~/Console/images/collapseAll.jpg"
                                    ExpandImageUrl="~/Console/images/expand_all.gif" ShowCheckBoxes="All">
                                </asp:TreeView>
                            </td>
                            <td width="5px">
                                &nbsp;
                            </td>
                            <td align="left" width="300px">
                                &nbsp;
                            </td>
                            <td width="150px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="200px">
                                &nbsp;
                            </td>
                            <td width="5px">
                                &nbsp;
                            </td>
                            <td align="left" width="300px">
                                &nbsp;
                            </td>
                            <td width="150px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="addTable" style="border: none; text-align: left; background-color:Teal; margin-left: 280px;
                width: 59.35%">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="200px">
                            &nbsp;
                        </td>
                        <td width="5px">
                            &nbsp;
                        </td>
                        <td align="left" width="300px">
                            &nbsp;
                            <asp:Button ID="btnSubmit" runat="server" OnClientClick="return CheckConfirm(this);"
                                OnClick="btnSubmit_Click" Text="Submit" Width="60px" />
                            &nbsp;<asp:Button ID="btnReset" runat="server" Text="Reset" Width="60px" OnClick="btnReset_Click" />
                        </td>
                        <td align="left" width="150px">
                            <asp:LinkButton ID="lnkGoLogin" runat="server" Visible="false" OnClick="lnkGoLogin_Click">&gt;&gt;Go 
                    To Login&gt;&gt;</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Label ID="lblProgressmsg" runat="server" Text=""></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlayContent">
                <img src="images/Loading.gif" alt="Loading" />
                Loading...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
