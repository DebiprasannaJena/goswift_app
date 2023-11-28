<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MapPealwithServiceRpt.aspx.cs"
    Inherits="Portal_MISReport_MapPealwithServiceRpt" MasterPageFile="~/MasterPage/Application.master" %>

<%@ Register Src="~/includes/PagingUserControl.ascx" TagPrefix="uc1" TagName="PagingUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
    <uc1:PagingUserControl runat="server" ID="PagingUserControl" />
   <script language="javascript" type="text/javascript">     
         function validReport() {
             var frdt = $('#ContentPlaceHolder1_txtFromDate').val();
             var todt = $('#ContentPlaceHolder1_txtToDate').val();
             if (customParse(frdt) > customParse(todt)) {
                 alert('From Date Can Not Be Greater Than To Date');
                 document.getElementById('txtFromDate').focus();
                 return false;
             }            
        }
        function customParse(str) {
                 var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                 var n = months.length;
                 var re = /(\d{2})-([a-z]{3})-(\d{4})/i;
                 var matches;
                 while (n--) {
                     months[months[n]] = n;
                 }
                 matches = str.match(re);
                 return new Date(matches[3], months[matches[2]], matches[1]);
        }


    </script>
     <script src="../js/custom.js" type="text/javascript"></script>
        <script type="text/javascript">            
             $(document).ready(function () {
                $('.datePicker').datepicker({
                    format: "dd-M-yyyy",
                    changeMonth: true,
                    changeYear: true,
                    autoclose: true
                });
            });

            function setDateValues(strFromDate, strToDate) {
                var appendId = "ContentPlaceHolder1_";
                var fromDate = $("#" + appendId + "txtFromDate").val();
                var toDate = $("#" + appendId + "txtToDate").val();
                $("#" + appendId + "txtFromDate").datepicker({
                    format: "dd-M-yyyy",
                    changeMonth: true,
                    changeYear: true,
                    autoclose: true
                }).datepicker("setDate", fromDate);
                $("#" + appendId + "txtToDate").datepicker({
                    format: "dd-M-yyyy",
                    changeMonth: true,
                    changeYear: true,
                    autoclose: true
                }).datepicker("setDate", toDate);
            }
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                             $('.datePicker').datepicker({
                                    format: "dd-M-yyyy",
                                    changeMonth: true,
                                    changeYear: true,
                                    autoclose: true
                             });
                        }
                    });

                };
        </script>
    <style type="text/css">
        .page-numbers
        {
            border: 1px solid #d4dfe3;
            color: #2283c5;
            display: block;
            float: left;
            font-size: 9pt;
            margin-right: 3px;
            padding: 4px;
            text-decoration: none;
            background-color: #fafafa;
        }
        
        .page-numbers.current
        {
            background-color: #2283c5;
            border: 1px solid #d4dfe3;
            color: white;
            font-weight: bold;
        }
        
        .page-numbers.next, .page-numbers.prev
        {
            border: 1px solid #d4dfe3;
            font-size: 10pt;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    MIS Report</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Proposal</a></li><li><a>View</a></li></ul>
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
                                    <li>
                                        <asp:LinkButton ID="lnkPdf" runat="server" CssClass=" fa fa-file-pdf-o" title="Export to PDF" OnClick="lnkPdf_Click"></asp:LinkButton></li>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" title="Export to Excel" OnClick="lnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
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
                                    <div class="col-sm-2">
                                                <label for="State">
                                                    From Date
                                                </label>
                                                <div class="input-group  date datePicker">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtFromDate" name="txtFromDate"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <label for="State">
                                                    To Date
                                                </label>
                                                <div class="input-group  date datePicker">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtToDate"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                            </div>
                                    <div class="col-sm-2">
                                        <label for="State">
                                            Investment Amount Range 
                                        </label>
                                        <asp:DropDownList ID="drpInvestmentAmt" runat="server" CssClass="form-control" TabIndex="3">
                                           
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1">
                                        <label for="State">
                                          	PEAL no.
                                        </label>
                                        <asp:TextBox ID="txtpeal" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>                                       
                                    </div>
                                     <div class="col-sm-3">
                                        <label for="State">
                                          	Company Name
                                        </label>
                                         <asp:TextBox ID="txtcompany" runat="server" CssClass="form-control" TabIndex="5"></asp:TextBox>   
                                    </div>
                                     <div class="col-sm-2">
                                        <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm" runat="server" Text="Search" TabIndex="6" OnClientClick="validReport();" OnClick="btnSearch_Click">
                                        </asp:Button> 
                                          <asp:Button ID="btnReset" Style="margin-top: 22px" CssClass="btn btn btn-add btn-sm" runat="server" Text="Reset" TabIndex="7" OnClick="btnReset_Click">
                                        </asp:Button>
                                    </div>                                          
                                           
                                </div>                               
                            </div> 
                            <div id="divPagingShow" runat="server" visible="false"> 
                                <div class="form-group row">  
                               <div class="col-sm-3" style="text-align:right;">
                                <asp:LinkButton ID="lnkbtnmode" runat="server" Text="All" OnClick="lnkbtnmode_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Literal ID="litStart" runat="server">1</asp:Literal> -
                                    <asp:Literal ID="litEnd" runat="server">9</asp:Literal>
                                     of
                                    <asp:Literal ID="litTotalRecord" runat="server"></asp:Literal>
                                   <div style="margin-top:-23px;">
                                <asp:DropDownList ID="ddlrecord" runat="server" CssClass="form-control" Width="128px" OnSelectedIndexChanged="ddlrecord_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                     <asp:ListItem Value="50">50</asp:ListItem>
                                     <asp:ListItem Value="100">100</asp:ListItem>
                                     <asp:ListItem Value="150">150</asp:ListItem>
                                     <asp:ListItem Value="200">200</asp:ListItem>
                                     <asp:ListItem Value="250">250</asp:ListItem>
                                     <asp:ListItem Value="300">300</asp:ListItem>
                                     <asp:ListItem Value="350">350</asp:ListItem>
                                     <asp:ListItem Value="400">400</asp:ListItem>
                                     <asp:ListItem Value="450">450</asp:ListItem>
                                </asp:DropDownList>
                                       </div>
                                   </div>
                                    </div>
                                </div>
                              <div class="table-responsive" id="viewTable" runat="server">                               
                                <asp:GridView ID="grdPealDetails" runat="server" class="table table-bordered table-hover"
                                    AutoGenerateColumns="false" EmptyDataText="No Record(s) found...." 
                                    DataKeyNames="INT_INVESTOR_ID" OnRowDataBound="grdPealDetails_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                 <asp:Label ID="lblsl" runat="server" Text='<%#(grdPealDetails.PageIndex * grdPealDetails.PageSize) + (grdPealDetails.Rows.Count + 1)%>'></asp:Label>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proposal No">
                                            <ItemTemplate>                                                
                                             <asp:Label ID="lblpeal" runat="server" Text='<%#Eval("vchProposalNo")%>'></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>                                                
                                             <asp:Label ID="lblcompany" runat="server" Text='<%#Eval("VCH_INV_NAME")%>'></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>                                                
                                             <asp:Label ID="lbllocation" runat="server" Text='<%#Eval("vchDistrictName")%>'></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SLSWCA approval Date">
                                            <ItemTemplate>                                                
                                             <asp:Label ID="lblslswcadate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Proposed Investment (INR crore)">
                                            <ItemTemplate>                                                
                                             <asp:Label ID="lblinvestment" runat="server" Text='<%#Eval("decProposedAnnualCapacity")%>'></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proposed Employment">
                                            <ItemTemplate>                                                
                                             <asp:Label ID="lblemployment" runat="server" Text='<%#Eval("intTotalProp")%>'></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div style="max-height:300px;overflow-y:auto;margin: 15px 0px;">
                                                <asp:GridView ID="grdServiceDetails" runat="server" class="table table-bordered table-hover" 
                                                    AutoGenerateColumns="false" EmptyDataText="No Service Applied"
                                                    DataKeyNames="INT_SERVICEID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Applications Applied">
                                                            <ItemTemplate>                                                               
                                                             <asp:Label ID="lblaplied" runat="server" Text='<%#Eval("VCH_SERVICENAME")%>'></asp:Label>                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Applied Date">
                                                            <ItemTemplate>                                                
                                                             <asp:Label ID="lblaplieddate" runat="server" Text='<%#Eval("Applieddate")%>'></asp:Label>                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Applications Approved">
                                                            <ItemTemplate>                                                
                                                             <asp:Label ID="lblapproved" runat="server" Text='<%#Eval("Approved")%>'></asp:Label>                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approval Date">
                                                            <ItemTemplate>                                                
                                                             <asp:Label ID="lblapprovedate" runat="server" Text='<%#Eval("Approveddate")%>'></asp:Label>                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Applications Rejected">
                                                            <ItemTemplate>                                                
                                                             <asp:Label ID="lblreject" runat="server" Text='<%#Eval("Rejected")%>'></asp:Label>                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejection Date">
                                                            <ItemTemplate>                                                
                                                             <asp:Label ID="lblrejectdate" runat="server" Text='<%#Eval("Rejecteddate")%>'></asp:Label>                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Applications under Query">
                                                            <ItemTemplate>                                                
                                                             <asp:Label ID="lblquery" runat="server" Text='<%#Eval("underQuery")%>'></asp:Label>                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Query Raised Date">
                                                            <ItemTemplate>                                                
                                                             <asp:Label ID="lblquerydate" runat="server" Text='<%#Eval("Querydate")%>'></asp:Label>                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                                    </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>                                     
                                </asp:GridView>                  
                              
                            </div>
                             <div style="float: right;" class="noPrint" id="divPaging" runat="server" visible="false">
                                             <uc1:PagingUserControl ID="uclPager" runat="server" />
                                             <asp:HiddenField ID="hdnCurrentIndex" runat="server" Value="Blank Value" />
                                  </div>

                                   <asp:UpdateProgress ID="updateProgress" runat="server">
                                         <ProgressTemplate>
                                             <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                                                 right: 0; left: 0; z-index: 9999999; -ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=50)';
                                                 filter: alpha(opacity=50); -moz-opacity: 0.5; opacity: 0.5; background-color: #000;">
                                                 <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/Loading.gif" AlternateText="Loading ..."
                                                     ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 10%; left: 40%;" />
                                             </div>
                                         </ProgressTemplate>
                                     </asp:UpdateProgress>
                        </div>


                       </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
       
    </div>
</asp:Content>
