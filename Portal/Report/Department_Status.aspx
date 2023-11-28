<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="Department_Status.aspx.cs" Inherits="Portal_Report_Department_Status" %>

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
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Department Wise Status Report</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Proposal</a></li><li><a>View</a></li></ul>
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
                                    EmptyDataText="No Record(s) found...." onpageindexchanging="gvService_PageIndexChanging" DataKeyNames="intLevelDetailId,INT_SERVICEID,INT_STATUS,INT_PAYMENT_STATUS" OnRowDataBound="gvService_RowDataBound"> 
                            <Columns>
                                     <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsl" runat="server" Text='<%#(gvService.PageIndex * gvService.PageSize) + (gvService.Rows.Count + 1)%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:BoundField HeaderText="Sl.No." HeaderStyle-Width="30"></asp:BoundField>--%>
                                    <%--<asp:BoundField HeaderText="Department Name" DataField="nvchLevelName" NullDisplayText="--"  />--%>
                                    <asp:TemplateField HeaderText="Department Name">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyDept" runat="server" Text='<%#Eval("nvchLevelName")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                       <%-- <ItemStyle HorizontalAlign="Left" />--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Received">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyRcvd" runat="server" Text='<%#Eval("Total_Recieved")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Applied">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyAppl" style="color:blue;font-weight:bold;" runat="server" Text='<%#Eval("Applied")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rejected">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyRejt" style="color:red;font-weight:bold;" runat="server" Text='<%#Eval("Rejected")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Approved">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyApprove" style="color:green;font-weight:bold;" runat="server" Text='<%#Eval("Approved")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="QueryRasied">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyQueryRasied" runat="server" Text='<%#Eval("QueryRasied")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="QueryReverted">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyQueryReverted" runat="server" Text='<%#Eval("QueryReverted")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Deferred">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyDiffered" style="color:dodgerblue;font-weight:bold;" runat="server" Text='<%#Eval("Differed")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="InProgress">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyInProgress" runat="server" Text='<%#Eval("InProgress")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pending">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyPending" style="color:violet;font-weight:bold;" runat="server" Text='<%#Eval("Pending")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Forwarded">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyForwarded" runat="server" Text='<%#Eval("Forwarded")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Generating Demand">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyGen" runat="server" Text='<%#Eval("GenerateDemand")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                  
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
