<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="AddImageGallery.aspx.cs" Inherits="Miscellaneous_AddImageGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #elements-container
        {
            text-align: center;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
    <script type="text/javascript" language="javascript">
        //        $(document).ready(function () {
        //            $('#ContentPlaceHolder1_btnSave').click(function () {
        //                debugger;
        //                if ($("#ContentPlaceHolder1_txtImgDesc").val() != "") {
        //                    return true;
        //                }
        //                else {
        //                    alert('Image Name cannot be left blank !');
        //                    return false;
        //                }
        //                var filePath = this.ContentPlaceHolder1_fileUploadImage.value;
        //                if (filePath.length < 1) {
        //                    alert("File Name Can not be empty");
        //                    return false;
        //                }

        //                //                if ($("#ContentPlaceHolder1_fileUploadImage").val() != "") {
        //                //                    return true;
        //                //                }
        //                //                else {
        //                //                    alert('Please upload an image !');
        //                //                    return false;
        //                //                }

        //            })
        //        });

        function lIEachData() {
            debugger;

            if ($('#ContentPlaceHolder1_txtImgDesc').val() == "") {
                jAlert('Image Name cannot be left blank !');
                $('#ContentPlaceHolder1_txtImgDesc').focus()
                return false;
            }
            var labelname = document.getElementById('ContentPlaceHolder1_lblUpload').innerText;
            var filePath = document.getElementById('<%= this.fileUploadImage.ClientID %>').value;
           
            if (filePath.length < 1 && labelname == "") {
                jAlert("Please upload an image");
                return false;
            }
            if (filePath.length > 1) {
                var validExtensions = new Array();
                var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
                validExtensions[1] = 'jpg';
                validExtensions[2] = 'jpeg';
                validExtensions[3] = 'png';
                for (var i = 0; i < validExtensions.length; i++) {
                    if (ext == validExtensions[i])

                        return true;
                }
                // alert('The image extension ' + ext.toUpperCase() + ' is not allowed!');
                jAlert("Invalid File! \n Please Upload JPG,JPEG,PNG files only !");

                return false;
            }
            return true;
        }
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                $('#ContentPlaceHolder1_lblUpload').text('');

            }
        }
    </script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-file-text-o"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Gallery</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Manage Gallery</a></li><li><a>Add Gallery</a></li></ul>
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
                              <a class="btn btn-add " href="AddImageGallery.aspx"> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewImageGallery.aspx"> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>
                           
                        </div>

                        <asp:Image ID="Image1" runat="server"></asp:Image>

                        <div class="panel-body">

                              <div class="form-group">                              
                                 <div class="row">
                              <label class="col-sm-2">Image Name</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:TextBox ID="txtImgDesc" CssClass="form-control" runat="server" ></asp:TextBox>                                
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>
                               <div class="form-group">
                              <div class="row">
                              <label class="col-sm-2"> Upload Image</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                             <asp:FileUpload ID="fileUploadImage" CssClass="form-control" runat="server" Text="Upload" />
                                <asp:Label ID="lblUpload" runat="server" Text='<%#Eval("vchImage")%>'></asp:Label><asp:HiddenField ID="hdnval" runat="server"></asp:HiddenField>
                               
                                 </div>  
                                 </div>
                              </div>                          
    <br/>
<div style="display:inline" class="nav">	
</div>
   <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-success" onclick="btnSave_Click" OnClientClick="return lIEachData();"/>  <%-- OnClientClick="return lIEachData();" --%>
   <asp:Button ID="btncancel" runat="server" Text="Cancel"  CssClass="btn btn-warning"/>
                        </div>
                        </div>
                        </div>
                        </div>
                        </section>
    </div>
</asp:Content>
