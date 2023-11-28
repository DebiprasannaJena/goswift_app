<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Application.master"
    CodeFile="ViewServiceApplication.aspx.cs" Inherits="Portal_Service_ViewServiceApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#btnSubmit').click(function () {

            });
            $('.datePicker').datepicker({
                autoclose: true,
                format: 'dd-M-yyyy'
            });
        });

        function setDateValues() {

            var appendId = "ContentPlaceHolder1_";
            var intMonth = (new Date().getMonth());
            var intYear = new Date().getFullYear();
            var fromDate = new Date();
            var toDate = new Date();
            if (intMonth == 0) {
                fromDate = new Date(intYear - 1, 11, 1);
                toDate = new Date();
            }
            else {
                fromDate = new Date(intYear, (intMonth - 1), 1);
                toDate = new Date();
            }

            $("#" + appendId + "txtFromdate").datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            }).datepicker("setDate", fromDate);
            $("#" + appendId + "txtTodate").datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            }).datepicker("setDate", toDate);
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
                  <h1>Services</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>View Application </a></li></ul>
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
                              <i class="fa fa-plus"></i>Take Action</a> 
                               
                           </div>
                             <div class="btn-group buttonlist" >                            
                               <a class="btn btn-add " href="ViewServiceApplication.aspx"> 
                              <i class="fa fa-file"></i>View</a> 
                             </div>
                        </div>
                        <div class="panel-body">
                            <div class="search-sec">
                           <div class="form-group">
                              <div class="row">
                               <label class="col-sm-2">Department</label>
                               <div class="col-sm-3">
                                <span class="colon">:</span>
                                  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                  <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control dpt"  onselectedindexchanged="ddldept_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                     </ContentTemplate>
                                  
                                 </asp:UpdatePanel>
                                          </div> <label class="col-sm-2">Service Sector</label>
                                <div class="col-sm-3">
                                            <span class="colon">:</span>
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
                                          </div> 
                                           <label class="col-sm-2">Proposal No</label>
                                <div class="col-sm-3">
                                         <span class="colon">:</span>
                                   <asp:TextBox ID="txtProposalno" MaxLength="20" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                 </div>
                                 </div>
                                   </div>
                                 <div class="form-group">
                                 <div class="row">
                               <label class="col-sm-2" >From Date</label>
                               <div class="col-sm-3">
                                  <span class="colon">:</span>
                                 <div class="input-group  date datePicker" id="datePicker1">
                                        <asp:TextBox ID="txtFromdate"  CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                
                                          </div>
                                              <label class="col-sm-2">To Date</label>
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
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                              <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server" 
                                AutoGenerateColumns="false" AllowPaging="true" onpageindexchanging="gvService_PageIndexChanging"
                Width="100%" EmptyDataText="No Record(s) Found..." 
               DataKeyNames="intServiceId,strApplicationUnqKey,intActionTobeTakenBy,Deptid,strReferenceFilename,intStatus,intQueryStatus"  PageSize="10" 
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
                                        <ItemStyle HorizontalAlign="Left" />
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
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                      <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                        Proposal No.
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob1" Text='<%#Eval("strProposalId") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                           

                                           <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                        Service Name
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob2" Text='<%#Eval("strServiceName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                               <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                       Investor's Name
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob" Text='<%#Eval("strInvesterName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                         <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                     Requested Date
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmobMM" Text='<%#Eval("Requestdate") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                                     <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                         Status
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob3" Text='<%#Eval("strStatus") %>' runat="server"></asp:Label>
                                  <%--<asp:Label ID="lblmob3" Text="Pending" runat="server"></asp:Label>--%>
                                        </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                                  <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                         Action Taken By
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                  <%--  <asp:Label ID="lblmob3" Text='<%#Eval("INT_STATUS") %>' runat="server"></asp:Label>--%>
                                  <asp:Label ID="lblmob3d" Text='<%#Eval("strActionTakenBy") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                                      <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                           <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                       Action to be Taken By
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                  <%--  <asp:Label ID="lblmob3" Text='<%#Eval("INT_STATUS") %>' runat="server"></asp:Label>--%>
                                  <asp:Label ID="lblmob3dd" Text='<%#Eval("strActionTobeTakenBy") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                               <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>



                                           <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                     Department Internal Application No
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                  <%--  <asp:Label ID="lblmob3" Text='<%#Eval("INT_STATUS") %>' runat="server"></asp:Label>--%>
                                  <asp:Label ID="lblappno" Text='<%#Eval("str_ApplicationNo") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                               <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>




                                       <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                         Download
                                            </HeaderTemplate>
                                        <ItemTemplate>
                     
<asp:HiddenField ID="hdnfileval" runat="server" Value='<%#Eval("strReferenceFilename") %>'></asp:HiddenField>
<asp:LinkButton ID="lnkCert" runat="server" onclick="lnkCert_Click"><i class="fa fa-download" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                       
                                       
                                       <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                         Query Details
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="hplnkqrydtl"  runat="server"  ToolTip="Query Details" CssClass="btn btn-success btn-sm">
                                                  <i class='fa fa-eye' aria-hidden='true'></i>
                                                            </asp:HyperLink>
                                            </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        </Columns>
                                          <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
                                          </ContentTemplate>
                                <triggers>
                                  <%-- <asp:asyncpostbacktrigger controlid="btnShow"  />--%>
                                  <asp:PostBackTrigger ControlID="gvService" />
                                    </triggers>
   
                                 </asp:UpdatePanel>
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
</asp:Content>
