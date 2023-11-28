<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvestorProxyLogin.aspx.cs" Inherits="InvestorProxyLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<head id="Head1" runat="server">
    <%--<script src="Scripts/Validator.js" type="text/javascript"></script>--%>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/investorlogin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            refresh();
            $('form').attr('autocomplete', 'off');
            $('#txtuserID').focus();
            $('#eamil').hide();
            $('#btnSubmit').hide();
            $('#btnBack').hide();
            $('.forgot-heading').hide();
            $('#forgotPwdLink').click(function () {
                $('#pass').hide();
                $('#btnLogin').hide();
                $('#eamil').show();
                $('#btnSubmit').show();
                $('#btnBack').show();
                $('#forgotPwdLink').hide();
                $('.captcha-sec').hide();
                $('#btnLoginId').hide();
                $('.forgot-heading').show(); $('.login-heading').hide();
            });

            $('#btnBack').click(function () {
                $('#pass').show();
                $('#btnLoginId').show();
                $('#eamil').hide();
                $('#btnSubmit').hide();
                $('#btnBack').hide();
                $('#forgotPwdLink').show();
                $('.captcha-sec').show();
                $('.forgot-heading').hide(); $('.login-heading').show();

            });

        });    
    </script>
    <script type="text/javascript" language="javascript">
        function Forgotpwdvalidation() {
            //            if (blankFieldValidation('txtuserID', 'User Name', 'SWP') == false) {
            //                return false;
            //            }
            if (blankFieldValidation('txtEmailID', 'Email Id', 'SWP') == false) {
                return false;
            }
            if (EmailValidation('txtEmailID', 'Email Id', 'SWP') == false) {
                return false;
            }
        }
    </script>
    <style type="text/css">
        .captchagroup .refresh-btn
        {
            border: 1px solid #dadada;
            margin-left: -1px;
            float: right;
            padding: 9px 12px 9px;
            font-size: 22px;
            color: #c50c13;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
      <div class="logheader">
                <a class="" href="Default.aspx">
                    <img src="images/Logo2.png" alt=""><img src="images/Logo.png" alt=""></a> <div class="clearfix">
                </div>
                    <p>Single Window Portal</p>
               
            </div>

    <div class="login-bg">
     
        <div class="logbg-sec">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
          
         
            <div class="login-container">
             <div class="login-header">
                <h4 class="login-heading">
                    Investor Proxy Login</h4>
                
            </div>
                <div class="login-control-sec">
                    <div class="form">
                        <div class="field-wrap" id="uid" style="display:none">
                            <%--       
        <input type="text" id="txtuserID" class="form-control" placeholder="Enter User Id" required autocomplete="off"/>--%>
                            <label class="col-lg-2 col-sm-3">
                            </label>
                            <asp:TextBox ID="txtuserID" class="form-control" runat="server" AutoCompleteType="disabled"
                                onPaste="return false" placeholder="User Name"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtuserID_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                                TargetControlID="txtuserID" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                InvalidChars="&quot;'<>&;">
                                                            </cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="field-wrap" id="eamil">
                            <%--<input type="text" class="form-control" placeholder="Enter Email" id="txtEmailID" name="txtEmailID"/>--%>
                            <asp:TextBox ID="txtEmailID" class="form-control" runat="server" AutoCompleteType="disabled"
                                onPaste="return false" placeholder="Enter Email"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtEmailID_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                                TargetControlID="txtEmailID" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                InvalidChars="&quot;'<>&;">
                                                            </cc1:FilteredTextBoxExtender>
                        </div>                                        
                            <div class="clr">                               
                            </div>
                        </div>                   

                        <asp:Button ID="btnSubmit" runat="server" class="btn btnblock " 
                        Text="Submit" OnClientClick="return Forgotpwdvalidation();" 
                        onclick="btnSubmit_Click" />
                         <div class="login-footer">
                
              <%--  <div class=" text-right">
                 <p class="forgot">
                        <a class="pull-left" href="inestorlogin.aspx"
                            id="A1">Back To Login</a></p>
                    <p>
                        New user <a href="InvestorRegistrationUser.aspx" title="Registration">&nbsp;Register
                            Now</a>
                    </p>
                </div>--%>
                <div class="clearfix">
                </div>
            </div>
                    </div>
                </div>
   <div class="logindatacontainer">
            <h4>Single Sign On</h4>
                    <div class="comment-sec">
                    <ul>
                      <li>Users in APAA/ CIF/ GOiPLUS/ CSR Portal/ eSuvidhaa can login through existing credential.</li>
                    <li>Users can use their registered email id as the user id for the login process.</li>
                    <li>New investors can create login credentials also.</li> 
                     
                    </ul>
                 
                        <asp:CheckBox ID="chkSSO" runat="server" CssClass="checkbox-inline" Text="Login through SSO" Visible="false" /></div>
                                <a href="Default.aspx" title="Go to Home" class="back-tohome"><i class="fa fa-home">
        </i> Back to Home</a>
                     
            
            </div>

                         
                <div class="clearfix">
                </div>
            </div>
            </div>                     
        </div>
    </form>
</body>
