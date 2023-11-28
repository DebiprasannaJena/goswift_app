<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCFormInitial.aspx.cs" Inherits="incentives_PCFormInitial" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0 minimum-scale=1.0, user-scalable=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Incentive Calculator</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet">
    <script src="../js/jquery-2.1.1.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/angular.min.js"></script>
    <script src="../js/controller/incentive-calculator.js"></script>
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
        });
    
    </script>
    <style>
        .valueText
        {
            visibility: hidden;
            position: absolute;
        }
        .listGap li, .assumptionList li
        {
            margin-bottom: 10px !important;
        }
        .assumptionList li
        {
            font-size: 12px !important;
        }
        .heading
        {
            margin-top: 10px;
            padding-bottom: 8px;
            font-size: 18px;
            color: #506bbb;
            border-bottom: 1px solid #e0e0e0;
        }
        .mainHdng
        {
            color: #cf0a11;
            font-size: 18px;
            padding: 10px 0px 10px;
            border-bottom: 1px solid #e0e0e0;
            margin: 0px 0px 10px;
        }
        .incentivesList .panel-heading
        {
            padding-left: 10px;
            font-size: 16px;
        }
        .incentivesList .panel-body
        {
            padding: 0px;
        }
        .incentivesList .panel-body .list-group
        {
            margin-bottom: 0px;
        }
        .incentivesList .panel-body label
        {
            border-radius: 0px;
            border: 0px;
        }
        .incentivesList .panel-body span
        {
            display: inline;
        }
        .incentivesList .panel-body .plicyTxt p
        {
            font-size: 14px;
            display: inline;
        }
        .valueText
        {
            visibility: hidden;
            position: absolute;
        }
        .listGap
        {
            margin: 0px;
            padding: 0px;
            padding-left: 18px;
        }
        .listGap li
        {
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
 <form id="form1" runat="server">   
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
                <div class="investrs-tab">
                    <ul class="nav nav-pills">
                        <li class="menudashboard"><a href="javascript:void(0)"><i class="fa fa-tachometer"></i>
                            Dashboard</a> </li>
                        <li class="menuprofile"><a href="../InvesterProfile.aspx"><i class="fa fa-user"></i>
                            Investor Profile</a> </li>
                        <li class="menuproposal"><a href="../Proposals.aspx"><i class="fa fa-briefcase"></i>
                            Proposals</a> </li>
                        <li class="menuservices"><a href="../DepartmentClearance.aspx"><i class="fa fa-wrench">
                        </i>Services</a> </li>
                        <%--  <li><a href="IncentiveCalculator.aspx">Incentive Calculator</a> </li>--%>
                        <li class="menuincentive"><a href="incentiveoffered.aspx"><i class="fa fa-money"></i>
                            Incentive</a> </li>
                    </ul>
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">
                            <div class="innertabs">
                                <ul class="nav nav-pills pull-right">
                                    <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                    <%if (Session["InvestorId"].ToString() == "49")
                                      { %>
                                    <li class="active"><a href="IncentiveCalc.aspx">Apply For incentive</a></li>
                                    <% }
                                      else
                                      { %>
                                    <li class="active"><a href="appliedindustrylist.aspx">Apply For incentive</a></li>
                                    <%}%>
                                    <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                                </ul>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-header">
                                <div class="iconsdiv">
                                    <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                        <i class="fa fa-print"></i></a><a href="javascript:void(0);" title="Export to Excel"
                                            id="A1" class="pull-right printbtn"><i class="fa fa-file-excel-o"></i></a>
                                    <a href="javascript:void(0);" title="Delete" id="A2" class="pull-right printbtn"><i
                                        class="fa fa-trash-o"></i></a>
                                </div>
                                <h2>
                                </h2>
                            </div>
                         
                                
                                 
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label>
                                        Application For :</label>
                                    <asp:RadioButtonList ID="rdBtnLstAppFor" runat="server" RepeatColumns="2" RepeatDirection="Vertical"
                                        RepeatLayout="Table">
                                    </asp:RadioButtonList>
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
                          <%--  <div class="row">
                                <div class="col-sm-4">
                                    <label>
                                        Name of Enterprise/Industrial Unit</label>
                                    <asp:TextBox ID="txtEnterpriseName" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                </div>
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
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <label>
                                        Name of Owner :</label>
                                    <asp:TextBox ID="txtOwnerName" MaxLength="100" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label>
                                        Ownership Code:</label>
                                    <asp:DropDownList ID="drpOwnerType" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
                        </div>
                    </fieldset>
                <%--    <fieldset>
                        <legend>Address Details</legend>
                        <div class="form-group">
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
                                <div class="col-sm-4">
                                    <label>
                                        Location of the unit:</label>
                                    <asp:TextBox ID="txtLocationOfUnit" MaxLength="100" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
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
                                        MaxLength="4" Text="0"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label>
                                        Supervisor:</label>
                                    <asp:TextBox ID="txtSupervisor" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                        MaxLength="4" Text="0"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label>
                                        Skilled:</label>
                                    <asp:TextBox ID="txtSkilled" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                        MaxLength="4" Text="0"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <label>
                                        Semi Skilled:</label>
                                    <asp:TextBox ID="txtSemiSkilled" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                        MaxLength="4" Text="0"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label>
                                        Un Skilled:</label>
                                    <asp:TextBox ID="txtUnSKilled" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                        MaxLength="4" Text="0"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label>
                                        Total:</label>
                                    <asp:TextBox ID="TextBox1" ReadOnly="true" runat="server" CssClass="form-control"
                                        Text="0"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <label>
                                        Total of SC:</label>
                                    <asp:TextBox ID="txtTotalSc" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                        MaxLength="4" Text="0"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label>
                                        Total of ST:</label>
                                    <asp:TextBox ID="txtTotalSt" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                        MaxLength="4" Text="0"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label>
                                        Total Women:</label>
                                    <asp:TextBox ID="txtWomen" MaxLength="4" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <label>
                                        Total Physically Handicapped:</label>
                                    <asp:TextBox ID="txtPhd" MaxLength="4" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <label>
                                        Total
                                    </label>
                                    <asp:TextBox ID="txtGrandTotal" MaxLength="6" CssClass="form-control" runat="server"
                                        ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </fieldset>--%>
                                </div>
                            </div>                            
                        </div>
                        <div align="center">
                           <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-warning"
                                        OnClick="btnSave_Click" CommandArgument="d" />
                        </div>
                            <div class="col-sm-5">
                                <h3 class="heading">
                                    Production certificate</h3>
                                <div class="well">
                                    <ol class="assumptionList">
                                        <li><strong>
                                            <asp:HyperLink ID="hypApply" runat="server">Apply With PC</asp:HyperLink></strong>
                                        </li>
                                        <li><strong>
                                            <asp:HyperLink ID="hypPC" runat="server">Apply Without PC</asp:HyperLink></strong>
                                        </li>
                                        <li><strong>
                                            <asp:HyperLink ID="HyperLink1" runat="server">Download PC Certificate</asp:HyperLink></strong>
                                        </li>
                                    </ol>
                                </div>
                            </div>
                            <div class="form-footer">
                                <div class="row">
                                    <div class="col-sm-12 text-right">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
         
        <uc3:footer ID="footer" runat="server" />
        </form>
</body>
</html>
