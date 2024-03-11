<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoGreenChildDetailsRpt.aspx.cs" Inherits="Portal_MISReport_GoGreenChildDetailsRpt" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
      <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
         $(window).load(function () {
             $("#PrintBtn").click(function () {
                 window.print();
             });

         });

     </script>
    <style>
        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th
        {
            padding: 4px;
            font-size: 13px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="form-group NOPRINT" id="divExport" runat="server" visible="true" align="right">
        <asp:LinkButton ID="LinkButton1" OnClick="lnkPdf_Click" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF"
            ></asp:LinkButton>
        <a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
            <asp:LinkButton ID="LinkButton2" OnClick="lnkExport_Click" CssClass="back" runat="server" title="Export to Excel"
                ><i class="fa fa-file-excel-o"></i></asp:LinkButton>
        </a><a class="PrintBtn" id="PrintBtn" data-tooltip="Print" data-toggle="tooltip"
            data-title="Print"><i class="panel-control-icon fa fa-print"></i></a>
    </div>
    <div class="table-responsive">
        <div style="text-align: right; width: 100%; text-align: right;" class="noprint pagelist">
            <asp:Label ID="lblDetails" runat="server"></asp:Label>
            <asp:DropDownList ID="ddlNoOfRec" OnSelectedIndexChanged="ddlNoOfRec_SelectedIndexChanged" ToolTip="Page Size" Width="80px" runat="server" AutoPostBack="True" 
                CssClass="form-control" Style="display: inline">
            </asp:DropDownList>
        </div>
        <asp:Label ID="lblSearchDetails" runat="server"></asp:Label>
        <br /><br />
        <asp:GridView ID="GrdDetilsChildRpt"  runat="server" OnRowDataBound="GrdDetilsChildRpt_RowDataBound" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
               CssClass="table table-bordered table-hover" ShowFooter="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsl" runat="server" Text='<%#(GrdDetilsChildRpt.PageIndex * GrdDetilsChildRpt.PageSize) + (GrdDetilsChildRpt.Rows.Count + 1)%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Application No">
                                            <ItemTemplate>
                                                <asp:Label ID="LblApplicationNo" runat="server" Text='<%# Eval("Application_No") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="LblCompanyName" runat="server" Text='<%# Eval("Developer_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project">
                                            <ItemTemplate>
                                                <asp:Label ID="Lblproject" runat="server" Text='<%# Eval("project") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Location">
                                            <ItemTemplate>
                                                <asp:Label ID="Lblproject_Location" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="District">
                                            <ItemTemplate>
                                                <asp:Label ID="LblDistrict" runat="server" Text='<%# Eval("District") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Investment">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbltotal_Investment" runat="server" Text='<%# Eval("Total_Investment") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Employment">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbltotal_Employment" runat="server" Text='<%# Eval("Total_Employment") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                     
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="LblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="11%" />
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Apply Date">
                                            <ItemTemplate>
                                              
                                                 <asp:Label ID="LblCreatedOn" runat="server" Text='<%# Convert.ToDateTime(Eval("CreatedOn")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination-grid no-print" />
                                </asp:GridView>
        <div style="float: right;" class="noPrint" id="divPaging" runat="server">
            <asp:Repeater ID="rptPager" runat="server">
                <ItemTemplate>
                    <ul class="pagination">
                        <li class='<%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %> '>
                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                              OnClick="Page_Changed"    OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                        </li>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
            <%--   <asp:HiddenField ID="HiddenField1" runat="server" Value="Blank Value" />--%>
            <asp:HiddenField ID="hdnPgindex" runat="server" Value="Blank Value" />
        </div>
    </div>
    </form>
</body>
</html>

