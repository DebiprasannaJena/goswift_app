<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="SmsAndMailStatusReport.aspx.cs" Inherits="Portal_Report_SmsAndMailStatusReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../..js/decimalrstr.js" type="text/javascript"></script>
      <script type="text/javascript">
        $(document).ready(function () {

            $('.datePicker').datepicker({
                autoclose: true,
                format: 'dd-M-yyyy'
            });
        });
        </script>
    <style>

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
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>SMS And Mail Status Report</h1>
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
                            
                                <div class="search-sec">
                           <div class="form-group">
                              <div class="row">
                              
                             
                                 <label class="col-sm-2">Department</label>  <div class="col-sm-3">
                                 <span class="colon">:</span>
                              
                                  <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control dpt"  onselectedindexchanged="ddldept_SelectedIndexChanged" AutoPostBack="True">
                                   <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                  </asp:DropDownList>
                                 <%--    </ContentTemplate>
                                  --%>
                               
                                          </div>
                                
                                          <label class="col-sm-2">Service Sector</label><div class="col-sm-3"><span class="colon">:</span>
                                     <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                                    <ContentTemplate>
                                 <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control" 
                                       >
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                          
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                  <triggers>
                                    <asp:asyncpostbacktrigger controlid="ddldept"  />
                                   
                                    </triggers>
                                 </asp:UpdatePanel>
                                 </div>
                                 </div>
                                 </div>
                                   <div class="form-group">
                                   <div class="row">
                                <label class="col-sm-2">Type</label>
                               <div class="col-sm-3">
                               
                                <span class="colon">:</span>
                                 <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Service" Value="ServiceORTPS" />
                                            <asp:ListItem Text="Service Query" Value="ServiceQueryORTPS" />
                                            <asp:ListItem Text="Peal" Value="PealORTPS" />
                                              <asp:ListItem Text="Peal Query" Value="PQueryORTPS" />
                                 </asp:DropDownList>
                                          </div> 
                               
                                 </div>
                                  </div>
                                   <div class="form-group">
                                 <div class="row">
                               <label class="col-sm-2">From Date</label>
                               <div class="col-sm-3">
                                <span class="colon">:</span>
                                 <div class="input-group  date datePicker" id="datePicker1">
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
                                 <div class="col-sm-2">
                                   <asp:Button ID="btnShow" runat="server" Text="Search" class="btn btn-add btn-sm" 
                                          onclick="btnShow_Click"></asp:Button>
                                  </div>
                              </div>
                               </div>
                                 
</div>
                            <div class="table-responsive">
                            
                             <div align="right" >
                                    <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                        OnClick="lbtnAll_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                </div>
                            <asp:GridView ID="gvService" runat="server" class="table table-bordered table-hover" AutoGenerateColumns="false" 
                                    EmptyDataText="No Record(s) found...." onpageindexchanging="gvService_PageIndexChanging" AllowPaging="true" PageSize="10"  onrowdatabound="gvService_RowDataBound"> 
                            <Columns>
                            <asp:BoundField  HeaderText="Sl No."/>
                                  
                                  <%--  <asp:TemplateField HeaderText="Application No">
                                        <ItemTemplate>
                                        <asp:Label ID="lblAppNo" runat="server" Text='<%#Eval("SMApplicationNo")%>'></asp:Label>
                                     
                                        </ItemTemplate>
                         
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Application Type">
                                        <ItemTemplate>
                                                            <asp:Label ID="lblTYpe" runat="server" Text='<%#Eval("SMType")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                      

                                    <asp:TemplateField HeaderText="Phone No">
                                        <ItemTemplate>
                                                  <asp:Label ID="lblPhoeneNo" runat="server" Text='<%#Eval("SMMobileNo")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField HeaderText="Email Id">
                                        <ItemTemplate>
                                                <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("SMEmail")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>--%>
                                 
                                     <asp:TemplateField HeaderText="SMS Status">
                                        <ItemTemplate>
                                                <asp:Label ID="lblSmsStatus" runat="server" Text='<%#Eval("SMSmsStatus")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="SMS Content" ControlStyle-Width="500px" >
                                        <ItemTemplate  >
                                           <asp:Label ID="lblSMSContent" runat="server" Text='<%#Eval("SMSMSContent")%>' align="left"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Email Id">
                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("SMEmail")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Email Status">
                                        <ItemTemplate>
                                           <asp:Label ID="lblEmailStatus" runat="server" Text='<%#Eval("SMMailStatus")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Email Content" ControlStyle-Width="500px">
                                        <ItemTemplate>
                                           <asp:Label ID="lblEmailContent" runat="server" Text='<%#Eval("SMMAILContent")%>' align="left"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                           <asp:Label ID="lblEmailDate" runat="server" Text=' <%#Eval("SMFrmDat", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                            </Columns>
                                   <PagerStyle CssClass="pagination-grid no-print" />
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


