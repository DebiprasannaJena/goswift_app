<%--'*******************************************************************************************************************
' File Name         : AddSector.aspx
' Description       : Add Sector details
' Created by        : AMit Sahoo
' Created On        : 11 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="AddSector.aspx.cs" Inherits="Master_AddSector" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                  <h1>Manage Sector</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Industry Sector</a></li><li><a>Add Sector</a></li></ul>
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
                              <a class="btn btn-add " href="AddSector.aspx"> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewSector.aspx"> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>
                           
                        </div>
                        
                        <div class="panel-body">
                              <div class="form-group">
                                  <div class="row">

                               <div class="col-sm-4">
                                 <label>Sector Name</label><span class="text-red">*</span>
                                 <asp:TextBox ID="txtSectorName" CssClass="form-control" runat="server" 
                                       Width="350px"></asp:TextBox>
                               </div>
                               <div class="col-sm-4">
                                 <label>Sector Code</label><span class="text-red">*</span>
                                 <asp:TextBox ID="txtSectorCode" CssClass="form-control" runat="server" 
                                       Width="350px"></asp:TextBox>
                               </div>
                                <div class="col-sm-4">
                                <label>Sector Description</label>
                                 <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" 
                                        TextMode="MultiLine" Width="350px"></asp:TextBox>
                               </div>
                              
                               </div>
                              </div>
                              <%--<section class ="content"--%>
                        <div class ="row">
                        <div class="col-sm-12">
                        <div class="panel panel-bd lobidrag">
                             <div class ="panel-body">
                                <div class ="form-group">
                                    <div class ="row">
                                         <div class="col-sm-4">
                                          <label>Is a priority Sector ?</label><span class="text-red">*</span>
                                          <asp:CheckBox ID="chkSector" CssClass="form-control" runat="server" ></asp:CheckBox>
                               </div>
                                    <%--</div>
                                </div>
                                <div class ="form-group">
                                    <div class ="row">--%>
                                         <div class="col-sm-4">
                                          <label>Policy References</label><span class="text-red">*</span>
                                          <asp:DropDownList ID="ddlPolicyReferences" runat="server" CssClass="form-control">
                                           <asp:ListItem Value="0">-Select-</asp:ListItem>
                                          </asp:DropDownList>
                                    </div>
                                   <%-- </div>
                                </div>
                                 <div class ="form-group">
                                    <div class ="row">--%>
                                         <div class="col-sm-3">
                                          <label>References policy effective Date</label><span class="text-red">*</span>
                                           <asp:TextBox ID="txtPolicydate" CssClass="form-control" runat="server"
                                         Width="350px"></asp:TextBox>
                                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPolicydate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                        </div>
                                 </div>
                                </div>
                             </div>
                             </div>
                             </div>
                             </div>
                           <%--  </section>--%>
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
