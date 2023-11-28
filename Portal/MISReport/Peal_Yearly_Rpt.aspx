<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="Peal_Yearly_Rpt.aspx.cs" Inherits="Portal_MISReport_Peal_Yearly_Rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                    MIS Yearly Report for PEAL</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Proposal</a></li><li><a>View</a></li></ul>
            </div>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="dropdown">
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <li>
                                        <asp:LinkButton ID="lnkPdf" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF"
                                            OnClick="lnkPdf_Click"></asp:LinkButton></li>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" title="Export to Excel"
                                            OnClick="lnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                                    </a></li>
                                    <li><a class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print">
                                        <i class="panel-control-icon fa fa-print"></i></a></li>
                                    <li><a href="javascript:history.back()" data-tooltip="Back" data-toggle="tooltip"
                                        data-title="Back"><i class="panel-control-icon fa  fa-chevron-circle-left"></i></a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="search-sec NOPRINT">
                                <div class="form-group row NOPRINT">
                                    <asp:UpdatePanel ID="up1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-3">
                                                <label for="Department">
                                                    Department</label>
                                                <asp:DropDownList ID="drpDepartment" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged">
                                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Service">
                                                    Service</label>
                                                <asp:DropDownList ID="drpService" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="col-sm-3">
                                        <label for="Year">
                                            Year</label>
                                        <asp:DropDownList ID="drpFinancialYear" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm"
                                            runat="server" Text="Search"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive" id="divMain" runat="server">
                                <div id="divLogo" runat="server" visible="false">
                                    <asp:Image ID="imgLogo" runat="server" Height="100" Width="100" />
                                    <h5>
                                        <asp:Label ID="lblMessage" runat="server"></asp:Label></h5>
                                </div>
                                <br />
                                <table style="width: 100%;" class="table table-bordered table-hover">
                                    <tr>
                                        <th>
                                            Month
                                        </th>
                                        <th>
                                            Total Applications
                                        </th>
                                        <th>
                                            Pending
                                        </th>
                                        <th>
                                            Carry Forward Pending
                                        </th>
                                        <th>
                                            Application passed ORTPS Timeline
                                        </th>
                                        <th>
                                            No. of Application with Query
                                        </th>
                                        <th>
                                            Approved
                                        </th>
                                        <th>
                                            Rejected
                                        </th>
                                        <th>
                                            Avg. No. of days for approval
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            April
                                        </td>
                                        <td>
                                           20
                                        </td>
                                        <td>
                                            10
                                        </td>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            8
                                        </td>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            6
                                        </td>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            May
                                        </td>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            15
                                        </td>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            June
                                        </td>
                                        <td>
                                            15
                                        </td>
                                        <td>
                                            10
                                        </td>
                                        <td>
                                            13
                                        </td>
                                        <td>
                                            10
                                        </td>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            5
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            July
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            August
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            September
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            October
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            November
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            December
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            January
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            February
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            March
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                        <td>
                                            0
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
