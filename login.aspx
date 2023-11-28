<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="Portal/Console/scripts/md5.js"></script>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/investorlogin.css" rel="stylesheet" type="text/css" />
    <title>GO-SWIFT | Single Window Portal | Department Of Industries, Govt. of Odisha
    </title>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menuservices').addClass('active');
        });
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
        function RemoveTarget() {
            document.forms[0].target = "";
        }
    </script>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function onload() {
            var login = document.getElementById("hidmsg").value;
            if (login != "") {
            }
        }

        $(document).ready(function () {
            $('form').attr('autocomplete', 'off');
            $('#Txt_User_Id').focus();

            ////Disable cut copy paste
            //$('body').bind('cut copy paste', function (e) {
            //    e.preventDefault();
            //});

            ////Disable mouse right click
            //$("body").on("contextmenu", function (e) {
            //    return false;
            //});

        });
    </script>
    <script type="text/javascript" language="javascript">
        function Validation() {

            if (blankFieldValidation('Txt_User_Id', 'User Name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('Txt_Password', 'Password', projname) == false) {
                return false;
            }
            if (blankFieldValidation('Txt_Captcha', 'Captcha', projname) == false) {
                return false;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function alertredirect(msg, userid) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    location.href = 'Update_PAN.aspx?userId=' + userid;
                    return true;
                }
                else {
                    return false;
                }
            });
        }

        function alertredirect2(msg, redirecturl) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    location.href = redirecturl;
                    return true;
                }
                else {
                    return false;
                }
            });
        }

    </script>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            opacity: 0.7;
        }

        /*-------------------------------------------------------------*/

        .modalPopup {
            background-color: #fbfbfb;
            border: 3px solid #ed3338;
            margin: 0px;
        }

            .modalPopup .mhead {
                padding: 5px 5px;
                border-bottom: 1px solid #ccc;
                background: #ed3338;
                color: #fff;
            }

                .modalPopup .mhead h4 {
                    display: inline-block;
                }

                .modalPopup .mhead a {
                    float: right;
                    color: #ac183e;
                    text-decoration: none;
                    background: #fff;
                    width: 20px;
                    height: 20px;
                    border-radius: 50%;
                    text-align: center;
                    margin-top: -15px;
                    margin-right: -15px;
                }

            .modalPopup .mbody {
                padding: 30px 15px;
            }

            .modalPopup .mFooter {
                padding: 27px 15px;
                text-align: right;
                border-top: 1px solid #e5e5e5;
            }

        /*-------------------------------------------------------------*/

        .modalPopup2 {
            background-color: #fbfbfb;
            border: 3px solid #006694;
            margin: 0px;
        }

            .modalPopup2 .mhead {
                padding: 5px 5px;
                border-bottom: 1px solid #ccc;
                background: #006694;
                color: #fff;
            }

                .modalPopup2 .mhead h4 {
                    display: inline-block;
                }

                .modalPopup2 .mhead a {
                    float: right;
                    color: #ac183e;
                    text-decoration: none;
                    background: #fff;
                    width: 20px;
                    height: 20px;
                    border-radius: 50%;
                    text-align: center;
                    margin-top: -15px;
                    margin-right: -15px;
                }

            .modalPopup2 .mbody {
                padding: 30px 15px;
            }

            .modalPopup2 .mFooter {
                padding: 27px 15px;
                text-align: right;
                border-top: 1px solid #e5e5e5;
            }


        /*-------------------------------------------------------------*/

        .radio-box input[type=checkbox], input[type=radio] {
            margin: 2px 0 0;
            width: 16px;
            height: 16px;
            float: left;
            margin-right: 5px;
        }

        label {
            margin-right: 12px;
        }

        .radiodiv {
            padding: 10px 0px 20px;
        }

        .Confdiv {
            padding: 25px 120px 20px;
        }

        #PanelIdco h4 {
            font-size: 17px;
            font-weight: bold;
            padding-bottom: 12px;
        }

        .radio-inline label {
            display: inline-block;
            padding-right: 20px;
            padding-left: 12px;
        }

        .reglogin {
            padding: 25px;
        }

            .reglogin p {
                text-align: justify;
            }

            .reglogin a {
                color: #0088cc;
                text-decoration: none;
            }

                .reglogin a:hover {
                    color: #159f45;
                }

        .popBox {
            position: absolute;
            -webkit-box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            -moz-box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            background: #fffdef;
            padding: 8px;
            border: 1px solid #ddd;
            width: 93%;
            left: 15px;
            font-size: 14px !important;
        }

        #pop-up {
            top: 120px;
        }

        #pop-up1 {
            top: 120px;
        }

        #pop-up2 {
            top: 100px;
        }

        .row {
            margin-left: -15px;
            margin-right: 0;
        }

        .navbar-inverse {
            background-color: none !important;
            border-color: none !important;
        }

        .portlet-sec {
            margin: 16px 15px 8px;
            padding: 5px;
            border-radius: 2px;
        }

            .portlet-sec h3 {
                text-transform: uppercase;
                font-size: 20px;
            }

                .portlet-sec h3 span {
                    font-weight: 600;
                    color: #ac2d00;
                    padding: 0px 4px;
                }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="Btn_Login">
        <div class="logheader">
            <a class="" href="Default.aspx">
                <img src="images/Logo2.png" alt="Odiash logo" /><img src="images/Logo.png" alt="Go Swift logo" /></a>
            <div class="clearfix">
            </div>
        </div>
        <div runat="server" visible="true" id="divScrollingText" style="color: #0d3fd1; font-size: 14px; font-family: Verdana; font-style:italic; font-weight: 600; padding-top: 5px;">
            </div>
        <%--<marquee style=" overflow: hidden; position: relative; background: #fefefe;
                    color: Red; font-style:italic; ">Due to maintenance activity by IT secretariat team,  GO-SWIFT portal will not be available to users till 11:59 PM on January 10, 2023.Inconvenience caused is regretted.</marquee>--%>
        <div class="login-bg">
            <div class="logbg-sec">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                </asp:ScriptManager>
                <div class="login-container">
                    <div class="login-header">
                        <h4 class="login-heading">Investor Login</h4>
                    </div>
                    <div class="login-control-sec">
                        <asp:RadioButtonList ID="Rbl_Industry_type" runat="server" CssClass="radio-box" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Industry" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Non Industry" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>           
                    <div class="login-control-sec">
                        <div class="form">
                            <div class="field-wrap" id="uid">
                                <asp:TextBox ID="Txt_User_Id" class="form-control" autocomplete="false" runat="server"
                                    MaxLength="100" AutoCompleteType="disabled" onPaste="return true" placeholder="User ID"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_User_Id"
                                    FilterMode="ValidChars" FilterType="UppercaseLetters,Numbers,Custom,LowercaseLetters"
                                    ValidChars="$@$!%*#?&._">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="field-wrap" id="pass">
                                <asp:TextBox ID="Txt_Password" class="form-control" runat="server" AutoCompleteType="disabled"
                                    MaxLength="50" TextMode="Password" onPaste="return false" autocomplete="false"
                                    placeholder="Password"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="Txt_Password"
                                    FilterMode="ValidChars" FilterType="UppercaseLetters,Numbers,Custom,LowercaseLetters"
                                    ValidChars="#?!@$%^&*-">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div style="display: block;">
                                <div class="field-wrap captcha-sec " id="captcha">
                                    <asp:TextBox ID="Txt_Captcha" class="form-control" autocomplete="false" runat="server"
                                        onPaste="return false" placeholder="Captcha"></asp:TextBox>
                                </div>
                                <div class="field-wrap captcha-sec" id="captchaimg">
                                    <div class="captchagroup">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <cc2:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                    CaptchaMinTimeout="5" CaptchaMaxTimeout="240" class="captchalabel" NoiseColor="#B1B1B1" />
                                                <asp:LinkButton ID="ImgBtn_Refresh_Captcha" CausesValidation="false" CssClass="refresh-btn"
                                                    runat="server">
                                            <span class="fa fa-refresh" aria-hidden="true"></span>                                            
                                                </asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <input name="hidden" type="hidden" id="hidmsg" runat="server" />
                                        <asp:HiddenField ID="hidSlt" runat="server" />
                                    </div>
                                </div>
                                <div class="clr">
                                </div>
                            </div>
                            <asp:Button ID="Btn_Login" runat="server" class="btn btnblock" Text="Login" OnClientClick="return Validation();"
                                OnClick="Btn_Login_Click" />
                        </div>
                        <div class="login-footer">
                            <div class=" text-right">
                                <p class="forgot">
                                    <a href="InvForgotPassword.aspx" id="forgotPwdLink" class="pull-left">Forgot Password
                                    ? </a>
                                </p>
                                <%-- change by anil satart--%>
                                <%--<p>
                                New user <a href="InvestorRegistrationUser.aspx" title="Registration">&nbsp;Register
                                    Now</a>
                            </p>--%>
                                <p>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <p>
                                                New User
                                                <asp:LinkButton ID="LinkBtnRegistrationUser" runat="server" OnClick="LinkBtnRegistrationUser_Click">&nbsp;Register Now</asp:LinkButton>
                                            </p>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </p>

                                <%-- change by anil end--%>
                            </div>
                            <div class="clearfix">
                            </div>
                        </div>

                        <%--<h4>Open Modal Popup start by anil</h4>--%>
                        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                        <cc1:ModalPopupExtender ID="ServiceModalPopup" BehaviorID="mpe" runat="server" PopupControlID="pnlPopup"
                            TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="Linkclose">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none; width: 600px; height: 330px;"
                            ToolTip="Important Notes">
                            <div class="mhead">
                                <asp:LinkButton ID="Linkclose" runat="server" OnClientClick="RemoveTarget();"><i class="fa fa-close"></i></asp:LinkButton>
                                <%--<span class="glyphicon glyphicon-alert"></span>--%>
                                <h4 class="modal-title">Important Notes</h4>
                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <ol style="padding-left: 20px;">
                                                    <li style="padding-bottom: 7px;">If you are an <span style="font-weight: bolder; color: red;">Industrial</span> unit (As defined by Govt of India from time to time either Large or MSME industrial unit) then click on the <span style="color: #5cb85c; font-weight: bolder;">Industrial User Registration </span>button.</li>
                                                    <li style="padding-bottom: 7px;">If you are a <span style="font-weight: bolder; color: red;">Non-Industrial</span> unit then click on <span style="color: #337ab7; font-weight: bolder;">Non-Industrial User Registration</span> button.</li>
                                                    <%-- <li style="padding-bottom: 7px;">If you are an <span style="font-weight: bolder;">industrial </span>unit then click on <span style="color: #337ab7; font-weight: bolder;">Industrial User Registration</span> button.</li>--%>
                                                    <li style="padding-bottom: 7px;">Industrial User Registration can avail all the services relating to establishment of industry including grievance module.</li>
                                                    <li>Non industrial registration can only raise grievance through grievance module and rest of the modules relevant to industry are not available in the portal.</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="mFooter">
                                <div class="row">
                                    <div class="col-sm-6 text-left">
                                        <asp:Button ID="BtnIndustryReg" runat="server" Text="Industrial User Registration" CssClass="btn btn-success" OnClick="BtnIndustryReg_Click" ToolTip="If you are an industrial user, then click here for registration." />
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Button ID="BtnNonIndustryReg" runat="server" Text="Non-Industrial User Registration" CssClass="btn btn-primary" OnClick="BtnNonIndustryReg_Click"
                                            ToolTip="If you are a non industrial user, then click here for registration." />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <%--<h4>Open Modal Popup end by anil</h4>--%>

                        <%--Modal2--%>
                        <asp:LinkButton ID="LnkBtnTarget2" runat="server"></asp:LinkButton>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe1" runat="server" PopupControlID="PanelPopup2"
                            TargetControlID="LnkBtnTarget2" BackgroundCssClass="modalBackground" CancelControlID="LnkBtnClose2">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="PanelPopup2" runat="server" CssClass="modalPopup2" Style="width: 650px; height: 330px;"
                            ToolTip="Alert">
                            <div class="mhead">
                                <asp:LinkButton ID="LnkBtnClose2" runat="server" OnClientClick="RemoveTarget();"><i class="fa fa-close"></i></asp:LinkButton>
                                <span class="glyphicon glyphicon-alert"></span>
                                <h4 class="modal-title">&nbsp;Alert</h4>
                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <span style="color: red; font-style: italic;">Before registering for Non-Industry, please read the following instructions carefully.</span>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <ol style="padding-left: 20px;">
                                                    <li style="padding-bottom: 7px;">Non industrial registration can only raise grievance through grievance module and rest of the modules relevant to industry are not available in the portal.</li>
                                                    <li style="padding-bottom: 7px;">Once the user is registered in the non industry category only Grievance Module is visible rest other modules relating to industrial set up are not available. </li>
                                                    <li>In the industry category user can apply all the services including Grievance Module relating to industry.</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <div class="row">
                                    <div class="col-sm-7 text-left">
                                        Are you sure want to register under Non-Industry ?
                                    </div>
                                    <div class="col-sm-5">   
                                        <a href="InvestorRegistrationNonIndustry.aspx" class="btn btn-danger" title="If you are a non industrial user, then click here for registration.">Yes</a>
                                        <a href="Login.aspx" class="btn btn-success" title="Click here to Cancel.">No</a>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                       

                    </div>
                </div>
                <div class="logindatacontainer">
                    <h4>Single Sign On</h4>
                    <div class="comment-sec">
                        <ul>
                            <li>All existing technological applications of the Industries Department , Govt. of
                            Odisha including <a href="https://invest.odisha.gov.in/swp/" data-toggle="tooltip"
                                target="_blank" title="GO-SWIFT">GO-SWIFT</a>,<br />
                                <a href="http://idco.in/2017/" data-toggle="tooltip" target="_blank" title="Automated Post Allotment Application">GO iPAS</a> / <a href="http://cicg.investodisha.org/iimsweb/Default.aspx" data-toggle="tooltip"
                                    target="_blank" title="GO SMILE">GO SMILE </a>/ <a href="http://gis.investodisha.org/"
                                        data-toggle="tooltip" target="_blank" title="Government of Odisha's industrial Portal for Land Use and Services">GO PLUS</a> / <a href="http://csr.odisha.gov.in/" target="_blank" data-toggle="tooltip"
                                            title="GO CARE">GO CARE</a> / <a href="https://esuvidha.gov.in/odisha/index.php"
                                                target="_blank" title="State Project Monitoring Group Portal/eSuvidha" data-toggle="tooltip">SPMG Portal </a>have been integrated via the Single Sign-on
                            Framework. Access to all these applications is available through the Single Sign-On
                            user credential. </li>
                            <li><span data-toggle="tooltip" title="Existing Users who have logged in at least once in GO-SWIFT/APAA / GO SMILE  / GO PLUS / GO CARE / SPMG Portal post implementation of Single-Sign On Framework are automatically part of the SSO.  ">Existing users</span> of these applications can login through their registered email
                            id as the user id for the login process. New Users can create login credential,
                            by clicking on <a title="Register Now" href="InvestorRegistrationUser.aspx">Register
                                Now</a>. </li>
                        </ul>
                        <asp:CheckBox ID="chkSSO" runat="server" CssClass="checkbox-inline" Text="Login through SSO"
                            Visible="false" />
                    </div>
                    <a href="Default.aspx" title="Go to Home" class="back-tohome"><i class="fa fa-home"></i>&nbsp;Back to Home</a>
                </div>

                 <%--POPPUP FOR UPDATE INFO--%>
                         <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe2" runat="server"
                            PopupControlID="pnlProfile" TargetControlID="LinkButton1" BackgroundCssClass="modalBackground" CancelControlID="LinkclosePopup">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlProfile" runat="server" CssClass="modalPopup" Style="display: none; width: 800px; height: 385px">
                            <div class="mhead">
                                <asp:LinkButton ID="LinkclosePopup" runat="server" OnClick="LinkclosePopup_Click"><i class="fa fa-close" onclick='return check()'></i></asp:LinkButton>
                                <h4>Update Information</h4>
                            </div>
                            <div class="mbody">
                                <div class="form-section">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <label>
                                                CIN number
                                            </label>
                                            <span style="color:red">*</span>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtCIN" runat="server" CssClass="form-control" MaxLength="21"></asp:TextBox>
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter CIN Number"
                                                ControlToValidate="txtCIN" ValidationGroup="a" ForeColor="Red" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                            
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <label>
                                                Constitution of Company/Entity Type
                                            </label>
                                            <span style="color:red">*</span>
                                        </div>
                                        <div class="col-sm-4">
                                             <asp:DropDownList ID="DrpDwn_Entity_Type" runat="server" CssClass="form-control"
                                                                 ToolTip="Select Constitution of Company/Entity Type name here.">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                           
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Constitution of Company/Entity Type"
                                                ControlToValidate="DrpDwn_Entity_Type" ValidationGroup="a" ForeColor="Red" SetFocusOnError="true" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            
                                        </div>
                                    </div>
                                    <br />
                                    
                                    
                                    <div style="text-align: center; margin-right: 183px;">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success"
                                            ValidationGroup="a" OnClick="btnUpdate_Click" />
                                        <asp:Button ID="btnHide" runat="server" Text="Skip" CssClass="btn btn-danger" OnClick="btnHide_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                <div class="clearfix">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
