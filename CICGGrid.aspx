<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CICGGrid.aspx.cs" Inherits="CICGGrid"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
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
    <div class="form-sec">
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
        <div class="form-group">
            <div class="table-responsive">
                <asp:GridView ID="gvCICGStatus" runat="server" CssClass="table table-bordered" AllowPaging="true"
                    PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                    OnPageIndexChanging="gvCICGStatus_PageIndexChanging" CellPadding="4" Width="100%">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl#">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inspector Name">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("InspectorName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Industry Name">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("IndustryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pending in Hours">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("TOTALHOUR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-grid no-print" />
                </asp:GridView>
            </div>
            <div class="table-responsive" id="viewTable" runat="server">
              <table style="width: 210px" runat="server" id="tbldv">
                <tr>
                    <td>
                        <asp:Label ID="lblCaption" runat="server" Style="font-weight: bold; color: White;"></asp:Label>
                    </td>
                </tr>
            </table>
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered"  AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                   CellPadding="4" Width="100%">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl#">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inspector Name">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("InspectorName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Industry Name">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("IndustryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pending in Hours">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("TOTALHOUR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
               
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
