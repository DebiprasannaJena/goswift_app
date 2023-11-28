<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadCertificate.aspx.cs"
    Inherits="Portal_Service_UploadCertificate" MasterPageFile="~/MasterPage/Application.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/decimalrstr.js" type="text/javascript"></script>
    <style>
    .control-label {
   
    display: block;
    margin: 0px;
}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.btnsub').click(function () {

                if ($(this).closest('tr').find('.docUpload').val() != "") {
                    if (!DocValid($(this).closest('tr').find('.docUpload')))
                    { return false; }
                }
                else if ($(this).closest('tr').find('.docUpload').val() == "") {
                    jAlert('<strong>Please Upload Approval Certificate  !</strong>', 'SWP');
                    return false;
                }
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

            // document.getElementById('ContentPlaceHolder1_hdnServiceId1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnServiceId2_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnServiceId1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnServiceId_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnApplicationUnqKey1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnApplicationUnqKey_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnProp').value = document.getElementById('ContentPlaceHolder1_gvService_hdnProposalId_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnInvesterName1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnInvesterName_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnActionTakenBy1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnActionTakenBy2_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnActionTobeTakenBy1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnActionTobeTakenBy2_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnlevel1').value = document.getElementById('ContentPlaceHolder1_gvService_hdnlevel_' + rows).value;
            document.getElementById('lblInvestorName').innerHTML = document.getElementById('ContentPlaceHolder1_gvService_hdnInvesterName_' + rows).value;
            document.getElementById('lblProposalId').innerHTML = document.getElementById('ContentPlaceHolder1_gvService_hdnProposalId_' + rows).value;
            document.getElementById('lblActiontobetaken').innerHTML = document.getElementById('ContentPlaceHolder1_gvService_hdnActionTakentobetaken_' + rows).value;
            document.getElementById('lblActiontakenby').innerHTML = document.getElementById('ContentPlaceHolder1_gvService_hdnActiontakenby_' + rows).value;
            document.getElementById('lblIndustriesName').innerHTML = document.getElementById('ContentPlaceHolder1_gvService_hdnIndustryName_' + rows).value;
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
            var arrnew = new Array('pdf', 'png', 'jpg', 'jpeg');
            var count = 0;
            var y, x, z;

            x = $(Controlname).val();
            z = document.getElementById(Controlname);
            y = x.substring(x.lastIndexOf(".") - 1);
            arr = y.split('.');

            for (var j = 0; j < arrnew.length; j++) {
                if (arr[1] == arrnew[j])
                    count = 1;
            }

            if (count == 0) {
                jAlert('<strong>Please Upload jpg or png or pdf file Only !</strong>', 'SWP');
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
        
    </script>
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Upload Certificate</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Mange users</a></li><li><a>Take Action</a></li></ul>
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
                              <a class="btn btn-add " href="ServiceViewAndTakeAction.aspx"> 
                              <i class="fa fa-plus"></i>Upload Certificate</a> 
                               
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
                              <%--  <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>--%>
                              <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server" 
                                AutoGenerateColumns="false" AllowPaging="true" onpageindexchanging="gvService_PageIndexChanging"
                Width="100%" EmptyDataText="No Record(s) Found..." 
               DataKeyNames="intServiceId,strApplicationUnqKey,intActionTobeTakenBy,Deptid,intQueryStatus,strQueryStatus"  PageSize="10" 
                                    onrowdatabound="gvService_RowDataBound" onrowcreated="gvService_RowCreated" 
                                >
                <Columns>
           

                         <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                              <HeaderTemplate>
                                         Sl No.
                                            </HeaderTemplate>
                                             <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                             </ItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                         Application No.
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:HyperLink ID="hlnkTkn" ForeColor="Black" Text='<%#Eval("strApplicationUnqKey") %>'  runat="server"></asp:HyperLink>
                                     <asp:HiddenField ID="hdnServiceId" runat="server" Value='<%#Eval("intServiceId") %>'></asp:HiddenField>
                                     <asp:HiddenField ID="hdnActionTakentobetaken" runat="server" Value='<%#Eval("strActionTobeTakenBy") %>'></asp:HiddenField>
                                     <asp:HiddenField ID="hdnActiontakenby" runat="server" Value='<%#Eval("strActionTakenBy") %>'></asp:HiddenField>
                                         <asp:HiddenField ID="hdnIndustryName" runat="server" Value='<%#Eval("strIndustryName") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                      <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                        Proposal No.
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob1" Text='<%#Eval("strProposalId") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                           

                                           <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                        Service Name
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob2" Text='<%#Eval("strServiceName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                       Investor's Name
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob" Text='<%#Eval("strInvesterName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                     Requested Date
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmobMM" Text='<%#Eval("Requestdate") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>

                                                     <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                         Status
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob3" Text='<%#Eval("strStatus") %>' runat="server"></asp:Label>
                                  <%--<asp:Label ID="lblmob3" Text="Pending" runat="server"></asp:Label>--%>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                                  <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                         Action Taken By
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                  <%--  <asp:Label ID="lblmob3" Text='<%#Eval("INT_STATUS") %>' runat="server"></asp:Label>--%>
                                  <asp:Label ID="lblmob3d" Text='<%#Eval("strActionTakenBy") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                           <%--<asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                       Action to be Taken By
                                            </HeaderTemplate>
                                        <ItemTemplate>
                              
                                  <asp:Label ID="lblmob3dd" Text='<%#Eval("strActionTobeTakenBy") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <%--<asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                         Payment Status
                                            </HeaderTemplate>
                                        <ItemTemplate>
                               
                                 <span class="label-custom label label-default">Required</span>
                                        </ItemTemplate>
                                        </asp:TemplateField>--%>

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
                                           <asp:LinkButton ID="LinkButton1" Text="Upload Certificate"  OnClientClick="setvaluesOfrow(this);" runat="server" class="label-primary label label-default" data-toggle="modal" data-target='<%# "#"+Eval("strApplicationUnqKey") %>'>Upload Certificate</asp:LinkButton>
                                          
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
                     Upload Certificate
                        </div>
                         <div class="panel-body">
                          <%--<div class="form-group">
                                       <div class="row">
                                       <label class="col-md-2">Status</label>
                                          <div class="col-md-4"> 
                                            <asp:DropDownList ID="drpStatus" runat="server" class="form-control">
                                            <asp:ListItem Selected="True" Value="2" Text="Approve"></asp:ListItem>
                                          
                                            </asp:DropDownList>
                                            </div>
                                             </div>
                                       </div>--%>
                                    <%--<div class="form-group" id="divdemandAmt" style="display:none">
                                       <div class="row">
                                         <label class="col-md-2">Estimated Amount</label>
                                           <div class="col-md-4"> 
                                            <asp:TextBox ID="txtEstimatedAmt" TextMode="SingleLine"  rows="3" CssClass="form-control"
                                        runat="server"    onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                            </div>
                                           </div>
                                        </div>--%>
                                   
                                      <%--<div class="form-group">
                                       <div class="row">
                                   <label class="col-md-2">Remark</label>
                                          <div class="col-md-4"> 
                                     
                                         <asp:TextBox ID="txtRemarks" TextMode="MultiLine"  rows="3" CssClass="form-control"
                                        runat="server" Onkeypress="return inputLimiter(event,'Address')"></asp:TextBox>
                                            </div>

                                       </div>
                                       </div>--%>
                                   
                                      <div class="form-group">
                                       <div class="row">
                                       <label class="col-md-2">Upload Approval document</label>
                                          <div class="col-md-4"> 
                                           <span class="colon">:</span>
                                           <asp:FileUpload  ID="docUpload" CssClass="form-control docUpload" runat="server"/>
                                             <div style=" float: left">
                                                                    <span style="color:Red" >  <small>(Only jpg,png,pdf and max size 4 MB files allowed.)*</small></span>
                                                                    <asp:HiddenField ID="hdnDoc" runat="server" />
                                                                    <%--  <b><asp:Label ID="lnkUFName" runat="server" ></asp:Label></b>--%>
                                                                    <asp:HyperLink ID="lnkUFName" runat="server" Text="" Target="_blank"></asp:HyperLink>
                                                                </div>
                                            </div>
                                                </div>
                                       </div>
                                      
                                       <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                          <div class="row">
                                          <%--<button type="submit" class="btn btn-add btn-sm">Save</button>--%>
                                             <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click"   class="btn btn-add btn-sm btnsub"
                                      />
                                          <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Cancel</button>
                                          </div>
                                              <asp:HiddenField ID="hdnServiceId1" runat="server" Value=""></asp:HiddenField>
                                        <asp:HiddenField ID="hdnApplicationUnqKey1" runat="server" Value=""></asp:HiddenField>
                                        <asp:HiddenField ID="hdnProp" runat="server" Value=""></asp:HiddenField>
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
    </div>
</asp:Content>
