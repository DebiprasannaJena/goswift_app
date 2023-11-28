<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewContentNews.aspx.cs"
    Inherits="Portal_ViewContentNews" MasterPageFile="~/MasterPage/Application.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage News & Events</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>News & Events</a></li><li><a>View News & Events</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                          <%-- <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddCMS.aspx"> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>--%>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewContentNews.aspx"> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>
                           
                        </div>
                         
                        <div class="panel-body">  
                        <div class="search-sec">
                        <div class="form-group">
                        <div class="row">
                       <label class="col-md-2 col-sm-3">Select Category</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>                        
                           <asp:DropDownList ID="ddltype" CssClass="form-control" runat="server" 
                                 onselectedindexchanged="ddltype_SelectedIndexChanged" AutoPostBack="true">
                           <asp:ListItem>---Select---</asp:ListItem>
                             <asp:ListItem Value="News">News</asp:ListItem>
                             <asp:ListItem Value="Annoncement"  >Annoncement</asp:ListItem>                            
                             <asp:ListItem Value="Notification" >Notification</asp:ListItem>
                           </asp:DropDownList>                        
                          </div>
                          
                        </div>
                        </div>
                         
                        </div>
                        
                                     
                       <span style="text-align: right; float: right; padding-right: 5px; height: 21px;">
                            <asp:LinkButton ID="lbtnAll" runat="server" CssClass="more" Text="All" onclick="lbtnAll_Click" 
                                   ></asp:LinkButton>
                            <asp:Label ID="lblPaging" runat="server" Text=""></asp:Label>
                            </span>
                                       <asp:GridView ID="grdContent" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" CellPadding="4"
                                           GridLines="None" DataKeyNames="INT_ID"
                                           PageSize="10" AllowPaging="true"   onpageindexchanging="grdContent_PageIndexChanging"> 
                                 
                                            
                                            <Columns>
                                              <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-CssClass="noPrint" ItemStyle-CssClass="noPrint" HeaderStyle-Width="20px">
                                         <%--<HeaderTemplate>
                                            <input name="cbAll" value="cbAll" type="checkbox" onclick="SelectAll(cbAll,'grdContent','ContentPlaceHolder1')" />
                                        </HeaderTemplate>--%>
                                      <%--  <ItemTemplate>
                                            <asp:CheckBox ID="chkItem" runat="server" onclick="return deSelectHeader(cbAll,'grdContent','ContentPlaceHolder1')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                                   --%><asp:TemplateField HeaderText="Sl.No." ItemStyle-Width="20">
                                                         <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                <asp:BoundField HeaderText="Type" DataField="VCH_TYPE" />                                            
                                                <asp:BoundField HeaderText="Heading" DataField="VCH_HEADING" />
                                             <%--   <asp:BoundField HeaderText="Image Name" DataField="VCH_IMAGE" />        --%>                                           
                                           <%--     <asp:BoundField HeaderText="Content" DataField="VCH_CONTENT"/> --%>
                                                <asp:BoundField HeaderText="Created On" DataField="DTM_CREATEDON"/> 
                                                  <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                <asp:HyperLink ID="HypEdit" runat="server" CssClass="btn btn-primary btn-sm fa fa-edit" ToolTip="Click To Edit" 
                                                                NavigateUrl='<%# string.Format("~/Portal/CMS/AddContentNews.aspx?Id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}",
                                                                 Eval("INT_ID").ToString(), Request.QueryString["linkn"].ToString(), Request.QueryString["linkm"].ToString(), Request.QueryString["btn"].ToString(), Request.QueryString["tab"].ToString()) %>'
                                                                 ></asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:TemplateField>
                                            </Columns>
                                                                                      <PagerStyle CssClass="pagination-grid no-print" HorizontalAlign="Right" />    
                                           
                                        </asp:GridView>
                                          
                                         <asp:Label ID="lblMessage" runat="server" Text="No Records found" Visible="false"></asp:Label>                 
                           </div>                       
                     </div>
                  </div>
   
    </div> </section>
        <!-- /.content -->
    </div>
</asp:Content>
