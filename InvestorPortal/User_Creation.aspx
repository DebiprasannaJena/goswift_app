<%--'*******************************************************************************************************************
' File Name         : User_Creation.aspx
' Description       : Second Level User Creation
' Created by        : Sushant Kumar Jena
' Created On        : 29-Aug-2018
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_Creation.aspx.cs" Inherits="InvestorPortal_User_Creation" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/pealwebfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/animate.css" rel="stylesheet" type="text/css" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('.menumanage').addClass('active');
        })     

    </script>
    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        /*--------------------------------------------------------------*/

        function Validate() {

            if (DropDownValidation('DrpDwn_Salutation', '0', 'prefix for name of Entrepreneur', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_Salutation").focus(); });
                return false;
            }
            if (blankFieldValidation('Txt_First_Name', 'First name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('Txt_Last_Name', 'Last name', projname) == false) {
                return false;
            }


            if (blankFieldValidation('Txt_Email_Id', 'Email id', projname) == false) {
                return false;
            }
            if ($('#Txt_Email_Id').val() != "") {
                var email = $('#Txt_Email_Id').val();
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (filter.test(email) == false) {
                    jAlert('<strong>Invalid email address !</strong>', projname);
                    $("#Txt_Email_Id").focus();
                    return false;
                }
            }

            if (blankFieldValidation('Txt_Mobile_No', 'Mobile number', projname) == false) {
                return false;
            }
            if ($('#Txt_Mobile_No').val().substring(0, 1) == '0') {
                jAlert('<strong>Mobile number should not be start with zero !</strong>', projname);
                $('#Txt_Mobile_No').val('');
                $('#Txt_Mobile_No').focus();
                return false;
            }
            if ($('#Txt_Mobile_No').val().length != 10) {
                jAlert('<strong>Mobile number should be 10 digits !</strong>', projname);
                $("#Txt_Mobile_No").focus();
                return false;
            }
            if (WhiteSpaceValidation1st('Txt_Mobile_No', 'Mobile number', projname) == false) {
                return false;
            }

            if (blankFieldValidation('Txt_Designation', 'Designation', projname) == false) {
                return false;
            }

            if (DropDownValidation('DrpDwn_Unit_Name', '0', 'unit name', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_Unit_Name").focus(); });
                return false;
            }

            if (blankFieldValidation('Txt_User_Id', 'User id', projname) == false) {
                return false;
            }
            if (blankFieldValidation('Txt_Pwd', 'Password', projname) == false) {
                return false;
            }
            if (!checkPassword()) {
                return false;
            }
            if (blankFieldValidation('Txt_Conf_Pwd', 'Confirm password', projname) == false) {
                return false;
            }
            if (document.getElementById("Txt_Pwd").value != document.getElementById("Txt_Conf_Pwd").value) {
                document.getElementById("Txt_Conf_Pwd").focus();
                jAlert("<strong>Confirm password should be same as password !</strong>", projname);
                return false;
            }
        }

        /*--------------------------------------------------------------*/

        function checkPassword() {
            var txtNewPsw = document.getElementById("Txt_Pwd");
            var pwdVal = txtNewPsw.value;
            var illegalChars = /[\W_]g/;
            var re = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,14}$/
            if (!re.test(pwdVal)) {
                jAlert("<strong>Password must contain atleast one uppercase letter,one lowercase letter,one number and one special character and length must be between 8-14 characters !</strong>", projname);
                txtNewPsw.focus();
                return false;
            }
            else {
                return true;
            }
        }

    </script>
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
    <form id="form2" runat="server" defaultbutton="Btn_Submit">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:pealmenu ID="pealmenu" runat="server" />
                    </div>
                    <div class="form-sec">
                        <div class="headerpeal m-b-10">
                            <h2>
                                User Creation
                            </h2>
                        </div>
                        <div class="form-sec">
                            <div class="form-header">
                                <span class="mandatoryspan pull-right">( * ) Mark Fields Are Mandatory</span>
                                <h2>
                                    Personal Details</h2>
                            </div>
                            <div class="form-body">
                                <div class="formbodycontent">
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="Contact" class="col-sm-2">
                                                Name
                                            </label>
                                            <div class="col-sm-1">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Salutation" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Mr" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Ms" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                                <span class="mandetory">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="Txt_First_Name" autocomplete="off" CssClass="form-control" runat="server"
                                                    placeholder="First Name" MaxLength="50"></asp:TextBox><span class="mandetory">*</span>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                    TargetControlID="Txt_First_Name" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                    InvalidChars="&quot;'<>&;/\|{}[]">
                                                </cc1:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="Txt_Middle_Name" autocomplete="off" CssClass="form-control" runat="server"
                                                    placeholder="Middle Name" MaxLength="50"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                    TargetControlID="Txt_Middle_Name" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                    InvalidChars="&quot;'<>&;/\|{}[]">
                                                </cc1:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="Txt_Last_Name" autocomplete="off" CssClass="form-control" runat="server"
                                                    placeholder="Last Name" MaxLength="50"></asp:TextBox><span class="mandetory">*</span>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                    TargetControlID="Txt_Last_Name" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                    InvalidChars="&quot;'<>&;/\|{}[]">
                                                </cc1:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="email" class="col-sm-2">
                                                Email Id
                                            </label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="Txt_Email_Id" autocomplete="off" CssClass="form-control height95"
                                                    MaxLength="250" runat="server"></asp:TextBox><span class="mandetory"> *</span>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                    TargetControlID="Txt_Email_Id" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                    InvalidChars="&quot;'<>&;">
                                                </cc1:FilteredTextBoxExtender>
                                            </div>
                                            <label for="Mobile" class="col-sm-2">
                                                Mobile Number
                                            </label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="Txt_Mobile_No" CssClass="form-control" runat="server" MaxLength="10"
                                                    autocomplete="off"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtMobileNo" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_Mobile_No" />
                                                <a data-toggle="tooltip" class="fieldinfo" title="This number will be used for all future official communication via SMS ,don't prefix with +91 or 0!">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a><span class="mandetory">
                                                        *</span>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                    TargetControlID="Txt_Mobile_No" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                    InvalidChars="&quot;'<>&;">
                                                </cc1:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="Email" class="col-sm-2">
                                                Designation
                                            </label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="Txt_Designation" autocomplete="off" CssClass="form-control height95"
                                                    MaxLength="250" runat="server"></asp:TextBox><span class="mandetory"> *</span>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                    TargetControlID="Txt_Designation" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                    InvalidChars="&quot;'<>&;">
                                                </cc1:FilteredTextBoxExtender>
                                            </div>
                                            <label for="unitName" class="col-sm-2">
                                                Unit Name
                                            </label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Unit_Name" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-sec">
                            <div class="form-header">
                                <span class="mandatoryspan pull-right">( * ) Mark Fields Are Mandatory</span>
                                <h2>
                                    Login Details</h2>
                            </div>
                            <div class="form-body">
                                <div class="formbodycontent">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="email" class="col-sm-3 col-md-2">
                                                        User ID
                                                    </label>
                                                    <div class="col-sm-6 col-md-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="Txt_User_Id" CssClass="form-control" runat="server" autocomplete="off"
                                                            Enabled="false" MaxLength="50"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="This is the auto generated user id and will be used for login to the system !">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a><span class="mandetory">
                                                                *</span> <small class="text-red" style="margin-left: 0px;">The above id will be used
                                                                    as the user id when logged into the system.</small>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                            TargetControlID="Txt_User_Id" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                            InvalidChars="&quot;'<>&;">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Password" class="col-sm-3 col-md-2">
                                                        Password
                                                    </label>
                                                    <div class="col-sm-9 col-md-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="Txt_Pwd" CssClass="form-control" runat="server" TextMode="Password"
                                                            autocomplete="off" MaxLength="14"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Provide Password as per Password Policy!">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a><span class="mandetory">
                                                                *</span><small class="text-red" style="margin-left: 0px;">Password Policy: It should
                                                                    be between 8-14 characters,should contain atleast one uppercase,one lowercase,one
                                                                    number and one special character(!@#$&*).</small>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                            TargetControlID="Txt_Pwd" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                            InvalidChars="&quot;'<>;">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <span id="password_strength"></span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Re-Password" class="col-sm-3 col-md-2">
                                                        Confirm Password
                                                    </label>
                                                    <div class="col-sm-9 col-md-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="Txt_Conf_Pwd" CssClass="form-control" runat="server" TextMode="Password"
                                                            autocomplete="off" MaxLength="14"></asp:TextBox>
                                                        <span class="mandetory">*</span>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                            TargetControlID="Txt_Conf_Pwd" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                            InvalidChars="&quot;'<>;">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-sec">
                            <div class="form-footer">
                                <div class="row">
                                    <div class="col-sm-12 text-center">
                                        <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass=" btn btn-success"
                                            OnClick="Btn_Submit_Click" OnClientClick="return Validate();" />
                                        <asp:Button ID="Btn_Reset" runat="server" Text="Reset" CssClass=" btn btn-danger" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
