<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginthroughSSO.aspx.cs" Inherits="LoginthroughSSO" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--<script src="Scripts/Validator.js" type="text/javascript"></script>--%>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/ssologin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

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
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnLoginId">
    
    <div id="loginHeader">
        <div class="login-heading">
            <div id="logo">
              <div class="logheader">
                <a class="" href="Default.aspx">
                    <img src="images/Logo2.png" alt="Industries Inspection Monitoring System, Invest Odisha, Come. invest. Grow"><img src="images/Logo.png" alt="Industries Inspection Monitoring System, Invest Odisha, Come. invest. Grow"></a>
                <div class="clearfix">
                </div>
            </div>
            <%--  <div class="header-text" >
                    <h1>
                        Single Sign On Framework</h1>
                    
                </div>--%>
                
                <div class="clearfix">
                </div>
            </div>
        </div>
    </div>

      <div id="Area">
        <div class="adminbox">
          
            <h4>
                Single Sign On</h4>
                <div class="content-sec">
            <p>
                Single sign-on (SSO)is a user authentication process that permits a user to enter
                one user id and password in order to access multiple applications. The process authenticates
                the user for all the applications they have been given rights to and eliminates
                further prompts when they switch applications during a particular session.</p>
            <p>
                Single sign on gives ability to enforce uniform enterprise authentication and/or
                authorization policies across the enterprise with end to end user audit sessions
                to improve security reporting and auditing.It removes application developers from
                having to understand and implement identity security in their applications.</p>
            <p>
                Single sign on can also take place between enterprises using federate authentication.</p>
            <p>
                Single sign on systems in medium to large enterprises can become a single point
                of enterprise.If the single sign on system goes down but the applications remain
                up, no user can access any resource or application protected by the SSO system.Therefore,
                it is essential that our enterprise single sign on system have a good and well tested
                failover and disaster recovery design.</p>
                </div>
        </div>
        <div class="loginBox boxnew">
            <h3>
               Single Sign On Login</h3>
            <div class="strip1">
            </div>
            <div id="loginCont" class="inputsection">
                <div class="login-container">
                 <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
                <div class="login-control-sec">
                    <div class="form">  
                        <div class="field-wrap" id="uid">
                         
                            <%--       
        <input type="text" id="txtuserID" class="form-control" placeholder="Enter User Id" required autocomplete="off"/>--%>
                            <label class="col-lg-2 col-sm-3">
                            </label>
                         
                            <asp:TextBox ID="txtuserID" class="form-control" autocomplete="false" runat="server"
                                AutoCompleteType="disabled" onPaste="return false" placeholder="Email ID"></asp:TextBox>
                        </div>
                        <div class="field-wrap" id="eamil">
                            <%--<input type="text" class="form-control" placeholder="Enter Email" id="txtEmailID" name="txtEmailID"/>--%>
                            <asp:TextBox ID="txtEmailID" class="form-control" runat="server" AutoCompleteType="disabled"
                                onPaste="return false" placeholder="Enter Email" Visible="false"></asp:TextBox>
                        </div>
                        <div class="field-wrap" id="pass">
                            <asp:TextBox ID="txtPassword" class="form-control" runat="server" AutoCompleteType="disabled"
                                TextMode="Password" onPaste="return false" autocomplete="false" placeholder="Password"></asp:TextBox>
                            <%-- <input type="password" id="txtPassword"  class="form-control" placeholder="Password"  required autocomplete="off"/>--%>
                        </div>
                        <div>
                            <div class="field-wrap captcha-sec " id="captcha">
                                <asp:TextBox ID="captchaID" class="form-control"  style="text-transform:uppercase;" autocomplete="false" runat="server"
                                    onPaste="return false" placeholder="Captcha"></asp:TextBox>
                                <%--  <input type="text" id="captchaID" class="form-control" placeholder="Captcha" required autocomplete="off"/>--%>
                            </div>
                            <div class="field-wrap captcha-sec" id="captchaimg">
                                <div class="captchagroup">
                                    <%--  <img src="images/captcha.png" />--%>
                                    <%--<a class="refresh-btn"><i class="fa fa-refresh"></i></a>--%>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <cc2:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"  
                                               CaptchaMinTimeout="5" CaptchaMaxTimeout="240" 
                                                class="captchalabel"  NoiseColor="#B1B1B1" />
                                            

 <asp:LinkButton ID="ImageButton1"  CausesValidation="false"  CssClass="refresh-btn"  runat="server">
                                            <span class="fa fa-refresh" aria-hidden="true"></span>
                                            
                                            </asp:LinkButton>
                                        </ContentTemplate>
                                       
                                    </asp:UpdatePanel>
                                    <%--<asp:HiddenField ID="lblCaptcha" runat="server" />--%>
                                    <%-- <asp:Label ID="lblCaptcha" runat="server" CssClass="captchalabel" Font-Italic="True" ></asp:Label>--%>
                                                                 
                                </div>
                            </div>
                            <div class="clr">
                            </div>
                        </div>                   
 
                        <asp:Button ID="btnLoginId" runat="server" class="btn btnblock" Text="Login" OnClientClick="return Validation();"
                            OnClick="btnLogin_click" /><%--OnClientClick="return LoginFuc();"--%>
                    </div>
                </div>
            </div>  
            </div>
        </div>
        <div class="clearfix">
        </div>
    </div>





   
    </form>
</body>
</html>
