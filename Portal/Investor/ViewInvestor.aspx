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
  
        <script language="javascript" type="text/javascript">
            function CheckAuthenticate(obj) {                
                var ID = $(obj).attr('id');
                var sub = ID.split('_');
                if ($("#ContentPlaceHolder1_gvInvestor_TxtRemark_" + sub[3]).val() == "") {
                    jAlert('Remarks can not be left blank !');
                    $("#ContentPlaceHolder1_gvInvestor_TxtRemark_" + sub[3]).focus();
                        return false;
                    }
               
            }
        </script>
   
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
                        <div class="panel-body">
                                  <span style="text-align: right; float: right; padding-right: 5px; height: 21px;">
                            <asp:LinkButton ID="lbtnAll" runat="server" CssClass="more" Text="All" onclick="lbtnAll_Click" 
                                   ></asp:LinkButton>
                            <asp:Label ID="LblPaging" runat="server" Text=""></asp:Label>
                            </span>
                              <asp:GridView ID="gvInvestor" runat="server" CssClass="table table-bordered" Width="100%"
                            AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" CellPadding="4" 
                                    AllowPaging="true" PageSize="10"
                            ForeColor="#333333" GridLines="None" DataKeyNames="IntIndGroupID" onpageindexchanging="gvInvestor_PageIndexChanging"                            
                                    onrowdatabound="gvInvestor_RowDataBound">
                       
                            <Columns>
                             <asp:TemplateField HeaderText="Sl#">
                                           <ItemTemplate>
                                                 <%#Container.DataItemIndex+1 %>                                                
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                <asp:TemplateField HeaderText="Industry">
                                    <ItemTemplate>
                                        <asp:Label ID="LblInvestorName" runat="server" Text='<%# Eval("strIndName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                             
                                <asp:TemplateField HeaderText="Entrepreneur Name">
                                    <ItemTemplate>
                                        <asp:Label ID="LblContactPerson" runat="server" Text='<%# Eval("strSecAnswer") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="LblAddress" runat="server" Text='<%# Eval("strAddress") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Mobile Number">
                                    <ItemTemplate>
                                       <asp:Image ID="ImgApprStatus" runat="server" />   

                                        <asp:Label ID="LblMobNo" runat="server" Text='<%# Eval("MobNo") %>'></asp:Label>
                                          <asp:Label ID="LblVerfied" runat="server" Text='<%#Eval("intOTPStatus") %>' Visible="false"></asp:Label>
                                             
                                                                            
                                    </ItemTemplate>
                                     </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Industry Group" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblIndGroup" runat="server" Text='<%# Eval("strIndGroupName") %>'></asp:Label>
                                          <asp:Label ID="LblInvestId" runat="server" Text='<%# Eval("IntIndGroupID") %>' Visible="false"></asp:Label>                                          
                                    </ItemTemplate>
                                     </asp:TemplateField>
                                    
                                       <asp:TemplateField HeaderText="Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblStatus" runat="server" Text='<%# Eval("IntIndStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                     </asp:TemplateField>   
                                           <asp:TemplateField HeaderText="Remarks">          
                                        <ItemTemplate>
                                        <asp:TextBox ID="TxtRemark" runat="server" ></asp:TextBox>
                                      <asp:Label ID="LblRemarks" runat="server" Visible="false" Text='<%# Eval("StrRemarks") %>'></asp:Label>
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
                                             <asp:Label ID="LblAppRejStatus" runat="server" Text='<%# Eval("IntIndStatus") %>' Visible="false"></asp:Label>                                         
                                    </ItemTemplate>
                                     </asp:TemplateField>                                                        
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="180px" Visible="false">
                                    <ItemTemplate>                                    
                                             <asp:HyperLink ID="HyplinkEdit" class="btn btn-primary btn-sm" runat="server" NavigateUrl="~/Investor/InvestorDetails.aspx" Text='Edit' ToolTip="Edit">  <i class="fa fa-pencil-square-o"></i></asp:HyperLink>
                                        </button>
                                        <button type="button" class="btn btn-danger btn-sm" data-toggle="modal" data-target="#customer2" title="Delete" >
                                            <i class="fa fa-trash-o"></i>
                                        </button>
                                         <asp:HyperLink ID="HyplinkBlock" class="btn btn-warning btn-sm" runat="server"  Text='Block'  ToolTip="Lock">  <i class="fa fa-lock"></i></asp:HyperLink>
                                          <asp:HyperLink ID="HyplinkUnblock" class="btn btn-success btn-sm" runat="server"  Text='Unblock'  ToolTip="UnLock">  <i class="fa fa-unlock"></i></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                               <PagerStyle CssClass="pagination-grid no-print" HorizontalAlign="Right" />                         
                        </asp:GridView>

                         
                        </div>
                     </div>
                  </div>
               </div>
                 
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
