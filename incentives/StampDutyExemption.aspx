<%--'*******************************************************************************************************************
' File Name         : StampDutyExemption.aspx
' Description       : Stamp Duty Exemption IPR-2022 Add and Draft Page
' Created by        : Debiprasanna Jena
' Created On        : 08th Aug 2023
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StampDutyExemption.aspx.cs" Inherits="incentives_StampDutyExemption" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> </title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css"/>
    <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Basic_Details.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });

            var $activePanelHeading = $('.panel-group .panel .panel-collapse.in').prev().addClass('active');  //add class="active" to panel-heading div above the "collapse in" (open) div
            $activePanelHeading.find('a').prepend('<span class="fa fa-minus"></span> ');  //put the minus-sign inside of the "a" tag
            $('.panel-group .panel-heading').not($activePanelHeading).find('a').prepend('<span class="fa fa-plus"></span> ');  //if it's not active, it will put a plus-sign inside of the "a" tag
            $('.panel-group').on('show.bs.collapse', function (e) {  //event fires when "show" instance is called
                //$('.panel-group .panel-heading.active').removeClass('active').find('.fa').toggleClass('fa-plus fa-minus'); - removed so multiple can be open and have minus sign
                $(e.target).prev().addClass('active').find('.fa').toggleClass('fa-plus fa-minus');
            });
            $('.panel-group').on('hide.bs.collapse', function (e) {  //event fires when "hide" method is called
                $(e.target).prev().removeClass('active').find('.fa').removeClass('fa-minus').addClass('fa-plus');
            });
        });

    </script>
    

    <script type="text/javascript" language="javascript">

        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    location.href = 'incentiveoffered.aspx?';
                    return true;
                }
                else {
                    return false;
                }
            });
        }

        /////////////////////////////////////////////////////////////////////////////

        function validateFile(e) {
            var ids = e.id;
            var fileExtension = ['pdf', 'zip'];
            if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                jAlert('<strong>Only .pdf or .zip formats are allowed.</strong>', projname);
                $("#" + ids).val(null);
                return false;
            }
            else {
                if ((e.files[0].size > parseInt(4 * 1024 * 1024)) && ($("#" + ids).val() != '')) {

                    jAlert('<strong>File size must be less then 4 MB !! </strong>', projname);
                    $("#" + ids).val(null);
                    //e.preventDefault();
                    return false;
                }
            }
        }

        /////////////////// jquery method for Industrial Unit////////////////////////////////////////

        function openpopup(flu) {
            var i = flu.id;
            $("#" + i).click();
            return false;
        }

        function SameAddressIndustry() {
            var cc = $('#Txt_Industry_Address').val();
            if ($("#ChkSameData").is(':checked')) {
                $('#Txt_Regd_Office_Address').val(cc);
            }
        }

