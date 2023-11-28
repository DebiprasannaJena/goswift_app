﻿<%--'*******************************************************************************************************************
' File Name         : ViewSector.aspx
' Description       : View/Edit/Update Sector details
' Created by        : AMit Sahoo
' Created On        : 11 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewSector.aspx.cs" Inherits="Master_ViewSector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Sector</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Industry Sector</a></li><li><a>View Sector</a></li></ul>
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
                              <a class="btn btn-add " href="AddSector.aspx"> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewSector.aspx"> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>
                           
                        </div>
                        <div class="panel-body">
                        <div class="search-sec">
                        <div class="form-group">
                        <div class="row">
                       <label class="col-md-2 col-sm-2">Sector Code</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                           <asp:DropDownList ID="ddlPolicyReferences" runat="server" CssClass="form-control">
                                           <asp:ListItem Value="0">-Select-</asp:ListItem>
                                          </asp:DropDownList>
                                          </div>
                                          
                                           <label class="col-md-3 col-sm-3"> Priority Sector</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                           <asp:CheckBox ID="chkSector"  runat="server" ></asp:CheckBox>
                          </div>
                           <label class="col-md-2 col-sm-2"> Sector Name</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                           <asp:TextBox ID="txtSectorName" CssClass="form-control" runat="server"></asp:TextBox>
                          </div>
                          
                          <div class="col-md-2 col-sm-2">
                              <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-success"
                                                    OnClick="btnsearch_Click" />
                          </div>
                        </div>
                        </div>
                          
                        </div>
                       
                           
                            <div class="table-responsive">
                              <asp:GridView ID="gvSector" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" CellPadding="4"
                                           GridLines="None" DataKeyNames="SECTORID" 
                                    onrowediting="gvSector_RowEditing">
                                            
                                            <Columns>
                                             
                                                   <asp:TemplateField HeaderText="Sl.No.">
                                                         <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField  HeaderText="SectorCode" DataField="SectorCode"  />
                                                  <asp:BoundField HeaderText="SectorName" DataField="strSectorName" />
                                                   <asp:BoundField HeaderText="Description" DataField="strSectorDescription" />
                                                    <asp:BoundField  HeaderText="Priority" DataField="SectorPriority"  />
                                                  <asp:BoundField HeaderText="PolicyRefrence" DataField="intPolicyReference" />
                                                
                                                <%--<asp:TemplateField HeaderText="Sector Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("SectorCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sector Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("SectorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                         <%--<button type="button" class="btn btn-add btn-sm" data-toggle="modal" data-target="#sector1"><i class="fa fa-pencil"></i></button>    --%>
                                                          <asp:LinkButton ID="lbtnAction" class="btn btn-add btn-sm" runat="server" CommandName="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                        <button type="button" class="btn btn-danger btn-sm" data-toggle="modal" data-target="#sector2"><i class="fa fa-trash-o"></i> </button>
                                                        <asp:HiddenField ID="hdn" runat="server" Value='<%#Eval("SECTORID")%>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                           
                                        </asp:GridView>

                           </div>
                        </div>
                     </div>
                  </div>
               </div>
                  <!-- sector Modal1 -->
               <div class="modal fade" id="sector1" tabindex="-1" role="dialog" aria-hidden="true">
                  <div class="modal-dialog">
                     <div class="modal-content">
                        <div class="modal-header modal-header-primary">
                           <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                           <h3><i class="fa fa-user m-r-5"></i> Update Sector</h3>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                              <div class="col-md-12">
                                 <form class="form-horizontal">
                                    <fieldset>
                                       <!-- Text input-->
                                       <div class="col-md-4 form-group">
                                          <label class="control-label">Sector Code:</label>
                                          <input type="text" placeholder="Sector Code" class="form-control">
                                       </div>
                                       <!-- Text input-->
                                       <div class="col-md-4 form-group">
                                          <label class="control-label">Sector Name:</label>
                                          <input type="text" placeholder="Sector Name" class="form-control">
                                       </div>
                                       <!-- Text input-->
                                       <div class="col-md-4 form-group">
                                          <label class="control-label">Description:</label>
                                          <textarea name="Description" rows="3"></textarea>
                                       </div>                                        
                                       <div class="col-md-12 form-group user-form-group">
                                          <div class="pull-right">
                                             <button type="button" class="btn btn-danger btn-sm">Cancel</button>
                                             <button type="submit" class="btn btn-add btn-sm">Save</button>
                                          </div>
                                       </div>
                                    </fieldset>
                                 </form>
                              </div>
                           </div>
                        </div>
                        <div class="modal-footer">
                           <button type="button" class="btn btn-danger pull-left" data-dismiss="modal">Close</button>
                        </div>
                     </div>
                     <!-- /.modal-content -->
                  </div>
                  <!-- /.modal-dialog -->
               </div>
               <!-- /.modal -->
               <!-- Modal -->    
               <!-- sector Modal2 -->
               <div class="modal fade" id="sector2" tabindex="-1" role="dialog" aria-hidden="true">
                  <div class="modal-dialog">
                     <div class="modal-content">
                        <div class="modal-header modal-header-primary">
                           <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                           <h3><i class="fa fa-user m-r-5"></i> Delete Sector</h3>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                              <div class="col-md-12">
                                 <form class="form-horizontal">
                                    <fieldset>
                                       <div class="col-md-12 form-group user-form-group">
                                          <label class="control-label">Delete Sector</label>
                                          <div class="pull-right">
                                             <button type="button" class="btn btn-danger btn-sm">NO</button>
                                             <button type="submit" class="btn btn-add btn-sm">YES</button>
                                          </div>
                                       </div>
                                    </fieldset>
                                 </form>
                              </div>
                           </div>
                        </div>
                        <div class="modal-footer">
                           <button type="button" class="btn btn-danger pull-left" data-dismiss="modal">Close</button>
                        </div>
                     </div>
                     <!-- /.modal-content -->
                  </div>
                  <!-- /.modal-dialog -->
               </div>
               <!-- /.modal -->
            </section>
        <!-- /.content -->
    </div>
</div>
</asp:Content>
