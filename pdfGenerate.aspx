<%--'*******************************************************************************************************************
' File Name         : ApplicationDetails.aspx
' Description       : Show the  details of Particular Applied Application
' Created by        : Prasun Kali
' Created On        : 19th September 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pdfGenerate.aspx.cs" Inherits="pdfGenerate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
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
        function valid() {

            if (document.getElementById('ContentPlaceHolder1_hdnNoofrecord').value == "0") {
                if (document.getElementById('ContentPlaceHolder1_txtq1').value == "") {
                    alert('Initial Query can not be left blank!');
                    document.getElementById('ContentPlaceHolder1_txtq1').focus();
                    return false;
                }
            }
            if (document.getElementById('ContentPlaceHolder1_hdnNoofrecord').value == "2") {
                if (document.getElementById('ContentPlaceHolder1_txtq2').value == "") {
                    alert('Second Set Of Query can not be left blank!');
                    document.getElementById('ContentPlaceHolder1_txtq2').focus();
                    return false;
                }
            }
        }

        function setvalue() {

            $('#charsLeft').html(1000 - $('#ContentPlaceHolder1_txtq1').val().length);
        }
        function setvalue1() {

            $('#charsLeft1').html(1000 - $('#ContentPlaceHolder1_txtq2').val().length);
        }
        $(document).ready(function () {
            $('.menuservices').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <header class="form-hdr">
 <div class="show-table"> 
  <div class="top-header usertop-header">
 
    <div class="navbar-header">
       <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse"> <span class="sr-only">Toggle navigation</span> <i class="fa fa-bars"></i></button>
      <a class="navbar-brand" href="Default.aspx" title="Invest Odisha"><img src="images/Logo2.png" alt="Government of Odisha"><img src="images/Logo.png" alt="Invest Odisha"></a> </div>
     <div class="tophdr-rightdiv"> 

   </div>


    


 
    <div class="clearfix"></div>

  </div>

  <!--// Menu //--> 

</div></header>
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    
                    
                  
                       
                        
                                         <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidrag">
                    

 <div class="dy-formview">
 <div class="dyformheader">
 <div class="header-details" runat="server" id="divHeader">
<%-- <h2>Application Header</h2>
 <p>Government of Odisha</p>--%>
 </div>
 </div>
  <div  class="dyformbody">
      <div class="row">
        <div class="col-sm-6 ">
        <label for="sss" class="col-sm-6">Application Number</label>
        <label for="sss" class="col-sm-6" id="lblapplication" runat="server">
        <span class="colon"></span></label>
        </div>
        <div class="col-sm-6 ">
        <label for="sss" class="col-sm-6">Applied Date</label>
        <label for="sss" class="col-sm-6" id="lblapplieddate" runat="server">
        <span class="colon"></span></label>
        </div>       
       </div>
   <div ID="frmContent" runat="server">
   
   </div>

    </div>
    </div>

                     
 </div>  

 
  
    <div class="col-sm-12" align="center" >
  <%--    <asp:Button ID="btnBack" runat="server" Text="Back" 
                                                OnClick="btnBack_Click" class="btn btn-primary btn-sm" />--%>
    <%--<asp:HyperLink ID="HyperLink1" NavigateUrl="~/Service/ServiceViewAndTakeAction.aspx" CssClass="btn btn-success" runat="server">Back</asp:HyperLink>--%>
<%--     <asp:LinkButton ID="LinkButton1" Text="Raise Query" runat="server" class="btn btn-danger btn-sm"
                data-toggle="modal" data-target="#customer1"></asp:LinkButton>--%>
    </div>
     </div>
                  </div>
                          
                        
                  
                </div>
            </div>
        </div>
    </div>
    </div>
    
    <!-- /.content -->
  
    </form>
</body>
</html>
