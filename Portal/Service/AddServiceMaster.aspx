<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="AddServiceMaster.aspx.cs" Inherits="ServiceMaster_AddServiceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
 <script type="text/javascript" src="../../js/CSMValidation.js"></script>
    <script language="javascript" type="text/javascript">
   
        $(document).ready(function () {
            var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
            if (($('#ContentPlaceHolder1_ddlDepartment').val()) > 0) {
               
                $('#ContentPlaceHolder1_ddlDepartment').prop('disabled', true);
                $('#ContentPlaceHolder1_txtServiceName').attr("disabled", "disabled");
            }

            if (($('#ContentPlaceHolder1_ddlServiceType').val()) == 1) {
                $("#ServiceURL").show();
                $("#divExternal").hide() //for exterNal Service Type
            }

            else {
                $("#ServiceURL").hide();
                $("#divExternal").show() //for exterNal Service Type

            }
          
            var rb = document.getElementById("<%=rdbExternal.ClientID%>");
            var radio = rb.getElementsByTagName("input");
            for (var i = 0; i < radio.length; i++) {
                if (radio[0].checked) {
                    $("#ServiceURL").show();
                }
                else if (radio[1].checked) {
                    $("#ServiceURL").hide();
                }
            }
            

            if ($("#ContentPlaceHolder1_txtPayment").val() != 0.00) {
                $("#ContentPlaceHolder1_Payment").show();
            }
            else {
                $("#ContentPlaceHolder1_Payment").hide();

            }

            $("#<%=rdbPayment.ClientID%> input").change(function () {
                if (($(this).val()) == 0) {
                    $("#ContentPlaceHolder1_Payment").show();
                  
                }
                else {
                    $("#ContentPlaceHolder1_Payment").hide();
                   
                }
            });

            $("#<%=rdbExternal.ClientID%> input").change(function () {
                if (($(this).val()) == 1) {
                    $("#ServiceURL").show();
                }
                else {
                  
                    $("#ServiceURL").hide();
                }
            });

            $("#ContentPlaceHolder1_ddlServiceType").change(function () {
                if (($(this).val()) == 1) {
                    $("#ServiceURL").show();
                    $("#divExternal").hide() //for exterNal Service Type

                }
                else {
                    $("#ServiceURL").hide();
                    $("#divExternal").show() //for exterNal Service Type

                }
            });
            $('#ContentPlaceHolder1_btnSubmit').click(function () {
                

                if (DropDownValidation('ContentPlaceHolder1_ddlDepartment', '0', 'Department', projname) == false) {
                    $("#ContentPlaceHolder1_ddlDepartment").focus();
                    return false;
                }
                if (blankFieldValidation('ContentPlaceHolder1_txtServiceName', 'Service Name', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('ContentPlaceHolder1_txtServiceName', 'Service Name', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('ContentPlaceHolder1_txtaliasname', 'Alias Name', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('ContentPlaceHolder1_txtaliasname', 'Alias Name', projname) == false) {
                    return false;
                }
                if (DropDownValidation('ContentPlaceHolder1_ddlServiceCategory', '0', 'Category', projname) == false) {
                    $("#ContentPlaceHolder1_ddlServiceCategory").focus();
                    return false;
                }
                if (blankFieldValidation('ContentPlaceHolder1_txtORTPSTimeline', 'ORTPSA Timeline in Days', projname) == false) {
                    return false;
                }
                if (WhiteSpaceValidation1st('ContentPlaceHolder1_txtORTPSTimeline', 'ORTPS Timeline in Days', projname) == false) {
                    return false;
                }

                var noofDays = parseInt($('#ContentPlaceHolder1_txtORTPSTimeline').val());
                noofDays = parseInt(noofDays);
                //alert(noofDays);
                if (isNaN(noofDays)) {
                    noofDays = 0;
                }
                if (noofDays <= 0) {
                    alert("ORTPSA Timeline in Days is blank");
                    return false;                   
                }
                //alert(noofDays);

                
                var rb = document.getElementById("<%=rdbPayment.ClientID%>");
                var radio = rb.getElementsByTagName("input");
                for (var i = 0; i < radio.length; i++) {
                    if (radio[0].checked) {
                        //alert("ok-1");
                        if (blankFieldValidation('ContentPlaceHolder1_txtPayment', 'Payment', projname) == false) {
                            return false;
                        }
                        var amount = parseInt($('#ContentPlaceHolder1_txtPayment').val());
                        if (amount <= 0) {
                            alert("Payment can not be blank !");
                            return false;
                        }
                       
                       
                    }

                }
            });
        });

        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
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
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Service</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Manage Service</a></li><li><a>Add Service</a></li></ul>
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
                              <a class="btn btn-add " href="AddServiceMaster.aspx"> 
                              <i class="fa fa-plus"></i>Add</a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewServiceMaster.aspx"> 
                              <i class="fa fa-file"></i>View</a>  
                           </div>
                             <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ServiceMaster.aspx"> 
                              <i class="fa fa-plus"></i> Edit Service Name </a>  
                           </div>
                        </div>

                        <div class="panel-body">
                           
                            <div class="form-group">
                                <div class="row">                               
                                   <div class="col-sm-6">
                                        <label class="col-md-3 col-sm-3">Department :<span class="text-red">*</span></label>
                                        <asp:HiddenField ID="hdnDeptId" runat="server" />
                                         <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" 
                                            Width="350px">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Automobile" Value="1" />
                                                <asp:ListItem Text="Environment" Value="2" />
                                                <asp:ListItem Text="Forest" Value="3" />
                                         </asp:DropDownList>
                                            
                                       </div>
                                   
                                    <div class="col-sm-6">
                                        <label class="col-md-3 col-sm-3">Service Name :<span class="text-red">*</span></label>
                                        <asp:HiddenField ID="hdnServiceName" runat="server" />
                                        <asp:TextBox ID="txtServiceName" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')"  runat="server" 
                                            Width="350px"></asp:TextBox>
                                        
                                    </div>
                                </div>
                              </div>
                          
                            <div class="form-group">
                                  <div class="row">
                                    
                                    <div class="col-sm-6">
                                        <label class="col-md-3 col-sm-3">Alias Name :<span class="text-red">*</span></label>
                                        <asp:TextBox ID="txtaliasname" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')"  runat="server" 
                                            TextMode="SingleLine" Width="350px"></asp:TextBox>
                                         
                                    </div>
                                    <div class="col-sm-6">
                                     <label class="col-md-3 col-sm-3">Service Type :<span class="text-red">*</span></label>
                                     <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="form-control" 
                                           Width="350px">
                                                <asp:ListItem Text="External Service" Value="1" />
                                                <asp:ListItem Text="Internal Service" Value="0" />                                          
                                       </asp:DropDownList>
                                            
                                   </div>
                                 </div>
                             </div>
                              
                            <div class="form-group">
                                  <div class="row">

                                    <div class="col-sm-6">
                                            <label class="col-md-3 col-sm-3">Category :<span class="text-red">*</span></label>
                                            <asp:DropDownList ID="ddlServiceCategory" runat="server" CssClass="form-control" 
                                             Width="350px">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Pre-Establishment" Value="1" />
                                                <asp:ListItem Text="Pre-Operation" Value="2" />
                                            </asp:DropDownList>   
                                    </div>
                                    <div class="col-sm-6" id ="divExternal">
                                          <label class="col-md-3 col-sm-3">If External Type :<span class="text-red">*</span></label>
                                          <asp:RadioButtonList ID="rdbExternal" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                            <asp:ListItem class="radio-inline" Text="Yes" Value="1" />
                                            <asp:ListItem class="radio-inline" Text="No" Value="0" Selected="True" />
                                          </asp:RadioButtonList>
                                    </div>

                                 </div>


                            </div>
                            <div class="form-group">
                                  <div class="row">
                                      <div class="col-sm-6">
                                            <label class="col-md-3 col-sm-3">ORTPSA Timeline in Days:<span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtORTPSTimeline" CssClass="form-control" MaxLength="3" Onkeypress="return inputLimiter(event,'Numbers')"  runat="server" 
                                                TextMode="SingleLine" Width="350px"></asp:TextBox>
                                                
                                     </div>
                                      <div class="col-sm-6">
                                            <label class="col-md-3 col-sm-3">Service Description :</label>
                                            <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')"
                                                TextMode="MultiLine" Width="350px"></asp:TextBox>
                                       </div>

                                  </div>
                            </div>
                            <div class="form-group">
                                  <div class="row">
                                      <div class="col-sm-6" id="ServiceURL">
                                         <label class="col-md-3 col-sm-3">Service URL :</label>
                                         <asp:TextBox ID="txtServiceURL" CssClass="form-control" Onkeypress="return inputLimiter(event,'Address')"  runat="server" TextMode="SingleLine" Width="350px"/>                                      
                                      </div>
                                      
                                      <div class="col-sm-6">
                                          <label class="col-md-3 col-sm-3">Payment Required :<span class="text-red">*</span></label>
                                          <asp:RadioButtonList ID="rdbPayment" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                            <asp:ListItem class="radio-inline" Text="Yes" Value="0" />
                                            <asp:ListItem class="radio-inline" Text="No" Value="1" Selected="True" />
                                          </asp:RadioButtonList>
                                       </div>
                                       
                                  </div>
                            </div>
                            
                            <div class="form-group">
                                <div class="row">  
                                    <div class="col-sm-6" runat="server" id="Payment">
                                            <label class="col-md-3 col-sm-3">Payment :<span class="text-red">*</span></label>
                                             <asp:TextBox ID="txtPayment" CssClass="form-control" runat="server" 
                                           Width="350px" Onkeypress="return inputLimiter(event,'Decimal')" MaxLength="8"></asp:TextBox>   
                                    </div>
                                </div>
                            </div>

                        </div>

                             
                        <div class="reset-button text-center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass=" btn btn-success"
                                Width="80" OnClick="btnSubmit_Click"  />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning" 
                                onclick="btnReset_Click"></asp:Button>
                        </div>
                        <br />   
                       
                     </div>
                  </div>
               </div>
            </section>
        <!-- /.content -->
    </div>
</asp:Content>
