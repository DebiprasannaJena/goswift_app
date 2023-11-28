<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncentiveInvestorStatus.aspx.cs"
    Inherits="IncentiveInvestorStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="tab-content clearfix">
        <div class="tab-pane active" id="1a">
            <div class="form-sec">
                <div class="form-body minheight350">
                    <div align="right">
                        <asp:LinkButton ID="lbtnAll" Visible="false" CssClass="all" runat="server" Text="All"
                            OnClick="lbtnAll_Click" />
                        &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
                    </div>
                    <div class="form-group">
                        <div class="table-responsive">
                            <asp:GridView ID="gvIncentive" runat="server" CssClass="table table-bordered" AllowPaging="true"
                                AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" Width="100%" OnPageIndexChanging="gvIncentive_PageIndexChanging"
                                OnRowDataBound="gvIncentive_RowDataBound">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#">
                                        <%--<ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company/Unit Applied">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("strINCCompanyname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sector">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("strIncentiveSector") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("strIncentiveStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Number of days since the incentive application was received">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("intDaysPass") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-grid no-print" HorizontalAlign="Right" />
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
