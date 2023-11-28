<%@ Page Language="C#" AutoEventWireup="true" CodeFile="APAARequestsGrid.aspx.cs"
    Inherits="APAARequestsGrid" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script>
        $(window).load(function () {
            $('.printbtn').click(function () {
                window.print();

            });

        })
       
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <%-- <div class="tab-content clearfix">
    <div class="tab-pane active" id="1a">--%>
    <div class="form-sec">
        <%-- <div class="form-body minheight350">--%>
        <div align="right" class="noprint">
            <asp:LinkButton ID="lbtnAll" Visible="false" CssClass="all" runat="server" Text="All"
                OnClick="lbtnAll_Click" />
            &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
            <img src="images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
            <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
            <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                <i class="fa fa-print"></i></a>
        </div>
        <div style="clear: both;">
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvAPAAStatus" runat="server" CssClass="table table-bordered" AllowPaging="true"
                PageSize="50" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                OnPageIndexChanging="gvAPAAStatus_PageIndexChanging" OnRowDataBound="gvAPAAStatus_RowDataBound">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="50px">
                        <%--  <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Name" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("PartyName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IE Name" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("IEName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Application Name">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("ApplicationName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pending Days" ItemStyle-HorizontalAlign="Right">
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
                 <PagerStyle CssClass="pagination-grid noprint" />
            </asp:GridView>
        </div>
        <div class="table-responsive" id="viewTable" runat="server">
        <table style="width: 210px" runat="server" id="tbldv">
                <tr>
                    <td style="background-color: green; border: 1px solid black" align="center">
                        <asp:Label ID="lblCaption" runat="server" Style="font-weight: bold; color: White;"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False"
                EmptyDataText="No Record(s) Found" Width="100%" CellPadding="4" OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Name">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("PartyName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IE Name">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("IEName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Application Name">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("ApplicationName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pending Days" ItemStyle-HorizontalAlign="Right">
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
                <PagerStyle CssClass="pagination-grid noprint" />
            </asp:GridView>
        </div>
    </div>
    <%--  </div>
                    </div>
    </div>--%>
    </form>
</body>
</html>
