<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultUser.aspx.cs" Inherits="Portal_DefaultUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
            <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>SWP(single Window Portal)</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta charset="utf-8" />
<meta name="description" content="ODISHA RANKS 1st IN INDIA with 17% of the total live projects in manufacturing sector worth Rs.33 lakh crore.,BRANDING ODISHA Attractive destination in the identified focus sectors of the state, LARGE SCALE INDUSTRIES The state by providing necessary support services,STRENGTHS & COMPETITIVENESS Highlighting the investment opportunities in the state" />
<meta name="author" content="IPICOL"/>
<meta name="keywords" content="Invest, Odisha, Investor, IPICOl, Industry, Industries, Team Odisha, Indudtrial Invest in Odisha,Single Window Portal"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
<link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
<link href="../css/login.css" rel="stylesheet" type="text/css" />

        <!--<link href="assets/dist/css/stylecrm-rtl.css" rel="stylesheet" type="text/css"/>-->
        <script src="Console/jscript48/Validator.js" type="text/javascript"></script>
        <script type="text/javascript" language="javascript" src="Console/scripts/md5.js"></script>
        <script type="text/javascript" language="javascript" src="Console/scripts/swfobject.js"></script>
        <script src="Console/scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
        <script src="Console/scripts/popup.js" type="text/javascript"></script>
   <script language="javascript" type="text/javascript">
       //Function to Refresh the Captcha Image
       function refresh() {
           var labelValue = document.getElementById("<%= lblCaptcha.ClientID %>").innerHTML;
           PageMethods.RefrshCaptcha(CallSuccess);
           return false;
       }
       function CallSuccess(rslt) {
           document.getElementById("<%= lblCaptcha.ClientID %>").innerHTML = rslt;
           document.getElementById("<%=HiddenField1.ClientID %>").value = rslt;
       }
    </script>
        <script type="text/javascript" language="JavaScript">

            function onload() {
                var login = document.getElementById("hidmsg").value;
                if (login != "") {
                    alert(login);
                }
                document.getElementById("txtusr").focus();
            }
            function validation() {
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
                if (!chkSingleQuote('txtusr')) {
                    return false;
                }
                if (!blankFieldValidation('txtpwd', 'Password')) {
                    document.getElementById("txtpwd").focus();
                    return false;
                }
                if (!chkSingleQuote('txtPwd')) {
                    return false;
                }
                var captchaVal = document.getElementById("captchaID").value;
                if (captchaVal != document.getElementById("HiddenField1").value) {
                    jAlert('<strong>Please enter the correct code. !</strong>', 'SWP');
                    document.getElementById("captchaID").value = "";
                    document.getElementById("captchaID").focus();
                    return false;
                }


            }
            function randomtext() {

                var the_number = Math.floor(Math.random() * 500);
                return (the_number)
            }
            function ResetValues() {
                document.getElementById("txtOldPsw").value = '';
                document.getElementById("txtNewPsw").value = '';
                document.getElementById("txtRetypPsw").value = '';
                document.getElementById("CodeNumberTextBox").value = '';
                return false;
            }
            function ChangePwdValidation() {
                debugger;
                var captchaVal = '<%=Session["CaptchaImageText"]%>';
                if (!blankFieldValidation('txtOldPsw', 'Old Password')) {
                    document.getElementById("txtOldPsw").focus();
                    return false;
                }
                if (!chkSingleQuote('txtOldPsw')) {
                    return false;
                }
                if (!blankFieldValidation('txtNewPsw', 'New Password')) {
                    document.getElementById("txtNewPsw").focus();
                    return false;
                }
                if (document.getElementById("txtOldPsw").value == document.getElementById("txtNewPsw").value) {
                    document.getElementById("txtNewPsw").focus();
                    alert("New Password Can not be same as Old Password.");
                    return false;
                }
                if (!chkSingleQuote('txtNewPsw')) {
                    return false;
                }
                if (!MinlengthValidation('txtNewPsw', 'New Password', 6)) {
                    return false;
                }
                if (!blankFieldValidation('txtRetypPsw', 'Retype Password')) {
                    document.getElementById("txtNewPsw").focus();
                    return false;
                }
                if (!chkSingleQuote('txtRetypPsw')) {
                    return false;
                }

                if (!PasswordValidation('txtNewPsw', 'txtRetypPsw', 'New Password', 'Retype Password')) {
                    document.getElementById("txtRetypPsw").value = "";
                    return false;
                }
                //                if (captchaVal != document.getElementById("CodeNumberTextBox").value) {
                //                    alert('Please enter the correct code.');
                //                    document.getElementById("CodeNumberTextBox").value = "";
                //                    document.getElementById("CodeNumberTextBox").focus();
                //                    return false;
                //                }


            }
    </script>
    <style>
    input[type="checkbox"], input[type="radio"] { margin: 4px 5px 0 10px;}

    </style>
        <style type="text/css">
    .captchagroup .refresh-btn {
    border: 1px solid #dadada;
    margin-left: -1px;
   float: right;
    padding: 9px 12px 9px;
    font-size: 22px;
    color: #c50c13;
}
    </style>
        <script type="text/javascript">
            $(document).ready(function () {
                refresh();
            });
    </script>
