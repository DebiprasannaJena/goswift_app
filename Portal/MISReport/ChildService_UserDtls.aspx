<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ChildService_UserDtls.aspx.cs"
    Inherits="Portal_MISReport_ChildService_UserDtls" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(window).load(function () {
            $("#PrintBtn").click(function () {
                window.print();
            });

        });
    
    </script>
    <style>
        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th
        {
            padding: 4px;
            font-size: 13px;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="form-group NOPRINT" id="divExport" runat="server" visible="false" align="right">
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF"
            OnClick="lnkPdf_Click"></asp:LinkButton>
        <a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
            <asp:LinkButton ID="LinkButton2" CssClass="back" runat="server" title="Export to Excel"
                OnClick="lnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
        </a><a class="PrintBtn" id="PrintBtn" data-tooltip="Print" data-toggle="tooltip"
            data-title="Print"><i class="panel-control-icon fa fa-print"></i></a>
    </div>
    <div class="table-responsive">
        <div style="text-align: right; width: 100%; text-align: right;" class="noprint pagelist">
            <asp:Label ID="lblDetails" runat="server"></asp:Label>
            <asp:DropDownList ID="ddlNoOfRec" ToolTip="Page Size" Width="80px" runat="server"
                AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRec_SelectedIndexChanged"
                CssClass="form-control" Style="display: inline">
            </asp:DropDownList>
        </div>
        <asp:Label ID="lblSearchDetails" runat="server"></asp:Label>
        <asp:GridView ID="grdService" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
            CssClass="table table-bordered table-hover" OnRowDataBound="grdService_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Sl#" ItemStyle-Width="5%">
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Service Name" DataField="ServiceName" ItemStyle-Width="25%" />
                <asp:BoundField HeaderText="Proposal No" DataField="ProposalNo" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="Application No" DataField="strApplicationNo" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="Applied Date" DataField="strApplicationDate" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="Approved Date" DataField="strApprovalDate" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="Payment Amount" DataField="decPaymentAmt" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="Investor Name" DataField="strCompany" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="Location (Block)" DataField="strBlock" ItemStyle-Width="10%" />
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
            <asp:HiddenField ID="hdnPgindex" runat="server" Value="Blank Value" />
        </div>
    </div>
    </form>
</body>
</html>
