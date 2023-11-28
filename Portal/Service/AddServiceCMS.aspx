<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="AddServiceCMS.aspx.cs" Inherits="Portal_CMS_AddServiceCMS" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #elements-container
        {
            text-align: center;
        }
        .CMS
        {
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
            if ($('#ContentPlaceHolder1_ddlServices').val() == 0) {
                jAlert('Please Select Service Name !');
                $('#ContentPlaceHolder1_ddlServices').focus()
                return false;
            }
            var ckValue = CKEDITOR.instances.ContentPlaceHolder1_txtRemark.getData();
            if (ckValue == '') {
                jAlert('Menu Content can not be left blank !');
                $('#cke_txtRemark').focus();
                return false;
            }

            var uploadcontrol = document.getElementById('<%=FileUploadManual.ClientID%>').value;
            //Regular Expression for fileupload control.
            var reg = /^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF)$/;
            if (uploadcontrol.length > 0)
            {
            //Checks with the control value.
            if (reg.test(uploadcontrol))
            {
            return true;
            }
            else 
            {
            //If the condition not satisfied shows error message.
            alert('Invalid file format only .pdf are allowed! ');
            return false;
            }
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
                  <h1>Manage Service Instruction </h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Manage Service Instruction Content</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidrag">
                        <div class="panel-heading">
                          <%-- <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddServiceMaster.aspx"> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewServiceMaster.aspx"> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>--%>
                           <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddServiceCMS.aspx"> 
                              <i class="fa fa-plus"></i> Service Instruction </a>  
                           </div>
                        </div>

                        <div class="panel-body">

                              <div class="form-group">
                              <div class="row">
                              <label class="col-sm-2">Services</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:DropDownList ID="ddlServices" runat="server" CssClass="form-control" 
                                       AutoPostBack="true" onselectedindexchanged="ddlServices_SelectedIndexChanged"></asp:DropDownList>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>
                              <div class="form-group">
                              <div class="row">
                              <label class="col-sm-2">Upload Manual</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                               <asp:FileUpload ID="FileUploadManual" runat="server"></asp:FileUpload>
                               
                                  
                                  <asp:HyperLink ID="HyprDownload" runat="server" Target="_blank" Visible="false">Download</asp:HyperLink>
                                  <asp:LinkButton ID="LnkRemove" runat="server" Visible="false" 
                                       onclick="LnkRemove_Click"><i class="fa fa-window-close" aria-hidden="true"></i></asp:LinkButton>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
