<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="AddGlink.aspx.cs" Inherits="Portal_CMS_AddGlink" %>

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
            if (allow == 'IDName') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
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
                  <h1>Manage Global Link</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Manage Global Link</a></li><li><a>Add Global Link</a></li></ul>
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
                              <a class="btn btn-add " href="AddGlink.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> "> 
                              <i class="fa fa-plus"></i>  Add </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewGlink.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> "> 
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
                                                            <asp:TextBox ID="txtGlink" CssClass="form-control"  Onkeypress="return inputLimiter(event,'NameCharacters')" runat="server"></asp:TextBox>
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
                                                                <asp:ListItem Value="3">Popup Modal</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <span class="mandetory">*</span>
                                                    </div>
                                                </div>
                                            </div>  
                              
                                               <%--   <div class="form-group">
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
                                                   --%>
                              
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
                                                    <div class="row" >
                                                        <label class="col-sm-2">Page Name</label>
                                                            <div class="col-sm-4">
                                                                <span class="colon" id="spn1">:</span>
                                                                    <asp:DropDownList ID="drpPageList" runat="server"   CssClass="form-control chosen-select-width">
                                                                    </asp:DropDownList>
                                                                    <%--  <span class="mandetory">*</span> CssClass="form-control"--%>
                                                            </div>
                                                    </div>
                                             </div>                 
                                            
                                            <div class="form-group" id ="divModal">
                                                <div class="row">
                                                    <label class="col-sm-2">Modal Id</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                            <asp:TextBox ID="txtModalId" CssClass="form-control"  Onkeypress="return inputLimiter(event,'IDName')" runat="server"></asp:TextBox>
                                                            <span class="mandetory">*</span>
                                                    </div>
                                                </div>
                                            </div>  
                                            
                                            <br/>
                                                <div style="display:inline" class="nav">	</div>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" onclick="btnSave_Click"/>   
                                                <asp:Button ID="btncancel" runat="server" Text="Cancel"  CssClass="btn btn-warning" onclick="btncancel_Click"/>
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
             var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
             $('#dvURL').hide();
             $('#dvURL2').hide();
             $('#divModal').hide();
             var checked_radio2 = $("[id*=ContentPlaceHolder1_rdnPageType] input:checked");
             var checked_radioModal = $("[id*=ContentPlaceHolder1_rdnWindowType] input:checked");
             var modalSelectedvalue = checked_radioModal.val();
             var value2 = checked_radio2.val();

             //chek for window type is Modal
             if (modalSelectedvalue == 3) {
                 $('#divModal').show();
             }
             else {
                 $('#divModal').hide();
             }
             //check page type
             if (value2 == 2) {
                 $('#dvURL').show();
                 $('#dvURL2').hide();
             }
             else {
                 $('#dvURL').hide();
                 $('#dvURL2').show();
             }
         $('#ContentPlaceHolder1_btnSave').click(function () {


                 if (blankFieldValidation('ContentPlaceHolder1_txtGlink', 'Global Link Name', projname) == false) {
                     return false;
                 }
                 var checked_radioModal = $("[id*=ContentPlaceHolder1_rdnWindowType] input:checked");
                 var modalSelectedvalue = checked_radioModal.val();
                 if (modalSelectedvalue == 3 ) {
                     if (blankFieldValidation('ContentPlaceHolder1_txtModalId', 'Modal Id Name', projname) == false) {
                         return false;
                     }
                 }
                 

                
             });
         }
         $('#dvURL').hide();
         $('#dvURL2').hide();
         $('#divModal').hide();
       
         var checked_radio2 = $("[id*=ContentPlaceHolder1_rdnPageType] input:checked");
         var checked_radioModal = $("[id*=ContentPlaceHolder1_rdnWindowType] input:checked");
         var modalSelectedvalue = checked_radioModal.val();
         var value2 = checked_radio2.val();

         //chek for window type is Modal
         if (modalSelectedvalue == 3) {
             $('#divModal').show();
         }
         else {
             $('#divModal').hide();
         }
         //check page type
         if (value2 == 2) {
             $('#dvURL').show();
             $('#dvURL2').hide();          
         }
         else {
             $('#dvURL').hide();
             $('#dvURL2').show();
         }

         $("#ContentPlaceHolder1_rdnPageType").change(function () {
             var checked_radio = $("[id*=ContentPlaceHolder1_rdnPageType] input:checked");
             var value = checked_radio.val();
             if (value == 2) {
                 $('#dvURL').show();
                 $('#dvURL2').hide();
             }
             else {
                 $('#dvURL').hide();
                 $('#dvURL2').show();
             }
         });
         $("#ContentPlaceHolder1_rdnWindowType").change(function () {
             var checked_radioModal = $("[id*=ContentPlaceHolder1_rdnWindowType] input:checked");
             var valueModal = checked_radioModal.val();
             if (valueModal == 3) {
                 
                 $('#divModal').show();
             }
             else {
                 $('#divModal').hide();
             }
         });
      
       
     </script>
     <style type="text/css" media="all">
    /* fix rtl for demo */
    .chosen-rtl .chosen-drop { left: -9000px; }
    .chosen-container .chosen-container-single 

.chosen-single{ width:100% !important;}
  </style>
                                <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "100%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }

  </script>
</asp:Content>

