<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IRForm_Large.aspx.cs" MasterPageFile="~/Portal/MasterPage/Application.master" Inherits="Portal_IRForm_Large" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../js/WebValidation.js"></script>
    <script src="../../js/decimalrstr.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/IrForm.js"></script>
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
                    <li><a>View Incentive</a></li></ul>
            </div>
        </section>
        <section class="content">
            <asp:Panel ID="pnlmain" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <fieldset>
                            <legend>Inspection Report</legend>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Date of Inspection :</label>
                                        <div class="input-group  date datePicker">
                                            <input name="txtTimescheduleforyearofcomm" type="text" id="txtDateOfInspection" class="form-control"
                                                runat="server" readonly="readonly">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12  margin-bottom10">
                                        <asp:CheckBox ID="chkverification" runat="server" Text="I / We have physically verified the implementation status of the project (Original/ E/ M / D) / Operational status of the Project and found it in under implementation
                                    / operation." OnCheckedChanged="chkVerification_CheckChanged" AutoPostBack="true" />
                                        <asp:GridView ID="grdOffice" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                            CssClass="table table-bordered" DataKeyNames="id" OnRowCommand="grdCommon_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name of the Officer">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOfficerName" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            Text='<%#Eval("strOfficer") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            Text='<%#Eval("strDesignation") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Organization">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOrganization" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            Text='<%#Eval("strAuthority") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aadhaar No.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAadhaarNo" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            Text='<%#Eval("AadhaarNo") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="add"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Industrial Unit's Details </legend>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Application For :</label>
                                        <asp:DropDownList ID="drpApplicationType" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Application No :</label>
                                        <asp:Label ID="lblApplicationNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Unit Category:</label>
                                        <asp:DropDownList ID="drpUnitCategory" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Change in:</label>
                                        <asp:CheckBoxList ID="chkLstChange" runat="server" RepeatColumns="5" RepeatLayout="Table"
                                            RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Company Type:</label>
                                        <asp:DropDownList ID="drpCompanyType" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Name of Enterprise/Industrial Unit</label>
                                        <asp:TextBox ID="txtEnterpriseName" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Sector of Activity</label>
                                        <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Sub Sector</label>
                                        <asp:DropDownList ID="ddlSubSector" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            EIN / EM-II/ PMT No :</label>
                                        <asp:TextBox ID="txtEin" runat="server" MaxLength="9" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Organization Type</label>
                                        <asp:DropDownList ID="drpOrganizationType" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Name of Owner :</label>
                                        <asp:TextBox ID="txtOwnerName" MaxLength="100" CssClass="form-control" runat="server"
                                            Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Ownership Code:</label>
                                        <asp:DropDownList ID="drpOwnerType" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Address Details</legend>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            District:</label>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="--Select District--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Block:</label>
                                        <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="-Select Block-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Address of Enterprise :</label>
                                        <asp:TextBox ID="txtEnterpriseAddress" runat="server" TextMode="MultiLine" Rows="5"
                                            Columns="10" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        &nbsp;(Maximum&nbsp;
                                        <asp:Label ID="lblRemark" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                        &nbsp; characters allowed)
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Telephone No :</label>
                                        <asp:TextBox ID="txtPhoneNo" MaxLength="10" CssClass="form-control" runat="server"
                                            Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            FAX:</label>
                                        <asp:TextBox ID="txtFax" MaxLength="10" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            E-mail:</label>
                                        <asp:TextBox ID="txtEmail" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Website:</label>
                                        <asp:TextBox ID="txtWebsite" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Registered Office / Communication Address :</label>
                                        <asp:TextBox ID="txtOfficeAddress" runat="server" TextMode="MultiLine" Rows="5" Columns="10"
                                            MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        &nbsp;(Maximum&nbsp;
                                        <asp:Label ID="lblOfficeAddress" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                        &nbsp; characters allowed)
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Telephone No :</label>
                                        <asp:TextBox ID="txtOfficePhone" MaxLength="10" CssClass="form-control" runat="server"
                                            Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            FAX:</label>
                                        <asp:TextBox ID="txtOfficeFax" MaxLength="10" CssClass="form-control" runat="server"
                                            Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            E-mail:</label>
                                        <asp:TextBox ID="txtOfficeEmail" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Website:</label>
                                        <asp:TextBox ID="txtOfficeWebsite" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Product details</legend>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label>
                                            Whether the unit is (as on date of inspection) :</label>
                                        <asp:RadioButtonList ID="rdBtnUntiCond" runat="server" AutoPostBack="true" RepeatColumns="2"
                                            RepeatDirection="Vertical" RepeatLayout="Table" OnSelectedIndexChanged="rdBtnUntiCond_SelectedIndexChanged">
                                            <asp:ListItem Text="Under Implementation" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Commenced Production" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row" id="divProdStartDate" runat="server" visible="false">
                                    <div class="col-sm-6">
                                        <label>
                                            Date of starting production</label>
                                        <div class="input-group  date datePicker">
                                            <input name="txtTimescheduleforyearofcomm" type="text" id="Text1" class="form-control"
                                                runat="server" readonly="readonly">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="divUnitCond" runat="server" visible="false">
                                    <div class="col-sm-12  margin-bottom10">
                                        <h4 class="h4-header">
                                            Details of Product / Service with capacity -
                                            <asp:Label ID="lblUnitCOnd" runat="server"></asp:Label></h4>
                                        <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                            CssClass="table table-bordered" DataKeyNames="id" OnRowCommand="grdCommon_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item of product">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtItemProduct" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            Text='<%#Eval("item") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            Text='<%#Eval("Code") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            Text='<%#Eval("Qty") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            Text='<%#Eval("unit") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Value (Rs. in lakh)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCost" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Decimal')"
                                                            Text='<%#Eval("Cost") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="add" OnClientClick="UpdateActiveAccordion();"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Control measures adopted in the unit -
                                        </label>
                                        <asp:Label ID="lblProductionControl" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtPollutionControl" runat="server" TextMode="MultiLine" Rows="5"
                                            Columns="10" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        &nbsp;(Maximum&nbsp;
                                        <asp:Label ID="lblOriginal" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                        &nbsp; characters allowed)
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Industrial Safety / Electrical Safety / Fire Safety adopted in the unit
                                        </label>
                                        <asp:TextBox ID="txtSafety" runat="server" TextMode="MultiLine" Rows="5" Columns="10"
                                            MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        &nbsp;(Maximum&nbsp;
                                        <asp:Label ID="lblSafety" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                        &nbsp; characters allowed)
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Connected Power Load with unit for-
                                        </label>
                                        <asp:Label ID="lblPowerLoad" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtPowerLoad" runat="server" TextMode="MultiLine" Rows="5" Columns="10"
                                            MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        &nbsp;(Maximum&nbsp;
                                        <asp:Label ID="lblPowerRemarks" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                        &nbsp; characters allowed)
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Details of CPP / D.G set
                                        </label>
                                        <asp:TextBox ID="txtCpp" runat="server" TextMode="MultiLine" Rows="5" Columns="10"
                                            MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        &nbsp;(Maximum&nbsp;
                                        <asp:Label ID="lblCpp" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                        &nbsp; characters allowed)
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Employement Details</legend>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12  margin-bottom10">
                                        <h4 class="h4-header">
                                            <asp:Label ID="lblEmployement" runat="server"></asp:Label></h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Managerial:</label>
                                        <asp:TextBox ID="txtManagarial" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Supervisor:</label>
                                        <asp:TextBox ID="txtSupervisor" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Skilled:</label>
                                        <asp:TextBox ID="txtSkilled" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Semi Skilled:</label>
                                        <asp:TextBox ID="txtSemiSkilled" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Un Skilled:</label>
                                        <asp:TextBox ID="txtUnSKilled" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Total:</label>
                                        <asp:TextBox ID="TextBox1" ReadOnly="true" runat="server" CssClass="form-control"
                                            Text="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Total of SC:</label>
                                        <asp:TextBox ID="txtTotalSc" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Total of ST:</label>
                                        <asp:TextBox ID="txtTotalSt" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                            MaxLength="4" Text="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Total Women:</label>
                                        <asp:TextBox ID="txtWomen" MaxLength="4" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Total Physically Handicapped:</label>
                                        <asp:TextBox ID="txtPhd" MaxLength="4" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>
                                            Total
                                        </label>
                                        <asp:TextBox ID="txtGrandTotal" MaxLength="6" CssClass="form-control" runat="server"
                                            ReadOnly="true" ClientIDMode="Static"> </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Investment Details </legend>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12  margin-bottom10">
                                        <h4 class="h4-header">
                                            Capital Investment (Rs.) for
                                            <asp:Label ID="lblCapitalInvest" runat="server"></asp:Label></h4>
                                        <asp:GridView ID="grdCapitalInvestment" runat="server" AutoGenerateColumns="false"
                                            EmptyDataText="No records found..." CssClass="table table-bordered" DataKeyNames="slno"
                                            ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
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
                                                <asp:TemplateField HeaderText="Actual expenditure incurred)">
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
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>
                                            Date of First Fixed Capital Investment <small>(for land/Building/plant and machinery
                                            & Balancing Equipment):</label>
                                        <div class="input-group  date datePicker">
                                            <input name="txtTimescheduleforyearofcomm" type="text" id="txtDateFFI" class="form-control"
                                                runat="server" readonly="readonly">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Term Loan Details</h4>
                                        <asp:GridView ID="grdTermLoan" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                            CssClass="table table-bordered" OnRowCreated="grdTermLoan_RowCreated" OnRowDataBound="grdTermLoan_RowDataBound"
                                            OnRowCommand="grdCommon_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name of Financial Institution">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTlInstitue" CssClass="form-control" runat="server" Text='<%#Eval("institute") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtState" CssClass="form-control" runat="server" Text='<%#Eval("state") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="City">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCity" CssClass="form-control" runat="server" Text='<%#Eval("city") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Term Loan Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" MaxLength="13"
                                                            Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                            onblur="isNumberBlur(event, this, 2);" Text='<%#Eval("Amt") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sanction Date">
                                                    <ItemTemplate>
                                                        <div class="input-group  date datePicker">
                                                            <asp:TextBox ID="txtSanctionDate" class="form-control" runat="server" ReadOnly="true"
                                                                value='<%#Eval("SanctionDate") %>' />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Availed Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAvailedAmount" runat="server" CssClass="form-control" MaxLength="13"
                                                            Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                            onblur="isNumberBlur(event, this, 2);" Text='<%#Eval("AvailedAmt") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Availed Date">
                                                    <ItemTemplate>
                                                        <div class="input-group  date datePicker">
                                                            <asp:TextBox ID="txtAvailedDate" class="form-control" runat="server" ReadOnly="true"
                                                                value='<%#Eval("AvailedDate") %>' />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="add" OnClientClick="UpdateActiveAccordion();"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Working Capital Details</h4>
                                        <asp:GridView ID="grdWorkingCapital" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                            CssClass="table table-bordered" OnRowCreated="grdTermLoan_RowCreated" OnRowDataBound="grdTermLoan_RowDataBound"
                                            OnRowCommand="grdCommon_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name of Financial Institution">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTlInstitue" CssClass="form-control" runat="server" Text='<%#Eval("institute") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtState" CssClass="form-control" runat="server" Text='<%#Eval("state") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="City">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCity" CssClass="form-control" runat="server" Text='<%#Eval("city") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Term Loan Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" MaxLength="13"
                                                            Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                            onblur="isNumberBlur(event, this, 2);" Text='<%#Eval("Amt") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sanction Date">
                                                    <ItemTemplate>
                                                        <div class="input-group  date datePicker">
                                                            <asp:TextBox ID="txtSanctionDate" class="form-control" runat="server" ReadOnly="true"
                                                                value='<%#Eval("SanctionDate") %>' />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Availed Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAvailedAmount" runat="server" CssClass="form-control" MaxLength="13"
                                                            Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                            onblur="isNumberBlur(event, this, 2);" Text='<%#Eval("AvailedAmt") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Availed Date">
                                                    <ItemTemplate>
                                                        <div class="input-group  date datePicker">
                                                            <asp:TextBox ID="txtAvailedDate" class="form-control" runat="server" ReadOnly="true"
                                                                value='<%#Eval("AvailedDate") %>' />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="add" OnClientClick="UpdateActiveAccordion();"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Whether the site / structure plan has been approved for
                                            <asp:Label ID="lblApproval" runat="server"></asp:Label></h4>
                                        <asp:GridView ID="grdApproval" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                            CssClass="table table-bordered" OnRowCommand="grdCommon_RowCommand" OnRowDataBound="grdApproval_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name of site / structure plan">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNameOfSite" CssClass="form-control" runat="server" Text='<%#Eval("name") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approval Authority">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drpApprovalAuthority" runat="server" CssClass="form-control"
                                                            OnSelectedIndexChanged="drpApprovalAuthority_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Directorate of Factory & Boiler" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Development authority " Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="any regulatory body " Value="3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnAuthority" runat="server" Value='<%#Eval("authority") %>' />
                                                        <asp:TextBox ID="txtOthers" CssClass="form-control" runat="server" Visible="false"
                                                            Text='<%#Eval("others") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Supporting Documents">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="add" OnClientClick="UpdateActiveAccordion();"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            List of valid Statutory Clearances
                                            <asp:Label ID="lblStatutaryCLearence" runat="server"></asp:Label></h4>
                                        <asp:GridView ID="grdStatutoryClearence" runat="server" AutoGenerateColumns="false"
                                            EmptyDataText="No Records Found" CssClass="table table-bordered" OnRowCommand="grdCommon_RowCommand"
                                            OnRowDataBound="grdStatutoryClearence_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Statutory Clearance">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drpClearence" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="OSPCB" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Electrical authority" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Factory & Boiler" Value="4"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnClearence" runat="server" Value='<%#Eval("clearence") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Period of validity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPeriod" CssClass="form-control" runat="server" Text='<%#Eval("Period") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="add" OnClientClick="UpdateActiveAccordion();"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Whether same management is having other enterprises, if so, details there of
                                        </h4>
                                        <asp:GridView ID="gvLOCDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                            OnRowDataBound="gvLocDetails_rowdatabound" OnRowCommand="grdCommon_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit Name">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtUnit" MaxLength="50" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            Text='<%#Eval("unitname") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtProduct" MaxLength="50" CssClass="form-control" runat="server"
                                                            Onkeypress="return inputLimiter(event,'NameCharacters')" Text='<%#Eval("product") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Capacity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCapacity" CssClass="form-control" runat="server" MaxLength="30"
                                                            Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')" Text='<%#Eval("totalCapacity") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnState" runat="server" Value='<%#Eval("state") %>' />
                                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="District">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnDistrict" runat="server" Value='<%#Eval("district") %>' />
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="add" OnClientClick="UpdateActiveAccordion();"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            It is verified that the applicant unit has availed / applied for following incentives.
                                        </h4>
                                        <asp:GridView ID="grdIncentiveApplied" runat="server" AutoGenerateColumns="false"
                                            EmptyDataText="No Records Found" CssClass="table table-bordered" OnRowCommand="grdCommon_RowCommand"
                                            OnRowDataBound="grdIncentiveApplied_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Incentive Type">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnType" runat="server" Value='<%#Eval("type") %>' />
                                                        <asp:DropDownList ID="drpIncentiveType" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="-Select Type-" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Interest subsidy" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Stamp Duty Exemption" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Employment Cost Subsidy" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantam">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantam" runat="server" class="form-control" Text='<%#Eval("quantam") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From">
                                                    <ItemTemplate>
                                                        <div class="input-group  date datePicker" id="Div10">
                                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Text='<%#Eval("fromdate") %>'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To">
                                                    <ItemTemplate>
                                                        <div class="input-group  date datePicker" id="Div10">
                                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Text='<%#Eval("todate") %>'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IPR Applicability">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnIpr" runat="server" Value='<%#Eval("ipr") %>' />
                                                        <asp:DropDownList ID="drpIpr" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="-Select IPR Applicability-" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="IPR 2007" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="IPR 2015" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="add" OnClientClick="UpdateActiveAccordion();"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" Visible="false"
                                                            CommandArgument="<%#Container.DataItemIndex%>" CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="vchName" HeaderText="Problems" />
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtProblems" runat="server" TextMode="MultiLine" Rows="2" Columns="10"
                                                            MaxLength="200" CssClass="form-control" Text='<%#Eval("remarks") %>'></asp:TextBox>
                                                        &nbsp;(Maximum&nbsp;
                                                        <asp:Label ID="lblProblems" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                                        &nbsp; characters allowed)
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <h4 class="h4-header">
                                            Remark(s) -
                                        </h4>
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="10"
                                            MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        &nbsp;(Maximum&nbsp;
                                        <asp:Label ID="lblEndRemarks" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                        &nbsp; characters allowed)
                                    </div>
                                    <div class="col-sm-4">
                                        <h4 class="h4-header">
                                            Suggestions/Views
                                        </h4>
                                        <asp:TextBox ID="txtSuggestions" runat="server" TextMode="MultiLine" Rows="5" Columns="10"
                                            MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        &nbsp;(Maximum&nbsp;
                                        <asp:Label ID="lblSuggestion" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                        &nbsp; characters allowed)
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Button ID="btnConfirm" runat="server" Text="Submit" CssClass="btn btn-success"
                                        OnClick="btnConfirm_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                        OnClick="btnCancel_Click" />
                                </div>
                            </div>
                            </small>
                        </fieldset>
                    </div>
                </div>
            </asp:Panel>
        </section>
    </div>
</asp:Content>