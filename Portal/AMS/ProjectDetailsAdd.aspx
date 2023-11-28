<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectDetailsAdd.aspx.cs"
    EnableEventValidation="false" Inherits="SingleWindow_ProjectDetailsAdd" %>

<%@ Register Src="~/Include/IncludeScript.ascx" TagName="IncludeScript" TagPrefix="ucIncludeScript" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Include/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/includes/Leftmenupanel.ascx" TagName="leftMenu" TagPrefix="ucLeftMenu" %>
<%@ Register Src="~/include/AMSfooter.ascx" TagName="footer" TagPrefix="ucfooter" %>
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
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../../js/Validator.js" type="text/javascript"></script>
    <script src="../js/jquery.timepicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        pageHeader = "Manage Project Details"
        indicate = "yes"

        window.onload = function () {
            configTitleBar();
        }
        function Load() {
            SetVisible();
        }

        $(document).ready(function () {
            $("#BtnAddMore").click(function (e) {
                debugger;
                if ($("#ddlCivilCost").val() > 0) {
                }
                else {
                    alert('Please select Description')
                    $("#ddlCivilCost").focus();
                    return false;
                }
                var X = $("#txtPlantCost").val();
                if (X == "") {
                    alert('Project Cost details can not be blank')
                    $("#txtPlantCost").focus();
                    return false;
                };
            });

            $("#BtnAddMoreFin").click(function (e) {
                debugger;
                if ($("#ddlFinDtls").val() > 0) {
                }
                else {
                    alert('Please select Financing Description')
                    $("#ddlFinDtls").focus();
                    return false;
                }
                var X = $("#txtFinAmnt").val();
                if (X == "") {
                    alert('Financing Amount details can not be blank')
                    $("#txtFinAmnt").focus();
                    return false;
                };

            });

            $('#ibtnAdd').click(function (e) {
                if (!BlankTextBox('txtFinDetail', ' Financing Details ')) { return false; }

                if ($("#txtFinDetail").val().length >= 6) {
                    var txt = $("[id*=txtFinDetail]");
                    var Directors = $(txt).val();
                    var lbFinDetail = $("[id*=lbFinDetail]");
                    var options = $('#lbFinDetail option');
                    var alreadyExist = false;

                    $(options).each(function () {
                        if ($(this).val() == Directors) {
                            alert("Financing Details Already Exists");
                            alreadyExist = true;
                            return false;
                        }
                    });
                    if (!alreadyExist) {
                        lbFinDetail.append($('<option></option>').attr('value', Directors).text(Directors));

                        option = "";
                        $('#lbFinDetail option').map(function () {
                            option = option + '~' + $(this).text();
                        });

                        $('#hdnFinDetail').val(option);
                    }
                    txt.val('');
                    e.preventDefault();
                }
                else {
                    alert('Sorry, Financing Details should be greater than equal to 6 character');
                    return false;
                }

            });
            $('#ibtnRemove').click(function () {
                if ($("#lbFinDetail option:selected").length == 0) {
                    alert('Select Financing Details To Remove !!!')
                    $('#lbFinDetail').focus();
                    return false;
                }
                else {
                    $('#lbFinDetail option:selected').remove();

                    option = "";
                    $('#lbFinDetail option').map(function () {
                        option = option + '~' + $(this).text();
                    });

                    $('#hdnFinDetail').val(option);

                    return false;
                }
            });
        });

        function pageLoad() {
            $(function () {

                $("[id*=grdAddMore] [id*=ButtonAdd]").click(function () {
                    var row = $(this).closest("tr");
                    var ddlDescription = row.find("[id*=DdlDescription]");
                    var txtCost = row.find("[id*=txtCost]");
                    var message = "";
                    if (ddlDescription.val() == "0" && ($.trim(txtCost.val()) != "")) {
                        message += "Please select Details Description.";
                        ddlDescription.focus();
                    }
                    if ($.trim(txtCost.val()) == "" && ddlDescription.val() != "0") {
                        message += "Please enter Cost.\n";
                        txtCost.focus();
                    }
                    if ($.trim(txtCost.val()) == "" && ddlDescription.val() == "0") {
                        message += "Please add Description and Cost details.\n";
                        ddlDescription.focus();
                    }
                    if (message != "") {
                        alert(message);
                        return false;
                    }
                    return true;
                });
            });

            $(function () {
                $("[id*=GrdSource] [id*=BtnAddSource]").click(function () {

                    var row = $(this).closest("tr");
                    var txtMaterial = row.find("[id*=txtMaterial]");
                    var txtSource = row.find("[id*=txtSource]");
                    var message = "";
                    if (($.trim(txtMaterial.val()) == "") && ($.trim(txtSource.val()) != "")) {
                        message += "Please enter Materials.";
                        txtMaterial.focus();
                    }
                    if ($.trim(txtSource.val()) == "" && ($.trim(txtMaterial.val()) != "")) {
                        message += "Please enter Source.\n";
                        txtSource.focus();
                    }
                    if (($.trim(txtMaterial.val()) == "") && ($.trim(txtSource.val()) == "")) {
                        message += "Please add Materials and Source.\n";
                        txtMaterial.focus();
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
            background: #ff8184;
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
            border-left-color: #ff8184;
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

                        //                        var x = document.getElementById('grdAddMore_ctl' + j + '_DdlDescription').value;
                        //                        var y = document.getElementById('grdAddMore_ctl' + j + '_txtCost').value;
                        var x = document.getElementById('grdAddMore_DdlDescription_' + i).value;
                        var y = document.getElementById('grdAddMore_txtCost_' + i).value;
                        if (x != "0") {
                            if (y == "") {
                                alert('Please add at least one Poject Cost Details.');
                                return false;
                            }
                            else {
                            }
                        }
                        else {
                            alert('Please add at least one Poject Cost Details.');
                            return false;
                        }
                    }
                }
                if (!blankFieldValidation('txtLand', ' Land')) { return false; }
                if (!WhiteSpaceValidation1st('txtLand', ' Land ')) { return false; }
                if (!blankFieldValidation('txtWater', ' Water ')) { return false; }
                if (!WhiteSpaceValidation1st('txtWater', ' Water ')) { return false; }
                if (!blankFieldValidation('txtPower', ' Power')) { return false; }
                if (!WhiteSpaceValidation1st('txtPower', ' Power ')) { return false; }
                //            if ($("#rbtCpp").is(":checked") == false && $("#rbtGrid").is(":checked") == false) {
                //                alert('Please Select Source');
                //                return false;
                //            }


                if (!blankFieldValidation('txtDirectEmployment', ' Direct Employment ')) { return false; }
                if (!WhiteSpaceValidation1st('txtDirectEmployment', ' Direct Employment ')) { return false; }
                if (!blankFieldValidation('txtContractual', ' Contractual Employment')) { return false; }
                if (!WhiteSpaceValidation1st('txtContractual', ' Contractual Employment ')) { return false; }

                if (!blankFieldValidation('txtMonths', ' Implementation Period ')) { return false; }
                if (!WhiteSpaceValidation1st('txtMonths', ' Implementation Period ')) { return false; }
                //            if (!ConfirmAction('btnSubmit', 'Are you sure want to Save in draft ?', 'Are you sure want to Update ?')) {
                //                return false;
                //            }
            }
        </script>
        <form id="form1" runat="server">
        <!--Header-->
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <ucheader:header ID="header1" runat="server" />
        <aside class="main-sidebar">
            <!-- sidebar -->
            <div class="sidebar">
                <!-- sidebar menu -->
                <ucLeftMenu:leftMenu ID="leftMenu1" runat="server" />
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
                                    <li class="past"><span>
                                        <h4>
                                            <a href="ProjectMasterAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-1</a></h4>
                                    </span><i></i></li>
                                    <li class="past"><span>
                                        <h4>
                                            <a href="ProposalMasterAdd.aspx?linkm=<%=Request["linkm"]%>&linkn=<%=Request["linkn"]%>&btn=<%=Request["btn"]%>&tab=<%=Request["tab"]%>&ID=<%=Request.QueryString["ID"]%>&PIndex=<%=string.IsNullOrEmpty(Request.QueryString["PIndex"])?"0":Request.QueryString["PIndex"]%>">
                                                Step-2</a></h4>
                                    </span><i></i></li>
                                    <li class="present"><span>
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
                                <div class="row">
                                    <label class="col-sm-2">
                                        Project Cost Details</label>
                                    <div class="col-sm-7">
                                        <span class="colon">:</span>
                                        <asp:UpdatePanel ID="UpdatePanelloc" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdAddMore" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                                                    CssClass="table table-bordered" ShowFooter="false" OnRowDataBound="grdAddMore_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details Description" HeaderStyle-Width="250px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="DdlDescription" runat="server" CssClass="form-control" TabIndex="1">
                                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cost (Rs in Crores)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCost" runat="server" MaxLength="100" CssClass="form-control"
                                                                    TabIndex="1" Text='<% #Eval("Cost") %>' Style="text-align: right" />
                                                                <cc1:FilteredTextBoxExtender ID="FEPlandCost" runat="server" FilterType="Numbers,Custom"
                                                                    ValidChars="." TargetControlID="txtCost">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/images/delete_btn.gif"
                                                                    ToolTip="Click To Delete !" OnClick="imgbtnDelete_Click" />
                                                                <asp:Button ID="ButtonAdd" runat="server" Text="Add More" CssClass="btn btn-success"
                                                                    OnClick="ButtonAdd_Click" TabIndex="1" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Land</label>
                                    <div class="col-sm-5">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtLand" runat="server" MaxLength="5000" TabIndex="2" TextMode="MultiLine"
                                            Rows="2" onkeyup="return TextCounter('txtLand','lblLand',5000)" ondrop="return false;"
                                            CssClass="form-control" />
                                        <cc1:FilteredTextBoxExtender ID="FELand" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                            TargetControlID="txtLand" InvalidChars="!%'*&" ValidChars=" .(),-/\{}[]<>">
                                        </cc1:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span> Maximum <span class="text-red">
                                            <asp:Label ID="lblLand" runat="server" Text="5000"></asp:Label>
                                        </span>&nbsp;characters
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Water</label>
                                    <div class="col-sm-5">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtWater" runat="server" MaxLength="5000" TabIndex="3" TextMode="MultiLine"
                                            Rows="2" onkeyup="return TextCounter('txtWater','lblWater',5000)" ondrop="return false;"
                                            CssClass="form-control" />
                                        <cc1:FilteredTextBoxExtender ID="FEWater" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                            TargetControlID="txtWater" InvalidChars="!%'*&" ValidChars=" .(),-/\{}[]<>">
                                        </cc1:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span> Maximum <span class="text-red">
                                            <asp:Label ID="lblWater" runat="server" Text="5000"></asp:Label>
                                        </span>&nbsp;characters
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Power</label>
                                    <div class="col-sm-5">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtPower" runat="server" MaxLength="5000" TabIndex="4" TextMode="MultiLine"
                                            onkeyup="return TextCounter('txtPower','lblPower',5000)" ondrop="return false;"
                                            CssClass="form-control" />
                                        <cc1:FilteredTextBoxExtender ID="FEPower" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                            TargetControlID="txtPower" InvalidChars="!%'*&" ValidChars=" .(),-/\{}[]<>">
                                        </cc1:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span> Maximum <span class="text-red">
                                            <asp:Label ID="lblPower" runat="server" Text="5000"></asp:Label>
                                        </span>&nbsp;characters
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Source</label>
                                    <div class="col-sm-5">
                                        <span class="colon">:</span>
                                        <div class="radiobtn pull-left">
                                            <label>
                                                <asp:CheckBox ID="rbtCpp" runat="server" Text="Captive Power Plant" />
                                            </label>
                                        </div>
                                        <div class="radiobtn pull-left">
                                            <label>
                                                <asp:CheckBox ID="rbtGrid" runat="server" Text="Grid" />
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-sm-2">
                                        Raw Materials Source</label>
                                    <div class="col-sm-7">
                                        <span class="colon">:</span>
                                        <asp:UpdatePanel ID="UpdSource" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GrdSource" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                                                    border="0" CssClass="table table-bordered" ShowFooter="false" OnRowDataBound="GrdSource_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="30px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Materials" HeaderStyle-Width="300px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMaterial" runat="server" MaxLength="100" CssClass="form-control"
                                                                    TabIndex="5" Text='<%# Eval("Materials") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="300px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Source">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSource" runat="server" MaxLength="2000" Width="200px" TabIndex="5"
                                                                    Text='<%# Eval("Source") %>' TextMode="MultiLine" CssClass="form-control" />
                                                                <cc1:FilteredTextBoxExtender ID="CESource" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                                                    TargetControlID="txtSource" InvalidChars="!<>%'*&" ValidChars=" .(),-/\">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDltSource" runat="server" ImageUrl="~/images/delete_btn.gif"
                                                                    ToolTip="Click To Delete !" OnClick="imgbtnDltSource_Click" />
                                                                <asp:Button ID="BtnAddSource" runat="server" Text="Add More" CssClass="btn btn-success"
                                                                    TabIndex="5" OnClick="BtnAddSource_Click" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="30px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Direct Employment</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtDirectEmployment" runat="server" MaxLength="5" TabIndex="6" CssClass="form-control" />
                                        <cc1:FilteredTextBoxExtender ID="FEDirectEmployment" runat="server" FilterType="Numbers"
                                            TargetControlID="txtDirectEmployment">
                                        </cc1:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span>
                                    </div>
                                    <label class="col-sm-2">
                                        Contractual Employment</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtContractual" runat="server" MaxLength="5" TabIndex="7" CssClass="form-control" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                            TargetControlID="txtContractual">
                                        </cc1:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">
                                        Implementation Period</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="txtMonths" runat="server" MaxLength="150" TabIndex="10" TextMode="MultiLine"
                                            Rows="3" onkeyup="return TextCounter('txtMonths','lblMonth',150)" ondrop="return false;"
                                            CssClass="form-control" />
                                        <cc1:FilteredTextBoxExtender ID="FEMonths" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom"
                                            TargetControlID="txtMonths" InvalidChars="!<>%'*&" ValidChars=".()-,&quot; ">
                                        </cc1:FilteredTextBoxExtender>
                                        <span class="mandetory">*</span> Maximum <span class="text-red">
                                            <asp:Label ID="lblMonth" runat="server" Text="150"></asp:Label>
                                        </span>&nbsp;characters
                                    </div>
                                </div>
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
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4 col-sm-offset-2">
                                        <asp:HiddenField ID="hdnRemarkID" runat="server" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Next" CssClass="btn btn-success"
                                            OnClientClick="return Validation();" OnClick="btnSubmit_Click" TabIndex="11" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click"
                                            TabIndex="12" /></div>
                                </div>
                            </div>
                        </div>
                    </div>
            </section>
        </div>
        <ucfooter:footer ID="footer1" runat="server" />
        <!-- Modal -->
        </form>
    </div>
</body>
