<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashboardLandDetails.aspx.cs"
    Inherits="Portal_Dashboard_DashboardLandDetails" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
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
            <asp:LinkButton Visible="false" ID="lbtnAll" CssClass="all" runat="server" Text="All" />
            &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
            <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
            <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>
            &nbsp;&nbsp;&nbsp; <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                <i class="fa fa-print"></i></a>
        </div>
        <div style="clear: both;">
        </div>
        <div class="table-responsive" id="viewTable" runat="server">
            <table style="width: 210px" runat="server" id="tbldv">
                <tr>
                    <td style="background-color: green; border: 1px solid black" align="center">
                        <asp:Label ID="lblCaption" runat="server" Style="font-weight: bold; color: White;"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvLand" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false"
                AllowPaging="false" Width="100%" EmptyDataText="No Record(s) Found..." PageSize="10">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            Sl No.
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Proposal No">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("PropNoForLand") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name Of The Company">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("strComapnyName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Land (In Acres)">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("AreaAllotLand") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("RequestDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Distirict">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Distirict") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-grid noprint" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
