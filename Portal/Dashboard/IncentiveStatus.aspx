<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncentiveStatus.aspx.cs"
    Inherits="Portal_Dashboard_IncentiveStatus" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script>
        $(window).load(function () {
            $('.printbtn').click(function () {
                window.print();

            });

        })
       
      
    </script>
</head>
<body>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
    <div align="right" class="noprint">
        <asp:LinkButton Visible="false" ID="lbtnAll" CssClass="all" runat="server" Text="All"
            OnClick="lbtnAll_Click" />
        &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
        <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
            <i class="fa fa-print"></i></a>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="gvIncentive" runat="server" CssClass="table table-bordered" AllowPaging="true"
            AutoGenerateColumns="False" PageSize="50" EmptyDataText="No Record(s) Found" GridLines="None"
            OnPageIndexChanging="gvIncentive_PageIndexChanging" OnRowDataBound="gvIncentive_RowDataBound">
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
                <asp:TemplateField HeaderText="District">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Distict") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sector">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("strIncentiveSector") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("strIncentiveStatus") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Number of days since the incentive application was received"
                    ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("intDaysPass") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-grid noprint" HorizontalAlign="Right" />
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
            EmptyDataText="No Record(s) Found" GridLines="None" OnRowDataBound="GridView1_RowDataBound">
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
                <asp:TemplateField HeaderText="District">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Distict") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sector">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("strIncentiveSector") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("strIncentiveStatus") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Number of days since the incentive application was received"
                    ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("intDaysPass") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
