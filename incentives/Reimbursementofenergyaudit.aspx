<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reimbursementofenergyaudit.aspx.cs"
    Inherits="Reimbursementofenergyaudit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <script src="../js/plugin/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../js/plugin/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        .fieldinfo2
        {
            float: right;
            margin-right: 8px;
            font-size: 17px;
            margin-top: 1px;
            color: #3abffb;
        }
    </style>
    <script>
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

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
            ImgSrc($('#hdnDemandDoc').val(), $('#ImgAgreement').attr("id"));
            ImgSrc($('#hdnDateofcompletion').val(), $('#ImgEnergyAuditor').attr("id"));
            ImgSrc($('#hdnDocumentinsupport').val(), $('#ImgImpEnergyAudit').attr("id"));
            ImgSrc($('#hdnAuditorDoc').val(), $('#ImgProfileAuditor').attr("id"));
            ImgSrc($('#hdnAccreditationDoc').val(), $('#ImgAccreditation').attr("id"));
            ImgSrc($('#hdnExpenditureDoc').val(), $('#ImgExpenditure').attr("id"));
            ImgSrc($('#hdnDocument').val(), $('#ImgEnergyExpenses').attr("id"));
            ImgSrc($('#hdnCertificate').val(), $('#Imgpartyagency').attr("id"));
            ImgSrc($('#hdnCarbonFootPrt').val(), $('#Imgredution').attr("id"));
            ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#ImgAssistance').attr("id"));
            ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#ImgNonAvailment').attr("id"));
            ImgSrc($('#D275').val(), $('#ImgOSPCB').attr("id"));
            ImgSrc($('#D274').val(), $('#ImgSectRel').attr("id"));
            ImgSrc($('#D280').val(), $('#ImgCleanApproveAuthority').attr("id"));
        }

        /*----------------------------------------------------------*/

        function checkPostSubDate() {

            if ($("#hdnPostSubFlag").val() == 1) {
                if ($("#txtComptionDate").val() != '') {
                    var currDate = new Date();
                    var supplyDate = new Date($("#txtComptionDate").val());
                    var TimeFrame = 0 - parseInt($("#hdnTimeFrame").val());

                    var fromDate = new Date().addMonths(TimeFrame);
                    if (supplyDate > fromDate && supplyDate <= currDate) {
                        return true;
                    }
                    else {
                        jAlert('<strong>Date of completion of successful implementation of energy audit must be within ' + $("#hdnTimeFrame").val() + ' months before current date !!</strong>', projname);
                        $("#txtComptionDate").val('');
                        return false;
                    }
                }
            }
            return false;
        }

        Date.prototype.addMonths = function (m) {
            var d = new Date(this);
            var years = Math.floor(m / 12);
            var months = m - (years * 12);
            if (years) d.setFullYear(d.getFullYear() + years);
            if (months) d.setMonth(d.getMonth() + months);
            return d;
        }

        /*----------------------------------------------------------*/
        function pageLoad() {

            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy",
                    clearBtn: true
                });
            });

            ////--------commented
            DocCheckList();
            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #IndustryDetails, #ContractDemand, #EnergyAuditorwithDetails, #AvailedClaimDetails, #AdditionalDocuments").removeClass('in');
                $(hdn).addClass("in");
            }
        }

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
        function OnChangeApplyBy() {

            $('.attorneysec,.adhardetails').hide();
            if ($("input[name='radApplyBy']:checked").val() == '1') {
                $('.adhardetails').show();
                $('.attorneysec').hide();
                ImgSrc('', $('#ImgSign').attr("id"));
            }
            else {
                $('.attorneysec').show();
                $('.adhardetails').hide();
                ImgSrc($('#hdnAUTHORIZEDFILE').val(), $('#ImgSign').attr("id"));
            }
        }
        function OnChangeAvailed() {
            $('.availgroup1,.availgroup2').hide();
            if ($("input[name='RadBtn_Availed_Earlier']:checked").val() == '1') {
                $('.availgroup1').show();
                $('.availgroup2').hide();
                ImgSrc('', $('#ImgNonAvailment').attr("id"));
                ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#ImgAssistance').attr("id"));
            }
            else {
                $('.availgroup2').show();
                $('.availgroup1').hide();
                ImgSrc('', $('#ImgAssistance').attr("id"));
                ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#ImgNonAvailment').attr("id"));
            }
        }
        function CheckEnergyConsumption() {

            if ($("#txtBeforeAudit").val() <= $("#txtAfterAudit").val()) {
                // $("#txtAfterAudit").val('');
                $("#txtAfterAudit").focus();
                jAlert('Energy consumption after audit should be lesser compared to the consumption before audit. You may not be eligible .Please recheck entered values .', projname);
                return false;
            }

        }


        function checkdate(id) {
            var dt = document.getElementById(id);
            if (id.value != '') {
                if (validatedate(id) == false) {
                    dt.value = '';
                    dt.focus()
                    return false
                }
                return true
            }
            return true
        }

        // To check decimal (controlId, DecimalPlaces)
        function CheckDecimal(e, t) { try { var n = ""; var r; if (parseInt(t)) { r = t } else { r = 2 } var i = document.getElementById(e); if (i == "undefined" || i == null) { i = e } if (typeof i.value === "undefined") { n = i.innerHTML.trim() } else { n = i.value.trim() } if (n.split(".").length - 1 > 1 || n.charAt(n.length - 1) == "." || n.charAt(0) == ".") { if (typeof i.value === "undefined") { setTimeout(function () { jAlert("Please enter valid decimal !", projname); $("#" + i.getAttribute("id")).effect("shake", { direction: "left", times: 2, distance: 5 }, 800) }, 1) } else { setTimeout(function () { alert("Please enter valid decimal !"); $(i).focus() }, 1) } return false } else { if (n.substr(n.lastIndexOf(".") + 1, n.length).length > r && n.lastIndexOf(".") > -1) { if (typeof i.value === "undefined") { setTimeout(function () { alert("Only " + r + " digits are allowed after decimal !"); $("#" + i.getAttribute("id")).effect("shake", { direction: "left", times: 2, distance: 5 }, 800) }, 1) } else { setTimeout(function () { alert("Only " + r + " digits are allowed after decimal !"); $(i).focus() }, 1) } return false } else { return true } } } catch (s) { } }

        // To make decimal (controlId, DecimalPlace)
        function makeDecimal(e, t) { var n = document.getElementById(e); var r; if (parseInt(t)) { r = t } else { r = 2 } if (n == "undefined" || n == null) { n = e } if (typeof n.value === "undefined") { if (n.innerHTML.trim().length > 0) { n.innerHTML = parseFloat(n.innerHTML.trim()).toFixed(r) } } else { if (n.value.trim().length > 0) { n.value = parseFloat(n.value.trim()).toFixed(r) } } }



        function validatedate(id) {
            var txtTest = id.value;
            try {
                var date = Date.parse(txtTest).toString();
                var pos = date.toString().search("Invalid");
                if (date != "NaN") {
                    var date1 = new Date(txtTest);
                    var curr_date = date1.getDate();
                    var curr_month = date1.getMonth();
                    var curr_year = date1.getFullYear();
                    if (curr_year >= 1900 && curr_year <= 2999) {
                        id.value = curr_month + "/" + curr_date + "/" + curr_year;
                        return true;
                    }
                    else {
                        jAlert('Invalid Date.', projname);
                        id.value = '';
                        id.focus();
                        return false;
                    }
                }
                else {
                    jAlert('Invalid Date.', projname);
                    id.value = '';
                    id.focus();
                    return false;
                }
            }
            catch (err) {
                jAlert('Invalid Date.', projname);
                return false;
            }
        }


        function AvailValidation() {

            if (!($('#RadBtn_Availed_Earlier_0').is(':checked') || $('#RadBtn_Availed_Earlier_1').is(':checked'))) {
                jAlert('Please select Subsidy/Incentive against the details in this application been availed earlier option', projname);
                return false;
            }


            if ($('#RadBtn_Availed_Earlier_0').is(':checked')) {
                var rowcount = $("#grdAssistanceDetailsAD tr").length;
                if (rowcount == "0") {
                    jAlert('<strong>Please enter atleast one Items of Details of Subsidy Already availed.</strong>', projname);
                    return false;
                }
                if ($("#Hid_Asst_Sanc_File_Name").val() == '') {
                    jAlert('Please upload Document details of assistance sanctioned .', projname);
                    return false;
                }
                if (!blankFieldValidation('txtdiffclaimamt', 'Amount of Differential Claim to be Exempted', projname)) {
                    return false;
                }

            }
            if ($('#RadBtn_Availed_Earlier_1').is(':checked')) {
                if ($("#Hid_Undertaking_File_Name").val() == '') {
                    jAlert('Please upload Document details of Undertaking on non-availment of subsidy.', projname);
                    return false;
                }
            }
            if (!blankFieldValidation('txtreimamt', 'Present Claim for reimbursement', projname)) {
                return false;
            }

            return true;
        }

        $(document).ready(function () {

            $(".panel-title > a").on("click", function () {
                //hdnVisibleAcc
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });


            /*---------------------------------------------------------------*/
            //// Hide Documents if not available

            if ($("#Lbl_Term_Loan_Doc_Name").text() == '') {
                $("#Div_Term_Loan_Doc").hide();
            }

            if ($("#Lbl_Pioneer_Doc_Name").text() == '') {
                $("#Div_Pioneer_Doc").hide();
            }



            $('#btnApply').click(function (e) {

                var rbtnVal = 0;
                // Industrial Unit's Details

                if (!DropDownValidation('DdlGender', '0', 'Applicant Salutation', projname)) {
                    return false;
                }
                if (!blankFieldValidation('TxtApplicantName', 'Name of Applicant', projname)) {
                    return false;
                };
                if (!WhiteSpaceValidation1st('TxtApplicantName', 'Name of Applicant', projname)) {
                    return false;
                };
                if (!WhiteSpaceValidationLast('TxtApplicantName', 'Name of Applicant', projname)) {
                    return false;
                };
                if (!SpecialCharacter1st('TxtApplicantName', 'Name of Applicant', projname)) {
                    return false;
                };
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
                            jAlert('Aadhaar no should be 12 digits.', projname);
                            return false;
                        }
                    }
                    else if (rbtnVal == "2") {
                        if (blankFieldValidation('hdnAUTHORIZEDFILE', 'Please upload Authorizing letter signed by Authorized Signatory !', projname) == false) {
                            return false;
                        }
                    }
                }


                //Contract Demand / Connected( In KVA)
                if (!blankFieldValidation('txtconnectedload', 'Contract Demand / Connected( In KVA)', projname)) {
                    return false;
                };
                // Consumer No.
                if (!blankFieldValidation('txtconsumenumber', ' Consumer No.', projname)) {
                    return false;
                };

                if ($("#hdnDemandDoc").val() == '') {

                    jAlert('Please provide document of Agreement between power distribution company and the Industrial Unit/Enterprise. ', projname);
                    return false;
                }




                //Name of Energy Auditor / Organization
                if (!blankFieldValidation('txtAuditorName', 'Name of Energy Auditor / Organization', projname)) {
                    return false;
                };
                if (!WhiteSpaceValidation1st('txtAuditorName', 'Name of Energy Auditor / Organization', projname)) {
                    return false;
                };
                if (!WhiteSpaceValidationLast('txtAuditorName', 'Name of Energy Auditor / Organization', projname)) {
                    return false;
                };
                if (!SpecialCharacter1st('txtAuditorName', 'Name of Energy Auditor / Organization', projname)) {
                    return false;
                };
                //Address of Energy Auditor / Organization
                if (!blankFieldValidation('txtEnergyAuditorAddress', 'Address of Energy Auditor / Organization', projname)) {
                    return false;
                };
                if (!WhiteSpaceValidation1st('txtEnergyAuditorAddress', 'Address of Energy Auditor / Organization', projname)) {
                    return false;
                };
                if (!WhiteSpaceValidationLast('txtEnergyAuditorAddress', 'Address of Energy Auditor / Organization', projname)) {
                    return false;
                };
                if (!SpecialCharacter1st('txtEnergyAuditorAddress', 'Address of Energy Auditor / Organization', projname)) {
                    return false;
                };



                //Accreditation of the Auditor
                if (!blankFieldValidation('txtAuditorAccreditation', 'Accreditation of the Auditor', projname)) {
                    return false;
                };
                if (!WhiteSpaceValidation1st('txtAuditorAccreditation', 'Accreditation of the Auditor', projname)) {
                    return false;
                };
                if (!WhiteSpaceValidationLast('txtAuditorAccreditation', 'Accreditation of the Auditor', projname)) {
                    return false;
                };
                if (!SpecialCharacter1st('txtAuditorAccreditation', 'Accreditation of the Auditor', projname)) {
                    return false;
                };


                //Expenditure incurred
                if (!blankFieldValidation('txtExpenditure', 'Expenditure incurred', projname)) {
                    return false;
                };

                //	Date of completion of successful implementation Energy Audit
                if (!blankFieldValidation('txtComptionDate', '	Date of completion of successful implementation Energy Audit', projname)) {
                    return false;
                };

                //Report of Energy Auditor
                if ($("#hdnDateofcompletion").val().length == 0) {
                    if ($("#fileDateofcompletion").val().length == 0) {
                        jAlert('<strong>Please provide document of Report of Energy Auditor !</strong>', projname);
                        return false;
                    }
                }

                //  Document in support of implementation of Energy Audit Report
                if ($("#hdnDocumentinsupport").val().length == 0) {
                    if ($("#FileDocumentinsupport").val().length == 0) {
                        jAlert('<strong>Please provide Document in support of implementation of Energy Audit Report !</strong>', projname);
                        return false;
                    }
                }

                // Profile of Energy Auditor
                if ($("#hdnAuditorDoc").val().length == 0) {
                    if ($("#fuAuditorDoc").val().length == 0) {
                        jAlert('<strong>Please provide document of Profile of Energy Auditor !</strong>', projname);
                        return false;
                    }
                }

                // Accreditation of the Auditor Doc
                if ($("#hdnAccreditationDoc").val().length == 0) {
                    if ($("#fuAuditorAccreditationDoc").val().length == 0) {
                        jAlert('<strong>Please provide document of Accreditation of the Auditor Doc !</strong>', projname);
                        return false;
                    }
                }

                // Expenditure incurred Doc
                if ($("#hdnExpenditureDoc").val().length == 0) {
                    if ($("#fuExpenditureDoc").val().length == 0) {
                        jAlert('<strong>Please provide document of Expenditure incurred Doc !</strong>', projname);
                        return false;
                    }
                }

                //Energy Consumption Before Audit
                if (!blankFieldValidation('txtBeforeAudit', 'Energy Consumption Before Audit', projname)) {
                    return false;
                };

                //Energy Consumption After Audit
                if (!blankFieldValidation('txtAfterAudit', 'Energy Consumption After Audit', projname)) {
                    return false;
                };

                if (parseFloat($("#txtBeforeAudit").val()) <= parseFloat($("#txtAfterAudit").val())) {
                    //  $("#txtAfterAudit").val('');

                    jAlert('Energy consumption after audit should be lesser compared to the consumption before audit. You may not be eligible. Please recheck entered values.', projname);
                    return false;
                }
                // Document(s) / proof on reduction of Energy expenses. 
                if ($("#hdnDocument").val().length == 0) {
                    if ($("#FileDocument").val().length == 0) {
                        jAlert('<strong>Please provide document of Document(s) / proof on reduction of Energy expenses !</strong>', projname);
                        return false;
                    }
                }

                // Certificate on energy efficiency by independent and credible third party agency
                if ($("#hdnCertificate").val().length == 0) {
                    if ($("#FileCertificate").val().length == 0) {
                        jAlert('<strong>Please provide document of Certificate on energy efficiency by independent and credible third party agency !</strong>', projname);
                        return false;
                    }
                }

                if (!AvailValidation()) {
                    return false;
                }





            });


            OnChangeApplyBy();
            OnChangeAvailed();

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
            $('.Pioneersec').hide();
        });




        function validformindraft() {

            if ($('#TxtAdhaar1').val() != '') {
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('Aadhaar no should be 12 digits.', '');
                    return false;
                }
            }



        }
    </script>
    <script type="text/javascript" language="javascript">

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


        function HasFile(fuControlId, strError) {
            if ($('#' + fuControlId).val() == "") {
                jAlert(strError, projname);
                return false;
            }
        }
        function calculetotal() {

            var cal = 0;
            var cal2 = 0;
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

            if ($("#txtLandtypeEMD").val() != '') {
                cal2 = cal2 + parseFloat($("#txtLandtypeEMD").val());

            }

            if ($("#txtBuildingEMD").val() != '') {
                cal2 = cal2 + parseFloat($("#txtBuildingEMD").val());

            }
            if ($("#txtPlantMachineryEMD").val() != '') {
                cal2 = cal2 + parseFloat($("#txtPlantMachineryEMD").val());
            }

            if ($("#txtBalancingEquipmentEMD").val() != '') {
                cal2 = cal2 + parseFloat($("#txtBalancingEquipmentEMD").val());
            }


            if ($("#txtOtherFixedAssestsEMD").val() != '') {
                cal2 = cal2 + parseFloat($("#txtOtherFixedAssestsEMD").val());
            }

            $("#lblTotalAmount").text(cal);
            $("#lblTotalAmountEMD").text(cal2);

            $("#lblTotalAmount").text(cal);

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
    <style>
        .not-active, .not-active2
        {
            pointer-events: none;
            cursor: default;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div id="exTab1">
                <div class="investrs-tab">
                    <uc5:pealmenu ID="Peal" runat="server" />
                </div>
                <div class="tab-content clearfix">
                    <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
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
                                <h2>
                                    Application For
                                    <asp:Label ID="lblHeader" runat="server" Text="One Time Reimbursement Of Energy Audit Cost Under IPR 2015"></asp:Label></h2>
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
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                                FilterType="Numbers" TargetControlID="TxtAdhaar1" FilterMode="ValidChars" ValidChars="0123456789" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group attorneysec" id="divAuthorizing" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            <small class="text-gray">Please provide Authorizing letter signed by Authorized Signatory</small>
                                                            <a data-toggle="tooltip" class="fieldinfo2" title="Provide Authorizing letter signed by Authorized Signatory">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <asp:Label runat="server" ID="lblAuthorizing"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hidAuthorizing" />
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FlupAUTHORIZEDFILE" onchange="return FileCheck(this, 'pdf','pdf', 4);"
                                                                    runat="server" CssClass="form-control" />
                                                                <asp:HiddenField ID="hdnAUTHORIZEDFILE" runat="server" />
                                                                <asp:LinkButton ID="lnkAUTHORIZEDFILE" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" ToolTip="Click here to upload the file."
                                                                    OnClientClick="return HasFile('FlupAUTHORIZEDFILE',' Please provide Authorizing letter signed by Authorized Signatory');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkAUTHORIZEDFILEDdelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
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
                                                                <asp:Label ID="lbl_Prod_Comm_Date_Before" runat="server" CssClass="form-control-static" />
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
                                                                <asp:Label ID="lbl_PC_Issue_Date_Before" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                                <asp:Label ID="lbl_Prod_Comm_Date_After" runat="server" CssClass="form-control-static" />
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
                                                                <asp:Label ID="lbl_PC_Issue_Date_After" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                            <label class="col-sm-5">
                                                                <small class="text-gray">
                                                                    <asp:Label ID="Lbl_FFCI_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                    <asp:HiddenField ID="Hid_FFCI_After_Doc_Code" runat="server" />
                                                                </small>
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
                                        <div class="panel-heading" role="tab" id="Div4">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#ContractDemand" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                    </i>Contract Demand / Connected load Details </a>
                                            </h4>
                                        </div>
                                        <div id="ContractDemand" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <p class="text-red text-right">
                                                    All Amounts to be entered in INR(in Lakhs)</p>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th>
                                                                        Contract Demand / Connected Load( In KVA) <a data-toggle="tooltip" class="fieldinfo2"
                                                                            title="Contract Demand / Connected Load( In KVA)"><i class="fa fa-question-circle"
                                                                                aria-hidden="true"></i></a>
                                                                    </th>
                                                                    <th>
                                                                        Consumer No. <a data-toggle="tooltip" class="fieldinfo2" title="Consumer No."><i
                                                                            class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtconnectedload" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars"
                                                                            FilterType="Numbers" ValidChars="1234567890" TargetControlID="txtconnectedload" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtconsumenumber" MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="flt1" runat="server" TargetControlID="txtconsumenumber"
                                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                            ValidChars=",,-,/\, ,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Agreement between power distribution company and the Industrial Unit/Enterprise
                                                            <a data-toggle="tooltip" class="fieldinfo2" title="Agreement between power distribution company and the Industrial Unit/Enterprise">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="flDemandDoc" runat="server" data-toggle="tooltip" CssClass="form-control"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                <asp:LinkButton ID="lnkDemandDocUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" OnClientClick="return HasFile('flDemandDoc','Please provide document of Agreement between power distribution company and the Industrial Unit/Enterprise');"
                                                                    ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDemandDocDelet" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink runat="server" ID="hypDemandDoc" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue"
                                                                    ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:HiddenField ID="hdnDemandDoc" runat="server" />
                                                            <asp:HiddenField ID="hdnDemandDocId" runat="server" Value="D271" />
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblDemandDoc" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="Div5">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#EnergyAuditorwithDetails" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-minus"></i>Energy Audit Details </a>
                                            </h4>
                                        </div>
                                        <div id="EnergyAuditorwithDetails" class="panel-collapse collapse in" role="tabpanel"
                                            aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12 ">
                                                            Energy Audit Details</label>
                                                        <div class="col-sm-12">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th colspan="2">
                                                                                Name of Energy Auditor / Organization
                                                                            </th>
                                                                            <th>
                                                                                Address of Energy Auditor / Organization
                                                                            </th>
                                                                            <th colspan="2">
                                                                                Accreditation of the Auditor
                                                                            </th>
                                                                            <th colspan="2">
                                                                                Expenditure incurred<a data-toggle="tooltip" class="fieldinfo2" title="Expenditure incurred"><i
                                                                                    class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                            </th>
                                                                            <th>
                                                                                Date of completion of successful implementation Energy Audit <a data-toggle="tooltip"
                                                                                    class="fieldinfo2" title="Date of completion of successful implementation Energy Audit">
                                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAuditorName" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtAuditorName"
                                                                                    FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                                    ValidChars=",,-,/\, ,.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtEnergyAuditorAddress" CssClass="form-control" runat="server"
                                                                                    MaxLength="200"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtEnergyAuditorAddress"
                                                                                    FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                                    ValidChars=",,-,/\, ,.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAuditorAccreditation" CssClass="form-control" runat="server"
                                                                                    MaxLength="40"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtAuditorAccreditation"
                                                                                    FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                                    ValidChars=",,-,/\, ,.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExpenditure" MaxLength="10" CssClass="form-control" onblur="return makeDecimal(this.id);CheckDecimal(this.id);"
                                                                                    runat="server"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                                                                    InvalidChars="." TargetControlID="txtExpenditure" />
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:HiddenField ID="hdnPostSubFlag" runat="server" />
                                                                                <asp:HiddenField ID="hdnTimeFrame" runat="server" />
                                                                                <div class="input-group  date datePicker" id="Div6">
                                                                                    <asp:TextBox ID="txtComptionDate" runat="server" CssClass="form-control" onchange="return checkPostSubDate();"></asp:TextBox>
                                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Report of Energy Auditor <a data-toggle="tooltip" class="fieldinfo2" title="Report of Energy Auditor">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fileDateofcompletion" data-toggle="tooltip" CssClass="form-control"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" runat="server" />
                                                                <asp:LinkButton ID="lnkDateofcompletionUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" OnClientClick="return HasFile('fileDateofcompletion','Please provide document of Report of Energy Auditor');"
                                                                    ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDateofcompletionDelet" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="HydDateofcompletion" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:HiddenField ID="hdnDateofcompletion" runat="server" />
                                                            <asp:HiddenField ID="hdnDateofcompletionID" runat="server" Value="D152" />
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblDateofcompletion" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Document in support of implementation of Energy Audit Report <a data-toggle="tooltip"
                                                                class="fieldinfo2" title="Document in support of implementation of Energy Audit Report">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileDocumentinsupport" CssClass="form-control" data-toggle="tooltip"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" runat="server" />
                                                                <asp:LinkButton ID="lnkDocumentinsupportUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" OnClientClick="return HasFile('FileDocumentinsupport','Please provide Document in support of implementation of Energy Audit Report');"
                                                                    ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDocumentinsupportDelet" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink runat="server" ID="hydDocumentinsupport" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:HiddenField ID="hdnDocumentinsupport" runat="server" />
                                                            <asp:HiddenField ID="hdnDocumentinsupportID" runat="server" Value="D155" />
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblDocumentinsupport" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Profile of Energy Auditor<a data-toggle="tooltip" class="fieldinfo2" title="Profile of Energy Auditor">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fuAuditorDoc" CssClass="form-control" data-toggle="tooltip" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);"
                                                                    runat="server" />
                                                                <asp:LinkButton ID="lnkAuditorDocUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" OnClientClick="return HasFile('fuAuditorDoc','Please provide document of Profile of Energy Auditor');"
                                                                    ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkAuditorDocDelet" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hplAuditorDoc" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:HiddenField ID="hdnAuditorDoc" runat="server" />
                                                            <asp:HiddenField ID="hdnAuditorDocId" runat="server" Value="D156" />
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblAuditorDoc" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Accreditation of the Auditor Doc<a data-toggle="tooltip" class="fieldinfo2" title="Accreditation of the Auditor Doc">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fuAuditorAccreditationDoc" CssClass="form-control" data-toggle="tooltip"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" runat="server" />
                                                                <asp:LinkButton ID="lnkAccreditationDocUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" OnClientClick="return HasFile('fuAuditorAccreditationDoc','Please provide document of Accreditation of the Auditor Doc');"
                                                                    ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkAccreditationDocDelet" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hplAuditorAccreditationDoc" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:HiddenField ID="hdnAccreditationDoc" runat="server" />
                                                            <asp:HiddenField ID="hdnAccreditationDocId" runat="server" Value="D180" />
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblAccreditationDoc" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Expenditure incurred Doc<a data-toggle="tooltip" class="fieldinfo2" title="Expenditure incurred Doc">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fuExpenditureDoc" CssClass="form-control" data-toggle="tooltip"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" runat="server" />
                                                                <asp:LinkButton ID="lnkExpenditureDocUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" OnClientClick="return HasFile('fuExpenditureDoc','Please provide document of Expenditure incurred Doc');"
                                                                    ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkExpenditureDocDelet" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hplExpenditureDoc" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:HiddenField ID="hdnExpenditureDoc" runat="server" />
                                                            <asp:HiddenField ID="hdnExpenditureDocId" runat="server" Value="D165" />
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblExpenditureDoc" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12">
                                                            Energy Consumption (KWH)</label>
                                                        <div class="col-sm-12">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th>
                                                                        Before Audit <a data-toggle="tooltip" class="fieldinfo2" title="Before Audit"><i
                                                                            class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                    </th>
                                                                    <th>
                                                                        After Audit <a data-toggle="tooltip" class="fieldinfo2" title="After Audit"><i class="fa fa-question-circle"
                                                                            aria-hidden="true"></i></a>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtBeforeAudit" CssClass="form-control" runat="server" MaxLength="10"
                                                                            onkeypress="return FloatOnly(event, this);" onblur="return makeDecimal(this.id);CheckDecimal(this.id);"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers,Custom"
                                                                            InvalidChars="." TargetControlID="txtBeforeAudit" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAfterAudit" CssClass="form-control" runat="server" MaxLength="10"
                                                                            onkeypress="return FloatOnly(event, this);" onblur="return makeDecimal(this.id);CheckDecimal(this.id);"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                                                            InvalidChars="." TargetControlID="txtAfterAudit" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Document(s) / proof on reduction of Energy expenses. <a data-toggle="tooltip" class="fieldinfo2"
                                                                title="Document(s) / proof on reduction of Energy expenses"><i class="fa fa-question-circle"
                                                                    aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileDocument" CssClass="form-control" data-toggle="tooltip" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);"
                                                                    runat="server" />
                                                                <asp:LinkButton ID="lnkDocumentUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" OnClientClick="return HasFile('FileDocument','Please provide document of Document(s) / proof on reduction of Energy expenses');"
                                                                    ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDocumentDelet" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="HydDocument" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue"
                                                                    ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:HiddenField ID="hdnDocument" runat="server" />
                                                            <asp:HiddenField ID="hdnDocumentId" runat="server" Value="D154" />
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblDocument" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Certificate on energy efficiency by independent and credible third party agency
                                                            <a data-toggle="tooltip" class="fieldinfo2" title="Certificate on energy efficiency by independent and credible third party agency">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileCertificate" CssClass="form-control" data-toggle="tooltip"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" runat="server" />
                                                                <asp:LinkButton ID="lnkCertificateUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" OnClientClick="return HasFile('FileCertificate','Please provide document of Certificate on energy efficiency by independent and credible third party agency');"
                                                                    ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkCertificateDelet" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hydCertificate" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:HiddenField ID="hdnCertificate" runat="server" />
                                                            <asp:HiddenField ID="hdnCertificateId" runat="server" Value="D127" />
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblCertificate" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Documents in support of reduction of carbon footprint (if applicable) by independent
                                                            and credible third party agency <a data-toggle="tooltip" class="fieldinfo2" title="Documents in support of reduction of carbon footprint (if applicable)
                                                                by independent and credible third party agency"><i class="fa fa-question-circle"
                                                                    aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileCarbonFootPrt" CssClass="form-control" data-toggle="tooltip"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" runat="server" />
                                                                <asp:LinkButton ID="lnkCarbonFootPrtUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" OnClientClick="return HasFile('FileCarbonFootPrt','Please provide document of reduction of carbon footprint (if applicable)
                                                                by independent and credible third party agency');" ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkCarbonFootPrtDelet" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypCarbonFootPrt" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:HiddenField ID="hdnCarbonFootPrt" runat="server" />
                                                            <asp:HiddenField ID="hdnCarbonFootPrtID" runat="server" Value="D272" />
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblCarbonFootPrt" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
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
                                                    href="#AvailedClaimDetails" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-plus"></i>Availed Details</a>
                                            </h4>
                                        </div>
                                        <div id="AvailedClaimDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Present Claim for reimbursement <a data-toggle="tooltip" class="fieldinfo2" title="Present Claim for reimbursement">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtreimamt" runat="server" CssClass="form-control" MaxLength="15"
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
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:RadioButtonList ID="RadBtn_Availed_Earlier" class="applyby" runat="server" RepeatDirection="Horizontal"
                                                                onchange="return OnChangeAvailed();">
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div class="form-group availdetailsec availgroup1">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-12 ">
                                                                    Details of Subsidy Already availed
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
                                                                                <asp:TextBox ID="txtagency" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender556" runat="server" TargetControlID="txtagency"
                                                                                    FilterMode="ValidChars" FilterType="Custom,UppercaseLetters,LowercaseLetters"
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
                                                                        AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false" OnRowDeleting="grdAssistanceDetailsAD_RowDeleting">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%"></ItemStyle>
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
                                                                                        runat="server" ToolTip="Remove"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                                    <asp:HiddenField ID="hdnRowId" runat="server" Value='<%#Eval("dcRowId")%>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="7%" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="form-group  availuploadsec availgroup1">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Document details of assistance sanctioned<a data-toggle="tooltip" class="fieldinfo2"
                                                                title="Document details of assistance sanctioned"> <i class="fa fa-question-circle"
                                                                    aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnSupportingDocsID" runat="server" Value="D253" />
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FU_Asst_Sanc_Doc" data-toggle="tooltip" CssClass="form-control"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" runat="server" />
                                                                <asp:HiddenField ID="Hid_Asst_Sanc_File_Name" runat="server" />
                                                                <asp:LinkButton ID="LnkBtn_Upload_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" ToolTip="Click here to upload the file."
                                                                    OnClientClick="return HasFile('FU_Asst_Sanc_Doc','Please Upload Document details of assistance sanctioned');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LnkBtn_Delete_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
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
                                                            Undertaking on non-availment of subsidy earlier on this project <a data-toggle="tooltip"
                                                                class="fieldinfo2" title="Undertaking on non-availment of subsidy earlier on this project">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnSubsidyAvailedID" runat="server" Value="D230" />
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FU_Undertaking_Doc" CssClass="form-control" data-toggle="tooltip"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" runat="server" />
                                                                <asp:HiddenField ID="Hid_Undertaking_File_Name" runat="server" />
                                                                <asp:LinkButton ID="LnkBtn_Upload_Undertaking_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" ToolTip="Click here to upload the file."
                                                                    OnClientClick="return HasFile('FU_Undertaking_Doc','Please Upload Undertaking on non-availment of subsidy earlier on this project.');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LnkBtn_Delete_Undertaking_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
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
                                                <div class="form-group availgroup1">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Amount of Differential Claim to be Exempted <a data-toggle="tooltip" class="fieldinfo2"
                                                                title="Amount of Differential Claim to be Exempted"><i class="fa fa-question-circle"
                                                                    aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtdiffclaimamt" runat="server" CssClass="form-control" MaxLength="15"
                                                                onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender116" runat="server" TargetControlID="txtdiffclaimamt"
                                                                FilterType="Custom,Numbers" ValidChars="." />
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
                                                    href="#InterestSubsidyDetails" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-plus"></i>Additional Documents</a>
                                            </h4>
                                        </div>
                                        <div id="InterestSubsidyDetails" class="panel-collapse collapse" role="tabpanel"
                                            aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="" id="div2" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-0">
                                                            OSPCB consent to operate <a data-toggle="tooltip" class="fieldinfo2" title="Except white category">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flValidStatutary" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'zip', 'zip', 4);" />
                                                                <asp:HiddenField ID="D275" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnIsOsPCBDownloaded" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUValidStatutary" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flValidStatutary','Please Upload OSPCB consent to operate related Document');"
                                                                    OnClick="lnkUValidStatutary_click" ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i> </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDValidStatutary" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDValidStatutary_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypValidStatutary" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblValidStatutary" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="" id="div3" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-0">
                                                            Sector Relevant Document <a data-toggle="tooltip" class="fieldinfo2" title="1. FSSAI/Food License- For food processing unit 
                                                                                                            2. Explosive License -For Explosive manufacturing unit 
                                                                                                            3. BIS Certification -For Packaged drinking water">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flDelay" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                <asp:HiddenField ID="D274" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUDelay" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flDelay','Please Upload Sector Relevant Document');"
                                                                    OnClick="lnkUDelay_Click" ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i> </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDDelay" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDDelay_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypDelay" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblDelay" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-0">
                                                            Factory & Boiler - For all industry (10 with direct employment / 20 no of employment
                                                            without power ) <a data-toggle="tooltip" class="fieldinfo2" title="Factory & Boiler-  For all industry (10 with direct employment / 20 no of employment with power ) ">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flCleanApproveAuthority" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this, 'zip', 'zip', 4);" />
                                                                <asp:HiddenField ID="D280" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnBoilerDownloaded" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUCleanApproveAuthority" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Please Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file." OnClick="lnkUCleanApproveAuthority_Click"><i class="fa fa-upload" aria-hidden="true"></i> </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDCleanApproveAuthority" runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false" OnClick="lnkDCleanApproveAuthority_Click"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypCleanApproveAuthority" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblCleanApproveAuthority" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
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
                                                            Agreement between power distribution company and the Industrial Unit/Enterprise
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgAgreement" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Report of Energy Auditor
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgEnergyAuditor" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Document in support of implementation of Energy Audit Report
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgImpEnergyAudit" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Profile of Energy Auditor
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgProfileAuditor" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Accreditation of the Auditor Doc
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgAccreditation" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Expenditure incurred Doc
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgExpenditure" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Document(s) / proof on reduction of Energy expenses.
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgEnergyExpenses" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Certificate on energy efficiency by independent and credible third party agency
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="Imgpartyagency" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Documents in support of reduction of carbon footprint (if applicable) by independent
                                                            and credible third party agency
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="Imgredution" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Document details of assistance sanctioned
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgAssistance" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Undertaking on non-availment of subsidy earlier on this project
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgNonAvailment" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            OSPCB consent to operate
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgOSPCB" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Sector Relevant Document
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgSectRel" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Factory & Boiler - For all industry (10 with direct employment / 20 no of employment
                                                            without power )
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgCleanApproveAuthority" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-footer">
                            <div class="row">
                                <div class="col-sm-12 text-right">
                                    <asp:Button ID="btnSaveasDraft" runat="server" Text="Save As Draft" CssClass="btn btn-warning"
                                        OnClientClick="return validformindraft();" OnClick="btnEdit_Click" />
                                    <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-success"
                                        OnClick="btnApply_Click" />
                                </div>
                            </div>
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
        function OpenUploadModal(controlId) {

            $("#" + controlId.id).click();
            return false;
        };

       
       
    </script>
    </form>
</body>
</html>
