<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvestorChangePasswordToken.aspx.cs"
    Inherits="InvestorChangePasswordToken" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/PlainHeader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/PlainFooter.ascx" TagName="header" TagPrefix="uc3" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<script src="js/WebValidation.js" type="text/javascript"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function ValidatePwdReset() {

            if (blankFieldValidation('txtNewPsw', 'New password', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtRetypPsw', 'Confirm password', projname) == false) {
                return false;
            }

            if (document.getElementById("txtNewPsw").value != document.getElementById("txtRetypPsw").value) {
                jAlert('<strong>New password and Confirm password does not match !</strong>', projname);
                document.getElementById("txtNewPsw").value = "";
                document.getElementById("txtRetypPsw").value = "";
                document.getElementById("txtNewPsw").focus();
                return false;
            }

            if (!checkPassword()) {
                return false;
            }
        }

        //////---------------------------------------------------------------------

        function checkPassword() {
            var Txt_New_Pwd = document.getElementById("txtNewPsw");
            var pwdVal = Txt_New_Pwd.value;
            var illegalChars = /[\W_]g/;
            var re = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,14}$/
            if (!re.test(pwdVal)) {
                jAlert("<strong>Password must contain atleast one uppercase letter,one lowercase letter,one number and one special character and length must be between 8-14 characters !</strong>", projname);
                Txt_New_Pwd.focus();
                return false;
            }
            else {
                return true;
            }
        }

        //////---------------------------------------------------------------------

        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    location.href = 'Login.aspx';
                    return true;
                }
                else {
                    return false;
                }
            });
        }
              
            
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="navigatorheader-div aboutheadernav">
            <div class="col-sm-12">
                <ul class="breadcrumb">
                    <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                    <li>Reset Password</li>
                </ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="content-form-section">
            <div class="col-sm-12">
                <div class="aboutcontent-sec">
                    <div class="panel-body">
                        <div id="divchange" runat="server">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <label>
                                            New Password
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtNewPsw" autocomplete="off" runat="server" TextMode="Password"
                                            onPaste="return false" MaxLength="14" CssClass="form-control"></asp:TextBox>
                                        <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtNewPsw"
                                            FilterMode="ValidChars" FilterType="UppercaseLetters,Numbers,Custom,LowercaseLetters"
                                            ValidChars="@_-">
                                        </cc2:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <label>
                                            Confirm Password
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtRetypPsw" autocomplete="off" runat="server" onPaste="return false"
                                            TextMode="Password" MaxLength="14" CssClass="form-control"></asp:TextBox>
                                        <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRetypPsw"
                                            FilterMode="ValidChars" FilterType="UppercaseLetters,Numbers,Custom,LowercaseLetters"
                                            ValidChars="@_-">
                                        </cc2:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <label>
                                            Enter Captcha
                                        </label>
                                    </div>
                                    <div class="col-sm-2">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtCaptcha" runat="server" placeholder="Enter Captcha" CssClass="form-control"
                                            MaxLength="6" autocomplete="off"></asp:TextBox>
                                        <a data-toggle="tooltip" class="fieldinfo" title="Enter the characters shown in image !">
                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        <cc2:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                            TargetControlID="txtCaptcha" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                            InvalidChars="&quot;'<>&;">
                                        </cc2:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <cc4:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                        CaptchaMinTimeout="5" CaptchaMaxTimeout="240" CssClass="homecaptchaimg" NoiseColor="#B1B1B1" />
                                                    <div class="refresh">
                                                        <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false"><span class="fa fa-refresh homerefreshbtn input-group-addon" style="cursor: pointer;" onclick="return RefreshCaptcha();"
                                                                                aria-hidden="true"></span></asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <label>
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                            CssClass="btn btn-success" OnClientClick="return ValidatePwdReset();" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-4">
                                        N.B.:-
                                    </label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <ol>
                                            <li>The new password must contain atleast one uppercase letter, one lowercase letter,
                                                one number and one special character and length must be between 8-14 characters.</li>
                                            <li>The passwords can be used instantly after resetting.</li>
                                            <li>In case you are not able to reset the password online, Please contact to our support
                                                team.</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divexpire" runat="server" align="center" visible="false">
                            <h3>
                                The password reset link has expired.</h3>
                        </div>
                        <div id="divSuc" runat="server" align="center" visible="false">
                            <h3>
                                Your password has been reset successfully.
                            </h3>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:header ID="footer" runat="server" />
    </form>
</body>
</html>
