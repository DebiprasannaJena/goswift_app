<%@ Page Language="C#" AutoEventWireup="true" CodeFile="APAAInvestorGrid.aspx.cs" Inherits="APAAInvestorGrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                                        <asp:GridView ID="gvAPAAStatus" runat="server" CssClass="table table-bordered"
                                            AllowPaging="true" PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"  Width="100%"
                                            OnPageIndexChanging="gvAPAAStatus_PageIndexChanging" CellPadding="4" onrowdatabound="gvAPAAStatus_RowDataBound">
                                            <AlternatingRowStyle />
                                            <Columns>
                                            <asp:TemplateField HeaderText="Sl#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Application Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("ApplicationName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IE Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("IEName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Party Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("PartyName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                               <asp:TemplateField HeaderText="Pending Days">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("PendingDays") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Request Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("RequestDate") %>'></asp:Label>
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
