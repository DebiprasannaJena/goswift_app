<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="DynamicformBuilder.aspx.cs" Inherits="Formbuilder_DynamicformBuilder" MasterPageFile="~/MasterPage/Application.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #elements-container {
            text-align: center;
        }

        .delete {
            cursor: pointer;
        }
    </style>

    <script src="../js/jQuery.alert.js" type="text/javascript"></script>
    <link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" />
    <script src="../js/WebValidation.js" type="text/javascript"></script>
     

    <script type="text/javascript">
        var Ids = ""; var name = ""; var label = ""; var length = ""; var tooltip = ""; var textMode = ""; var cssCls = ""; var resdOnly = ""; var validation = ""; var maxsize = ""; var fileAllowed = ""; var dataTextField = "";
        var dataValueField = ""; var requiredfield = ""; var datasource = ""; var option = ""; var defaultval = ""; var plugnPag = ""; var hdngText = ""; var AutoMapping = "";
    </script>

    <script>
        $(document).ready(function () {
            $("#txt").click(function () {
                var strstatust = true;
                strstatust = clearAllVariable();
                if (strstatust == false) {
                    return false
                }
                else {
                    if ($("#hdftxt").val() == '') {
                        var txtid = 1;
                    }
                    else {
                        txtid = parseInt($("#hdftxt").val()) + 1
                    }
                    $("#hdftxt").val(txtid);
                    var txtbxId = 'txt_' + txtid;
                    var liid = txtbxId;
                    var chkids = "'l" + liid + "'";
                    var liData = "text";
                    var ctrl = '<input type="text" id="' + txtbxId + '" name="text" class="text form-control">';
                    var li = '<li id=' + chkids + ' data="" title=' + liData + ',' + txtbxId + ' >' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')" ><i class="fa fa-list-alt"></i></span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div></li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });

            $("#chk").click(function () {
                var strstatusc = true;
                strstatusc = clearAllVariable();
                if (strstatusc == false) {
                    return false
                }
                else {
                    if ($("#hdfcheck").val() == '') {
                        var chkid = 1;
                    }
                    else {
                        chkid = parseInt($("#hdfcheck").val()) + 1
                    }
                    $("#hdfcheck").val(chkid);
                    var ChechId = 'chk_' + chkid;
                    var liid = ChechId;
                    var chkids = "'l" + liid + "'";
                    var liData = "checkbox";
                    var ctrl = '<input type="checkbox" id="' + ChechId + '" name="' + ChechId + '" class="chk"><input type="checkbox" id="12" name="' + ChechId + '" class="chk">';
                    var li = '<li id=' + chkids + ' data="" title=' + liData + ',' + ChechId + ' >' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')"><i class="fa fa-list-alt"></i> </span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div> </li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });

            $("#rad").click(function () {
                var strstatusr = true;
                strstatusr = clearAllVariable();
                if (strstatusr == false) {
                    return false
                }
                else {

                    if ($("#hdnrad").val() == '') {
                        var radid = 1;
                    }
                    else {
                        radid = parseInt($("#hdnrad").val()) + 1
                    }
                    $("#hdnrad").val(radid);
                    var radiobtnId = 'rad_' + radid;
                    var liid = radiobtnId;
                    var chkids = "'l" + liid + "'";
                    var liData = "radio";
                    var ctrl = '<input type="radio" id="' + radiobtnId + '" name="text" class="rad">';
                    var li = '<li id=' + chkids + '  title=' + liData + ',' + radiobtnId + ' data="" >' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')"> <i class="fa fa-list-alt"></i></span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div></li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });

            $("#filUld").click(function () {
                var strstatusf = true;
                strstatusf = clearAllVariable();
                if (strstatusf == false) {
                    return false
                }
                else {
                    if ($("#hdnFile").val() == '') {
                        var flid = 1;
                    }
                    else {
                        flid = parseInt($("#hdnFile").val()) + 1
                    }
                    $("#hdnFile").val(flid);
                    var filId = 'fil_' + flid;
                    var liid = filId;
                    var chkids = "'l" + liid + "'";
                    var liData = "file";
                    var ctrl = '<input type="file" id="' + filId + '" name="text" class="rad form-control">';
                    var li = '<li id=' + chkids + ' title=' + liData + ',' + filId + ' data=""  value="' + filId + '">' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')"><i class="fa fa-list-alt"></i> </span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div> </li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });


            $("#SelectId").click(function () {
                var strstatuss = true;
                strstatuss = clearAllVariable();
                if (strstatuss == false) {
                    return false
                }
                else {
                    if ($("#hdnDrop").val() == '') {
                        var drop = 1;
                    }
                    else {
                        drop = parseInt($("#hdnDrop").val()) + 1
                    }
                    $("#hdnDrop").val(drop);
                    var drpId = 'sel_' + drop;
                    var liid = drpId;
                    var chkids = "'l" + liid + "'";
                    var liData = "select";
                    var ctrl = '<select  id="' + drpId + '" name="text" class="select form-control" > <option>option1</option></select>';
                    var li = '<li id=' + chkids + ' title=' + liData + ',' + drpId + ' data=""  value="' + drpId + '">' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')"><i class="fa fa-list-alt" ></i></span> <a class="delete"onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div></li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });

            $("#ListboxId").click(function () {
                var strstatusl = true;
                strstatusl = clearAllVariable();
                if (strstatusl == false) {
                    return false
                }
                else {
                    if ($("#hdnListBox").val() == '') {
                        var list = 1;
                    }
                    else {
                        list = parseInt($("#hdnListBox").val()) + 1
                    }
                    $("#hdnListBox").val(list);
                    var drpId = 'Lstbx_' + list;
                    var liid = drpId;
                    var chkids = "'l" + liid + "'";
                    var liData = "Listbox";
                    var ctrl = '<select  id="' + drpId + '" name="text" class="select form-control" multiple > <option>option1</option></select>';
                    var li = '<li id=' + chkids + ' title=' + liData + ',' + drpId + ' data=""  value="' + drpId + '">' + ctrl + '<span class="icon-sec height94"><span class="drag-area" onclick="clickFunc(' + chkids + ')"><i class="fa fa-list-alt"></i> </span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div> </li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });

            $("#PluginId").click(function () {
                var strstatusp = true;
                strstatusp = clearAllVariable();
                if (strstatusp == false) {
                    return false
                }
                else {
                    if ($("#hdnPlugin").val() == '') {
                        var list = 1;
                    }
                    else {
                        list = parseInt($("#hdnPlugin").val()) + 1
                    }
                    $("#hdnPlugin").val(list);
                    var drpId = 'Plugn_' + list;
                    var liid = drpId;
                    var chkids = "'l" + liid + "'";
                    var liData = "Plugin";
                    var ctrl = '<div id="' + drpId + '" class="form-control">Plugin</div>';
                    var li = '<li id=' + chkids + ' title=' + liData + ',' + drpId + ' data=""  value="' + drpId + '">' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')"><i class="fa fa-list-alt"></i></span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div>  </li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });

            $("#hdngId").click(function () {
                var strstatush = true;
                strstatush = clearAllVariable();
                if (strstatush == false) {
                    return false
                }
                else {
                    if ($("#hdnHeading").val() == '') {
                        var list = 1;
                    }
                    else {
                        list = parseInt($("#hdnHeading").val()) + 1
                    }
                    $("#hdnHeading").val(list);
                    var drpId = 'hdn_' + list;
                    var liid = drpId;
                    var chkids = "'l" + liid + "'";
                    var liData = "Heading";
                    // var ctrl = '<input type="text" id="' + drpId + '" Text="<h1>Heading</h1>" name="text" class="text">'; //<h1 id="' + drpId + '">Heading</h1>';'<input type="text" id="' + txtbxId + '" name="text" class="text">';
                    var ctrl = '<h1 id="' + drpId + '" class="form-control">Heading</h1>';
                    //var ctrl = '<input type="text" id="' + drpId + '"  name="text" class="text"><h1>Heading</h1></input>';
                    var li = '<li id=' + chkids + ' title=' + liData + ',' + drpId + ' data=""  value="' + drpId + '">' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')"><i class="fa fa-list-alt"></i> </span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div>  </li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });


            $("#labelId").click(function () {
                var strstatush = true;
                strstatush = clearAllVariable();
                if (strstatush == false) {
                    return false
                }
                else {
                    if ($("#hdnLable").val() == '') {
                        var list = 1;
                    }
                    else {
                        list = parseInt($("#hdnLable").val()) + 1
                    }
                    $("#hdnLable").val(list);
                    var drpId = 'LblLbl_' + list;
                    var liid = drpId;
                    var chkids = "'l" + liid + "'";
                    var liData = "Label";
                    // var ctrl = '<input type="text" id="' + drpId + '" Text="<h1>Heading</h1>" name="text" class="text">'; //<h1 id="' + drpId + '">Heading</h1>';'<input type="text" id="' + txtbxId + '" name="text" class="text">';
                    var ctrl = '<h1 id="' + drpId + '" class="form-control">Label</h1>';
                    //var ctrl = '<input type="text" id="' + drpId + '"  name="text" class="text"><h1>Heading</h1></input>';
                    var li = '<li id=' + chkids + ' title=' + liData + ',' + drpId + ' data=""  value="' + drpId + '">' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')"><i class="fa fa-list-alt"></i> </span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div>  </li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });

            $("#CenterId").click(function () {
                var strstatush = true;
                strstatush = clearAllVariable();
                if (strstatush == false) {
                    return false
                }
                else {
                    if ($("#hdnCenter").val() == '') {
                        var list = 1;
                    }
                    else {
                        list = parseInt($("#hdnCenter").val()) + 1
                    }
                    $("#hdnCenter").val(list);
                    var drpId = 'LblCe_' + list;
                    var liid = drpId;
                    var chkids = "'l" + liid + "'";
                    var liData = "Center";
                    // var ctrl = '<input type="text" id="' + drpId + '" Text="<h1>Heading</h1>" name="text" class="text">'; //<h1 id="' + drpId + '">Heading</h1>';'<input type="text" id="' + txtbxId + '" name="text" class="text">';
                    var ctrl = '<h1 id="' + drpId + '" class="form-control">Center</h1>';
                    //var ctrl = '<input type="text" id="' + drpId + '"  name="text" class="text"><h1>Heading</h1></input>';
                    var li = '<li id=' + chkids + ' title=' + liData + ',' + drpId + ' data=""  value="' + drpId + '">' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')"><i class="fa fa-list-alt"></i> </span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div>  </li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });

            $("#DeclarationCheckId").click(function () {
                var strstatusc = true;
                strstatusc = clearAllVariable();
                if (strstatusc == false) {
                    return false
                }
                else {
                    if ($("#hdnDeclaration").val() == '') {
                        var chkid = 1;
                    }
                    else {
                        chkid = parseInt($("#hdnDeclaration").val()) + 1
                    }
                    $("#hdnDeclaration").val(chkid);
                    var ChechId = 'Dchk_' + chkid;
                    var liid = ChechId;
                    var chkids = "'l" + liid + "'";
                    var liData = "Declaration";
                    var ctrl = '<input type="checkbox" id="' + ChechId + '" name="' + ChechId + '" class="chk">';
                    var li = '<li id=' + chkids + ' data="" title=' + liData + ',' + ChechId + ' >' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')"><i class="fa fa-list-alt"></i> </span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div> </li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnDeclaration').val(liData);
                    return true;
                }
            });

            $("#SameAsCheckId").click(function () {
                var strstatusc = true;
                strstatusc = clearAllVariable();
                if (strstatusc == false) {
                    return false
                }
                else {
                    if ($("#hdnSameAsCheck").val() == '') {
                        var chkid = 1;
                    }
                    else {
                        chkid = parseInt($("#hdnSameAsCheck").val()) + 1
                    }
                    $("#hdnSameAsCheck").val(chkid);
                    var ChechId = 'Smchk_' + chkid;
                    var liid = ChechId;
                    var chkids = "'l" + liid + "'";
                    var liData = "SameAs";
                    var ctrl = '<input type="checkbox" id="' + ChechId + '" name="' + ChechId + '" class="chk">';
                    var li = '<li id=' + chkids + ' data="" title=' + liData + ',' + ChechId + ' >' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')"><i class="fa fa-list-alt"></i> </span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div> </li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnSameAsCheck').val(liData);
                    return true;
                }
            });

            $("#DatetimeId").click(function () {
                var strstatust = true;
                strstatust = clearAllVariable();
                if (strstatust == false) {
                    return false
                }
                else {
                    if ($("#hdnDatetime").val() == '') {
                        var dtid = 1;
                    }
                    else {
                        dtid = parseInt($("#hdnDatetime").val()) + 1
                    }
                    $("#hdnDatetime").val(dtid);
                    var txtbxId = 'Date_' + dtid;
                    var liid = txtbxId;
                    var chkids = "'l" + liid + "'";
                    var liData = "DateTime";
                    var ctrl = '<input type="text" id="' + txtbxId + '" name="text" class="text form-control">';
                    var li = '<li id=' + chkids + ' data="" title=' + liData + ',' + txtbxId + ' >' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')" ><i class="fa fa-list-alt"></i></span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div></li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });


            $("#FromToDtId").click(function () {
                var strstatust = true;
                strstatust = clearAllVariable();
                if (strstatust == false) {
                    return false
                }
                else {
                    if ($("#hdnFrmToDt").val() == '') {
                        var dtid = 1;
                    }
                    else {
                        dtid = parseInt($("#hdnFrmToDt").val()) + 1
                    }
                    $("#hdnFrmToDt").val(dtid);
                    var txtbxId = 'FrmToDt_' + dtid;
                    var liid = txtbxId;
                    var chkids = "'l" + liid + "'";
                    var liData = "FromToDate";
                    var ctrl = '<input type="text" id="' + txtbxId + '" name="text" class="text form-control">';
                    var li = '<li id=' + chkids + ' data="" title=' + liData + ',' + txtbxId + ' >' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')" ><i class="fa fa-list-alt"></i></span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div></li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnValidation').val(liData);
                    return true;
                }
            });

            $("#FullNameId").click(function () {
                var strstatust = true;
                strstatust = clearAllVariable();
                if (strstatust == false) {
                    return false
                }
                else {
                    if ($("#hdnFullName").val() == '') {
                        var txtid = 1;
                    }
                    else {
                        txtid = parseInt($("#hdnFullName").val()) + 1
                    }
                    $("#hdnFullName").val(txtid);
                    var txtbxId = 'ful_' + txtid;
                    var liid = txtbxId;
                    var chkids = "'l" + liid + "'";
                    var liData = "FullName";
                    var ctrl = '<input type="text" id="' + txtbxId + '" name="text" class="text form-control">';
                    var li = '<li id=' + chkids + ' data="" title=' + liData + ',' + txtbxId + ' >' + ctrl + '<span class="icon-sec"><span class="drag-area" onclick="clickFunc(' + chkids + ')" ><i class="fa fa-list-alt"></i></span> <a class="delete" onclick="DeleteFunc(' + chkids + ')"><i class="fa fa-trash-o"></i></a><div class="clearfix"></div></li>';
                    $("#listControl").append(li);
                    testDrag();
                    $('#hdnFullName').val(liData);
                    return true;
                }
            });

        });
        $(".delete").click(function () {
            // alert('hii');
        })
    </script>

 
       <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
  

    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-file-text-o"></i>
            </div>
            <div class="header-title">
                <h1>Form Builder</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Services</a></li>
                    <li><a>Form Builder</a></li>
                </ul>
            </div>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidrag">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="Addservicemaster.aspx">
                                    <i class="fa fa-plus"></i>Add List </a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="demoitems.aspx">
                                    <i class="fa fa-file"></i>View List </a>
                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">Service Name</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="ddlServiceId" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <span class="mandetory">*</span>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">Form Logo</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:FileUpload ID="fulLogo" CssClass="form-control" runat="server" />
                                        <span class="mandetory">*</span>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">Form Header</label>
                                    <div class="col-sm-8">
                                        <span class="colon">:</span>
                                        <cc1:Editor ID="editHeader" runat="server" Width="100%" />
                                    </div>
                                </div>
                            </div>

                            <div>

                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">Form Footer</label>
                                        <div class="col-sm-8">
                                            <span class="colon">:</span>
                                            <cc1:Editor ID="editFooter" runat="server" Width="100%" NoScript="True" />
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">Allignment</label>
                                        <div class="col-sm-8 chkgp ">
                                            <span class="colon">:</span>
                                            <asp:RadioButtonList ID="rdbAllignment" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow" CssClass="rdBtn">
                                                <asp:ListItem Value="1" Text="OneLine" Selected="True">One Column <img src="../images/layout1.jpg" alt="Single line layout" /></asp:ListItem>
                                                <asp:ListItem Value="2" Text="TwoLine">Two Column <img src="../images/layout2.jpg"  alt="Two line layout" /></asp:ListItem>
                                                <asp:ListItem Value="2" Text="3rdLine">Three Column <img src="../images/layout3.jpg"  alt="Three line layout" /></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>


                                    </div>
                                </div>

                                <div class="form-group dynamicform-sec">
                                    <div class="row">
                                        <div class="col-sm-4 col-md-3 paddingright0">
                                            <div class="dycontrol-sec">
                                                <div class="header-sec">
                                                    <h3>Controls Section</h3>
                                                </div>
                                                <div class="body-sec">
                                                    <a class="button" id="txt" style="">
                                                        <img src="../images/tool-tip.png" />Add TextBox</a>
                                                    <a class="button" id="chk" style="">
                                                        <img src="../images/check-box.png" />Add CheckBox</a>
                                                    <a class="button" id="rad" style="">
                                                        <img src="../images/radio-on-button.png" />Add RadioButton</a>
                                                    <a class="button" id="filUld" style="">
                                                        <img src="../images/upload-folder.png" />Add File Upload</a>
                                                    <a class="button" id="SelectId" style="">
                                                        <img src="../images/drop-down-list.png" />Add Drop Dwon</a>
                                                    <a class="button" id="ListboxId" style="">
                                                        <img src="../images/list.png" />Add ListBox</a>
                                                    <a class="button" id="PluginId" style="">
                                                        <img src="../images/puzzle-piece-plugin.png" />Add Matrix</a>
                                                    <a class="button" id="hdngId" style="">
                                                        <img src="../images/capitals.png" />Add Heading</a>
                                                    <a class="button" id="labelId" style="">
                                                        <img src="../images/capitals.png" />Add Label</a>
                                                    <a class="button" id="CenterId" style="">
                                                        <img src="../images/capitals.png" />Add Center</a>
                                                    <a class="button" id="DeclarationCheckId" style="">
                                                        <img src="../images/capitals.png" />Add DeclarationCheckBox</a>
                                                    <a class="button" id="DatetimeId" style="">
                                                        <img src="../images/capitals.png" />Add DateTime</a>
                                                    <a class="button" id="FromToDtId" style="">
                                                        <img src="../images/capitals.png" />Add From&To Date</a>
                                                    <a class="button" id="SameAsCheckId" style="">
                                                        <img src="../images/capitals.png" />Add SameAs</a>
                                                    <a class="button" id="FullNameId" style="">
                                                        <img src="../images/capitals.png" />Add FullName</a>

                                                    <input type="hidden" id="hdnValidation" />                                                  
                                                    <input type="hidden" id="hdftxt" />
                                                    <input type="hidden" id="hdfcheck" />
                                                    <input type="hidden" id="hdnrad" />
                                                    <input type="hidden" id="hdnFile" />
                                                    <input type="hidden" id="hdnDrop" />
                                                    <input type="hidden" id="hdnListBox" />
                                                    <input type="hidden" id="hdnPlugin" />
                                                    <input type="hidden" id="hdnHeading" />
                                                    <input type="hidden" id="hdnLable" />
                                                    <input type="hidden" id="hdnCenter" />
                                                    <input type="hidden" id="hdnDeclaration" />
                                                    <input type="hidden" id="hdnDatetime" />
                                                    <input type="hidden" id="hdnFrmToDt" />
                                                    <input type="hidden" id="hdnSameAsCheck" />
                                                    <input type="hidden" id="hdnFullName" />
                                                    <input type="hidden" id="hdnIntialValue" runat="server" />
                                                    <input type="hidden" id="hdnLiDataValues" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-8 col-md-6 padding0">
                                            <div class="controls-list-sec">
                                                <div class="header-sec">
                                                    <h3>Form Section</h3>
                                                </div>
                                                <div class="body-sec">
                                                    <div id="dynamicControl">
                                                        <div class="popupcls1">
                                                            <ul id="listControl" class="drag-list">
                                                            </ul>
                                                        </div>
                                                        <div id="divRadioId">
                                                        </div>
                                                       
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 col-md-3 paddingleft0">
                                            <div class="properties-sec">
                                                <div class="header-sec">
                                                    <h3>Properties</h3>
                                                </div>
                                                <div class="body-sec">

                                                    <div id="divAllLabelId" class="property-cobtrols">
                                                        <div class="label-cls">Label</div>
                                                        <div class="control-cls">
                                                            <input type="text" id="txtLabel" class="form-control" onblur="EachControlOnblur(this);" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divAllNameId" class="property-cobtrols">
                                                        <div class="label-cls">Name</div>
                                                        <div class="control-cls">
                                                            <input type="text" id="txtAllName" class="form-control" onblur="EachControlOnblur(this);" onkeyup="nospaces(this)" maxlength="50" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divAllId" class="property-cobtrols">
                                                        <div class="label-cls">ID</div>
                                                        <div class="control-cls">
                                                            <input type="text" id="txtAllId" class="form-control" onblur="EachControlOnblur(this);" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divAllLngth" class="property-cobtrols">
                                                        <div class="label-cls">Length</div>
                                                        <div class="control-cls">
                                                            <input type="text" id="txtAllLength" class="form-control" onkeypress="return isNumber(event);" onblur="EachControlOnblur(this);" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divAllVal" class="property-cobtrols">
                                                        <div class="label-cls">Validation</div>
                                                        <div class="control-cls">

                                                            <select onchange="EachControlOnblur(this);" class="form-control" id="selVal">
                                                                <option value="0">Select</option>
                                                                <option value="Email">Email</option>
                                                                <option value="Character">Character</option>
                                                                <option value="Number">Number</option>
                                                                <option value="PhoneNumber">PhoneNumber</option>
                                                            </select>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divRequ" class="property-cobtrols">
                                                        <div class="label-cls">Required</div>
                                                        <div class="control-cls">
                                                            <select onchange="EachControlOnblur(this);" class="form-control" id="SelRequ">
                                                                <option value="0">False</option>
                                                                <option value="1">True</option>
                                                            </select>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divToolTip" class="property-cobtrols">
                                                        <div class="label-cls">ToolTip</div>
                                                        <div class="control-cls">
                                                            <input type="text" id="txtAllTooltip" class="form-control" onblur="EachControlOnblur(this);" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>

                                                    <div id="divMapping" class="property-cobtrols">
                                                        <div class="label-cls">AutoMapping</div>
                                                        <div class="control-cls">

                                                            <select onchange="EachControlOnblur(this);" class="form-control" id="selMapping">
                                                                <option value="0">No</option>
                                                                <option value="1">Yes</option>

                                                            </select>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divTextMode" class="property-cobtrols">
                                                        <div class="label-cls">Text Mode</div>
                                                        <div class="control-cls">
                                                            <select onchange="EachControlOnblur(this);" class="form-control" id="selTextMode">
                                                                <option value="0">Select</option>
                                                                <option value="SingleLine">SingleLine</option>
                                                                <option value="MultiLine">MultiLine</option>
                                                                <option value="Password">Password</option>
                                                                <option value="DateTime">DateTime</option>
                                                            </select>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divCssClass" class="property-cobtrols">
                                                        <div class="label-cls">Css Class</div>
                                                        <div class="control-cls">
                                                            <input type="text" id="txtCssCls" class="form-control" onblur="EachControlOnblur(this);" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divDataSource" class="property-cobtrols">
                                                        <div class="label-cls">DataSource TableName</div>
                                                        <div class="control-cls">
                                                            <select onchange="EachControlOnblur(this);" class="form-control" id="selDtSource">
                                                                <option value="0">Select</option>
                                                                <option value="T_GMS_CATEGORY">T_GMS_CATEGORY</option>
                                                                <option value="T_GMS_SUBCATEGORY">T_GMS_SUBCATEGORY</option>
                                                            </select>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divDataValuFld" class="property-cobtrols">
                                                        <div class="label-cls">DataValueField</div>
                                                        <div class="control-cls">
                                                            <select onchange="EachControlOnblur(this);" class="form-control" id="selDtValue">
                                                                <option value="0">Select</option>                                                              
                                                            </select>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divDataTextFld" class="property-cobtrols">
                                                        <div class="label-cls">DataTextField</div>
                                                        <div class="control-cls">
                                                            <select onchange="EachControlOnblur(this);" class="form-control" id="selDtTextFld">
                                                                <option value="0">Select</option>                                                               
                                                            </select>
                                                            <div class="clearfix"></div>
                                                        </div>
                                                    </div>
                                                    <div id="divFileAllowed" class="property-cobtrols">
                                                        <div class="label-cls">Type of FileAllowed</div>
                                                        <div class="control-cls">
                                                            <input type="text" id="txtFileAllow" class="form-control" onblur="EachControlOnblur(this);" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divMaxSize" class="property-cobtrols">
                                                        <div class="label-cls">Max Size</div>
                                                        <div class="control-cls">
                                                            <input type="text" id="txtMaxSz" class="form-control" onkeypress="return isNumber(event);" onblur="EachControlOnblur(this);" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divAllOption" class="property-cobtrols">
                                                        <div class="label-cls">Option</div>
                                                        <div class="control-cls">
                                                            <input type="text" id="txtAllOption" class="form-control" onblur="EachControlOnblur(this);" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divDefault" class="property-cobtrols">
                                                        <div class="label-cls">Default Value</div>
                                                        <div class="control-cls">
                                                            <input type="text" id="txtAllDefault" class="form-control" onblur="EachControlOnblur(this);" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>

                                                    <div id="divText" class="property-cobtrols">
                                                        <div class="label-cls">Text</div>
                                                        <div class="control-cls">

                                                            <input type="text" id="txtHdngText" class="form-control" onblur="EachControlOnblur(this);" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>


                                                    <div id="divUnqNmId" class="property-cobtrols">
                                                        <div class="label-cls">UniqueName</div>
                                                        <div class="control-cls">

                                                            <input type="text" id="txtUnqNm" class="form-control" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divTypeId" class="property-cobtrols">
                                                        <div class="label-cls">Type</div>
                                                        <div class="control-cls">

                                                            <input type="text" id="txtType" class="form-control" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divDisplayId" class="property-cobtrols">
                                                        <div class="label-cls">Display</div>
                                                        <div class="control-cls">

                                                            <input type="text" id="txtDisplay" class="form-control" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="divControlOptionId" class="property-cobtrols">
                                                        <div class="label-cls">ControlOption</div>
                                                        <div class="control-cls">

                                                            <input type="text" id="txtControlOption" class="form-control" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>


                                                    <div id="divPlugin" class="property-cobtrols">
                                                        <div class="label-cls">Plugin Option</div>
                                                        <div class="control-cls">
                                                            <select onchange="EachControlOnblur(this);" class="form-control" id="selPlugin">
                                                                <option value="0">Select</option>
                                                                <option value="1">Set AddMoreOption</option>
                                                                <option value="2">Static Value</option>
                                                                <option value="3">Vertical Add More</option>                                                              
                                                            </select>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>

                                                    <div id="divHeaderList" class="property-cobtrols">
                                                        <div class="label-cls">HeaderList</div>
                                                        <div class="control-cls">

                                                            <input type="text" id="txtHaederList" class="form-control" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>

                                                    <div id="divColmList" class="property-cobtrols">
                                                        <div class="label-cls">ColumnList</div>
                                                        <div class="control-cls">

                                                            <input type="text" id="txtColumnList" class="form-control" />
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>

                                                </div>

                                            </div>

                                        </div>

                                    </div>
                                </div>


                            </div>
                            <br />
                            <div style="display: inline" class="nav">
                            </div>

                            <asp:Button ID="Button1" runat="server" Text="Save"
                                OnClientClick="return lIEachData();" CssClass="btn btn-success"
                                OnClick="Button1_Click" />

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

    

    <script language="javascript" type="text/javascript">
        pageHeader = "Escalation"
        strFirstLink = "Config"
        strLastLink = "Escalation"
        printMe = "no"
        $(document).ready(function () {
            FillTable();           
        });


    </script>  
    
    <script src="../js/drag-arrange.js" type="text/javascript"></script>

    <script type="text/javascript">
        function testDrag() {
            $(function () {
                $('.draggable-element').arrangeable();
                $('li').arrangeable({ dragSelector: '.drag-area' });

            });
        }

        $(function () {
            $('.draggable-element').arrangeable();
            $('li').arrangeable({ dragSelector: '.drag-area' });



        });
    </script>

    <script type="text/javascript">
        var tag = "";

        function clickFunc(id) {
           
            ClearAllControl();
            var strType = $('#' + id).attr('title');
            var strdata = $('#' + id).attr('data');
            var array = strType.split(',');//controltype,controlid
            var type = array[0];
            var controlid = array[1];
            //tag = $('#' + array[1]).prop('type');
            tag = type;
            $('#selDtValue').children().remove();
            $('#selDtTextFld').children().remove();
            $("#ContentPlaceHolder1_hdnIntialValue").val(id + ',' + controlid);
            if (type == "text") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').show();
                $('#divAllLabelId').show();
                $('#divAllId').show();
                $('#divAllLngth').show();
                $('#divAllVal').show();
                $('#divRequ').show();
                $('#divReadOnly').show();
                $('#divToolTip').show();
                $('#divTextMode').show();
                $('#divCssClass').show();
                $('#divDataValuFld').hide();
                $('#divDataTextFld').hide();
                $('#divDataSource').hide();
                $('#divFileAllowed').hide();
                $('#divMaxSize').hide();
                $('#divAllOption').hide();
                $('#divDefault').show();
                $('#divPlugin').hide();
                $('#divText').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();
                $('#divMapping').show();

            }
            else if (type == "FullName") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').show();
                $('#divAllLabelId').show();
                $('#divAllId').show();
                $('#divAllLngth').show();
                $('#divAllVal').show();
                $('#divRequ').show();
                $('#divReadOnly').show();
                $('#divToolTip').show();
                $('#divTextMode').show();
                $('#divCssClass').show();
                $('#divDataValuFld').hide();
                $('#divDataTextFld').hide();
                $('#divDataSource').hide();
                $('#divFileAllowed').hide();
                $('#divMaxSize').hide();
                $('#divAllOption').hide();
                $('#divDefault').show();
                $('#divPlugin').hide();
                $('#divText').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();
                $('#divMapping').show();

            }
            else if (type == "file") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').show();
                $('#divAllLabelId').show();
                $('#divAllId').show();
                $('#divAllLngth').hide();
                $('#divAllVal').hide();
                $('#divRequ').show();
                $('#divReadOnly').hide();
                $('#divToolTip').show();
                $('#divTextMode').hide();
                $('#divCssClass').show();
                $('#divDataValuFld').hide();
                $('#divDataTextFld').hide();
                $('#divDataSource').hide();
                $('#divFileAllowed').show();
                $('#divMaxSize').show();
                $('#divAllOption').hide();
                $('#divDefault').hide();
                $('#divPlugin').hide();
                $('#divText').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();

            }
            else if (type == "checkbox") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').show();
                $('#divAllLabelId').show();
                $('#divAllId').show();
                $('#divAllLngth').hide();
                $('#divAllVal').hide();
                $('#divRequ').show();
                $('#divReadOnly').hide();
                $('#divToolTip').show();
                $('#divTextMode').hide();
                $('#divCssClass').show();
                $('#divDataValuFld').show();
                $('#divDataTextFld').show();
                $('#divDataSource').show();
                $('#divFileAllowed').hide();
                $('#divMaxSize').show();
                $('#divAllOption').show();
                $('#divDefault').show();
                $('#divPlugin').hide();
                $('#divText').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();

            }
            else if (type == "select") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').show();
                $('#divAllLabelId').show();
                $('#divAllId').show();
                $('#divAllLngth').hide();
                $('#divAllVal').hide();
                $('#divRequ').show();
                $('#divReadOnly').hide();
                $('#divToolTip').show();
                $('#divTextMode').hide();
                $('#divCssClass').show();
                $('#divDataValuFld').show();
                $('#divDataTextFld').show();
                $('#divDataSource').show();
                $('#divFileAllowed').hide();
                $('#divMaxSize').show();
                $('#divAllOption').show();
                $('#divDefault').show();
                $('#divPlugin').hide();
                $('#divText').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();


            }
            else if (type == "radio") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').show();
                $('#divAllLabelId').show();
                $('#divAllId').show();
                $('#divAllLngth').hide();
                $('#divAllVal').hide();
                $('#divRequ').show();
                $('#divReadOnly').hide();
                $('#divToolTip').show();
                $('#divTextMode').hide();
                $('#divCssClass').show();
                $('#divDataValuFld').show();
                $('#divDataTextFld').show();
                $('#divDataSource').show();
                $('#divFileAllowed').hide();
                $('#divMaxSize').show();
                $('#divAllOption').show();
                $('#divDefault').show();
                $('#divPlugin').hide();
                $('#divText').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();

            }
            else if (type == "Listbox") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').show();
                $('#divAllLabelId').show();
                $('#divAllId').show();
                $('#divAllLngth').hide();
                $('#divAllVal').hide();
                $('#divRequ').show();
                $('#divReadOnly').hide();
                $('#divToolTip').show();
                $('#divTextMode').hide();
                $('#divCssClass').show();
                $('#divDataValuFld').show();
                $('#divDataTextFld').show();
                $('#divDataSource').show();
                $('#divFileAllowed').hide();
                $('#divMaxSize').show();
                $('#divAllOption').show();
                $('#divDefault').show();
                $('#divMultiSelected').show();
                $('#divPlugin').hide();
                $('#divText').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();
            }
            else if (type == "Plugin") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').show();
                $('#divAllLabelId').show();
                $('#divAllId').show();
                $('#divAllLngth').hide();
                $('#divAllVal').hide();
                $('#divRequ').show();
                $('#divReadOnly').hide();
                $('#divToolTip').hide();
                $('#divTextMode').hide();
                $('#divCssClass').hide();
                $('#divDataValuFld').hide();
                $('#divDataTextFld').hide();
                $('#divDataSource').show();
                $('#divFileAllowed').hide();
                $('#divMaxSize').hide();
                $('#divAllOption').hide();
                $('#divDefault').hide();
                $('#divPlugin').show();
                $('#divText').hide();
                $('#divUnqNmId').show();
                $('#divTypeId').show();
                $('#divDisplayId').show();
                $('#divControlOptionId').show();
                $('#divHeaderList').show();
                $('#divColmList').show();

            }
            else if (type == "Heading") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').hide();
                $('#divAllLabelId').hide();
                $('#divAllId').hide();
                $('#divAllLngth').hide();
                $('#divAllVal').hide();
                $('#divRequ').hide();
                $('#divReadOnly').hide();
                $('#divToolTip').hide();
                $('#divTextMode').hide();
                $('#divCssClass').hide();
                $('#divDataValuFld').hide();
                $('#divDataTextFld').hide();
                $('#divDataSource').hide();
                $('#divFileAllowed').hide();
                $('#divMaxSize').hide();
                $('#divAllOption').hide();
                $('#divDefault').hide();
                $('#divPlugin').hide();
                $('#divText').show();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();

            }

            else if (type == "Label") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').hide();
                $('#divAllLabelId').hide();
                $('#divAllId').hide();
                $('#divAllLngth').hide();
                $('#divAllVal').hide();
                $('#divRequ').hide();
                $('#divReadOnly').hide();
                $('#divToolTip').hide();
                $('#divTextMode').hide();
                $('#divCssClass').hide();
                $('#divDataValuFld').hide();
                $('#divDataTextFld').hide();
                $('#divDataSource').hide();
                $('#divFileAllowed').hide();
                $('#divMaxSize').hide();
                $('#divAllOption').hide();
                $('#divDefault').hide();
                $('#divPlugin').hide();
                $('#divText').show();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();

            }
            else if (type == "Center") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').hide();
                $('#divAllLabelId').hide();
                $('#divAllId').hide();
                $('#divAllLngth').hide();
                $('#divAllVal').hide();
                $('#divRequ').hide();
                $('#divReadOnly').hide();
                $('#divToolTip').hide();
                $('#divTextMode').hide();
                $('#divCssClass').hide();
                $('#divDataValuFld').hide();
                $('#divDataTextFld').hide();
                $('#divDataSource').hide();
                $('#divFileAllowed').hide();
                $('#divMaxSize').hide();
                $('#divAllOption').hide();
                $('#divDefault').hide();
                $('#divPlugin').hide();
                $('#divText').show();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();

            }

            else if (type == "Declaration") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').show();
                $('#divAllLabelId').show();
                $('#divAllId').show();
                $('#divAllLngth').hide();
                $('#divAllVal').hide();
                $('#divRequ').show();
                $('#divReadOnly').hide();
                $('#divToolTip').show();
                $('#divTextMode').hide();
                $('#divCssClass').show();
                $('#divDataValuFld').hide();
                $('#divDataTextFld').hide();
                $('#divDataSource').hide();
                $('#divFileAllowed').hide();
                $('#divMaxSize').show();
                $('#divAllOption').hide();
                $('#divDefault').show();
                $('#divPlugin').hide();
                $('#divText').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();

            }

            else if (type == "DateTime") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').show();
                $('#divAllLabelId').show();
                $('#divAllId').show();
                $('#divAllLngth').show();
                $('#divAllVal').show();
                $('#divRequ').show();
                $('#divReadOnly').show();
                $('#divToolTip').show();
                $('#divTextMode').show();
                $('#divCssClass').show();
                $('#divDataValuFld').hide();
                $('#divDataTextFld').hide();
                $('#divDataSource').hide();
                $('#divFileAllowed').hide();
                $('#divMaxSize').hide();
                $('#divAllOption').hide();
                $('#divDefault').show();
                $('#divPlugin').hide();
                $('#divText').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();
            }
            else if (type == "FromToDate") {
                $('#txtAllId').val(controlid);
                $('#txtAllId').prop('readonly', true);
                $('#divAllNameId').show();
                $('#divAllLabelId').show();
                $('#divAllId').show();
                $('#divAllLngth').show();
                $('#divAllVal').show();
                $('#divRequ').show();
                $('#divReadOnly').show();
                $('#divToolTip').show();
                $('#divTextMode').show();
                $('#divCssClass').show();
                $('#divDataValuFld').hide();
                $('#divDataTextFld').hide();
                $('#divDataSource').hide();
                $('#divFileAllowed').hide();
                $('#divMaxSize').hide();
                $('#divAllOption').hide();
                $('#divDefault').show();
                $('#divPlugin').hide();
                $('#divText').hide();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                $('#divHeaderList').hide();
                $('#divColmList').hide();

            }
            EditFunc(strdata);
        }

        function DeleteFunc(id) {

            $('#' + id).remove();
        }
    </script>

    <script type="text/javascript">       

        function EditFunc(strdata) {
           
            if (strdata != "") {
                var array = strdata.split('+');
                $('#txtAllName').val(array[1]);
                $('#txtLabel').val(array[2]);
                $('#txtAllLength').val(array[3]);
                $('#SelRequ').val(array[13]);
                $('#txtAllTooltip').val(array[4]);
                $('#selVal').val(array[8]);
                $('#selTextMode').val(array[5]);
                $('#txtCssCls').val(array[6]);
                $('#txtFileAllow').val(array[10]);
                $('#txtMaxSz').val(array[9]);
                $('#txtAllOption').val(array[15]);
                if (array[16] != "") {
                    $('#txtAllDefault').val(array[16]);
                    var ide = $("input[value=" + array[16] + "]").attr("id");
                    $('#' + ide).prop("checked", true);
                }
                if (array[14] != "") {
                    $('#selDtSource').val(array[14]);
                    FillColumnName(array[14]);
                }
                else {
                    $('#selDtSource').val(0);
                }
                if (array[12] != "") {

                    $('#selDtValue').val(array[12]);
                    $('#selDtTextFld').val(array[11]);
                }
                $('#selPlugin').val(array[17]);
                $('#txtHdngText').val(array[18]);
                $('#selMapping').val(array[19]);
                //assigned value to the variable
                Ids = array[0]; name = array[1]; label = array[2]; length = array[3]; tooltip = array[4]; textMode = array[5]; cssCls = array[6]; resdOnly = ""; validation = array[8]; maxsize = array[9]; fileAllowed = array[10]; dataTextField = array[11];
                dataValueField = array[12]; requiredfield = array[13]; datasource = array[14]; option = array[15]; defaultval = array[16]; plugnPag = array[17]; hdngText = array[18]; AutoMapping = array[19];

            }
        }

    </script>

    <script type="text/javascript">


        function EachControlOnblur(elem) {
            
            var onBlrStatus = true;
            var strRadio = "";
            var liIds = $("#ContentPlaceHolder1_hdnIntialValue").val();
            var array1 = liIds.split(','); //ControlType and ControlId
            var id = $(elem).attr("id");
            var strType;
            resdOnly = tag;
            //.................................................checkbox get selected other onblur function
            if ($('#txtAllDefault').val() != "") {
                if (tag == "select") {
                    $('#' + array1[1]).val($('#txtAllDefault').val());

                }
                if (tag == "Listbox") {
                    $('#' + array1[1]).val($('#txtAllDefault').val());
                    defaultval = $('#' + id).val();
                }
                else if (tag == "radio") {
                    var defaultvala = $('#txtAllDefault').val();
                    var ide = $("input[value=" + defaultvala + "]").attr("id");
                    $('#' + ide).prop("checked", true);
                }
                else if (tag == "checkbox") {
                    var defaultval2 = $('#txtAllDefault').val();
                    var ide = $("input[value=" + defaultval2 + "]").attr("id");
                    $('#' + ide).prop('checked', true);
                }
            }

            Ids = $('#txtAllId').val();
            if (id == "txtAllName") {
                name = $('#' + id).val();
                //                onBlrStatus = UnqueName(name);
                //                if (onBlrStatus == false) {
                //                    return false;
                //                }
            }
            if (id == "txtLabel") {
                label = $('#' + id).val();
            }
            if (id == "txtAllLength") {
                length = $('#' + id).val();
            }
            if (id == "selVal") {
                validation = $('#' + id).val();
            }
            if (id == "SelRequ") {
                requiredfield = $('#' + id).val();
            }
            if (id == "txtAllTooltip") {
                tooltip = $('#' + id).val();
            }
            if (id == "selTextMode") {
                textMode = $('#' + id).val();
            }
            if (id == "txtCssCls") {
                cssCls = $('#' + id).val();
            }
            if (id == "selDtValue") {
                dataValueField = $('#' + id).val();
            }
            if (id == "selDtTextFld") {
                dataTextField = $('#' + id).val();
            }
            if (id == "selDtSource") {
                datasource = $('#' + id).val();
                FillColumnName(datasource);
            }
            if (id == "txtFileAllow") {
                fileAllowed = $('#' + id).val();
            }
            if (id == "txtMaxSz") {
                maxsize = $('#' + id).val();
            }

            if (id == "selMapping") {
                AutoMapping = $('#' + id).val();
                if (AutoMapping == "1") {
                    $('#divDataValuFld').show();
                    $('#divDataTextFld').show();
                    $('#divDataSource').show();
                }
                else {
                    $('#divDataValuFld').hide();
                    $('#divDataTextFld').hide();
                    $('#divDataSource').hide();
                }
            }
            if (id == "txtAllOption") {
                option = $('#' + id).val();

                var arr = unique(option.split(','));

                if (tag == "select") {
                    if (option != "") {
                        $('#' + array1[1])[0].options.length = 0;

                        for (var i = 0; i < arr.length; i++) {
                            if (arr[i] != "") {
                                $('#' + array1[1]).append(
                                    $('<option></option>').val(arr[i]).html(arr[i]));
                            }
                        }
                    }
                }
                if (tag == "Listbox") {
                    if (option != "") {
                        $('#' + array1[1])[0].options.length = 0;

                        for (var i = 0; i < arr.length; i++) {
                            if (arr[i] != "") {
                                $('#' + array1[1]).append(
                                    $('<option></option>').val(arr[i]).html(arr[i]));
                            }
                        }
                    }
                }
                else if (tag == "radio") {
                    for (var i = 0; i < arr.length; i++) {
                        strRadio = strRadio + '<input type="radio" id="' + array1[0] + '_' + i + '" name="text" value="' + arr[i] + '" class="rad">' + arr[i] + '</input>';
                    }
                    $('#' + array1[0]).html(strRadio + '<span class="drag-area"> </span>');
                }

                else if (tag == "checkbox") {
                    for (var i = 0; i < arr.length; i++) {
                        strRadio = strRadio + '<input type="checkbox" id="' + array1[0] + '_' + i + '" name="text" value="' + arr[i] + '" class="chk">' + arr[i] + '</input>';
                    }
                    $('#' + array1[0]).html(strRadio + '<span class="drag-area"> </span>');
                }
            }


            if (id == "txtAllDefault") {
                defaultval = $('#' + id).val();
                if (tag == "select") {
                    $('#' + array1[1]).val($('#' + id).val());

                }
                if (tag == "Listbox") {
                    $('#' + array1[1]).val($('#' + id).val());
                    defaultval = $('#' + id).val();
                }
                else if (tag == "radio") {
                    defaultval = $('#' + id).val();
                    var ide = $("input[value=" + defaultval + "]").attr("id");
                    $('#' + ide).prop("checked", true);
                }
                else if (tag == "checkbox") {
                    defaultval = $('#' + id).val();
                    var ide = $("input[value=" + defaultval + "]").attr("id");
                    //                    $('#' + ide).selected(true);
                    $('#' + ide).prop('checked', true);
                }

            }
            if (id == "selPlugin") {
                //plugnPag = $('#' + id).val();
                plugnPag = PluginJsonString();
                datasource = $('#' + id).val();
            }
            if (id == "txtHdngText") {
                hdngText = $('#' + id).val();
            }

            $('#' + array1[0]).attr("unname", name);
            var unStatus = UnqueName(name);
            if (unStatus == false) {

                name = "";
                strType = Ids + '+' + name + '+' + label + '+' + length + '+' + tooltip + '+' + textMode + '+' + cssCls + '+' + resdOnly + '+' + validation + '+' + maxsize + '+' + fileAllowed + '+' + dataTextField + '+' + dataValueField + '+' + requiredfield + '+' + datasource + '+' + option + '+' + defaultval + '+' + plugnPag + '+' + hdngText + '+' + AutoMapping;
                $('#' + array1[0]).attr("data", strType);
                return false;

            }
            else {
                strType = Ids + '+' + name + '+' + label + '+' + length + '+' + tooltip + '+' + textMode + '+' + cssCls + '+' + resdOnly + '+' + validation + '+' + maxsize + '+' + fileAllowed + '+' + dataTextField + '+' + dataValueField + '+' + requiredfield + '+' + datasource + '+' + option + '+' + defaultval + '+' + plugnPag + '+' + hdngText + '+' + AutoMapping;
                $('#' + array1[0]).attr("data", strType);
                return true;
            }


        }

        function ClearAllControl() {
            $('#txtAllName').val("");
            $('#txtLabel').val("");
            $('#txtAllLength').val("");
            $('#txtLength').val("");
            $('#SelRequ').val(0);
            $('#txtAllTooltip').val("");
            $('#txtTooltip').val("");
            $('#selVal').val(0);
            $('#selTextMode').val(0);
            $('#txtCssCls').val("");
            $('#selDtValue').val(0);
            $('#selDtTextFld').val(0);
            $('#selDtSource').val(0);
            $('#txtFileAllow').val("");
            $('#txtMaxSz').val("");
            $('#txtAllDefault').val("");
            $('#txtAllOption').val("");
            $('#selPlugin').val(0);
            $('#txtHdngText').val("");
            $('#selMapping').val(0);

        }

        function unique(list) {
            var result = [];
            $.each(list, function (i, e) {
                if ($.inArray(e, result) == -1) result.push(e);
            });
            return result;
        }
    </script>

    <script type="text/javascript">
        function lIEachData() {          
            var strStatus = null;
            $('.drag-list li').each(function (i) {
                var cnt = parseInt(i) + 1;
                var strvalue = $(this).attr('data');
                if (strvalue == "") {
                    alert('Please set the properties of Line no. ' + cnt);
                    strStatus = false;
                }
                else {

                    $('#ContentPlaceHolder1_hdnLiDataValues').val($('#ContentPlaceHolder1_hdnLiDataValues').val() + strvalue + "@")
                    strStatus = true;
                }
            });
            return strStatus;

        }
    </script>

    <script type="text/javascript">
        
        function FillTable() {

            $.ajax({
                type: "POST",
                url: "DynamicformBuilder.aspx/GetTableName",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var selDtSource = $("[id*=selDtSource]");
                    selDtSource.empty().append('<option selected="selected" value="0"> Select</option>');
                    $.each(r.d, function () {
                        selDtSource.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            });
        }

        
        function FillColumnName(tablename) {
            $('#selDtValue').children().remove();
            $('#selDtTextFld').children().remove();

            $.ajax({
                type: "POST",
                url: "DynamicformBuilder.aspx/GetColumnList",
                data: "{tablename:'" + tablename + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var selDtValue = $("[id*=selDtValue]");
                    selDtValue.empty().append('<option selected="selected" value="0"> Select</option>');
                    var selDtTextFld = $("[id*=selDtTextFld]");
                    selDtTextFld.empty().append('<option selected="selected" value="0">Select</option>');
                    $.each(r.d, function () {

                        selDtValue.append($("<option></option>").val(this['Value']).html(this['Text']));
                        selDtTextFld.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            });
        }

        function FillPluginPages() {

            $.ajax({
                type: "POST",
                url: "DynamicformBuilder.aspx/GetPluginPages",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var selPlugin = $("[id*=selPlugin]");
                    selPlugin.empty().append('<option selected="selected" value="0"> Select</option>');
                    $.each(r.d, function () {
                        selPlugin.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            });
        }

    </script>

    <script type="text/javascript">
        
        function isNumber(e) {
            e = e || window.event;
            var charCode = e.which ? e.which : e.keyCode;
            return /\d/.test(String.fromCharCode(charCode));
        }
        
        function nospaces(t) {
            if (t.value.match(/\s/g)) {
                t.value = t.value.replace(/\s/g, '');
            }
        }
    </script>
    
    <script>
        
        function UnqueName(nam) {

            var strStatusUnq = true;
            var list = new Array();
            var ctr = 0;
            $('.drag-list li').each(function (i) {
                var strvalue01 = $(this).attr('unname');
                if (nam == strvalue01) {
                    ctr = ctr + 1;

                }
                if (ctr > 1) {
                    //  alert("Duplicate Name can not be allow !!");
                    $('#txtAllName').val("");
                    $(this).attr("unname", "");
                    ctr = 0;
                    strStatusUnq = false;

                }
            });

            return strStatusUnq;
        }

    </script>

    <script>
        function PluginJsonString() {
           
            if ($('#selPlugin').val() == "1") {
                $('#divHeaderList').hide();
                $('#divColmList').hide();
                $('#divUnqNmId').show();
                $('#divTypeId').show();
                $('#divDisplayId').show();
                $('#divControlOptionId').show();
                var s1 = [];
                var reqclass = "";
                var strtitle = "This filed";
                var strUnqNamearray = ($('#txtUnqNm').val()).split('-');
                var strdisplayarray = ($('#txtDisplay').val()).split('-');
                var strtyparray = ($('#txtType').val()).split('-');
                var ctrlOptionsarray = ($('#txtControlOption').val()).split('-');
                for (var i = 0; i < strUnqNamearray.length; i++) {
                    var ControlName = strUnqNamearray[i];
                    var strdisplay = strdisplayarray[i];
                    var strtyp = strtyparray[i];
                    var Opt = ctrlOptionsarray[i].split(',');
                    var ctrl = [];
                    for (var i1 = 0; i1 < Opt.length; i1++) {
                        ctrl.push(Opt[i1]);
                    }
                    var ctrlOptions1 = ctrl;
                    if ($('#SelRequ').val() == "1") {
                        reqclass = " req rset"
                    }
                    else {
                        reqclass = "";
                    }
                    s1.push({ name: ControlName, display: strdisplay, type: strtyp, ctrlOptions: ctrlOptions1, ctrlClass: reqclass, ctrlAttr: { title: strtitle } });
                }
                var strfy = JSON.stringify(s1);
                var js1 = JSON.parse(strfy);
                option = $('#txtUnqNm').val();
                return strfy;
            }
            else if ($('#selPlugin').val() == "2") {
                $('#divHeaderList').show();
                $('#divColmList').show();
                $('#divUnqNmId').hide();
                $('#divTypeId').hide();
                $('#divDisplayId').hide();
                $('#divControlOptionId').hide();
                option = $('#txtColumnList').val();
                return $('#txtHaederList').val();

            }

            else if ($('#selPlugin').val() == "3") {
                $('#divHeaderList').hide();
                $('#divColmList').hide();
                $('#divUnqNmId').show();
                $('#divTypeId').show();
                $('#divDisplayId').show();
                $('#divControlOptionId').show();
                option = $('#txtControlOption').val();
                defaultval = $('#txtType').val();
                hdngText = $('#txtDisplay').val();
                return $('#txtUnqNm').val();

            }
        }
    </script>

    <script>
        function clearAllVariable() {
       
            var strStatus = true;
            $('.drag-list li').each(function (i) {
                var cnt = parseInt(i) + 1;
                var strDtAry = $(this).attr('data').split('+');
                if ($(this).attr('data') == "") {
                    alert('Please set the properties of above line');
                    strStatus = false;
                }
                else {
                    if (strDtAry[7] != 'Declaration' && strDtAry[7] != 'Heading' && strDtAry[7] != 'Center' && strDtAry[7] != 'Label')
                        if (strDtAry[1] == "") {
                            //                            status = true;
                            Ids = ""; name = ""; label = ""; length = ""; tooltip = ""; textMode = ""; cssCls = ""; resdOnly = ""; validation = ""; maxsize = ""; fileAllowed = ""; dataTextField = "";
                            dataValueField = ""; requiredfield = ""; datasource = ""; option = ""; defaultval = ""; plugnPag = ""; hdngText = ""; AutoMapping = "";
                            alert('First enter the NameField of Above control');
                            strStatus = false;
                        }
                        else {
                            Ids = ""; name = ""; label = ""; length = ""; tooltip = ""; textMode = ""; cssCls = ""; resdOnly = ""; validation = ""; maxsize = ""; fileAllowed = ""; dataTextField = "";
                            dataValueField = ""; requiredfield = ""; datasource = ""; option = ""; defaultval = ""; plugnPag = ""; hdngText = ""; AutoMapping = "";
                            strStatus = true;
                            //                            status = true;

                        }
                }
            });
            return strStatus;

        }
    </script>
       
</asp:Content>
