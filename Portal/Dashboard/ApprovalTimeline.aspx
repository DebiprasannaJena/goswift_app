<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalTimeline.aspx.cs"
    Inherits="Portal_Dashboard_ApprovalTimeline" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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
            <div align="right " class="noprint">
                <asp:LinkButton ID="lbtnAll" CssClass="all" runat="server" Text="All"
                    OnClick="lbtnAll_Click" />
                &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
            <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
                <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>
                &nbsp;&nbsp;&nbsp; <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                    <i class="fa fa-print"></i></a>
            </div>
            <div class="table-responsive" id="viewTable1" runat="server">
                <asp:GridView ID="gvService" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" Width="100%" EmptyDataText="No Record(s) Found..." PageSize="50"
                    OnRowDataBound="gvService_RowDataBound" OnPageIndexChanging="gvService_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl#"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Application No">
                            <ItemTemplate>
                                <a href="../../ParticularApplicationDetails.aspx?ApplicationNo=<%#Eval("UniqueKey")%>&ServiceId=<%#Eval("intServiceId")%>" target="_blank">
                                    <asp:Label ID="lblUniq" runat="server" Text='<%# Eval("UniqueKey") %>'></asp:Label></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service Name">
                            <ItemTemplate>
                                <asp:Label ID="Label343" runat="server" Text='<%# Eval("strServiceName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Label ID="Label367" runat="server" Text='<%# Eval("VCH_DEPT_NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Name">
                            <ItemTemplate>
                                <asp:Label ID="Label387" runat="server" Text='<%# Eval("strComapnyName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Applied Date">
                            <ItemTemplate>
                                <asp:Label ID="Label38723" runat="server" Text='<%# Eval("strDistApproved") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ORTPSA Timeline">
                            <ItemTemplate>
                                <asp:Label ID="Label38756" runat="server" Text='<%# Eval("ApplnLandORTPS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="District">
                            <ItemTemplate>
                                <asp:Label ID="gfgg" runat="server" Text='<%# Eval("strDistrictName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action To Be Taken By">
                            <ItemTemplate>
                                <asp:Label ID="jjjj" runat="server" Text='<%# Eval("InvestorName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Count" ItemStyle-HorizontalAlign="Right" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Label390" runat="server" Text='<%# Eval("intDaysPass") %>'></asp:Label>
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
                <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false"
                    Width="100%" EmptyDataText="No Record(s) Found..." OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl#"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Application No">
                            <ItemTemplate>
                                <asp:Label ID="lblUniq" runat="server" Text='<%# Eval("UniqueKey") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service Name">
                            <ItemTemplate>
                                <asp:Label ID="Label343" runat="server" Text='<%# Eval("strServiceName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Label ID="Label367" runat="server" Text='<%# Eval("VCH_DEPT_NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Name">
                            <ItemTemplate>
                                <asp:Label ID="Label387" runat="server" Text='<%# Eval("strComapnyName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Applied Date">
                            <ItemTemplate>
                                <asp:Label ID="Label38723" runat="server" Text='<%# Eval("strDistApproved") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ORTPSA Timeline">
                            <ItemTemplate>
                                <asp:Label ID="Label38756" runat="server" Text='<%# Eval("ApplnLandORTPS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="District">
                            <ItemTemplate>
                                <asp:Label ID="gfgg" runat="server" Text='<%# Eval("strDistrictName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action To Be Taken By">
                            <ItemTemplate>
                                <asp:Label ID="jjjj" runat="server" Text='<%# Eval("InvestorName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Count" ItemStyle-HorizontalAlign="Right" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Label390" runat="server" Text='<%# Eval("intDaysPass") %>'></asp:Label>
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
