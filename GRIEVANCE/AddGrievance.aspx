<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddGrievance.aspx.cs" Inherits="GRIEVANCE_AddGrievance" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/pealwebfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="Grievancemenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link rel="stylesheet" type="text/css" href="../css/custom.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menugrievance').addClass('active');

            $('#TxtEmail').change(function () {
                var email = $('#TxtEmail').val();
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (filter.test(email) == false) {
                    jAlert('<strong>Invalid email format !</strong>', projname);
                    $("#TxtEmail").focus();
                    $('#TxtEmail').val('');
                    return false;
                }
            });
        });
    </script>
    <script type="text/javascript">

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

        //        function CounterText(txtCtr, spanVal) {
        //            var max = 1000;
        //            var len = $('#' + txtCtr).val().length;
        //            if (len >= max) {
        //                $('#' + spanVal).val(0);
        //                return false;
        //            } else {
        //                var char = max - len;
        //                $('#' + spanVal).val(char);
        //            }
        //        }

        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            } else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function Validate() {

            if ($('#LblCompanyName').text() == "") {
                jAlert('<strong>Please enter company name !</strong>', projname);
                return false;
            }
            if (blankFieldValidation('TxtApplicantName', 'Applicant name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('TxtDesignation', 'Designation', projname) == false) {
                return false;
            }
            if (blankFieldValidation('TxtMobileNo', 'Mobile number', projname) == false) {
                return false;
            }
            if ($('#TxtMobileNo').val().substring(0, 1) == '0') {
                jAlert('<strong>Mobile number should not be start with zero !</strong>', projname);
                $('#TxtMobileNo').val('');
                $('#TxtMobileNo').focus();
                return false;
            }
            if ($('#TxtMobileNo').val().length != 10) {
                jAlert('<strong>Mobile number should be 10 digits !</strong>', projname);
                $("#TxtMobileNo").focus();
                return false;
            }
            if (WhiteSpaceValidation1st('TxtMobileNo', 'Mobile number', projname) == false) {
                return false;
            }
            if (DropDownValidation('DrpDwnInvestmentLevel', '0', 'Investment level', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwnInvestmentLevel").focus(); });
                return false;
            }
            if (DropDownValidation('DrpDwnDistrict', '0', 'district', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwnDistrict").focus(); });
                return false;
            }
            if (blankFieldValidation('TxtEmail', 'Email id', projname) == false) {
                return false;
            }
            if ($('#TxtEmail').val() != "") {
                var email = $('#TxtEmail').val();
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (filter.test(email) == false) {
                    jAlert('<strong>Invalid email address !</strong>', projname);
                    $("#TxtEmail").focus();
                    return false;
                }
            }
            if (DropDownValidation('DrpDwnGrivType', '0', 'grievance type', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwnGrivType").focus(); });
                return false;
            }
            if (DropDownValidation('DrpDwnGrivSubType', '0', 'grievance sub type', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwnGrivSubType").focus(); });
                return false;
            }
            if (blankFieldValidation('TxtGrievanceTitle', 'Grievance title', projname) == false) {
                return false;
            }
            if (blankFieldValidation('TxtGrievanceDetail', 'Grievance detail', projname) == false) {
                return false;
            }
        }

    </script>
</head>
<body class="form-body-pd">
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <div class="registration-div">
                <div class="investrs-tab">
                    <uc4:Grievancemenu ID="Grievance" runat="server" />
                </div>
                <div class="wizard wizard2">
                    <div class="form-sec">
                        <h1 class="headerpeal">Add New Grievance</h1>
                        <div class="form-header">
                            <%--  <span class="pull-right"><span class="mandatoryspan ">(*) </span>Mark fields are mandatory</span>--%>
                            <span class="mandatoryspan pull-right">( * ) Marked fields are mandatory</span>

                            <h2 class="mt-0 mb-0">1.Company Information</h2>
                        </div>
                        <div class="form-body pd-l-r-10">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 col-sm-12">
                                        <label for="company">
                                            Name of the Company/Enterprise
                                        </label>
                                        <asp:Label ID="LblCompanyName" runat="server" class="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-9 col-sm-12">
                                        <div class="form-group row">
                                            <div class="col-md-4 col-sm-4">
                                                <label for="applicant">
                                                    Name of the Applicant <span class="text-red">*</span></label>
                                                <asp:TextBox ID="TxtApplicantName" runat="server" class="form-control" autocomplete="off" placeholder="Enter Applicant Name" ToolTip="Enter applicant name here."
                                                    Onkeypress="return inputLimiter(event,'NameCharacters')" MaxLength="100"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 col-sm-4">
                                                <label for="desig">
                                                    Designation<span class="text-red">*</span></label>
                                                <asp:TextBox ID="TxtDesignation" runat="server" class="form-control" autocomplete="off" placeholder="Enter Designation" ToolTip="Enter designation of the applicant here."
                                                    Onkeypress="return inputLimiter(event,'NameCharacters')" MaxLength="100"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 col-sm-4">
                                                <label for="mobile">
                                                    Mobile Number<span class="text-red">*</span></label>
                                                <asp:TextBox ID="TxtMobileNo" runat="server" class="form-control" MaxLength="10" autocomplete="off" placeholder="Enter Mobile No" ToolTip="Enter mobile number of the applicant here."
                                                    Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-9 col-sm-12">
                                        <div class="form-group row">
                                            <div class="col-md-4 col-sm-4">
                                                <label for="Grievanceinvestmentlebel">
                                                    Investment Level <span class="text-red">*</span></label>
                                                <asp:DropDownList ID="DrpDwnInvestmentLevel" runat="server" class="form-control" ToolTip="Select investment level here.">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Project Cost >= 50 crore" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Project cost upto < 50 crore" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4 col-sm-4">
                                                <label for="Grievancedistrict">
                                                    District <span class="text-red">*</span></label>
                                                <asp:DropDownList ID="DrpDwnDistrict" runat="server" class="form-control" ToolTip="Choose your district here.">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4 col-sm-4">
                                                <label for="Grievanceemail">
                                                    Email Id <span class="text-red">*</span></label>
                                                <asp:TextBox ID="TxtEmail" runat="server" class="form-control" autocomplete="off" placeholder="Enter Email Address" ToolTip="Enter email address of the applicant here."
                                                    Onkeypress="return inputLimiter(event,'Email')"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-sec">
                        <div class="form-header">
                            <h2 class="mt-0 mb-0">2.Grievance Information</h2>
                        </div>
                        <div class="form-body pd-l-r-10">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="Grievancetype" class="col-sm-3  col-md-2">
                                                Grievance Type <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwnGrivType" runat="server" class="form-control" OnSelectedIndexChanged="DrpDwnGrivType_SelectedIndexChanged" ToolTip="Select grievance type here."
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="Grievancesubtype" class="col-sm-3 col-md-2">
                                                Grievance Sub Type <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwnGrivSubType" runat="server" class="form-control" ToolTip="Select grievance sub type here.">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <div class="row">
                                    <label for="Topic" class="col-sm-3 col-md-2">
                                        Grievance Title <span class="text-red">*</span></label>
                                    <div class="col-sm-7">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="TxtGrievanceTitle" runat="server" class="form-control" autocomplete="off" placeholder="Grievance Title" ToolTip="Put the title of your grievance here."
                                            Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label for="Grievancedetail" class="col-sm-3 col-md-2">
                                        Grievance Detail <span class="text-red">*</span></label>
                                    <div class="col-sm-7">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="TxtGrievanceDetail" runat="server" class="form-control" autocomplete="off" ToolTip="Fill your grievance details here."
                                            TextMode="MultiLine" MaxLength="1000" Height="200px" onKeyUp="limitText(this,this.form.count,1000);"
                                            onKeyDown="limitText(this,this.form.count,1000);"></asp:TextBox>
                                        <span class="mandatory" style="font-size: 14px; color: red"><small>Maximum
                                        <input id="count" class="inputCss" readonly="readonly" style="font-weight: bold; color: red; width: 32px;"
                                            type="text" value="1000" />
                                            Characters Left</small></span>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                            TargetControlID="txtGrievanceDetail" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            ValidChars="a-zA-Z0-9-. ">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label for="attachment1" class="col-sm-3 col-md-2">
                                        Attachment 1</label>
                                    <div class="col-sm-7">
                                        <div class="input-group">
                                            <span class="colon">:</span>
                                            <asp:FileUpload ID="fileUpldAttach1" runat="server" class="form-control" ToolTip="Upload your attachment here." />
                                            <a data-toggle="tooltip" class="fieldinfo" title="" data-original-title="Upload pdf,jpeg,jpg,png with size < = 5 MB">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            <asp:HiddenField ID="HdnAttach1" runat="server" />
                                            <asp:LinkButton ID="LnkBtnUploadAttach1" runat="server" CssClass="input-group-addon bg-green"
                                                OnClick="LnkBtnUploadAttach1_Click" ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:LinkButton ID="LnkBtnDelAttach1" runat="server" CssClass="input-group-addon bg-red"
                                                Visible="false" OnClick="LnkBtnDelAttach1_Click" ToolTip="Click here to remove the file."><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:HyperLink ID="HypLnkAttach1" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue" ToolTip="Click here to view the file.">
                                                              <i class="fa fa-download"></i></asp:HyperLink>
                                        </div>
                                        <small class="text-danger">(.pdf/.jpg/.jpeg/.png file only and Max file size 5 MB)</small>
                                        <asp:Label ID="LblAttach1" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                            runat="server" Text="Attachment 1 uploaded successfully">

                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label for="attachment2" class="col-sm-3 col-md-2">
                                        Attachment 2</label>
                                    <div class="col-sm-7">
                                        <div class="input-group">
                                            <span class="colon">:</span>
                                            <asp:FileUpload ID="fileUpldAttach2" runat="server" class="form-control" ToolTip="Upload your attachment here." />
                                            <a data-toggle="tooltip" class="fieldinfo" title="" data-original-title="Upload pdf,jpeg,jpg,png with size < = 5 MB">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            <asp:HiddenField ID="HdnAttach2" runat="server" />
                                            <asp:LinkButton ID="LnkBtnUploadAttach2" runat="server" CssClass="input-group-addon bg-green"
                                                OnClick="LnkBtnUploadAttach2_Click" ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:LinkButton ID="LnkBtnDelAttach2" runat="server" CssClass="input-group-addon bg-red"
                                                Visible="false" OnClick="LnkBtnDelAttach2_Click" ToolTip="Click here to remove the file."><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:HyperLink ID="HypLnkAttach2" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue" ToolTip="Click here to view the file.">
                                                              <i class="fa  fa-download"></i></asp:HyperLink>
                                        </div>
                                        <small class="text-danger">(.pdf/.jpg/.jpeg/.png file only and Max file size 5 MB)</small>
                                        <asp:Label ID="LblAttach2" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                            runat="server" Text="Attachment 2 uploaded successfully">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-sec">
                        <div class="p-xs text-center">
                            <asp:Button ID="BtnSave" runat="server" Text="Save" class="btn btn-success" OnClick="BtnSave_Click"
                                OnClientClick="return Validate();" ToolTip="Click here to submit your Grievance." />
                            <asp:Button ID="BtnReset" runat="server" Text="Reset" class="btn btn-danger" OnClick="BtnReset_Click" ToolTip="Click here to reset." />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
