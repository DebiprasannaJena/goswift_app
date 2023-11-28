<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryPendingStatus.aspx.cs"
    Inherits="Portal_Dashboard_QueryPendingStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="row">
                    <label class="col-sm-3">
                        Days Difference</label>
                    <div class="col-sm-4">
                        <span class="colon">:</span>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Text="--select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="0-3 days" Value="1"></asp:ListItem>
                            <asp:ListItem Text="3-10 days" Value="2"></asp:ListItem>
                            <asp:ListItem Text=">10 days" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
             <div align="right">
                <asp:LinkButton Visible="false" ID="lbtnAll" CssClass="all" runat="server" Text="All"
                    OnClick="lbtnAll_Click" />
                &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
            </div>
            <asp:GridView ID="gvService" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false"
                AllowPaging="true" Width="100%" EmptyDataText="No Record(s) Found..." DataKeyNames=""
                PageSize="10" GridLines="None"
                    OnPageIndexChanging="gvService_PageIndexChanging" OnRowDataBound="gvService_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sl No">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("intSlNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Name">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("strUnitname") %>'></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
