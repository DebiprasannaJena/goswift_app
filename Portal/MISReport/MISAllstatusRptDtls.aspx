<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MISAllstatusRptDtls.aspx.cs"
    Inherits="Portal_MISReport_MISAllstatusRptDtls" EnableEventValidation="false" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script>
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
    <div class="form-group" id="divExport" runat="server" visible="false">
        <div class="row" align="right" class="noprint">
            <asp:LinkButton ID="lnkPdf" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF"
                OnClick="lnkPdf_Click"></asp:LinkButton>
            <a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" title="Export to Excel"
                    OnClick="lnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
            </a><a class="PrintBtn" id="PrintBtn" data-tooltip="Print" data-toggle="tooltip"
                data-title="Print"><i class="panel-control-icon fa fa-print"></i></a>
        </div>
    </div>
    <div class="table-responsive" style="overflow: hidden;">
        <div style="text-align: right; width: 100%; text-align: right;" class="NOPRINT pagelist">
            <asp:Label ID="Lbl_Details" runat="server"></asp:Label>
            <asp:DropDownList ID="DrpDwn_NoOfRec" ToolTip="Page Size" Width="80px" runat="server"
                AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRec_SelectedIndexChanged"
                CssClass="form-control NOPRINT" Style="display: inline">
            </asp:DropDownList>
        </div>
        <asp:Label ID="Lbl_Search_Details" runat="server"></asp:Label>
        <br /><br />
        <asp:GridView ID="GrdPealDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
            CssClass="table table-bordered table-responsive" ShowFooter="true" OnRowDataBound="GrdPealDetails_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Sl#">
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Proposal No" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:HyperLink ID="hypProposalNo" runat="server" Text='<%#Eval("ProposalNo")%>' ToolTip="View Details"></asp:HyperLink>
                        <asp:HiddenField ID="HdnFieldRemarks" runat="server" Value='<%# Bind("strRemarks") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Company/Organization Name" DataField="strCompany" />
                <asp:BoundField HeaderText="Applied Date" DataField="strApplicationDate" />
                <asp:BoundField HeaderText="ORTPSA Timeline Date" DataField="strORTPSATimelineDate" />
                <asp:BoundField HeaderText="Approval Date" DataField="strApprovalDate" />
                <asp:BoundField HeaderText="Location (Block)" DataField="strBlock" />
                <asp:BoundField HeaderText="Sector" DataField="strSector" />
                <asp:BoundField HeaderText="Sub Sector" DataField="strSubSector" />
                <asp:BoundField HeaderText="Investment  (Rs. In Lakhs)" DataField="decInvestment"
                    FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="Proposed Employment" DataField="intPropEmployment" FooterStyle-Font-Bold="true"
                    FooterStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="decTotalLandRequired" HeaderText="Total Land Required"
                    FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="decLandRecommendedToIdco" HeaderText="Land Recommended To Idco"
                    FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
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
            <asp:HiddenField ID="Hdn_Pgindex" runat="server" Value="Blank Value" />
        </div>
    </div>
    </form>
</body>
</html>
