<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="Issue_Registration.aspx.cs" Inherits="Portal_HelpDesk_Issue_Registration" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('.ddluser').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });
            $('.ddlInvestor').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });
        });
     
        function validation() {
            var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

            
            if ($('#ContentPlaceHolder1_ddlPriority').val() == 0) {
                jAlert('Please Select Priority !');
                $('#ContentPlaceHolder1_ddlPriority').focus()
                return false;
            }
            if ($('#ContentPlaceHolder1_ddlCategory').val() == 0) {
                jAlert('Please Select Category Name !');
                $('#ContentPlaceHolder1_ddlCategory').focus()
                return false;
            }
            if ($('#ContentPlaceHolder1_ddlSubcategory').val() == 0) {
                jAlert('Please Select SubCategory Name !');
                $('#ContentPlaceHolder1_ddlSubcategory').focus()
                return false;
            }
            if ($('#ContentPlaceHolder1_drpStatus').val() == 0) {
                jAlert('Please Select Request Source  !');
                $('#ContentPlaceHolder1_drpStatus').focus()
                return false;
            }
            if ($('#ContentPlaceHolder1_txtIssue').val() == '') {
                jAlert('Please Enter Issue Status !');
                $('#ContentPlaceHolder1_txtIssue').focus()
                return false;
            }
            if ($('#ContentPlaceHolder1_txtEmail').val() == '') {
                jAlert('Please Enter Email !');
                $('#ContentPlaceHolder1_txtEmail').focus()
                return false;
            }
            if (!ValidateEmail($("#ContentPlaceHolder1_txtEmail").val())) {
                alert("Invalid email address.");
                return false;
            }
            else {
                return true;
               // alert("Valid email address.");
            }
            if ($("#ddlType").val() == "1") {

                if ($('#ContentPlaceHolder1_ddldepartment').val() == 0) {
                    jAlert('Please Select Department Name !');
                    $('#ContentPlaceHolder1_drpStatus').focus()
                    return false;
                }
                if ($('#ContentPlaceHolder1_ddlUser').val() == 0) {
                    jAlert('Please Select User Name !');
                    $('#ContentPlaceHolder1_ddlUser').focus()
                    return false;
                }
                

            }
            if ($("#ddlType").val() == "2") {
                if ($('#ContentPlaceHolder1_ddlInvestor').val() == 0) {
                    jAlert('Please Select Investor Name !');
                    $('#ContentPlaceHolder1_ddlInvestor').focus()
                    return false;
                }
               
            }
            if ($("#ddlType").val() == "3") {

                if ($('#ContentPlaceHolder1_txtOName').val() == '') {
                    jAlert('Please Enter Other Name !');
                    $('#ContentPlaceHolder1_txtOName').focus()
                    return false;
                }
                if ($('#ContentPlaceHolder1_txtOName').val() == '') {
                    jAlert('Please Enter Mobile no !');
                    $('#ContentPlaceHolder1_txtOName').focus()
                    return false;
                }
                if ($('#ContentPlaceHolder1_txtAddress').val() == '') {
                    jAlert('Please Enter Address !');
                    $('#ContentPlaceHolder1_txtAddress').focus()
                    return false;
                }
            }

        }
        function ValidateEmail(email) {
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            return expr.test(email);
        };
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
  
    .control-label {
    border: 1px solid #b9bdbf;
    padding: 6px 10px;
    border-radius: 2px;
    height: 31px;
    width: 100%;
    background: #f9f9f9;
    display: block;
    margin: 0px;
}
  
