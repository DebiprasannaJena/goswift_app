<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="GoGreenDetailsRpt.aspx.cs" Inherits="Portal_MISReport_GoGreenDetailsRpt" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/decimalrstr.js" type="text/javascript"></script>

     <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datePicker').datepicker({
                    format: 'dd-M-yyyy',
                    autoclose: true
                });
            }
        });



        
         function ValidatePage() {
             if (new Date($('#ContentPlaceHolder1_TxtTodate').val()) > new Date()) {
                 jAlert('<strong>From date cannot be greater than current date.</strong>', projname);
                 $("#popup_ok").click(function () { $("#ContentPlaceHolder1_TxtFromdate").focus(); });
                 return false;
             }
             if (new Date($('#ContentPlaceHolder1_TxtTodate').val()) > new Date()) {
                 jAlert('<strong>To date cannot be greater than current date.</strong>', projname);
                 $("#popup_ok").click(function () { $("#ContentPlaceHolder1_TxtTodate").focus(); });
                 return false;
             }
             if (new Date($('#ContentPlaceHolder1_TxtFromdate').val()) > new Date($('#ContentPlaceHolder1_TxtTodate').val())) {
                 jAlert('<strong>From date cannot be greater than to date.</strong>', projname);
                 $("#popup_ok").click(function () { $("#ContentPlaceHolder1_TxtFromdate").focus(); });
                 return false;
             }
         }


        function GetDate(str) {
            var arr = str.split('-');
            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
            var i = 1;
            for (i; i <= months.length; i++) {
                if (months[i] == arr[1]) {
                    break;
                }
            }
            var formatddate = i + '/' + arr[0] + '/' + arr[2];
            return formatddate;
         }

         function ViewModal(ModalPath) {
             $('#DetailsModal').modal();
             $('#DetailsModal .modal-body iframe').attr('src', ModalPath);
         }

     </script>
   
    <style type="text/css">
    .bold-cell {
        font-weight: bold;
    }
   </style>
      <div class="content-wrapper">
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    District  Report Details</h1>
            </div>
        </section>
        <section class="content">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
        <div class="row">

                    <div class="col-sm-12 " data-lobipanel-child-inner-id="CgbyYkSXVZ">
                    <div class="panel panel-bd lobidisable">
                    <div class="panel-heading noprint">
                     <div class="dropdown">

                     <ul class="dropdown-menu dropdown-menu-right">
                                <li>
                                        <asp:LinkButton ID="lnkPdf" OnClick="lnkPdf_Click"  runat="server" CssClass=" fa fa-file-pdf-o" 
                                            title="Export to PDF" ></asp:LinkButton></li>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="lnkExport" OnClick="lnkExport_Click"  CssClass="back" runat="server" 
                                            title="Export to Excel" ><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                                    </a></li>
                                    <li><a class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print">
                                        <i class="panel-control-icon fa fa-print"></i></a></li>
                                        <li><a href="javascript:history.back()" data-tooltip="Back" data-toggle="tooltip"
                                        data-title="Back"><i class="panel-control-icon fa  fa-chevron-circle-left"></i></a>
                                    </li>
                                </ul>
                    </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                  <div class="table-responsive">
                                        <asp:Label ID="LblSearchDetails" runat="server" Font-Bold="true"></asp:Label>
                                        <div style="display: inline-block; text-align: right; width: 100%">
                                            <asp:LinkButton ID="LbtnAll" OnClick="LbtnAll_Click" runat="server"  Visible="true" CssClass="" Text="All" ></asp:LinkButton>
                                            
                                            
                                            <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                        </div>
                                                    <div class="table-responsive" id="Div1" runat="server">
                                   <asp:GridView ID="GrdDetailsRpt" OnRowDataBound="GrdDetailsRpt_RowDataBound"  runat="server" AutoGenerateColumns="false" OnPageIndexChanging="GrdDetailsRpt_PageIndexChanging"  EmptyDataText="No Records Found...."
                                            CssClass="table table-bordered table-hover"  AllowPaging="true"   ShowFooter="false" Width="98%" PageSize="50">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GrdDetailsRpt.PageIndex * GrdDetailsRpt.PageSize) + (GrdDetailsRpt.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Application No">
                                                    <ItemTemplate>
                                                       
                                                        <asp:Label ID="ApplicationNo" runat="server" Text='<%# Eval("Application_No") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                       
                                                        <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField> 

                                                 <asp:TemplateField HeaderText="Developer Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DeveloperName" runat="server" Text='<%# Eval("Developer_Name") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Project">
                                                    <ItemTemplate>                                                    
                                                        <asp:Label ID="project" runat="server" Text='<%# Eval("project") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CreatedOn">
                                                    <ItemTemplate>
                                                     
                                                        <asp:Label ID="CreatedOn" runat="server" Text='<%# Eval("CreatedOn") %>'></asp:Label>
                                                   </ItemTemplate>
                                                     
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="District">
                                                    <ItemTemplate>
                                                           <asp:HiddenField ID="HidDistrict" runat="server" Value='<%# Eval("District") %>' />
                                                         <asp:HyperLink ID="hypDistrict" runat="server" Text='<%#Eval("District")%>'></asp:HyperLink>       
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                           </asp:GridView>
                                             </div>
                                           </div>


                  <div class="table-responsive" id="Div2" runat="server" visible="false">
                                   <asp:GridView ID="GridViewExcel" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                                            CssClass="table table-bordered table-hover" AllowPaging="false" PageSize="50"  ShowFooter="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GridViewExcel.PageIndex * GridViewExcel.PageSize) + (GridViewExcel.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Application No">
                                                    <ItemTemplate>
                                                       
                                                        <asp:Label ID="ApplicationNo" runat="server" Text='<%# Eval("Application_No") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                       
                                                        <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField> 

                                                 <asp:TemplateField HeaderText="Developer Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DeveloperName" runat="server" Text='<%# Eval("Developer_Name") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Project">
                                                    <ItemTemplate>                                                    
                                                        <asp:Label ID="project" runat="server" Text='<%# Eval("project") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CreatedOn">
                                                    <ItemTemplate>
                                                     
                                                        <asp:Label ID="CreatedOn" runat="server" Text='<%# Eval("CreatedOn") %>'></asp:Label>
                                                   </ItemTemplate>
                                                     
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="District">
                                                    <ItemTemplate>
                                                           <asp:HiddenField ID="HidDistrict" runat="server" Value='<%# Eval("District") %>' />
                                                         <asp:HyperLink ID="hypDistrict" runat="server" Text='<%#Eval("District")%>'></asp:HyperLink>       
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                           </asp:GridView>
                                             </div>
                     
                                     </ContentTemplate>
                                   </asp:UpdatePanel>
                  
                                  </div>
                                  </div>

                               </div>
                          </section>
                        </div>
    <div id="DetailsModal" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content modal-primary ">
                        <div class="modal-header bg-red">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lbldet1" runat="server" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body" style="height: 500px;">
                            <iframe name="myIframe" id="myIframe" width="100%" style="height: 500px;" runat="server">
                            </iframe>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

