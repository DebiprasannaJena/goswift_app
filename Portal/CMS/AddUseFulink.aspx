<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="AddUseFulink.aspx.cs" Inherits="Portal_CMS_AddUseFulink" %>

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
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Manage Useful Link</a></li><li><a>Add Useful Link</a></li></ul>
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
                              <a class="btn btn-add " href="AddUseFulink.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> "">  
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewUseFulink.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> "">  
                              <i class="fa fa-file"></i> View </a>  
                           </div>
                           
                        </div>
                      <%--  <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>--%>
                        <div class="panel-body">

             
                                 <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Useful Link Name</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:TextBox ID="txtUsefulLink" CssClass="form-control"  Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>  

                              <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">URL</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:TextBox ID="txtURL" CssClass="form-control"  Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>
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
                              <asp:HyperLink ID="hlnkfile1" runat="server" Font-Bold="true" Target="_blank"></asp:HyperLink>
                                  <asp:HiddenField ID="hdnFile" runat="server" />
                                 </div>  
                                 </div>
                              </div>  
    <br/>
<div style="display:inline" class="nav">	
</div>
   <asp:Button ID="btnSave" runat="server" Text="Save"
                                  CssClass="btn btn-success" onclick="btnSave_Click" />   
   <asp:Button ID="btncancel" runat="server" Text="Cancel"  CssClass="btn btn-warning" onclick="btncancel_Click" 
                                  />
                                  <asp:HiddenField ID="hdnLastPageName" runat="server" />
                        </div>
                      <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        </div>
                        </div>
                        </div>
                        </section>
    </div>
     
</asp:Content>
