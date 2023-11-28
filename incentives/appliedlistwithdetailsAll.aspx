<%@ Page Language="C#" AutoEventWireup="true" CodeFile="appliedlistwithdetailsAll.aspx.cs" Inherits="incentives_appliedlistwithdetailsAll" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css" />
    <style>
        @media (min-width: 992px)
        {
            .provisionmodal
            {
                width: 672px;
                margin: 30px auto;
            }
        }
    </style>
    <script type="text/javascript" language="javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
            $('#checkfalse').click(function () {
                alert("hii");
            });
            $('#checktrue').click(function () {
                alert("hii");
            })

            $('.enableaplybtn').click(function () {
                $('a').removeClass('inactivelink');
            });

            $('.underIPR').click(function () {
                $('a').removeClass('inactivelink');
            });
            $('.underTourism').click(function () {
                $('a').removeClass('inactivelink');
            });

            $('[data-toggle="popover"]').popover();
            $('body').on('click', function (e) {
                $('[data-toggle="popover"]').each(function () {
                    if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                        $(this).popover('hide');
                    }
                });
            });

        });
    </script>
    <script type="text/javascript" language="javascript">
        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {

                    location.href = 'incentiveoffered.aspx?';
                    return true;
                }
                else {
                    return false;
                }
            });
        }
    </script>
    <style>
        .investordashboard-sec h4
        {
            background-color: transparent;
            padding: 10px 12px 5px 0px;
            font-weight: normal;
            border-bottom: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <uc2:header ID="header" runat="server" />
                <div class="registration-div investors-bg investboxconain applieddtl">
                    <div id="exTab1" class="">
                        <div class="investrs-tab">
                            <uc4:investoemenu ID="ineste" runat="server" />
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="form-sec">
                                    <div class="innertabs">
                                        <ul class="nav nav-pills pull-right">
                                            <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                            <li class="active"><a href="appliedlistwithdetails.aspx">Apply For incentive</a></li>
                                            <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                                        </ul>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-header m-b-10">
                                        <%-- <div class="iconsdiv pull-right">
                                    <a href="javascript:void(0);" title="Print" id="printbtn" data-toggle="tooltip" data-placement="top"
                                        class="pull-right">
                                        <img src="../images/printer.png" width="32" alt="Print" /></a> <a href="javascript:void(0);"
                                            title="Export to Excel" id="A1" data-toggle="tooltip" data-placement="top" class="pull-right m-r-15">
                                            <img src="../images/excel.png" width="32" alt="Export to Excel" /></a> <a href="javascript:void(0);"
                                                title="Delete" id="A2" data-toggle="tooltip" data-placement="top" class="pull-right m-r-15">
                                                <img src="../images/delet.png" width="26" alt="Delete" /></a>
                                </div>--%>
                                        <h2>
                                            Applicable Incentives
                                        </h2>
                                    </div>
                                    <div class="leftcolm">
                                        <h1>
                                            Summary
                                            <hr>
                                            <h1>
                                            </h1>
                                            <div class="investordashboard-sec">
                                                <div class="search-panel">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-2">
                                                            Industry Code</label>
                                                            <div class="col-sm-3">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="Lbl_Industry_Code" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                            <label class="col-sm-2">
                                                            Unit/Enterprise Name</label>
                                                            <div class="col-sm-4">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="Lbl_Comp_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 p-r-35">
                                                        <div class="criteriadiv">
                                                            <h4>
                                                                Criteria Provided</h4>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-6">
                                                                    Sector
                                                                    </label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="Lbl_Sector_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-6">
                                                                    Date of FFCI
                                                                    </label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="Lbl_FFCI_Date" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div ID="Div_Prod_Comm" runat="server" class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-6">
                                                                    Date of Production Commencement
                                                                    </label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="Lbl_Prod_Comm_Date" runat="server" 
                                                                            CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-6">
                                                                    Total Capital Investment</label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="Lbl_Total_Capital_Invest" runat="server" 
                                                                            CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-6">
                                                                    Investment In Plant Machinery</label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="Lbl_PM_Invest" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--<div class="rightarrow-gray">
                                            </div>--%>
                                                        <div class="rightarrow-wrapper rightarrow-sm">
                                                            <div class="rightarrow-stem">
                                                            </div>
                                                            <div class="rightarrow-head">
                                                            </div>
                                                        </div>
                                                        <div class="bottomarrow-wrapper bottomarrow-xs">
                                                            <div class="bottomarrow-stem">
                                                            </div>
                                                            <div class="bottomarrow-head">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 p-l-35">
                                                        <div class="criteriadiv">
                                                            <h4>
                                                                Derived Unit Details</h4>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-6">
                                                                    Unit Type
                                                                    </label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="Lbl_Unit_Type" runat="server" CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-6">
                                                                    District category
                                                                    </label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="Lbl_District_Category" runat="server" 
                                                                            CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div ID="Div_Empl_Rating" runat="server" class="form-group">
                                                                <div class="row">
                                                                    <label class="col-sm-6">
                                                                    Employment &amp; Investment Rating</label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:Label ID="Lbl_Empl_Invest_Rating" runat="server" 
                                                                            CssClass="form-control-static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<div class="leftbotarrow-gray">
                                            </div>
                                            <div class="clearfix">
                                            </div>--%>
                                                    <div class="col-sm-12">
                                                        <div class="bottomarrow-wrapper">
                                                            <div class="bottomarrow-stem">
                                                            </div>
                                                            <div class="bottomarrow-head">
                                                            </div>
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                        <div class="criteriadiv derivecon derivecon2 ">
                                                            <h4>
                                                                Policies from which you may apply for Incentives</h4>
                                                            <table class="table table-bordered m-t-0">
                                                                <tr>
                                                                    <th width="30%">
                                                                        Parent Policies
                                                                    </th>
                                                                    <th width="30%">
                                                                        Sectoral Policies
                                                                    </th>
                                                                    <th width="30%">
                                                                        Other Policies
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="Grd_Parent_Policy" runat="server" AutoGenerateColumns="false" 
                                                                            class="table table-bordered table-hover" GridLines="Horizontal" 
                                                                            ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Lbl_Parent_Policy_Name" runat="server" 
                                                                                            Text='<%# Eval("vchPlcName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                Not Applicable
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td>
                                                                        <asp:GridView ID="Grd_Sectoral_Policy" runat="server" 
                                                                            AutoGenerateColumns="false" class="table table-bordered table-hover" 
                                                                            GridLines="Horizontal" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Lbl_Sectoral_Policy_Name" runat="server" 
                                                                                            Text='<%# Eval("vchPlcName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                Not Applicable
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td>
                                                                        <asp:GridView ID="Grd_Other_policy" runat="server" AutoGenerateColumns="false" 
                                                                            class="table table-bordered table-hover" GridLines="Horizontal" 
                                                                            ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Lbl_Other_Policy_Name" runat="server" 
                                                                                            Text='<%# Eval("vchPlcName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                Not Applicable
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <%--<div class="col-sm-1">
                                            <div class="leftbotarrow-gray">
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>--%>
                                                </div>
                                                <%--<ul class="line-list">
                                        <li>
                                            <div class="hrzline">
                                                <div class="blank-bg">
                                                </div>--%>
                                                <%--</div>
                                        </li>
                                        <li>
                                            <div class="hrzline">--%>
                                                <%--</div>
                                        </li>
                                        <li>
                                            <div class="hrzline">--%>
                                                <%--</div>
                                        </li>
                                    </ul>--%>
                                                <div>
                                                    <%--<div class="bottomchinarowbox">
                                            <div class="bottomchainarrow">
                                                            </div>
                                        </div>--%>
                                                    <div class="bottomchinarowbox">
                                                    </div>
                                                </div>
                                                <h1>
                                                    <small class="pull-right" style="font-size: 80%"><b class="text-red">* TimeLine
                                                    </b>: Sanction is within 30 days. </small>List of Applicable Incentives
                                                    <hr>
                                                    <h1>
                                                    </h1>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="details-section">
                                                                <div class="table-responsive">
                                                                    <asp:GridView ID="Grd_Incentives_New" runat="server" 
                                                                        AutoGenerateColumns="false" class="table table-bordered table-hover" 
                                                                        EmptyDataText="You Are not Eligible for Any Incentive !!" 
                                                                        OnRowDataBound="Grd_Incentives_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Incentive">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Inct_Name" runat="server" Text='<%# Eval("vchInctName") %>'></asp:Label>
                                                                                    <asp:HiddenField ID="Hid_Inct_Id" runat="server" 
                                                                                        Value='<%# Eval("intInctId") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Provision">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="LnkBtn_Read_Provision" runat="server" 
                                                                                        OnClick="LnkBtn_Read_Provision_Click" 
                                                                                        ToolTip="Click Here to View Provision Details">View Details</asp:LinkButton>
                                                                                    <asp:HiddenField ID="Hid_Prov_File_Name" runat="server" 
                                                                                        Value='<%# Eval("vchProvFileName") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Operational Guidelines">
                                                                                <ItemTemplate>
                                                                                    <%-- Text='<%# Eval("vchPlcName") %>'--%>
                                                                                    <asp:HyperLink ID="Hlnk_Policy_Name" runat="server" Target="_blank" 
                                                                                        Text="View Details" ToolTip="Click Here to View OG Doc"></asp:HyperLink>
                                                                                    <asp:HiddenField ID="Hid_OG_Doc" runat="server" 
                                                                                        Value='<%# Eval("vchOGDoc") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="15%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText=" Disbursement Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Disbursement_Type" runat="server" 
                                                                                        Text='<%# Eval("vchDisburseType") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="13%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText=" Availment Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Avail_Type" runat="server" 
                                                                                        Text='<%# Eval("vchAvailType") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nature">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Inct_Nature" runat="server" 
                                                                                        Text='<%# Eval("vchInctNature") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Apply">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="LnkBtn_Apply" runat="server" OnClick="LnkBtn_Apply_Click" 
                                                                                        ToolTip="Click Here to Apply this Incentive !!">Apply</asp:LinkButton>
                                                                                    <asp:HiddenField ID="Hid_Form_Id" runat="server" 
                                                                                        Value='<%# Eval("nvchFormId") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="7%" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <h1>
                                                                        List of All Incentives Without Eligibility Check
                                                                        <hr />
                                                                        <h1>
                                                                        </h1>
                                                                        <asp:GridView ID="Grd_Incentives" runat="server" AutoGenerateColumns="false" 
                                                                            class="table table-bordered table-hover" 
                                                                            EmptyDataText="You Are not Eligible for Any Incentive !!" 
                                                                            OnRowDataBound="Grd_Incentives_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Incentive">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Lbl_Inct_Name" runat="server" Text='<%# Eval("strInctName") %>'></asp:Label>
                                                                                        <asp:HiddenField ID="Hid_Inct_Id" runat="server" 
                                                                                            Value='<%# Eval("intInctId") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Provision">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="LnkBtn_Read_Provision" runat="server" 
                                                                                            OnClick="LnkBtn_Read_Provision_Click" 
                                                                                            ToolTip="Click Here to View Provision Details">View Details</asp:LinkButton>
                                                                                        <asp:HiddenField ID="Hid_Prov_File_Name" runat="server" 
                                                                                            Value='<%# Eval("strProvFileName") %>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="10%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Operational Guidelines">
                                                                                    <ItemTemplate>
                                                                                        <asp:HyperLink ID="Hlnk_Policy_Name" runat="server" Target="_blank" 
                                                                                            Text="View Details" ToolTip="Click Here to View OG Doc"></asp:HyperLink>
                                                                                        <asp:HiddenField ID="Hid_OG_Doc" runat="server" 
                                                                                            Value='<%# Eval("strOGDoc") %>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="15%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText=" Disbursement Type">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Lbl_Disbursement_Type" runat="server" 
                                                                                            Text='<%# Eval("strDisburseType") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="13%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText=" Availment Type">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Lbl_Avail_Type" runat="server" 
                                                                                            Text='<%# Eval("strAvailType") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="10%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Nature">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Lbl_Inct_Nature" runat="server" 
                                                                                            Text='<%# Eval("strInctNature") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="5%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Apply">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="LnkBtn_Apply" runat="server" OnClick="LnkBtn_Apply_Click" 
                                                                                            ToolTip="Click Here to Apply this Incentive !!">Apply</asp:LinkButton>
                                                                                        <asp:HiddenField ID="Hid_Form_Id" runat="server" 
                                                                                            Value='<%# Eval("strFormId") %>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="7%" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <h1>
                                                                            List of All Incentives
                                                                            <hr />
                                                                            <h1>
                                                                            </h1>
                                                                            <asp:GridView ID="Grd_Without_Eligibility" runat="server" 
                                                                                AutoGenerateColumns="false" class="table table-bordered table-hover" 
                                                                                EmptyDataText="You Are not Eligible for Any Incentive !!" 
                                                                                OnRowDataBound="Grd_Incentives_RowDataBound">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Incentive">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Lbl_Inct_Name" runat="server" Text='<%# Eval("strInctName") %>'></asp:Label>
                                                                                            <asp:HiddenField ID="Hid_Inct_Id" runat="server" 
                                                                                                Value='<%# Eval("intInctId") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Provision">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="LnkBtn_Read_Provision" runat="server" 
                                                                                                OnClick="LnkBtn_Read_Provision_Click" 
                                                                                                ToolTip="Click Here to View Provision Details">View Details</asp:LinkButton>
                                                                                            <asp:HiddenField ID="Hid_Prov_File_Name" runat="server" 
                                                                                                Value='<%# Eval("strProvFileName") %>' />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="10%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Operational Guidelines">
                                                                                        <ItemTemplate>
                                                                                            <asp:HyperLink ID="Hlnk_Policy_Name" runat="server" Target="_blank" 
                                                                                                Text="View Details" ToolTip="Click Here to View OG Doc"></asp:HyperLink>
                                                                                            <asp:HiddenField ID="Hid_OG_Doc" runat="server" 
                                                                                                Value='<%# Eval("strOGDoc") %>' />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="15%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText=" Disbursement Type">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Lbl_Disbursement_Type" runat="server" 
                                                                                                Text='<%# Eval("strDisburseType") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="13%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText=" Availment Type">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Lbl_Avail_Type" runat="server" 
                                                                                                Text='<%# Eval("strAvailType") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="10%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Nature">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Lbl_Inct_Nature" runat="server" 
                                                                                                Text='<%# Eval("strInctNature") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="5%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Apply">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="LnkBtn_Apply_WO_Eligibility" runat="server" 
                                                                                                OnClick="LnkBtn_Apply_WO_Eligibility_Click" 
                                                                                                ToolTip="Click Here to Apply this Incentive !!">Apply</asp:LinkButton>
                                                                                            <asp:HiddenField ID="Hid_Form_Id" runat="server" 
                                                                                                Value='<%# Eval("strFormId") %>' />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="7%" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                            <h4>
                                                                                Disclaimer</h4>
                                                                            <p>
                                                                                <small style="font-size: 85%">Applicable incentives shown are purely based on 
                                                                                the inputs provided by the applicant. The actual claim will be verified by the 
                                                                                concerned authority.</small></p>
                                                                        </h1>
                                                                    </h1>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    </hr>
                                                </h1>
                                            </div>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="Hid_Pop" runat="server" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="Btn_Close">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: none;">
                <div class="modal-dialog modal-lg provisionmodal ">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header bg-purpul">
                            <asp:LinkButton ID="Btn_Close" CssClass="close" runat="server">&times;</asp:LinkButton>
                            <h4 class="modal-title">
                                Provision Details</h4>
                        </div>
                        <div class="modal-body">
                            <div style="overflow: auto; height: 400px;">
                                <asp:Image ID="Img_Provision" runat="server" CssClass="img img-responsive" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-sm-6">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Btn_Close" />
        </Triggers>
    </asp:UpdatePanel>
    <uc3:footer ID="footer" runat="server" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 450px;
            height: 550px;
        }
    </style>
    <script type="text/javascript">
        function closeModPopUp() {
            $('#Panel1').dialog('close');
        }
    </script>
    </form>
</body>
</html>
