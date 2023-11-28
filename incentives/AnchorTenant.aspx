<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnchorTenant.aspx.cs" Inherits="Subsidy_Plant_MC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" />
<script src="../js/WebValidation.js" type="text/javascript"></script>
<script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Subsidy on cost of land for Anchor Tenant under industrial policy resolution
        2015</title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .not-active
        {
            pointer-events: none;
            cursor: default;
        }
        .fieldinfo2
        {
            float: right;
            margin-right: 8px;
            font-size: 17px;
            margin-top: 1px;
            color: #3abffb;
        }
        .collapse.in
        {
            display: block;
            height: auto !important;
        }
    </style>
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
            $('.Pioneersec,.attorneysec,.adhardetails').hide();
            $(".applyby").on("click", function () {
                if ($("input:checked").val() == 'Self') {
                    $('.adhardetails').show();
                    $('.attorneysec').hide();
                }
                else {
                    $('.attorneysec').show();
                    $('.adhardetails').hide();
                }
            });
            $(".optradioPriority").on("click", function () {
                if ($("input:checked").val() == 'Yes') {
                    $('.Pioneersec').show();
                }
                else {
                    $('.Pioneersec').hide();
                }
            });

            /*====================================added by Ritika lath on 17th Jan 2018 to open correct accordion after postback========================================================*/
            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails, #BriefDetails, #DLSWCA, #BankDetails, #DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").click(function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });
            /*====================================added by Ritika lath to open correct accordion after postback========================================================*/
        }); ///-------------end of document of ready

        function OnlyNumber(control, evt) {
            var e = evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            else {
                return true;
            }
        }

        function TextCounter(ctlTxtName, numTextSize) {
            var txtName = document.getElementById(ctlTxtName).value;
            var txtNameLength = txtName.length;
            if (parseInt(txtNameLength) > parseInt(numTextSize)) {
                var txtMaxTextSize = txtName.substr(0, numTextSize)
                document.getElementById(ctlTxtName).value = txtMaxTextSize;
                //                alert("enteredText Exceeds '" + numTextSize + "' Characters.");
                jAlert('<strong> enteredText Exceeds ' + numTextSize + ' Characters</strong>', projname);
                document.getElementById(lblCouter).innerHTML = 0;
                return false;
            }
            else {

                return true;
            }
        }

        function calculetotal() {
            var cal = 0;
            if ($("#txtLandtype").val() != '') {
                cal = cal + parseFloat($("#txtLandtype").val());

            }
            if ($("#txtBuilding").val() != '') {
                cal = cal + parseFloat($("#txtBuilding").val());

            }
            if ($("#txtPlantMachinery").val() != '') {
                cal = cal + parseFloat($("#txtPlantMachinery").val());
            }

            if ($("#txtBalancingEquipment").val() != '') {
                cal = cal + parseFloat($("#txtBalancingEquipment").val());
            }
            if ($("#txtOtherFixedAssests").val() != '') {
                cal = cal + parseFloat($("#txtOtherFixedAssests").val());
            }

            $("#lblTotalAmount").text(cal);

        }

        function openpopup(flu) {
            var i = flu.id;
            $("#" + i).click();
            return false;
        }

        function OnChangePriority() {
            $('.Pioneersec').hide();
            if ($("input[name='RadIsPriority']:checked").val() == '1') {
                $('.Pioneersec').show();
            }
            else {
                $('.Pioneersec').hide();
            }
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
        }

        function SameAddressIndustry() {
            var cc = $('#TxtAddressInd').val();
            if ($("#ChkSameData").is(':checked')) {
                $('#TxtRegAddress').val(cc);
            }
            else {
                $('#TxtRegAddress').val('');
            }
        }

        function valCheck(e) {
            debugger;
            var val = e.value;

            if (val == 0) {
                e.value = '';
            }

        }

        function valRestore(e) {
            debugger;
            var val = e.value;

            if (val == '') {
                e.value = 0;
            }
        }

        function checkFatalitiesDLSWCA() {
            if (blankFieldValidation('txtProposedCommonFacl', 'Proposed common facilities to attract Secondary Tenants', projname) == false) {
                return false;
            }
            return true;
        }

        function checkManufactureActivity() {

            if (blankFieldValidation('txtProductName', 'Product/Service Name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtProcuctQuantity', 'Quantity', projname) == false) {
                return false;
            }
            if (DropDownValidation('ddlItemUnit', '0', 'Units', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtItemValue', 'Value', projname) == false) {
                return false;
            }
            return true;
        }

        function checkTermLoanDtl() {

            if (blankFieldValidation('txtNameOfFinancialInst', 'Name of Financial Institution', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtLocationState', 'State', projname) == false) {
                return false;
            }

            if (blankFieldValidation('txtLocationCity', 'City', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtLoanAmt', 'Term Loan Amount', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtSactionDate', 'Sanction Date', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtAvailedAmt', 'Availed Amount', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtAvailedDate', 'Availed Date', projname) == false) {
                return false;
            }
            return true;
        }

        function checkWorkingCapitalLoadDtl() {

            if (blankFieldValidation('txtNameOfFinancialInst_working', 'Name of Financial Institution', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtLocationState_working', 'State', projname) == false) {
                return false;
            }

            if (blankFieldValidation('txtLocationCity_working', 'City', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtLoanAmt_working', 'Term Loan Amount', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtSactionDate_working', 'Sanction Date', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtAvailedAmt_working', 'Availed Amount', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtAvailedDate_working', 'Availed Date', projname) == false) {
                return false;
            }
            return true;
        }

        function CheckValidation() {
            //----------------------------industry Unit----------------------
            debugger;
            if (!DropDownValidation('DdlGender', '0', 'Applicant Salutation', projname)) {
                return false;
            }
            if (blankFieldValidation('TxtApplicantName', 'Applicant Name', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('TxtApplicantName', 'Applicant Name', projname) == false) {
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
                        jAlert('Aadhaar Card no should be 12 digits.', projname);
                        return false;
                    }
                }
                else if (rbtnVal == "2") {
                    if ($("#hdnAUTHORIZEDFILE").val().length == 0) {
                        if ($("#FlupAUTHORIZEDFILE").val().length == 0) {
                            jAlert('<strong>Please upload Authorizing letter .</strong>', projname);
                            return false;
                        }
                    }
                }

            }


            //---------------------------Brief Details of Proposed Activity ------------
            if (blankFieldValidation('txtBriefDtlProposed', 'Brief Details of Proposed Activity', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtPropDwnstrm', 'Prospects Downstream Enterprises', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtPropAnci', 'Prospects of ancillary enterprises', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtDevelopUtil', 'Development of utility infrastructure', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtExternalities', 'Externalities like R & D facilities or technology sourcing mechanism', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtPropCfc', 'Proposed CFC', projname) == false) {
                return false;
            }


            var rowsBriefDtl = $("[id*=grdPropCmmnFacl] tbody tr").length;
            if (rowsBriefDtl == 0) {
                jAlert('<strong> Proposed common facilities to attract Secondary Tenants can not be blank !</strong>', projname);
                return false;
            }

            if ($("#hdnDtlOfSecTnt").val().length == 0) {
                if ($("#fupDtlOfSecTnt").val().length == 0) {
                    jAlert('<strong>Please upload Details of Secondary tenants !</strong>', projname);
                    return false;
                }
            }
            if ($("#hdnDtlBusnessPlan").val().length == 0) {
                if ($("#fupldDtlBusnessPlanUpload").val().length == 0) {
                    jAlert('<strong>Please upload Details of Business plan for attracting Secondary Tenants !</strong>', projname);
                    return false;
                }
            }
            if ($("#hdnConsetSecUpload").val().length == 0) {
                if ($("#fupldConsetSecUpload").val().length == 0) {
                    jAlert('<strong>Please upload Consent of Secondary Tenant !</strong>', projname);
                    return false;
                }
            }
            //----------------------------------------------------------End of Brief Details of Proposed Activity

            //-------------------------------DLSWCA/SWSLCA/HLCA Approval Details--------------------
            if (blankFieldValidation('txtDLSWCADateOfApproval', 'Date of Approval', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtDLSWCALandApproved', 'Requirment of land approved by DLSWCA / SLSWCA / HLCA', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtDLSWCALandCost', 'Cost of land', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtDLSWCASubsidyAmt', 'Eligible Amount of subsidy', projname) == false) {
                return false;
            }
            if ($("#hdnDLSWCAApprovalDoc").val().length == 0) {
                if ($("#fupDLSWCAApprovalDocUpload").val().length == 0) {
                    jAlert('<strong>Please upload Copy of Approval of DLSWCA / SLSWCA / HLCA !</strong>', projname);
                    return false;
                }
            }
            if ($("#hdnDLSWCASubstanDoc").val().length == 0) {
                if ($("#fupDLSWCASubstanDocUpload").val().length == 0) {
                    jAlert('<strong>Please upload Copy of Documents to substantitate of land cost !</strong>', projname);
                    return false;
                }
            }
            //--------------------------------------------------------------End of DLSWCA/SWSLCA/HLCA Approval Details
            //-----------------------------Bank Details-------------------------------
            if (blankFieldValidation('txtAccNo', 'Account No of Industrial Unit', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtBnkNm', 'Bank Name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtBranch', 'Branch Name', projname) == false) {
                return false;
            }
            debugger;
            if (blankFieldValidation('txtIFSC', 'IFSC Code', projname) == false) {
                return false;
            }
            if ($("#hdnBank").val().length == 0) {
                jAlert('<strong>Please Upload cancelled cheque to verify the entered A/c details !</strong>', projname);
                return false;

            }

        }

        function validformindraft() {
            if ($('#TxtAdhaar1').val() != '') {
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('Aadhaar Card no should be 12 digits.', '');
                    return false;
                }
            }
            if ($("#txtAccNo").val() != '') {
                var acno = $("#txtAccNo").val();
                if (acno.length > 16) {
                    jAlert('<strong> Account Number should not be more than 16 digit </strong>', 'Alert');
                    $("#acno").focus();
                    return false;
                }

            }
            if ($("#txtIFSC").val() != '') {
                var ifsc = $("#txtIFSC").val();
                if (ifsc.length < 7) {
                    jAlert('<strong> IFSC code should have minimum 7 characters </strong>', 'Alert');
                    $("#txtIFSC").focus();
                    return false;
                }


            }
        }

       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdnTimeFrame" runat="server" />
    <asp:HiddenField ID="hdnPostSubFlag" runat="server" />
    <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container">
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
                                    <li class="active"><a href="Basic_Details.aspx">Apply For incentive</a></li>
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
                                <div class="pull-left">
                                    <i>All Amounts to be entered in <span class="text-red">INR(in Lakhs)</span></i></div>
                                <div class="pull-right">
                                    <span class="text-red">( * )</span><i> All fields in this section are mandatory</i></div>
                                <div class="clearfix">
                                </div>
                                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="headingOne">
                                            <h4 class="panel-title">
                                                <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                                    aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa  fa-plus">
                                                    </i>Industrial Unit's Details </a>
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
                                                        <div class="col-sm-6  ">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lbl_Org_Type" runat="server" CssClass="form-control-static">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Name of Applicant <span class="text-red">*</span></label>
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
                                                            Application By<span class="text-red">*</span></label>
                                                        <div class="col-sm-6  ">
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
                                                            Aadhaar No.<span class="text-red">*</span></label>
                                                        <div class="col-sm-6  ">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TxtAdhaar1" CssClass="form-control" placeholder="123412341234" runat="server"
                                                                MaxLength="12"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                                                TargetControlID="TxtAdhaar1" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group attorneysec" id="divAuthorizing" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            <small class="text-gray">Please provide Authorizing letter signed by Authorized Signatory<span
                                                                class="text-red">*</span></small><a data-toggle="tooltip" class="fieldinfo2" title="Please provide Authorizing letter signed by Authorized Signatory">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <%-- <asp:RadioButtonList  runat="server" ID="radAuthorizing"></asp:RadioButtonList>--%>
                                                            <asp:Label runat="server" ID="lblAuthorizing"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hidAuthorizing" />
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FlupAUTHORIZEDFILE" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                <asp:LinkButton ID="lnkAUTHORIZEDFILE" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkAUTHORIZEDFILEDdelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" ToolTip="Delete File" OnClick="lnkOrgDocumentDelete_Click"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypAUTHORIZEDFILE" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-blue"
                                                                    runat="server" OnClientClick="JavaScript: return false;" Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="hdnAUTHORIZEDFILE" runat="server" />
                                                            <asp:HiddenField ID="hdnAUTHORIZEDFILEDocId" runat="server" />
                                                            <asp:Label ID="lblAUTHORIZEDFILED" Style="font-size: 12px;" CssClass="text-blue"
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
                                                        <div class="col-sm-6  ">
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
                                                        <div class="col-sm-6  ">
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
                                                            <div class="col-sm-6  ">
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
                                                            <div class="col-sm-6  ">
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
                                                        <div class="col-sm-6  ">
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
                                                        <div class="col-sm-6  ">
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
                                                        <div class="col-sm-6  ">
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
                                                        <div class="col-sm-6  ">
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
                                                            <div class="col-sm-6  ">
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
                                                            <div class="col-sm-6  ">
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
                                                                    <%--<asp:FileUpload ID="FU_Pioneer_Doc" CssClass="form-control" runat="server" />
                                                                            <asp:HiddenField ID="Hid_Pioneer_Doc_File_Name" runat="server" />
                                                                            <asp:LinkButton ID="LnkBtn_Upload_Pioneer_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                 ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LnkBtn_Delete_Pioneer_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                 Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>--%>
                                                                    <asp:HyperLink ID="Hyp_View_Pioneer_Doc" runat="server" Target="_blank" class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <%--<asp:Label ID="Lbl_Msg_Pioneer_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                            Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>--%>
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
                                                            <%--<asp:CheckBox ID="ChkBx_Sectoral" runat="server" Enabled="false" />--%>
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
                                                    <i class="more-less fa  fa-plus"></i>Major Operational Activities of the Company</a>
                                            </h4>
                                        </div>
                                        <div id="ProductionEmploymentDetails" class="panel-collapse collapse" role="tabpanel"
                                            aria-labelledby="headingThree">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="panel-body">
                                                        <div runat="server" id="divbefor1">
                                                            <h4>
                                                                <asp:Label runat="server" ID="lblemdBefore" Text="Before E/M/D" CssClass="h2-hdr"></asp:Label>
                                                            </h4>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Iname" class="col-sm-12">
                                                                        Items of Manufacture/Activity
                                                                    </label>
                                                                    <div class="col-sm-12   ">
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
                                                                        Direct Employment In Numbers<small>(on Company Payroll)</small><span class="text-red">*</span>
                                                                    </label>
                                                                    <div class="col-sm-2">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lbl_Direct_Emp_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                    <label for="Iname" class="col-sm-4">
                                                                        Contractual Employment In Numbers</label>
                                                                    <div class="col-sm-2">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lbl_Contract_Emp_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="Div_Direct_Emp_Doc_Before">
                                                                <div class="row">
                                                                    <label class="col-sm-4">
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
                                                                    <div class="col-sm-12   ">
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
                                                                        Direct Employment In Numbers<small>(on Company Payroll)</small><span class="text-red">*</span>
                                                                    </label>
                                                                    <div class="col-sm-2">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lbl_Direct_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                    <label for="Iname" class="col-sm-4">
                                                                        Contractual Employment In Numbers</label>
                                                                    <div class="col-sm-2">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lbl_Contract_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="Div_Direct_Emp_Doc_After">
                                                                <div class="row">
                                                                    <label class="col-sm-4 col-sm-offset-1">
                                                                        <small class="text-gray">
                                                                            <asp:Label ID="Lbl_Direct_Emp_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                            <asp:HiddenField ID="Hid_Direct_Emp_After_Doc_Code" runat="server" />
                                                                        </small>
                                                                    </label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <div class="input-group">
                                                                            <%--<asp:FileUpload ID="FU_Direct_Emp_After" CssClass="form-control" runat="server" />
                                                                                    <asp:HiddenField ID="Hid_Direct_Emp_After_File_Name" runat="server" />
                                                                                    <asp:LinkButton ID="LnkBtn_Upload_Direct_Emp_After_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                         ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="LnkBtn_Delete_Direct_Emp_After_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                         Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>--%>
                                                                            <asp:HyperLink ID="Hyp_View_Direct_Emp_After_Doc" runat="server" Target="_blank"
                                                                                class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                                        </div>
                                                                        <%-- <asp:Label ID="Lbl_Msg_Direct_Emp_After_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                                    Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>--%>
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
                                                                Date of First Fixed Capital Investment (for land/Building/plant and machinery &
                                                                Balancing Equipment)
                                                            </label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <%-- <div class="input-group  date datePicker" id="Div4">--%>
                                                                <asp:Label ID="Txt_FFCI_Date_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>--%>
                                                                <%--</div>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                <asp:Label ID="Lbl_FFCI_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="Hid_FFCI_Before_Doc_Code" runat="server" />
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <div class="input-group">
                                                                    <%--<asp:FileUpload ID="FU_FFCI_Before" CssClass="form-control" runat="server" />
                                                                            <asp:HiddenField ID="Hid_FFCI_Before_File_Name" runat="server" />
                                                                            <asp:LinkButton ID="LnkBtn_Upload_FFCI_Before_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                 ToolTip=""><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LnkBtn_Delete_FFCI_Before_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                                 Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>--%>
                                                                    <asp:HyperLink ID="Hyp_View_FFCI_Before_Doc" runat="server" Target="_blank" class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <%--asp:Label ID="Lbl_Msg_FFCI_Before_Doc" Style="font-size: 12px;" CssClass="text-blue"
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
                                                                                Sl #
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
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                <asp:Label ID="Lbl_Approved_DPR_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="Hid_Approved_DPR_Before_Doc_Code" runat="server" />
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
                                                                Date of First Fixed Capital Investment (for land/Building/plant and machinery &
                                                                Balancing Equipment)
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
                                                            <label class="col-sm-4 col-sm-offset-1">
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
                                                                                Sl #
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
                                                                                <asp:Label runat="server" ID="Label2" Text="Building"></asp:Label>
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
                                                                                <asp:Label runat="server" ID="Label3" Text="Plant & Machinery"></asp:Label>
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
                                                                                <asp:Label runat="server" ID="Label4" Text="Other Fixed Assets"></asp:Label>
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
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                <asp:Label ID="Lbl_Approved_DPR_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="Hid_Approved_DPR_After_Doc_Code" runat="server" />
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
                                                    Means of Finance
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
                                                        <label class="col-sm-4 col-sm-offset-1">
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
                                                <div class="form-group row">
                                                    <label class="col-sm-2">
                                                        FDI(If Any)
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
                                        <div class="panel-heading" role="tab" id="Div2">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#BriefDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-minus">
                                                    </i>Brief Details of Proposed Activity</a>
                                            </h4>
                                        </div>
                                        <div id="BriefDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-3 ">
                                                            <label for="Iname">
                                                                Brief Details of Proposed Activity<span class="text-red">*</span>
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtBriefDtlProposed" MaxLength="200" TabIndex="73" CssClass="form-control"
                                                                runat="server" Width="275.2px" Height="74px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="flt1" runat="server" TargetControlID="txtBriefDtlProposed"
                                                                FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                ValidChars=",,-,/\, ,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 ">
                                                            Proposed Business Plan</label>
                                                        <div class="col-sm-12">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th>
                                                                        Proposed Business Plan
                                                                    </th>
                                                                    <th width="50%">
                                                                        Details there of
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <th>
                                                                        Prospects Downstream Enterprises for utilization of end, intermediate & by e-products
                                                                        for value addition through; if any<span class="text-red">*</span>
                                                                    </th>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPropDwnstrm" MaxLength="250" TabIndex="73" CssClass="form-control"
                                                                            runat="server" Width="578px" Height="74px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtPropDwnstrm"
                                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                            ValidChars=",,-,/\, ,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>
                                                                        Prospects of ancillary enterprises; if any<span class="text-red">*</span>
                                                                    </th>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPropAnci" MaxLength="250" TabIndex="73" CssClass="form-control"
                                                                            runat="server" Width="578px" Height="74px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtPropAnci"
                                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                            ValidChars=",,-,/\, ,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>
                                                                        Development of utility infrastructure like Product-I lines; if any<span class="text-red">*</span>
                                                                    </th>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDevelopUtil" MaxLength="250" TabIndex="73" CssClass="form-control"
                                                                            runat="server" Width="578px" Height="74px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtDevelopUtil"
                                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                            ValidChars=",,-,/\, ,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>
                                                                        Externalities like R & D facilities or technology sourcing mechanism from Technical
                                                                        institutions / university research so as to provide the smaller firm an opportunity
                                                                        to lower their costs, and improve their prospects for future profitability & growth;
                                                                        if any<span class="text-red">*</span>
                                                                    </th>
                                                                    <td>
                                                                        <asp:TextBox ID="txtExternalities" MaxLength="250" TabIndex="73" CssClass="form-control"
                                                                            runat="server" Width="578px" Height="74px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtExternalities"
                                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                            ValidChars=",,-,/\, ,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>
                                                                        Proposed CFC like testing laboratory, training/ skill development centre etc. ;if
                                                                        any<span class="text-red">*</span>
                                                                    </th>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPropCfc" MaxLength="250" TabIndex="73" CssClass="form-control"
                                                                            runat="server" Width="578px" Height="74px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtPropCfc"
                                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                            ValidChars=",,-,/\, ,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>
                                                                        Any Others
                                                                    </th>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOthers" MaxLength="250" TabIndex="73" CssClass="form-control"
                                                                            runat="server" Width="578px" Height="74px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtOthers"
                                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                            ValidChars=",,-,/\, ,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel runat="server" ID="updAdd">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-3 ">
                                                                    <label for="Iname">
                                                                        Proposed common facilities to attract Secondary Tenants
                                                                    </label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label1" runat="server" Text="#"></asp:Label><span class="text-red">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtProposedCommonFacl" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtProposedCommonFacl"
                                                                                    FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                                    ValidChars=",,-,/\, ,.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="lnkAddMorePropCommon" CssClass="btn btn-success " data-toggle="tooltip"
                                                                                    title="Add More" runat="server" OnClick="lnkAddMorePropCommon_Click" OnClientClick="return checkFatalitiesDLSWCA();"><i class="fa fa-plus"></i></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div id="Div11">
                                                                        <asp:HiddenField ID="strUpdateStatusBrief" Value="0" runat="server" />
                                                                        <asp:HiddenField ID="hdnPropCmnFacl" Value="Add" runat="server" />
                                                                        <asp:GridView ID="grdPropCmmnFacl" OnRowDeleting="grdPropCmmnFacl_RowDeleting" runat="server"
                                                                            Width="100%" AutoGenerateColumns="false" class="table table-striped table-bordered table-hover table-responsive"
                                                                            DataKeyNames="intPropComSecTnt,intSlNo">
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-Width="3%">
                                                                                    <HeaderTemplate>
                                                                                        Slno.
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%# Container.DataItemIndex + 1 %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="left">
                                                                                    <HeaderTemplate>
                                                                                        Proposed common facilities to attract Secondary Tenants
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblProductName" Text='<%#Eval("vchPropCommonFacility")%>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-CssClass="noPrint" ItemStyle-CssClass="noPrint">
                                                                                    <HeaderTemplate>
                                                                                        Delete
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="imgbtnDelete" CssClass="btn btn-xs bigger btn-danger" runat="server"
                                                                                            CommandName="Delete"><i class="ace-icon fa fa-trash-o icon-only bigger-110"></i>
                                                                                        </asp:LinkButton>
                                                                                        <asp:HiddenField ID="hdnItr" runat="server" Value='<%#Eval("intPropComSecTnt") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-5">
                                                            <label for="Iname">
                                                                Details of Secondary tenants <span class="text-red">*</span> <a data-toggle="tooltip"
                                                                    class="fieldinfo2" style="padding-left: 10px" title="Sample Text"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="fupDtlOfSecTnt" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'xls,xlsx', 'xls/xlsx', 4);" />
                                                                <asp:LinkButton ID="lnkAddDtlOfSecTnt" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelDtlOfSecTnt" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete File"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="lknViewDtlOfSecTnt" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-blue"
                                                                    runat="server" OnClientClick="JavaScript: return false;" Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.xls/.xlsx file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="hdnDtlOfSecTnt" runat="server" />
                                                            <asp:HiddenField ID="hdnDtlOfSecTntnDocId" runat="server" Value="D161" />
                                                            <asp:Label ID="lblDtlOfSecTnt" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-5">
                                                            <label for="Iname">
                                                                Details of Business plan for attracting Secondary Tenants <span class="text-red">*</span><a
                                                                    data-toggle="tooltip" class="fieldinfo2" style="padding-left: 10px" title="Sample Text">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="fupldDtlBusnessPlanUpload" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                <asp:LinkButton ID="lnkAddDtlBusnessPlan" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelDtlBusnessPlan" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete File"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="lknViewDtlBusnessPlan" data-toggle="tooltip" title="View file"
                                                                    CssClass="input-group-addon bg-blue" runat="server" OnClientClick="JavaScript: return false;"
                                                                    Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="hdnDtlBusnessPlan" runat="server" />
                                                            <asp:HiddenField ID="hdnDtlBusnessPlanDocId" runat="server" Value="D162" />
                                                            <asp:Label ID="lbltlBusnessPlan" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-5">
                                                            <label for="Iname">
                                                                Consent of Secondary Tenant ( if any ) <span class="text-red">*</span><a data-toggle="tooltip"
                                                                    class="fieldinfo2" style="padding-left: 10px" title="Sample Text"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="fupldConsetSecUpload" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                <asp:LinkButton ID="lnkAddConsetSecUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelConsetSecUpload" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete File"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="lknViewConsetSecUpload" data-toggle="tooltip" title="View file"
                                                                    CssClass="input-group-addon bg-blue" runat="server" OnClientClick="JavaScript: return false;"
                                                                    Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="hdnConsetSecUpload" runat="server" />
                                                            <asp:HiddenField ID="hdnConsetSecUploadDocId" runat="server" Value="D163" />
                                                            <asp:Label ID="lblConsetSecUpload" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
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
                                                    href="#DLSWCA" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                    </i>DLSWCA / SLSWCA / HLCA Apporval Details</a>
                                            </h4>
                                        </div>
                                        <div id="DLSWCA" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-5">
                                                            <label for="Iname">
                                                                Date of Approval<span class="text-red">*</span>
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <span class="colon">:</span>
                                                            <div class="input-group  date datePicker" id="Div4">
                                                                <asp:TextBox ID="txtDLSWCADateOfApproval" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Requirement of land approved by DLSWCA / SLSWCA / HLCA (<span class="text-red">In Acre</span>)<span
                                                                class="text-red">*</span></label>
                                                        <div class="col-sm-3">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtDLSWCALandApproved" MaxLength="10" runat="server" onkeypress="return  FloatOnly(event, this);"
                                                                CssClass="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FEtxtDLSWCALandApproved" runat="server" TargetControlID="txtDLSWCALandApproved"
                                                                ValidChars="." FilterMode="ValidChars" FilterType="Custom,Numbers">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Cost of land (<span class="text-red">In Lakhs</span>)<span class="text-red">*</span>
                                                        </label>
                                                        <div class="col-sm-3">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtDLSWCALandCost" MaxLength="10" runat="server" onkeypress="return  FloatOnly(event, this);"
                                                                CssClass="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FEtxtDLSWCALandCost" runat="server" TargetControlID="txtDLSWCALandCost"
                                                                ValidChars="." FilterMode="ValidChars" FilterType="Custom,Numbers">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Eligible Amount of subsidy<%--(Details Calculation Sheet to be Enclosed)--%><span
                                                                class="text-red">*</span></label>
                                                        <div class="col-sm-3">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtDLSWCASubsidyAmt" MaxLength="10" runat="server" onkeypress="return  FloatOnly(event, this);"
                                                                CssClass="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FEtxtDLSWCASubsidyAmt" runat="server" TargetControlID="txtDLSWCASubsidyAmt"
                                                                ValidChars="." FilterMode="ValidChars" FilterType="Custom,Numbers">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Copy of Approval of DLSWCA / SLSWCA / HLCA <span class="text-red">*</span><a data-toggle="tooltip"
                                                                class="fieldinfo2" style="padding-left: 10px" title="Copy of Approval of DLSWCA / SLSWCA / HLCA< (with in 4MB)"><i
                                                                    class="fa fa-question-circle" aria-hidden="true"></i> </a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="fupDLSWCAApprovalDocUpload" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                <asp:LinkButton ID="lnkAddDLSWCAApprovalDoc" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelDLSWCAApprovalDoc" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete File"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="lnkDLSWCAApprovalDocView" data-toggle="tooltip" title="View file"
                                                                    CssClass="input-group-addon bg-blue" runat="server" OnClientClick="JavaScript: return false;"
                                                                    Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="hdnDLSWCAApprovalDoc" runat="server" />
                                                            <asp:HiddenField ID="hdnDLSWCAApprovalDocId" runat="server" Value="D113" />
                                                            <asp:Label ID="lblDLSWCAApprovalDoc" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Copy of Documents to substantiate of land cost <span class="text-red">*</span><a
                                                                data-toggle="tooltip" class="fieldinfo2" style="padding-left: 10px" title="Copy of Documents to substantiate of land cost"><i
                                                                    class="fa fa-question-circle" aria-hidden="true"></i> </a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="fupDLSWCASubstanDocUpload" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                <asp:LinkButton ID="lnkAddDLSWCASubstanDoc" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Uploaad File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelDLSWCASubstanDoc" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete file"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="lnkDLSWCASubstanDocView" data-toggle="tooltip" title="View file"
                                                                    CssClass="input-group-addon bg-blue" runat="server" OnClientClick="JavaScript: return false;"
                                                                    Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="hdnDLSWCASubstanDoc" runat="server" />
                                                            <asp:HiddenField ID="hdnDLSWCASubstanDocId" runat="server" Value="D114" />
                                                            <asp:Label ID="lblDLSWCASubstanDoc" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="Div12">
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
                                                        <label for="Iname" class="col-sm-2 ">
                                                            Account No. of Industrial Unit <span class="text-red">*</span></label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtAccNo" runat="server" CssClass="form-control" MaxLength="16"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAccNo" runat="server"
                                                                Enabled="True" FilterType="Numbers" TargetControlID="txtAccNo" FilterMode="ValidChars"
                                                                ValidChars="0123456789" />
                                                        </div>
                                                        <label for="Iname" class="col-sm-2 ">
                                                            Bank Name <span class="text-red">*</span></label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtBnkNm" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txtBnkNm"
                                                                FilterMode="ValidChars" ValidChars=" " />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-2 ">
                                                            Branch Name <span class="text-red">*</span></label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" MaxLength="50"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtLocation" runat="server"
                                                                Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txtBranch"
                                                                FilterMode="ValidChars" ValidChars=" " />
                                                        </div>
                                                        <label for="Iname" class="col-sm-2 ">
                                                            IFSC Code<span class="text-red">*</span></label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control" MaxLength="18"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtIFSC" runat="server" Enabled="True"
                                                                FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txtIFSC"
                                                                FilterMode="ValidChars" ValidChars="0123456789" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-2 ">
                                                            MICR No.</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtMICRNo" runat="server" CssClass="form-control" MaxLength="9"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txtMICRNo"
                                                                FilterMode="ValidChars" ValidChars="0123456789" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <small class="text-gray">Upload cancelled cheque to verify the entered A/c details<a
                                                                data-toggle="tooltip" class="fieldinfo2" style="padding-left: 10px" title="Upload cancelled cheque to verify the entered A/c details"><i
                                                                    class="fa fa-question-circle" aria-hidden="true"></i> </a></small>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fuBank" CssClass="form-control" runat="server" />
                                                                <asp:HiddenField ID="hdnBank" runat="server" />
                                                                <asp:HiddenField ID="hdnBankID" runat="server" Value="D266" />
                                                                <asp:LinkButton ID="lnkBankUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkOrgDocumentPdf_Click" ToolTip="Click here to upload the file." OnClientClick="return HasFile('fuBank','Please Upload cancelled cheque to verify account details.');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkBankDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypBank" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue"
                                                                    ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.jpg/.jpeg file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblBank" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
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
                                                            Details of Secondary tenants
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgDtlOfSecTnt" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Details of Business plan for attracting Secondary Tenants
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgDtlBusnessPlan" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Consent of Secondary Tenant
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgConsetSecUpload" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Copy of Approval of DLSWCA / SLSWCA / HLCA
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgDLSWCAApprovalDoc" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Copy of Documents to substantiate of land cost
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgDLSWCASubstanDoc" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Cancelled cheque to verify the entered A/c details
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgBank" Height="15" Width="15" src="../images/cancel-square.png" />
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
                                            OnClick="btnApply_Click" OnClientClick="return CheckValidation();" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--</div>--%>
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

                if ($("#hdnPostSubFlag").val() == 1) {
                    $("#txtDLSWCADateOfApproval").datepicker({
                        format: "dd-M-yyyy",
                        autoclose: true
                    }).on("changeDate", function (e) {

                        var fromdate = new Date(e.date);
                        var toDate = new Date(fromdate);
                        toDate.setMonth(fromdate.getMonth() + parseInt($("#hdnTimeFrame").val()));

                        if (dateCheck(fromdate, toDate, new Date())) {
                        }
                        else {

                            jAlert('<strong> Current date must be with in ' + $("#hdnTimeFrame").val() + ' months of date of Approval.</strong>', projname);
                            $("#txtDLSWCADateOfApproval").focus();
                        }
                        return true;
                    });
                }
            });


            $(".currentProduction").keyup(function () {

                var val = 0, newVal = 0;
                $('.currentProduction').each(function () {

                    if ($.trim($(this).val()) != "" && $.trim($(this).val()) != null) {
                        val += parseInt($.trim($(this).val()));
                    }

                })

                $("#txtTotal_Curr").val(val);

            });

            $(".proposedProduction").keyup(function () {

                var val = 0, newVal = 0;
                $('.proposedProduction').each(function () {

                    if ($.trim($(this).val()) != "" && $.trim($(this).val()) != null) {
                        val += parseInt($.trim($(this).val()));
                    }

                })

                $("#txtTotal_Prop").val(val);

            });

            DocCheckList();
            OnChangeApplyBy();

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

            /*====================================added by Ritika lath on 17th Jan 2018 to open correct accordion after postback========================================================*/
            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails, #BriefDetails, #DLSWCA, #BankDetails, #DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").click(function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });
            /*====================================added by Ritika lath to open correct accordion after postback========================================================*/

        } //// end page load function


        function dateCheck(from, to, check) {

            var fDate, lDate, cDate;
            fDate = Date.parse(from);
            lDate = Date.parse(to);
            cDate = Date.parse(check);

            if ((cDate <= lDate && cDate > fDate)) {
                return true;
            }
            return false;
        }

        
    </script>
    <script type="text/javascript">
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
            ImgSrc($('#hdnDtlOfSecTnt').val(), $('#ImgDtlOfSecTnt').attr("id"));
            ImgSrc($('#hdnDtlBusnessPlan').val(), $('#ImgDtlBusnessPlan').attr("id"));
            ImgSrc($('#hdnConsetSecUpload').val(), $('#ImgConsetSecUpload').attr("id"));
            ImgSrc($('#hdnDLSWCAApprovalDoc').val(), $('#ImgDLSWCAApprovalDoc').attr("id"));
            ImgSrc($('#hdnDLSWCASubstanDoc').val(), $('#ImgDLSWCASubstanDoc').attr("id"));
            ImgSrc($('#hdnBank').val(), $('#ImgBank').attr("id"));
        }

    </script>
    </form>
</body>
</html>
