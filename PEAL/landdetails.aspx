<%@ Page Language="C#" AutoEventWireup="true" CodeFile="landdetails.aspx.cs" Inherits="landdetails"
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/pealwebfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/decimalrstr.js" type="text/javascript"></script>
    <style type="text/css">
        input[type=checkbox], input[type=radio] {
            margin: 4px 4px 0 0px;
        }

        .primary-heading {
            border-bottom: 1px solid #bbbaba;
            margin-bottom: 0px;
            padding: 10px;
        }

            .primary-heading h4 {
                margin-top: 0px;
                margin-bottom: 0px;
                color: #000;
            }

            .primary-heading h2 {
                line-height: 32px;
                margin-top: 0px;
                margin-bottom: 0px;
            }

                .primary-heading h2 small {
                    font-size: 80%;
                }

        .form-body .table {
            margin-bottom: 0px;
        }
        .form-body {
    padding: 10px 10px;
   
}

        @media print {
            .noprint {
                display: none;
            }

            .wizard-inner {
                display: none;
            }

            .nav nav-tabs {
                display: none;
            }

            .rightpnl-btn {
                display: none;
            }

            .rightpnl-btn {
                display: none;
            }

            .navbar-brand {
                float: left;
            }

            #btnBack {
                display: none;
            }

            #btnNext {
                display: none;
            }

            #btnSaveAsdraft {
                display: none;
            }

            #btnReset {
                display: none;
            }

            #btnSave {
                display: none;
            }

            .header-investorDetails {
                display: none;
            }

            .investrs-tab {
                display: none;
            }

            .col-sm-4 {
                width: 33%;
                float: left;
            }

            label {
                font-weight: 400;
            }

            .iconsdiv, .footer-wrapper {
                display: none;
            }

            .form-group {
                margin-bottom: 8px;
            }

            header {
                border-bottom: 1px solid #ccc;
            }

            .form-header {
                padding: 0px;
                border-bottom: 0px;
            }

            .form-body {
                padding: 10px 0px;
            }

            .form-sec h2 {
                font-weight: 400;
                color: #000;
                border-bottom: 0px;
                background: #ccc;
            }

            .form-sec {
                margin-bottom: 10px;
            }

            .col-sm-3 {
                width: 25%;
                float: left;
            }

            .col-sm-6 {
                width: 50%;
                float: left;
            }

            .col-sm-2 {
                width: 16%;
                float: left;
            }

            .col-sm-4 {
                width: 32.2%;
                float: left;
                padding: 0px;
                margin-right: 7px;
            }

            .collapse, collapsed {
                display: block;
                height: auto !important;
            }

            .more-less {
                display: none;
            }

            .navbar-toggle {
                display: none;
            }

            .scrollup {
                display: none;
            }

            .panel-title {
                font-weight: 400;
            }

            .panel-body .h4-header, .table > tbody > tr > th {
                font-weight: 400;
            }

            a[href]:after {
                content: none !important;
            }

            .pinsection {
                margin-left: 15px;
            }

            .form-control {
                height: 26px;
                padding: 0px 3px;
                font-size: 12px;
            }

            .input-group-addon {
                padding: 3px 12px;
            }

            .form-body .form-group {
                margin-bottom: 5px;
            }

            label {
                font-size: 13px;
                margin: 2px 0px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <div class="registration-div">
                <div class="investrs-tab">
                    <div class="iconsdiv tab-icondiv">
                        <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                            <i class="fa fa-print"></i></a><a href="javascript:history.back()" title="Back" id="A2"
                                class="pull-right printbtn"><i class="fa fa-chevron-circle-left"></i></a>
                    </div>
                    <uc4:pealmenu ID="pealmenu" runat="server" />
                </div>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <div class="wizard wizard2">
                            <div class="wizard-inner" tabindex="-1">
                                <div class="connecting-line" tabindex="-1">
                                </div>
                                <ul class="nav nav-tabs" role="tablist" tabindex="-1">
                                    <li class="backactive" tabindex="-1"><a href="PromoterDetails.aspx" aria-controls="Company Information"
                                        title="Company Information" data-toggle="tooltip" data-placement="top" tabindex="-1"><span class="round-tab" tabindex="-1">
                                            <i class="glyphicon glyphicon-home"></i></span><small><i class="fa fa-check text-success backcheck"
                                                aria-hidden="true"></i>&nbsp;<b>1.</b> Company Information</small> </a>
                                    </li>
                                    <li class="backactive" tabindex="-1"><a href="proposeddetails.aspx" aria-controls="Project Information"
                                        title="Project Information" data-toggle="tooltip" data-placement="top" tabindex="-1"><span class="round-tab" tabindex="-1">
                                            <i class="glyphicon glyphicon-pencil"></i></span><small><i class="fa fa-check text-success backcheck2"
                                                aria-hidden="true"></i>&nbsp;<b>2.</b> Project Information</small> </a>
                                    </li>
                                    <li role="presentation" class="active" tabindex="-1"><a href="javascript:void(0)" aria-controls="Land and Utility Requirment"
                                        title=" Land and Utility Requirment" data-toggle="tooltip" data-placement="top" tabindex="-1">
                                        <span class="round-tab" tabindex="-1"><i class="glyphicon glyphicon-picture"></i></span><small><i
                                            class="fa fa-check text-success backcheck3" aria-hidden="true"></i>&nbsp;<b> 3.</b>
                                            Land and Utility Requirement</small> </a></li>
                                    <li role="presentation" class="disabled" tabindex="-1"><a href="#complete" data-toggle="tab" aria-controls="Declaration"
                                        role="tab" title="Declaration" data-toggle="tooltip" data-placement="top" tabindex="-1"><span class="round-tab" tabindex="-1">
                                            <i class="glyphicon glyphicon-ok"></i></span><small><b>4.</b> Declaration</a></small>
                                    </a></li>
                                </ul>
                            </div>
                            <div id="divExistunit">
                                <div class="form-sec">
                                    <h1 class="headerpeal">Project Evaluation including Allotment of Land</h1>
                                    <div class="form-header">
                                        <h2 class="m-t-0 m-b-0">12.Proposed location of land</h2>
                                    </div>
                                    <div class="form-body">
                                        <div class="form-group">
                                            <div class="row pinsection">
                                                <div class="col-sm-4">
                                                    <label for="Type4">
                                                        Land required from government<span class="text-red">*</span></label>
                                                </div>
                                                <div class="col-sm-4 ">
                                                    <asp:RadioButtonList ID="rdbExIndustry" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                        OnSelectedIndexChanged="rdbExIndustry_SelectedIndexChanged" AutoPostBack="true"
                                                        TabIndex="1">
                                                        <asp:ListItem class="radio-inline" Text="Yes" Value="1" />
                                                        <asp:ListItem class="radio-inline" Text="No" Value="0" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="Div1" class="form-group" runat="server">
                                            <div class="row pinsection">
                                                <div class="col-sm-4">
                                                    <label for="District">
                                                        District <span class="text-red">*</span></label><br />
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                        AutoPostBack="true" TabIndex="2">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <a data-toggle="tooltip" class="fieldinfo" title="Specify the District of proposed location of land">
                                                        <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Block">
                                                        Block <span class="text-red">*</span></label><br />
                                                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" TabIndex="3">
                                                    </asp:DropDownList>
                                                    <a data-toggle="tooltip" class="fieldinfo" title="Specify the Block of proposed location of land">
                                                        <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="ICapacity">
                                                        Extent of land
                                                    <%--<asp:Label ID="lblDet" runat="server"></asp:Label>--%>(in acre)<span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtExtent" CssClass="form-control" runat="server" MaxLength="8"
                                                        Onkeypress="return inputLimiter(event,'Decimal')" TabIndex="4" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                        onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group" id="dvB" runat="server">
                                            <div class="row pinsection">
                                                <div class="col-sm-4">
                                                    <label for="Block">
                                                        Whether land is required in IDCO industrial estate <span class="text-red">*</span></label><br />
                                                    <asp:DropDownList ID="ddlrequired" runat="server" CssClass="form-control" TabIndex="5">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4" id="divLD" runat="server">
                                                    <label for="Block">
                                                        Name of the IDCO industrial estate <span class="text-red">*</span></label><br />
                                                    <asp:DropDownList ID="ddlIDCOName" runat="server" CssClass="form-control" TabIndex="6">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4" id="divLD2" runat="server">
                                                    <label for="Block">
                                                        Whether land to be acquired by IDCO</label><br />
                                                    <asp:DropDownList ID="ddlLandacquiredIDCO" runat="server" CssClass="form-control" TabIndex="7">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group" id="DVNewlayout">
                                            <div class="row pinsection">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-md-4 col-sm-12">
                                                            <label for="Supply">
                                                                Project Land Use Statement</label>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="upldProjectlandStatement" CssClass="form-control" runat="server"
                                                                    TabIndex="8" ToolTip="Upload Project Land Use Statement, within 4 MB" />
                                                                <asp:HiddenField ID="hdnProjectlandStatement" runat="server" />
                                                                <%--<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                <asp:LinkButton ID="lnkProjectlandStatement" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkProjectlandStatement_Click">
                                                   <i class="fa fa-upload" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelProjectlandStatement" CssClass="input-group-addon bg-red"
                                                                    runat="server" OnClick="lnkDelProjectlandStatement_Click">
                                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <asp:HyperLink ID="hypProjectlandStatement" CssClass="input-group-addon bg-blue"
                                                                    Visible="false" runat="server" Target="_blank">
                                                                <i class="fa  fa-download"></i>
                                                                </asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblProjectlandStatement" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Project Land Use Statement uploaded successfully."></asp:Label>
                                                            <asp:HiddenField ID="hdn2ProjectlandStatement" runat="server" />
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkProjectlandStatement" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <label for="Supply" class="col-md-8 col-sm-12">
                                                            Project Layout Plan</label>
                                                        <div class="col-md-4 col-sm-12">
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="upldProjectLaoutPlan" CssClass="form-control" runat="server"
                                                                    TabIndex="9" ToolTip="Upload relevant document on this adopted, within 4 MB" />
                                                                <asp:HiddenField ID="hdnProjectLaoutPlan" runat="server" />
                                                                <asp:LinkButton ID="lnkProjectLaoutPlan" CssClass="input-group-addon bg-green" runat="server"
                                                                    OnClick="lnkProjectLaoutPlan_Click"> 
                                                   <i class="fa fa-upload" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelProjectLaoutPlan" CssClass="input-group-addon bg-red" runat="server"
                                                                    OnClick="lnkDelProjectLaoutPlan_Click">
                                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <asp:HyperLink ID="hypProjectLaoutPlan" CssClass="input-group-addon bg-blue" Visible="false"
                                                                    runat="server" Target="_blank">
                                                               <i class="fa  fa-download"></i>
                                                                </asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblProjectLaoutPlan" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Project Layout Plan uploaded successfully."></asp:Label>
                                                            <asp:HiddenField ID="hdn2ProjectLaoutPlan" runat="server" />
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkProjectLaoutPlan" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <%--<div class="col-sm-4" id="DvFilAdoption">
                                <label for="Supply">
                                    Adoption of water Conservation system details report</label>
                                <asp:FileUpload ID="FileUpload3" CssClass="form-control" runat="server" />
                            </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="dvD" runat="server">
                                        <div class="row pinsectio">
                                            <div class="col-sm-12">
                                                <label for="Capacity">
                                                    <strong>NB:-</strong> <span>For further details… for land availabity in IDCO landbank
                                                    and IDCO industrial estate please visit</span> <a href="http://gis.investodisha.org"
                                                        target="_blank">"http://gis.investodisha.org"</a></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="DivNB2" runat="server">
                                        <div class="row pinsectio">
                                            <div class="col-sm-12">
                                                <label for="Note">
                                                    <span style="font-weight: 600; font-size: 13px; color: #ed3237; font-style: italic;">NB:- Investors cannot change the application form if the Land requirement from the
                                                    government option is "No" and the user submits the application successfully after
                                                    the payment is done.</span>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec">
                                <div class="form-header">
                                    <h2 class="m-t-0 m-b-0">13.Power requirement during production</h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group row pinsection">
                                        <div class="col-md-2 col-sm-4">
                                            <label for="Capacity">
                                                Source of supply</label>
                                        </div>
                                        <div class="col-md-1 col-sm-2">
                                            <asp:CheckBox ID="chkGr" TabIndex="10" Text="Grid" runat="server" />
                                        </div>
                                        <div class="col-md-1 col-sm-2">
                                            <asp:CheckBox ID="chkCP" TabIndex="11" Text="CPP" runat="server" />
                                        </div>
                                        <div class="col-md-1 col-sm-2">
                                            <asp:CheckBox ID="chkIP" TabIndex="12" Text="IPP" runat="server" />
                                        </div>


                                    </div>
                                    <div class="form-group row m-b-0">
                                        <div class="col-md-4 col-sm-6" id="divLDGRID">
                                            <label for="Capacity">
                                                Power demand from GRID (in KW)<span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtLoadGrid" CssClass="form-control" runat="server" MaxLength="8"
                                                TabIndex="13" onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Specify the Power demand from GRID (in KW)">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                        <span class="fromdiv" id="divCPP">
                                            <div class="col-md-4 col-sm-6" id="">
                                                <label for="Capacity">
                                                    Power drawal from CPP (in KW)<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtPowerDrawalCPP" CssClass="form-control" runat="server" MaxLength="8"
                                                    onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                    TabIndex="14"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Specify the Power drawal from CPP (in KW)">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-md-4 col-sm-6">
                                                <label for="Capacity">
                                                    Capacity of the CPP plant (in KW)<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtCapacityCPP" CssClass="form-control" runat="server" MaxLength="8"
                                                    onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                    TabIndex="15"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Specify the Capacity of the CPP plant (in KW)">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                        </span>

                                        <div class="col-md-4 col-sm-6" id="divIPP">
                                            <label for="Capacity">
                                                Independent Power Producer (in KW)<span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtPowerProducerIpp" CssClass="form-control" runat="server" MaxLength="8"
                                                TabIndex="16" onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Specify the Power demand for IPP (in KW)">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>


                                    </div>
                                </div>
                            </div>
                            <div class="form-sec">
                                <div class="form-header">
                                    <h2 class="m-t-0 m-b-0">14.Water requirement</h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group">
                                        <div class="row pinsection">
                                            <div class="col-md-3 col-sm-4">
                                                <label for="Supply">
                                                    Sources of water for production</label>
                                            </div>
                                            <div class="col-sm-2 ">
                                                <asp:CheckBox ID="chkSurfacedWtr" runat="server" Text="Surface water" TabIndex="17"></asp:CheckBox>
                                            </div>
                                            <div class="col-sm-2 ">
                                                <asp:CheckBox ID="chkIdco" runat="server" Text="IDCO supply" TabIndex="18"></asp:CheckBox>
                                            </div>
                                            <div class="col-sm-2 ">
                                                <asp:CheckBox ID="chkRainWtr" runat="server" Text="Rain water harvesting" TabIndex="19"></asp:CheckBox>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chkOther" runat="server" Text="Others" TabIndex="20"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row pinsection">
                                        <div id="DvOther">
                                            <label for="Supply" class="col-sm-4 col-md-3">
                                                Other (Please specify)<span class="text-red">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtOtherSpecify" CssClass="form-control" runat="server" MaxLength="8"
                                                    TabIndex="21" Onkeypress="return inputLimiter(event,'AlphanumericSpccharc')"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row pinsection">
                                            <div class="col-sm-12">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th></th>
                                                        <th align="center">Existing
                                                        </th>
                                                        <th align="center">Proposed
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label for="Supply">
                                                                Total Water requirement (in cusec)</label>
                                                            <span class="text-red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtWaterRequirExist" CssClass="form-control" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                runat="server" MaxLength="8" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                                onblur="isNumberBlur(event, this, 2);" TabIndex="22"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Specify the existing water requirements">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtWaterRequirProposed" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                CssClass="form-control" runat="server" MaxLength="8" TabIndex="23" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                                onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Specify the proposed water requirements">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row pinsection">
                                            <label for="Supply" class="col-sm-4 col-md-3">
                                                Water required for production (in cusec)</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtWaterRequirProduction" Onkeypress="return inputLimiter(event,'Decimal')"
                                                    CssClass="form-control" runat="server" TabIndex="24" MaxLength="8" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                    onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Specify the water requirements for production">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec">
                                <div class="form-header">
                                    <h2 class="m-t-0 m-b-0">15.Waste Water Management</h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group">
                                        <div class="row pinsection">
                                            <label for="Supply" class="col-sm-4">
                                                Quantum of recycling of waste water (in cusec)</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtQuantumRecylling" CssClass="form-control" MaxLength="8" runat="server"
                                                    TabIndex="25" Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                    onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Specify the Quantum of recycling of waste water (in cusec)">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row pinsection">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-4 col-sm-12">
                                                        <label for="Supply">
                                                            Waste conservation measures</label>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpldWaterCon" CssClass="form-control" runat="server" TabIndex="26"
                                                                ToolTip="Upload relevant document for Water conservation measures taken, within 4 MB" />
                                                            <asp:HiddenField ID="hdnWaterFile" runat="server" />                                                           
                                                            <asp:LinkButton ID="lnkWaterCon" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkWaterCon_Click">
                                                   <i class="fa fa-upload" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelWaterCon" CssClass="input-group-addon bg-red" runat="server"
                                                                OnClick="lnkDelWaterCon_Click">
                                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                            <asp:HyperLink ID="hlDoc1" CssClass="input-group-addon bg-blue" Visible="false" runat="server"
                                                                Target="_blank">
                                                                <i class="fa  fa-download"></i>
                                                            </asp:HyperLink>
                                                        </div>
                                                        <asp:Label ID="lblWaterCon" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Water conservation uploaded successfully."></asp:Label>
                                                        <asp:HiddenField ID="hdn1" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkWaterCon" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <label for="Supply" class="col-md-8 col-sm-12">
                                                        Waste water treatment technology and management of solid/hazardous waste</label>
                                                    <div class="col-md-4 col-sm-12">
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpldHazardous" CssClass="form-control" runat="server" TabIndex="27"
                                                                ToolTip="Upload relevant document on this adopted, within 4 MB" />
                                                            <asp:HiddenField ID="hdnHazardousFile" runat="server" />
                                                            <asp:LinkButton ID="lnkHazardousFile" CssClass="input-group-addon bg-green" runat="server"
                                                                OnClick="lnkHazardousFile_Click"> 
                                                   <i class="fa fa-upload" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelHazardousFile" CssClass="input-group-addon bg-red" runat="server"
                                                                OnClick="lnkDelHazardousFile_Click">
                                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                            <asp:HyperLink ID="hlDoc2" CssClass="input-group-addon bg-blue" Visible="false" runat="server"
                                                                Target="_blank">
                                                               <i class="fa  fa-download"></i>
                                                            </asp:HyperLink>
                                                        </div>
                                                        <asp:Label ID="lblHazardousFile" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Waste water treatment technology and management of solid/hazardous waste uploaded successfully."></asp:Label>
                                                        <asp:HiddenField ID="hdn2" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkHazardousFile" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%--<div class="col-sm-4" id="DvFilAdoption">
                                <label for="Supply">
                                    Adoption of water Conservation system details report</label>
                                <asp:FileUpload ID="FileUpload3" CssClass="form-control" runat="server" />
                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec">
                                <div class="form-footer">
                                    <div class="row pinsection">
                                        <div class="col-sm-12" align="center">
                                            <asp:Button ID="btnBack" TabIndex="28" runat="server" Text="Back" CssClass="btn btn-warning" OnClick="btnBack_Click" />
                                            <asp:Button ID="btnNext" TabIndex="29" runat="server" Text="Next" CssClass="btn btn-success" OnClick="btnNext_Click"
                                                OnClientClick="return validation();" />
                                            <asp:Button ID="btnSave" TabIndex="30" runat="server" Text="Save As Draft" CssClass=" btn btn-primary draftbtn noprint"
                                                OnClick="btnSave_Click" />
                                            <asp:Button ID="btnReset" TabIndex="31" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                            <asp:HiddenField ID="hdnAllFileValue" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <uc3:footer ID="footer" runat="server" />
    </form>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#printbtn").click(function () {
                window.print();
            });

            $('.backcheck3').hide();


            var sessioncal = '<%=Session["proposalno"] %>';
            var Qrvalue = '<%= Request.QueryString["StrPropNo"] %>';
            // alert(Qrvalue);
            // alert(sessioncal);
            if ((sessioncal != null) && (sessioncal != "")) {
                // alert('hiii');
                $('.wizard2 .nav-tabs > li').addClass("backactive");


            }
            else if ((Qrvalue != null) && (Qrvalue != "")) {
                $('.wizard2 .nav-tabs > li').addClass("backactive");

            }
            else {

                $('.wizard2 .nav-tabs > li').removeClass("backactive");

            }

        });



        function pageLoad(sender, args) {

            if ('<%=Session["AllFileValue"] %>' != "") {
                $('#hdnAllFileValue').val('<%=Session["AllFileValue"] %>');
            }
            var allFileVal = $("#hdnAllFileValue").val();



            $('#DVNewlayout').hide();
            $("#FileUpldWaterCon").change(function (e) {
                var filename = $("#FileUpldWaterCon").val().replace(/C:\\fakepath\\/i, '');
                var fileExtension = ['jpeg', 'jpg', 'png', 'pdf'];
                var yourValues = $("#hdnAllFileValue").val();
                var array = yourValues.split(",");
                var isValue = 0;
                for (i in array) {
                    if (array[i] == filename) {
                        isValue = 1;
                    }
                }
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    jAlert('<strong>Please Upload  PDF,PNG,JPG,JPEG file Only!', projname);
                    $("#FileUpldWaterCon").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUpldWaterCon").val('');
                        return false;
                    }
                }

            });
            $('.backcheck3').hide();

            $("#FileUpldHazardous").change(function (e) {
                var filename = $("#FileUpldHazardous").val().replace(/C:\\fakepath\\/i, '');
                var fileExtension = ['jpeg', 'jpg', 'png', 'pdf'];
                var yourValues = $("#hdnAllFileValue").val();
                var array = yourValues.split(",");
                var isValue = 0;
                for (i in array) {
                    if (array[i] == filename) {
                        isValue = 1;
                    }
                }
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    jAlert('<strong>Please Upload  PDF,PNG,JPG,JPEG file Only!', projname);
                    $("#FileUpldHazardous").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUpldHazardous").val('');
                        return false;
                    }
                }

            });

            $("#upldProjectlandStatement").change(function (e) {
                var filename = $("#upldProjectlandStatement").val().replace(/C:\\fakepath\\/i, '');
                var fileExtension = ['jpeg', 'jpg', 'png', 'pdf'];
                var yourValues = $("#hdnAllFileValue").val();
                var array = yourValues.split(",");
                var isValue = 0;
                for (i in array) {
                    if (array[i] == filename) {
                        isValue = 1;
                    }
                }
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    jAlert('<strong>Please Upload  PDF,PNG,JPG,JPEG file Only!', projname);
                    $("#upldProjectlandStatement").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#upldProjectlandStatement").val('');
                        return false;
                    }
                }

            });
            $("#upldProjectLaoutPlan").change(function (e) {
                var filename = $("#upldProjectLaoutPlan").val().replace(/C:\\fakepath\\/i, '');
                var fileExtension = ['jpeg', 'jpg', 'png', 'pdf'];
                var yourValues = $("#hdnAllFileValue").val();
                var array = yourValues.split(",");
                var isValue = 0;
                for (i in array) {
                    if (array[i] == filename) {
                        isValue = 1;
                    }
                }
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    jAlert('<strong>Please Upload  PDF,PNG,JPG,JPEG file Only!', projname);
                    $("#upldProjectLaoutPlan").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#upldProjectLaoutPlan").val('');
                        return false;
                    }
                }

            });

            //            $('#dvD').hide();
            //            $('#dvC').hide();
            //            $('#dvB').hide();
            //            $('#dvA').hide();
            if ('<%=Session["ApplicationFor"] %>' == "1") {
                document.getElementById("txtWaterRequirExist").readOnly = true;
            }
            else {
                document.getElementById("txtWaterRequirExist").readOnly = false;
            }

            if ('<%=Session["ProjectCategory"] %>' == "1") {
                $('#DVNewlayout').hide();
            }
            else {
                $('#DVNewlayout').show();
            }

            $('#DvFilAdoption').hide();
            $('#divLDGRID').hide();
            $('#divLD').hide();
            $('#divLD2').hide();
            $('#divCPP').hide();
            $('#divIPP').hide();
            $('#DvOther').hide();

            /*-----------------------------------------------------*/

            if (($('#ddlrequired').val() == "1")) {
                $("#divLD").show();
                $("#divLD2").hide();
            }
            else if (($('#ddlrequired').val() == "2")) {
                $("#divLD").hide();
                $("#divLD2").show();
            }

            /*-----------------------------------------------------*/

            if (document.getElementById("chkOther").checked) {
                $("#DvOther").show();
            }
            else {
                $("#DvOther").hide();
            }

            if (document.getElementById("chkGr").checked) {
                $('#divLDGRID').show();
            }
            else {
                $('#divLDGRID').hide();
            }

            if (document.getElementById("chkCP").checked) {
                $('#divCPP').show();
            }
            else {
                $('#divCPP').hide();
            }

            if (document.getElementById("chkIP").checked) {
                $('#divIPP').show();
            }
            else {
                $('#divIPP').hide();
            }


            //            if ($('#rdbExIndustry').val() == "1") {
            //                $('#dvD').show();
            //                $('#dvC').show();
            //                $('#dvB').show();
            //                $('#dvA').show();   
            //            }
            //            else {

            //                $('#dvD').hide();
            //                $('#dvC').hide();
            //                $('#dvB').hide();
            //                $('#dvA').hide();
            //            }


            $('#chkOther').click(function () {
                if (document.getElementById("chkOther").checked) {
                    $('#DvOther').show();
                }
                else {
                    $('#DvOther').hide();
                }
            });
            $('#chkGr').click(function () {
                if (document.getElementById("chkGr").checked) {
                    $('#divLDGRID').show();
                }
                else {
                    $('#divLDGRID').hide();
                }
            });
            $('#chkCP').click(function () {
                if (document.getElementById("chkCP").checked) {
                    $('#divCPP').show();
                }
                else {
                    $('#divCPP').hide();
                }
            });
            $('#chkIP').click(function () {
                if (document.getElementById("chkIP").checked) {
                    $('#divIPP').show();
                }
                else {
                    $('#divIPP').hide();
                }
            });
            $('input[name="rdnAdoption"]').change(function () {
                if (($(this).val() == "1")) {
                    $('#DvFilAdoption').show();

                }
                else {
                    $('#DvFilAdoption').hide();
                }
            });

            /*-----------------------------------------------------------*/

            $('#ddlrequired').change(function () {
                if (($(this).val() == "1")) {
                    $("#divLD").show();
                    $("#divLD2").hide();
                    $("#ddlIDCOName").val('0');
                }
                else {
                    $("#divLD").hide();
                    $("#divLD2").show();
                    $("#ddlLandacquiredIDCO").val('0');
                }
            });

            /*-----------------------------------------------------------*/

            $('#drpMrs').change(function () {
                if (($(this).val() == "2")) {
                    $("#lblIdco").html('which');
                }
                else {
                    $("#lblIdco").html('IDCO');
                }
            });

            /*-----------------------------------------------------------*/

            $("#divind").hide();
            $("#divLB").hide();
            $("#divLAdir").hide();
            $("#divLA").hide();
            $('.menuproposal').addClass('active');
            $('#ddllandinidco').change(function () {

                if (($(this).val() == "2")) {
                    $("#divind").show();
                }
                else {
                    $("#divind").hide();
                }
            });

            $('#ddlLabdbank').change(function () {
                if (($(this).val() == "2")) {
                    $("#divLB").show();
                }
                else {
                    $("#divLB").hide();
                }
            });

            $('#ddlLandacquired').change(function () {

                if (($(this).val() == "2")) {
                    $("#divLA").show();
                }
                else {
                    $("#divLA").hide();
                }
            });

            $('#ddlLandacquireddir').change(function () {

                if (($(this).val() == "2")) {
                    $("#divLAdir").show();
                }
                else {
                    $("#divLAdir").hide();
                }
            });

            $('input[name="rdbApplication"]').change(function () {
                if (($(this).val() == "0")) {
                    $("#divExistunit").hide();
                }
                else {
                    $("#divExistunit").show();
                }
            });

            if ('<%=Session["LandRequired"] %>' == "0") {
                $("#rdbLand_0").attr('checked', 'checked');
                $("#DivA").show();
            }
            else {
                $("#rdbLand_1").attr('checked', 'checked');
                $("#DivA").hide();
            }

            if ('<%=Session["ApplicationFor"] %>' == "0") {
                $("#rdbApplication_0").attr('checked', 'checked');
                $("#divExistunit").hide();
            }
            else {
                $("#rdbApplication_1").attr('checked', 'checked');
                $("#divExistunit").show();
            }

            $('#rdbLand_0').attr('disabled', true);
            $('#rdbLand_1').attr('disabled', true);
            $('#rdbApplication_0').attr('disabled', true);
            $('#rdbApplication_1').attr('disabled', true);
            //            $('#txtExtent').tooltip({
            //                html: true,
            //                placement: 'bottom',
            //                title: '<p align="left">Extent of land required</p>'
            //            });
            //            $('#txtLoadGrid').tooltip({
            //                html: true,
            //                placement: 'bottom',
            //                title: '<p align="left">Power demand from grid</p>'
            //            });
            //            $('#txtPowerDrawalCPP').tooltip({
            //                html: true,
            //                placement: 'bottom',
            //                title: '<p align="left">Power Drawal from CPP </p>'
            //            });
            //            $('#txtCapacityCPP').tooltip({
            //                html: true,
            //                placement: 'bottom',
            //                title: '<p align="left">capacity of the CPP plant</p>'
            //            });
            //            $('#txtWaterRequirExist').tooltip({
            //                html: true,
            //                placement: 'bottom',
            //                title: '<p align="left">Water requirement Existing</p>'
            //            });
            //            $('#txtWaterRequirProposed').tooltip({
            //                html: true,
            //                placement: 'bottom',
            //                title: '<p align="left">Water requirement proposed</p>'
            //            });
            //            $('#txtWaterRequirProduction').tooltip({
            //                html: true,
            //                placement: 'bottom',
            //                title: '<p align="left">Water required for production</p>'
            //            });
            //            $('#txtOtherSpecify').tooltip({
            //                html: true,
            //                placement: 'bottom',
            //                title: '<p align="left">Other (Please specify)</p>'
            //            });
            //            $('#txtQuantumRecylling').tooltip({
            //                html: true,
            //                placement: 'bottom',
            //                title: '<p align="left">Quantum of recycling of waste water</p>'
            //            });

            $('#txtWaterRequirProduction').change(function () {
                var a = $('#txtWaterRequirExist').val();
                var b = $('#txtWaterRequirProposed').val();
                if (a == "") {
                    a = 0;
                }
                if (b == "") {
                    b = 0;
                }
                var result = parseFloat(a) + parseFloat(b);
                if (parseFloat($(this).val()) > result) {
                    jAlert('<strong>Water required for production (in cusec) should not be greater than existing and proposed water.</strong>', projname);
                    $('#txtWaterRequirProduction').val('');
                    return false;
                }
            });

            $('#txtCapacityCPP').change(function () {
                var a = $('#txtCapacityCPP').val();
                var b = $('#txtPowerDrawalCPP').val();
                if (a != '0.00') {
                    if (parseFloat(b) > parseFloat(a)) {
                        jAlert('<strong>Power drawal from CPP (in KW) should not be greater than Capacity of the CPP plant (in KW)</strong>', projname);
                        $('#txtCapacityCPP').val('0.00');
                        return false;

                    }
                }
            });

            $('#txtPowerDrawalCPP').change(function () {
                var a = $('#txtCapacityCPP').val();
                var b = $('#txtPowerDrawalCPP').val();
                if (a != '0.00') {
                    if (parseFloat(b) > parseFloat(a)) {
                        jAlert('<strong>Power drawal from CPP (in KW) should not be greater than Capacity of the CPP plant (in KW)</strong>', projname);
                        $('#txtPowerDrawalCPP').val('0.00');
                        return false;

                    }
                }
            });
        }

        function inputLimiter(e, allow) {

            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'AlphanumericSpccharc') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"<>=';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._';
            }
            if (allow == 'Address') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#-,./;\'';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
            }
            var k;
            k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
            if (k != 13 && k != 8 && k != 0) {
                if ((e.ctrlKey == false) && (e.altKey == false)) {
                    return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }
        }


        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        function validation() {
            //            if ((sessioncal != null) && (sessioncal != "")) {
            //                // alert('hiii');

            //                $('.backcheck2').show();

            //            }
            //            else if ((Qrvalue != null) && (Qrvalue != "")) {

            //                $('.backcheck2').show();
            //            }
            //            else {
            //                $('.wizard2 .nav-tabs > li').removeClass("backactive");
            //                $('.backcheck2').hide();

            //            }
            if (DropDownValidation('ddlDistrict', '0', 'District', projname) == false) {
                return false;
            }
            if (DropDownValidation('ddlBlock', '0', 'Block ', projname) == false) {
                return false;
            }
            if (blankFieldValidation('txtExtent', 'Extent of land required (in acre)', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidation1st('txtExtent', 'Extent of land required (in acre)', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('txtExtent', 'Extent of land required (in acre)', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('txtExtent', 'Extent of land required (in acre)', projname) == false) {
                return false;
            }
            var radioValue = $("input[name='rdbExIndustry']:checked").val();
            if (radioValue == 1) {

                if (DropDownValidation('ddlrequired', '0', 'Whether land is required in IDCO industrial estate ', projname) == false) {
                    return false;
                }
                if ($('#ddlrequired').val() == 1) {
                    if (DropDownValidation('ddlIDCOName', '0', 'Name of the IDCO industrial estate', projname) == false) {
                        return false;
                    }
                }
                if ($('#ddlrequired').val() == 2) {
                    if (DropDownValidation('ddlLandacquiredIDCO', '0', 'whether land to be acquired by IDCO', projname) == false) {
                        return false;
                    }
                }
            }
            if (radioValue == 1) {
                if ('<%=Session["ProjectCategory"] %>' == "2") {

                    if ($('#hdnProjectlandStatement').val() == '') {
                        jAlert('<strong>Please upload Project Land Use Statement file</strong>', projname);
                        $("#upldProjectlandStatement").focus();
                        return false;
                    }
                    if ($('#hdnProjectLaoutPlan').val() == '') {
                        jAlert('<strong>Please upload Project Layout Plan file</strong>', projname);
                        $("#upldProjectLaoutPlan").focus();
                        return false;
                    }
                }
            }

            if (document.getElementById("chkGr").checked == true) {
                if (blankFieldValidation('txtLoadGrid', 'Power demand from GRID ', projname) == false) {
                    return false;
                }


                if (WhiteSpaceValidation1st('txtLoadGrid', 'Power demand from GRID ', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtLoadGrid', 'Power demand from GRID ', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtLoadGrid', 'Power demand from GRID ', projname) == false) {
                    return false;
                }
            }
            if (document.getElementById("chkCP").checked == true) {

                if (blankFieldValidation('txtPowerDrawalCPP', 'Power drawal from CPP (in KW)', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtPowerDrawalCPP', 'Power drawal from CPP (in KW)', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtPowerDrawalCPP', 'Power drawal from CPP (in KW)', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtPowerDrawalCPP', 'Power drawal from CPP (in KW)', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtCapacityCPP', 'Capacity of the CPP plant (in KW)', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtCapacityCPP', 'Capacity of the CPP plant (in KW)', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtCapacityCPP', 'Capacity of the CPP plant (in KW)', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtCapacityCPP', 'Capacity of the CPP plant (in KW)', projname) == false) {
                    return false;
                }
            }

            if (WhiteSpaceValidation1st('txtWaterRequirExist', 'water Requirement Exist', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('txtWaterRequirExist', 'water Requirement Exist', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('txtWaterRequirExist', 'water Requirement Exist', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidation1st('txtWaterRequirProposed', 'Water requirement proposed', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('txtWaterRequirProposed', 'Water requirement proposed', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('txtWaterRequirProposed', 'Water requirement proposed', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidation1st('txtWaterRequirProduction', 'Water required for production(in cusec)', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('txtWaterRequirProduction', 'Sea water Existing', projname) == false) {
                return false;
            }

            if (WhiteSpaceValidation1st('txtQuantumRecylling', 'Quantum of recycling of waste water', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('txtQuantumRecylling', 'Quantum of recycling of waste water', projname) == false) {
                return false;
            }
            if (document.getElementById("chkOther").checked == true) {

                if (blankFieldValidation('txtOtherSpecify', 'Other (Please specify)', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtOtherSpecify', 'Other (Please specify)', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtOtherSpecify', 'Other (Please specify)', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtOtherSpecify', 'Other (Please specify)', projname) == false) {
                    return false;
                }
            }
            if ('<%=Session["ApplicationFor"] %>' == "1") {//1 for New Unit 2 for Existing Unit
                if (blankFieldValidation('txtWaterRequirProposed', 'Water requirement proposed', projname) == false) {
                    return false;
                }


            }
            if ('<%=Session["ApplicationFor"] %>' == "2") {//1 for New Unit 2 for Existing Unit

                if (blankFieldValidation('txtWaterRequirExist', 'water Requirement Exist', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtWaterRequirProposed', 'Water requirement proposed', projname) == false) {
                    return false;
                }

            }
        }

    </script>
</body>
</html>
