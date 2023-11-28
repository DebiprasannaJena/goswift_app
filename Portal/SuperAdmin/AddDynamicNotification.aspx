<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="AddDynamicNotification.aspx.cs" Inherits="Portal_SuperAdmin_AddDynamicNotification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div>
             <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-file-text-o"></i>
               </div>
               <div class="header-title">
                  <h1>Dynamic Notification Page</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Dynamic Notification Page</a></li><li><a>Add Page</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidrag">
                        <div class="panel-heading">
                           <%--<div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddPlink.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> ""> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>--%>
                          <%--  <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewPlink.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> ""> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>--%>
                           
                        </div>
                        
                <ContentTemplate>
                        <div class="panel-body">

                 <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Notification</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                     <asp:TextBox ID="TxtNotification" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>  
                                  <%--<span class="mandetory">*</span>--%>
                                 </div>
                                 </div>
                              </div>                              

                               <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Default Page</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:RadioButtonList ID="rdnDefaultpage" runat="server" RepeatDirection="Horizontal">
                                       <asp:ListItem Selected="True" Value="0">Yes</asp:ListItem>
                                        <asp:ListItem Value="1">No</asp:ListItem>
                                       </asp:RadioButtonList>
                                  <%--<span class="mandetory">*</span>--%>
                                 </div>
                                 </div>
                              </div>  
                                  <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Login Page</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:RadioButtonList ID="rdnLoginPage" runat="server" RepeatDirection="Horizontal">
                                       <asp:ListItem Selected="True" Value="0">Yes</asp:ListItem>
                                        <asp:ListItem Value="1">No</asp:ListItem>
                                       </asp:RadioButtonList>
                                
                                 </div>
                                 </div>
                              </div>
                                <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Industrial Page</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:RadioButtonList ID="rdnIndustrialPage" runat="server" RepeatDirection="Horizontal">
                                       <asp:ListItem Selected="True" Value="0">Yes</asp:ListItem>
                                        <asp:ListItem Value="1">No</asp:ListItem>
                                       </asp:RadioButtonList>
                                 <%-- <span class="mandetory">*</span>--%>
                                 </div>
                                 </div>
                              </div> 
                            
                                            <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">NonIndustrial Page</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:RadioButtonList ID="rdnNonIndustrialPage" runat="server" RepeatDirection="Horizontal">
                                       <asp:ListItem Selected="True" Value="0">Yes</asp:ListItem>
                                        <asp:ListItem Value="1">No</asp:ListItem>
                                       </asp:RadioButtonList>
                                  <%--<span class="mandetory">*</span>--%>
                                 </div>
                                 </div>
                              </div>            
    <br/>
<div style="display:inline" class="nav">	
</div>
   <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-success" />   
   <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"  CssClass="btn btn-warning" 
                                  />
                                  <asp:HiddenField ID="hdnLastPageName" runat="server" />
                        </div>
                    <asp:GridView ID="GridView1" runat="server"></asp:GridView>  
                        </ContentTemplate>
                        
                        </div>
                        </div>
                        </div>
                        </section>
        </div>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>--%>

