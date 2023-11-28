<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewIssueRegistration.aspx.cs" Inherits="Portal_HelpDesk_ViewIssueRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/decimalrstr.js" type="text/javascript"></script>
    <style></style>--%>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#btnSubmit').click(function () {

            });
            $('.datePicker').datepicker({
                autoclose: true,
                format: 'dd-M-yyyy'
            });



        });
        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._';
            }
            if (allow == 'Address') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#-,./;\'';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
            }
            var k;
            k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
            if (k != 13 && k != 8 && k != 0) {
                if ((e.ctrlKey == false) && (e.altKey == false)) {
                    return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }
        }


        $(document).ready(function () {
            $("a").click(function (event) {
                debugger;
                var href = $(this).attr('href');
                //$(this).attr('href', '#');
                var Filename = href.split('/');
                if (Filename[4] != undefined) {
                    if (Filename[4].indexOf('.pdf') > -1) {
                        $('#ContentPlaceHolder1_hdnFileNames').val(Filename[4]);
                        document.getElementById('<%= btnDownload.ClientID %>').click();
                        event.preventDefault();
                    }
                };


            });
            $('.ddluser').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });


            $('.btnsubmit').click(function () {
                if ($(this).closest('tr').find('.ddlsts').val() == "0") {
                    jAlert('<strong>Please Select Status  !</strong>', 'SWP');
                    $(this).closest('tr').find('.ddlsts').focus();
                    return false;
                }
                if ($(this).closest('tr').find('.remark').val() == "") {
                    jAlert('<strong>Please Select Remark  !</strong>', 'SWP');
                    $(this).closest('tr').find('.remark').focus();
                    return false;
                }
            });
        });
    </script>
    <style>
        .control-label
        {
            border: 1px solid #b9bdbf;
            padding: 6px 10px;
            border-radius: 2px;
            height: 31px;
            width: 100%;
            background: #f9f9f9;
            display: block;
            margin: 0px;
        }
    </style>
    <style type="text/css" media="all">
