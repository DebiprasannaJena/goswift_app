<%--'*******************************************************************************************************************
' File Name         : ViewCMS.aspx
' Description       : View added Content
' Created by        : AMit Sahoo
' Created On        : 21 August 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewCMS.aspx.cs" Inherits="CMS_ViewCMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/Validator.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>  
     <script type="text/javascript">
         function chkAllCheckbox(obj) {
             var gv = document.getElementById('<%=gvCMS.ClientID %>');
             for (var i = 0; i < gv.all.length; i++) {
                 var node = gv.all[i];
                 node.checked = obj.checked;
             }
         }
         function UnsetAll(e) {
             var eState = e.checked;
             if (eState == false)
                 document.getElementById("gvCMS_ctl02_Checkbox1").checked = false;
         }
     </script> 
     <script type="text/javascript">
         function CheckAuthenticate() {
             for (var i = 0; i < document.forms[0].elements.length; i++) {
                 if (document.forms[0].elements[i].checked == true) {
                     if (confirm(" Are you sure you want to delete the record ?")) {
                         return true;
                     }
                     else {
                         return false;
                     }
                 }
             }

             jAlert("Please select a record to delete !");
             return false;
         }

     </script>
    <div class="content-wrapper">
          
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Content</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Content</a></li><li><a>View Content</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">

              <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                           <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddCMS.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> ""> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewCMS.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> ""> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>
                           
                        </div>
                        <div class="panel-body">               
                      
                               <asp:GridView ID="gvCMS" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" CellPadding="4"
                                           GridLines="None" DataKeyNames="IntCmsId" 
                                   onrowediting="gvCMS_RowEditing">
                                            
                                            <Columns>
                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-CssClass="noPrint" ItemStyle-CssClass="noPrint" HeaderStyle-Width="20px">
                                     <%--    <HeaderTemplate>
                                            <input name="cbAll" value="cbAll" type="checkbox" onclick="SelectAll(cbAll,'gvCMS','ContentPlaceHolder1')" />
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkItem" runat="server" onclick="return deSelectHeader(cbAll,'gvCMS','ContentPlaceHolder1')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Sl.No." ItemStyle-Width="20">
                                                         <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                <asp:BoundField HeaderText="Menu" DataField="StrMenuName" />                                            
                                                  <asp:BoundField HeaderText="Content" DataField="StrContent" />
                                                   <asp:BoundField HeaderText="Description" DataField="StrDescription" />                                                   
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                          <asp:LinkButton ID="lbtnAction" class="btn btn-add btn-sm" runat="server" CommandName="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                        <asp:HiddenField ID="hdn" runat="server" Value='<%#Eval("IntCmsId")%>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                     <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="NOPRINT" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="NOPRINT" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="paging noPrint" HorizontalAlign="Right" />
                                        </asp:GridView>
                                         <asp:Button runat="server" Text="Delete" ID="btnDelete" CssClass="btn btn-danger" 
                                    onclick="btnDelete_Click" ></asp:Button>
                                         <asp:Label ID="Label1" runat="server" Text="No Records found" Visible="false"></asp:Label>     
                                         <asp:Label ID="lblMessage" runat="server" Text="No Records found" Visible="false"></asp:Label>                 
                           </div>
                        </div>
                     </div>
                  </div>
    </div>
    </div> </section>
    <!-- /.content -->
    </div> </div>
</asp:Content>
