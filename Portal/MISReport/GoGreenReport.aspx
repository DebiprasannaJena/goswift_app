<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="GoGreenReport.aspx.cs" Inherits="Portal_MISReport_GoGreenReport" %>


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
             // Check if from date and to date are not empty
             var fromDate = $('#ContentPlaceHolder1_TxtFromdate').val();
             var toDate = $('#ContentPlaceHolder1_TxtTodate').val();

             // Check if from date is greater than to date
             if (new Date(fromDate) > new Date(toDate)) {
                 jAlert('<strong>From date cannot be greater than to date.</strong>', projname);
                 $("#popup_ok").click(function () { $("#ContentPlaceHolder1_TxtFromdate").focus(); });
                 return false;
             }
             // Check if from date is greater than current date
             if (new Date(fromDate) > new Date()) {
                 jAlert('<strong>From date cannot be greater than current date.</strong>', projname);
                 $("#popup_ok").click(function () { $("#ContentPlaceHolder1_TxtFromdate").focus(); });
                 return false;
             }
             // Check if to date is greater than current date
             if (new Date(toDate) > new Date()) {
                 jAlert('<strong>To date cannot be greater than current date.</strong>', projname);
                 $("#popup_ok").click(function () { $("#ContentPlaceHolder1_TxtTodate").focus(); });
                 return false;
             }

             return true; // Validation passed
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

         function ShowDistrictwise(dstid, status, projectId,fromdate,todate,dstname) {
             debugger;

             document.getElementById('ContentPlaceHolder1_myIframe').src = "GoGreenChildDetailsRpt.aspx?DistrictId=" + dstid + "&Status=" + status + "&ProjectId=" + projectId + "&FromDate=" + fromdate + "&ToDate=" + todate + "&DistrictName=" + dstname;
             
         }

     </script>
     <script type="text/javascript">
         function search() {
             var districtName = $('#<%= drpDristrict.ClientID %>').val();
            var status = $('#<%= ddlStatus.ClientID %>').val();
            $('#<%= btnSearch.ClientID %>').click();
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
                    District Wise Go-Green Report</h1>
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
                        <div class="panel-body">
                                <div class="search-sec NOPRINT">
                                <div class="form-group row NOPRINT">
                                   
                                       
                                            <div class="col-sm-3">
                                                <label for="District">
                                                    District Name</label>
                                                <asp:DropDownList ID="drpDristrict" runat="server"  CssClass="form-control"  >
                                                    <asp:ListItem Text="-Select-" Value=""></asp:ListItem>                         
                                                    <asp:ListItem Text="Angul" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Balangir" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Balasore" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Bargarh" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Bhadrak" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Boudh" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Cuttack" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Debagarh" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="Dhenkanal" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="Gajapati" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Ganjam" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="Jagatsinghpur" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="Jajpur" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="Jharsuguda" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="Kalahandi" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="Kandhamal" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="Kendrapara" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="Kendujhar" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="Khorda" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="Koraput" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="Malkangiri" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="Mayurbhanj" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="Nabarangpur" Value="23"></asp:ListItem>
                                                    <asp:ListItem Text="Nayagarh" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="Nuapada" Value="125"></asp:ListItem>
                                                    <asp:ListItem Text="Puri" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="Rayagada" Value="27"></asp:ListItem>
                                                    <asp:ListItem Text="Sambalpur" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="Sundargarh" Value="29"></asp:ListItem>
                                                    <asp:ListItem Text="Subarnapur" Value="30"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>                                          
                                       <div class="col-sm-3">
                                            <label for="Service">
                                                    Status </label>
                                           <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlStatus" runat="server" >
                                            <asp:ListItem Value="">---Select---</asp:ListItem>
                                            <asp:ListItem Value="Approved"> Approved </asp:ListItem>
                                            <asp:ListItem Value="Rejected"> Rejected </asp:ListItem>
                                            <asp:ListItem Value="Pending"> Pending </asp:ListItem>                                      
                                        </asp:DropDownList>
                                       </div>

                                     <div class="col-sm-3" >
                                        <label for="State">
                                            From Date
                                        </label>
                                        <div class="input-group date datePicker">
                                            <asp:TextBox runat="server" class="form-control" ID="TxtFromdate" name="TxtFromdate"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" >
                                        <label for="State">
                                            To Date
                                        </label>
                                        <div class="input-group date datePicker">
                                            <asp:TextBox runat="server" class="form-control" ID="TxtTodate"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                    </div>

                                   <div class="col-sm-3 ">
                                    <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm"
                                        runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return ValidatePage();" />
                                </div>
                                </div>
                            </div>
                        
                       
                                    <div class="table-responsive">
                                        <asp:Label ID="LblSearchDetails" runat="server" Font-Bold="true"></asp:Label>
                                        <div style="display: inline-block; text-align: right; width: 100%">
                                            <asp:LinkButton ID="LbtnAll" OnClick="LbtnAll_Click" runat="server"  Visible="true" CssClass="" Text="All" ></asp:LinkButton>
                                            
                                            
                                            <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                        </div>
                                                    <div class="table-responsive" id="Div1" runat="server">
                                   <asp:GridView ID="GrdGoGreenRpt" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="GrdGoGreenRpt_PageIndexChanging"  EmptyDataText="No Records Found...."
                                            CssClass="table table-bordered table-hover"  AllowPaging="true"   ShowFooter="false" Width="98%" PageSize="100">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GrdGoGreenRpt.PageIndex * GrdGoGreenRpt.PageSize) + (GrdGoGreenRpt.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>                                              
                                              
                                                <asp:TemplateField HeaderText="District Name">
                                                    <ItemTemplate>
                                                       
                                                        <asp:Label ID="LblDistrictName" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField> 

                                                  <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>                                                    
                                                        <asp:Label ID="LblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                  
                                        
                                                 <asp:TemplateField HeaderText="Approved">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" data-target="#Div3" style="color:green;font-weight:bold;" title="" onclick="ShowDistrictwise('<%# Eval("DistrictId") %>','<%#Eval("stsApprove")%>','<%#Eval("ProjectId")%>','<%#Eval("FromDate")%>','<%#Eval("ToDate")%>','<%#Eval("DistrictName")%>');">
                                                    <asp:Label ID="LblApprovedCount" runat="server" Text='<%#Eval("ApprovedCount")%>' Visible="true"></asp:Label></a>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pending">
                                            <ItemTemplate>  
                                                <a href="#Div3" data-toggle="modal" style="color:darkorange;font-weight:bold;" data-target="#Div3" title="" onclick="ShowDistrictwise('<%# Eval("DistrictId") %>','<%#Eval("stsPending")%>','<%#Eval("ProjectId")%>','<%#Eval("FromDate")%>','<%#Eval("ToDate")%>','<%#Eval("DistrictName")%>');">
                                                    <asp:Label ID="LblPendingCount" runat="server" Text='<%#Eval("PendingCount")%>' Visible="true"></asp:Label></a>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rejected">
                                            <ItemTemplate>
                                                <a href="#Div3" data-toggle="modal" style="color:red;font-weight:bold;" data-target="#Div3" title="" onclick="ShowDistrictwise('<%# Eval("DistrictId") %>','<%#Eval("stsReject")%>','<%#Eval("ProjectId")%>','<%#Eval("FromDate")%>','<%#Eval("ToDate")%>','<%#Eval("DistrictName")%>');">
                                                    <asp:Label ID="LblRejectCount" runat="server" Text='<%#Eval("RejectedCount")%>' Visible="true"></asp:Label></a>
                                               
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Project" >
                                                    <ItemTemplate >                                                                                                            
                                                        <asp:Label ID="LblTotalCount" style="color:black;font-weight:bold;" runat="server" Text='<%# Eval("TotalCount") %>'></asp:Label> 
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
                                            CssClass="table table-bordered table-hover" AllowPaging="false" PageSize="100"  ShowFooter="false" Width="100%">
                                           <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GridViewExcel.PageIndex * GridViewExcel.PageSize) + (GridViewExcel.Rows.Count + 1)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>                                              
                                              
                                                <asp:TemplateField HeaderText="District Name">
                                                    <ItemTemplate>
                                                       
                                                        <asp:Label ID="DistrictName" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField> 

                                                  <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>                                                    
                                                        <asp:Label ID="ProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Approved">
                                            <ItemTemplate>                                 
                                                  <asp:Label ID="ApprovedCount" runat="server" Text='<%# Eval("ApprovedCount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pending">
                                            <ItemTemplate>                                    
                                                <asp:Label ID="PendingCount" runat="server" Text='<%# Eval("PendingCount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rejected">
                                            <ItemTemplate>                                      
                                                 <asp:Label ID="RejectedCount" runat="server" Text='<%# Eval("RejectedCount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Total Project" >
                                                    <ItemTemplate >                                                                                                              
                                                        <asp:Label ID="LblTotalCount" style="color:black;font-weight:bold;" runat="server" Text='<%# Eval("TotalCount") %>'></asp:Label> 
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
           
   <div id="Div3" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg" style="width: 90%;">
                <!-- Modal content-->
                <div class="modal-content modal-primary ">
                    <div class="modal-header bg-red">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">
                            <asp:Label ID="lbldet1" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <iframe name="myIframe" id="myIframe" style="width: 100%; height: 450px;" runat="server"></iframe>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

