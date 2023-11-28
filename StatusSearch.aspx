<%--'*******************************************************************************************************************
' File Name         : StatusSearch.aspx
' Description       : Status Search
' Created by        : Radhika Rani Patri
' Created On        : 03 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatusSearch.aspx.cs" Inherits="StatusSearch" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        
    </style>
      

    <script language="javascript">
     function OTPValidation() {

         debugger;
         if ($('#txtToken').val() == "") {
             alert('Plaese Enter the Token No')
             $('#txtToken').focus();
         }
         else {

             $('#divOtp').show();
             
             return false;
             
         }

     

        }
</script>
</head>
<body>
    <form id="form2" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="pagenavigator">
        <h2>
            <a class="" href="javascript:history.back()"><i class="fa fa-chevron-circle-left"></i>
            </a>Search Status</h2>
    </div>
    <div class="registration-div">
        <div class="container">
            <div class="form-sec">
             <div class="form-header">
              <span class="mandatoryspan pull-right">( * ) Indicate Mandatory Fields</span>
                <h2>
                    Search Status</h2>
               </div>
                <div class="form-body"> 
                <div class="form-group search-sec">
                    <div class="row">
                        <div class="col-sm-2">
                            <label for="lblToken">
                                Enter Proposal No.</label>
                        
                        </div>
                        <div class="col-sm-4">
                           
                            <asp:TextBox ID="txtToken" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <label for="Address2">
                                </label>                            
                            <asp:Button ID="btnBack" runat="server" Text="Search" CssClass=" btn btn-success" OnClientClick=" return OTPValidation();"
                                Width="80"  />
                        </div>
                    </div>
                </div>
                   <div  id="divOtp" style="display:none">
                          
                           <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                      Please enter the OTP sent to your registered mobile no<span class="text-red">*</span>
                                       
                                    </div>
                                  
                                </div>
                            </div>

                             <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                          <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>                                  
                                    </div>
                                    <div class="col-sm-4">
                                       <asp:Button ID="Button3" runat="server" Text="Confirm" 
                                            CssClass=" btn btn-success"  
                                            />
                                    </div>
                                  
                                </div>
                            </div>
                             <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:HyperLink ID="hypId" CssClass="text-red" Text="Resend OTP" runat="server" NavigateUrl="#"></asp:HyperLink>                            
                                    </div>
                                   
                                </div>
                            </div>
                         </div>
                </div>
                  <div class="form-footer" style="display:none"> 
              <div class="form-group" id="divSubmit" style="display:none" >
                                <div class="row">
                                    <div class="col-sm-4">                                       
                                    </div>
                                    <div class="col-sm-4" align="center">
                                
                                        <asp:Button ID="Button1" runat="server" Text="Submit" CssClass=" btn btn-success" 
                                            />
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>

                            </div>
             
              
        </div>
    </div>
    </div> 
    <uc3:footer ID="footer" runat="server" />
    </form>
      <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/loadComponent.js" type="text/javascript"></script>
    <script src="js/CSMValidation.js" type="text/javascript"></script>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <script src="js/jQuery.js" type="text/javascript"></script>
</body>
</html>
