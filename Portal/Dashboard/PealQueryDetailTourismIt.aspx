<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PealQueryDetailTourismIt.aspx.cs" Inherits="Portal_Dashboard_PealQueryDetailTourismIt" EnableEventValidation="false" %>

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
    <%--<div class="tab-content clearfix">
        <div class="tab-pane active" id="1a">--%>
            <div class="form-sec">
                <%--<div class="form-body minheight350">--%>
                    <div align="right">
                        <asp:LinkButton Visible="false" ID="lbtnAll" CssClass="all" runat="server" Text="All" />
                        &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
                          <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
                <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;
              <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                    <i class="fa fa-print"></i></a>
                    </div>
                    <div style="clear:both;"></div>
                 
                        <div class="table-responsive" id="viewTable" runat="server">
                            <asp:GridView ID="gvpeal" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false"
                                AllowPaging="false" Width="100%" EmptyDataText="No Record(s) Found..." PageSize="10">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            Sl No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Application No">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("vchProposalNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("vchCompName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Application Date" >
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("APPN_dATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="1st Query Raise Date" >
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("1stQueryDateRaise") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="2nd Query Raise Date" >
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("2ndQueryDateRaise") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="1st Query Raise Remarks" >
                                        <ItemTemplate>
                                          <asp:Label ID="Label6" runat="server" Text='<%# Eval("1stQueryRaise") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="2nd Query Raise Remarks" >
                                        <ItemTemplate>
                                           <asp:Label ID="Label7" runat="server" Text='<%# Eval("2ndQueryRaise") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="1st Query Response Date" >
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("1stQueryDateResponse") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="2nd Query Response Date" >
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("2ndQueryDateResponse") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="1st Query Response Remarks" >
                                        <ItemTemplate>
                                          <asp:Label ID="Label6" runat="server" Text='<%# Eval("1stQueryResponse") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="2nd Query Response Remarks" >
                                        <ItemTemplate>
                                           <asp:Label ID="Label7" runat="server" Text='<%# Eval("2ndQueryResponse") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Avg Day" >
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("Avg_time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-grid no-print" />
                            </asp:GridView>
                        </div>
                   
              <%--  </div>
            </div>
        </div>--%>
    </div>
    </form>
</body>
</html>
