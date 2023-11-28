<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrievanceGridStatus.aspx.cs" Inherits="Portal_Dashboard_GrievanceGridStatus"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(window).load(function () {
            $('.printbtn').click(function () {
                window.print();
            });
        });
    </script>
</head>
<body class="bg-white" style="background: none;">
    <form id="form1" runat="server">
        <div align="right" class="noprint">
            <asp:LinkButton ID="LnkBtnAll" Visible="false" CssClass="all" runat="server" Text="All"
                OnClick="LnkBtnAll_Click" />
            &nbsp;&nbsp;<asp:Label ID="LblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
        <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
            <asp:LinkButton ID="LnkBtnExcelExport" CssClass="back" runat="server" Text="Export" OnClick="LnkBtnExcelExport_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
            <i class="fa fa-print"></i></a>
        </div>
        <div style="clear: both;">
        </div>

        <div class="table-responsive" id="viewTable" runat="server">
         
            <asp:GridView ID="GrdGrivDetails" runat="server" CssClass="table table-bordered" AllowPaging="true"
                PageSize="50" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                OnPageIndexChanging="GrdGrivDetails_PageIndexChanging" CellPadding="4" Width="100%"
                OnRowDataBound="GrdGrivDetails_RowDataBound" DataKeyNames="vchGrivId">
                <Columns>
                    <asp:TemplateField HeaderText="Sl#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grievance Id">
                        <ItemTemplate>
                            <asp:HyperLink ID="HypLnk_Griv_Id" ForeColor="Blue" Text='<%#Eval("vchGrivId") %>'
                                runat="server" ToolTip="Click here to view grievance details" Target="_blank"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Applicant Name">
                        <ItemTemplate>
                            <asp:Label ID="lblapplicant" runat="server" Text='<%# Eval("vchApplicantName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Grievance Type">
                        <ItemTemplate>
                            <asp:Label ID="lblGtype" runat="server" Text='<%# Eval("vchGrivType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grievance Sub Type">
                        <ItemTemplate>
                            <asp:Label ID="lblGstype" runat="server" Text='<%# Eval("vchGrivSubType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate>
                            <asp:Label ID="lbldesig" runat="server" Text='<%# Eval("vchDesignation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile Number">
                        <ItemTemplate>
                            <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("vchMobileNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email Id">
                        <ItemTemplate>
                            <asp:Label ID="lblemail" runat="server" Text='<%# Eval("vchEmail") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Apply Date">
                        <ItemTemplate>
                            <asp:Label ID="LblApplyDate" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="13%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("vchStatusName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="9%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action Date">
                        <ItemTemplate>
                            <asp:Label ID="LblActionDate" runat="server" Text='<%# Eval("dtmActionDate" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="13%" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-grid noprint" />
            </asp:GridView>
        </div>

        <div class="table-responsive" id="DivForExcel" runat="server" style="display: none;">
            <table runat="server" id="Table1" width="100%">
                <tr>
                    <td style="background-color: green; border: 1px solid black; font-weight: 800;" align="center">
                        <asp:Label ID="LblExcelCaption" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GrdExcel" runat="server" CssClass="table table-bordered"
                            AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                            CellPadding="4" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grievance Id">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HypLnk_Griv_Id" ForeColor="Blue" Text='<%#Eval("vchGrivId") %>'
                                            runat="server" ToolTip="Click here to view grievance details" Target="_blank"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Applicant Name">
                        <ItemTemplate>
                            <asp:Label ID="lblapplicant" runat="server" Text='<%# Eval("vchApplicantName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Grievance Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGtype" runat="server" Text='<%# Eval("vchGrivType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grievance Sub Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGstype" runat="server" Text='<%# Eval("vchGrivSubType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate>
                            <asp:Label ID="lbldesig" runat="server" Text='<%# Eval("vchDesignation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile Number">
                        <ItemTemplate>
                            <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("vchMobileNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email Id">
                        <ItemTemplate>
                            <asp:Label ID="lblemail" runat="server" Text='<%# Eval("vchEmail") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Apply Date">
                                    <ItemTemplate>
                                        <asp:Label ID="LblApplyDate" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="13%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("vchStatusName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="9%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action Date">
                                    <ItemTemplate>
                                        <asp:Label ID="LblActionDate" runat="server" Text='<%# Eval("dtmActionDate" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="13%" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-grid noprint" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
