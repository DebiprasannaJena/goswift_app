<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectMasterAdd.aspx.cs"
    Inherits="SingleWindow_ProjectMasterAdd" %>

<%@ Register Src="~/Include/IncludeScript.ascx" TagName="IncludeScript" TagPrefix="ucIncludeScript" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Include/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/includes/Leftmenupanel.ascx" TagName="leftMenu" TagPrefix="ucLeftMenu" %>
<%@ Register Src="~/include/AMSfooter.ascx" TagName="footer" TagPrefix="ucfooter" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>SWP</title>
    <!-- Favicon and touch icons -->
    <link rel="shortcut icon" href="../images/favicon.ico" type="image/x-icon">
    <!-- Start Styles
         =====================================================================-->
    <script src="../../js/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../js/jQuery.alert.js" type="text/javascript"></script>
    <link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="../../PortalCSS/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/lobipanel.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/flash.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/pe-icon-7-stroke.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/themify-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/emojionearea.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/monthly.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/jquery.timepicker.css" rel="stylesheet" type="text/css" />
    <!-- End Styles
         =====================================================================-->
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../../js/Validator.js" type="text/javascript"></script>
    <script src="../../js/custom.js" type="text/javascript"></script>
    <script src="../js/jquery.timepicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        pageHeader = "Add Proposal"
        indicate = "yes"
        //backMe = "yes"
        //printMe = "yes"

        window.onload = function () {
            //            configTab();
            //            configButton();
            configTitleBar();
        }
        $(document).ready(function () {

            var i = 1;
            var option = '';
            $("#txtSector").hide();
            var values = $('#ddlSector').val();

            if (values == 21) {

                $("#txtSector").show();
            }
            else {
                $("#txtSector").hide();
                $("#txtSector").val("");
            }
            $('#ibtnCAdd').click(function (e) {
                //                if (!BlankTextBox('txtCapacity', ' Capacity')) { return false; }

                if ($("#txtCapacity").val().length >= 5) {
                    var txt = $("[id*=txtCapacity]");
                    var Capacity = $(txt).val();
                    var lbCapacity = $("[id*=lbCapacity]");
                    var options = $('#lbCapacity option');
                    var alreadyExist = false;

                    $(options).each(function () {
                        //alert(Capacity); alert($(this).val());
                        if ($(this).val() == Capacity) {
                            alert("Project Capacity Already Exists");
                            alreadyExist = true;
                            return false;
                        }
                    });
                    if (!alreadyExist) {
                        lbCapacity.append($('<option></option>').attr('value', Capacity).text(Capacity));
                        txt.focus();
                        option = "";
                        $('#lbCapacity option').map(function () {
                            option = option + '~' + $(this).text();
                        });

                        $('#hdnCapacity').val(option);
                    }
                    txt.val('');
                    e.preventDefault();
                }
                else {
                    alert('Sorry, Project Capacity Length should be greater than equal to 5 character');
                    return false;
                }

            });
            $('#ibtnAdd').click(function (e) {
                debugger;
                if (!BlankTextBox('txtDirectors', ' Board Of Directors')) { return false; }
                if ($("#txtDirectors").val().length >= 6) {
                    var txt = $("[id*=txtDirectors]");
                    var Directors = $(txt).val();
                    var lbDirectors = $("[id*=lbDirectors]");
                    var options = $('#lbDirectors option');
                    var alreadyExist = false;

                    $(options).each(function () {
                        //alert(Directors); alert($(this).val());
                        if ($(this).val() == Directors) {
                            alert("Directors Name Already Exists");
                            alreadyExist = true;
                            return false;
                        }
                    });
                    if (!alreadyExist) {
                        lbDirectors.append($('<option></option>').attr('value', Directors).text(Directors));

                        option = "";
                        $('#lbDirectors option').map(function () {
                            option = option + '~' + $(this).text();
                        });

                        $('#hdnDirectors').val(option);
                    }
                    txt.val('');
                    e.preventDefault();
                }
                else {
                    alert('Sorry, Name of a director should be greater than equal to 6 character');
                    return false;
                }

            });
            $('#btnAdd').click(function (e) {
                if (!BlankTextBox('txtBusiness', ' Existing Business Intrest Of the compnay')) { return false; }

                if ($("#txtBusiness").val().length >= 4) {
                    var txt = $("[id*=txtBusiness]");
                    var Business = $(txt).val();
                    var lbBusiness = $("[id*=lbBusiness]");
                    var options = $('#lbBusiness option');
                    var alreadyExist = false;

                    $(options).each(function () {
                        //alert(Business); alert($(this).val());
                        if ($(this).val() == Business) {
                            alert("Business Alread Exists");
                            alreadyExist = true;
                            return false;
                        }
                    });
                    if (!alreadyExist) {
                        lbBusiness.append($('<option></option>').attr('value', Business).text(Business));
                        txt.focus();
                        option = "";
                        $('#lbBusiness option').map(function () {
                            option = option + '~' + $(this).text();
                        });

                        $('#hdnBusiness').val(option);
                    }
                    txt.val('');
                    e.preventDefault();
                }
                else {
                    alert('Sorry, Name of existing business should be greater than equal to 4 character');
                    return false;
                }

            });
            $('#ddlSector').change(function () {
                var SectorId = $('#ddlSector').val();

                if (SectorId == 21) {
                    $('#txtSector').show();
                }
                else {
                    $('#txtSector').hide();
                    $('#txtSector').val('');
                }
            });
            $('#ibtnCRemove').click(function () {
                if ($("#lbCapacity option:selected").length == 0) {
                    alert('Select Project Capacity To Remove !!!')
                    $('#lbCapacity').focus();
                    return false;
                }
                else {
                    $('#lbCapacity option:selected').remove();

                    option = "";
                    $('#lbCapacity option').map(function () {
                        option = option + '~' + $(this).text();
                    });

                    $('#hdnCapacity').val(option);

                    return false;
                }
            });
            $('#ibtnRemove').click(function () {
                if ($("#lbDirectors option:selected").length == 0) {
                    alert('Select Directors Name To Remove !!!')
                    $('#lbDirectors').focus();
                    return false;
                }
                else {
                    $('#lbDirectors option:selected').remove();

                    option = "";
                    $('#lbDirectors option').map(function () {
                        option = option + '~' + $(this).text();
                    });

                    $('#hdnDirectors').val(option);

                    return false;
                }
            });
            $("#btnRemove").click(function (e) {

                if ($('#lbBusiness option:selected').length == 0) {
                    alert('Select Existing Business To Remove !!!');
                    $('#lbBusiness').focus();
                    return false;
                }
                else {
                    $('#lbBusiness option:selected').remove();
                    option = "";
                    $('#lbBusiness option').map(function () {
                        option = option + '~' + $(this).text();
                    });

                    $('#hdnBusiness').val(option);
                    return false;
                }
            });
        });

        function pageLoad() {
            $(function () {
                $("[id*=Grdlocation] [id*=ButtonAdd]").click(function () {
                    var row = $(this).closest("tr");
                    var ddlDistrict = row.find("[id*=DdlDistrict]");
                    var txtLocation = row.find("[id*=txtLocation]");
                    var message = "";
                    if (ddlDistrict.val() == "0" && ($.trim(txtLocation.val()) != "")) {
                        message += "Please select District.";
                        ddlDistrict.focus();
                    }
                    //                    if ($.trim(txtLocation.val()) == "" && ddlDistrict.val() != "0") {
                    //                        message += "Please enter Location.\n";
                    //                        txtLocation.focus();
                    //                    }
                    //                  if ($.trim(txtLocation.val()) == "" && ddlDistrict.val() == "0") {
                    if (ddlDistrict.val() == "0") {
                        message += "Please add District of the project.\n";
                        ddlDistrict.focus();
                    }
                    if (message != "") {
                        alert(message);
                        return false;
                    }
                    return true;
                });
            });
            $(function () {
                $("[id*=grdAddMore] [id*=BtnAddMore]").click(function () {
                    var row = $(this).closest("tr");
                    var txtPro = row.find("[id*=txtProduct]");
                    var message = "";
                    if ($.trim(txtPro.val()) == "") {
                        message += "Please enter Product.";
                        txtPro.focus();
                    }
                    if (message != "") {
                        alert(message);
                        return false;
                    }
                    return true;
                });
            });

        }

     
    </script>
    <style>
        a
        {
            text-decoration: none !important;
        }
        .steps
        {
            padding-left: 0;
            list-style: none;
            font-size: 12px;
            line-height: 1;
            margin: 0 auto 20px;
            border-radius: 3px;
            width: 72%;
        }
        
        .steps strong
        {
            font-size: 14px;
            display: block;
            line-height: 1.4;
        }
        .steps h4
        {
            line-height: 24px;
            font-size: 16px;
            margin: 4px;
        }
        .steps > li
        {
            position: relative;
            display: block;
            width: auto;
        }
        .steps > li a
        {
            padding: 6px 20px 6px 44px;
            display: block;
            color: #000;
        }
        .steps > li
        {
            display: inline-block;
        }
        .steps .past
        {
            color: #fff;
            background: #c18a0a;
        }
        .steps .present
        {
            color: #000;
            background: #ed3237;
        }
        .steps .present a
        {
            color: #FFF;
        }
        .steps .future
        {
            color: #777;
            background: #efefef;
        }
        .steps .past a
        {
            color: #fff !important;
            display: block;
        }
        .steps li > span:after, .steps li > span:before
        {
            content: "";
            display: block;
            width: 0px;
            height: 0px;
            position: absolute;
            top: 0;
            left: -6px;
            border: solid transparent;
            border-left-color: #f0f0f0;
            border-width: 22px;
        }
        
        .steps li > span:after
        {
            top: -5px;
            z-index: 1;
            border-left-color: white;
            border-width: 27px;
        }
        
        .steps li > span:before
        {
            z-index: 2;
        }
        
        .steps li.past + li > span:before
        {
            border-left-color: #c18a0a;
        }
        .steps li.present + li > span:before
        {
            border-left-color: #ed3237;
        }
        .steps li.future + li > span:before
        {
            border-left-color: #efefef;
        }
        
        .steps li:first-child > span:after, .steps li:first-child > span:before
        {
            display: none;
        }
        
        /* Arrows at start and end */
        .steps li:first-child i, .steps li:last-child i
        {
            display: block;
            position: absolute;
            top: 0;
            left: -6;
            border: solid transparent;
            border-left-color: white;
            border-width: 22px;
        }
        
        .steps li:last-child i
        {
            left: auto;
            right: -25px;
            border-left-color: transparent;
            border-top-color: white;
            border-bottom-color: white;
        }
    </style>
