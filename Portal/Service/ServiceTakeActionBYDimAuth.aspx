<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceTakeActionBYDimAuth.aspx.cs"
    MasterPageFile="~/MasterPage/Application.master" Inherits="Portal_Service_ServiceTakeActionBYDimAuth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/decimalrstr.js" type="text/javascript"></script>
    <style></style>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#btnSubmit').click(function () {

            });
            $('.datePicker').datepicker({
                autoclose: true,
                format: 'dd-M-yyyy'
            });
        });
        function htmlUnescape(value) {
            return String(value)
        .replace(/&quot;/g, '"')
        .replace(/&#39;/g, "'")
        .replace(/&lt;/g, '<')
        .replace(/&gt;/g, '>')
        .replace(/&amp;/g, '&');
        }
        function setvaluesOfrowRaise(flu) {
            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 2

            document.getElementById('ContentPlaceHolder1_gvService_txtQueryRemarks_' + rows).value = "";

            document.getElementById('divResults').innerHTML = "";
            document.getElementById('divResults').innerHTML = htmlUnescape(document.getElementById('ContentPlaceHolder1_gvService_hdnremark_' + rows).value);
            document.getElementById('ContentPlaceHolder1_hdnQryServiceId').value = document.getElementById('ContentPlaceHolder1_gvService_hdnServiceId2_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnQryApplicationUnqKey').value = document.getElementById('ContentPlaceHolder1_gvService_hdnApplicationUnqKey_' + rows).value;

        }
        function setvaluesOfrow(flu) {
            debugger;
            //  alert('hi');
            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 2


        }

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
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        function validate(obj) {
            debugger;
            var ID = obj.id;
            var arr = ID.split('_');
            if ($('#ContentPlaceHolder1_gvService_txtQueryRemarks_' + arr[3]).val() == "") {
                jAlert('<strong>Query cannot be left blank!</strong>', projname);
                $('#gvProposal_txtA1_' + arr[3]).focus();
                return false;
            }

            if ($('#ContentPlaceHolder1_gvService_docqueryUpload_' + arr[3]).val() != "") {
                if (!DocValid('#ContentPlaceHolder1_gvService_docqueryUpload_' + arr[3]))
                { return false; }
            }
        }
        function DocValid(Controlname) {
           
            var arr = new Array;
            var arr2 = new Array;
            var arrnew = new Array('pdf');
            var count = 0;
            var y, x, z;

            x = $(Controlname).val();
            z = $(Controlname);
          //  z = $(Controlname).attr('id');
            y = x.substring(x.lastIndexOf(".") - 1);
            arr = y.split('.');

            for (var j = 0; j < arrnew.length; j++) {
                if (arr[1] == arrnew[j])
                    count = 1;
            }

            if (count == 0) {
                jAlert('<strong>Please Upload PDF file Only !</strong>', 'SWP');
                return false;
            }
            else if (z.files[0].size > 4 * 1024 * 1024) {
                jAlert('<strong>The file size can not exceed 4MB. !</strong>', 'SWP');
                return false;
            }
            else
                return true;
        }
        function setvalue(obj) {
            var ID = obj.id;
            var arr = ID.split('_');
            $('#charsLeft').html(1000 - $('#ContentPlaceHolder1_gvService_txtQueryRemarks_' + arr[3]).val().length);

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

            $(".ddlsts").change(function () {



                $(this).closest('tr').find('.certificateupl').hide();
                $(this).closest('tr').find('.fwd').hide();
                $(this).closest('tr').find('.dmdamt').hide();
                if ($(this).val() == "2") {
                    // document.getElementById('certificateupload').style.display = "block";
                    $(this).closest('tr').find('.certificateupl').show();
                }





            });
            $('.btnsubmit').click(function () {
               
                var refdoc = $(this).closest('tr').find('.uploadReferencedoc').attr('id');
               
                if ($(this).closest('tr').find('.ddlsts').val() == "2") {
                    var appcert = $(this).closest('tr').find('.docUpload').attr('id');
                    if ($(this).closest('tr').find('.docUpload').val() != "") {
                        if (!DocValid('#' + appcert + ''))
                        { return false; }
                    }
                    else {
                        jAlert('<strong>Please Upload Approval Certificate  !</strong>', 'SWP');
                        $(this).closest('tr').find('.docUpload').focus();
                        return false;
                    }
                }
                else if ($(this).closest('tr').find('.uploadReferencedoc').val() != "") {
                    if (!DocValid('#' + refdoc + ''))
                    { return false; }
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
                  <h1>Services</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Take Action</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                           <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ServiceTakeActionBYDimAuth.aspx"> 
                              <i class="fa fa-plus"></i>Take Action</a> 
                               
                           </div>
                             <div class="btn-group buttonlist" >                            
                               <a class="btn btn-add " href="ViewServiceTakeActionBYDimAuth.aspx"> 
                              <i class="fa fa-file"></i>View</a> 
                             </div>
                        </div>
                        <div class="panel-body">
                          <div class="search-sec">
                           <div class="form-group">
                              <div class="row">
                              
                             
                                 <label class="col-sm-2">Department</label>  <div class="col-sm-3">
                                 <span class="colon">:</span>
                                  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                  <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control dpt"  onselectedindexchanged="ddldept_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                     </ContentTemplate>
                                  
                                 </asp:UpdatePanel>
                                          </div>
                                
                                          <label class="col-sm-2">Service Sector</label><div class="col-sm-3"><span class="colon">:</span>
                                          <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                 <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control" 
                                       >
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Aluminium Industry" Value="1" />
                                            <asp:ListItem Text="Cement, Lime and Plaster" Value="2" />
                                            <asp:ListItem Text="Chemicals and Chemical products" Value="3" />
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
                                <label class="col-sm-2">Application No</label>
                               <div class="col-sm-3">
                               
                                <span class="colon">:</span>
                                  <asp:TextBox ID="txtAppno" MaxLength="20" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                          </div>  <label class="col-sm-2">Proposal No</label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                   <asp:TextBox ID="txtProposalno" MaxLength="20" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
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
                            <div style="display: inline-block; text-align: right; width: 100%">
                            <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                        OnClick="lbtnAll_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                            </div>
                              <%--  <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>--%>
                              <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server" 
                                AutoGenerateColumns="False" AllowPaging="True" onpageindexchanging="gvService_PageIndexChanging"
                Width="100%" EmptyDataText="No Record(s) Found..." 
               DataKeyNames="intServiceId,strApplicationUnqKey,intActionTobeTakenBy,Deptid,intQueryStatus,strQueryStatus,strProposalId" 
                                    onrowdatabound="gvService_RowDataBound" onrowcreated="gvService_RowCreated" PageSize="10" 
                                >
                <Columns>
           

                         <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                              <HeaderTemplate>
                                         Sl No.
                                            </HeaderTemplate>
                                             <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                             </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                            <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                         Application No.
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:HyperLink ID="hlnkTkn" ForeColor="Blue" Text='<%#Eval("strApplicationUnqKey") %>'  runat="server" ToolTip="ApplicationNo"></asp:HyperLink>
                                     <asp:HiddenField ID="hdnServiceId" runat="server" Value='<%#Eval("intServiceId") %>'></asp:HiddenField>
                                     <asp:HiddenField ID="hdnActionTakentobetaken" runat="server" Value='<%#Eval("strActionTobeTakenBy") %>'></asp:HiddenField>
                                     <asp:HiddenField ID="hdnActiontakenby" runat="server" Value='<%#Eval("strActionTakenBy") %>'></asp:HiddenField>
                                         <asp:HiddenField ID="hdnIndustryName" runat="server" Value='<%#Eval("strIndustryName") %>'></asp:HiddenField>
                                        </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                      <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                        Proposal No.
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob1" Text='<%#Eval("strProposalId") %>' runat="server" Visible="false"></asp:Label>
                                      <asp:HyperLink ID="hlnkproposal" ForeColor="Blue" Text='<%#Eval("strProposalId") %>'  runat="server" ToolTip="ProposalNo" ></asp:HyperLink>
                                        </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                           

                                           <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                        Service Name
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob2" Text='<%#Eval("strServiceName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                       Investor's Name
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob" Text='<%#Eval("strInvesterName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                         <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                     Requested Date
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmobMM" Text='<%#Eval("Requestdate") %>' runat="server"></asp:Label>
                                        </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                                     <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                         Status
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob3" Text='<%#Eval("strStatus") %>' runat="server"></asp:Label>
                                  <%--<asp:Label ID="lblmob3" Text="Pending" runat="server"></asp:Label>--%>
                                        </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                                  <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                         Action Taken By
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                  <%--  <asp:Label ID="lblmob3" Text='<%#Eval("INT_STATUS") %>' runat="server"></asp:Label>--%>
                                  <asp:Label ID="lblmob3d" Text='<%#Eval("strActionTakenBy") %>' runat="server"></asp:Label>
                                        </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                           <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                       Action to be Taken By
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                  <%--  <asp:Label ID="lblmob3" Text='<%#Eval("INT_STATUS") %>' runat="server"></asp:Label>--%>
                                  <asp:Label ID="lblmob3dd" Text='<%#Eval("strActionTobeTakenBy") %>' runat="server"></asp:Label>
                                        </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Source">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnProposal" Value='<%#Eval("strProposalId") %>'  runat="server" />
                                                
                                                <asp:Label ID="lblRouted" runat="server" Text="Routed Through SLFC/DLFC"></asp:Label>
                                                
                                            </ItemTemplate>
                         </asp:TemplateField>

                                        <asp:TemplateField  ItemStyle-HorizontalAlign="Left" Visible="false">
                       <HeaderTemplate>
                                         Payment Status
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                  <%--  <asp:Label ID="lblmob3" Text='<%#Eval("INT_STATUS") %>' runat="server"></asp:Label>--%>
                                 <span class="label-custom label label-default">Required</span>
                                        </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                   <%--     <asp:TemplateField>
                                        <HeaderTemplate>
                                         Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        
                                            <asp:LinkButton ID="LinkButton1" Text="Take Action"  OnClientClick="setvaluesOfrow(this);" runat="server" class="label-warning label label-default" data-toggle="modal" data-target='<%# "#" + Eval("strApplicationUnqKey")%>' >Take Action</asp:LinkButton>
                                          <div class="modal fade" id='<%#Eval("strApplicationUnqKey")%>' tabindex="-1" role="dialog" aria-hidden="true">
                                            
                                                               
                                          </div>
                                        </ItemTemplate>
                                        </asp:TemplateField>--%>


                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                        Take Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:HiddenField ID="hdnServiceId2" runat="server" Value='<%# Eval("intServiceId")%>'></asp:HiddenField>
                                        <asp:HiddenField ID="hdnApplicationUnqKey" runat="server" Value='<%# Eval("strApplicationUnqKey")%>'></asp:HiddenField>

                               

                                        <asp:HiddenField ID="hdnProposalId" runat="server" Value='<%# Eval("strProposalId")%>'></asp:HiddenField>
                                        <asp:HiddenField ID="hdnInvesterName" runat="server" Value='<%# Eval("strInvesterName")%>'></asp:HiddenField>
                                        <asp:HiddenField ID="hdnActionTakenBy2" runat="server" Value='<%# Eval("intActionTakenBy")%>'></asp:HiddenField>
                                         <asp:HiddenField ID="hdnActionTobeTakenBy2" runat="server" Value='<%# Eval("intActionTobeTakenBy")%>'></asp:HiddenField>
                                          <asp:HiddenField ID="hdnlevel" runat="server" Value='<%# Eval("intEscalationId")%>'></asp:HiddenField>
                                            <asp:HiddenField ID="hdnstrServiceName" runat="server" Value='<%# Eval("strServiceName")%>'></asp:HiddenField>
                                           <asp:LinkButton ID="LinkButton1" Text="Take Action"   runat="server" class="label-warning label label-default" data-toggle="modal" data-target='<%# "#"+Eval("strApplicationUnqKey") %>'>Take Action</asp:LinkButton>
                                          
                                        <%--   <a href="#" data-toggle="modal" data-target='<%# "#"+Eval("strApplicationUnqKey") %>'  class="label-warning label label-default" data-toggle="modal"  OnClientClick="setvaluesOfrow(this);">
                                                               TakeAction</a>
--%>
                                          <div class="modal fade"  id='<%#Eval("strApplicationUnqKey") %>' tabindex="-1" role="dialog" aria-hidden="true">
                
               <div class="modal-dialog modal-lg">
                                                <div class="modal-content">
                        <div class="modal-header modal-header-primary">
                           <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                         
                           <%--<h3><i class="fa fa-user m-r-5"></i> Take Action</h3>--%>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                              <div class="col-md-12">
                            <%--     <form class="form-horizontal">--%>
                                    <fieldset>
                                    <div class="panel panel-bd ">
                        <div class="panel-heading">
                         Details
                        </div>
                        <div class="panel-body">
                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Proposal No.</label>
                                    <div class="col-md-4">
                                    <span class="colon">:</span>
                                   <label class="control-label" id="lblProposalId"><span><%#Eval("strProposalId")%></span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Name Of Industries/Enterprises</label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                    <label class="control-label" id="lblIndustriesName"><span><%#Eval("strIndustryName")%></span></label>
                                    </div>
                                    </div>
                                    </div>
                           <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Investor's Name</label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                   <label class="control-label" id="lblInvestorName"><span><%#Eval("strInvesterName")%></span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Industry Type</label>
                                    <div class="col-md-4">
                                     <span class="colon">:</span>
                                     <label class="control-label" id="lblIndustryType"><span><%#Eval("strIndType")%></span></label>
                                    </div>
                                    </div>
                                    </div>
                      <%--     <div class="form-group">
                                    <div class="row">
                                    <label class="col-md-2" style="color: #000000">Action to be Taken By</label>
                                    <div class="col-md-4">
                                   <label class="control-label" id="lblActiontobetaken"><span><%#Eval("strActionTobeTakenBy")%></span></label>
                                    </div>
                                     <label class="col-md-2" style="color: #000000">Action Taken By</label>
                                    <div class="col-md-4">
                                     <label class="control-label" id="lblActiontakenby"><span><%#Eval("strActionTakenBy")%></span></label>
                                    </div>
                                    </div>
                                    </div>  --%>
                        </div>
                     </div>
                      <div class="panel panel-bd ">
                        <div class="panel-heading">
                        Take Action
                        </div>
                         <div class="panel-body">
                          <div class="form-group">
                                       <div class="row">
                                       <label class="col-md-2">Status</label>
                                          <div class="col-md-4"> 
                                           <span class="colon">:</span>
                                            <asp:DropDownList ID="drpStatus" runat="server" class="form-control ddlsts">
                                            <asp:ListItem Selected="True" Value="2" Text="Approve"></asp:ListItem>
                                           <%-- <asp:ListItem  Value="3" Text="Forward"></asp:ListItem>--%>
                                             <asp:ListItem  Value="4" Text="Reject"></asp:ListItem>
                                              <%--<asp:ListItem  Value="5" Text="Raised Query"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            </div>
                                             </div>
                                       </div>
                                   
                                   
                                      <div class="form-group">
                                       <div class="row">
                                   <label class="col-md-2">Remark</label>
                                          <div class="col-md-4"> 
                                           <span class="colon">:</span>
                                      <%--   <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                         <asp:TextBox ID="txtRemarks" TextMode="MultiLine"  rows="3" CssClass="form-control"
                                        runat="server" Onkeypress="return inputLimiter(event,'Address')"></asp:TextBox>
                                            </div>

                                       </div>
                                       </div>
                                   
                                      <div class="form-group">
                                       <div class="row">
                                       <label class="col-md-2">Upload Reference document</label>
                                          <div class="col-md-4"> 
                                           <span class="colon">:</span>
                                           <asp:FileUpload  ID="docUpload" CssClass="form-control uploadReferencedoc" runat="server"/>
                                             <div style=" float: left">
                                                                    <span class="mandatory"><small>(Upload only .pdf file, Maximum 4 M.B)</small></span>
                                                                    <asp:HiddenField ID="hdnDoc" runat="server" />
                                                                    <%--  <b><asp:Label ID="lnkUFName" runat="server" ></asp:Label></b>--%>
                                                                    <asp:HyperLink ID="lnkUFName" runat="server" Text="" Target="_blank"></asp:HyperLink>
                                                                </div>
                                            </div>
                                                </div>
                                       </div>
                                          <div class="form-group certificateupl" id="certificateupload" >
                                       <div class="row">
                                         <label class="col-md-2">Upload Approval Certificate</label>
                                           <div class="col-md-4"> 
                                            <span class="colon">:</span>
                                             <asp:FileUpload  ID="fluApprovalCert" CssClass="form-control docUpload" runat="server"/>
                                             <div style=" float: left">
                                                                    <span style="color:Red" >  <small>(Only jpg,png,pdf and max size 4 MB files allowed.)*</small></span>
                                                                 
                                                                </div>
                                            </div>
                                           </div>
                                        </div>
                                       <div class="form-group fwd" id="forward" style="display:none">
                                       <div class="row">
                                         <label class="col-md-2">Select User</label>
                                           <div class="col-md-4"> 
                                            <span class="colon">:</span>
                                                 <%--<asp:DropDownList ID="ddluser" runat="server" class="form-control ddluser">
                                                  <asp:ListItem  Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Selected="True" Value="2" Text="Approve"></asp:ListItem>
                                         
                                       
                                           
                                            </asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlUser" runat="server" CssClass="chosen-select-width ddluser"  style="width:100%" > </asp:DropDownList>
                                             <div style=" float: left">
                                                                    <span style="color:Red" >  *</small></span>
                                                                 
                                                                </div>
                                            </div>
                                           </div>
                                        </div>
                                       <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                          <div class="row">
                                          <%--<button type="submit" class="btn btn-add btn-sm">Save</button>--%>
                                             <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click"   class="btn btn-add btn-sm btnsubmit"
                                      />
                                          <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Cancel</button>
                                          </div>
                                              <asp:HiddenField ID="hdnServiceId1" runat="server" Value=""></asp:HiddenField>
                                        <asp:HiddenField ID="hdnApplicationUnqKey1" runat="server" Value=""></asp:HiddenField>
                                        <asp:HiddenField ID="hdnProposalId1" runat="server" Value=""></asp:HiddenField>
                                        <asp:HiddenField ID="hdnInvesterName1" runat="server" Value=""></asp:HiddenField>
                                        <asp:HiddenField ID="hdnActionTakenBy1" runat="server" Value=""></asp:HiddenField>
                                         <asp:HiddenField ID="hdnActionTobeTakenBy1" runat="server" Value=""></asp:HiddenField>
                                          <asp:HiddenField ID="hdnlevel1" runat="server" Value=""></asp:HiddenField>
                                       </div>
                         </div>
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
                                                    <!-- /.modal-content -->
                                    </div>
                                     </div>

                                        </ItemTemplate>


                                        </asp:TemplateField>

                                          
                                        </Columns>
                                             <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
                                          <asp:Button ID="btnDownload" runat="server" Text="Download" style="display:none"  OnClick="btnDownload_Click" />
                            <asp:HiddenField ID="hdnFileNames" runat="server" />
                                          <asp:HiddenField ID="hdnqueryFile" runat="server" ></asp:HiddenField>
                                          <%--</ContentTemplate>
                                <triggers>
                                   <asp:asyncpostbacktrigger controlid="btnShow"  />
                                
                                    </triggers>
   
                                 </asp:UpdatePanel>--%>
                           </div>
                        </div>
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
