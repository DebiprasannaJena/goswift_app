<%@ Page Language="C#" AutoEventWireup="true" CodeFile="thankYou.aspx.cs" Inherits="thankYou" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .footer-top
        {
            display: none;
        }
    </style>
    <script type="text/javascript">

        window.onload = window.history.forward(0);  //calling function on window onload
    
    </script>
    <script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
</head>
<body>
    <form id="form2" runat="server">
          <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
      
        <div class="registration-div">
            <div class="">
                <div id="exTab1">
                    <div class="wizard">
                        <div class="wizard-inner">
                            <div class="connecting-line">
                            </div>
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation" class="backactive"><a href="#step2" data-toggle="tab" aria-controls="Profile Creation"
                                    role="tab" title="Profile Creation"><span class="round-tab"><i class="glyphicon glyphicon-pencil">
                                    </i></span><small>Profile Creation</small> </a></li>
                                <li role="presentation" class="backactive"><a href="#step3" data-toggle="tab" aria-controls="OTP Confirmation"
                                    role="tab" title="OTP Confirmation"><span class="round-tab"><i class="glyphicon glyphicon-picture">
                                    </i></span><small>OTP Confirmation</small> </a></li>
                                <li role="presentation" class="backactive"><a href="#complete" data-toggle="tab"
                                    aria-controls="Success" role="tab" title="Complete"><span class="round-tab"><i class="glyphicon glyphicon-ok">
                                    </i></span><small>Success</small> </a></li>
                            </ul>
                            <%--
                 <ol class="breadcrumb breadcrumb-arrow">
		
		<li><a href="InvestorRegistrationUser.aspx"> <span >   <i class="glyphicon glyphicon-pencil"></i>  </span> Profile Creation</a></li>
		<li ><a href="otpValidation.aspx" ><span > <i class="glyphicon glyphicon-picture"></i> </span>    OTP Confirmation</a></li>
        <li ><a href="thankYou.aspx" class="active"><span >  <i class="glyphicon glyphicon-ok"></i>   </span>Success</a></li>

	</ol>--%>
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="form-sec">
                                    <div class="form-body">
                                        <div class="success-div">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <img src="images/checked.png" alt="success img" />
                                                        <h2>
                                                            Thank you for registration</h2>
                                                        <p>
                                                            <label for="fname" style="display: block;">
                                                                You will receive a SMS in your registered mobile number ....The Dept will take action within 24 to 48 hrs(official days) to activate the registration after which you will  be able to login into the portal.</label></p>
                                                        <a href="Default.aspx" class="btn btn-primary">Back To Home</a>
                                                        <%-- <p> To activate your account please check the email sent to your email-id. </p>--%>
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
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
