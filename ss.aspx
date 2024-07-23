<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditInvestorProfile.aspx.cs"
    Inherits="EditInvestorProfile" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script  language="javascript" type="text/javascript">

        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._';
            }
            if (allow == 'Address') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
            }
            if (allow == 'RawMetrial') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
            }
            var k;
            k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
            if (k != 13 && k != 8 && k != 0) {
                if ((e.ctrlKey == false) && (e.altKey == false)) {
                    return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }
        }

        $(document).ready(function () {

            $('.menuprofile').addClass('active');

        });
    </script>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function Validate() {

            if (blankFieldValidation('Txt_Unit_Name', 'Unit Name', 'SWP') == false) {
                return false;
            }
            if (DropDownValidation('DrpDwn_Salutation', '0', 'Prefix of Name', 'SWP') == false) {
                return false;
            }
            if (blankFieldValidation('Txt_First_Name', 'First Name', 'SWP') == false) {
                return false;
            }
            if (blankFieldValidation('Txt_Last_Name', 'Last Name', 'SWP') == false) {
                return false;
            }
            //            if (EmailValidation('txtEmail', 'Email ID', 'SWP') == false) {
            //                return false;
            //            }
            //            if (blankFieldValidation('txtEmail', 'Email Address', 'SWP') == false) {
            //                return false;
            //            }
            //function emailCheck() {
            //    var email = document.getElementById('#txtEmail');
            //    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            //    if (!filter.test(email.value) || txtEmail.value == "") {
            //        jAlert('<strong>Invalid Email_Address !</strong>', 'SWP');
            //        document.getElementById('txtEmail').focus();
            //        return false;
            //    }
            //}
            if (blankFieldValidation('Txt_Address', 'Address', 'SWP') == false) {
                return false;
            }
            if (blankFieldValidation('Txt_Mobile_No', 'Mobile Number', 'SWP') == false) {
                return false;
            }
            if (WhiteSpaceValidation1st('Txt_Mobile_No', 'Mobile Number', 'SWP') == false) {
                return false;
            }
            var val = ($("#Txt_Mobile_No").val());
            if (($("#Txt_Mobile_No").val()).substring(0, 1) == '0') {
                jAlert('Mobile Number should not be start with zero !');
                $("#Txt_Mobile_No").val('');
                $("#Txt_Mobile_No").focus();
                return false;
            }
            if (($("#Txt_Mobile_No").val().length < 10) && ($("#Txt_Mobile_No").val().length > 0)) {
                jAlert('<strong>Mobile Number can not be less then 10 characters !</strong>', 'SWP');
                $("#Txt_Mobile_No").focus();
                return false;
            }

            if (blankFieldValidation('Txt_CIN_No', 'CIN number', 'SWP') == false) {
                return false;
            }
            if ($('#Txt_CIN_No').val().substring(0, 1) == '0') {
                jAlert('<strong>CIN number should not be start with zero !</strong>', 'SWP');
                $('#Txt_CIN_No').val('');
                $('#Txt_CIN_No').focus();
                return false;
            }
            if ($('#Txt_CIN_No').val().length != 21) {
                jAlert('<strong>CIN number should be 21 digits !</strong>', 'SWP');
                $("#Txt_CIN_No").focus();
                return false;
            }
            if (WhiteSpaceValidation1st('Txt_CIN_No', 'CIN number', 'SWP') == false) {
                return false;
            }

            if (DropDownValidation('DrpDwn_Entity_Type', '0', 'Entity Type', 'SWP') == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_Entity_Type").focus(); });
                return false;
            }
        }
        $(document).ready(function () {
            // Function to validate dropdown selection
            $('#DrpDwn_Entity_Type').change(function () {
                var entityType = $(this).val();
                var panNumber = $('#Hid_Pan_Number').val();
                debugger;
                // Get the 4th letter of PAN number
                var fourthLetter = panNumber.charAt(3).toUpperCase();

                // Validate based on entity type
                switch (fourthLetter) {
                    case 'C':
                        if (entityType != 1) {
                            alert('PAN number does not match the selected entity type (Incorporated Company)');
                            $(this).val(''); // Reset dropdown selection
                        }
                        break;
                    case 'F':
                        if (entityType != 2 || entityType != 9) {
                            alert('PAN number does not match the selected entity type (LLP and Partnership Firm)');
                            $(this).val(''); // Reset dropdown selection
                        }
                        break;
                    case 'P':
                        if (entityType != 3) {
                            alert('PAN number does not match the selected entity type (Sole Proprietorship)');
                            $(this).val(''); // Reset dropdown selection
                        } else {
                            alert('dhd');
                        }
                        break;
                    case 'H':
                        if (entityType != 7) {
                            alert('PAN number does not match the selected entity type (Hindu Undivided Family)');
                            $(this).val(''); // Reset dropdown selection
                        }
                        break;
                    case 'T':
                        if (entityType != 5) {
                            alert('PAN number does not match the selected entity type (Trust)');
                            $(this).val(''); // Reset dropdown selection
                        }
                        break;
                    
                }
            });
        });


        //$(document).ready(function () {
        //    // Function to validate dropdown selection
        //    $('#DrpDwn_Entity_Type').change(function () {
        //        var entityType = $(this).val();
        //        var panNumber = $('#Hid_Pan_Number').val();

        //        // Get the 4th letter of PAN number
        //        var fourthLetter = panNumber.charAt(3).toUpperCase();

        //        // Validate based on entity type
        //        if (entityType == 1 && fourthLetter == 'C')
        //        {
                   
        //        }
        //        else
        //        {
        //            alert('Entity type does not type with Pan');
        //            $(this).val(''); // Reset dropdown selection
        //        }
        //        if (entityType == 3 && fourthLetter == 'P') {

        //        }
        //        else {
        //            alert('Entity type does not type with Pan');
        //            $(this).val(''); // Reset dropdown selection
        //        }
        //    });
        //});
    </script>

   
      
  

    <script language="javascript" type="text/javascript">
        function UpLoadStarted(sender) {
            if (document.getElementById('Txt_Unit_Name').value != '') {
                if (sender.value.trim() != "") {
                    var checkfileupload = sender.value.trim();
                    if (checkfileupload != '') {
                        var path1_length1 = checkfileupload.length;
                        var path1_length2 = checkfileupload.lastIndexOf('.');
                        var path1_extension = checkfileupload.substr(path1_length2 + 1, path1_length1);
                        if ((path1_extension.toUpperCase() != "JPG") && (path1_extension.toUpperCase() != "JPEG") && (path1_extension.toUpperCase() != "BMP") && (path1_extension.toUpperCase() != "GIF")) {
                            jAlert("<strong>Invalid File! \n Please Upload JPG,JPEG,BMP,GIF files only !</strong>", 'SWP');
                            sender.value = "";
                            return false;
                        }
                        else {
                            $('#filesave').click();
                            return true;
                        }
                    }
                }
            }
        }
        function TextCounter(ctlTxtName, lblCouter, numTextSize) {
            debugger;
            var txtName = document.getElementById(ctlTxtName).value;
            var txtNameLength = txtName.length;
            if (parseInt(txtNameLength) > parseInt(numTextSize)) {
                var txtMaxTextSize = txtName.substr(0, numTextSize)
                document.getElementById(ctlTxtName).value = txtMaxTextSize;
                alert("Entered Text Exceeds '" + numTextSize + "' Characters.");
                document.getElementById(lblCouter).innerHTML = 0;
                return false;
            }
            else {
                document.getElementById(lblCouter).innerHTML = parseInt(numTextSize) - parseInt(txtNameLength);
                return true;
            }
        }


    </script>
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="InvestRegUser" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
                <div class="investrs-tab">
                    <uc4:investoemenu ID="ineste" runat="server" />
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">
                            <div class="form-header">
                                <a href="ss.aspx" title="Edit Profile" class="pull-right proposalbtn active">
                                    Edit Profile</a> <a href="ChangeUserIdInvestor.aspx" title="Create Alternate User Name"
                                        class="pull-right proposalbtn ">Create Alternate User Name</a>
                                <h2>
                                    Investor Details</h2>
                            </div>
                            <div class="form-body">
                                <div class="col-sm-12 ">
                                    <div class="form-group">
                                        <label class="col-sm-2">
                                            Unit Name</label>
                                        <div class="col-sm-6">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Unit_Name" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">
                                            Name of the Applicant
                                        </label>
                                        <div class="col-sm-1 ">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DrpDwn_Salutation" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Mr" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Ms" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="Txt_First_Name" CssClass="form-control" runat="server"
                                                                        placeholder="Full name of the applicant" MaxLength="100"></asp:TextBox><span class="mandetory">*</span>
                                           <%-- <asp:TextBox ID="Txt_First_Name" CssClass="form-control" runat="server" placeholder="First Name"></asp:TextBox>--%>
                                            <cc1:FilteredTextBoxExtender ID="txtFirstName1" runat="server" Enabled="True" TargetControlID="Txt_First_Name"
                                                FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;/\|{}[]">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <%--<div class="col-sm-2">
                                            <asp:TextBox ID="Txt_Middle_Name" CssClass="form-control" runat="server" placeholder="Middle Name"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtMiddleName1" runat="server" Enabled="True" TargetControlID="Txt_Middle_Name"
                                                FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;/\|{}[]">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="Txt_Last_Name" CssClass="form-control" runat="server" placeholder="Last Name"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                TargetControlID="Txt_Last_Name" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;/\|{}[]">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>--%>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">
                                            Email Id</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Email_Id" autocomplete="off" CssClass="form-control height95"
                                                MaxLength="250" runat="server" Enabled="false"></asp:TextBox><span class="mandetory"> *</span>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                TargetControlID="Txt_Email_Id" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <label class="col-sm-2">
                                            Mobile Number</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Mobile_No" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Txt_Mobile_No" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="Txt_Mobile_No" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                TargetControlID="Txt_Mobile_No" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">
                                            Address</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Address" CssClass="form-control height95" TextMode="MultiLine"
                                                onKeyUp="return TextCounter('Txt_Address','Label4',250)" MaxLength="250" runat="server"></asp:TextBox>
                                            <span class="mandatory" style="font-size: 14px; color: red">(&nbsp;Maximum&nbsp;
                                                <asp:Label ID="Label4" runat="server" Text="250" Style="font-size: 14px" Height="20px" />
                                                Characters )</span>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                TargetControlID="Txt_Address" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <label class="col-sm-2">
                                            Registration Address</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_RegAddress_Invst" CssClass="form-control height95" TextMode="MultiLine"
                                                onKeyUp="return TextCounter('Txt_RegAddress_Invst','Label5',128)" MaxLength="128" runat="server"></asp:TextBox>
                                            <span class="mandatory" style="font-size: 14px; color: red">(&nbsp;Maximum&nbsp;
                                                <asp:Label ID="Label5" runat="server" Text="128" Style="font-size: 14px" Height="20px" />
                                                Characters )</span>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="Txt_RegAddress_Invst" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>
                                       <%-- <label for="SiteLocation" class="col-sm-2">
                                            Site Location
                                        </label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Site_Loc" autocomplete="off" CssClass="form-control height95"
                                                TextMode="MultiLine" onKeyUp="return TextCounter('Txt_Site_Loc','Label3',250)"
                                                MaxLength="250" runat="server"></asp:TextBox>
                                            <span class="mandetory">*</span> <span class="mandatory" style="font-size: 14px;
                                                color: red">(&nbsp;Maximum&nbsp;
                                                <asp:Label ID="Label3" runat="server" Text="250" Style="font-size: 14px" Height="20px" />
                                                Characters )</span>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                TargetControlID="Txt_Site_Loc" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>--%>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                 <div class="form-group">
                                        <label class="col-sm-2">
                                          Registration Country</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                          <asp:DropDownList CssClass="form-control" TabIndex="17" ID="DrpRegdCountry" runat="server"
                                                            AutoPostBack="true" OnSelectedIndexChanged="DrpRegdCountry_SelectedIndexChanged" >
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>                                                                                             
                                            <span class="mandetory">*</span>
                                        </div>

                                     <div runat="server" id="st3">
                                        <label class="col-sm-2">
                                           Registration State</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:DropDownList CssClass="form-control" TabIndex="18" ID="DrpRegdState" runat="server">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                          </div>

                                      <div runat="server" id="st4">
                                        <label class="col-sm-2">
                                           Registration State</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                           <asp:TextBox CssClass="form-control" TabIndex="19" ID="TxtRegdState" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="100" runat="server"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                          </div>
                                    </div>
                                    
                                 <div class="form-group">
                                        <label class="col-sm-2">
                                           Registration City</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Regd_City" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="Txt_Mobile_No" />
                                           
                                            <span class="mandetory">*</span>
                                        </div>
                                        <label class="col-sm-2">
                                           Registration Pin Code</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Regd_Pincode" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="Txt_Mobile_No" />
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                               
                                               
                                     

                                     <div class="form-group">
                                        <%--<label class="col-sm-2">
                                            Registration Address</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_RegAddress_Invst" CssClass="form-control height95" TextMode="MultiLine"
                                                onKeyUp="return TextCounter('Txt_RegAddress_Invst','Label5',128)" MaxLength="128" runat="server"></asp:TextBox>
                                            <span class="mandatory" style="font-size: 14px; color: red">(&nbsp;Maximum&nbsp;
                                                <asp:Label ID="Label5" runat="server" Text="128" Style="font-size: 14px" Height="20px" />
                                                Characters )</span>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="Txt_RegAddress_Invst" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;">
                                            </cc1:FilteredTextBoxExtender>
                                            <span class="mandetory">*</span>
                                        </div>--%>

                                            <label for="SiteLocation" class="col-sm-2">
                                            Site Location
                                        </label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Site_Loc" autocomplete="off" CssClass="form-control height95"
                                                TextMode="MultiLine" onKeyUp="return TextCounter('Txt_Site_Loc','Label3',250)"
                                                MaxLength="250" runat="server"></asp:TextBox>
                                            <span class="mandetory">*</span> <span class="mandatory" style="font-size: 14px;
                                                color: red">(&nbsp;Maximum&nbsp;
                                                <asp:Label ID="Label3" runat="server" Text="250" Style="font-size: 14px" Height="20px" />
                                                Characters )</span>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                TargetControlID="Txt_Site_Loc" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                        <label for="SiteLocation" class="col-sm-2">
                                            Site Location Address
                                        </label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_SitAddress_Invst" autocomplete="off" CssClass="form-control height95"
                                                TextMode="MultiLine" onKeyUp="return TextCounter('Txt_SitAddress_Invst','Label6',128)"
                                                MaxLength="128" runat="server"></asp:TextBox>
                                            <span class="mandetory">*</span> <span class="mandatory" style="font-size: 14px;
                                                color: red">(&nbsp;Maximum&nbsp;
                                                <asp:Label ID="Label6" runat="server" Text="128" Style="font-size: 14px" Height="20px" />
                                                Characters )</span>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                TargetControlID="Txt_SitAddress_Invst" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                InvalidChars="&quot;'<>&;">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-2">
                                          Site Location Country</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                          <asp:DropDownList CssClass="form-control" TabIndex="17" ID="DrpSlCountry" runat="server"
                                                            AutoPostBack="true" OnSelectedIndexChanged="DrpSlCountry_SelectedIndexChanged" >
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>                                                                                             
                                            <span class="mandetory">*</span>
                                        </div>

                                     <div runat="server" id="st1">
                                        <label class="col-sm-2">
                                           Site Location State</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:DropDownList CssClass="form-control" TabIndex="18" ID="DrpSlState" runat="server">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                          </div>

                                      <div runat="server" id="st2">
                                        <label class="col-sm-2">
                                           Site Location State</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                           <asp:TextBox CssClass="form-control" TabIndex="19" ID="Txt_SL_State" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="100" runat="server"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                          </div>
                                    </div>
                                    
                                 <div class="form-group">
                                        <label class="col-sm-2">
                                           Site Location City</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Sl_City" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="Txt_Mobile_No" />
                                           
                                            <span class="mandetory">*</span>
                                        </div>
                                        <label class="col-sm-2">
                                           Site Location Pin Code</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Sl_Pincode" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="Txt_Mobile_No" />
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>


                                     <div class="form-group">
                                        <label class="col-sm-2">
                                            Constitution of Company/Entity Type</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DrpDwn_Entity_Type" runat="server" OnSelectedIndexChanged="DrpDwn_Entity_Type_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"
                                                                 ToolTip="Select Constitution of Company/Entity Type name here.">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                           <span class="mandetory"> *</span>
                                            
                                            
                                        </div>
                                      <div runat="server" id="Div1">
                                        <label class="col-sm-2">
                                           CIN Number</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                             <asp:TextBox CssClass="form-control" TabIndex="19" ID="Txt_CIN_Number" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="100" runat="server"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                          </div>

                                      <div runat="server" id="Div2">
                                        <label class="col-sm-2">
                                          LLPIN Number</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                           <asp:TextBox CssClass="form-control" TabIndex="19" ID="Txt_LLPIN_Number" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="100" runat="server"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                          </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                   <asp:HiddenField ID="Hid_Pan_Number" runat="server" />
                                </div>
                                <div class="form-footer">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass=" btn btn-warning"
                                                OnClick="Btn_Back_Click" />
                                            <asp:Button ID="Btn_Update" runat="server" Text="Update" CssClass=" btn btn-success"
                                                OnClick="Btn_Update_Click" OnClientClick="return Validate();" />
                                        </div>
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
