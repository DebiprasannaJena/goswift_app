<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="HelpdeskReport.aspx.cs" Inherits="Portal_Report_HelpdeskReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/decimalrstr.js" type="text/javascript"></script>
    <style>
        /* Comments
---------------------------------- */
        .comments
        {
            margin-top: 60px;
        }
        .comments h2.title
        {
            margin-bottom: 40px;
            border-bottom: 1px solid #d2d2d2;
            padding-bottom: 10px;
        }
        .comment
        {
            font-size: 14px;
        }
        .comment .comment
        {
            margin-left: 75px;
        }
        .comment-avatar
        {
            margin-top: 5px;
            width: 55px;
            float: left;
        }
        .comment-content
        {
            border-bottom: 1px solid #d2d2d2;
            margin-bottom: 25px;
        }
        .comment h3
        {
            margin-top: 0;
            margin-bottom: 5px;
        }
        .comment-meta
        {
            margin-bottom: 15px;
            color: #999999;
            font-size: 12px;
        }
        .comment-meta a
        {
            color: #666666;
        }
        .comment-meta a:hover
        {
            text-decoration: underline;
        }
        .comment .btn
        {
            font-size: 12px;
            padding: 7px;
            min-width: 100px;
            margin-top: 5px;
            margin-bottom: -1px;
        }
        .btn-gray
        {
            color: #ffffff;
            background-color: #666666;
            border-color: #666666;
        }
        .comment .btn i
        {
            padding-right: 5px;
        }
    </style>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Helpdesk Staus Wise Report</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Helpdesk</a></li><li><a>HelpDesk Report</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                 
                                              
                                                
                                          
                  <div class="col-sm-12">

               


                     <div class="panel panel-bd lobidisable">
                             <div class="panel-heading">
                            <div class="dropdown">
                        <ul class="dropdown-menu dropdown-menu-right">
                        <li><a  data-tooltip="Save as Pdf" data-toggle="tooltip" data-title="Save as Pdf" ><i class="panel-control-icon fa fa-file-pdf-o"></i></a></li>
                        <li><a  class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print" ><i class="panel-control-icon fa fa-print"></i></a></li>
                       <li><a  href="javascript:history.back()" data-tooltip="Back" data-toggle="tooltip" data-title="Back" ><i class="panel-control-icon fa  fa-chevron-circle-left"></i></a></li>
                        </ul>
                        </div>
                        </div>
                        <div class="panel-body">
                             <div align="right" >
                                    <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                        OnClick="lbtnAll_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                </div>
                            <div class="table-responsive">
                            

                            <asp:GridView ID="gvService" runat="server" class="table table-bordered table-hover" AutoGenerateColumns="false" 
                                    EmptyDataText="No Record(s) found...." onpageindexchanging="gvService_PageIndexChanging" DataKeyNames="int_CategoryId,int_SubcategoryId,status,vchIssueNo" OnRowDataBound="gvService_RowDataBound"> 
                            <Columns>
                                     <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsl" runat="server" Text='<%#(gvService.PageIndex * gvService.PageSize) + (gvService.Rows.Count + 1)%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Issue No">
                                       <%-- <ItemTemplate>
                                            <asp:HyperLink ID="hyIssue" runat="server" Text='<%#Eval("vchIssueNo")%>' HeaderStyle-Width="30 "></asp:HyperLink>
                                        </ItemTemplate>--%>
                                        <ItemTemplate>
                                        <asp:HyperLink ID="hlnkTkn" ForeColor="Blue" Text='<%#Eval("vchIssueNo") %>'  runat="server" ToolTip="IssueNo" data-toggle="modal" data-target='<%# "#"+Eval("vchIssueNo") %>'></asp:HyperLink>
                                
                                         <div class="modal fade"  id='<%#Eval("vchIssueNo") %>' tabindex="-1" role="dialog" aria-hidden="true">
                
               <div class="modal-dialog modal-lg">
                                                <div class="modal-content" >
                        <div class="modal-header modal-header-primary">
                           <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                         
                           <h3><i class="fa fa-user m-r-5"></i> Details</h3>
                        </div>
                        <div class="modal-body" > 
                           <div class="row">
                              <div class="col-md-12">
                            <%--     <form class="form-horizontal">--%>
                                    <fieldset>
                                    <div class="panel panel-bd ">
                  <%--      <div class="panel-heading">
                         Details
                        </div>--%>
                      <div class="panel-body">
                           <div class="form-group">
                                    <div class="row">
                                  <label class="col-md-2" style="color: #000000">Issue No</label>
                                    <div class="col-md-4">
                                    <span class="colon">:</span>
                                   <label class="form-control-static" id="lblProposalId"><span><%#Eval("vchIssueNo")%></span></label>
                                    </div>
                                    <label class="col-md-2" style="color: #000000">User Name</label>
                                    <div class="col-md-4">
                                    <span class="colon">:</span>
                                   <label class="form-control-static" id="Label6"><span><%#Eval("vch_UserName")%></span></label>
                                    </div>
                                    
                                    </div>
                                    </div>
                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Request date</label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                   <label class="form-control-static" id="lblInvestorName"><span><%#Eval("dtmCreatedOn")%></span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000"> Type</label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                     <label class="form-control-static" id="lblIndustryType"><span><%#Eval("vch_Type")%></span></label>
                                    </div>
                                    </div>
                                    </div>
                       
                         <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Mobile</label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                   <label class="form-control-static" id="Label1"><span><%#Eval("VchMobile")%></span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Priority </label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                     <label class="form-control-static" id="Label2"><span><%#Eval("vch_Priority")%></span></label>
                                    </div>
                                    </div>
                                    </div>
                       
                         <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Category</label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                   <label class="form-control-static" id="Label3"><span><%#Eval("CategoryName")%></span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">SubCategory </label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                     <label class="form-control-static" id="Label4"><span><%#Eval("SubCategory")%></span></label>
                                    </div>
                                    </div>
                                    </div>
                                      <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">File Upload</label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                     <asp:HyperLink  ID="hplnkCertificate" NavigateUrl='<%#Eval("vch_FIleUpload") %>' Target="_blank" runat="server"  CssClass="fa fa-download" ></asp:HyperLink>
                                  
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Email </label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                     <label id="Label1" class="form-control-static" runat="server"><span><%#Eval("Email")%></span></label>
                                    
                                    </div>
                                    </div>
                                    </div>
                                    <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Address</label>
                                    <div class="col-md-10">
                                     <span class="colon">:</span>
                                    <label class="form-control-static" id="Label7"><span><%#Eval("Address")%></span></label>
                                 
                                    </div>
                                    </div>
                                    </div>

                                    <div class="form-group">
                                     <div class="row">
                                      <label class="col-md-2" style="color: #000000">Issue Details</label>
                                    <div class="col-md-10">
                                     <span class="colon">:</span>
                                    <label class="form-control-static" id="lblIndustriesName"><span><%#Eval("vch_IssueDetails")%></span></label>
                                    </div>
                                    </div>  </div>
                                     </div>
                        
                        
                     </div>
                      <div class="panel panel-bd ">
                        
                        </div>
                                       <!-- Text input-->
                                      
                                    </fieldset>
                         <%--        </form>--%>
                              </div>
                           </div>
                        </div>
                        <div class="modal-footer">
                           <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Close</button>
                        </div>
                         </div>
                          </div>
                          </div>
                     </div>
                                                    <!-- /.modal-content -->
                                    </div>


                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                   <asp:BoundField HeaderText="User Name" DataField="vch_UserName" NullDisplayText="--" />
                                    <asp:BoundField HeaderText="Email" DataField="Email" NullDisplayText="--"  />
                                     <asp:BoundField HeaderText="Date" DataField="dtmCreatedOn" NullDisplayText="--" />
                                    <asp:BoundField HeaderText="Status" DataField="VCH_STATUS" NullDisplayText="--"  />
                            </Columns>
                            </asp:GridView>
                                       
                           
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
                
            </section>
        <!-- /.content -->
    </div>
</asp:Content>