<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalTimeLineDepartment.aspx.cs"
    Inherits="Portal_Dashboard_ApprovalTimeLineDepartment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="tab-content clearfix">
        <div class="tab-pane active" id="1a">
            <div class="form-sec">
                <div class="form-body minheight350">
                    <div align="right">
                        <asp:LinkButton Visible="false" ID="lbtnAll" CssClass="all" runat="server" Text="All" />
                        &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
                    </div>
                    <%--<div class="row">
                 <label class="col-sm-4">Days Difference</label>
                  
                    <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlDays" runat="server" Width="200px">
                <asp:ListItem Value="20">0-20 Days</asp:ListItem>
                <asp:ListItem Value="30">20-30 Days</asp:ListItem>
                <asp:ListItem Value="60">30-60 Days</asp:ListItem>
                <asp:ListItem Value="61">>60 Days</asp:ListItem>
                </asp:DropDownList>
                  
                  </div>--%>
                    <div class="form-group">
                        <div class="table-responsive">
                            <asp:GridView ID="gvService" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false"
                                AllowPaging="false" Width="100%" EmptyDataText="No Record(s) Found..." DataKeyNames=""
                                PageSize="10">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            Sl No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("strComapnyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("intDaysPass") %>'></asp:Label>
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
    </div>
    </form>
</body>
</html>
