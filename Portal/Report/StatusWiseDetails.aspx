﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" 
AutoEventWireup="true" CodeFile="StatusWiseDetails.aspx.cs" Inherits="Portal_Report_StatusWiseDetails" %>


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
                  <h1>Status Report</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Proposal</a></li><li><a>View</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                 
                                              
                                                
                                          
                  <div class="col-sm-12">
                  <div class="search-sec">
               <div class="form-group row">
                                                    <%--<div class="col-sm-4">
                                                        <label for="Country">
                                                            Project Type</label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlProjrctType" runat="server"
                                                          >
                                                           <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Project Cost >= 50 crore" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Project cost upto < 50 crore" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>--%>
                                                   <%-- <div class="col-sm-4" runat="server" id="st3">
                                                        <label for="State">
                                                            Status</label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpStatus" runat="server">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                            <asp:ListItem Value="1">Success</asp:ListItem>
                                                            <asp:ListItem Value="2">Failure</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>--%>
                                                   <%--   <div class="col-sm-4">
                                                        <label for="Country">
                                                           District</label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlDistrict" runat="server"
                                                          >
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>--%>
                             <%--                        <label class="col-sm-2">From Date</label>
                               <div class="col-sm-3">
                                <span class="colon">:</span>
                                 <div class="input-group  date datePicker" id="datePicker1">
                                        <asp:TextBox ID="txtFromdate"  CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                
                                          </div> --%>
                                                       
                              <%-- <div class="col-sm-4">
                                 <label>From Date</label>
                                 <div class="input-group  date datePicker" id="datePicker1">
                                        <asp:TextBox ID="txtFromdate"  CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                
                                          </div>
                                <div class="col-sm-4">
                                          <label>To Date</label>
                                     <div class="input-group  date datePicker" >
                                        <asp:TextBox ID="txtTodate"  CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                   
                                 </div>--%>

                                                   
                                                </div>
<div class="form-group">
                                   <div class="row">
                                <label class="col-sm-2">Application No</label>
                               <div class="col-sm-3">
                               
                                <span class="colon">:</span>
                                  <asp:TextBox ID="txtAppno" MaxLength="20" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                          </div>  <label class="col-sm-2">Status</label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpStatus" runat="server">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                            <asp:ListItem Value="1">Success</asp:ListItem>
                                                            <asp:ListItem Value="2">Failure</asp:ListItem>
                                                        </asp:DropDownList>
                                 </div>
                                 </div>
                                  </div>
                                   <div class="form-group">
                                 <div class="row">
                               <label class="col-sm-2">From Date</label>
                               <div class="col-sm-3">
                                <span class="colon">:</span>
                                 <div class="input-group  date datePicker" id="Div1">
                                        <asp:TextBox ID="txtFromdate"  CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                
                                          </div>  <label class="col-sm-2">To Date</label>
                                <div class="col-sm-3">
                                    <span class="colon">:</span>    
                                     <div class="input-group  date datePicker" >
                                        <asp:TextBox ID="txtTodate"  CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                   
                                 </div>
                                <%-- <div class="col-sm-2">
                                   <asp:Button ID="btnShow" runat="server" Text="Search" class="btn btn-add btn-sm" 
                                          onclick="btnShow_Click"></asp:Button>
                                  </div>--%>
                                    <div class="col-sm-4">
                                                       
                                                     <asp:Button ID="btnSearch" style="margin-top:30px" 
                                                             CssClass="btn btn btn-add btn-sm" runat="server" Text="Search" 
                                                             onclick="btnSearch_Click"></asp:Button>

                                                    </div>
                              </div>
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
                Width="100%" EmptyDataText="No Record(s) Found..." ShowFooter="true"
               DataKeyNames="intServiceId,VCH_APPLICATION_NO"  PageSize="10" onrowdatabound="gvService_RowDataBound" 
                                >
                <Columns>
           <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsl" runat="server" Text='<%#(gvService.PageIndex * gvService.PageSize) + (gvService.Rows.Count + 1)%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Application No.">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyAppli" runat="server" Text='<%#Eval("VCH_APPLICATION_NO")%>' HeaderStyle-Width="30 "></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Order No" DataField="vchOrderNo" NullDisplayText="--" />
                                     <asp:TemplateField  HeaderText="Challan Amount">
                                       <ItemTemplate>
                                          <asp:Label ID="lblChallan" runat="server" Text='<%#Eval("vchChallanAmount")%>'></asp:Label>
                                       </ItemTemplate>
                           
                                       </asp:TemplateField>
                                    <asp:BoundField HeaderText="Date" DataField="dtmCreatedOn" NullDisplayText="--" />
                                         <%--<asp:BoundField  HeaderText=" Sl No."   />--%>
                  <%-- <asp:TemplateField HeaderText="Proposal No">
                                      <ItemTemplate>
                                          <asp:HyperLink ID="hypLink" runat="server" NavigateUrl="ProposalDetails.aspx" Text='<%# Eval("strFileName") %>'></asp:HyperLink>
                                              <asp:HiddenField ID="hdnTextVal1" runat="server" Value='<%# Eval("strFileName")%>'>
                                                            </asp:HiddenField>
                                      </ItemTemplate>
                                      </asp:TemplateField>--%>
                                        <asp:BoundField DataField="status" HeaderText="Status" />
                                       <%-- <asp:BoundField DataField="strRemarks" HeaderText="Investor Name" />

                                          

                                         <asp:BoundField DataField="dtmCreatedOn" HeaderText="Application Date" />
                                        
                                     <asp:BoundField DataField="strStatus" HeaderText="Status" />--%>
                                        
                                      
                                         
                                     
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


