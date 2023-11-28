<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceProcess.aspx.cs"
    Inherits="ServiceProcess"  enableviewstate="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
     <%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>    --%>

    <link href="css/custom.css" rel="stylesheet" type="text/css" />
  <%--  <link href="css/global.css" rel="stylesheet" type="text/css" />--%>
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.menuservices').addClass('active');

             $('#HyprLnk').hide();
            $(document).on('click', '.custom-accordion .accordion-heading', function (e) {
                debugger
                var target_url = $(this).attr('data-url');
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');
                    $('.accordion-body').remove();
                    $('#hdnmenuactive').val('');
                }
                else {
                    $('#hdnmenuactive').val($(this).attr("id"));

                    $('.custom-accordion .accordion-heading').removeClass('active');
                    $('.accordion-body').remove();
                    $(this).addClass('active');
                    $(this).after('<div class="accordion-body"><iframe src="' + target_url + '" id="accordion-iframe" width="100%" ></iframe></div>');

                    LockScreen('<span class="loader1"><img src="images/loader-new.gif" alt="GO Swift"></span>');
                    start(1);
                    if ($(this).attr("id") != 'six');
                    {
                        debugger;
                        getusermanual();
                    }
                    var $iframe = $('iframe');
                    $iframe.autoHeight();
                }
            });
        });

        $(function () {
            "use strict";

            $.fn.copyHeight = function (timer) {
               
                var $this = $(this);
                timer = timer || 10000;

                return $this.each(function (index, iframe) {

                    var $iframe = $(iframe);

                    var action = function () {
                        var $mirror = $('html', $iframe.contents());
                        var mirrorHeight = $mirror.css('height', 'auto').outerHeight();

                        $iframe.css('height', mirrorHeight);
                     var lock = document.getElementById('divlock');
               
                    lock.className = 'LockOff';
                    };

                    var timeout = setTimeout(function () {
                        action($iframe);
                    }, timer);

                    $iframe.load(function () {
                        clearTimeout(timeout);
                        action($this);
                    });

                });

            };

            $.fn.autoHeight = function (interval) {
               
                var $this = $(this);
                interval = interval || 20000;

                return $this.each(function (index, iframe) {

                    var $iframe = $(iframe);
                    var $mirror = $('html', $iframe.contents());

                    $iframe.copyHeight(0);

                    var cachedHeight = $mirror.outerHeight();

                    setInterval(function () {
                        var currentHeight = $mirror.outerHeight();
                        if (cachedHeight !== currentHeight) {
                            $iframe.copyHeight(0);
                            cachedHeight = currentHeight;
                        }
                    }, interval);

                });

            };

        });
        function SetTarget() {
            document.forms[0].target = "";
        }
        function RemoveTarget() {
            document.forms[0].target = "";
        }
        function getnavmenu() { // get menu list from list of services with completed status
            debugger
            var obj = {};
            obj.intServiceId =  $('#hdnmenuactive').val();
            $.ajax({
                type: "POST",
                url: "ServiceProcess.aspx/getnavigations",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    debugger
                    if (r.d != "") {
                        var va = r.d.split('|');
                        if (va == '404') {
                            window.location.href = 'login.aspx';

                        } else {
                             $('#div' + va[0]).html('<span class="spanicon"><i class="fa fa-check" aria-hidden="true"></i></span>');
                                $('#div' + va[0]).removeClass('pending');
                            if (va.length > 1) {
                            for (var i = 0; i <va.length-1 ; i++) {
                                $('#div' + va[i]).html('<span class="spanicon"><i class="fa fa-check" aria-hidden="true"></i></span>');
                                $('#div' + va[i]).removeClass('pending');
                            }
                            
                                 var target_url = $('#six').attr('data-url');
                                if ( typeof target_url == "undefined" ) {
                                    
                                    var cn = va.length;
                                    var data = va[cn-1];
                                    $('#ulmenuid').append(data);
                                }
                            }
                        }
                    }
                }
            });

        }
        function start(id) {
            debugger
            $('#hdnbackwork').val(id);
            if ($('#hdnbackwork').val(id) != "" && $('#hdnstopwork').val() == "") {
                setTimeout(function (e) {
                    getnavmenu();
                }, 5000);
            }
        }
        function getnavmenudata() {  // get services status and accordingly add loader if external application
              debugger
            var obj = {};
            obj.intServiceId =  $('#hdnmenuactive').val();
            $.ajax({
                type: "POST",
                url: "ServiceProcess.aspx/getnavdataurl",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    debugger
                    if (r.d == '404') {
                       // window.location.href = 'login.aspx';

                    } else {
                        var va = r.d.split('|');
                        var id = $('#hdnmenuactive').val()
                        $('#' + id).attr('data-url', va[0]);
                        if (va.length > 1) {
                            $('#div' + id).html('<span class="loader"><img src="images/hourglass.png" style="height: 15px;" alt="GO Swift"></span>');
                        }
                    }
                 
                }
            });

        }
          function checkurl(id) { // loader and get menu on click of proceed and menu li link open close
            LockScreen('<span class="loader1"><img src="images/loader-new.gif" alt="GO Swift"></span>');
            $('#hdnbackwork').val(id);
            if ($('#hdnbackwork').val(id) != "" && $('#hdnstopwork').val() == "") {
                setTimeout(function (e) {
                    getnavmenudata();
                }, 20000);
                 setTimeout(function (e) {
                    var lock = document.getElementById('divlock');               
                    lock.className = 'LockOff';
                }, 500);
                 
            }
        }
         function LockScreen(str) {
            var r = true;
            if (r) {
                var lock = document.getElementById('divlock');
                if (lock)
                    lock.className = 'LockOn';

                lock.innerHTML = str;
                return true;
            }
            else {
                return false;
            }

        }
 
  function getusermanual() { // get user manual link of selected form
            debugger
            var obj = {};
            obj.strFormId =  $('#hdnmenuactive').val();
            $.ajax({
                type: "POST",
                url: "ServiceProcess.aspx/FillContent",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    debugger
                    if (r.d != "") {
                        var va = r.d;
                        if (va == '404') {
                            $('#HyprLnk').href('#');
                             $('#HyprLnk').hide();
                        } else {
                            $('#HyprLnk').attr("href", va.replace("~/", ""));
                            $('#HyprLnk').show();
                        }
                    }
                }
            });

        }

    
    </script>
    <style type="text/css">
        .LockOff
        {
            display: none;
            visibility: hidden;
        }
        
        
        
        .LockOn
        {
            display: block;
            visibility: visible;
            position: absolute;
            z-index: 999;
            top: 0px;
            left: 0px;
            width: 100%;
            right: 0px;
            bottom: 0px;
            height: 100%;
            color: Green;
            background-color: #fff;
            text-align: center;
            padding-top: 30%;
            font-size: x-large;
            filter: alpha(opacity=75);
            opacity: 0.75;
        }
        .form-body{border:none;}
        .loader1 img{width: 48px;}
        .guidelines {
            display: table;
            width: 100%;
            min-height: 200px;
            text-align: center;
            background: #eee;
            margin-bottom: 10px;
        }

            .guidelines p {
                display: table-cell;
                vertical-align: middle;
                font-size: 18px;
                letter-spacing: 1px;
            }

        .guidelinesdetails {
        }

            .guidelinesdetails h4 {
                margin-top: 0px;
                font-weight: 600;
            }

        .instructiondiv {
            padding: 20px 40px;
        }

            .instructiondiv h2 {
                color: #b1020a;
            }

        .minheight300 {
            min-height: 300px;
        }

        .links {
            margin-top: 0px;
            right: 14px;
            background-color: #cd1c24;
            border-color: #cd1c24;
            color: #fff;
            font-size: 12px;
            padding: 4px 10px;
        }

            .links:hover, .links:focus {
                background: #bd1018;
                color: #fff;
            }
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #fbfbfb;
            border: 3px solid #AC183E;
            margin: 0px;
        }

            .modalPopup .mhead {
                padding: 5px 5px;
                border-bottom: 1px solid #ccc;
                background: #AC183E;
                color: #fff;
            }

                .modalPopup .mhead h4 {
                    display: inline-block;
                }

                .modalPopup .mhead a {
                    float: right;
                    color: #fff;
                    text-decoration: none;
                }

            .modalPopup .mbody {
                padding: 30px 15px;
            }

            .modalPopup .mFooter {
                padding: 15px;
                text-align: right;
                border-top: 1px solid #e5e5e5;
            }


        .radiodiv {
            padding: 10px 0px 20px;
        }

        .Confdiv {
            padding: 25px 120px 20px;
        }

        #PanelIdco h4 {
            font-size: 17px;
            font-weight: bold;
            padding-bottom: 12px;
        }

        .radio-inline label {
            display: inline-block;
            padding-right: 20px;
            padding-left: 12px;
        }

        .reglogin {
            padding: 25px;
        }

            .reglogin p {
                text-align: justify;
            }

            .reglogin a {
                color: #0088cc;
                text-decoration: none;
            }

                .reglogin a:hover {
                    color: #159f45;
                }

        .popBox {
            position: absolute;
            -webkit-box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            -moz-box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            background: #fffdef;
            padding: 8px;
            border: 1px solid #ddd;
            width: 93%;
            left: 15px;
            font-size: 14px !important;
        }

        #pop-up {
            top: 120px;
        }

        #pop-up1 {
            top: 120px;
        }

        #pop-up2 {
            top: 100px;
        }

        .row {
            margin-left: -15px;
            margin-right: 0;
        }

        .navbar-inverse {
            background-color: none !important;
            border-color: none !important;
        }

        .portlet-sec {
            margin: 16px 15px 8px;
            padding: 5px;
            border-radius: 2px;
        }

            .portlet-sec h3 {
                text-transform: uppercase;
                font-size: 20px;
            }

                .portlet-sec h3 span {
                    font-weight: 600;
                    color: #ac2d00;
                    padding: 0px 4px;
                }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <div class="registration-div investors-bg">
                <div class="">
                    <div id="exTab1">
                        <div class="investrs-tab">
                            <uc4:investoemenu ID="ineste" runat="server" />
                        </div>


                        <div class="form-sec">
                            <div class="form-header">
                                <asp:HyperLink class="btn pull-right links" ID="HyprLnk" Target="_blank" data-toggle="tooltip" 
                                    runat="server" title="Download User Manual"><i class="fa fa-download"></i>&nbsp;User Manual</asp:HyperLink>

                                <h2>
                                    <%-- <asp:Label ID="lblService" runat="server" Text=""></asp:Label>--%>
                                    <%--(Code-Industry Name)--%></h2>
                            </div>
                            <div class="wizard">
                                <div class="wizard-inner margin-top15">
                                    <div class="connecting-line">
                                    </div>
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li role="presentation" class="active"><a href="#step2" data-toggle="tab" aria-controls="Profile Creation"
                                            role="tab" title="Form Registration"><span class="round-tab"><i class="fa fa-file-text-o"></i></span><small>Form Registration</small> </a></li>
                                        <li role="presentation" class="disabled"><a href="#step3" data-toggle="tab" aria-controls="Payment Details"
                                            role="tab" title="Payment Details"><span class="round-tab"><i class="fa fa-credit-card"></i></span><small>Payment Details</small> </a></li>
                                        <li role="presentation" class="disabled"><a href="#complete" data-toggle="tab" aria-controls="Success"
                                            role="tab" title="Complete"><span class="round-tab"><i class="glyphicon glyphicon-ok"></i></span><small>Success</small> </a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="form-body serviceprocess">
                                  <div id="divlock" class="LockOff">
                            </div>
                                <div class="legend-section pull-right" style="display: flex; margin-bottom: 10px;">
                                    <div class="green" style="display: flex;">
                                        <span class='spanicon'><i class='fa fa-check' aria-hidden='true' style="margin-left: -20px;"></i></span><span></span>Applied</span>
                                    </div>
                                    <div class="red" style="display: flex; margin-left: 30px;">
                                        <span class='spanicon pending'><i class='fa fa-times' aria-hidden='true' style="margin-left: -20px;"></i> </span><span>Not-Applied</span>
                                    </div>
                                </div>

                                <div class="" runat="server" id="myNavbar">
                                </div>
                                <asp:HiddenField ID="hdnmenuactive" runat="server" />
                                <asp:HiddenField ID="hdnbackwork" runat="server" />
                                <asp:HiddenField ID="hdnstopwork" runat="server" />

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
