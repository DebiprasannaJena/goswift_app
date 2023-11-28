<%@ Page Language="C#"  MasterPageFile="~/MasterPage/Application.master"  AutoEventWireup="true" CodeFile="MISReport.aspx.cs" Inherits="Portal_HelpDesk_MISReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script src="../js/jquery.min.js" type="text/javascript"></script>
    
      <script type="text/javascript" language="javascript">
          $(document).ready(function () {
              $('.datePicker').datepicker({
                  autoclose: true,
                  format: 'dd-M-yyyy'
              });
          });

        
          var config = {
              '.chosen-select': {},
              '.chosen-select-deselect': { allow_single_deselect: true },
              '.chosen-select-no-single': { disable_search_threshold: 10 },
              '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
              '.chosen-select-width': { width: "100%" }
          }
          for (var selector in config) {
              $(selector).chosen(config[selector]);
          }
    </script>



    <script type="text/javascript" language="javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    for (var selector in config) {
                        $(selector).chosen(config[selector]);
                    }

                }
            });
        };
</script>
   
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>MIS Report</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>HelpDesk</a></li><li><a>Add</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                 
                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>                        
                                                
                                          
                  <div class="col-sm-12">
                   <div class="panel panel-bd lobidisable"> 
                <div class="panel-heading">
                      
                        <div class="btn-group buttonlist" > 
                            <a class="btn btn-add " href="MISReport.aspx"> 
                            <i class="fa fa-file"></i>View</a>  
                        </div>
                           
                </div>   
                                
                    <div class="panel-body">
                           <div class="search-sec NOPRINT">
                               <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            From Date</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <div class="input-group  date datePicker" id="datePicker1">
                                                <asp:TextBox ID="txtFromdate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                        <label class="col-sm-2">
                                            To Date</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <div class="input-group  date datePicker">
                                                <asp:TextBox ID="txtTodate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                  <%--      <div class="col-sm-2">
                                            <asp:Button ID="btnShow" runat="server" Text="Search" class="btn btn-add btn-sm"
                                                OnClick="btnShow_Click"></asp:Button>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="form-group row NOPRINT">
                                   
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm"
                                            runat="server" Text="Search" onclick="btnSearch_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                        <div align="right" class="noprint">
          
                        <img src="../../images/excelIcon.png" width="18" height="18" align="absmiddle" alt="" />
                        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>
                      
                        </div>
                        <div style="clear: both;">
                        </div>
                                <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server"
                                    AutoGenerateColumns="False"    OnRowDataBound="gvService_RowDataBound" DataKeyNames="int_CategoryId,int_SubcategoryId"
                                    Width="100%" EmptyDataText="No Record(s) Found..." 
                                    onprerender="gvService_PreRender" ShowFooter="True" >
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Sl No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsl" runat="server" Text='<%#(gvService.PageIndex * gvService.PageSize) + (gvService.Rows.Count + 1)%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                     <asp:BoundField DataField="strTypeName" HeaderText="Type"  ItemStyle-HorizontalAlign="Left" />
                                     <asp:BoundField DataField="CategoryName" HeaderText="Issue Category"  ItemStyle-HorizontalAlign="Left" />
                                     <asp:BoundField DataField="vch_SubCategoryName" HeaderText="Issue Sub Category"  ItemStyle-HorizontalAlign="Left" />
                                     <asp:BoundField DataField="Total_Count" HeaderText="Number of Calls(Issue Type)" ItemStyle-HorizontalAlign="Right"  />
                                     <asp:BoundField DataField="Total_Resolved" HeaderText="Issues Resolved" ItemStyle-HorizontalAlign="Right" />
                                     <asp:TemplateField HeaderText="Issues Pending">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyCatepending" runat="server" Text='<%#Eval("Total_Pending")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="Total_IssueResolvedSLA" HeaderText="No.of Issue Resolved within SLA" ItemStyle-HorizontalAlign="Right" />
                                  
                                    <asp:TemplateField HeaderText="No.of Issue Open Past SLA">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyCate" runat="server" Text='<%#Eval("Total_IssueOpenPastSLA")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Total_IssuePastSLA" HeaderText="Issue Resolved Past SLA" ItemStyle-HorizontalAlign="Right" />--%>
                                    <asp:TemplateField HeaderText="Issue Resolved Past SLA">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyPastSLA" runat="server" Text='<%#Eval("Total_IssuePastSLA")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Avrage Hour of Resolution">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyPastSLA3" runat="server" Text='<%#Eval("Total_Resoved_Hour")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                            </div>
                             <div class="table-responsive" id="viewTable" runat="server">
                             <asp:GridView ID="GridView1" class="table table-bordered table-hover" runat="server"
                                    AutoGenerateColumns="False"    OnRowDataBound="GridView1_RowDataBound" DataKeyNames="int_CategoryId,int_SubcategoryId"
                                    Width="100%" EmptyDataText="No Record(s) Found..." 
                                    onprerender="GridView1_PreRender" ShowFooter="True" >
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Sl No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsl" runat="server" Text='<%#(gvService.PageIndex * gvService.PageSize) + (gvService.Rows.Count + 1)%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                     <asp:BoundField DataField="strTypeName" HeaderText="Type"  ItemStyle-HorizontalAlign="Left" />
                                     <asp:BoundField DataField="CategoryName" HeaderText="Issue Category"  ItemStyle-HorizontalAlign="Left" />
                                     <asp:BoundField DataField="vch_SubCategoryName" HeaderText="Issue Sub Category"  ItemStyle-HorizontalAlign="Left" />
                                     <asp:BoundField DataField="Total_Count" HeaderText="Number of Calls(Issue Type)" ItemStyle-HorizontalAlign="Right"  />
                                     <asp:BoundField DataField="Total_Resolved" HeaderText="Issues Resolved" ItemStyle-HorizontalAlign="Right" />
                                        <asp:TemplateField HeaderText="Issues Pending">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyCatepending" runat="server" Text='<%#Eval("Total_Pending")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="Total_IssueResolvedSLA" HeaderText="No.of Issue Resolved SLA" ItemStyle-HorizontalAlign="Right" />
                                      <asp:TemplateField HeaderText="No.of Issue Open Past SLA">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyCate" runat="server" Text='<%#Eval("Total_IssueOpenPastSLA")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="Total_IssuePastSLA" HeaderText="Issue Resolved Past SLA" ItemStyle-HorizontalAlign="Right" />
                                      <asp:BoundField DataField="Total_Resoved_Hour" HeaderText="Avrage Hour of Resolution" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                             </div>
                             </div>
                        </div>
                  </div>
               </div>
                
            </section>
        <!-- /.content -->
    </div>
    </div>
   
   
</asp:Content>