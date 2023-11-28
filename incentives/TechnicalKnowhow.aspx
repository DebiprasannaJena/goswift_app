<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TechnicalKnowhow.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="incentives_TechnicalKnowhow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="PMenu" TagPrefix="uc5" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
            $('.techcol').hide();
            $('.Pioneersec,.attorneysec,.adhardetails,.exem_details').hide();

            $("#dvgrd").hide();
            $("#dvfup").hide();

            showhideapply();
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

            $("#ddlimp").change(function () {

                if ($(this).val() == '1') {

                    $('a.fieldinfo2').prop('title', 'Any proprietary series of practical, non-patented knowledge and owners experience or tests which is secret, substantial and identified in nature, when brought into India from outside India ');

                }
                if ($(this).val() == '2') {

                    $('a.fieldinfo2').prop('title', 'Technical know-how imparted by any Govt Agency/any other organization approved by the state Govt or Union Govt in the country shall be treated as indigenous');

                }

                showimported($(this).find('input').val());

            });

        });

        function onAvailChange() {
            $('#dvgrd,#dvfup').hide();
            if ($("input[name='RadBtn_Availed_Earlier']:checked").val() == '1') {
                $('#dvgrd').show();
                $('#dvfup').hide();
            }
            else {
                $('#dvfup').show();
                $('#dvgrd').hide();
            }

        }

        function DecimalValidation(Ctl) {
            var CtlId = Ctl.id;
            var amount = $("#" + CtlId).val();
            amount = Number(amount).toFixed(2);
            if (isNaN(amount)) {
                amount = Number(0).toFixed(2);
            }
            $("#" + CtlId).val(amount);
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

        function OpenUploadModal(controlId) {
            $("#" + controlId.id).click();
            return false;
        };

        function opendetailsfun(ctrlvalue) {
            window.open("../incentives/TKH/" + ctrlvalue);
        }

        function blnkfileuploadchecking(ctrl) {
            var ids = ctrl.id;

            if ($("#" + ids).val() == '') {
                jAlert('<strong>Please choose the file to upload </strong>', projname);
                return false;
            }

        }
        function blnkfileviewchecking(ctrl) {
            var ids = ctrl.id;
            if ($("#" + ids).val() == '') {
                jAlert('<strong>No Files to view </strong>', projname);
                return false;
            }
        }

        function blnkfiledeletechecking(ctrl) {
            var ids = ctrl.id;
            if ($("#" + ids).val() == '') {
                jAlert('<strong>No Files to delete </strong>', projname);
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
        }
        
        function validnonaddmorefields() {

            if (!DropDownValidation('DdlGender', '0', 'Applicant Salutation', projname)) {
                return false;
            }
            if (!blankFieldValidation('TxtApplicantName', 'Applicant Name', projname)) {
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
                        jAlert('<strong> Please fill Aadhar number.</strong>', projname);
                        return false;
                    }
                    var adhar = $('#TxtAdhaar1').val();
                    if (adhar.length < 12) {
                        jAlert('Aadhaar no should be 12 digits.', projname);
                        return false;
                    }
                }
                if (rbtnVal == "2") {
                    if (blankFieldValidation('hdnAUTHORIZEDFILE', 'Please upload Authorizing letter signed by Authorized Signatory !', projname) == false) {
                        return false;
                    }
                }
            }

            if (!blankFieldValidation('txtbrief', 'Brief of Technical know how')) {
                return false;
            }

            if ($("#<%=grdindigious.ClientID %> tr").length == 0) {
                jAlert('<strong>Please add Source of Obtaining Technical Know How</strong>', projname);
                $('#ddlimp').focus();
                return false;
            }

            if ($("#<%=grdindigious.ClientID %> tr").length == 0) {
                jAlert('<strong>Please add Source of Obtaining Technical Know How</strong>', projname);
                $('#ddlimp').focus();
                return false;
            }
        }

        function validtechnicalgrd() {

            if (!DropDownValidation('ddlimp', 0, 'Imported/indigenous', projname)) {
                return false;
            }           
            if (!blankFieldValidation('txtagenname', 'Agency Name', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtagenadd', 'Agency Address', projname)) {
                return false;
            }
            if ($("#FUPProfdoc").val() == "") {
                jAlert('<strong>Please upload profile document</strong>', projname);
                return false;
            }

            if ($("#ddlimp").val() == '1') {
                if (!blankFieldValidation('txtimported', 'Permission Obtained', projname)) {
                    return false;
                }
            }
            if (!blankFieldValidation('txtagenamt', 'Expenditure Amount', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtbill', 'Bill No', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtbilldate', 'Bill date', projname)) {
                return false;
            }
            if (!blankFieldValidation('txtbillamt', 'Total bill amount', projname)) {
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

        function showimported(ctrlval) {

            var vimpval = ctrlval;
            if (vimpval == '1') {
                $('.techcol').show();

            }
            else {
                $('.techcol').hide();

            }

        }
            
    </script>
    <style>
        .hidelist
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
                <div class="investrs-tab">
                    <uc5:PMenu ID="PMenu" runat="server" />
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
                                                            Aadhaar No.</label>
                                                        <div class="col-sm-6  ">
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
                                                            <asp:Label runat="server" ID="lblAuthorizing"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hidAuthorizing" />
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FlupAUTHORIZEDFILE" onchange="return FileCheck(this,'pdf','pdf',4);"
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
                                                            <%--   <asp:DropDownList CssClass="form-control" ID="DrpDwn_Gender_Partner"
                                                                            runat="server">
                                                                            <asp:ListItem Value="1">Mr.</asp:ListItem>
                                                                            <asp:ListItem Value="2">Ms.</asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                        </div>
                                                        <%-- <div class="col-sm-5">
                                                                        <asp:TextBox ID="Txt_Partner_Name" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </div>--%>
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
                                                            <%--<asp:RadioButtonList ID="Rad_Is_Priority" class="optradioPriority" runat="server"
                                                                            RepeatDirection="Horizontal" onchange="return OnChangePriority();">
                                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="2">No</asp:ListItem>
                                                                        </asp:RadioButtonList>--%>
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
                                                            <label class="col-sm-5">
                                                                <asp:Label ID="Lbl_Pioneer_Doc_Name" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="Hid_Pioneer_Doc_Code" runat="server" />
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
                                        <div class="panel-heading" role="tab" id="Div2">
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
                                                                        Direct Employment in Numbers<small>(on Company Payroll)</small><span class="text-red">*</span>
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
                                                                        Direct Employment in Numbers<small>(on Company Payroll)</small><span class="text-red">*</span>
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
                                                                Date of First Fixed Capital Investment (in land/Building/plant and machinery & Balancing
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
                                                                Date of First Fixed Capital Investment (in land/Building/plant and machinery & Balancing
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
                                                    <label class="col-sm-5">
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
                                        <div class="panel-heading" role="tab" id="Div5">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#TechnicalKnowHowClaimDetails" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-minus"></i><small><span class="text-red pull-right">All Amounts
                                                        Enter in INR(in Lakhs)&nbsp;&nbsp;</span></small>Technical Know How Claim Details
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="TechnicalKnowHowClaimDetails" class="panel-collapse collapse in" role="tabpanel"
                                            aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4">
                                                            Brief on technical know-how ( how purchased )</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtbrief" MaxLength="300" CssClass="form-control" runat="server"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="flt1" runat="server" TargetControlID="txtbrief"
                                                                FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                ValidChars=",,-,/\, ,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <h4 class="h4-header">
                                                                Source of Obtaining Technical Know How</h4>
                                                        </div>
                                                        <div class="col-sm-12">
                                                            <div class="table-responsive">
                                                                <table class="table table-bordered">
                                                                    <tr>
                                                                        <th style="width: 10%;">
                                                                            Imported /Indigenous <a data-toggle="tooltip" class="fieldinfo2" style="padding-left: 10px">
                                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                        </th>
                                                                        <th style="width: 10%;">
                                                                            Name of the Agency
                                                                        </th>
                                                                        <th style="width: 10%;">
                                                                            Address of the agency
                                                                        </th>
                                                                        <th style="width: 10%;">
                                                                            Profile
                                                                            <br />
                                                                            (upload document)
                                                                        </th>
                                                                        <th class="techcol" style="width: 10%;">
                                                                            Permission obtained from (GoI/ dept of GOI)
                                                                        </th>
                                                                        <th style="width: 10%;">
                                                                            Amount of expenditure
                                                                        </th>
                                                                        <th style="width: 15%;">
                                                                            Bill no
                                                                        </th>
                                                                        <th style="width: 10%;">
                                                                            Bill date
                                                                        </th>
                                                                        <th style="width: 10%;">
                                                                            Total bill amount
                                                                        </th>
                                                                        <th style="width: 5%;">
                                                                            Add More
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 10%;">
                                                                            <asp:DropDownList ID="ddlimp" CssClass="form-control ddlImportedIndigenous" runat="server">
                                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">Imported</asp:ListItem>
                                                                                <asp:ListItem Value="2">Indigenous</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width: 10%;">
                                                                            <asp:TextBox ID="txtagenname" MaxLength="30" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtagenname"
                                                                                FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                                ValidChars=",,-,/\, ,.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td style="width: 10%;">
                                                                            <asp:TextBox ID="txtagenadd" MaxLength="30" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtagenadd"
                                                                                FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                                ValidChars=",,-,/\, ,.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td style="width: 10%;">
                                                                            <asp:LinkButton ID="LinkButton2" CssClass="btn btn-danger btn-sm" data-toggle="tooltip"
                                                                                title="Upload profile document" runat="server" OnClientClick="return OpenUploadModal(FUPProfdoc);"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                                            <asp:FileUpload ID="FUPProfdoc" CssClass="form-control" runat="server" Style="display: none;"
                                                                                onchange="return FileCheckGrid(this, lblProfDoc, 'pdf','pdf',4);" />
                                                                            <asp:Label ID="lblProfDoc" runat="server"></asp:Label>
                                                                            <asp:HiddenField ID="hdnProfdoc" runat="server" />
                                                                        </td>
                                                                        <td class="techcol" style="width: 10%;">
                                                                            <asp:TextBox ID="txtimported" MaxLength="30" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtimported"
                                                                                FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                                ValidChars=",,-,/\, ,.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td style="width: 10%;">
                                                                            <asp:TextBox ID="txtagenamt" CssClass="form-control" MaxLength="10" runat="server"
                                                                                onblur="DecimalValidation(this);" onkeypress="return  FloatOnly(event, this);"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtagenamt"
                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                            <asp:TextBox ID="txtbill" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtbill"
                                                                                FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                                ValidChars=",,-,/\, ,.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-danger btn-sm" data-toggle="tooltip"
                                                                                title="Upload copies orginal bills" runat="server" OnClientClick="return OpenUploadModal(FUPbilldoc);"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                                            <asp:FileUpload ID="FUPbilldoc" runat="server" Style="display: none;" onchange="return FileCheckGrid(this, lblBillDoc, 'pdf','pdf',4);" />
                                                                            <asp:HiddenField ID="hdnbilldoc" runat="server" />
                                                                            <asp:Label ID="lblBillDoc" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%;">
                                                                            <div class="input-group  date datePicker" id="Div12">
                                                                                <asp:TextBox ID="txtbilldate" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>
                                                                        </td>
                                                                        <td style="width: 10%;">
                                                                            <asp:TextBox ID="txtbillamt" CssClass="form-control" MaxLength="10" runat="server"
                                                                                onblur="DecimalValidation(this);" onkeypress="return  FloatOnly(event, this);"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtbillamt"
                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
                                                                        </td>
                                                                        <td style="width: 5%;">
                                                                            <asp:LinkButton ID="LinkButton23" CssClass="btn btn-success btn-sm" runat="server"
                                                                                OnClientClick="return validtechnicalgrd();" OnClick="LinkButton23_Click"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <br />
                                                                <asp:GridView ID="grdindigious" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                                                    OnRowDeleting="grdindigious_RowDeleting" OnRowDataBound="grdindigious_RowDataBound">
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="Imported /Indigenous" DataField="vchimport" ItemStyle-Width="10%" />
                                                                        <asp:BoundField HeaderText="Agency Name" DataField="vchagenname" ItemStyle-Width="10%" />
                                                                        <asp:BoundField HeaderText="Agency Address" DataField="vchagenadd" ItemStyle-Width="10%" />
                                                                        <asp:TemplateField HeaderText="Profile Document" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="hypProf" runat="server" Target="_blank" Text='<%# Eval("vchprof") %>'
                                                                                    Visible="false"></asp:HyperLink>
                                                                                <asp:HiddenField ID="hdnRowId" Value='<%# Eval("dcRowId") %>' runat="server" />
                                                                                <asp:HiddenField ID="hdnimportid" Value='<%# Eval("vchimportid") %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Permission Obtained" DataField="vchpermi" ItemStyle-Width="10%" />
                                                                        <asp:BoundField HeaderText="Amount of Expenditure" DataField="vchamt" ItemStyle-Width="10%" />
                                                                        <asp:TemplateField HeaderText="Bill No." ItemStyle-Width="15%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblbill" runat="server" Text='<%# Eval("vchbillno") %>'></asp:Label>
                                                                                <asp:HyperLink ID="hypBill" runat="server" Target="_blank" Text='<%# Eval("vchbill") %>'
                                                                                    Visible="false"></asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Bill Date" DataField="vchbilldt" ItemStyle-Width="10%" />
                                                                        <asp:BoundField HeaderText="Total Bill Amount" DataField="vchbillamt" ItemStyle-Width="10%" />
                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnbtechdel" runat="server" CommandName="delete" CssClass="btn btn-danger btn-sm"
                                                                                    ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
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
                                                    <i class="more-less fa  fa-plus"></i><small><span class="text-red pull-right">All Amounts
                                                        Enter in INR(in Lakhs) &nbsp;&nbsp; </span></small>Incentives Availed Earlier
                                                    Details</a>
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
                                                            <asp:TextBox ID="txtreimamt" MaxLength="10" runat="server" onblur="DecimalValidation(this);"
                                                                onkeypress="return  FloatOnly(event, this);" CssClass="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtreimamt"
                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
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
                                                            <asp:RadioButtonList ID="RadBtn_Availed_Earlier" runat="server" onchange="onAvailChange();"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="dvfup">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5 ">
                                                            Undertaking on non-availment of subsidy earlier on this project :
                                                            <asp:HiddenField ID="hdnundertakingdocid" runat="server" Value="D230" />
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FU_Undertaking_Doc" CssClass="form-control" onchange="return FileCheck(this, 'pdf,zip','pdf/zip', 4);"
                                                                    runat="server" />
                                                                <asp:LinkButton ID="LnkBtn_Upload_Undertaking_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return blnkfileuploadchecking(FU_Undertaking_Doc);" OnClick="LnkBtn_Upload_Undertaking_Doc_Click"
                                                                    ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LnkBtn_Delete_Undertaking_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="LnkBtn_Delete_Undertaking_Doc_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
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
                                                <div class="form-group" id="dvgrd">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12">
                                                            Details of Incentive already availed for the Technical know-how being applied for
                                                            now
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
                                                                        <asp:TextBox ID="txtagency" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtagency"
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
                                                                        <asp:TextBox ID="txtsacord" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtsacord"
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
                                                                        <asp:TextBox ID="txtavilamt" MaxLength="10" runat="server" onblur="DecimalValidation(this);"
                                                                            onkeypress="return  FloatOnly(event, this);" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtavilamt"
                                                                            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="LinkButton41" CssClass="btn btn-success btn-sm" OnClick="LinkButton41_Click"
                                                                            runat="server" OnClientClick="return validAvailgrid();"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:GridView ID="grdAssistanceDetailsAD" runat="server" CssClass="table table-bordered"
                                                                AutoGenerateColumns="false" ShowFooter="false" ShowHeader="false" OnRowDeleting="grdAssistanceDetailsAD_RowDeleting">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Slno." ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Disbursing Agency">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblBody" Text='<%# Eval("vchagency") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sanctioned Amount" ItemStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblName" Text='<%# Eval("vchsacamt") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sanction Order No." ItemStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblAmountAvailed" Text='<%# Eval("vchsacord") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date of Sanction" ItemStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblAvailedDate" Text='<%# Eval("vchsacdat") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Availed Amount" ItemStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblSanctionOrderNo" Text='<%# Eval("vchavilamt") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="7%">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDelRecord" CommandName="delete" CssClass="btn btn-danger btn-sm"
                                                                                runat="server" ToolTip="Remove"><i class="fa fa-trash"></i>
                                                                            </asp:LinkButton>
                                                                            <asp:HiddenField ID="hdnRowId" runat="server" Value='<%#Eval("dcRowId")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
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
                                                                    <asp:FileUpload ID="FU_Asst_Sanc_Doc" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip','pdf/zip', 4);" />
                                                                    <asp:LinkButton ID="LnkBtn_Upload_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClientClick="return blnkfileuploadchecking(FU_Asst_Sanc_Doc);" OnClick="LnkBtn_Upload_Asst_Sanc_Doc_Click"
                                                                        ToolTip="Upload"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="LnkBtn_Delete_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="LnkBtn_Delete_Asst_Sanc_Doc_Click" ToolTip="Delete" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
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
                                                                Amount of Differential Claim to be Exempted
                                                            </label>
                                                            <div class="col-sm-5">
                                                                <span class="colon">:</span>
                                                                <asp:TextBox ID="txtdiffclaimamt" MaxLength="10" runat="server" onblur="DecimalValidation(this);"
                                                                    onkeypress="return  FloatOnly(event, this);" CssClass="form-control"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtdiffclaimamt"
                                                                    FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
                                                            </div>
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
                                                    href="#AdditionalDocuments" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-plus"></i>Other Documents </a>
                                            </h4>
                                        </div>
                                        <div id="AdditionalDocuments" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="" id="div3" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-0">
                                                            OSPCB consent to Operate (except white category) <a data-toggle="tooltip" class="fieldinfo2"
                                                                title="1. FSSAI/Food License- For food processing unit 
                                                                                                            2. Explosive License -For Explosive manufacturing unit 
                                                                                                            3. BIS Certification -For Packaged drinking water ">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flValidStatutary" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'zip','zip', 4);" />
                                                                <asp:HiddenField ID="hdnValidStatutaryCode" runat="server" Value="D275" />
                                                                <asp:HiddenField ID="hdnIsOsPCBDownloaded" runat="server" />
                                                                <asp:LinkButton ID="lnkUValidStatutary" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkUValidStatutary_Click" ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDValidStatutary" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDValidStatutary_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypValidStatutary" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblValidStatutary" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdnvalidstatutaryfile" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="" id="div9" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-0">
                                                            Sector Relevant Document <a data-toggle="tooltip" class="fieldinfo2" title="Except white category">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flDelay" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip','pdf/zip', 4);" />
                                                                <asp:HiddenField ID="hdnDelayCode" runat="server" Value="D274" />
                                                                <asp:LinkButton ID="lnkUDelay" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkUDelay_Click" ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDDelay" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDDelay_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypDelay" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblDelay" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdndelaydocfile" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="" id="div10" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-0">
                                                            Factory & Boiler - For all industry (10 with direct employment / 20 no of employment
                                                            with power ) <a data-toggle="tooltip" class="fieldinfo2" title="Factory & Boiler-  For all industry (10 with direct employment / 20 no of employment with power ) ">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flCleanApproveAuthority" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this, 'zip','zip', 4);" />
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
                                                            <asp:HiddenField ID="hdnBoiler" runat="server" />
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
                                                            <asp:Image runat="server" ID="ImgSector" Height="15" Width="15" src="../images/cancel-square.png" />
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
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-footer">
                        <div class="row">
                            <div class="col-sm-12 text-right">
                                <asp:Button ID="Button1" runat="server" Text="Save As Draft" CssClass="btn btn-warning"
                                    OnClientClick="return validformindraft();" OnClick="Button1_Click" />
                                <asp:Button ID="btnEdit" runat="server" Text="Apply" CssClass="btn btn-success" OnClientClick="return validnonaddmorefields();"
                                    OnClick="btnEdit_Click" />
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
        function pageLoad() {

            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails, #TechnicalKnowHowClaimDetails, #AvailedClaimDetails, #AdditionalDocuments, #DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").on("click", function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });

            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy"
                });
            });
            OnChangeApplyBy();
            onAvailChange();

            DocCheckList();

            $("#ddlimp").change(function () {

                if ($(this).find('input').val() == '1') {

                    $('a.fieldinfo2').prop('title', 'Any proprietary series of practical, non-patented knowledge and owners experience or tests which is secret, substantial and identified in nature, when brought into India from outside India');

                }
                if ($(this).find('input').val() == '2') {

                    $('a.fieldinfo2').prop('title', 'Technical know-how imparted by any Govt Agency/any other organization approved by the state Govt or Union Govt in the country shall be treated as indigenous');

                }


                showimported(this.value);

            });

            $(".edexem").change(function () {
                if ($(this).find('input').val() == 'rdbdutyyes') {
                    $('.exem_details').show();
                }
                if ($(this).find('input').val() == 'rdbdutyno') {
                    $('.exem_details').hide();
                }
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
            ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#Imgsanction').attr("id"));
            ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#ImgUndertaking').attr("id"));
            ImgSrc($('#hdndelaydocfile').val(), $('#ImgSector').attr("id"));
            ImgSrc($('#hdnvalidstatutaryfile').val(), $('#ImgOSPCB').attr("id"));
            ImgSrc($('#D280').val(), $('#Imgboiler').attr("id"));

        }
    

    </script>
    </form>
</body>
</html>
