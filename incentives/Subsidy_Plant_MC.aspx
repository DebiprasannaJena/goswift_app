<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Subsidy_Plant_MC.aspx.cs"
    Inherits="Subsidy_Plant_MC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<script src="../js/WebValidation.js" type="text/javascript"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/Incentive/JS_Inct_Common_Validation.js"></script>
    <script type="text/javascript" language="javascript">

        function SameAddressIndustry() {
            var cc = $('#TxtAddressInd').val();
            if ($("#ChkSameData").is(':checked')) {
                $('#TxtRegAddress').val(cc);
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
        function OnChangeAvailed() {

            $('.availgroup1,.availgroup2').hide();
            if ($("input[name='RadBtn_Availed_Earlier']:checked").val() == '1') {
                $('.availgroup1').show();
                $('.availgroup2').hide();
                ImgSrc('', $('#ImgSubsidyAvailed').attr("id"));
                ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#ImgSupportingDocs').attr("id"));
            }
            else if ($("input[name='RadBtn_Availed_Earlier']:checked").val() == '2') {
                $('.availgroup2').show();
                $('.availgroup1').hide();
                ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#ImgSubsidyAvailed').attr("id"));
                ImgSrc('', $('#ImgSupportingDocs').attr("id"));
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
    </script>
    <script type="text/javascript">
        function openpopup(flu) {
            var i = flu.id;
            $("#" + i).click();
            return false;
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
        $(document).ready(function () {
            OnChangeAvailed();


            $('#txtQuantity').blur(function () {
                var amount = $('#txtQuantity').val();
                amount = Number(amount).toFixed(2);
                if (isNaN(amount)) {
                    amount = Number(0).toFixed(2);
                }
                $('#txtQuantity').val(amount);
            });
            $('#txtValue').blur(function () {
                var amount = $('#txtValue').val();
                amount = Number(amount).toFixed(2);
                if (isNaN(amount)) {
                    amount = Number(0).toFixed(2);
                }
                $('#txtValue').val(amount);
            });




            $('#fuEmployees').change(function (e) {
                var ext = $('#fuEmployees').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['PDF', 'pdf']) == -1) {
                    alert('Only Pdf file Applicable!');
                    $(this).val('');
                    $('#lnkFileNew').text('');
                    e.preventDefault();
                    return false;

                }

                if ((this.files[0].size > 2097152) && ($(this).val() != '')) {

                    alert('File size must be less then 2mb!');
                    $(this).val('');
                    $('#lnkFileNew').text('');
                    e.preventDefault();
                    return false;
                }
            });




            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
            $('.Pioneersec,.attorneysec,.adhardetails').hide();

        });



        function HasFile(fuControlId, strError) {
            if ($('#' + fuControlId).val() == "") {
                jAlert(strError, msgTitle);
                return false;
            }
        }   
    </script>
    <style>
        .not-active
        {
            pointer-events: none;
            cursor: default;
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
                                                            <asp:Label ID="lbl_Org_Type" runat="server" AutoPostBack="true">
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
                                                            <asp:TextBox ID="TxtAdhaar1" CssClass="form-control" placeholder="123412341234" runat="server"
                                                                MaxLength="12"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTxtAdhaar1" runat="server"
                                                                FilterType="Numbers" TargetControlID="TxtAdhaar1">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <div class="clerfix">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group attorneysec" id="divAuthorizing" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            <small class="text-gray">Please provide Authorizing letter signed by Authorized Signatory</small><a
                                                                data-toggle="tooltip" class="fieldinfo2" title="Provide Authorizing letter signed by Authorized Signatory">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <%-- <asp:RadioButtonList  runat="server" ID="radAuthorizing"></asp:RadioButtonList>--%>
                                                            <asp:Label runat="server" ID="lblAuthorizing"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hidAuthorizing" />
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FlupAUTHORIZEDFILE" onchange="return FileCheck(this, 'pdf', 'PDF',4);"
                                                                    runat="server" CssClass="form-control" />
                                                                <asp:HiddenField ID="hdnAUTHORIZEDFILE" runat="server" />
                                                                <asp:LinkButton ID="lnkAUTHORIZEDFILE" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="LnkUpAUTHORIZEDFILE_Click" ToolTip="Click here to upload the file."
                                                                    OnClientClick="return HasFile('FlupAUTHORIZEDFILE','Please provide Authorizing letter signed by Authorized Signatory');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkAUTHORIZEDFILEDdelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="LnkDelAUTHORIZEDFILE_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
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
                                                                <asp:Label ID="Lbl_Unit_Type_Doc_Name" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                            <asp:Label ID="Lbl_Org_Name_Type" runat="server" Text="Name of Managing Partner"
                                                                CssClass="form-control-static"></asp:Label>
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
                                                                <asp:Label ID="Lbl_Org_Doc_Type" runat="server" Text="Document in Support of Managing Partner"
                                                                    CssClass="form-control-static"></asp:Label>
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
                                                            <asp:Label for="Iname" CssClass="col-sm-4 col-sm-offset-1" Text="Date of Production Commencement- Before E/M/D"
                                                                ID="lblAfterEMD" runat="server"></asp:Label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Prod_Comm_Date_Before" runat="server" CssClass="form-control-static" />
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
                                                                <asp:Label ID="lbl_PC_Issue_Date_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                <small class="text-gray">
                                                                    <asp:Label ID="Lbl_Prod_Comm_Before_Doc_Name" runat="server" Text="Certificate on Date of Commencement of production- Before E/M/D"
                                                                        CssClass="form-control-static"></asp:Label>
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
                                                            <asp:Label for="Iname" class="col-sm-4 col-sm-offset-1" Text="PC Issuance Date After E/M/D"
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
                                                                    <asp:Label ID="Lbl_Pioneer_Doc_Name" runat="server" Text="" CssClass="form-control-static"></asp:Label>
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
                                                                    <div class="col-sm-12   ">
                                                                        <asp:GridView ID="Grd_Production_Before" runat="server" CssClass="table table-bordered"
                                                                            DataKeyNames="vchProductName" AutoGenerateColumns="false" EmptyDataText="No Records found...">
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
                                                                    <label for="Iname" class="col-sm-3 ">
                                                                        Direct Employment In Numbers<small>(on Company Payroll)</small><span class="text-red">*</span>
                                                                    </label>
                                                                    <div class="col-sm-3">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lbl_Direct_Emp_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                    <label for="Iname" class="col-sm-3 ">
                                                                        Contractual Employment IN NUMBERS</label>
                                                                    <div class="col-sm-3">
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
                                                                    <div class="col-sm-12   ">
                                                                        <asp:GridView ID="Grd_Production_After" runat="server" CssClass="table table-bordered"
                                                                            DataKeyNames="vchProductName" AutoGenerateColumns="false" EmptyDataText="No Records found...">
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
                                                                        Direct Employment In Numbers<small>(On Company Payroll)</small>
                                                                    </label>
                                                                    <div class="col-sm-2">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="lbl_Direct_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                    <label for="Iname" class="col-sm-3">
                                                                        Contractual Employment In Numbers</label>
                                                                    <div class="col-sm-3">
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
                                                            <div class="form-group">
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
                                                                <asp:Label ID="Lbl_FFCI_Before_Doc_Name" runat="server" Text="" CssClass="form-control-static"></asp:Label>
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
                                                                <asp:Label ID="Lbl_FFCI_After_Doc_Name" runat="server" Text="" CssClass="form-control-static"></asp:Label>
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
                                                            <asp:GridView ID="Grd_TL" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                                EmptyDataText="No Records found...">
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
                                                            <asp:GridView ID="Grd_WC" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                                EmptyDataText="No Records found...">
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
                                                    <label class="col-sm-2">
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
                                        <div class="panel-heading" role="tab" id="DivPlantMcss">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#DivPlantMc" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-minus">
                                                    </i>Investment Plant & Machinery Details </a>
                                            </h4>
                                        </div>
                                        <div id="DivPlantMc" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <h4 class="h4-header">
                                                    NEW PLANT & MACHINERY DETAILS
                                                </h4>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 ">
                                                            <small>Format for Providing Plant & Machinery Investment Details</small></label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span> <a data-toggle="tooltip" title="Download" download=""
                                                                href="Files/investment/Plant And Machinary Doc.xls" class="btn btn-success "><i class="fa fa-file-excel-o">
                                                                </i></a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 ">
                                                            <small>Upload Plant & Machinery Investment Details(In the format as downloaded from
                                                                this form)</small><a data-toggle="tooltip" class="fieldinfo2" title="Upload Plant & Machinery Investment Details(In the format as downloaded from
                                                                this form)"> <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <div class="input-group">
                                                                        <asp:FileUpload ID="fileDocfirstinvestment" CssClass="form-control" runat="server"
                                                                            onchange="return FileCheck(this, 'xls,xlsx', 'xls/xlsx',4);" />
                                                                        <asp:HiddenField ID="hdnDocfirstinvestment" runat="server" />
                                                                        <asp:HiddenField ID="hdnDocfirstinvestmentID" runat="server" Value="D255" />
                                                                        <asp:LinkButton ID="lnkDocfirstinvestmentUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                            OnClick="lnkDocfirstinvestmentUpload_Click" ToolTip="Click here to upload the file."
                                                                            OnClientClick="return HasFile('fileDocfirstinvestment','Please Upload Plant & Machinery Investment Details(In the format as downloaded from this form)');">
                                                                    <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDocfirstinvestmentDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                            OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:HyperLink ID="hypDocfirstinvestment" runat="server" Target="_blank" Visible="false"
                                                                            CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                    </div>
                                                                    <span class="text-red"><small>(.xls/.xlsx file only and Max file Size 4 MB) </small>
                                                                    </span>
                                                                    <asp:Label ID="lblDocfirstinvestment" Style="font-size: 12px;" CssClass="text-blue"
                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="lnkDocfirstinvestmentUpload" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 ">
                                                            <small>Upload Bills/Vouchers for Purchase of Plant & Machinery</small><a data-toggle="tooltip"
                                                                class="fieldinfo2" title="Upload Bills/Vouchers for Purchase of Plant & Machinery">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <div class="input-group">
                                                                        <asp:FileUpload ID="fileDocfirstinvestment2" CssClass="form-control" runat="server"
                                                                            onchange="return FileCheck(this, 'zip', 'zip',4);" />
                                                                        <asp:HiddenField ID="hdnDocfirstinvestment2" runat="server" />
                                                                        <asp:HiddenField ID="hdnDocfirstinvestmentID2" runat="server" Value="D255" />
                                                                        <asp:LinkButton ID="lnkDocfirstinvestmentUpload2" runat="server" CssClass="input-group-addon bg-green"
                                                                            OnClick="lnkDocfirstinvestmentUpload_Click" ToolTip="Click here to upload the file."
                                                                            OnClientClick="return HasFile('fileDocfirstinvestment2','Please Upload Bills/Vouchers for purchase of plant & machinery.');">
                                                                    <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDocfirstinvestmentDelete2" runat="server" CssClass="input-group-addon bg-red"
                                                                            OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:HyperLink ID="hypDocfirstinvestment2" runat="server" Target="_blank" Visible="false"
                                                                            CssClass="input-group-addon bg-blue">

                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                    </div>
                                                                    <span class="text-red"><small>(.zip file only and Max file Size 4 MB)</small></span>
                                                                    <asp:Label ID="lblDocfirstinvestment2" Style="font-size: 12px;" CssClass="text-blue"
                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="lnkDocfirstinvestmentUpload2" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <h4 class="h4-header">
                                                    SECOND HAND PLANT & MACHINERY DETAILS
                                                </h4>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 ">
                                                            <small>Format for Providing Second Hand Plant & Machinery Investment Details</small>
                                                            <a data-toggle="tooltip" class="fieldinfo2" style="padding-left: 10px" title="Second Hand Plant & Machinery Equipment that acquired by the unit">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span> <a data-toggle="tooltip" title="Download" download=""
                                                                href="Files/investment/Plant And Machinary Doc 2.xls" class="btn btn-success "><i
                                                                    class="fa fa-file-excel-o"></i></a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 ">
                                                            <small>Upload Second Hand Plant & Machinery Investment Details(In the format as downloaded
                                                                from this form)</small><a data-toggle="tooltip" class="fieldinfo2" title="Upload Second Hand Plant & Machinery Investment Details(In the format as downloaded
                                                                from this form)"> <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <div class="input-group">
                                                                        <asp:FileUpload ID="fileDocSecondinvestment" CssClass="form-control" runat="server"
                                                                            onchange="return FileCheck(this, 'xls,xlsx', 'xls/xlsx',4);" />
                                                                        <asp:HiddenField ID="hdnDocSecondinvestment" runat="server" />
                                                                        <asp:HiddenField ID="hdnDocSecondinvestmentID" runat="server" Value="D255" />
                                                                        <asp:LinkButton ID="lnkDocSecondinvestmentUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                            OnClick="lnkDocfirstinvestmentUpload_Click" ToolTip="Click here to upload the file."
                                                                            OnClientClick="return HasFile('fileDocSecondinvestment','Upload Second Hand Plant & Machinery Investment Details(In the format as downloaded from this form)');">
                                                                    <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDocSecondmentDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                            OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:HyperLink ID="hypDocSecondinvestment" runat="server" Target="_blank" Visible="false"
                                                                            CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                    </div>
                                                                    <span class="text-red"><small>(.xls/.xlsx file only and Max file Size 4 MB) </small>
                                                                    </span>
                                                                    <asp:Label ID="lblDocSecondinvestment" Style="font-size: 12px;" CssClass="text-blue"
                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="lnkDocSecondinvestmentUpload" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3 ">
                                                            <small>Upload Bills/Vouchers for Purchase of Plant & Machinery</small><a data-toggle="tooltip"
                                                                class="fieldinfo2" title="Upload Bills/Vouchers for Purchase of Plant & Machinery">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <div class="input-group">
                                                                        <asp:FileUpload ID="fileDocSecondinvestment2" CssClass="form-control" runat="server"
                                                                            onchange="return FileCheck(this, 'zip', 'zip',4);" />
                                                                        <asp:HiddenField ID="hdnDocSecondinvestment2" runat="server" />
                                                                        <asp:HiddenField ID="hdnDocSecondinvestmentID2" runat="server" Value="D255" />
                                                                        <asp:LinkButton ID="lnkDocSecondinvestmentUpload2" runat="server" CssClass="input-group-addon bg-green"
                                                                            OnClick="lnkDocfirstinvestmentUpload_Click" ToolTip="Click here to upload the file."
                                                                            OnClientClick="return HasFile('fileDocSecondinvestment2','Please Upload Bills/Vouchers for purchase of plant & machinery.');">
                                                                    <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDocSecondmentDelete2" runat="server" CssClass="input-group-addon bg-red"
                                                                            OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:HyperLink ID="hypDocSecondinvestment2" runat="server" Target="_blank" Visible="false"
                                                                            CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                    </div>
                                                                    <span class="text-red"><small>(.zip file only and Max file Size 4 MB)</small></span>
                                                                    <asp:Label ID="lblDocSecondinvestment2" Style="font-size: 12px;" CssClass="text-blue"
                                                                        Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="lnkDocSecondinvestmentUpload2" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
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
                                                            Present Claim for reimbursement<span class="text-red">*</span>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtreimamt" runat="server" CssClass="form-control" MaxLength="15"
                                                                onblur="DecimalValidation(this);"></asp:TextBox>
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
                                                            <asp:RadioButtonList ID="RadBtn_Availed_Earlier" runat="server" RepeatDirection="Horizontal"
                                                                onchange="return OnChangeAvailed();">
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group availdetailsec availgroup1">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-12">
                                                            Details of Subsidy Already Availed
                                                        </label>
                                                        <div class="col-sm-12">
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
                                                                    <td width="5%">
                                                                        -
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtagency" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender556" runat="server" TargetControlID="txtagency"
                                                                            FilterMode="ValidChars" FilterType="Custom,UppercaseLetters,LowercaseLetters"
                                                                            ValidChars=" .,-" />
                                                                    </td>
                                                                    <td width="15%">
                                                                        <asp:TextBox ID="txtsacamt" runat="server" CssClass="form-control" MaxLength="15"
                                                                            onblur="DecimalValidation(this);"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender566" runat="server" TargetControlID="txtsacamt"
                                                                            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="." />
                                                                    </td>
                                                                    <td width="15%">
                                                                        <asp:TextBox ID="txtsacord" runat="server" CssClass="form-control" MaxLength="50"
                                                                            Style="text-align: right"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server" TargetControlID="txtsacord"
                                                                            FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                                                            ValidChars=",,-,/\, ,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td width="15%">
                                                                        <div class="input-group  date datePicker" id="Div14">
                                                                            <asp:TextBox ID="txtsacdat" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td width="15%">
                                                                        <asp:TextBox ID="txtavilamt" runat="server" CssClass="form-control" MaxLength="15"
                                                                            onblur="DecimalValidation(this);"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" TargetControlID="txtavilamt"
                                                                            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="." />
                                                                    </td>
                                                                    <td width="7%">
                                                                        <asp:LinkButton ID="LinkButton41" CssClass="btn btn-success btn-sm" OnClick="LinkButton41_Click"
                                                                            runat="server" OnClientClick="return validAvailgrid();"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:GridView ID="grdAssistanceDetailsAD" runat="server" CssClass="table table-bordered"
                                                                AutoGenerateColumns="false" ShowFooter="false" ShowHeader="false" OnRowDeleting="grdAssistanceDetailsAD_RowDeleting"
                                                                EmptyDataText="No Records found...">
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="Slno.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbl_WC_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
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
                                                                    <asp:TemplateField HeaderText="Add More" ItemStyle-Width="7%">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDelRecord" CommandName="delete" CssClass="btn btn-danger btn-sm"
                                                                                ToolTip="Remove" runat="server"><i class="fa fa-trash"></i>
                                                                            </asp:LinkButton>
                                                                            <asp:HiddenField ID="hdnRowId" runat="server" Value='<%#Eval("dcRowId")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group  availuploadsec availgroup1">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Document details of assistance sanctioned<span class="text-red">*</span><a data-toggle="tooltip"
                                                                class="fieldinfo2" title="Document details of assistance sanctioned"> <i class="fa fa-question-circle"
                                                                    aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnSupportingDocsID" runat="server" Value="D253" />
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FU_Asst_Sanc_Doc" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip',4);" />
                                                                <asp:HiddenField ID="Hid_Asst_Sanc_File_Name" runat="server" />
                                                                <asp:LinkButton ID="LnkBtn_Upload_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" ToolTip="Click here to upload the file."
                                                                    OnClientClick="return HasFile('FU_Asst_Sanc_Doc','Please Upload Document details of assistance sanctioned');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LnkBtn_Delete_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="Hyp_View_Asst_Sanc_Doc" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue" title="View" ToolTip="View">
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
                                                            Undertaking on non-availment of subsidy earlier on this project<span class="text-red">*</span>
                                                            <a data-toggle="tooltip" class="fieldinfo2" title="Undertaking on non-availment of subsidy earlier on this project">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnSubsidyAvailedID" runat="server" Value="D230" />
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FU_Undertaking_Doc" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip',4);" />
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
                                                            Amount of Differential Claim to be Exempted<span class="text-red">*</span>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtdiffclaimamt" runat="server" CssClass="form-control" MaxLength="15"
                                                                onblur="DecimalValidation(this);"></asp:TextBox>
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
                                                            Account No of Industrial Unit <span class="text-red">*</span></label>
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
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender112" runat="server" Enabled="True"
                                                                FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txtBnkNm"
                                                                FilterMode="ValidChars" ValidChars=" ,.-" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-2 ">
                                                            Branch Name <span class="text-red">*</span></label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtLocation" runat="server"
                                                                Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txtBranch"
                                                                FilterMode="ValidChars" ValidChars=" ,.-" />
                                                        </div>
                                                        <label for="Iname" class="col-sm-2 ">
                                                            IFSC Code <span class="text-red">*</span></label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
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
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender111" runat="server" Enabled="True"
                                                                FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txtMICRNo"
                                                                FilterMode="ValidChars" ValidChars="0123456789" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <small class="text-gray">Upload cancelled cheque to verify the entered A/c details</small><span
                                                                class="text-red">*</span><a data-toggle="tooltip" class="fieldinfo2" title="Upload cancelled cheque to verify the entered A/c details">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fuBank" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,jpg,jpeg,png', 'pdf/jpg/jpeg/png',4);" />
                                                                <asp:HiddenField ID="hdnBank" runat="server" />
                                                                <asp:HiddenField ID="hdnBankID" runat="server" Value="D266" />
                                                                <asp:LinkButton ID="lnkBankUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocfirstinvestmentUpload_Click" ToolTip="Click here to upload the file."
                                                                    OnClientClick="return HasFile('fuBank','Please Upload any sample supporting document to verify account details.');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkBankDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocfirstinvestmentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypBank" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue"
                                                                    ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.jpg/.jpeg/.png file only and Max file Size 4 MB)</small>
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
                                                            Plant & Machinery Investment Details(In the format as downloaded from this form)
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgDocfirstinvestment" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Bills/Vouchers for Purchase of Plant & Machinery
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgDocfirstinvestment2" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Second Hand Plant & Machinery Investment Details(In the format as downloaded from
                                                            this form)
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgDocSecondinvestment" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Bills/Vouchers for Purchase of Plant & Machinery
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgDocSecondinvestment2" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Undertaking on non-availment of subsidy earlier on this project
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgSubsidyAvailed" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Document details of assistance sanctioned
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgSupportingDocs" Height="15" Width="15" src="../images/cancel-square.png" />
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
                        </div>
                        <div class="form-footer">
                            <div class="row">
                                <div class="col-sm-12 text-right">
                                    <asp:Button ID="btnDraft" runat="server" Text="Save As Draft" CssClass="btn btn-warning"
                                        OnClientClick="return SaveDraft();" OnClick="btnDraft_Click" />
                                    <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-success"
                                        OnClick="btnApply_Click" OnClientClick="return MainVadidation();" />
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
        var msgTitle = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';
        function MeansValidation() {
            if (!blankFieldValidation('txtNameOfFinancialInst', 'Name of Financial Institution', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtLocationState', 'State', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtLocationCity', 'City', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtLoanAmt', 'Term Loan Amount', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtSactionDate', '	Sanction Date', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtAvailedAmt', 'Availed Amount', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtAvailedDate', 'Availed Date', msgTitle)) {
                return false;
            }
        }
        function AvailRecords() {
            if (!blankFieldValidation('grdIncentiveAvailed_txtBody', 'Body (Pvt, State Govt (Specify State),GoI)', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('grdIncentiveAvailed_txtName', 'Name of Financial Institution', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('grdIncentiveAvailed_txtAmountAvailed', 'Amount Availed', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('grdIncentiveAvailed_txtAvailedDate', 'Availed Date', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('grdIncentiveAvailed_txtSanctionOrderNo', 'Sanction Order no.', msgTitle)) {
                return false;
            }
        }
        function MainVadidation() {


            if (!InvestmentVadidation()) {
                return false;
            }
            //            if (!ProductionValidation()) {
            //                return false;
            //            }
            if (!InvestmentValidationNew()) {
                return false;
            }
            //            if (!MeansOfFinanceValidation()) {
            //                return false;
            //            }
            if (!AvailValidation()) {
                return false;
            }
            if (!BankValidation()) {
                return false;
            }

        }
        function InvestmentVadidation() {
            //Industrial Unit

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


                //                if (($("input[name='radAuthorizing']:checked").val() != 'D102') && ($("input[name='radAuthorizing']:checked").val() != 'D219') && ($("input[name='radAuthorizing']:checked").val() != 'D218')) {
                //                    jAlert('Please select Authorizing letter signed by Authorized Signatory option.', msgTitle);
                //                    return false;
                //                }


                if ($("#hdnAUTHORIZEDFILE").val() == '') {

                    jAlert('Please provide Authorizing letter signed by Authorized Signatory.', msgTitle);
                    return false;
                }
            }
            return true;
        }
        function ProductionValidation() {
            //--Production
            var rowcount = $("#grdProduction tr").length;
            if (rowcount == "0") {
                jAlert('<strong>Please enter atleast one Items of Manufacture/Activity.</strong>', msgTitle);
                return false;
            }
            if (!blankFieldValidation('txtDirectEmp', 'Direct Employment IN NUMBERS', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtContractualEmp', 'Contractual Employment', msgTitle)) {
                return false;
            }
            if ($('#lnkFileNew').text() == '') {
                jAlert('<strong>Please Upload Document in Support of Number of Employees shown as directly employed (e.g. Certificate by DLO)- this certificate has to be taken </strong>', msgTitle);
                return false;
            }
            if (!blankFieldValidation('txtCurrentManagerial', 'Current Managerial', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtProposedManagerial', 'Proposed Managerial', msgTitle)) {
                return false;
            }

            if (!blankFieldValidation('txtCurrentSupervisory', 'Current Supervisory', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtProposedSupervisory', 'Proposed Supervisory', msgTitle)) {
                return false;
            }

            if (!blankFieldValidation('txtCurrentSkilled', 'Current Skilled', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtProposedSkilled', 'Proposed Skilled', msgTitle)) {
                return false;
            }

            if (!blankFieldValidation('txtCurrentSemiSkilled', 'Current Semi-Skilled', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtProposedSemiSkilled', 'Proposed Semi-Skilled', msgTitle)) {
                return false;
            }

            if (!blankFieldValidation('txtCurrentUnSkilled', 'Current UnSkilled', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtProposedUnSkilled', 'Proposed UnSkilled', msgTitle)) {
                return false;
            }

            if (!blankFieldValidation('txtCurrentTotal', 'Current Total', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtProposedTotal', 'Proposed Total', msgTitle)) {
                return false;
            }
            return true;
        }
        function InvestmentValidationNew() {
            if ($('#hdnDocfirstinvestment').val() == '' && $('#hdnDocfirstinvestment2').val() == '' && $('#hdnDocSecondinvestment').val() == '' && $('#hdnDocSecondinvestment2').val() == '') {
                jAlert('<strong>Please fill atleast one Plant & Machinery Details.(New/Second Hand) </strong>', msgTitle);
                return false;
            }
            if ($('#hdnDocfirstinvestment').val() != '' || $('#hdnDocfirstinvestment2').val() != '') {
                if ($('#hdnDocfirstinvestment').val() == '') {
                    jAlert('<strong>Please Upload New Plant & Machinery Investment Details(In the format as downloaded from this form) </strong>', msgTitle);
                    return false;
                }
                if ($('#hdnDocfirstinvestment2').val() == '') {
                    jAlert('<strong>Please Upload Bills/Vouchers for New Purchase of Plant & Machinery </strong>', msgTitle);
                    return false;
                }

            }
            if ($('#hdnDocSecondinvestment').val() != '' || $('#hdnDocSecondinvestment2').val() != '') {
                if ($('#hdnDocSecondinvestment').val() == '') {
                    jAlert('<strong>Please Upload Second Hand Plant & Machinery Investment Details(In the format as downloaded from this form) </strong>', msgTitle);
                    return false;
                }
                if ($('#hdnDocSecondinvestment2').val() == '') {
                    jAlert('<strong>Please Upload Bills/Vouchers for Second Hand Purchase of Plant & Machinery </strong>', msgTitle);
                    return false;
                }

            }
            return true;
        }

        function MeansOfFinanceValidation() {
            //Means of Finance
            if ($('#LnkFUSanOrder').text() == '') {
                jAlert('<strong>Please Upload Term loan sanction order of Financial Institute (s) / Banks </strong>', msgTitle);
                return false;
            }
            return true;
        }
        function AvailValidation() {
            //Avail Details         

            if (!($('#RadBtn_Availed_Earlier_0').is(':checked') || $('#RadBtn_Availed_Earlier_1').is(':checked'))) {
                jAlert('Please select Subsidy/Incentive against the details in this application been availed earlier option', msgTitle);
                return false;
            }


            if ($('#RadBtn_Availed_Earlier_0').is(':checked')) {
                var rowcount = $("#grdAssistanceDetailsAD tr").length;
                if (rowcount == "0") {
                    jAlert('<strong>Please enter atleast one Items of Details of Subsidy Already availed.</strong>', msgTitle);
                    return false;
                }
                if ($("#Hid_Asst_Sanc_File_Name").val() == '') {
                    jAlert('Please upload Document details of assistance sanctioned .', msgTitle);
                    return false;
                }
                if (!blankFieldValidation('txtdiffclaimamt', 'Amount of Differential Claim to be Exempted', msgTitle)) {
                    return false;
                }

            }
            if ($('#RadBtn_Availed_Earlier_1').is(':checked')) {
                if ($("#Hid_Undertaking_File_Name").val() == '') {
                    jAlert('Please upload Document deatils of Undertaking on non-availment of subsidy.', msgTitle);
                    return false;
                }
            }
            if (!blankFieldValidation('txtreimamt', 'Present Claim for reimbursement', msgTitle)) {
                return false;
            }

            return true;
        }
        function BankValidation() {
            //Bank
            if (!blankFieldValidation('txtAccNo', 'Account No of Industrial Unit', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtBnkNm', 'Bank Name', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtBranch', 'Branch Name', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtIFSC', 'IFSC', msgTitle)) {
                return false;
            }
            //            if (!blankFieldValidation('txtMICRNo', 'MICR No.', msgTitle)) {
            //                return false;
            //            }
            if ($("#hdnBank").val() == '') {
                jAlert('Please Upload any Cancelled Cheque to verify account details.', msgTitle);
                return false;
            }
            return true;
        }
        function calculetotalemp() {

            var cal = 0;
            if ($("#txtCurrentManagerial").val() != '') {
                cal = cal + parseInt($("#txtCurrentManagerial").val());

            }
            if ($("#txtCurrentSupervisory").val() != '') {
                cal = cal + parseInt($("#txtCurrentSupervisory").val());

            }
            if ($("#txtCurrentSkilled").val() != '') {
                cal = cal + parseInt($("#txtCurrentSkilled").val());
            }

            if ($("#txtCurrentSemiSkilled").val() != '') {
                cal = cal + parseInt($("#txtCurrentSemiSkilled").val());
            }
            if ($("#txtCurrentUnSkilled").val() != '') {
                cal = cal + parseInt($("#txtCurrentUnSkilled").val());
            }

            $("#txtCurrentTotal").val(cal);

            var calProp = 0;
            if ($("#txtProposedManagerial").val() != '') {
                calProp = calProp + parseInt($("#txtProposedManagerial").val());

            }
            if ($("#txtProposedSupervisory").val() != '') {
                calProp = calProp + parseInt($("#txtProposedSupervisory").val());

            }
            if ($("#txtProposedSkilled").val() != '') {
                calProp = calProp + parseInt($("#txtProposedSkilled").val());
            }

            if ($("#txtProposedSemiSkilled").val() != '') {
                calProp = calProp + parseInt($("#txtProposedSemiSkilled").val());
            }
            if ($("#txtProposedUnSkilled").val() != '') {
                calProp = calProp + parseInt($("#txtProposedUnSkilled").val());
            }

            $("#txtProposedTotal").val(calProp);


        }
        function pageLoad() {
            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy"
                });
            });
            DocCheckList();
            OnChangeApplyBy();
            // OnChangePriority();
            OnChangeAvailed();
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
            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails, #DivPlantMc, #AvailedClaimDetails, #BankDetails, #DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").on("click", function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });
        }

        function Validate() {

            if ($('#txtAccNo').val().trim() == '') {
                $('#txtAccNo').val('');
                jAlert('Please enter Account No.', msgTitle);
                $("#txtAccNo").focus();
                return false;
            }
            if ($('#txtBnkNm').val().trim() == '') {
                $('#txtBnkNm').val('');
                jAlert('Please enter Bank Name.', msgTitle);
                $("#txtBnkNm").focus();
                return false;
            }


            if ($('#txtAccNo').val().trim() != '') {
                if ($('#txtAccNo').val().length > 16) {
                    jAlert('Please enter Account No. within 16 characters.', msgTitle);
                    $("#txtAccNo").focus();
                    return false;
                }
            }

            if ($('#txtIFSC').val().trim() == '') {
                $('#txtIFSC').val('');
                jAlert('Please enter IFSC Code .', msgTitle);
                $("#txtIFSC").focus();
                return false;
            }
            if ($('#txtIFSC').val().trim() != '') {
                if ($('#txtIFSC').val().length > 50) {
                    jAlert('Please enter IFSC Code within 50 characters  .', msgTitle);
                    $("#txtIFSC").focus();
                    return false;
                }
            }

            if ($('#txtBranch').val().trim() == '') {
                $('#txtBranch').val('');
                jAlert('Please enter Branch Name  .', msgTitle);
                $("#txtBranch").focus();
                return false;
            }

            if ($('#txtBranch').val().trim() != '') {
                if ($('#txtBranch').val().length > 50) {
                    jAlert('Please enter Location within 50 characters .', msgTitle);
                    $("#txtBranch").focus();
                    return false;
                }
            }

        }
        // Avail Details Start
        $(document).ready(function () {
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
                        jAlert('Enter Valid Amount of Differential Claim to be Exempted .', msgTitle);
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
        function ProductionRecords() {
            if (!blankFieldValidation('txtProductName', 'Product/Service Name', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtQuantity', 'Quantity', msgTitle)) {
                return false;
            }
            if (!DropDownValidation('ddlUnit', '0', 'Unit', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtValue', 'Value', msgTitle)) {
                return false;
            }

        }

        /*----------------------------------------------------------------------------*/

        function validAvailgrid() {
            if (!blankFieldValidation('txtagency', 'Disbursing agency', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtsacamt', 'Sanctioned amount', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtsacord', 'Sanction order no.', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('txtsacdat', 'Date of sanction', msgTitle)) {
                return false;
            }
            var CommDt = $('#txtsacdat').val();
            if (new Date(CommDt) > new Date()) {
                jAlert('<strong>Date of Sanction should not be greater than current date.</strong>', msgTitle);
                $('#txtsacdat').val('');
                $('#txtsacdat').focus();
                return false;
            }
            if (!blankFieldValidation('txtavilamt', 'Availed amount', msgTitle)) {
                return false;
            }
            var sanctionAmt = parseFloat($('#txtsacamt').val());
            var availedAmt = parseFloat($('#txtavilamt').val());
            if (availedAmt > sanctionAmt) {
                jAlert('<strong>Availed amount cannot be greater than sanctioned amount !!</strong>', msgTitle);
                $("#popup_ok").click(function () { $("#txtavilamt").focus(); });
                //$('#txtavilamt').focus();
                //$('#txtavilamt').val('');      
                return false;
            }
        }

        /*----------------------------------------------------------------------------*/

        function SaveDraft() {
            if ($('#TxtAdhaar1').val() != "") {
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('Aadhaar no should be 12 digits.', msgTitle);
                    return false;
                }
            }

            if ($('#txtAccNo').val().trim() != '') {
                if ($('#txtAccNo').val().length > 16) {
                    jAlert('Please enter Account No. within 16 characters .', msgTitle);
                    $("#txtAccNo").focus();
                    return false;
                }
            }

            if ($('#txtIFSC').val().trim() != '') {
                var IFSCODE = $('#txtIFSC').val();
                if (IFSCODE.length < 7) {
                    jAlert('IFSC Code should be 7 digits.', msgTitle);
                    return false;
                }
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
            ImgSrc($('#hdnDocfirstinvestment').val(), $('#ImgDocfirstinvestment').attr("id"));
            ImgSrc($('#hdnDocfirstinvestment2').val(), $('#ImgDocfirstinvestment2').attr("id"));
            ImgSrc($('#hdnDocSecondinvestment').val(), $('#ImgDocSecondinvestment').attr("id"));
            ImgSrc($('#hdnDocSecondinvestment2').val(), $('#ImgDocSecondinvestment2').attr("id"));
            ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#ImgSubsidyAvailed').attr("id"));

            ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#ImgSupportingDocs').attr("id"));
            ImgSrc($('#hdnBank').val(), $('#ImgBank').attr("id"));
        } 

    </script>
    </form>
</body>
</html>
