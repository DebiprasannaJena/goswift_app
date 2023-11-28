<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="DynamicContent.aspx.cs" Inherits="Portal_CMS_DynamicContent" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <script type="text/javascript" src="../../js/CSMValidation.js"></script>
<%--  <script type="text/javascript" src="../../js/jquery.richtext.js"></script>--%>
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
 <script>
     $(document).ready(function () {
         debugger;
         var vrhdnvl = $('#<%=hdnTempVl.ClientID%>').val(); //$('#hdnTempVl').val();


         if (vrhdnvl != "" && vrhdnvl != null) {
             if (vrhdnvl == "0") {
                 $(".layoutsec").find("#cont").show();
                 $(".layoutsec").find("#cont1").hide();
                 $(".layoutsec").find("#cont2").hide();
                 $(".layoutsec").find("#cont3").hide();
             }
             else if (vrhdnvl == "1") {
                 $(".layoutsec").find("#cont").hide();
                 $(".layoutsec").find("#cont1").show();
                 $(".layoutsec").find("#cont2").show();
                 $(".layoutsec").find("#cont3").hide();


             }
             else if (vrhdnvl == "2") {
                 $(".layoutsec").find("#cont").hide();
                 $(".layoutsec").find("#cont1").show();
                 $(".layoutsec").find("#cont2").show();
                 $(".layoutsec").find("#cont3").show();
             }

             else if (vrhdnvl == "3") {
                 $(".layoutsec").find("#cont").show();
                 $(".layoutsec").find("#cont1").show();
                 $(".layoutsec").find("#cont2").show();
                 $(".layoutsec").find("#cont3").hide();
             }
             else if (vrhdnvl == "4") {
                 $(".layoutsec").find("#cont").show();
                 $(".layoutsec").find("#cont1").show();
                 $(".layoutsec").find("#cont2").show();
                 $(".layoutsec").find("#cont3").show();
             }
             else if (vrhdnvl == "5") {
                 $(".layoutsec").find("#cont").hide();
                 $(".layoutsec").find("#cont1").show();
                 $(".layoutsec").find("#cont2").show();
                 $(".layoutsec").find("#cont3").show();
             }
             else if (vrhdnvl == "6") {
                 $(".layoutsec").find("#cont").show();
                 $(".layoutsec").find("#cont1").show();
                 $(".layoutsec").find("#cont2").show();
                 $(".layoutsec").find("#cont3").show();
             }
             else if (vrhdnvl == "7") {
                 $(".layoutsec").find("#cont").hide();
                 $(".layoutsec").find("#cont1").show();
                 $(".layoutsec").find("#cont2").show();
                 $(".layoutsec").find("#cont3").show();
             }
         }

     });
 </script>
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
                AllowableCharacters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
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
<%--   <script type="text/javascript" language="javascript">
       Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
       function EndRequestHandler(sender, args) {
           if (args.get_error() != undefined) {
               args.set_errorHandled(true);
           }
       }
