<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="AddPlink.aspx.cs" Inherits="Portal_CMS_AddPlink" %>
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
                              <a class="btn btn-add " href="AddPlink.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> ""> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewPlink.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> ""> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>
                           
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                        <div class="panel-body">

                 <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Global Link Name</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                        <asp:DropDownList ID="ddlGlink" runat="server"  CssClass="form-control">
                                        </asp:DropDownList>
                                  <%--<span class="mandetory">*</span>--%>
                                 </div>
                                 </div>
                              </div>
                                 <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Primary Link Name</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:TextBox ID="txtPlinkNmae" CssClass="form-control"  Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>  

                               <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Window Type</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:RadioButtonList ID="rdnWindowType" runat="server" RepeatDirection="Horizontal">
                                       <asp:ListItem Selected="True" Value="1">Same</asp:ListItem>
                                        <asp:ListItem Value="2">New</asp:ListItem>
                                       </asp:RadioButtonList>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>  
                                  <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Page Type</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:RadioButtonList ID="rdnPageType" runat="server" RepeatDirection="Horizontal">
                                       <asp:ListItem Selected="True" Value="1">Normal</asp:ListItem>
                                        <asp:ListItem Value="2">Plugin</asp:ListItem>
                                       </asp:RadioButtonList>
                                
                                 </div>
                                 </div>
                              </div>
                                <div class="form-group">
                                    <div class="row">
                              <label class="col-sm-2">Link Type</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:RadioButtonList ID="rdnLinkType" runat="server" RepeatDirection="Horizontal">
                                       <asp:ListItem Selected="True" Value="1">Internal</asp:ListItem>
                                        <asp:ListItem Value="2">External</asp:ListItem>
                                       </asp:RadioButtonList>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>  
                              <div class="form-group" id="dvURL">
                                    <div class="row">
                              <label class="col-sm-2">URL</label>
                               <div class="col-sm-4">
                                <span class="colon">:</span>
                                       <asp:TextBox ID="txtURL" CssClass="form-control"  Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>
                                  <span class="mandetory">*</span>
                                 </div>
                                 </div>
                              </div>  
                                <div class="form-group" id="dvURL2">
                      <div class="row">
                              <label class="col-sm-2">Page Name</label>
                               <div class="col-sm-4">
                                <span class="colon" id="spn1">:</span>
                                        <asp:DropDownList ID="drpPageList" runat="server"  CssClass="form-control">
                                        </asp:DropDownList>
                                <%--  <span class="mandetory">*</span>--%>
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
    </div>
     <script language="javascript" type="text/javascript">

         function pageLoad(sender, args) {
             $('#dvURL').hide();
             $('#dvURL2').show();
//             $('#spn1').show();
//             var checked_radio2 = $("[id*=ContentPlaceHolder1_rdnLinkType] input:checked");
//             var value2 = checked_radio2.val();
//            
//             if (value2 == 2) {
//                 $('#dvURL').show();
//                 $('#spn1').hide();
//                 $('#dvURL2').hide();
//             }
//             else {
//                 $('#dvURL').hide();
//                 $('#spn1').show();
//                 $('#dvURL2').show();
//             }

             $("#ContentPlaceHolder1_rdnPageType").change(function () {
                 var checked_radio = $("[id*=ContentPlaceHolder1_rdnPageType] input:checked");
                 var checked_radion = $("[id*=ContentPlaceHolder1_rdnLinkType] input:checked");
                 var value1 = checked_radio.val();
                 var value2 = checked_radion.val();
                 if (value1 == 2) {
                     $('#dvURL').show();
                     $('#dvURL2').hide();
                 }

                else if ((value1 == '2') && (value2 == '2')) {

                     $('#dvURL').show();
                     $('#dvURL2').hide();
                 }
                 else if ((value1 == '1') && (value2 == '2')) {

                     $('#dvURL').show();
                     $('#dvURL2').hide();
                 }
                 else if ((value1 == '2') && (value2 == '1')) {
                     $('#dvURL').show();
                     $('#dvURL2').hide();
                 }
                 else if ((value1 == '1') && (value2 == '1')) {

                     $('#dvURL').hide();
                     $('#dvURL2').show();
                 }

                
                 else {
                     $('#dvURL').hide();
                     $('#dvURL2').show();
                 }
             });
             $("#ContentPlaceHolder1_rdnLinkType").change(function () {
                 var checked_radio = $("[id*=ContentPlaceHolder1_rdnPageType] input:checked");
                 var checked_radiom = $("[id*=ContentPlaceHolder1_rdnLinkType] input:checked");

                 var value2 = checked_radiom.val();
                 var value = checked_radio.val();

                 if (value2 == 2) {
                     $('#dvURL').show();
                     $('#spn1').hide();
                     $('#dvURL2').hide();
                 }
                 else if ((value == '2') && (value2 == '2')) {

                     $('#dvURL').show();
                     $('#dvURL2').hide();
                 }
                 else if ((value == '1') && (value2 == '2')) {

                     $('#dvURL').show();
                     $('#dvURL2').hide();
                 }
                 else if ((value == '2') && (value2 == '1')) {
                     $('#dvURL').show();
                     $('#dvURL2').hide();
                 }
                 else if ((value == '1') && (value2 == '1')) {

                     $('#dvURL').hide();
                     $('#dvURL2').show();
                 }
                 else {
                     $('#dvURL').hide();
                     $('#spn1').show();
                     $('#dvURL2').show();
                 }
             });

             var checked_radion = $("[id*=ContentPlaceHolder1_rdnPageType] input:checked");
             var checked_radiom = $("[id*=ContentPlaceHolder1_rdnLinkType] input:checked");
             var value1 = checked_radion.val();
             var value2 = checked_radiom.val();

             if ((value1 == '2') && (value2 == '2')) {
               
                 $('#dvURL').show();
                 $('#dvURL2').hide();
             }
             else if ((value1 == '1') && (value2 == '2')) {
               
                 $('#dvURL').show();
                 $('#dvURL2').hide();
             }
             else if ((value1 == '2') && (value2 == '1')) {
                 $('#dvURL').show();
                 $('#dvURL2').hide();
             }
             else if ((value1 == '1') && (value2 == '1')) {
              
                 $('#dvURL').hide();
                 $('#dvURL2').show();
             }
             else {
                
                 $('#dvURL').hide();
                 $('#dvURL2').show();
             }

             var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

             $('#ContentPlaceHolder1_btnSave').click(function () {

//                 if (DropDownValidation('ContentPlaceHolder1_ddlGlink', '0', 'Global Link Name', projname) == false) {
//                     $("#ContentPlaceHolder1_ddlGlink").focus();
//                     return false;
//                 }
                 if (blankFieldValidation('ContentPlaceHolder1_txtPlinkNmae', 'Primary Link Name', projname) == false) {
                     return false;
                 }
                 var checked_radio1 = $("[id*=ContentPlaceHolder1_rdnLinkType] input:checked");
                 var value1 = checked_radio1.val();
                 if (value1 == 2) {
                     if (blankFieldValidation('ContentPlaceHolder1_txtURL', 'URL', projname) == false) {
                         return false;
                     }
                 }
//                 if (value1 == 1) {
//                     if (DropDownValidation('ContentPlaceHolder1_drpPageList', '0', 'Page Name', projname) == false) {
//                         $("#ContentPlaceHolder1_drpPageList").focus();
//                         return false;
//                     }
//                 }
                
             });
         }

      
        
       
    </script>
</asp:Content>

