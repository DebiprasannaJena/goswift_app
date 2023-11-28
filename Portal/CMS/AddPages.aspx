<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AddPages.aspx.cs" Inherits="Portal_CMS_AddPages" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <script type="text/javascript" src="../../js/CSMValidation.js"></script>
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
    <script type="text/javascript">
        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'GSTINDET') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZ';
            }
            if (allow == 'PageName') {
                AllowableCharacters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.';
            }
            if (allow == 'OtherSpecify') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'Profit') {
                AllowableCharacters = '1234567890-.';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._-';
            }
            if (allow == 'Address') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
            }
            if (allow == 'RawMetrial') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-file-text-o"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Page</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Manage Page</a></li><li><a>Add Page</a></li></ul>
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
                              <a class="btn btn-add " href="AddPages.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> "> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewPage.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> "> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>
                           
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                        <div class="panel-body">
                         
                         <div class="row">
                         <div class="col-sm-6">
                             <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-4">Templates</label>
                               <div class="col-sm-8">
                                <span class="colon">:</span>
                                        <asp:DropDownList ID="drpTemplates" runat="server"  CssClass="form-control"  AutoPostBack="true" onselectedindexchanged="drpTemplates_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem  Value="1">Layout With Single Menu</asp:ListItem>
                                        <asp:ListItem Value="2">Layout With Left Menu</asp:ListItem>
                                        <asp:ListItem  Value="3">Layout With Right Menu</asp:ListItem>
                                        </asp:DropDownList>
                                        
                                  <span class="mandetory">*</span>
                                 </div>
                                 <br />
                               
                                 </div>

                              </div>
           


                                  <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-4">Page Name</label>
                               <div class="col-sm-8">
                                <span class="colon">:</span>
                                       <asp:TextBox ID="txtPageName" CssClass="form-control"  Onkeypress="return inputLimiter(event,'PageName')" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>  
                              
                                  <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-4">Snippet</label>
                               <div class="col-sm-8">
                                <span class="colon">:</span>
                                       <asp:TextBox ID="txtSnippet" CssClass="form-control" TextMode="MultiLine" Height="80px" Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>  

                               </div>

                   <div class="col-sm-6">
                    <div class="col-sm-4">
                          <%--     <a href="../../DemoTempleteone.aspx" >Link to popup</a>--%>
                                <span runat="server" id="dv1"> <img src="../images/priview1.png" height="100px" width="180px"  /><a href="../../DemoTempleteone.aspx" onclick="return popitup('../../DemoTempleteone.aspx')">Click here to Preview</a> </span>
                                <span runat="server" id="Dv2"> <img src="../images/priview2.png" height="100px" width="180px" /> <a href="../../DemoTempleteTwo.aspx" onclick="return popitup('../../DemoTempleteTwo.aspx')">Click here to Preview</a></span>
                                <span runat="server" id="Dv3">  <img src="../images/priview3.png" height="100px" width="180px" /> <a href="../../DemoTempleteThree.aspx" onclick="return popitup('../../DemoTempleteThree.aspx')">Click here to Preview</a></span>
                
                                </div>
                         </div>
                         </div>
                                  <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Content</label>
                               <div class="col-sm-10">
                                <span class="colon">:</span>
                                   <CKEditor:CKEditorControl ID="txtContent"  runat="server" Width="80%" Height="200px" 
                                        BasePath="../../CKEditor/ckeditor" 
                                       CustomConfig="../../CKEditor/ckeditor/config.js" 
                                       FilebrowserBrowseUrl="CMSImages.aspx" 
                                       ></CKEditor:CKEditorControl> 

                                     
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>  
                                  <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">browse file</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control" ></asp:TextBox>
                                       <asp:Button ID="btnBrowse" runat="server" Text="Browse"  OnClientClick="return childpageOpen();"/>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>  
                               <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Meta Title</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:TextBox ID="txtMetaTitle" CssClass="form-control"  Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div> 
                               <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Meta Author</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:TextBox ID="txtMetaAuthor" CssClass="form-control"  Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>   
                               <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Meta KeyWord</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:TextBox ID="txtMetaKeyWord" CssClass="form-control" TextMode="MultiLine" Height="80px"  Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>  
                              <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Meta Description</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:TextBox ID="txtMetaDescription" CssClass="form-control" TextMode="MultiLine" Height="80px" Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>           
                        
                         

                                       
    <br/>
<div style="display:inline" class="nav">	
</div>
   <asp:Button ID="btnSave" runat="server" Text="Save"
                                  CssClass="btn btn-success" onclick="btnSave_Click"/>   
   <asp:Button ID="btncancel" runat="server" Text="Cancel"  CssClass="btn btn-warning" onclick="btncancel_Click" 
                                  />
                                  <asp:HiddenField ID="hdnLastPageName" runat="server" />
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                        </div>
                        </div>
                        </section>
                             <div id="DsModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content modal-primary ">
                            <div class="modal-header bg-red">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                   Department Wise Approvals Details in (<asp:Label ID="lbl44det" runat="server" Text=""></asp:Label>)</h4>
                            </div>
                            <div class="modal-body">
                                <iframe name="myIframe" id="myservcIframe" width="100%" style="height: 298px" runat="server">
                                </iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
    </div>
     <script language="javascript" type="text/javascript">

         function pageLoad(sender, args) {
             var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

             $('#ContentPlaceHolder1_btnSave').click(function () {

                 if (DropDownValidation('ContentPlaceHolder1_drpTemplates', '0', 'Templates', projname) == false) {
                     $("#ContentPlaceHolder1_drpTemplates").focus();
                     return false;
                 }
                 if (blankFieldValidation('ContentPlaceHolder1_txtPageName', 'Page Name', projname) == false) {
                     return false;
                 }
                 if (blankFieldValidation('ContentPlaceHolder1_txtSnippet', 'Snippet', projname) == false) {
                     return false;
                 }
                 var ckValue = CKEDITOR.instances.ContentPlaceHolder1_txtContent.getData();
                 if (ckValue == '') {
                     jAlert('Content can not be left blank !');
                     $('#cke_txtContent').focus();
                     return false;
                 }

                 if (blankFieldValidation('ContentPlaceHolder1_txtMetaTitle', 'Meta Title', projname) == false) {
                     return false;
                 }
                 if (blankFieldValidation('ContentPlaceHolder1_txtMetaAuthor', 'Meta Author', projname) == false) {
                     return false;
                 }
                 if (blankFieldValidation('ContentPlaceHolder1_txtMetaKeyWord', 'Meta KeyWord', projname) == false) {
                     return false;
                 }
                 if (blankFieldValidation('ContentPlaceHolder1_txtMetaDescription', 'Meta Description', projname) == false) {
                     return false;
                 }
             });
         }
//         function ShowAPAA(APAAStatus) {
//             debugger;
//             document.getElementById('ContentPlaceHolder2_myservcIframe').src = "DashboardLandDetails.aspx?Status=" + Status + "&Year=" + Year + "&Type=" + 0 + "&Dist=" + Dist;
//         }
//        
         function popitup(url) {
             newwindow = window.open(url, 'name', 'toolbar=0,menubar=0,location=0');
             if (window.focus) { newwindow.focus() }
             return false;
         }

       
    </script>
        <script>
            function childpageOpen() {
                //window.open("PartySearch.aspx");
                window.open("CMSWithoutCkEdtor.aspx", 'name', 'height=400,width=400');
                return false;
            }
    </script>
</asp:Content>

