<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceQueryDetails.aspx.cs"
    Inherits="Portal_Dashboard_ServiceQueryDetails" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
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
<body class="bg-white" style="background: none;">
    <form id="form1" runat="server">
    <div align="right" class="noprint">
        <asp:LinkButton ID="lbtnAll" CssClass="all" runat="server" Text="All" OnClick="lbtnAll_Click" />
        &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
        <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
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
        <asp:GridView ID="gvService" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" Width="100%" EmptyDataText="No Record(s) Found..." PageSize="50"
            OnRowDataBound="gvService_RowDataBound" OnPageIndexChanging="gvService_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="Sl#"></asp:TemplateField>
                <asp:TemplateField HeaderText="Application No">
                    <ItemTemplate>
                        <asp:Label ID="Label1det" runat="server" Text='<%# Eval("VCH_APPLICATION_UNQ_KEY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Service Name">
                    <ItemTemplate>
                        <asp:Label ID="Label21" runat="server" Text='<%# Eval("VCH_SERVICENAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department Name">
                    <ItemTemplate>
                        <asp:Label ID="Label91" runat="server" Text='<%# Eval("nvchLevelName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Application Date">
                    <ItemTemplate>
                        <asp:Label ID="Label42" runat="server" Text='<%# Eval("APPN_dATE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Details of Query Raised">
                    <ItemTemplate>
                        <asp:Label ID="Label666" runat="server" Text='<%# Eval("1stQueryrRaiseRemark") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Query Raised Date">
                    <ItemTemplate>
                        <asp:Label ID="Label35" runat="server" Text='<%# Eval("1stQueryDateRaise") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Query Response Date">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%# Eval("1stQueryDateResponse") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Details of Response to Query">
                    <ItemTemplate>
                        <asp:Label ID="Label68" runat="server" Text='<%# Eval("1stQueryResponseRemarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date of Clarification on Query Response">
                    <ItemTemplate>
                        <asp:Label ID="Label59" runat="server" Text='<%# Eval("2ndQueryDateRaise") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Details of Clarification Query Response">
                    <ItemTemplate>
                        <asp:Label ID="Label70" runat="server" Text='<%# Eval("2ndQueryRaiseRemark") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date of Response to Clarification">
                    <ItemTemplate>
                        <asp:Label ID="Label589" runat="server" Text='<%# Eval("2ndQueryDateResponse") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Details of Response to Clarification">
                    <ItemTemplate>
                        <asp:Label ID="Label756" runat="server" Text='<%# Eval("2ndQueryResponseRemarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Days Taken to Raise Query">
                    <ItemTemplate>
                        <asp:Label ID="Label800" runat="server" Text='<%# Eval("Avg_time") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-grid noprint" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
