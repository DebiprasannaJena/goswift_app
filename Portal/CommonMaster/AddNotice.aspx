<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="AddNotice.aspx.cs" Inherits="Portal_CommonMaster_AddNotice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        #elements-container
        {
            text-align: center;
        }
        .CMS
        {
            float: right;
            color: #f00;
            margin-top: -30px;
            margin-right: -221px;
            font-size: 15px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<script type="text/javascript">
    $(document).ready(function () {
        $('.datePicker').datepicker({
            format: "dd-M-yyyy",
            changeMonth: true,
            changeYear: true,
            autoclose: true
        });
    });

    </script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-file-text-o"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Service Instruction </h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Manage Service Instruction Content</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidrag">
           
                           <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddNotice.aspx"> 
                              <i class="fa fa-plus"></i> Notice </a>  
                           </div>
                        </div>

                        <div class="panel-body">
                      
                              <div class="form-group">
                                  <div class="row">

                               <div class="col-sm-4">
                                 <label>Notice</label><span class="text-red">*</span>
                                 <asp:TextBox ID="txtNotice" required CssClass="form-control" runat="server" 
                                       Width="350px"></asp:TextBox>
                               </div>
                               </div> 
                               </div>
                                         <div class="form-group">
                                         <div class="row">
                               <div class="col-sm-4">
                                 <label>From Date</label><span class="text-red">*</span>
                                 <asp:TextBox ID="txtFromDate" required CssClass="form-control datePicker" runat="server" 
                                       Width="350px"></asp:TextBox>
                               </div>
                               </div>
                               </div>
                                         <div class="form-group">
                                         <div class="row">
                                <div class="col-sm-4">
                                <label>To Date</label>
                                 <asp:TextBox ID="txtTodate" required CssClass="form-control datePicker" runat="server" 
                                         Width="350px"></asp:TextBox>
                               </div>
                               </div>
                              
                               </div>
                       <div class="form-group">
                       <div class="row">
     <asp:Button ID="Button1" runat="server" Text="Save" onclick="Button1_Click"  CssClass="btn btn-success" />
                              </div>
                              </div>
                    
                                               
    <br/>

                        </div>
                        </div>
                        </div>

       </section>
    </div>
</asp:Content>
<%-- <script type="text/javascript">
     $(document).ready(function () {
         $('.datePicker').datepicker({
             format: "dd-M-yyyy",
             changeMonth: true,
             changeYear: true,
             autoclose: true
         });
     });
     var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

    </script>
   
 <div class="panel-body">
                              <div class="form-group">
                                  <div class="row">

                               <div class="col-sm-4">
                                 <label>Notice</label><span class="text-red">*</span>
                                 <asp:TextBox ID="txtNotice" required CssClass="form-control" runat="server" 
                                       Width="350px"></asp:TextBox>
                               </div>
                               <div class="col-sm-4">
                                 <label>From Date</label><span class="text-red">*</span>
                                 <asp:TextBox ID="txtFromDate" required CssClass="form-control datePicker" runat="server" 
                                       Width="350px"></asp:TextBox>
                               </div>
                                <div class="col-sm-4">
                                <label>To Date</label>
                                 <asp:TextBox ID="txtTodate" required CssClass="form-control datePicker" runat="server" 
                                         Width="350px"></asp:TextBox>
                               </div>
                              
                               </div>
                              </div>
     <asp:Button ID="Button1" runat="server" Text="Save" onclick="Button1_Click" />
                              </div>--%>



