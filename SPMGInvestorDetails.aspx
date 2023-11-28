<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SPMGInvestorDetails.aspx.cs"
    Inherits="SPMGInvestorDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body class="bg-white" style="background: none;">
    <form id="form1" runat="server">
    <div align="right">
        <asp:LinkButton ID="lbtnAll" Visible="false" CssClass="all" runat="server" Text="All"
            OnClick="lbtnAll_Click" />
        &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
    </div>
    <asp:GridView ID="grdSPMGDtl" runat="server" CssClass="table table-bordered" AllowPaging="true"
        PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
        OnPageIndexChanging="grdSPMGDtl_PageIndexChanging" CellPadding="4" Width="100%"
        OnRowDataBound="grdSPMGDtl_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Sl#">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Project Name">
                <ItemTemplate>
                    <asp:Label ID="lblProj" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Project Department">
                <ItemTemplate>
                    <asp:Label ID="lblProj" runat="server" Text='<%# Eval("Project_Department") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type of the Issue">
                <ItemTemplate>
                    <asp:Label ID="lblIssues" runat="server" Text='<%# Eval("Type_Of_Issue") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Issue Date">
                <ItemTemplate>
                    <asp:Label ID="lblIssue_Date" runat="server" Text='<%# Eval("Issue_Date") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Issue Description">
                <ItemTemplate>
                    <asp:Label ID="lblIssue_Description" runat="server" Text='<%# Eval("Issue_Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Issue Category">
                <ItemTemplate>
                    <asp:Label ID="lblIsCat" runat="server" Text='<%# Eval("IssueCategory") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name Of Investor">
                <ItemTemplate>
                    <asp:Label ID="lblName_Of_Investor" runat="server" Text='<%# Eval("Name_Of_Investor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Pending Department">
                <ItemTemplate>
                    <asp:Label ID="lblPending_Department" runat="server" Text='<%# Eval("Pending_Department") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Pending Department Type">
                <ItemTemplate>
                    <asp:Label ID="lblPending_Department_Type" runat="server" Text='<%# Eval("Pending_Department_Type") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Pending Days">
                <ItemTemplate>
                    <asp:Label ID="lblDays" runat="server" Text='<%# Eval("PendingDays") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-grid no-print" />
    </asp:GridView>
    </form>
</body>
</html>
