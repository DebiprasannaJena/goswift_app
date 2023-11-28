<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Capitalinvestsubsidy.aspx.cs"
    Inherits="incentives_Capitalinvestsubsidy" %>

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
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        function openpopup(flu) {
            var i = flu.id;
            $("#" + i).click();
            return false;
        }
        $(document).ready(function () {
            $('.Pioneersec,.attorneysec,.adhardetails').hide();
            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
        });

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

        function DecimalValidation(Ctl) {
            var CtlId = Ctl.id;
            var amount = $("#" + CtlId).val();
            amount = Number(amount).toFixed(2);
            if (isNaN(amount)) {
                amount = Number(0).toFixed(2);
            }
            $("#" + CtlId).val(amount);
        }

        function validformindraft() {
            if ($('#TxtAdhaar1').val() != '') {
                var adhar = $('#TxtAdhaar1').val();
                if (adhar.length < 12) {
                    jAlert('<strong> Aadhaar no should be 12 digits.</strong>', '');
                    return false;
                }
            }
            if ($("#txtAccNo").val() != '') {
                var acno = $("#txtAccNo").val();
                if (acno.length > 16) {
                    jAlert('<strong> Account Number should not be more than 16 digit </strong>', 'Alert');
                    $("#txtAccNo").focus();
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
    <style>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdnTimeFrame" runat="server" />
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
                                                            <asp:ListItem Value="0">Mr.</asp:ListItem>
                                                            <asp:ListItem Value="1">Ms.</asp:ListItem>
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
                                                        <asp:HiddenField ID="hdnRadibutton" runat="server" />
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
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                            FilterType="Numbers" TargetControlID="TxtAdhaar1" FilterMode="ValidChars" ValidChars="0123456789" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group attorneysec" id="divAuthorizing" runat="server">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Please provide Authorizing letter signed by Authorized Signatory<a data-toggle="tooltip"
                                                            class="fieldinfo2" title="provide Authorizing letter signed by Authorized Signatory"><i
                                                                class="fa fa-question-circle" aria-hidden="true"></i></a></label>
                                                    <div class="col-sm-6">
                                                        <asp:Label runat="server" ID="lblAuthorizing"></asp:Label>
                                                        <br />
                                                        <asp:HiddenField runat="server" ID="hidAuthorizing" />
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="FlupAUTHORIZEDFILE" onchange="return FileCheck(this,'pdf','pdf', 4);"
                                                                runat="server" CssClass="form-control" />
                                                            <asp:HiddenField ID="hdnAUTHORIZEDFILE" runat="server" />
                                                            <asp:LinkButton ID="lnkAUTHORIZEDFILE" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkDocumentUpload_Click" ToolTip="Click here to upload the file." OnClientClick="return HasFile('FlupAUTHORIZEDFILE','Please Upload Authorizing letter');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkAUTHORIZEDFILEDdelete" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypAUTHORIZEDFILE" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue" ToolTip="View File"><i class="fa fa-download"></i></asp:HyperLink>
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
                                                        All Amounts to be Entered in INR( in Lakhs)</p>
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
                                                                <label for="Iname" class="col-sm-4 ">
                                                                    Direct Employment in numbers<small>(on Company Payroll)</small><span class="text-red">*</span>
                                                                </label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Direct_Emp_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-4 ">
                                                                    Contractual Employment in numbers</label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Contract_Emp_Before" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="Div_Direct_Emp_Doc_Before">
                                                            <div class="row">
                                                                <label class="col-sm-4 col-sm-offset-1">
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
                                                                <label for="Iname" class="col-sm-4 ">
                                                                    Direct Employment in numbers<small>(on Company Payroll)</small><span class="text-red">*</span>
                                                                </label>
                                                                <div class="col-sm-2">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="lbl_Direct_Emp_After" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-4 ">
                                                                    Contractual Employment in numbers</label>
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
                                                <label class="col-sm-2 ">
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
                                    <div class="panel-heading" role="tab" id="Div3">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#Investmentinpollution" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa  fa-minus"></i>Investment in pollution Control Equipment
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="Investmentinpollution" class="panel-collapse collapse in" role="tabpanel"
                                        aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <p class="text-red text-right">
                                                All Amounts to be Entered in INR( in Lakhs)</p>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-5">
                                                        Date of operationalization of pollution Control Equipment <a data-toggle="tooltip"
                                                            class="fieldinfo2" title="Operationalization Date"><i class="fa fa-question-circle"
                                                                aria-hidden="true"></i></a>
                                                    </label>
                                                    <div class="col-sm-5">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div11">
                                                            <asp:HiddenField ID="hdnPostSubFlag" runat="server" />
                                                            <asp:TextBox name="txtOperationalization" type="text" ID="txtOperationalization"
                                                                MaxLength="12" class="form-control" runat="server"></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-5">
                                                        Proof of Document in support of date of operationalization of pollution Control
                                                        Equipment <a data-toggle="tooltip" class="fieldinfo2" title="Upload proof of document/certificate from SPCB,Odisha">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </label>
                                                    <div class="col-sm-5">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="fuOperationalization" CssClass="form-control" runat="server"
                                                                onchange="return FileCheck(this,'pdf,zip','pdf/zip', 4);" />
                                                            <asp:LinkButton ID="lnkOperationalizationDoc" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkDocumentUpload_Click" OnClientClick="return HasFile('fuOperationalization','Please provide document of operationalization of pollution Control Equipment');"
                                                                ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkOperationalizationDocDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypOperationalization" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue" ToolTip="View File"><i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <asp:HiddenField ID="hdnOperationalization" runat="server" />
                                                        <asp:HiddenField ID="hdnOperationalizationDocId" runat="server" Value="D111" />
                                                        <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                        <asp:Label ID="lblOperationalization" Style="font-size: 12px;" CssClass="text-blue"
                                                            Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="up1" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-12 ">
                                                                <h4 class="h4-header">
                                                                    Equipment Details</h4>
                                                            </label>
                                                            <div class="col-sm-12">
                                                                <table class="table table-bordered">
                                                                    <tr>
                                                                        <th style="width: 5%">
                                                                            SL#
                                                                        </th>
                                                                        <th style="width: 25%">
                                                                            Equipment Type
                                                                        </th>
                                                                        <th style="width: 30%">
                                                                            Equipment Name
                                                                        </th>
                                                                        <th style="width: 30%">
                                                                            Invested Amount
                                                                        </th>
                                                                        <th style="width: 10%">
                                                                            Add More
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <th style="width: 5%">
                                                                            1
                                                                        </th>
                                                                        <td style="width: 25%">
                                                                            <asp:DropDownList ID="ddlEquipmentType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEquipmentType_SelectedIndexChanged"
                                                                                AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox CssClass="form-control" runat="server" ID="txtOtherEquipmentType"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtOtherEquipmentType"
                                                                                FilterMode="ValidChars" FilterType="Custom,UppercaseLetters,LowercaseLetters"
                                                                                ValidChars=" .,-" />
                                                                        </td>
                                                                        <td style="width: 30%">
                                                                            <asp:TextBox ID="txtEquipmentName" MaxLength="35" runat="server" CssClass="form-control text-left"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FEtxtEquipmentName" runat="server" TargetControlID="txtEquipmentName"
                                                                                ValidChars=" " FilterMode="ValidChars" FilterType="Custom,Lowercaseletters,Uppercaseletters,Numbers">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td class="text-right" style="width: 30%">
                                                                            <asp:TextBox ID="txtInvestedAmount" runat="server" CssClass="form-control text-right"
                                                                                MaxLength="15" onblur="DecimalValidation(this);"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txtInvestedAmount"
                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars="123456789." />
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <asp:LinkButton ID="lnkAddMore" CssClass="btn btn-success btn-sm" runat="server"
                                                                                OnClick="lnkEquipmentAdd_Click" OnClientClick="return checkEquipmentActivity();"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <br />
                                                                <asp:GridView ID="gvEquipment" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                                    ShowFooter="true" OnRowDataBound="gvEquipment_RowDataBound" ShowHeader>
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="Sl#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblslno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="25%" HeaderText="Equipment Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEuipmentType" runat="server" Text='<%# Eval("VCHEQIPMENTTYPE") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hdnEquipmentType" runat="server" Value='<%# Eval("INTEQUIPMENTTYPE") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="20%" HeaderText="Other Equipment Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOthetEuipmentType" runat="server" Text='<%# Eval("VCHOTHEREQIPTYPE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="20%" HeaderText="Equipment Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEquipmentName" runat="server" Text='<%#Eval("VCHEQUIPMENTNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <strong>
                                                                                    <label style="float: right;">
                                                                                        Total</label>
                                                                                </strong>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Invested Amount" ItemStyle-Width="20%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblInvestmentAmt" runat="server" Text='<%#Eval("DCMINVESTEDAMT") %>'
                                                                                    Style="float: right;"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTotal" runat="server" Style="float: right;"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger" CommandArgument='<%# Container.DataItemIndex %>'
                                                                                    OnClick="ImageButtonDelete_Click">
                                                                              
                                                                                    <i class="fa fa-trash"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
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
                                                        <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtMICRNo" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender111" runat="server" Enabled="True"
                                                            FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txtMICRNo"
                                                            FilterMode="ValidChars" ValidChars="0123456789" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <small class="text-gray">Upload cancelled cheque to verify the entered A/c details</small><span
                                                            class="text-red">*</span><a data-toggle="tooltip" class="fieldinfo2" title="Upload cancelled cheque to verify the entered A/c details"><i
                                                                class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="fuBank" CssClass="form-control" runat="server" onchange="return FileCheck(this,'pdf,jpg,jpeg,png','pdf/jpg/jpeg/png', 4);" />
                                                            <asp:HiddenField ID="hdnBank" runat="server" />
                                                            <asp:HiddenField ID="hdnBankID" runat="server" Value="D266" />
                                                            <asp:LinkButton ID="lnkBankUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkDocumentUpload_Click" ToolTip="Click here to upload the file." OnClientClick="return HasFile('fuBank','Please Upload any sample supporting document to verify account details.');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkBankDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkDocumentDelete_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypBank" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue"
                                                                ToolTip="View File"><i class="fa fa-download"></i></asp:HyperLink>
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
                                                        Proof of Document in support of date of operationalization of pollution Control
                                                        Equipment
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="ImgPollutionControl" Height="15" Width="15" src="../images/cancel-square.png" />
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
                    </div>
                    <div class="form-footer">
                        <div class="row">
                            <div class="col-sm-12 text-right">
                                <asp:Button ID="btnDraft" runat="server" Text="Save As Draft" CssClass="btn btn-warning"
                                    OnClientClick="return validformindraft();" OnClick="btnDraft_Click" />
                                <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-success"
                                    OnClick="btnApply_Click" OnClientClick="return MainVadidation();" />
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

        function HasFile(fuControlId, strError) {
            if ($('#' + fuControlId).val() == "") {
                jAlert(strError, projname);
                return false;
            }
        }

        function checkEquipmentActivity() {
            if (DropDownValidation('ddlEquipmentType', '0', 'Equipment Type', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtEquipmentName', 'Equipment Name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtInvestedAmount', 'Invested Amount', projname) == false) {
                return false;
            }
            if ($("#ddlEquipmentType").val('Others')) {
                if (blankFieldValidation('txtOtherEquipmentType', 'Equipment Type', projname) == false) {
                    return false;
                }
            }
            return true;
        }
        $('button selector').click(function () {
            window.location.href = 'the_link_to_go_to.html';
        })

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
            ImgSrc($('#hdnOperationalization').val(), $('#ImgPollutionControl').attr("id"));
        }
        function pageLoad() {
            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy"
                });
                if ($("#hdnPostSubFlag").val() == 1) {
                    $("#txtOperationalization").datepicker({
                        format: "dd-M-yyyy",
                        autoclose: true
                    }).on("changeDate", function (e) {
                        debugger;
                        var fromdate = new Date(e.date);
                        var toDate = new Date(fromdate);
                        toDate.setMonth(fromdate.getMonth() + parseInt($("#hdnTimeFrame").val()));
                        if (!isNaN(fromdate.getTime())) {
                            if (dateCheck(fromdate, toDate, new Date())) {
                            }
                            else {
                                jAlert('<strong> Current date must be with in ' + $("#hdnTimeFrame").val() + ' months of date of Approval.</strong>', projname);
                                $("#txtOperationalization").focus();
                                $("#<%=txtOperationalization.ClientID %>").val(e.Date);
                            }
                        }
                        else {
                            jAlert('<strong> Please select date.</strong>', projname);
                        }
                        return true;
                    });
                }
            });

            DocCheckList();
            OnChangeApplyBy();
            $(function () {
                if ($("#btnApply").attr('disabled')) {
                    jAlert('<strong> Not Eligible to apply for Large Unit Category!</strong>', projname);
                }

            });

            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails, #Investmentinpollution, #BankDetails, #DivDocCheck").removeClass('in');
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
        function Validation() {
            var rbtnVal = 0;

            if (!DropDownValidation('DdlGender', '0', 'Applicant Salutation', projname)) {
                return false;
            }
            if (blankFieldValidation('TxtApplicantName', 'Applicant Name', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('TxtApplicantName', 'Applicant Name', projname) == false) {
                return false;
            }
            if (!$('input[name=radApplyBy]:checked').val()) {
                jAlert('<strong> Please select Application Applying By</strong>', projname);
                return false;
            }
            else {
                rbtnVal = $('input[name=radApplyBy]:checked').val();
                if (rbtnVal == "1") {
                    if ($('#TxtAdhaar1').val() == '') {
                        jAlert('<strong> Please fill correct Aadhaar number.</strong>', projname);
                        return false;
                    }
                    var adhar = $('#TxtAdhaar1').val();
                    if (adhar.length < 12) {
                        jAlert('<strong>Aadhaar no should be 12 digits.</strong>', projname);
                        return false;
                    }
                }
                else if (rbtnVal == "2") {
                    if (blankFieldValidation('hdnAUTHORIZEDFILE', 'Please upload Authorizing letter signed by Authorized Signatory !', projname) == false) {
                        return false;
                    }
                }
            }

            if (blankFieldValidation('txtOperationalization', 'Date of operationalization of pollution Control Equipment', projname) == false) {
                return false;
            }
            if (blankFieldValidation('hdnOperationalization', 'Please upload Proof of Document/Certificate from SPCB,Odisha ', projname) == false) {
                return false;
            }

            var rowsBriefDtl = $("[id*=gvEquipment] tbody tr").length;
            if (rowsBriefDtl == 0) {
                jAlert('<strong> Equipment Details can not be blank !</strong>', projname);
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
            return true;
        }
    </script>
    </form>
</body>
</html>
