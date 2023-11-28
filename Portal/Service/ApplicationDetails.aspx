<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ApplicationDetails.aspx.cs" Inherits="Portal_Service_ApplicationDetails" %>

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
                  <h1>Add Take Action</h1>
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
                              <a class="btn btn-add " href="ViewServiceApplication.aspx"> 
                              <i class="fa fa-plus"></i>View</a>  
                           </div>
                        </div>
                        <div class="panel-body">
                           <div class="form-group">
                           <div class="table-responsive">
                            <div class="dyformheader">
                                
 <div class="header-details" runat="server" id="divHeader">
<%-- <h2>Application Header</h2>
 <p>Government of Odisha</p>--%>
 </div>
 </div>
  <div  class="dyformbody">
      <div class="row">
        <div class="col-sm-6 ">
        <label for="sss" class="col-sm-6">Application Number</label>
        <label for="sss" class="col-sm-6" id="lblapplication" runat="server">
        <span class="colon"></span></label>
        </div>
        <div class="col-sm-6 ">
        <label for="sss" class="col-sm-6">Applied Date</label>
        <label for="sss" class="col-sm-6" id="lblapplieddate" runat="server">
        <span class="colon"></span></label>
        </div>       
       </div>
   <div ID="frmContent" runat="server">
   
   </div>

    </div>

    <b>Take Action Details</b>
                              <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server" 
                                AutoGenerateColumns="false" AllowPaging="true" 
                Width="100%" EmptyDataText="No Record(s) Found..." DataKeyNames="strFileUpload,strCertificateFilename"
                 PageSize="10" 
                                    onrowdatabound="gvService_RowDataBound"
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
                                      Action Taken By
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                              <asp:Label ID="lblmob1" Text='<%#Eval("strfullname") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                      <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                       Remark
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob" Text='<%#Eval("strRemark") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                           

                                           <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                       Status
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob2" Text='<%#Eval("strStatus") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                    Date
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <asp:Label ID="lblmob3" Text='<%#Eval("strTodate") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                       Reference Document
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                    <%--     <asp:HyperLink ID="hplnkdoc" NavigateUrl='<%#Eval("strFileUpload") %>' runat="server" Target="_blank" ToolTip="Download Certificate">
                                                  <i class="fa fa-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>
--%>
                                                            <asp:HiddenField ID="hdnfileval" runat="server" Value='<%#Eval("strFileUpload") %>'></asp:HiddenField>
<asp:LinkButton ID="lnkCert" runat="server" onclick="lnkCert_Click"><i class="fa fa-download" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          
                                        <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                       Approval Certificate
                                            </HeaderTemplate>
                                        <ItemTemplate>
                              
                                                            <asp:HiddenField ID="hdnfilevalcert" runat="server" Value='<%#Eval("strCertificateFilename") %>'></asp:HiddenField>
<asp:LinkButton ID="lnkAppCert" runat="server" onclick="lnkAppCert_Click"><i class="fa fa-download" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        </Columns>
                                        </asp:GridView>
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
