<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Application.master" CodeFile="MISIssuePendingDetails.aspx.cs" Inherits="Portal_HelpDesk_MISIssuePendingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script src="../js/jquery.min.js" type="text/javascript"></script>
    
      <script type="text/javascript" language="javascript">
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
                           
                            <div class="table-responsive">
                                <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server"
                                    AutoGenerateColumns="False" AllowPaging="true"
                                    Width="100%" EmptyDataText="No Record(s) Found..." >
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
                                        <asp:BoundField DataField="CategoryName" HeaderText="Category"  ItemStyle-HorizontalAlign="Left" />
                                     <asp:BoundField DataField="vch_SubCategoryName" HeaderText="Sub Category"  ItemStyle-HorizontalAlign="Left" />
                                   <asp:BoundField DataField="dtmCreatedOn" HeaderText="Request Date"  ItemStyle-HorizontalAlign="Left" />
                                   <asp:BoundField DataField="vch_UserName" HeaderText="Contact Person"  ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="vch_IssueDetails" HeaderText="Request Details"  ItemStyle-HorizontalAlign="Left" />
                                    <%--<asp:BoundField DataField="strRemark" HeaderText="Remarks"  ItemStyle-HorizontalAlign="Left" />--%>
                                    
                                    </Columns>
                                </asp:GridView>
                               <asp:GridView ID="GridView1" class="table table-bordered table-hover" runat="server"
                                    AutoGenerateColumns="False" AllowPaging="true"
                                    Width="100%" EmptyDataText="No Record(s) Found..." >
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Sl No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsl" runat="server" Text='<%#(GridView1.PageIndex * GridView1.PageSize) + (GridView1.Rows.Count + 1)%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CategoryName" HeaderText="Category"  ItemStyle-HorizontalAlign="Left" />
                                     <asp:BoundField DataField="vch_SubCategoryName" HeaderText="Sub Category"  ItemStyle-HorizontalAlign="Left" />
                                  
                                   <asp:BoundField DataField="vch_UserName" HeaderText="Contact Person"  ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="vch_IssueDetails" HeaderText="Request Details"  ItemStyle-HorizontalAlign="Left" />
                                    <%--<asp:BoundField DataField="strRemark" HeaderText="Remarks"  ItemStyle-HorizontalAlign="Left" />--%>
                                     <asp:BoundField DataField="dtmCreatedOn" HeaderText="Request Date"  ItemStyle-HorizontalAlign="Left" />
                                      <asp:BoundField DataField="dtmUpdatedOn" HeaderText="Resolution Date"  ItemStyle-HorizontalAlign="Left" />
                                      <asp:BoundField DataField="dtDiffrence" HeaderText="Hour of Resolution"  ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    
             
                  </div>
               </div>
                
            </section>
        <!-- /.content -->
    </div>
   
   
</asp:Content>
