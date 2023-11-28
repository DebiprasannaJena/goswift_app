<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Enterpreneurship_Subsidy.aspx.cs"
    Inherits="Enterpreneurship_Subsidy" %>

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
    <script type="text/javascript" src="../js/Incentive/JS_Inct_Common_Validation.js"></script>
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
            $('.Pioneersec,.attorneysec,.adhardetails,.availuploadsec,.availdetailsec,.availundertakingsec').hide();

            $(".applyby").on("click", function () {
                if ($("input:checked").val() == 'Self') {
                    $('.adhardetails').show();
                    $('.attorneysec').hide();
                    ImgSrc('', $('#ImgSign').attr("id"));
                }
                else {
                    $('.attorneysec').show();
                    $('.adhardetails').hide();
                    ImgSrc($('#hdnAUTHORIZEDFILE').val(), $('#ImgSign').attr("id"));
                }
            });

            if ($("#Lbl_Pioneer_Doc_Name").text() == '') {
                $("#Div_Pioneer_Doc").hide();
            }

            OnChangeAvailed();

            $('input[id="<%= txtCourseDuration.ClientID %>"]').on('focusout', function () {
                var validateVal = $('input[id="<%= txtCourseDuration.ClientID %>"]').val();
                if (180 < validateVal || validateVal < 7) {
                    jAlert('<strong>Course Period should be minimum 7 days and maximum 180 days</strong>', projname);
                    $("#<%=txtCourseDuration.ClientID %>").val("7");
                }
            });

            $('input[id="<%= txtCourseFee.ClientID %>"]').on('focusout', function () {
                var validateVal = $('input[id="<%= txtCourseFee.ClientID %>"]').val();
                if (validateVal > 50000) {
                    jAlert('<strong> Course fee limited to Rs.50,000 per course</strong>', projname);
                    $("#<%=txtCourseFee.ClientID %>").val("0.00");
                }
            });
        });

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

        function OnChangeAvailed() {
            $('.availuploadsec,.availdetailsec,.availundertakingsec').hide();
            if ($("input[name='RadBtn_Availed_Earlier']:checked").val() == '1') {
                $('.availdetailsec').show();
                $('.availuploadsec').show();
                $('.availundertakingsec').hide();
                $('.availgroup1').show();
                $('.availgroup2').hide();
                ImgSrc('', $('#ImgNonAvailment').attr("id"));
                ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#ImgAssistance').attr("id"));
            }
            else {
                $('.availuploadsec').hide();
                $('.availdetailsec').hide();
                $('.availundertakingsec').show();
                $('.availgroup2').show();
                $('.availgroup1').hide();
                ImgSrc('', $('#ImgAssistance').attr("id"));
                ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#ImgNonAvailment').attr("id"));
            }
        } ////if ($("#radApplyBy_1").checked == true)

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
        .fieldinfo2
        {
            float: right;
            margin-right: 8px;
            font-size: 17px;
            margin-top: 1px;
            color: #3abffb;
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
                            <div class="innertabs  m-b-10">
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
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:RadioButtonList ID="radApplyBy" class="applyby" runat="server" RepeatDirection="Horizontal"
                                                                onchange="return OnChangeApplyBy();">
                                                                <asp:ListItem Value="1" Selected="True">Self</asp:ListItem>
                                                                <asp:ListItem Value="2">Authorized Person</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:HiddenField ID="hdnRadibutton" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group adhardetails" id="divadhhardetails" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Aadhar No.</label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="TxtAdhaar1" CssClass="form-control" placeholder="123412341234" runat="server"
                                                                MaxLength="12"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                                TargetControlID="TxtAdhaar1" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group attorneysec" id="divAuthorizing" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Please provide Authorizing letter signed by Authorized Signatory <a data-toggle="tooltip"
                                                                class="fieldinfo2" title="Provide Authorizing letter signed by Authorized Signatory">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <asp:Label runat="server" ID="lblAuthorizing"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hidAuthorizing" />
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FlupAUTHORIZEDFILE" onchange="return FileCheck(this, 'pdf', 'pdf', 4);"
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
                                                            <asp:Label ID="lbl_Unit_Cat" runat="server">
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
                                        <div class="panel-heading" role="tab" id="Div1">
                                            <h4 class="panel-title">
                                                <a role="button" data-toggle="collapse" data-parent="#accordion" href="#CourseDetails"
                                                    aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa  fa-minus">
                                                    </i><span class="text-red pull-right " style="margin-right: 20px;">* All fields in this
                                                        section are mandatory</span>Course Details </a>
                                            </h4>
                                        </div>
                                        <div id="CourseDetails" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-2">
                                                            Institution Details<a data-toggle="tooltip" class="fieldinfo2" title="Enter Institution Details"><i
                                                                class="fa fa-question-circle" aria-hidden="true"></i></a></label>
                                                        <div class="col-sm-12">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th width="40%">
                                                                        Institution Name
                                                                    </th>
                                                                    <th>
                                                                        Location of Institution
                                                                    </th>
                                                                    <th>
                                                                        Address
                                                                    </th>
                                                                </tr>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlInstitute" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                <small class="text-danger">Others(Please specify)</small> <span class="text-red">*</span>
                                                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtOtherInstitution"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtOtherInstitution"
                                                                                    FilterMode="ValidChars" FilterType="Custom,UppercaseLetters,LowercaseLetters"
                                                                                    ValidChars=" .,-" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlInsttuteLoc" runat="server" CssClass="form-control">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox CssClass="form-control" runat="server" MaxLength="80" ID="txtAddress"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FteESIRegNo" runat="server" TargetControlID="txtAddress"
                                                                                    FilterMode="ValidChars" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                                                                    ValidChars=" .,/-">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                        </tr>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-2">
                                                            Course Details<a data-toggle="tooltip" class="fieldinfo2" title="Enter Course Details"><i
                                                                class="fa fa-question-circle" aria-hidden="true"></i></a></label>
                                                        <div class="col-sm-12">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th rowspan="2">
                                                                        Course Duration (in days)
                                                                        <br />
                                                                        <small class="text-danger">(minimum 7 days to 180 days)</small>
                                                                    </th>
                                                                    <th colspan="2">
                                                                        Course Fee
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <th>
                                                                        Amount
                                                                    </th>
                                                                    <th>
                                                                        Attachment
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" class="form-control" ID="txtCourseDuration" MaxLength="3"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txtCourseDuration"
                                                                            FilterType="Numbers" Enabled="True">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" class="form-control" ID="txtCourseFee" onblur="return makeDecimal(this.id);CheckDecimal(this.id);"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtCourseFee"
                                                                            FilterType="Custom,Numbers" Enabled="True" FilterMode="ValidChars" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group">
                                                                            <asp:FileUpload ID="fuAttachment" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                            <asp:LinkButton ID="lnkAttachmentDoc" runat="server" CssClass="input-group-addon bg-green"
                                                                                OnClick="lnkDocumentUpload_Click" OnClientClick="return HasFile('fuAttachment','Please upload Course Detail document');"
                                                                                ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkAttachmentDocDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                                OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                            <asp:HyperLink ID="hypAttachmentDoc" runat="server" Target="_blank" Visible="false"
                                                                                CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                                      <i class="fa fa-download"></i></asp:HyperLink>
                                                                            <asp:HiddenField ID="hdnAttachmentDoc" runat="server" />
                                                                            <asp:HiddenField ID="hdnAttachmentDocDocId" runat="server" Value="D111" />
                                                                        </div>
                                                                        <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                                        <asp:Label ID="lblAttachmentDoc" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                            runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3">
                                                            Date of selection
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <div class="input-group  date datePicker" id="Div3">
                                                                <asp:HiddenField ID="hdnIsProvisional" runat="server" />
                                                                <asp:HiddenField ID="hdnProvisionalDoc" runat="server" />
                                                                <asp:TextBox runat="server" class="form-control" ID="txtDateofselection" name="txtTimescheduleforyearofcomm"></asp:TextBox>
                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3">
                                                            Copy of letter of selection<a data-toggle="tooltip" class="fieldinfo2" title="Upload the Selection document"><i
                                                                class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fuCopyletterselection" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                <asp:HiddenField ID="hdnCopyletterselection" runat="server" />
                                                                <asp:HiddenField ID="hdnCopyletterselectionDocId" runat="server" Value="D232" />
                                                                <asp:LinkButton ID="lnkCopyletterselectionDoc" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocumentUpload_Click" OnClientClick="return HasFile('fuCopyletterselection','Please upload Selection letter');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkCopyletterselectionDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypCopyletterselection" runat="server" Target="_blank" Visible="false"
                                                                    ToolTip="View File" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblCopyletterselection" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group copyofProvisionalSec">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3">
                                                            Copy of Provisional Sanction Letter<a data-toggle="tooltip" class="fieldinfo2" title="View the Provisional Sanction Letter"><i
                                                                class="fa fa-question-circle" aria-hidden="true"></i></a></label>
                                                        <div class="col-sm-4">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fuSanctionLetter" CssClass="form-control" runat="server" />
                                                                <asp:HiddenField ID="hdnSanctionLetter" runat="server" />
                                                                <asp:HiddenField ID="hdnSanctionLetterDocId" runat="server" Value="D146" />
                                                                <asp:LinkButton ID="lnkSanctionLetterDoc" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocumentUpload_Click" OnClientClick="return HasFile('fuSanctionLetter','Please upload Provisional Sanction letter');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkSanctionLetterDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypSanctionLetter" runat="server" Target="_blank" Visible="false"
                                                                    ToolTip="View File" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblSanctionLetterDocument" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-3">
                                                                Sanction Letter No.<a data-toggle="tooltip" class="fieldinfo2" title="Fill the Sanction Letter No. from Provisional Sanction Letter"><i
                                                                    class="fa fa-question-circle" aria-hidden="true"></i></a></label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:TextBox runat="server" class="form-control" ID="txtSanctionLetterNo" MaxLength="20"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtSanctionLetterNo"
                                                                    FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters" Enabled="True"
                                                                    FilterMode="ValidChars" ValidChars="./-">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-3">
                                                                Date of Sanction Letter<a data-toggle="tooltip" class="fieldinfo2" title="Fill the Date of Sanction letter from Provisional Sanction Letter"><i
                                                                    class="fa fa-question-circle" aria-hidden="true"></i></a></label>
                                                            <div class="col-sm-4">
                                                                <div class="input-group  date datePicker" id="Div13">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox runat="server" class="form-control" ID="txtSanctiondt" name="txtSanctiondt"></asp:TextBox>
                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group text-right margin-top15">
                                        <asp:Button ID="btnProvisional" runat="server" Text="Apply for Provisional Sanction"
                                            CssClass="btn btn-success" OnClick="btnProvisional_Click" OnClientClick="return ProvMainVadidation();" />
                                    </div>
                                    <div class="panel panel-default availedsec">
                                        <div class="panel-heading" role="tab" id="Div2">
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
                                                            Present Claim for reimbursement<a data-toggle="tooltip" class="fieldinfo2" title="Enter Present Claim for reimbursement amount"><i
                                                                class="fa fa-question-circle" aria-hidden="true"></i></a>
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
                                                            Details of Subsidy Already availed
                                                        </label>
                                                        <div class="col-sm-12 ">
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
                                                                        --
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtagency" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender556" runat="server" TargetControlID="txtagency"
                                                                            FilterMode="ValidChars" FilterType="Custom,UppercaseLetters,LowercaseLetters"
                                                                            ValidChars=" .,-" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtsacamt" runat="server" CssClass="form-control" MaxLength="15"
                                                                            onkeypress="return FloatOnly(event, this);" onblur="DecimalValidation(this);"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender566" runat="server" TargetControlID="txtsacamt"
                                                                            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="." />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtsacord" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtsacord"
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
                                                                            onkeypress="return FloatOnly(event, this);" onblur="DecimalValidation(this);"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" TargetControlID="txtavilamt"
                                                                            FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="." />
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
                                                                    <asp:TemplateField ItemStyle-Width="5%">
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
                                                                                runat="server" ToolTip="Remove"><i class="fa fa-trash"></i>
                                                                            </asp:LinkButton>
                                                                            <asp:HiddenField ID="hdnRowId" runat="server" Value='<%#Eval("dcRowId")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group availuploadsec availgroup1">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Document details of assistance sanctioned<a data-toggle="tooltip" class="fieldinfo2"
                                                                title="Upload Assisatnce sanctioned letter"><i class="fa fa-question-circle" aria-hidden="true"></i></a></label>
                                                        <asp:HiddenField ID="hdnSupportingDocsID" runat="server" Value="D253" />
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FU_Asst_Sanc_Doc" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                <asp:HiddenField ID="Hid_Asst_Sanc_File_Name" runat="server" />
                                                                <asp:LinkButton ID="LnkBtn_Upload_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocumentUpload_Click" ToolTip="Click here to upload the file." OnClientClick="return HasFile('FU_Asst_Sanc_Doc','Please Upload Document details of assistance sanctioned');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LnkBtn_Delete_Asst_Sanc_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
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
                                                            Undertaking on non-availment of subsidy earlier on this project <a data-toggle="tooltip"
                                                                class="fieldinfo2" title="Upload non-availment of subsidy letter"><i class="fa fa-question-circle"
                                                                    aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnSubsidyAvailedID" runat="server" Value="D230" />
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FU_Undertaking_Doc" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
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
                                                <div class="form-group availgroup1">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            Amount of Differential Claim to be Exempted <a data-toggle="tooltip" class="fieldinfo2"
                                                                title="Enter Differential Claim to be Exempted amount"><i class="fa fa-question-circle"
                                                                    aria-hidden="true"></i></a>
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
                                    <div class="panel panel-default BankDtlSec">
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
                                                            <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtLocation" runat="server"
                                                                Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txtBranch"
                                                                FilterMode="ValidChars" ValidChars=" " />
                                                        </div>
                                                        <label for="Iname" class="col-sm-2 ">
                                                            IFSC Code<span class="text-red">*</span></label>
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
                                                        <label class="col-sm-2">
                                                            <small class="text-gray">Upload cancelled cheque to verify the entered A/c details <span
                                                                class="text-red">*</span></small> <a data-toggle="tooltip" class="fieldinfo2" title="Upload cancelled cheque to verify the entered A/c details">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-4">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fuBank" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,jpg,jpeg,png', 'pdf/jpg/jpeg/png', 4);" />
                                                                <asp:HiddenField ID="hdnBank" runat="server" />
                                                                <asp:HiddenField ID="hdnBankID" runat="server" Value="D266" />
                                                                <asp:LinkButton ID="lnkBankUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocumentUpload_Click" ToolTip="Click here to upload the file." OnClientClick="return HasFile('fuBank','Please Upload any sample supporting document to verify account details.');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkBankDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
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
                                    <div class="panel panel-default docstatussec">
                                        <div class="panel-heading" role="tab" id="Div7">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#AdditionalDocuments" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-plus"></i>Documents to be Submitted After Completion
                                                    of Course</a>
                                            </h4>
                                        </div>
                                        <div id="AdditionalDocuments" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3">
                                                            Date of course completion <a data-toggle="tooltip" class="fieldinfo2" title="Enter course completion Date">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </label>
                                                        <div class="col-sm-5">
                                                            <span class="colon">:</span>
                                                            <div class="input-group  date datePicker" id="Div4">
                                                                <asp:TextBox runat="server" class="form-control" ID="txtExcepteddateofcourse" name="txtTimescheduleforyearofcomm"></asp:TextBox>
                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-3">
                                                            <small class="text-gray">Copy of Certificate in support of successful completion of
                                                                Management Development Programme</small></label>
                                                        <asp:HiddenField ID="hdnPostSubFlag" runat="server" />
                                                        <asp:HiddenField ID="hdnTimeFrame" runat="server" />
                                                        <div class="col-sm-5">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="fuCoursecomplitation" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this, 'pdf,zip', 'pdf/zip', 4);" />
                                                                <asp:HiddenField ID="hdnCoursecomplitation" runat="server" />
                                                                <asp:HiddenField ID="hdnCoursecomplitationId" runat="server" Value="D123" />
                                                                <asp:LinkButton ID="lnkCoursecomplitationDoc" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkDocumentUpload_Click" OnClientClick="return HasFile('fuCoursecomplitation','Please upload Course complitation letter');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkCoursecomplitationDocDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypCoursecomplitation" runat="server" Target="_blank" ToolTip="View File"
                                                                    Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                            <asp:Label ID="lblCoursecomplitationDoc" Style="font-size: 12px;" CssClass="text-blue"
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
                                                            Course Details Attachment document
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgAttachment" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Copy of selection letter
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgSelectionletter" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Copy of Provisional Sanction Letter
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgSanctionletter" Height="15" Width="15" src="../images/cancel-square.png" />
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
                                                            Copy of Certificate in support of successful completion of Management Development
                                                            Programme
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgCompletion" Height="15" Width="15" src="../images/cancel-square.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Cancelled cheque to verify the entered A/c details
                                                        </td>
                                                        <td>
                                                            <asp:Image runat="server" ID="ImgCancelCheque" Height="15" Width="15" src="../images/cancel-square.png" />
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
                                            OnClick="btnDraft_Click" OnClientClick="return SaveDraft();" />
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
    </div>
    <uc3:footer ID="footer" runat="server" />
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">


        // To check decimal (controlId, DecimalPlaces)
        function CheckDecimal(e, t) { try { var n = ""; var r; if (parseInt(t)) { r = t } else { r = 2 } var i = document.getElementById(e); if (i == "undefined" || i == null) { i = e } if (typeof i.value === "undefined") { n = i.innerHTML.trim() } else { n = i.value.trim() } if (n.split(".").length - 1 > 1 || n.charAt(n.length - 1) == "." || n.charAt(0) == ".") { if (typeof i.value === "undefined") { setTimeout(function () { alert("Please enter valid decimal !"); $("#" + i.getAttribute("id")).effect("shake", { direction: "left", times: 2, distance: 5 }, 800) }, 1) } else { setTimeout(function () { alert("Please enter valid decimal !"); $(i).focus() }, 1) } return false } else { if (n.substr(n.lastIndexOf(".") + 1, n.length).length > r && n.lastIndexOf(".") > -1) { if (typeof i.value === "undefined") { setTimeout(function () { alert("Only " + r + " digits are allowed after decimal !"); $("#" + i.getAttribute("id")).effect("shake", { direction: "left", times: 2, distance: 5 }, 800) }, 1) } else { setTimeout(function () { alert("Only " + r + " digits are allowed after decimal !"); $(i).focus() }, 1) } return false } else { return true } } } catch (s) { } }

        // To make decimal (controlId, DecimalPlace)
        function makeDecimal(e, t) { var n = document.getElementById(e); var r; if (parseInt(t)) { r = t } else { r = 2 } if (n == "undefined" || n == null) { n = e } if (typeof n.value === "undefined") { if (n.innerHTML.trim().length > 0) { n.innerHTML = parseFloat(n.innerHTML.trim()).toFixed(r) } } else { if (n.value.trim().length > 0) { n.value = parseFloat(n.value.trim()).toFixed(r) } } }

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
            ImgSrc($('#hdnBank').val(), $('#ImgCancelCheque').attr("id"));
            ImgSrc($('#hdnAttachmentDoc').val(), $('#ImgAttachment').attr("id"));
            ImgSrc($('#hdnCopyletterselection').val(), $('#ImgSelectionletter').attr("id"));
            ImgSrc($('#hdnSanctionLetter').val(), $('#ImgSanctionletter').attr("id"));
            ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#ImgAssistance').attr("id"));
            ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#ImgNonAvailment').attr("id"));
            ImgSrc($('#hdnCoursecomplitation').val(), $('#ImgCompletion').attr("id"));
        }

        function pageLoad() {

            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy"
                });
                if ($("#hdnPostSubFlag").val() == 1) {
                    $("#txtExcepteddateofcourse").datepicker({
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
                            $("#txtExcepteddateofcourse").focus();
                            $("#<%=txtExcepteddateofcourse.ClientID %>").val("");
                        }
                        return true;
                    });
                }
            });
            DocCheckList();
            $(function () {
                if ($("#hdnRadibutton").val() == 2) {
                    $('.attorneysec').show();
                    $('.adhardetails').hide();
                    ImgSrc($('#hdnAUTHORIZEDFILE').val(), $('#ImgSign').attr("id"));
                }
                else {
                    if ($("input:checked").val() == 2) {
                        $('.attorneysec').show();
                        $('.adhardetails').hide();
                        ImgSrc($('#hdnAUTHORIZEDFILE').val(), $('#ImgSign').attr("id"));
                    }
                    else {
                        $('.adhardetails').show();
                        $('.attorneysec').hide();
                        ImgSrc('', $('#ImgSign').attr("id"));
                    }
                }
            });
            $(function () {
                if ($("#hdnIsProvisional").val() == 1) {
                    if ($("#hdnProvisionalDoc").val() != '') {
                        $('.BankDtlSec').show();
                        $('.docstatussec').show();
                        $('.availedsec').show();
                        $('.copyofProvisionalSec').show();
                    }
                    else {
                        $('.BankDtlSec').hide();
                        $('.docstatussec').hide();
                        $('.availedsec').hide();
                        $('.copyofProvisionalSec').hide();

                        jAlert('<strong> Copy of Provisional Sanction Letter is not available.<br/>You cannot proceed !</strong>', projname);
                    }
                }
                else {
                    $('.BankDtlSec').hide();
                    $('.docstatussec').hide();
                    $('.availedsec').hide();
                    $('.copyofProvisionalSec').hide();
                }
            });

            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#CourseDetails, #collapseOne, #AvailedClaimDetails, #BankDetails, #AdditionalDocuments, #DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").on("click", function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });
        }

        function dateCheck(from, to, check) {
            var fDate, lDate, cDate;
            fDate = Date.parse(from);
            lDate = Date.parse(to);
            cDate = Date.parse(check);
            if ((cDate <= lDate && cDate >= fDate)) {
                return true;
            }
            return false;
        }

        function MainVadidation() {
            if (!Validation()) {
                return false;
            }
        }


        function ProvMainVadidation() {
            if (!ProValidation()) {
                return false;
            }
        }

        function ProValidation() {


            if (!DropDownValidation('DdlGender', '0', 'Applicant Salutation', projname)) {
                return false;
            }
            if (!blankFieldValidation('TxtApplicantName', 'Applicant Name', projname)) {
                return false;
            }
            if (($("input[name='radApplyBy']:checked").val() != '1') && ($("input[name='radApplyBy']:checked").val() != '2')) {
                jAlert('Please select Application Applying By option', projname);
                return false;
            }
            if ($("input[name='radApplyBy']:checked").val() == '1') {
                if (!blankFieldValidation('TxtAdhaar1', 'Aadhaar Card no', projname)) {
                    return false;
                };
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('Aadhaar no should be 12 digits.', projname);
                    return false;
                }
            }
            if ($("input[name='radApplyBy']:checked").val() == '2') {
                if ($("#hdnAUTHORIZEDFILE").val() == '') {
                    jAlert('Please upload Authorizing letter signed by Authorized Signatory.', projname);
                    return false;
                }
            }

            if (DropDownValidation('ddlInstitute', '0', 'Institution Name', projname) == false) {
                return false;
            }

            if (blankFieldValidation('txtAddress', 'Address', projname) == false) {
                return false;
            }

            if (blankFieldValidation('txtCourseDuration', 'Course Duration', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtCourseFee', 'Course Fee', projname) == false) {
                return false;
            }
            if (blankFieldValidation('hdnAttachmentDoc', 'Please upload Course Detail document', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtDateofselection', 'Date of Selection', projname) == false) {
                return false;
            }

            if (blankFieldValidation('hdnCopyletterselection', 'Please upload Copy of Selection letter ', projname) == false) {
                return false;
            }
            return true;
        }

        function Validation() {

            if (!DropDownValidation('DdlGender', '0', 'Applicant Salutation', projname)) {
                return false;
            }
            if (!blankFieldValidation('TxtApplicantName', 'Applicant Name', projname)) {
                return false;
            }
            if (($("input[name='radApplyBy']:checked").val() != '1') && ($("input[name='radApplyBy']:checked").val() != '2')) {
                jAlert('Please select Application Applying By option', projname);
                return false;
            }
            if ($("input[name='radApplyBy']:checked").val() == '1') {
                if (!blankFieldValidation('TxtAdhaar1', 'Aadhaar Card no', projname)) {
                    return false;
                };
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('Aadhaar Card no should be 12 digits.', projname);
                    return false;
                }
            }
            if ($("input[name='radApplyBy']:checked").val() == '2') {
                if ($("#hdnAUTHORIZEDFILE").val() == '') {
                    jAlert('Please upload Authorizing letter signed by Authorized Signatory.', projname);
                    return false;
                }
            }

            if (blankFieldValidation('txtSanctionLetterNo', 'Sanction Letter No', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtSanctiondt', 'Date of Sanction Letter', projname) == false) {
                return false;
            }

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
                    jAlert('Please upload Document deatils of Undertaking on non-availment of subsidy.', projname);
                    return false;
                }
            }
            if (!blankFieldValidation('txtreimamt', 'Present Claim for reimbursement', projname)) {
                return false;
            }
            if (blankFieldValidation('txtAccNo', 'Account No of Industrial Unit', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtBnkNm', 'Bank Name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtBranch', 'Branch Name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtIFSC', '0', 'IFSC Code', projname) == false) {
                return false;
            }
            if ($("#hdnBank").val() == '') {
                jAlert('Please Upload any sample supporting document to verify account details(e.g. Bank Statement/Cancelled Cheque etc.).', projname);
                return false;
            }
            if (blankFieldValidation('txtExcepteddateofcourse', 'Date of course complitation', projname) == false) {
                return false;
            }

            if ($("#hdnCoursecomplitation").val() == '') {
                jAlert('Please upload Copy of Certificate in support of successful completion of Management Development Programme.', projname);
                $("#fuCoursecomplitation").focus();
                return false;
            }

            return true;
        }
        function SaveDraft() {
            if ($('#TxtAdhaar1').val() != "") {
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('Aadhaar no should be 12 digits.', projname);
                    return false;
                }
            }

            if ($('#txtAccNo').val().trim() != '') {
                if ($('#txtAccNo').val().length > 16) {
                    jAlert('Please enter Account No. within 16 characters .', projname);
                    $("#txtAccNo").focus();
                    return false;
                }
            }

            if ($('#txtIFSC').val().trim() != '') {
                var IFSCODE = $('#txtIFSC').val();
                if (IFSCODE.length < 7) {
                    jAlert('IFSC Code should be 7 digits.', projname);
                    return false;
                }
            }
        }
    </script>
    </form>
</body>
</html>
