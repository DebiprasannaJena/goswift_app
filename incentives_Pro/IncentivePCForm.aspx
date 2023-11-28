<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncentivePCForm.aspx.cs"
    Inherits="incentives_IncentivePCForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
        });                                          
    </script>
    <script type="text/javascript" src="../js/WebValidation.js">
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="registration-div investors-bg">
        <div id="exTab1" class="container">
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
                <div class="form-sec">
                    <div class="innertabs">
                        <ul class="nav nav-pills pull-right">
                            <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                            <li class="active"><a href="appliedindustrylist.aspx">Apply For incentive</a></li>
                            <li><a href="javscript:void(0);">View Application Status</a></li>
                        </ul>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="form-header">
                        <div class="iconsdiv">
                            <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                <i class="fa fa-print"></i></a><a href="javascript:void(0);" title="Export to Excel"
                                    id="A1" class="pull-right printbtn"><i class="fa fa-file-excel-o"></i></a>
                        </div>
                        <h2>
                            Application for Production Certificate for Micro, Small & Medium Entrepreneurs</h2>
                    </div>
                    <div class="form-body">
                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingOne">
                                    <h4 class="panel-title">
                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                            aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa  fa-minus">
                                            </i><span class="text-red pull-right " style="margin-right: 20px;">* All fields in this
                                                section are mandatory</span>Industrial Unit's Details </a>
                                    </h4>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Application For</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="drpApplicationType" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Change in</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:CheckBoxList ID="chkLstChange" runat="server" RepeatColumns="5" RepeatLayout="Table"
                                                        RepeatDirection="Horizontal">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Application No.</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtApplicationNo" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    EIN / EM-II/ PMT No.</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtEin" runat="server" MaxLength="9" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    UAN</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtUan" runat="server" MaxLength="12" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Name of Enterprise/Industrial Unit</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><asp:TextBox ID="txtEnterpriseName" Text="JRD Farma &amp; Research"
                                                        ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Unit Category</label>
                                                <div class="col-sm-6 margin-bottom10">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="drpUnitCategory" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Company Type</label>
                                                <div class="col-sm-6 margin-bottom10">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="drpCompanyType" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Organization Type</label>
                                                <div class="col-sm-6 margin-bottom10">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="drpOrganizationType" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Name of Owner</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><asp:TextBox ID="txtOwnerName" MaxLength="100" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Ownership Code</label>
                                                <div class="col-sm-6 margin-bottom10">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtOwnerCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingTwo">
                                    <h4 class="panel-title">
                                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                            href="#AddressDetails" aria-expanded="false" aria-controls="collapseTwo"><i class="more-less fa  fa-plus">
                                            </i>Address Details</a>
                                    </h4>
                                </div>
                                <div id="AddressDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Address of Enterprise</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtEnterpriseAddress" CssClass="form-control" TextMode="MultiLine"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Telephone No.</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><asp:TextBox ID="txtPhoneNo" MaxLength="10" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    FAX</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><asp:TextBox ID="txtFax" MaxLength="10" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    E-mail</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><asp:TextBox ID="txtEmail" MaxLength="100" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Website</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><asp:TextBox ID="txtWebsite" MaxLength="100" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Location of the unit</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><asp:TextBox ID="txtLocationOfUnit" MaxLength="100" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Registered Office / Communication Address</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <input type="checkbox" />Same as Address of Enterprise</label>
                                                    <asp:TextBox ID="txtOfficeAddress" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Telephone No.</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><asp:TextBox ID="txtOfficePhone" MaxLength="10" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    FAX</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><asp:TextBox ID="txtOfficeFax" MaxLength="10" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    E-mail</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><asp:TextBox ID="txtOfficeEmail" MaxLength="100" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Website</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><asp:TextBox ID="txtOfficeWebsite" MaxLength="100" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
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
                                        <p class="text-red text-right">
                                            All Amouts to be Entered in INR(Exact Amount)</p>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Date of First Fixed Capital Investment <small>(for land/Building/plant and machinery
                                                        & Balancing Equipment)</small></label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <div class="input-group  date datePicker" id="Div10">
                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="txtDateFFI" class="form-control"
                                                            runat="server">
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    First Fixed Capital Investment Done in
                                                </label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:CheckBoxList ID="chkInvestIn" runat="server" RepeatColumns="5" RepeatLayout="Table"
                                                        RepeatDirection="Horizontal">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Mode of Investment
                                                </label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="txtModeOfInvestment" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <h4 class="h4-header">
                                            Project cost</h4>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Fixed capital
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtFixedCapital" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Working capital
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtWorkingCapital" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <h4 class="h4-header">
                                            Finance</h4>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Self
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtSelfFinance" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Borrowed
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtBorrowed" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
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
                                            href="#EmployementInformation" aria-expanded="false" aria-controls="collapseTwo">
                                            <i class="more-less fa  fa-plus"></i>Production & Employment Details</a>
                                    </h4>
                                </div>
                                <div id="EmployementInformation" class="panel-collapse collapse" role="tabpanel"
                                    aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <p class="text-red text-right">
                                            All Amouts to be Entered in INR(Exact Amount)</p>
                                        <h4 class="h4-header">
                                            Employment Details</h4>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>
                                                                Type
                                                            </th>
                                                            <th>
                                                                General
                                                            </th>
                                                            <th>
                                                                SC
                                                            </th>
                                                            <th>
                                                                ST
                                                            </th>
                                                            <th>
                                                                Local
                                                            </th>
                                                            <th>
                                                                Local Out of Total
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Technical (Supervisory + Skilled)
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Gen_Technical" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_SC_Technical" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_ST_Technical" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Local_Technical" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_LocalGen_Technical" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Non-Technical (Admin. + Unskilled)
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Gen_NonNonTechnical" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_SC_NonTechnical" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_ST_NonTechnical" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Local_NonTechnical" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_LocalGen_NonTechnical" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                TOTAL
                                                            </td>
                                                            <td class=" text-right">
                                                                -
                                                            </td>
                                                            <td class="text-right">
                                                                -
                                                            </td>
                                                            <td class="text-right">
                                                                -
                                                            </td>
                                                            <td class="text-right">
                                                                -
                                                            </td>
                                                            <td class="text-right">
                                                                -
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <h4 class="h4-header">
                                            Out of Above</h4>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Women
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtWomen" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Physically challenged person
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtPhysicallyChallenged" runat="server" CssClass="form-control"
                                                        MaxLength="4"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <h4 class="h4-header">
                                            Main Category of product</h4>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Code(Code may be entered as per ASICC / NIC 2004 / NIC 2008)
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtProductCode" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Name
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Power Requirement
                                                </label>
                                                <div class="col-sm-6 margin-bottom10">
                                                    <span class="colon">:</span>
                                                    <asp:RadioButtonList ID="rdBtnLstPower" runat="server" RepeatColumns="2" RepeatLayout="Table"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Contract Demand (KW)
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtContractDemand" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <h4 class="h4-header">
                                            Item of Production / Service with Capacity</h4>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12  margin-bottom10">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>
                                                                Sl#
                                                            </th>
                                                            <th>
                                                                Item of product
                                                            </th>
                                                            <th>
                                                                Item Code
                                                            </th>
                                                            <th>
                                                                Quantity
                                                            </th>
                                                            <th>
                                                                Unit
                                                            </th>
                                                            <th>
                                                                Value (Rs. in lakh)
                                                            </th>
                                                            <th>
                                                                Action
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                1
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox16" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox18" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="drpUnitType" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox23" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton34" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-3 ">
                                                    Date of commencement of Production</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <div class="input-group  date datePicker" id="Div2">
                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="txtProdComm" class="form-control"
                                                            runat="server">
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-footer">
                                <div class="row">
                                    <div class="col-sm-12 text-right">
                                        <div id="myModal2" class="modal fade" role="dialog">
                                            <div class="modal-dialog modal-sm">
                                                <!-- Modal content-->
                                                <div class="modal-content">
                                                    <div class="modal-body text-center">
                                                        <div class="form-group">
                                                            <h4 class="text-success">
                                                                You have already availed incentive against this patient/ IPR Registration No.-123455</h4>
                                                            <br />
                                                            <a class="btn btn-success" href="PatentRegistration.aspx">Close</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" CssClass="btn btn-warning"
                                            OnClientClick="return ValidatePage()" OnClick="btnSaveAsDraft_Click" CommandArgument="d" />
                                        <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-success"
                                            OnClientClick="return ValidatePage()" OnClick="btnSaveAsDraft_Click" CommandArgument="s" />
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
        $(function () {
            $('.datePicker').datepicker({ dateFormat: 'dd:mm:yyyy', separator: ' @ ', minDate:
    new Date(), autoclose: true
            });
        });
    </script>
    </form>
</body>
</html>
