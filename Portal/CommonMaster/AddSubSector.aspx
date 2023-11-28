<%--'*******************************************************************************************************************
' File Name         : AddSubSector.aspx
' Description       : Add Sub-Sector details
' Created by        : AMit Sahoo
' Created On        : 11 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="AddSubSector.aspx.cs" Inherits="Master_AddSubSector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Sub-Sector</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Industry Sub-Sector</a></li><li><a>Add Sub-Sector</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidrag">
                        <div class="panel-heading">
                           <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddSubSector.aspx"> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewSubSector.aspx"> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>
                           
                        </div>

                        <div class="panel-body">
                              <div class="form-group">
                                  <div class="row">

                                  <div class="col-sm-12">
                                <label>Sector </label><span class="text-red">*</span>
                                  <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control" Width="350px">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Aluminium Industry" Value="1" />
                                            <asp:ListItem Text="Electrical Equipment" Value="2" />
                                            <asp:ListItem Text="Computer, Electronic and Optical Products" Value="3" />
                                        </asp:DropDownList>
                               </div>
                               <div class="col-sm-12">
                                 <label>Sub-Sector Name</label><span class="text-red">*</span>
                                 <asp:TextBox ID="txtSectorName" CssClass="form-control" runat="server" 
                                       Width="350px"></asp:TextBox>
                               </div>
                               <div class="col-sm-12">
                                 <label>Sub-Sector Code</label><span class="text-red">*</span>
                                 <asp:TextBox ID="txtSectorCode" CssClass="form-control" runat="server" 
                                       Width="350px"></asp:TextBox>
                               </div>
                                <div class="col-sm-12">
                                <label>Sub-Sector Description</label>
                                 <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" 
                                        TextMode="MultiLine" Width="350px"></asp:TextBox>
                               </div>
                              
                               </div>
                              </div>

                             
                              <div class="reset-button">
                              <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass=" btn btn-success"
                                        Width="80" OnClick="btnSubmit_Click" OnClientClick="return Validate();" />
                                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-warning" onclick="btnCancel_Click" 
                                      ></asp:Button>
                                 
                              </div>
                           
                        </div>
                     </div>
                  </div>
               </div>
            </section>
        <!-- /.content -->
    </div>
</asp:Content>

