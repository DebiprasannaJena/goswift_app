<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormEditView1.aspx.cs" Inherits="FormEditView" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .LockOff
        {
            display: none;
            visibility: hidden;
        }
        
        .wizard
        {
            position: relative;
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
    </style>
    <script>
        $(document).ready(function () {
            $('.menuservices').addClass('active');



        });
    </script>
    <style>
        .header-details img
        {
            height: 70px;
            margin-bottom: 5px;
        }
        
        .form-error
        {
            border-radius: 6px;
            color: red;
            font-size: 0.8em;
            height: 100%;
            position: relative;
            padding: 0px 14px 0 2px;
            opacity: 1;
            top: 0px;
            text-align: left;
            left: 0;
            width: 100%;
            z-index: 1;
        }
        
        .form-error2
        {
            border-radius: 6px;
            color: #ff0000;
            font-size: 0.8em;
            height: 100%;
            position: relative;
            padding: 0px 14px 0 2px;
            opacity: 1;
            top: -10px;
            text-align: left;
            left: 0;
            width: 100%;
            z-index: 1;
        }
        
        .margin-bottom15
        {
            margin-bottom: 15px;
        }
        
        .text-red
        {
            margin-left: 0px;
        }
        
        input[type=checkbox]:last-child, input[type=radio]:last-child
        {
            margin: 4px 4px 0 8px;
        }
        
        .loader
        {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('images/uploadloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
        
        
        /*.srvListArea .loga-area, .srvListArea .searchpanel, .newList, .note-text {box-shadow:3px 3px 3px 0px #e7e4dc;-webkit-box-shadow:3px 3px 3px 0px #e7e4dc;}*/
    </style>
    <script src="js/formValidation.js" type="text/javascript"></script>
    <%------------------------------Plugin----------------------%>
    <link href="css/plugin/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="css/plugin/qunit-1.18.0.css" rel="stylesheet" type="text/css" />
    <script src="js/plugin/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="js/plugin/jquery-ui.min.js" type="text/javascript"></script>
    <script src="js/plugin/qunit-1.18.0.js" type="text/javascript"></script>
    <script src="js/jquery.appendGrid-development.js" type="text/javascript"></script>
    <script src="js/NestedGridTest.js" type="text/javascript"></script>
    <script src="js/Encryption/aes.js" type="text/javascript"></script>
    <%-- <script src='<% ="js/EditServicejs/JsFile_" + Request.QueryString["FormId"].ToString() + ".js" %>' type="text/javascript"></script>--%>
    <script src='<% ="js/EditServicejs/JsFile_" + Request.QueryString["FormId"].ToString() + ".js?V=" +  Guid.NewGuid() %>'
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
   <%-- <uc2:header ID="header" runat="server" />--%>
   <%-- <div class="container wrapper">--%>
        <div class="registration-div">
            <%-- <div class="investrs-tab">
               <uc4:investoemenu ID="ineste" runat="server" />
            </div>--%>
            <div class="">
               <%-- <div class="investrs-tab services--tabs" runat="server" id="myNavbar">
                </div>--%>
                <div class="wizard">
                   <%-- <div class="wizard-inner margin-top15">
                        <div class="connecting-line">
                        </div>
                        <ul class="nav nav-tabs" role="tablist">
                            <li role="presentation" class="active"><a href="#step2" data-toggle="tab" aria-controls="Profile Creation"
                                role="tab" title="Form Registration"><span class="round-tab"><i class="fa fa-file-text-o">
                                </i></span><small>Form Registration</small> </a></li>
                            <li role="presentation" class="disabled"><a href="#step3" data-toggle="tab" aria-controls="Payment Details"
                                role="tab" title="Payment Details"><span class="round-tab"><i class="fa fa-credit-card">
                                </i></span><small>Payment Details</small> </a></li>
                            <li role="presentation" class="disabled"><a href="#complete" data-toggle="tab" aria-controls="Success"
                                role="tab" title="Complete"><span class="round-tab"><i class="glyphicon glyphicon-ok">
                                </i></span><small>Success</small> </a></li>
                        </ul>
                    </div>--%>
                    <div class="form-sec dynamicform">
                        <div class="dyformheader">
                            <div class="logo-sec ">
                                <img id="imgLogo" runat="server" alt="Logo" />
                            </div>
                            <div class="header-details" id="divHeaderId" runat="server">
                            </div>
                            <div class="clearfix">
                            </div>
                        </div>
                        <div class="dyformbody">
                            <div id="divlock" class="LockOff">
                            </div>
                            <div id="frmContent" runat="server">
                            </div>
                            <div class="row">
                                <div class="col-sm-12" align="center">
                                    <div class="form-group">
                                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    </div>
                                    <% if (Request.QueryString["ReqMode"].ToString() == "S")
                                       { %>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success"
                                        OnClientClick="return SubmiClickFun();" OnClick="btnSubmit_Click" />
                                    <% } %>
                                    <asp:Button ID="btnDraft" runat="server" CssClass="btn btn-success" OnClientClick="return SubmiClickFunDraft();"
                                        OnClick="btnDraft_Click" />
                                </div>
                            </div>
                        </div>
                        <div id="divHeader" runat="server">
                        </div>
                        <div id="divLogo" runat="server">
                        </div>
                        <div class="loader" id="fileimage" style="display: none;">
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="clearfix">
                        </div>
                        <input type="hidden" id="hdnRadioNam" runat="server" />
                        <input type="hidden" id="hdnCheckBox" runat="server" />
                        <input type="hidden" id="hdnFileUpload" runat="server" />
                        <input type="hidden" id="hdnResultOutPut" runat="server" />
                        <input type="hidden" id="hdnPluginValue" runat="server" />
                        <input type="hidden" id="hdnPluginJson" runat="server" />
                        <input type="hidden" id="hdnTotalAmount" runat="server" />
                        <input type="hidden" id="hdnTotalAmount1" runat="server" />
                        <input type="hidden" id="hdnAccountHead" runat="server" />
                        <input type="hidden" id="hdnProposalNo" runat="server" />
                        <input type="hidden" id="hdnFormId" runat="server" />
                        <input type="hidden" id="hdnApplicationFee" runat="server" />
                        <input type="hidden" id="hdnfileidname" runat="server" />
                    </div>
                </div>
            </div>
        </div>
   <%-- </div>--%>
   <%-- <div class="container footer">
        <div class="dyformfooter">
            <div id="divFooterId" runat="server">
            </div>
        </div>
       
    </div>--%>
    <asp:HiddenField ID="hdnInvestor" runat="server" />
        </form>
    <script type="text/javascript">

        var strName = "";
        var strValue = "";
        var strFirstName = "";
        var strCheckBx = "";
        var strChechBoxCheck = "";


        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }

        function SubmiClickFun() {

            var outStatus = validateForm('lang');
            //  outStatus = true;
            if (outStatus == false) {
                return false;
            }
            else {
                strValue = "";
                var cnt = 0;
                $('#hdnCheckBox').val("");
                $('#hdnRadioNam').val("");
                var strJson = "{";

                //----------------------------------------------------------Iframe--------------------------//

                $('#hdnVal').val('');

                strChechBoxCheck = "";
                var j = "";
                var rows = [];
                var s1 = [];

                var strifmDt = "";
                var fulNm = "";
                var flnmStatus = "No";
                var cnt = 0;
                var drpdtmCnt = 0;
                var fulDtFmt = "";

                LockScreen('We are processing your request...');

                $('.drpDtm').each(function () {
                    drpdtmCnt = drpdtmCnt + 1;
                    var dtmId = this.id;
                    strName = $(this).attr('name');
                    var dtmVl = $('#' + dtmId).val();
                    fulDtFmt = fulDtFmt + dtmVl;
                    if (drpdtmCnt == "2") {
                        drpdtmCnt = 0;
                        strJson = strJson + '"' + strName + '":"' + fulDtFmt + '"' + ',';
                        strValue = "";
                        fulDtFmt = "";
                    }
                });

                $(".flnm").each(function () {

                    cnt = cnt + 1;
                    var tblidd = this.id;
                    strName = $(this).attr('name');
                    var vl = $('#' + tblidd).val();
                    fulNm = fulNm + vl + ' ';
                    if (cnt == "4") {
                        cnt = 0;
                        strJson = strJson + '"' + strName + '":"' + fulNm + '"' + ',';
                        strValue = "";
                        fulNm = "";
                        flnmStatus = "Yes";
                    }
                });

                $("#form1 Table").each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        var allrw = "[";
                        var tblidd = this.id;
                        var strDtSrsName = $('#' + tblidd).attr("name");
                        var strTitle = this.title;
                        var id = 0;
                        var titlearray = strTitle.split('-');
                        $("#" + tblidd + " >tbody > tr").each(function () {
                            var $row = $(this);
                            var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                            if (parentId != undefined && parentId != 'undefined') {
                                id = parseInt(id) + 1;
                                var p = "{";
                                for (var i = 0; i < titlearray.length; i++) {
                                    var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                                    strName = titlearray[i];
                                    var idd = tblidd + '_' + titlearray[i] + '_' + id;
                                    if ($('#' + idd).attr("type") == "file") {
                                        if (strJsonValue != "") {
                                            var FnlDateFrmt = getFormattedDate();
                                            var strValue1 = $('#' + idd).val();
                                            var fileNameExt = strValue1.substr(strValue1.lastIndexOf('.') + 1);

                                            var newFilName = idd + '_' + FnlDateFrmt + '.' + fileNameExt;
                                            fileu(idd, newFilName);
                                            strJsonValue = newFilName;
                                        }
                                    }
                                    if ($('#' + idd).attr("type") == "select") {
                                        strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').text;
                                    }
                                    p = p + '"' + strName + '"' + ':' + '"' + strJsonValue + '"' + ',';
                                }
                                var h = p.substring(0, p.length - 1);
                                j = h + '}'
                                allrw = allrw + j + ',';
                            }

                        });
                        allrw = allrw.substring(0, allrw.length - 1);
                        allrw = allrw + ']';
                        strifmDt = strifmDt + strDtSrsName + "?" + allrw + ';';
                    }
                });

                $('#hdnPluginValue').val(strifmDt);
                //----------------------------------

                $("#form1 textarea").each(function () {

                    if ($(this).attr("formCntl") == "yes") {
                        strChechBoxCheck = "";
                        strName = this.name;
                        strValue = this.value;
                        strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                        strValue = "";
                    }
                });

                //                $("#form1 input[type=text]").each(function () {
                //                    debugger;
                //                    if ($(this).attr("formCntl") == "yes") {
                //                        strChechBoxCheck = "";
                //                        strName = this.name;
                //                        strValue = this.value;
                //                        strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                //                        strValue = "";
                //                    }
                //                });




                $("#form1 input[type=text]").each(function () {

                    if ($(this).attr("formCntl") == "yes") {

                        var strGroupName = $(this).attr("groupName");
                        if (strGroupName == "DEG" || strGroupName == "MIN" || strGroupName == "SEC") {

                            strChechBoxCheck = "";
                            strName = this.name + strGroupName;
                            strValue = this.value;
                            strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                            strValue = "";
                        }
                        else {
                            strChechBoxCheck = "";
                            strName = this.name;
                            strValue = this.value;
                            strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                            strValue = "";
                        }
                    }
                });



                $('input[type=number]').each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        strChechBoxCheck = "";
                        strName = this.name;
                        strValue = this.value;
                        strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                        strValue = "";
                    }
                });

                $("#form1 input[type=radio]").each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        strChechBoxCheck = "";
                        strName = this.name;
                        var aa = $('#hdnRadioNam').val();
                        if ($('#hdnRadioNam').val() == null || $('#hdnRadioNam').val() == "") {

                            $('#hdnRadioNam').val(strName);
                            strValue = $('input[name=' + strName + ']:checked').val();
                        }
                        else {
                            if (strName != $('#hdnRadioNam').val()) {
                                $('#hdnRadioNam').val(strName);
                                strValue = $('input[name=' + strName + ']:checked').val();
                            }
                            else {
                                strValue = "";
                            }
                        }
                        if (strValue != null && strValue != "") {
                            strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                            strValue = "";
                        }
                    }
                });

                $('#form1 select').each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        strChechBoxCheck = "";
                        var strVal = "";
                        strName = this.name;
                        for (var i = 0; i < this.options.length; i++) {
                            if (this.options[i].selected == true) {
                                strVal = strVal + this.options[i].text + ',';
                            }
                        }
                        strVal = strVal.substr(0, strVal.length - 1);
                        strJson = strJson + '"' + strName + '":"' + strVal + '"' + ',';
                        strValue = "";
                    }
                });


                $("#form1 input[type=checkbox]").each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        var sThisVal = "";
                        strName = this.name;
                        strChechBoxCheck = strName;
                        if ($('#hdnCheckBox').val() == null || $('#hdnCheckBox').val() == "") {
                            $('#hdnCheckBox').val(strName);
                        }
                        if ($('#hdnCheckBox').val() == strName) {
                            strFirstName = strName;
                            sThisVal = (this.checked ? $(this).val() : "");
                            if (sThisVal != "") {
                                strValue = strValue + sThisVal + ',';
                            }
                        }
                        else {
                            strValue = strValue.substr(0, strValue.length - 1);
                            strJson = strJson + '"' + strFirstName + '":"' + strValue + '"' + ',';
                            $('#hdnCheckBox').val(strName);
                            strValue = "";
                            sThisVal = (this.checked ? $(this).val() : "");
                            if (sThisVal != "") {
                                strValue = strValue + sThisVal + ',';
                            }
                        }
                    }
                });

                if (strFirstName != "" && strFirstName != null) {
                    strValue = strValue.substr(0, strValue.length - 1);
                    strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                }

                $("#form1 input[type=file]").each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        var FnlDateFrmt = getFormattedDate();
                        strName = this.name;
                        strValue = this.value;
                        var strhdnFileid = 'hdn' + this.id;

                        if (strValue == "" && $("#" + strhdnFileid).val() == "") {
                            newFilName = "";
                        }
                        else if (strValue == "" && $("#" + strhdnFileid).val() != "") {
                            newFilName = $("#" + strhdnFileid).val();
                        }
                        else {
                            var id = this.id;
                            var fileNameExt = strValue.substr(strValue.lastIndexOf('.') + 1);
                            var newFilName = id + '_' + FnlDateFrmt + '.' + fileNameExt;
                            $('#' + id).attr('value', "df")
                            $('#hdnFileUpload').val("");
                            //fileu(id, newFilName);
                            Singlefileu(id, newFilName);
                            var name = $('#hdnFileUpload').val();
                            //Comment By Manoj Kumar Behera
                            if (name == undefined || name == "") {
                                newFilName = "";
                            }
                        }
                        strJson = strJson + '"' + strName + '":"' + newFilName + '"' + ',';
                    }
                });

                strJson = strJson.substr(0, strJson.length - 1);
                if (strJson != "" && strJson != null) {
                    strJson = strJson + "}"
                }
                SubmitsEncry(strJson);
            }

        }

        //------------------------------Edit---------------------------------------------------------------

        function SubmiClickFunDraft() {
            parent.start(1);
            //var outStatus = true; //  validateForm('lang');
            var outStatus = "";
            var Mode = GetParameterValues('ReqMode');
            if (Mode == "S") {
                outStatus = true;
            }
            else {
                outStatus = validateForm('lang');
            }
            if (outStatus == false) {
                return false;
            }
            else {
                strValue = "";
                var cnt = 0;
                $('#hdnCheckBox').val("");
                $('#hdnRadioNam').val("");
                var strJson = "{";

                //----------------------------------------------------------Iframe--------------------------

                $('#hdnVal').val('');

                strChechBoxCheck = "";
                var j = "";
                var rows = [];
                var s1 = [];

                var strifmDt = "";
                var fulNm = "";
                var flnmStatus = "No";
                var cnt = 0;
                var drpdtmCnt = 0;
                var fulDtFmt = "";

                LockScreen('We are processing your request...');

                $('.drpDtm').each(function () {
                    drpdtmCnt = drpdtmCnt + 1;
                    var dtmId = this.id;
                    strName = $(this).attr('name');
                    var dtmVl = $('#' + dtmId).val();
                    fulDtFmt = fulDtFmt + dtmVl;
                    if (drpdtmCnt == "2") {
                        drpdtmCnt = 0;
                        strJson = strJson + '"' + strName + '":"' + fulDtFmt + '"' + ',';
                        strValue = "";
                        fulDtFmt = "";
                    }
                });

                $(".flnm").each(function () {

                    cnt = cnt + 1;
                    var tblidd = this.id;
                    strName = $(this).attr('name');
                    var vl = $('#' + tblidd).val();
                    fulNm = fulNm + vl + ' ';
                    if (cnt == "4") {
                        cnt = 0;
                        strJson = strJson + '"' + strName + '":"' + fulNm + '"' + ',';
                        strValue = "";
                        fulNm = "";
                        flnmStatus = "Yes";
                    }
                });

                $("#form1 Table").each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        var allrw = "[";
                        var tblidd = this.id;
                        var strDtSrsName = $('#' + tblidd).attr("name");
                        var strTitle = this.title;
                        var id = 0;
                        var titlearray = strTitle.split('-');
                        $("#" + tblidd + " >tbody > tr").each(function () {
                            var $row = $(this);
                            var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                            if (parentId != undefined && parentId != 'undefined') {
                                id = parseInt(id) + 1;
                                var p = "{";
                                for (var i = 0; i < titlearray.length; i++) {
                                    var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                                    strName = titlearray[i];
                                    var idd = tblidd + '_' + titlearray[i] + '_' + id;
                                    if ($('#' + idd).attr("type") == "file") {
                                        if (strJsonValue != "") {
                                            var FnlDateFrmt = getFormattedDate();
                                            var strValue1 = $('#' + idd).val();
                                            var fileNameExt = strValue1.substr(strValue1.lastIndexOf('.') + 1);

                                            var newFilName = idd + '_' + FnlDateFrmt + '.' + fileNameExt;
                                            fileu(idd, newFilName);
                                            strJsonValue = newFilName;
                                        }
                                    }
                                    if ($('#' + idd).attr("type") == "select") {
                                        strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').text;
                                    }
                                    p = p + '"' + strName + '"' + ':' + '"' + strJsonValue + '"' + ',';
                                }
                                var h = p.substring(0, p.length - 1);
                                j = h + '}'
                                allrw = allrw + j + ',';
                            }

                        });
                        allrw = allrw.substring(0, allrw.length - 1);
                        allrw = allrw + ']';
                        strifmDt = strifmDt + strDtSrsName + "?" + allrw + ';';
                    }
                });

                $('#hdnPluginValue').val(strifmDt);

                //----------------------------------

                $("#form1 textarea").each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        strChechBoxCheck = "";
                        strName = this.name;
                        strValue = this.value;
                        strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                        strValue = "";
                    }
                });

                //                $("#form1 input[type=text]").each(function () {
                //                    if ($(this).attr("formCntl") == "yes") {
                //                        strChechBoxCheck = "";
                //                        strName = this.name;
                //                        strValue = this.value;
                //                        strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                //                        strValue = "";
                //                    }
                //                });


                $("#form1 input[type=text]").each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        var strGroupName = $(this).attr("groupName");

                        if (strGroupName == "DEG" || strGroupName == "MIN" || strGroupName == "SEC") {
                            strChechBoxCheck = "";
                            strName = this.name + strGroupName;
                            strValue = this.value;
                            strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                            strValue = "";
                        }
                        else {
                            strChechBoxCheck = "";
                            strName = this.name;
                            strValue = this.value;
                            strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                            strValue = "";
                        }
                    }
                });


                $('input[type=number]').each(function () {


                    if ($(this).attr("formCntl") == "yes") {
                        strChechBoxCheck = "";
                        strName = this.name;
                        strValue = this.value;
                        strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                        strValue = "";
                    }
                });

                $("#form1 input[type=radio]").each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        strChechBoxCheck = "";
                        strName = this.name;
                        var aa = $('#hdnRadioNam').val();
                        if ($('#hdnRadioNam').val() == null || $('#hdnRadioNam').val() == "") {

                            $('#hdnRadioNam').val(strName);
                            strValue = $('input[name=' + strName + ']:checked').val();
                        }
                        else {
                            if (strName != $('#hdnRadioNam').val()) {
                                $('#hdnRadioNam').val(strName);
                                strValue = $('input[name=' + strName + ']:checked').val();
                            }
                            else {
                                strValue = "";
                            }
                        }
                        if (strValue != null && strValue != "") {
                            strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                            strValue = "";
                        }
                    }
                });

                $('#form1 select').each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        strChechBoxCheck = "";
                        var strVal = "";
                        strName = this.name;
                        for (var i = 0; i < this.options.length; i++) {
                            if (this.options[i].selected == true) {
                                strVal = strVal + this.options[i].text + ',';
                            }
                        }
                        strVal = strVal.substr(0, strVal.length - 1);
                        strJson = strJson + '"' + strName + '":"' + strVal + '"' + ',';
                        strValue = "";
                    }
                });


                $("#form1 input[type=checkbox]").each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        var sThisVal = "";
                        strName = this.name;
                        strChechBoxCheck = strName;
                        if ($('#hdnCheckBox').val() == null || $('#hdnCheckBox').val() == "") {
                            $('#hdnCheckBox').val(strName);
                        }
                        if ($('#hdnCheckBox').val() == strName) {
                            strFirstName = strName;
                            sThisVal = (this.checked ? $(this).val() : "");
                            if (sThisVal != "") {
                                strValue = strValue + sThisVal + ',';
                            }
                        }
                        else {
                            strValue = strValue.substr(0, strValue.length - 1);
                            strJson = strJson + '"' + strFirstName + '":"' + strValue + '"' + ',';
                            $('#hdnCheckBox').val(strName);
                            strValue = "";
                            sThisVal = (this.checked ? $(this).val() : "");
                            if (sThisVal != "") {
                                strValue = strValue + sThisVal + ',';
                            }
                        }
                    }
                });

                if (strFirstName != "" && strFirstName != null) {
                    strValue = strValue.substr(0, strValue.length - 1);
                    strJson = strJson + '"' + strName + '":"' + strValue + '"' + ',';
                }

                $("#form1 input[type=file]").each(function () {
                    if ($(this).attr("formCntl") == "yes") {
                        var FnlDateFrmt = getFormattedDate();
                        strName = this.name;
                        strValue = this.value;
                        var strhdnFileid = 'hdn' + this.id;

                        if (strValue == "" && $("#" + strhdnFileid).val() == "") {
                            newFilName = "";
                        }
                        else if (strValue == "" && $("#" + strhdnFileid).val() != "") {
                            newFilName = $("#" + strhdnFileid).val();
                        }
                        else {
                            var id = this.id;
                            var fileNameExt = strValue.substr(strValue.lastIndexOf('.') + 1);
                            var newFilName = id + '_' + FnlDateFrmt + '.' + fileNameExt;
                            $('#' + id).attr('value', "df");
                            $('#hdnFileUpload').val("");
                            //fileu(id, newFilName);
                            Singlefileu(id, newFilName);
                            var name = $('#hdnFileUpload').val();
                            //Comment By Manoj Kumar Behera
                            if (name == undefined || name == "") {
                                newFilName = "";
                            }
                        }
                        strJson = strJson + '"' + strName + '":"' + newFilName + '"' + ',';
                    }
                });

                strJson = strJson.substr(0, strJson.length - 1);
                if (strJson != "" && strJson != null) {
                    strJson = strJson + "}"
                }
                SubmitsEncry(strJson);
            }

        }

        //---------------------------------EDITEND--------------------------------------

        function isNumber(e) {
            e = e || window.event;
            var charCode = e.which ? e.which : e.keyCode;
            return /\d/.test(String.fromCharCode(charCode));
        }

        function getFormattedDate() {
            var date = new Date();
            var yr = date.getFullYear();
            var mnth = (getFormattedPartTime(date.getMonth()) + 1);
            var dt = getFormattedPartTime(date.getDate());
            var hr = getFormattedPartTime(date.getHours());
            var mint = getFormattedPartTime(date.getMinutes());
            var snd = getFormattedPartTime(date.getSeconds());
            var str = yr + mnth + dt + hr + mint + snd;

            return str;
        }


        function getFormattedPartTime(partTime) {
            if (partTime < 10)
                return "0" + partTime;
            return partTime;
        }


        function UploadFileValidation(id) {
            var validExtensions = ['jpg', 'png', 'pdf']; //array of valid extensions
            var fileName = f.value;
            var fileNameExt = fileName.substr(fileName.lastIndexOf('.') + 1);
            if ($.inArray(fileNameExt, validExtensions) == -1) {
                alert("Invalid file type");
                $("#yourElem").uploadifyCancel(q);
                return false;
            }
            else {
                return fileNameExt;
            }
        }

        function ZeroAtFirstPlace(id) {
            var veroVl = $('#' + id).val().substring(0, 1);
            if (veroVl == "0") {
                var vl = $('#' + id).val();
                $('#' + id).val(vl.substring(1, vl.length));
                if (parseInt($('#' + id).val().length) != 10) {

                    $('#' + id).val("");
                    return false;
                }
                //alert(vl.substring(1,vl.length));
            }


        }

    </script>
    <script type="text/javascript">
        function fileu(id, FnlDateFrmt) {
            var ids = id;
            var fileUpload = $('#' + ids).get(0);
            var files = fileUpload.files;
            var test = new FormData();
            for (var i = 0; i < files.length; i++) {
                test.append(files[i].name, files[i]);
                var hh = files[i];
            }
            $.ajax({
                url: "FileHandler.ashx?FnlDateFrmt=" + FnlDateFrmt,
                type: "POST",
                contentType: false,
                processData: false,
                data: test,
                success: function (result) {
                    $('#hdnFileUpload').val(result);
                    var v = $('#hdnFileUpload').val();
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        }

        function Singlefileu(id, FnlDateFrmt) {
            var ids = id;
            var fileUpload = $('#' + ids).get(0);
            var files = fileUpload.files;
            var test = new FormData();
            for (var i = 0; i < files.length; i++) {
                test.append(files[i].name, files[i]);
                var hh = files[i];
            }
            $.ajax({
                url: "FileHandler.ashx?FnlDateFrmt=" + FnlDateFrmt,
                type: "POST",
                contentType: false,
                processData: false,
                data: test,
                async: false,
                success: function (result) {
                    $('#hdnFileUpload').val(result);
                    var v = $('#hdnFileUpload').val();
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        }
    </script>
    <script type="text/javascript">

        function ValidFileExtentionAndSize(ControlName, Extension, SizeInMB, Units, btndownloadid, btndelid) {

            $('.form-error, .form-error2').remove();
            $('.req-file, .form-error2').remove();


            $("#" + btndownloadid + "").attr('href', '#');
            $("#" + btndownloadid + "").attr("style", "visibility:hidden");
            $("#" + btndelid + "").attr("style", "visibility:hidden");
            $('#hdn_' + ControlName.id + '').val("");


            var fileExtension = Extension.split(','); // ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
            file = $(ControlName).val();
            var extension = file.substr((file.lastIndexOf('.') + 1)).toLowerCase(); ;

            if ($.inArray(extension, fileExtension) == -1) {
                $(ControlName).parent().parent().append('<p class="req-file"><small>' + " Only " + Extension + "  are allowed" + '</small></p>');
                $(ControlName).val("");
                return false;
            }
            else {
                $('.req-file, .form-error2').remove();
                var uploadControl = document.getElementById(ControlName);
                var convertMb = 0;
                if (Units == 'MB') {
                    convertMb = eval(SizeInMB) * 1024 * 1024;
                }
                else {
                    convertMb = eval(SizeInMB) * 1024;
                }


                if (ControlName.files[0].size > convertMb) {
                    $(ControlName).parent().parent().append('<p class="req-file"><small>' + "File size should be less then " + SizeInMB + "  MB" + '</samll></p>');
                    $(ControlName).val("");
                    return false;
                }
                else {
                }
            }
            //fileUploadEvent(ControlName);
            //$('#hdnfileidname').val(ControlName.id);
        }


        function fileDeleteEvent(btnfileuploadid, btndownloadid, btndelid) {
            $("#" + btnfileuploadid + "").val("");
            $('#hdnFileUpload').val("");
            $('#hdnfileidname').val("");
            $("#" + btndownloadid + "").attr('href', '#');
            $("#" + btndownloadid + "").attr("style", "visibility:hidden");
            $("#" + btndelid + "").attr("style", "visibility:hidden");
            $('#hdn_' + btnfileuploadid + '').val("");
        }


        function fileUploadEvent(btndownloadid, btndelid) {
            var ids = btndownloadid.replace('btndownload', '');
            //var ids = $('#hdnfileidname').val();
            var strValue = $('#' + ids).val();
            var FnlDateFrmt = getFormattedDate();
            var fileNameExt = strValue.substr(strValue.lastIndexOf('.') + 1);
            var newFilName = ids + '_' + FnlDateFrmt + '.' + fileNameExt;
            $('#hdn_' + ids).val(newFilName);
            var fileUpload = $('#' + ids).get(0);
            var files = fileUpload.files;
            var test = new FormData();
            for (var i = 0; i < files.length; i++) {
                test.append(files[i].name, files[i]);
                var hh = files[i];
            }
            $("#fileimage").show();
            $.ajax({
                url: "FileHandler.ashx?FnlDateFrmt=" + newFilName,
                type: "POST",
                contentType: false,
                processData: false,
                data: test,
                success: function (result) {
                    $('#hdnFileUpload').val(result);
                    var v = $('#hdnFileUpload').val();
                    $("#" + btndownloadid + "").attr('href', 'Portal/Document/Upload/' + result + '');
                    $("#" + btndownloadid + "").removeAttr("style");
                    $("#" + btndownloadid + "").attr("visibility", "visible");
                    $("#" + btndelid + "").removeAttr("style");
                    $("#" + btndelid + "").attr("visibility", "visible");
                    $("#fileimage").hide();
                },
                error: function (err) {
                    alert(err.statusText);
                    $("#" + btndownloadid + "").attr('href', '#');
                    $("#" + btndownloadid + "").attr("style", "visibility:hidden");
                    $("#" + btndelid + "").attr("style", "visibility:hidden");
                    $("#fileimage").hide();
                }
            });
        }



        //function fileUploadEvent(idss) {

        //    var ids = idss.id;
        //    var strValue = $('#' + ids).val();
        //    var FnlDateFrmt = getFormattedDate();
        //    var fileNameExt = strValue.substr(strValue.lastIndexOf('.') + 1);
        //    var newFilName = ids + '_' + FnlDateFrmt + '.' + fileNameExt;
        //    $('#hdn_' + ids).val(newFilName);
        //    var fileUpload = $('#' + ids).get(0);
        //    var files = fileUpload.files;
        //    var test = new FormData();
        //    for (var i = 0; i < files.length; i++) {
        //        test.append(files[i].name, files[i]);
        //        var hh = files[i];
        //    }
        //    $.ajax({
        //        url: "FileHandler.ashx?FnlDateFrmt=" + newFilName,
        //        type: "POST",
        //        contentType: false,
        //        processData: false,
        //        data: test,
        //        success: function (result) {
        //            $('#hdnFileUpload').val(result);
        //            var v = $('#hdnFileUpload').val();
        //        },
        //        error: function (err) {
        //            alert(err.statusText);
        //        }
        //    });
        //}

    </script>
    <script type="text/javascript">
        function cloneTable(id) {

        }
    </script>
    <script src="js/moment-with-locales.js" type="text/javascript"></script>
    <script src="js/bootstrap-datetimepicker1.js" type="text/javascript"></script>
    <link href="css/bootstrap-datetimepicker1.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#fileimage").hide();
            $('#frmContent').on('keyup', '.rset', function () {
                $(this).css('border-color', '#c5c5c5');
            });
            $('#frmContent').on('change', '.rset', function () {
                $(this).css('border-color', '#c5c5c5');
                $(this).parent().find('.form-error, .form-error2').remove();
            });

            var i = 1;
            AppendGrid();
        });
    </script>
    <script type="text/javascript">
        function AppendGrid() {

            var gData = [{}];
            var gSubColumns = [{}];
            var AllTableId = $('#hdnPluginJson').val();

            if (AllTableId != "" && AllTableId != null) {
                var tblIdAry = AllTableId.split(',');
                for (var i = 0; i < tblIdAry.length; i++) {
                    var tblId = tblIdAry[i];
                    var strvalue = $('#' + tblId).attr('data');
                    var js1 = JSON.parse(strvalue);

                    sa(js1, tblId);
                }
            }

        }


        function sa(js1, tableid) {
            var gData = null;
            var editvalue1 = $('#' + tableid).attr("EditData");
            var gData1 = JSON.parse(editvalue1);
            gData = gData1; // [{ "Details": "ERTE", "DateOfPurchase": "22-Feb-2018", "PlaceWhereToPlace": "RET", "Remark": "ERTE"}];
            QUnit.test('appendGrid.subgrid01', function (assert) {

                var eValid = false, eValues, eOtherValues, eIndex;

                var $grid = $('#' + tableid).appendGrid({
                    columns: js1,
                    initData: gData
                    // Load: editvalue //{ "name": "Remark", "display": "Remarks (Specify type,capacity)", "type": "text", "ctrlOptions": ["a"], "ctrlClass": "req rset clsDt", "ctrlAttr": { "title": "This filed", "maxlength": "200"} }

                });
            });

            return false;
        }



    </script>
    <script type="text/javascript">
        function bindTable() {
            var gData = [{}]

            var strvalue = $('#Plugn_1').attr('data');
            var js1 = JSON.parse(strvalue);
            // Start the test
            QUnit.test('appendGrid.subgrid01', function (assert) {
                // Declare variables for testing
                var eValid = false, eValues, eOtherValues, eIndex;
                // Create grid
                var $grid = $('#Plugn_1').appendGrid({
                    columns: js1,
                    initData: gData,
                    useSubPanel: true,
                    subPanelBuilder: function (cell, uniqueIndex) {
                        var $subgrid = $('<table></table>').attr('id', 'tblSubGrid_' + uniqueIndex).appendTo(cell);
                        $subgrid.appendGrid({
                            initRows: 0,
                            columns: gSubColumns
                        });
                    },
                    subPanelGetter: function (uniqueIndex) {
                        return $('#tblSubGrid_' + uniqueIndex).appendGrid('getAllValue', true);
                    },
                    rowDataLoaded: function (caller, record, rowIndex, uniqueIndex) {
                        if (record.SubGridData) {
                            $('#tblSubGrid_' + uniqueIndex, caller).appendGrid('load', record.SubGridData);
                        }
                    }
                });
            });
        }
    </script>
    <script type="text/javascript">

        function MobileValidation(fld) {
            var val = $('#' + fld).val();
            if (val.substring(0, 1) === '0') {
                $('#' + fld).val('');
                $('#' + fld).focus();
                return false;
            }
            if (val.length < 10) {
                $('#' + fld).focus();
                $('#' + fld).val('');
                return false;
            }
            if (val.length > 10) {
                $('#' + fld).focus();
                $('#' + fld).val('');
                return false;
            }

        }

        function inputLimiter(e, allow) {

            var AllowableCharacters = '';
            if (allow == 'Mobile') {
                AllowableCharacters = '1234567890';



            }
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
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&(),-./:=@\_|';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
            }
            if (allow == 'RawMetrial') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:<=>?@\_|~';
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

        function isNumberKey(evt, element) {

            //getting key code of pressed key
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (
                (charCode != 45 || $('#' + element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $('#' + element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57))
                return false;

            return true;
        }





        function EncriptData(strData) {
            var encdata = "";
            $.ajax({
                type: "POST",
                url: "FormView.aspx/GetEncriptCode",
                data: "{'strData':'" + strData + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    encdata = r.d;
                    return encdata;
                    //                        alert('hi');
                    //                        $('#hdnResultOutPut').val(r.d);
                    //                        alert($('#hdnResultOutPut').val());
                }
            });
            return encdata;
        }

        function fillHdnData(strdt) {

            $('#hdnResultOutPut').val(strdt);
            // alert($('#hdnResultOutPut').val());
        }
        function PluginisNumberKey(evt, ths) {


            var idsss = ths.id;

            //getting key code of pressed key
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (
                (charCode != 45 || $('#' + idsss).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $('#' + idsss).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function FloatOnly(evt, control) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                //alert("Enter Numerals only in this Field!");             
                return false;
            }
            if (charCode == 46) {
                var patt1 = new RegExp("\\.");
                var ch = patt1.exec(control.value);
                if (ch == ".") {
                    //alert("More then One Decimal Point not Allowed");
                    return false;
                }
            }
            return true;
        }


    </script>
    <script type="text/javascript">

        $(function () {

            $('.date').datetimepicker(
                {
                    format: 'DD-MMM-YYYY'
                }
            );
            $('body').on('focus', ".date", function () {
                $(this).datetimepicker({
                    format: 'DD-MMM-YYYY'
                });
            });

        })

    </script>
    <script type="text/javascript">
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
    </script>
    <script type="text/javascript">

        function SubmitsEncry(strData) {
            var key = CryptoJS.enc.Utf8.parse('8080808080808080');
            var iv = CryptoJS.enc.Utf8.parse('8080808080808080');

            var encryptedlogin = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(strData), key,
                {
                    keySize: 128 / 8,
                    iv: iv,
                    mode: CryptoJS.mode.CBC,
                    padding: CryptoJS.pad.Pkcs7
                });

            $('#hdnResultOutPut').val(encryptedlogin);
        }
    </script>
</body>
</html>
