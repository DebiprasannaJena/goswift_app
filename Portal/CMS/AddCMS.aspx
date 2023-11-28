<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="AddCMS.aspx.cs" Inherits="CMS_AddCMS" %>
    <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #elements-container
        {
            text-align: center;
        }
        .CMS {
    float: right;
    color: #f00;
    margin-top: -30px;
    margin-right: -221px;
    font-size: 15px;
}
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" language="javascript">
        function lIEachData() {
            
            var strStatus = null;
            if ($('#ContentPlaceHolder1_ddlMenu').val() == 0) {
                jAlert('Please Select Menu Name !');
                $('#ContentPlaceHolder1_ddlMenu').focus()
                return false;
            }
            var ckValue = CKEDITOR.instances.ContentPlaceHolder1_txtRemark.getData();
            if (ckValue == '') {
                jAlert('Menu Content can not be left blank !');
                $('#cke_txtRemark').focus();
                return false;
            }
         

            //            if ($('ContentPlaceHolder1_ckEditor').val() == '') {
            //                alert('Menu Content cannot be left blank');
            //                strStatus = false;
            //            }
            //            return strStatus;
//            alert($('#ContentPlaceHolder1_ckEditor').val());
//            var ckValue = CKEDITOR.instances['ContentPlaceHolder1_ckEditor'].getData();
//            if (ckValue == '') {
//                alert('Description can not be left blank');
//                $('#ContentPlaceHolder1_ckEditor').focus();
//                return false;
            //            }

        }
    </script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-file-text-o"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Content</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Manage Content</a></li><li><a>Add Content</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidrag">
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

                              <div class="form-group">
                              <div class="row">
                              <label class="col-sm-2">Menu</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:DropDownList ID="ddlMenu" runat="server" CssClass="form-control" AutoPostBack="true"
                                       onselectedindexchanged="ddlMenu_SelectedIndexChanged"></asp:DropDownList>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>
                               <div class="form-group">
                              <div class="row">
                              <label class="col-sm-2"> Menu Content</label>
                               <div class="col-sm-8">
                                <span class="colon">:</span>
                           <%--  <cc1:editor ID="txtRemark" runat="server" Width="100%" />--%>
                            <CKEditor:CKEditorControl ID="txtRemark"  runat="server" Width="100%" Height="411px"
                                        BasePath="../../CKEditor/ckeditor"   ></CKEditor:CKEditorControl> 
                                 <span class="CMS">*</span> </div> 
                                 </div>
                              </div>                          
    <br/>
<div style="display:inline" class="nav">	
</div>
   <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return lIEachData();" 
                                  CssClass="btn btn-success" onclick="btnSave_Click"/>   
   <asp:Button ID="btncancel" runat="server" Text="Cancel"  CssClass="btn btn-warning" 
                                  onclick="btncancel_Click"/>
                        </div>
                        </div>
                        </div>
                        </div>
                        </section>
    </div>
</asp:Content>