</head>
<body class="hold-transition sidebar-mini">
    <div class="wrapper">
        <script type="text/javascript" language="javascript">
            function Validation() {
                debugger;
                if (!blankFieldValidation('txtTitle', ' Project Title')) { return false; }
                if (!WhiteSpaceValidation1st('txtTitle', ' Project Title ')) { return false; }

                if (!blankFieldValidation('txtCompanyName', ' Company Name')) { return false; }
                if (!WhiteSpaceValidation1st('txtCompanyName', ' Company Name ')) { return false; }
                if (!DropDownValidation('ddlSector', ' Sector ')) { return false; }
                var SectorId = $('#ddlSector').val();
                if (SectorId == 21) {
                    if (!blankFieldValidation('txtSector', ' Other Sector Name')) { return false; }
                    if (!WhiteSpaceValidation1st('txtSector', ' Sector ')) { return false; }
                }
                if (!blankFieldValidation('txtApplDate', ' Date of Application')) { return false; }
                if (!CheckGreaterDate('txtApplDate', ' Date of Application')) { return false; }


                var rows = $("[id*=Grdlocation] tr").length;
                if (rows == 2) {
                    var j = 0;
                    for (var i = 0; i < rows - 1; i++) {

                        if (i + 2 < 10) {

                            j = "0" + (i + 2);
                        }
                        else {

                            j = i + 2;
                        }

                        //                        var x = document.getElementById('Grdlocation_ctl' + j + '_DdlDistrict').value;
                        //                        var y = document.getElementById('Grdlocation_ctl' + j + '_txtLocation').value;

                        var x = document.getElementById('Grdlocation_DdlDistrict_' + i).value;
                        var y = document.getElementById('Grdlocation_txtLocation_' + i).value;

                        if (x != "0") {
                            if (y == "") {
                                //                            alert('Please add at least one location Details.');
                                //                            return false;
                            }
                            else {
                            }
                        }
                        else {
                            alert('Please add at least one location Details.');
                            return false;
                        }
                    }
                }

                var rows = $("[id*=grdAddMore] tr").length;
                if (rows == 2) {
                    var j = 0;
                    for (var i = 0; i < rows - 1; i++) {

                        if (i + 2 < 10) {

                            j = "0" + (i + 2);
                        }
                        else {

                            j = i + 2;
                        }
                        //                        var x = document.getElementById('grdAddMore_ctl' + j + '_txtProduct').value;
                        var x = document.getElementById('grdAddMore_txtProduct_' + i).value;
                        if (x == "") {
                            alert('Please add at least one Product Details.');
                            return false;
                        }
                    }
                }
                if (!DropDownValidation('ddlCATEGORY', ' Category ')) { return false; }

                if ($('#lbDirectors').children().length <= 0) {
                    alert('Add Board Of Directors');
                    $('#lbDirectors').focus();
                    return false;
                }
                //            if (!ConfirmAction('btnSubmit', 'Are you sure want to Save in draft ?', 'Are you sure want to Update ?')) {
                //                return false;
                //            }
            }

        </script>
        <form id="form1" runat="server" defaultbutton="btnSubmit">
        <!--Header-->
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <ucheader:header ID="header1" runat="server" />
        <!-- Left side column. contains the sidebar -->
        <aside class="main-sidebar">
            <!-- sidebar -->
            <div class="sidebar">
                <!-- sidebar menu -->
                <ucLeftMenu:leftMenu ID="leftMenu2" runat="server" />
            </div>
            <!-- /.sidebar -->
        </aside>
        <div class="content-wrapper">
            <section class="content-header">
                <div class="header-icon">
                    <i class="fa fa-dashboard"></i>
                </div>
                <div class="header-title">
                    <h1>
                        Add Proposal</h1>
                    <ul class="breadcrumb">
                        <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                        <li><a>AMS</a></li><li><a>Create Project</a></li>
                    </ul>
                </div>
            </section>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-bd lobidrag">
                            <div class="panel-body">
                                <ul class="steps">
                                    <li class="present"><span>
                                        <h4>
                                            <a href="ProjectMasterAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-1</a></h4>
                                    </span><i></i></li>
                                    <li class="future"><span>
                                        <h4>
                                            <a href="ProposalMasterAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-2</a></h4>
                                    </span><i></i></li>
                                    <li class="future"><span>
                                        <h4>
                                            <a href="ProjectDetailsAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-3</a></h4>
                                    </span><i></i></li>
                                    <li class="future"><span>
                                        <h4>
                                            <a href="FinancingDetailsAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-4</a></h4>
                                    </span><i></i></li>
                                    <li class="future"><span>
                                        <h4>
                                            <a href="FinancialPerformanceAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-5</a></h4>
                                    </span><i></i></li>
                                    <li class="future"><span>
                                        <h4>
                                            <a href="FinancialDocumentAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-6</a></h4>
                                    </span><i></i></li>
                                </ul>
                                <div class="clearfix">
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Project Title</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Rows="2" TabIndex="1"
                                            spellcheck="true" TextMode="MultiLine" />
                                        <span class="mandetory">*</span>
                                        <cc1:FilteredTextBoxExtender ID="FETitle" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                            ValidChars=" /.\(),:-_&quot;" TargetControlID="txtTitle" InvalidChars="'%*|">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                    <label class="col-sm-2">
                                        Company Name</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span><asp:TextBox ID="txtCompanyName" runat="server" MaxLength="100"
                                            CssClass="form-control" TabIndex="2" />
                                        <span class="mandetory">*</span>
                                        <cc1:FilteredTextBoxExtender ID="FECompanyName" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                            ValidChars=" /.\(),:-_" TargetControlID="txtCompanyName" InvalidChars="'%*|">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Sector</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control" TabIndex="3">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                        <span class="mandetory">*</span>
                                        <asp:TextBox ID="txtSector" runat="server" MaxLength="50" CssClass="form-control "
                                            Style="margin-top: 10px" TabIndex="4"> </asp:TextBox>
                                        <%--     <span class="mandatory">*</span>--%>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,custom"
                                            ValidChars=" /.\" TargetControlID="txtSector" InvalidChars="#$^&*()_+[]{}?:;|',~`-=!<>%">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                    <label class="col-sm-2">
                                        Date of Application</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <div class="input-group date datePicker">
                                            <asp:TextBox ID="txtApplDate" runat="server" CssClass="form-control date-picker"
                                                TabIndex="5"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        </div>
                                        <span class="mandetory">*</span></div>
                                </div>
                                <hr>
                                <div class="row">
                                    <label class="col-sm-2">
                                        Project Location Details</label>
                                    <div class="col-sm-6 ">
                                        <span class="colon">:</span>
                                        <asp:UpdatePanel ID="UpdatePanelloc" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="Grdlocation" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                                                    border="0" CssClass="table table-bordered" Visible="true" OnRowDataBound="Grdlocation_RowDataBound"
                                                    ShowFooter="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="30px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District" HeaderStyle-Width="250px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DistrictId") %>' Visible="false" />
                                                                <asp:DropDownList ID="DdlDistrict" runat="server" CssClass="form-control" TabIndex="6">
                                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="250px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location of Project">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtLocation" runat="server" MaxLength="100" CssClass="form-control"
                                                                    TabIndex="6" Text='<%#Eval("ProjectLocation") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDelete1" runat="server" ImageUrl="~/images/delete_btn.gif"
                                                                    ToolTip="Click To Delete !" OnClick="imgbtnDelete1_Click" />
                                                                <asp:Button ID="ButtonAdd" runat="server" Text="Add More" OnClick="ButtonAdd_Click"
                                                                    CssClass="btn btn-success" TabIndex="6" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="30px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-sm-2">
                                        Product and Capacity Details</label>
                                    <div class="col-sm-6">
                                        <span class="colon">:</span>
                                        <asp:UpdatePanel ID="UpdPnlProductDtls" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdAddMore" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                                                    border="0" CssClass="table table-bordered" ShowFooter="false" OnRowDataBound="grdAddMore_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product" HeaderStyle-Width="250px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProduct" runat="server" MaxLength="500" CssClass="form-control"
                                                                    TabIndex="7" Text='<% #Eval("Product") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Capacity">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCapacity" runat="server" MaxLength="500" CssClass="form-control"
                                                                    TabIndex="7" Text='<% #Eval("Capacity") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/images/delete_btn.gif"
                                                                    ToolTip="Click To Delete !" OnClick="imgbtnDelete_Click" />
                                                                <asp:Button ID="BtnAddMore" runat="server" Text="Add More" CssClass="btn btn-success secAdd"
                                                                    OnClick="BtnAddMore_Click" TabIndex="7" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <hr>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Environment Category</label>
                                    <div class="col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="ddlCATEGORY" runat="server" CssClass="form-control" TabIndex="8">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Green"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Orange"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Red"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="White"></asp:ListItem>
                                        </asp:DropDownList>
                                        <span class="mandetory">*</span>
                                    </div>
                                    <label class="col-sm-1">
                                        Type</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <div class="radio-inline ">
                                            <label class="radio-inline ">
                                                <asp:RadioButton ID="rbtNew" Text="New" Checked="true" GroupName="PType" runat="server" />
                                            </label>
                                            <label class="radio-inline ">
                                                <asp:RadioButton ID="rbtExpansion" Text="Expansion" GroupName="PType" runat="server" />
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Board Of Directors</label>
                                    <div class="col-sm-3">
                                        <span class="colon">:</span><asp:TextBox ID="txtDirectors" runat="server" MaxLength="150"
                                            CssClass="form-control" TabIndex="9" class="form-control"></asp:TextBox>
                                        <span class="mandetory">*</span>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="UppercaseLetters, LowercaseLetters,custom"
                                            ValidChars=". ()-" TargetControlID="txtDirectors" InvalidChars=",!<>%">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                    <div class="col-sm-1 text-center">
                                        <asp:ImageButton ID="ibtnAdd" CssClass="btn btn-success m-b-5" ImageUrl="~/images/double-angle-pointing-to-right.png"
                                            runat="server" TabIndex="10" /><br />
                                        <asp:ImageButton ID="ibtnRemove" CssClass="btn btn-danger" ImageUrl="~/images/access-denied.png"
                                            runat="server" />
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:ListBox ID="lbDirectors" CssClass="form-control" runat="server"></asp:ListBox>
                                        <span class="mandetory">*</span>
                                        <asp:HiddenField ID="hdnDirectors" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Existing Business Intrests Of the compnay</label>
                                    <div class="col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtBusiness" runat="server" MaxLength="500" TabIndex="11" class="form-control"></asp:TextBox>
                                        <%--<span class="mandatory">*</span>--%>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="UppercaseLetters, LowercaseLetters,custom"
                                            ValidChars=".-,() " TargetControlID="txtBusiness" InvalidChars="!<>%">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                    <div class="col-sm-1 text-center">
                                        <asp:ImageButton ID="btnAdd" CssClass="btn btn-success m-b-5" ImageUrl="~/images/double-angle-pointing-to-right.png"
                                            runat="server" TabIndex="12" /><br />
                                        <asp:ImageButton ID="btnRemove" CssClass="btn btn-danger" ImageUrl="~/images/access-denied.png"
                                            runat="server" /></div>
                                    <div class="col-sm-3">
                                        <asp:ListBox ID="lbBusiness" CssClass="form-control" runat="server"></asp:ListBox>
                                        <asp:HiddenField ID="hdnBusiness" runat="server" />
                                    </div>
                                </div>
                                <hr>
                                <div id="trRemark" runat="server" class="row m-b-10">
                                    <div class="row">
                                        <label class="col-sm-2">
                                        </label>
                                        <div class="col-sm-4">
                                            <asp:Repeater ID="RptCMDRemark" runat="server">
                                                <HeaderTemplate>
                                                    <table class="table table-bordered table-condensed">
                                                        <tr>
                                                            <th colspan="2">
                                                                <b>CMD Comments</b>
                                                            </th>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            No. of Times:
                                                            <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("NO_OF_REMARK") %>' Font-Bold="true" />
                                                            <br />
                                                            <asp:Label ID="lblCMDRemark" runat="server" Text='<%#Eval("CMDRemark") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Repeater ID="RptGMRemark" runat="server">
                                                <HeaderTemplate>
                                                    <table class="table table-bordered table-condensed">
                                                        <tr>
                                                            <th colspan="2">
                                                                <b>GM Comments</b>
                                                            </th>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            No. of Times:
                                                            <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("NO_OF_REMARK") %>' Font-Bold="true" />
                                                            <br />
                                                            <asp:Label ID="lblCMDRemark" runat="server" Text='<%#Eval("CMDRemark") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="row m-b-10" id="trRemarkEnt" runat="server">
                                    <label class="col-sm-2">
                                        Comment</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtRemark" runat="server" Rows="3" CssClass="form-control" TextMode="MultiLine"
                                            MaxLength="500"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FERemark" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            ValidChars=".-,() " TargetControlID="txtRemark" InvalidChars="!<>%'">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:HiddenField ID="hdnRemarkID" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4 col-sm-offset-2">
                                        <asp:HiddenField ID="hfUID" runat="server" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Next" CssClass="btn btn-success"
                                            OnClientClick="return Validation();" OnClick="btnSubmit_Click" TabIndex="20" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click"
                                            TabIndex="21" />
                                    </div>
                                </div>
                            </div>
                            <div class="addTable">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr style="display: none">
                                        <td width="160">
                                            Tourism
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <div class="radiobtn pull-left">
                                                <label>
                                                    <asp:RadioButton ID="rbtnY" Text="Yes" GroupName="PType1" runat="server" Checked="true" />
                                                </label>
                                            </div>
                                            <div class="radiobtn pull-left">
                                                <label>
                                                    <asp:RadioButton ID="rbtnN" Text="No" GroupName="PType1" runat="server" />
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <!-- Modal -->
        <ucfooter:footer ID="footer1" runat="server" />
        </form>
    </div>
</body>
</html>
