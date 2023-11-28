<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatentRegistration.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="incentives_PatentRegistration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/IncUC.ascx" TagName="Patentuc" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/plugin/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../js/plugin/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        /*----------------------------------------------------------------------------*/

        function OnChangeAvailed() {
            $('.availuploadsec,.availdetailsec,.availundertakingsec').hide();
            if ($("input[name='RadBtn_Availed_Earlier']:checked").val() == '1') {
                $('.availdetailsec').show();
                $('.availuploadsec').show();
                $('.availundertakingsec').hide();
                ImgSrc('', $('#Imgundertaking').attr("id"));
                ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#Imgsanctioned').attr("id"));

            }
            else {
                $('.availuploadsec').hide();
                $('.availdetailsec').hide();
                $('.availundertakingsec').show();
                ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#Imgundertaking').attr("id"));
                ImgSrc('', $('#Imgsanctioned').attr("id"));
            }
        }

        /*----------------------------------------------------------------------------*/

        function validformindraft() {

            if ($('#TxtAdhaar1').val() != '') {
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('<strong>Aadhaar no should be 12 digits.</strong>', 'SWP');
                    return false;
                }
            }
        }

        /*----------------------------------------------------------------------------*/

        function validation() {

            if (!blankFieldValidation('TxtApplicantName', 'Applicant Name', projname)) {
                return false;
            }
            var rbtnVal = 0;
            if (!$('input[name=radApplyBy]:checked').val()) {
                jAlert('<strong> Please select Application Applying By</strong>', 'SWP');
                return false;
            }
            else {
                rbtnVal = $('input[name=radApplyBy]:checked').val();
                if (rbtnVal == "1") {
                    if ($('#TxtAdhaar1').val() == '') {
                        jAlert('<strong> Please fill correct Aadhaar number.</strong>', 'SWP');
                        return false;
                    }
                    var adhar = $('#TxtAdhaar1').val();
                    if (adhar.length < 12) {
                        jAlert('<strong>Aadhaar no should be 12 digits.</strong>', 'SWP');
                        return false;
                    }
                }
                if (rbtnVal == "2") {
                    if (blankFieldValidation('hdnAUTHORIZEDFILE', 'Please upload Authorizing letter signed by Authorized Signatory !', '') == false) {
                        return false;
                    }
                }
            }

            if ($("#grvItmDetail tr").length > 0) {

            }
            else {
                jAlert('<strong>Please add atleast one Patented Items or Processes /Intellectual Property Right Details</strong>', 'Incentives');
                return false;
            }
            if ($("#grdMeansOfFinancePatent tr").length > 0) {

            }
            else {
                jAlert('<strong>Please add atleast one Loan Details of Loan for Patent Registration.</strong>', 'Incentives');
                return false;
            }
            return AvailValidation();
        }

        /*----------------------------------------------------------------------------*/

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

        /*----------------------------------------------------------------------------*/

        function validationPatentDetails() {

            if (!blankFieldValidation("txtagencyName", "Name of Authorised agency", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtagencyAddress", "Address of Authorised agency", projname)) {
                return false;
            }
            if (!DropDownValidation("ddlPatCategory", "0", "Patent/IPR Category", projname)) {
                return false;
            }
            if (!DropDownValidation("ddlPatSubCategory", "0", "Patent/IPR Sub Category", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtCommercialDt", "Date of Commercial Use", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtregistrNo", "Patent / IPR Registration Number", projname)) {
                return false;
            }
            if (!blankFieldValidation("FLPRegistertnUpload", "Upload Patent /IPR Registration Certificate", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtRegistrationdt", "Patent / IPR Registration Date", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtExpendincurr", "Expenditure Incurred to Obtain Patent/IPR", projname)) {
                return false;
            }
            if (!blankFieldValidation("FLPExpenditureUpload", "Upload Copy of Bills/Vouchers/receipts as Patent Expenditure Statement", projname)) {
                return false;
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

        function validationPATENTLoanDetails() {
            if (!blankFieldValidation("txtPatFinancialinst", "Name of Financial Institution", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtPatavailedLoan", "Amount Availed", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtPatLoanAvl", "Amount Avaialed Date", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtPatLoanNo", "Loan Number", projname)) {
                return false;
            }
        }

        /*----------------------------------------------------------------------------*/

        function validationAvailedDetails() {

            if (!blankFieldValidation("grdAssistanceDetailsAD_txtBody", "Body (Pvt, State Govt (Specify State),GoI)", projname)) {
                return false;
            }
            if (!blankFieldValidation("grdAssistanceDetailsAD_txtName", "Name of Financial Institution", projname)) {
                return false;
            }
            if (!blankFieldValidation("grdAssistanceDetailsAD_txtAmountAvailed", "Amount Availed", projname)) {
                return false;
            }
            if (!blankFieldValidation("grdAssistanceDetailsAD_txtAvailedDate", "Availed Date", projname)) {
                return false;
            }
            if (!blankFieldValidation("grdAssistanceDetailsAD_txtSanctionOrderNo", "Sanction Order no.", projname)) {
                return false;
            }
        }

        /*----------------------------------------------------------------------------*/

        //grdIncentiveAvailed
        function validationAvailedDetails1() {

            if (!blankFieldValidation("grdIncentiveAvailed_txtBody", "Body (Pvt, State Govt (Specify State),GoI)", projname)) {
                return false;
            }
            if (!blankFieldValidation("grdIncentiveAvailed_txtName", "Name of Financial Institution", projname)) {
                return false;
            }
            if (!blankFieldValidation("grdIncentiveAvailed_txtAmountAvailed", "Amount Availed", projname)) {
                return false;
            }
            if (!blankFieldValidation("grdIncentiveAvailed_txtAvailedDate", "Availed Date", projname)) {
                return false;
            }
            if (!blankFieldValidation("grdIncentiveAvailed_txtSanctionOrderNo", "Sanction Order no.", projname)) {
                return false;
            }
        }

        /*----------------------------------------------------------------------------*/

        function validationTermLoanDetails() {
            if (!blankFieldValidation("txtNameOfFinancialInst", "Name of Financial Institution", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtLocationState", "State", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtLocationCity", "City", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtLoanAmt", "Term Loan Amount", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtSactionDate", "Sanction Date", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtAvailedAmt", "Availed Amount", projname)) {
                return false;
            }
            if (!blankFieldValidation("txtAvailedDate", "Availed Date", projname)) {
                return false;
            }
        }

        /*----------------------------------------------------------------------------*/

        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });

            $('.Pioneersec,.attorneysec,.adhardetails').hide();

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

            $(".optradioPriority").on("click", function () {
                if ($("input:checked").val() == 'Yes') {
                    $('.Pioneersec').show();
                }
                else {
                    $('.Pioneersec').hide();
                }
            });
            $("#txtExpendincurr").blur(function () {
                CheckDecimal(this, 2, 'SWP');
                makeDecimal(this);
            });

            $('#lnkAddMore').click(function (e) {
                //Item/Process/IPR Name
                if (!blankFieldValidation('txtItemName', 'Item/Process/IPR Name', 'SWP')) {
                    return false;
                };
                if (!WhiteSpaceValidation1st('txtItemName', 'Item/Process/IPR Name', 'SWP')) {
                    return false;
                };
                if (!WhiteSpaceValidationLast('txtItemName', 'Item/Process/IPR Name', 'SWP')) {
                    return false;
                };
                if (!SpecialCharacter1st('txtItemName', 'Item/Process/IPR Name', 'SWP')) {
                    return false;
                };
                //Name of Authority


                //Date of Commercial Use
                if (!blankFieldValidation('txtCommercialDt', 'Date of Commercial Use', 'SWP')) {
                    return false;
                };

                //Patent/IPR Registration No.
                if (!blankFieldValidation('txtregistrNo', 'Patent/IPR Registration No.', 'SWP')) {
                    return false;
                };
                if (!WhiteSpaceValidation1st('txtregistrNo', 'Patent/IPR Registration No.', 'SWP')) {
                    return false;
                };
                if (!WhiteSpaceValidationLast('txtregistrNo', 'Patent/IPR Registration No.', 'SWP')) {
                    return false;
                };
                if (!SpecialCharacter1st('txtregistrNo', 'Patent/IPR Registration No.', 'SWP')) {
                    return false;
                };

                //Registration Date
                if (!blankFieldValidation('txtRegistrationdt', 'Registration Date', 'SWP')) {
                    return false;
                };

                //Expenditure incurred
                if (!blankFieldValidation('txtExpendincurr', 'Expenditure incurred', 'SWP')) {
                    return false;
                };
            });

            /*====================================added by Ritika lath on 17th Jan 2018 to open correct accordion after postback========================================================*/
            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails, #PatentDetails, #AvailedClaimDetails, #DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").click(function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });
            /*====================================added by Ritika lath to open correct accordion after postback========================================================*/
        });


        // To check decimal (controlId, DecimalPlaces)
        function CheckDecimal(e, t, Title) {
            try {
                var n = ""; var r; if (parseInt(t)) { r = t } else { r = 2 } var i = document.getElementById(e); if (i == "undefined" || i == null) { i = e } if (typeof i.value === "undefined") { n = i.innerHTML.trim() } else { n = i.value.trim() } if (n.split(".").length - 1 > 1 || n.charAt(n.length - 1) == "." || n.charAt(0) == ".") {
                    if (typeof i.value === "undefined") {
                        setTimeout(function () { jAlert('<strong>Please enter valid decimal !</strong>', Title); $("#" + i.getAttribute("id")).effect("shake", { direction: "left", times: 2, distance: 5 }, 800) }, 1)
                    }
                    else { setTimeout(function () { jAlert('<strong>Please enter valid decimal  !</strong>', Title); $(i).focus() }, 1) } return false
                } else {
                    if (n.substr(n.lastIndexOf(".") + 1, n.length).length > r && n.lastIndexOf(".") > -1) {
                        if (typeof i.value === "undefined")
                        { setTimeout(function () { jAlert('<strong>Only ' + r + ' digits are allowed after decimal ! </strong>', Title); $("#" + i.getAttribute("id")).effect("shake", { direction: "left", times: 2, distance: 5 }, 800) }, 1) } else { setTimeout(function () { jAlert('<strong>Only ' + r + ' digits are allowed after decimal  !</strong>', Title); $(i).focus() }, 1) } return false
                    } else { return true }
                }
            } catch (s) { }
        }

        /*----------------------------------------------------------------------------*/

        // To make decimal (controlId, DecimalPlace)
        function makeDecimal(e, t) {
            var n = document.getElementById(e);
            var r;
            if (parseInt(t)) {
                r = t
            }
            else {
                r = 2
            }
            if (n == "undefined" || n == null) {
                n = e
            }
            if (typeof n.value === "undefined") {
                if (n.innerHTML.trim().length > 0) {
                    n.innerHTML = parseFloat(n.innerHTML.trim()).toFixed(r)
                }
            }
            else {
                if (n.value.trim().length > 0) {
                    n.value = parseFloat(n.value.trim()).toFixed(r)
                }
            }
        }

        /*----------------------------------------------------------------------------*/
        /////////////////// jquery method for Industrial Unit////////////////////////////////////////
        function openpopup(flu) {
            var i = flu.id;
            $("#" + i).click();
            return false;
        }

        function OnChangeApplyBy() {
            $('.attorneysec,.adhardetails').hide();
            if ($("input[name='radApplyBy']:checked").val() == '1') {
                $('.adhardetails').show();
                $('.attorneysec').hide();

                ImgSrc('', $('#ImgSign').attr("id"));
            }
            else if ($("input[name='radApplyBy']:checked").val() == '2') {
                $('.attorneysec').show();
                $('.adhardetails').hide();
                ImgSrc($('#hdnAUTHORIZEDFILE').val(), $('#ImgSign').attr("id"));
            }
        } ////if ($("#radApplyBy_1").checked == true)

        function SameAddressIndustry() {
            var cc = $('#TxtAddressInd').val();
            if ($("#ChkSameData").is(':checked')) {
                $('#TxtRegAddress').val(cc);
            }
        }
        /////////////////// jquery method for Industrial Unit////////////////////////////////////////
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
                            </div>
                            <h2>
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </h2>
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
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Application By</label>
                                                    <div class="col-sm-6">
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
                                                    <div class="col-sm-6">
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
                                                        <small class="text-gray">Please provide Authorizing letter signed by Authorized Signatory</small></label>
                                                    <div class="col-sm-6">
                                                        <%-- <asp:RadioButtonList  runat="server" ID="radAuthorizing"></asp:RadioButtonList>--%>
                                                        <asp:Label runat="server" ID="lblAuthorizing"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hidAuthorizing" />
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="FlupAUTHORIZEDFILE" onchange="return FileCheck(this, 'pdf', 'pdf',4);"
                                                                runat="server" CssClass="form-control" />
                                                            <asp:HiddenField ID="hdnAUTHORIZEDFILE" runat="server" />
                                                            <asp:LinkButton ID="lnkAUTHORIZEDFILE" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkDocumentUpload_Click" ToolTip="Click here to upload the file." OnClientClick="return HasFile('FlupAUTHORIZEDFILE','Please Upload Authorizing letter');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkAUTHORIZEDFILEDdelete" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypAUTHORIZEDFILE" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf file only and Max file Size 4 MB)</small>
                                                        <asp:Label ID="lblAUTHORIZEDFILE" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Document uploaded successfully"></asp:Label>
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
                                                    <div class="col-sm-6">
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
                                                            <%--     <asp:FileUpload ID="FU_Org_Doc" CssClass="form-control" runat="server" />
                                                                            <asp:HiddenField ID="Hid_Org_Doc_File_Name" runat="server" />
                                                                            <asp:LinkButton ID="LnkBtn_Upload_Org_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                 ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LnkBtn_Delete_Org_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                 Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>--%>
                                                            <asp:HyperLink ID="Hyp_View_Org_Doc" runat="server" Target="_blank" class="btn btn-info">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <%--<asp:Label ID="Lbl_Msg_Org_Doc" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                            runat="server" Text="Document uploaded successfully"></asp:Label>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        EIN/ IEM/ IL No.</label>
                                                    <div class="col-sm-6">
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
                                                        <asp:Label ID="lbl_EIN_IL_Date" runat="server" CssClass="form-control-static"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="divbefor">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            PC No. Befor E/M/D
                                                        </label>
                                                        <div class="col-sm-6">
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
                                                            <asp:Label ID="lbl_Prod_Comm_Date_Before" runat="server" />
                                                            <%-- <span class="input-group-addon"><i class="fa fa-calendar"></i></span>--%>
                                                            <%-- </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="PC Issuance Date Before E/M/D"
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
                                                                <%--<asp:FileUpload ID="FU_Prod_Comm_Before" CssClass="form-control" runat="server" />
                                                                            <asp:HiddenField ID="Hid_Prod_Comm_Before_File_Name" runat="server" />
                                                                            <asp:LinkButton ID="LnkBtn_Upload_Prod_Comm_Before_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                 ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LnkBtn_Delete_Prod_Comm_Before_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                 Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>--%>
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
                                                        <div class="col-sm-6">
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
                                                            <asp:Label ID="lbl_Prod_Comm_Date_After" runat="server" />
                                                            <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="PC Issuance Date After E/M/D"
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
                                                                <%--  <asp:FileUpload ID="FU_Prod_Comm_After" CssClass="form-control" runat="server" />
                                                                                <asp:HiddenField ID="Hid_Prod_Comm_After_File_Name" runat="server" />
                                                                                <asp:LinkButton ID="LnkBtn_Upload_Prod_Comm_After_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                     ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="LnkBtn_Delete_Prod_Comm_After_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                     Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>--%>
                                                                <asp:HyperLink ID="Hyp_View_Prod_Comm_After_Doc" runat="server" Target="_blank" class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <%--<asp:Label ID="Lbl_Msg_Prod_Comm_After_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        District</label>
                                                    <div class="col-sm-6">
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
                                                    <div class="col-sm-6">
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
                                                    <div class="col-sm-6">
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
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblIs_Priority" runat="server" CssClass="form-control-static"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div runat="server" id="Pioneersec">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4 col-sm-offset-1">
                                                            Derived Sector</label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_Derived_Sector" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Is Pioneer</label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblIs_Is_Pioneer" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="Div_Pioneer_Doc">
                                                    <div class="row">
                                                        <label class="col-sm-4 col-sm-offset-1">
                                                            <asp:Label ID="Lbl_Pioneer_Doc_Name" runat="server" Text=""></asp:Label>
                                                            <asp:HiddenField ID="Hid_Pioneer_Doc_Code" runat="server" />
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
                                    <div class="panel-heading" role="tab" id="Div8">
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
                                                        All Amounts to be Entered in INR(in Lakhs)</p>
                                                    <div runat="server" id="divbefor1">
                                                        <h4>
                                                            <asp:Label runat="server" ID="lblemdBefore" Text="Before E/M/D" CssClass="h2-hdr"></asp:Label>
                                                        </h4>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12">
                                                                    Items of Manufacture/Activity
                                                                </label>
                                                                <div class="col-sm-12 ">
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
                                                                <label class="col-sm-5">
                                                                    <asp:Label ID="Lbl_Direct_Emp_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                    <asp:HiddenField ID="Hid_Direct_Emp_Before_Doc_Code" runat="server" />
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
                                                                <label for="Iname" class="col-sm-4 ">
                                                                    Contractual Employment in Numbers</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Contract_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="Div_Direct_Emp_Doc_After">
                                                            <div class="row">
                                                                <label class="col-sm-5">
                                                                    <asp:Label ID="Lbl_Direct_Emp_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                    <asp:HiddenField ID="Hid_Direct_Emp_After_Doc_Code" runat="server" />
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
                                                        <label class="col-sm-5">
                                                            <asp:Label ID="Lbl_FFCI_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                            <asp:HiddenField ID="Hid_FFCI_Before_Doc_Code" runat="server" />
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
                                                        <label class="col-sm-5">
                                                            <small class="text-gray">
                                                                <asp:Label ID="Lbl_Approved_DPR_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="Hid_Approved_DPR_Before_Doc_Code" runat="server" />
                                                            </small>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:HyperLink ID="Hyp_View_Approved_DPR_Before_Doc" runat="server" Target="_blank"
                                                                    class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
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
                                                            <asp:Label ID="lbl_FFCI_Date_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-5 ">
                                                            <asp:Label ID="Lbl_FFCI_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                            <asp:HiddenField ID="Hid_FFCI_After_Doc_Code" runat="server" />
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:HyperLink ID="Hyp_View_FFCI_After_Doc" runat="server" Target="_blank" title="Click to View"
                                                                    class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
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
                                                                            <asp:Label runat="server" ID="Label3" Text="Other Fixed Assets"></asp:Label>
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
                                                        <label class="col-sm-5">
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
                                            <div class="form-group row">
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
                                                    <label class="col-sm-5">
                                                        <asp:Label ID="Lbl_Term_Loan_Doc_Name" runat="server" Text=""></asp:Label>
                                                        <asp:HiddenField ID="Hid_Term_Loan_Doc_Code" runat="server" />
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
                                            <div class="form-group row">
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
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div11">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#PatentDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-minus">
                                                </i>Patent Details </a>
                                        </h4>
                                    </div>
                                    <div id="PatentDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="form-group" style="display: none;">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Detail of Authorising agency for patented/IPR</label>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>
                                                                Name of Authorised agency <a data-toggle="tooltip" class="fieldinfo2" style="padding-left: 10px"
                                                                    title="The Agency where the Unit Permission to get patent"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                            </th>
                                                            <th>
                                                                Address of Authorised agency
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Patented Items or Processes /Intellectual Property Right Details</label>
                                                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>--%>
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th width="4%">
                                                                    Slno.
                                                                </th>
                                                                <th width="10%">
                                                                    Name of Authorised agency
                                                                </th>
                                                                <th width="10%">
                                                                    Address of Authorised agency
                                                                </th>
                                                                <th width="10%">
                                                                    Patent/IPR Category
                                                                </th>
                                                                <th width="10%">
                                                                    Patent/IPR Sub Category
                                                                </th>
                                                                <th width="10%">
                                                                    Date of Commercial Use
                                                                </th>
                                                                <th width="10%">
                                                                    Patent / IPR Registration Number
                                                                </th>
                                                                <th width="10%">
                                                                    Upload Patent /IPR Registration Certificate<a data-toggle="tooltip" class="fieldinfo2"
                                                                        style="padding-left: 10px" title=" Upload Patent /IPR Registration Certificate"><i
                                                                            class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                </th>
                                                                <th width="11%">
                                                                    Patent / IPR Registration Date
                                                                </th>
                                                                <th width="11%">
                                                                    Expenditure Incurred to Obtain Patent/IPR
                                                                </th>
                                                                <th width="11%">
                                                                    Upload Copy of Bills/Vouchers/receipts as Patent Expenditure Statement<a data-toggle="tooltip"
                                                                        class="fieldinfo2" style="padding-left: 10px" title="Upload Copy of Bills/Vouchers/receipts as Patent Expenditure Statement"><i
                                                                            class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                </th>
                                                                <th width="5%">
                                                                    Add More
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtagencyName" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="flt1" runat="server" TargetControlID="txtagencyName"
                                                                        FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                        ValidChars=",,-,/\, ,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtagencyAddress" CssClass="form-control" TextMode="MultiLine"
                                                                        MaxLength="500"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtagencyAddress"
                                                                        FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                        ValidChars=",-/ .">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlPatCategory" runat="server" CssClass="form-control" AutoPostBack="True"
                                                                                OnSelectedIndexChanged="ddlPatCategory_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlPatSubCategory" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div5">
                                                                        <asp:TextBox ID="txtCommercialDt" CssClass="form-control" runat="server" Width="100px"></asp:TextBox>
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtregistrNo" MaxLength="20" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" TargetControlID="txtregistrNo"
                                                                        FilterType="UppercaseLetters, LowercaseLetters,Numbers" Enabled="True" FilterMode="ValidChars"
                                                                        ValidChars="- .,">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkRegistertnUpload" CssClass="btn btn-danger btn-sm" data-toggle="tooltip"
                                                                        title="Upload Patent /IPR Registration Certificate" runat="server" OnClientClick="return openpopup(FLPRegistertnUpload);"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="lblRegistertnUpload" OnClientClick="return false;"></asp:LinkButton>
                                                                    <asp:FileUpload ID="FLPRegistertnUpload" runat="server" Width="100px" Style="display: none"
                                                                        onchange="return FileCheckGrid(this,lblRegistertnUpload, 'pdf','pdf',4);" />
                                                                </td>
                                                                <td>
                                                                    <div class="input-group  date datePicker" id="Div6">
                                                                        <asp:TextBox ID="txtRegistrationdt" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtExpendincurr" CssClass="form-control" MaxLength="10" runat="server"
                                                                        onkeypress="return  FloatOnly(event, this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" TargetControlID="txtExpendincurr"
                                                                        FilterType="Custom,Numbers" Enabled="True" FilterMode="ValidChars" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkExpenditureUpload" CssClass="btn btn-danger btn-sm" data-toggle="tooltip"
                                                                        title="Upload Copy of Bills/Vouchers/receipts as Patent Expenditure Statement"
                                                                        runat="server" OnClientClick="return openpopup(FLPExpenditureUpload);"><i class="fa fa-cloud-upload" ></i></asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="lblExpenditureUpload" OnClientClick="return false;"></asp:LinkButton>
                                                                    <asp:FileUpload ID="FLPExpenditureUpload" runat="server" Style="display: none" onchange="return FileCheckGrid(this,lblExpenditureUpload, 'pdf','pdf',4);" />
                                                                </td>
                                                                <td>
                                                                    <%--  <a title="Add" id="anchItemAdd" runat="server" href="#">--%>
                                                                    <asp:LinkButton ID="lnkAddMorePatent" CssClass="btn btn-success btn-sm" runat="server"
                                                                        OnClick="lnkAddMorePatent_Click" OnClientClick="return validationPatentDetails();"><i class="fa fa-plus-square"></i></asp:LinkButton><%--</a>--%>
                                                                    <asp:HiddenField ID="hdnAddMore" Value="Add" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="12">
                                                                    <asp:GridView ID="grvItmDetail" runat="server" class="table table-bordered table-condensed"
                                                                        Width="100%" AutoGenerateColumns="False" OnRowDeleting="grvItmDetail_RowDeleting"
                                                                        ShowHeader="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno.">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="5%" />
                                                                                <ItemStyle Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="vchAgencyName" HeaderText="Name of Authorised agency">
                                                                                <HeaderStyle Width="10%" />
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="vchAgencyAddress" HeaderText="Address of Authorised agency">
                                                                                <HeaderStyle Width="10%" />
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Patent/IPR Category">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("VchCatgoryName") %>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdnCategory" runat="server" Value='<%# Eval("IntCatgoryid") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="10%" />
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Patent/IPR Sub Category">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSubCategory" runat="server" Text='<%# Eval("VchSubCatgoryName") %>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdnSubCategory" runat="server" Value='<%# Eval("IntSubCatgoryid") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="10%" />
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="dtCommercialDt" HeaderText="Date of Commercial Use">
                                                                                <HeaderStyle Width="10%" />
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="vchIPRRegistrationNo" HeaderText="Patent / IPR Registration Number">
                                                                                <HeaderStyle Width="10%" />
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Patent /IPR Registration Certificate">
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField ID="hdnvchIPRRegistrationFile" runat="server" Value='<%# Eval("vchIPRRegistrationFile") %>' />
                                                                                    <asp:HyperLink runat="server" ID="hypvchIPRRegistrationFile" NavigateUrl='<%# "~/incentives/Files/PatentDetail/"+Eval("vchIPRRegistrationFile")  %>'
                                                                                        Target="_blank"><%# Eval("vchIPRRegistrationFile") %></asp:HyperLink>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="dtRegistrationDate" HeaderText="Patent / IPR Registration Date">
                                                                                <HeaderStyle Width="10%" />
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="decExpenditureincurred" HeaderText="Expenditure Incurred to Obtain Patent/IPR">
                                                                                <HeaderStyle Width="10%" />
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Copy of Bills/Vouchers/receipts as Patent Expenditure Statement">
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField ID="hdnvchExpenditureFile" runat="server" Value='<%# Eval("vchExpenditureFile") %>' />
                                                                                    <asp:HyperLink runat="server" ID="hypvchExpenditureFile" NavigateUrl='<%# "~/incentives/Files/PatentDetail/"+Eval("vchExpenditureFile") %>'
                                                                                        Target="_blank"><%# Eval("vchExpenditureFile")%></asp:HyperLink>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delete">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="imgbtnDelete" CssClass="btn btn-xs bigger btn-danger" runat="server"
                                                                                        CommandName="Delete"><i class="ace-icon fa fa-trash-o icon-only bigger-110"></i>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="5%" />
                                                                                <ItemStyle Width="5%" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <h4 class="h4-header">
                                                MEANS OF FINANCE FOR PATENT REGISTRATION</h4>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Loan Details</label>
                                                    <div class="col-sm-12">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <table class="table table-bordered">
                                                                    <tr>
                                                                        <th width="5%">
                                                                            Slno.
                                                                        </th>
                                                                        <th width="20%">
                                                                            Name of Financial Institution <a data-toggle="tooltip" class="fieldinfo2" style="padding-left: 10px"
                                                                                title="Financial institution like' Bank' where the unit get money from"><i class="fa fa-question-circle"
                                                                                    aria-hidden="true"></i></a>
                                                                        </th>
                                                                        <th width="20%">
                                                                            Amount Availed
                                                                        </th>
                                                                        <th width="20%">
                                                                            Amount Availed Date
                                                                        </th>
                                                                        <th width="20%">
                                                                            Sanction Order No
                                                                        </th>
                                                                        <th>
                                                                            Add More
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            1
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPatFinancialinst" MaxLength="30" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPatFinancialinst"
                                                                                FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                                ValidChars=",,-,/\, ,.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPatavailedLoan" CssClass="form-control" MaxLength="10" runat="server"
                                                                                onkeypress="return  FloatOnly(event, this);"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" Enabled="True"
                                                                                FilterMode="ValidChars" ValidChars="0123456789." TargetControlID="txtPatavailedLoan">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <div class="input-group  date datePicker" id="Div12">
                                                                                <asp:TextBox ID="txtPatLoanAvl" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPatLoanNo" MaxLength="20" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPatLoanNo"
                                                                                FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                                ValidChars=",,-,/\, ,.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnk_PatentLoan" OnClick="lnk_PatentLoan_click" CssClass="btn btn-success btn-sm"
                                                                                runat="server" OnClientClick="return  validationPATENTLoanDetails()"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6">
                                                                            <asp:GridView ID="grdMeansOfFinancePatent" runat="server" CssClass="table table-bordered"
                                                                                AutoGenerateColumns="false" ShowHeader="false">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblslno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="5%"></ItemStyle>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPatFinancialName" runat="server" Text='<%# Eval("VCH_NAME_OF_FINANCIAL_INST") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="20%"></ItemStyle>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPatAvailedAmt" runat="server" Text='<%# Eval("DEC_AVAILED_AMT") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="20%"></ItemStyle>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPatAvailedDate" runat="server" Text='<%# Eval("DTM_AVAILED_DATE") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="20%"></ItemStyle>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPatLoanNo" runat="server" Text='<%# Eval("VCH_LOAN_NO") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="20%"></ItemStyle>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImageButtonDeleteMeans" runat="server" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                                                CommandArgument='<%# Container.DataItemIndex %>' OnClick="ImageButtonDeletePatentmeans_Click" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
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
                                                href="#AvailedClaimDetails" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa  fa-plus"></i>Availed Details</a>
                                        </h4>
                                    </div>
                                    <div id="AvailedClaimDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-5">
                                                        Present Claim for reimbursement
                                                    </label>
                                                    <div class="col-sm-5">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtreimamt" MaxLength="10" runat="server" CssClass="form-control"
                                                            onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1226" runat="server" TargetControlID="txtreimamt"
                                                            FilterType="Custom,Numbers" ValidChars="." />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-5">
                                                        Has Subsidy/Incentive against the details in this application been availed earlier
                                                    </label>
                                                    <div class="col-sm-5">
                                                        <span class="colon">:</span>
                                                        <asp:RadioButtonList ID="RadBtn_Availed_Earlier" class="applyby" runat="server" RepeatDirection="Horizontal"
                                                            onchange="return OnChangeAvailed();">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group availdetailsec availgroup1">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-12 ">
                                                        Details of Subsidy Already Availed
                                                    </label>
                                                    <div class="col-sm-12 ">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
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
                                                                            <asp:TextBox ID="txtagency" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender556" runat="server" TargetControlID="txtagency"
                                                                                FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters"
                                                                                ValidChars=" .,-" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtsacamt" runat="server" CssClass="form-control" MaxLength="15"
                                                                                onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender566" runat="server" TargetControlID="txtsacamt"
                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtsacord" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server" TargetControlID="txtsacord"
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
                                                                            <asp:TextBox ID="txtavilamt" runat="server" CssClass="form-control" MaxLength="15"
                                                                                onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" TargetControlID="txtavilamt"
                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton41" CssClass="btn btn-success btn-sm" OnClick="LinkButton41_Click"
                                                                                runat="server" OnClientClick="return validAvailgrid();"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <asp:GridView ID="grdAssistanceDetailsAD" runat="server" CssClass="table table-bordered"
                                                                    ShowHeader="false" AutoGenerateColumns="false" ShowFooter="false" OnRowDeleting="grdAssistanceDetailsAD_RowDeleting">
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
                                                                        <asp:TemplateField HeaderText="Add More">
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
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group  availuploadsec availgroup1">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-5">
                                                        Document details of assistance sanctioned
                                                    </label>
                                                    <div class="col-sm-5">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="FU_Asst_Sanc_Doc" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip',4);" />
                                                            <asp:HiddenField ID="Hid_Asst_Sanc_File_Name" runat="server" />
                                                            <asp:LinkButton ID="LnkBtn_Upload_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkDocumentUpload_Click" ToolTip="Click here to upload the file." OnClientClick="return HasFile('FU_Asst_Sanc_Doc','Please Upload Document details of assistance sanctioned');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LnkBtn_Delete_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="Hyp_View_Asst_Sanc_Doc" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                        <asp:Label ID="Lbl_Msg_Asst_Sanc_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                            Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group availundertakingsec availgroup2">
                                                <div class="row">
                                                    <div class="col-sm-5">
                                                        Undertaking on non-availment of subsidy earlier on this project
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="FU_Undertaking_Doc" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip',4);" />
                                                            <asp:HiddenField ID="Hid_Undertaking_File_Name" runat="server" />
                                                            <asp:LinkButton ID="LnkBtn_Upload_Undertaking_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkDocumentUpload_Click" ToolTip="Click here to upload the file." OnClientClick="return HasFile('FU_Undertaking_Doc','Please Upload Undertaking on non-availment of subsidy earlier on this project.');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LnkBtn_Delete_Undertaking_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="Hyp_View_Undertaking_Doc" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                        <asp:Label ID="Lbl_Msg_Undertaking_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                            Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group availuploadsec availgroup1">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-5">
                                                        Amount of Differential Claim to be Exempted
                                                    </label>
                                                    <div class="col-sm-5">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtdiffclaimamt" MaxLength="10" runat="server" CssClass="form-control"
                                                            onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender116" runat="server" TargetControlID="txtdiffclaimamt"
                                                            FilterType="Custom,Numbers" ValidChars="." />
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
                                                <tr>
                                                    <td>
                                                        Document details of assistance sanctioned
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="Imgsanctioned" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Undertaking on non-availment of subsidy earlier on this project
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="Imgundertaking" Height="15" Width="15" src="../images/cancel-square.png" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-footer">
                            <div class="row">
                                <div class="col-sm-12 text-right">
                                    <asp:Button ID="btnDraft" runat="server" Text="Save As Draft" CssClass="btn btn-warning"
                                        OnClientClick="return validformindraft();" OnClick="btnDraft_Click" />
                                    <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-success"
                                        OnClick="btnApply_Click" OnClientClick="return validation();" />
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

            DocCheckList();
            OnChangeAvailed();
            OnChangeApplyBy();

            /*====================================added by Ritika lath on 17th Jan 2018 to open correct accordion after postback========================================================*/
            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails, #PatentDetails, #AvailedClaimDetails, #DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").click(function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });
            /*====================================added by Ritika lath to open correct accordion after postback========================================================*/

        }

        // Avail Details Start
        $(document).ready(function () {
            OnChangeAvailed();
            $("#txtClaimtExempted").blur(function () {
                ValidPrice($("#txtClaimtExempted"));
            });
            $("#txtClaimReimbursement").blur(function () {
                ValidPrice($("#txtClaimReimbursement"))
            });
            function ValidPrice(ths) {
                var numeric = ths.val();
                if (numeric != "") {
                    var regex = /^\d{0,12}(\.\d{1,2})?$/;
                    if (!regex.test(numeric)) {
                        jAlert("<strong>Enter Valid Amount of Differential Claim to be Exempted.</strong>", 'SWP');
                        ths.val("");
                        ths.focus();
                        ths.select();
                        return false;
                    }
                }
                else {
                    ths.val("0.00");
                    ths.select();
                }
            }
        });
        // Avail Details End
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
        function DocCheckList() {
            ImgSrc($('#hdnAUTHORIZEDFILE').val(), $('#ImgSign').attr("id"));
            ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#Imgsanctioned').attr("id"));
            ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#Imgundertaking').attr("id"));


        }
        

    </script>
</body>
</html>
