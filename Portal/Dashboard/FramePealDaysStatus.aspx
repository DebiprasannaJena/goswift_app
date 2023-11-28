<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FramePealDaysStatus.aspx.cs"
    Inherits="Portal_Dashboard_FramePealDaysStatus"  EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $('#printbtn').click(function () {
                window.print();

            })
        }
    </script>
</head>
<body>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="row">
                    <label class="col-sm-3">
                        Days Difference</label>
                    <div class="col-sm-4">
                        <span class="colon">:</span>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Text="--select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="0-15 days" Value="1"></asp:ListItem>
                            <asp:ListItem Text="15-30 days" Value="2"></asp:ListItem>
                            <asp:ListItem Text=">30 days" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div align="right" class="noprint">
                <asp:LinkButton Visible="false" ID="lbtnAll" CssClass="all" runat="server" Text="All"
                    OnClick="lbtnAll_Click" />
                &nbsp;&nbsp;<asp:Label ID="lblPaging" runat="server" />&nbsp;&nbsp;&nbsp;
                   <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
                <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                    <i class="fa fa-print"></i></a>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="gvPeal" runat="server" CssClass="table table-bordered" AllowPaging="true"
                    AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" GridLines="None"
                    OnPageIndexChanging="gvPeal_PageIndexChanging" OnRowDataBound="gvPeal_RowDataBound">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl#">
                            <%--<ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Name">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("strComapnyName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Days since the PEAL form was received">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("VCH_DAYS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Capital Investment(In Lakhs)">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchCapitalInvestment") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Employment">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchEmployement") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-grid noprint" HorizontalAlign="Right" />
                </asp:GridView>
            </div>
               <div class="table-responsive" id="viewTable" runat="server">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered"
                    AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" GridLines="None"
                    OnRowDataBound="GridView1_RowDataBound">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl#">
                            <%--<ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Name">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("strComapnyName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Days since the PEAL form was received">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("VCH_DAYS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Capital Investment(In Lakhs)">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchCapitalInvestment") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Employment">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchEmployement") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                    </Columns>
                   
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
