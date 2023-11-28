<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CSRInvestorGrid.aspx.cs"
    Inherits="CSRInvestorGrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="tab-content clearfix">
        <div class="tab-pane active" id="1a">
            <div class="form-sec">
                <div class="form-body minheight350">
                    <div align="right">
                        <asp:LinkButton ID="lbtnAll1" Visible="false" CssClass="all" runat="server" Text="All"
                            OnClick="lbtnAll1_Click" />
                        &nbsp;&nbsp;<asp:Label ID="lblPaging1" runat="server" />&nbsp;&nbsp;&nbsp;
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="gvCSRStatus" runat="server" CssClass="table table-bordered" AllowPaging="true"
                            PageSize="10" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                            OnPageIndexChanging="gvCSRStatus_PageIndexChanging" CellPadding="4" OnRowDataBound="gvCSRStatus_RowDataBound">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#"></asp:TemplateField>
                                <asp:TemplateField HeaderText="Spent Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("AmountSpent") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Name">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-grid no-print" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
