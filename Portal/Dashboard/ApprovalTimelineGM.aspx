<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalTimelineGM.aspx.cs"
    Inherits="Portal_Dashboard_ApprovalTimelineGM"  EnableEventValidation="false" %>

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
   <%-- <div class="tab-content clearfix">
        <div class="tab-pane active" id="1a">--%>
            <div class="form-sec">
             <%--   <div class="form-body minheight350">--%>
                    <div align="right" class="noprint">
                        <asp:LinkButton Visible="false" ID="lbtnAll" CssClass="all" runat="server" Text="All" />
                        &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
                          <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
                <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;
              <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                    <i class="fa fa-print"></i></a>
                    </div>
                    <div class="form-group">
                    <div class="row" id="dv1" runat="server">
                 <label class="col-sm-3">Days Difference</label>
                  <div class="col-sm-4">
                    <span class="colon">:</span>
                <asp:DropDownList CssClass="form-control" ID="ddlDays" runat="server" 
                            onselectedindexchanged="ddlDays_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="0">-Select-</asp:ListItem>
                <asp:ListItem Value="1">0-20 Days</asp:ListItem>
                <asp:ListItem Value="2">20-30 Days</asp:ListItem>
                <asp:ListItem Value="3">30-60 Days</asp:ListItem>
                <asp:ListItem Value="4">>60 Days</asp:ListItem>
                </asp:DropDownList>
                  </div>
                  </div>
                       
                    </div>
                    <div class="form-group">
                       <div class="row">
                       <div class="col-sm-12">
                        <div class="table-responsive" id="viewTable" runat="server">
                            <asp:GridView ID="gvService" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" 
                                Width="100%" EmptyDataText="No Record(s) Found..." 
                                onrowdatabound="gvService_RowDataBound">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            Sl#.
                                        </HeaderTemplate>
                                        <HeaderStyle Width="40px" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Application No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUniq" runat="server" Text='<%# Eval("UniqueKey") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Service Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label17" runat="server" Text='<%# Eval("strServiceName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label38" runat="server" Text='<%# Eval("Distirict") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label39" runat="server" Text='<%# Eval("strComapnyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Count of days since the receipt of the approval application " ItemStyle-HorizontalAlign="Right" ItemStyle-Width ="30%">
                                    
                                        <ItemTemplate>
                                            <asp:Label ID="Label390" runat="server" Text='<%# Eval("intDaysPass") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-grid noprint" />
                            </asp:GridView>
                        </div>
                       </div>
                       </div>
                    </div>
               <%-- </div>
            </div>
        </div>--%>
    </div>
    </form>
</body>
</html>
