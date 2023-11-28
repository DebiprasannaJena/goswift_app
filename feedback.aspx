<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="aboutus" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc4" %>
<html>
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title>SWP(Single Window Portal)</title>
    <style type="text/css">
        .upper-case
        {
            text-transform: uppercase;
        }
    </style>
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
                    <li>Feedback</li></ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="content-form-section">
            <div class="col-sm-12">
                <div class="aboutcontent-sec">
                    <h3>
                        Feedback</h3>
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="feedback-homesec">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-6 ">
                                            <label>
                                                First Name
                                            </label>
                                            <asp:TextBox CssClass="form-control" ID="txtFirstName" runat="server" MaxLength="30"
                                                AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtFirstName_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="txtFirstName" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                ValidChars="a-zA-Z ">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="col-sm-6 ">
                                            <label>
                                                Last Name</label>
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" MaxLength="30"
                                                AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtLastName_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="txtLastName" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                ValidChars="a-zA-Z ">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label>
                                                Email</label>
                                            <asp:TextBox CssClass="form-control" ID="txtEmail" runat="server" MaxLength="50"
                                                AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtEmail_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="txtEmail" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                ValidChars="_.@">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>
                                                Mobile Number</label>
                                            <asp:TextBox CssClass="form-control" ID="txtMobileNumber" runat="server" MaxLength="10"
                                                AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtMobileNumber_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="txtMobileNumber" FilterMode="ValidChars" FilterType="Numbers"
                                                ValidChars="0-9">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <label>
                                                Subject</label>
                                            <asp:TextBox CssClass="form-control" ID="txtSubject" runat="server" MaxLength="100"
                                                AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtSubject_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="txtSubject" FilterMode="ValidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                ValidChars="a-zA-Z0-9-. ">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <label>
                                                Feedback</label>
                                            <asp:TextBox CssClass="form-control" TextMode="MultiLine" ID="txtFeedback" runat="server"
                                                MaxLength="250" AutoCompleteType="None" autocomplete="off" onKeyUp="limitText(this,this.form.count,250);"
                                                onKeyDown="limitText(this,this.form.count,250);"></asp:TextBox>
                                            <span class="mandetory">*</span> <span class="mandatory" style="font-size: 14px;
                                                color: red"><small>Maximum
                                                    <input name="count" class="inputCss" readonly="readonly" style="font-weight: bold;
                                                        color: red; width: 26px;" type="text" value="250" tabindex="-1" />
                                                    Characters Left</small></span>
                                            <cc1:FilteredTextBoxExtender ID="txtFeedback_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="txtFeedback" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                ValidChars="a-zA-Z0-9-. ">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-6">
                                            <asp:TextBox ID="txtCaptcha" runat="server" placeholder="Enter Captcha" CssClass="form-control"
                                                MaxLength="6" autocomplete="off"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Enter the characters shown in image !">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                TargetControlID="txtCaptcha" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="col-sm-12 col-md-6">
                                            <div class="input-group">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <cc4:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                            CaptchaMinTimeout="5" CaptchaMaxTimeout="240" CssClass="homecaptchaimg" NoiseColor="#B1B1B1" />
                                                        <div class="refresh">
                                                            <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false"> <span class="fa fa-refresh homerefreshbtn input-group-addon" style="cursor: pointer;" onclick="return RefreshCaptcha();"
                                                                                aria-hidden="true"></span></asp:LinkButton>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </div>
                                        <div class="clear20">
                                        </div>
                                    </div>
                                </div>
                                <asp:Button CssClass="btn btn-success" ID="btnSubmit" runat="server" Text="Submit"
                                    OnClick="btnSubmit_Click" OnClientClick="return Validate();" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix">
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function Validate() {

            if (blankFieldValidation('txtFirstName', 'First name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtLastName', 'Last name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtEmail', 'Email address', projname) == false) {
                return false;
            }
            if (EmailValidation('txtEmail', 'email address', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtMobileNumber', 'Mobile number', projname) == false) {
                return false;
            }

            var val = ($("#txtMobileNumber").val());
            if (($("#txtMobileNumber").val()).substring(0, 1) == '0') {
                jAlert('<strong>Mobuile number should not be start with zero !</strong>');
                $("#txtMobileNumber").val('');
                $("#txtMobileNumber").focus();
                return false;
            }

            if ($("#txtMobileNumber").val().length != 10) {
                jAlert('<strong>Mobile number should be 10 digits !</strong>');
                $("#txtMobileNumber").focus();
                return false;
            }
            if (blankFieldValidation('txtSubject', 'Subject', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtFeedback', 'Feedback', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtCaptcha', 'Captcha', projname) == false) {
                return false;
            }
        }
        
        ///-------------------------------------------------------------------------

        function numeralsOnly(evt, tt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                //alert("Enter Numerals only in this Field!");
                //tt.value="";              
                return false;
            }
            return true;
        }

        ///-------------------------------------------------------------------------

        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            } else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }    

    </script>
</body>
</html>
