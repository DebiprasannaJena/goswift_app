<%--'*******************************************************************************************************************
' File Name         : PromoterDetails.aspx
' Description       : Details of Promoter
' Created by        : Subhasmita Behera
' Created On        : 01 July 2017
' Modification History:
' "VERION= v2"
'           <CR no.>                 <Date>             <Modified by>        <Modification Summary>                      <Instructed By>       
               1                   27-Aug-2019         Sushant Jena        Group of Company Net Worth Details Added    Ramarao Teki
      
'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PromoterDetails.aspx.cs"
    Inherits="Promoter_Details" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/pealwebfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/decimalrstr.js" type="text/javascript"></script>
    <script src="../js/jspdf.min.js" type="text/javascript"></script>
    <style>
        .input-group a.input-group-addon
        {
            background: #fff !important;
        }
        .hdnm
        {
            display: none !important;
        }
        .backcheck
        {
            display: none;
        }
        @media print
        {
            .noprint
            {
                display: none;
            }
            .wizard-inner
            {
                display: none;
            }
            .nav nav-tabs
            {
                display: none;
            }
            .rightpnl-btn
            {
                display: none;
            }
            .rightpnl-btn
            {
                display: none;
            }
            .navbar-brand
            {
                float: left;
            }
            #btnBack
            {
                display: none;
            }
            #btnNext
            {
                display: none;
            }
            #btnSaveAsdraft
            {
                display: none;
            }
            .pinsection
            {
                margin-left: 15px;
            }
            #btnReset
            {
                display: none;
            }
        
            #btnSave
            {
                display: none;
            }
            .header-investorDetails
            {
                display: none;
            }
            .investrs-tab
            {
                display: none;
            }
            .col-sm-4
            {
                width: 32.2%;
                float: left;
                padding: 0px;
                margin-right: 7px;
            }
            .upladdocs .col-sm-4
            {
                width: 98%;
                float: left;
                padding: 0px;
                margin-right: 7px;
            }
            .form-control
            {
                height: 26px;
                padding: 0px 3px;
                font-size: 12px;
            }
            textarea.form-control
            {
                height: 74px !important;
            }
            .form-body .form-group
            {
                margin-bottom: 5px;
            }
            .row-xs
            {
                margin-left: 10px;
                margin-right: 10px;
            }
            .form-header
            {
                background-color: #efefef;
            }
            label
            {
                font-weight: 400;
            }
            .iconsdiv, .footer-wrapper
            {
                display: none;
            }
            .form-group
            {
                margin-bottom: 8px;
            }
            header
            {
                border-bottom: 1px solid #ccc;
            }
            .form-header
            {
                padding: 0px;
                border-bottom: 0px;
            }
            .form-body
            {
                padding: 10px 0px;
            }
            .form-sec h2
            {
                font-weight: 400;
                color: #000;
                border-bottom: 0px;
                background: #ccc;
            }
            .form-sec
            {
                margin-bottom: 10px;
            }
            .col-sm-3
            {
                width: 25%;
                float: left;
            }
            .col-sm-6
            {
                width: 50%;
                float: left;
            }
            .collapse, collapsed
            {
                display: block;
                height: auto !important;
            }
            .phnum2
            {
                width: 50%;
            }
            .input-group-addon
            {
                padding: 3px 12px;
            }
            .more-less
            {
                display: none;
            }
            .navbar-toggle
            {
                display: none;
            }
            .scrollup
            {
                display: none;
            }
            .panel-title
            {
                font-weight: 400;
            }
            .panel-body .h4-header, .table > tbody > tr > th
            {
                font-weight: 400;
            }
            label
            {
                margin: 2px 0px;
            }
            a[href]:after
            {
                content: none !important;
            }
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
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
                    <%--<a href="javascript:void(0);" title="Print" id="conertpdf" class="pull-right printbtn">
                        <i class="fa fa-file-pdf-o"></i></a>--%>
                </div>
                <uc4:pealmenu ID="pealmenu" runat="server" />
            </div>
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                    <div class="" id="employeelistDiv" runat="server">
                        <div class="wizard wizard2">
                            <div class="wizard-inner">
                                <div class="connecting-line">
                                </div>
                                <ul class="nav nav-tabs" role="tablist">
                                    <li role="presentation" class="active"><a href="PromoterDetails.aspx" data-toggle="tooltip"
                                        data-placement="top" aria-controls="Company Information" role="tab" title="Company Information">
                                        <span class="round-tab"><i class="glyphicon glyphicon-home"></i></span><small><i
                                            class="fa fa-check text-success backcheck" aria-hidden="true"></i>&nbsp;<b>1.</b>
                                            Company Information</small> </a></li>
                                    <li role="presentation" class="disabled"><a href="javascript:void(0)" data-toggle="tooltip"
                                        data-placement="top" aria-controls="Project Information" role="tab" title="Project Information">
                                        <span class="round-tab"><i class="glyphicon glyphicon-pencil"></i></span><small><b>2.</b>
                                            Project Information</small> </a></li>
                                    <li role="presentation" class="disabled"><a href="javascript:void(0)" data-toggle="tooltip"
                                        data-placement="top" aria-controls="Land and Utility Requirment" role="tab" title="Land and Utility Requirment">
                                        <span class="round-tab"><i class="glyphicon glyphicon-picture"></i></span><small><b>
                                            3.</b> Land and Utility Requirement</small> </a></li>
                                    <li role="presentation" class="disabled"><a href="#complete" data-toggle="tooltip"
                                        data-placement="top" aria-controls="Declaration" role="tab" title="Declaration">
                                        <span class="round-tab"><i class="glyphicon glyphicon-ok"></i></span><small><b>4.</b>
                                        Declaration</a></small> </a></li>
                                </ul>
                            </div>
                            <div class="form-sec">
                                <h1 class="headerpeal">
                                    Project Evaluation including Allotment of Land</h1>
                                <div class="form-header">
                                    <span class="pull-right"><span class="mandatoryspan ">(*) </span>Mark Fields Are Mandatory</span>
                                    <h2>
                                        1.Company Information</h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group row row-xs m-b-0">
                                        <label for="Address2" class="col-md-3 col-sm-4 col-form-label">
                                            <asp:HiddenField ID="vers" Value="v1" runat="server" />
                                            Name of the Company/Enterprise <span class="text-red">*</span>
                                        </label>
                                        <div class="col-md-4 col-sm-8">
                                            <asp:TextBox ID="TextBox1" CssClass="form-control phcode" runat="server" ReadOnly="true"
                                                Text="M/s" ></asp:TextBox>
                                            <asp:TextBox CssClass="form-control phnum" ID="txtIName" MaxLength="100" runat="server"
                                                TabIndex="1"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                TargetControlID="txtIName" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                ValidChars=" ">
                                            </cc1:FilteredTextBoxExtender>
                                            <a data-toggle="tooltip" class="fieldinfo" title="It can accept all characters"><i
                                                class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="sbuhdr">
                                                    Corporate Office Address</h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-3 col-sm-12">
                                                <label for="Address1">
                                                    Address<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtAddress" TabIndex="2" onkeyup="setvalue();" TextMode="MultiLine"
                                                    Onkeypress="return inputLimiter(event,'Address')" MaxLength="250" CssClass="form-control"
                                                    Height="104px" runat="server"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="The applicant will fill details of the main office/headquarters (i.e. where the executives of the company, including the CEO, maintain their offices and is the central location where top decisions are made). It can accept all characters">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><i>Maximum <span
                                                        id="SpanLbl" class="mandatoryspan">250</span> characters left</i></small>
                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <div class="form-group row">
                                                    <div class="col-md-4 col-sm-4">
                                                        <label for="Country">
                                                            Country<span class="text-red">*</span></label>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlCountry" runat="server" TabIndex="3"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                            <asp:ListItem Value="0" Selected="True">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Country can be selected from the drop-down menu">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4" id="st1" runat="server">
                                                        <label for="State">
                                                            State<span class="text-red">*</span></label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="4" ID="ddlState" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="State can be selected from the drop-down menu">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4" id="st2" runat="server">
                                                        <label for="State">
                                                            State<span class="text-red">*</span></label>
                                                        <asp:TextBox CssClass="form-control" TabIndex="5" ID="txtState" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="100" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Only alphabets  and space allowed.">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4">
                                                        <label for="Address2">
                                                            City<span class="text-red">*</span></label>
                                                        <asp:TextBox CssClass="form-control" TabIndex="6" ID="txtCity" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="50" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Only alphabets and space allowed.">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-4">
                                                        <label for="PhoneNo">
                                                            Phone Number</label>
                                                        <div class="clearfix">
                                                        </div>
                                                        <asp:DropDownList ID="ddlCode" TabIndex="7" runat="server" CssClass="form-control phcode"
                                                            Width="65px">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtPhoneNoStateCodedet" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="6" TabIndex="8" CssClass="form-control phnum" Width="50px" Style="margin-right: 2px;"></asp:TextBox>
                                                        <asp:TextBox ID="txtPhoneNo" TabIndex="9" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="10" CssClass="form-control phnum2" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Official Phone Number. In case it is a landline number, it should only be numbers, with area code (2-4 digits) and local number (6-8 digits) in separate boxes, and no special characters will be allowed. In case it is a mobile number, it should only be numbers, with a minimum length of 10, and no special characters will be allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="FaxNo">
                                                            Fax Number</label>
                                                        <div class="clearfix">
                                                        </div>
                                                        <asp:DropDownList ID="drpFx" TabIndex="10" runat="server" CssClass="form-control phcode">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtFaxNo" TabIndex="11" CssClass="form-control phnum" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="10" runat="server"></asp:TextBox>
                                                         <a data-toggle="tooltip" class="fieldinfo" title="Official Fax Number. It should only be numbers, with area code (2-4 digits) and local number (6-8 digits) in separate boxes, and no special characters will be allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><i>Specify with STD
                                                                Code, Example: 0674256123</i></small>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="Email">
                                                            Email Address<span class="text-red">*</span></label>
                                                        <asp:TextBox ID="txtEmail" TabIndex="12" CssClass="form-control" Onkeypress="return inputLimiter(event,'Email')"
                                                            MaxLength="50" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="It can accept both alphabets and numbers. Special Characters like '@', '-', '_', are allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row pinsection">
                                        <div class="col-md-3  col-sm-4">
                                            <label for="PINcode" class=" col-form-label">
                                                PIN Code<span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtPinCode" TabIndex="13" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                MaxLength="6" runat="server"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Only Numbers are accepted and it should not start with zero. No special characters will be allowed">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec">
                                <div class="form-header">
                                    Correspondence Address &nbsp;<label><asp:CheckBox TabIndex="14" ID="chkBoxAddress"
                                        AutoPostBack="true" CssClass="padding-left-20 " runat="server" Text="Address same as corporate address"
                                        OnCheckedChanged="chkBoxAddress_CheckedChanged"></asp:CheckBox>
                                    </label>
                                </div>
                                <div class="form-body">
                                    <div class="form-group row row-xs">
                                        <label for="Address2" class="col-md-3 col-sm-4 col-form-label">
                                            Name of the Contact Person <span class="text-red">*</span>
                                        </label>
                                        <div class="col-md-3 col-sm-6">
                                            <asp:TextBox CssClass="form-control pull-left phnum" TabIndex="15" ID="txtCperson"
                                                Onkeypress="return inputLimiter(event,'NameCharacters')" MaxLength="100" runat="server"></asp:TextBox>
                                            <%--     <a data-toggle="tooltip" class="fieldinfo" title="It will accept only alphabets and spaces, no special characters are allowed">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>--%>
                                            <%--<a data-toggle="tooltip" class="fieldinfo" title="The applicant will fill details of the office that will be directly involved in executing the project. It can accept all characters">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-3 col-sm-12">
                                                <label for="Address1">
                                                    Address<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtCorAddrs" onkeyup="setvalue1();" TabIndex="16" TextMode="MultiLine"
                                                    CssClass="form-control" Onkeypress="return inputLimiter(event,'Address')" MaxLength="250"
                                                    Height="104px" runat="server"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="The applicant will fill details of the office that will be directly involved in executing the project. It can accept all characters">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><i>Maximum <span
                                                        id="SpanLbl1" class="mandatoryspan">250</span> characters left</i></small>
                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <div class="form-group row">
                                                    <div class="col-sm-4">
                                                        <label for="Country">
                                                            Country<span class="text-red">*</span></label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpCorCountry" runat="server"
                                                            AutoPostBack="true" OnSelectedIndexChanged="drpCorCountry_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Country can be selected from the drop-down menu">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <div class="col-sm-4" runat="server" id="st3">
                                                        <label for="State">
                                                            State<span class="text-red">*</span></label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="18" ID="drpCorState" runat="server">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="State can be selected from the drop-down menu">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <div class="col-sm-4" id="st4" runat="server">
                                                        <label for="State">
                                                            State<span class="text-red">*</span></label>
                                                        <asp:TextBox CssClass="form-control" TabIndex="19" ID="txtCorState" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="100" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Only alphabets  and space allowed.">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <%--  <div class="col-sm-3">
                                            <label for="State">
                                                District<span class="text-red">*</span></label>
                                            <asp:DropDownList CssClass="form-control" ID="drpCorDist" runat="server">
                                                <asp:ListItem Value="0">---Select---</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>
                                                    <div class="col-sm-4">
                                                        <label for="Address2">
                                                            City<span class="text-red">*</span></label>
                                                        <asp:TextBox CssClass="form-control" ID="txtCorCity" TabIndex="20" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="50" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Only alphabets and space allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-4">
                                                        <label for="PhoneNo">
                                                            Mobile Number<span class="text-red">*</span></label>
                                                        <div class="clearfix">
                                                        </div>
                                                        <asp:DropDownList ID="drpMob" TabIndex="21" runat="server" CssClass="form-control phcode">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtCorMob" TabIndex="22" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            onclick="checkLength()" MaxLength="10" CssClass="form-control phnum" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Do not include +91 or start with 0">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="FaxNo">
                                                            Fax Number</label>
                                                        <div class="clearfix">
                                                        </div>
                                                        <asp:DropDownList ID="drpFax2" TabIndex="23" runat="server" CssClass="form-control phcode">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtCorFax" TabIndex="24" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="10" CssClass="form-control phnum" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Official Fax Number. It should only be numbers, with area code (2-4 digits) and local number (6-8 digits) in separate boxes, and no special characters will be allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><i>Specify with STD
                                                                Code, Example: 0674256123</i></small>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="Email">
                                                            Email Address<span class="text-red">*</span></label>
                                                        <asp:TextBox ID="txtCorEmailid" TabIndex="25" Onkeypress="return inputLimiter(event,'Email')"
                                                            MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="It can accept both alphabets and numbers. Special Characters like '@', '-', '_', are allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row pinsection">
                                        <div class="col-md-3 col-sm-4">
                                            <label for="PIN code">
                                                PIN Code<span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtCorPin" TabIndex="26" Onkeypress="return inputLimiter(event,'Numbers')"
                                                MaxLength="6" CssClass="form-control" runat="server"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Only Numbers are accepted and it should not start with zero. No special characters will be allowed">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                        <div class="col-md-3 col-sm-4">
                                            <label for="Iname">
                                                Constitution of Company/Enterprise<span class="text-red">*</span></label>
                                            <asp:DropDownList ID="ddlConstitution" TabIndex="27" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Proprietorship" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Partnership" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Private Limited Company" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Public Limited Company" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="PSU" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="SPV" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="Co-operative" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="Others" Value="8"></asp:ListItem>
                                            </asp:DropDownList>
                                            <a data-toggle="tooltip" class="fieldinfo" title="The applicant will select from the list of dropdown values and the default value shall be blank">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                        <div class="col-sm-3" id="DvOths">
                                            <label for="Iname">
                                                Others (Please specify)<span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtOthrConsti" TabIndex="28" CssClass="form-control" Onkeypress="return inputLimiter(event,'OtherSpecify')"
                                                runat="server"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="In case, in the above dropdown values, “Others” is selected. A text box will appear and the applicant will enter the name. Only alphabets are allowed and the minimum length is 5">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec">
                                <div class="form-header">
                                    <span class="mandatoryspan pull-right" id="spmandFile">(File type allowed is pdf, .jpg,.png
                                        , Max Size 4 MB and for Memorandum & articles of association is 12 MB) </span>
                                    <h2>
                                        2.Entrepreneur Registration Details</h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group" id="dvyearofIncrp">
                                        <div class="row pinsection">
                                            <div class="col-sm-4" id="DVC1">
                                                <label for="Designation">
                                                    Year of Establishment<span class="text-red">*</span></label>
                                                <%--<asp:TextBox ID="txtyrIncorporation" TabIndex="28" Onkeypress="return inputLimiter(event,'Numbers');"
                                                    MaxLength="4" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="drpYearofEstablishment" TabIndex="29" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Should be <= current year"><i class="fa fa-question-circle"
                                                    aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-4" id="DVC2">
                                                <label for="Others">
                                                    Place of incorporation</label>
                                                <asp:TextBox ID="txtPlaceIncorporation" TabIndex="30" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                    MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Place of Registration of the Company. It should be only alphabets and no special characters will be allowed">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="Others">
                                                    GSTIN<%--<span class="text-red">*</span>--%></label><asp:TextBox ID="txtGSTIN" Style="text-transform: capitalize:uppercase;"
                                                        TabIndex="31" Onkeypress="return inputLimiter(event,'GSTINDET')" MaxLength="15"
                                                        CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="form-group row pinsection upladdocs" id="dvPAN">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-4">
                                                        <label for="Others">
                                                            PAN<span class="text-red">*</span></label>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpldPan" TabIndex="32" CssClass="form-control" runat="server" />
                                                            <%--<asp:HiddenField ID="hdn1" runat="server" />--%>
                                                            <asp:HiddenField ID="hdnPanFile" runat="server" />
                                                            <%--<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <asp:LinkButton ID="lnkPan"  runat="server" OnClick="lnkPan_Click" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelPan"  runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkDelPan_Click"> <i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink CssClass="input-group-addon bg-blue" ID="hlDoc1" Visible="false" runat="server"
                                                                Target="_blank">
                                                             <i class="fa  fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <asp:Label ID="lblPAN" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="PAN uploaded successfully."></asp:Label>
                                                        <asp:HiddenField ID="hdPn1" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkPan" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-4">
                                                        <label for="Others">
                                                            GSTIN</label>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpldGstn" TabIndex="35" CssClass="form-control" runat="server" />
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 1 Mb">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnGstinFile" runat="server" />
                                                            <asp:LinkButton ID="lnkGSTIN" runat="server"  OnClick="lnkGSTIN_Click" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelGST"  runat="server" OnClick="lnkDelGST_Click" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i> </asp:LinkButton>
                                                            <asp:HyperLink CssClass="input-group-addon bg-blue" ID="hlDoc2" Visible="false" runat="server"
                                                                Target="_blank">
                                                             <i class="fa  fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <asp:Label ID="lblGSTIN" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="GSTIN uploaded successfully."></asp:Label>
                                                        <asp:HiddenField ID="hdPn2" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkGSTIN" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-4" id="DVC3">
                                                        <label for="Others">
                                                            Upload Memorandum & Articles of Association<span class="text-red">*</span></label>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpldMemo" TabIndex="38" CssClass="form-control" runat="server" />
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnMemoFile" runat="server" />
                                                            <%-- <asp:Button ID="btnUpload2" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <%-- <asp:LinkButton ID="lnkMemo" runat="server"></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="lnkMemo"  runat="server" OnClick="lnkMemo_Click" CssClass="input-group-addon bg-green"> <i class="fa fa-upload " aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelMemo"  runat="server" OnClick="lnkDelMemo_Click" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i> </asp:LinkButton>
                                                            <asp:HyperLink ID="hlDoc3" runat="server" Visible="false" Target="_blank" CssClass="input-group-addon bg-blue">
                                                             <i class="fa  fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <asp:Label ID="lblMemo" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Memorandum & Articles of Association uploaded successfully."></asp:Label>
                                                        <asp:HiddenField ID="hdPn3" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkMemo" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="form-group row pinsection upladdocs">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-4" id="DVC4">
                                                        <label for="Others">
                                                            Certificate of incorporation/Registration/Partnership Deed<span style="color: Red;
                                                                font-weight: bold;">*</span></label>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpldCerti" TabIndex="41" CssClass="form-control" runat="server" />
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnCerti" runat="server" />
                                                            <%--<asp:Button ID="btnUpload3" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <asp:LinkButton ID="lnkCerti"  runat="server" OnClick="lnkCerti_Click" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelCerti"  runat="server" OnClick="lnkDelCerti_Click" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i> </asp:LinkButton>
                                                            <asp:HyperLink ID="hlDoc4" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa  fa-download" aria-hidden="true"></i></asp:HyperLink>
                                                        </div>
                                                        <asp:Label ID="lblCerti" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Certificate of incorporation/Registration/Partnership Deed uploaded successfully."></asp:Label>
                                                        <asp:HiddenField ID="hdPn4" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkCerti" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="col-sm-4">
                                                <label for="Others">
                                                    Project Type<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="drpProjectCat" TabIndex="44" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Project Cost >= 50 crore" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Project cost upto < 50 crore" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="The applicant will select either Large (for projects that cost more than INR 50 Cr) or MSME (for projects that cost less that INR 50 Cr) from the dropdown list">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="Others">
                                                    Application For<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="drpApplicationFor" TabIndex="45" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">New Unit</asp:ListItem>
                                                    <asp:ListItem Value="2">Expansion of existing unit</asp:ListItem>
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="New Unit/Expansion of Unit - Can be selected from the dropdown menu">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec" id="dvPromoterNM1">
                                <div class="form-header">
                                    <h2>
                                        <asp:Label ID="lblDet" runat="server"></asp:Label>
                                    </h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group">
                                        <div class="row pinsection" id="DvtxtPromoter">
                                            <div class="col-sm-3">
                                                <label for="Others">
                                                    Name of Promoter<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtNameOfPromoter" TabIndex="46" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                    MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="It will accept only alphabets and spaces, no special characters are allowed">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                <%--<table class="table table-bordered">
                                                <tr>
                                                    <th>
                                                        Name of promoter
                                                    </th>
                                                    <th>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtNameOfPromoter" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" Text="Add More" ID="AddMore" CssClass="btn btn-mini btn-danger pull-right"
                                                            Height="40px" OnClick="AddMore_Click"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>--%>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" id="DvtxtNoOfParter">
                                                <label for="Others">
                                                    Number of Partner(s) <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtNoOfParter" TabIndex="47" Onkeypress="return inputLimiter(event,'Numbers')"
                                                    MaxLength="4" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3" id="DvtxtNameOfMP">
                                                <label for="Others">
                                                    Name of the Managing Partner <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtNameOfMP" TabIndex="48" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                    MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="dvNm">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:GridView ID="GrvNameOfPromoter" TabIndex="49" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                <asp:HiddenField ID="hdpid" Value='<%#Eval("intProId") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="vchNameOfPromoter" HeaderText="Name of Promoter" />
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-Height="45">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDelete" CssClass="btn btn-danger btn-sm" runat="server"
                                                                    ImageUrl="~/images/DeleteIcn.png" ToolTip="Click To Delete !" OnClick="imgbtnDelete_Click"
                                                                    OnClientClick="return confirm('Are you sure you want to delete?');" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="DvDirectorww">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th align="center">
                                                            Name
                                                        </th>
                                                        <th align="center">
                                                            Designation
                                                        </th>
                                                        <th width="100px">
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtPName" TabIndex="50" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                                MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="It will accept only alphabets and spaces, no special characters are allowed. Up to 5 number of rows can be added">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPDesg" TabIndex="51" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                                MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button runat="server" TabIndex="52" Text="Add" ID="AddMoreBD" CssClass="btn btn-primary" OnClick="AddMoreBD_Click">
                                                            </asp:Button>
                                                            <asp:HiddenField ID="hdna" Value="0" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-sm-6 " id="GrdBD1">
                                                <asp:GridView ID="GrdBD" runat="server" TabIndex="53" AutoGenerateColumns="false" CssClass="table table-bordered margin-bottom0">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#" ItemStyle-Height="45">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                <asp:HiddenField ID="hdpid1" Value='<%#Eval("intProId1") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnName" Value='<%#Eval("vchName") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="vchName" HeaderText="Name" />
                                                        <asp:BoundField DataField="vchDesignation" HeaderText="Designation" />
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDelete1" CssClass="btn btn-danger" runat="server" ImageUrl="~/images/rubbish.png"
                                                                    OnClientClick="return confirm('Are you sure you want to delete?');" ToolTip="Click To Delete !"
                                                                    OnClick="imgbtnDelete1_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row" id="DvEdu">
                                            <div class="col-sm-3" id="TgToo">
                                                <label for="Others">
                                                    Name<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="drpTagTo" TabIndex="54" CssClass="form-control" runat="server">
                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Others">
                                                    Educational Qualification<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="drpEducation" TabIndex="55" CssClass="form-control" runat="server"
                                                    AutoPostBack="true" OnSelectedIndexChanged="drpEducation_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="The applicant will select the Highest Educational Qualification of Promoter from the drop-down menu. (Only if the proposed project category is MSME)">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Others">
                                                    Technical Qualification<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="drpTechnical" TabIndex="56" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="The applicant will select Technical Qualifications of Promoter from the drop-down menu. Any number of qualifications that are relevant to the project being applied for, can be selected. (Only if the proposed project category is MSME)">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Others">
                                                    Experience in Years<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtexpinYr" TabIndex="57" Onkeypress="return inputLimiter(event,'Numbers')"
                                                    MaxLength="2" CssClass="form-control" runat="server"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Experience of the Promoter, in years.">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row" id="Enc">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-3">
                                                            <label for="Others">
                                                                Educational Qualification<span class="text-red">*</span></label>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpldEducational" TabIndex="58" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 4 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnEdu" runat="server" />
                                                                <asp:LinkButton ID="lnkQuali" runat="server" OnClick="lnkQuali_Click" CssClass="input-group-addon bg-green"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelEdu" runat="server" OnClick="lnkDelEdu_Click" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc5" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                 <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblEdu" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Educational Qualification uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn5" runat="server" />
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkQuali" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-3">
                                                            <label for="Others">
                                                                Non Technical/Technical Qualification<span class="text-red" id="q1">*</span></label>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpldTechnical" TabIndex="59" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 4 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnTecnical" runat="server" />
                                                                <asp:LinkButton ID="lnkTechni" runat="server" OnClick="lnkTechni_Click" CssClass="input-group-addon bg-green"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelTechni" runat="server" OnClick="lnkDelTechni_Click" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc6" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                 <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblTechni" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Non Technical/Technical Qualification uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn6" runat="server" />
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkTechni" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-3">
                                                            <label for="Others">
                                                                Experience<span class="text-red">*</span></label>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpldExperience" TabIndex="60" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 4 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnExperience" runat="server" />
                                                                <asp:LinkButton ID="lnkExperience" runat="server" OnClick="lnkExperience_Click" CssClass="input-group-addon bg-green"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelExperience" runat="server" OnClick="lnkDelExperience_Click"
                                                                    CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc7" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                     <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblExp" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Experience uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn7" runat="server" />
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkExperience" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec">
                                <div class="form-header">
                                    <h2>
                                        4.Financial Status (INR in Lakhs)</h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th align="center">
                                                        </th>
                                                        <th align="center">
                                                            <asp:DropDownList ID="drpFinYear1" TabIndex="61" runat="server" CssClass="form-control"
                                                                OnSelectedIndexChanged="drpFinYear1_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </th>
                                                        <th align="center">
                                                            <asp:DropDownList ID="drpFinYear2" TabIndex="62" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="2025">2025-2026</asp:ListItem>
                                                                <asp:ListItem Value="2024">2024-2025</asp:ListItem>
                                                                <asp:ListItem Value="2023">2023-2024</asp:ListItem>
                                                                <asp:ListItem Value="2022">2022-2023</asp:ListItem>
                                                                <asp:ListItem Value="2021">2021-2022</asp:ListItem>
                                                                <asp:ListItem Value="2020">2020-2021</asp:ListItem>
                                                                <asp:ListItem Value="2019">2019-2020</asp:ListItem>
                                                                <asp:ListItem Value="2018">2018-2019</asp:ListItem>
                                                                <asp:ListItem Value="2017">2017-2018</asp:ListItem>
                                                                <asp:ListItem Value="2016">2016-2017</asp:ListItem>
                                                                <asp:ListItem Value="2015">2015-2016</asp:ListItem>
                                                                <asp:ListItem Value="2014">2014-2015</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:Label ID="drpFinYear2" runat="server" Text=""></asp:Label>--%>
                                                        </th>
                                                        <th align="center">
                                                            <asp:DropDownList ID="drpFinYear3" TabIndex="63" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="2025">2025-2026</asp:ListItem>
                                                                <asp:ListItem Value="2024">2024-2025</asp:ListItem>
                                                                <asp:ListItem Value="2023">2023-2024</asp:ListItem>
                                                                <asp:ListItem Value="2022">2022-2023</asp:ListItem>
                                                                <asp:ListItem Value="2021">2021-2022</asp:ListItem>
                                                                <asp:ListItem Value="2020">2020-2021</asp:ListItem>
                                                                <asp:ListItem Value="2019">2019-2020</asp:ListItem>
                                                                <asp:ListItem Value="2018">2018-2019</asp:ListItem>
                                                                <asp:ListItem Value="2017">2017-2018</asp:ListItem>
                                                                <asp:ListItem Value="2016">2016-2017</asp:ListItem>
                                                                <asp:ListItem Value="2015">2015-2016</asp:ListItem>
                                                                <asp:ListItem Value="2014">2014-2015</asp:ListItem>
                                                                <asp:ListItem Value="2013">2013-2014</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label for="Others">
                                                                Annual turn over</label><span style="font-weight: bold;" class="text-red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAnnual1" TabIndex="64" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAnnual2" TabIndex="65" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                MaxLength="15" onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAnnual3" TabIndex="66" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                MaxLength="15" onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label for="Others">
                                                                Profit after tax</label><span style="font-weight: bold;" class="text-red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProfit1" TabIndex="67" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProfit2" TabIndex="68" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProfit3" TabIndex="69" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                    </tr>
                                                    <tr id="dvReserveAndSurplus">
                                                        <td>
                                                            <label for="Others">
                                                                Reserve and surplus</label><span style="font-weight: bold;" class="text-red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReserve1" TabIndex="70" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="15" CssClass="form-control" onkeyup="sumExist();" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReserve2" TabIndex="71" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="15" CssClass="form-control" onkeyup="sumExist1();" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReserve3" TabIndex="72" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="15" CssClass="form-control" onkeyup="sumExist2();" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                    </tr>
                                                    <tr id="dvShareCapital">
                                                        <td>
                                                            <label for="Others">
                                                                Share capital</label>
                                                            <span style="font-weight: bold;" class="text-red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtShare1" TabIndex="73" onkeyup="sumExist();" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtShare2" TabIndex="74" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="15" CssClass="form-control" onkeyup="sumExist1();" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtShare3" TabIndex="75" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="15" CssClass="form-control" onkeyup="sumExist2();" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label for="Others">
                                                                Net worth</label><span style="font-weight: bold;" class="text-red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNtWorth1" TabIndex="76" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNtWorth2" TabIndex="77" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNtWorth3" TabIndex="78" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="row pinsection">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group ">
                                                        <label for="Others" class="col-md-5 col-sm-6">
                                                            <asp:Label ID="lblf1" runat="server" Text="Upload Audited Financial Statements for First Year"></asp:Label>
                                                            <%--Upload Audited Financial Statements for First Year--%>
                                                            <asp:Label ID="lblFirstYear" runat="server" Style="font-weight: bold;" Text=""></asp:Label><span
                                                                id="dv123" class="text-red">*</span></label>
                                                        <div class="col-md-6 col-sm-6">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileUpldAudited" TabIndex="79" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnAudit" runat="server" />
                                                                <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                <asp:LinkButton ID="lnkAudit" runat="server" OnClick="lnkAudit_Click" CssClass="input-group-addon bg-green"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelAudit" runat="server" OnClick="lnkDelAudit_Click" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc8" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                              <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblAudit1" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Audited Financial Statements for First Year uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn8" runat="server" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkAudit" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="row pinsection">
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <label for="Others" class="col-md-5 col-sm-6">
                                                            <asp:Label ID="lblf2" runat="server" Text="Upload Audited Financial Statements for Second Year"></asp:Label>
                                                            <asp:Label ID="lblSecondYear" Style="font-weight: bold;" runat="server" Text=""></asp:Label><span
                                                                id="adt1" class="text-red">*</span></label>
                                                        <div class="col-md-6 col-sm-6">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileUploadSecond" TabIndex="80" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnFySecond" runat="server" />
                                                                <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                <asp:LinkButton ID="lnkFySecond" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkFySecond_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkFySecondDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkFySecondDelete_Click"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc12" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblAudit2" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Audited Financial Statements for Second Year uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn10" runat="server" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkFySecond" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="row pinsection">
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <label for="Others" class="col-md-5 col-sm-6">
                                                            <asp:Label ID="lblf3" runat="server" Text="Upload Audited Financial Statements for Third Year"></asp:Label>
                                                            <asp:Label ID="lblThirdYear" Style="font-weight: bold;" runat="server" Text=""></asp:Label><span
                                                                id="adt2" class="text-red">*</span></label>
                                                        <div class="col-md-6 col-sm-6">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileUploadThird" TabIndex="81" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnFyThird" runat="server" />
                                                                <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                <asp:LinkButton ID="lnkFyThird" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkFyThird_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkFyThirdDel" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkFyThirdDel_Click"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc13" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                               <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblAudit3" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Audited Financial Statements for Third Year uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn11" runat="server" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkFyThird" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="row pinsection" runat="server" id="dvfy4">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <label for="Others" class="col-md-5 col-sm-6">
                                                            <asp:Label ID="lblIncomeTax" runat="server" Text="Income tax return"></asp:Label>
                                                            <span id="Span1" class="text-red">*</span></label>
                                                        <div class="col-md-6 col-sm-6">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileUploadFourthupd" TabIndex="82" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnFyFourth" runat="server" />
                                                                <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                <asp:LinkButton ID="lnkFyFourth" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClick="lnkFyFourth_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkFyFourDel" runat="server" CssClass="input-group-addon bg-red"
                                                                    OnClick="lnkFyFourDel_Click"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc14" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                               <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblAudit4" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Audited Financial Statements for Third Year uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn14" runat="server" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkFyFourth" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="row pinsection">
                                            <div class="form-group">
                                                <label for="Others" class="col-md-4 col-sm-6">
                                                    <asp:Label ID="lblf4" runat="server" Text="(financial statements,profit/loss accounts,balance sheet)"></asp:Label>
                                                    <%-- (financial statements,profit/loss accounts,balance sheet)--%>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="row pinsection">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                     <div class="form-group" id="DvNetWorth">
                                                    
                                                        <label for="Others" class="col-md-5 col-sm-6">
                                                            Net worth certified by CA<span class="text-red">*</span></label>
                                                        <div class="col-md-6 col-sm-6">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="FileUpldNetWorth" TabIndex="83" CssClass="form-control" runat="server" />
                                                            <asp:HiddenField ID="hdnNetWorth" runat="server" />
                                                            <%--<asp:Button ID="btnUpload8" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <asp:LinkButton ID="lnknetWorth" runat="server" OnClick="lnknetWorth_Click" CssClass="input-group-addon bg-green"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelnetWorth" runat="server" OnClick="lnkDelnetWorth_Click"
                                                                CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hlDoc9" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                               <i class="fa  fa-download"></i>
                                                            </asp:HyperLink>
                                                        </div>
                                                            </div>
                                                        <asp:Label ID="lblNet" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Net worth certified by CA uploaded successfully"></asp:Label>
                                                        <asp:HiddenField ID="hdPn9" runat="server" />
                                                     
                                                         </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnknetWorth" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-header" id="DvIndustry1">
                                        <h2>
                                            5.Existing Industry details</h2>
                                    </div>
                                    <div class="form-body" id="DvIndustry">
                                        <div class="form-group">
                                            <div class="row ">
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        Existing industry name</label>
                                                    <asp:TextBox ID="txtExtIndName" MaxLength="100" TabIndex="84" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Iname">
                                                        District<span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" TabIndex="85" CssClass="form-control"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        Block<span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="drpBlock" TabIndex="86" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="ICapacity">
                                                        Whether land allotted by IDCO<span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlAlloted" TabIndex="87" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4" id="divLD">
                                                    <label for="Capacity">
                                                        Extent of land(in acres)<span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtExtentLand" TabIndex="88" CssClass="form-control" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                        onblur="isNumberBlur(event, this, 2);" runat="server" Onkeypress="return inputLimiter(event,'Decimal')"
                                                        MaxLength="10"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4" id="divLD1">
                                                    <label for="Capacity">
                                                        Nature of activity<span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtNatureAct" TabIndex="89" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')"
                                                        MaxLength="100" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        Sector<span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlsector" TabIndex="90" runat="server" CssClass="form-control"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlsector_SelectedIndexChanged">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        Sub Sector<span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlSubSec" TabIndex="91" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        Capacity<span class="text-red">*</span></label>
                                                    <div class="clearfix">
                                                    </div>
                                                    <asp:TextBox ID="txtCapacity" TabIndex="92" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                        onblur="isNumberBlur(event, this, 2);" Onkeypress="return inputLimiter(event,'Decimal')"
                                                        MaxLength="10" CssClass="form-control" Style="width: 58%; float: left;" runat="server"></asp:TextBox>
                                                    <asp:DropDownList ID="drpCp" TabIndex="93" runat="server" CssClass="form-control" Style="width: 40%;
                                                        float: left; margin-left: 5px">
                                                    </asp:DropDownList>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                                <div class="col-sm-3" id="dvoth">
                                                    <label for="Iname">
                                                        Others (Please specify)<span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtCapOthr" TabIndex="94" Onkeypress="return inputLimiter(event,'OtherSpecify')"
                                                        MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                    </div>
                                    <div id="DvRaw">
                                        <div class="form-group">
                                            <div class="row ">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>
                                                                Raw material for production
                                                            </th>
                                                            <th>
                                                                Material source
                                                            </th>
                                                            <th>
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtRawMaterial" TabIndex="95" Onkeypress="return inputLimiter(event,'RawMetrial')"
                                                                    MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="drprawMeterial" TabIndex="96" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Within the state" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Outside the state" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Imported" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="100px">
                                                                <asp:Button runat="server" Text="Add" TabIndex="97" ID="btnAddMoreRWM" CssClass="btn btn-mini btn-primary"
                                                                    OnClick="btnAddMoreRWM_Click"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="GrvRWM" runat="server" TabIndex="98" AutoGenerateColumns="false" CssClass="table table-bordered margin-bottom0">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdpid2" Value='<%#Eval("intProId2") %>' runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="vchRawMaterial" HeaderText="Raw material for production" />
                                                            <asp:BoundField DataField="vchRawMeterialSrc" HeaderText="Material source" />
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnDelete2" CssClass="btn btn-danger btn-sm" runat="server"
                                                                        ImageUrl="~/images/rubbish.png" OnClientClick="return confirm('Are you sure you want to delete?');"
                                                                        ToolTip="Click To Delete !" OnClick="imgbtnDelete2_Click" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="p-xs text-center">
                                    <asp:Button ID="btnNext" runat="server" TabIndex="99" Text="Next" CssClass=" btn btn-success" OnClick="btnNext_Click" />
                                    <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save As Draft" CssClass=" btn btn-primary draftbtn noprint"
                                        OnClick="btnSaveAsDraft_Click" />
                                    <%-- <input type="reset" text="Reset" class=" btn btn-reset" />--%>
                                    <asp:Button ID="btnReset" runat="server" TabIndex="100" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                    <asp:HiddenField ID="hdnAllFileValue" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            debugger;
            $('.backcheck').hide();
            var sessioncal = '<%=Session["proposalno"] %>';
            var Qrvalue = '<%= Request.QueryString["StrPropNo"] %>';
            // alert(Qrvalue);
            // alert(sessioncal);
            if ((sessioncal != null) && (sessioncal != "")) {
                // alert('hiii');
                $('.wizard2 .nav-tabs > li').addClass("backactive");
                $('.backcheck').show();
            }

            else if ((Qrvalue != null) && (Qrvalue != "")) {
                $('.wizard2 .nav-tabs > li').addClass("backactive");
                $('.backcheck').show(); $('.backcheck2').hide(); $('.backcheck3').hide(); $('.backcheck4').hide();
            }
            else {
                $('.wizard2 .nav-tabs > li').removeClass("backactive");
                $('.backcheck').hide();
            }
            //alert("load document ready");
            //EnableAndDiseableBasedOnddlConstitution($("#ddlConstitution").val());
        });
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        var allFileVal = $("#hdnAllFileValue").val();

        function pageLoad(sender, args) {
            //alert("ok call page load");
            var techVal = $("#drpTechnical option:selected").text();
            if (techVal == 'NA') {
                $('#q1').hide();
            }
            else {
                $('#q1').show();
            }
            $("#drpTechnical").change(function (e) {
                var techVal = $("#drpTechnical option:selected").text();
                if (techVal == 'NA') {
                    $('#q1').hide();
                }
                else {
                    $('#q1').show();
                }
            });


            $("#FileUpldPan").change(function (e) {
                debugger;
                var filename = $("#FileUpldPan").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUpldPan").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUpldPan").val('');
                        return false;
                    }
                }
            });

            $("#FileUpldGstn").change(function (e) {
                var filename = $("#FileUpldGstn").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUpldGstn").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUpldGstn").val('');
                        return false;
                    }
                }
            });
            $("#FileUpldMemo").change(function (e) {
                var filename = $("#FileUpldMemo").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUpldMemo").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUpldMemo").val('');
                        return false;
                    }
                }
            });

            /*---------------------------------------------------------------------------------*/

            $("#FileUpldCerti").change(function (e) {
                var filename = $("#FileUpldCerti").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUpldCerti").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUpldCerti").val('');
                        return false;
                    }
                }

            });

            /*---------------------------------------------------------------------------------*/

            $("#FileUpldEducational").change(function (e) {
                var filename = $("#FileUpldEducational").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUpldEducational").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUpldEducational").val('');
                        return false;
                    }
                }
            });

            /*---------------------------------------------------------------------------------*/

            $("#FileUpldTechnical").change(function (e) {
                var filename = $("#FileUpldTechnical").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUpldEducational").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUpldEducational").val('');
                        return false;
                    }
                }
            });

            /*---------------------------------------------------------------------------------*/

            $("#FileUpldExperience").change(function (e) {
                var filename = $("#FileUpldExperience").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUpldExperience").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUpldExperience").val('');
                        return false;
                    }
                }
            });

            /*---------------------------------------------------------------------------------*/

            $("#FileUpldAudited").change(function (e) {
                var filename = $("#FileUpldAudited").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUpldAudited").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUpldAudited").val('');
                        return false;
                    }
                }
            });

            /*---------------------------------------------------------------------------------*/

            $("#FileUploadSecond").change(function (e) {
                var filename = $("#FileUploadSecond").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUploadSecond").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUploadSecond").val('');
                        return false;
                    }
                }
            });

            /*---------------------------------------------------------------------------------*/

            $("#FileUploadThird").change(function (e) {
                var filename = $("#FileUploadThird").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUploadThird").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUploadThird").val('');
                        return false;
                    }
                }
            });

            /*---------------------------------------------------------------------------------*/

            $("#FileUploadFourthupd").change(function (e) {
                var filename = $("#FileUploadFourthupd").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUploadFourthupd").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUploadFourthupd").val('');
                        return false;
                    }
                }
            });

            /*---------------------------------------------------------------------------------*/

            $("#FileUpldNetWorth").change(function (e) {
                var filename = $("#FileUpldNetWorth").val().replace(/C:\\fakepath\\/i, '');
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
                    $("#FileUpldNetWorth").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileUpldNetWorth").val('');
                        return false;
                    }
                }

            });

            /*---------------------------------------------------------------------------------*/

            $('#TgToo').hide();
            $('#DVC1').hide();
            $('#DVC2').hide();
            $('#DVC3').hide();
            $('#DVC4').hide();


            $('#adt2').hide();
            $('#adt1').hide();
            $('#dv123').hide();
            var val3 = $("#drpYearofEstablishment").val();
            var noyr3 = 0;
            var dt3 = new Date();
            var month = parseInt(dt3.getMonth()) + 1;
            if (month > 3)
            {
                if (parseInt(noyr3) > 0) {
                    $('#dv123').show();
                }
                else if (parseInt(noyr3) > 1) {
                    $('#adt1').show();
                }
                else if (parseInt(noyr3) > 2) {
                    $('#adt2').show();
                }
            }
            else
            {
                noyr3 = parseInt(dt3.getFullYear()) - parseInt(val3);

                if (parseInt(noyr3) > 1) {
                    $('#dv123').show();
                }
                else if (parseInt(noyr3) > 2) {
                    $('#adt1').show();
                }
                else if (parseInt(noyr3) > 3) {
                    $('#adt2').show();
                }
            }
           
            //if (parseInt(noyr3) > 0) {
            //    $('#dv123').show();
            //}
            //else if (parseInt(noyr3) > 1) {
            //    $('#adt1').show();
            //}
            //else if (parseInt(noyr3) > 2) {
            //    $('#adt2').show();
            //}
            $('#txtAddress').keyup(function () {
                var left = 250 - $(this).val().length;
                if (left < 0) {
                    left = 0;
                }
                if (left == 0) {
                    jAlert('<strong>Maximum length exceeded.</strong>', projname);
                }
                $('#SpanLbl').text(left);
            });


            $('#txtCorAddrs').keyup(function () {
                var left = 250 - $(this).val().length;
                if (left < 0) {
                    left = 0;
                }
                if (left == 0) {
                    jAlert('<strong>Maximum length exceeded.</strong>', projname);
                }
                $('#SpanLbl1').text(left);
            });
            if ($('#txtCorAddrs').val().length > 0) {
                var leftChar = 250 - $('#txtCorAddrs').val().length;
                $('#SpanLbl1').text(leftChar);
            }


            $('#txtEmail').change(function () {
                var EmailAddress = $('#txtEmail').val();
                if (EmailAddress != "") {
                    if (!echeck(EmailAddress)) {
                        $('#txtEmail').focus();
                        $('#txtEmail').val('');
                        return false;
                    }
                }
            });
            $('#txtCorEmailid').change(function () {
                var EmailAddress1 = $('#txtCorEmailid').val();
                if (EmailAddress1 != "") {
                    if (!echeck(EmailAddress1)) {
                        $('#txtCorEmailid').focus();
                        $('#txtCorEmailid').val('');
                        return false;
                    }
                }
            });
            $('#btnSaveAsDraft').hide();
            var jkn = $("#txtIName").val();
            if (jkn != "") {
                $('#btnSaveAsDraft').show();
            }
            else {
                $('#btnSaveAsDraft').hide();
            }
            $("#txtIName").change(function () {
                var jkk = $(this).val();
                if (jkk != "") {
                    $('#btnSaveAsDraft').show();
                }
                else {
                    $('#btnSaveAsDraft').hide();
                }
            });
            $('[data-toggle="tooltip"]').tooltip();
            //            $("#txtGSTIN").change(function () {
            //                var GSTN = /^([0-9]){2}([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})([0-9]){1}([Z]){1}([a-zA-Z0-9]){1}$/;
            //                if (document.getElementById("txtGSTIN").value.search(GSTN) == -1) {
            //                    jAlert('<strong>Invalid GST Identification No.</strong>', projname);
            //                    $(this).val('');
            //                    $(this).focus();
            //                    return false;
            //                }
            //            });
            $("#DvRaw").hide();
            $("#DvEnclos22").hide();
            $('#Enc').hide();
            $("#DvEdu").hide();
            $("#DvDirectorww").hide();
            $('#GrdBD1').hide();
            $("#DvEnclos2").hide();
            $('#DvIndustry').hide();
            $('#DvIndustry1').hide();
            $("#DvtxtPromoter").hide();
            $("#dvNm").hide();
            $("#DvtxtNoOfParter").hide();
            $("#DvtxtNameOfMP").hide();
            $("#dvPromoterNM1").hide();
            $("#DvOths").hide();
            $("#dvoth").hide();
            $("#DvNetWorth").hide();
            if ($("#drpCp option:selected").text() == "Other") {
                $('#dvoth').show();
            }
            else {
                $('#dvoth').hide();
            }
            $("#drpCp").change(function () {
                if ($("#drpCp option:selected").text() == "Other") {
                    $('#dvoth').show();
                }
                else {
                    $('#dvoth').hide();
                }
            });

            $("#txtNoOfParter").change(function () {
                var noofPartnr = $(this).val();
                if (noofPartnr < 2) {
                    jAlert('<strong>Number of partner(s) should not be less than 2.</strong>', projname);
                    $(this).val('');
                    $(this).focus();
                    return false;
                }
            });
            $("#txtPinCode").change(function () {
                var val = $(this).val();
                if (val.substring(0, 1) === '0') {
                    jAlert('<strong>PIN Code should not be start with zero</strong>', projname);
                    $(this).val('');
                    $(this).focus();
                    return false;
                }
                if (val.length < 6) {
                    jAlert('<strong>The minimum length of the PIN Code should be 6.</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
            });
            $("#txtCorPin").change(function () {
                var val = $(this).val();
                if (val.substring(0, 1) === '0') {
                    jAlert('<strong>PIN Code should not be start with zero</strong>', projname);
                    $(this).val('');
                    $(this).focus();
                    return false;
                }
                if (val.length < 6) {
                    jAlert('<strong>The minimum length of the PIN Code should be 6.</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
            });
            $("#txtCorMob").change(function () {
                var val = $(this).val();
                if (val.substring(0, 1) === '0') {
                    jAlert('<strong>Mobile number should not start with zero</strong>', projname);
                    $(this).val('');
                    $(this).focus();
                    return false;
                }
                if (val.length < 10) {
                    jAlert('<strong>The minimum length of the mobile number should be 10.</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
            });
            $("#txtPhoneNo").change(function () {
                var val = $(this).val();
                //                if (val.substring(0, 1) === '0') {
                //                    jAlert('<strong>Phone number should not start with zero</strong>', projname);
                //                    $(this).val('');
                //                    $(this).focus();
                //                    return false;
                //                }
                if (val.length < 4) {
                    jAlert('<strong>The minimum length of the phone number should be 4.</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
            });
            $("#txtFaxNo").change(function () {
                var val = $(this).val();
                if (val.length < 4) {
                    jAlert('<strong>The minimum length of the fax number should be 4.</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
            });
            $("#txtCorFax").change(function () {
                var val = $(this).val();
                if (val.length < 4) {
                    jAlert('<strong>The minimum length of the fax number should be 4.</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
            });

            /*---------------------------------------------------------------------------------*/

            $("#drpYearofEstablishment").change(function () {

                var val = $(this).val();
                var noyr = 0;

                if (val.length == 4) {
                    var dt = new Date();
                    
                    var month = parseInt(dt.getMonth()) + 1;// 

                    if (month > 3)// from APRIL 2023-24
                    {
                        noyr = parseInt(dt.getFullYear()) - parseInt(val);
                        if (val > dt.getFullYear()) {
                            jAlert('<strong>Year of Establishment does not accept future Year</strong>', projname);
                            $(this).focus();
                            $(this).val('');
                            return false;
                        }
                        else if (parseInt(val) == 0) // parseInt(dt.getFullYear())
                        {
                            $("#drpFinYear1").prop("disabled", true);
                            document.getElementById("drpFinYear1").value = "0";
                            document.getElementById("drpFinYear2").value = "0";
                            document.getElementById("drpFinYear3").value = "0";
                            document.getElementById("txtAnnual1").readOnly = true;
                            document.getElementById("txtAnnual2").readOnly = true;
                            document.getElementById("txtAnnual3").readOnly = true;
                            document.getElementById("txtProfit1").readOnly = true;
                            document.getElementById("txtProfit2").readOnly = true;
                            document.getElementById("txtProfit3").readOnly = true;
                            document.getElementById("txtReserve1").readOnly = true;
                            document.getElementById("txtReserve2").readOnly = true;
                            document.getElementById("txtReserve3").readOnly = true;
                            document.getElementById("txtShare1").readOnly = true;
                            document.getElementById("txtShare2").readOnly = true;
                            document.getElementById("txtShare3").readOnly = true;
                            document.getElementById("txtNtWorth3").readOnly = true;
                            document.getElementById("txtNtWorth1").readOnly = true;
                            document.getElementById("txtNtWorth2").readOnly = true;
                            $('#adt2').hide();
                            $('#adt1').hide();
                            $('#dv123').hide();
                        }
                        else if (parseInt(noyr) == 1) {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual2").readOnly = true;
                            document.getElementById("txtAnnual3").readOnly = true;
                            document.getElementById("txtProfit2").readOnly = true;
                            document.getElementById("txtProfit3").readOnly = true;
                            document.getElementById("txtReserve2").readOnly = true;
                            document.getElementById("txtReserve3").readOnly = true;
                            document.getElementById("txtShare2").readOnly = true;
                            document.getElementById("txtShare3").readOnly = true;
                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            document.getElementById("txtNtWorth2").readOnly = true;
                            document.getElementById("txtNtWorth3").readOnly = true;
                            $('#adt1').hide();
                            $('#adt2').hide();
                            $('#dv123').show();
                        }
                        else if (parseInt(noyr) == 2) {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual2").readOnly = false;
                            document.getElementById("txtAnnual3").readOnly = true;
                            document.getElementById("txtProfit2").readOnly = false;
                            document.getElementById("txtProfit3").readOnly = true;
                            document.getElementById("txtReserve2").readOnly = false;
                            document.getElementById("txtReserve3").readOnly = true;
                            document.getElementById("txtShare2").readOnly = false;
                            document.getElementById("txtShare3").readOnly = true;

                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtNtWorth3").readOnly = true;
                            document.getElementById("txtNtWorth2").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            $('#adt1').show();
                            $('#dv123').show();
                            $('#adt2').hide();
                        }
                        else {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtAnnual2").readOnly = false;
                            document.getElementById("txtAnnual3").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtProfit2").readOnly = false;
                            document.getElementById("txtProfit3").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtReserve2").readOnly = false;
                            document.getElementById("txtReserve3").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtShare2").readOnly = false;
                            document.getElementById("txtShare3").readOnly = false;
                            document.getElementById("txtNtWorth3").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            document.getElementById("txtNtWorth2").readOnly = false;
                            $('#adt2').show();
                            $('#adt1').show();
                            $('#dv123').show();
                        }
                    }
                    else // upto  march 2022-23
                    {
                        noyr = (parseInt(dt.getFullYear()) - 1) - parseInt(val);//2023
                        if (val > dt.getFullYear()) {
                            jAlert('<strong>Year of Establishment does not accept future Year</strong>', projname);
                            $(this).focus();
                            $(this).val('');
                            return false;
                        }
                        else if (parseInt(noyr) == 0) {

                            $("#drpFinYear1").prop("disabled", true);
                            document.getElementById("drpFinYear1").value = "0";
                            document.getElementById("drpFinYear2").value = "0";
                            document.getElementById("drpFinYear3").value = "0";
                            document.getElementById("txtAnnual1").readOnly = true;
                            document.getElementById("txtAnnual2").readOnly = true;
                            document.getElementById("txtAnnual3").readOnly = true;
                            document.getElementById("txtProfit1").readOnly = true;
                            document.getElementById("txtProfit2").readOnly = true;
                            document.getElementById("txtProfit3").readOnly = true;
                            document.getElementById("txtReserve1").readOnly = true;
                            document.getElementById("txtReserve2").readOnly = true;
                            document.getElementById("txtReserve3").readOnly = true;
                            document.getElementById("txtShare1").readOnly = true;
                            document.getElementById("txtShare2").readOnly = true;
                            document.getElementById("txtShare3").readOnly = true;
                            document.getElementById("txtNtWorth3").readOnly = true;
                            document.getElementById("txtNtWorth1").readOnly = true;
                            document.getElementById("txtNtWorth2").readOnly = true;
                            $('#adt2').hide();
                            $('#adt1').hide();
                            $('#dv123').hide();

                        }
                        else if (parseInt(noyr) == 1) {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual2").readOnly = true;
                            document.getElementById("txtAnnual3").readOnly = true;
                            document.getElementById("txtProfit2").readOnly = true;
                            document.getElementById("txtProfit3").readOnly = true;
                            document.getElementById("txtReserve2").readOnly = true;
                            document.getElementById("txtReserve3").readOnly = true;
                            document.getElementById("txtShare2").readOnly = true;
                            document.getElementById("txtShare3").readOnly = true;
                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            document.getElementById("txtNtWorth2").readOnly = true;
                            document.getElementById("txtNtWorth3").readOnly = true;
                            $('#adt1').hide();
                            $('#adt2').hide();
                            $('#dv123').show();
                        }
                        else if (parseInt(noyr) == 2) {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual2").readOnly = false;
                            document.getElementById("txtAnnual3").readOnly = true;
                            document.getElementById("txtProfit2").readOnly = false;
                            document.getElementById("txtProfit3").readOnly = true;
                            document.getElementById("txtReserve2").readOnly = false;
                            document.getElementById("txtReserve3").readOnly = true;
                            document.getElementById("txtShare2").readOnly = false;
                            document.getElementById("txtShare3").readOnly = true;

                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtNtWorth3").readOnly = true;
                            document.getElementById("txtNtWorth2").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            $('#adt1').show();
                            $('#dv123').show();
                            $('#adt2').hide();
                        }
                        else
                        {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtAnnual2").readOnly = false;
                            document.getElementById("txtAnnual3").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtProfit2").readOnly = false;
                            document.getElementById("txtProfit3").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtReserve2").readOnly = false;
                            document.getElementById("txtReserve3").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtShare2").readOnly = false;
                            document.getElementById("txtShare3").readOnly = false;
                            document.getElementById("txtNtWorth3").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            document.getElementById("txtNtWorth2").readOnly = false;
                            $('#adt2').show();
                            $('#adt1').show();
                            $('#dv123').show();
                        }




                    }
                    
                    
                    
                }
                else {
                    jAlert('<strong>The maximum length should be 4.</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                }
            });

            /*------------------------------------Financial Status---------------------------------------------*/

            $("#drpFinYear1").change(function () {

                debugger;
                var val = $("#drpYearofEstablishment").val();//2021
                var noyr = 0;
                if (val.length == 4) {
                    var dt = new Date();

                    var month = parseInt(dt.getMonth()) + 1;// 
                    //var year = parseInt(dt.getFullYear());

                    if (month > 3)// from APRIL 2023-24
                    {
                        noyr = parseInt(dt.getFullYear()) - parseInt(val);

                        if (parseInt(noyr) == 0) {
                            $("#drpFinYear1").prop("disabled", true);
                            document.getElementById("txtAnnual2").readOnly = true;
                            document.getElementById("txtAnnual3").readOnly = true;
                            document.getElementById("txtProfit2").readOnly = true;
                            document.getElementById("txtProfit3").readOnly = true;
                            document.getElementById("txtReserve2").readOnly = true;
                            document.getElementById("txtReserve3").readOnly = true;
                            document.getElementById("txtShare2").readOnly = true;
                            document.getElementById("txtShare3").readOnly = true;
                            document.getElementById("txtAnnual1").readOnly = true;
                            document.getElementById("txtProfit1").readOnly = true;
                            document.getElementById("txtReserve1").readOnly = true;
                            document.getElementById("txtShare1").readOnly = true;
                            document.getElementById("txtNtWorth1").readOnly = true;
                            document.getElementById("txtNtWorth2").readOnly = true;
                            document.getElementById("txtNtWorth3").readOnly = true;
                            $('#adt1').hide();
                            $('#adt2').hide();
                            $('#dv123').hide();
                        }
                        
                       else if (parseInt(noyr) == 1)
                        {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual2").readOnly = true;
                            document.getElementById("txtAnnual3").readOnly = true;
                            document.getElementById("txtProfit2").readOnly = true;
                            document.getElementById("txtProfit3").readOnly = true;
                            document.getElementById("txtReserve2").readOnly = true;
                            document.getElementById("txtReserve3").readOnly = true;
                            document.getElementById("txtShare2").readOnly = true;
                            document.getElementById("txtShare3").readOnly = true;
                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            document.getElementById("txtNtWorth2").readOnly = true;
                            document.getElementById("txtNtWorth3").readOnly = true;
                            $('#adt1').hide();
                            $('#adt2').hide();
                            $('#dv123').show();
                        }
                        else if (parseInt(noyr) == 2) {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual2").readOnly = false;
                            document.getElementById("txtAnnual3").readOnly = true;
                            document.getElementById("txtProfit2").readOnly = false;
                            document.getElementById("txtProfit3").readOnly = true;
                            document.getElementById("txtReserve2").readOnly = false;
                            document.getElementById("txtReserve3").readOnly = true;
                            document.getElementById("txtShare2").readOnly = false;
                            document.getElementById("txtShare3").readOnly = true;

                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtNtWorth3").readOnly = true;
                            document.getElementById("txtNtWorth2").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            $('#adt1').show();
                            $('#dv123').show();
                            $('#adt2').hide();
                        }
                        else {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtAnnual2").readOnly = false;
                            document.getElementById("txtAnnual3").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtProfit2").readOnly = false;
                            document.getElementById("txtProfit3").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtReserve2").readOnly = false;
                            document.getElementById("txtReserve3").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtShare2").readOnly = false;
                            document.getElementById("txtShare3").readOnly = false;
                            document.getElementById("txtNtWorth3").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            document.getElementById("txtNtWorth2").readOnly = false;
                            $('#adt2').show();
                            $('#adt1').show();
                            $('#dv123').show();
                        }
                    }
                    else // upto  march 2022-23
                    {
                        noyr = (parseInt(dt.getFullYear())) - parseInt(val);//2023-2021=2
                        
                        if (parseInt(noyr) == 2)
                        {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual2").readOnly = true;
                            document.getElementById("txtAnnual3").readOnly = true;
                            document.getElementById("txtProfit2").readOnly = true;
                            document.getElementById("txtProfit3").readOnly = true;
                            document.getElementById("txtReserve2").readOnly = true;
                            document.getElementById("txtReserve3").readOnly = true;
                            document.getElementById("txtShare2").readOnly = true;
                            document.getElementById("txtShare3").readOnly = true;
                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            document.getElementById("txtNtWorth2").readOnly = true;
                            document.getElementById("txtNtWorth3").readOnly = true;
                            $('#adt1').hide();
                            $('#adt2').hide();
                            $('#dv123').show();
                        }
                        else if (parseInt(noyr) == 3) {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual2").readOnly = false;
                            document.getElementById("txtAnnual3").readOnly = true;
                            document.getElementById("txtProfit2").readOnly = false;
                            document.getElementById("txtProfit3").readOnly = true;
                            document.getElementById("txtReserve2").readOnly = false;
                            document.getElementById("txtReserve3").readOnly = true;
                            document.getElementById("txtShare2").readOnly = false;
                            document.getElementById("txtShare3").readOnly = true;

                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtNtWorth3").readOnly = true;
                            document.getElementById("txtNtWorth2").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            $('#adt1').show();
                            $('#dv123').show();
                            $('#adt2').hide();
                        }
                        else {
                            $("#drpFinYear1").prop("disabled", false);
                            document.getElementById("txtAnnual1").readOnly = false;
                            document.getElementById("txtAnnual2").readOnly = false;
                            document.getElementById("txtAnnual3").readOnly = false;
                            document.getElementById("txtProfit1").readOnly = false;
                            document.getElementById("txtProfit2").readOnly = false;
                            document.getElementById("txtProfit3").readOnly = false;
                            document.getElementById("txtReserve1").readOnly = false;
                            document.getElementById("txtReserve2").readOnly = false;
                            document.getElementById("txtReserve3").readOnly = false;
                            document.getElementById("txtShare1").readOnly = false;
                            document.getElementById("txtShare2").readOnly = false;
                            document.getElementById("txtShare3").readOnly = false;
                            document.getElementById("txtNtWorth3").readOnly = false;
                            document.getElementById("txtNtWorth1").readOnly = false;
                            document.getElementById("txtNtWorth2").readOnly = false;
                            $('#adt2').show();
                            $('#adt1').show();
                            $('#dv123').show();
                        }




                    }

                }

            });



            /*---------------------------------------------------------------------------------*/
            ////// Check validation against Constitution/Enterprise on Page Load
            /*---------------------------------------------------------------------------------*/

            debugger;
            EnableAndDiseableBasedOnddlConstitution($("#ddlConstitution").val());
            //if ($("#ddlConstitution").val() == "1") {
            //    $("#lblDet").text('3.Promoter Details');
            //    $("#DvtxtPromoter").show();
            //    $("#dvNm").show();
            //    $("#DvtxtNoOfParter").hide();
            //    $("#DvtxtNameOfMP").hide();
            //    $("#DvDirectorww").hide();
            //    $("#GrdBD1").hide();
            //    $("#dvPromoterNM1").show();
            //    $("#DvOths").hide();
            //    $("#DVC1").hide();
            //    $("#DVC2").hide();
            //    $("#DVC3").hide();
            //    $("#DVC4").hide();
            //    $('#TgToo').hide();
            //    document.getElementById("drpFinYear1").disabled = true;
            //    document.getElementById("drpFinYear2").readOnly = true;
            //    document.getElementById("drpFinYear3").readOnly = true;
            //    document.getElementById("drpFinYear1").value = "0";
            //    document.getElementById("drpFinYear2").value = "0";
            //    document.getElementById("drpFinYear3").value = "0";
            //    document.getElementById("txtAnnual1").readOnly = true;
            //    document.getElementById("txtAnnual2").readOnly = true;
            //    document.getElementById("txtAnnual3").readOnly = true;
            //    document.getElementById("txtProfit1").readOnly = true;
            //    document.getElementById("txtProfit2").readOnly = true;
            //    document.getElementById("txtProfit3").readOnly = true;
            //    document.getElementById("txtReserve1").readOnly = true;
            //    document.getElementById("txtReserve2").readOnly = true;
            //    document.getElementById("txtReserve3").readOnly = true;
            //    document.getElementById("txtShare1").readOnly = true;
            //    document.getElementById("txtShare2").readOnly = true;
            //    document.getElementById("txtShare3").readOnly = true;
            //    document.getElementById("txtNtWorth3").readOnly = true;
            //    document.getElementById("txtNtWorth1").readOnly = true;
            //    document.getElementById("txtNtWorth2").readOnly = true;
            //    $('#adt2').show();
            //    $('#adt1').show();
            //    if ($('#drpApplicationFor').val() == "1") { //// Added by Sushant Jena
            //        $('#adt1').hide();
            //    }
            //    $('#dv123').show();         
            //    document.getElementById("txtAnnual1").value = "0";
            //    document.getElementById("txtAnnual2").value = "0";
            //    document.getElementById("txtAnnual3").value = "0";
            //    document.getElementById("txtProfit1").value = "0";
            //    document.getElementById("txtProfit2").value = "0";
            //    document.getElementById("txtProfit3").value = "0";
            //    document.getElementById("txtReserve1").value = "0";
            //    document.getElementById("txtReserve2").value = "0";
            //    document.getElementById("txtReserve3").value = "0";
            //    document.getElementById("txtShare1").value = "0";
            //    document.getElementById("txtShare2").value = "0";
            //    document.getElementById("txtShare3").value = "0";
            //    document.getElementById("txtNtWorth3").value = "0";
            //    document.getElementById("txtNtWorth1").value = "0";
            //    document.getElementById("txtNtWorth2").value = "0";
            //    $("#lblf1").text("Networth Certificate of the Proprietor duly certified by CA for Current/latest year.");
            //    $("#lblf2").text("Tax Audit Report(if applicable) for Current/latest year.");
            //    $("#lblf3").text("Income tax return for Current/latest year.");
            //    $('#lblf4').hide();
            //    $('#dvfy4').hide();
            //    $('#dvShareCapital').show();
            //    $('#dvReserveAndSurplus').show();
               

            //}
            //else if ($("#ddlConstitution").val() == "2") {

            //    $("#lblDet").text('3.Partnership Details');
            //    $("#DvtxtNoOfParter").show();
            //    $("#DvtxtNameOfMP").show();
            //    $("#DvtxtPromoter").hide();
            //    $("#dvNm").hide();
            //    $("#DvDirectorww").hide();
            //    $("#GrdBD1").hide();
            //    $("#dvPromoterNM1").show();
            //    $("#DvOths").hide();
            //    $("#DVC1").show();
            //    $("#DVC2").hide();
            //    $("#DVC3").hide();
            //    $("#DVC4").show();
            //    $('#TgToo').hide();
            //    document.getElementById("drpFinYear1").disabled = false;
            //    document.getElementById("drpFinYear2").readOnly = false;
            //    document.getElementById("drpFinYear3").readOnly = false;
            //    document.getElementById("txtAnnual1").readOnly = false;
            //    document.getElementById("txtAnnual2").readOnly = false;
            //    document.getElementById("txtAnnual3").readOnly = false;
            //    document.getElementById("txtProfit1").readOnly = false;
            //    document.getElementById("txtProfit2").readOnly = false;
            //    document.getElementById("txtProfit3").readOnly = false;
            //    document.getElementById("txtReserve1").readOnly = false;
            //    document.getElementById("txtReserve2").readOnly = false;
            //    document.getElementById("txtReserve3").readOnly = false;
            //    document.getElementById("txtShare1").readOnly = false;
            //    document.getElementById("txtShare2").readOnly = false;
            //    document.getElementById("txtShare3").readOnly = false;
            //    document.getElementById("txtNtWorth3").readOnly = false;
            //    document.getElementById("txtNtWorth1").readOnly = false;
            //    document.getElementById("txtNtWorth2").readOnly = false;
            //    $('#adt2').show();
            //    if ($('#drpApplicationFor').val() == "1") { //// Added by Sushant Jena
            //        $('#adt2').hide();
            //    }
            //    $('#adt1').show();
            //    $('#dv123').show();
            //    document.getElementById("txtReserve1").value = "0";
            //    document.getElementById("txtReserve2").value = "0";
            //    document.getElementById("txtReserve3").value = "0";
            //    document.getElementById("txtShare1").value = "0";
            //    document.getElementById("txtShare2").value = "0";
            //    document.getElementById("txtShare3").value = "0";

            //    $("#lblf1").text("Partnership deed.");
            //    $("#lblf2").text("Complete balance sheet of the firm(latest 3 years).");
            //    $("#lblf3").text("Tax audit report of the Partnership firm.");
            //    $('#lblf4').hide();
            //    $('#dvfy4').show();
            //    $('#dvShareCapital').hide();
            //    $('#dvReserveAndSurplus').hide();
                
            //}
            //else if ($("#ddlConstitution").val() == "8") {
            //    $("#lblDet").text('3.Board Of Directors');
            //    $("#DvtxtPromoter").hide();
            //    $("#dvNm").hide();
            //    $("#DvtxtNoOfParter").hide();
            //    $("#DvtxtNameOfMP").hide();
            //    $("#DvDirectorww").show();
            //    $("#GrdBD1").show();
            //    $("#dvPromoterNM1").show();
            //    $("#DvOths").show();
            //    $("#DVC1").show();
            //    $("#DVC2").show();
            //    $("#DVC3").show();
            //    $("#DVC4").show();
            //    $('#TgToo').show();
            //    document.getElementById("drpFinYear1").disabled = false;
            //    document.getElementById("drpFinYear2").readOnly = false;
            //    document.getElementById("drpFinYear3").readOnly = false;
            //    document.getElementById("txtAnnual1").readOnly = false;
            //    document.getElementById("txtAnnual2").readOnly = false;
            //    document.getElementById("txtAnnual3").readOnly = false;
            //    document.getElementById("txtProfit1").readOnly = false;
            //    document.getElementById("txtProfit2").readOnly = false;
            //    document.getElementById("txtProfit3").readOnly = false;
            //    document.getElementById("txtReserve1").readOnly = false;
            //    document.getElementById("txtReserve2").readOnly = false;
            //    document.getElementById("txtReserve3").readOnly = false;
            //    document.getElementById("txtShare1").readOnly = false;
            //    document.getElementById("txtShare2").readOnly = false;
            //    document.getElementById("txtShare3").readOnly = false;
            //    document.getElementById("txtNtWorth3").readOnly = false;
            //    document.getElementById("txtNtWorth1").readOnly = false;
            //    document.getElementById("txtNtWorth2").readOnly = false;
            //    $('#adt2').show();
            //    $('#adt1').show();
            //    $('#dv123').show();
            //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
            //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
            //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
            //    $('#lblf4').show();
            //    $('#dvfy4').hide();
            //    $('#dvShareCapital').show();
            //    $('#dvReserveAndSurplus').show();
                
            //}
            //else if ($("#ddlConstitution").val() == "3") {
            //    $("#lblDet").text('3.Board Of Directors');
            //    $("#DvtxtPromoter").hide();
            //    $("#dvNm").hide();
            //    $("#DvtxtNoOfParter").hide();
            //    $("#DvtxtNameOfMP").hide();
            //    $("#DvDirectorww").show();
            //    $("#GrdBD1").show();
            //    $("#dvPromoterNM1").show();
            //    $("#DvOths").hide();
            //    $("#DVC1").show();
            //    $("#DVC2").show();
            //    $("#DVC3").show();
            //    $("#DVC4").show();
            //    $('#TgToo').show();
            //    document.getElementById("drpFinYear1").disabled = false;
            //    document.getElementById("drpFinYear2").readOnly = false;
            //    document.getElementById("drpFinYear3").readOnly = false;
            //    document.getElementById("txtAnnual1").readOnly = false;
            //    document.getElementById("txtAnnual2").readOnly = false;
            //    document.getElementById("txtAnnual3").readOnly = false;
            //    document.getElementById("txtProfit1").readOnly = false;
            //    document.getElementById("txtProfit2").readOnly = false;
            //    document.getElementById("txtProfit3").readOnly = false;
            //    document.getElementById("txtReserve1").readOnly = false;
            //    document.getElementById("txtReserve2").readOnly = false;
            //    document.getElementById("txtReserve3").readOnly = false;
            //    document.getElementById("txtShare1").readOnly = false;
            //    document.getElementById("txtShare2").readOnly = false;
            //    document.getElementById("txtShare3").readOnly = false;
            //    document.getElementById("txtNtWorth3").readOnly = false;
            //    document.getElementById("txtNtWorth1").readOnly = false;
            //    document.getElementById("txtNtWorth2").readOnly = false;
            //    $('#adt2').show();
            //    $('#adt1').show();
            //    $('#dv123').show();
            //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
            //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
            //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
            //    $('#lblf4').show();
            //    $('#dvfy4').hide();
            //    $('#dvShareCapital').show();
            //    $('#dvReserveAndSurplus').show();
                
            //}
            //else if ($("#ddlConstitution").val() == "4") {
            //    $("#lblDet").text('3.Board Of Directors');
            //    $("#DvtxtPromoter").hide();
            //    $("#dvNm").hide();
            //    $("#DvtxtNoOfParter").hide();
            //    $("#DvtxtNameOfMP").hide();
            //    $("#DvDirectorww").show();
            //    $("#GrdBD1").show();
            //    $("#dvPromoterNM1").show();
            //    $("#DvOths").hide();
            //    $("#DVC1").show();
            //    $("#DVC2").show();
            //    $("#DVC3").show();
            //    $("#DVC4").show();
            //    $('#TgToo').show();
            //    document.getElementById("drpFinYear1").disabled = false;
            //    document.getElementById("drpFinYear2").readOnly = false;
            //    document.getElementById("drpFinYear3").readOnly = false;
            //    document.getElementById("txtAnnual1").readOnly = false;
            //    document.getElementById("txtAnnual2").readOnly = false;
            //    document.getElementById("txtAnnual3").readOnly = false;
            //    document.getElementById("txtProfit1").readOnly = false;
            //    document.getElementById("txtProfit2").readOnly = false;
            //    document.getElementById("txtProfit3").readOnly = false;
            //    document.getElementById("txtReserve1").readOnly = false;
            //    document.getElementById("txtReserve2").readOnly = false;
            //    document.getElementById("txtReserve3").readOnly = false;
            //    document.getElementById("txtShare1").readOnly = false;
            //    document.getElementById("txtShare2").readOnly = false;
            //    document.getElementById("txtShare3").readOnly = false;
            //    document.getElementById("txtNtWorth3").readOnly = false;
            //    document.getElementById("txtNtWorth1").readOnly = false;
            //    document.getElementById("txtNtWorth2").readOnly = false;
            //    $('#adt2').show();
            //    $('#adt1').show();
            //    $('#dv123').show();
            //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
            //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
            //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
            //    $('#lblf4').show();
            //    $('#dvfy4').hide();
            //    $('#dvShareCapital').show();
            //    $('#dvReserveAndSurplus').show();
               
            //}
            //else if ($("#ddlConstitution").val() == "5") {
            //    $("#lblDet").text('3.Board Of Directors');
            //    $("#DvtxtPromoter").hide();
            //    $("#dvNm").hide();
            //    $("#DvtxtNoOfParter").hide();
            //    $("#DvtxtNameOfMP").hide();
            //    $("#DvDirectorww").show();
            //    $("#GrdBD1").show();
            //    $("#dvPromoterNM1").show();
            //    $("#DvOths").hide();
            //    $("#DVC1").show();
            //    $("#DVC2").show();
            //    $("#DVC3").show();
            //    $("#DVC4").show();
            //    $('#TgToo').show();
            //    document.getElementById("drpFinYear1").disabled = false;
            //    document.getElementById("drpFinYear2").readOnly = false;
            //    document.getElementById("drpFinYear3").readOnly = false;
            //    document.getElementById("txtAnnual1").readOnly = false;
            //    document.getElementById("txtAnnual2").readOnly = false;
            //    document.getElementById("txtAnnual3").readOnly = false;
            //    document.getElementById("txtProfit1").readOnly = false;
            //    document.getElementById("txtProfit2").readOnly = false;
            //    document.getElementById("txtProfit3").readOnly = false;
            //    document.getElementById("txtReserve1").readOnly = false;
            //    document.getElementById("txtReserve2").readOnly = false;
            //    document.getElementById("txtReserve3").readOnly = false;
            //    document.getElementById("txtShare1").readOnly = false;
            //    document.getElementById("txtShare2").readOnly = false;
            //    document.getElementById("txtShare3").readOnly = false;
            //    document.getElementById("txtNtWorth3").readOnly = false;
            //    document.getElementById("txtNtWorth1").readOnly = false;
            //    document.getElementById("txtNtWorth2").readOnly = false;
            //    $('#adt2').show();
            //    $('#adt1').show();
            //    $('#dv123').show();
            //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
            //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
            //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
            //    $('#lblf4').show();
            //    $('#dvfy4').hide();
            //    $('#dvShareCapital').show();
            //    $('#dvReserveAndSurplus').show();
               
            //}
            //else if ($("#ddlConstitution").val() == "6") {
            //    $("#lblDet").text('3.Board Of Directors');
            //    $("#DvtxtPromoter").hide();
            //    $("#dvNm").hide();
            //    $("#DvtxtNoOfParter").hide();
            //    $("#DvtxtNameOfMP").hide();
            //    $("#DvDirectorww").show();
            //    $("#GrdBD1").show();
            //    $("#dvPromoterNM1").show();
            //    $("#DvOths").hide();
            //    $("#DVC3").hide();
            //    $("#DVC1").show();
            //    $("#DVC2").show();
            //    $("#DVC4").show();
            //    $('#TgToo').show();
            //    document.getElementById("drpFinYear1").disabled = false;
            //    document.getElementById("drpFinYear2").readOnly = false;
            //    document.getElementById("drpFinYear3").readOnly = false;
            //    document.getElementById("txtAnnual1").readOnly = false;
            //    document.getElementById("txtAnnual2").readOnly = false;
            //    document.getElementById("txtAnnual3").readOnly = false;
            //    document.getElementById("txtProfit1").readOnly = false;
            //    document.getElementById("txtProfit2").readOnly = false;
            //    document.getElementById("txtProfit3").readOnly = false;
            //    document.getElementById("txtReserve1").readOnly = false;
            //    document.getElementById("txtReserve2").readOnly = false;
            //    document.getElementById("txtReserve3").readOnly = false;
            //    document.getElementById("txtShare1").readOnly = false;
            //    document.getElementById("txtShare2").readOnly = false;
            //    document.getElementById("txtShare3").readOnly = false;
            //    document.getElementById("txtNtWorth3").readOnly = false;
            //    document.getElementById("txtNtWorth1").readOnly = false;
            //    document.getElementById("txtNtWorth2").readOnly = false;
            //    $('#adt2').show();
            //    $('#adt1').show();
            //    $('#dv123').show();
            //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
            //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
            //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
            //    $('#lblf4').show();
            //    $('#dvfy4').hide();
            //    $('#dvShareCapital').show();
            //    $('#dvReserveAndSurplus').show();
               
            //}
            //else if ($("#ddlConstitution").val() == "7") {
            //    $("#lblDet").text('3.Board Of Directors');
            //    $("#DvtxtPromoter").hide();
            //    $("#dvNm").hide();
            //    $("#DvtxtNoOfParter").hide();
            //    $("#DvtxtNameOfMP").hide();
            //    $("#DvDirectorww").show();
            //    $("#GrdBD1").show();
            //    $("#dvPromoterNM1").show();
            //    $("#DvOths").hide();
            //    $("#DVC3").hide();
            //    $("#DVC1").show();
            //    $("#DVC2").show();
            //    $("#DVC4").show();
            //    $('#TgToo').show();
            //    document.getElementById("drpFinYear1").disabled = false;
            //    document.getElementById("drpFinYear2").readOnly = false;
            //    document.getElementById("drpFinYear3").readOnly = false;
            //    document.getElementById("txtAnnual1").readOnly = false;
            //    document.getElementById("txtAnnual2").readOnly = false;
            //    document.getElementById("txtAnnual3").readOnly = false;
            //    document.getElementById("txtProfit1").readOnly = false;
            //    document.getElementById("txtProfit2").readOnly = false;
            //    document.getElementById("txtProfit3").readOnly = false;
            //    document.getElementById("txtReserve1").readOnly = false;
            //    document.getElementById("txtReserve2").readOnly = false;
            //    document.getElementById("txtReserve3").readOnly = false;
            //    document.getElementById("txtShare1").readOnly = false;
            //    document.getElementById("txtShare2").readOnly = false;
            //    document.getElementById("txtShare3").readOnly = false;
            //    document.getElementById("txtNtWorth3").readOnly = false;
            //    document.getElementById("txtNtWorth1").readOnly = false;
            //    document.getElementById("txtNtWorth2").readOnly = false;
            //    $('#adt2').show();
            //    $('#adt1').show();
            //    $('#dv123').show();
            //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
            //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
            //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
            //    $('#lblf4').show();
            //    $('#dvfy4').hide();
            //    $('#dvShareCapital').show();
            //    $('#dvReserveAndSurplus').show();
               
            //}
            //else {
            //    $("#DvtxtPromoter").hide();
            //    $("#dvNm").hide();
            //    $("#DvtxtNoOfParter").hide();
            //    $("#DvtxtNameOfMP").hide();
            //    $("#DvDirectorww").hide();
            //    $("#GrdBD1").hide();
            //    $("#dvPromoterNM1").hide();
            //    $("#DvOths").hide();
            //    $("#DVC3").show();
            //    $("#DVC1").show();
            //    $("#DVC2").show();
            //    $("#DVC4").show();
            //    $('#TgToo').hide();
            //    document.getElementById("drpFinYear1").disabled = false;
            //    document.getElementById("drpFinYear2").readOnly = false;
            //    document.getElementById("drpFinYear3").readOnly = false;
            //    document.getElementById("txtAnnual1").readOnly = false;
            //    document.getElementById("txtAnnual2").readOnly = false;
            //    document.getElementById("txtAnnual3").readOnly = false;
            //    document.getElementById("txtProfit1").readOnly = false;
            //    document.getElementById("txtProfit2").readOnly = false;
            //    document.getElementById("txtProfit3").readOnly = false;
            //    document.getElementById("txtReserve1").readOnly = false;
            //    document.getElementById("txtReserve2").readOnly = false;
            //    document.getElementById("txtReserve3").readOnly = false;
            //    document.getElementById("txtShare1").readOnly = false;
            //    document.getElementById("txtShare2").readOnly = false;
            //    document.getElementById("txtShare3").readOnly = false;
            //    document.getElementById("txtNtWorth3").readOnly = false;
            //    document.getElementById("txtNtWorth1").readOnly = false;
            //    document.getElementById("txtNtWorth2").readOnly = false;
            //    $('#adt2').show();
            //    $('#adt1').show();
            //    $('#dv123').show();
            //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
            //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
            //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
            //    $('#lblf4').show();
            //    $('#dvfy4').hide();
            //    $('#dvShareCapital').show();
            //    $('#dvReserveAndSurplus').show();
                
            //}

         

           
            /*---------------------------------------------------------------------------------*/
            /////// Constitution/Enterprise Change Event
            /*---------------------------------------------------------------------------------*/
            $("#ddlConstitution").change(function () {
                debugger;
                EnableAndDiseableBasedOnddlConstitution($(this).val());
                ////alert("Change Event");
                //if ($(this).val() == "1") {
                //    $("#lblf1").text("Networth Certificate of the Proprietor duly certified by CA for Current/latest year.");
                //    $("#lblf2").text("Tax Audit Report(if applicable) for Current/latest year.");
                //    $("#lblf3").text("Income tax return for Current/latest year.");
                //    $('#lblf4').hide();
                //    $("#lblDet").text('3.Promoter Details');
                //    $("#DvtxtPromoter").show();
                //    $("#dvNm").show();
                //    $("#DvtxtNoOfParter").hide();
                //    $("#DvtxtNameOfMP").hide();
                //    $("#DvDirectorww").hide();
                //    $("#GrdBD1").hide();
                //    $("#dvPromoterNM1").show();
                //    $("#DvOths").hide();
                //    $("#DVC1").hide();
                //    $("#DVC2").hide();
                //    $("#DVC3").hide();
                //    $("#DVC4").hide();
                //    $('#TgToo').hide();
                //    document.getElementById("drpFinYear1").disabled = true;
                //    document.getElementById("drpFinYear1").value = "0";
                //    document.getElementById("drpFinYear2").value = "0";
                //    document.getElementById("drpFinYear3").value = "0";
                //    document.getElementById("txtAnnual1").readOnly = true;
                //    document.getElementById("txtAnnual2").readOnly = true;
                //    document.getElementById("txtAnnual3").readOnly = true;
                //    document.getElementById("txtProfit1").readOnly = true;
                //    document.getElementById("txtProfit2").readOnly = true;
                //    document.getElementById("txtProfit3").readOnly = true;
                //    document.getElementById("txtReserve1").readOnly = true;
                //    document.getElementById("txtReserve2").readOnly = true;
                //    document.getElementById("txtReserve3").readOnly = true;
                //    document.getElementById("txtShare1").readOnly = true;
                //    document.getElementById("txtShare2").readOnly = true;
                //    document.getElementById("txtShare3").readOnly = true;
                //    document.getElementById("txtNtWorth3").readOnly = true;
                //    document.getElementById("txtNtWorth1").readOnly = true;
                //    document.getElementById("txtNtWorth2").readOnly = true;
                //    $('#adt2').show();
                //    $('#adt1').show(); ////// sushant
                //    if ($('#drpApplicationFor').val() == "1") {
                //        $('#adt1').hide();
                //    }
                //    $('#dv123').show();
                //    document.getElementById("drpYearofEstablishment").value = "0"; //// Added by Sushant Jena on Dt. 13-Aug-2019                   
                //    document.getElementById("txtAnnual1").value = "0";
                //    document.getElementById("txtAnnual2").value = "0";
                //    document.getElementById("txtAnnual3").value = "0";
                //    document.getElementById("txtProfit1").value = "0";
                //    document.getElementById("txtProfit2").value = "0";
                //    document.getElementById("txtProfit3").value = "0";
                //    document.getElementById("txtReserve1").value = "0";
                //    document.getElementById("txtReserve2").value = "0";
                //    document.getElementById("txtReserve3").value = "0";
                //    document.getElementById("txtShare1").value = "0";
                //    document.getElementById("txtShare2").value = "0";
                //    document.getElementById("txtShare3").value = "0";
                //    document.getElementById("txtNtWorth3").value = "0";
                //    document.getElementById("txtNtWorth1").value = "0";
                //    document.getElementById("txtNtWorth2").value = "0";
                //    $('#dvfy4').hide();
                //    $('#dvShareCapital').show();
                //    $('#dvReserveAndSurplus').show();
                    
                //}
                //else if ($(this).val() == "2") {
                //    $("#lblDet").text('3.Partnership Details');
                //    $("#DvtxtNoOfParter").show();
                //    $("#DvtxtNameOfMP").show();
                //    $("#DvtxtPromoter").hide();
                //    $("#dvNm").hide();
                //    $("#DvDirectorww").hide();
                //    $("#GrdBD1").hide();
                //    $("#dvPromoterNM1").show();
                //    $("#DvOths").hide();
                //    $("#DVC1").show();
                //    $("#DVC2").hide();
                //    $("#DVC3").hide();
                //    $("#DVC4").show();
                //    $('#TgToo').hide();

                //    document.getElementById("drpFinYear1").disabled = false;
                //    document.getElementById("drpFinYear2").readOnly = false;
                //    document.getElementById("drpFinYear3").readOnly = false;

                //    document.getElementById("txtAnnual1").readOnly = false;
                //    document.getElementById("txtAnnual2").readOnly = false;
                //    document.getElementById("txtAnnual3").readOnly = false;
                //    document.getElementById("txtProfit1").readOnly = false;
                //    document.getElementById("txtProfit2").readOnly = false;
                //    document.getElementById("txtProfit3").readOnly = false;
                //    document.getElementById("txtReserve1").readOnly = false;
                //    document.getElementById("txtReserve2").readOnly = false;
                //    document.getElementById("txtReserve3").readOnly = false;
                //    document.getElementById("txtShare1").readOnly = false;
                //    document.getElementById("txtShare2").readOnly = false;
                //    document.getElementById("txtShare3").readOnly = false;
                //    document.getElementById("txtNtWorth3").readOnly = false;
                //    document.getElementById("txtNtWorth1").readOnly = false;
                //    document.getElementById("txtNtWorth2").readOnly = false;

                //    document.getElementById("txtReserve1").readOnly = true;
                //    document.getElementById("txtReserve2").readOnly = true;
                //    document.getElementById("txtReserve3").readOnly = true;
                //    document.getElementById("txtShare1").readOnly = true;
                //    document.getElementById("txtShare2").readOnly = true;
                //    document.getElementById("txtShare3").readOnly = true;

                //    $('#adt1').show();
                //    $('#adt2').show(); ////// sushant
                //    if ($('#drpApplicationFor').val() == "1") {
                //        $('#adt2').hide();
                //    }
                //    $('#dv123').show();

                //    $("#lblf1").text("Partnership deed.");
                //    $("#lblf2").text("Complete balance sheet of the firm(latest 3 years).");
                //    $("#lblf3").text("Tax audit report of the Partnership firm.");
                //    $('#lblf4').hide();
                //    $('#dvfy4').show();
                //    $('#dvShareCapital').hide();
                //    $('#dvReserveAndSurplus').hide();
                //}
                //else if ($(this).val() == "8") {
                //    $("#lblDet").text('3.Board Of Directors');
                //    $("#DvtxtPromoter").hide();
                //    $("#dvNm").hide();
                //    $("#DvtxtNoOfParter").hide();
                //    $("#DvtxtNameOfMP").hide();
                //    $("#DvDirectorww").show();
                //    $("#GrdBD1").show();
                //    $("#dvPromoterNM1").show();
                //    $("#DvOths").show();
                //    $("#DVC1").show();
                //    $("#DVC2").show();
                //    $("#DVC3").show();
                //    $("#DVC4").show();
                //    $('#TgToo').show();
                //    document.getElementById("drpFinYear1").disabled = false;
                //    document.getElementById("drpFinYear2").readOnly = false;
                //    document.getElementById("drpFinYear3").readOnly = false;
                //    document.getElementById("txtAnnual1").readOnly = false;
                //    document.getElementById("txtAnnual2").readOnly = false;
                //    document.getElementById("txtAnnual3").readOnly = false;
                //    document.getElementById("txtProfit1").readOnly = false;
                //    document.getElementById("txtProfit2").readOnly = false;
                //    document.getElementById("txtProfit3").readOnly = false;
                //    document.getElementById("txtReserve1").readOnly = false;
                //    document.getElementById("txtReserve2").readOnly = false;
                //    document.getElementById("txtReserve3").readOnly = false;
                //    document.getElementById("txtShare1").readOnly = false;
                //    document.getElementById("txtShare2").readOnly = false;
                //    document.getElementById("txtShare3").readOnly = false;
                //    document.getElementById("txtNtWorth3").readOnly = false;
                //    document.getElementById("txtNtWorth1").readOnly = false;
                //    document.getElementById("txtNtWorth2").readOnly = false;
                //    $('#adt2').show();
                //    $('#adt1').show();
                //    $('#dv123').show();
                //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //    $('#lblf4').show();
                //    $('#dvfy4').hide();
                //    $('#dvShareCapital').show();
                //    $('#dvReserveAndSurplus').show();
                //}
                //else if ($(this).val() == "3") {
                //    $("#lblDet").text('3.Board Of Directors');
                //    $("#DvtxtPromoter").hide();
                //    $("#dvNm").hide();
                //    $("#DvtxtNoOfParter").hide();
                //    $("#DvtxtNameOfMP").hide();
                //    $("#DvDirectorww").show();
                //    $("#GrdBD1").show();
                //    $("#dvPromoterNM1").show();
                //    $("#DvOths").hide();
                //    $("#DVC1").show();
                //    $("#DVC2").show();
                //    $("#DVC3").show();
                //    $("#DVC4").show();
                //    $('#TgToo').show();
                //    document.getElementById("drpFinYear1").disabled = false;
                //    document.getElementById("drpFinYear2").readOnly = false;
                //    document.getElementById("drpFinYear3").readOnly = false;
                //    document.getElementById("txtAnnual1").readOnly = false;
                //    document.getElementById("txtAnnual2").readOnly = false;
                //    document.getElementById("txtAnnual3").readOnly = false;
                //    document.getElementById("txtProfit1").readOnly = false;
                //    document.getElementById("txtProfit2").readOnly = false;
                //    document.getElementById("txtProfit3").readOnly = false;
                //    document.getElementById("txtReserve1").readOnly = false;
                //    document.getElementById("txtReserve2").readOnly = false;
                //    document.getElementById("txtReserve3").readOnly = false;
                //    document.getElementById("txtShare1").readOnly = false;
                //    document.getElementById("txtShare2").readOnly = false;
                //    document.getElementById("txtShare3").readOnly = false;
                //    document.getElementById("txtNtWorth3").readOnly = false;
                //    document.getElementById("txtNtWorth1").readOnly = false;
                //    document.getElementById("txtNtWorth2").readOnly = false;
                //    $('#adt2').show();
                //    $('#adt1').show();
                //    $('#dv123').show();
                //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //    $('#lblf4').show();
                //    $('#dvfy4').hide();
                //    $('#dvShareCapital').show();
                //    $('#dvReserveAndSurplus').show();
                //}
                //else if ($(this).val() == "4") {
                //    $("#lblDet").text('3.Board Of Directors');
                //    $("#DvtxtPromoter").hide();
                //    $("#dvNm").hide();
                //    $("#DvtxtNoOfParter").hide();
                //    $("#DvtxtNameOfMP").hide();
                //    $("#DvDirectorww").show();
                //    $("#GrdBD1").show();
                //    $("#dvPromoterNM1").show();
                //    $("#DvOths").hide();
                //    $("#DVC1").show();
                //    $("#DVC2").show();
                //    $("#DVC3").show();
                //    $("#DVC4").show();
                //    $('#TgToo').show();
                //    document.getElementById("drpFinYear1").disabled = false;
                //    document.getElementById("drpFinYear2").readOnly = false;
                //    document.getElementById("drpFinYear3").readOnly = false;
                //    document.getElementById("txtAnnual1").readOnly = false;
                //    document.getElementById("txtAnnual2").readOnly = false;
                //    document.getElementById("txtAnnual3").readOnly = false;
                //    document.getElementById("txtProfit1").readOnly = false;
                //    document.getElementById("txtProfit2").readOnly = false;
                //    document.getElementById("txtProfit3").readOnly = false;
                //    document.getElementById("txtReserve1").readOnly = false;
                //    document.getElementById("txtReserve2").readOnly = false;
                //    document.getElementById("txtReserve3").readOnly = false;
                //    document.getElementById("txtShare1").readOnly = false;
                //    document.getElementById("txtShare2").readOnly = false;
                //    document.getElementById("txtShare3").readOnly = false;
                //    document.getElementById("txtNtWorth3").readOnly = false;
                //    document.getElementById("txtNtWorth1").readOnly = false;
                //    document.getElementById("txtNtWorth2").readOnly = false;
                //    $('#adt2').show();
                //    $('#adt1').show();
                //    $('#dv123').show();
                //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //    $('#lblf4').show();
                //    $('#dvfy4').hide();
                //    $('#dvShareCapital').show();
                //    $('#dvReserveAndSurplus').show();
                //}
                //else if ($(this).val() == "5") {
                //    $("#lblDet").text('3.Board Of Directors');
                //    $("#DvtxtPromoter").hide();
                //    $("#dvNm").hide();
                //    $("#DvtxtNoOfParter").hide();
                //    $("#DvtxtNameOfMP").hide();
                //    $("#DvDirectorww").show();
                //    $("#GrdBD1").show();
                //    $("#dvPromoterNM1").show();
                //    $("#DvOths").hide();
                //    $("#DVC1").show();
                //    $("#DVC2").show();
                //    $("#DVC3").show();
                //    $("#DVC4").show();
                //    $('#TgToo').show();
                //    document.getElementById("drpFinYear1").disabled = false;
                //    document.getElementById("drpFinYear2").readOnly = false;
                //    document.getElementById("drpFinYear3").readOnly = false;
                //    document.getElementById("txtAnnual1").readOnly = false;
                //    document.getElementById("txtAnnual2").readOnly = false;
                //    document.getElementById("txtAnnual3").readOnly = false;
                //    document.getElementById("txtProfit1").readOnly = false;
                //    document.getElementById("txtProfit2").readOnly = false;
                //    document.getElementById("txtProfit3").readOnly = false;
                //    document.getElementById("txtReserve1").readOnly = false;
                //    document.getElementById("txtReserve2").readOnly = false;
                //    document.getElementById("txtReserve3").readOnly = false;
                //    document.getElementById("txtShare1").readOnly = false;
                //    document.getElementById("txtShare2").readOnly = false;
                //    document.getElementById("txtShare3").readOnly = false;
                //    document.getElementById("txtNtWorth3").readOnly = false;
                //    document.getElementById("txtNtWorth1").readOnly = false;
                //    document.getElementById("txtNtWorth2").readOnly = false;
                //    $('#adt2').show();
                //    $('#adt1').show();
                //    $('#dv123').show();
                //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //    $('#lblf4').show();
                //    $('#dvfy4').hide();
                //    $('#dvShareCapital').show();
                //    $('#dvReserveAndSurplus').show();
                //}
                //else if ($(this).val() == "6") {
                //    $("#lblDet").text('3.Board Of Directors');
                //    $("#DvtxtPromoter").hide();
                //    $("#dvNm").hide();
                //    $("#DvtxtNoOfParter").hide();
                //    $("#DvtxtNameOfMP").hide();
                //    $("#DvDirectorww").show();
                //    $("#GrdBD1").show();
                //    $("#dvPromoterNM1").show();
                //    $("#DvOths").hide();
                //    $("#DVC3").hide();
                //    $("#DVC1").show();
                //    $("#DVC2").show();
                //    $("#DVC4").show();
                //    $('#TgToo').show();
                //    document.getElementById("drpFinYear1").disabled = false;
                //    document.getElementById("drpFinYear2").readOnly = false;
                //    document.getElementById("drpFinYear3").readOnly = false;
                //    document.getElementById("txtAnnual1").readOnly = false;
                //    document.getElementById("txtAnnual2").readOnly = false;
                //    document.getElementById("txtAnnual3").readOnly = false;
                //    document.getElementById("txtProfit1").readOnly = false;
                //    document.getElementById("txtProfit2").readOnly = false;
                //    document.getElementById("txtProfit3").readOnly = false;
                //    document.getElementById("txtReserve1").readOnly = false;
                //    document.getElementById("txtReserve2").readOnly = false;
                //    document.getElementById("txtReserve3").readOnly = false;
                //    document.getElementById("txtShare1").readOnly = false;
                //    document.getElementById("txtShare2").readOnly = false;
                //    document.getElementById("txtShare3").readOnly = false;
                //    document.getElementById("txtNtWorth3").readOnly = false;
                //    document.getElementById("txtNtWorth1").readOnly = false;
                //    document.getElementById("txtNtWorth2").readOnly = false;
                //    $('#adt2').show();
                //    $('#adt1').show();
                //    $('#dv123').show();
                //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //    $('#lblf4').show();
                //    $('#dvfy4').hide();
                //    $('#dvShareCapital').show();
                //    $('#dvReserveAndSurplus').show();
                //}
                //else if ($(this).val() == "7") {
                //    $("#lblDet").text('3.Board Of Directors');
                //    $("#DvtxtPromoter").hide();
                //    $("#dvNm").hide();
                //    $("#DvtxtNoOfParter").hide();
                //    $("#DvtxtNameOfMP").hide();
                //    $("#DvDirectorww").show();
                //    $("#GrdBD1").show();
                //    $("#dvPromoterNM1").show();
                //    $("#DvOths").hide();
                //    $("#DVC3").hide();
                //    $("#DVC1").show();
                //    $("#DVC2").show();
                //    $("#DVC4").show();
                //    $('#TgToo').show();
                //    document.getElementById("drpFinYear1").disabled = false;
                //    document.getElementById("drpFinYear2").readOnly = false;
                //    document.getElementById("drpFinYear3").readOnly = false;
                //    document.getElementById("txtAnnual1").readOnly = false;
                //    document.getElementById("txtAnnual2").readOnly = false;
                //    document.getElementById("txtAnnual3").readOnly = false;
                //    document.getElementById("txtProfit1").readOnly = false;
                //    document.getElementById("txtProfit2").readOnly = false;
                //    document.getElementById("txtProfit3").readOnly = false;
                //    document.getElementById("txtReserve1").readOnly = false;
                //    document.getElementById("txtReserve2").readOnly = false;
                //    document.getElementById("txtReserve3").readOnly = false;
                //    document.getElementById("txtShare1").readOnly = false;
                //    document.getElementById("txtShare2").readOnly = false;
                //    document.getElementById("txtShare3").readOnly = false;
                //    document.getElementById("txtNtWorth3").readOnly = false;
                //    document.getElementById("txtNtWorth1").readOnly = false;
                //    document.getElementById("txtNtWorth2").readOnly = false;
                //    $('#adt2').show();
                //    $('#adt1').show();
                //    $('#dv123').show();
                //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //    $('#lblf4').show();
                //    $('#dvfy4').hide();
                //    $('#dvShareCapital').show();
                //    $('#dvReserveAndSurplus').show();
                //}
                //else {
                //    $("#DvtxtPromoter").hide();
                //    $("#dvNm").hide();
                //    $("#DvtxtNoOfParter").hide();
                //    $("#DvtxtNameOfMP").hide();
                //    $("#DvDirectorww").hide();
                //    $("#GrdBD1").hide();
                //    $("#dvPromoterNM1").hide();
                //    $("#DvOths").hide();
                //    $("#DVC3").show();
                //    $("#DVC1").show();
                //    $("#DVC2").show();
                //    $("#DVC4").show();
                //    $('#TgToo').show();
                //    document.getElementById("drpFinYear1").disabled = false;
                //    document.getElementById("drpFinYear2").readOnly = false;
                //    document.getElementById("drpFinYear3").readOnly = false;
                //    document.getElementById("txtAnnual1").readOnly = false;
                //    document.getElementById("txtAnnual2").readOnly = false;
                //    document.getElementById("txtAnnual3").readOnly = false;
                //    document.getElementById("txtProfit1").readOnly = false;
                //    document.getElementById("txtProfit2").readOnly = false;
                //    document.getElementById("txtProfit3").readOnly = false;
                //    document.getElementById("txtReserve1").readOnly = false;
                //    document.getElementById("txtReserve2").readOnly = false;
                //    document.getElementById("txtReserve3").readOnly = false;
                //    document.getElementById("txtShare1").readOnly = false;
                //    document.getElementById("txtShare2").readOnly = false;
                //    document.getElementById("txtShare3").readOnly = false;
                //    document.getElementById("txtNtWorth3").readOnly = false;
                //    document.getElementById("txtNtWorth1").readOnly = false;
                //    document.getElementById("txtNtWorth2").readOnly = false;
                //    $('#adt2').show();
                //    $('#adt1').show();
                //    $('#dv123').show();
                //    $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                //    $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                //    $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //    $('#lblf4').show();
                //    $('#dvfy4').hide();
                //    $('#dvShareCapital').show();
                //    $('#dvReserveAndSurplus').show();
                //}
            });

            /*---------------------------------------------------------------------------------*/

            if ($("#drpApplicationFor").val() == "2") {
                $('#DvIndustry').show();
                $('#DvIndustry1').show();
            }
            else {
                $('#DvIndustry').hide();
                $('#DvIndustry1').hide();
            }

            /*---------------------------------------------------------------------------------*/
            ///// Application For Change Event
            /*---------------------------------------------------------------------------------*/
            $("#drpApplicationFor").change(function () {
                var j = $("#drpProjectCat").val();
                if ($(this).val() == "2") {
                    $('#DvIndustry').show();
                    $('#DvIndustry1').show();

                    if ($("#ddlConstitution").val() == "1") {//// Proprietorship
                        $('#adt1').show();
                    }
                    else if ($("#ddlConstitution").val() == "2") {//// Partnership
                        $('#adt2').show();
                    }
                }
                else if ($(this).val() == "1") {
                    if (j == "2") {
                        $("#DvNetWorth").show();
                    }
                    $('#DvIndustry').hide();
                    $('#DvIndustry1').hide();

                    if ($("#ddlConstitution").val() == "1") {//// Proprietorship
                        $('#adt1').hide();
                    }
                    else if ($("#ddlConstitution").val() == "2") {//// Partnership
                        $('#adt2').hide();
                    }
                }
                else {
                    $('#DvIndustry').hide();
                    $('#DvIndustry1').hide();
                }
            });

            /*---------------------------------------------------------------------------------*/

            if ($("#drpProjectCat").val() == "2") {
                var n = $("#drpApplicationFor").val();
                $("#DvEdu").show();
                $('#Enc').show();
                $("#DvEnclos2").show();
                $('#DvRaw').show();
                if (n == "1") {
                    $("#DvNetWorth").show();
                }
            }
            else {
                $("#DvEdu").hide();
                $('#Enc').hide();
                $("#DvEnclos2").hide();
                $('#DvRaw').hide();
                $("#DvNetWorth").hide();
            }

            /*---------------------------------------------------------------------------------*/

            $("#drpProjectCat").change(function () {
                if ($(this).val() == "2") {
                    var k = $("#drpApplicationFor").val();
                    $("#DvEdu").show();
                    $('#Enc').show();
                    $("#DvEnclos2").show();
                    $('#DvRaw').show();
                    if (k == "1") {
                        $("#DvNetWorth").show();
                    }
                }
                else {
                    $("#DvEdu").hide();
                    $('#Enc').hide();
                    $("#DvEnclos2").hide();
                    $('#DvRaw').hide();
                    $("#DvNetWorth").hide();
                }
            });

            /*---------------------------------------------------------------------------------*/

            $("#divLD").hide();
            $("#divLD1").hide();
            $("#divLD").hide();
            $("#divLDGRID").hide();
            if (($('#ddlAlloted').val() == "1")) {
                $("#divLD").show();
                $("#divLD1").show();
            }
            else {

                $("#divLD").hide();
                $("#divLD1").hide();
            }

            /*---------------------------------------------------------------------------------*/

            $('#ddlAlloted').change(function () {
                if (($(this).val() == "1")) {
                    $("#divLD").show();
                    $("#divLD1").show();
                }
                else {

                    $("#divLD").hide();
                    $("#divLD1").hide();
                }
            });

            /*---------------------------------------------------------------------------------*/
            debugger;

            var val = $("#drpYearofEstablishment").val();
            if (val == "") {
                val == 0;
            }
            if (isNaN(parseInt(val))) {
                val = 0;
            }
            var noyr = 0;

            if (val.length == 4) {
                var dt = new Date();
                if (val > dt.getFullYear()) {
                    jAlert('<strong>Year of Establishment does not accept future Year</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
                var month = parseInt(dt.getMonth()) + 1;
                if (month > 3)// from APRIL 2023-24
                {
                    noyr = parseInt(dt.getFullYear()) - parseInt(val);
                    if (parseInt(noyr) == 0) {
                        $("#drpFinYear1").prop("disabled", true);

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;

                        //$('#adt1').hide();                    
                        //$('#adt2').hide();
                        //$('#dv123').show();

                        // $('#adt1').show();
                        $('#adt1').hide();
                        if ($('#drpApplicationFor').val() == "1") { //// Added by Sushant Jena
                            $('#adt1').hide();
                        }
                        $('#adt2').hide();
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 1) {
                        $("#drpFinYear1").prop("disabled", false);

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;

                        //$('#adt1').hide();                    
                        //$('#adt2').hide();
                        //$('#dv123').show();

                       // $('#adt1').show();
                        $('#adt1').hide();
                        if ($('#drpApplicationFor').val() == "1") { //// Added by Sushant Jena
                            $('#adt1').hide();
                        }
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 2) {
                        $("#drpFinYear1").prop("disabled", false);

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;

                        //$('#adt1').show();
                        //$('#dv123').show();
                        //$('#adt2').hide();

                        $('#adt1').show();
                        $('#dv123').show();
                        //$('#adt2').show();
                        $('#adt2').hide()
                        if ($('#drpApplicationFor').val() == "1") { //// Added by Sushant Jena
                            $('#adt2').hide();
                        }


                    }
                    else {

                        $("#drpFinYear1").prop("disabled", false);

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();
                    }

                }
                else // upto  march 2022-23
                {
                    noyr = (parseInt(dt.getFullYear())) - parseInt(val);//2023-2022=1

                    if (parseInt(noyr)==1) {
                        $("#drpFinYear1").prop("disabled", true);

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;

                        //$('#adt1').hide();                    
                        //$('#adt2').hide();
                        //$('#dv123').show();


                        //  $('#adt1').show();
                        $('#adt1').hide();
                        if ($('#drpApplicationFor').val() == "1") { //// Added by Sushant Jena
                            $('#adt1').hide();
                        }
                        $('#adt2').hide();
                        $('#dv123').hide();
                    }

                    else if (parseInt(noyr) == 2) {
                        $("#drpFinYear1").prop("disabled", false);

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;

                        //$('#adt1').hide();                    
                        //$('#adt2').hide();
                        //$('#dv123').show();


                      //  $('#adt1').show();
                        $('#adt1').hide();
                        if ($('#drpApplicationFor').val() == "1") { //// Added by Sushant Jena
                            $('#adt1').hide();
                        }
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 3) {
                        $("#drpFinYear1").prop("disabled", false);

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;

                        //$('#adt1').show();
                        //$('#dv123').show();
                        //$('#adt2').hide();

                        $('#adt1').show();
                        $('#dv123').show();
                        //$('#adt2').show();
                        $('#adt2').hide();
                        if ($('#drpApplicationFor').val() == "1") { //// Added by Sushant Jena
                            $('#adt2').hide();
                        }
                    }
                    else {
                        $("#drpFinYear1").prop("disabled", false);

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();
                    }

                }






                //noyr = parseInt(dt.getFullYear()) - parseInt(val);

                //if (val > dt.getFullYear()) {
                //    jAlert('<strong>Year of Establishment does not accept future Year</strong>', projname);
                //    $(this).focus();
                //    $(this).val('');
                //    return false;
                //}
                //else if (parseInt(val) == parseInt(dt.getFullYear())) {

                //    $("#drpFinYear1").prop("disabled", true);
                //    document.getElementById("drpFinYear1").value = "0";
                //    document.getElementById("drpFinYear2").value = "0";
                //    document.getElementById("drpFinYear3").value = "0";

                //    document.getElementById("txtAnnual1").readOnly = true;
                //    document.getElementById("txtAnnual2").readOnly = true;
                //    document.getElementById("txtAnnual3").readOnly = true;
                //    document.getElementById("txtProfit1").readOnly = true;
                //    document.getElementById("txtProfit2").readOnly = true;
                //    document.getElementById("txtProfit3").readOnly = true;
                //    document.getElementById("txtReserve1").readOnly = true;
                //    document.getElementById("txtReserve2").readOnly = true;
                //    document.getElementById("txtReserve3").readOnly = true;
                //    document.getElementById("txtShare1").readOnly = true;
                //    document.getElementById("txtShare2").readOnly = true;
                //    document.getElementById("txtShare3").readOnly = true;
                //    document.getElementById("txtNtWorth3").readOnly = true;
                //    document.getElementById("txtNtWorth1").readOnly = true;
                //    document.getElementById("txtNtWorth2").readOnly = true;
                //    $('#adt1').hide();
                //    $('#adt2').hide();
                //    $('#dv123').hide();
                //}
                //else if (parseInt(noyr) == 1) {

                //    $("#drpFinYear1").prop("disabled", false);

                //    document.getElementById("txtAnnual2").readOnly = true;
                //    document.getElementById("txtAnnual3").readOnly = true;
                //    document.getElementById("txtProfit2").readOnly = true;
                //    document.getElementById("txtProfit3").readOnly = true;
                //    document.getElementById("txtReserve2").readOnly = true;
                //    document.getElementById("txtReserve3").readOnly = true;
                //    document.getElementById("txtShare2").readOnly = true;
                //    document.getElementById("txtShare3").readOnly = true;
                //    document.getElementById("txtAnnual1").readOnly = false;
                //    document.getElementById("txtProfit1").readOnly = false;
                //    document.getElementById("txtReserve1").readOnly = false;
                //    document.getElementById("txtShare1").readOnly = false;
                //    document.getElementById("txtNtWorth1").readOnly = false;
                //    document.getElementById("txtNtWorth2").readOnly = true;
                //    document.getElementById("txtNtWorth3").readOnly = true;

                //    //$('#adt1').hide();                    
                //    //$('#adt2').hide();
                //    //$('#dv123').show();

                //    $('#adt1').show();
                //    if ($('#drpApplicationFor').val() == "1") { //// Added by Sushant Jena
                //        $('#adt1').hide();
                //    }
                //    $('#adt2').hide();
                //    $('#dv123').show();
                //}
                //else if (parseInt(noyr) == 2) {
                //    $("#drpFinYear1").prop("disabled", false);

                //    document.getElementById("txtAnnual2").readOnly = false;
                //    document.getElementById("txtAnnual3").readOnly = true;
                //    document.getElementById("txtProfit2").readOnly = false;
                //    document.getElementById("txtProfit3").readOnly = true;
                //    document.getElementById("txtReserve2").readOnly = false;
                //    document.getElementById("txtReserve3").readOnly = true;
                //    document.getElementById("txtShare2").readOnly = false;
                //    document.getElementById("txtShare3").readOnly = true;
                //    document.getElementById("drpFinYear1").readOnly = false;
                //    document.getElementById("txtAnnual1").readOnly = false;
                //    document.getElementById("txtProfit1").readOnly = false;
                //    document.getElementById("txtReserve1").readOnly = false;
                //    document.getElementById("txtShare1").readOnly = false;
                //    document.getElementById("txtNtWorth3").readOnly = true;
                //    document.getElementById("txtNtWorth2").readOnly = false;
                //    document.getElementById("txtNtWorth1").readOnly = false;

                //    //$('#adt1').show();
                //    //$('#dv123').show();
                //    //$('#adt2').hide();

                //    $('#adt1').show();
                //    $('#dv123').show();
                //    $('#adt2').show();
                //    if ($('#drpApplicationFor').val() == "1") { //// Added by Sushant Jena
                //        $('#adt2').hide();
                //    }
                //}
                //else {

                //    $("#drpFinYear1").prop("disabled", false);

                //    document.getElementById("txtAnnual1").readOnly = false;
                //    document.getElementById("txtAnnual2").readOnly = false;
                //    document.getElementById("txtAnnual3").readOnly = false;
                //    document.getElementById("txtProfit1").readOnly = false;
                //    document.getElementById("txtProfit2").readOnly = false;
                //    document.getElementById("txtProfit3").readOnly = false;
                //    document.getElementById("txtReserve1").readOnly = false;
                //    document.getElementById("txtReserve2").readOnly = false;
                //    document.getElementById("txtReserve3").readOnly = false;
                //    document.getElementById("txtShare1").readOnly = false;
                //    document.getElementById("txtShare2").readOnly = false;
                //    document.getElementById("txtShare3").readOnly = false;
                //    document.getElementById("txtNtWorth3").readOnly = false;
                //    document.getElementById("txtNtWorth1").readOnly = false;
                //    document.getElementById("txtNtWorth2").readOnly = false;
                //    $('#adt2').show();
                //    $('#adt1').show();
                //    $('#dv123').show();
                //}
            }

            $('#dv123').show();

            /*---------------------------------------------------------------------------------*/
            ////// Button Save Validation
            /*---------------------------------------------------------------------------------*/
            $('#btnNext').click(function () {

                if (blankFieldValidation('txtIName', 'Name of the Company/Enterprise', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtIName', 'Name of the Company/Enterprise', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtIName', 'Name of the Company/Enterprise', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtIName', 'Name of the Company/Enterprise', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('txtAddress', 'Address', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtAddress', 'Address', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtAddress', 'Address', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtAddress', 'Address', projname) == false) {
                    return false;
                }
                if (DropDownValidation('ddlCountry', '0', 'Country', projname) == false) {
                    $("#ddlCountry").focus();
                    return false;
                }
                if (DropDownValidation('ddlState', '0', 'State', projname) == false) {
                    $("#ddlState").focus();
                    return false;
                }

                if (blankFieldValidation('txtCity', 'City', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtCity', 'City', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtCity', 'City', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtCity', 'City', projname) == false) {
                    return false;
                }

                //                if (blankFieldValidation('txtPhoneNo', 'Phone number', projname) == false) {
                //                    return false;
                //                }
                if (WhiteSpaceValidation1st('txtPhoneNo', 'Phone number', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtPhoneNo', 'Phone number', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtPhoneNo', 'Phone number', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtFaxNo', 'Fax number', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtFaxNo', 'Fax number', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtFaxNo', 'Fax number', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtEmail', 'Email Address', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtEmail', 'Email Address', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtEmail', 'Email Address', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtEmail', 'Email Address', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('txtPinCode', 'PIN Code', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtPinCode', 'PIN Code', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtPinCode', 'PIN Code', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtPinCode', 'PIN Code', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('txtCperson', 'Name of the Contact Person', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtCperson', 'Name of the Contact Person', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtCperson', 'Name of the Contact Person', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtCperson', 'Name of the Contact Person', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('txtCorAddrs', 'Address', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtCorAddrs', 'Address', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtCorAddrs', 'Address', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtCorAddrs', 'Address', projname) == false) {
                    return false;
                }
                if (DropDownValidation('drpCorCountry', '0', 'Country', projname) == false) {
                    $("#ddlCountry").focus();
                    return false;
                }
                if (DropDownValidation('drpCorState', '0', 'State', projname) == false) {
                    $("#ddlState").focus();
                    return false;
                }

                if (blankFieldValidation('txtCorCity', 'City', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtCorCity', 'City', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtCorCity', 'City', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtCorCity', 'City', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('txtCorMob', 'Mobile number', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtCorMob', 'Mobile number', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtCorMob', 'Mobile number', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtCorMob', 'Mobile number', projname) == false) {
                    return false;
                }

                if (WhiteSpaceValidation1st('txtCorFax', 'Fax number', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtCorFax', 'Fax number', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtCorFax', 'Fax number', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('txtCorEmailid', 'Email Address', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtCorEmailid', 'Email Address', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtCorEmailid', 'Email Address', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtCorEmailid', 'Email Address', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('txtCorPin', 'PIN Code', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('txtCorPin', 'PIN Code', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtCorPin', 'PIN Code', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtCorPin', 'PIN Code', projname) == false) {
                    return false;
                }
                if (DropDownValidation('ddlConstitution', '0', 'Constitution of Company/Enterprise', projname) == false) {
                    $("#ddlConstitution").focus();
                    return false;
                }
                if ($("#ddlConstitution").val() == "8") {
                    if (blankFieldValidation('txtOthrConsti', 'Others (Please specify)', projname) == false) {
                        return false;
                    }
                }
                //                if (($("#ddlConstitution").val() != "1") && ($("#ddlConstitution").val() != "2")) {
                //                 
                //                    if (DropDownValidation('drpYearofEstablishment', '0', 'Year of establishment', projname) == false) {
                //                        $("#ddlConstitution").focus();
                //                        return false;
                //                    }
                //                }
                if ($("#ddlConstitution").val() != "1") {

                    if (DropDownValidation('drpYearofEstablishment', '0', 'Year of establishment', projname) == false) {
                        $("#ddlConstitution").focus();
                        return false;
                    }
                }
                //                if (WhiteSpaceValidation1st('txtyrIncorporation', 'Year of Establishment', projname) == false) {
                //                    return false;
                //                }
                //                if (WhiteSpaceValidationLast('txtyrIncorporation', 'Year of Establishment', projname) == false) {
                //                    return false;
                //                }
                //                if (SpecialCharacter1st('txtyrIncorporation', 'Year of Establishment', projname) == false) {
                //                    return false;
                //                }

                if (WhiteSpaceValidation1st('txtPlaceIncorporation', 'Place of incorporation', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtPlaceIncorporation', 'Place of incorporation', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtPlaceIncorporation', 'Place of incorporation', projname) == false) {
                    return false;
                }

                //                if (blankFieldValidation('txtGSTIN', 'GSTIN', projname) == false) {
                //                    return false;
                //                }
                if (WhiteSpaceValidation1st('txtGSTIN', 'GSTIN', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidationLast('txtGSTIN', 'GSTIN', projname) == false) {
                    return false;
                }
                if (SpecialCharacter1st('txtGSTIN', 'GSTIN', projname) == false) {
                    return false;
                }
                //               Start of Temporary Comment
                if ($('#hdnPanFile').val() == '') {
                    jAlert('<strong>Please upload PAN file</strong>', projname);
                    $("#FileUpldPan").focus();
                    return false;
                }
                //                if (($('#FileUpldGstn').val() == '') && ($('#hdnGstinFile').val() == '')) {
                //                    jAlert('<strong>Please attach GSTIN file</strong>', projname);
                //                    $("#FileUpldGstn").focus();
                //                    return false;
                //                }
                if (($("#ddlConstitution").val() != "1") && ($("#ddlConstitution").val() != "2") && ($("#ddlConstitution").val() != "6") && ($("#ddlConstitution").val() != "7")) {
                    if ($('#hdnMemoFile').val() == '') {
                        jAlert('<strong>Please upload  memorandum & articles of association file</strong>', projname);
                        $("#FileUpldMemo").focus();
                        return false;
                    }
                }
                if ($("#ddlConstitution").val() != "1") {
                    if ($('#hdnCerti').val() == '') {
                        jAlert('<strong>Please upload certificate of incorporation/registration/partnership deed file</strong>', projname);
                        $("#FileUpldCerti").focus();
                        return false;
                    }
                }
                //               End of Temporary Comment
                if (DropDownValidation('drpProjectCat', '0', 'Project type', projname) == false) {
                    $("#drpProjectCat").focus();
                    return false;
                }

                //                if ($("#drpProjectCat").val() == "2") {
                //                        if (blankFieldValidation('txtRawMaterial', 'Raw Material', projname) == false) {
                //                            return false;
                //                        }
                //                        if (DropDownValidation('drprawMeterial', '0', 'Material source', projname) == false) {
                //                            $("#drprawMeterial").focus();
                //                            return false;
                //                        }
                //                }

                if (DropDownValidation('drpApplicationFor', '0', 'Application for', projname) == false) {
                    $("#drpApplicationFor").focus();
                    return false;
                }
                if ($("#ddlConstitution").val() == "2") {
                    if (blankFieldValidation('txtNoOfParter', 'Number of partner(s)', projname) == false) {
                        return false;
                    }
                    if (WhiteSpaceValidation1st('txtNoOfParter', 'Number of partner(s)', projname) == false) {
                        return false;
                    }
                    if (WhiteSpaceValidationLast('txtNoOfParter', 'Number of partner(s)', projname) == false) {
                        return false;
                    }
                    if (SpecialCharacter1st('txtNoOfParter', 'Number of partner(s)', projname) == false) {
                        return false;
                    }

                    if (blankFieldValidation('txtNameOfMP', 'Name of the managing partner', projname) == false) {
                        return false;
                    }
                    if (WhiteSpaceValidation1st('txtNameOfMP', 'Name of the managing partner', projname) == false) {
                        return false;
                    }
                    if (WhiteSpaceValidationLast('txtNameOfMP', 'Name of the managing partner', projname) == false) {
                        return false;
                    }
                    if (SpecialCharacter1st('txtNameOfMP', 'Name of the managing partner', projname) == false) {
                        return false;
                    }
                }
                if (($("#ddlConstitution").val() != "1") && ($("#ddlConstitution").val() != "2")) {
                    var grdDseg1 = $("#<%=GrdBD.ClientID %> tr").length;
                    if (grdDseg1 < 2) {
                        jAlert('<strong>Please provide name and designation of board of directors and click on add button.</strong>', projname);
                        $("#txtPName").focus();
                        return false;
                    }
                }
                if ($("#drpProjectCat").val() == "2") {
                    var grdDseg = $("#<%=GrdBD.ClientID %> tr").length;
                    if (parseInt(grdDseg) > 1) {
                        if (DropDownValidation('drpTagTo', '0', 'Name', projname) == false) {
                            $("#drpTagTo").focus();
                            return false;
                        }
                    }
                    if (DropDownValidation('drpEducation', '0', 'Educational Qualification', projname) == false) {
                        $("#drpEducation").focus();
                        return false;
                    }
                    if (DropDownValidation('drpTechnical', '0', 'Technical Qualification', projname) == false) {
                        $("#drpTechnical").focus();
                        return false;
                    }
                    if (blankFieldValidation('txtexpinYr', 'Experience in Years', projname) == false) {
                        return false;
                    }
                    if (WhiteSpaceValidation1st('txtexpinYr', 'Experience in Years', projname) == false) {
                        return false;
                    }
                    if (WhiteSpaceValidationLast('txtexpinYr', 'Experience in Years', projname) == false) {
                        return false;
                    }
                    if (SpecialCharacter1st('txtexpinYr', 'Experience in Years', projname) == false) {
                        return false;
                    }
                    //               Start of Temporary Comment
                    if ($('#hdnEdu').val() == '') {
                        jAlert('<strong>Please upload educational qualification file</strong>', projname);
                        $("#FileUpldEducational").focus();
                        return false;
                    }
                    var techVal1 = $("#drpTechnical option:selected").text();
                    if (techVal1 != 'NA') {
                        if ($('#hdnTecnical').val() == '') {
                            jAlert('<strong>Please upload qualification file</strong>', projname);
                            $("#FileUpldTechnical").focus();
                            return false;
                        }
                    }
                    if ($('#hdnExperience').val() == '') {
                        jAlert('<strong>Please upload experience file</strong>', projname);
                        $("#FileUpldExperience").focus();
                        return false;
                    }
                    //               End of Temporary Comment

                }

                debugger;

                /*------------------------------------------------*/
                ///// If company type is not Proprietorship 
                ///// then check for Financial Status details.
                /*------------------------------------------------*/

                var val1 = $("#drpYearofEstablishment").val();//2022-23   return 2022  or  2021-22  return 2021
                                                              //2023-24   return 2023  or  2022-23  return 2022  or 2021-22  return 2021
                if (val1 == "") {
                    val1 = 0;
                }
                var noyr1 = 0;
                var dt1 = new Date();
               // noyr1 = parseInt(dt1.getFullYear()) - parseInt(val1);  for temporary comment

                var month = parseInt(dt1.getMonth) + 1;
                if (month > 3)
                {
                    noyr1 = parseInt(dt1.getFullYear()) - parseInt(val1); //2023-23=0 or 2023-22=1 (1 column show) or 2023-21=2 (2 column show)

                    if ($('#ddlConstitution').val() != '1') { //// Not Proprietorship --- Added by Sushant Jena On 13-Aug-2019

                        if (val1 != 0) {
                            if (parseInt(noyr1) > 0) {
                                if (DropDownValidation('drpFinYear1', '0', 'Financial Year', projname) == false) {
                                    $("#drpFinYear1").focus();
                                    return false;
                                }
                            }
                        }

                        if (val1 != 0) {
                            if (parseInt(noyr1) > 0) {
                                if (blankFieldValidation('txtAnnual1', 'Annual turn over', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtAnnual1', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtAnnual1', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtAnnual1', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 1) {
                                if (blankFieldValidation('txtAnnual2', 'Annual turn over', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtAnnual2', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtAnnual2', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtAnnual2', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 2) {
                                if (blankFieldValidation('txtAnnual3', 'Annual turn over', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtAnnual3', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtAnnual3', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtAnnual3', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 0) {
                                if (blankFieldValidation('txtProfit1', 'Profit after tax', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtProfit1', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtProfit1', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtProfit1', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 1) {
                                if (blankFieldValidation('txtProfit2', 'Profit after tax', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtProfit2', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtProfit2', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtProfit2', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 2) {
                                if (blankFieldValidation('txtProfit3', 'Profit after tax', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtProfit3', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtProfit3', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtProfit3', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 0) {
                                if (blankFieldValidation('txtNtWorth1', 'Net worth', projname) == false) {
                                    return false;
                                }
                                //// Added by Sushant Jena On Dt.13-Aug-2019
                                if ($("#txtNtWorth1").val() <= 0) {
                                    jAlert('<strong>Net worth should be greater than zero(0) for 1st financial year !</strong>', projname);
                                    $("#txtNtWorth1").focus();
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtNtWorth1', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtNtWorth1', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtNtWorth1', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 1) {
                                if (blankFieldValidation('txtNtWorth2', 'Net worth', projname) == false) {
                                    return false;
                                }
                                //// Added by Sushant Jena On Dt.13-Aug-2019
                                if ($("#txtNtWorth2").val() <= 0) {
                                    jAlert('<strong>Net worth should be greater than zero(0) for 2nd financial year !</strong>', projname);
                                    $("#txtNtWorth2").focus();
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtNtWorth2', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtNtWorth2', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtNtWorth2', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 2) {
                                if (blankFieldValidation('txtNtWorth3', 'Net worth', projname) == false) {
                                    return false;
                                }
                                //// Added by Sushant Jena On Dt.13-Aug-2019
                                if ($("#txtNtWorth3").val() <= 0) {
                                    jAlert('<strong>Net worth should be greater than zero(0) for 3rd financial year !</strong>', projname);
                                    $("#txtNtWorth3").focus();
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtNtWorth3', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtNtWorth3', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtNtWorth3', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 0) {
                                if (blankFieldValidation('txtReserve1', 'Reserve and surplus', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtReserve1', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtReserve1', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtReserve1', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 1) {
                                if (blankFieldValidation('txtReserve2', 'Reserve and surplus', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtReserve2', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtReserve2', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtReserve2', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 2) {
                                if (blankFieldValidation('txtReserve3', 'Reserve and surplus', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtReserve3', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtReserve3', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtReserve3', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 0) {
                                if (blankFieldValidation('txtShare1', 'Share capital', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidationLast('txtShare1', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtShare1', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 1) {
                                if (blankFieldValidation('txtShare2', 'Share capital', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtShare2', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtShare2', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtShare2', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 2) {
                                if (blankFieldValidation('txtShare3', 'Share capital', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtShare3', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtShare3', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtShare3', 'Share capital', projname) == false) {
                            return false;
                        }
                        var netw1 = $('#txtNtWorth1').val();

                        if (parseFloat(netw1) > 0) {
                            $('#dv123').show();
                        }
                        else {
                            $('#dv123').hide();
                        }

                    }
                    if ($('#ddlConstitution').val() == '1') {
                        if (parseInt(noyr1) > 0) {
                            if ($('#hdnAudit').val() == '') {
                                $('#dv123').show();
                                jAlert('<strong>Networth Certificate of the Proprietor duly certified by CA for Current/latest year</strong>', projname);
                                $("#FileUpldAudited").focus();
                                return false;
                            }
                        }
                    }
                    else if ($('#ddlConstitution').val() == '2') {
                        if (parseInt(noyr1) > 0) {
                            if ($('#hdnAudit').val() == '') {
                                $('#dv123').show();
                                jAlert('<strong>Partnership deed.</strong>', projname);
                                $("#FileUpldAudited").focus();
                                return false;
                            }
                        }
                    }
                    else {
                        if (parseInt(noyr1) > 0) {
                            if (parseFloat(netw1) > 0) {
                                if ($('#hdnAudit').val() == '') {
                                    jAlert('<strong>Please upload audited financial statements for first year(financial statements,profit/loss accounts, balance sheets) file</strong>', projname);
                                    $("#FileUpldAudited").focus();
                                    return false;
                                }
                            }
                        }
                    }


                    var netw2 = $('#txtNtWorth2').val();
                    if (parseFloat(netw2) > 0) {
                        $('#adt1').show();
                    }
                    else {
                        $('#adt1').hide();
                    }

                    /*---------------------------------------------------------------------------------*/
                    ///// Tax Audit Report Validation For Proprietorship

                    if ($('#ddlConstitution').val() == '1') {
                        if ($('#drpApplicationFor').val() == '2') { //// Added by Sushant Jena
                            if (parseInt(noyr1) > 0) {
                                if ($('#hdnFySecond').val() == '') {
                                    $('#adt1').show();
                                    jAlert('<strong>Tax Audit Report(if applicable) for Current/latest year.</strong>', projname);
                                    $("#FileUploadSecond").focus();
                                    return false;
                                }
                            }
                        }
                    }
                    else if ($('#ddlConstitution').val() == '2') {
                        if (parseInt(noyr1) > 0) {
                            if ($('#hdnFySecond').val() == '') {
                                $('#adt1').show();
                                jAlert('<strong>Complete balance sheet of the firm(latest 3 years).</strong>', projname);
                                $("#FileUploadSecond").focus();
                                return false;
                            }
                        }
                    }
                    else {
                        if (parseInt(noyr1) > 1) {
                            if (parseFloat(netw2) > 0) {
                                if ($('#hdnFySecond').val() == '') {
                                    jAlert('<strong>Please upload audited financial statements for second year(financial statements,profit/loss accounts, balance sheets) file</strong>', projname);
                                    $("#FileUploadSecond").focus();
                                    return false;
                                }
                            }
                        }
                    }


                    /*---------------------------------------------------------------------------------*/
                    //               End of Temporary Comment
                    var netw3 = $('#txtNtWorth3').val();
                    if (parseFloat(netw3) > 0) {
                        $('#adt2').show();
                    }
                    else {
                        $('#adt2').hide();
                    }


                    /*---------------------------------------------------------------------------------*/
                    ///// Tax Audit Report Validation For Partnership

                    if ($('#ddlConstitution').val() == '1') {
                        if (parseInt(noyr1) > 0) {
                            if ($('#hdnFyThird').val() == '') {
                                jAlert('<strong>Income tax return for Current/latest year. </strong>', projname);
                                $("#FileUploadThird").focus();
                                return false;
                            }
                        }
                    }
                    else if ($('#ddlConstitution').val() == '2') {
                        if ($('#drpApplicationFor').val() == '2') {
                            if (parseInt(noyr1) > 0) {
                                if ($('#hdnFyThird').val() == '') {
                                    jAlert('<strong>Tax audit report of the Partnership firm.</strong>', projname);
                                    $("#FileUploadThird").focus();
                                    return false;
                                }
                            }
                        }
                    }
                    else {
                        if (parseInt(noyr1) > 2) {
                            if (parseFloat(netw3) > 0) {
                                if ($('#hdnFyThird').val() == '') {
                                    jAlert('<strong>Please upload audited financial statements for Third Year(financial statements,profit/loss accounts, balance sheets) file</strong>', projname);
                                    $("#FileUploadThird").focus();

                                    return false;
                                }
                            }
                        }
                    }

                    /*---------------------------------------------------------------------------------*/

                    if ($('#ddlConstitution').val() == '2') {
                        if (parseInt(noyr1) > 0) {
                            if ($('#hdnFyFourth').val() == '') {
                                jAlert('<strong>Please upload Income tax return.</strong>', projname);
                                $("#FileUploadFourthupd").focus();

                                return false;
                            }
                        }
                    }



                    //               End of Temporary Comment
                    if ($("#drpProjectCat").val() == "2") {
                        var a = $("#drpApplicationFor").val();
                        //               Start of Temporary Comment
                        if (a == '1') {
                            if ($('#hdnNetWorth').val() == '') {
                                jAlert('<strong>Please upload net worth certified by CA file</strong>', projname);
                                $("#FileUpldNetWorth").focus();
                                return false;
                            }
                        }
                        //               End of Temporary Comment
                    }



                }
                else // before april
                {
                    noyr1 = parseInt(dt1.getFullYear()) - parseInt(val1); //2023-22=1 or 2023-21=2

                    if ($('#ddlConstitution').val() != '1')
                    {
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 1) {
                                if (DropDownValidation('drpFinYear1', '0', 'Financial Year', projname) == false) {
                                    $("#drpFinYear1").focus();
                                    return false;
                                }
                            }
                        }

                        if (val1 != 0) {
                            if (parseInt(noyr1) > 1) {
                                if (blankFieldValidation('txtAnnual1', 'Annual turn over', projname) == false) {
                                    return false;
                                }
                            }
                        }

                        if (WhiteSpaceValidation1st('txtAnnual1', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtAnnual1', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtAnnual1', 'Annual turn over', projname) == false) {
                            return false;
                        }

                        if (val1 != 0) {
                            if (parseInt(noyr1) > 2) {
                                if (blankFieldValidation('txtAnnual2', 'Annual turn over', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtAnnual2', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtAnnual2', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtAnnual2', 'Annual turn over', projname) == false) {
                            return false;
                        }


                        if (val1 != 0) {
                            if (parseInt(noyr1) > 3) {
                                if (blankFieldValidation('txtAnnual3', 'Annual turn over', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtAnnual3', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtAnnual3', 'Annual turn over', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtAnnual3', 'Annual turn over', projname) == false) {
                            return false;
                        }


                        if (val1 != 0) {
                            if (parseInt(noyr1) > 1) {
                                if (blankFieldValidation('txtProfit1', 'Profit after tax', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtProfit1', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtProfit1', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtProfit1', 'Profit after tax', projname) == false) {
                            return false;
                        }

                        if (val1 != 0) {
                            if (parseInt(noyr1) > 2) {
                                if (blankFieldValidation('txtProfit2', 'Profit after tax', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtProfit2', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtProfit2', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtProfit2', 'Profit after tax', projname) == false) {
                            return false;
                        }


                        if (val1 != 0) {
                            if (parseInt(noyr1) > 3) {
                                if (blankFieldValidation('txtProfit3', 'Profit after tax', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtProfit3', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtProfit3', 'Profit after tax', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtProfit3', 'Profit after tax', projname) == false) {
                            return false;
                        }


                        if (val1 != 0) {
                            if (parseInt(noyr1) > 1) {
                                if (blankFieldValidation('txtNtWorth1', 'Net worth', projname) == false) {
                                    return false;
                                }
                                //// Added by Sushant Jena On Dt.13-Aug-2019
                                if ($("#txtNtWorth1").val() <= 0) {
                                    jAlert('<strong>Net worth should be greater than zero(0) for 1st financial year !</strong>', projname);
                                    $("#txtNtWorth1").focus();
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtNtWorth1', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtNtWorth1', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtNtWorth1', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 2) {
                                if (blankFieldValidation('txtNtWorth2', 'Net worth', projname) == false) {
                                    return false;
                                }
                                //// Added by Sushant Jena On Dt.13-Aug-2019
                                if ($("#txtNtWorth2").val() <= 0) {
                                    jAlert('<strong>Net worth should be greater than zero(0) for 2nd financial year !</strong>', projname);
                                    $("#txtNtWorth2").focus();
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtNtWorth2', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtNtWorth2', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtNtWorth2', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 3) {
                                if (blankFieldValidation('txtNtWorth3', 'Net worth', projname) == false) {
                                    return false;
                                }
                                //// Added by Sushant Jena On Dt.13-Aug-2019
                                if ($("#txtNtWorth3").val() <= 0) {
                                    jAlert('<strong>Net worth should be greater than zero(0) for 3rd financial year !</strong>', projname);
                                    $("#txtNtWorth3").focus();
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtNtWorth3', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtNtWorth3', 'Net worth', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtNtWorth3', 'Net worth', projname) == false) {
                            return false;
                        }


                        if (val1 != 0) {
                            if (parseInt(noyr1) > 1) {
                                if (blankFieldValidation('txtReserve1', 'Reserve and surplus', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtReserve1', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtReserve1', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtReserve1', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 2) {
                                if (blankFieldValidation('txtReserve2', 'Reserve and surplus', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtReserve2', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtReserve2', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtReserve2', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 3) {
                                if (blankFieldValidation('txtReserve3', 'Reserve and surplus', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtReserve3', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtReserve3', 'Reserve and surplus', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtReserve3', 'Reserve and surplus', projname) == false) {
                            return false;
                        }


                        if (val1 != 0) {
                            if (parseInt(noyr1) > 1) {
                                if (blankFieldValidation('txtShare1', 'Share capital', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidationLast('txtShare1', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtShare1', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 2) {
                                if (blankFieldValidation('txtShare2', 'Share capital', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtShare2', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtShare2', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtShare2', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (val1 != 0) {
                            if (parseInt(noyr1) > 3) {
                                if (blankFieldValidation('txtShare3', 'Share capital', projname) == false) {
                                    return false;
                                }
                            }
                        }
                        if (WhiteSpaceValidation1st('txtShare3', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtShare3', 'Share capital', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtShare3', 'Share capital', projname) == false) {
                            return false;
                        }
                        var netw1 = $('#txtNtWorth1').val();

                        if (parseFloat(netw1) > 0) {
                            $('#dv123').show();
                        }
                        else {
                            $('#dv123').hide();
                        }

                    }

                    if ($('#ddlConstitution').val() == '1') {
                        if (parseInt(noyr1) > 1) {
                            if ($('#hdnAudit').val() == '') {
                                $('#dv123').show();
                                jAlert('<strong>Networth Certificate of the Proprietor duly certified by CA for Current/latest year</strong>', projname);
                                $("#FileUpldAudited").focus();
                                return false;
                            }
                        }
                    }
                    else if ($('#ddlConstitution').val() == '2') {
                        if (parseInt(noyr1) > 1) {
                            if ($('#hdnAudit').val() == '') {
                                $('#dv123').show();
                                jAlert('<strong>Partnership deed.</strong>', projname);
                                $("#FileUpldAudited").focus();
                                return false;
                            }
                        }
                    }
                    else {
                        if (parseInt(noyr1) > 1) {
                            if (parseFloat(netw1) > 0) {
                                if ($('#hdnAudit').val() == '') {
                                    jAlert('<strong>Please upload audited financial statements for first year(financial statements,profit/loss accounts, balance sheets) file</strong>', projname);
                                    $("#FileUpldAudited").focus();
                                    return false;
                                }
                            }
                        }
                    }

                    var netw2 = $('#txtNtWorth2').val();
                    if (parseFloat(netw2) > 0) {
                        $('#adt1').show();
                    }
                    else {
                        $('#adt1').hide();
                    }

                    /*---------------------------------------------------------------------------------*/
                    ///// Tax Audit Report Validation For Proprietorship

                    if ($('#ddlConstitution').val() == '1') {
                        if ($('#drpApplicationFor').val() == '2') { //// Added by Sushant Jena
                            if (parseInt(noyr1) > 1) {
                                if ($('#hdnFySecond').val() == '') {
                                    $('#adt1').show();
                                    jAlert('<strong>Tax Audit Report(if applicable) for Current/latest year.</strong>', projname);
                                    $("#FileUploadSecond").focus();
                                    return false;
                                }
                            }
                        }
                    }
                    else if ($('#ddlConstitution').val() == '2') {
                        if (parseInt(noyr1) > 1) {
                            if ($('#hdnFySecond').val() == '') {
                                $('#adt1').show();
                                jAlert('<strong>Complete balance sheet of the firm(latest 3 years).</strong>', projname);
                                $("#FileUploadSecond").focus();
                                return false;
                            }
                        }
                    }
                    else {
                        if (parseInt(noyr1) > 2) {
                            if (parseFloat(netw2) > 0) {
                                if ($('#hdnFySecond').val() == '') {
                                    jAlert('<strong>Please upload audited financial statements for second year(financial statements,profit/loss accounts, balance sheets) file</strong>', projname);
                                    $("#FileUploadSecond").focus();
                                    return false;
                                }
                            }
                        }
                    }

                    /*---------------------------------------------------------------------------------*/
                    //               End of Temporary Comment
                    var netw3 = $('#txtNtWorth3').val();
                    if (parseFloat(netw3) > 0) {
                        $('#adt2').show();
                    }
                    else {
                        $('#adt2').hide();
                    }


                    /*---------------------------------------------------------------------------------*/
                    ///// Tax Audit Report Validation For Partnership

                    if ($('#ddlConstitution').val() == '1') {
                        if (parseInt(noyr1) > 1) {
                            if ($('#hdnFyThird').val() == '') {
                                jAlert('<strong>Income tax return for Current/latest year. </strong>', projname);
                                $("#FileUploadThird").focus();
                                return false;
                            }
                        }
                    }
                    else if ($('#ddlConstitution').val() == '2') {
                        if ($('#drpApplicationFor').val() == '2') {
                            if (parseInt(noyr1) > 1) {
                                if ($('#hdnFyThird').val() == '') {
                                    jAlert('<strong>Tax audit report of the Partnership firm.</strong>', projname);
                                    $("#FileUploadThird").focus();
                                    return false;
                                }
                            }
                        }
                    }
                    else {
                        if (parseInt(noyr1) > 3) {
                            if (parseFloat(netw3) > 0) {
                                if ($('#hdnFyThird').val() == '') {
                                    jAlert('<strong>Please upload audited financial statements for Third Year(financial statements,profit/loss accounts, balance sheets) file</strong>', projname);
                                    $("#FileUploadThird").focus();

                                    return false;
                                }
                            }
                        }
                    }


                    /*---------------------------------------------------------------------------------*/

                    if ($('#ddlConstitution').val() == '2') {
                        if (parseInt(noyr1) > 1) {
                            if ($('#hdnFyFourth').val() == '') {
                                jAlert('<strong>Please upload Income tax return.</strong>', projname);
                                $("#FileUploadFourthupd").focus();

                                return false;
                            }
                        }
                    }



                    //               End of Temporary Comment
                    if ($("#drpProjectCat").val() == "2") {
                        var a = $("#drpApplicationFor").val();
                        //               Start of Temporary Comment
                        if (a == '1') {
                            if ($('#hdnNetWorth').val() == '') {
                                jAlert('<strong>Please upload net worth certified by CA file</strong>', projname);
                                $("#FileUpldNetWorth").focus();
                                return false;
                            }
                        }
                        //               End of Temporary Comment
                    }



                }
                

                ////if ($('#ddlConstitution').val() != '1') { /// Not Proprietorship --- Added by Sushant Jena On 13-Aug-2019

                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 0) {
                //            if (DropDownValidation('drpFinYear1', '0', 'Financial Year', projname) == false) {
                //                $("#drpFinYear1").focus();
                //                return false;
                //            }
                //        }
                //    }

                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 0) {
                //            if (blankFieldValidation('txtAnnual1', 'Annual turn over', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtAnnual1', 'Annual turn over', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtAnnual1', 'Annual turn over', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtAnnual1', 'Annual turn over', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 1) {
                //            if (blankFieldValidation('txtAnnual2', 'Annual turn over', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtAnnual2', 'Annual turn over', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtAnnual2', 'Annual turn over', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtAnnual2', 'Annual turn over', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 2) {
                //            if (blankFieldValidation('txtAnnual3', 'Annual turn over', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtAnnual3', 'Annual turn over', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtAnnual3', 'Annual turn over', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtAnnual3', 'Annual turn over', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 0) {
                //            if (blankFieldValidation('txtProfit1', 'Profit after tax', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtProfit1', 'Profit after tax', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtProfit1', 'Profit after tax', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtProfit1', 'Profit after tax', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 1) {
                //            if (blankFieldValidation('txtProfit2', 'Profit after tax', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtProfit2', 'Profit after tax', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtProfit2', 'Profit after tax', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtProfit2', 'Profit after tax', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 2) {
                //            if (blankFieldValidation('txtProfit3', 'Profit after tax', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtProfit3', 'Profit after tax', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtProfit3', 'Profit after tax', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtProfit3', 'Profit after tax', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 0) {
                //            if (blankFieldValidation('txtNtWorth1', 'Net worth', projname) == false) {
                //                return false;
                //            }
                //            //// Added by Sushant Jena On Dt.13-Aug-2019
                //            if ($("#txtNtWorth1").val() <= 0) {
                //                jAlert('<strong>Net worth should be greater than zero(0) for 1st financial year !</strong>', projname);
                //                $("#txtNtWorth1").focus();
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtNtWorth1', 'Net worth', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtNtWorth1', 'Net worth', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtNtWorth1', 'Net worth', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 1) {
                //            if (blankFieldValidation('txtNtWorth2', 'Net worth', projname) == false) {
                //                return false;
                //            }
                //            //// Added by Sushant Jena On Dt.13-Aug-2019
                //            if ($("#txtNtWorth2").val() <= 0) {
                //                jAlert('<strong>Net worth should be greater than zero(0) for 2nd financial year !</strong>', projname);
                //                $("#txtNtWorth2").focus();
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtNtWorth2', 'Net worth', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtNtWorth2', 'Net worth', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtNtWorth2', 'Net worth', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 2) {
                //            if (blankFieldValidation('txtNtWorth3', 'Net worth', projname) == false) {
                //                return false;
                //            }
                //            //// Added by Sushant Jena On Dt.13-Aug-2019
                //            if ($("#txtNtWorth3").val() <= 0) {
                //                jAlert('<strong>Net worth should be greater than zero(0) for 3rd financial year !</strong>', projname);
                //                $("#txtNtWorth3").focus();
                //                return false;
                //            }
                //        }
                //    }
                //  //  if (WhiteSpaceValidation1st('txtNtWorth3', 'Net worth', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtNtWorth3', 'Net worth', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtNtWorth3', 'Net worth', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 0) {
                //            if (blankFieldValidation('txtReserve1', 'Reserve and surplus', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtReserve1', 'Reserve and surplus', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtReserve1', 'Reserve and surplus', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtReserve1', 'Reserve and surplus', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 1) {
                //            if (blankFieldValidation('txtReserve2', 'Reserve and surplus', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtReserve2', 'Reserve and surplus', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtReserve2', 'Reserve and surplus', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtReserve2', 'Reserve and surplus', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 2) {
                //            if (blankFieldValidation('txtReserve3', 'Reserve and surplus', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtReserve3', 'Reserve and surplus', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtReserve3', 'Reserve and surplus', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtReserve3', 'Reserve and surplus', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 0) {
                //            if (blankFieldValidation('txtShare1', 'Share capital', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidationLast('txtShare1', 'Share capital', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtShare1', 'Share capital', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 1) {
                //            if (blankFieldValidation('txtShare2', 'Share capital', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtShare2', 'Share capital', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtShare2', 'Share capital', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtShare2', 'Share capital', projname) == false) {
                //        return false;
                //    }
                //    if (val1 != 0) {
                //        if (parseInt(noyr1) > 2) {
                //            if (blankFieldValidation('txtShare3', 'Share capital', projname) == false) {
                //                return false;
                //            }
                //        }
                //    }
                //    if (WhiteSpaceValidation1st('txtShare3', 'Share capital', projname) == false) {
                //        return false;
                //    }
                //    if (WhiteSpaceValidationLast('txtShare3', 'Share capital', projname) == false) {
                //        return false;
                //    }
                //    if (SpecialCharacter1st('txtShare3', 'Share capital', projname) == false) {
                //        return false;
                //    }
                //    var netw1 = $('#txtNtWorth1').val();

                //    if (parseFloat(netw1) > 0) {
                //        $('#dv123').show();
                //    }
                //    else {
                //        $('#dv123').hide();
                //    }

                //}



                //               Start of Temporary Comment

                //if ($('#ddlConstitution').val() == '1') {
                //    if (parseInt(noyr1) > 0) {
                //        if ($('#hdnAudit').val() == '') {
                //            $('#dv123').show();
                //            jAlert('<strong>Networth Certificate of the Proprietor duly certified by CA for Current/latest year</strong>', projname);
                //            $("#FileUpldAudited").focus();
                //            return false;
                //        }
                //    }
                //}
                //else if ($('#ddlConstitution').val() == '2') {
                //    if (parseInt(noyr1) > 0) {
                //        if ($('#hdnAudit').val() == '') {
                //            $('#dv123').show();
                //            jAlert('<strong>Partnership deed.</strong>', projname);
                //            $("#FileUpldAudited").focus();
                //            return false;
                //        }
                //    }
                //}
                //else {
                //    if (parseInt(noyr1) > 0) {
                //        if (parseFloat(netw1) > 0) {
                //            if ($('#hdnAudit').val() == '') {
                //                jAlert('<strong>Please upload audited financial statements for first year(financial statements,profit/loss accounts, balance sheets) file</strong>', projname);
                //                $("#FileUpldAudited").focus();
                //                return false;
                //            }
                //        }
                //    }
                //}
                ////               End of Temporary Comment
                //var netw2 = $('#txtNtWorth2').val();
                //if (parseFloat(netw2) > 0) {
                //    $('#adt1').show();
                //}
                //else {
                //    $('#adt1').hide();
                //}

                ///*---------------------------------------------------------------------------------*/
                /////// Tax Audit Report Validation For Proprietorship

                ///if ($('#ddlConstitution').val() == '1') {
                //    if ($('#drpApplicationFor').val() == '2') { //// Added by Sushant Jena
                //        if (parseInt(noyr1) > 0) {
                //            if ($('#hdnFySecond').val() == '') {
                //                $('#adt1').show();
                //                jAlert('<strong>Tax Audit Report(if applicable) for Current/latest year.</strong>', projname);
                //                $("#FileUploadSecond").focus();
                //                return false;
                //            }
                //        }
                //    }
                //}
                //else if ($('#ddlConstitution').val() == '2') {
                //    if (parseInt(noyr1) > 0) {
                //        if ($('#hdnFySecond').val() == '') {
                //            $('#adt1').show();
                //            jAlert('<strong>Complete balance sheet of the firm(latest 3 years).</strong>', projname);
                //            $("#FileUploadSecond").focus();
                //            return false;
                //        }
                //    }
                //}
                //else {
                //    if (parseInt(noyr1) > 1) {
                //        if (parseFloat(netw2) > 0) {
                //            if ($('#hdnFySecond').val() == '') {
                //                jAlert('<strong>Please upload audited financial statements for second year(financial statements,profit/loss accounts, balance sheets) file</strong>', projname);
                //                $("#FileUploadSecond").focus();
                //                return false;
                //            }
                //        }
                //    }
                //}

                ///*---------------------------------------------------------------------------------*/
                ////               End of Temporary Comment
                //var netw3 = $('#txtNtWorth3').val();
                //if (parseFloat(netw3) > 0) {
                //    $('#adt2').show();
                //}
                //else {
                //    $('#adt2').hide();
                //}


                ///*---------------------------------------------------------------------------------*/
                /////// Tax Audit Report Validation For Partnership

                //if ($('#ddlConstitution').val() == '1') {
                //    if (parseInt(noyr1) > 0) {
                //        if ($('#hdnFyThird').val() == '') {
                //            jAlert('<strong>Income tax return for Current/latest year. </strong>', projname);
                //            $("#FileUploadThird").focus();
                //            return false;
                //        }
                //    }
                //}
                //else if ($('#ddlConstitution').val() == '2') {
                //    if ($('#drpApplicationFor').val() == '2') {
                //        if (parseInt(noyr1) > 0) {
                //            if ($('#hdnFyThird').val() == '') {
                //                jAlert('<strong>Tax audit report of the Partnership firm.</strong>', projname);
                //                $("#FileUploadThird").focus();
                //                return false;
                //            }
                //        }
                //    }
                //}
                //else {
                //    if (parseInt(noyr1) > 2) {
                //        if (parseFloat(netw3) > 0) {
                //            if ($('#hdnFyThird').val() == '') {
                //                jAlert('<strong>Please upload audited financial statements for Third Year(financial statements,profit/loss accounts, balance sheets) file</strong>', projname);
                //                $("#FileUploadThird").focus();

                //                return false;
                //            }
                //        }
                //    }
                //}

                ///*---------------------------------------------------------------------------------*/

                //if ($('#ddlConstitution').val() == '2') {
                //    if (parseInt(noyr1) > 0) {
                //        if ($('#hdnFyFourth').val() == '') {
                //            jAlert('<strong>Please upload Income tax return.</strong>', projname);
                //            $("#FileUploadFourthupd").focus();

                //            return false;
                //        }
                //    }
                //}



                ////               End of Temporary Comment
                //if ($("#drpProjectCat").val() == "2") {
                //    var a = $("#drpApplicationFor").val();
                //    //               Start of Temporary Comment
                //    if (a == '1') {
                //        if ($('#hdnNetWorth').val() == '') {
                //            jAlert('<strong>Please upload net worth certified by CA file</strong>', projname);
                //            $("#FileUpldNetWorth").focus();
                //            return false;
                //        }
                //    }
                //    //               End of Temporary Comment
                //}



                if ($("#drpApplicationFor").val() == "2") {
                    if (WhiteSpaceValidation1st('txtExtIndName', 'Existing industry name', projname) == false) {
                        return false;
                    }
                    if (WhiteSpaceValidationLast('txtExtIndName', 'Existing industry name', projname) == false) {
                        return false;
                    }
                    if (SpecialCharacter1st('txtExtIndName', 'Existing industry name', projname) == false) {
                        return false;
                    }
                    if (DropDownValidation('ddlDistrict', '0', 'District', projname) == false) {
                        $("#ddlDistrict").focus();
                        return false;
                    }
                    if (DropDownValidation('drpBlock', '0', 'Block', projname) == false) {
                        $("#drpBlock").focus();
                        return false;
                    }
                    if (DropDownValidation('ddlAlloted', '0', 'Whether land allotted by IDCO', projname) == false) {
                        $("#ddlAlloted").focus();
                        return false;
                    }
                    if ($("#ddlAlloted").val() == "1") {
                        if (blankFieldValidation('txtExtentLand', 'Extent of land(in acres)', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidation1st('txtExtentLand', 'Extent of land(in acres)', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtExtentLand', 'Extent of land(in acres)', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtExtentLand', 'Extent of land(in acres)', projname) == false) {
                            return false;
                        }

                        if (blankFieldValidation('txtNatureAct', 'Nature of activity', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidation1st('txtNatureAct', 'Nature of activity', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtNatureAct', 'Nature of activity', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtNatureAct', 'Nature of activity', projname) == false) {
                            return false;
                        }
                    }
                    if (DropDownValidation('ddlsector', '0', 'Sector', projname) == false) {
                        $("#ddlsector").focus();
                        return false;
                    }
                    if (DropDownValidation('ddlSubSec', '0', 'Sub Sector', projname) == false) {
                        $("#ddlSubSec").focus();
                        return false;
                    }
                    if (blankFieldValidation('txtCapacity', 'Capacity', projname) == false) {
                        return false;
                    }
                    if (WhiteSpaceValidation1st('txtCapacity', 'Capacity', projname) == false) {
                        return false;
                    }
                    if (WhiteSpaceValidationLast('txtCapacity', 'Capacity', projname) == false) {
                        return false;
                    }
                    if (SpecialCharacter1st('txtCapacity', 'Capacity', projname) == false) {
                        return false;
                    }
                    if (DropDownValidation('drpCp', '0', 'Unit', projname) == false) {
                        $("#drpCp").focus();
                        return false;
                    }
                    if ($("#drpCp").val() == "4") {
                        if (blankFieldValidation('txtCapOthr', 'Others (Please specify)', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidation1st('txtCapOthr', 'Others (Please specify)', projname) == false) {
                            return false;
                        }
                        if (WhiteSpaceValidationLast('txtCapOthr', 'Others (Please specify)', projname) == false) {
                            return false;
                        }
                        if (SpecialCharacter1st('txtCapOthr', 'Others (Please specify)', projname) == false) {
                            return false;
                        }
                    }
                }
            });

            /*---------------------------------------------------------------------------------*/
            ///// Button Add Mode Validation
            /*---------------------------------------------------------------------------------*/
            $('#AddMore').click(function () {
                if (blankFieldValidation('txtNameOfPromoter', 'Name of promoter', projname) == false) {
                    return false;
                }
            });

            /*---------------------------------------------------------------------------------*/
            ////// Group of Company Net Worth Add More Validation
            $('#Btn_Add_GC_Net_Worth').click(function () {
                if (blankFieldValidation('Txt_GC_Company_Name', 'Company name', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('Txt_GC_Net_Worth', 'Net worth of last financial year', projname) == false) {
                    return false;
                }
                if ($('#Hid_GC_New_Worth_File_Name').val() == '') {
                    jAlert('<strong>Please upload document related to networth.</strong>', projname);
                    $("#FU_GC_New_Worth").focus();
                    return false;
                }
            });

            /*---------------------------------------------------------------------------------*/

            $('#AddMoreBD').click(function () {
                if (blankFieldValidation('txtPName', 'Name', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtPDesg', 'Designation', projname) == false) {
                    return false;
                }
            });

            /*---------------------------------------------------------------------------------*/
            if ($("#drpProjectCat").val() == "2") {
                $('#btnAddMoreRWM').click(function () {

                    if (blankFieldValidation('txtRawMaterial', 'Raw Material', projname) == false) {
                        return false;
                    }
                    if (DropDownValidation('drprawMeterial', '0', 'Material source', projname) == false) {
                        $("#drprawMeterial").focus();
                        return false;
                    }
                });
            }
            //            var shurp1 = document.getElementById('txtShare1').value;
            //            var share1 = document.getElementById('txtReserve1').value;
            //            if (shurp1 == "") {
            //                shurp1 = 0;
            //            }
            //            if (share1 == "") {
            //                share1 = 0;
            //            }
            //            var result = parseFloat(shurp1) + parseFloat(share1);
            //            if (!isNaN(result)) {
            //                document.getElementById('txtNtWorth1').value = parseFloat(result).toFixed(2);
            //            }

            //            var shurp2 = document.getElementById('txtShare2').value;
            //            var share2 = document.getElementById('txtReserve2').value;
            //            if (shurp2 == "") {
            //                shurp2 = 0;
            //            }
            //            if (share2 == "") {
            //                share2 = 0;
            //            }
            //            var result2 = parseFloat(shurp2) + parseFloat(share2);
            //            if (!isNaN(result2)) {
            //                document.getElementById('txtNtWorth2').value = parseFloat(result2).toFixed(2);
            //            }

            //            var shurp3 = document.getElementById('txtShare3').value;
            //            var share3 = document.getElementById('txtReserve3').value;
            //            if (shurp3 == "") {
            //                shurp3 = 0;
            //            }
            //            if (share3 == "") {
            //                share3 = 0;
            //            }
            //            var result3 = parseFloat(shurp3) + parseFloat(share3);
            //            if (!isNaN(result3)) {
            //                document.getElementById('txtNtWorth3').value = parseFloat(result3).toFixed(2);
            //            }
            //            var netw1 = $('#txtNtWorth1').val();
            //            
            //            if (parseFloat(netw1) > 0) {

            //                $('#dv123').show();
            //            }
            //            else {

            //                $('#dv123').hide();
            //            }


            //            var netw2 = $('#txtNtWorth2').val();
            //            if (parseFloat(netw2) > 0) {
            //                $('#adt1').show();
            //            }
            //            else {
            //                $('#adt1').hide();
            //            }

            //            var netw3 = $('#txtNtWorth3').val();
            //            if (parseFloat(netw3) > 0) {
            //                $('#adt2').show();
            //            }
            //            else {
            //                $('#adt2').hide();
            //            }
            if ($('#txtAddress').val().length > 0) {
                var leftChar = 250 - $('#txtAddress').val().length;
                $('#SpanLbl').text(leftChar);
            }
        }

        
        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'GSTINDET') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZ';
            }

            if (allow == 'OtherSpecify') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'Profit') {
                AllowableCharacters = '1234567890-.';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._-';
            }
            if (allow == 'Address') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
            }
            if (allow == 'RawMetrial') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
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
        function checkboxDet() {

            if (document.getElementById("chkBoxAddress").checked) {
                $('#txtCorAddrs').val($('#txtAddress').val());
                $('#drpCorCountry').val($('#ddlCountry').val());
                $('#ddlCode').val($('#drpMob').val());
                if ($('#ddlCountry').val() == "1") {

                    $('#drpCorState').val($('#ddlState').val());
                }
                else {
                    $('#st2').show();
                    $('#st1').hide();
                    $('#txtCorState').val('');

                }
                $('#txtCorCity').val($('#txtCity').val());
                $('#txtCorFax').val($('#txtFaxNo').val());
                $('#txtCorEmailid').val($('#txtEmail').val());
                $('#txtCorPin').val($('#txtPinCode').val());
            }
            else {
                $('#txtCorAddrs').val('');
                $('#drpCorCountry').val('0');
                if ($('#ddlCountry').val() == "1") {
                    $('#drpCorState').val('0');
                }
                else {
                    $('#txtCorState').val('');
                }

                $('#txtCorCity').val('');
                $('#txtCorFax').val('');
                $('#txtCorEmailid').val('');
                $('#txtCorPin').val('');
            }
        }
        function echeck(str) {

            var at = "@"
            var dot = "."
            var lat = str.indexOf(at)
            var lstr = str.length
            var ldot = str.indexOf(dot)
            var undscore = "_";
            var Hypn = "-";
            var ldot1 = (str.indexOf(ldot) + 1);
            if (str.indexOf(at) == -1) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }

            if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr || str.lastIndexOf(at) == lstr - 1) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }

            if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr || str.lastIndexOf(dot) == lstr - 1) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }

            if (str.indexOf(at, (lat + 1)) != -1) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }

            if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }

            if (str.indexOf(dot, (lat + 2)) == -1) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }

            if (str.indexOf(" ") != -1) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }
            if (str.substring(lat - 1, lat) == undscore || str.substring(lat + 1, lat + 2) == undscore) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }
            if (str.substring(lat - 1, lat) == Hypn || str.substring(lat + 1, lat + 2) == Hypn) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }
            if (str.indexOf(undscore) == 0 || str.indexOf(undscore) == lstr - 1 || str.lastIndexOf(undscore) == lstr - 1) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }
            if (str.indexOf(Hypn) == 0 || str.indexOf(Hypn) == lstr - 1 || str.lastIndexOf(Hypn) == lstr - 1) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }

            if (str.substring(ldot - 1, ldot1) == dot || str.substring(ldot + 1, ldot + 2) == dot) {
                jAlert('<strong>Invalid E-mail id</strong>', projname);
                return false
            }

            return true
        }
        function EnableAndDiseableBasedOnddlConstitution(ddlConstitution) {
            debugger;
            var dt = new Date();
            var val = $("#drpYearofEstablishment").val();
            if (val == "") {
                val == 0;
            }
            if (isNaN(parseInt(val)))
            {
                val = 0;
            }
            var noyr = 0;
            if (val.length == 4) {
               
                if (val > dt.getFullYear()) {
                    jAlert('<strong>Year of Establishment does not accept future Year</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
            }
           var month = parseInt(dt.getMonth()) + 1;

            if (ddlConstitution == "1") {
                $("#lblf1").text("Networth Certificate of the Proprietor duly certified by CA for Current/latest year.");
                $("#lblf2").text("Tax Audit Report(if applicable) for Current/latest year.");
                $("#lblf3").text("Income tax return for Current/latest year.");
                $('#lblf4').hide();
                $("#lblDet").text('3.Promoter Details');
                $("#DvtxtPromoter").show();
                $("#dvNm").show();
                $("#DvtxtNoOfParter").hide();
                $("#DvtxtNameOfMP").hide();
                $("#DvDirectorww").hide();
                $("#GrdBD1").hide();
                $("#dvPromoterNM1").show();
                $("#DvOths").hide();
                $("#DVC1").hide();
                $("#DVC2").hide();
                $("#DVC3").hide();
                $("#DVC4").hide();
                $('#TgToo').hide();
                document.getElementById("drpFinYear1").disabled = true;
                document.getElementById("drpFinYear1").value = "0";
                document.getElementById("drpFinYear2").value = "0";
                document.getElementById("drpFinYear3").value = "0";
                document.getElementById("txtAnnual1").readOnly = true;
                document.getElementById("txtAnnual2").readOnly = true;
                document.getElementById("txtAnnual3").readOnly = true;
                document.getElementById("txtProfit1").readOnly = true;
                document.getElementById("txtProfit2").readOnly = true;
                document.getElementById("txtProfit3").readOnly = true;
                document.getElementById("txtReserve1").readOnly = true;
                document.getElementById("txtReserve2").readOnly = true;
                document.getElementById("txtReserve3").readOnly = true;
                document.getElementById("txtShare1").readOnly = true;
                document.getElementById("txtShare2").readOnly = true;
                document.getElementById("txtShare3").readOnly = true;
                document.getElementById("txtNtWorth3").readOnly = true;
                document.getElementById("txtNtWorth1").readOnly = true;
                document.getElementById("txtNtWorth2").readOnly = true;
                $('#adt2').show();
                $('#adt1').show(); ////// sushant
                if ($('#drpApplicationFor').val() == "1") {
                    $('#adt1').hide();
                }
                $('#dv123').show();
                document.getElementById("drpYearofEstablishment").value = "0"; //// Added by Sushant Jena on Dt. 13-Aug-2019                   
                document.getElementById("txtAnnual1").value = "0";
                document.getElementById("txtAnnual2").value = "0";
                document.getElementById("txtAnnual3").value = "0";
                document.getElementById("txtProfit1").value = "0";
                document.getElementById("txtProfit2").value = "0";
                document.getElementById("txtProfit3").value = "0";
                document.getElementById("txtReserve1").value = "0";
                document.getElementById("txtReserve2").value = "0";
                document.getElementById("txtReserve3").value = "0";
                document.getElementById("txtShare1").value = "0";
                document.getElementById("txtShare2").value = "0";
                document.getElementById("txtShare3").value = "0";
                document.getElementById("txtNtWorth3").value = "0";
                document.getElementById("txtNtWorth1").value = "0";
                document.getElementById("txtNtWorth2").value = "0";
                $('#dvfy4').hide();
                $('#dvShareCapital').show();
                $('#dvReserveAndSurplus').show();

            }
            else if (ddlConstitution == "2") {
                $("#lblDet").text('3.Partnership Details');
                $("#DvtxtNoOfParter").show();
                $("#DvtxtNameOfMP").show();
                $("#DvtxtPromoter").hide();
                $("#dvNm").hide();
                $("#DvDirectorww").hide();
                $("#GrdBD1").hide();
                $("#dvPromoterNM1").show();
                $("#DvOths").hide();
                $("#DVC1").show();
                $("#DVC2").hide();
                $("#DVC3").hide();
                $("#DVC4").show();
                $('#TgToo').hide();


                if (month > 3) {
                    noyr = parseInt(dt.getFullYear()) - parseInt(val); //2023-23=0

                    if (parseInt(noyr) == 0) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant
                        if ($('#drpApplicationFor').val() == "1") {
                            $('#adt2').hide();
                        }
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant
                        if ($('#drpApplicationFor').val() == "1") {
                            $('#adt2').hide();
                        }
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;


                        $('#adt1').show();
                        $('#adt2').hide(); ////// sushant
                        if ($('#drpApplicationFor').val() == "1") {
                            $('#adt2').hide();
                        }
                        $('#dv123').show();
                    }
                    else
                    {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;


                        $('#adt1').show();
                        $('#adt2').show(); ////// sushant
                        if ($('#drpApplicationFor').val() == "1") {
                            $('#adt2').show();
                        }
                        $('#dv123').show();
                    }

                }
                else {
                    noyr = (parseInt(dt.getFullYear())) - parseInt(val);//2023-2022=1

                    if (parseInt(noyr) == 1) {

                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant
                        if ($('#drpApplicationFor').val() == "1") {
                            $('#adt2').hide();
                        }
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant
                        if ($('#drpApplicationFor').val() == "1") {
                            $('#adt2').hide();
                        }
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 3) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;


                        $('#adt1').show();
                        $('#adt2').hide(); ////// sushant
                        if ($('#drpApplicationFor').val() == "1") {
                            $('#adt2').hide();
                        }
                        $('#dv123').show();
                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;


                        $('#adt1').show();
                        $('#adt2').show(); ////// sushant
                        if ($('#drpApplicationFor').val() == "1") {
                            $('#adt2').show();
                        }
                        $('#dv123').show();
                    }

                }

                //document.getElementById("drpFinYear1").disabled = false;
                //document.getElementById("drpFinYear2").readOnly = false;
                //document.getElementById("drpFinYear3").readOnly = false;

                //document.getElementById("txtAnnual1").readOnly = false;
                //document.getElementById("txtAnnual2").readOnly = false;
                //document.getElementById("txtAnnual3").readOnly = false;
                //document.getElementById("txtProfit1").readOnly = false;
                //document.getElementById("txtProfit2").readOnly = false;
                //document.getElementById("txtProfit3").readOnly = false;
                //document.getElementById("txtReserve1").readOnly = false;
                //document.getElementById("txtReserve2").readOnly = false;
                //document.getElementById("txtReserve3").readOnly = false;
                //document.getElementById("txtShare1").readOnly = false;
                //document.getElementById("txtShare2").readOnly = false;
                //document.getElementById("txtShare3").readOnly = false;
                //document.getElementById("txtNtWorth3").readOnly = false;
                //document.getElementById("txtNtWorth1").readOnly = false;
                //document.getElementById("txtNtWorth2").readOnly = false;

                //document.getElementById("txtReserve1").readOnly = true;
                //document.getElementById("txtReserve2").readOnly = true;
                //document.getElementById("txtReserve3").readOnly = true;
                //document.getElementById("txtShare1").readOnly = true;
                //document.getElementById("txtShare2").readOnly = true;
                //document.getElementById("txtShare3").readOnly = true;

                //$('#adt1').show();
                //$('#adt2').show(); ////// sushant
                //if ($('#drpApplicationFor').val() == "1") {
                //    $('#adt2').hide();
                //}
                //$('#dv123').show();

                $("#lblf1").text("Partnership deed.");
                $("#lblf2").text("Complete balance sheet of the firm(latest 3 years).");
                $("#lblf3").text("Tax audit report of the Partnership firm.");
                $('#lblf4').hide();
                $('#dvfy4').show();
                $('#dvShareCapital').hide();
                $('#dvReserveAndSurplus').hide();
            }
            else if (ddlConstitution == "8") {
                $("#lblDet").text('3.Board Of Directors');
                $("#DvtxtPromoter").hide();
                $("#dvNm").hide();
                $("#DvtxtNoOfParter").hide();
                $("#DvtxtNameOfMP").hide();
                $("#DvDirectorww").show();
                $("#GrdBD1").show();
                $("#dvPromoterNM1").show();
                $("#DvOths").show();
                $("#DVC1").show();
                $("#DVC2").show();
                $("#DVC3").show();
                $("#DVC4").show();
                $('#TgToo').show();


                if (month > 3) {

                    noyr = parseInt(dt.getFullYear()) - parseInt(val); //2023-23=0

                    if (parseInt(noyr) == 0) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;


                        $('#adt1').show();
                        $('#dv123').show();

                        $('#adt2').hide()
                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();
                    }
                }
                else {

                    noyr = (parseInt(dt.getFullYear())) - parseInt(val);//2023-2022=1

                    if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 3) {

                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;

                        //$('#adt1').show();
                        //$('#dv123').show();
                        //$('#adt2').hide();

                        $('#adt1').show();
                        $('#dv123').show();
                        //$('#adt2').show();
                        $('#adt2').hide()

                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();

                    }

                }


                document.getElementById("drpFinYear1").disabled = false;
                document.getElementById("drpFinYear2").readOnly = false;
                document.getElementById("drpFinYear3").readOnly = false;
                document.getElementById("txtAnnual1").readOnly = false;
                document.getElementById("txtAnnual2").readOnly = false;
                document.getElementById("txtAnnual3").readOnly = false;
                document.getElementById("txtProfit1").readOnly = false;
                document.getElementById("txtProfit2").readOnly = false;
                document.getElementById("txtProfit3").readOnly = false;
                document.getElementById("txtReserve1").readOnly = false;
                document.getElementById("txtReserve2").readOnly = false;
                document.getElementById("txtReserve3").readOnly = false;
                document.getElementById("txtShare1").readOnly = false;
                document.getElementById("txtShare2").readOnly = false;
                document.getElementById("txtShare3").readOnly = false;
                document.getElementById("txtNtWorth3").readOnly = false;
                document.getElementById("txtNtWorth1").readOnly = false;
                document.getElementById("txtNtWorth2").readOnly = false;
                $('#adt2').show();
                $('#adt1').show();
                $('#dv123').show();
                $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //$('#lblf4').show();
                $('#lblf4').hide();
                $('#dvfy4').hide();
                $('#dvShareCapital').show();
                $('#dvReserveAndSurplus').show();
            }
            else if (ddlConstitution == "3") {
                $("#lblDet").text('3.Board Of Directors');
                $("#DvtxtPromoter").hide();
                $("#dvNm").hide();
                $("#DvtxtNoOfParter").hide();
                $("#DvtxtNameOfMP").hide();
                $("#DvDirectorww").show();
                $("#GrdBD1").show();
                $("#dvPromoterNM1").show();
                $("#DvOths").hide();
                $("#DVC1").show();
                $("#DVC2").show();
                $("#DVC3").show();
                $("#DVC4").show();
                $('#TgToo').show();

                if (month > 3)
                {
                    noyr = parseInt(dt.getFullYear()) - parseInt(val); //2023-23=0

                    if (parseInt(noyr) == 0) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;


                        $('#adt1').show();
                        $('#dv123').show();

                        $('#adt2').hide()
                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();
                    }


                }
                else
                {
                    noyr = (parseInt(dt.getFullYear())) - parseInt(val);//2023-2022=1

                    if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;

                       
                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 3) {

                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;

                        //$('#adt1').show();
                        //$('#dv123').show();
                        //$('#adt2').hide();

                        $('#adt1').show();
                        $('#dv123').show();
                        //$('#adt2').show();
                        $('#adt2').hide()

                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();

                    }
                }



                //document.getElementById("drpFinYear1").disabled = false;
                //document.getElementById("drpFinYear2").readOnly = false;
                //document.getElementById("drpFinYear3").readOnly = false;
                //document.getElementById("txtAnnual1").readOnly = false;
                //document.getElementById("txtAnnual2").readOnly = false;
                //document.getElementById("txtAnnual3").readOnly = false;
                //document.getElementById("txtProfit1").readOnly = false;
                //document.getElementById("txtProfit2").readOnly = false;
                //document.getElementById("txtProfit3").readOnly = false;
                //document.getElementById("txtReserve1").readOnly = false;
                //document.getElementById("txtReserve2").readOnly = false;
                //document.getElementById("txtReserve3").readOnly = false;
                //document.getElementById("txtShare1").readOnly = false;
                //document.getElementById("txtShare2").readOnly = false;
                //document.getElementById("txtShare3").readOnly = false;
                //document.getElementById("txtNtWorth3").readOnly = false;
                //document.getElementById("txtNtWorth1").readOnly = false;
                //document.getElementById("txtNtWorth2").readOnly = false;
                //$('#adt2').show();
                //$('#adt1').show();
                //$('#dv123').show();

                $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //$('#lblf4').show();
                $('#lblf4').hide();
                $('#dvfy4').hide();
                $('#dvShareCapital').show();
                $('#dvReserveAndSurplus').show();
            }
            else if (ddlConstitution == "4") {
                $("#lblDet").text('3.Board Of Directors');
                $("#DvtxtPromoter").hide();
                $("#dvNm").hide();
                $("#DvtxtNoOfParter").hide();
                $("#DvtxtNameOfMP").hide();
                $("#DvDirectorww").show();
                $("#GrdBD1").show();
                $("#dvPromoterNM1").show();
                $("#DvOths").hide();
                $("#DVC1").show();
                $("#DVC2").show();
                $("#DVC3").show();
                $("#DVC4").show();
                $('#TgToo').show();

                if (month > 3) {

                    noyr = parseInt(dt.getFullYear()) - parseInt(val); //2023-23=0

                    if (parseInt(noyr) == 0) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;


                        $('#adt1').show();
                        $('#dv123').show();

                        $('#adt2').hide()
                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();
                    }
                }
                else {

                    noyr = (parseInt(dt.getFullYear())) - parseInt(val);//2023-2022=1

                    if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 3) {

                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;

                        //$('#adt1').show();
                        //$('#dv123').show();
                        //$('#adt2').hide();

                        $('#adt1').show();
                        $('#dv123').show();
                        //$('#adt2').show();
                        $('#adt2').hide()

                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();

                    }

                }




                //document.getElementById("drpFinYear1").disabled = false;
                //document.getElementById("drpFinYear2").readOnly = false;
                //document.getElementById("drpFinYear3").readOnly = false;
                //document.getElementById("txtAnnual1").readOnly = false;
                //document.getElementById("txtAnnual2").readOnly = false;
                //document.getElementById("txtAnnual3").readOnly = false;
                //document.getElementById("txtProfit1").readOnly = false;
                //document.getElementById("txtProfit2").readOnly = false;
                //document.getElementById("txtProfit3").readOnly = false;
                //document.getElementById("txtReserve1").readOnly = false;
                //document.getElementById("txtReserve2").readOnly = false;
                //document.getElementById("txtReserve3").readOnly = false;
                //document.getElementById("txtShare1").readOnly = false;
                //document.getElementById("txtShare2").readOnly = false;
                //document.getElementById("txtShare3").readOnly = false;
                //document.getElementById("txtNtWorth3").readOnly = false;
                //document.getElementById("txtNtWorth1").readOnly = false;
                //document.getElementById("txtNtWorth2").readOnly = false;
                //$('#adt2').show();
                //$('#adt1').show();
                //$('#dv123').show();
                $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //$('#lblf4').show();
                $('#lblf4').hide();
                $('#dvfy4').hide();
                $('#dvShareCapital').show();
                $('#dvReserveAndSurplus').show();
            }
            else if (ddlConstitution == "5") {
                $("#lblDet").text('3.Board Of Directors');
                $("#DvtxtPromoter").hide();
                $("#dvNm").hide();
                $("#DvtxtNoOfParter").hide();
                $("#DvtxtNameOfMP").hide();
                $("#DvDirectorww").show();
                $("#GrdBD1").show();
                $("#dvPromoterNM1").show();
                $("#DvOths").hide();
                $("#DVC1").show();
                $("#DVC2").show();
                $("#DVC3").show();
                $("#DVC4").show();
                $('#TgToo').show();

                if (month > 3) {

                    noyr = parseInt(dt.getFullYear()) - parseInt(val); //2023-23=0

                    if (parseInt(noyr) == 0) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;


                        $('#adt1').show();
                        $('#dv123').show();

                        $('#adt2').hide()
                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();
                    }
                }
                else {

                    noyr = (parseInt(dt.getFullYear())) - parseInt(val);//2023-2022=1

                    if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 3) {

                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;

                        //$('#adt1').show();
                        //$('#dv123').show();
                        //$('#adt2').hide();

                        $('#adt1').show();
                        $('#dv123').show();
                        //$('#adt2').show();
                        $('#adt2').hide()

                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();

                    }

                }




                //document.getElementById("drpFinYear1").disabled = false;
                //document.getElementById("drpFinYear2").readOnly = false;
                //document.getElementById("drpFinYear3").readOnly = false;
                //document.getElementById("txtAnnual1").readOnly = false;
                //document.getElementById("txtAnnual2").readOnly = false;
                //document.getElementById("txtAnnual3").readOnly = false;
                //document.getElementById("txtProfit1").readOnly = false;
                //document.getElementById("txtProfit2").readOnly = false;
                //document.getElementById("txtProfit3").readOnly = false;
                //document.getElementById("txtReserve1").readOnly = false;
                //document.getElementById("txtReserve2").readOnly = false;
                //document.getElementById("txtReserve3").readOnly = false;
                //document.getElementById("txtShare1").readOnly = false;
                //document.getElementById("txtShare2").readOnly = false;
                //document.getElementById("txtShare3").readOnly = false;
                //document.getElementById("txtNtWorth3").readOnly = false;
                //document.getElementById("txtNtWorth1").readOnly = false;
                //document.getElementById("txtNtWorth2").readOnly = false;
                //$('#adt2').show();
                //$('#adt1').show();
                //$('#dv123').show();
                $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //$('#lblf4').show();
                $('#lblf4').hide();
                $('#dvfy4').hide();
                $('#dvShareCapital').show();
                $('#dvReserveAndSurplus').show();
            }
            else if (ddlConstitution == "6") {
                $("#lblDet").text('3.Board Of Directors');
                $("#DvtxtPromoter").hide();
                $("#dvNm").hide();
                $("#DvtxtNoOfParter").hide();
                $("#DvtxtNameOfMP").hide();
                $("#DvDirectorww").show();
                $("#GrdBD1").show();
                $("#dvPromoterNM1").show();
                $("#DvOths").hide();
                $("#DVC3").hide();
                $("#DVC1").show();
                $("#DVC2").show();
                $("#DVC4").show();
                $('#TgToo').show();


                if (month > 3) {

                    noyr = parseInt(dt.getFullYear()) - parseInt(val); //2023-23=0

                    if (parseInt(noyr) == 0) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;


                        $('#adt1').show();
                        $('#dv123').show();

                        $('#adt2').hide()
                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();
                    }
                }
                else {

                    noyr = (parseInt(dt.getFullYear())) - parseInt(val);//2023-2022=1

                    if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 3) {

                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;

                        //$('#adt1').show();
                        //$('#dv123').show();
                        //$('#adt2').hide();

                        $('#adt1').show();
                        $('#dv123').show();
                        //$('#adt2').show();
                        $('#adt2').hide()

                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();

                    }

                }


                //document.getElementById("drpFinYear1").disabled = false;
                //document.getElementById("drpFinYear2").readOnly = false;
                //document.getElementById("drpFinYear3").readOnly = false;
                //document.getElementById("txtAnnual1").readOnly = false;
                //document.getElementById("txtAnnual2").readOnly = false;
                //document.getElementById("txtAnnual3").readOnly = false;
                //document.getElementById("txtProfit1").readOnly = false;
                //document.getElementById("txtProfit2").readOnly = false;
                //document.getElementById("txtProfit3").readOnly = false;
                //document.getElementById("txtReserve1").readOnly = false;
                //document.getElementById("txtReserve2").readOnly = false;
                //document.getElementById("txtReserve3").readOnly = false;
                //document.getElementById("txtShare1").readOnly = false;
                //document.getElementById("txtShare2").readOnly = false;
                //document.getElementById("txtShare3").readOnly = false;
                //document.getElementById("txtNtWorth3").readOnly = false;
                //document.getElementById("txtNtWorth1").readOnly = false;
                //document.getElementById("txtNtWorth2").readOnly = false;
                //$('#adt2').show();
                //$('#adt1').show();
                //$('#dv123').show();
                $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //$('#lblf4').show();
                $('#lblf4').hide();
                $('#dvfy4').hide();
                $('#dvShareCapital').show();
                $('#dvReserveAndSurplus').show();
            }
            else if (ddlConstitution == "7") {
                $("#lblDet").text('3.Board Of Directors');
                $("#DvtxtPromoter").hide();
                $("#dvNm").hide();
                $("#DvtxtNoOfParter").hide();
                $("#DvtxtNameOfMP").hide();
                $("#DvDirectorww").show();
                $("#GrdBD1").show();
                $("#dvPromoterNM1").show();
                $("#DvOths").hide();
                $("#DVC3").hide();
                $("#DVC1").show();
                $("#DVC2").show();
                $("#DVC4").show();
                $('#TgToo').show();

                if (month > 3) {

                    noyr = parseInt(dt.getFullYear()) - parseInt(val); //2023-23=0

                    if (parseInt(noyr) == 0) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;


                        $('#adt1').show();
                        $('#dv123').show();

                        $('#adt2').hide()
                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();
                    }
                }
                else {

                    noyr = (parseInt(dt.getFullYear())) - parseInt(val);//2023-2022=1

                    if (parseInt(noyr) == 1) {
                        document.getElementById("drpFinYear1").disabled = true;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = true;
                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit1").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve1").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare1").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth1").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = true;

                        $('#adt1').hide();
                        $('#adt2').hide(); ////// sushant                        
                        $('#dv123').hide();
                    }
                    else if (parseInt(noyr) == 2) {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = true;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = true;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = true;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = true;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = true;
                        document.getElementById("txtNtWorth3").readOnly = true;


                        $('#adt1').hide();
                        $('#adt2').hide();
                        $('#dv123').show();
                    }
                    else if (parseInt(noyr) == 3) {

                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = true;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = true;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = true;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = true;
                        document.getElementById("drpFinYear1").readOnly = false;
                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = true;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;

                        //$('#adt1').show();
                        //$('#dv123').show();
                        //$('#adt2').hide();

                        $('#adt1').show();
                        $('#dv123').show();
                        //$('#adt2').show();
                        $('#adt2').hide()

                    }
                    else {
                        document.getElementById("drpFinYear1").disabled = false;
                        document.getElementById("drpFinYear2").readOnly = true;
                        document.getElementById("drpFinYear3").readOnly = true;

                        document.getElementById("txtAnnual1").readOnly = false;
                        document.getElementById("txtAnnual2").readOnly = false;
                        document.getElementById("txtAnnual3").readOnly = false;
                        document.getElementById("txtProfit1").readOnly = false;
                        document.getElementById("txtProfit2").readOnly = false;
                        document.getElementById("txtProfit3").readOnly = false;
                        document.getElementById("txtReserve1").readOnly = false;
                        document.getElementById("txtReserve2").readOnly = false;
                        document.getElementById("txtReserve3").readOnly = false;
                        document.getElementById("txtShare1").readOnly = false;
                        document.getElementById("txtShare2").readOnly = false;
                        document.getElementById("txtShare3").readOnly = false;
                        document.getElementById("txtNtWorth3").readOnly = false;
                        document.getElementById("txtNtWorth1").readOnly = false;
                        document.getElementById("txtNtWorth2").readOnly = false;
                        $('#adt2').show();
                        $('#adt1').show();
                        $('#dv123').show();

                    }

                }




                //document.getElementById("drpFinYear1").disabled = false;
                //document.getElementById("drpFinYear2").readOnly = false;
                //document.getElementById("drpFinYear3").readOnly = false;
                //document.getElementById("txtAnnual1").readOnly = false;
                //document.getElementById("txtAnnual2").readOnly = false;
                //document.getElementById("txtAnnual3").readOnly = false;
                //document.getElementById("txtProfit1").readOnly = false;
                //document.getElementById("txtProfit2").readOnly = false;
                //document.getElementById("txtProfit3").readOnly = false;
                //document.getElementById("txtReserve1").readOnly = false;
                //document.getElementById("txtReserve2").readOnly = false;
                //document.getElementById("txtReserve3").readOnly = false;
                //document.getElementById("txtShare1").readOnly = false;
                //document.getElementById("txtShare2").readOnly = false;
                //document.getElementById("txtShare3").readOnly = false;
                //document.getElementById("txtNtWorth3").readOnly = false;
                //document.getElementById("txtNtWorth1").readOnly = false;
                //document.getElementById("txtNtWorth2").readOnly = false;
                //$('#adt2').show();
                //$('#adt1').show();
                //$('#dv123').show();
                $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //$('#lblf4').show();
                $('#lblf4').hide();
                $('#dvfy4').hide();
                $('#dvShareCapital').show();
                $('#dvReserveAndSurplus').show();
            }
            else {
                $("#DvtxtPromoter").hide();
                $("#dvNm").hide();
                $("#DvtxtNoOfParter").hide();
                $("#DvtxtNameOfMP").hide();
                $("#DvDirectorww").hide();
                $("#GrdBD1").hide();
                $("#dvPromoterNM1").hide();
                $("#DvOths").hide();
                $("#DVC3").show();
                $("#DVC1").show();
                $("#DVC2").show();
                $("#DVC4").show();
                $('#TgToo').show();
                document.getElementById("drpFinYear1").disabled = false;
                document.getElementById("drpFinYear2").readOnly = false;
                document.getElementById("drpFinYear3").readOnly = false;
                document.getElementById("txtAnnual1").readOnly = false;
                document.getElementById("txtAnnual2").readOnly = false;
                document.getElementById("txtAnnual3").readOnly = false;
                document.getElementById("txtProfit1").readOnly = false;
                document.getElementById("txtProfit2").readOnly = false;
                document.getElementById("txtProfit3").readOnly = false;
                document.getElementById("txtReserve1").readOnly = false;
                document.getElementById("txtReserve2").readOnly = false;
                document.getElementById("txtReserve3").readOnly = false;
                document.getElementById("txtShare1").readOnly = false;
                document.getElementById("txtShare2").readOnly = false;
                document.getElementById("txtShare3").readOnly = false;
                document.getElementById("txtNtWorth3").readOnly = false;
                document.getElementById("txtNtWorth1").readOnly = false;
                document.getElementById("txtNtWorth2").readOnly = false;
                $('#adt2').show();
                $('#adt1').show();
                $('#dv123').show();
                $("#lblf1").text("Upload Audited Financial Statements for First Year.");
                $("#lblf2").text("Upload Audited Financial Statements for Second Year.");
                $("#lblf3").text("Upload Audited Financial Statements for Third Year.");
                //$('#lblf4').show();
                $('#lblf4').hide();
                $('#dvfy4').hide();
                $('#dvShareCapital').show();
                $('#dvReserveAndSurplus').show();
            }
        }
        
    </script>
    <script type="text/javascript">
        //        function sumExist() {
        //            var shurp1 = document.getElementById('txtShare1').value;
        //            var share1 = document.getElementById('txtReserve1').value;
        //            if (shurp1 == "") {
        //                shurp1 = 0;
        //            }
        //            if (share1 == "") {
        //                share1 = 0;
        //            }
        //            var result = parseFloat(shurp1) + parseFloat(share1);
        //            if (!isNaN(result)) {
        //                document.getElementById('txtNtWorth1').value = parseFloat(result).toFixed(2);
        //            }
        //        }
        //        function sumExist1() {
        //            var shurp2 = document.getElementById('txtShare2').value;
        //            var share2 = document.getElementById('txtReserve2').value;
        //            if (shurp2 == "") {
        //                shurp2 = 0;
        //            }
        //            if (share2 == "") {
        //                share2 = 0;
        //            }
        //            var result2 = parseFloat(shurp2) + parseFloat(share2);
        //            if (!isNaN(result2)) {
        //                document.getElementById('txtNtWorth2').value = parseFloat(result2).toFixed(2);
        //            }
        //        }
        //        function sumExist2() {
        //            var shurp3 = document.getElementById('txtShare3').value;
        //            var share3 = document.getElementById('txtReserve3').value;
        //            if (shurp3 == "") {
        //                shurp3 = 0;
        //            }
        //            if (share3 == "") {
        //                share3 = 0;
        //            }
        //            var result3 = parseFloat(shurp3) + parseFloat(share3);
        //            if (!isNaN(result3)) {
        //                document.getElementById('txtNtWorth3').value = parseFloat(result3).toFixed(2);
        //            }
        //        }
        $('#txtReserve1').change(function () {

            var tot;
            var aa;
            if ($(this).val().length != 0) {
                var strarr = $(this).val().split('.');
                var hypn = $(this).val().split('-');
                if (strarr.length > 2) {
                    jAlert('<strong>Decimal point should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
                else if (hypn.length > 2) {
                    jAlert('<strong> - should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('0');
                    sumExist();
                    return false;
                }
                else if ($(this).val().indexOf('-') != -1) {
                    if ($(this).val().indexOf('-') != 0) {
                        var val = $(this).val();
                        val = val.replace('-', '');
                        $(this).val("-" + val);
                        sumExist();
                    }
                }
                else {
                    if (strarr.length > 1) {
                        aa = strarr[1].substring(0, 2);
                        tot = strarr[0] + '.' + aa;
                        $(this).val(tot);
                    }
                    else {
                        tot = strarr[0] + '.00';
                        $(this).val(tot);
                    }
                }

            }

        });
        $('#txtReserve2').change(function () {

            var tot;
            var aa;
            if ($(this).val().length != 0) {
                var strarr = $(this).val().split('.');
                var hypn = $(this).val().split('-');
                if (strarr.length > 2) {
                    jAlert('<strong>Decimal point should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
                else if (hypn.length > 2) {
                    jAlert('<strong> - should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('0');
                    sumExist1();
                    return false;
                }
                else if ($(this).val().indexOf('-') != -1) {
                    if ($(this).val().indexOf('-') != 0) {
                        var val = $(this).val();
                        val = val.replace('-', '');
                        $(this).val("-" + val);
                        sumExist1();
                    }
                }
                else {
                    if (strarr.length > 1) {
                        aa = strarr[1].substring(0, 2);
                        tot = strarr[0] + '.' + aa;
                        $(this).val(tot);
                    }
                    else {
                        tot = strarr[0] + '.00';
                        $(this).val(tot);
                    }
                }

            }
        });
        $('#txtReserve3').change(function () {

            var tot;
            var aa;
            if ($(this).val().length != 0) {
                var strarr = $(this).val().split('.');
                var hypn = $(this).val().split('-');
                if (strarr.length > 2) {
                    jAlert('<strong>Decimal point should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
                else if (hypn.length > 2) {
                    jAlert('<strong> - should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('0');
                    sumExist2();
                    return false;
                }
                else if ($(this).val().indexOf('-') != -1) {
                    if ($(this).val().indexOf('-') != 0) {
                        var val = $(this).val();
                        val = val.replace('-', '');
                        $(this).val("-" + val);
                        sumExist2();
                    }
                }
                else {
                    if (strarr.length > 1) {
                        aa = strarr[1].substring(0, 2);
                        tot = strarr[0] + '.' + aa;
                        $(this).val(tot);
                    }
                    else {
                        tot = strarr[0] + '.00';
                        $(this).val(tot);
                    }
                }

            }
        });

        $('#txtProfit1').change(function () {

            var tot;
            var aa;
            if ($(this).val().length != 0) {
                var strarr = $(this).val().split('.');
                var hypn = $(this).val().split('-');
                if (strarr.length > 2) {
                    jAlert('<strong>Decimal point should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
                else if (hypn.length > 2) {
                    jAlert('<strong> - should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
                else if ($(this).val().indexOf('-') != -1) {
                    if ($(this).val().indexOf('-') != 0) {
                        var val = $(this).val();
                        val = val.replace('-', '');
                        $(this).val("-" + val);
                    }
                }
                else {
                    if (strarr.length > 1) {
                        aa = strarr[1].substring(0, 2);
                        tot = strarr[0] + '.' + aa;
                        $(this).val(tot);
                    }
                    else {
                        tot = strarr[0] + '.00';
                        $(this).val(tot);
                    }
                }

            }
        });
        $('#txtProfit2').change(function () {

            var tot;
            var aa;
            if ($(this).val().length != 0) {
                var strarr = $(this).val().split('.');
                var hypn = $(this).val().split('-');
                if (strarr.length > 2) {
                    jAlert('<strong>Decimal point should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
                else if (hypn.length > 2) {
                    jAlert('<strong> - should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
                else if ($(this).val().indexOf('-') != -1) {
                    if ($(this).val().indexOf('-') != 0) {
                        var val = $(this).val();
                        val = val.replace('-', '');
                        $(this).val("-" + val);
                    }
                }
                else {
                    if (strarr.length > 1) {
                        aa = strarr[1].substring(0, 2);
                        tot = strarr[0] + '.' + aa;
                        $(this).val(tot);
                    }
                    else {
                        tot = strarr[0] + '.00';
                        $(this).val(tot);
                    }
                }

            }
        });
        $('#txtProfit3').change(function () {

            var tot;
            var aa;
            if ($(this).val().length != 0) {
                var strarr = $(this).val().split('.');
                var hypn = $(this).val().split('-');
                if (strarr.length > 2) {
                    jAlert('<strong>Decimal point should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
                else if (hypn.length > 2) {
                    jAlert('<strong> - should not be more than one</strong>', projname);
                    $(this).focus();
                    $(this).val('');
                    return false;
                }
                else if ($(this).val().indexOf('-') != -1) {
                    if ($(this).val().indexOf('-') != 0) {
                        var val = $(this).val();
                        val = val.replace('-', '');
                        $(this).val("-" + val);
                    }
                }
                else {
                    if (strarr.length > 1) {
                        aa = strarr[1].substring(0, 2);
                        tot = strarr[0] + '.' + aa;
                        $(this).val(tot);
                    }
                    else {
                        tot = strarr[0] + '.00';
                        $(this).val(tot);
                    }
                }

            }
        });
        $('#txtCapOthr').keyup(function () {
            this.value = this.value.toUpperCase();
        });
        $('.menuproposal').addClass('active');
        $('#printbtn').click(function () {
            window.print();
        })
        //        var doc = new jsPDF();
        //        var specialElementHandlers = {
        //            '#editor': function (element, renderer) {
        //                return true;
        //            }
        //        };
        //       
        //        $('#conertpdf').click(function () {
        //            doc.fromHTML($('.container').html(), 15, 15, {
        //                'width': 170,
        //                'elementHandlers': specialElementHandlers
        //            });
        //            doc.save('sample-file.pdf');
        //        });

        //        var  
        //         form = $('.form'),
        //         cache_width = form.width(),
        //         a4 = [595.28, 841.89]; // for a4 size paper width and height  

        //        $('#conertpdf').on('click', function () {
        //            $('body').scrollTop(0);
        //            createPDF();
        //        });
        //        //create pdf  
        //        function createPDF() {
        //            getCanvas().then(function (canvas) {
        //                var 
        //                 img = canvas.toDataURL("image/png"),
        //                 doc = new jsPDF({
        //                     unit: 'px',
        //                     format: 'a4'
        //                 });
        //                doc.addImage(img, 'JPEG', 20, 20);
        //                doc.save('Bhavdip-html-to-pdf.pdf');
        //                form.width(cache_width);
        //            });
        //        }

        //        // create canvas object  
        //        function getCanvas() {
        //            form.width((a4[0] * 1.33333) - 80).css('max-width', 'none');
        //            return html2canvas(form, {
        //                imageTimeout: 2000,
        //                removeContainer: true
        //            });
        //        }  

    </script>
    </form>
</body>
</html>
