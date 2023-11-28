<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Exemption_Premium_Land_Conversion.aspx.cs"
    Inherits="incentives_Exemption_Premium_Land_Conversion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/Incentive/JS_Inct_Common_Validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                $('.innertabs ,.investrs-tab').hide();
                window.print();
                $('.innertabs ,.investrs-tab').show();
            });
            $('.Pioneersec,.attorneysec,.adhardetails').hide();
            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails, #LandDetails, #AdditionalDocuments, #DivDocCheck").removeClass('in');
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



        });

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

        function HasFile(fuControlId, strError) {
            if ($('#' + fuControlId).val() == "") {
                jAlert(strError, msgTitle);
                return false;
            }
        }

        function OnChangePriority() {
            $('.Pioneersec').hide();
            if ($("input[name='RadIsPriority']:checked").val() == '1') {
                $('.Pioneersec').show();
                $("#FluPinoneerDoc").val(null);
                $('#LnkPinoneerDoc').text('');
            }
            else if ($("input[name='RadIsPriority']:checked").val() == '2') {
                $('.Pioneersec').hide();
                $("#FluPinoneerDoc").val(null);
                $('#LnkPinoneerDoc').text('');
            }
        }



        function SameAddressIndustry() {
            var cc = $('#TxtAddressInd').val();
            if ($("#ChkSameData").is(':checked')) {
                $('#TxtRegAddress').val(cc);
            }
        }

        function CountMultiLine(text, maxlength) {
            var object = document.getElementById(text.id)  //get your object
            if (object.value.length > maxlength) {
                object.focus(); //set focus to prevent jumping
                object.value = text.value.substring(0, maxlength); //truncate the value
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            return true;
        }
        /////////////////// jquery method for Industrial Unit////////////////////////////////////////

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
            if ($("#txtOtherFixedAssests").val() != '') {
                cal = cal + parseFloat($("#txtOtherFixedAssests").val());
            }
            $("#lblTotalAmount").text(cal);
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

        function IndustryUnitValidation() {

            if (!DropDownValidation('DdlGender', '0', 'Applicant Salutation', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('TxtApplicantName', 'Applicant Name', msgTitle)) {
                return false;
            }
            if (($("input[name='radApplyBy']:checked").val() != '1') && ($("input[name='radApplyBy']:checked").val() != '2')) {
                jAlert('Please select Application Applying By option', msgTitle);
                return false;
            }
            if ($("input[name='radApplyBy']:checked").val() == '1') {
                if (!blankFieldValidation('TxtAdhaar1', 'Aadhaar no', msgTitle)) {
                    return false;
                };
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('Aadhaar no should be 12 digits.', msgTitle);
                    return false;
                }
            }
            if ($("input[name='radApplyBy']:checked").val() == '2') {
                if ($("#hdnAUTHORIZEDFILE").val() == '') {
                    jAlert('Please provide Authorizing letter signed by Authorized Signatory.', msgTitle);
                    return false;
                }
            }
            return true;

        }

        function LandDetails() {
            if (!blankFieldValidation('txtCostofProject', 'Cost of the Project', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtLandRequiredAsperRpt', 'Area of Land required as per DPR', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtLandRequired', 'Area of Land acquired', msgTitle)) {
                return false;
            }
            if ($('#txtLandRequired').val() > 0) {
                var rowcount = $("#grvLandInfo tr").length;
                if (rowcount == "0") {
                    jAlert('<strong>Please enter atleast one Items of Details of Particulars of Land to be converted.</strong>', msgTitle);
                    return false;
                }
            }
            if ($('#hdnFileUpload').val() == '') {
                jAlert('<strong>Please Upload Land document.</strong>', msgTitle);
                return false;
            }
            return true;
        }

        function SaveDraft() {
            if ($('#TxtAdhaar1').val() != "") {
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('Aadhaar no should be 12 digits.', msgTitle);
                    return false;
                }
            }
        }

        function SaveApplyBy() {
            debugger;
            if (!IndustryUnitValidation()) {
                return false;
            }
            if (!LandDetails()) {
                return false;
            }
        }

        var msgTitle = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'; //'SWP';

        function validationLandDetails() {
           
            if (!blankFieldValidation('txtMouza', 'Mouza', msgTitle)) {
                return false;
            }
            //Khata No
            if (!blankFieldValidation('txtKhataNo', 'Khata No', msgTitle)) {
                return false;
            }

            //PlotNo
            if (!blankFieldValidation('txtPlotNo', 'Plot No', msgTitle)) {
                return false;
            }

            //Area
            if (!blankFieldValidation('txtArea', 'Area', msgTitle)) {
                return false;
            };
            if (!blankFieldValidation('txtLandRequired', 'Area of Land acquired (in Acres)', msgTitle)) {
                return false;
            };


            var rowcount = $("#grvLandInfo tr").length;
            if (rowcount == "0") {
                if (parseFloat($('#txtArea').val()) > parseFloat($('#txtLandRequired').val())) {
                    jAlert('<strong>Particulars of Land to be converted area cannot be more than Area of Land acquired. </strong>', msgTitle);
                    return false;
                }
            }
            else {
                var total = 0;
                $('#grvLandInfo tbody tr td span.lblArea').each(function () {
                    total = parseFloat(total) + parseFloat($(this).text());
                });
                total = total + parseFloat($('#txtArea').val());
                if (total > parseFloat($('#txtLandRequired').val())) {
                    jAlert('<strong>Sum of Particulars Land to be converted area cannot be more than Area of Land acquired. </strong>', msgTitle);
                    return false;
                }
            }
            //Present Kisam
            if (!blankFieldValidation('txtKisam', 'Present Kisam', msgTitle)) {
                return false;
            }
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                                    <li class="active"><a href="appliedlistwithdetails.aspx">Apply For incentive</a></li>
                                    <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                                </ul>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-header">
                                <%-- <div class="iconsdiv">
                                    <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                        <i class="fa fa-print"></i></a>
                                </div>--%>
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
                                                            <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
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
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtAdhaar1"
                                                                FilterType="Numbers">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group attorneysec" id="divAuthorizing" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Please provide Authorizing letter signed by Authorized Signatory<a data-toggle="tooltip"
                                                                class="fieldinfo2" title="Provide Authorizing letter signed by Authorized Signatory">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <asp:Label runat="server" ID="lblAuthorizing"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hidAuthorizing" />
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FlupAUTHORIZEDFILE" onchange="return FileCheck(this, 'pdf','pdf',4);"
                                                                    runat="server" CssClass="form-control" />
                                                                <asp:HiddenField ID="hdnAUTHORIZEDFILE" runat="server" />
                                                                <asp:LinkButton ID="lnkAUTHORIZEDFILE" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkUpload_Click" ToolTip="Click here to upload the file." OnClientClick="return HasFile('FlupAUTHORIZEDFILE','Please provide Authorizing letter signed by Authorized Signatory');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkAUTHORIZEDFILEDdelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
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
                                                    <div class="form-group" id="Div_Pioneer_Doc" runat="server">
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
                                                    <i class="more-less fa  fa-plus"></i>Production & Employment Details </a>
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
                                                                                <asp:Label ID="lbl_Semi_Skilled_Before" runat="server" Text="0"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                Total
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Total_Cast_Emp_Before" runat="server" Text="0"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Un Skilled
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Unskilled_Before" runat="server" Text="0"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                Women
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Women_Before" runat="server" Text="0"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Total
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Total_Emp_Before" runat="server" Text="0"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                Differently Abled Persons
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_PHD_Before" runat="server" Text="0"></asp:Label>
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
                                                                        Direct Employment In Numbers<small>(on Company Payroll)</small>
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
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                <small class="text-gray">
                                                                    <asp:Label ID="Lbl_FFCI_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                                    <asp:HiddenField ID="Hid_FFCI_Before_Doc_Code" runat="server" />
                                                                </small>
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:HyperLink ID="Hyp_View_FFCI_Before_Doc" runat="server" Target="_blank" class="btn btn-info"><i class="fa fa-download"></i></asp:HyperLink>
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
                                                                                    Other Fixed Assests</label>
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
                                                    <label class="col-sm-4 ">
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
                                                    </i>Land Details<span class="text-red pull-right " style="margin-right: 20px;">* All
                                                        fields in this section are mandatory</span></a>
                                            </h4>
                                        </div>
                                        <div id="LandDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                            <p class="text-red text-right">
                                                All Amounts to be Entered in INR(in Lakh)</p>
                                            <div class="panel-body">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-5 ">
                                                                    Cost of the Project ( New / Existing & E/M/D)</label>
                                                                <div class="col-sm-5">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="txtCostofProject" runat="server" CssClass="form-control" MaxLength="15"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1226" runat="server" TargetControlID="txtCostofProject"
                                                                        FilterType="Custom,Numbers" ValidChars="." />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-5 ">
                                                                    Area of Land required as per DPR / Project report (in Acres)</label>
                                                                <div class="col-sm-5">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="txtLandRequiredAsperRpt" runat="server" CssClass="form-control"
                                                                        MaxLength="15" onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtLandRequiredAsperRpt"
                                                                        FilterType="Custom,Numbers" ValidChars="." />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-5">
                                                                    Area of Land acquired (in Acres)
                                                                </label>
                                                                <div class="col-sm-5">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="txtLandRequired" runat="server" CssClass="form-control" MaxLength="15"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtLandRequired"
                                                                        FilterType="Custom,Numbers" ValidChars="." />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-8 ">
                                                                    Particulars of Land to be converted
                                                                </label>
                                                                <div class="col-sm-12  margin-bottom10">
                                                                    <table class="table table-bordered">
                                                                        <tr>
                                                                            <th width="5%">
                                                                                Slno.
                                                                            </th>
                                                                            <th>
                                                                                Mouza
                                                                            </th>
                                                                            <th width="18%">
                                                                                Khata No
                                                                            </th>
                                                                            <th width="18%">
                                                                                Plot No
                                                                            </th>
                                                                            <th width="18%">
                                                                                Area (in Acres)
                                                                            </th>
                                                                            <th width="18%">
                                                                                Present Kisam
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
                                                                                <asp:TextBox ID="txtMouza" runat="server" CssClass="form-control" MaxLength="80"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtMouza"
                                                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" Enabled="True"
                                                                                    FilterMode="ValidChars" ValidChars=" -,./">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtKhataNo" runat="server" CssClass="form-control" MaxLength="80"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtKhataNo"
                                                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" Enabled="True"
                                                                                    FilterMode="ValidChars" ValidChars=" -,./">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPlotNo" runat="server" CssClass="form-control" MaxLength="80"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtPlotNo"
                                                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" Enabled="True"
                                                                                    FilterMode="ValidChars" ValidChars=" /-">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtArea" runat="server" CssClass="form-control" MaxLength="15" onblur="DecimalValidation(this);"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtArea"
                                                                                    FilterType="Custom,Numbers" Enabled="True" FilterMode="ValidChars" ValidChars=".">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtKisam" runat="server" CssClass="form-control" MaxLength="80"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtKisam"
                                                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" Enabled="True"
                                                                                    FilterMode="ValidChars" ValidChars=" -,./">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="lnkAddMoreLand" CssClass="btn btn-success btn-sm" runat="server"
                                                                                    OnClientClick="return validationLandDetails()" OnClick="lnkAddMore_Click"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                                <asp:HiddenField ID="hdnAddMore" Value="Add" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <asp:GridView ID="grvLandInfo" runat="server" CssClass="table table-bordered table-condensed"
                                                                        AutoGenerateColumns="false" ShowHeader="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblslno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMouza" runat="server" Text='<%# Eval("Mouza") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblKhataNo" runat="server" Text='<%# Eval("KhataNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="18%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPlotNo" runat="server" Text='<%# Eval("PlotNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="18%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblArea" runat="server" Text='<%# Eval("Area") %>' class="lblArea"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="18%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPresentKisam" runat="server" Text='<%# Eval("PresentKisam") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="18%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ImageButtonDelete" runat="server" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                                        CommandArgument='<%# Container.DataItemIndex %>' OnClick="imgbtnDelete_Click" />
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
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Land document with land particulars to be converted for industrial use <a data-toggle="tooltip"
                                                                class="fieldinfo2" title="Land document with land particulars to be converted for industrial
                                                                use"><i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fldLandDocument" runat="server" CssClass="form-control" onchange="return FileCheck(this, 'pdf','pdf',4);" />
                                                                <asp:HiddenField ID="hdnFileUpload" runat="server" />
                                                                <asp:HiddenField ID="hdnFileUploadID" runat="server" Value="D124" />
                                                                <asp:LinkButton ID="lnkLandfileupload" CssClass="input-group-addon bg-green" data-toggle="tooltip"
                                                                    ToolTip="Click here to upload the file." title="Upload" runat="server" OnClientClick="return HasFile('fldLandDocument','Please Upload Land document with land particulars to be converted for industrial');"
                                                                    OnClick="lnkUpload_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkLandDocumentDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDelete_Click" Visible="false" ToolTip="Click here to delete the uploaded file."><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="lnkLandfileview" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue" ToolTip="Click here to download the file.">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblLandDocument" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Undertaking on non-judicial Stamp Paper duly signed by the applicant <a data-toggle="tooltip"
                                                                class="fieldinfo2" title="Undertaking on non-judicial Stamp Paper duly signed by the applicant
                                                                use"><i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fldLandUndertaking" runat="server" CssClass="form-control" onchange="return FileCheck(this, 'pdf','pdf',4);" />
                                                                <asp:HiddenField ID="hdnLandUndertaking" runat="server" />
                                                                <asp:HiddenField ID="hdnLandUndertakingID" runat="server" Value="D281" />
                                                                <asp:LinkButton ID="lnkUpLandUndertaking" CssClass="input-group-addon bg-green" data-toggle="tooltip"
                                                                    OnClick="lnkUpload_Click" ToolTip="Click here to upload the file." title="Upload"
                                                                    runat="server" OnClientClick="return HasFile('fldLandUndertaking','Please Upload Undertaking on non-judicial Stamp Paper duly signed by the applicant');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelLandUndertaking" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDelete_Click" Visible="false" ToolTip="Click here to delete the uploaded file."><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypLandUndertaking" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue" ToolTip="Click here to download the file.">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblLandUndertaking" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="Div4">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#AdditionalDocuments" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-plus"></i>Other Documents</a>
                                            </h4>
                                        </div>
                                        <div id="AdditionalDocuments" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-0">
                                                            OSPCB consent to Establishment <a data-toggle="tooltip" class="fieldinfo2" title="Except white category">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flValidStatutary" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'zip','zip',4);" />
                                                                <asp:HiddenField ID="D275" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnIsOsPCBDownloaded" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUValidStatutary" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flValidStatutary','Please Upload OSPCB consent to establishment related Document');"
                                                                    OnClick="lnkUValidStatutary_click" ToolTip="Click here to upload the file.">
                                                                <i class="fa fa-upload" aria-hidden="true"></i>                      
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDValidStatutary" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDValidStatutary_Click" Visible="false">

                                                         <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <asp:HyperLink ID="hypValidStatutary" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue">
                                                           <i class="fa fa-download"></i>
                                                                </asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblValidStatutary" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-0">
                                                            Sector Relevant Document <a data-toggle="tooltip" class="fieldinfo2" title="1. FSSAI/Food License- For food processing unit 
                                                                                                            2. Explosive License -For Explosive manufacturing unit 
                                                                                                            3. BIS Certification -For Packaged drinking water ">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flDelay" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip','pdf/zip',4);" />
                                                                <asp:HiddenField ID="D274" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUDelay" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flDelay','Please Upload Sector Relevant Document');"
                                                                    OnClick="lnkUDelay_Click" ToolTip="Click here to upload the file.">
                                                                <i class="fa fa-upload" aria-hidden="true"></i>                      
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDDelay" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDDelay_Click" Visible="false">
                                                         <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <asp:HyperLink ID="hypDelay" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                           <i class="fa fa-download"></i>
                                                                </asp:HyperLink>
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
                                                                    onchange="return FileCheck(this, 'zip','zip',4);" />
                                                                <asp:HiddenField ID="D280" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnBoilderDownloaded" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUCleanApproveAuthority" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Please Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file." OnClick="lnkUCleanApproveAuthority_Click">
                                                                <i class="fa fa-upload" aria-hidden="true"></i>                      
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDCleanApproveAuthority" runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false" OnClick="lnkDCleanApproveAuthority_Click">
                                                         <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <asp:HyperLink ID="hypCleanApproveAuthority" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue">
                                                           <i class="fa fa-download"></i>
                                                                </asp:HyperLink>
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
                                                            Land document with land particulars to be converted for industrial use
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgLandDoc" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Undertaking on non-judicial Stamp Paper duly signed by the applicant
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgLandUndertaking" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            OSPCB consent to Establishment
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
                            <div class="form-footer">
                                <div class="row">
                                    <div class="col-sm-12 text-right">
                                        <asp:Button ID="btnEdit" runat="server" Text="Save As Draft" CssClass="btn btn-warning"
                                            OnClientClick="return SaveDraft();" OnClick="btnEdit_Click" />
                                        <asp:Button ID="BtnApply" runat="server" Text="Apply" CssClass="btn btn-success"
                                            OnClientClick="return SaveApplyBy();" OnClick="BtnApply_Click" />
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
            OnChangePriority();
            DocCheckList();


            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails, #LandDetails, #AdditionalDocuments, #DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").on("click", function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });


        }
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
            ImgSrc($('#hdnFileUpload').val(), $('#ImgLandDoc').attr("id"));
            ImgSrc($('#hdnLandUndertaking').val(), $('#ImgLandUndertaking').attr("id"));
            ImgSrc($('#D275').val(), $('#ImgOSPCB').attr("id"));
            ImgSrc($('#D274').val(), $('#ImgSectRel').attr("id"));
            ImgSrc($('#D280').val(), $('#ImgCleanApproveAuthority').attr("id"));

        }

        
    </script>
    </form>
</body>
</html>
