<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ChangeORPTSTimeline.aspx.cs" Inherits="Portal_Service_ChangeORPTSTimeline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage ORPTS Time</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Mange ORPTS Time</a></li><li><a>Edit ORPTS Time</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
                    <section class="content">      
                     <div class="row">
                     <div class="col-sm-12">
                          <div class="panel-body">
                          <div class="search-sec">
                          <div class="form-group row" id="save" runat="server">
                               <div class="col-sm-1">
                                  <strong>Industry Type:</strong>
                               </div> 
                              <div class="col-sm-3">
                                   <asp:DropDownList ID="ddltype" CssClass="form-control" runat="server">
                                       <asp:ListItem Value="0"><--Select--></asp:ListItem>
                                       <asp:ListItem Value="90">MAH</asp:ListItem>
                                       <asp:ListItem Value="60">2cb</asp:ListItem>
                                       <asp:ListItem Value="30">Others</asp:ListItem>
                                   </asp:DropDownList>
                              </div> 
                               <div class="col-sm-3"> 
                                   <asp:Button ID="btnchange" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnchange_Click"></asp:Button>
                               </div>
                              </div>
                               <div class="table-responsive" id="error" runat="server">
                                   <img typeof="foaf:Image" src="//assets.prestashop2.com/sites/default/files/styles/blog_750x320/public/blog/2018/02/error_404_http_code.jpg?itok=s_q4FEH7" width="750" height="320" alt="Error 404 http code Page Not Found - PrestaShop Blog" title="Error 404 http code Page Not Found - PrestaShop Blog" style="width:100%;">
                                </div>
                              </div>
                              </div>
                    
                     </div>
                     </div> 
                     </section>
        <!-- /.content -->
    </div>
</asp:Content>
