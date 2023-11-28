<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SPMGDeptwiseStatusdtls.aspx.cs"
    Inherits="Portal_Dashboard_SPMGDeptwiseStatusdtls" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
<body>
    <form id="form1" runat="server">
   <%-- <div class="tab-content clearfix">
        <div class="tab-pane active" id="1a">--%>
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
                    <div style="clear:both;"></div>
                    <div class="form-group">
                        <div class="table-responsive">
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
                                    <asp:TemplateField HeaderText="Type of the Issue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssues" runat="server" Text='<%# Eval("TypeofIssue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsCat" runat="server" Text='<%# Eval("IssueCategory") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Pending Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="server" Text='<%# Eval("PendingDays") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name Of Investor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInv" runat="server" Text='<%# Eval("InvestorName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Issue Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsdate" runat="server" Text='<%# Eval("Issue_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server" Text='<%# Eval("Issue_Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-grid noprint" />
                            </asp:GridView>
                        </div>
                        <div class="table-responsive" id="viewTable" runat="server">
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AllowPaging="true"
                                PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                                CellPadding="4" Width="100%"
                                OnRowDataBound="GridView1_RowDataBound">
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
                                    <asp:TemplateField HeaderText="Type of the Issue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssues" runat="server" Text='<%# Eval("TypeofIssue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsCat" runat="server" Text='<%# Eval("IssueCategory") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Pending Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="server" Text='<%# Eval("PendingDays") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name Of Investor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInv" runat="server" Text='<%# Eval("InvestorName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Issue Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsdate" runat="server" Text='<%# Eval("Issue_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server" Text='<%# Eval("Issue_Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                               
                            </asp:GridView>
                        </div>
                    </div>
            <%--    </div>
            </div>
        </div>--%>
    </div>
    </form>
</body>
</html>
