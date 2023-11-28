<%@ Page Language="C#" AutoEventWireup="true" CodeFile="investorlogin_temp_161117.aspx.cs" Inherits="website_inestorlogin" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--<script src="Scripts/Validator.js" type="text/javascript"></script>--%>
    <%--<script src="js/jquery-1.4.1.js" type="text/javascript"></script>--%>
<script src="https://code.jquery.com/jquery-1.4.1.min.js" integrity="sha256-LOx49zn73f7YUs15NNJTDnzEyPFLOGc7A7pfuICtTMc=" crossorigin="anonymous"></script>
<%-- Scripts --%>

    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>

 
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>
   
    <script type="text/javascript">
        $(document).ready(function () {



            $('form').attr('autocomplete', 'off');
            $('#txtuserID').focus();
            $('#eamil').hide();
            $('#btnSubmit').hide();
            $('#btnBack').hide();
            $('.forgot-heading').hide();


           

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
        function Validation() {
            if (blankFieldValidation('txtuserID', 'Email ID', 'SWP') == false) {
                return false;
            }
            if (blankFieldValidation('txtPassword', 'Password', 'SWP') == false) {
                return false;
            }
            if (blankFieldValidation('captchaID', 'Captcha', 'SWP') == false) {
                return false;
            }
         
        }
        
    </script>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnLoginId">
 
        <%--   <p>Single Window Portal</p>--%>
    </div>
    <div class="login-bg">
        <div class="logbg-sec">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
            <div class="login-container">
                <div class="login-header">
                    <h4 class="login-heading">
                        Investor Login</h4>
                  
                </div>
                <div class="login-control-sec">
                    <div class="form">
                        <div class="field-wrap" id="uid">
                            <%--       
        <input type="text" id="txtuserID" class="form-control" placeholder="Enter User Id" required autocomplete="off"/>--%>
                            <asp:TextBox ID="txtuserID" class="form-control" autocomplete="false" runat="server"
                                MaxLength="100" AutoCompleteType="disabled" onPaste="return false"></asp:TextBox>
                                 <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtuserID"
                                                        FilterMode="ValidChars" FilterType="UppercaseLetters,Numbers,Custom,LowercaseLetters"
                                                        ValidChars="$@$!%*#?&._">
                                                    </cc2:FilteredTextBoxExtender>
                        </div>
                        <div class="field-wrap" id="eamil">
                            <%--<input type="text" class="form-control" placeholder="Enter Email" id="txtEmailID" name="txtEmailID"/>--%>
                            <asp:TextBox ID="txtEmailID" class="form-control" runat="server" AutoCompleteType="disabled"
                                MaxLength="100" onPaste="return false" ></asp:TextBox>
                            <cc2:filteredtextboxextender id="tstfilteremail" runat="server" enabled="True"
                                targetcontrolid="txtEmailID" filtermode="ValidChars" filtertype="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                    ValidChars="$@$!%*#?&_">
                            </cc2:filteredtextboxextender>                            
                        </div>
                        <div class="field-wrap" id="pass">
                            <asp:TextBox ID="txtPassword" class="form-control" runat="server" AutoCompleteType="disabled"
                                MaxLength="50" TextMode="Password" onPaste="return false" autocomplete="false"
                              ></asp:TextBox>
                            <%-- <input type="password" id="txtPassword"  class="form-control" placeholder="Password"  required autocomplete="off"/>--%>
                              <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtPassword"
                                                        FilterMode="ValidChars" FilterType="UppercaseLetters,Numbers,Custom,LowercaseLetters"
                                                        ValidChars="#?!@$%^&*-">
                                                    </cc2:FilteredTextBoxExtender>
                        </div>
                        <div>
                            <div class="field-wrap captcha-sec " id="captcha">
                                <asp:TextBox ID="captchaID" class="form-control" Style="text-transform: uppercase;"
                                    autocomplete="false" runat="server" onPaste="return false" ></asp:TextBox>
                                <%--  <input type="text" id="captchaID" class="form-control" placeholder="Captcha" required autocomplete="off"/>--%>
                            </div>
                            <div class="field-wrap captcha-sec" id="captchaimg">
                                <div class="captchagroup">
                                    <%--  <img src="images/captcha.png" />--%>
                                    <%--<a class="refresh-btn"><i class="fa fa-refresh"></i></a>--%>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <cc2:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                CaptchaMinTimeout="5" CaptchaMaxTimeout="240" class="captchalabel" NoiseColor="#B1B1B1" />
                                            <asp:LinkButton ID="ImageButton1" CausesValidation="false" CssClass="refresh-btn"
                                                runat="server">
                                            <span class="fa fa-refresh" aria-hidden="true"></span>
                                            
                                            </asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <%--<asp:HiddenField ID="lblCaptcha" runat="server" />--%>
                                    <%-- <asp:Label ID="lblCaptcha" runat="server" CssClass="captchalabel" Font-Italic="True" ></asp:Label>--%>
                                    <asp:Button ID="Button1" Style="background-color: transparent; border: 0; display: none;
                                        height: 1px; width: 1px;" runat="server" Text="" OnClick="Button1_Click" />
                                </div>
                            </div>
                            <div class="clr">
                            </div>
                        </div>
                        <%--<button class="btn btnblock " id="btnSubmit" name="btnSubmit"  />
      Submit
      </button>--%>
                        <asp:Button ID="btnSubmit" runat="server" class="btn btnblock " Text="Submit" OnClick="btnSubmit_Click" />
                        <%--      <button class="btn btnblock" id="btnLogin" name="btnLogin" onclick="return LoginFuc()"/>
      Login
      </button>--%>
                        <asp:Button ID="btnLoginId" runat="server" class="btn btnblock" Text="Login" OnClientClick="return Validation();"
                            OnClick="btnLogin_click" /><%--OnClientClick="return LoginFuc();"--%>
                    </div>
                  
                </div>
            </div>
        
            <div class="clearfix">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
