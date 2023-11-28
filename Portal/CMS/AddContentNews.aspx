<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Application.master" CodeFile="AddContentNews.aspx.cs" Inherits="Portal_CMS_AddContentNews" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

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
           $(document).ready(function () {
               var CStatus = $("#<%=radbtnlst.ClientID%>").find(":checked").val();
               if (CStatus == "News") {
                   $('.divarea').hide();
               }
               else {
                   $('.divarea').show();
               }

               
               $('#<%=radbtnlst.ClientID %> input').change(function () {

                   if ($(this).val() != 'News') {
                       $('.img-sec').hide();

                       $('.divarea').show();
                       $('.divchk').hide();
                   }
                   else {
                       $('.img-sec').show();
                       $('.divarea').hide();
                       $('.divchk').show();
                   }
                   return false;
               });
           });
           function UploadFile(fileUpload) {
               if (fileUpload.value != '') {
                   $('#ContentPlaceHolder1_lblUFName').text('');
                  
               }
           }
                    </script>
    <script type="text/javascript" language="javascript">
        function lIEachData() {
            var CStatus = $("#<%=radbtnlst.ClientID%>").find(":checked").val();
        
            var strStatus = null;
            if ($('#ContentPlaceHolder1_txtheading').val() == 0) {
                jAlert('Heading can not be left blank !');
                $('#ContentPlaceHolder1_txtheading').focus()
                return false;
            }
//            if ($('#ContentPlaceHolder1_txtheading').val() == 0) {
//                jAlert('Heading can not be left blank !');
//                $('#ContentPlaceHolder1_txtheading').focus()
//                return false;
            //            }
            if (CStatus == "News") {
                var ckValue = CKEDITOR.instances.ContentPlaceHolder1_txtRemark.getData();
                if (ckValue == '') {
                    jAlert('Content can not be left blank !');
                    $('#cke_txtRemark').focus();
                    return false;
                }
            }
            else {
                if ($('#ContentPlaceHolder1_txtarea').val() == 0) {
                    jAlert('Content can not be left blank !');
                    $('#ContentPlaceHolder1_txtarea').focus()
                    return false;
                }

            }
//            
//                        if ($('ContentPlaceHolder1_ckEditor').val() == '') {
//                            alert('Content cannot be left blank');
//                            strStatus = false;
//                        }
//                        return strStatus;
//                        alert($('#ContentPlaceHolder1_ckEditor').val());
//                        var ckValue = CKEDITOR.instances['ContentPlaceHolder1_ckEditor'].getData();
//                        if (ckValue == '') {
//                            alert('Content can not be left blank');
//                            $('#ContentPlaceHolder1_ckEditor').focus();
//                            return false;
//                        }

        }
          </script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-file-text-o"></i>
               </div>
               <div class="header-title">
                  <h1>Manage News & Events</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Manage News & Events</a></li><li><a>Add Manage News & Events</a></li></ul>
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
                              <a class="btn btn-add " href="AddContentNews.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> "">  
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                          <%--  <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewContentNews.aspx"> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>--%>
                           
                        </div>

                        <div class="panel-body">
                           <div class="form-group">
                              <div class="row">
                              <label class="col-sm-2">Type</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                             <asp:RadioButtonList ID="radbtnlst"  runat="server">
                              <asp:ListItem Value="News" Selected="True">News</asp:ListItem>
                             <asp:ListItem Value="Annoncement"  >Annoncement</asp:ListItem>                            
                             <asp:ListItem Value="Notification" >Notification</asp:ListItem>
                             </asp:RadioButtonList>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>
                              <div class="form-group">
                              <div class="row">
                              <label class="col-sm-2">Heading</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                               <asp:TextBox ID="txtheading" CssClass="form-control" runat="server"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                    TargetControlID="txtheading" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                    InvalidChars="&quot;'<>&;">
                                </cc1:FilteredTextBoxExtender>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>
                               <div class="form-group img-sec" id="divimg"  runat="server" >
                              <div class="row">
                              <label id="lblImg" class="col-sm-2" runat="server">Upload Image</label>
                               <div class="col-sm-4" id="Divupload" runat="server"><span class="colon">:</span>                                         
                        <asp:FileUpload ID="fldAttachment" CssClass="form-control" runat="server" />
                        <asp:Label ID="lblUFName" runat="server" Text=""></asp:Label> <span class="text-red"><small>(File size must be less than 2MB)</small></span>                              
                                 <%-- <span class="mandetory">*</span>--%>  <br />
                                 </div>
                                 </div>
                              </div>
                               <div class="form-group divchk" id="divchk" runat="server">
                              <div class="row">
                              <label class="col-sm-2">Content</label>
                               <div class="col-sm-8">
                                <span class="colon">:</span>
                           <%--  <cc1:editor ID="txtRemark" runat="server" Width="100%" />--%>
                            <CKEditor:CKEditorControl ID="txtRemark" runat="server" Width="100%" Height="411px"
                                        BasePath="~/CKEditor/ckeditor"></CKEditor:CKEditorControl> 
                                 <span class="CMS">*</span> </div> 
                                 </div>
                              </div>   
                                <div class="form-group divarea" id="divarea" runat="server">
                              <div class="row">
                              <label class="col-sm-2">Content</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>                        
                      <asp:TextBox ID="txtarea" TextMode="MultiLine" MaxLength="2000" runat="server"  Width="100%" Height="200px"></asp:TextBox>
                      <span class="mandetory">*</span></div> 
                                 </div>
                              </div>                                        
    <br/>
<div style="display:inline" class="nav">	
</div>
   <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return lIEachData();" 
                                  CssClass="btn btn-success" onclick="btnSave_Click" />   
   <asp:Button ID="btncancel" runat="server" Text="Cancel"  CssClass="btn btn-warning" 
                                 />
                        </div>
                        </div>
                        </div>
                        </div>
                        </section>
    </div>
</asp:Content>
