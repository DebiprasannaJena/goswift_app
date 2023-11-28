<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="ViewGlink.aspx.cs" Inherits="Portal_CMS_ViewGlink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Page</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Manage Page</a></li><li><a>View Page</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                           <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddGlink.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> "> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewGlink.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> "> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>
                           
                        </div>
                        <div class="panel-body">               
                           <div class="search-sec">
                        <div class="form-group">
                        <div class="row">
                       <label class="col-md-2 col-sm-3">Global Link Name</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>                        
                                    <asp:TextBox ID="txtGlink" CssClass="form-control"  Onkeypress="return inputLimiter(event,'PageName')" runat="server"></asp:TextBox>              
                          </div>
                          <div class="col-md-2 col-sm-2">
                              <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success"
                                                    OnClick="btnSearch_Click" />
                          </div>
                        </div>


                        </div>
                         
                        </div>
                            <div class="table-responsive">
                                <div align="right" style="margin-bottom:10px;"> 
                                <asp:LinkButton ID="lbtnAll" Visible="false" runat="server" Text="ALL" OnClick="lbtnAll_Click"></asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:Label ID="lblPaging" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                </div>
                                <asp:GridView ID="GrdViewData" CssClass="table table-striped table-bordered datatable dataTable no-footer" runat="server" AutoGenerateColumns="false" 
                                DataKeyNames="Glinkid" onrowdatabound="GrdViewData_RowDataBound" 
                                onrowcommand="GrdViewData_RowCommand" 
                                onrowdeleting="GrdViewData_RowDeleting" 
                                onpageindexchanging="GrdViewData_PageIndexChanging" >
                                <Columns>
                                <asp:BoundField  HeaderText=" Sl#."   />
                             
                                <asp:BoundField HeaderText="Global Link Name" DataField="Glink" />
                                <asp:BoundField HeaderText="Window Type" DataField="viewWindowType" />
                                <asp:BoundField HeaderText="Page Type" DataField="viewPageType" />
                                <asp:BoundField HeaderText="Page Name" DataField="pagename" />
                                <asp:BoundField HeaderText="Modal Id" DataField="vchModalId" />
                                
                                <asp:TemplateField HeaderText="Edit/Delete">
                                <ItemTemplate>
                                <asp:HyperLink ID="hypEdit" runat="server" ToolTip="Edit" CssClass="btn btn-info" Text ="Edit">Edit</asp:HyperLink>
                                <asp:LinkButton ID="lbtndelete" class="btn btn-danger"  runat="server" Text="Delete" CommandName="delete">Delete</asp:LinkButton>
                                </ItemTemplate>
                                 <ItemStyle Width="15%" />
                                </asp:TemplateField>
                                </Columns>
                                </asp:GridView>
                                           
                                         </div>             
                           </div>                       
                     </div>
                  </div>
   
    </div> </section>
    <!-- /.content -->
    </div> 
</asp:Content>