</script>--%>
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
                              <a class="btn btn-add " href="ViewPage.aspx?linkm=<%=Request.QueryString["linkm"]%>&linkn=<%= Request.QueryString["linkn"]%>&btn=<%=Request.QueryString["btn"]%> &tab=<%=Request.QueryString["tab"]%> <%=Request.QueryString["index"]%> "> 
                              <i class="fa fa-file"></i> View </a>  
                           </div>
                           
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                        <div class="panel-body">

                        
                            
                                <div class="form-group" id="divPage">
                      <div class="row" >
                              <label class="col-sm-2">Page Name</label>
                               <div class="col-sm-4">
                                <span class="colon" id="spn1">:</span>
                                    <asp:TextBox ID="txtPageName" CssClass="form-control"  Onkeypress="return inputLimiter(event,'PageName')" runat="server" onkeyup="nospaces(this)"></asp:TextBox>
                                    <asp:HiddenField ID="hdnTempVl" runat="server" />
                                      <%--<asp:TextBox ID="txtPageName" runat="server"></asp:TextBox>--%>
                                  <span class="mandetory">*</span> 
                                 </div>
                                 </div>
                                 </div>     
                                 
                              
   <br /><br />
   <section class="thumbsec">
	
		<div class="container-fluid">
			<div class="row">
				<div class="col-sm-12">
                    <div class="jthumbs">
                        <i class="fa fa-eye"></i>
                        <figure><img src="../../images/tmb0.png"></figure> 
                        <img src="../../images/tmb0.png">
                         <label class="myradio">
                      
                            <input type="radio" name="radio" onclick="SetValue('0');" runat="server" id="rdbId0" class="rad0"/>
                            <span class="checkmark"></span>
                          </label>     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                    

					<div class="jthumbs">
                        <i class="fa fa-eye"></i>
                        <figure><img src="../../images/tmb1.png"></figure> 
                        <img src="../../images/tmb1.png">
                         <label class="myradio">
                      
                            <input type="radio" name="radio" onclick="SetValue('1');" runat="server" id="rdbId1" class="rad1"/>
                            <span class="checkmark"></span>
                          </label>     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                    <div class="jthumbs">
                            <i class="fa fa-eye"></i>
                            <figure><img src="../../images/tmb2.png"></figure>
                        <img src="../../images/tmb2.png">   
                        <label class="myradio">
                            <input type="radio" name="radio" runat="server" onclick="SetValue('2');" id="rdbId2" class="rad2"/>
                            <span class="checkmark"></span>
                          </label>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                   <div class="jthumbs">
                        <i class="fa fa-eye"></i>
                        <figure><img src="../../images/tmb3.png"></figure>
                         <img src="../../images/tmb3.png">   
                         <label class="myradio">
                            <input type="radio" name="radio"  runat="server" onclick="SetValue('3');" id="rdbId3" class="rad3" />
                            <span class="checkmark"></span>
                          </label>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                    <div class="jthumbs">
                            <i class="fa fa-eye"></i>
                            <figure><img src="../../images/tmb4.png"></figure>
                        <img src="../../images/tmb4.png">   
                        <label class="myradio">
                            <input type="radio" name="radio"  runat="server" onclick="SetValue('4');" id="rdbId4" class="rad4"/>
                            <span class="checkmark"></span>
                          </label>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                   <div class="jthumbs">
                        <i class="fa fa-eye"></i>
                        <figure><img src="../../images/tmb5.png"></figure>
                         <img src="../../images/tmb5.png">   
                         <label class="myradio">
                            <input type="radio" name="radio"  runat="server" onclick="SetValue('5');" id="rdbId5" class="rad5"/>
                            <span class="checkmark"></span>
                          </label>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                    <div class="jthumbs">
                            <i class="fa fa-eye"></i>
                            <figure><img src="../../images/tmb6.png"></figure>
                        <img src="../../images/tmb6.png">   
                        <label class="myradio">
                            <input type="radio" name="radio" runat="server" onclick="SetValue('6');" id="rdbId6" class="rad6"/>
                            <span class="checkmark"></span>
                          </label>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                   <div class="jthumbs">
                        <i class="fa fa-eye"></i>
                        <figure><img src="../../images/tmb7.png"></figure>
                         <img src="../../images/tmb7.png">   
                         <label class="myradio">
                            <input type="radio" name="radio" runat="server" onclick="SetValue('7');" id="rdbId7" class="rad7"/>
                            <span class="checkmark"></span>
                          </label>  
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                    
				</div>
			</div>
		</div>
