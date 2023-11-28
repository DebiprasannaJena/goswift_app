<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinancialPerformanceView.aspx.cs"
    Inherits="SingleWindow_FinancialPerformanceView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IPICOL Agenda Monitoring System</title>
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <style>
        body
        {
            background: #fff;
        }
        .form-control-static
        {
            padding-top: 5px;
            display: inline-block;
        }
    </style>
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/bootstrap.min.js" type="text/javascript" language="javascript"></script>
    <script src="../js/loadComponent.js" language="javascript" type="text/javascript"></script>
    <script src="../js/custom.js" type="text/javascript" language="javascript"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            groupTable($('#GrdFinanace tr:has(td)'), 1, 2);
            $('#GrdFinanace .deleted').remove();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <%--    <table border="0" cellpadding="0" cellspacing="0" class="table" width="100%">
        <tr>
            <td width="100px">
                Project Name
            </td>
            <td align="center">
                :
            </td>
            <td>
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">--%>
    <div class="viewTable" id="viewTable">
        <div class="table-responsive">
            <asp:GridView ID="GrdFinanace" runat="server" Width="100%" AutoGenerateColumns="False"
                OnDataBound="OnDataBound" CssClass="table table-bordered" OnRowDataBound="GrdFinanace_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sl#">
                        <ItemTemplate>
                            <span>
                                <%#Container.DataItemIndex + 1%>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ComapnyName" HeaderText="Company Name">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Particulars" HeaderText="Particulars">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FinYear1" HeaderText="Finance Year">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FinYear2" HeaderText="Finance Year">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FinYear3" HeaderText="Finance Year">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Remark" HeaderText="Remark">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Financial Document" Visible="false">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnFinDoc" runat="server" Value='<%# Eval("FinDoc") %>' />
                            <asp:HyperLink ID="hlDoc" runat="server" Target="_blank" ImageUrl="~/img/pdf_icon_32.png"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="paging NOPRINT" />
                <PagerSettings Mode="NumericFirstLast" NextPageText="Next" FirstPageText="First"
                    LastPageText="Last" PreviousPageText="Prev" Position="Bottom" />
            </asp:GridView>
            <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
                CssClass="lblMessage" />
        </div>
    </div>
    <%-- </td>
        </tr>
    </table>--%>
    </form>
</body>
</html>
