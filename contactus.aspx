<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contactus.aspx.cs" Inherits="contactus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.contacuslink').addClass('active');
        });
    </script>
    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function ValidateContact() {

            if (blankFieldValidation('txtName', 'Your name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtEmail', 'Email address', projname) == false) {
                return false;
            }
            if (EmailValidation('txtEmail', 'email address', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtMobileNumber', 'Phone number', projname) == false) {
                return false;
            }

            var val = ($("#txtMobileNumber").val());
            if (($("#txtMobileNumber").val()).substring(0, 1) == '0') {
                jAlert('<strong>Phone number should not be start with zero !</strong>');
                $("#txtMobileNumber").val('');
                $("#txtMobileNumber").focus();
                return false;
            }

            if (($("#txtMobileNumber").val().length < 10) && ($("#txtMobileNumber").val().length > 0)) {
                jAlert('<strong>Phone number can not be less then 10 characters !</strong>');
                $("#txtMobileNumber").focus();
                return false;
            }

            if (blankFieldValidation('txtCompany', 'Company name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtMsg', 'Message', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtCaptcha', 'Captcha', projname) == false) {
                return false;
            }
        }

        /////-------------------------------------------------------------------------

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

        /////-------------------------------------------------------------------------

        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            } else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }    
        
            
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <div class="navigatorheader-div aboutheadernav">
                <div class="col-sm-12">
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                        <li>Contact Us</li>
                    </ul>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="content-form-section">
                <div class="">
                    <div class="col-sm-12">
                        <div class="col-md-8 col-sm-12 contactform">
                            <div class="contactform-ctlist">
                                <h3>
                                    Send us a message <i class="fa fa"></i>
                                </h3>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-6 ">
                                            <label>
                                                Your Name</label>
                                            <asp:TextBox CssClass="form-control" ID="txtName" runat="server" MaxLength="30" AutoCompleteType="None"
                                                autocomplete="off"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required"
                                                ValidationGroup="a" ForeColor="Red" ControlToValidate="txtName" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REValphaOnly" runat="server" ErrorMessage="Please enter only alphabets."
                                                ControlToValidate="txtName" ValidationExpression="^[a-zA-Z ]+$" SetFocusOnError="true"
                                                ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                            <cc1:FilteredTextBoxExtender ID="txtFirstName_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="txtName" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                ValidChars="a-zA-Z ">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>
                                                Email Address</label>
                                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" MaxLength="30"
                                                AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required"
                                                ValidationGroup="a" ForeColor="Red" ControlToValidate="txtEmail" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                ErrorMessage="Invalid email address" SetFocusOnError="true" ValidationGroup="a" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                TargetControlID="txtEmail" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                ValidChars="_.@">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label>
                                                Phone Number</label>
                                            <asp:TextBox CssClass="form-control" ID="txtMobileNumber" runat="server" MaxLength="10"
                                                onkeypress="return numeralsOnly(event,this);" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required"
                                                ValidationGroup="a" ForeColor="Red" ControlToValidate="txtMobileNumber" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revMobNo" runat="server" ErrorMessage="Invalid Mobile Number."
                                                ValidationExpression="^([0-9]{10})$" ControlToValidate="txtMobileNumber" ValidationGroup="a"
                                                ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="txtMobileNumber" FilterMode="ValidChars" FilterType="Numbers"
                                                ValidChars="0-9">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>
                                                Company</label>
                                            <asp:TextBox CssClass="form-control" ID="txtCompany" runat="server" MaxLength="100"
                                                AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required"
                                                ValidationGroup="a" ForeColor="Red" ControlToValidate="txtCompany" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                TargetControlID="txtCompany" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                ValidChars="a-zA-Z ">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <label>
                                                Message</label>
                                            <asp:TextBox ID="txtMsg" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                MaxLength="500" AutoCompleteType="None" autocomplete="off" onKeyUp="limitText(this,this.form.count,500);"
                                                onKeyDown="limitText(this,this.form.count,500);"></asp:TextBox>
                                            <span class="mandetory">*</span> <span class="mandatory" style="font-size: 14px;
                                                color: red"><small>Maximum
                                                    <input name="count" class="inputCss" readonly="readonly" style="font-weight: bold;
                                                        color: red; width: 26px;" type="text" value="500" tabindex="-1" />
                                                    Characters Left</small></span>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Required"
                                            ValidationGroup="a" ForeColor="Red" ControlToValidate="txtMsg" SetFocusOnError="true">
                                        </asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                            TargetControlID="txtMsg" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            ValidChars="a-zA-Z0-9-. ">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-6">
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
                                        <div class="col-sm-6">
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
                                        <div class="col-sm-12">
                                            <asp:Button ID="Button1" CssClass="btn btn-sent" runat="server" Text="Submit" OnClick="Button1_Click"
                                                OnClientClick="return ValidateContact();" ValidationGroup="a" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-12 contact-infodetails">
                            <h3>
                                Contact information</h3>
                            <div class="address-list" id="divabout" runat="server">
                                <ul>
                                    <li><span><i class="fa fa-globe"></i></span><span class="span-text">IPICOL, IPICOL House,
                                        Janpath, Sahid Nagar Bhubaneswar Odisha 751022 India</span><span class="clearfix"></span></li>
                                    <li><span><i class="fa fa-envelope-o"></i></span><span class="span-text line-height50 ">
                                        <%--info@ipicolodisha.com,
                                        info@ipicolodisha.com , info@investodisha.org--%>
                                        support.investodisha@nic.in</span><span class="clearfix"></span></li>
                                    <li><span><i class="fa fa-phone-square"></i></span><span class="span-text line-height50 ">
                                        0674 - 2542601-03</span><span class="clearfix"></span></li>
                                    <li><span><i class="fa fa-fax"></i></span><span class="span-text line-height50">0674
                                        - 2543766</span><span class="clearfix"></span></li>
                                </ul>
                                <p>
                                    Toll Free Helpline - 1800 345 7157 ( Timing 10.00 A.M to 06.00 PM on working days)</p>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
