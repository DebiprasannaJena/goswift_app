<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="EscalationDetailsPopup.aspx.cs"
    Inherits="Portal_HelpDesk_EscalationDetailsPopup" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
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
    <div class="BuleboldTxt" align="center" style="background-color: #bfbfbf; }">
        <b>Escalation Details</b></div>
    <div class="box">
        <div class="box-body form-style table-responsive">
            <table class="table table-filter">
                <tr>
                    <td style="width: 20%;">
                        <strong>Issue Category</strong>
                    </td>
                    <td>
                        <strong>:</strong>
                    </td>
                    <td>
                        <asp:Label ID="lblComplaintType" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;">
                        <strong>Issue SubCategory</strong>
                    </td>
                    <td style="width: 2%;">
                        <strong>:</strong>
                    </td>
                    <td style="width: 78%;">
                        <asp:Label ID="lblSubComplaintType" runat="server" Style="word-wrap: break-word;"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-body table-responsive" id="viewTable">
        <asp:GridView ID="gvEscalationDtls" runat="server" AutoGenerateColumns="false" CssClass="table table-hover"
            Height="74px" Style="background-color: #bfbfbf !important; color: #000  !important;">
            <Columns>
                <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%#(gvEscalationDtls.PageIndex * gvEscalationDtls.PageSize) + (gvEscalationDtls.Rows.Count + 1)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="vch_UserName" HeaderText="Authority Level" ItemStyle-Width="10%" />
                <asp:BoundField DataField="VCH_STANDARD_DAYS" HeaderText="Standard Action Taking Days"
                    ItemStyle-Width="5%" />
                <asp:BoundField DataField="VchMobile" HeaderText="Notify Phone" ItemStyle-Width="20%" />
                <asp:BoundField DataField="vchMobileContent" HeaderText="Notify Phone Content" ItemStyle-Width="20%" />
                <asp:BoundField DataField="Email" HeaderText="Notify Email" ItemStyle-Width="20%" />
                <asp:TemplateField HeaderText="Notify Email Content" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <%#HttpUtility.HtmlDecode(Eval("vchEmailContent").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
               
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
