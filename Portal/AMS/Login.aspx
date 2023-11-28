<%--'******************************************************************************
' File Name             :   Login.aspx
' Description           :   The respective Department admin need to be login for admin related activity.
' Created by            :   Himalaya Pagada
' Created On            :   06-04-2014
' Modification History  :
'                           <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
'
' Register File Name    :   
' Style sheet           :   bootstrap.min.css,custom.css
' JavaScript            :   jquery.min.js,bootstrap.min.js
'**************************************************************************************/--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html>
<head>
    <title>Login - Agenda Monitoring System</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <!--[if lte IE 8]>
<script src="js/html5shiv.js"></script>
<script src="js/respond.min.js"></script>
<![endif]-->
    <link href="css/login.css" rel="stylesheet" />
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/md5.js" type="text/javascript"></script>
    <script src="js/Validator.js" type="text/javascript"></script>
    <script type="text/javascript">
        //window.history.forward();

        function onload() {
            var login = document.getElementById("hidmsg").value;
            //alert(document.getElementById("hidmsg"));
            //alert(document.getElementById("hidmsg").value);
            if (login != "") {
                alert(login);
            }
            document.getElementById("txtusr").focus();
        }

        function Forgetpassword() {
            if (!blankFieldValidation('txtUser', 'UserId')) {
                document.getElementById("txtUser").focus();
                return false;
            }

            if (document.getElementById('txtEmail').value != '') {

                if (EmailValid($.trim($('#txtEmail').val()))) {
                    alert('Please Enter Valid Email Id');
                    $('#txtEmail').focus();
                    return false;
                }
            }
        }
        function CheckValidation() {
            if (document.getElementById("txtusr").value == "Enter User Id") {
                document.getElementById("txtusr").value = '';
                document.getElementById("txtusr").focus();
                alert("Please Enter User Id");
                return false;
            }
            if (!blankFieldValidation('txtusr', 'UserId')) {
                document.getElementById("txtpwd").focus();
                return false;
            }
            if (!chkSingleQuote('txtusr')) {
                return false;
            }
            if (!blankFieldValidation('txtpwd', 'Password')) {
                document.getElementById("txtpwd").focus();
                return false;
            }
            if (!chkSingleQuote('txtpwd')) {
                return false;
            }
            if (document.getElementById("txtpwd").value != "")
                var str2;
            var slt;
            slt = randomtext();
            document.getElementById("hidSlt").value = slt;
            str2 = hex_md5(document.getElementById("txtpwd").value).toUpperCase() + slt;
            document.getElementById("txtpwd").value = hex_md5(str2).toUpperCase();
        }
        function randomtext() {

            var the_number = Math.floor(Math.random() * 500);
            return (the_number)
        }
        function IsSpecialCharacter1stPalce(cntr) {
            //            var strValue = $('#' + cntr).val();
            var strValue = $(cntr).val();
            var FistChar = strValue.charAt(0);
            if (/^[a-zA-Z0-9]*$/.test(FistChar) == false) {
                alert('Special characters and white space are not allowed at 1st place !!!');
                $(cntr).val('');
                $(cntr).focus();
                return false;
            } else { return true; }
            return true;
        }
        function EmailValid(email) {
            if (email.length == 0)
                return false;
            filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (filter.test(email))
                return false;
            else
                return true;
        }

        function noBack() {
            window.history.go(-1);
        }

    </script>
</head>
<body class="ap_bg" onload="onload()">
    <form id="form1" runat="server" class="login_form" defaultfocus="txtusr" defaultbutton="btnSubmit">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="loginHeader">
        <div class="login-heading">
            <div id="logo">
                <img src="img/CICG-Logo.png" alt="Agenda Monitoring System, Invest Odisha, Come. invest. Grow"
                    title="Agenda Monitoring System, Industrial Promotion & Investment Corporation of Odisha Ltd."
                    border="0" align="absmiddle" class="pull-left" />
                <div class="pull-left">
                    <h1 style="margin-top: 15px;">
                        Online Agenda Monitoring System</h1>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <div id="loginArea">
        <div class="loginBox">
            <div class="header">
                <h1>
                    User Login
                </h1>
            </div>
            <div id="loginCont" runat="server">
                <asp:TextBox ID="txtusr" CssClass="userinput" runat="server" MaxLength="20" onblur="if (this.value == '') {this.value = 'Enter User Id';}"
                    onfocus="if (this.value == 'Enter User Id') {this.value = '';}" value="Enter User Id"
                    ToolTip="Enter User Id" onKeyup="IsSpecialCharacter1stPalce(this);"> </asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="Flt_txtUserName" runat="server" Enabled="True" TargetControlID="txtusr"
                    FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                    ValidChars="._@ ">
                </cc1:FilteredTextBoxExtender>
                <asp:TextBox ID="txtpwd" CssClass="passwordinput" TextMode="Password" onblur="if (this.value == '') {this.value = 'Enter Password';}"
                    onfocus="if (this.value == 'Enter Password') {this.value = '';}" value="Enter Password"
                    ToolTip="Enter User id" runat="server" onKeyup="IsSpecialCharacter1stPalce(this);"> </asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="Flt_txtpwd" runat="server" Enabled="True" TargetControlID="txtpwd"
                    FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                    ValidChars="_-@#$ ">
                </cc1:FilteredTextBoxExtender>
                <asp:Button ID="btnSubmit" runat="server" Text="LOGIN" CssClass="btn LoginBtn" OnClick="btnSubmit_Click" />
                <asp:HiddenField ID="hidSlt" runat="server" />
                <asp:HiddenField ID="hidmsg" runat="server" />
                <asp:LinkButton ID="lbtnForgot" runat="server" CssClass="pull-right forgot" OnClick="lbtnForgot_Click">Forgot Password?</asp:LinkButton>
                <div class="clearfix">
                </div>
            </div>
        </div>
        <div class="clearfix">
        </div>
    </div>
    <div class="footer">
        Copyright &copy; 2016 IPICOL. All Rights Reserved.</div>
    </form>
</body>
</html>
