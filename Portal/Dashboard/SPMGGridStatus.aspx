<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SPMGGridStatus.aspx.cs" Inherits="Portal_Dashboard_SPMGGridStatus"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
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
        <asp:LinkButton ID="lbtnAll" Visible="false" CssClass="all" runat="server" Text="All"
            OnClick="lbtnAll_Click" />
        &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
        <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
            <i class="fa fa-print"></i></a>
    </div>
    <asp:GridView ID="grdSPMGDtl" runat="server" CssClass="table table-bordered" AllowPaging="true"
        PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
        OnPageIndexChanging="grdSPMGDtl_PageIndexChanging" CellPadding="4" Width="100%"
        OnRowDataBound="grdSPMGDtl_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Sl#">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Project Name">
                <ItemTemplate>
                    <asp:Label ID="lblProj" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Project Department">
                <ItemTemplate>
                    <asp:Label ID="lblProj" runat="server" Text='<%# Eval("Project_Department") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type of the Issue">
                <ItemTemplate>
                    <asp:Label ID="lblIssues" runat="server" Text='<%# Eval("Type_Of_Issue") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Issue Date">
                <ItemTemplate>
                    <asp:Label ID="lblIssue_Date" runat="server" Text='<%# Eval("Issue_Date") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Issue Description">
                <ItemTemplate>
                    <asp:Label ID="lblIssue_Description" runat="server" Text='<%# Eval("Issue_Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Issue Category">
                <ItemTemplate>
                    <asp:Label ID="lblIsCat" runat="server" Text='<%# Eval("IssueCategory") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name Of Investor" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblName_Of_Investor" runat="server" Text='<%# Eval("Name_Of_Investor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Pending Department">
                <ItemTemplate>
                    <asp:Label ID="lblPending_Department" runat="server" Text='<%# Eval("Pending_Department") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Pending Department Type">
                <ItemTemplate>
                    <asp:Label ID="lblPending_Department_Type" runat="server" Text='<%# Eval("Pending_Department_Type") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Pending Days">
                <ItemTemplate>
                    <asp:Label ID="lblDays" runat="server" Text='<%# Eval("PendingDays") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-grid noprint" />
    </asp:GridView>
    <div class="table-responsive" id="viewTable" runat="server">
        <table style="width: 210px" runat="server" id="tbldv">
            <tr>
                <td style="background-color: green; border: 1px solid black" align="center">
                    <asp:Label ID="lblCaption" runat="server" Style="font-weight: bold; color: White;"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False"
            EmptyDataText="No Record(s) Found" CellPadding="4" Width="100%" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Sl#">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Project Name">
                    <ItemTemplate>
                        <asp:Label ID="lblProj" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Project Department">
                    <ItemTemplate>
                        <asp:Label ID="lblProj" runat="server" Text='<%# Eval("Project_Department") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type of the Issue">
                    <ItemTemplate>
                        <asp:Label ID="lblIssues" runat="server" Text='<%# Eval("Type_Of_Issue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue Date">
                    <ItemTemplate>
                        <asp:Label ID="lblIssue_Date" runat="server" Text='<%# Eval("Issue_Date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue Description">
                    <ItemTemplate>
                        <asp:Label ID="lblIssue_Description" runat="server" Text='<%# Eval("Issue_Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue Category">
                    <ItemTemplate>
                        <asp:Label ID="lblIsCat" runat="server" Text='<%# Eval("IssueCategory") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name Of Investor">
                    <ItemTemplate>
                        <asp:Label ID="lblName_Of_Investor" runat="server" Text='<%# Eval("Name_Of_Investor") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pending Department">
                    <ItemTemplate>
                        <asp:Label ID="lblPending_Department" runat="server" Text='<%# Eval("Pending_Department") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pending Department Type">
                    <ItemTemplate>
                        <asp:Label ID="lblPending_Department_Type" runat="server" Text='<%# Eval("Pending_Department_Type") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pending Days">
                    <ItemTemplate>
                        <asp:Label ID="lblDays" runat="server" Text='<%# Eval("PendingDays") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-grid noprint" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
