<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quality_Certification.aspx.cs"
    Inherits="incentives_Quality_Certification100" %>

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
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        $(document).ready(function () {

            OnChangeAvailed();
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

            $('#linAddMore').click(function (e) {
                ///////  ---------------------------------Quality  -------------------------
                if (!blankFieldValidation('txtProductnameQuality', 'Product/Activity name', projname)) {
                    return false;
                }
                if (!blankFieldValidation('txtAddress', 'Name & address of the registration authority', projname)) {
                    return false;
                }
                if (($('#txtCertificateNo').val() == '') && ($('#txtRenewal').val() == '')) {
                    jAlert('<strong>Provide either certificate no or renewal no !</strong>', projname);
                    return false;
                }
                if ($('#txtCertificateNo').val() != '') {
                    if (!blankFieldValidation('txtCertDate', 'Certificate date', projname)) {
                        return false;
                    }
                    if ($('#fuCertificateDetailsDoc').val() == '') {
                        jAlert('<strong>Please upload certificate !</strong>', projname);
                        return false;
                    }
                }

                if ($('#txtRenewal').val() != '') {
                    if (!blankFieldValidation('txtRenewDate', 'Renewal date', projname)) {
                        return false;
                    }
                    if ($('#fuRenewalDetails').val() == '') {
                        jAlert('<strong>Please upload renewal certificate !</strong>', projname);
                        return false;
                    }
                }

                if ($('#txtCertDate').val() != '') {
                    if (new Date($('#txtCertDate').val()) > new Date()) {
                        jAlert('<strong>Certificate date should not be greater than current date !</strong>', projname);
                        $('#txtCertDate').focus();
                        return false;
                    }
                }
                if (($('#txtCertDate').val() != '') && ($('#txtRenewDate').val() != '')) {
                    if (new Date($('#txtCertDate').val()) > new Date($('#txtRenewDate').val())) {
                        jAlert('<strong>Certificate date should not be greater than renewal date !</strong>', projname);
                        return false;
                    }
                }

                if (!blankFieldValidation('txtAmountExpenditure', 'Amount of Expenditure Incurred', projname)) {
                    return false;
                }

            }); // end of  $('#linAddMore').click
        });


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
                    jAlert('<strong>Please upload Document deatils of Undertaking on non-availment of subsidy.</strong>', projname);
                    return false;
                }
            }
            if (!blankFieldValidation('txtreimamt', 'Present Claim for reimbursement', projname)) {
                return false;
            }

            return true;
        }
        function validformindraft() {

            if ($('#TxtAdhaar').val() != '') {
                var adhar = $('#TxtAdhaar').val();
                if (adhar.length < 12) {
                    jAlert('Aadhaar Card no should be 12 digits.', '');
                    return false;
                }
            }
        }
        function SaveDraft() {
            var rbtnVal = 0;
            //----------------------------Industrial Unit's --------------------------------------------------

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
                    if ($('#TxtAdhaar').val() == '') {
                        jAlert('<strong> Please fill correct Aadhar number.</strong>', projname);
                        return false;
                    }
                    var adhar = $('#TxtAdhaar').val();
                    if (adhar.length < 12) {
                        jAlert('Aadhaar Card no should be 12 digits.', projname);
                        return false;
                    }
                }
                else if (rbtnVal == "2") {
                    if (blankFieldValidation('hdnAUTHORIZEDFILE', 'Please upload Authorizing letter signed by Authorized Signatory !', projname) == false) {
                        return false;
                    }
                }
            }
            /// --------------------------------------------------------------------------------------------Quality Certification---------------------------------------------------------------------------------------------
            var rowcount = $("#gvdQuality tr").length;
            if (rowcount == "0") {
                jAlert('<strong>Please enter atleast one Items of Quality Certification Details.</strong>', projname);
                return false;
            }
            if ($('#txtTotal').val() != '') {
                if ($('#txtTotal').val() > 300000) {
                    jAlert('<strong> Quality Certification Details Total should not exceed 3lakh  !</strong>', projname);
                    return false;
                }
            }

            if (!AvailValidation()) {
                return false;
            }
            //  --------------------------------------------------------------------------------------------Other Documents   --------------------------------------------------------------------------------------------
            //            if ($("#D275").val() == '') {

            //                jAlert('Please provide Document in Support of OSPCB consent to operate. ', projname);
            //                return false;
            //            }




        }
    </script>
    <script type="text/javascript" language="javascript">

        function HasFile(fuControlId, strError) {
            if ($('#' + fuControlId).val() == "") {
                jAlert(strError, projname);
                return false;
            }
        }
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

        function OnChangePriority() {
            $('.Pioneersec').hide();
            if ($("input[name='RadIsPriority']:checked").val() == '1') {
                $('.Pioneersec').show();
            }
            else {
                $('.Pioneersec').hide();
            }
        }

        function FileCheckNew(e, k, Img) {
            var ids = e.id;
            var lnkid = k.id;
            var ar = ["pdf", "zip"];

            if (jQuery.inArray($("#" + ids).val().split('.').pop().toLowerCase(), ar) == -1) {
                jAlert('<strong>Only pdf/zip formats are allowed. </strong>', 'Alert');
                $("#" + ids).val('');
                $('#' + lnkid).text('');
                return false;
            }
            else {
                if ((e.files[0].size > 4194304) && ($("#" + ids).val() != '')) {

                    jAlert('<strong>File size must be less then 4mb! </strong>', 'Alert');
                    $("#" + ids).val('');
                    $('#' + lnkid).text('');
                    e.preventDefault();
                    return false;
                }
                else {
                    //$('#' + lnkid).text($("#" + ids).val().split('\\').pop());                   
                    $('#' + Img).attr('src', '../images/incapproved.png');
                    $('#' + Img).attr('title', $("#" + ids).val().split('\\').pop());
                }
            }
        }

        function FileCheck(e) {
            var ids = e.id;
            var ar = ["pdf", "zip"];

            if (jQuery.inArray($("#" + ids).val().split('.').pop().toLowerCase(), ar) == -1) {
                jAlert('<strong>Only pdf/zip formats are allowed. </strong>', 'Alert');
                $("#" + ids).val('');

                return false;
            }
            else {
                if ((e.files[0].size > 4194304) && ($("#" + ids).val() != '')) {

                    jAlert('<strong>File size must be less then 4mb! </strong>', 'Alert');
                    $("#" + ids).val('');
                    e.preventDefault();
                    return false;
                }

            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        var msgTitle = 'Incentive';
        var fileExtension = ['pdf'];

    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
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
                                <h2>
                                    Application For Quality Certification</h2>
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
                                                        <div class="col-sm-6 ">
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
                                                            Aadhar No.</label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TxtAdhaar" CssClass="form-control" placeholder="123412341234" runat="server"
                                                                MaxLength="12"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                FilterType="Numbers" TargetControlID="TxtAdhaar" FilterMode="ValidChars" ValidChars="0123456789" />
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
                                                                <asp:FileUpload ID="FlupAUTHORIZEDFILE" onchange="return FileCheck(this);" runat="server"
                                                                    CssClass="form-control" />
                                                                <asp:HiddenField ID="hdnAUTHORIZEDFILE" runat="server" />
                                                                <asp:LinkButton ID="lnkAUTHORIZEDFILE" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocumentUpload_Click" ToolTip="Click here to upload the file." OnClientClick="return HasFile('FlupAUTHORIZEDFILE',' Please provide Authorizing letter signed by Authorized Signatory');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkAUTHORIZEDFILEDdelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypAUTHORIZEDFILE" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblAUTHORIZEDFILE" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded  successfully"></asp:Label>
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
                                                            All Amounts to be entered in INR(in Lakhs)</p>
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
                                                                                <asp:TemplateField HeaderText="Sl#">
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
                                                                        Direct Empolyment In Numbers<small>(on Company Payroll)</small>
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
                                                                                <asp:TemplateField HeaderText="Sl#">
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
                                                                    <label for="Iname" class="col-sm-4 ">
                                                                        Direct Empolyment In Numbers<small>(on Company Payroll)</small>
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
                                                            <label class="col-sm-5 ">
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
                                                            <label class="col-sm-5">
                                                                <asp:Label ID="Lbl_Approved_DPR_Before_Doc_Name" runat="server" Text="" CssClass="form-control-static"></asp:Label>
                                                                <asp:HiddenField ID="Hid_Approved_DPR_Before_Doc_Code" runat="server" />
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
                                                                    <asp:Label ID="Lbl_FFCI_After_Doc_Name" runat="server" Text="" CssClass="form-control-static"></asp:Label>
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
                                                                <asp:Label ID="Lbl_Approved_DPR_After_Doc_Name" runat="server" Text="" CssClass="form-control-static"></asp:Label>
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
                                                    <label class="col-sm-4">
                                                        FDI (If any)
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
                                        <div class="panel-heading" role="tab" id="Div7">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#LandDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-minus">
                                                    </i>Quality Certification Details</a>
                                            </h4>
                                        </div>
                                        <div id="LandDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12">
                                                            Product/Activities Quality Certification Details
                                                        </label>
                                                        <div class="col-sm-12  margin-bottom10">
                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                <ContentTemplate>
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th>
                                                                                &nbsp;
                                                                            </th>
                                                                            <th>
                                                                                &nbsp;
                                                                            </th>
                                                                            <th>
                                                                                &nbsp;
                                                                            </th>
                                                                            <th colspan="3" style="text-align: center;">
                                                                                <strong>Certificate Details</strong>
                                                                            </th>
                                                                            <th colspan="3" style="text-align: center;">
                                                                                <strong>Certificate Renewal Details</strong>
                                                                            </th>
                                                                            <th colspan="2" style="text-align: center;">
                                                                                <strong>Expenditure Details</strong>
                                                                            </th>
                                                                            <th>
                                                                                &nbsp;
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th width="4%">
                                                                                Sl #
                                                                            </th>
                                                                            <th>
                                                                                Product/Activity Name
                                                                            </th>
                                                                            <th width="10%">
                                                                                Name & address of the Registration Authority
                                                                            </th>
                                                                            <th width="7%">
                                                                                Certificate No
                                                                            </th>
                                                                            <th width="12%">
                                                                                Certificate Date
                                                                            </th>
                                                                            <th width="8%">
                                                                                Attachment
                                                                            </th>
                                                                            <th width="7%">
                                                                                Renewal No
                                                                            </th>
                                                                            <th width="12%">
                                                                                Renewal Date
                                                                            </th>
                                                                            <th width="8%">
                                                                                Attachment
                                                                            </th>
                                                                            <th width="8%">
                                                                                Amount of Expenditure Incurred
                                                                            </th>
                                                                            <th width="8%">
                                                                                Attachment
                                                                            </th>
                                                                            <th width="5%">
                                                                                Action
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtProductnameQuality" MaxLength="25" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtProductnameQuality_FilteredTextBoxExtender9"
                                                                                    runat="server" TargetControlID="txtProductnameQuality" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                                                                    ValidChars=".- ">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAddress" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtAddress_FilteredTextBoxExtender9" runat="server"
                                                                                    TargetControlID="txtAddress" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                                                                    ValidChars=" /-,">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtCertificateNo" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtCertificateNo_FilteredTextBoxExtender11" runat="server"
                                                                                    TargetControlID="txtCertificateNo" FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters,Custom,Numbers"
                                                                                    ValidChars=" /-,">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <div class="input-group  date datePicker" id="Div9">
                                                                                    <input name="txtTimescheduleforyearofcomm" type="text" id="txtCertDate" runat="server"
                                                                                        class="form-control" />
                                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <asp:HyperLink ID="hplCertificateDetailsDoc" runat="server"></asp:HyperLink>
                                                                                <asp:Label runat="server" ID="lblCertificateDetailsDoc"></asp:Label>
                                                                                <asp:LinkButton ID="lbtnCertificateDetailsDoc" OnClientClick="return openpopup(fuCertificateDetailsDoc);"
                                                                                    CssClass="btn btn-danger" data-toggle="tooltip" title="Quality Certificate / Registration Certificate issued by the competent authority"
                                                                                    runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                                <asp:Image runat="server" ID="ImgCertDetAttachment" Height="15" Width="15" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtRenewal" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtRenewal_FilteredTextBoxExtender9" runat="server"
                                                                                    TargetControlID="txtRenewal" FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters,Custom,Numbers"
                                                                                    ValidChars=" /-,">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <div class="input-group  date datePicker" id="Div10">
                                                                                    <input name="txtTimescheduleforyearofcomm" type="text" id="txtRenewDate" runat="server"
                                                                                        class="form-control" />
                                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <asp:HyperLink ID="hplRenewalDetails" runat="server"></asp:HyperLink>
                                                                                <asp:LinkButton ID="LinkButton14" OnClientClick="return openpopup(fuRenewalDetails);"
                                                                                    CssClass="btn btn-danger" data-toggle="tooltip" title="Quality Certificate / Registration Certificate issued by the competent authority"
                                                                                    runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                                <asp:Label ID="lblRenewalDetails" runat="server"></asp:Label>
                                                                                <asp:Image runat="server" ID="ImgRenewalDetAttachment" Height="15" Width="15" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAmountExpenditure" MaxLength="10" runat="server" onkeypress="return FloatOnly(event, this);"
                                                                                    CssClass="form-control"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtAmountExpenditure_FilteredTextBoxExtender9" runat="server"
                                                                                    TargetControlID="txtAmountExpenditure" FilterType="Custom, Numbers" FilterMode="ValidChars"
                                                                                    ValidChars="1234567890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:HyperLink ID="hplExpenditure" runat="server"></asp:HyperLink>
                                                                                <asp:LinkButton ID="LinkButton37" CssClass="btn btn-danger" data-toggle="tooltip"
                                                                                    OnClientClick="return openpopup(fuExpenditure);" title="Statement on expenditure incurred for obtaining Quality certification / its renewal
                                                        supported with copies of the bills / vouchers / receipt etc." runat="server"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                                <asp:Label ID="lblExpenditure" runat="server"></asp:Label>
                                                                                <asp:Image runat="server" ID="ImgExpenDetAttachment" Height="15" Width="15" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="linAddMore" CssClass="btn btn-success btn-sm" runat="server"
                                                                                    OnClick="BulkPlusEntryQuality"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <asp:GridView ID="gvdQuality" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                                        ShowHeader="false" OnRowDataBound="gvdQuality_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sl#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblslno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="4%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Product/Activity Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstrProductName" runat="server" Text='<%# Eval("strProductName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Name & address of the Registration Authority">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstrNameaddressofRA" runat="server" Text='<%# Eval("strNameaddressofRA") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Certificate No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstrCertificateNo" runat="server" Text='<%# Eval("strCertificateNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="7%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Certificate Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldtmCertificateDate" runat="server" Text='<%# Eval("dtmCertificateDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="12%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Certificate Attachment">
                                                                                <ItemTemplate>
                                                                                    <div class="input-group">
                                                                                        <asp:HyperLink ID="lbllblstrCertificateDetailsDOC" runat="server" Target="_blank"
                                                                                            CssClass="input-group-addon bg-blue" ToolTip="Click Here to View Document !!"
                                                                                            NavigateUrl='<%# "~/incentives/Files/QualityCertificate/"  +  Eval("strCertificateDetailsDOC") %>'><i class="fa fa-download"></i>
                                                                                        </asp:HyperLink>
                                                                                        <asp:HiddenField ID="Hid_Cert_Doc_File_Name" runat="server" Value='<%# Eval("strCertificateDetailsDOC") %>' />
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="8%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Renewal No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblintRenewalSlno" runat="server" Text='<%# Eval("intRenewalSlno") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="7%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Renewal Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldtmRenewalDate" runat="server" Text='<%# Eval("dtmRenewalDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="12%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Renewal Attachment">
                                                                                <ItemTemplate>
                                                                                    <div class="input-group">
                                                                                        <asp:HyperLink ID="lblstrRenewalDateDOC" runat="server" Target="_blank" ToolTip="Click Here to View Document !!"
                                                                                            CssClass="input-group-addon bg-blue" NavigateUrl='<%# "~/incentives/Files/QualityCertificate/" + Eval("strRenewalDateDOC") %>'><i class="fa fa-download"></i></asp:HyperLink>
                                                                                        <asp:HiddenField ID="Hid_Renewal_Doc_File_Name" runat="server" Value='<%# Eval("strRenewalDateDOC") %>' />
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="8%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount of Expenditure Incurred">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstrAmountofExpenditure" runat="server" Text='<%# Eval("strAmountofExpenditure") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="8%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Expenditure Attachment">
                                                                                <ItemTemplate>
                                                                                    <div class="input-group">
                                                                                        <asp:HyperLink ID="lblstrExpenditureDetails" runat="server" Target="_blank" ToolTip="Click Here to View Document !!"
                                                                                            CssClass="input-group-addon bg-blue" NavigateUrl='<%# "~/incentives/Files/QualityCertificate/"  +  Eval("strExpenditureDetails") %>'><i class="fa fa-download"></i></asp:HyperLink>
                                                                                        <asp:HiddenField ID="Hid_Expen_Doc_File_Name" runat="server" Value='<%# Eval("strExpenditureDetails") %>' />
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="8%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ImageButtonDeleteQuality" runat="server" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                                        ToolTip="Click Here to Remove Record !!" OnClick="BulkMinusEntryQuality" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <br />
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-1">
                                                                            <strong>Total</strong>
                                                                        </label>
                                                                        <div class="col-sm-2">
                                                                            <span class="colon">:</span>
                                                                            <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="linAddMore" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            <div style="display: none;">
                                                                <asp:FileUpload ID="fuCertificateDetailsDoc" onchange="return FileCheckNew(this, lblCertificateDetailsDoc,$('#ImgCertDetAttachment').attr('id'));"
                                                                    runat="server" />
                                                                <asp:FileUpload ID="fuRenewalDetails" onchange="return FileCheckNew(this, lblRenewalDetails,$('#ImgRenewalDetAttachment').attr('id'));"
                                                                    runat="server" />
                                                                <asp:FileUpload ID="fuExpenditure" onchange="return FileCheckNew(this, lblExpenditure,$('#ImgExpenDetAttachment').attr('id'));"
                                                                    runat="server" />
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
                                                    href="#AvailedClaimDetails" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-plus"></i>Availed Details</a>
                                            </h4>
                                        </div>
                                        <div id="AvailedClaimDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4">
                                                            Present Claim for reimbursement <a data-toggle="tooltip" class="fieldinfo2" title="Present Claim for reimbursement">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtreimamt" runat="server" onkeypress="return  FloatOnly(event, this);"
                                                                CssClass="form-control" MaxLength="15" onblur="DecimalValidation(this);"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1226" runat="server" TargetControlID="txtreimamt"
                                                                FilterType="Custom,Numbers" ValidChars="." />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4">
                                                            Has Subsidy/Incentive against the details in this application been availed earlier
                                                        </label>
                                                        <div class="col-sm-6 margin-bottom10">
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
                                                                    Details of Subsidy Already Availed
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
                                                                                <asp:TextBox ID="txtsacamt" runat="server" onkeypress="return FloatOnly(event, this);"
                                                                                    CssClass="form-control" MaxLength="15" onblur="DecimalValidation(this);"></asp:TextBox>
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
                                                                                <div class="input-group date datePicker" id="Div14">
                                                                                    <asp:TextBox ID="txtsacdat" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtavilamt" runat="server" CssClass="form-control" MaxLength="15"
                                                                                    onkeypress="return FloatOnly(event, this);" onblur="DecimalValidation(this);"></asp:TextBox>
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
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="form-group  availuploadsec availgroup1">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4">
                                                            Document details of assistance sanctioned <a data-toggle="tooltip" class="fieldinfo2"
                                                                title="Document details of assistance sanctioned"><i class="fa fa-question-circle"
                                                                    aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnSupportingDocsID" runat="server" Value="D253" />
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FU_Asst_Sanc_Doc" data-toggle="tooltip" CssClass="form-control"
                                                                    onchange="return FileCheck(this);" runat="server" />
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
                                                                Visible="false" runat="server" Text="Document uploaded  successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group availundertakingsec availgroup2">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            Undertaking on non-availment of subsidy earlier on this project <a data-toggle="tooltip"
                                                                class="fieldinfo2" title="Undertaking on non-availment of subsidy earlier on this project">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a></label>
                                                            <asp:HiddenField ID="hdnSubsidyAvailedID" runat="server" Value="D230" />
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FU_Undertaking_Doc" CssClass="form-control" data-toggle="tooltip"
                                                                    onchange="return FileCheck(this);" runat="server" />
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
                                                        <label for="Iname" class="col-sm-4">
                                                            Amount of Differential Claim to be Exempted <a data-toggle="tooltip" class="fieldinfo2"
                                                                title="Amount of Differential Claim to be Exempted"><i class="fa fa-question-circle"
                                                                    aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtdiffclaimamt" runat="server" onkeypress="return  FloatOnly(event, this);"
                                                                CssClass="form-control" MaxLength="15" onblur="DecimalValidation(this);"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender116" runat="server" TargetControlID="txtdiffclaimamt"
                                                                FilterType="Custom,Numbers" ValidChars="." />
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
                                                    href="#InterestSubsidyDetails" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-plus"></i>Other Documents</a>
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
                                                                <asp:FileUpload ID="flValidStatutary" CssClass="form-control" runat="server" onchange="return FileCheckZip(this);" />
                                                                <asp:HiddenField ID="D275" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnIsOsPCBDownloaded" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUValidStatutary" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flValidStatutary','Please Upload OSPCB consent to operate related Document');"
                                                                    OnClick="lnkUValidStatutary_click" ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
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
                                                                <asp:FileUpload ID="flDelay" CssClass="form-control" runat="server" onchange="return FileCheckNew(this);" />
                                                                <asp:HiddenField ID="D274" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUDelay" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flDelay','Please Upload Sector Relevant Document');"
                                                                    OnClick="lnkUDelay_Click" ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
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
                                                                    onchange="return FileCheckZip(this);" />
                                                                <asp:HiddenField ID="D280" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnBoilderDownloaded" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUCleanApproveAuthority" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Please Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file." OnClick="lnkUCleanApproveAuthority_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
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
                                    <asp:Button ID="btnDraft" runat="server" OnClick="btnDraft_Click" Text="Save As Draft"
                                        OnClientClick="return validformindraft();" CssClass="btn btn-warning" />
                                    <asp:Button ID="btnApply" OnClientClick="return SaveDraft();" runat="server" OnClick="btnApply_Click"
                                        Text="Apply" CssClass="btn btn-success" />
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
            ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#ImgAssistance').attr("id"));
            ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#ImgNonAvailment').attr("id"));
            ImgSrc($('#D275').val(), $('#ImgOSPCB').attr("id"));
            ImgSrc($('#D274').val(), $('#ImgSectRel').attr("id"));
            ImgSrc($('#D280').val(), $('#ImgCleanApproveAuthority').attr("id"));
        }
        function pageLoad() {

            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy"
                });
            });
            OnChangeApplyBy();
            OnChangePriority();

            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails,#LandDetails,#AvailedClaimDetails,#InterestSubsidyDetails,#DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").on("click", function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });

        }
        DocCheckList();
        function OpenUploadModal(controlId) {

            $("#" + controlId.id).click();
            return false;
        };


        function vFinanceAddMore() {
            if (!blankFieldValidation('txtNameOfFinancialInst', 'Name of Financial Institution', msgTitle)) {
                return false;
            }
            else if (!blankFieldValidation('txtMFAddress', 'Location', msgTitle)) {
                return false;
            }
            else if (!blankFieldValidation('txtLoanAmt', 'Term Loan Amount', msgTitle)) {
                return false;
            }
            else if (!blankFieldValidation('txtSactionDate', '	Sanction Date', msgTitle)) {
                return false;
            }
            else if (!blankFieldValidation('txtAvailedAmt', 'Availed Amount', msgTitle)) {
                return false;
            }
            else if (!blankFieldValidation('txtAvailedDate', 'Availed Date', msgTitle)) {
                return false;
            }
            else {
                return true;
            }
        }

        function FileCheckZip(e) {
            var ids = e.id;
            var fileExtension = ['zip'];
            if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                //// alert('Only pdf formats are allowed.');
                jAlert('Only zip formats are allowed.', 'GO-SWIFT');
                $("#" + ids).val(null);
                return false;
            }
            else {
                if ((e.files[0].size > parseInt(4 * 1024 * 1024)) && ($("#" + ids).val() != '')) {
                    //alert('File size must be less then 4MB!');
                    jAlert('File must be less then 4MB !', 'GO-SWIFT');
                    $("#" + ids).val(null);
                    //e.preventDefault();
                    return false;
                }
            }
        }
    </script>
    </form>
</body>
</html>
