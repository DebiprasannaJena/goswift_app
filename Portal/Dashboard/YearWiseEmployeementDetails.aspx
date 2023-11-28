<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YearWiseEmployeementDetails.aspx.cs"
    Inherits="Portal_Dashboard_YearWiseEmployeementDetails" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $('#printbtn').click(function () {
                window.print();

            })
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div align="right" class="noprint">
                <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
                <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                    <i class="fa fa-print"></i></a>
            </div>
            <div class="table-responsive" id="viewTable" runat="server">
               <table style="width: 210px" runat="server" id="tbldv">
                    <tr>
                        <td style="background-color: green; border: 1px solid black" align="center">
                            <asp:Label ID="lblCaption" runat="server" Style="font-weight: bold;
                                color: White;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False"
                    EmptyDataText="No Record(s) Found" GridLines="None" OnRowDataBound="GridView1_RowDataBound"
                    ShowFooter="true" FooterStyle-Font-Bold="true">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl#"></asp:TemplateField>
                        <asp:BoundField DataField="strComapnyName" HeaderText="Company Name" NullDisplayText="--"
                            HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="strDistrictName" HeaderText="District Name" NullDisplayText="--"
                            HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="intDirectEmployee" HeaderText="Direct Employees" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="intContractualEmployee" HeaderText="Indirect Employees" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="intEmployeement" HeaderText="Employees" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="intExhistingemployee" HeaderText="Existing Employees" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                     <PagerStyle CssClass="pagination-grid noprint" />
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkExport" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
