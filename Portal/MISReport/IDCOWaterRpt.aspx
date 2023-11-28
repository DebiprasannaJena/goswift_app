<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IDCOWaterRpt.aspx.cs" MasterPageFile="~/MasterPage/Application.master"
    Inherits="IDCOWaterRpt" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/decimalrstr.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
        });
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
        function ValidatePage() {
            var fDate = $("#ContentPlaceHolder1_txtFromdate").val();
            var tDate = $("#ContentPlaceHolder1_txtTodate").val();
            if (fDate == null || fDate == undefined || fDate == '') {
                jAlert('<strong>Please select from date.</strong>', 'GO-SWIFT');
                return false;
            }
            if (tDate == null || tDate == undefined || tDate == '') {
                jAlert('<strong>Please select to date.</strong>', 'GO-SWIFT');
                return false;
            }
            var dtmFromDate = new Date(GetDate(fDate));
            var dtmToDate = new Date(GetDate(tDate));

            if (dtmFromDate > dtmToDate) {
                jAlert('<strong>From date cannot be greater than to date.</strong>', 'GO-SWIFT');
                return false;
            }
            else {
                return true;
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
    </script>
    <style type="text/css">
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
        .buttn{float: RIGHT;
    margin: -4PX 10PX 0PX 7PX;}
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
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            overflow-y: scroll;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 1200px;
            height: 700px;
        }
    </style>
    <div class="content-wrapper">
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    IDCO Water Report Details</h1>
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

                   <%--  <ul class="dropdown-menu dropdown-menu-right">
                                <li>
                                        <asp:LinkButton ID="lnkPdf" runat="server" CssClass=" fa fa-file-pdf-o" 
                                            title="Export to PDF" onclick="lnkPdf_Click"></asp:LinkButton></li>
                                    <li><a class="PrintBtn" data-tooltip="Export To Excel" data-toggle="tooltip" data-title="Excel">
                                        <asp:LinkButton ID="lnkExport" CssClass="back" runat="server" 
                                            title="Export to Excel" onclick="lnkExport_Click"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                                    </a></li>
                                    <li><a class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print">
                                        <i class="panel-control-icon fa fa-print"></i></a></li>
                                        <li><a href="javascript:history.back()" data-tooltip="Back" data-toggle="tooltip"
                                        data-title="Back"><i class="panel-control-icon fa  fa-chevron-circle-left"></i></a>
                                    </li>
                                </ul>--%>
                    </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                     <div class="panel-body">
                     <div class="search-sec"> 


                                                <div class="form-group"> 
                                                 <div class="row"> 

                                                    <label class="col-sm-1">
                                                   From Date</label>

                                                   <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker">
                                                            <asp:TextBox ID="txtFromdate" CssClass="form-control" runat="server" TabIndex="1" AutoComplete="Off"></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>

                                                    <label class="col-sm-1">
                                                  To Date</label>

                                                  <div class="col-sm-2">
                                                 <span class="colon">:</span>
                                                 <div class="input-group  date datePicker">
                                                     <asp:TextBox ID="txtTodate"  CssClass="form-control" runat="server" TabIndex="2" AutoComplete="Off"></asp:TextBox>
                                                     <span class="input-group-addon"><i class="fa fa-calendar"></i></span>                                                     
                                                 </div>                                                 
                                                </div> 

                                                <label class="col-sm-1">
                                                            District</label>

                                                            <div class="col-sm-2">
                                                       <span class="colon">:</span>                                                      
                                                         <asp:DropDownList CssClass="form-control" ID="ddlDistrict" runat="server" TabIndex="3">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">                                                    
                                                     <asp:Button ID="btnSearch" CssClass="btn btn btn-add -sm" runat="server" 
                                                            Text="Search" onclick="btnSearch_Click" OnClientClick="return ValidatePage();"></asp:Button>
                                                    </div>                                                
                                                 
                                                 
                                                 </div>
                                                 </div> 



                                                 


                     </div>

                      
                                                   <div class="table-responsive" id="viewTable" runat="server">

                                                   <asp:Label ID="lblSearchDetails" runat="server"></asp:Label>
                                                   <br />
                                                    <asp:GridView ID="grdDepartment" runat="server" AutoGenerateColumns="false" 
                                                           EmptyDataText="No Records Found...." 
                                                           CssClass="table table-bordered table-hover" ShowFooter="true" 
                                                           onrowcommand="grdDepartment_RowCommand">
                                                   <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Service" DataField="strDeptName" FooterStyle-Font-Bold="true" />
                                                <asp:TemplateField HeaderText="Opening Balance" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>                                                        
                                                        <asp:Label ID="lblCarryFwdPending" runat="server" Text='<%#Eval("intCarryFwdPending")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Application received in current period" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTotalApplication" runat="server" Text='<%#Eval("intTotalApplication")%>' CommandName="re"></asp:LinkButton>                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approved" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkApproved" runat="server" Text='<%#Eval("intTotalApproved")%>' CommandName="ap"></asp:LinkButton>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rejected" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkRejected" runat="server" Text='<%#Eval("intTotalRejected")%>' CommandName="rj"></asp:LinkButton>                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. of Application with Query" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkQuery" runat="server" Text='<%#Eval("intTotalQueryRaised")%>' CommandName="query"></asp:LinkButton>                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total application pending in current period" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPending" runat="server" Text='<%#Eval("intTotalPending")%>' CommandName="cp"></asp:LinkButton>                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Applications pending" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAllPending" runat="server" Text='<%#Eval("intAllTotalPending")%>' CommandName="p"></asp:LinkButton>                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Application passed ORTPS Timeline" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkORTPS" runat="server" Text='<%#Eval("intTotalORTPSAtimelinePassed")%>' CommandName="ortps"></asp:LinkButton>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Avg. No. of days for approval" FooterStyle-Font-Bold="true"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%#Eval("intAvgDaysApproval")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>



                                                   </div>





                                                   <asp:Label ID="Label1" runat="server"></asp:Label>
                                              <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="Label1" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                             </cc1:ModalPopupExtender>

                                                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">
                                               
                                                    <asp:LinkButton ID="btnClose" runat="server" CssClass="buttn">X</asp:LinkButton> 
                                                    <br />
                                                    <div class="table-responsive" runat="server">
                                                    <asp:GridView ID="gvpopup" runat="server" EmptyDataText="No Records Found...." 
                                                        AutoGenerateColumns="false"  
                                                        CssClass="table table-bordered table-hover">
                                                    
                                                    <Columns>
                                                     <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Application Number">
                                                    <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLinkReportNo" runat="server" Text='<%# Bind("ApplicationNumber") %>' NavigateUrl='<%# Bind("AppURL") %>' Target="_blank"></asp:HyperLink>                                                                                                  
                                                    </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Investor Name">
                                                    <ItemTemplate>
                                                    
                                                    <asp:Label ID="lblinvestor" runat="server" Text='<%#Eval("PartyName")%>'></asp:Label>     
                                                    </ItemTemplate>
                                                    </asp:TemplateField>    
                                                     <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("CurrentStatus")%>'></asp:Label>                                                     
                                                    </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    
                                                     <asp:TemplateField HeaderText="Applied Date">
                                                    <ItemTemplate>
                                                     <asp:Label ID="lbldate" runat="server" Text='<%#Eval("RequestDate")%>'></asp:Label>     
                                                    
                                                    </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Capacity">
                                                    <ItemTemplate>
                                                     <asp:Label ID="lblcapacity" runat="server" Text='<%#Eval("CapacityPerday")%>'></asp:Label>     
                                                    
                                                    </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     
                                                     <asp:TemplateField HeaderText="Location">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lbldate" runat="server" Text='<%#Eval("vch_DistrictName")%>'></asp:Label>  
                                                    
                                                    </ItemTemplate>
                                                    </asp:TemplateField>                                                                                                      
                                                    </Columns>                                               
                                                    <PagerStyle CssClass="pagination-grid noprint" />
                                                    </asp:GridView>                                                   
                                                    </div>
                                                                                                      
                                                </asp:Panel>


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
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.panel-title a').click(function () {

                $(this).find('i').toggleClass('fa-minus fa-plus');
            });
        })
    </script>
    <!-- /.content -->
</asp:Content>
