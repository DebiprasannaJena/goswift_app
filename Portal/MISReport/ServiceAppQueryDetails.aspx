<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ServiceAppQueryDetails.aspx.cs"
    Inherits="Portal_MISReport_ServiceAppQueryDetails" %>

<html >
<head id="Head1" runat="server">
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script >
        $(window).load(function () {
            $("#PrintBtn").click(function () {

                window.print();
            });

        });
    
    </script>
    <title></title>
    <style>
        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th
        {
            padding: 4px;
            font-size: 13px;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="form-group" id="divExport" runat="server" visible="false">
        <div class="row" align="right" class="noprint">
            <asp:LinkButton ID="lnkPdf" CssClass="back" runat="server" title="Export to PDF"
                OnClick="lnkPdf_Click"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
            <a class="PrintBtn"  data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" title="Export to Excel"
                    OnClick="lnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
            </a>
            <a class="PrintBtn" id="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print">
                <i class="panel-control-icon fa fa-print"></i></a>
        </div>
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
        <asp:GridView ID="grdPealDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
            CssClass="table table-bordered table-responsive">
            <Columns>
                <asp:TemplateField HeaderText="Sl#" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Proposal No" DataField="ProposalNo" />
                <asp:BoundField HeaderText="Service Name" DataField="ServiceName" />
                <asp:BoundField HeaderText="Company/Organization Name" DataField="strCompany" />
                <asp:BoundField HeaderText="Location (Block)" DataField="strBlock" />
                <asp:BoundField HeaderText="First Time Query" DataField="FirstTimeQuery" />
                <asp:BoundField HeaderText="Response" DataField="FirstResponse" />
                <asp:BoundField HeaderText="Clarification on First Query" DataField="SecondQuery" />
                <asp:BoundField HeaderText="Response on Clarification" DataField="SecondResponse" />
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
