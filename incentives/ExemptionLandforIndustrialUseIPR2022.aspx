<%--'*******************************************************************************************************************
' File Name         : ExemptionLandforIndustrialUseIPR2022.aspx
' Description       : Exemption Land for Industrial Use IPR-2022 Add and Draft Page
' Created by        : Debiprasanna Jena
' Created On        : 11th Oct 2023
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExemptionLandforIndustrialUseIPR2022.aspx.cs" Inherits="incentives_ExemptionLandforIndustrialUseIPR2022" %>

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
        ///*----------------------------------------------------------------------------------------------*/
             ///Add by Debiprasanna Jena on Dt-11-07-2023
        ///*----------------------------------------------------------------------------------------------*/
        function validateThrustprioritysectorstatus() {
            if (!WhiteSpaceValidation1st('Txt_Regd_Office_Address', 'Address of Registered Office Unit ', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Regd_Office_Address").focus(); });
                return false;
            }

            var indAddLength = $('#Txt_Regd_Office_Address').val().length;
            if (indAddLength > 500) {
                jAlert('<strong>Address of Registered Office Unit Should be Maximum 500 Characters  !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Regd_Office_Address").focus(); });
                return false;
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
            if (!blankFieldValidation('Txt_FFCI_Date_After', 'Date of First Fixed Capital Investment', projname)) {
                return false;
            }
            if (new Date($('#Txt_FFCI_Date_After').val()) > new Date()) {
                jAlert('<strong>Date of First Fixed Capital Investment should not be greater than Current Date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_FFCI_Date_After").focus(); });
                return false;
            }
            if ($('#Hid_FFCI_After_File_Name').val() == '') {
                jAlert('<strong>Please Upload Document in Support of FFCI !!</strong>', projname);
                $("#popup_ok").click(function () { $("#FU_FFCI_After").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_Land_After', 'Land Investment', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Building_After', 'Building Investment', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Plant_Mach_After', 'Plant and Machinary Investment', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Other_Fixed_Asset_After', 'Other Fixed Asset', projname)) {
                return false;
            }
            if ($('#Hid_Approved_DPR_After_File_Name').val() == '') {
                jAlert('<strong>Please Upload Document in Support of Approved DPR(Detail Project Report) !!</strong>', projname);
                $("#popup_ok").click(function () { $("#FU_Approved_DPR_After").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_Equity_Amt', 'Equity Amount', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Loan_Bank_FI', 'Loan from Bank/FI Amount', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_FDI_Componet', 'FDI Componet Amount', projname)) {
                return false;
            }

            var fdi = parseFloat($('#Txt_FDI_Componet').val())
            var equity = parseFloat($('#Txt_Equity_Amt').val())

            if (fdi > equity) {
                jAlert('<strong>FDI Cannot be Greater than Equity !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_FDI_Componet").focus(); });
                return false;
            }
        }

   ///*------------------------------------------------------------------------------------------------------*/
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

 ////*---------------------------------------------------------------------------------------------------*//
        function validateItemAdd() {           
            if (!blankFieldValidation('Txt_Mouza', 'Mouza', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_Mouza', 'Mouza', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Mouza").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_Khata_No', 'Khata No', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Plot_No', 'Plot No', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Area', 'Area', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Present_Kisam', 'Present Kisam', projname)) {
                return false;
            }
        }

///*------------------------------------------------------------------------------------------------------------//
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
                   <div  class="container wrapper">
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
                                            <li><a href="incentiveoffered.aspx" title="Click Here to View Incentive Offered !!">
                                                Incentive Offered</a></li>
                                            
                                            <li><a href="ViewApplicationStatus.aspx" title="Click Here to View Application Status !!">
                                                View Application Status</a></li>
                                        </ul>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-header">                                      
                                        <h2>
                                            Exemption Land For Industrial Use IPR-2022</h2>
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
                                                                                   1.  Name of Enterprise/Industrial Unit &nbsp;</label>
                                                                                    
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
                                                                                 2.  Category of the  Unit  &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:DropDownList ID="DrpDwn_Unit_Cat" CssClass="form-control" runat="server"  ToolTip="Select Category of the  Unit Here !!">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                           <div class="form-group">
                                                                          
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4 ">
                                                                                  3. Thrust Sector / Priority Sector  &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_ThrustorPriority" Onkeypress="return inputLimiter(event,'Email')" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter Thrust or Priority Sector Here !!"></asp:TextBox>
                                                                                    
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                       

                                                                        <div class="form-group">                                
                                                                           
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4 ">
                                                                                   4. Address of Registered Office Unit &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_Regd_Office_Address" CssClass="form-control" MaxLength="500" TextMode="MultiLine"
                                                                                        runat="server" ToolTip="Enter Address of Registered Office Unit Here !!"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTxtExt_Industry_Address" runat="server"
                                                                                        TargetControlID="Txt_Regd_Office_Address" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom"
                                                                                        ValidChars=",-/. ">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                   
                                                                     

                                                                        <div class="form-group">
                                                                           
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                   5. Type of  Organization  &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:DropDownList ID="DrpDwn_Org_Type"  CssClass="form-control" runat="server" OnSelectedIndexChanged="DrpDwn_Org_Type_SelectedIndexChanged" AutoPostBack="true"  ToolTip="Select Type of  Organization Here !!">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                   6. <asp:Label ID="Lbl_Org_Name_Type" runat="server" Text="Name of Managing Partner"></asp:Label>
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
                                                                                  7. Date of first fixed capital investment i.e. land / bulding / plant & machinary and balancing equipment</label>
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
                                                                                   8. EIN/ PC/ IEM/PEAL approval letter & Production Certificate / IL No.</label>
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
                                                                                       9. Proposed items or Items of manufacture / activities with proposed capacity / installed capacity&nbsp;
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
                                                                                                    <asp:DropDownList ID="DrpDwn_Unit_Before" runat="server" OnSelectedIndexChanged="DrpDwn_Unit_Before_SelectedIndexChanged"  CssClass="form-control"
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
                                                                                                    <asp:LinkButton ID="LnkBtn_Add_Item_Before" CssClass="btn btn-success btn-sm"  runat="server" OnClick="LnkBtn_Add_Item_Before_Click" OnClientClick="return validateItemAddBefore();"
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
                                                                                                        <asp:ImageButton ID="ImgBtn_Delete_Before" runat="server" OnClick="ImgBtn_Delete_Before_Click"  ImageUrl="~/Portal/images/deleteIcon.png"
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
                                                                              10.  Proposed date of production / Date of Production</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group date datePicker" id="Div7" runat="server">
                                                                                    <asp:TextBox ID="Txt_Proposed_Date" CssClass="form-control" type="text" runat="server"
                                                                                        MaxLength="11" ToolTip="Enter  Proposed date of production / Date of Production Here !!"></asp:TextBox>
                                                                                    <span id="Span1" runat="server" class="input-group-addon"><i class="fa fa-calendar">
                                                                                    </i></span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                        <div class="form-group">
                                                                                 
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                               11. Proposed location of the Project</label>
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
                                                                              12. Name of the Financer with details of Loan applied for / sanctioned / availed</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:TextBox ID="Txt_Financer" CssClass="form-control" MaxLength="100" runat="server"
                                                                                            ToolTip="Enter  Name of the Financer with details of Loan applied for / sanctioned / availed Here !!"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                         <div class="form-group">
                                                                                  
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                             13.  Cost of the Project (New / Existing & E/M/D)</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:TextBox ID="Txt_Cost_Project" CssClass="form-control" MaxLength="100" runat="server"
                                                                                    ToolTip="Enter Cost of the Project (New / Existing & E/M/D) Here !!"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                           <div class="form-group">
                                                                                 
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                             14. Area of Land required as per DPR / Project report</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:TextBox ID="Txt_Land_required" CssClass="form-control" MaxLength="100" runat="server"
                                                                                            ToolTip="Enter  Area of Land required as per DPR / Project report Here !!"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                            <div class="form-group">
                                                                                 
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                            15.   Area of Land Acquired</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:TextBox ID="Txt_Land_Acquired" CssClass="form-control" MaxLength="100" runat="server"
                                                                                            ToolTip="Enter Area of Land Acquired Here !!"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>


                                                                          
                                                                         <div class="form-group">
                                                                                  
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                             16.  Particulars of Land to be converted</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                              <asp:RadioButtonList ID="Rad_Land_converted" OnSelectedIndexChanged="Rad_Land_converted_SelectedIndexChanged" AutoPostBack="true" runat="server" RepeatDirection="Horizontal"
                                                                                        CssClass="radio-inline">
                                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                        <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                         <div id="Div_Land_Converter" runat="server">

                                                                            <div class="form-group">
                                                                               
                                                                                <div class="row">
                                                                                    <label for="Iname" class="col-sm-12">
                                                                                        Particulars of Land to be converted&nbsp;
                                                                                    </label>
                                                                                    <div class="col-sm-12  margin-bottom10">
                                                                                        <table class="table table-bordered">
                                                                                            <tr>
                                                                                                <th width="5%">SlNo
                                                                                                </th>
                                                                                                <th>Mouza
                                                                                                </th>
                                                                                                <th width="15%">Khata No
                                                                                                </th>
                                                                                                <th width="20%">Plot No
                                                                                                </th>
                                                                                                <th width="20%">Area
                                                                                                </th>
                                                                                                <th width="20%">Present Kisam
                                                                                                </th>
                                                                                                <th width="5%">Action
                                                                                                </th>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>&nbsp;
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Mouza" runat="server" CssClass="form-control"
                                                                                                        MaxLength="100" ToolTip="Enter Mouza Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="Txt_Mouza"
                                                                                                        FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Khata_No" runat="server" CssClass="form-control" MaxLength="10"
                                                                                                        onkeypress="return FloatOnly(event, this);" ToolTip="Enter Khata No Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="Txt_Khata_No"
                                                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Plot_No" runat="server" CssClass="form-control" MaxLength="10"
                                                                                                        onkeypress="return FloatOnly(event, this);" ToolTip="Enter Plot No Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="Txt_Plot_No"
                                                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Area" runat="server" CssClass="form-control" MaxLength="10"
                                                                                                       ToolTip="Enter Area Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="Txt_Area"
                                                                                                        FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                  <td>
                                                                                                    <asp:TextBox ID="Txt_Present_Kisam" runat="server" CssClass="form-control" MaxLength="10"
                                                                                                         ToolTip="Enter Present Kisam Here !!"></asp:TextBox>
                                                                                                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="Txt_Present_Kisam"
                                                                                                       FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="LnkBtn_Add_Item" CssClass="btn btn-success btn-sm"  runat="server" OnClientClick="return validateItemAdd();" OnClick="LnkBtn_Add_Item_Click"  
                                                                                                        ToolTip="Click Here to Add Items !!"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <asp:GridView ID="Grd_Land" runat="server" CssClass="table table-bordered"
                                                                                            AutoGenerateColumns="false" ShowHeader="false">
                                                                                            <Columns>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Sl_No_Land" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Mouza" runat="server" Text='<%# Eval("vchMouza") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Khata_No" runat="server" Text='<%# Eval("vchKhataNo") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="15%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Plot_No" runat="server" Text='<%# Eval("vchPlotNo") %>'></asp:Label>
                                                                                                       
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="10%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Area" runat="server" Text='<%# Eval("vchArea") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="10%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Prsent_Kisam" runat="server" Text='<%# Eval("vchPresentKisam") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="20%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="ImgBtn_Delete" runat="server" OnClick="ImgBtn_Delete_Click"  ImageUrl="~/Portal/images/deleteIcon.png"
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
                                                                                <asp:Label ID="Label1" runat="server"></asp:Label></h4>


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
                                                            1.Entrepreneurs Identification Number / PC / IEM / Industrial License / Letter on PEAL approval letter
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flEinno" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnEinno_code" runat="server" Value="D324" />
                                                                <asp:HiddenField ID="hdnEinno_name" runat="server" />
                                                                <asp:LinkButton ID="lnkUEinno"  runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDEinno" 
                                                                    runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypEinno" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblEinno" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                   <div class="form-group" id="div6" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                             2. Power of Attorney / Board Resolution / Spciety Resolution as applicable while signing as Partner / Managing Director / Authorized person
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flPoweratt" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnPoweratt_code" runat="server" Value="D325" />
                                                                <asp:HiddenField ID="hdnPoweratt_name" runat="server" />
                                                                <asp:LinkButton ID="lnkUPoweratt"  runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDPoweratt" 
                                                                    runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypPoweratt" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblPoweratt" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                   <div class="form-group" id="div8" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                              3. Certificate of registration under Indian Partnership Act-1932 / Societies Registration Act-1860 /Certificate of incorporation (Memorandum of Association & Article of Association) under Company Act-1956
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flcertofreg" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="certofreg_code" runat="server" Value="D326" />
                                                                <asp:HiddenField ID="certofreg_name" runat="server" />
                                                                <asp:LinkButton ID="lnkUcertofreg"  runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDcertofreg" 
                                                                    runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwcertofreg" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblcertofreg" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                

                                                <div class="form-group" id="div10" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            4.Document in support of date of first investment in fixed capital of industrial unit i.e. land / building / plant & machinery and balancing equipment 
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flfixcapital" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnfixcapital_Code" runat="server" Value="D327"/>
                                                                <asp:HiddenField ID="hdnfixcapital_Name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUfixcapital" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDfixcapital" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwfixcapital" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblfixcapital" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                 <div class="form-group" id="div11" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            5. Provisional Certificate on Priority Sector / Thrust Sector Status
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flThrustcerti" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnThrustcerti_code" runat="server" Value="D328" />
                                                                <asp:HiddenField ID="hdnThrustcerti_name" runat="server" />
                                                                <asp:LinkButton ID="lnkUThrustcerti" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDThrustcerti" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwThrustcerti" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblThrustcerti" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                 <div class="form-group" id="div4" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            6.Approved detailed project report as approved by DSWCA / SLSWCA / HLCA
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flAppprovedproj" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnAppprovedproj_code" runat="server" Value="D329" />
                                                                <asp:HiddenField ID="hdnAppprovedproj_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUAppprovedproj" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDAppprovedproj" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwAppprovedproj" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblAppprovedproj" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div12" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            7.Appraisal & approval in support of expansion / modernization / diversification 
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flAppraisal" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnAppraisal_Code" runat="server" Value="D330" />
                                                                <asp:HiddenField ID="hdnAppraisal_Name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUAppraisal" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDAppraisal" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwAppraisal" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblAppraisal" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>


                                                 <div class="form-group" id="div5" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            8.Land document with land particulars to be converted for industrial use.  
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flLanddocument" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnLanddocument_code" runat="server" Value="D331" />
                                                                <asp:HiddenField ID="hdnLanddocument_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkULanddocument" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDLanddocument" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwLanddocument" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblLanddocument" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                               
   

                                                <div class="form-group" id="div22" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            9. Valid statutory clearances / approvals / permissions for authorities including OSPCB,as applicable
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flstatutory" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnstatutory_code" runat="server" Value="D332" />
                                                                <asp:HiddenField ID="hdnstatutory_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUstatutory" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDstatutory" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwstatutory" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblstatutory" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div21" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            10. Undertaking on non-judicial Stamp Paper duly signed by the applicant in the format-Annexure- B1 
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flStampPaper" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnStampPaper_code" runat="server" Value="D333" />
                                                                <asp:HiddenField ID="hdnStampPaper_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUStampPaper" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                  
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDStampPaper" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwStampPaper" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblStampPaper" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
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
                                                <asp:Button ID="BtnApply" runat="server" style="font-weight:bold;" Text="Submit" OnClick="BtnApply_Click" 
 CssClass="btn btn-success"  
                                                     ToolTip="Click Here to Submit" />
                                                <asp:Button ID="BtnDraft" style="font-weight:bold;" OnClick="BtnDraft_Click" runat="server"  Text="Draft" 
 CssClass="btn btn-warning" 
                                                     ToolTip="Click Here to Draft" />
                                                 <asp:Button ID="BtnCancel" runat="server" style="font-weight:bold;" Text="Cancel" CssClass="btn btn-danger"
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
                                                <h4 style="color: #777;">
                                                    General Instructions (To fill up subsequent incentive applications)</h4>
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
        </ContentTemplate>
       <Triggers>
            <asp:PostBackTrigger ControlID="lnkUEinno" />
            <asp:PostBackTrigger ControlID="lnkUPoweratt" />
            <asp:PostBackTrigger ControlID="lnkUcertofreg" />
            <asp:PostBackTrigger ControlID="lnkUfixcapital" />
            <asp:PostBackTrigger ControlID="lnkUThrustcerti" />
            <asp:PostBackTrigger ControlID="lnkUAppprovedproj" />
            <asp:PostBackTrigger ControlID="lnkUAppraisal" />
            <asp:PostBackTrigger ControlID="lnkULanddocument" />
            <asp:PostBackTrigger ControlID="lnkUstatutory" />
            <asp:PostBackTrigger ControlID="lnkUStampPaper" />
           
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
    <uc3:footer ID="footer" runat="server" />
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
   
    </form>
</body>
</html>