</head>
<body>
  <form id="form1" runat="server">
         <!-- Content Wrapper -->
         <div class="login"> 
         <div class="top-header">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
    <div class="logo"><a href="" alt="Solid Waste Monitoring System Logo"><img src="images/logo2.png" ><img src="images/logo.png" ></a>
    <span class="clr"></span>
     <h1 >SWP(Single Window Portal)</h1>
    </div>
    
    <div class="clr"></div>
  </div>
  
  <div class="login-inner">
     <div class="form">
         <div id="Div1" class="form-group" runat="server" visible="false">
                                <label class="control-label" for="username">UserType</label>
                                <asp:RadioButton ID="rbtSelf" runat="server" Text="Self" Checked="true" GroupName="grp"/>
                                <asp:RadioButton ID="rbtProxy" runat="server" Text="Proxy" GroupName="grp" />
                            </div>

      <div class="field-wrap" id="uid">
        <label> Enter User Id </label>
       
          <asp:TextBox ID="txtusr" runat="server"   AutoCompleteType="disabled" autocomplete="false" onPaste="return false"  class="form-control"></asp:TextBox>

      </div>
     <%-- <div class="field-wrap" id="eamil">
        <label> Enter Email </label>
        <input type="text" class="emailInput" id="txtEmailID" name="txtEmailID"/>
      </div>--%>
      <div class="field-wrap" id="pass" style="display: none;">
        <label style="display:none"> Password </label>
     <asp:TextBox class="form-control"  ID="txtpwd"   runat="server"  AutoCompleteType="disabled" autocomplete="false" onPaste="return false" TextMode="Password" Visible="false"></asp:TextBox>
                                  <input type="hidden" name="linkm" value="linkm" />
                                        <input type="hidden" name="linkn" value="linkn" />
                                        <input type="hidden" name="fname" value="fname" />
                                        <asp:HiddenField ID="hidSlt" runat="server" />
      </div>

 <div>
                            <div class="field-wrap captcha-sec " id="Div2">
                                <asp:TextBox ID="captchaID" class="form-control" autocomplete="false" runat="server" AutoCompleteType="disabled"
                                    onPaste="return false" placeholder="Captcha"></asp:TextBox>
                                <%--  <input type="text" id="captchaID" class="form-control" placeholder="Captcha" required autocomplete="off"/>--%>
                            </div>
                            <div class="field-wrap captcha-sec" id="Div3">
                                <div class="captchagroup">
                                 
                                        <asp:Label ID="lblCaptcha" runat="server" CssClass="captchalabel" Font-Italic="True" ></asp:Label>
                                    <asp:LinkButton ID="Button1" runat="server" CssClass="refresh-btn"  OnClientClick="return refresh();">
                                     <span class="fa fa-refresh"
                                            aria-hidden="true"></span></asp:LinkButton>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                </div>
                            </div>
                            <div class="clr">
                            </div>
                        </div>
    <p class="forgot"><a href="forgotpass.aspx" id="forgotPwdLink">Forgot Password? </a> 
      </p>



        <asp:Button class="button button-block" ID="btnSubmit" runat="server" Text="Login" 
                                            OnClick="btnSubmit_Click" />
                                <input name="hidden" type="hidden" id="hidmsg" runat="server" />


     <%-- <button class="button button-block " id="Button1" name="btnSubmit"  /> Submit </button>--%>
   
    </div>
    </div>
         </div>
         <div class="footer">&copy; Copyright 2017 SWP, All Rights Reserved.</div>

    
        <!-- /.content-wrapper -->
        <!-- jQuery -->
        <script src="js/jquery-1.12.4.min.js" type="text/javascript"></script>
        <!-- bootstrap js -->
        <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/login.js" type="text/javascript"></script>
    </form>
</body>
</html>
