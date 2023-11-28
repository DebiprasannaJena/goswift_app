<%--'*******************************************************************************************************************
' File Name         : ViewInvestorDetails.aspx
' Description       : View details of Investor data
' Created by        : AMit Sahoo
' Created On        : 11 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewInvestor.aspx.cs" Inherits="Investor_ViewInvestor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
  
    <%--<head id="Head1" runat="server">--%>
        <script language="javascript" type="text/javascript">
            function CheckAuthenticate(obj) {                
                var ID = $(obj).attr('id');
                var sub = ID.split('_');
                if ($("#ContentPlaceHolder1_gvInvestor_txtRemark_" + sub[3]).val() == "") {
                    jAlert('Remarks can not be left blank !');
                    $("#ContentPlaceHolder1_gvInvestor_txtRemark_" + sub[3]).focus();
                        return false;
                    }
               
            }
        </script>
    <%--</head>--%>
    <asp:ScriptManager ID="ScriptManager1"  runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Investor</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Investors</a></li><li><a>View Investor</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <%--<div class="panel-heading">
                           <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddServiceMaster.aspx"> 
                              <i class="fa fa-plus"></i>  Add List </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewServiceMaster.aspx"> 
                              <i class="fa fa-file"></i> View List </a>  
                           </div>
                           
                        </div>--%>
                        <div class="panel-body">
                        <%--<div class="search-sec">
                        <div class="form-group">
                        <div class="row">
                         <label class="col-md-2 col-sm-3">Industry Name</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                            <asp:TextBox ID="txtIndName" CssClass="form-control" runat="server"></asp:TextBox>
                          </div>
                       <label class="col-md-2 col-sm-3">District</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                          <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="--Select--" Value="0">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Jharsuguda" Value="1"> </asp:ListItem>
                                    <asp:ListItem Text="Jajpur" Value="2"> </asp:ListItem>
                                    <asp:ListItem Text="Jagatsinghpur" Value="3"> </asp:ListItem>
                                </asp:DropDownList>
                          </div>                           
                        </div>
                        </div>
                          <div class="form-group">
                        <div class="row">
                        <label class="col-md-2 col-sm-3">Block</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                          <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Lakhanpur" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Sukinda" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Ersama" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                          </div>
                       <label class="col-md-2 col-sm-3"> Category</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                            <asp:DropDownList ID="ddlCategory" CssClass="form-control"  runat="server"
                                    AutoPostBack="True">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="MSME" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Large" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                          </div>
                            <div class="col-md-2 col-sm-2">
                              <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-success"
                                                    OnClick="btnsearch_Click" />
                          </div>
                        </div>
                        </div>
                        </div>--%>
                     
                          <%--   <div align="right" >
                                    <asp:LinkButton ID="lbtnAll" runat="server"  CssClass="" Text="All"
                                        OnClick="lbtnAll_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                </div>--%>
                                  <span style="text-align: right; float: right; padding-right: 5px; height: 21px;">
                            <asp:LinkButton ID="lbtnAll" runat="server" CssClass="more" Text="All" onclick="lbtnAll_Click" 
                                   ></asp:LinkButton>
                            <asp:Label ID="lblPaging" runat="server" Text=""></asp:Label>
                            </span>
                         <%--   <div class="table-responsive">--%>
                              <asp:GridView ID="gvInvestor" runat="server" CssClass="table table-bordered" Width="100%"
                            AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" CellPadding="4" 
                                    AllowPaging="true" PageSize="10"
                            ForeColor="#333333" GridLines="None" DataKeyNames="IntIndGroupID" onpageindexchanging="gvInvestor_PageIndexChanging"                            
                                    onrowdatabound="gvInvestor_RowDataBound">
                            <%--<AlternatingRowStyle BackColor="White" ForeColor="#284775" />--%>
                            <Columns>
                             <asp:TemplateField HeaderText="Sl#">
                                           <ItemTemplate>
                                                 <%#Container.DataItemIndex+1 %>                                                
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                <asp:TemplateField HeaderText="Industry">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvestorName" runat="server" Text='<%# Eval("strIndName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("District") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Block">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Block") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Entrepreneur Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactPerson" runat="server" Text='<%# Eval("strSecAnswer") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("strAddress") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Mobile Number">
                                    <ItemTemplate>
                                       <asp:Image ID="imgApprStatus" runat="server" />   

                                        <asp:Label ID="lblMobNo" runat="server" Text='<%# Eval("MobNo") %>'></asp:Label>
                                          <asp:Label ID="lblverfied" runat="server" Text='<%#Eval("intOTPStatus") %>' Visible="false"></asp:Label>
                                             
                                                                            
                                    </ItemTemplate>
                                     </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Industry Group" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndGroup" runat="server" Text='<%# Eval("strIndGroupName") %>'></asp:Label>
                                          <asp:Label ID="lblInvestId" runat="server" Text='<%# Eval("IntIndGroupID") %>' Visible="false"></asp:Label>                                          
                                    </ItemTemplate>
                                     </asp:TemplateField>
                                    
                                       <asp:TemplateField HeaderText="Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("IntIndStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                     </asp:TemplateField>   
                                           <asp:TemplateField HeaderText="Remarks">          
                                        <ItemTemplate>
                                        <asp:TextBox ID="txtRemark" runat="server" ></asp:TextBox>
                                      <asp:Label ID="lblRemarks" runat="server" Visible="false" Text='<%# Eval("StrRemarks") %>'></asp:Label>
                                         </ItemTemplate>
                                        </asp:TemplateField>                                          
                                         <asp:TemplateField HeaderText="Approve/Reject" ItemStyle-Width="250px" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnStatusApprove" OnClientClick="return CheckAuthenticate(this);"  runat="server" CommandArgument='<%# Eval("IntInvestorId") %>'
                                            CssClass="btn btn-success"                                        
                                            onclick="lbtnStatusApprove_Click">Approve</asp:LinkButton> 
                                             <asp:LinkButton ID="lbtnStatusReject" runat="server" CommandArgument='<%# Eval("IntInvestorId") %>'
                                            CssClass="btn btn-danger" OnClientClick="return CheckAuthenticate(this);" 
                                        
                                            onclick="lbtnStatusReject_Click">Reject</asp:LinkButton>     
                                             <asp:Label ID="lblAppRejStatus" runat="server" Text='<%# Eval("IntIndStatus") %>' Visible="false"></asp:Label>                                         
                                    </ItemTemplate>
                                     </asp:TemplateField>                                                        
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="180px" Visible="false">
                                    <ItemTemplate>
                                       <%-- <button type="button" class="btn btn-add btn-sm" data-toggle="modal" data-target="#customer1">
                                            <i class="fa fa-pencil"></i>--%>
                                             <asp:HyperLink ID="hypLink" class="btn btn-primary btn-sm" runat="server" NavigateUrl="~/Investor/InvestorDetails.aspx" Text='Edit' ToolTip="Edit">  <i class="fa fa-pencil-square-o"></i></asp:HyperLink>
                                        </button>
                                        <button type="button" class="btn btn-danger btn-sm" data-toggle="modal" data-target="#customer2" title="Delete" >
                                            <i class="fa fa-trash-o"></i>
                                        </button>
                                         <asp:HyperLink ID="hypLink1" class="btn btn-warning btn-sm" runat="server"  Text='Block'  ToolTip="Lock">  <i class="fa fa-lock"></i></asp:HyperLink>
                                          <asp:HyperLink ID="hypLink2" class="btn btn-success btn-sm" runat="server"  Text='Unblock'  ToolTip="UnLock">  <i class="fa fa-unlock"></i></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                               <PagerStyle CssClass="pagination-grid no-print" HorizontalAlign="Right" />                         
                        </asp:GridView>

                           <%--</div>--%>
                        </div>
                     </div>
                  </div>
               </div>
                  <!-- customer Modal1 -->
               <%--<div class="modal fade" id="customer1" tabindex="-1" role="dialog" aria-hidden="true">
                  <div class="modal-dialog">
                     <div class="modal-content">
                        <div class="modal-header modal-header-primary">
                           <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                           <h3><i class="fa fa-user m-r-5"></i> Update Customer</h3>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                              <div class="col-md-12">
                                 <form class="form-horizontal">
                                    <fieldset>
                                       <!-- Text input-->
                                       <div class="col-md-4 form-group">
                                          <label class="control-label">Customer Name:</label>
                                          <input type="text" placeholder="Customer Name" class="form-control">
                                       </div>
                                       <!-- Text input-->
                                       <div class="col-md-4 form-group">
                                          <label class="control-label">Email:</label>
                                          <input type="email" placeholder="Email" class="form-control">
                                       </div>
                                       <!-- Text input-->
                                       <div class="col-md-4 form-group">
                                          <label class="control-label">Mobile</label>
                                          <input type="number" placeholder="Mobile" class="form-control">
                                       </div>
                                       <div class="col-md-6 form-group">
                                          <label class="control-label">Address</label><br>
                                          <textarea name="address" rows="3"></textarea>
                                       </div>
                                       <div class="col-md-6 form-group">
                                          <label class="control-label">type</label>
                                          <asp:FileUpload  ID="fulLogo" CssClass="form-control" runat="server"/>
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
               </div>--%>
               <!-- /.modal -->
               <!-- Modal -->    
               <!-- Customer Modal2 -->
               <div class="modal fade" id="customer2" tabindex="-1" role="dialog" aria-hidden="true">
                  <div class="modal-dialog">
                     <div class="modal-content">
                        <div class="modal-header modal-header-primary">
                           <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                           <h3><i class="fa fa-user m-r-5"></i> Delete Investor</h3>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                              <div class="col-md-12">
                                 <form class="form-horizontal">
                                    <fieldset>
                                       <div class="col-md-12 form-group user-form-group">
                                          <label class="control-label">Delete Investor</label>
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
    </from>
   
</asp:Content>
