<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CSRGridStatus.aspx.cs" Inherits="CSRGridStatus"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <%--   <div class="tab-pane active" id="1a">
            <div class="form-sec">
                <div class="form-body minheight350">--%>
        <div align="right" class="noprint">
            <asp:LinkButton ID="lbtnAll1" Visible="false" CssClass="all" runat="server" Text="All"
                OnClick="lbtnAll1_Click" />
            &nbsp;&nbsp;<asp:Label ID="lblPaging1" runat="server" />&nbsp;&nbsp;&nbsp;
            <img src="images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
            <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
            <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                <i class="fa fa-print"></i></a>
        </div>
        <div style="clear: both;">
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvCSRStatus" runat="server" CssClass="table table-bordered" AllowPaging="true"
                PageSize="50" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                OnPageIndexChanging="gvCSRStatus_PageIndexChanging" CellPadding="4" OnRowDataBound="gvCSRStatus_RowDataBound">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl#"></asp:TemplateField>
                    <asp:TemplateField HeaderText="Category Name">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spent Amount (in Cr.)">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("AmountSpent") %>'></asp:Label>
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
            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" Width="100%"
                AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" CellPadding="4"
                OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl#"></asp:TemplateField>
                    <asp:TemplateField HeaderText="Category Name">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spent Amount (in Cr.)">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("AmountSpent") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-grid noprint" />
            </asp:GridView>
        </div>
        <%-- </div>
            </div>
        </div>--%>
    </div>
    </form>
</body>
</html>
