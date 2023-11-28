<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvForgotUserId.aspx.cs"
    Inherits="InvForgotUserId" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/investorlogin.css" rel="stylesheet" type="text/css" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('form').attr('autocomplete', 'off');
        });    
    </script>
    <script type="text/javascript" language="javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        /*--------------------------------------------------------------*/

        function Forgotpwdvalidation() {
            if ($('#Txt_Email_Id').val() == "" && $('#Txt_PAN').val() == "") {
                jAlert('<strong>Please enter either PAN or Email id !!</strong>');
                $("#Txt_PAN").focus();
                return false;
            }
            if ($('#Txt_Email_Id').val() != "" && $('#Txt_PAN').val() != "") {
                jAlert('<strong>Please provide either Email id or PAN !!</strong>');
                $("#Txt_PAN").focus();
                return false;
            }
        }

        ////// Alert and Redirect
        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    location.href = 'Login.aspx?';
                    return true;
                }
                else {
                    return false;
                }
            });
        }

        function RadioCheck(rb) {
            var gv = document.getElementById("<%=GrdUserList.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
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
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 900px;
            height: 550px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div class="logheader">
        <a class="" href="Default.aspx">
            <img src="images/Logo2.png" alt="Odisha Govternment"><img src="images/Logo.png" alt="GO Swift"></a>
        <div class="clearfix">
        </div>
        <%--<p>Single Window Portal</p>--%>
    </div>
    <div class="login-bg">
        <div class="logbg-sec">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
            <div class="login-container" id="Div_Input" runat="server">
                <div class="login-header">
                    <h4 class="login-heading">
                        Forgot Investor Login</h4>
                </div>
                <div class="login-control-sec">
                    <div class="form">
                        <div class="field-wrap" id="Div1">
                            <asp:TextBox ID="Txt_PAN" class="form-control" runat="server" AutoCompleteType="disabled"
                                autocomplete="off" onPaste="return true" placeholder="Enter PAN" MaxLength="10"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                TargetControlID="Txt_PAN" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                InvalidChars="&quot;'<>&;">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="field-wrap" id="Div2">
                            <div style="text-align: center; font-weight: 600;">
                                Or</div>
                        </div>
                        <div class="field-wrap" id="eamil">
                            <asp:TextBox ID="Txt_Email_Id" class="form-control" runat="server" AutoCompleteType="disabled"
                                onPaste="return true" placeholder="Enter Email"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtEmailID_FilteredTextBoxExtender" runat="server"
                                Enabled="True" TargetControlID="Txt_Email_Id" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                InvalidChars="&quot;'<>&;">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="clr">
                        </div>
                    </div>
                    <asp:Button ID="Btn_Submit" runat="server" class="btn btnblock" Text="Submit" OnClientClick="return Forgotpwdvalidation();"
                        OnClick="Btn_Submit_Click" />
                    <div class="login-footer">
                        <div class=" text-right">
                            <p class="forgot">
                                <a class="pull-left" href="InvForgotPassword.aspx" id="A1">Forgot Password</a></p>
                            <p>
                                New user <a href="InvestorRegistrationUser.aspx" title="Registration">&nbsp;Register
                                    Now</a>
                            </p>
                            <br />
                            <p class="forgot" style="text-align: center;">
                                <a class="btn btn-info" style="color: #fff; border-radius: 3px;" href="Login.aspx"
                                    id="A3">Back To Login</a></p>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
            </div>
            <div class="login-container" id="Div_OTP" runat="server" style="min-height: 452px;">
                <div class="login-header">
                    <h4 class="login-heading">
                        Validate OTP</h4>
                </div>
                <div class="login-control-sec">
                    <div class="form">
                        <div class="field-wrap">
                            <asp:TextBox ID="Txt_OTP" class="form-control" runat="server" AutoCompleteType="disabled"
                                autocomplete="off" onPaste="return false" placeholder="Enter OTP" MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="clr">
                        </div>
                        <div class="field-wrap">
                            Enter One Time Password (OTP) sent to your registered mobile number and email id.
                        </div>
                        <div class="field-wrap">
                            <span style="font-family: Verdana; font-size: 12px; color: Red;">Please don't press
                                back button or refresh the page.</span>
                        </div>
                    </div>
                    <asp:Button ID="Btn_Validate_OTP" runat="server" class="btn btnblock " Text="Submit"
                        OnClick="Btn_Validate_OTP_Click" />
                    <div class="login-footer">
                        <div class=" text-right">
                            <p class="forgot">
                                <a class="pull-left" href="Login.aspx" id="A2">Back To Login</a></p>
                            <p>
                                New user <a href="InvestorRegistrationUser.aspx" title="Registration">&nbsp;Register
                                    Now</a>
                            </p>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
            </div>
            <div class="logindatacontainer" style="min-height: 452px;">
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
            <asp:HiddenField ID="Hid_Pop" runat="server" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="LnkBtn_Close">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: none;">
                <div id="undertakingipr2015">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header bg-purpul">
                                <div class="form-group">
                                    <div class="col-sm-11">
                                        <h4 class="modal-title">
                                            Alert</h4>
                                    </div>
                                    <div class="col-sm-1" style="text-align: right;">
                                        <asp:LinkButton ID="LnkBtn_Close" runat="server" ForeColor="White" ToolTip="Click here to close the popup">X</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-body">
                                <p>
                                    Following email id(s) and mobile number(s) has been registred under your PAN.
                                    <br />
                                    Please select appropriate record to recovery your user id.
                                </p>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="GrdUserList" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                                DataKeyNames="VCH_OFF_MOBILE_ACTUAL,VCH_EMAIL_ACTUAL,VCH_PAN,VCH_INV_USERID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="RadBtn_User_Select" runat="server" onclick="RadioCheck(this);" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VCH_EMAIL" HeaderText="Email Id" />
                                                    <asp:BoundField DataField="VCH_OFF_MOBILE" HeaderText="Mobile No" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Button ID="Btn_Submit_Popup" runat="server" Text="Submit" OnClick="Btn_Submit_Popup_Click"
                                            class="btn btn-success" ToolTip="Click Here to Proceed" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
            </asp:Panel>
        </div>
    </div>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
