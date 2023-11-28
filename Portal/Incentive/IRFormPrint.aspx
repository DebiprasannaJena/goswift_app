<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IRFormPrint.aspx.cs" Inherits="Portal_Incentive_IRFormPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SWP</title>
    <link href="~/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
    <style type="text/css">
        .commonHyp
        {
            background: #31b0d5 !important;
            color: #FFF !important;
            width: 20%;
            padding: 4px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#printbtn").click(function () {
                //$(this).css('display', 'none');
                $(".print-area").css({ "border": "0px", "box-shadow": "none", "padding": "10px 0px" });
                window.print();
            });
        });
    </script>
    <style type="text/css">
        body
        {
            padding: 0px;
            margin: 0px;
            font-family: "Sans-Serif" ,Arial;
        }
        .print-area
        {
            margin-top: 10px;
            max-width: 100% !important;
            margin-right: 0px;
        }
        .header
        {
            border: 1px solid #ddd;
            padding: 6px;
            text-align: center;
            margin-bottom: 5px;
        }
        table > tbody > tr > th, table > tbody > tr > td, table > thead > tr > td, table > thead > tr > th
        {
            border: 1px solid #ddd;
        }
        table > tbody > tr > td, table > tbody > tr > th, table > tfoot > tr > td, table > tfoot > tr > th, table > thead > tr > td, table > thead > tr > th
        {
            padding: 2px 0px 2px 8px;
            line-height: 1.3;
            font-size: 13px;
            vertical-align: middle;
        }
        table
        {
            width: 100%;
            max-width: 100%;
            margin-bottom: 10px;
        }
        
        table tr th
        {
            background-color: #eee;
            padding: 8px !important;
            border-bottom: 1px solid #c1c1c1 !important;
            font-size: 14px !important;
        }
        table label
        {
            margin-bottom: 0px;
            font-weight: 600;
        }
        table .fa
        {
            margin-right: 3px;
        }
        .border-top
        {
            border-top: 1px solid #000 !important;
        }
        .border-table > tbody > tr > th, .border-table > tbody > tr > td, .border-table > thead > tr > td, .border-table > thead > tr > th
        {
            border: 1px solid #ababab !important;
        }
        table > tbody > tr > td, table > tbody > tr > th, table > tfoot > tr > td, table > tfoot > tr > th, table > thead > tr > td, table > thead > tr > th
        {
            padding: 1px 0px 1px 8px;
            line-height: 1.2;
        }
        .border-table
        {
            margin: 2px 2px 2px 2px;
            width: 99.5%;
        }
        
        
        
        
        
        .heading-sec h5
        {
            font-size: 15px;
            margin: 5px;
        }
        .heading-sec h4
        {
            font-size: 20px;
            margin-top: 4px;
            margin-bottom: 4px;
        }
        .info-secs
        {
            margin-bottom: 5px;
            width: 100%;
            border: 1px solid #ddd;
            padding: 5px;
        }
        
        .margin-top20
        {
            margin-top: 20px;
        }
        
        #printbtn
        {
            margin-top: 10px;
        }
        .input-div
        {
            height: 20px;
            border: 1px solid #000;
        }
        .padding5
        {
            padding: 0px 5px;
        }
        .info-secs h4
        {
            font-size: 14px;
        }
        .info-secs p
        {
            text-align: justify;
        }
        .info-secs ol
        {
            margin-left: -25px;
        }
        .info-secs ol li
        {
            margin-bottom: 10px;
            text-align: justify;
        }
        .address
        {
            width: 100%;
            margin-bottom: 20px;
        }
        .info-secs ol ol
        {
            list-style-type: lower-alpha;
        }
        .info-secs ol ol li
        {
            margin-left: -12px;
        }
        .info-secs ol ol li:first-child
        {
            margin-left: 0px;
        }
        .info-secs ol ol ol
        {
            list-style-type: lower-roman;
        }
        .info-secs ol ol ol li
        {
            margin-left: 0px;
        }
        .address p
        {
            margin-bottom: 0px;
            line-height: 18px;
            font-size: 13px;
        }
        .address h4
        {
            font-size: 16px;
            margin: 0px;
            margin-bottom: 8px;
        }
        .margin-top50
        {
            margin-top: 50px;
        }
        .no-border > tbody > tr > th, .no-border > tbody > tr > td, .no-border > thead > tr > td, .no-borderthead > tr > th
        {
            border: 0px solid #ddd;
        }
        @media print
        {
            .pagebreak
            {
                page-break-after: always;
            }
            .container
            {
                width: 100%;
            }
            .logo
            {
                height: 60px;
            }
            .heading-sec h1
            {
                margin: 0px;
                font-size: 20px;
                line-height: 22px;
            }
            .heading-sec h4
            {
                font-size: 16px;
                margin-top: 2px;
                margin-bottom: 2px;
            }
            .heading-sec h5
            {
                font-size: 13px;
                margin: 2px;
            }
            .info-secs
            {
                border: 0px solid #ddd;
                padding: 0px;
            }
            .noprint
            {
                display: none;
            }
        
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <div class="text-right">
            <a href="#" class="btn btn-danger btn-sm noprint" id="printbtn">PRINT <i class="fa fa-print">
            </i></a>
        </div>
        <div class="print-area">
            <div class="header" align="center">
                <div class="heading-sec text-center">
                    <h1>
                        Inspection Report</h1>
                    <h5>
                        For Production Certificate
                        <br />
                        In respect of Micro / Small / Medium Enterprises
                        <br />
                        (Inspection / verification of enterprises by RIC / DIC / Directorate of Industries,
                        Odisha / Joint inspection)
                    </h5>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="info-secs padding5">
                <fieldset>
                    <asp:HiddenField ID="hdnInvestorId" runat="server" />
                    <asp:Panel ID="pnlmain" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-bd lobidrag">
                                    <div class="panel-body">
                                        <fieldset>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Date of Inspection
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker">
                                                            <asp:TextBox ID="txtDateOfInspection" class="form-control datePicker" runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            <asp:HiddenField ID="hdnScheduleDt" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="upofficer" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12 ">
                                                                <asp:CheckBox ID="chkverification" runat="server" Text="I / We have physically verified the implementation status of the project (Original/ E/ M / D) / Operational status of the Project and found it in under implementation
                                    / operation." AutoPostBack="true" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12 table-fixedh">
                                                                <asp:GridView ID="grdOffice" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                                                    CssClass="table table-bordered" OnRowCommand="grdCommon_RowCommand" DataKeyNames="id">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="2%">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Name of Officer" DataField="strOfficer" ItemStyle-Width="18%" />
                                                                        <asp:BoundField HeaderText="Designation" DataField="strDesignation" ItemStyle-Width="25%" />
                                                                        <asp:BoundField HeaderText="Organization" DataField="strAuthority" ItemStyle-Width="25%" />
                                                                        <%--    <asp:BoundField HeaderText="Aadhaar No." DataField="AadhaarNo" ItemStyle-Width="25%" />--%>
                                                                        <%-- <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <%--    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Upd" CssClass="btn btn-xs bigger btn-primary noPrint getedit">
                                                                          <i class="ace-icon fa fa-pencil-square-o icon-only"></i>                                                           
                                                                    </asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                            CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="panel panel-bd lobidrag">
                                    <div class="panel-body">
                                        <fieldset>
                                            <legend>Industrial Unit's Details </legend>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Industry Code
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblIndustryCode" runat="server"></asp:Label>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Application No
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnAppNo" runat="server"></asp:HiddenField>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Application For
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:RadioButtonList ID="rdBtnApplicationFor" runat="server" AutoPostBack="true"
                                                            OnSelectedIndexChanged="rdBtnApplicationFor_SelectedIndexChanged" RepeatColumns="2"
                                                            RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="New" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Existing" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:DropDownList ID="drpApplicationType" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div id="divChangeIn" runat="server" visible="false">
                                                        <label class="col-sm-2">
                                                            Change in</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:CheckBoxList ID="chkLstChange" runat="server" RepeatColumns="5" RepeatLayout="Table"
                                                                RepeatDirection="Horizontal">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Unit Category</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="drpUnitCategory" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Nature of Activity</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="drpCompanyType" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Name of Enterprise/Industrial Unit</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtEnterpriseName" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Organization Type</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="drpOrganizationType" CssClass="form-control" runat="server"
                                                            OnSelectedIndexChanged="drpOrganizationType_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <asp:TextBox ID="txtOtherOrg" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="upSector" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-2">
                                                                Sector of Activity</label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <label class="col-sm-2">
                                                                Sub Sector</label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="ddlSubSector" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        EIN No
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtEin" runat="server" MaxLength="9" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" ValidChars="-" TargetControlID="txtEin"
                                                            runat="server" FilterType="Custom,Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Udyog Aadhaar Number
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtUan" runat="server" MaxLength="9" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtUan"
                                                            runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <asp:Label ID="lblOwnerLabel" runat="server" class="col-sm-2"></asp:Label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="drpSalutation" runat="server" CssClass="form-control phcode"
                                                            Style="width: 30%; margin-right: 10px; float: left;">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtOwnerName" MaxLength="100" CssClass="form-control" runat="server"
                                                            Onkeypress="return inputLimiter(event,'NameCharacters')" Style="width: 65%; margin-right: 10px;
                                                            float: left;"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Ownership Pattern</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="drpOwnerType" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="panel panel-bd lobidrag">
                                    <div class="panel-body">
                                        <fieldset>
                                            <legend>Address Details</legend>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Registered Office / Communication Address
                                                            </label>
                                                            <div class="col-sm-7">
                                                                <span class="colon">:</span>
                                                                <asp:TextBox ID="txtOfficeAddress" runat="server" TextMode="MultiLine" Rows="4" Columns="10"
                                                                    MaxLength="200" CssClass="form-control"></asp:TextBox>
                                                                <small>&nbsp;(Maximum&nbsp;
                                                                    <asp:Label ID="lblOfficeAddress" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                                                    &nbsp; characters allowed)</small>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                E-mail</label>
                                                            <div class="col-sm-7">
                                                                <span class="colon">:</span>
                                                                <asp:TextBox ID="txtOfficeEmail" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Address of Enterprise
                                                            </label>
                                                            <div class="col-sm-7">
                                                                <span class="colon">:</span>
                                                                <asp:TextBox ID="txtEnterpriseAddress" runat="server" TextMode="MultiLine" Rows="4"
                                                                    Columns="10" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                                                <small>&nbsp;(Maximum&nbsp;
                                                                    <asp:Label ID="lblRemark" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                                                    &nbsp; characters allowed)</small>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                E-mail</label>
                                                            <div class="col-sm-7">
                                                                <span class="colon">:</span>
                                                                <asp:TextBox ID="txtEmail" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--=============================-->
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Telephone No
                                                            </label>
                                                            <div class="col-sm-3 p-r-0">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="ddlCode" TabIndex="7" runat="server" CssClass="form-control
                phcode">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <asp:TextBox ID="txtOfficePhone" MaxLength="10" CssClass="form-control" runat="server"
                                                                    Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                FAX</label>
                                                            <div class="col-sm-3 p-r-0">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="drpFx" TabIndex="9" runat="server" CssClass="form-control phcode">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <asp:TextBox ID="txtOfficeFax" MaxLength="10" CssClass="form-control" runat="server"
                                                                    Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Website</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:TextBox ID="txtOfficeWebsite" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Telephone No
                                                            </label>
                                                            <div class="col-sm-3 p-r-0">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="drpEntCode" TabIndex="7" runat="server" CssClass="form-control
                phcode">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <asp:TextBox ID="txtPhoneNo" MaxLength="10" CssClass="form-control" runat="server"
                                                                    Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                FAX</label>
                                                            <div class="col-sm-3 p-r-0">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="drpEnterpriseFax" TabIndex="9" runat="server" CssClass="form-control phcode">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <asp:TextBox ID="txtFax" MaxLength="10" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4">
                                                                Website</label>
                                                            <div class="col-sm-8">
                                                                <span class="colon">:</span>
                                                                <asp:TextBox ID="txtWebsite" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="upDistrict" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-4">
                                                                        District</label>
                                                                    <div class="col-sm-7">
                                                                        <span class="colon">:</span>
                                                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                            AutoPostBack="true">
                                                                            <asp:ListItem Text="--Select District--" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-4">
                                                                        Block</label>
                                                                    <div class="col-sm-8">
                                                                        <span class="colon">:</span>
                                                                        <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Text="-Select Block-" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="panel panel-bd lobidrag">
                                    <div class="panel-body">
                                        <fieldset>
                                            <legend>Product details</legend>
                                            <asp:UpdatePanel ID="upProducts" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-md-4 col-lg-3">
                                                                Whether the unit is <small>(as on date of inspection) </small>
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:RadioButtonList ID="rdBtnUntiCond" runat="server" AutoPostBack="true" RepeatColumns="2"
                                                                    RepeatDirection="Vertical" RepeatLayout="Table" OnSelectedIndexChanged="rdBtnUntiCond_SelectedIndexChanged">
                                                                    <asp:ListItem Text="Under Implementation" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Commenced Production" Value="2"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="divProdStartDate" runat="server" visible="false">
                                                            <label class="col-sm-2">
                                                                Date of starting production</label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <div class="input-group  date datePicker">
                                                                    <asp:TextBox ID="Text1" class="form-control datePicker" runat="server" />
                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <h4 class="h4-header">
                                                        Main Category of product</h4>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-2">
                                                                Code(Code may be entered as per NIC 2008) <a data-toggle="tooltip" class="fieldinfo2"
                                                                    title="Accepts 5-digit code only"><i class="fa fa-question-circle" aria-hidden="true">
                                                                    </i></a>
                                                            </label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtProductCode" runat="server" CssClass="form-control" MaxLength="5"
                                                                    Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                                            </div>
                                                            <label class="col-sm-2">
                                                                Name
                                                            </label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="100"
                                                                    Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row" id="divUnitCond" runat="server" visible="false">
                                                            <div class="col-sm-12  margin-bottom10">
                                                                <h4 class="h4-header">
                                                                    Details of Product / Service with capacity -
                                                                    <asp:Label ID="lblUnitCOnd" runat="server"></asp:Label>
                                                                </h4>
                                                                <div class="table-fixedh">
                                                                    <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                                                        CssClass="table table-bordered" DataKeyNames="id" Style="width: 100%;" OnRowDataBound="grdProducts_RowDataBound"
                                                                        OnRowCommand="grdCommon_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="5%">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                    <asp:HiddenField ID="hdnSlnos" runat="server" Value='<%#Container.DataItemIndex+1 %>' />
                                                                                    <asp:HiddenField ID="hdnUnit" runat="server" Value='<%#Eval("UnitId") %>' />
                                                                                    <asp:HiddenField ID="hdnUnitOthers" runat="server" Value='<%#Eval("unitothers") %>' />
                                                                                    <asp:HiddenField ID="hdnIsMainProduct" runat="server" Value='<%#Eval("bitMainProduct") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Product Name" DataField="item" ItemStyle-Width="15%" />
                                                                            <asp:BoundField HeaderText="Code" DataField="Code" ItemStyle-Width="10%" />
                                                                            <asp:BoundField HeaderText="Quantity" DataField="qty" ItemStyle-Width="15%" />
                                                                            <asp:BoundField HeaderText="Unit" DataField="Unit" ItemStyle-Width="15%" />
                                                                            <asp:BoundField HeaderText="Cost" DataField="Cost" ItemStyle-Width="10%" />
                                                                            <asp:BoundField HeaderText="Date of Production" DataField="DtmProd" ItemStyle-Width="15%" />
                                                                            <asp:BoundField HeaderText="Is Main Category" DataField="VchIsMainProduct" ItemStyle-Width="10%" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-2">
                                                                Date of Production Commencement <small></small>
                                                            </label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <div class="input-group  date datePicker">
                                                                    <asp:TextBox ID="txtProdCommencement" class="form-control datePicker" runat="server" />
                                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Connected Power Load with unit for-
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblPowerLoad" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtPowerLoad" runat="server" TextMode="MultiLine" Rows="5" Columns="10"
                                                            MaxLength="200" CssClass="form-control"></asp:TextBox>
                                                        <small>&nbsp;(Maximum&nbsp;
                                                            <asp:Label ID="lblPowerRemarks" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                                            &nbsp; characters allowed)</small>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Details of CPP / D.G set
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtCpp" runat="server" TextMode="MultiLine" Rows="5" Columns="10"
                                                            MaxLength="200" CssClass="form-control"></asp:TextBox>
                                                        <small>&nbsp;(Maximum&nbsp;
                                                            <asp:Label ID="lblCpp" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                                            &nbsp; characters allowed)</small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Date of Power Connection
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <div class="input-group date datePicker">
                                                            <asp:TextBox ID="txtPowerConnection" class="form-control datePicker" runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Date of Commissioning
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <div class="input-group date datePicker">
                                                            <asp:TextBox ID="txtPowerCommisioning" class="form-control datePicker" runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="panel panel-bd lobidrag">
                                    <div class="panel-body">
                                        <fieldset>
                                            <legend>Employement Details</legend>
                                            <div class="row">
                                                <div class="col-sm-12  margin-bottom10">
                                                    <h4 class="h4-header">
                                                        <asp:Label ID="lblEmployement" runat="server"></asp:Label>
                                                    </h4>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Direct Employment (in Numbers) As on Company Payroll</label><div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtDirectEmployement" MaxLength="4" CssClass="form-control phnum"
                                                                runat="server" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                                        </div>
                                                    <label class="col-sm-2">
                                                        Contractual Employment</label><div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtContractualEmp" MaxLength="4" CssClass="form-control phnum" runat="server"
                                                                Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                                        </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Managerial</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtManagarial" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        No. of General Employee</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtGeneral" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Supervisor</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtSupervisor" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        No. of SC Employees</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtTotalSc" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Skilled</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtSkilled" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        No. of ST Employees</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtTotalSt" runat="server" CssClass="form-control" Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Semi Skilled</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtSemiSkilled" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Total
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="4" Text="0" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        UnSkilled</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtUnskilled" MaxLength="4" CssClass="form-control" runat="server"
                                                            Onkeypress="return inputLimiter(event,'Numbers')" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Total Women</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtWomen" MaxLength="4" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Total
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtGrandTotal" MaxLength="6" CssClass="form-control" runat="server"
                                                            ReadOnly="true" ClientIDMode="Static"> </asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-2">
                                                        Total Physically Handicapped</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtPhd" MaxLength="4" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="panel panel-bd lobidrag">
                                    <div class="panel-body">
                                        <fieldset>
                                            <legend>Investment Details </legend>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12  margin-bottom10">
                                                        <h4 class="h4-header">
                                                            Capital Investment (in lakh) for
                                                            <asp:Label ID="lblCapitalInvest" runat="server"></asp:Label>
                                                        </h4>
                                                        <asp:GridView ID="grdCapitalInvestment" runat="server" AutoGenerateColumns="false"
                                                            EmptyDataText="No records found..." CssClass="table table-bordered" DataKeyNames="slno"
                                                            ShowFooter="true">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SlNo.">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="vchName" />
                                                                <asp:TemplateField HeaderText="As per DPR">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAsPerDpr" runat="server" CssClass="form-control dpr" MaxLength="13"
                                                                            Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                                            onblur="isNumberBlur(event, this, 2);" Text='<%#Eval("decAsPerDpr") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <span class="spDpr"></span>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual expenditure incurred">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtActualExpIncuured" runat="server" CssClass="form-control exp"
                                                                            MaxLength="13" Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                                            onblur="isNumberBlur(event, this, 2);" Text='<%#Eval("decActualExp") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <span class="spExp"></span>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="row" id="dvFFCIOld" runat="server">
                                                    <label class="col-sm-4">
                                                        Date of First Fixed Capital Investment (Original) <small>
                                                            <br />
                                                            (for land/Building/plant and machinery & Balancing Equipment)</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker">
                                                            <asp:TextBox ID="txtDateFFIOld" class="form-control datePicker" runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Date of First Fixed Capital Investment <small>
                                                            <br />
                                                            (for &nbsp;<asp:Label ID="lblModeOfInvestment" runat="server"></asp:Label>)</small></label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker">
                                                            <asp:TextBox ID="txtDateFFI" class="form-control datePicker" runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-4">
                                                        Date of investment of Plant and machinery</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker">
                                                            <asp:TextBox ID="txtDateOfPlant" class="form-control datePicker" runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="upCapital" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <h4 class="h4-header">
                                                                    Term Loan Details</h4>
                                                                <div class="table-fixedh">
                                                                    <asp:GridView ID="grdTermLoan" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                                                        CssClass="table table-bordered" OnRowCreated="grdTermLoan_RowCreated" OnRowDataBound="grdTermLoan_RowDataBound"
                                                                        OnRowCommand="grdCommon_RowCommand" DataKeyNames="id">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="2%">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Name of Financial Institution" DataField="institute"
                                                                                ItemStyle-Width="15%" />
                                                                            <asp:BoundField HeaderText="State" DataField="State" ItemStyle-Width="10%" />
                                                                            <asp:BoundField HeaderText="City" DataField="City" ItemStyle-Width="10%" />
                                                                            <asp:BoundField HeaderText="Term Loan Amount" DataField="Amt" ItemStyle-Width="13%" />
                                                                            <asp:BoundField HeaderText="Sanction Date" DataField="SanctionDate" ItemStyle-Width="15%" />
                                                                            <asp:BoundField HeaderText="Availed Amount" DataField="AvailedAmt" ItemStyle-Width="15%" />
                                                                            <asp:BoundField HeaderText="Availed Date" DataField="AvailedDate" ItemStyle-Width="15%" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="upWCapital" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <h4 class="h4-header">
                                                                    Working Capital Details</h4>
                                                                <div class="table-fixedh">
                                                                    <asp:GridView ID="grdWorkingCapital" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                                                        CssClass="table table-bordered" OnRowCreated="grdTermLoan_RowCreated" OnRowDataBound="grdTermLoan_RowDataBound"
                                                                        OnRowCommand="grdCommon_RowCommand" DataKeyNames="id">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="2%">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Name of Financial Institution" DataField="institute"
                                                                                ItemStyle-Width="15%" />
                                                                            <asp:BoundField HeaderText="State" DataField="State" ItemStyle-Width="10%" />
                                                                            <asp:BoundField HeaderText="City" DataField="City" ItemStyle-Width="10%" />
                                                                            <asp:BoundField HeaderText="Amount" DataField="Amt" ItemStyle-Width="13%" />
                                                                            <asp:BoundField HeaderText="Sanction Date" DataField="SanctionDate" ItemStyle-Width="15%" />
                                                                            <asp:BoundField HeaderText="Availed Amount" DataField="AvailedAmt" ItemStyle-Width="15%" />
                                                                            <asp:BoundField HeaderText="Availed Date" DataField="AvailedDate" ItemStyle-Width="15%" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="panel panel-bd lobidrag">
                                    <div class="panel-body">
                                        <fieldset>
                                            <legend>Investment Approval Details </legend>
                                            <asp:UpdatePanel ID="upApproval" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <h4 class="h4-header">
                                                                    Whether the site / structure plan has been approved for
                                                                    <asp:Label ID="lblApproval" runat="server" /></h4>
                                                                <div class="table-fixedh">
                                                                    <asp:GridView ID="grdApproval" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                                                        CssClass="table table-bordered" OnRowCommand="grdCommon_RowCommand" OnRowDataBound="grdApproval_RowDataBound"
                                                                        DataKeyNames="id">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="2%">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Name of site / structure plan" DataField="name" ItemStyle-Width="33%" />
                                                                            <asp:TemplateField HeaderText="Approval Authority" ItemStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField ID="hdnApprovalId" Value='<%#Eval("authority") %>' runat="server" />
                                                                                    <asp:HiddenField ID="hdnApprovalName" Value='<%#Eval("authorityName") %>' runat="server" />
                                                                                    <asp:HiddenField ID="hdnOthers" Value='<%#Eval("Others") %>' runat="server" />
                                                                                    <asp:Label ID="lblValue" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Supporting Document" ItemStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink CssClass="input-group-addon bg-blue" ID="hypViewProductFile" Visible="false"
                                                                                        runat="server" Target="_blank" Style="background: #31b0d5 !important; color: #FFF !important;"> <i class="fa fa-download"></i></asp:HyperLink>
                                                                                    </div>
                                                                                    <asp:HiddenField ID="hdnProductFile" runat="server" Value='<%#Eval("filename") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <h4 class="h4-header">
                                                Plant & Machinery
                                            </h4>
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-sm-12  margin-bottom10">
                                                                <asp:GridView ID="gvPlant" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                                                    CssClass="table table-bordered" DataKeyNames="id" OnRowCommand="gvPlant_RowCommand"
                                                                    Style="width: 100%;">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Plant & Machinery Name" DataField="MachineryName" />
                                                                        <asp:BoundField HeaderText="Date of Purchase" DataField="DateofPurchase" />
                                                                        <asp:BoundField HeaderText="Investment Amount" DataField="Cost" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <%--<asp:PostBackTrigger ControlID="lnkProductAdd" />--%>
                                                        <%-- <asp:PostBackTrigger ControlID="lnkMachinery" />--%>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <asp:UpdatePanel ID="upClearence" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <h4 class="h4-header">
                                                                    List of valid Statutory Clearances
                                                                    <asp:Label ID="lblStatutaryCLearence" runat="server"></asp:Label>
                                                                </h4>
                                                                <div class="table-fixedh">
                                                                    <asp:GridView ID="grdStatutoryClearence" runat="server" AutoGenerateColumns="false"
                                                                        EmptyDataText="No Records Found" CssClass="table table-bordered" OnRowCommand="grdCommon_RowCommand"
                                                                        OnRowDataBound="grdStatutoryClearence_RowDataBound" OnRowCreated="grdStatutoryClearence_RowCreated"
                                                                        DataKeyNames="id">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="2%">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                    <asp:HiddenField ID="hdnClearence" runat="server" Value='<%#Eval("clearence") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Statutory Clearance" DataField="clearencename" ItemStyle-Width="33%" />
                                                                            <asp:BoundField HeaderText="From Date" DataField="fromDate" ItemStyle-Width="30%" />
                                                                            <asp:BoundField HeaderText="To Date" DataField="toDate" ItemStyle-Width="30%" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <h4 class="h4-header">
                                                                    Whether same management is having other enterprises, if so, details there of
                                                                </h4>
                                                                <div class="table-fixedh">
                                                                    <asp:GridView ID="gvLOCDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                                                        OnRowCommand="grdCommon_RowCommand" EmptyDataText="No records found..." DataKeyNames="id">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SlNo.." ItemStyle-Width="2%">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                    <asp:HiddenField ID="hdnState" runat="server" Value='<%#Eval("state") %>' />
                                                                                    <asp:HiddenField ID="hdnDistrict" runat="server" Value='<%#Eval("district") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Unit Name" DataField="unitname" ItemStyle-Width="18%" />
                                                                            <asp:BoundField HeaderText="Product" DataField="product" ItemStyle-Width="20%" />
                                                                            <asp:BoundField HeaderText="Total Capacity" DataField="totalCapacity" ItemStyle-Width="20%" />
                                                                            <asp:BoundField HeaderText="State" DataField="statename" ItemStyle-Width="20%" />
                                                                            <asp:BoundField HeaderText="District" DataField="distname" ItemStyle-Width="15%" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <h4 class="h4-header">
                                                                    It is verified that the applicant unit has availed / applied for following incentives.
                                                                </h4>
                                                                <div class="table-fixedh">
                                                                    <asp:GridView ID="grdIncentiveApplied" runat="server" AutoGenerateColumns="false"
                                                                        EmptyDataText="No Records Found" CssClass="table table-bordered" OnRowCommand="grdCommon_RowCommand"
                                                                        DataKeyNames="id">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="5%">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                    <asp:HiddenField ID="hdnType" runat="server" Value='<%#Eval("type") %>' />
                                                                                    <asp:HiddenField ID="hdnIpr" runat="server" Value='<%#Eval("ipr") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="incentiveText" HeaderText="Incentive Type" ItemStyle-Width="30%" />
                                                                            <asp:BoundField DataField="quantam" HeaderText="Amount (in Rs.)" ItemStyle-Width="20%" />
                                                                            <asp:BoundField DataField="fromdate" HeaderText="From" ItemStyle-Width="15%" />
                                                                            <asp:BoundField DataField="todate" HeaderText="To" ItemStyle-Width="15%" />
                                                                            <asp:BoundField DataField="IPRText" HeaderText="IPR Applicability" ItemStyle-Width="10%" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12  margin-bottom10">
                                                                <h4 class="h4-header">
                                                                    Problems if any in obtaining
                                                                </h4>
                                                                <asp:GridView ID="grdProblems" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                                                    CssClass="table table-bordered" DataKeyNames="slno" OnRowDataBound="grdProblems_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SlNo.">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="vchName" HeaderText="Problems" />
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtProblems" runat="server" TextMode="MultiLine" Rows="2" Columns="10"
                                                                                    MaxLength="200" CssClass="form-control" Text='<%#Eval("remarks") %>'></asp:TextBox>
                                                                                <small>&nbsp;(Maximum&nbsp;
                                                                                    <asp:Label ID="lblProblems" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                                                                    &nbsp; characters allowed)</small>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-2">
                                                                Remark(s)
                                                            </label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="10"
                                                                    MaxLength="200" CssClass="form-control"></asp:TextBox>
                                                                <small>&nbsp;(Maximum&nbsp;
                                                                    <asp:Label ID="lblEndRemarks" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                                                    &nbsp; characters allowed)</small>
                                                            </div>
                                                            <label class="col-sm-2">
                                                                Suggestions/Views
                                                            </label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:TextBox ID="txtSuggestions" runat="server" TextMode="MultiLine" Rows="5" Columns="10"
                                                                    MaxLength="200" CssClass="form-control"></asp:TextBox>
                                                                <small>&nbsp;(Maximum&nbsp;
                                                                    <asp:Label ID="lblSuggestion" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                                                    &nbsp; characters allowed)</small>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-12  margin-bottom10">
                                <h4 class="h4-header">
                                    Documents submitted by the investor
                                </h4>
                                <asp:GridView ID="grdFiles" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                    DataKeyNames="vchDocId" OnRowDataBound="grdFiles_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SlNo.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Document Name" DataField="vchDocName" />
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:HyperLink CssClass=" commonHyp bg-blue" ID="hypViewProductFile" runat="server"
                                                    Target="_blank" Style="background: #31b0d5 !important; color: #FFF !important;"> <i class="fa fa-download"></i></asp:HyperLink>
                                                <asp:HiddenField ID="hdnFileName" Value='<%#Eval("VCHFILENAME") %>' runat="server" />
                                                <asp:HiddenField ID="hdnFolderName" Value='<%#Eval("vchFolderPath") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="upSignature" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="row" id="divSignatureUpload" runat="server">
                                    <label class="col-sm-4 col-sm-offset-1">
                                        Signature & designation of Inspecting Officer of RIC/ DIC/ DI, Odisha,
                                        <asp:Label ID="lblDist" runat="server" Text="Label"></asp:Label>
                                        with Date</label>
                                    <div id="dvSign" class="col-sm-6" visible="false">
                                        <asp:Image ID="imgSignature" runat="server" Width="100" Height="100" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="form-group" style="display: none;">
                        <div class="row" style="display: none;">
                            <asp:Button ID="btnConfirm" runat="server" Text="Submit" CssClass="btn btn-success"
                                OnClick="btnConfirm_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
