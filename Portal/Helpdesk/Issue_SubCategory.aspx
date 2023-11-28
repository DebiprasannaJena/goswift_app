<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="Issue_SubCategory.aspx.cs" Inherits="Portal_HelpDesk_Issue_SubCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('.ddluser').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });
            $('.ddlInvestor').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });
        });

        function validation() {
            var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'


            if ($('#ContentPlaceHolder1_ddlCategory').val() == 0) {
                jAlert('Please Select Category !');
                $('#ContentPlaceHolder1_ddlCategory').focus()
                return false;
            }
            if ($('#ContentPlaceHolder1_txtSubCategory').val() == '') {
                jAlert('Please Select SubCategory Name !');
                $('#ContentPlaceHolder1_txtSubCategory').focus()
                return false;
            }
           
            

        }

        function inputLimiter(e, allow) {

            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'AlphanumericSpccharc') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"<>=';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._';
            }
            if (allow == 'Address') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#-,./;\'';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
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
    <script type="text/javascript" language="javascript">
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
    <script type="text/javascript" language="javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    for (var selector in config) {
                        $(selector).chosen(config[selector]);
                    }

                }
            });
        };
    </script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>
    <style>
        /* Comments
---------------------------------- */
        .comments
        {
            margin-top: 60px;
        }
        .comments h2.title
        {
            margin-bottom: 40px;
            border-bottom: 1px solid #d2d2d2;
            padding-bottom: 10px;
        }
        .comment
        {
            font-size: 14px;
        }
        .comment .comment
        {
            margin-left: 75px;
        }
        .comment-avatar
        {
            margin-top: 5px;
            width: 55px;
            float: left;
        }
        .comment-content
        {
            border-bottom: 1px solid #d2d2d2;
            margin-bottom: 25px;
        }
        .comment h3
        {
            margin-top: 0;
            margin-bottom: 5px;
        }
        .comment-meta
        {
            margin-bottom: 15px;
            color: #999999;
            font-size: 12px;
        }
        .comment-meta a
        {
            color: #666666;
        }
        .comment-meta a:hover
        {
            text-decoration: underline;
        }
        .comment .btn
        {
            font-size: 12px;
            padding: 7px;
            min-width: 100px;
            margin-top: 5px;
            margin-bottom: -1px;
        }
        .btn-gray
        {
            color: #ffffff;
            background-color: #666666;
            border-color: #666666;
        }
        .comment .btn i
        {
            padding-right: 5px;
        }
        
        .control-label
        {
            border: 1px solid #b9bdbf;
            padding: 6px 10px;
            border-radius: 2px;
            height: 31px;
            width: 100%;
            background: #f9f9f9;
            display: block;
            margin: 0px;
        }
        
        .chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        .chosen-container .chosen-container-single .chosen-single
        {
            width: 100% !important;
        }
        .searchbox
        {
            background-color: #def3ff;
            padding: 8px;
        }
    </style>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Issue SubCategory</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>HelpDesk</a></li><li><a>Add</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                 
                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>                        
                                                
                                          
                  <div class="col-sm-12">
                   <div class="panel panel-bd lobidisable"> 
                <div class="panel-heading">
                        <div class="btn-group buttonlist" > 
                            <a class="btn btn-add " href="Issue_SubCategory.aspx"> 
                            <i class="fa fa-plus"></i>Add</a>  
                        </div>
                        <div class="btn-group buttonlist" > 
                            <a class="btn btn-add " href="ViewIssue_SubCategory.aspx"> 
                            <i class="fa fa-file"></i>View</a>  
                        </div>
                           
                </div>   
                                
                    <div class="panel-body">
                    <%-- <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                       <ContentTemplate>   --%>
                    
                                                    <div class="form-group">
                                   <div class="row">
                                          <label class="col-sm-2">Type <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                      
                                       
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlType" runat="server"    AutoPostBack="True"  onselectedindexchanged="ddlType_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                  
                                 </div>
                                 <label class="col-sm-2">Category <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                      
                                       
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlCategory" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                  
                                 </div>
                                   
                                   
                         
                               
                                
                                 </div>
                                 </div>
                                           <div class="form-group">
                                 <div class="row">
                            
                                        <label class="col-sm-2">Sub-Category  <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                      
                                    <asp:TextBox ID="txtSubCategory" MaxLength="100"  CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')"></asp:TextBox>
                                                  
                                 </div>
                              <label class="col-sm-2">Escalation Level  <span class="text-red">*</span></label>
                                    <div class="col-sm-3">
                                                       
                                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlEscLvl" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
    <asp:ListItem Value="1">1</asp:ListItem>
    <asp:ListItem Value="2">2</asp:ListItem>
    <asp:ListItem Value="3">3</asp:ListItem>
    <asp:ListItem Value="4">4</asp:ListItem>
    <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                            
                                                        </asp:DropDownList>

                                                    </div>


                              </div>
                               </div>
                           

 <%--</ContentTemplate>
                                                        
                                                         </asp:UpdatePanel>--%>

                                   <div class="form-group">
                                 <div class="row">
                            
                              
                             
                                    <div class="col-sm-4">
                                                       
                                                     <asp:Button ID="btnSave" 
                                                             CssClass="btn btn btn-success" runat="server" Text="Save" onclick="btnSave_Click"  OnClientClick="return validation();" 
                                                             ></asp:Button>
                                                             <asp:Button ID="btnCancel" 
                                                             CssClass="btn btn btn-warning" runat="server" Text="Cancel" onclick="btnCancel_Click"  OnClientClick="return validation();" 
                                                             ></asp:Button>

                                                    </div>
                              </div>
                               </div>
                    </div>

                    
             
                  </div>
               </div>
                
            </section>
        <!-- /.content -->
    </div>
    <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
    </div>
</asp:Content>
