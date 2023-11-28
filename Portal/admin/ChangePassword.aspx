<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Portal_changepassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-lock"></i>
               </div>
               <div class="header-title">
                  <h1>Change Password</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Change Password</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidrag">
                        <div class="panel-heading">
                          
                           
                        </div>

                        <div class="panel-body">

                              <div class="form-group">
                              <div class="row">
                              <label class="col-sm-2">Old Password</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                               <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>
                               <div class="form-group">
                              <div class="row">
                              <label class="col-sm-2">New Password</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                               <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>
                               <div class="form-group">
                              <div class="row">
                              <label class="col-sm-2">Confirm Password</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                               <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>
                                      
    <br/>
<div style="display:inline" class="nav">	
</div>
   <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return lIEachData();" 
                                  CssClass="btn btn-success" />   
   <asp:Button ID="btncancel" runat="server" Text="Cancel"  CssClass="btn btn-warning" 
                                 />
                        </div>
                        </div>
                        </div>
                        </div>
                        </section>
    </div>
</asp:Content>

