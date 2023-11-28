<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncentiveDetailsLarge.aspx.cs"
    MasterPageFile="~/MasterPage/Application.master" Inherits="Portal_Incentive_IncentiveDetailsLarge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/WebValidation.js">
    </script>
    <script src="../js/jQuery.alert.js" type="text/javascript"></script>
    <script src="../../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/Js_IncentiveDetails.js"></script>
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
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
                    <li><a>View Incentive</a></li></ul>
            </div>
        </section>
        <asp:Panel ID="pnlMain" runat="server">
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <fieldset>
                            <legend>Industrial Unit's Details</legend>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Industry Code
                                    </label>
                                    <div class="col-sm-4">
                                        <asp:Label ID="lblIndustryCode" runat="server"></asp:Label>
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
                                        <asp:UpdatePanel ID="upAppFor" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rdBtnApplicationFor" runat="server" AutoPostBack="true"
                                                    OnSelectedIndexChanged="rdBtnApplicationFor_SelectedIndexChanged" RepeatColumns="2"
                                                    RepeatDirection="Horizontal" RepeatLayout="Table">
                                                    <asp:ListItem Text="New" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Existing" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:DropDownList ID="drpApplicationType" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <label class="col-sm-2">
                                        Application No
                                    </label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="divChangeIn" runat="server" visible="false">
                                <div class="row">
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
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        EIN / EM-II/ PMT No
                                    </label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtEin" runat="server" MaxLength="9" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-2">
                                        Udyog Aadhaar Number
                                    </label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtUan" runat="server" MaxLength="9" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
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
                                        Unit Category</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="drpUnitCategory" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
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
                                                Sub Sector</label><div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="ddlSubSector" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="upOrg" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Constitution of Organization</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span> <span class="colon">:</span>
                                                <asp:DropDownList ID="drpOrganizationType" CssClass="form-control" runat="server"
                                                    OnSelectedIndexChanged="drpOrganizationType_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:TextBox ID="txtOtherOrg" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                            </div>
                                            <label class="col-sm-2">
                                                Company Type</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="drpCompanyType" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        <asp:Label ID="lblOwnerLabel" runat="server"></asp:Label>
                                    </label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="drpSalutation" runat="server" CssClass="form-control phcode"
                                            Style="width: 30%; margin-right: 10px; float: left;">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtOwnerName" MaxLength="100" CssClass="form-control" runat="server"
                                            Onkeypress="return inputLimiter(event,'NameCharacters')" Style="width: 65%; float: left;"></asp:TextBox>
                                        <div style="clear: both;">
                                        </div>
                                    </div>
                                    <label class="col-sm-2">
                                        Ownership Code</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="drpOwnerType" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Address Details</legend>
                            <asp:UpdatePanel ID="upDistrict" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                District</label><div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Text="--Select District--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            <label class="col-sm-2">
                                                Block</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-Select Block-" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Registered Office / Communication Address
                                    </label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtOfficeAddress" runat="server" TextMode="MultiLine" Rows="5" Columns="10"
                                            MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        <small>&nbsp;(Maximum&nbsp;
                                            <asp:Label ID="lblOfficeAddress" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                            &nbsp; characters allowed)</small>
                                    </div>
                                    <label class="col-sm-2">
                                        Telephone No
                                    </label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="ddlCode" TabIndex="7" runat="server" CssClass="form-control phcode"
                                            Style="width: 30%; margin-right: 10px; float: left;">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtOfficePhone" MaxLength="10" CssClass="form-control" runat="server"
                                            Onkeypress="return inputLimiter(event,'Numbers')" Style="width: 65%; float: left;"></asp:TextBox>
                                        <div style="clear: both;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        FAX</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="drpFx" TabIndex="9" runat="server" CssClass="form-control phcode"
                                            Style="width: 30%; margin-right: 10px; float: left;">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtOfficeFax" MaxLength="10" CssClass="form-control" runat="server"
                                            Onkeypress="return inputLimiter(event,'Numbers')" Style="width: 65%; float: left;"></asp:TextBox>
                                        <div style="clear: both;">
                                        </div>
                                    </div>
                                    <label class="col-sm-2">
                                        E-mail</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtOfficeEmail" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Website</label><div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtOfficeWebsite" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Address of Enterprise
                                    </label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtEnterpriseAddress" runat="server" TextMode="MultiLine" Rows="5"
                                            Columns="10" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        <small>&nbsp;(Maximum&nbsp;
                                            <asp:Label ID="lblRemark" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                            &nbsp; characters allowed)</small>
                                    </div>
                                    <label class="col-sm-2">
                                        Telephone No
                                    </label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="drpEntCode" TabIndex="7" runat="server" CssClass="form-control phcode"
                                            Style="width: 30%; margin-right: 10px; float: left;">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtPhoneNo" MaxLength="10" CssClass="form-control" runat="server"
                                            Onkeypress="return inputLimiter(event,'Numbers')" Style="width: 65%; float: left;"></asp:TextBox>
                                        <div style="clear: both;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        FAX</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="drpEnterpriseFax" TabIndex="9" runat="server" CssClass="form-control phcode"
                                            Style="width: 30%; margin-right: 10px; float: left;">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtFax" MaxLength="10" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                            Style="width: 65%; float: left;"></asp:TextBox>
                                        <div style="clear: both;">
                                        </div>
                                    </div>
                                    <label class="col-sm-2">
                                        E-mail</label><div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtEmail" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Website</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtWebsite" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Investment Details</legend>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Date of First Fixed Capital Investment <small>(for land/Building/plant and machinery
                                        & Balancing Equipment):</label>
                                    <div class="col-sm-4">
                                        <div class="input-group  date datePicker">
                                            <input name="txtTimescheduleforyearofcomm" type="text" id="txtDateFFI" class="form-control"
                                                runat="server" readonly="readonly">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <label class="col-sm-2">
                                        First Fixed Capital Investment Done in:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="drpChangeIn" AutoPostBack="true" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Mode of Investment:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtModeOfInvestment" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Decimal')"
                                            onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                            MaxLength="13"> </asp:TextBox></div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label for="Iname" class="col-sm-12 ">
                                        Total Capital Investment</label>
                                    <div class="col-sm-12">
                                        <table class="table table-bordered">
                                            <tr>
                                                <th>
                                                    Sl #
                                                </th>
                                                <th>
                                                    Investment Head
                                                </th>
                                                <th>
                                                    Interest Amount
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    1
                                                </td>
                                                <td>
                                                    Land including land development
                                                </td>
                                                <td class="text-right">
                                                    <asp:TextBox ID="txtland" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Decimal')"
                                                        onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                        MaxLength="13"> </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    2
                                                </td>
                                                <td>
                                                    Building & Civil Construction
                                                </td>
                                                <td class="text-right">
                                                    <asp:TextBox ID="txtBuilding" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Decimal')"
                                                        onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                        MaxLength="13"> </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    3
                                                </td>
                                                <td>
                                                    Plant & Machinery
                                                </td>
                                                <td class="text-right">
                                                    <asp:TextBox ID="txtPlantMachinery" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Decimal')"
                                                        onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                        MaxLength="13"> </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    4
                                                </td>
                                                <td>
                                                    Other Fixed Assests
                                                </td>
                                                <td class="text-right">
                                                    <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Decimal')"
                                                        onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                        MaxLength="13"> </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <strong>Total</strong>
                                                </td>
                                                <td class="text-left">
                                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Working capital:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtWorkingCapital" runat="server" CssClass="form-control" MaxLength="13"
                                            Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                            onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-2">
                                        Equity:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtEquity" runat="server" CssClass="form-control" MaxLength="13"
                                            Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                            onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Loan:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtLoan" runat="server" CssClass="form-control" MaxLength="13" Onkeypress="return inputLimiter(event,'Decimal')"
                                            onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-2">
                                        FDi Components:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtFdiComponent" runat="server" CssClass="form-control" MaxLength="13"
                                            Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                            onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <h4 class="h4-header">
                                Plant & Machinery
                            </h4>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12  margin-bottom10">
                                                <asp:GridView ID="gvPlant" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                                    CssClass="table table-bordered" DataKeyNames="id" Style="width: 100%;">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Plant & Machinery Name" DataField="MachineryName" />
                                                        <asp:BoundField HeaderText="Date of Purchase" DataField="DateofPurchase" />
                                                        <asp:BoundField HeaderText="Investment Amount" DataField="Cost" />
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%" Visible="false">
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
                                        <%-- <asp:PostBackTrigger ControlID="lnkMachinery" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Production & Employment Details</legend>
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
                                        Managerial</label><div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtManagarial" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    <label class="col-sm-2">
                                        General
                                    </label>
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
                                        Supervisor</label><div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtSupervisor" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    <label class="col-sm-2">
                                        SC
                                    </label>
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
                                        Skilled</label><div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtSkilled" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    <label class="col-sm-2">
                                        ST
                                    </label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtTotalSt" runat="server" CssClass="form-control" Text="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Semi Skilled</label><div class="col-sm-4">
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
                                        UnSkilled</label><div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtUnSKilled" MaxLength="4" CssClass="form-control" runat="server"
                                                Onkeypress="return inputLimiter(event,'Numbers')" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    <label class="col-sm-2">
                                        Women</label><div class="col-sm-4">
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
                                        Differently abled person</label><div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtPhd" MaxLength="4" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                </div>
                            </div>
                            <h4 class="h4-header">
                                Main Category of product</h4>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Code(Code may be entered as per ASICC / NIC 2004 / NIC 2008)
                                    </label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtProductCode" runat="server" CssClass="form-control" MaxLength="4"
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
                                <div class="row">
                                    <label class="col-sm-2">
                                        Date of commencement of Production
                                    </label>
                                    <div class="col-sm-4">
                                        <div class="input-group  date datePicker" id="Div2">
                                            <input name="txtTimescheduleforyearofcomm" type="text" id="txtProdComm" class="form-control"
                                                runat="server" readonly="readonly">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h4 class="h4-header">
                                Item of Production / Service with Capacity</h4>
                            <div class="form-group">
                                <asp:UpdatePanel ID="up3" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12  margin-bottom10">
                                                <table class="table table-bordered" id="tblProducts" runat="server">
                                                    <tr>
                                                        <th>
                                                            Item Product
                                                        </th>
                                                        <th>
                                                            Code
                                                        </th>
                                                        <th colspan="2">
                                                            Quantity
                                                        </th>
                                                        <th>
                                                            Value
                                                        </th>
                                                        <th>
                                                            Action
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtItemProduct" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox><br />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drpUnitType" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                OnSelectedIndexChanged="drpUnitType_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtUnitType" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                                Visible="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCost" runat="server" CssClass="form-control" MaxLength="13" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm" runat="server" OnClick="lnkAdd_ClicK"
                                                                CommandName="add"><i class="fa fa-plus-square" onClientClick="return ValidateAdd();"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                                    CssClass="table table-bordered" OnRowCommand="grdProducts_RowCommand" DataKeyNames="id"
                                                    Style="width: 100%;" OnRowDataBound="grdProducts_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnUnit" runat="server" Value='<%#Eval("UnitId") %>' />
                                                                <asp:HiddenField ID="hdnUnitOthers" runat="server" Value='<%#Eval("Unitothers") %>' />
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Item Of Product" DataField="item" />
                                                        <asp:BoundField HeaderText="Code" DataField="Code" />
                                                        <asp:BoundField HeaderText="Quantity" DataField="qty" />
                                                        <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                        <asp:BoundField HeaderText="Cost" DataField="Cost" />
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
                                </asp:UpdatePanel>
                            </div>
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Power Requirement
                                            </label>
                                            <div class="col-sm-4">
                                                <asp:RadioButtonList ID="rdBtnLstPower" runat="server" RepeatColumns="2" RepeatLayout="Table"
                                                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rdBtnLstPower_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div id="divPower" runat="server">
                                                <label class="col-sm-2">
                                                    Contract Demand (KW)
                                                </label>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtContractDemand" runat="server" CssClass="form-control" MaxLength="6"
                                                        Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                                </div>
                                                <label class="col-sm-2">
                                                    Date of Power Connection
                                                </label>
                                                <div class="col-sm-4">
                                                    <div class="input-group  date datePicker" id="Div4">
                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="txtPowerConnection" class="form-control"
                                                            runat="server">
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </div>
                </div>
            </section>
        </asp:Panel>
        <section class="content">
            <div class="form-group">
                <div class="row">
                    <div class="col-sm-12  margin-bottom10">
                        <h4 class="h4-header">
                            Documents submitted by the investor
                        </h4>
                        <asp:GridView ID="grdFiles" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                            DataKeyNames="vchDocId" OnRowDataBound="grdFiles_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="sl#">
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
        </section>
    </div>
</asp:Content>