.chosen-rtl .chosen-drop { left: -9000px; }
.chosen-container .chosen-container-single .chosen-single{ width:100% !important;}
.searchbox {
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
                  <h1>Issue Registration</h1>
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
                            <a class="btn btn-add " href="Issue_Registration.aspx"> 
                            <i class="fa fa-plus"></i>Add</a>  
                        </div>
                        <div class="btn-group buttonlist" > 
                            <a class="btn btn-add " href="ViewIssueRegistration.aspx"> 
                            <i class="fa fa-file"></i>View</a>  
                        </div>
                           
                </div>   
                                
                    <div class="panel-body">
                     <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                       <ContentTemplate>   
                    <div class="form-group">
                                   <div class="row">
                                   <label class="col-sm-2">Priority <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                      
                                       
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlPriority" runat="server" >
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">High</asp:ListItem>
                                                            <asp:ListItem Value="2">Medium</asp:ListItem>
                                                            <asp:ListItem Value="3">Low</asp:ListItem>
                                                        </asp:DropDownList>
                                                  
                                 </div>

                                    <label class="col-sm-2">Type <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                      
                                       
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlType" runat="server" 
                                            AutoPostBack="True" onselectedindexchanged="ddlType_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                         
                                                        </asp:DropDownList>
                                                  
                                 </div>
                                
                                 </div>
                                 </div>
<div class="form-group">
                                   <div class="row">
                                    <label class="col-sm-2">Category <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlCategory" 
                                            runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlCategory_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                           <%-- <asp:ListItem Value="1">Department</asp:ListItem>
                                                            <asp:ListItem Value="2">Investor</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                 </div>
                                  <label class="col-sm-2">Sub Category <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlSubcategory" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                           <%-- <asp:ListItem Value="1">Department</asp:ListItem>
                                                            <asp:ListItem Value="2">Investor</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                 </div>
                                 
                                
 </div>
                                 </div>
                                  <div class="form-group" runat="server" id="DvDept">
                                   <div class="row">
                                    
                                  <label class="col-sm-2">Department <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddldepartment" 
                                            runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddldepartment_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                           <%-- <asp:ListItem Value="1">Department</asp:ListItem>
                                                            <asp:ListItem Value="2">Investor</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                 </div>
                                 
                                  <label class="col-sm-2">User <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:DropDownList TabIndex="17" ID="ddlUser" runat="server" 
                                            CssClass="chosen-select-width ddluser"  style="width:100%" 
                                            onselectedindexchanged="ddlUser_SelectedIndexChanged" AutoPostBack="true" >
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                           <%-- <asp:ListItem Value="1">Department</asp:ListItem>
                                                            <asp:ListItem Value="2">Investor</asp:ListItem>--%>
                                                        </asp:DropDownList>
                               
                                 </div>
                                 
                             </div>
                        </div>
                         <div class="form-group" runat="server" id="userdetails" >
                                   <div class="row">
                                
                                  <label class="col-sm-2">Name <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                   <asp:TextBox ID="txtOName" MaxLength="50"  CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                 </div>
                                
                                  
                                 
                                
                                
                                  <label class="col-sm-2">Mobile <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                   <asp:TextBox ID="txtMobile" MaxLength="50"  CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                 </div>
                                 
                             </div>
                        </div>
<div class="form-group">
                                   <div class="row">
                                   <div id="divInvest" runat="server" >
                                  <label class="col-sm-2">Investor <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:DropDownList  TabIndex="17" ID="ddlInvestor" CssClass="chosen-select-width ddlInvestor"
                                            runat="server" onselectedindexchanged="ddlInvestor_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                           <%-- <asp:ListItem Value="1">Department</asp:ListItem>
                                                            <asp:ListItem Value="2">Investor</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                 </div>
                                 </div>

                                 <div id="useraddress" runat="server">
                                  <label class="col-sm-2">Address <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:TextBox ID="txtAddress" MaxLength="500" TextMode="MultiLine" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Address')"></asp:TextBox>
                                 </div>
                                 </div>
                                <label class="col-sm-2">Issue Details <span class="text-red">*</span></label>
                               <div class="col-sm-3">
                               
                                <span class="colon">:</span>
                                  <asp:TextBox ID="txtIssue" MaxLength="500" TextMode="MultiLine" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Address')"></asp:TextBox>
                                          </div> 

                                           </div>
                                 </div>
 
<div class="form-group">
                                   <div class="row">
                                           <label class="col-sm-2">Request Source <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpStatus" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Email</asp:ListItem>
                                                            <asp:ListItem Value="2">Mobile</asp:ListItem>
                                                            <asp:ListItem Value="2">Letter</asp:ListItem>
                                                        </asp:DropDownList>
                                 </div>
                                  <label class="col-sm-2">File Upload</label>
                                  <div class="col-sm-3">
                                        <span class="colon">:</span>
                                 <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server"></asp:FileUpload>
                                 </div>
                                   
                                 </div>
                                  </div><div class="form-group">
                                 <div class="row">
                                 <label class="col-sm-2">Email <span class="text-red">*</span></label>
                               <div class="col-sm-3">
                               
                                <span class="colon">:</span>
                                  <asp:TextBox ID="txtEmail" MaxLength="100"  CssClass="form-control" runat="server"
                                      ></asp:TextBox>
                                          </div> 
                                 </div>
                                 </div>
                                 </ContentTemplate>
                                                        
                                                         </asp:UpdatePanel>
                                   <div class="form-group">

                                 <div class="row">
                            
                             
                             
                                    <div class="col-sm-4">
                                                       
                                                     <asp:Button ID="btnSave" 
                                                             CssClass="btn btn btn-success" runat="server" Text="Save" onclick="btnSave_Click1"  OnClientClick="return validation();" 
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
