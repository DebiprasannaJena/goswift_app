<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryPastTimeline.aspx.cs" Inherits="Portal_Dashboard_QueryPastTimeline" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvService" CssClass="table table-bordered" runat="server"
            AutoGenerateColumns="false" AllowPaging="true" Width="100%" EmptyDataText="No Record(s) Found..."
            DataKeyNames="" PageSize="10">
            <Columns>
                <asp:TemplateField HeaderText="Sl No">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("intSlNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department Name">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("strDistrictName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type Of Query">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("strComapnyName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Time since the receipt of the query">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("intDaysPass") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-grid no-print" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
