<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="ViewServiceMaster.aspx.cs" Inherits="ServiceMaster_ViewServiceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
<asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
<div class="content-wrapper">
            <!-- Content Header (Page header) -->
            <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Service</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Services</a></li><li><a>View Service</a></li></ul>
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
                              <a class="btn btn-add " href="AddServiceMaster.aspx"> 
                              <i class="fa fa-plus"></i>Add</a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewServiceMaster.aspx"> 
                              <i class="fa fa-file"></i>View</a>  
                           </div>
                             <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ServiceMaster.aspx"> 
                              <i class="fa fa-plus"></i> Edit Service Name </a>  
                           </div>
                        </div>
                        <div class="panel-body">
                        <div class="search-sec">
                        <div class="form-group">
                        <%--<div class="row">
                       <label class="col-md-2 col-sm-3">Service Sector</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                            <asp:DropDownList ID="ddlServiceSector" CssClass="form-control"  runat="server"
                                                    AutoPostBack="True">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Aluminium Industry" Value="1" />
                                                    <asp:ListItem Text="Cement, Lime and Plaster" Value="2" />
                                                    <asp:ListItem Text="Chemicals and Chemical products" Value="3" />
                                                </asp:DropDownList>
                          </div>
                           <label class="col-md-2 col-sm-3"> Service Sub-Sector</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                           <asp:DropDownList ID="ddlServiceSubSector" CssClass="form-control" 
                                                    runat="server" AutoPostBack="True">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Basic Aluminium" Value="1" />
                                                    <asp:ListItem Text="Cement, Lime and Plaster" Value="2" />
                                                    <asp:ListItem Text="Basic chemicals" Value="3" />
                                                </asp:DropDownList>
                          </div>
                        </div>--%>
                        </div>
                          <div class="form-group">
                        <div class="row">
                       <label class="col-md-2 col-sm-3"> Department</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                            <asp:DropDownList ID="ddlDept" CssClass="form-control"  runat="server"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Automobile" Value="1" />
                                                    <asp:ListItem Text="Environment" Value="2" />
                                                    <asp:ListItem Text="Forest" Value="3" />
                                                </asp:DropDownList>
                          </div>
                           <label class="col-md-2 col-sm-3">Service Name</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                            <asp:TextBox ID="txtServiceName" CssClass="form-control" runat="server" autocomplete="off" Text=""></asp:TextBox>
                          </div>
                            <%--<div class="col-md-2 col-sm-2">
                              <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-success"
                                                    OnClick="btnsearch_Click" />
                            </div>--%>
                        </div>
                        <br />
                         <div class="form-group row">
                              <div class="col-md-12  text-center">
                                  <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-success" OnClick="btnsearch_Click" />
                              </div>
                        </div>

                        </div>
                        </div>
                       
                           
                            <div class="table-responsive">
                                 <div style="display: inline-block; text-align: right; width: 100%">
                            <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                        OnClick="lbtnAll_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                            </div>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                              <asp:GridView ID="gvService" runat="server" 
                                     class="table table-bordered table-hover" AllowPaging="true"
                                            AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" 
                                     CellPadding="4" PageSize="10"
                                           GridLines="None" DataKeyNames="intPaymentStatus,intServiceId" 
                                     OnRowDataBound="gvService_RowDataBound" 
                                     onpageindexchanging="gvService_PageIndexChanging" 
                                     onrowcommand="gvService_RowCommand">
                                            
                                            <Columns>
                                            <asp:BoundField HeaderText="SL#" />
                                                
                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("str_Department") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("strServiceName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Alias Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("strServiceAliasName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Service Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblservicetype" runat="server" Text='<%# Eval("Int_ServiceType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Service Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("strRemark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Payment Required">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("intPaymentStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Payment Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("strPaymentAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ORTPS Time line(in Days)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("strExcalationDays") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="If External Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblExternalType" runat="server" Text='<%# Eval("intExternalType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Service URL" DataField="Str_ExtrnalServiceUrl" HeaderStyle-Width="4px" ItemStyle-Width="4px" />
                                                  <%--   <asp:TemplateField HeaderText="Service URL" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblserviceURL" runat="server"  Text='<%# Eval("Str_ExtrnalServiceUrl") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                               
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                     <asp:HyperLink ID="hlnk" ForeColor="Blue"  runat="server" ToolTip="ApplicationNo"><i class="fa fa-pencil"></i></asp:HyperLink>
                                                         
                                                         <asp:ImageButton ID="imgdelete" runat="server" OnClientClick="return confirm('Are you sure to Delete Service !!');" Height="16px" ImageUrl="../../images/DeleteIcn.png" Width="16px" CommandName="DeleteRow" > 
                                                         </asp:ImageButton>
                                    
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                             <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
 </ContentTemplate>
                                <triggers>
                                   <asp:asyncpostbacktrigger controlid="btnsearch"  />
                                  <asp:asyncpostbacktrigger controlid="ddlDept"  />
                                    </triggers>
   
                                 </asp:UpdatePanel>
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
                 
            </section>
           
            <!-- /.content -->
         </div>
</asp:Content>

