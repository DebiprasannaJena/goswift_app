<%--'*******************************************************************************************************************
' File Name         : otpValidation.aspx
' Description       : Validation of OTP
' Created by        : AMit Sahoo
' Created On        : 16 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="otpValidation.aspx.cs" Inherits="otpValidation" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            //jQuery example
            $("#Btn_Resend_OTP").hide();
            $('#Btn_Confirm').click(function () {
                if ($("#txtToken").val() != "") {
                    return true;
                }
                else {
                    jAlert('<strong>Please enter OTP !</strong>', 'SWP');
                    return false;
                }
            })
        });
    </script>
    <script src="js/jQuery.alert.js" type="text/javascript"></script>
    <link href="css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/WebValidation.js" type="text/javascript"></script>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
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
    <form id="form1" runat="server">
    <asp:ScriptManager ID="otp" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="registration-div">
            <div class="">
                <div id="exTab1" class="">
                    <div class="wizard">
                        <div class="wizard-inner">
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation" class="backactive"><a href="#step2" data-toggle="tab" aria-controls="Profile Creation"
                                    role="tab" title="Profile Creation"><span class="round-tab"><i class="glyphicon glyphicon-pencil">
                                    </i></span><small>Profile Creation</small> </a></li>
                                <li role="presentation" class="active"><a href="#step3" data-toggle="tab" aria-controls="OTP Confirmation"
                                    role="tab" title="OTP Confirmation"><span class="round-tab"><i class="glyphicon glyphicon-picture">
                                    </i></span><small>OTP Confirmation</small> </a></li>
                                <li role="presentation" class="disabled"><a href="#complete" data-toggle="tab" aria-controls="Success"
                                    role="tab" title="Complete"><span class="round-tab"><i class="glyphicon glyphicon-ok">
                                    </i></span><small>Success</small> </a></li>
                            </ul>
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="form-sec">
                                    <div class="form-header">
                                        <span class="mandatoryspan pull-right">( * ) Mark Fields Are Mandatory</span>
                                        <h2>
                                            OTP Validation</h2>
                                    </div>
                                    <div class="form-body minheight350 padding-top50">
                                        <div class="formbodycontent">
                                            <div id="divOtp">
                                                <div class="form-group ">
                                                    <div class="row">
                                                        <div class="col-sm-5">
                                                            Please enter the OTP sent to your registered mobile number/Email
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="txtToken" MaxLength="6" CssClass="form-control" runat="server" autocomplete="off"></asp:TextBox>
                                                            <span class="mandetory">*</span>
                                                            <cc1:FilteredTextBoxExtender ID="txtToken_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtToken" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                InvalidChars="&quot;'<>&;">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:Button ID="Btn_Confirm" runat="server" Text="Confirm" CssClass=" btn btn-success"
                                                                OnClick="Btn_Confirm_Click" />
                                                            <asp:Label ID="Lbl_Msg" ForeColor="Red" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-5">
                                                        </div>
                                                        <div class="col-sm-7">
                                                            Validity of OTP : <span class="countdown" id="timer" style="color: Red"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-footer">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12" align="center">
                                                    <asp:Button ID="Btn_Resend_OTP" runat="server" Text="Resend OTP" CssClass=" btn btn-danger countdown"
                                                        OnClick="Btn_Resend_OTP_Click" />
                                                    <asp:HiddenField ID="Hid_Time_Left" runat="server" />
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
    <%-- <script src="js/simplyCountdown.js" type="text/javascript"></script>--%>
    <script type="text/javascript" language="javascript">

        var startTime = document.getElementById('Hid_Time_Left').value;
        //        alert(startTime);
        var timer2 = startTime;  //"10:01";
        var interval = setInterval(function () {
            //debugger;
            var timer = timer2.split(':');
            //by parsing integer, avoid all extra string processing
            var minutes = parseInt(timer[0], 10);
            var seconds = parseInt(timer[1], 10);
            --seconds;
            minutes = (seconds < 0) ? --minutes : minutes;
            seconds = (seconds < 0) ? 59 : seconds;
            seconds = (seconds < 10) ? '0' + seconds : seconds;
            //minutes = (minutes < 10) ?  minutes : minutes;
            $('.countdown').html(minutes + ':' + seconds);
            if (minutes < 0) clearInterval(interval);
            //check if both minutes and seconds are 0
            if ((seconds <= 0) && (minutes <= 0)) clearInterval(interval);
            timer2 = minutes + ':' + seconds;
            if ((seconds <= 0) && (minutes <= 5)) {
                $("#Btn_Resend_OTP").show();
            }
        }, 1000);    
    </script>
</body>
</html>
