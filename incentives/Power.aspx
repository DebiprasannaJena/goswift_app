<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Power.aspx.cs" Inherits="incentives_EmploymentRating" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script type="text/javascript">

        var msgTitle = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'; //'Incentive';
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


            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
            $('.Pioneersec,.attorneysec,.adhardetails').hide();

        });
    </script>
    <script type="text/javascript" language="javascript">

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
        function DecimalValidation(Ctl) {
            var CtlId = Ctl.id;
            var amount = $("#" + CtlId).val();
            amount = Number(amount).toFixed(2);
            if (isNaN(amount)) {
                amount = Number(0).toFixed(2);
            }
            $("#" + CtlId).val(amount);
        }
        /////////////////// jquery method for Industrial Unit////////////////////////////////////////
       
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
                                <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                    <i class="fa fa-print"></i></a>
                            </div>
                            <h2>
                                <asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
                        </div>
                        <div class="form-body">
                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                <!--Start Commom Panel***-->
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
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtAdhaar1"
                                                            FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <div class="clerfix">
                                                        </div>
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
                                                            <asp:FileUpload ID="FlupAUTHORIZEDFILE" onchange="return FileCheck(this, 'pdf','pdf',4);"
                                                                runat="server" CssClass="form-control" />
                                                            <asp:HiddenField ID="hdnAUTHORIZEDFILE" runat="server" />
                                                            <asp:LinkButton ID="lnkUpAUTHORIZEDFILE" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="LnkUpAUTHORIZEDFILE_Click" ToolTip="Click here to upload the file."
                                                                OnClientClick="return HasFile('FlupAUTHORIZEDFILE','Please Upload Authorizing letter.');"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LnkDelAUTHORIZEDFILE" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="LnkDelAUTHORIZEDFILE_Click" Visible="false" ToolTip="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="HypViewAUTHORIZEDFILE" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue" ToolTip="View File">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf file only and Max file Size 4 MB)</small>
                                                        <asp:Label ID="LblAUTHORIZEDFILE" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
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
                                                <div class="form-group" id="div_Pioneer_Doc_Name" runat="server">
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
                                        <div class="panel-body">
                                            <p class="text-red text-right">
                                                All Amounts to be Entered in INR(in Lakh)</p>
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
                                                                DataKeyNames="vchProductName" AutoGenerateColumns="false" EmptyDataText="No records found...">
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
                                                            Direct Employment In Numbers<small>(on Company Payroll) </small>
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
                                                        <label class="col-sm-4">
                                                            <asp:Label ID="Lbl_Direct_Emp_Before_Doc_Name" runat="server" Text=""></asp:Label>
                                                            <asp:HiddenField ID="Hid_Direct_Emp_Before_Doc_Code" runat="server" />
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
                                                                DataKeyNames="vchProductName" AutoGenerateColumns="false" EmptyDataText="No records found...">
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
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            <asp:Label ID="Lbl_Direct_Emp_After_Doc_Name" runat="server" Text=""></asp:Label>
                                                            <asp:HiddenField ID="Hid_Direct_Emp_After_Doc_Code" runat="server" />
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
                                                        <div class="col-sm-4">
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
                                                    <label class="col-sm-4">
                                                        <asp:Label ID="Lbl_Term_Loan_Doc_Name" runat="server" Text=""></asp:Label>
                                                        <asp:HiddenField ID="Hid_Term_Loan_Doc_Code" runat="server" />
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
                                                <label class="col-sm-5">
                                                    FDI(If any)
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="lbl_FDI_Componet" runat="server" CssClass="form-control-static"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--End Commom Panel***-->
                                <!--Start Page Panel***-->
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingTwo">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#ConstractionDetails" aria-expanded="false" aria-controls="collapseTwo"><i
                                                    class="more-less fa  fa-minus"></i>Details for Reimbursement of Power Tariff
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="ConstractionDetails" class="panel-collapse collapse in" role="tabpanel"
                                        aria-labelledby="headingTwo">
                                        <p class="text-red text-right">
                                            All Amounts to be Entered in INR(in Lakh)</p>
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-8">
                                                        Investment for setting up of industrial unit /Additional investment for expansion
                                                        / modernization / diversification
                                                    </label>
                                                    <div class="col-sm-12 ">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                    Slno.
                                                                </th>
                                                                <th>
                                                                    Total Capital Investment for New / (E/M/D)
                                                                </th>
                                                                <th>
                                                                    Schematic provisions (Rs)
                                                                </th>
                                                                <th>
                                                                    Till Date of Commencement of Production (Rs)
                                                                </th>
                                                                <th>
                                                                    If different, reasons thereof
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    1
                                                                </td>
                                                                <td>
                                                                    New investment<span class="text-red">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtNewSP" runat="server" CssClass="form-control" MaxLength="14"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" TargetControlID="TxtNewSP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtNewCP" runat="server" CssClass="form-control" MaxLength="14"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="TxtNewCP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtNewJR" runat="server" CssClass="form-control" MaxLength="100"
                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender556" runat="server" TargetControlID="TxtNewJR"
                                                                        FilterMode="ValidChars" FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"
                                                                        ValidChars=" .,-/_" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    2
                                                                </td>
                                                                <td>
                                                                    Land<span class="text-red">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtLandSP" runat="server" CssClass="form-control" MaxLength="14"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" TargetControlID="TxtLandSP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtLandCP" runat="server" CssClass="form-control" MaxLength="14"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" TargetControlID="TxtLandCP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtLandJR" runat="server" CssClass="form-control" MaxLength="100"
                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="TxtLandJR"
                                                                        FilterMode="ValidChars" FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"
                                                                        ValidChars=" .,-/_" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    3
                                                                </td>
                                                                <td>
                                                                    Building<span class="text-red">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtBuildSP" runat="server" CssClass="form-control" MaxLength="14"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" TargetControlID="TxtBuildSP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtBuildCP" runat="server" CssClass="form-control" MaxLength="14"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server" TargetControlID="TxtBuildCP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtBuildJR" runat="server" CssClass="form-control" MaxLength="100"
                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TxtBuildJR"
                                                                        FilterMode="ValidChars" FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"
                                                                        ValidChars=" .,-/_" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    4
                                                                </td>
                                                                <td>
                                                                    Plant & Machinery<span class="text-red">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtPMSP" runat="server" CssClass="form-control" MaxLength="14" onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" TargetControlID="TxtPMSP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtPMCP" runat="server" CssClass="form-control" MaxLength="14" onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" runat="server" TargetControlID="TxtPMCP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtPMJR" runat="server" CssClass="form-control" MaxLength="100"
                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TxtPMJR"
                                                                        FilterMode="ValidChars" FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"
                                                                        ValidChars=" .,-/_" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    5
                                                                </td>
                                                                <td>
                                                                    Other Fixed Assets<span class="text-red">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtOFASP" runat="server" CssClass="form-control" MaxLength="14"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server" TargetControlID="TxtOFASP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtOFACP" runat="server" CssClass="form-control" MaxLength="14"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server" TargetControlID="TxtOFACP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtOFAJR" runat="server" CssClass="form-control" MaxLength="100"
                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="TxtOFAJR"
                                                                        FilterMode="ValidChars" FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"
                                                                        ValidChars=" .,-/_" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    6
                                                                </td>
                                                                <td>
                                                                    Electrical Installations<span class="text-red">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtElectSP" runat="server" CssClass="form-control" MaxLength="14"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" runat="server" TargetControlID="TxtElectSP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtElectCP" runat="server" CssClass="form-control" MaxLength="14"
                                                                        onblur="DecimalValidation(this);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender34" runat="server" TargetControlID="TxtElectCP"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtElectJR" runat="server" CssClass="form-control" MaxLength="100"
                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="TxtElectJR"
                                                                        FilterMode="ValidChars" FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"
                                                                        ValidChars=" .,-/_" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 ">
                                                        Justification for excess investment, if any..-</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="TxtJustExc" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-8 ">
                                                        Total unit consumed for the production during the year
                                                        <asp:Label class="monthadd" ID="LblYear" runat="server">
                                                        </asp:Label></label>
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <th>
                                                                    Total unit consumed for the production<span class="text-red">*</span>
                                                                </th>
                                                                <th>
                                                                    Amount paid in Rs<span class="text-red">*</span>
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="TxtTotUnit" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtTotAmtPaid" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender35" runat="server" TargetControlID="TxtTotAmtPaid"
                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="" id="div4" runat="server">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-0">
                                                        Bills & Money receipt towards payment of Electricity Bill for production purpose
                                                        only <a data-toggle="tooltip" class="fieldinfo2" title="Bills & Money receipt towards payment of Electricity Bill for
                                                            production purpose only"><i class="fa fa-question-circle" aria-hidden="true"></i>
                                                            <span class="text-red">*</span> </a>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="flMoneyreceipt" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip','pdf/zip',4);" />
                                                            <asp:HiddenField ID="D131" runat="server" Value="" />
                                                            <asp:LinkButton ID="lnkUMoneyreceipt" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClientClick="return HasFile('flMoneyreceipt','Please Upload Bills & Money receipt.');"
                                                                OnClick="lnkUMoneyreceipt_Click" ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDMoneyreceipt" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkDMoneyreceipt_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypMoneyreceipt" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                        <asp:Label ID="lblMoneyreceipt" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
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
                                                href="#BankDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                                </i>Bank Details</a>
                                        </h4>
                                    </div>
                                    <div id="BankDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
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
                                                        <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" MaxLength="50"
                                                            TextMode="MultiLine"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtLocation" runat="server"
                                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txtBranch"
                                                            FilterMode="ValidChars" ValidChars=" ,.-" />
                                                    </div>
                                                    <label for="Iname" class="col-sm-2 ">
                                                        IFSC Code <span class="text-red">*</span></label>
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
                                                        MICR No.
                                                    </label>
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
                                                            <asp:FileUpload ID="fuBank" CssClass="form-control" runat="server" onchange="return FileCheck(this, 'pdf,zip','pdf/zip',4);" />
                                                            <asp:HiddenField ID="hdnBank" runat="server" />
                                                            <asp:HiddenField ID="hdnBankID" runat="server" Value="D266" />
                                                            <asp:LinkButton ID="lnkBankUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                ToolTip="Click here to upload the file." OnClientClick="return HasFile('fuBank','Please Upload any sample supporting document to verify account details.');"
                                                                OnClick="lnkBankUpload_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkBankDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                Visible="false" ToolTip="Delete" OnClick="lnkBankDelete_Click"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypBank" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue"
                                                                ToolTip="View File"><i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf/.zip file only and Max file Size 4 MB)</small>
                                                        <asp:Label ID="lblBank" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
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
                                                href="#InterestSubsidyDetails" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa  fa-plus"></i>Additional Documents</a>
                                        </h4>
                                    </div>
                                    <div id="InterestSubsidyDetails" class="panel-collapse collapse" role="tabpanel"
                                        aria-labelledby="headingThree">
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
                                                                <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDDelay" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkDDelay_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypDelay" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                           <i class="fa fa-download"></i></asp:HyperLink>
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
                                                        without power ) <a data-toggle="tooltip" class="fieldinfo2" title="Factory & Boiler-  For all industry (10 with direct employment / 20 no of employment without power ) ">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="flCleanApproveAuthority" CssClass="form-control" runat="server"
                                                                onchange="return FileCheck(this, 'zip','zip',4);" />
                                                            <asp:HiddenField ID="D280" runat="server" Value="" />
                                                            <asp:HiddenField ID="hdnBoilerDownloaded" runat="server" Value="" />
                                                            <asp:LinkButton ID="lnkUCleanApproveAuthority" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClientClick="return HasFile('flCleanApproveAuthority','Please Upload Factory & Boiler for all industry related Document');"
                                                                ToolTip="Click here to upload the file." OnClick="lnkUCleanApproveAuthority_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDCleanApproveAuthority" runat="server" CssClass="input-group-addon bg-red"
                                                                Visible="false" OnClick="lnkDCleanApproveAuthority_Click"><i class="fa fa-trash-o" aria-hidden="true"></i> </asp:LinkButton>
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
                                                        Bills & Money receipt towards payment of Electricity Bill for production purpose
                                                        only
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="ImgMoneyReceipt" Height="15" Width="15" src="../images/cancel-square.png" />
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
                                <!--End Page Panel***-->
                            </div>
                        </div>
                    </div>
                    <div class="form-footer">
                        <div class="row">
                            <div class="col-sm-12 text-right">
                                <asp:Button ID="btnDraft" runat="server" Text="Save As Draft" CssClass="btn btn-warning"
                                    OnClientClick="return SaveDraft()" OnClick="btnDraft_Click" />
                                <asp:Button ID="btnEdit" runat="server" Text="Apply" CssClass="btn btn-success" OnClientClick="return ApplySubmit()"
                                    OnClick="btnApply_Click" />
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
    <script language="javascript" type="text/javascript">

        var msgTitle = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'; //  'Incentive';
        var fileExtension = ['pdf'];
        function IndustryUnitValidation() {

            if (!DropDownValidation('DdlGender', '0', 'Applicant Salutation', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('TxtApplicantName', 'Applicant Name', msgTitle)) {
                return false;
            }
            if (($("input[name='radApplyBy']:checked").val() != '1') && ($("input[name='radApplyBy']:checked").val() != '2')) {
                jAlert('Please select Application By option', msgTitle);
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
                    jAlert('Please upload Authorizing letter .', msgTitle);
                    return false;
                }
            }
            return true;
        }



        function vPower() {
            var xxLeft = 0;
            var xxRight = 0;
            if (!blankFieldValidation("TxtNewSP", "New investment Schematic provisions ", msgTitle)) {
                return false;
            }
            if (!blankFieldValidation("TxtNewCP", "New investment Till Date of Commencement of Production", msgTitle)) {
                return false;
            }
            xxLeft = parseFloat($('#TxtNewSP').val());
            xxRight = parseFloat($('#TxtNewCP').val());
            if (xxLeft < xxRight) {
                if (!blankFieldValidation("TxtNewJR", "New investment Difference reasons", msgTitle)) {
                    $('TxtNewJR').focus();
                    return false;
                }
            }
            if (!WhiteSpaceValidation1st("TxtNewJR", "New investment Difference reasons", msgTitle)) {
                return false;
            }

            if (!blankFieldValidation("TxtLandSP", "Land Schematic provisions ", msgTitle)) {
                return false;
            }
            if (!blankFieldValidation("TxtLandCP", "Land Till Date of Commencement of Production", msgTitle)) {
                return false;
            }
            xxLeft = parseFloat($('#TxtLandSP').val());
            xxRight = parseFloat($('#TxtLandCP').val());
            if (xxLeft < xxRight) {
                if (!blankFieldValidation("TxtLandJR", "Land Difference reasons", msgTitle)) {
                    return false;
                }
            }
            if (!WhiteSpaceValidation1st("TxtLandJR", "Land Difference reasons", msgTitle)) {
                return false;
            }

            if (!blankFieldValidation("TxtBuildSP", "Building Schematic provisions ", msgTitle)) {
                return false;
            }
            if (!blankFieldValidation("TxtBuildCP", "Building Till Date of Commencement of Production", msgTitle)) {
                return false;
            }
            xxLeft = parseFloat($('#TxtBuildSP').val());
            xxRight = parseFloat($('#TxtBuildCP').val());
            if (xxLeft < xxRight) {
                if (!blankFieldValidation("TxtBuildJR", "Building Difference reasons", msgTitle)) {
                    return false;
                }
            }
            if (!WhiteSpaceValidation1st("TxtBuildJR", "Building Difference reasons", msgTitle)) {
                return false;
            }

            if (!blankFieldValidation("TxtPMSP", "Plant & Machinery Schematic provisions ", msgTitle)) {
                return false;
            }
            if (!blankFieldValidation("TxtPMCP", "Plant & Machinery Till Date of Commencement of Production", msgTitle)) {
                return false;
            }
            xxLeft = parseFloat($('#TxtPMSP').val());
            xxRight = parseFloat($('#TxtPMCP').val());
            if (xxLeft < xxRight) {
                if (!blankFieldValidation("TxtPMJR", "Plant & Machinery Difference reasons", msgTitle)) {
                    return false;
                }
            }
            if (!WhiteSpaceValidation1st("TxtPMJR", "Plant & Machinery Difference reasons", msgTitle)) {
                return false;
            }

            if (!blankFieldValidation("TxtOFASP", "Other Fixed Assets Schematic provisions ", msgTitle)) {
                return false;
            }
            if (!blankFieldValidation("TxtOFACP", "Other Fixed Assets Till Date of Commencement of Production", msgTitle)) {
                return false;
            }
            xxLeft = parseFloat($('#TxtOFASP').val());
            xxRight = parseFloat($('#TxtOFACP').val());
            if (xxLeft < xxRight) {
                if (!blankFieldValidation("TxtOFAJR", "Other Fixed Assets Difference reasons", msgTitle)) {
                    return false;
                }
            }
            if (!WhiteSpaceValidation1st("TxtOFAJR", "Other Fixed Assets Difference reasons", msgTitle)) {
                return false;
            }

            if (!blankFieldValidation("TxtElectSP", "Electrical Installations Schematic provisions ", msgTitle)) {
                return false;
            }
            if (!blankFieldValidation("TxtElectCP", "Electrical Installations Till Date of Commencement of Production", msgTitle)) {
                return false;
            }
            xxLeft = parseFloat($('#TxtElectSP').val());
            xxRight = parseFloat($('#TxtElectCP').val());
            if (xxLeft < xxRight) {
                if (!blankFieldValidation("TxtElectJR", "Electrical Installations Difference reasons", msgTitle)) {
                    return false;
                }
            }
            if (!WhiteSpaceValidation1st("TxtElectJR", "Electrical Installations Difference reasons", msgTitle)) {
                return false;
            }

            /////////////////////////------------------------------------------------------------------------------------------
            /////////////////////////------------------------------------------------------------------------------------------
            /////////////////////////------------------------------------------------------------------------------------------
            /////////////////////////------------------------------------------------------------------------------------------


            if (!blankFieldValidation("TxtTotUnit", "Total unit consumed for the production", msgTitle)) {
                return false;
            }
            if (!WhiteSpaceValidation1st("TxtTotUnit", "Total unit consumed for the production", msgTitle)) {
                return false;
            }
            else if (!blankFieldValidation("TxtTotAmtPaid", "Amount paid ", msgTitle)) {
                return false;
            }
            else if ($("#D131").val() == '') {
                jAlert('Please upload Bills & Money receipt  .', msgTitle);
                return false;
            }
            else {
                return true;
            }
        }



        function vBank() {

            var IFSCODE = $('#txtIFSC').val();
            if (!blankFieldValidation("txtAccNo", "Account No. of Industrial Unit", msgTitle)) {
                return false;
            }
            else if (!blankFieldValidation("txtBnkNm", "Bank Name", msgTitle)) {
                return false;
            }
            else if (!blankFieldValidation("txtBranch", "Branch Name", msgTitle)) {
                return false;
            }
            else if (!blankFieldValidation("txtIFSC", "IFSC code", msgTitle)) {
                return false;
            }
            ////            else if (!blankFieldValidation("txtMICRNo", "MICR No.", msgTitle)) {
            ////                return false;
            ////            }
            else if (!WhiteSpaceValidation1st("txtAccNo", "Account No. of Industrial Unit", msgTitle)) {
                return false;
            }
            else if (!WhiteSpaceValidation1st("txtBnkNm", "Bank Name", msgTitle)) {
                return false;
            }
            else if (!WhiteSpaceValidation1st("txtBranch", "Branch Name", msgTitle)) {
                return false;
            }
            else if (!WhiteSpaceValidation1st("txtIFSC", "IFSC code", msgTitle)) {
                return false;
            }
            else if (!WhiteSpaceValidation1st("txtMICRNo", "MICR No.", msgTitle)) {
                return false;
            }
            else if (!WhiteSpaceValidationLast("txtAccNo", "Account No. of Industrial Unit", msgTitle)) {
                return false;
            }
            else if (!WhiteSpaceValidationLast("txtBnkNm", "Bank Name", msgTitle)) {
                return false;
            }
            else if (!WhiteSpaceValidationLast("txtBranch", "Branch Name", msgTitle)) {
                return false;
            }
            else if (!WhiteSpaceValidationLast("txtIFSC", "IFSC code", msgTitle)) {
                return false;
            }
            else if (IFSCODE.length < 7) {
                jAlert('IFSC Code should be minimum 7 digits.', msgTitle);
                return false;
            }
            else if ($("#hdnBank").val() == '') {
                jAlert('Please Upload any cancelled cheque document to verify account details .', msgTitle);
                return false;
            }
            else {
                return true;
            }


        }

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
        function ApplySubmit() {
            if (!IndustryUnitValidation()) {
                return false;
            }
            if (!vPower()) {
                return false;
            }
            if (!vBank()) {
                return false;
            }
            ////            if ($("#D275").val() == '') {
            ////                jAlert('Please Upload OSPCB consent to operate related document .', msgTitle);
            ////                return false;
            ////            }
            ////            if ($("#D274").val() == '') {
            ////                jAlert('Please Upload Sector Relevant Document  .', msgTitle);
            ////                return false;
            ////            }
            ////            if ($("#D280").val() == '') {
            ////                jAlert('Please Upload Factory & Boiler - For all industry related Document  .', msgTitle);
            ////                return false;
            ////            }


        }



        function pageLoad() {
            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails, #ConstractionDetails, #BankDetails, #InterestSubsidyDetails, #DivDocCheck").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").on("click", function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });

            if ($("#Lbl_Term_Loan_Doc_Name").text() == '') {
                $("#termloan").hide();
            }
            if ($("#Lbl_Direct_Emp_After_Doc_Name").text() == '') {
                $("#after").hide();
            }
            if ($("#Lbl_Direct_Emp_Before_Doc_Name").text() == '') {
                $("#before").hide();
            }

            $('.datePicker').datepicker({
                minDate: new Date(),
                autoclose: true,
                format: "dd-M-yyyy"
            });
            DocCheckList();
            OnChangeApplyBy();
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
            ImgSrc($('#hdnBank').val(), $('#ImgCancelCheque').attr("id"));
            ImgSrc($('#D275').val(), $('#ImgOSPCB').attr("id"));
            ImgSrc($('#D274').val(), $('#ImgSectRel').attr("id"));
            ImgSrc($('#D280').val(), $('#ImgCleanApproveAuthority').attr("id"));
            ImgSrc($('#D131').val(), $('#ImgMoneyReceipt').attr("id"));

        }

    </script>
    </form>
</body>
</html>
