<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinalSubmitWithThankYou.aspx.cs" Inherits="FinalSubmitWithThankYou" %>

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
</head>
<body>
    <form id="form2" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="pagenavigator">
        <h2>
            <a class="" href="javascript:history.back()"><i class="fa fa-chevron-circle-left"></i>
            </a>Registration</h2>
    </div>
    <div class="registration-div">
        <div class="container">
            <div id="exTab1" class="container">
              <div class="wizard">
            <div class="wizard-inner">
                <div class="connecting-line"></div>
                <ul class="nav nav-tabs" role="tablist">

                    <li role="presentation" class="disabled backactive" >
                        <a href="#step1" data-toggle="tab" aria-controls="step1" role="tab" title="Step 1">
                            <span class="round-tab">
                                <i class="glyphicon glyphicon-folder-open"></i>
                            </span>
                            <small>Investors Details</small>
                        </a>
                    </li>

                    <li role="presentation" class="disabled backactive">
                        <a href="#step2" data-toggle="tab" aria-controls="step2" role="tab" title="Step 2">
                            <span class="round-tab">
                                <i class="glyphicon glyphicon-pencil"></i>
                            </span>
                             <small>Profile Creation</small>
                        </a>
                    </li>
                    <li role="presentation" class="disabled backactive">
                        <a href="#step3" data-toggle="tab" aria-controls="step3" role="tab" title="Step 3">
                            <span class="round-tab">
                                <i class="glyphicon glyphicon-picture"></i>
                            </span>
                             <small>OTP Confirmation</small>
                        </a>
                    </li>

                    <li role="presentation"  class="active">
                        <a href="#complete" data-toggle="tab" aria-controls="complete" role="tab" title="Complete">
                            <span class="round-tab">
                                <i class="glyphicon glyphicon-ok"></i>
                            </span>
                            <small>Success</small>
                        </a>
                    </li>
                </ul>
            </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a" >
                  <div class="form-sec">
                       <div class="form-body">
                           
                           <div class="success-div">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12" >

                                    <img src="images/checked.png">
                                     <h2>
                                Thank you for registration</h2>
                                        <p><label for="fname">
                                           A confirmation mail has been sent to your email id.</label></p>
                                   <p> To activate your account please check the email sent to your email-id. </p>
                                    </div>
                                   
                                </div>
                            </div>
</div>
                            
                           </div>
                          </div>
                          
                         
                          
                  
                    </div>
                </div>
                </div>
                </div>
                </div>
                </div>
                <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
