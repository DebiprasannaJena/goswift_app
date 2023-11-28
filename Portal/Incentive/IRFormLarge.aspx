<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IRFormLarge.aspx.cs" MasterPageFile="~/MasterPage/Application.master"
    Inherits="Portal_Incentive_IRFormLarge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/WebValidation.js"></script>
    <script src="../../js/decimalrstr.js" type="text/javascript"></script>
    <script src="../js/IRFormLarge.js" type="text/javascript"></script>
    <style type="text/css">
        .commonHyp
        {
            background: #31b0d5 !important;
            color: #FFF !important;
            width: 20%;
            padding: 4px;
        }
        .table-fixedh
        {
            max-height: 100px !important;
            overflow-x: hidden;
            overflow-y: auto;
        }
        textarea.form-control
        {
            height: 55px;
        }
        .m-r-0
        {
            margin-right: 0px !important;
        }
        .p-r-0
        {
            padding-right: 0px !important;
        }
    </style>
    <div class="content-wrapper">
        <asp:ScriptManager ID="sm1" runat="server">
        </asp:ScriptManager>
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Incentive</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>View Incentive</a></li>
                </ul>
            </div>
        </section>
        <section class="content">
            <div class="info-secs padding5">
                <asp:HiddenField ID="hdnInvestorId" runat="server" />
                <asp:Panel ID="pnlmain" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-bd lobidrag">
                                <div class="panel-body">
                                    <fieldset>
                                        <legend>Inspection Report</legend>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Date of Inspection
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <div class="input-group date datePicker">
                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="txtDateOfInspection" class="form-control datePicker"
                                                            runat="server">
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
                                                            <asp:CheckBox ID="chkverification" runat="server" Text="I / We have physically verified the implementation status of the project (Original/ E/ M / D) / Operational status of the Project and found it in under implementation/ operation." />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12 ">
                                                            <table id="tblOfficer" runat="server" class="table table-bordered">
                                                                <tr>
                                                                    <th style="width: 20%;">
                                                                        Name of the Officer
                                                                    </th>
                                                                    <th style="width: 25%;">
                                                                        Designation
                                                                    </th>
                                                                    <th style="width: 25%;">
                                                                        Organization
                                                                    </th>
                                                                    <th style="width: 5%;">
                                                                        Action
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOfficerName" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                                            MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteOfficeName" runat="server" FilterMode="InvalidChars"
                                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;.,{}+=[];'.,/|\_-~`" TargetControlID="txtOfficerName">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                                            MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteDesignation" runat="server" FilterMode="InvalidChars"
                                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;.,{}+=[];'.,/|\_-~`" TargetControlID="txtDesignation">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOrganization" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                                            MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteOrganization" runat="server" FilterMode="InvalidChars"
                                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;.,{}+=[];'.,/|\_-~`" TargetControlID="txtOrganization">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkOfficerAdd" CssClass="btn btn-success btn-sm" runat="server"
                                                                            CommandName="add" OnClick="lnkAdd_Click"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
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
                                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
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
                                                    Name of Industrial Unit</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtEnterpriseName" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                                </div>
                                                <label class="col-sm-2">
                                                    Constitution of Organization
                                                </label>
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
                                                    IEM/IL
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtEin" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" ValidChars="-" TargetControlID="txtEin"
                                                        runat="server" FilterType="Custom,Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <label class="col-sm-2">
                                                    Date of IEM/IL Issuance
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <div class="input-group  date datePicker" id="Div7">
                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="txtDateOfIssuance" class="form-control"
                                                            runat="server" />
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Udyog Aadhaar Number
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtUan" runat="server" MaxLength="12" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtUan"
                                                        runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <label class="col-sm-2">
                                                    GSTIN
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtGST" runat="server" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fteGst" TargetControlID="txtGST" runat="server"
                                                        FilterType="LowercaseLetters,UppercaseLetters,Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <asp:Label ID="lblOwnerLabel" runat="server" class="col-sm-2" Style="font-weight: 600"></asp:Label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="drpSalutation" runat="server" CssClass="form-control phcode"
                                                        Style="width: 30%; margin-right: 10px; float: left;">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtOwnerName" MaxLength="100" CssClass="form-control" runat="server"
                                                        Onkeypress="return inputLimiter(event,'NameCharacters')" Style="width: 62%; margin-right: 10px;
                                                        float: left;"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fteOwnername" runat="server" FilterMode="InvalidChars"
                                                        InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtOwnerName">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <div style="clear: both">
                                                    </div>
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
                                                            <cc1:FilteredTextBoxExtender ID="fteOfficeAddress" runat="server" FilterMode="InvalidChars"
                                                                InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;{}+=[];'|\~`" TargetControlID="txtOfficeAddress">
                                                            </cc1:FilteredTextBoxExtender>
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
                                                            <cc1:FilteredTextBoxExtender ID="fteOfficeEmail" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                                ValidChars="@.-_" TargetControlID="txtOfficeEmail">
                                                            </cc1:FilteredTextBoxExtender>
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
                                                            <cc1:FilteredTextBoxExtender ID="fteEnterpriseAddress" runat="server" FilterMode="InvalidChars"
                                                                InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;{}+=[];'|\~`" TargetControlID="txtEnterpriseAddress">
                                                            </cc1:FilteredTextBoxExtender>
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
                                                            <cc1:FilteredTextBoxExtender ID="fteEmail" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                                ValidChars="@.-_" TargetControlID="txtEmail">
                                                            </cc1:FilteredTextBoxExtender>
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
                                                            <asp:DropDownList ID="ddlCode" TabIndex="7" runat="server" CssClass="form-control phcode">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <asp:TextBox ID="txtOfficePhone" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="fteOfficePhone" TargetControlID="txtOfficePhone"
                                                                runat="server" FilterType="Numbers">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">
                                                            FAX</label>
                                                        <div class="col-sm-3 p-r-0">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="drpFx" runat="server" CssClass="form-control phcode">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <asp:TextBox ID="txtOfficeFax" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="fteOfficeFax" TargetControlID="txtOfficeFax" runat="server"
                                                                FilterType="Numbers">
                                                            </cc1:FilteredTextBoxExtender>
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
                                                            <cc1:FilteredTextBoxExtender ID="fteOfficeWebsite" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                                ValidChars="." TargetControlID="txtOfficeWebsite">
                                                            </cc1:FilteredTextBoxExtender>
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
                                                            <asp:TextBox ID="txtPhoneNo" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftePhoneNo" TargetControlID="txtPhoneNo" runat="server"
                                                                FilterType="Numbers">
                                                            </cc1:FilteredTextBoxExtender>
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
                                                            <asp:TextBox ID="txtFax" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="fteFax" TargetControlID="txtFax" runat="server"
                                                                FilterType="Numbers">
                                                            </cc1:FilteredTextBoxExtender>
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
                                                            <cc1:FilteredTextBoxExtender ID="fteWebsite" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                                ValidChars="." TargetControlID="txtWebsite">
                                                            </cc1:FilteredTextBoxExtender>
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
                                                                <asp:TextBox ID="txtProductCode" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="fteProductCode" TargetControlID="txtProductCode"
                                                                    runat="server" FilterType="Numbers">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </div>
                                                            <label class="col-sm-2">
                                                                Name
                                                            </label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="100"
                                                                    Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="fteProductName" runat="server" FilterMode="InvalidChars"
                                                                    InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;.,{}+=[];',/|\_-~`" TargetControlID="txtName">
                                                                </cc1:FilteredTextBoxExtender>
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
                                                                <table class="table table-bordered" runat="server" id="tblProducts">
                                                                    <tr>
                                                                        <th style="width: 5%">
                                                                            Is Main Category
                                                                        </th>
                                                                        <th style="width: 15%">
                                                                            Product Name
                                                                        </th>
                                                                        <th style="width: 15%">
                                                                            Code <a data-toggle="tooltip" class="fieldinfo2" title="Accepts 5-digit code only"><i
                                                                                class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                        </th>
                                                                        <th colspan="2" style="width: 25%">
                                                                            Quantity
                                                                        </th>
                                                                        <th style="width: 15%">
                                                                            Cost
                                                                        </th>
                                                                        <th style="width: 15%">
                                                                            Date of Production
                                                                        </th>
                                                                        <th style="width: 5%">
                                                                            Action
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkMainCategory" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtItemProduct" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteItemProduct" runat="server" FilterMode="InvalidChars"
                                                                                InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtItemProduct">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtItemCode" runat="server" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteItemCode" runat="server" TargetControlID="txtItemCode"
                                                                                FilterType="Numbers">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtQuantity" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox><br />
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtQuantity"
                                                                                FilterType="Numbers">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="drpUnitType" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                                OnSelectedIndexChanged="drpUnitType_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox ID="txtUnitType" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                                                Visible="false"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCost" runat="server" CssClass="form-control" MaxLength="11" onkeypress="return FloatOnly(event, this);"
                                                                                onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteCost" TargetControlID="txtCost" runat="server"
                                                                                FilterType="Custom,Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <div class="input-group  date datePicker">
                                                                                <asp:TextBox ID="txtDateOfProd" class="form-control datePicker" runat="server" />
                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm" runat="server" OnClick="lnkAdd_Click"
                                                                                CommandName="add"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                                            <asp:HiddenField ID="hdnSlNo" runat="server"></asp:HiddenField>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                                                            <asp:TemplateField HeaderText="Edit/Delete" ItemStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Upd" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                        CssClass="btn btn-xs bigger btn-primary noPrint getedit">
                                                                             <i class="ace-icon fa fa-pencil-square-o icon-only bigger-110"></i>     </asp:LinkButton>
                                                                                    <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                        CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
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
                                                        <cc1:FilteredTextBoxExtender ID="ftePowerLoad" runat="server" FilterMode="InvalidChars"
                                                            InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;{}+=[];'|\~`" TargetControlID="txtPowerLoad">
                                                        </cc1:FilteredTextBoxExtender>
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
                                                        <cc1:FilteredTextBoxExtender ID="fteCpp" runat="server" FilterMode="InvalidChars"
                                                            InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;{}+=[];'|\~`" TargetControlID="txtCpp">
                                                        </cc1:FilteredTextBoxExtender>
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
                                                        <asp:TextBox ID="txtDirectEmployement" MaxLength="6" CssClass="form-control phnum"
                                                            runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteDirectEmployement" TargetControlID="txtDirectEmployement"
                                                            runat="server" FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                <label class="col-sm-2">
                                                    Contractual Employment</label><div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtContractualEmp" MaxLength="4" CssClass="form-control phnum" runat="server"
                                                            ClientIDMode="Static"> </asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteContractualEmp" TargetControlID="txtContractualEmp"
                                                            runat="server" FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Managerial</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtManagarial" runat="server" CssClass="form-control" MaxLength="6"
                                                        Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fteManagarial" TargetControlID="txtManagarial" runat="server"
                                                        FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <label class="col-sm-2">
                                                    No. of General Employee</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtGeneral" runat="server" CssClass="form-control" MaxLength="6"
                                                        Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fteGeneral" TargetControlID="txtGeneral" runat="server"
                                                        FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Supervisor</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtSupervisor" runat="server" CssClass="form-control" MaxLength="6"
                                                        Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fteSupervisor" TargetControlID="txtSupervisor" runat="server"
                                                        FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <label class="col-sm-2">
                                                    No. of SC Employees</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtTotalSc" runat="server" CssClass="form-control" MaxLength="6"
                                                        Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fteTotalSc" TargetControlID="txtTotalSc" runat="server"
                                                        FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Skilled</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtSkilled" runat="server" CssClass="form-control" MaxLength="6"
                                                        Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ftetxtSkilled" TargetControlID="txtSkilled" runat="server"
                                                        FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <label class="col-sm-2">
                                                    No. of ST Employees</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtTotalSt" runat="server" CssClass="form-control" MaxLength="6"
                                                        Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fteTotalSt" TargetControlID="txtTotalSt" runat="server"
                                                        FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Semi Skilled</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtSemiSkilled" runat="server" CssClass="form-control" MaxLength="6"
                                                        Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fteSemiSkilled" TargetControlID="txtSemiSkilled"
                                                        runat="server" FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <label class="col-sm-2">
                                                    Total
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                        MaxLength="6" Text="0" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    UnSkilled</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtUnskilled" runat="server" CssClass="form-control" MaxLength="6"
                                                        Text="0" ClientIDMode="Static"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fteUnSKilled" TargetControlID="txtUnskilled" runat="server"
                                                        FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <label class="col-sm-2">
                                                    Total Women</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtWomen" runat="server" CssClass="form-control" MaxLength="6" Text="0"
                                                        ClientIDMode="Static"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fteWomen" TargetControlID="txtWomen" runat="server"
                                                        FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
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
                                                    Total Differently Abled</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtPhd" runat="server" CssClass="form-control" MaxLength="6" Text="0"
                                                        ClientIDMode="Static"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ftePhd" TargetControlID="txtPhd" runat="server"
                                                        FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
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
                                                                        onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"
                                                                        Text='<%#Eval("decAsPerDpr") %>'></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteAsPerDpr" TargetControlID="txtAsPerDpr" runat="server"
                                                                        FilterType="Custom,Numbers" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <span class="spDpr"></span>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Actual expenditure incurred">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtActualExpIncuured" runat="server" CssClass="form-control exp"
                                                                        MaxLength="13" onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"
                                                                        Text='<%#Eval("decActualExp") %>'></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteActualExpIncurred" TargetControlID="txtActualExpIncuured"
                                                                        runat="server" FilterType="Custom,Numbers" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <span class="spExp"></span>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="row" id="dvFFCIOld" runat="server" visible="false">
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
                                                        (for &nbsp;
                                                        <asp:Label ID="lblModeOfInvestment" runat="server"></asp:Label>)</small></label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <div class="input-group date
                datePicker">
                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="txtDateFFI" class="form-control datePicker"
                                                            runat="server">
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
                                                            <table class="table table-bordered" runat="server" id="tblTermLoan">
                                                                <tr>
                                                                    <th rowspan="2" style="width: 15%;">
                                                                        Name of Financial Institution
                                                                    </th>
                                                                    <th colspan="2" style="width: 20%;">
                                                                        Location
                                                                    </th>
                                                                    <th rowspan="2" style="width: 15%;">
                                                                        Amount (in Lakh)
                                                                    </th>
                                                                    <th rowspan="2" style="width: 15%;">
                                                                        Sanction Date
                                                                    </th>
                                                                    <th rowspan="2" style="width: 15%;">
                                                                        Availed Amount
                                                                    </th>
                                                                    <th rowspan="2" style="width: 15%;">
                                                                        Availed Date
                                                                    </th>
                                                                    <th rowspan="2" style="width: 5%;">
                                                                        Action
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <th>
                                                                        State
                                                                    </th>
                                                                    <th>
                                                                        City
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTlInstitue" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteTlInstitute" runat="server" FilterMode="InvalidChars"
                                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtTlInstitue">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtState" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteState" runat="server" FilterMode="InvalidChars"
                                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtState">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCity" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteCity" runat="server" FilterMode="InvalidChars"
                                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtCity">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" MaxLength="13"
                                                                            onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteAmount" TargetControlID="txtAmount" runat="server"
                                                                            FilterType="Custom,Numbers" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker">
                                                                            <asp:TextBox ID="txtSanctionDate" class="form-control datePicker" runat="server" />
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAvailedAmount" runat="server" CssClass="form-control" MaxLength="13"
                                                                            onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteAvailedAmount" TargetControlID="txtAvailedAmount"
                                                                            runat="server" FilterType="Custom,Numbers" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker">
                                                                            <asp:TextBox ID="txtAvailedDate" class="form-control datePicker" runat="server" />
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkTermAdd" CssClass="btn btn-success btn-sm" runat="server"
                                                                            OnClick="lnkAdd_Click" CommandName="add"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                    CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
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
                                        <asp:UpdatePanel ID="upWCapital" runat="server">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <h4 class="h4-header">
                                                                Working Capital Details</h4>
                                                            <table class="table table-bordered" runat="server" id="Table1">
                                                                <tr>
                                                                    <th rowspan="2" style="width: 15%;">
                                                                        Name of Financial Institution
                                                                    </th>
                                                                    <th colspan="2" style="width: 20%;">
                                                                        Location
                                                                    </th>
                                                                    <th rowspan="2" style="width: 15%;">
                                                                        Amount (in Lakh)
                                                                    </th>
                                                                    <th rowspan="2" style="width: 15%;">
                                                                        Sanction Date
                                                                    </th>
                                                                    <th rowspan="2" style="width: 15%;">
                                                                        Availed Amount
                                                                    </th>
                                                                    <th rowspan="2" style="width: 15%;">
                                                                        Availed Date
                                                                    </th>
                                                                    <th rowspan="2" style="width: 5%;">
                                                                        Action
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <th>
                                                                        State
                                                                    </th>
                                                                    <th>
                                                                        City
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWcInstitue" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteWcInstitute" runat="server" FilterMode="InvalidChars"
                                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtWcInstitue">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWcState" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteWcState" runat="server" FilterMode="InvalidChars"
                                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtWcState">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWcCity" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteWcCity" runat="server" FilterMode="InvalidChars"
                                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtWcCity">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWcAmt" runat="server" CssClass="form-control" MaxLength="13"
                                                                            onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteWcAmt" TargetControlID="txtWcAmt" runat="server"
                                                                            FilterType="Custom,Numbers" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker">
                                                                            <asp:TextBox ID="txtWcSanctionDate" class="form-control datePicker" runat="server" />
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWcAvailedAmt" runat="server" CssClass="form-control" MaxLength="13"
                                                                            onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteWcAvailedAmt" TargetControlID="txtWcAvailedAmt"
                                                                            runat="server" FilterType="Custom,Numbers" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker">
                                                                            <asp:TextBox ID="txtWcAvailedDate" class="form-control datePicker" runat="server" />
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkWcAdd" CssClass="btn btn-success btn-sm" runat="server" OnClick="lnkAdd_Click"
                                                                            CommandName="add"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                    CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
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
                                                                Whether the site / structure plan has been approved
                                                                <asp:Label ID="lblApproval" runat="server" /></h4>
                                                            <table class="table table-bordered" id="tblApproval" runat="server">
                                                                <tr>
                                                                    <td style="width: 35%;">
                                                                        Name of site / structure plan
                                                                    </td>
                                                                    <td style="width: 30%;">
                                                                        Approval Authority
                                                                    </td>
                                                                    <td style="width: 30%;">
                                                                        Supporting Documents
                                                                    </td>
                                                                    <td style="width: 5%;">
                                                                        Action
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNameOfSite" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteNameOfSite" runat="server" FilterMode="InvalidChars"
                                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtNameOfSite">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpApprovalAuthority" runat="server" CssClass="form-control"
                                                                            OnSelectedIndexChanged="drpApprovalAuthority_SelectedIndexChanged" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField ID="hdnAuthority" runat="server" />
                                                                        <asp:TextBox ID="txtOthers" CssClass="form-control" runat="server" Visible="false"
                                                                            Text='<%#Eval("others") %>' MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="fteOthers" runat="server" FilterMode="InvalidChars"
                                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtOthers">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group">
                                                                            <asp:FileUpload ID="fuSupportingDocuments" CssClass="form-control" runat="server" />
                                                                            <asp:Label ID="lblSupDocument" runat="server"></asp:Label>
                                                                            <asp:HiddenField ID="hdnSupDocument" runat="server" />
                                                                            <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                            <asp:LinkButton ID="lnkSupDocUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                                OnClick="lnkSupDocUpload_Click"><i class="fa fa-upload" aria-hidden="true" style="color:#FFF;" ></i></asp:LinkButton>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkSupDocAdd" CssClass="btn btn-success btn-sm" runat="server"
                                                                            OnClick="lnkAdd_Click" CommandName="add"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                                                <asp:HiddenField ID="hdnProductFile" runat="server" Value='<%#Eval("filename") %>'
                                                                                    ClientIDMode="Static" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                    CommandName="D"> <i class="fa fa-trash" ></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkSupDocUpload" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <h4 class="h4-header">
                                            Plant & Machinery
                                        </h4>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-12  margin-bottom10">
                                                            <table class="table table-bordered" id="tblMachinery" runat="server">
                                                                <tr>
                                                                    <th style="width: 15%">
                                                                        Plant & Machinery Name<span class="text-red">*</span>
                                                                    </th>
                                                                    <th style="width: 15%">
                                                                        Date of Purchase <span class="text-red">*</span>
                                                                    </th>
                                                                    <th style="width: 15%">
                                                                        Investment Amount<span class="text-red">*</span>
                                                                    </th>
                                                                    <th style="width: 5%">
                                                                        Add
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtMachinery" runat="server" Width="70%" CssClass="form-control"
                                                                            MaxLength="100"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtMachinery"
                                                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                                            ValidChars=" ">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group date datePicker">
                                                                            <asp:TextBox ID="txtDateofPurchase" runat="server" MaxLength="40" CssClass="form-control date-picker"></asp:TextBox>
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAmt" runat="server" MaxLength="10" Width="50%" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtAmt"
                                                                            runat="server" FilterType="Custom,Numbers" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkMachinery" CssClass="btn btn-success btn-sm" runat="server"
                                                                            OnClick="lnkMachinery_Click" CommandName="add"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
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
                                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <%--<asp:PostBackTrigger ControlID="lnkProductAdd" />--%>
                                                    <asp:PostBackTrigger ControlID="lnkMachinery" />
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
                                                            <table id="tblClearence" runat="server" class="table table-bordered">
                                                                <tr>
                                                                    <th rowspan="2" style="width: 35%;">
                                                                        Statutory Clearence
                                                                    </th>
                                                                    <th colspan="2">
                                                                        Period
                                                                    </th>
                                                                    <th rowspan="2" style="width: 5%;">
                                                                        Action
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <th style="width: 30%;">
                                                                        From Date
                                                                    </th>
                                                                    <th style="width: 30%;">
                                                                        To Date
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpClearence" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker">
                                                                            <asp:TextBox ID="txtFromClDate" class="form-control datePicker" runat="server" />
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="input-group  date datePicker">
                                                                            <asp:TextBox ID="txtToClDate" class="form-control datePicker" runat="server" />
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkClearenceAdd" CssClass="btn btn-success btn-sm" runat="server"
                                                                            OnClick="lnkAdd_Click"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                    CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
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
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-bd lobidrag">
                            <div class="panel-body">
                                <fieldset>
                                    <legend>Documents submitted by the investor</legend>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-12  margin-bottom10">
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
                                                    <label class="col-sm-4">
                                                        Signature & designation of Inspecting Officer of RIC/ DIC/ DI, Odisha,
                                                        <asp:Label ID="lblDist" runat="server" Text="Label"></asp:Label>
                                                        with Date</label>
                                                    <div class="col-sm-6" id="dvSig" runat="server">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="fuSignature" CssClass="form-control" runat="server" />
                                                            <asp:HiddenField ID="hdnSignature" runat="server" />
                                                            <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <asp:LinkButton ID="lnkUploadSignature" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkUploadSignature_Click" Style="color: White;"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkUploadSignatureDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkUploadSignatureDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypUploadSignature" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue"> <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                        <asp:Label ID="lblUploadSignature" Style="font-size: 12px;" CssClass="text-blue"
                                                            Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                    </div>
                                                    <div id="dvSign" class="col-sm-6">
                                                        <asp:Image ID="imgSignature" runat="server" Width="100" Height="100" Visible="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkUploadSignature" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:Button ID="btnDraft" runat="server" Text="Save As Draft" CssClass="btn btn-success"
                                                    CommandArgument="d" OnClick="btnConfirm_Click" />
                                                <asp:Button ID="btnConfirm" runat="server" Text="Submit" CssClass="btn btn-success"
                                                    OnClick="btnConfirm_Click" OnClientClick="return ValidatePage();" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                                    OnClick="btnCancel_Click" />
                                                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-success"
                                                    OnClick="btnApprove_click" Visible="false" />
                                                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger"
                                                    OnClick="btnReject_Click" Visible="false" />
                                                <asp:HiddenField ID="hdnFlag" runat="server" Value="0"></asp:HiddenField>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
