<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrivMisDrillDownStatusRpt.aspx.cs"
    Inherits="Portal_MISReport_GrivMisDrillDownStatusRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        $(window).load(function () {
            $("#PrintBtn").click(function () {
                window.print();
            });
        });

    </script>
    <style type="text/css">
        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 4px;
            font-size: 13px;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-group" id="divExport" runat="server" visible="false">
            <div class="row" align="right" class="noprint">
                <asp:LinkButton ID="LnkBtnPdf" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF"
                    OnClick="LnkBtnPdf_Click"></asp:LinkButton>
                <a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                    <asp:LinkButton ID="LnkBtnExcel" CssClass="back" runat="server" title="Export to Excel"
                        OnClick="LnkBtnExcel_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                </a><a class="PrintBtn" id="PrintBtn" data-tooltip="Print" data-toggle="tooltip"
                    data-title="Print"><i class="panel-control-icon fa fa-print"></i></a>
            </div>
        </div>
        <div class="table-responsive" style="overflow: hidden;">
            <div style="text-align: right; width: 100%; text-align: right;" class="NOPRINT pagelist">
                <asp:Label ID="lblDetails" runat="server"></asp:Label>
                <asp:DropDownList ID="DrpDwn_NoOfRec" ToolTip="Page Size" Width="80px" runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="DrpDwn_NoOfRec_SelectedIndexChanged"
                    CssClass="form-control NOPRINT" Style="display: inline">
                </asp:DropDownList>
            </div>
            <asp:Label ID="lblSearchDetails" runat="server"></asp:Label>
            <asp:GridView ID="GrdGrivDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                CssClass="table table-bordered table-responsive" ShowFooter="true" OnRowDataBound="GrdGrivDetails_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sl#">
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grievance No" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:HyperLink ID="hypGrievanceNo" runat="server" Text='<%#Eval("vchGrivId")%>' ToolTip="View Details"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Company/Organization Name" DataField="VCH_INV_NAME" />
                    <asp:BoundField HeaderText="Grievance Type" DataField="vchGrivType" />
                    <asp:BoundField HeaderText="Grievance Sub Type" DataField="vchGrivSubType" />
                    <asp:TemplateField HeaderText="Applied Date">
                        <ItemTemplate>
                            <asp:Label ID="Lbl_Apply_Date" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Status" DataField="vchStatusName" ItemStyle-Width="9%" />
                    <asp:BoundField HeaderText="Action Date" DataField="dtmApprovalDate" ItemStyle-Width="7%" />
                </Columns>
            </asp:GridView>
            <div style="float: right;" class="noPrint" id="divPaging" runat="server">
                <asp:Repeater ID="rptPager" runat="server">
                    <ItemTemplate>
                        <ul class="pagination">
                            <li class='<%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %> '>
                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                    OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
                <%--   <asp:HiddenField ID="HiddenField1" runat="server" Value="Blank Value" />--%>
                <asp:HiddenField ID="Hid_Page_Index" runat="server" Value="Blank Value" />
            </div>
        </div>
    </form>
</body>
</html>
