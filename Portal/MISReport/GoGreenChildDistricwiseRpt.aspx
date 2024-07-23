<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoGreenChildDistricwiseRpt.aspx.cs" Inherits="Portal_MISReport_GoGreenChildDistricwiseRpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#PrintBtn").click(function () {
                window.print();
            });
        });
    </script>
    <style>
        .table-responsive {
            margin-bottom: 15px;
            overflow-x: auto;
            min-height: .01%;
            overflow-y: hidden;
            -ms-overflow-style: -ms-autohiding-scrollbar;
        }
        .table {
            width: 100%;
            max-width: 100%;
            margin-bottom: 20px;
            border-collapse: collapse;
            border-spacing: 0;
            display: table;
        }
        .table-bordered {
            border: 1px solid #ddd;
        }
        .table-hover tbody tr:hover {
            background-color: #f5f5f5;
        }
        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px;
            line-height: 1.42857143;
            vertical-align: top;
            border-top: 1px solid #ddd;
        }
        .table > thead > tr > th {
            vertical-align: bottom;
            border-bottom: 2px solid #ddd;
        }
        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #ddd;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .noprint {
            display: none;
        }
        .pagelist {
            text-align: right;
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-group NOPRINT" id="divExport" runat="server" visible="false" align="right">
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF"
                ></asp:LinkButton>
            <a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                <asp:LinkButton ID="LinkButton2" CssClass="back" runat="server" title="Export to Excel"
                    ><i class="fa fa-file-excel-o"></i></asp:LinkButton>
            </a>
            <a class="PrintBtn" id="PrintBtn" data-tooltip="Print" data-toggle="tooltip"
                data-title="Print"><i class="panel-control-icon fa fa-print"></i></a>
        </div>
        <div class="content-wrapper">
            <section class="content-header">
                <div class="header-icon">
                    <i class="fa fa-dashboard"></i>
                </div>
                <div class="header-title">
                    <h1> Report Details</h1>
                </div>
            </section>
            <section class="content">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="table-responsive">
                            <asp:Label ID="LblSearchDetails" runat="server" Font-Bold="true"></asp:Label>
                            <div style="display: inline-block; text-align: right; width: 100%">
                                <asp:LinkButton ID="LbtnAll" runat="server" Visible="true" CssClass="" Text="All"></asp:LinkButton>
                                <asp:Label ID="lblPaging" runat="server"></asp:Label>
                            </div>
                            <div class="table-responsive" id="Div1" runat="server">
                                <asp:GridView ID="GrdChildRpt" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                    CssClass="table table-bordered table-hover" AllowPaging="true" ShowFooter="false" Width="100%" PageSize="100">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsl" runat="server" Text='<%#(GrdChildRpt.PageIndex * GrdChildRpt.PageSize) + (GrdChildRpt.Rows.Count + 1)%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District Name">
                                            <ItemTemplate>
                                                <asp:Label ID="DistrictName" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="ProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Developer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="DeveloperName" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-grid no-print" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
