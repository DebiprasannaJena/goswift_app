<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostGrantprioritysectorsstatus.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="incentives_PostGrantprioritysectorsstatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" />
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            if ($("#Lbl_Term_Loan_Doc_Name").text() == '') {
                $("#termloan").hide();
            }
            if ($("#Lbl_Direct_Emp_After_Doc_Name").text() == '') {
                $("#after").hide();
            }
            if ($("#Lbl_Direct_Emp_Before_Doc_Name").text() == '') {
                $("#before").hide();
            }
            if ($("#Lbl_Pioneer_Doc_Name").text() == '') {
                $("#Div_Pioneer_Doc").hide();
            }


            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            }); $('.Pioneersec,.attorneysec,.adhardetails').hide();
            $(".applyby").on("click", function () {
                debugger;
                if ($("input:checked").val() == '1') {
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

            $('#PSCertificateApplied').hide();

            $('#rdBtnPSCertificateYes').click(function () {
                $('#PSCertificateYes').show();
                $('#PSCertificateApplied').hide();
            });
            $('#rdBtnPSCertificateApplied').click(function () {
                $('#PSCertificateYes').hide();
                $('#PSCertificateApplied').show();
            });

            $('#rdBtnPSCertificateNo').click(function () {
                $('#PSCertificateYes').hide();
                $('#PSCertificateApplied').hide();
            });

        });

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
    
    </script>
    <script type="text/javascript">

        var projname = 'GO-SWIFT';
        function AlreadyAvailed() {

            if (DropDownValidation('ddlIncentiveType', '0', 'Incentive Type', projname) == false) {
                return false;
            }

            if (blankFieldValidation('txtQuantum', 'Quantum/Value', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtTimeschedulefrom', ' Start Date', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtTimescheduleTo', 'Period Date', projname) == false) {
                return false;
            }

            if (DropDownValidation('ddlApplicability', '0', 'IPR Applicability', projname) == false) {
                return false;
            }

            var startDate = document.getElementById("txtTimeschedulefrom").value;
            var endDate = document.getElementById("txtTimescheduleTo").value;

            if ((Date.parse(startDate) >= Date.parse(endDate))) {
                jAlert('<strong>End date should be greater than Start date</strong>', projname);
                document.getElementById("txtTimescheduleTo").value = "";
                return false;
            }

            return true;
        }

        function validformindraft() {
            if ($('#TxtAdhaar1').val() != '') {
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('Aadhaar Card no should be 12 digits.', '');
                    return false;
                }
            }
        }

        function CheckValidation() {

            //----------------------------industry Unit----------------------
            if (blankFieldValidation('TxtApplicantName', 'Applicant Name', '') == false) {
                return false;
            }
            if (SpecialCharacter1st('TxtApplicantName', 'Applicant Name', '') == false) {
                return false;
            }

            var rbtnVal = 0;
            if (!$('input[name=radApplyBy]:checked').val()) {
                jAlert('<strong> Please select Application Applying By</strong>', '');
                return false;
            }
            else {
                rbtnVal = $('input[name=radApplyBy]:checked').val();
                if (rbtnVal == "1") {
                    if ($('#TxtAdhaar1').val() == '') {
                        jAlert('<strong> Please fill correct Aadhar number.</strong>', '');
                        $('#TxtAdhaar1').focus();
                        return false;
                    }
                    var adhar = $('#TxtAdhaar1').val();
                    if (adhar.length < 12) {
                        jAlert('Aadhaar Card no should be 12 digits.', '');
                        $('#TxtAdhaar1').focus();
                        return false;
                    }
                }
                if (rbtnVal == "2") {
                    if (blankFieldValidation('hdnAUTHORIZEDFILE', 'Please upload Authorizing letter signed by Authorized Signatory !', '') == false) {
                        return false;
                    }
                }
            }


            //-------------------------------DLSWCA/SWSLCA/HLCA Approval Details--------------------

            //--------------------------------------------------------------End of DLSWCA/SWSLCA/HLCA Approval Details

            //---------------------------Priority Sector Details------------

            if ($("#hdfmode").val() == "2") {
                if ($("#HDNPriorCertdoc").val().length == "0") {
                    jAlert('<strong>Please upload priority certificate  !</strong>', projname);
                    $("#FUPPriorCert").focus();
                    return false;
                }
            }


            if ($("#hdnUploadApplAcknow").val().length == 0) {
                if ($("#fldUploadApplAcknow").val().length == 0) {
                    jAlert('<strong>Please upload Undertaking in context of that Industrial units  !</strong>', projname);
                    return false;
                }
            }
            if ($("#HiddenField1").val().length == 0) {
                if ($("#FileUpload1").val().length == 0) {
                    jAlert('<strong>Please upload Document / certificate in support of Category fall   !</strong>', projname);
                    return false;
                }
            }

            //--------------------------------------------------------------End -Priority Sector Details------------




            //-------------------------------Additional Document--------------------

            //--------------------------------------------------------------End of Additional Document

            if ($("#hdfmode").val() == "2") {
                if ($("#SignFiles").val() == "") {
                    jAlert('<strong>Please upload signature   !</strong>', projname);
                    $("#SignFiles").focus();
                    return false;
                }
            }

        }

        function TocheckPrior() {
            jAlert('<strong>You are not eligible</strong>', projname);
        }

        /*-------------------------------------------------------------*/

        function FileCheck(e) {
            var ids = e.id;
            var ar = ["pdf", "zip"];

            if (jQuery.inArray($("#" + ids).val().split('.').pop().toLowerCase(), ar) == -1) {
                jAlert('<strong>Only pdf/zip formats are allowed. </strong>', projname);
                $("#" + ids).val('');
                return false;
            }
            else {
                if ((e.files[0].size > parseInt(4 * 1024 * 1024)) && ($("#" + ids).val() != '')) {

                    jAlert('<strong>File size must be less than 4 MB !! </strong>', projname);
                    // $("#" + ids).val().val('');
                    $("#" + ids).val('');
                    e.preventDefault();
                    return false;
                }
            }
        }

        /*-------------------------------------------------------------*/

        function readURL(input) {
            if (input.files && input.files[0]) {//Check if input has files.
                var reader = new FileReader(); //Initialize FileReader.

                reader.onload = function (e) {
                    $('#PreviewImage').attr('src', e.target.result);
                    $('#PreviewImage').attr('style', 'display:block');
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
        function alertredirect(msg) {

            jAlert(msg, msgTitle, function (r) {

                if (r) {
                    location.href = 'IncentiveOffered.aspx';
                    return true;
                }
                else {
                    return false;
                }
            });
        }       
    </script>
    <style type="text/css">
        .PopupDtlsdv
        {
            z-index: 99999;
            height: 100%;
            position: fixed !important;
            top: 60;
            left: 0px;
        }
        #PreviewImage
        {
            height: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container">
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
                                                    aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa fa-plus">
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
                                                            <asp:Label ID="lbl_Org_Type" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Name of Applicant</label>
                                                        <div class="col-sm-1" style="padding-right: 0px">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList CssClass="form-control" ID="DdlGender" runat="server">
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
                                                            Applied By</label>
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
                                                            <div>
                                                                <asp:TextBox ID="TxtAdhaar1" CssClass="form-control" placeholder="123412341234" runat="server"
                                                                    MaxLength="12"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTxtAdhaar1" runat="server"
                                                                    FilterType="Numbers" TargetControlID="TxtAdhaar1">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group attorneysec" id="divAuthorizing" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Please provide Authorizing letter signed by Authorized Signatory
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
                                                                    OnClick="lnkDocumentUpload_Click" ToolTip="Click here to upload the file." OnClientClick="return HasFile('fuSupportingDocs','Plase Upload Document details of assistance sanctioned');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
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
                                                            <asp:Label ID="lbl_Unit_Cat" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Unit Type</label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lbl_Unit_Type" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                            <asp:Label ID="lbl_Regd_Office_Address" runat="server"></asp:Label>
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
                                                            <asp:Label ID="lbl_District" runat="server" CssClass="form-control-static"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4 col-sm-offset-1">
                                                            Sector</label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lbl_Sector" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                            <asp:Label ID="lbl_Sub_Sector" runat="server">
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
                                        <div class="panel-heading" role="tab" id="Div7">
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
                                                                        Direct Employment In Numbers <small>(On Company Payroll)</small>
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
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-6">
                                                                        <small class="text-gray">
                                                                            <asp:Label ID="Lbl_Direct_Emp_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                            <asp:HiddenField ID="Hid_Direct_Emp_Before_Doc_Code" runat="server" />
                                                                        </small>
                                                                    </label>
                                                                    <div id="before" class="col-sm-6">
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
                                                                        Direct Employment In Numbers <small>(On Company Payroll)</small>
                                                                    </label>
                                                                    <div class="col-sm-2">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lbl_Direct_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                    <label for="Iname" class="col-sm-4">
                                                                        Contractual Employment In Numbers
                                                                    </label>
                                                                    <div class="col-sm-2">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lbl_Contract_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-6">
                                                                        <small class="text-gray">
                                                                            <asp:Label ID="Lbl_Direct_Emp_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                                            <asp:HiddenField ID="Hid_Direct_Emp_After_Doc_Code" runat="server" />
                                                                        </small>
                                                                    </label>
                                                                    <div id="after" class="col-sm-6">
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
                                        <div class="panel-heading" role="tab" id="Div1">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#divInvestment" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa fa-plus">
                                                    </i>Investment Details </a>
                                            </h4>
                                        </div>
                                        <div id="divInvestment" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
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
                                                                                <asp:Label ID="lbl_Other_Fixed_Asset_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <strong>Total</strong>
                                                                            </td>
                                                                            <td class="text-right">
                                                                                <strong>
                                                                                    <asp:Label ID="lbl_Total_Capital_Before" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                                <asp:Label ID="Lbl_Approved_DPR_Before_Doc_Name" runat="server" Text=""></asp:Label>
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
                                                                Date of First Fixed Capital Investment (for land/Building/plant and machinery &
                                                                Balancing Equipment)
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
                                                            <label class="col-sm-5">
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
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4 col-sm-offset-1">
                                                            <small class="text-gray">
                                                                <asp:Label ID="Lbl_Term_Loan_Doc_Name" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="Hid_Term_Loan_Doc_Code" runat="server" />
                                                            </small>
                                                        </label>
                                                        <div id="termloan" class="col-sm-6">
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
                                                        FDI Component
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
                                                    href="#IndustryDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa fa-minus">
                                                    </i>Priority Sector Details </a>
                                                <asp:HiddenField ID="hdfmode" runat="server" />
                                            </h4>
                                        </div>
                                        <div id="IndustryDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group" id="dvPriority">
                                                    <div class="row">
                                                        <label class="col-sm-3">
                                                            Priority Certificate
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FUPPriorCert" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:LinkButton ID="LNBUploadPrior" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="LNBUploadPrior_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LNBDeletePrior" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="LNBDeletePrior_Click" Visible="false" ToolTip="Delete file"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="HypviewPrior" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-blue"
                                                                    runat="server" OnClientClick="JavaScript: return false;" Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="HDNPriorCertdoc" runat="server" />
                                                            <asp:HiddenField ID="HDNPriorCertCode" runat="server" Value="---" />
                                                            <asp:Label ID="LBLPriorCert" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="sectordv" style="display: none;">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-3">
                                                                Sector</label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="ddl_Sector" runat="server" class="form-control" OnSelectedIndexChanged="ddl_Sector_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-4">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="subsectordv" style="display: none;">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-3">
                                                                Sub Sector</label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="ddl_Sub_Sector" runat="server" class="form-control">
                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-4">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="deriveddiv" style="display: none;">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-3">
                                                                Derived Sector</label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_DerivedSector" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="liesdiv" style="display: none;">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-3">
                                                                Lies in Priority Sector</label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <%--  <span class="aspNetDisabled">--%>
                                                                <asp:CheckBox ID="ChkBx_Priority" runat="server" Enabled="False" />
                                                                <%--  <input id="ChkBx_Priority" type="checkbox" name="ChkBx_Priority" disabled="disabled"></span>--%>
                                                            </div>
                                                            <div class="col-sm-4">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-3">
                                                            Chose Priority Sector Specific Activity</label>
                                                        <span class="colon">:</span>
                                                        <div class="col-sm-4 margin-bottom10" id="divddl" runat="server">
                                                            <asp:DropDownList ID="ddlActivities" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlActivities_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-offset-3 col-sm-8">
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <asp:ListBox ID="ddlSpecificActivity_old" SelectionMode="Multiple" class="form-control"
                                                                        runat="server"></asp:ListBox>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <asp:LinkButton ID="btnselect" runat="server" OnClick="btnselect_Click" CssClass="btn btn-success"><i class="fa fa-angle-double-right"></i></asp:LinkButton>
                                                                    <br />
                                                                    <asp:LinkButton ID="btnremove" runat="server" OnClick="btnremove_Click" CssClass="btn btn-danger"><i class="fa fa-angle-double-left"></i></asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:ListBox ID="ddlSpecificActivity" class="form-control" runat="server"></asp:ListBox>
                                                                </div>
                                                            </div>
                                                            <asp:HiddenField ID="hdnsectorid" runat="server" />
                                                            <asp:HiddenField ID="hdnsubsectorid" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-3">
                                                            A brief note on the present stage of implementation
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload2" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete file"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="HyperLink2" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-blue"
                                                                    runat="server" OnClientClick="JavaScript: return false;" Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                                            <asp:HiddenField ID="HiddenField4" runat="server" Value="D279" />
                                                            <asp:Label ID="Label5" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-3 ">
                                                            <label for="Iname">
                                                                <small>Migrated industrial unit treated as new industrial unit under Priority Sector
                                                                    / Rehabilitated sick industrial unit treated at par with new industrial unit under
                                                                    Priority sector / lndustrial unit seized under Section 29 of the State Financial
                                                                    Corporation Act, 1951 / SARFAESI Act, 2002 and thereafter sold to a new entrepreneur
                                                                    on sale of assets basis and treated as new industrial unit for the purpose of this
                                                                    IPR under Priority Sector</small> <a data-toggle="tooltip" class="fieldinfo2" style="padding-left: 10px"
                                                                        title="Migrated industrial unit treated as new industrial unit under Priority Sector / Rehabilitated sick
                                                                    industrial unit treated at par with new industrial unit under Priority sector / lndustrial unit seized
                                                                    under Section 29 of the State Financial Corporation Act, 1951 / SARFAESI Act, 2002 and
                                                                    thereafter sold to a new entrepreneur on sale of assets basis and treated as new industrial unit
                                                                    for the purpose of this IPR under Priority Sector"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="fldUploadCertificate" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:LinkButton ID="lknAddCertificate" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lknDelCertificate" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete file"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlkViewCertificate" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-blue"
                                                                    runat="server" OnClientClick="JavaScript: return false;" Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="hdnUploadCertificate" runat="server" />
                                                            <asp:HiddenField ID="hdnCertificateDocId" runat="server" Value="D108" />
                                                            <asp:Label ID="lblCertificate" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group Acknowledgementdetails">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 ">
                                                            <small>Undertaking in context of that Industrial units shall have to go in to production
                                                                within three vears from the date of starting first fixed capital investment / from
                                                                the effective date in case of Migrated industrial units</small>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="fldUploadApplAcknow" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:LinkButton ID="lknAddApplAcknow" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lknDelApplAcknow" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete file"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlkViewApplAcknow" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-blue"
                                                                    runat="server" OnClientClick="JavaScript: return false;" Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="hdnUploadApplAcknow" runat="server" />
                                                            <asp:HiddenField ID="hdnApplAcknowDocId" runat="server" Value="D277" />
                                                            <asp:Label ID="lblApplAcknow" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-3 ">
                                                            <label for="Iname">
                                                                <small>Document / certificate in support of Category fall under Priority Sector
                                                                </small><a data-toggle="tooltip" class="fieldinfo2" style="padding-left: 10px" title="Document / certificate in support of Category fall under Priority Sector">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="input-group-addon bg-red"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentDelete_Click" Visible="false" ToolTip="Delete file"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="HyperLink1" data-toggle="tooltip" title="View file" CssClass="input-group-addon bg-blue"
                                                                    runat="server" OnClientClick="JavaScript: return false;" Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                            <asp:HiddenField ID="HiddenField2" runat="server" Value="D278" />
                                                            <asp:Label ID="Label4" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-3 ">
                                                            <label for="Iname">
                                                                <small>Is Provisinal Certificate of Priority Sector available </small>
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:Label ID="lblprop" runat="server" Text="Not Available" Visible="false"></asp:Label>
                                                                <asp:HyperLink ID="hypProposalPriority" title="View file" CssClass="btn btn-info"
                                                                    runat="server" Target="_blank" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
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
                                                    href="#IncentiveavailedDetails " aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-plus"></i>Incentives Availed in Earlier IPRs</a>
                                            </h4>
                                        </div>
                                        <div id="IncentiveavailedDetails" class="panel-collapse collapse" role="tabpanel"
                                            aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <p class="text-red text-right">
                                                    All Amounts to be Entered in INR(in Lakhs)</p>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12 ">
                                                            Already Availed incentive under IPR 2007</label>
                                                        <div class="col-sm-12">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th rowspan="2" width="5%">
                                                                        Slno.
                                                                    </th>
                                                                    <th rowspan="2">
                                                                        Incentive Type
                                                                    </th>
                                                                    <th rowspan="2" width="15%">
                                                                        Quantum/Value
                                                                    </th>
                                                                    <th colspan="2" width="26%" style="text-align: center;">
                                                                        Period
                                                                    </th>
                                                                    <th rowspan="2" width="15%">
                                                                        IPR Applicability
                                                                    </th>
                                                                    <th rowspan="2" width="7%">
                                                                        Add More
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <th width="13%" style="text-align: center;">
                                                                        From
                                                                    </th>
                                                                    <th width="13%" style="text-align: center;">
                                                                        To
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        --
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlIncentiveType" runat="server" class="form-control">
                                                                            <asp:ListItem Text="--Select--" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Text="Interest subsidy" Value="1">Interest subsidy</asp:ListItem>
                                                                            <asp:ListItem Text="Stamp Duty Exemption" Value="2">Stamp Duty Exemption</asp:ListItem>
                                                                            <asp:ListItem Text="Employment Cost Subsidy" Value="3">Employment Cost Subsidy</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQuantum" MaxLength="10" runat="server" onkeypress="return FloatOnly(event, this);"
                                                                            class="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtQuantum"
                                                                            ValidChars="." FilterMode="ValidChars" FilterType="Custom,Numbers">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker" id="Div3">
                                                                            <asp:TextBox ID="txtTimeschedulefrom" runat="server" class="form-control"></asp:TextBox>
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker" id="Div6">
                                                                            <asp:TextBox ID="txtTimescheduleTo" runat="server" class="form-control"></asp:TextBox>
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlApplicability" runat="server" class="form-control">
                                                                            <asp:ListItem Text="--Select--" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Text="IPR 2015" Value="1">IPR 2015</asp:ListItem>
                                                                            <asp:ListItem Text="IPR 1996" Value="2">IPR 1996</asp:ListItem>
                                                                            <asp:ListItem Text="MSMED 2009" Value="3">MSMED 2009</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="lnkAddmore" class="btn btn-success btn-sm" OnClick="lnkAddmore_Click"
                                                                            OnClientClick="return AlreadyAvailed();">
                                                                       
                                                                            <i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                        <%--<a id="A3" class="btn btn-success btn-sm" href="javascript:__doPostBack('LinkButton27','')"></a>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:GridView ID="grdAlreadyAvailed" runat="server" CssClass="table table-bordered"
                                                                DataKeyNames="vchIncentiveType" AutoGenerateColumns="false" OnRowDeleting="grdAlreadyAvailed_RowDeleting">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Slno.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblSlNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="5%"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Incentive Type ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblIncentiveType" runat="server" Text='<%# Eval("vchIncentiveType") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantum/Value">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbl_Quantity" runat="server" Text='<%# Eval("decQuantum") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="15%"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Period From">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbl_PeriodFrom" runat="server" Text='<%# Eval("dtmFrom") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="13%"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Period To">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbl_PeriodTo" runat="server" Text='<%# Eval("dtmto") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="13%"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="IPR Applicability">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbl_PeriodFrom1" runat="server" Text='<%# Eval("vchIPRApplicability") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="15%"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="imgbtnDelete" CssClass="btn btn-xs bigger btn-danger" runat="server"
                                                                                CommandName="delete"><i class="ace-icon fa fa-trash-o icon-only bigger-110"></i>
                                                                            </asp:LinkButton>
                                                                            <asp:HiddenField ID="hdnItr" runat="server" Value='<%#Eval("dcRowId") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="7%"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
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
                                                                Date of Approval <a data-toggle="tooltip" class="fieldinfo2" style="padding-left: 10px"
                                                                    title="Sample Text"><i class="fa fa-question-circle" aria-hidden="true"></i>
                                                                </a>
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-5">
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
                                                            Requirement of land approved by DLSWCA / SLSWCA / HLCA</label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtDLSWCALandApproved" MaxLength="10" runat="server" onkeypress="return  FloatOnly(event, this);"
                                                                CssClass="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FEtxtDLSWCALandApproved" runat="server" TargetControlID="txtDLSWCALandApproved"
                                                                ValidChars="." FilterMode="ValidChars" FilterType="Custom,Numbers">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="display: none;">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Cost of Land
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtDLSWCALandCost" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FEtxtDLSWCALandCost" runat="server" TargetControlID="txtDLSWCALandCost"
                                                                ValidChars="." FilterMode="ValidChars" FilterType="Custom,Numbers">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="display: none;">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Eligible Amount of subsidy(Details Calculation Sheet to be Enclosed)</label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtDLSWCASubsidyAmt" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FEtxtDLSWCASubsidyAmt" runat="server" TargetControlID="txtDLSWCASubsidyAmt"
                                                                ValidChars="." FilterMode="ValidChars" FilterType="Custom,Numbers">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Copy of Approval of DLSWCA / SLSWCA / HLCA
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="fupDLSWCAApprovalDocUpload" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
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
                                                            Copy of Documents to substantiate of land cost
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="fupDLSWCASubstanDocUpload" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:LinkButton ID="lnkAddDLSWCASubstanDoc" runat="server" CssClass="input-group-addon bg-green"
                                                                    data-toggle="tooltip" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload File"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
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
                                                <div class="form-group" id="div5" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            OSPCB consent to Establishment (except white category) <a data-toggle="tooltip" class="fieldinfo2"
                                                                title="Except white category"><i class="fa fa-question-circle" aria-hidden="true">
                                                                </i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flValidStatutary" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="D275" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnIsOsPCBDownloaded" runat="server" />
                                                                <asp:LinkButton ID="lnkUValidStatutary" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkOrgDocumentPdf_Click" ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDValidStatutary" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypValidStatutary" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblValidStatutary" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="div9" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Sector Relevant Document <a data-toggle="tooltip" class="fieldinfo2" title="1. FSSAI/Food License- For food processing unit 
                                                            2. Explosive License -For Explosive manufacturing unit 
                                                            3. BIS Certification -For Packaged drinking water 
                                                            "><i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flDelay" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="D274" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUDelay" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkOrgDocumentPdf_Click" ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDDelay" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypDelay" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblDelay" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="div10" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Factory & Boiler - For all industry (10 with direct employment / 20 no of employment
                                                            with power ) <a data-toggle="tooltip" class="fieldinfo2" title="Factory & Boiler-  For all industry (10 with direct employment / 20 no of employment with power ) ">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flCleanApproveAuthority" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="D280" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnBoilderDownloaded" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUCleanApproveAuthority" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file." OnClick="lnkOrgDocumentPdf_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDCleanApproveAuthority" runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false" OnClick="lnkOrgDocumentDelete_Click"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypCleanApproveAuthority" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblCleanApproveAuthority" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default" id="DivCheckList">
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
                                                            A brief note on the present stage of implementation
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="Imgbrief" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Migrated industrial unit treated as new industrial unit under Priority Sector /
                                                            Rehabilitated sick industrial unit treated at par with new industrial unit under
                                                            Priority sector / lndustrial unit seized under Section 29 of the State Financial
                                                            Corporation Act, 1951 / SARFAESI Act, 2002 and thereafter sold to a new entrepreneur
                                                            on sale of assets basis and treated as new industrial unit for the purpose of this
                                                            IPR under Priority Sector
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgMigrated" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Undertaking in context of that Industrial units shall have to go in to production
                                                            within three years from the date of starting first fixed capital investment / from
                                                            the effective date in case of Migrated industrial units
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="Imgundertaking" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Document / certificate in support of Category fall under Priority Sector
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgCategory" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Copy of Approval of DLSWCA / SLSWCA / HLCA
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgApproval" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Copy of Documents to substantiate of land cost
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="Imgsubstant" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Sector Relevant Document
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgSector" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            OSPCB consent to Establishment (except white category)
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgOSPCB" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Factory & Boiler - For all industry (10 with direct employment / 20 no of employment
                                                            with power )
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="Imgboiler" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row" id="divUpload">
                                            <label class="col-sm-2">
                                                Upload Signature</label>
                                            <div class="col-sm-6">
                                                <img id="PreviewImage" src="" alt="" style="height: 100px!important;" runat="server" /><br />
                                                <asp:FileUpload CssClass="form-control" onchange="readURL(this);" ID="SignFiles"
                                                    runat="server" /></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-footer">
                                    <div class="row">
                                        <div class="col-sm-12 text-right">
                                            <asp:Button ID="Button1" runat="server" Text="Save As Draft" CssClass="btn btn-warning"
                                                OnClick="Button1_Click" />
                                            <asp:Button ID="btnEdit" runat="server" Text="Apply" CssClass="btn btn-success" OnClientClick="return CheckValidation();"
                                                OnClick="btnEdit_Click" />
                                            <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="staicmodal" runat="server" id="pnldet" visible="false">
            <div class="modalcontenter">
                <div class="mod-header">
                    <h4>
                        GO-SWIFT</h4>
                </div>
                <div class="mod-con">
                    <asp:Label ID="lblelgmsg" runat="server"></asp:Label></div>
                <div class="mod-footer">
                    <input type="button" id="successclick" class="btn btn-success btn-sm" value="OK"></div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    <style>
        .staicmodal
        {
            position: absolute;
            top: 0px;
            bottom: 0px;
            right: 0px;
            left: 0px;
            background: rgba(0,0,0,0.5);
            padding-top: 10%;
        }
        .staicmodal .modalcontenter
        {
            width: 320px;
            margin: 0px auto;
            background: #fff;
            border-radius: 4px;
        }
        .staicmodal .mod-header
        {
            padding: 4px 10px;
            text-align: center;
            background: #f00;
            border-radius: 4px 4px 0px 0px;
        }
        .staicmodal .mod-header h4
        {
            color: #fff;
            margin: 0px;
        }
        .staicmodal .mod-con
        {
            padding: 10px 4px;
            text-align: center;
        }
        .staicmodal .mod-con p
        {
            font-size: 14px;
            color: #000;
            margin-bottom: 0px;
        }
        .staicmodal .mod-footer
        {
            padding: 4px;
            border-top: 1px solid #e2e2e2;
            text-align: center;
        }
        .staicmodal .mod-footer .btn
        {
            padding: 3px 15px;
        }
    </style>
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var msgTitle = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'; //  'Incentive';
        function pageLoad() {

            OnChangeApplyBy();
            checkmode();
            DocCheckList();

            if ($("#Lbl_Term_Loan_Doc_Name").text() == '') {
                $("#termloan").hide();
            }
            if ($("#Lbl_Direct_Emp_After_Doc_Name").text() == '') {
                $("#after").hide();
            }
            if ($("#Lbl_Direct_Emp_Before_Doc_Name").text() == '') {
                $("#before").hide();
            }

            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails,#divInvestment, #IndustryDetails,#IncentiveavailedDetails,#InterestSubsidyDetails,#DLSWCA,#DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").on("click", function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });
        }

        $(function () {
            $('.datePicker').datepicker({
                minDate: new Date(),
                autoclose: true,
                format: "dd-M-yyyy"
            });
        });


        $(document).ready(function () {

            checkmode();

            // $('#pnldet').modal('show');

            $('#successclick').click(function () {
                window.location.href = 'incentiveoffered.aspx';
            });

            $('.specifycategory').hide();

            $('.computerdevices').hide();

            $(window).load(function () {
                $('#DerivedSector').change(function () {

                    var sectorval = $(this).val();
                    $("#SpecificActivity option").each(function () {

                        var SpecificActivityvalue = $(this).val()

                        if (SpecificActivityvalue == sectorval) {
                            $("#SpecificActivity option[value='" + SpecificActivityvalue + "']").show();
                        }
                        else {
                            // alert('Not matched');
                            $("#SpecificActivity option[value='" + SpecificActivityvalue + "']").hide()
                            // alert(sectorval);
                        }
                    });
                });
                $('#SpecificActivity').change(function () {
                    var SpecificActivity = $(this).find(":selected").text();
                    if (SpecificActivity == "Other") {
                        $('.specifycategory').show();
                        $('.computerdevices').hide();
                    }
                    else if (SpecificActivity == "Computing Devices") {
                        $('.computerdevices').show();
                    }
                })
            })
        });

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

        function checkmode() {
            if ($("#hdfmode").val() == '2') {
                $("#divUpload").show();
                $("#dvPriority").show();
                $("#Button1").hide();
            }
            else {
                $("#dvPriority").hide();
                $("#Button1").show();
                $("#divUpload").hide();
            }
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
        function DocCheckList() {
            ImgSrc($('#hdnAUTHORIZEDFILE').val(), $('#ImgSign').attr("id"));
            ImgSrc($('#HiddenField3').val(), $('#Imgbrief').attr("id"));
            ImgSrc($('#hdnUploadCertificate').val(), $('#ImgMigrated').attr("id"));
            ImgSrc($('#hdnUploadApplAcknow').val(), $('#Imgundertaking').attr("id"));
            ImgSrc($('#HiddenField1').val(), $('#ImgCategory').attr("id"));
            ImgSrc($('#hdnDLSWCAApprovalDoc').val(), $('#ImgApproval').attr("id"));
            ImgSrc($('#hdnDLSWCASubstanDoc').val(), $('#Imgsubstant').attr("id"));
            ImgSrc($('#D274').val(), $('#ImgSector').attr("id"));
            ImgSrc($('#D275').val(), $('#ImgOSPCB').attr("id"));
            ImgSrc($('#D280').val(), $('#Imgboiler').attr("id"));

        }    
    </script>
    </form>
</body>
</html>
