<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InnerInvestorDashboard.aspx.cs"
    Inherits="InnerInvestorDashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
        .colon
        {
            float: left;
            margin-left: -10px;
        }
    </style>
</head>
<body>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <form id="form2" runat="server">
    <div align="right">
        <asp:LinkButton Visible="false" ID="lbtnAll" CssClass="all" runat="server" Text="All" />
        &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <div>
                    <div class="form-group">
                        <label class="col-sm-3">
                            Department Name</label>
                        <div class="col-sm-5">
                            <span class="colon">:</span><asp:DropDownList CssClass="form-control" ID="ddldept"
                                OnSelectedIndexChanged="ddldept_OnSelectedIndexChanged" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <asp:GridView ID="grdServiceDtl" runat="server" CssClass="table table-bordered" AllowPaging="true"
                        PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                        CellPadding="4" GridLines="None">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pending Approvals">
                                <ItemTemplate>
                                    <asp:Label ID="lblPending" runat="server" Text='<%# Eval("strPending") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rejected Approvals">
                                <ItemTemplate>
                                    <asp:Label ID="lblReject" runat="server" Text='<%# Eval("strRejected") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pagination-grid no-print" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
