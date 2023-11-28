<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvForgotPassword.aspx.cs"
    Inherits="InvForgotPassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title></title>
    <%--<script src="Scripts/Validator.js" type="text/javascript"></script>--%>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/investorlogin.css" rel="stylesheet" type="text/css" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            refresh();
            $('form').attr('autocomplete', 'off');
        });    
    </script>
    <script type="text/javascript" language="javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        /*--------------------------------------------------------------*/

        function Forgotpwdvalidation() {
            if (blankFieldValidation('Txt_User_Id', 'User Id', projname) == false) {
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
    <form id="form1" runat="server" defaultbutton="Btn_Submit">
    <div class="logheader">
        <a class="" href="Default.aspx">
            <img src="images/Logo2.png" alt="Odisha Govternment"><img src="images/Logo.png" alt="GO Swift"></a>
        <div class="clearfix">
        </div>
    </div>
    <div class="login-bg">
        <div class="logbg-sec">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
            <div class="login-container" style="min-height: 422px;">
                <div class="login-header">
                    <h4 class="login-heading">
                        Forgot Password</h4>
                </div>
                <div class="login-control-sec">
                    <div class="form">
                        <div class="field-wrap" id="eamil">
                            <asp:TextBox ID="Txt_User_Id" class="form-control" runat="server" AutoCompleteType="disabled"
                                onPaste="return false" placeholder="Enter User Id"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtEmailID_FilteredTextBoxExtender" runat="server"
                                Enabled="True" TargetControlID="Txt_User_Id" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                InvalidChars="&quot;'<>&;">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="clr">
                        </div>
                    </div>
                    <asp:Button ID="Btn_Submit" runat="server" class="btn btnblock " Text="Submit" OnClientClick="return Forgotpwdvalidation();"
                        OnClick="Btn_Submit_Click" />
                    <div class="login-footer">
                        <div class=" text-right">
                            <p class="forgot">
                                <a class="pull-left" href="InvForgotUserId.aspx" id="A1">Forgot Investor Login</a></p>
                            <p>
                                New user <a href="InvestorRegistrationUser.aspx" title="Registration">&nbsp;Register
                                    Now</a>
                            </p>
                            <br />
                            <br />
                            <p class="forgot" style="text-align: center;">
                                <a class="btn btn-info" style="color: #fff" href="Login.aspx" id="A2">Back To Login</a></p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="logindatacontainer">
                <h4>
                    Single Sign On</h4>
                <div class="comment-sec">
                    <ul>
                        <li>All existing technological applications of the Industries Department , Govt. of
                            Odisha including <a href="https://invest.odisha.gov.in/swp/" data-toggle="tooltip"
                                target="_blank" title="GO-SWIFT">GO-SWIFT</a>, <a href="http://idco.in/2017/" data-toggle="tooltip"
                                    target="_blank" title="Automated Post Allotment Application">APAA</a> /
                            <a href="http://cicg.investodisha.org/iimsweb/Default.aspx" data-toggle="tooltip"
                                target="_blank" title="GO SMILE">GO SMILE </a>/ <a href="http://gis.investodisha.org/"
                                    data-toggle="tooltip" target="_blank" title="Government of Odisha's industrial Portal for Land Use and Services">
                                    GO PLUS</a> / <a href="http://csr.odisha.gov.in/" target="_blank" data-toggle="tooltip"
                                        title="GO CARE">GO CARE</a> / <a href="https://esuvidha.gov.in/odisha/index.php"
                                            target="_blank" title="State Project Monitoring Group Portal/eSuvidha" data-toggle="tooltip">
                                            SPMG Portal </a>have been integrated via the Single Sign-on Framework.
                            Access to all these applications is available through the Single Sign-On user credential.
                        </li>
                        <li><span data-toggle="tooltip" title="Existing Users who have logged in at least once in GO-SWIFT/APAA / GO SMILE  / GO PLUS / GO CARE / SPMG Portal post implementation of Single-Sign On Framework are automatically part of the SSO.  ">
                            Existing users</span> of these applications can login through their registered email
                            id as the user id for the login process. New Users can create login credential,
                            by clicking on <a title="Register Now" href="InvestorRegistrationUser.aspx">Register
                                Now</a>. </li>
                    </ul>
                    <asp:CheckBox ID="chkSSO" runat="server" CssClass="checkbox-inline" Text="Login through SSO"
                        Visible="false" /></div>
                <a href="Default.aspx" title="Go to Home" class="back-tohome"><i class="fa fa-home">
                </i>Back to Home</a>
            </div>
            <div class="clearfix">
            </div>
        </div>
    </div>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    </form>
</body>
</html>