</section>
                               <br />
                               <br />        <br />
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                        Meta Title</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="form-control" 
                                                Onkeypress="return inputLimiter(event,'Address')"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                        Meta Author</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtMetaAuthor" runat="server" CssClass="form-control" 
                                                Onkeypress="return inputLimiter(event,'Address')"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                        Meta KeyWord</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtMetaKeyWord" runat="server" CssClass="form-control" 
                                                Height="80px" Onkeypress="return inputLimiter(event,'Address')" 
                                                TextMode="MultiLine"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                        Meta Description</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtMetaDescription" runat="server" CssClass="form-control" 
                                                Height="80px" Onkeypress="return inputLimiter(event,'Address')" 
                                                TextMode="MultiLine"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                </div>
                                <hr>
                                <section class="layoutsec">
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div ID="cont" class="col-sm-12 br-bottom br-top">
                                                <CKEditor:CKEditorControl ID="CKEditorControl0"   runat="server" 
                                                    BasePath="../../CKEditor/ckeditor" 
                                                    CustomConfig="../../CKEditor/ckeditor/config.js" 
                                                    FilebrowserBrowseUrl="CMSImages.aspx" Height="200px" EnterMode="BR">
                                                </CKEditor:CKEditorControl>
                                                <%-- <textarea class="dcontent" id="c1" runat="server" name="example"></textarea>--%>
                                            </div>
                                            <div ID="cont1" class="col-sm-3 br-right">
                                                <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server" 
                                                    BasePath="../../CKEditor/ckeditor" 
                                                    CustomConfig="../../CKEditor/ckeditor/config.js" 
                                                    FilebrowserBrowseUrl="CMSImages.aspx" Height="200px" EnterMode="BR">
                                                </CKEditor:CKEditorControl>
                                                <%--		<textarea class="dcontent1" name="example"></textarea>--%>
                                            </div>
                                            <div ID="cont2" class="col-sm-6">
                                                <CKEditor:CKEditorControl ID="CKEditorControl2" runat="server" 
                                                    BasePath="../../CKEditor/ckeditor" 
                                                    CustomConfig="../../CKEditor/ckeditor/config.js" 
                                                    FilebrowserBrowseUrl="CMSImages.aspx" Height="200px" EnterMode="BR">
                                                </CKEditor:CKEditorControl>
                                                <%--                        <textarea class="dcontent2" name="example"></textarea>--%>
                                            </div>
                                            <div ID="cont3" class="col-sm-3 br-left">
                                                <CKEditor:CKEditorControl ID="CKEditorControl3" runat="server" 
                                                    BasePath="../../CKEditor/ckeditor" 
                                                    CustomConfig="../../CKEditor/ckeditor/config.js" 
                                                    FilebrowserBrowseUrl="CMSImages.aspx" Height="200px" EnterMode="BR">
                                                </CKEditor:CKEditorControl>
                                                <%--             <textarea class="dcontent3" name="example"></textarea>--%>
                                            </div>
                                        </div>
                                    </div>
                                </section>
                                <hr />
                                <div class="nav" style="display:inline">
                                </div>
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" 
                                     Text="Save" onclick="btnSave_Click"/>
                                <asp:Button ID="btncancel" runat="server" CssClass="btn btn-warning" 
                                    onclick="btncancel_Click" Text="Cancel" />
                                <asp:HiddenField ID="hdnLastPageName" runat="server" />
                                </hr>
                             
                               
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                        </div>
                        </div>
                        </section>

    </div><script>
    $(document).ready(function () {
//        $(".layoutsec").find("#cont").hide();
//        $(".layoutsec").find("#cont1").hide();
//        $(".layoutsec").find("#cont2").hide();
//        $(".layoutsec").find("#cont3").hide();

        $("input[class$='rad0']").click(function () {
            $(".layoutsec").find("#cont").slideDown().removeClass().addClass("col-sm-12");
            $(".layoutsec").find("#cont1").slideUp();
            $(".layoutsec").find("#cont2").slideUp();
            $(".layoutsec").find("#cont3").slideUp();
        });
        $("input[class$='rad1']").click(function () {
            $(".layoutsec").find("#cont").slideUp();
            $(".layoutsec").find("#cont1").slideDown().removeClass().addClass("col-sm-3");
            $(".layoutsec").find("#cont2").slideDown().removeClass().addClass("col-sm-9");
            $(".layoutsec").find("#cont3").slideUp();
        });
        $("input[class$='rad2']").click(function () {
            $(".layoutsec").find("#cont").slideUp();
            $(".layoutsec").find("#cont1").slideDown().removeClass().addClass("col-sm-3");
            $(".layoutsec").find("#cont2").slideDown().removeClass().addClass("col-sm-6");
            $(".layoutsec").find("#cont3").slideDown();
        });

        $("input[class$='rad3']").click(function () {
            $(".layoutsec").find("#cont").slideDown();
            $(".layoutsec").find("#cont1").slideDown().removeClass().addClass("col-sm-9");
            $(".layoutsec").find("#cont2").slideDown().removeClass().addClass("col-sm-3");
            $(".layoutsec").find("#cont3").slideUp();
        });
        $("input[class$='rad4']").click(function () {
            $(".layoutsec").find("#cont").slideDown();
            $(".layoutsec").find("#cont1").slideDown().removeClass().addClass("col-sm-3");
            $(".layoutsec").find("#cont2").slideDown().removeClass().addClass("col-sm-6");
            $(".layoutsec").find("#cont3").slideDown().removeClass().addClass("col-sm-3");
        });
        $("input[class$='rad5']").click(function () {
            $(".layoutsec").find("#cont").slideUp();
            $(".layoutsec").find("#cont1").slideDown().removeClass().addClass("col-sm-6");
            $(".layoutsec").find("#cont2").slideDown().removeClass().addClass("col-sm-3");
            $(".layoutsec").find("#cont3").slideDown().removeClass().addClass("col-sm-3");
        });
        $("input[class$='rad6']").click(function () {
            $(".layoutsec").find("#cont").slideDown();
            $(".layoutsec").find("#cont1").slideDown().removeClass().addClass("col-sm-3");
            $(".layoutsec").find("#cont2").slideDown().removeClass().addClass("col-sm-6");
            $(".layoutsec").find("#cont3").slideDown().removeClass().addClass("col-sm-3");
        });
        $("input[class$='rad7']").click(function () {
            $(".layoutsec").find("#cont").slideUp();
            $(".layoutsec").find("#cont1").slideDown().removeClass().addClass("col-sm-3");
            $(".layoutsec").find("#cont2").slideDown().removeClass().addClass("col-sm-6");
            $(".layoutsec").find("#cont3").slideDown().removeClass().addClass("col-sm-3");
        });

        $(".thumbsec .jthumbs>i").mouseover(function () {
            $(this).parent().find("figure").slideDown();

        });
        $(".thumbsec .jthumbs>i").mouseout(function () {
            $(this).parent().find("figure").slideUp();

        });
    });
    </script>
           <script>
               function childpageOpen() {
                   //window.open("PartySearch.aspx");
                   window.open("CMSWithoutCkEdtor.aspx", 'name', 'height=400,width=400');
                   return false;
               }


             