/* fix rtl for demo */
.chosen-rtl .chosen-drop { left: -9000px; }
.chosen-container .chosen-container-single .chosen-single{ width:100% !important;;}
.searchbox {
background-color: #def3ff;
padding: 8px;
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
                <h1>
                    Add Take Action</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>HelpDesk</a></li><li><a>Helpdesk View</a></li></ul>
            </div>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="Issue_Registration.aspx"><i class="fa fa-plus"></i>Add</a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ViewIssueRegistration.aspx"><i class="fa fa-file"></i>
                                    View</a>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                       <ContentTemplate>   
                        <div class="panel-body">
                            <div class="search-sec">
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            Type</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control dpt" 
                                                onselectedindexchanged="ddltype_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">Department</asp:ListItem>
                                                <asp:ListItem Value="2">Investor</asp:ListItem>
                                                <asp:ListItem Value="3">Other</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-2">
                                            Ticket No</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtIssue" MaxLength="20" CssClass="form-control" runat="server"
                                                Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            From Date</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <div class="input-group  date datePicker" id="datePicker1">
                                                <asp:TextBox ID="txtFromdate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                        <label class="col-sm-2">
                                            To Date</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <div class="input-group  date datePicker">
                                                <asp:TextBox ID="txtTodate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                  <%--      <div class="col-sm-2">
                                            <asp:Button ID="btnShow" runat="server" Text="Search" class="btn btn-add btn-sm"
                                                OnClick="btnShow_Click"></asp:Button>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                   <div class="row">
                                    <label class="col-sm-2">Category </label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlCategory" 
                                            runat="server" AutoPostBack="True" onselectedindexchanged="ddlCategory_SelectedIndexChanged" 
                                            >
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                           <%-- <asp:ListItem Value="1">Department</asp:ListItem>
                                                            <asp:ListItem Value="2">Investor</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                 </div>
                                  <label class="col-sm-2">Sub Category </label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlSubcategory" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                           <%-- <asp:ListItem Value="1">Department</asp:ListItem>
                                                            <asp:ListItem Value="2">Investor</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                 </div>
                                 
                                
 </div>
                                 </div>
                                <div class="form-group">
                                    <div class="row">
                                      <label class="col-sm-2">
                                            Status</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control dpt">
                                            </asp:DropDownList>
                                        </div>
                                 
                                        <div class="col-sm-2">
                                            <asp:Button ID="Button1" runat="server" Text="Search" class="btn btn-add btn-sm"
                                                OnClick="btnShow_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div align="right">
                                <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                    OnClick="lbtnAll_Click"></asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:Label ID="lblPaging" runat="server"></asp:Label>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server"
                                    AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvService_PageIndexChanging"
                                    ShowFooter="true" Width="100%" EmptyDataText="No Record(s) Found..." PageSize="10"
                                    DataKeyNames="int_IssueId,vchIssueNo,vch_FIleUpload,Email,int_SubcategoryId" OnRowDataBound="gvService_RowDataBound"
                                    OnRowCreated="gvService_RowCreated">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Sl No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsl" runat="server" Text='<%#(gvService.PageIndex * gvService.PageSize) + (gvService.Rows.Count + 1)%>'></asp:Label>
                                                <%--<%# Container.DataItemIndex + 1 %>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Ticket No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkTkn" ForeColor="Blue" Text='<%#Eval("vchIssueNo") %>' runat="server"
                                                    ToolTip="IssueNo" data-toggle="modal" data-target='<%# "#"+Eval("vchIssueNo") %>'></asp:HyperLink>
                                                <div class="modal fade" id='<%#Eval("vchIssueNo") %>' tabindex="-1" role="dialog"
                                                    aria-hidden="true">
                                                    <div class="modal-dialog modal-lg">
                                                        <div class="modal-content">
                                                            <div class="modal-header modal-header-primary">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                    ×</button>
                                                                <h3>
                                                                    <i class="fa fa-user m-r-5"></i>Details</h3>
                                                            </div>
                                                            <div class="modal-body">
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
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Issue No</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="lblProposalId">
                                                                                                    <span>
                                                                                                        <%#Eval("vchIssueNo")%></span></label>
                                                                                            </div>
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                User Name</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label6">
                                                                                                    <span>
                                                                                                        <%#Eval("vch_UserName")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Request date</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="lblInvestorName">
                                                                                                    <span>
                                                                                                        <%#Eval("dtmCreatedOn")%></span></label>
                                                                                            </div>
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Type</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="lblIndustryType">
                                                                                                    <span>
                                                                                                        <%#Eval("vch_Type")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Mobile</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label1">
                                                                                                    <span>
                                                                                                        <%#Eval("VchMobile")%></span></label>
                                                                                            </div>
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Priority
                                                                                            </label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label2">
                                                                                                    <span>
                                                                                                        <%#Eval("vch_Priority")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Category</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label3">
                                                                                                    <span>
                                                                                                        <%#Eval("CategoryName")%></span></label>
                                                                                            </div>
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                SubCategory
                                                                                            </label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label4">
                                                                                                    <span>
                                                                                                        <%#Eval("SubCategory")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                                 <label class="col-md-2" style="color: #000000">File Upload</label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                     <asp:HyperLink  ID="hplnkCertificate" Text="File"  Target="_blank" NavigateUrl='<%# "../ApprovalDocs/"+ Eval("vch_FIleUpload")%>' runat="server" CssClass='<%# (Eval("vch_FIleUpload")=="" || Eval("vch_FIleUpload")==null) ? "hidden":""  %>'   ></asp:HyperLink>
                                  
                                    </div>
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Email
                                                                                            </label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" runat="server">
                                                                                                    <span>
                                                                                                        <%#Eval("Email")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Address</label>
                                                                                            <div class="col-md-10">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label7">
                                                                                                    <span>
                                                                                                        <%#Eval("Address")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Issue Details</label>
                                                                                            <div class="col-md-10">
                                                                                                <span class="colon">:</span>
                                                                                                 <asp:HiddenField ID="hdnIssuedetis" runat="server" Value='<%#Eval("vch_IssueDetails") %>' />
                                                                                                <label class="form-control-static" id="lblIndustriesName">
                                                                                                    <span>
                                                                                                        <%#Eval("vch_IssueDetails")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Issue Intimented</label>
                                                                                            <div class="col-md-10">
                                                                                                <span class="colon">:</span>
                                                                                        <asp:GridView runat="server" CssClass ="table" DataKeyNames="vch_FIleUpload" ID="gvIntimateSent" AutoGenerateColumns="false" >
           <Columns>
              <asp:TemplateField >
                <HeaderStyle  Width="50px" ></HeaderStyle>
              <HeaderTemplate>
              
             SL No.
              </HeaderTemplate>
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
       <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                User Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob2" Text='<%#Eval("vch_UserName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
     <%--<asp:TemplateField>
                                            <HeaderTemplate>
                                         Authority Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblVCH_DIST" runat="server" Text='<%#Eval("VCHUSERNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>--%>
   
           <asp:TemplateField>
                                            <HeaderTemplate>
                                           Status
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblVCH_COMPLIANT_STATUS" runat="server" Text='<%#Eval("VCH_STATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
     <%--       <asp:BoundField DataField="VCH_COMPLIANT_STATUS" HeaderText="Type of Action" />--%>
          
             <asp:TemplateField > 
                 <HeaderTemplate>
                                        Remarks 
                                            </HeaderTemplate>
                        <ItemTemplate>
                             <asp:Label ID="lblRemark" runat="server" Text= '<%# Eval("strRemark").ToString().Length>75 ? (Eval("strRemark") as string).Substring(0,75) : Eval("strRemark")  %>'  tooltip = '<%# Eval("strRemark") %> '  />
                        </ItemTemplate>
                    </asp:TemplateField>
           <asp:TemplateField HeaderText="Attached Document">
            <ItemTemplate>
               <asp:HyperLink Text="File" Target="_blank" NavigateUrl='<%# "../ApprovalDocs/"+ Eval("vch_FIleUpload")%>' cssClass='<%# (Eval("vch_FIleUpload")=="" || Eval("vch_FIleUpload")==null) ? "hidden": " " %>'  runat="server" 
                   />
              
            </ItemTemplate>
            </asp:TemplateField>
           <%-- <asp:BoundField DataField="DTMAPPROXRESDATE" HeaderText="Possible Resolve Date" DataFormatString="{0:dd-MMM-yyyy}" />--%>
        <%--      <asp:TemplateField>
                                            <HeaderTemplate>
                                            <%=Resources.Resource.Action_Taken_on %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblVCH_COMPLIANT_STATUS" runat="server" Text='<%#Eval("DTMUPDATEDON", "{0:dd-MMM-yyyy}") %>' ></asp:Label>
                                            </ItemTemplate>
                                              </asp:TemplateField>--%>
<%--            <asp:BoundField DataField="DTMUPDATEDON" HeaderText="Action Date" DataFormatString = "{0:dd-MMM-yyyy h:mm tt}" />--%>
            <asp:BoundField DataField="dtmCreatedOn" HeaderText="Date Of Action" DataFormatString = "{0:dd-MMM-yyyy}" />
            </Columns>
            </asp:GridView>
                                                                                            </div>
                                                                                        </div>
                                                                                        </div>
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
                                                                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">
                                                                    Close</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                                <!-- /.modal-content -->
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                User Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob2" Text='<%#Eval("vch_UserName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Type
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob" Text='<%#Eval("vch_Type") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Requested Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmobMM" Text='<%#Eval("dtmCreatedOn") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Resolution Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmobMM344" Text='<%#Eval("OtherName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                Status
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob3" Text='<%#Eval("status") %>' runat="server"></asp:Label>
                                             
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Take Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnIssueid" runat="server" Value='<%# Eval("int_IssueId")%>'>
                                                </asp:HiddenField>
                                                 <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("status")%>'>   </asp:HiddenField>
                                                <asp:LinkButton ID="lnkbtn" Text="Take Action" runat="server" class="label-warning label label-default"
                                                    data-toggle="modal" data-target='<%# "#"+Eval("int_IssueId") %>'>Take Action</asp:LinkButton>
                                                <div class="modal fade" id='<%#Eval("int_IssueId") %>' tabindex="-1" role="dialog"
                                                    aria-hidden="true">
                                                    <div class="modal-dialog modal-lg">
                                                        <div class="modal-content">
                                                            <div class="modal-header modal-header-primary">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                    ×</button>
                                                            </div>
                                                            <div class="modal-body" style="height: 450px; overflow: hidden; overflow-y: scroll;">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <fieldset>
                                                                            <div class="panel panel-bd ">
                                                                                <div class="panel-heading text-center">
                                                                                    Details
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Issue No</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="lblProposalId">
                                                                                                    <span>
                                                                                                        <%#Eval("vchIssueNo")%></span></label>
                                                                                            </div>
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                User Name</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="lblIndustriesName">
                                                                                                    <span>
                                                                                                        <%#Eval("vch_UserName")%></span></label>
                                                                                                        <asp:HiddenField ID="hdnUsername"  Value='<%#Eval("vch_UserName")%>' runat="server"></asp:HiddenField>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Request date</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="lblInvestorName">
                                                                                                    <span>
                                                                                                        <%#Eval("dtmCreatedOn")%></span></label>
                                                                                            </div>
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Type</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="lblIndustryType">
                                                                                                    <span>
                                                                                                        <%#Eval("vch_Type")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Mobile</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label1">
                                                                                                    <span>
                                                                                                        <%#Eval("VchMobile")%></span></label>
                                                                                            </div>
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Priority
                                                                                            </label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label2">
                                                                                                    <span>
                                                                                                        <%#Eval("vch_Priority")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Category</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label3">
                                                                                                    <span>
                                                                                                        <%#Eval("CategoryName")%></span></label>
                                                                                            </div>
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                SubCategory
                                                                                            </label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label4">
                                                                                                    <span>
                                                                                                        <%#Eval("SubCategory")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                File Upload</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:HyperLink ID="hplnk" NavigateUrl='<%#Eval("vch_FIleUpload") %>' 
                                                                                                    runat="server" >File</asp:HyperLink>
                                                                                            </div>
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Email
                                                                                            </label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label8">
                                                                                                    <span>
                                                                                                        <%#Eval("Email")%></span></label>
                                                                                                <asp:HiddenField ID="hdnEmail" runat="server" Value='<%#Eval("Email")%>' />
                                                                                                <asp:HiddenField ID="hdnIssueNo" runat="server" Value='<%#Eval("vchIssueNo") %>' />
                                                                                                <asp:HiddenField ID="hdnMobile" runat="server" Value='<%#Eval("VchMobile") %>' />
                                                                                                <asp:HiddenField ID="hdnCategory" runat="server" Value='<%#Eval("CategoryName") %>' />
                                                                                    <asp:HiddenField ID="hdnSubCategoryId" runat="server" Value='<%#Eval("int_SubcategoryId") %>' />

                                                                                            </div>SSSS
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Address</label>
                                                                                            <div class="col-md-10">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label7">
                                                                                                    <span>
                                                                                                        <%#Eval("Address")%></span></label>
                                                                                                <%-- <label class="form-control-static" id="Label9"><span><%#Eval("vch_FIleUpload")%></span></label>--%>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2" style="color: #000000">
                                                                                                Issue Details</label>
                                                                                            <div class="col-md-10">
                                                                                                <span class="colon">:</span>
                                                                                                <label class="form-control-static" id="Label5">
                                                                                                    <span>
                                                                                                        <%#Eval("vch_IssueDetails")%></span></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="panel panel-bd ">
                                                                                <div class="panel-heading">
                                                                                    Take Action
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2">
                                                                                                Status<span class="text-red">*</span></label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:DropDownList ID="drpStatus" runat="server" class="form-control ddlsts">
                                                                                               <%--     <asp:ListItem Selected="True" Value="0" Text="--Select--"></asp:ListItem>
                                                                                                    <asp:ListItem Value="2" Text="Resolved"></asp:ListItem>
                                                                                                    <asp:ListItem Value="3" Text="Inprogress"></asp:ListItem>
                                                                                                    <asp:ListItem Value="4" Text="Discard"></asp:ListItem>
                                                                                                     <asp:ListItem Value="5" Text="Reopen"></asp:ListItem>--%>
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <%-- <div class="form-group remark" id="divremark" style="display:none">--%>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2">
                                                                                                Remark<span class="text-red">*</span></label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="3" CssClass="form-control remark"
                                                                                                    runat="server" Onkeypress="return inputLimiter(event,'Address')"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <%--</div>--%>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label class="col-md-2">
                                                                                                Upload Reference document</label>
                                                                                            <div class="col-md-4">
                                                                                                <span class="colon">:</span>
                                                                                                <asp:FileUpload ID="docUpload" CssClass="form-control" runat="server" />
                                                                                                <div style="float: left">
                                                                                                    <span class="mandatory"><small>(Upload only .pdf file, Maximum 4 M.B)</small></span>
                                                                                                    <asp:HiddenField ID="hdnDoc" runat="server" />
                                                                                                    <asp:HyperLink ID="lnkUFName" runat="server" Text="" Target="_blank"></asp:HyperLink>
                                                                                                </div>
                                                                                            </div>
                                                                                            <asp:UpdatePanel runat="server" ID="ShareUpdate">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Button ID="btnShare" runat="server" Text="Share Document" OnClick="btnShare_Click"
                                                                                                        class="btn btn-sm btn-success btnsubmit2" />
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                                                                <ContentTemplate>
                                                                                                    <div class="col-sm-offset-2 col-sm-10">
                                                                                                        <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="false" Visible="false"
                                                                                                            EmptyDataText="No Record(s) found...." DataKeyNames="UserManual,ServiceName,ServiceId"
                                                                                                            CssClass="table table-bordered">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField HeaderText="Select">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                                                                        <asp:HiddenField ID="hdnServiceID" runat="server" Value='<%#Eval("ServiceId")%>' />
                                                                                                                        <asp:HiddenField ID="hdnUserManual" runat="server" Value='<%#Eval("UserManual")%>' />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <%-- <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                                                                                                <%-- <asp:BoundField DataField="UserManual" HeaderText="File Name" />--%>
                                                                                                                <asp:BoundField DataField="ServiceName" HeaderText="File Name" />
                                                                                                            </Columns>
                                                                                                        </asp:GridView>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                                                                        <div class="row">
                                                                                            <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" OnClientClick="return validation();"
                                                                                                class="btn btn-add btn-sm btnsubmit" />
                                                                                            <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">
                                                                                                Cancel</button>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <!-- Text input-->
                                                                        </fieldset>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">
                                                                    Close</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- /.modal-content -->
                                                </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-grid no-print" />
                                </asp:GridView>
                                <asp:Button ID="btnDownload" runat="server" Text="Download" Style="display: none"
                                    OnClick="btnDownload_Click" />
                                <asp:HiddenField ID="hdnFileNames" runat="server" />
                                <asp:HiddenField ID="hdnqueryFile" runat="server"></asp:HiddenField>
                            </div>
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <!-- customer Modal1 -->
            <!-- /.modal -->
            <!-- Modal -->
        </section>
        <!-- /.content -->
    </div>
    <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
    </div>
</asp:Content>
