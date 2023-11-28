<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CICGStatusGrid.aspx.cs" Inherits="Portal_Dashboard_CICGStatusGrid"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <%--<div class="tab-content clearfix">--%>
    <%--<div class="tab-pane active" id="1a">--%>
    <div class="form-sec">
        <%--<div class="form-body minheight350">--%>
        <div align="right" class="noprint">
            <asp:LinkButton ID="lbtnAll" Visible="false" CssClass="all" runat="server" Text="All"
                OnClick="lbtnAll_Click" />
            &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
            <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
            <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
            <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                <i class="fa fa-print"></i></a>
        </div>
        <div style="clear: both;">
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvCICGStatus" runat="server" CssClass="table table-bordered" AllowPaging="true"
                PageSize="50" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                GridLines="None" OnPageIndexChanging="gvCICGStatus_PageIndexChanging" OnRowDataBound="gvCICGStatus_RowDataBound">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Block" HeaderStyle-Width="100px">
                        <%--1--%>
                        <ItemTemplate>
                            <asp:Label ID="lblBlock" runat="server" Text='<%# Eval("Block") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="100px">
                        <%--2--%>
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CICGDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="District" HeaderStyle-Width="100px">
                        <%--3--%>
                        <ItemTemplate>
                            <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("Distict") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EndDate" HeaderStyle-Width="100px">
                        <%--4--%>
                        <ItemTemplate>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Industry Name" HeaderStyle-Width="100px">
                        <%--5--%>
                        <ItemTemplate>
                            <asp:Label ID="lblIndustryName" runat="server" Text='<%# Eval("IndustryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inspecting Dept" HeaderStyle-Width="100px">
                        <%--6--%>
                        <ItemTemplate>
                            <asp:Label ID="lblInspectingDept" runat="server" Text='<%# Eval("InspectingDept") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inspection Date" HeaderStyle-Width="100px">
                        <%--7--%>
                        <ItemTemplate>
                            <asp:Label ID="lblInspectionDate" runat="server" Text='<%# Eval("InspectionDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inspector Name" HeaderStyle-Width="100px">
                        <%--8--%>
                        <ItemTemplate>
                            <asp:Label ID="lblInspectorName" runat="server" Text='<%# Eval("InspectorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inspector Remark" HeaderStyle-Width="100px">
                        <%--9--%>
                        <ItemTemplate>
                            <asp:Label ID="lblInspectorRemark" runat="server" Text='<%# Eval("InspectorRemark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rescheduled Date" HeaderStyle-Width="100px">
                        <%--10--%>
                        <ItemTemplate>
                            <asp:Label ID="lblRescheduledDate" runat="server" Text='<%# Eval("RescheduledDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date" HeaderStyle-Width="100px">
                        <%--11--%>
                        <ItemTemplate>
                            <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate") %>'></asp:Label>
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
                EmptyDataText="No Record(s) Found" CellPadding="4" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Block">
                        <%--1--%>
                        <ItemTemplate>
                            <asp:Label ID="lblBlock" runat="server" Text='<%# Eval("Block") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <%--2--%>
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CICGDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="District">
                        <%--3--%>
                        <ItemTemplate>
                            <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("Distict") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date">
                        <%--4--%>
                        <ItemTemplate>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Industry Name">
                        <%--5--%>
                        <ItemTemplate>
                            <asp:Label ID="lblIndustryName" runat="server" Text='<%# Eval("IndustryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inspecting Dept">
                        <%--6--%>
                        <ItemTemplate>
                            <asp:Label ID="lblInspectingDept" runat="server" Text='<%# Eval("InspectingDept") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inspection Date">
                        <%--7--%>
                        <ItemTemplate>
                            <asp:Label ID="lblInspectionDate" runat="server" Text='<%# Eval("InspectionDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inspector Name">
                        <%--8--%>
                        <ItemTemplate>
                            <asp:Label ID="lblInspectorName" runat="server" Text='<%# Eval("InspectorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inspector Remark">
                        <%--9--%>
                        <ItemTemplate>
                            <asp:Label ID="lblInspectorRemark" runat="server" Text='<%# Eval("InspectorRemark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rescheduled Date">
                        <%--10--%>
                        <ItemTemplate>
                            <asp:Label ID="lblRescheduledDate" runat="server" Text='<%# Eval("RescheduledDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date">
                        <%--11--%>
                        <ItemTemplate>
                            <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-grid noprint" HorizontalAlign="Right" />
            </asp:GridView>
        </div>
        <%-- </div>
            </div>
        </div>--%>
    </div>
    </form>
</body>
</html>
