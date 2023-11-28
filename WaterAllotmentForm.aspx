<%--'*******************************************************************************************************************
' File Name         : WaterAllotmentForm.aspx
' Description       : Water Allotment Form
' Created by        : Asish Kar
' Created On        : 06 Sept 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WaterAllotmentForm.aspx.cs"
    Inherits="WaterAllotmentForm" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<script src="Scripts/Validator.js" type="text/javascript" language="javascript"></script>
--%><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function ValidateWater() {
            if (blankFieldValidation('txtUnitName', 'Unit Name', 'SWP') == false) {
                $("#txtUnitName").focus();
                return false;
            }
            if (DropDownValidation('ddlIE', '0', 'Industrial Estate / Cluster', 'SWP') == false) {
                $("#ddlIE").focus();
                return false;
            }
            if (blankFieldValidation('txtPlotNo', 'Plot / Shed No.', 'SWP') == false) {
                $("#txtPlotNo").focus();
                return false;
            }
            if (DropDownValidation('ddlPurpose', '0', 'Purpose for Connection', 'SWP') == false) {
                $("#ddlPurpose").focus();
                return false;
            }
            if (blankFieldValidation('txtQuantity', 'Quantity of water required per day (in KL)', 'SWP') == false) {
                $("#txtQuantity").focus();
                return false;
            }
            if (blankFieldValidation('txtSize', 'Flows Meter Size', 'SWP') == false) {
                $("#txtSize").focus();
                return false;
            }
            if ($("#txtSize").val() == "0") {
                jAlert('<strong>Flows Meter Size can not be zero</strong>', 'SWP');
                $("#txtSize").focus();
                return false;
            }
            if (blankFieldValidation('txtModel', 'Make & Model', 'SWP') == false) {
                $("#txtModel").focus();
                return false;
            }
            if ($("#txtModel").val() == "0") {
                jAlert('<strong>Make & Model can not be zero</strong>', 'SWP');
                $("#txtModel").focus();
                return false;
            }
            if (blankFieldValidation('txtSerialNo', 'Manufacturer Serial No.', 'SWP') == false) {
                $("#txtSerialNo").focus();
                return false;
            }
            if ($("#txtSerialNo").val() == "0") {
                jAlert('<strong>Manufacturer Serial No. can not be zero</strong>', 'SWP');
                $("#txtSerialNo").focus();
                return false;
            }
            if (blankFieldValidation('txtTankSize', 'O.H. Tank Size', 'SWP') == false) {
                $("#txtTankSize").focus();
                return false;
            }
            if ($("#txtTankSize").val() == "0") {
                jAlert('<strong>O.H. Tank Size can not be zero</strong>', 'SWP');
                $("#txtTankSize").focus();
                return false;
            }
            if (blankFieldValidation('txtTankNo', 'O.H. Tank No.', 'SWP') == false) {
                $("#txtTankNo").focus();
                return false;
            }
            if ($("#txtTankNo").val() == "0") {
                jAlert('<strong>O.H. Tank No. can not be zero</strong>', 'SWP');
                $("#txtTankNo").focus();
                return false;
            }
            if (blankFieldValidation('txtSumpSize', 'Sump / Vat Size', 'SWP') == false) {
                $("#txtSumpSize").focus();
                return false;
            }
            if ($("#txtSumpSize").val() == "0") {
                jAlert('<strong>Sump / Vat Size can not be zero</strong>', 'SWP');
                $("#txtSumpSize").focus();
                return false;
            }
            if (blankFieldValidation('txtSumpNo', 'Sump / Vat No.', 'SWP') == false) {
                $("#txtSumpNo").focus();
                return false;
            }
            if ($("#txtSumpNo").val() == "0") {
                jAlert('<strong>Sump / Vat No. can not be zero</strong>', 'SWP');
                $("#txtSumpNo").focus();
                return false;
            }
            if (blankFieldValidation('txtCorName', 'Correspondence  Name', 'SWP') == false) {
                $("#txtCorName").focus();
                return false;
            }
            if (blankFieldValidation('txtCorEmail', 'Correspondence  Email', 'SWP') == false) {
                $("#txtCorEmail").focus();
                return false;
            }
            if (blankFieldValidation('txtCorMobile', 'Correspondence  Mobile', 'SWP') == false) {
                $("#txtCorMobile").focus();
                return false;
            }
            if (document.getElementById("txtCorMobile").value != "") {
                if ($("#txtCorMobile").val().length < 10) {
                    jAlert('<strong>Correspondence Mobile can not be less then 10 characters</strong>', 'SWP');
                    $("#txtCorMobile").focus();
                    return false;
                }
            }
            if (blankFieldValidation('txtCorAddress', 'Correspondence  Address', 'SWP') == false) {
                $("#txtCorAddress").focus();
                return false;
            }
            if (blankFieldValidation('txtPlumberName', 'Plumber  Name', 'SWP') == false) {
                $("#txtPlumberName").focus();
                return false;
            }
            if (blankFieldValidation('txtPlumberEmail', 'Plumber  Email', 'SWP') == false) {
                $("#txtPlumberEmail").focus();
                return false;
            }
            if (blankFieldValidation('txtPlumberMobile', 'Plumber  Mobile', 'SWP') == false) {
                $("#txtPlumberMobile").focus();
                return false;
            }
            if (document.getElementById("txtPlumberMobile").value != "") {
                if ($("#txtPlumberMobile").val().length < 10) {
                    jAlert('<strong>Plumber Mobile can not be less then 10 characters</strong>', 'SWP');
                    $("#txtPlumberMobile").focus(); 
                    return false; 
                }
            }
            if (blankFieldValidation('txtPlumberAddress', 'Plumber  Address', 'SWP') == false) {
                $("#txtPlumberAddress").focus();
                return false;
            }
        }
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

        $('#charsLeft').html(250 - $('#txtCorAddress').val().length);
        function setvalue() {
            $('#charsLeft').html(250 - $('#txtCorAddress').val().length);
        }
        $('#charsLeft1').html(250 - $('#txtPlumberAddress').val().length);
        function setvalue1() {
            $('#charsLeft1').html(250 - $('#txtPlumberAddress').val().length);
        }
    </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        });

    </script>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>
    <style type="text/css">
        .footer-top
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div">
  <div class="investrs-tab">
                    <uc4:investoemenu ID="ineste" runat="server" />
                </div>
            <div class="">
                <div id="exTab1" class="">
                    <div class="wizard">
                        <div class="wizard-inner">
                        </div>
                        <div class="tab-content clearfix">
                            <div class="form-sec dynamicform">
                                <div class="dyformheader">
                                    <div id="" class="header-details text-center">
                                        <h2>
                                            Application Form for New Water Connection</h2>
                                        <span class="mandatoryspan pull-right">( * ) Indicate Mandatory Fields</span>
                                        <div class="clearfix">
                                        </div>
                                        <div align="center">
                                        <asp:RadioButtonList ID="rdbWatr" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                            AutoPostBack="true" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Selected="True">IDCO</asp:ListItem>
                                            <asp:ListItem Value="1">DOWR</asp:ListItem>
                                        </asp:RadioButtonList></div>
                                    </div>
                                </div>
                                <div class="form-body">
                                    <div class="formbodycontent">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4" runat="server">
                                                    <label for="State">
                                                        Unit Name : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtUnitName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                        TargetControlID="txtUnitName" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars="&quot;<>&;">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:HiddenField ID="hdnUnitCode" runat="server" />
                                                    <asp:HiddenField ID="hdnStatus" runat="server" />
                                                </div>
                                                <div class="col-sm-4" runat="server">
                                                    <label for="State">
                                                        Industrial Estate / Cluster : <span class="text-red">*</span></label>
                                                    <asp:DropDownList CssClass="form-control" ID="ddlIE" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdnProjectCode" runat="server" />
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Address2">
                                                        Plot / Shed No. : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtPlotNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        TargetControlID="txtPlotNo" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars="&quot;<>&;">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4" runat="server">
                                                    <label for="State">
                                                        Purpose for Connection : <span class="text-red">*</span></label>
                                                    <asp:DropDownList CssClass="form-control" ID="ddlPurpose" runat="server">
                                                        <asp:ListItem Value="0" Selected="True">---Select---</asp:ListItem>
                                                        <asp:ListItem Value="1">General Use </asp:ListItem>
                                                        <asp:ListItem Value="2">Construction</asp:ListItem>
                                                        <asp:ListItem Value="3">Process</asp:ListItem>
                                                        <asp:ListItem Value="4">Raw Material</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Address2">
                                                        Quantity of water required per day (in KL) : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                        TargetControlID="txtQuantity" FilterType="Custom,Numbers"
                                                         FilterMode="ValidChars" InvalidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec margin-bottom0">
                                <div class="form-header">
                                    <h2>
                                        Flows Meter Details</h2>
                                </div>
                                <div class="form-body">
                                    <div class="formbodycontent">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4" runat="server">
                                                    <label for="State">
                                                        Size : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtSize" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                        TargetControlID="txtSize" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars="&quot;<>&;">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-sm-4" runat="server">
                                                    <label for="State">
                                                        Make & Model : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtModel" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                        TargetControlID="txtModel" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars="&quot;<>&;">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Address2">
                                                        Manufacturer Serial No. : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtSerialNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                        TargetControlID="txtSerialNo" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars="&quot;<>&;">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec margin-bottom0">
                                <div class="form-header">
                                    <h2>
                                        Water Storage Details</h2>
                                </div>
                                <div class="form-body">
                                    <div class="formbodycontent">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4" runat="server">
                                                    <label for="State">
                                                        O.H. Tank Size : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtTankSize" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                        TargetControlID="txtTankSize" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars="&quot;<>&;">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-sm-4" runat="server">
                                                    <label for="State">
                                                        O.H. Tank No. : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtTankNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                        TargetControlID="txtTankNo" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars="&quot;<>&;">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Address2">
                                                        Sump / Vat Size : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtSumpSize" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                        TargetControlID="txtSumpSize" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars="&quot;<>&;">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                </div></div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="Address2">
                                                        Sump / Vat No. : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtSumpNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                        TargetControlID="txtSumpNo" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars="&quot;<>&;">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec margin-bottom0">
                                <div class="form-header">
                                    <h2>
                                        Correspondence Details</h2>
                                </div>
                                <div class="form-body">
                                    <div class="formbodycontent">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="Country">
                                                        Name : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtCorName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                        TargetControlID="txtCorName" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars=";'&quot;-*|<>%^~!#?@{}[]/\_+,0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-sm-4" runat="server">
                                                    <label for="State">
                                                        Email : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtCorEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                                        TargetControlID="txtCorEmail" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars="&quot;<>&;">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-sm-4" runat="server">
                                                    <label for="State">
                                                        Mobile : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtCorMobile" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                        TargetControlID="txtCorMobile" FilterType="Custom,Numbers"
                                                         FilterMode="ValidChars" InvalidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                </div></div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="Address1">
                                                        Address : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtCorAddress" onkeyup="setvalue();" TextMode="MultiLine" Onkeypress="return inputLimiter(event,'Address')"
                                                        MaxLength="250" CssClass="form-control" Height="80px" runat="server"></asp:TextBox>
                                                    <small><i>Maximum <span id="charsLeft" class="mandatoryspan">250</span> characters left</i></small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec margin-bottom0">
                                <div class="form-header">
                                    <h2>
                                        Licensed Plumber / Regd. PHED contractor Details</h2>
                                </div>
                                <div class="form-body">
                                    <div class="formbodycontent">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="Country">
                                                        Name : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtPlumberName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                        TargetControlID="txtPlumberName" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars=";'&quot;-*|<>%^~!#?@{}[]/\_+,0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-sm-4" runat="server">
                                                    <label for="State">
                                                        Email : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtPlumberEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                                        TargetControlID="txtPlumberEmail" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        InvalidChars="&quot;<>&;">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-sm-4" runat="server">
                                                    <label for="State">
                                                        Mobile : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtPlumberMobile" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                        TargetControlID="txtPlumberMobile" FilterType="Custom,Numbers"
                                                         FilterMode="ValidChars" InvalidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                </div></div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="Address1">
                                                        Address : <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtPlumberAddress" onkeyup="setvalue1();" TextMode="MultiLine" Onkeypress="return inputLimiter(event,'Address')"
                                                        MaxLength="250" CssClass="form-control" Height="80px" runat="server"></asp:TextBox>
                                                    <small><i>Maximum <span id="charsLeft1" class="mandatoryspan">250</span> characters left</i></small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="form-footer">
                                    <div class="row">
                                        <div class="col-sm-12 text-center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                                CssClass=" btn btn-success" onclick="btnSubmit_Click" OnClientClick="return ValidateWater();" />
                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass=" btn btn-danger" 
                                                onclick="btnReset_Click" />
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
