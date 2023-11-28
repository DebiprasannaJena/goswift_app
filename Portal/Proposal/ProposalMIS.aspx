<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Application.master"
    CodeFile="ProposalMIS.aspx.cs" Inherits="Mastermodule_ProposalMIS" %>

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
                  <h1>MIS Report</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Proposal</a></li><li><a>View</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                 
                                              
                                                
                                          
                  <div class="col-sm-12">

               <div class="form-group row">
                                                    <div class="col-sm-4">
                                                        <label for="Country">
                                                            Project Type</label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlProjrctType" runat="server"
                                                          >
                                                           <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Project Cost >= 50 crore" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Project cost upto < 50 crore" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-4" runat="server" id="st3">
                                                        <label for="State">
                                                            Status</label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpStatus" runat="server">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                      <div class="col-sm-4">
                                                        <label for="Country">
                                                           District</label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlDistrict" runat="server"
                                                          >
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                       
                               <div class="col-sm-4">
                                 <label>From Date</label>
                                 <div class="input-group  date datePicker" id="datePicker1">
                                        <asp:TextBox ID="txtFromdate" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                
                                          </div>
                                <div class="col-sm-4">
                                          <label>To Date</label>
                                     <div class="input-group  date datePicker" >
                                        <asp:TextBox ID="txtTodate" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                   
                                 </div>

                                                     <div class="col-sm-4">
                                                       
                                                     <asp:Button ID="btnSearch" style="margin-top:30px" 
                                                             CssClass="btn btn btn-add btn-sm" runat="server" Text="Search" 
                                                             onclick="btnSearch_Click"></asp:Button>

                                                    </div>
                                                </div>


                     <div class="panel panel-bd lobidisable">
                     
                        <div class="panel-body">
                             <div align="right" >
                                    <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                        OnClick="lbtnAll_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                </div>
                            <div class="table-responsive">
                            

                              <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server" 
                                AutoGenerateColumns="false" AllowPaging="true" onpageindexchanging="gvService_PageIndexChanging"
                Width="100%" EmptyDataText="No Record(s) Found..." 
               DataKeyNames="intProposalId,intActionToBeTakenBy,strFileName,intQueryStatus,strQueryStatus"  PageSize="10" onrowdatabound="gvService_RowDataBound" 
                                >
                <Columns>
           

                                         <asp:BoundField  HeaderText=" Sl No."   />
                   <asp:TemplateField HeaderText="Proposal No">
                                      <ItemTemplate>
                                          <asp:HyperLink ID="hypLink" runat="server" NavigateUrl="ProposalDetails.aspx" Text='<%# Eval("strFileName") %>'></asp:HyperLink>
                                              <asp:HiddenField ID="hdnTextVal1" runat="server" Value='<%# Eval("strFileName")%>'>
                                                            </asp:HiddenField>
                                      </ItemTemplate>
                                      </asp:TemplateField>
                                        <asp:BoundField DataField="decAmount" HeaderText="Industry Type" />
                                        <asp:BoundField DataField="strRemarks" HeaderText="Name Of Company/Enterprises" />

                                          

                                         <asp:BoundField DataField="dtmCreatedOn" HeaderText="Application Date" />
                                        
                                     <asp:BoundField DataField="strStatus" HeaderText="Status" />
                                        
                                      
                                         
                                     
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
