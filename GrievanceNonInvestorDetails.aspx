<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrievanceNonInvestorDetails.aspx.cs" Inherits="GrievanceNonInvestorDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/css/custom.css" rel="stylesheet" type="text/css" />
</head>
<body class="bg-white" style="background: none;">
    <form id="form1" runat="server">
    <div align="right">
        <asp:LinkButton ID="lbtnAll" Visible="false" CssClass="all" runat="server" Text="All"
            OnClick="lbtnAll_Click" />
        &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
    </div>
    <asp:GridView ID="grdGRVDtl" runat="server" class="table table-bordered table-hover"
        AllowPaging="true" PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
        OnPageIndexChanging="grdGRVDtl_PageIndexChanging" Width="100%" DataKeyNames="vchGrivId"
        OnRowDataBound="grdGRVDtl_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Sl#">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Grievance Id">
                <ItemTemplate>
                    <asp:HyperLink ID="HypLnk_Griv_Id" ForeColor="Blue" Text='<%#Eval("vchGrivId") %>'
                        runat="server" ToolTip="Click here to view grievance details" Target="_blank"></asp:HyperLink>
                    <%--<asp:Label ID="lblGrievno" runat="server" Text='<%# Eval("vchGrivId") %>'></asp:Label>--%>
                </ItemTemplate>
            </asp:TemplateField>
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
            <asp:TemplateField HeaderText="Apply Date">
                <ItemTemplate>
                    <asp:Label ID="LblApplyDate" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("vchStatusName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action Date">
                <ItemTemplate>
                    <asp:Label ID="LblActionDate" runat="server" Text='<%# Eval("dtmActionDate" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <%-- <asp:TemplateField HeaderText="Applicant Name">
                <ItemTemplate>
                    <asp:Label ID="lblapplicant" runat="server" Text='<%# Eval("vchApplicantName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <%-- <asp:TemplateField HeaderText="Designation">
                <ItemTemplate>
                    <asp:Label ID="lbldesig" runat="server" Text='<%# Eval("vchDesignation") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <%-- <asp:TemplateField HeaderText="Mobile Number">
                <ItemTemplate>
                    <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("vchMobileNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <%-- <asp:TemplateField HeaderText="Email Id">
                <ItemTemplate>
                    <asp:Label ID="lblemail" runat="server" Text='<%# Eval("vchEmail") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
        <PagerStyle CssClass="pagination-grid no-print" />
    </asp:GridView>
    </form>
</body>
</html>
