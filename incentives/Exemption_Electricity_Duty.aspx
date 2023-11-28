<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Exemption_Electricity_Duty.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="incentives_Exemption_Electricity_Duty" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function OnChangeAvailed() {
            $('.availuploadsec,.availdetailsec,.availundertakingsec,.avilrad,.avilrad1').hide();

            if ($("input[name='radNeverAvailedPrior']:checked").val() == '1') {
                $('.availdetailsec').show();
                $('.availuploadsec').show();
                $('.availundertakingsec').hide();
            }
            else {
                $('.availuploadsec').hide();
                $('.availdetailsec').hide();
                $('.availundertakingsec').show();
            }
        }

        function OnChangeApplyBy() {
            $('.attorneysec,.adhardetails').hide();
            if ($("input[name='radApplyBy']:checked").val() == '1') {
                $('.adhardetails').show();
                $('.attorneysec').hide();
            }
            else {
                $('.attorneysec').show();
                $('.adhardetails').hide();
            }
        }
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            }); $('.Pioneersec,.attorneysec,.adhardetails,.exem_details').hide();


            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails,#ElectricityConsumption,#AvailedClaimDetails,#BankDetails,#DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").on("click", function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });




            /*---------------------------------------------------------------*/
            //// Hide Documents if not available

            if ($("#Lbl_Term_Loan_Doc_Name").text() == '') {
                $("#Div_Term_Loan_Doc").hide();
            }
            if ($("#Lbl_Direct_Emp_After_Doc_Name").text() == '') {
                $("#Div_Direct_Emp_Doc_After").hide();
            }
            if ($("#Lbl_Direct_Emp_Before_Doc_Name").text() == '') {
                $("#Div_Direct_Emp_Doc_Before").hide();
            }
            if ($("#Lbl_Pioneer_Doc_Name").text() == '') {
                $("#Div_Pioneer_Doc").hide();
            }

            /*---------------------------------------------------------------*/

            showhideapply();
            chklblName();
            onAvailChange();

            $(".edexem").change(function () {
                if ($(this).find('input').val() == 'rdbdutyyes') {
                    $('.exem_details').show();
                }
                if ($(this).find('input').val() == 'rdbdutyno') {
                    $('.exem_details').hide();
                }
            });

            $(".optradioPriority").change(function () {
                if ($(this).find('input').val() == 'rdbprior') {
                    $('.Pioneersec').show();
                }
                if ($(this).find('input').val() == 'rdbprior1') {
                    $('.Pioneersec').hide();
                }
            });

        });

        function showhideapply() {

            $(".applyby").change(function () {
                if ($(this).find('input').val() == 'rdbapplyby') {
                    $('.adhardetails').show();
                    $('.attorneysec').hide();
                }
                if ($(this).find('input').val() == 'rdbapplyby1') {
                    $('.attorneysec').show();
                    $('.adhardetails').hide();
                }
            });
        }

        function validelectricgrid() {

            if (!blankFieldValidation('txtstatefrmdate', 'From date', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtstatetodate', 'To date', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtstateamt', 'Amount claimed', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtfininst', 'Distribution company', projname)) {
                return false;
            }
            if (new Date($("#txtstatefrmdate").val()) > new Date($("#txtstatetodate").val())) {
                jAlert('<strong>From date can not be greater than to date</strong>', projname);
                $('#txtstatefrmdate').focus();
                return false;
            }
            if (new Date($("#txtstatefrmdate").val()) < new Date($("#hdffinfrm").val())) {
                jAlert('<strong>From Date not coming within financial year</strong>', projname);
                $('#txtstatefrmdate').focus();
                return false;
            }
            if (new Date($("#txtstatefrmdate").val()) > new Date($("#hdffintod").val())) {
                jAlert('<strong>From Date not coming within financial year</strong>', projname);
                $('#txtstatefrmdate').focus();
                return false;
            }
            if ((new Date($("#txtstatetodate").val()) < new Date($("#hdffinfrm").val())) || (new Date($("#txtstatetodate").val()) > new Date($("#hdffintod").val()))) {
                jAlert('<strong>To Date not coming within financial year</strong>', projname);
                $('#txtstatetodate').focus();
                return false;
            }
        }


        function validformindraft() {

            if ($('#TxtAdhaar1').val() != '') {
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('<strong>Aadhaar no should be 12 digits.</strong>', projname);
                    return false;
                }
            }

            if ($("#hdncontext").val().trim() == '1') {
                if ($("#txtconnectedload").val() != '') {
                    if (parseInt($("#txtconnectedload").val()) > 500) {

                        jAlert('<strong> Connected Load / Contract Demand should not be greater than 500 KVA </strong>', projname);
                        $("#txtconnectedload").focus();
                        return false;
                    }
                }
            }

            if ($("#hdncontext").val().trim() == '2') {
                if ($("#txtconnectedload").val() != '') {
                    if (parseInt($("#txtconnectedload").val()) > 5) {
                        jAlert('<strong> Connected Load / Contract Demand should not be greater than 5 MVA </strong>', projname);
                        $("#txtconnectedload").focus();
                        return false;
                    }
                }
            }

            if ($("#hdncontext").val().trim() == '3') {
                if ($("#txtconnectedload").val() != '') {
                    if (parseInt($("#txtconnectedload").val()) > 5000) {
                        jAlert('<strong> Connected Load / Contract Demand should not be greater than 5 MVA </strong>', projname);
                        $("#txtconnectedload").focus();
                        return false;
                    }
                }
            }

            if ($("#txtAccNo").val() != '') {
                var acno = $("#txtAccNo").val();
                if (acno.length > 16) {
                    jAlert('<strong> Account Number should not be more than 16 digit </strong>', projname);
                    $("#acno").focus();
                    return false;
                }
            }

            if ($("#txtifsc").val() != '') {
                var ifsc = $("#txtifsc").val();
                if (ifsc.length < 7) {
                    jAlert('<strong> IFSC code should have minimum 7 characters </strong>', projname);
                    $("#txtifsc").focus();
                    return false;
                }
            }
        }

        /*-----------------------------------------------------------------------------------*/

        function validform() {

            //Common Industrial Part Validation
            //-----------------------------------------------------------------------------------------------------------------------

            if (!DropDownValidation('DdlGender', '0', 'Applicant Salutation', projname)) {
                return false;
            }
            if (!blankFieldValidation('TxtApplicantName', 'Name of Applicant', projname)) {
                return false;
            }

            var rbtnVal = 0;
            if (!$('input[name=radApplyBy]:checked').val()) {
                jAlert('<strong> Please select Application Applying By</strong>', projname);
                return false;
            }
            else {
                rbtnVal = $('input[name=radApplyBy]:checked').val();
                if (rbtnVal == "1") {
                    if ($('#TxtAdhaar1').val() == '') {
                        jAlert('<strong> Please fill correct Aadhar number.</strong>', projname);
                        return false;
                    }
                    var adhar = $('#TxtAdhaar1').val();
                    if (adhar.length < 12) {
                        jAlert('<strong> Aadhaar no should be 12 digits.</strong>', projname);
                        return false;
                    }
                }
                if (rbtnVal == "2") {
                    if (blankFieldValidation('Hid_Auth_Letter_File_Name', 'Please upload Authorizing letter signed by Authorized Signatory !', projname) == false) {
                        return false;
                    }
                }
            }

            //Common Industrial Part Validation
            //-----------------------------------------------------------------------------------------------------------------------

            var txtsupplydate = $("#txtsupplydate");
            if (txtsupplydate != null && txtsupplydate != undefined && txtsupplydate.length != 0) {
                if (!blankFieldValidation('txtsupplydate', 'Date of Power Supply', projname)) {
                    return false;
                }
            }
            if ($("#Hid_DPS_File_Name").val().trim() == '') {
                jAlert('<strong>Please upload the document for DPS </strong>', projname);
                $("#FU_DPS_Doc").focus();
                return false;
            }
            if (!blankFieldValidation('txtconsumenumber', 'Consumer no of the lndustry', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtconnectedload', 'Connected Load / Contract Demand', projname)) {
                return false;
            }
            if ($("#Hid_Connect_Load_File_Name").val().trim() == '') {
                jAlert('<strong>Please upload the document for Connected load </strong>', projname);
                $("#FU_Connect_Load_Doc").focus();
                return false;
            }
            if ($("#<%=grdelectric.ClientID %> tr").length == 0) {
                jAlert('<strong>Please add electricity duty exemption availed details</strong>', projname);
                $('#txtstategovt').focus();
                return false;
            }
            if ($("#Hid_Electricity_Bill_File_Name").val().trim() == '') {
                jAlert('<strong>Please upload the Last month Electricity Bill with payment voucher</strong>', projname);
                $("#FU_Electricity_Bill").focus();
                return false;
            }

            //------------------Added by Sushant Jena-------------
            return AvailValidation();

            ///-------------------------

            if (!blankFieldValidation('txtAccNo', 'Account No', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtbankname', 'Bank Name', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtbranchname', 'Branch Name', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtifsc', 'IFSC Code', projname)) {
                return false;
            }
            if ($("#hdncontext").val().trim() == '1') {
                if (parseInt($("#txtconnectedload").val()) > 500) {
                    jAlert('<strong> Connected Load / Contract Demand should not be greater than 500 KVA </strong>', projname);
                    $("#txtconnectedload").focus();
                    return false;
                }
            }
            if ($("#hdncontext").val().trim() == '2') {
                if (parseInt($("#txtconnectedload").val()) > 5) {
                    jAlert('<strong> Connected Load / Contract Demand should not be greater than 5 MVA </strong>', projname);
                    $("#txtconnectedload").focus();
                    return false;
                }
            }
            if ($("#hdncontext").val().trim() == '3') {
                if (parseInt($("#txtconnectedload").val()) > 5000) {
                    jAlert('<strong> Connected Load / Contract Demand should not be greater than 5 MVA </strong>', projname);
                    $("#txtconnectedload").focus();
                    return false;
                }
            }
            if ($("#txtAccNo").val() != '') {
                var acno = $("#txtAccNo").val();
                if (acno.length > 16) {
                    jAlert('<strong> Account Number should not be more than 16 digit </strong>', projname);
                    $("#acno").focus();
                    return false;
                }
            }
            if ($("#txtifsc").val() != '') {
                var ifsc = $("#txtifsc").val();
                if (ifsc.length < 7) {
                    jAlert('<strong> IFSC code should have minimum 7 characters </strong>', projname);
                    $("#txtifsc").focus();
                    return false;
                }
            }
        }

        /*-----------------------------------------------------------------------------------*/
        /////--Added by Sushant Jena
        function AvailValidation() {

            if (!($('#RadBtn_Availed_Earlier_0').is(':checked') || $('#RadBtn_Availed_Earlier_1').is(':checked'))) {
                jAlert('<strong>Please select Subsidy/Incentive against the details in this application been availed earlier option</strong>', projname);
                return false;
            }
            if ($('#RadBtn_Availed_Earlier_0').is(':checked')) {
                var rowcount = $("#grdAssistanceDetailsAD tr").length;
                if (rowcount == "0") {
                    jAlert('<strong>Please enter atleast one Items of Details of Subsidy Already availed.</strong>', projname);
                    return false;
                }
                if ($("#Hid_Asst_Sanc_File_Name").val() == '') {
                    jAlert('<strong>Please upload Document details of assistance sanctioned.</strong>', projname);
                    return false;
                }
                if (!blankFieldValidation('txtdiffclaimamt', 'Amount of Differential Claim to be Exempted', projname)) {
                    return false;
                }
            }
            if ($('#RadBtn_Availed_Earlier_1').is(':checked')) {
                if ($("#Hid_Undertaking_File_Name").val() == '') {
                    jAlert('<strong>Please upload Document deatils of Undertaking on non-availment of subsidy.</strong>', projname);
                    return false;
                }
            }
            if (!blankFieldValidation('txtreimamt', 'Present Claim for reimbursement', projname)) {
                return false;
            }
        }

        /*-----------------------------------------------------------------------------------*/

        function blnkfileuploadchecking(ctrl) {
            var ids = ctrl.id;

            if ($("#" + ids).val() == '') {
                jAlert('<strong>Please choose the file to upload </strong>', projname);
                return false;
            }
        }

        function chklblName() {

            var arr = ["Micro", "Small", "Medium"];
            var vCat = $("#lbl_Unit_Cat").text();
            var vType = $("#lbl_Unit_Type").text();

            if (vType.trim().substring(0, 3) == 'New') {
                if ((vCat.trim() == 'Large') && ($("#lblIs_Priority").text().trim() == 'Yes')) {
                    $("#lblcontext").text('MVA');
                    $("#hdncontext").val('2');
                }
                if (($.inArray(vCat, arr) != -1) && ($("#lblIs_Priority").text().trim() == 'No')) {
                    $("#lblcontext").text('KVA');
                    $("#hdncontext").val('1');
                }
                if (($.inArray(vCat, arr) != -1) && ($("#lblIs_Priority").text().trim() == 'Yes')) {
                    $("#lblcontext").text('KVA');
                    $("#hdncontext").val('3');
                }
            }
            else {
                $("#lblcontext").text('KVA');
                $("#hdncontext").val('1');
            }
        }

        /*----Availed Details Yes/No Show Hide-----*/
        function onAvailChange() {
            if ($("input[name='RadBtn_Availed_Earlier']:checked").val() == '1') {
                $('.avilrad1').show();
                $('.avilrad').hide();
            }
            else {
                $('.avilrad').show();
                $('.avilrad1').hide();
            }
        }


        /*----------------------------------------------------------------------------*/

        function validAvailgrid() {
            if (!blankFieldValidation('txtagency', 'Disbursing agency', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtsacamt', 'Sanctioned amount', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtsacord', 'Sanction order no.', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtsacdat', 'Date of sanction', projname)) {
                return false;
            }
            var CommDt = $('#txtsacdat').val();
            if (new Date(CommDt) > new Date()) {
                jAlert('<strong>Date of Sanction should not be greater than current date.</strong>', projname);
                $('#txtsacdat').val('');
                $('#txtsacdat').focus();
                return false;
            }
            if (!blankFieldValidation('txtavilamt', 'Availed amount', projname)) {
                return false;
            }
            var sanctionAmt = parseFloat($('#txtsacamt').val());
            var availedAmt = parseFloat($('#txtavilamt').val());
            if (availedAmt > sanctionAmt) {
                jAlert('<strong>Availed amount cannot be greater than sanctioned amount !!</strong>', projname);
                $("#popup_ok").click(function () { $("#txtavilamt").focus(); });
                //$('#txtavilamt').focus();
                //$('#txtavilamt').val('');      
                return false;
            }
        }

        /*----------------------------------------------------------------------------*/
                   
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="registration-div investors-bg">
        <div id="exTab1" class="container">
            <div class="investrs-tab">
                <uc5:pealmenu ID="Peal" runat="server" />
            </div>
            <div class="tab-content clearfix">
                <div class="tab-pane active" id="1a">
                    <div class="form-sec">
                        <div class="innertabs m-b-10">
                            <ul class="nav nav-pills pull-right">
                                <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                <li class="active"><a href="appliedlistwithdetails.aspx">Apply For incentive</a></li>
                                <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                            </ul>
                            <div class="clearfix">
                            </div>
                        </div>
                        <div class="form-header">
                            <div class="iconsdiv">
                                <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                    <i class="fa fa-print"></i></a>
                            </div>
                            <h2>
                                <asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
                        </div>
                        <div class="form-body">
                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingOne">
                                        <h4 class="panel-title">
                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                                aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa  fa-plus">
                                                </i><span class="text-red pull-right " style="margin-right: 20px;">* All fields in this
                                                    section are mandatory</span>Industrial Unit's Details </a>
                                        </h4>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne"
                                        runat="server">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Name of Enterprise/Industrial Unit</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span><asp:Label ID="lbl_EnterPrise_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Organization Type</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lbl_Org_Type" runat="server" CssClass="form-control-static">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Name of Applicant</label>
                                                    <div class="col-sm-1" style="padding-right: 0px">
                                                        <span class="colon">:</span><asp:DropDownList CssClass="form-control" ID="DdlGender"
                                                            runat="server">
                                                            <asp:ListItem Value="1">Mr.</asp:ListItem>
                                                            <asp:ListItem Value="2">Ms.</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="TxtApplicantName" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FteApplicantName" runat="server" TargetControlID="TxtApplicantName"
                                                            FilterMode="ValidChars" FilterType="Custom,UppercaseLetters,LowercaseLetters"
                                                            ValidChars=" .">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Application By</label>
                                                    <div class="col-sm-6 ">
                                                        <span class="colon">:</span>
                                                        <asp:RadioButtonList ID="radApplyBy" class="applyby" runat="server" RepeatDirection="Horizontal"
                                                            onchange="return OnChangeApplyBy();">
                                                            <asp:ListItem Value="1" Selected="True">Self</asp:ListItem>
                                                            <asp:ListItem Value="2">Authorized Person</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group adhardetails" id="divadhhardetails" runat="server">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Aadhaar No.</label>
                                                    <div class="col-sm-6 ">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TxtAdhaar1" CssClass="form-control" placeholder="123412341234" runat="server"
                                                            MaxLength="12"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTxtAdhaar1" runat="server"
                                                            FilterType="Numbers" TargetControlID="TxtAdhaar1">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group attorneysec" id="divAuthorizing" runat="server">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Please provide Authorizing letter signed by Authorized Signatory</label>
                                                    <div class="col-sm-6">
                                                        <asp:Label runat="server" ID="lblAuthorizing"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hidAuthorizing" />
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="FU_Auth_Letter" onchange="return FileCheck(this, 'pdf', 'pdf', 4);"
                                                                runat="server" CssClass="form-control" />
                                                            <asp:HiddenField ID="Hid_Auth_Letter_File_Name" runat="server" />
                                                            <asp:LinkButton ID="LnkBtn_Upload_Auth_Letter" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="LnkBtn_Upload_Auth_Letter_Click" ToolTip="Click here to upload the file."
                                                                OnClientClick="return HasFile('FU_Auth_Letter','Plase Upload Document details of assistance sanctioned');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LnkBtn_Delete_Auth_Letter" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="LnkBtn_Delete_Auth_Letter_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="Hyp_View_Auth_Letter" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf file only and Max file Size 4 MB)</small>
                                                        <asp:Label ID="Lbl_Msg_Auth_Letter" Style="font-size: 12px;" CssClass="text-blue"
                                                            Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Address of Industrial Unit</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lbl_Industry_Address" runat="server" CssClass="form-control-static"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Unit Category</label>
                                                    <div class="col-sm-6 ">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lbl_Unit_Cat" runat="server" CssClass="form-control-static">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Unit Type</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lbl_Unit_Type" runat="server" CssClass="form-control-static">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="Div_Unit_Type_Doc" runat="server">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        <small class="text-gray">
                                                            <asp:Label ID="Lbl_Unit_Type_Doc_Name" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="Hid_Unit_Type_Doc_Code" runat="server" />
                                                        </small>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group">
                                                            <asp:HyperLink ID="Hyp_View_Unit_Type_Doc" runat="server" Target="_blank" class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Address of Registered Office of the Industrial Unit</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <%--<label>
                                                                <input type="checkbox" />Same as Address of Industrial Unit</label>--%>
                                                        <asp:Label ID="lbl_Regd_Office_Address" runat="server" CssClass="form-control-static"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        <asp:Label ID="Lbl_Org_Name_Type" runat="server" Text="Name of Managing Partner"></asp:Label>
                                                    </label>
                                                    <div class="col-sm-6" style="padding-right: 0px">
                                                        <span class="colon">:</span>
                                                        <asp:Label runat="server" ID="lbl_Gender_Partner" CssClass="form-control-static"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        <small class="text-gray">
                                                            <asp:Label ID="Lbl_Org_Doc_Type" runat="server" Text="Document in Support of Managing Partner"></asp:Label>
                                                            <asp:HiddenField ID="Hid_Org_Doc_Type" runat="server" />
                                                        </small>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group">
                                                            <asp:HyperLink ID="Hyp_View_Org_Doc" runat="server" Target="_blank" class="btn btn-info">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        EIN/ IEM/ IL No.</label>
                                                    <div class="col-sm-6 ">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lbl_EIN_IL_NO" runat="server" CssClass="form-control-static"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Date of EIN/ IEM/ IL Date</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <%--<div class="input-group  date datePicker" id="Div2">--%>
                                                        <asp:Label ID="lbl_EIN_IL_Date" runat="server" CssClass="form-control-static"></asp:Label>
                                                        <%-- <span class="input-group-addon"><i class="fa fa-calendar"></i></span>--%>
                                                        <%-- </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="divbefor">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            PC No. Befor E/M/D
                                                        </label>
                                                        <div class="col-sm-6 ">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lbl_pcno_befor" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="Date of Production Commencement- Before E/M/D"
                                                            ID="lblAfterEMD" runat="server"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <%-- <div class="input-group  date datePicker" id="Div13">--%>
                                                            <asp:Label ID="lbl_Prod_Comm_Date_Before" runat="server" CssClass="form-control-static" />
                                                            <%-- <span class="input-group-addon"><i class="fa fa-calendar"></i></span>--%>
                                                            <%-- </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="PC Issurance Date Before E/M/D"
                                                            ID="lblAfterEMD1" runat="server">
                                                        </asp:Label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <%--<div class="input-group  date datePicker" id="Div9">--%>
                                                            <asp:Label ID="lbl_PC_Issue_Date_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                            <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4 col-sm-offset-1">
                                                            <small class="text-gray">
                                                                <asp:Label ID="Lbl_Prod_Comm_Before_Doc_Name" runat="server" Text="Certificate on Date of Commencement of production- Before E/M/D"></asp:Label>
                                                                <asp:HiddenField ID="Hid_Prod_Comm_Before_Doc_Code" runat="server" />
                                                            </small>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:HyperLink ID="Hyp_View_Prod_Comm_Before_Doc" runat="server" Target="_blank"
                                                                    class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="Lbl_Msg_Prod_Comm_Before_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="divafter">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <%--  <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                        PC No.</label>--%>
                                                        <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text=" PC No. After E/M/D"
                                                            ID="lbl_PC_No_After" runat="server"></asp:Label>
                                                        <div class="col-sm-6 ">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lbl_PC_No" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="Date of Production Commencement- After E/M/D"
                                                            ID="lblAfterEMD11" runat="server"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <%-- <div class="input-group  date datePicker" id="Div3">--%>
                                                            <asp:Label ID="lbl_Prod_Comm_Date_After" runat="server" CssClass="form-control-static" />
                                                            <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="PC Issurance Date After E/M/D"
                                                            ID="lblAfterEMD189" runat="server">
                                                        </asp:Label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <%--  <div class="input-group  date datePicker" id="Div9">--%>
                                                            <asp:Label ID="lbl_PC_Issue_Date_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                            <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4 col-sm-offset-1">
                                                            <small class="text-gray">
                                                                <asp:Label ID="Lbl_Prod_Comm_After_Doc_Name" runat="server" Text="Certificate on Date of Commencement of production-After E/M/D"></asp:Label>
                                                            </small>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:HyperLink ID="Hyp_View_Prod_Comm_After_Doc" runat="server" Target="_blank" class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        District</label>
                                                    <div class="col-sm-6 ">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lbl_District" runat="server" CssClass="form-control-static">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        Sector</label>
                                                    <div class="col-sm-6 ">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lbl_Sector" runat="server" CssClass="form-control-static">
                                                        </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        Sub Sector</label>
                                                    <div class="col-sm-6 ">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lbl_Sub_Sector" runat="server" CssClass="form-control-static">
                                                        </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Lies in IPR 2015 Priority Sector</label>
                                                    <div class="col-sm-6 ">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblIs_Priority" runat="server" CssClass="form-control-static"></asp:Label>
                                                        <%--<asp:RadioButtonList ID="Rad_Is_Priority" class="optradioPriority" runat="server"
                                                                            RepeatDirection="Horizontal" onchange="return OnChangePriority();">
                                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="2">No</asp:ListItem>
                                                                        </asp:RadioButtonList>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        Derived Sector</label>
                                                    <div class="col-sm-6 ">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="Lbl_Derived_Sector" runat="server" CssClass="form-control-static"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="Pioneersec">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Is Pioneer</label>
                                                        <div class="col-sm-6 ">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblIs_Is_Pioneer" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="Div_Pioneer_Doc">
                                                    <div class="row">
                                                        <label class="col-sm-4 col-sm-offset-1">
                                                            <small class="text-gray">
                                                                <asp:Label ID="Lbl_Pioneer_Doc_Name" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="Hid_Pioneer_Doc_Code" runat="server" />
                                                            </small>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:HyperLink ID="Hyp_View_Pioneer_Doc" runat="server" Target="_blank" class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        Lies in Sectoral Policy</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label runat="server" ID="lbl_Sectoral" CssClass="form-control-static"></asp:Label>
                                                        <%--<asp:CheckBox ID="ChkBx_Sectoral" runat="server" Enabled="false" />--%>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        GSTIN</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label runat="server" ID="lblGstin" CssClass="form-control-static"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div1">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#ProductionEmploymentDetails" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa  fa-plus"></i>Production & Employment Details </a>
                                        </h4>
                                    </div>
                                    <div id="ProductionEmploymentDetails" class="panel-collapse collapse" role="tabpanel"
                                        aria-labelledby="headingThree">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="panel-body">
                                                    <p class="text-red text-right">
                                                        All Amounts to be Entered in INR( in Lakhs )</p>
                                                    <div runat="server" id="divbefor1">
                                                        <h4>
                                                            <asp:Label runat="server" ID="lblemdBefore" Text="Before E/M/D" CssClass="h2-hdr"></asp:Label>
                                                        </h4>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12">
                                                                    Items of Manufacture/Activity
                                                                </label>
                                                                <div class="col-sm-12  margin-bottom10">
                                                                    <asp:GridView ID="Grd_Production_Before" runat="server" CssClass="table table-bordered"
                                                                        DataKeyNames="vchProductName" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Sl_No_Product_Before" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Product/Service Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Product_Name_Before" runat="server" Text='<%# Eval("vchProductName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="35%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Quantity">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Quantity_Before" runat="server" Text='<%# Eval("intQuantity") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Unit_Before" runat="server" Text='<%# Eval("MeasunitName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Other Units">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Other_Unit_Before" runat="server" Text='<%# Eval("vchotherunit") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Value">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Value_Before" runat="server" Text='<%# Eval("decValue") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-4">
                                                                    Direct Employment in Numbers<small>(on Company Payroll)</small>
                                                                </label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Direct_Emp_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-4">
                                                                    Contractual Employment in Numbers</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Contract_Emp_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="Div_Direct_Emp_Doc_Before">
                                                            <div class="row">
                                                                <label class="col-sm-6">
                                                                    <small class="text-gray">
                                                                        <asp:Label ID="Lbl_Direct_Emp_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                        <asp:HiddenField ID="Hid_Direct_Emp_Before_Doc_Code" runat="server" />
                                                                    </small>
                                                                </label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <div class="input-group">
                                                                        <asp:HyperLink ID="Hyp_View_Direct_Emp_Before_Doc" runat="server" Target="_blank"
                                                                            class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <table class="table table-bordered" id="tblEmployement" runat="server">
                                                                    <tr>
                                                                        <td style="width: 20%;">
                                                                            Managerial
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                            <asp:Label ID="lbl_Managarial_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            General
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                            <asp:Label ID="lbl_General_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Supervisor
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Supervisor_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            SC
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SC_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Skilled
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Skilled_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            ST
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_ST_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Semi Skilled
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Semi_Skilled_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            Total
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Total_Cast_Emp_Before" ReadOnly="true" runat="server" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Un Skilled
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Unskilled_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            Women
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Women_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Total
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Total_Emp_Before" ReadOnly="true" runat="server" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            Differently Abled Persons
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_PHD_Before" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div runat="server" id="divafter1">
                                                        <h4>
                                                            <asp:Label runat="server" ID="lblemd" Text="After E/M/D" CssClass="h2-hdr"></asp:Label>
                                                        </h4>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12">
                                                                    Items of Manufacture/Activity
                                                                </label>
                                                                <div class="col-sm-12  margin-bottom10">
                                                                    <asp:GridView ID="Grd_Production_After" runat="server" CssClass="table table-bordered"
                                                                        DataKeyNames="vchProductName" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Sl_No_Product_After" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Product/Service Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Product_Name_After" runat="server" Text='<%# Eval("vchProductName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="35%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Quantity">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Quantity_After" runat="server" Text='<%# Eval("intQuantity") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Unit_After" runat="server" Text='<%# Eval("MeasunitName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Other Units">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_other_Unit_After" runat="server" Text='<%# Eval("vchotherunit") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Value">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Value_After" runat="server" Text='<%# Eval("decValue") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="20%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-4">
                                                                    Direct Employment in Numbers<small>(on Company Payroll)</small>
                                                                </label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Direct_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-4">
                                                                    Contractual Employment in Numbers</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Contract_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="Div_Direct_Emp_Doc_After">
                                                            <div class="row">
                                                                <label class="col-sm-6">
                                                                    <small class="text-gray">
                                                                        <asp:Label ID="Lbl_Direct_Emp_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                        <asp:HiddenField ID="Hid_Direct_Emp_After_Doc_Code" runat="server" />
                                                                    </small>
                                                                </label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <div class="input-group">
                                                                        <asp:HyperLink ID="Hyp_View_Direct_Emp_After_Doc" runat="server" Target="_blank"
                                                                            class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <table class="table table-bordered" id="Table1" runat="server">
                                                                    <tr>
                                                                        <td style="width: 20%;">
                                                                            Managerial
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                            <asp:Label ID="lbl_Managarial_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            General
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                            <asp:Label ID="lbl_General_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Supervisor
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Supervisor_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            SC
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SC_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Skilled
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Skilled_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            ST
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_ST_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Semi Skilled
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Semi_Skilled_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            Total
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Total_Cast_Emp_After" ReadOnly="true" runat="server" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Un Skilled
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Unskilled_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            Women
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Women_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Total
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Total_Emp_After" ReadOnly="true" runat="server" Text="0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            Differently Abled Persons
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_PHD_After" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                                                MaxLength="4" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingThree">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#IndustryDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>Investment Details </a>
                                        </h4>
                                    </div>
                                    <div id="IndustryDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div runat="server" id="divbefor2">
                                                <h3>
                                                    <asp:Label class="h2-hdr" runat="server" ID="Before"> Before E/M/D</asp:Label></h3>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Date of First Fixed Capital Investment (In Land/Building/Plant and Machinery & Balancing
                                                            Equipment)
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Txt_FFCI_Date_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4 col-sm-offset-1">
                                                            <small class="text-gray">
                                                                <asp:Label ID="Lbl_FFCI_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="Hid_FFCI_Before_Doc_Code" runat="server" />
                                                            </small>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:HyperLink ID="Hyp_View_FFCI_Before_Doc" runat="server" Target="_blank" class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12 ">
                                                            Total Capital Investment</label>
                                                        <div class="col-sm-12">
                                                            <table class="table table-bordered">
                                                                <tbody>
                                                                    <tr>
                                                                        <th>
                                                                            Slno.
                                                                        </th>
                                                                        <th>
                                                                            Investment Head
                                                                        </th>
                                                                        <th>
                                                                            Investment Amount
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            1
                                                                        </td>
                                                                        <td>
                                                                            Land Including Land Development
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:Label ID="lbl_Land_Before" runat="server" onblur="calculetotal()"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            2
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblBuilding" Text="Building"></asp:Label>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:Label ID="lbl_Building_Before" runat="server" onblur="calculetotal()"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            3
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblPlantMachinery" Text="Plant & Machinery"></asp:Label>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:Label ID="lbl_Plant_Mach_Before" runat="server" onblur="calculetotal()"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            4
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Other Fixed Assets</label>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:Label ID="lbl_Other_Fixed_Asset_Before" runat="server" onblur="calculetotal()"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <strong>Total</strong>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <strong>
                                                                                <asp:Label ID="lbl_Total_Capital_Before" runat="server"></asp:Label>
                                                                            </strong>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            <small class="text-gray">
                                                                <asp:Label ID="Lbl_Approved_DPR_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="Hid_Approved_DPR_Before_Doc_Code" runat="server" />
                                                            </small>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <%--asp:FileUpload ID="FU_Approved_DPR_Before" CssClass="form-control" runat="server" />
                                                                            <asp:HiddenField ID="Hid_Approved_DPR_Before_File_Name" runat="server" />
                                                                            <asp:LinkButton ID="LnkBtn_Upload_Approved_DPR_Before_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                 ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LnkBtn_Delete_Approved_DPR_Before_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                 Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>--%>
                                                                <asp:HyperLink ID="Hyp_View_Approved_DPR_Before_Doc" runat="server" Target="_blank"
                                                                    class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <%--<asp:Label ID="Lbl_Msg_Approved_DPR_Before_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                            Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="divAfter2">
                                                <h3>
                                                    <asp:Label runat="server" Text="After E/M/D" ID="lblEMDInvestment" CssClass="h2-hdr"></asp:Label>
                                                </h3>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Date of First Fixed Capital Investment (In Land/Building/Plant and Machinery & Balancing
                                                            Equipment)
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <%--<div class="input-group  date datePicker" id="Div7">--%>
                                                            <asp:Label ID="lbl_FFCI_Date_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                            <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-5">
                                                            <small class="text-gray">
                                                                <asp:Label ID="Lbl_FFCI_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="Hid_FFCI_After_Doc_Code" runat="server" />
                                                            </small>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <%-- <asp:FileUpload ID="FU_FFCI_After" CssClass="form-control" runat="server" />
                                                                            <asp:HiddenField ID="Hid_FFCI_After_File_Name" runat="server" />
                                                                            <asp:LinkButton ID="LnkBtn_Upload_FFCI_After_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                 ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LnkBtn_Delete_FFCI_After_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                 Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>--%>
                                                                <asp:HyperLink ID="Hyp_View_FFCI_After_Doc" runat="server" Target="_blank" title="Click to View"
                                                                    class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <%-- <asp:Label ID="Lbl_Msg_FFCI_After_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                            Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12 ">
                                                            Total Capital Investment</label>
                                                        <div class="col-sm-12">
                                                            <table class="table table-bordered">
                                                                <tbody>
                                                                    <tr>
                                                                        <th>
                                                                            Slno.
                                                                        </th>
                                                                        <th>
                                                                            Investment Head
                                                                        </th>
                                                                        <th>
                                                                            Investment Amount
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            1
                                                                        </td>
                                                                        <td>
                                                                            Land Including Land Development
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:Label ID="lbl_Land_After" runat="server" onblur="calculetotal()"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            2
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="Label1" Text="Building"></asp:Label>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:Label ID="lbl_Building_After" runat="server" onblur="calculetotal()"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            3
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="Label2" Text="Plant & Machinery"></asp:Label>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:Label ID="lbl_Plant_Mach_After" runat="server" onblur="calculetotal()"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            4
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="Label3" Text="Other Fixed Assests"></asp:Label>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:Label ID="lbl_Other_Fixed_Asset_After" runat="server" onblur="calculetotal()"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <strong>Total</strong>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <strong>
                                                                                <asp:Label ID="lbl_Total_Capital_After" runat="server"></asp:Label>
                                                                            </strong>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            <small class="text-gray">
                                                                <asp:Label ID="Lbl_Approved_DPR_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="Hid_Approved_DPR_After_Doc_Code" runat="server" />
                                                            </small>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:HyperLink ID="Hyp_View_Approved_DPR_After_Doc" runat="server" Target="_blank"
                                                                    title="Click to View" class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <h4 class="h4-header">
                                                Means Of Finance
                                            </h4>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Equity
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lbl_Equity_Amt" runat="server" CssClass="form-control-static"></asp:Label>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Loan From Bank/FI</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lbl_Loan_Bank_FI" runat="server" CssClass="form-control-static"></asp:Label>
                                                        <br />
                                                        <small class="text-gray lablespan">Total Amount (Excluding Loan for Working Capital)</small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        <strong>Term Loan Details</strong></label>
                                                    <div class="col-sm-12">
                                                        <asp:GridView ID="Grd_TL" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Slno.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_TL_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="4%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name of Financial Institution">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_TL_Financial_Inst" runat="server" Text='<%# Eval("vchNameOfFinancialInst") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="State">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_TL_State" runat="server" Text='<%# Eval("vchState") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="12%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="City">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_TL_City" runat="server" Text='<%# Eval("vchCity") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="13%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Term Loan Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_TL_Amount" runat="server" Text='<%# Eval("decLoanAmt") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="13%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sanction Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_TL_Sanction_Date" runat="server" Text='<%# Eval("dtmSanctionDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="9%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Availed Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_TL_Avail_Amt" runat="server" Text='<%# Eval("decAvailedAmt") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Availed Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_TL_Avail_Date" runat="server" Text='<%# Eval("dtmAvailedDate" , "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="8%"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        <strong>Working Capital Loan Details</strong></label>
                                                    <div class="col-sm-12">
                                                        <asp:GridView ID="Grd_WC" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Slno.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_WC_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="4%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name of Financial Institution">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_WC_Financial_Inst" runat="server" Text='<%# Eval("vchNameOfFinancialInst") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="State">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_WC_State" runat="server" Text='<%# Eval("vchState") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="12%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="City">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_WC_City" runat="server" Text='<%# Eval("vchCity") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="13%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Loan Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_WC_Amount" runat="server" Text='<%# Eval("decLoanAmt") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="13%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sanction Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_WC_Sanction_Date" runat="server" Text='<%# Eval("dtmSanctionDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="9%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Availed Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_WC_Avail_Amt" runat="server" Text='<%# Eval("decAvailedAmt") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Availed Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_WC_Avail_Date" runat="server" Text='<%# Eval("dtmAvailedDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="8%"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="Div_Term_Loan_Doc">
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        <small class="text-gray">
                                                            <asp:Label ID="Lbl_Term_Loan_Doc_Name" runat="server" Text=""></asp:Label>
                                                            <asp:HiddenField ID="Hid_Term_Loan_Doc_Code" runat="server" />
                                                        </small>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group">
                                                            <asp:HyperLink ID="Hyp_View_Term_Loan_Doc" runat="server" Target="_blank" title="Click to View"
                                                                class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 ">
                                                        FDI (If Any)
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lbl_FDI_Componet" runat="server" CssClass="form-control-static"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div11">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#ElectricityConsumption" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa  fa-minus"></i>Electricity Consumption/Load Details (<b><asp:Label
                                                    ID="lblfinyear" runat="server"></asp:Label></b>) </a>
                                        </h4>
                                    </div>
                                    <div id="ElectricityConsumption" class="panel-collapse collapse in" role="tabpanel"
                                        aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                        Date of Power Supply for Production(DPS)</label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div4" runat="server">
                                                            <asp:TextBox ID="txtsupplydate" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                        <div id="Div8" runat="server">
                                                            <asp:Label ID="lblsupplydate" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <label for="Iname" class="col-sm-2">
                                                        Document for DPS <a data-toggle="tooltip" class="fieldinfo2" style="padding-left: 10px"
                                                            title="Date of Power Supply Document required"><i class="fa fa-question-circle" aria-hidden="true">
                                                            </i></a>
                                                    </label>
                                                    <asp:HiddenField ID="hdnsopdocid" runat="server" Value="D267" />
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FU_DPS_Doc" runat="server" CssClass="form-control" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                            <asp:LinkButton ID="LnkBtn_Upload_DPS_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClientClick="return blnkfileuploadchecking(FU_DPS_Doc);" OnClick="LnkBtn_Upload_DPS_Doc_Click"
                                                                ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LnkBtn_Delete_DPS_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                ToolTip="Delete" OnClick="LnkBtn_Delete_DPS_Doc_Click" Visible="false"><i class="fa fa-trash-o"  aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="Hyp_View_DPS_Doc" runat="server" CssClass="input-group-addon bg-blue"
                                                                data-toggle="tooltip" ToolTip="View" OnClientClick="JavaScript: return false;"
                                                                Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                        <asp:HiddenField ID="Hid_DPS_File_Name" runat="server" />
                                                        <asp:Label ID="Lbl_Msg_DPS_Doc" Style="font-size: 12px;" CssClass="text-blue" runat="server"
                                                            Text="Document uploaded successfully" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                        Consumer no of the lndustry</label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtconsumenumber" MaxLength="20" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="flt1" runat="server" TargetControlID="txtconsumenumber"
                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                            ValidChars=",,-,/\, ,.">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                        Connected Load / Contract Demand (In
                                                        <asp:Label ID="lblcontext" runat="server"></asp:Label>) <a data-toggle="tooltip"
                                                            class="fieldinfo2" style="padding-left: 10px" title="The Electrical load connected for the industrial area Contract Demand-is the KVA that consumed by that industrial unit
                                                            "><i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtconnectedload" CssClass="form-control" MaxLength="4" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtconnectedload"
                                                            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789.">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:HiddenField ID="hdncontext" runat="server" />
                                                    </div>
                                                    <label for="Iname" class="col-sm-2">
                                                        Document for Connected Load</label>
                                                    <asp:HiddenField ID="hdncondocid" runat="server" Value="D268" />
                                                    <div class="col-sm-4">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="FU_Connect_Load_Doc" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                            <asp:LinkButton ID="LnkBtn_Upload_Connect_Load_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClientClick="return blnkfileuploadchecking(FU_Connect_Load_Doc);" OnClick="LnkBtn_Upload_Connect_Load_Doc_Click"
                                                                ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LnkBtn_Delete_Connect_Load_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                ToolTip="Delete" OnClick="LnkBtn_Delete_Connect_Load_Doc_Click" Visible="false"><i class="fa fa-trash-o"  aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="Hyp_View_Connect_Load" runat="server" CssClass="input-group-addon bg-blue"
                                                                data-toggle="tooltip" ToolTip="View" OnClientClick="JavaScript: return false;"
                                                                Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                        <asp:HiddenField ID="Hid_Connect_Load_File_Name" runat="server" />
                                                        <asp:Label ID="Lbl_Msg_Connect_Load_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                            runat="server" Text="Document uploaded successfully" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 ">
                                                    </label>
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th width="15%">
                                                                    From Date
                                                                </th>
                                                                <th width="15%">
                                                                    To Date
                                                                </th>
                                                                <th width="25%">
                                                                    Amount Claimed(in Lakhs)
                                                                </th>
                                                                <th>
                                                                    Distribution Company
                                                                </th>
                                                                <th width="7%">
                                                                    Add More
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div3">
                                                                        <asp:TextBox ID="txtstatefrmdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div12">
                                                                        <asp:TextBox ID="txtstatetodate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtstateamt" MaxLength="10" runat="server" onkeypress="return  FloatOnly(event, this);"
                                                                        CssClass="form-control"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="txtstateamt"
                                                                        FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtfininst" runat="server" MaxLength="30" CssClass="form-control"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtfininst"
                                                                        FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                        ValidChars=",,-,/\, ,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton20" CssClass="btn btn-success btn-sm" runat="server"
                                                                        OnClientClick="return validelectricgrid();" OnClick="LinkButton20_Click"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:GridView ID="grdelectric" runat="server" ShowHeader="true" CssClass="table table-bordered"
                                                            AutoGenerateColumns="false" Width="100%" OnRowDeleting="grdelectric_RowDeleting"
                                                            OnRowDataBound="grdelectric_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="dcstategovt" HeaderText="Extend of Electricity Duty" Visible="false" />
                                                                <asp:BoundField DataField="dcfrmdate" HeaderText="From Date" ItemStyle-Width="15%" />
                                                                <asp:BoundField DataField="dctodate" HeaderText="To Date" ItemStyle-Width="15%" />
                                                                <asp:BoundField DataField="dcamtclaim" HeaderText="Amount Claimed" ItemStyle-Width="25%" />
                                                                <asp:BoundField DataField="dcmfininst" HeaderText="Distribution Company" />
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkdel" runat="server" Text="Delete" CommandName="delete"></asp:LinkButton>
                                                                        <asp:HiddenField ID="elechdfrowid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"dcRowId") %>' />
                                                                        <asp:HiddenField ID="hdnelecrowdb" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"vchrowdb") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="7%"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4">
                                                        Last month electricity bill with payment voucher</label>
                                                    <asp:HiddenField ID="hdnpmtvoucher" Value="D149" runat="server" />
                                                    <div class="col-sm-6">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="FU_Electricity_Bill" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                            <asp:LinkButton ID="LnkBtn_Upload_Electricity_Bill" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClientClick="return blnkfileuploadchecking(FU_Electricity_Bill);" OnClick="LnkBtn_Upload_Electricity_Bill_Click"
                                                                ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LnkBtn_Delete_Electricity_Bill" runat="server" CssClass="input-group-addon bg-red"
                                                                ToolTip="Delete" OnClick="LnkBtn_Delete_Electricity_Bill_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="Hyp_View_Electricity_Bill_Doc" runat="server" CssClass="input-group-addon bg-blue"
                                                                data-toggle="tooltip" title="Upload" ToolTip="View" OnClientClick="JavaScript: return false;"
                                                                Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                        <asp:HiddenField ID="Hid_Electricity_Bill_File_Name" runat="server" />
                                                        <asp:Label ID="Lbl_Msg_Electricity_Bill_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                            runat="server" Text="Document uploaded successfully" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div2">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#AvailedClaimDetails" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa  fa-plus"></i>Details Of Incentives Availed Earlier</a>
                                        </h4>
                                    </div>
                                    <div id="AvailedClaimDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-5">
                                                        Present Claim for Reimbursement
                                                    </label>
                                                    <div class="col-sm-5">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtreimamt" MaxLength="10" runat="server" onkeypress="return  FloatOnly(event, this);"
                                                            CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtreimamt"
                                                            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-5">
                                                        Has Subsidy/Incentive against the details in this application been availed earlier
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:RadioButtonList ID="RadBtn_Availed_Earlier" runat="server" onchange="onAvailChange();"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="avilrad" id="divAvailNo" runat="server" visible="true">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5 ">
                                                            Undertaking on non-availment of subsidy earlier on this project
                                                            <asp:HiddenField ID="hdnundertakingdocid" runat="server" Value="D230" />
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FU_Undertaking_Doc" CssClass="form-control" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);"
                                                                    runat="server" />
                                                                <asp:LinkButton ID="LnkBtn_Upload_Undertaking_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return blnkfileuploadchecking(Fupunderdoc);" OnClick="LnkBtn_Upload_Undertaking_Doc_Click"
                                                                    ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LnkBtn_Delete_Undertaking_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="LnkBtn_Delete_Undertaking_Doc_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o"  aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="Hyp_View_Undertaking_Doc" CssClass="input-group-addon bg-blue"
                                                                    data-toggle="tooltip" title="View" runat="server" OnClientClick="JavaScript: return false;"
                                                                    Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="Hid_Undertaking_File_Name" runat="server" />
                                                            <asp:Label ID="Lbl_Msg_Undertaking_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                runat="server" Text="Document uploaded successfully" Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="avilrad1" id="divAvailYes" runat="server">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12">
                                                            Details of Incentive already availed
                                                        </label>
                                                        <div class="col-sm-12  margin-bottom10">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th width="5%">
                                                                        Slno.
                                                                    </th>
                                                                    <th>
                                                                        Disbursing Agency
                                                                    </th>
                                                                    <th width="15%">
                                                                        Sanctioned Amount
                                                                    </th>
                                                                    <th width="15%">
                                                                        Sanction Order No.
                                                                    </th>
                                                                    <th width="15%">
                                                                        Date of Sanction
                                                                    </th>
                                                                    <th width="15%">
                                                                        Availed Amount
                                                                    </th>
                                                                    <th width="7%">
                                                                        Add More
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        -
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtagency" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtagency"
                                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                            ValidChars=",,-,/\, ,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtsacamt" MaxLength="10" runat="server" onkeypress="return FloatOnly(event, this);"
                                                                            CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtsacamt"
                                                                            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtsacord" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtsacord"
                                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                            ValidChars=",,-,/\, ,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker" id="Div14">
                                                                            <asp:TextBox ID="txtsacdat" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtavilamt" MaxLength="10" runat="server" onkeypress="return FloatOnly(event, this);"
                                                                            CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtavilamt"
                                                                            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="LinkButton41" CssClass="btn btn-success btn-sm" runat="server"
                                                                            OnClientClick="return validAvailgrid();" OnClick="LinkButton41_Click"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:GridView ID="grdAssistanceDetailsAD" runat="server" CssClass="table table-bordered"
                                                                AutoGenerateColumns="false" ShowHeader="false" OnRowDeleting="grdAssistanceDetailsAD_RowDeleting">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblslno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="5%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Disbursing Agency">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblBody" Text='<%# Eval("vchagency") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sanctioned Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblName" Text='<%# Eval("vchsacamt") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sanction Order No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblAmountAvailed" Text='<%# Eval("vchsacord") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date of Sanction">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblAvailedDate" Text='<%# Eval("vchsacdat") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Availed Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblSanctionOrderNo" Text='<%# Eval("vchavilamt") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDelRecord" CommandName="delete" CssClass="btn btn-danger btn-sm"
                                                                                runat="server" ToolTip="Remove"><i class="fa fa-trash"></i>
                                                                            </asp:LinkButton>
                                                                            <asp:HiddenField ID="hdnRowId" runat="server" Value='<%#Eval("dcRowId")%>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="7%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Document details of assistance sanctioned</label>
                                                        <asp:HiddenField ID="hdndetailsassistantdocid" runat="server" Value="D253" />
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FU_Asst_Sanc_Doc" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                <asp:LinkButton ID="LnkBtn_Upload_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return blnkfileuploadchecking(FU_Asst_Sanc_Doc);" OnClick="LnkBtn_Upload_Asst_Sanc_Doc_Click"
                                                                    ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LnkBtn_Delete_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                    ToolTip="Delete" Visible="false" OnClick="LnkBtn_Delete_Asst_Sanc_Doc_Click"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="Hyp_View_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-blue"
                                                                    data-toggle="tooltip" title="View" ToolTip="View" OnClientClick="JavaScript: return false;"
                                                                    Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="Hid_Asst_Sanc_File_Name" runat="server" />
                                                            <asp:Label ID="Lbl_Msg_Asst_Sanc_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                runat="server" Text="Document uploaded successfully" Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Amount of Differential Claim to be Exempted (in Lakhs)
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtdiffclaimamt" MaxLength="10" runat="server" onkeypress="return  FloatOnly(event, this);"
                                                                CssClass="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtdiffclaimamt"
                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div8">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#BankDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>Bank Details</a>
                                        </h4>
                                    </div>
                                    <div id="BankDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <p class="text-red text-right">
                                                <small style="font-size: 12px;">Please provide details of the account of the bank where
                                                    term loan is availed ,if availed Else, provide account details of any other bank
                                                    account associated with your industrial unit</small></p>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-2">
                                                        Account No of Industrial Unit <span class="text-red">*</span></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtAccNo" runat="server" MaxLength="16" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtAccNo"
                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                            ValidChars=",,-,/\, ,.">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <label for="Iname" class="col-sm-2">
                                                        Bank Name <span class="text-red">*</span></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtbankname" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtbankname"
                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                            ValidChars=",,-,/\, ,.">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-2 ">
                                                        Branch Name <span class="text-red">*</span></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtbranchname" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtbranchname"
                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                            ValidChars=",,-,/\, ,.">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <label for="Iname" class="col-sm-2 ">
                                                        IFSC Code<span class="text-red">*</span></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtifsc" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtifsc"
                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                            ValidChars=",,-,/\, ,.">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-2 ">
                                                        MICR No.</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtmicr" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtmicr"
                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                            ValidChars=",,-,/\, ,.">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <div for="Iname" class="col-sm-2">
                                                        Upload cancelled cheque to verify the entered A/c details <a data-toggle="tooltip"
                                                            class="fieldinfo2" title="Upload cancelled cheque to verify the Entered A/c details">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        <asp:HiddenField ID="hdnbnkdocid" runat="server" Value="D266" />
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="FU_Cancelled_Cheque" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                            <asp:LinkButton ID="LnkBtn_Upload_Cancelled_Cheque" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClientClick="return blnkfileuploadchecking(FU_Cancelled_Cheque);" OnClick="LnkBtn_Upload_Cancelled_Cheque_Click"
                                                                ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LnkBtn_Delete_Cancelled_Cheque" runat="server" CssClass="input-group-addon bg-red"
                                                                ToolTip="Delete" OnClick="LnkBtn_Delete_Cancelled_Cheque_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="Hyp_View_Cancelled_Cheque" runat="server" CssClass="input-group-addon bg-blue"
                                                                data-toggle="tooltip" title="Upload" ToolTip="View" OnClientClick="JavaScript: return false;"
                                                                Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                        <asp:HiddenField ID="Hid_Cancelled_Cheque_File_Name" runat="server" />
                                                        <asp:Label ID="Lbl_Msg_Cancelled_Cheque" Style="font-size: 12px;" CssClass="text-blue"
                                                            runat="server" Text="Document uploaded successfully" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default" id="DivCheckList" runat="server">
                                    <div class="panel-heading" role="tab">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#DivDocCheck" aria-expanded="false" aria-controls="collapseThree"><i id="i1"
                                                    class="more-less fa  fa-plus"></i>Document Checklist</a>
                                        </h4>
                                    </div>
                                    <div id="DivDocCheck" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <table class="table table-bordered">
                                                <tr>
                                                    <th>
                                                        Document Name
                                                    </th>
                                                    <th width="150px">
                                                        Uploaded Status
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Authorizing letter signed by Authorized Signatory
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="ImgSign" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr id="trunitdoc" runat="server">
                                                    <td id="tdunitDoc" runat="server">
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="Imgunitdoc" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr id="trorgdoc" runat="server">
                                                    <td id="tdorgdoc" runat="server">
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="Imgorgdoc" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr id="trcommafterdoc" runat="server">
                                                    <td id="tdcommafterdoc" runat="server">
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="Imgcommafterdoc" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr id="trpioneerdoc" runat="server">
                                                    <td id="tdpioneerdoc" runat="server">
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="Imgpioneerdoc" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr id="trempafterdoc" runat="server">
                                                    <td id="tdempafterdoc" runat="server">
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="Imgempafterdoc" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr id="trFFCIafterdoc" runat="server">
                                                    <td id="tdFFCIafterdoc" runat="server">
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="ImgFFCIafterdoc" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr id="trDPRafterdoc" runat="server">
                                                    <td id="tdDPRafterdoc" runat="server">
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="ImgDPRafterdoc" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr id="trtermloandoc" runat="server">
                                                    <td id="tdtermloandoc" runat="server">
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="Imgtermloandoc" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Document for DPS
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="ImgDPS" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Document for Connected Load
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="ImgLoad" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Last month Electricity Bill with payment voucher
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="imgLastMonthBill" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Document details of assistance sanctioned
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="Imgsanction" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Undertaking on non-availment of subsidy earlier on this project
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="ImgUndertaking" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Cancelled cheque to verify the Entered A/c details
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="Imgcancell" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-footer">
                    <div class="row">
                        <div class="col-sm-12 text-right">
                            <asp:HiddenField ID="hdffinfrm" runat="server" />
                            <asp:HiddenField ID="hdffintod" runat="server" />
                            <asp:Button ID="btnDraft" runat="server" Text="Save As Draft" OnClientClick="return validformindraft();"
                                CssClass="btn btn-warning" OnClick="btnDraft_Click" />
                            <asp:Button ID="btnsave" runat="server" OnClientClick="return validform();" Text="Apply"
                                OnClick="btnsave_Click" CssClass="btn btn-success" />
                            <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function pageLoad() {
            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy"
                });
            });
            OnChangeApplyBy();
            chklblName();
            onAvailChange();
            DocCheckList();
            trdisplay();

            $(".edexem").change(function () {
                if ($(this).find('input').val() == 'rdbdutyyes') {
                    $('.exem_details').show();
                }
                if ($(this).find('input').val() == 'rdbdutyno') {
                    $('.exem_details').hide();
                }
            });

        }
    </script>
    <script language="javascript" type="text/javascript">

        function ImgSrc(hdnfld, Img) {
            if (hdnfld != "") {
                $('#' + Img).attr('src', '../images/incapproved.png');
            }
            else {
                $('#' + Img).attr('src', '../images/cancel-square.png');
            }
        }
        function ImgSrcHyp(Img, HypCtrl, trid) {
            if ($('#' + HypCtrl).length) {
                var hrf = $('#' + HypCtrl).attr('href');
                if (hrf != "") {
                    $('#' + Img).attr('src', '../images/incapproved.png');
                }
                else {
                    $('#' + Img).attr('src', '../images/cancel-square.png');
                }
            }
            else {
                //                alert(trid);
                $('#' + trid).css("display", "none");
            }
        }


        function DocCheckList() {
            ImgSrc($('#Hid_Auth_Letter_File_Name').val(), $('#ImgSign').attr("id"));

            ImgSrcHyp($('#Imgorgdoc').attr("id"), 'Hyp_View_Org_Doc', 'trCertInCorp');
            ImgSrcHyp($('#Imgunitdoc').attr("id"), 'Hyp_View_Unit_Type_Doc', 'trUnit_Type_Doc_Name');

            ImgSrcHyp($('#ImgProd_Comm_After_Doc_Name').attr("id"), 'Hyp_View_Prod_Comm_After_Doc', 'trProd_Comm_After_Doc_Name');
            ImgSrcHyp($('#ImgPioneer_Doc_Name').attr("id"), 'Hyp_View_Pioneer_Doc', 'trPioneer_Doc_Name');
            ////----- Production & Emp 
            ImgSrcHyp($('#ImgDirect_Emp_After_Doc_Name').attr("id"), 'Hyp_View_Direct_Emp_After_Doc', 'trDirect_Emp_After_Doc_Name');
            ////----- Investement Details
            ImgSrcHyp($('#ImgFFCI_After_Doc_Name').attr("id"), 'Hyp_View_FFCI_After_Doc', 'trFFCI_After_Doc_Name');
            ImgSrcHyp($('#ImgApproved_DPR_After_Doc_Name').attr("id"), 'Hyp_View_Approved_DPR_After_Doc', 'trApproved_DPR_After_Doc_Name');
            ImgSrcHyp($('#ImgTerm_Loan_Doc_Name').attr("id"), 'Hyp_View_Term_Loan_Doc', 'trTerm_Loan_Doc_Name');

            ImgSrc($('#Hid_DPS_File_Name').val(), $('#ImgDPS').attr("id"));
            ImgSrc($('#Hid_Connect_Load_File_Name').val(), $('#ImgLoad').attr("id"));
            ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#Imgsanction').attr("id"));
            ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#ImgUndertaking').attr("id"));
            ImgSrc($('#Hid_Cancelled_Cheque_File_Name').val(), $('#Imgcancell').attr("id"));
            ImgSrc($('#Hid_Electricity_Bill_File_Name').val(), $('#imgLastMonthBill').attr("id"));
        }

        function trdisplay() {
            if ($('#tdunitDoc').val() == '') { $('#trunitdoc').hide(); }
            if ($('#tdorgdoc').val() == '') { $('#trorgdoc').hide(); }
            if ($('#tdcommafterdoc').val() == '') { $('#trcommafterdoc').hide(); }
            if ($('#tdpioneerdoc').val() == '') { $('#trpioneerdoc').hide(); }
            if ($('#tdempafterdoc').val() == '') { $('#trempafterdoc').hide(); }
            if ($('#tdFFCIafterdoc').val() == '') { $('#trFFCIafterdoc').hide(); }
            if ($('#tdDPRafterdoc').val() == '') { $('#trDPRafterdoc').hide(); }
            if ($('#tdtermloandoc').val() == '') { $('#trtermloandoc').hide(); }
        }
       
    </script>
    </form>
</body>
</html>