///*-------------------------------------------------------------------------------------------------------------------------*/
///Add by Debiprasanna Jena on Dt-11-07-2023
        function validateThrustprioritysectorstatus() {
            if (!WhiteSpaceValidation1st('Txt_Industry_Address', 'Address of Registered Office Unit ', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Industry_Address").focus(); });
                return false;
            }

            var indAddLength = $('#Txt_Industry_Address').val().length;
            if (indAddLength > 500) {
                jAlert('<strong>Address of Registered Office Unit Should be Maximum 500 Characters  !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Industry_Address").focus(); });
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_Regd_Office_Address', 'Address of Correspondence ', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Regd_Office_Address").focus(); });
                return false;
            }
            var offAddLength = $('#Txt_Regd_Office_Address').val().length;
            if (offAddLength > 500) {
                jAlert('<strong>Address of Correspondence  Should be Maximum 500 Characters  !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Regd_Office_Address").focus(); });
                return false;
            }
            if (WhiteSpaceValidation1st('Txt_Phone_no', 'Mobile number', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_Phone_no', 'Mobile number', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_Phone_no', 'Mobile number', projname) == false) {
                return false;
            }
            var Phoneno = $('#Txt_Phone_no').val().length;
            if (Phoneno < 10) {
                jAlert('<strong>The minimum length of the mobile number should be 10.  !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Phone_no").focus(); });
                return false;
            }           
            if (WhiteSpaceValidation1st('Txt_Email', 'Email Address', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_Email', 'Email Address', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_Email', 'Email Address', projname) == false) {
                return false;
            }
            var Email = $("#Txt_Email").val();
            if (Email != '') {
                var InctMail = new RegExp(/^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i);
                if (!InctMail.test(Email)) {
                    jAlert('<strong>Invalid Email !!</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_Email").focus(); });
                    return false;
                }
            }
           
            var orgName = $('#Lbl_Org_Name_Type').text();
            if (!WhiteSpaceValidation1st('Txt_Partner_Name', orgName, projname)) {
                $("#popup_ok").click(function () { $("#Txt_Partner_Name").focus(); });
                return false;
            }
            if (WhiteSpaceValidation1st('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname) == false) {
                return false;
            } 

            var EINDt = $('#Txt_EIN_IL_Date').val()
            if (EINDt != '') {

                if (new Date(EINDt) > new Date()) {
                    jAlert('<strong>EIN/ PC/ IEM/PEAL approval Date issued by SLNA/DLNA Date should not be greater than Current Date.</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_EIN_IL_Date").focus(); });
                    return false;
                }
            }

            var DtCapitaInv = $('#Txt_Proposed_Date').val()
            if (DtCapitaInv != '') {

                if (new Date(DtCapitaInv) > new Date()) {
                    jAlert('<strong>Proposed Date/ Date of first fixed capital investment should not be greater than Current Date.</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_Proposed_Date").focus(); });
                    return false;
                }
            }

            var DtCommencProd = $('#Txt_Commence_production').val()
            if (DtCommencProd != '') {

                if (new Date(DtCommencProd) > new Date()) {
                    jAlert('<strong>Proposed Date/ Date of Commencement of production / Activity Date should not be greater than Current Date.</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_Commence_production").focus(); });
                    return false;
                }
            }
            if ($("input[name='Rad_production']:checked").val() == '1') {
               
                if (WhiteSpaceValidation1st('Txt_PC_EMI_No', 'Production certificate / EM-II No. ', projname) == false) {
                    return false;
                }
                else if (WhiteSpaceValidationLast('Txt_PC_EMI_No', 'Production certificate / EM-II No. ', projname) == false) {
                    return false;
                }
                var DtPC = $('#Txt_PC_EMI_Date').val()
                if (DtPC != '') {

                    if (new Date(DtPC) > new Date()) {
                        jAlert('<strong>Production certificate/EM-II Date should not be greater than Current Date.</strong>', projname);
                        $("#popup_ok").click(function () { $("#Txt_PC_EMI_Date").focus(); });
                        return false;
                    }
                }
            }
            if ($("input[name='Rad_production']:checked").val() == '2') {

                if (WhiteSpaceValidation1st('Txt_Uam_No', 'UAM no. for MSME ', projname) == false) {
                    return false;
                }
                else if (WhiteSpaceValidationLast('Txt_Uam_No', 'UAM no. for MSME', projname) == false) {
                    return false;
                }
                var DtUam = $('#Txt_Uam_Date').val()
                if (DtUam != '') {

                    if (new Date(DtUam) > new Date()) {
                        jAlert('<strong>UAM no. and Date for MSME should not be greater than Current Date.</strong>', projname);
                        $("#popup_ok").click(function () { $("#Txt_Uam_Date").focus(); });
                        return false;
                    }
                }
            }
            if (WhiteSpaceValidation1st('Txt_total_emp_Number', 'Total Employement Numbers', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_total_emp_Number', 'Total Employement Numbers', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_total_emp_Number', 'Total Employement Numbers', projname) == false) {
                return false;
            }
        }

///*--------------------------------------------------------------------------------------------------------------------------*/
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

    </script>
    <style type="text/css">
        .fieldinfo-left
        {
            float: left;
            margin-right: 7px;
            left: 10px;
            font-size: 17px;
            margin-top: -23px;
            color: #337ab7;
            position: relative;
            z-index: 2;
        }
        .listdiv ol
        {
            margin-left: 20px;
        }
        .listdiv ol li
        {
            font-size: 13px;
            line-height: 22px;
        }
    
        .unitdtl .groupmastreportlet2 .portletdivider
        {
            width: 20%;
        }
        .groupmastreportlet2 .portletdivider span
        {
            font-size: 20px;
            margin-left: 3px;
        }
        ul ol
        {
            margin-left: 35px !important;
        }
   
        .overlayContent
        {
            z-index: 99;
            margin: 250px auto;
            width: 90px;
            height: 90px;
        }
        .overlay
        {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #aaa;
            filter: alpha(opacity=50);
            opacity: 0.6;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <uc2:header ID="header" runat="server" />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelMain">
        <ProgressTemplate>
            <div class="overlay">
                <div class="overlayContent">
                    <img alt="" src="../images/basicloader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanelMain">
        <ContentTemplate>
            <div class="container">
                <div class="container wrapper">
                    <div class="registration-div investors-bg">
                        <div id="exTab1" class="">
                            <div class="investrs-tab">
                                <uc4:investoemenu ID="ineste" runat="server" />
                            </div>
                            <div class="tab-content clearfix">
                                <div class="tab-pane active" id="1a">
                                    <div class="form-sec">
                                        <div class="innertabs  m-b-10">
                                            <ul class="nav nav-pills pull-right">
                                                <li></li>
                                               
                                                <li></li>
                                            </ul>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-header">
                                            <a href="incentiveoffered.aspx" title="Click Here to View Incentive Offered !!" class="pull-right proposalbtn ">Incentive Offered</a>
                                            <a href="ViewApplicationStatus.aspx" title="Click Here to View Application Status !!" class="pull-right proposalbtn active">View Application Status</a>
                                            <h2>Stamp Duty Exemption IPR-2022</h2>
                                        </div>
                                        <div class="incentivesec">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="details-section leftcolm">
                                                        <div class="panel-group p-t-20" id="accordion" role="tablist" aria-multiselectable="true">

                                                            <div class="panel panel-default">
                                                                <div class="panel-heading" role="tab" id="headingOne">
                                                                    <h4 class="panel-title">
                                                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                                                            aria-expanded="true" aria-controls="collapseOne"><span class="text-red pull-right "
                                                                                style="margin-right: 20px;">All fields marked with an asterisk (*) are mandatory</span>Industrial
                                                                        Unit's Details </a>
                                                                    </h4>
                                                                </div>
                                                                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne"
                                                                    runat="server">
                                                                    <div class="panel-body">
                                                                        <div class="form-group">
                                                                           
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    1. Name of Enterprise/Industrial Unit &nbsp;</label>

                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span><asp:TextBox ID="Txt_EnterPrise_Name" CssClass="form-control"
                                                                                        runat="server" MaxLength="100" ToolTip="Enter Name of Enterprise/Industrial Unit Here !!"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" TargetControlID="Txt_EnterPrise_Name"
                                                                                        FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" ValidChars=",-/. ">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </div>

                                                                            </div>


                                                                        </div>

                                                                        <div class="form-group">
                                                                         
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4 ">
                                                                                    2. Category of the  Unit  &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:DropDownList ID="DrpDwn_Unit_Cat" CssClass="form-control" runat="server" ToolTip="Select Category of the  Unit Here !!">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                           
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4 ">
                                                                                    3. Address of Registered Office Unit &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_Industry_Address" CssClass="form-control" MaxLength="500" TextMode="MultiLine"
                                                                                        runat="server" ToolTip="Enter Address of Registered Office Unit Here !!"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTxtExt_Industry_Address" runat="server"
                                                                                        TargetControlID="Txt_Industry_Address" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom"
                                                                                        ValidChars=",-/. ">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                   

                                                                        <div class="form-group">
                                                                         
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    4. Type of  Organization  &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:DropDownList ID="DrpDwn_Org_Type" OnSelectedIndexChanged="DrpDwn_Org_Type_SelectedIndexChanged" CssClass="form-control" runat="server" AutoPostBack="true" ToolTip="Select Type of  Organization Here !!">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                           
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    5.
                                                                                    <asp:Label ID="Lbl_Org_Name_Type" runat="server" Text="Name of Managing Partner"></asp:Label>
                                                                                    &nbsp;
                                                                                </label>
                                                                                <div class="col-sm-1" style="padding-right: 0px">
                                                                                    <span class="colon">:</span><asp:DropDownList CssClass="form-control" ID="DrpDwn_Gender_Partner"
                                                                                        runat="server" ToolTip="Select Salutation Here !!">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="Txt_Partner_Name" MaxLength="100" CssClass="form-control" runat="server"
                                                                                        ToolTip="Enter Name Here !!"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" TargetControlID="Txt_Partner_Name"
                                                                                        FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=",-/. ">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    6.  Date of first fixed capital investment i.e. land / bulding / plant & machinary and balancing equipment</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group date datePicker" id="Div1" runat="server">
                                                                                        <asp:TextBox ID="Txt_Commence_production" CssClass="form-control" type="text" runat="server"
                                                                                            MaxLength="11" ToolTip="Enter Date of first fixed capital investment i.e. land / bulding / plant & machinary and balancing equipment Here !!"></asp:TextBox>
                                                                                        <span id="Span2" runat="server" class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                    </div>
                                                                                   
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                        <div class="form-group">
                                                                            
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    7. EIN/ PC/ IEM/PEAL approval letter & Production Certificate / IL No.</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_EIN_IL_NO" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter EIN/ PC/ IEM/PEAL approval letter & Production Certificate / IL No. Here !!"></asp:TextBox>
                                                                                 
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    EIN/ PC/ IEM/PEAL approval letter & Production Certificate / IL Date</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group date datePicker" id="Div_Date_EIN" runat="server">
                                                                                        <asp:TextBox ID="Txt_EIN_IL_Date" CssClass="form-control" type="text" runat="server"
                                                                                            MaxLength="11" ToolTip="Enter EIN/ PC/ IEM/PEAL approval letter & Production Certificate / IL Date Here !!"></asp:TextBox>
                                                                                        <span id="Span_Date_EIN" runat="server" class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div id="Div_Prod_Emp_Before" runat="server">

                                                                            <div class="form-group">
                                                                              
                                                                                <div class="row">
                                                                                    <label for="Iname" class="col-sm-12">
                                                                                        8. Proposed items or Items of manufacture / activities with proposed capacity / installed capacity&nbsp;
                                                                                    </label>
                                                                                    <div class="col-sm-12  margin-bottom10">
                                                                                        <table class="table table-bordered">
                                                                                            <tr>
                                                                                                <th width="5%">SlNo
                                                                                                </th>
                                                                                                <th>Product/Items Name
                                                                                                </th>
                                                                                                <th width="15%">Quantity
                                                                                                </th>
                                                                                                <th width="20%">Units
                                                                                                </th>
                                                                                                <th width="20%">Value
                                                                                                </th>
                                                                                                <th width="5%">Action
                                                                                                </th>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>&nbsp;
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Product_Name_Before" runat="server" CssClass="form-control"
                                                                                                        MaxLength="100" ToolTip="Enter Product/Items Name Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_Product_Name_Before"
                                                                                                        FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Quantity_Before" runat="server" CssClass="form-control" MaxLength="10"
                                                                                                        onkeypress="return FloatOnly(event, this);" ToolTip="Enter Quantity Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Txt_Quantity_Before"
                                                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="DrpDwn_Unit_Before" runat="server" OnSelectedIndexChanged="DrpDwn_Unit_Before_SelectedIndexChanged" CssClass="form-control"
                                                                                                        AutoPostBack="true"
                                                                                                        ToolTip="Select Unit Here !!">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:TextBox ID="Txt_Other_Unit_Before" runat="server" CssClass="form-control" placeholder="Enter Other Unit"
                                                                                                        MaxLength="100"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender64" runat="server" TargetControlID="Txt_Other_Unit_Before"
                                                                                                        FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Value_Before" runat="server" CssClass="form-control" MaxLength="10"
                                                                                                        onkeypress="return FloatOnly(event, this);" ToolTip="Enter Value Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="Txt_Value_Before"
                                                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="LnkBtn_Add_Item_Before" CssClass="btn btn-success btn-sm" OnClick="LnkBtn_Add_Item_Before_Click" runat="server" OnClientClick="return validateItemAddBefore();"
                                                                                                        ToolTip="Click Here to Add Items !!"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <asp:GridView ID="Grd_Production_Before" runat="server" CssClass="table table-bordered"
                                                                                            DataKeyNames="vchProductName" AutoGenerateColumns="false" ShowHeader="false">
                                                                                            <Columns>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Sl_No_Product_Before" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Product_Name_Before" runat="server" Text='<%# Eval("vchProductName") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Quantity_Before" runat="server" Text='<%# Eval("intQuantity") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="15%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Unit_Before" runat="server" Text='<%# Eval("vchUnit") %>'></asp:Label>
                                                                                                        <asp:HiddenField ID="Hid_Unit_Before" runat="server" Value='<%# Eval("intUnit") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="10%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Other_Unit_Before" runat="server" Text='<%# Eval("vchOtherUnit") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="10%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Value_Before" runat="server" Text='<%# Eval("decValue") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="20%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="ImgBtn_Delete_Before" runat="server" OnClick="ImgBtn_Delete_Before_Click" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                                                            CommandArgument='<%# Container.DataItemIndex %>'
                                                                                                            ToolTip="Click Here to Remove" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </div>
                                                                            </div>



                                                                        </div>

                                                                        <h4>
                                                                            <asp:Label ID="Lbl_Header_Prod_Emp" runat="server"></asp:Label></h4>

                                                                        <div class="form-group">
                                                                           
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    9. Proposed date of production / Date of Production</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group date datePicker" id="Div7" runat="server">
                                                                                        <asp:TextBox ID="Txt_Proposed_Date" CssClass="form-control" type="text" runat="server"
                                                                                            MaxLength="11" ToolTip="Enter  Proposed date of production / Date of Production Here !!"></asp:TextBox>
                                                                                        <span id="Span1" runat="server" class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                          
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    10. Proposed location of the Project</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_Propsed_location" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter  Proposed location of the Project Here !!"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <label class="col-sm-4">
                                                                                </label>
                                                                            </div>
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    Present status of the Project</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_Status" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter Present status of the Project Here !!"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                           
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    11.  Type of Deed / Agreement to be executed</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_deed" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter Type of Deed / Agreement to be executed Here !!"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                          
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    12.  Amount of Stamp Duty Exemption claimed</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_Stampduty_claimed" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter Amount of Stamp Duty Exemption claimed Here !!"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                           
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    13. Amount of Stamp Duty Exemption availed under any scheme of State Govt / Central Govt(Gol)/ Govt. Agencies / Financial institutions(mention details)</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_Availed" TextMode="MultiLine" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter  Amount of Stamp Duty Exemption availed under any scheme of State Govt / Central Govt(Gol)/ Govt. Agencies / Financial institutions(mention details) Here !!"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                           
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    14.   Amount of deferential claim to be exempted</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="txt_Deferential" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter Amount of deferential claim to be exempted Here !!"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>



                                                                        <div class="form-group">
                                                                            
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    15.  Statutory clearances,if any</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_clearances" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter Statutory clearances,if any Here !!"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>



                                                                    </div>
                                                                </div>
                                                            </div>
                                                            </div>
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading" role="tab" id="Div_other">
                                                                    
                                                                    <h4 class="panel-title">
                                                                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                                            href="#InterestSubsidyDetails" aria-expanded="false" aria-controls="collapseThree">Other Documents </a>

                                                                    </h4>
                                                                </div>


                                                                <div id="InterestSubsidyDetails" class="panel-collapse collapse" role="tabpanel"
                                                                    aria-labelledby="headingThree">
                                                                    <div class="panel-body">
                                                                        <div class="form-group" id="div3" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    1.Entrepreneurs Identification Number / PC / IEM / Industrial License / PEAL approval letter
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluEinno" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnEinno_Code" runat="server" Value="D309" />
                                                                                        <asp:HiddenField ID="HdnEinno_Name" runat="server" />
                                                                                        <asp:LinkButton ID="LnkUEinno" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDEinno"
                                                                                            runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypEinno" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblEinno" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                                        runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div8" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    2. Power of Attorney / Board Resolution / Spciety Resolution as applicable while signing as Partner / Managing Director / Authorized person
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluPoweratt" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnPoweratt_Code" runat="server" Value="D310" />
                                                                                        <asp:HiddenField ID="HdnPoweratt_Name" runat="server" />
                                                                                        <asp:LinkButton ID="LnkUPoweratt" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDPoweratt" OnClick="LnkBtn_Delete_Doc_Click"
                                                                                            runat="server" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypPoweratt" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblPoweratt" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                                        runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div9" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    3. Certificate of registration under Indian Partnership Act-1932 / Societies Registration Act-1860 /Certificate of incorporation (Memorandum of Association & Article of Association) under Company Act-1956
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluCertofreg" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnCertofreg_Code" runat="server" Value="D311" />
                                                                                        <asp:HiddenField ID="HdnCertofreg_Name" runat="server" />
                                                                                        <asp:LinkButton ID="LnkUcertofreg" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDcertofreg" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypvwCertofreg" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblCertofreg" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                                        runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group" id="div10" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    4.Document in support of date of first investment in fixed capital of industrial unit i.e. land / 
                                                               building / plant & machinery and balancing equipment 
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluFixcapital" CssClass="form-control" runat="server"
                                                                                            onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnFixcapital_Code" runat="server" Value="D312" />
                                                                                        <asp:HiddenField ID="HdnFixcapital_Name" runat="server" Value="" />
                                                                                        <asp:LinkButton ID="LnkUfixcapital" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDfixcapital" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypVwfixcapital" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblFixcapital" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div12" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    5.Appraisal/ approval/ documents in support of expansion / modernization / diversification on 
                                                               unit under Thrust/ Priority sector
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluAppraisal" CssClass="form-control" runat="server"
                                                                                            onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnAppraisal_Code" runat="server" Value="D313" />
                                                                                        <asp:HiddenField ID="HdnAppraisal_Name" runat="server" Value="" />
                                                                                        <asp:LinkButton ID="LnkUAppraisal" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDAppraisal" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypVwAppraisal" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblAppraisal" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div13" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    6.Certificate on date of first commercial production in case of taking up E/M/D
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluCommproduction" CssClass="form-control" runat="server"
                                                                                            onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnCommproduction_Code" runat="server" Value="D314" />
                                                                                        <asp:HiddenField ID="HdnCommproduction_Name" runat="server" Value="" />
                                                                                        <asp:LinkButton ID="LnkUCommproduction" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDCommproduction" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HyVwCommproduction" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblCommproduction" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div14" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    7. Certificate on migration industrial unit under Thrust/Priority sector in case of Migration 
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluMigrationindust" CssClass="form-control" runat="server"
                                                                                            onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnMigrationindust_Code" runat="server" Value="D315" />
                                                                                        <asp:HiddenField ID="HdnMigrationindust_Name" runat="server" Value="" />
                                                                                        <asp:LinkButton ID="LnkUMigrationindust" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDMigrationindust" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypVwMigrationindust" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblMigrationindust" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div17" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    8. Documents in support of Private Industrial Estate developer
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluPrivateIndust" CssClass="form-control" runat="server"
                                                                                            onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnPrivateIndust_Code" runat="server" Value="D316" />
                                                                                        <asp:HiddenField ID="HdnPrivateIndust_Name" runat="server" Value="" />
                                                                                        <asp:LinkButton ID="LnkUPrivateIndust" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDPrivateIndust" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypVwPrivateIndust" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblPrivateIndust" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div18" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    9. Deed / Agreement to be executed in Original with two copies
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluDeed" CssClass="form-control" runat="server"
                                                                                            onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnDeed_Code" runat="server" Value="D317" />
                                                                                        <asp:HiddenField ID="HdnDeed_Name" runat="server" Value="" />
                                                                                        <asp:LinkButton ID="LnkUDeed" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDDeed" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypVwDeed" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblDeed" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div19" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    10.Document(s) in support of transfer of unit to a new owner / management under the 
                                                               provisions of the State Financial Corporation (SFC) Act, 1951 or under Securitization and 
                                                               Reconstruction of Financial Assets and Enforcement of Security Interest (SARFAESI) Act 
                                                                2002, and IBC 2016 
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluFinancialAssets" CssClass="form-control" runat="server"
                                                                                            onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnFinancialAssets_Code" runat="server" Value="D318" />
                                                                                        <asp:HiddenField ID="HdnFinancialAssets_Name" runat="server" Value="" />
                                                                                        <asp:LinkButton ID="LnkUFinancialAssets" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDFinancialAssets" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypVwFinancialAssets" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblFinancialAssets" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div20" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    11. Where exemption is requested under the provisions enunciated at Para -4.5.1 (f), (g), (h) & 
                                                                (i) of IPR 2022, a certified copy of the relevant records of the Companies shall be produced 
                                                                 by the parties to the instrument to prove that the conditions prescribed are fulfilled 
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluExemption" CssClass="form-control" runat="server"
                                                                                            onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnExemption_Code" runat="server" Value="D319" />
                                                                                        <asp:HiddenField ID="HdnExemption_Name" runat="server" Value="" />
                                                                                        <asp:LinkButton ID="LnkUexemption" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDexemption" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypvwExemption" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblExemption" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div22" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    12. Valid statutory clearances / approvals / permissions for authorities including OSPCB, or undertaking thereof 
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluStatutory" CssClass="form-control" runat="server"
                                                                                            onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnStatutory_Code" runat="server" Value="D320" />
                                                                                        <asp:HiddenField ID="HdnStatutory_Name" runat="server" Value="" />
                                                                                        <asp:LinkButton ID="LnkUstatutory" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDstatutory" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypvwStatutory" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblStatutory" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div21" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    13. Undertaking on non-judicial Stamp Paper duly signed by the applicant in the format-Annexure- B1 
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluStampPaper" CssClass="form-control" runat="server"
                                                                                            onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnStampPaper_Code" runat="server" Value="D321" />
                                                                                        <asp:HiddenField ID="HdnStampPaper_Name" runat="server" Value="" />
                                                                                        <asp:LinkButton ID="LnkUStampPaper" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDStampPaper" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypvwStampPaper" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblStampPaper" Style="font-size: 12px;" CssClass="text-blue"
                                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                        <div class="form-group" id="div11" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    14. Provisional Priority/Thrust Sector Certificate or Priority/Thrust Sector Certificate as the case 
                                                                 may be
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluThrustcerti" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnThrustcerti_Code" runat="server" Value="D322" />
                                                                                        <asp:HiddenField ID="HdnThrustcerti_Name" runat="server" />
                                                                                        <asp:LinkButton ID="LnkUThrustcerti" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDThrustcerti" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypvwThrustcerti" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblThrustcerti" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                                        runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" id="div23" runat="server">
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-5">
                                                                                    15.Document in support of delay in implementation condoned by Empowered Committee in 
                                                                case of delay
                                                                                </label>
                                                                                <div class="col-sm-6">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload ID="FluEmpCommittee" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                                        <asp:HiddenField ID="HdnEmpCommittee_Code" runat="server" Value="D323" />
                                                                                        <asp:HiddenField ID="HdnEmpCommittee_Name" runat="server" />
                                                                                        <asp:LinkButton ID="LnkUEmpCommittee" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                                            ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="LnkDEmpCommittee" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                                            Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                        <asp:HyperLink ID="HypvwEmpCommittee" runat="server" Target="_blank" Visible="false"
                                                                                            CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                                                    <asp:Label ID="LblEmpCommittee" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                                        runat="server" Text="Document uploaded successfully"></asp:Label>
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
                                        </div>

                                        <div class="form-footer">
                                            <div class="row">
                                                <div class="col-sm-12 text-center">
                                                    <asp:Button ID="BtnApply" runat="server" Style="font-weight: bold;" OnClick="BtnApply_Click" Text="Submit"
                                                        CssClass="btn btn-success" OnClientClick="return validateThrustprioritysectorstatus();"
                                                        ToolTip="Click Here to Submit" />
                                                    <asp:Button ID="BtnDraft" OnClick="BtnDraft_Click" Style="font-weight: bold;" runat="server" Text="Draft"
                                                        CssClass="btn btn-warning" OnClientClick="return validateThrustprioritysectorstatus();"
                                                        ToolTip="Click Here to Draft" />
                                                    <asp:Button ID="BtnCancel" runat="server" Style="font-weight: bold;" Text="Cancel" CssClass="btn btn-danger"
                                                        ToolTip="Click Here to Cancel" />
                                                    <asp:HiddenField ID="Hid_Is_Exist_Before" runat="server" />
                                                    <asp:HiddenField ID="Hid_Is_Exist_After" runat="server" />
                                                    <asp:HiddenField ID="Hid_Data_Source" runat="server" />
                                                    <asp:HiddenField ID="Hid_PC_Status" runat="server" />
                                                    <asp:HiddenField ID="Hid_Project_Type" runat="server" />
                                                    <asp:HiddenField ID="Hid_Inct_Mode" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-footer">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 style="color: #777;">General Instructions (To fill up subsequent incentive applications)</h4>
                                                    <div class="listdiv">
                                                        <ol>
                                                            <li>Need to be provid by department </li>

                                                        </ol>
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
               
            </div>
            <uc3:footer ID="footer" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkUEinno" />
            <asp:PostBackTrigger ControlID="lnkUPoweratt" />
            <asp:PostBackTrigger ControlID="lnkUcertofreg" />
            <asp:PostBackTrigger ControlID="lnkUfixcapital" />
            <asp:PostBackTrigger ControlID="lnkUAppraisal" />
            <asp:PostBackTrigger ControlID="lnkUCommproduction" />
            <asp:PostBackTrigger ControlID="lnkUMigrationindust" />
            <asp:PostBackTrigger ControlID="lnkUPrivateIndust" />
            <asp:PostBackTrigger ControlID="lnkUDeed" />
            <asp:PostBackTrigger ControlID="lnkUFinancialAssets" />
            <asp:PostBackTrigger ControlID="lnkUexemption" />
            <asp:PostBackTrigger ControlID="lnkUstatutory" />
            <asp:PostBackTrigger ControlID="lnkUStampPaper" />
            <asp:PostBackTrigger ControlID="lnkUThrustcerti" />
            <asp:PostBackTrigger ControlID="lnkUEmpCommittee" />
            
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="Hid_Pop" runat="server" />
    <asp:HiddenField ID="Hid_Pop_2" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
        TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="Btn_Close">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: none;">
        <div id="undertakingipr2015">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header bg-purpul">
                        <h4 class="modal-title">
                            Please provide undertaking stating that your unit is not a part of the negative
                            unit list under <strong>IPR 2015</strong></h4>
                    </div>
                    <div class="modal-body">
                        <h4>
                            UNDERTAKING</h4>
                        <p>
                            I hereby declare that my Unit/Enterprise does not fall under the following ineligible
                            unit.
                        </p>
                      
                        <h4>
                            List of Ineligible Unit Types</h4>
                        <h5 class="text-red">
                            Reference IPR-2015 Point-16,Annexure II</h5>
                        <div class="listdiv">
                            <ul type="I">
                                <li>B. Industrial Unit will not include non-manufacturing/servicing industries
                                    <ol>
                                        <li>General workshops including repair workshops having investment less than 50 Lakhs
                                            and running with power </li>
                                        <li>Cold storage and seafood freezing unit having investment less than Rs. 25 Lakhs
                                        </li>
                                        <li>Electronics repair and maintenance unit for professional grade equipment and computer
                                            software ITES/BPO and related with invest less than Rs. 25 lakhs </li>
                                        <li>Technology Development Laboratory/Prototype Development Centre/Research & Development
                                            with investment less than Rs. 25 Lakh </li>
                                        <li>Printing press with investment in plant and machinery/equipment of less than Rs.
                                            50 Lakhs </li>
                                        <li>Laundry/Dry Cleaning with investment in plant and machinery/equipment of less than
                                            Rs.25 Lakh </li>
                                    </ol>
                                </li>
                                <li>C. The following units shall neither be eligible for fiscal incentives specified
                                    under this IPR nor for allotment of land at concessional rates in the State, but
                                    shall be eligible for investment facilitation, allotment of land under normal rules
                                    at benchmark value/market rate and recommendation to the financial institutions
                                    for term loan and working capital and for recommendation, if necessary to the Power
                                    Distribution Companies:
                                    <ol>
                                        <li>Hullers and Rice mills with investment in plant and machinery of less than Rs.25
                                            Lakhs for industrially backward districts and less than one crore rupees for other
                                            areas </li>
                                        <li>Flour mills including manufacture of besan, pulse mills and chuda mills expect investment
                                            in plant and machinery of more than Rs. 25 Lakhs for industrially backward districts
                                            and less than 1 crore for other areas (Excluding Roller Flour mills) </li>
                                        <li>
                                            <ol>
                                                1. Processing of spices with investment in plant and machinery with less than Rs
                                                10 lakhs for industrially backward districts and less than 2 crore rupees for other
                                                areas.
                                            </ol>
                                            <ol>
                                                2. Units without Spice-mark or Agmark
                                            </ol>
                                        </li>
                                        <li>Confectionary with investment in plant and machinery with less than Rs.10 Lakhs
                                            for industrially backward districts and less than two crore rupees for other areas
                                        </li>
                                        <li>Oil mills with expellers including oil processing, filtering , de-coloring ,coloring
                                            ,refining of edible oils and hydro-generation thereof except investment in plant
                                            and machinery of RS. 10 Lakhs in industrial backward areas. </li>
                                        <li>Preparation of sweets and savories etc. excluding units using mechanized process
                                            with an investment in plant and machinery </li>
                                        <li>Bread making(excluding mechanized bakery) </li>
                                        <li>Mixture.Bhujia and chanachur preparation units </li>
                                        <li>Manufacture of ice candy </li>
                                        <li>Manufacture and processing of betel nuts </li>
                                        <li>Hatcheries, Piggeries, rabbit or Broiler farming </li>
                                        <li>Standalone sponge iron plants </li>
                                        <li>Iron and steel processors, such as cutting of sheets,bars,angles,coils,M.S. sheets,
                                            recoiling, straightening,corrugating,drophammer units etc with low value addition
                                        </li>
                                        <li>Cracker-making units </li>
                                        <li>Tyre retreading units with investment in plant and machinery of less Rs.20 Lakhs
                                        </li>
                                        <li>Stone crushing units </li>
                                        <li>Coal/coke screening coal /coke Briquetting </li>
                                        <li>Production of firewood and charcoal </li>
                                        <li>Painting and spray-painting units with investment in plant and machinery of less
                                            than Rs. 20 Lakhs </li>
                                        <li>Units for physical mixing of fertilizers. </li>
                                        <li>Brick- making units (except units making refractory bricks and those making bricks
                                            from flyash, red mud and similar industrial waste not less than 25% as base martial)
                                        </li>
                                        <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                                            of less than Rs. 20 Lakhs. </li>
                                        <li>Saw mills, sawing of timber. </li>
                                        <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                                            cluster of at least 20 units. </li>
                                        <li>Drilling rigs, Bore-wells and Tube-wells </li>
                                        <li>Units for mixing or blending/packaging of tea. </li>
                                        <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purpose and Gudakhu
                                            manufacturing units. </li>
                                        <li>Units for bottling of medicines. </li>
                                        <li>Bookbinding/Rubber stamp making/making notebooks, exercise notebook s and envelopes.
                                        </li>
                                        <li>Distilled water units </li>
                                        <li>Tailoring (other than readymade garment manufacturing units) </li>
                                        <li>Repacking /stitching/printing of woven sacks out of woven fabrics. </li>
                                        <li>Pre-Processing of oil seeds-Decorticating, expelling, crushing, parching and frying.
                                        </li>
                                        <li>Aerated water and soft drinks units </li>
                                        <li>Bottling units or any activity in respect of IMFL or liquor of any kind </li>
                                        <li>Size reducing/size separating units/ Grinding / mixing units with investment in
                                            plant and machinery of less than ten crore rupees except manufacturing of cement
                                            with clinker. </li>
                                        <li>Polythene less than 40 micron in thickness /recycling of plastic materials.
                                        </li>
                                        <li>Thermal power plants. </li>
                                        <li>Repackaging units. </li>
                                       
                                    </ol>
                                    <small class="text-red">Note: List of industrial units indicated above may be modified
                                        by the Government from time to time.</small> </li>
                            </ul>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-sm-6 text-left">
                                <asp:CheckBox ID="ChkBx_Agree" runat="server" Text="I agree that provided information is correct." /></div>
                            <div class="col-sm-6">
                                <asp:Button ID="Btn_Submit" runat="server" Text="Submit"
                                    class="btn btn-success" OnClientClick="return validate_checkbox();" ToolTip="Click Here to Submit and Proceed" />
                                <asp:Button ID="Btn_Close" runat="server" Text="Close" class="btn btn-danger" ToolTip="Click Here to Close Window" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel2"
        TargetControlID="Hid_Pop_2" BackgroundCssClass="modalBackground" CancelControlID="Btn_No">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" CssClass="modalfade" Style="display: none;">
        <div id="Div2">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header bg-purpul">
                        <h4 class="modal-title">
                            Alert</h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            Your unit has exceeded the time period permitted by the IPR 2015 to commence production
                            from the Date of FFCI by
                            <asp:Label ID="Lbl_Dynamic_No" runat="server"></asp:Label>
                            years. You may only apply for incentives, once your application for condonation
                            of delay in implementation is approved by the Empowered Committee .
                        </p>
                        <p>
                            Do you wish to proceed to apply for condonation of delay?
                            <asp:Button ID="Btn_Yes" runat="server" Text="Yes"  class="btn btn-success"
                                ToolTip="Click here if you wish to proceed to apply for condonation of delay" />
                            <asp:Button ID="Btn_No" runat="server" Text="No" class="btn btn-danger" ToolTip="Click here if you don't wish to apply for condonation of delay." />
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    
   <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function pageLoad() {
            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy",
                    clearBtn: true
                });
            });

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").on("click", function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });
        }

        function validate_checkbox() {
            if (document.getElementById('<%= ChkBx_Agree.ClientID %>').checked == false) {
                jAlert('<strong>Please Click on CheckBox to Agree !!</strong>', projname);
                document.getElementById('<%= ChkBx_Agree.ClientID %>').focus();
                return false;
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
        
    </form>
</body>
</html>