//               alert($('#txtContent0').val();
    </script>
     <style type="text/css" media="all">
    /* fix rtl for demo */
    .chosen-rtl .chosen-drop { left: -9000px; }
    .chosen-container .chosen-container-single 

.chosen-single{ width:100% !important;}
  </style>
  <link href="../../css/style1.css" rel="stylesheet" type="text/css" />
  <link href="../../css/richtext.min.css" rel="stylesheet" type="text/css" />
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

              if (blankFieldValidation('ContentPlaceHolder1_hdnTemplate', 'Template', projname) == false) {
                  return false;
              }


              if ($('#ContentPlaceHolder1_hdnTemplate').val() == "0") {
                  var idname = $("#ContentPlaceHolder1_CKEditorControl0").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
              }
             else if ($('#ContentPlaceHolder1_hdnTemplate').val() == "1") {

                  var idname = $("#ContentPlaceHolder1_CKEditorControl1").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }

                  var idname = $("#ContentPlaceHolder1_CKEditorControl2").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
              }

             else if ($('#ContentPlaceHolder1_hdnTemplate').val() == "2") {

                  var idname = $("#ContentPlaceHolder1_CKEditorControl1").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }

                  var idname = $("#ContentPlaceHolder1_CKEditorControl2").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
                  var idname = $("#ContentPlaceHolder1_CKEditorControl3").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
              }
              else if ($('#ContentPlaceHolder1_hdnTemplate').val() == "3") {

                  var idname = $("#ContentPlaceHolder1_CKEditorControl0").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }

                  var idname = $("#ContentPlaceHolder1_CKEditorControl1").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
                  var idname = $("#ContentPlaceHolder1_CKEditorControl2").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
              }
             else if ($('#ContentPlaceHolder1_hdnTemplate').val() == "4") {

                  var idname = $("#ContentPlaceHolder1_CKEditorControl0").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }

                  var idname = $("#ContentPlaceHolder1_CKEditorControl1").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }

                  var idname = $("#ContentPlaceHolder1_CKEditorControl2").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
                  var idname = $("#ContentPlaceHolder1_CKEditorControl3").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
              }
             else if ($('#ContentPlaceHolder1_hdnTemplate').val() == "5") {

                  var idname = $("#ContentPlaceHolder1_CKEditorControl1").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }

                  var idname = $("#ContentPlaceHolder1_CKEditorControl2").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
                  var idname = $("#ContentPlaceHolder1_CKEditorControl3").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
              }

             else if ($('#ContentPlaceHolder1_hdnTemplate').val() == "6") {

                  var idname = $("#ContentPlaceHolder1_CKEditorControl0").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }

                  var idname = $("#ContentPlaceHolder1_CKEditorControl1").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }

                  var idname = $("#ContentPlaceHolder1_CKEditorControl2").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
                  var idname = $("#ContentPlaceHolder1_CKEditorControl3").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
              }
             else if ($('#ContentPlaceHolder1_hdnTemplate').val() == "7") {

                  var idname = $("#ContentPlaceHolder1_CKEditorControl1").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }

                  var idname = $("#ContentPlaceHolder1_CKEditorControl2").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
                  var idname = $("#ContentPlaceHolder1_CKEditorControl3").attr('id');
                  var editor = CKEDITOR.instances[idname];
                  var ckValue = editor.getData().trim();
                  if (ckValue.length === 0) {
                      jAlert('Please add content !');
                      editor.focus();
                      return false;
                  }
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
      function nospaces(t) {
          if (t.value.match(/\s/g)) {
              t.value = t.value.replace(/\s/g, '');
          }
      }
       
    </script>
    <asp:HiddenField ID="hdnTemplate" runat="server" />
        <script>

            function SetValue(temp) {
                document.getElementById("ContentPlaceHolder1_hdnTemplate").value = temp;
            }
        </script>
</asp:Content>



