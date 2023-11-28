<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DynamicForm.aspx.cs" Inherits="DynamicForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
 <style>
      body {
        font-family: arial;
        background: rgb(242, 244, 246);
        -webkit-user-select: none;
        -moz-user-select: none;
        -ms-user-select: none;
        user-select: none;
      }

      h3 {
        color: rgb(199, 204, 209);
        font-size: 28px;
        text-align: center;
      }

      #elements-container {
        text-align: center;
      }

      .draggable-element {
        display: inline-block;
        width: 200px;
        height: 200px;
        background: white;
        border: 1px solid rgb(196, 196, 196);
        line-height: 200px;
        text-align: center;
        margin: 10px;
        color: rgb(51, 51, 51);
        font-size: 30px;
        cursor: move;
      }

      .drag-list {
        width: 600px;
        margin: 0 auto;
      }

      .drag-list > li {
        list-style: none;
        background: rgb(255, 255, 255);
        border: 1px solid rgb(196, 196, 196);
        margin: 5px 0;
        font-size: 18px;
      }

      .drag-list .title {
        display: inline-block;
        width: 90%;
        padding: 6px 6px 6px 12px;
        vertical-align: top;
      }

      .drag-list .drag-area {
        display: inline-block;
        background: rgb(158, 211, 179);
        width: 10%;
        height: 40px;
        vertical-align: top;
        float: right;
        cursor: move;
      }

      .code {
        background: rgb(255, 255, 255);
        border: 1px solid rgb(196, 196, 196);
        width: 600px;
        margin: 22px auto;
        position: relative;
      }

      .code::before {
        content: 'Code';
        background: rgb(80, 80, 80);
        width: 96%;
        position: absolute;
        padding: 8px 2%;
        color: rgb(255, 255, 255);
      }
      
      .code pre {
        margin-top: 50px;
        padding: 0 13px;
        font-size: 1em;
      }
      .popupcls
      {
          width:100px;
          height:100px;
          azimuth:right-side;
      }
        .popupcls1
      {
          width:400px;
          height:400px;
          azimuth:left-side;
      }
    </style>
      <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
<script type="text/javascript">
    var Ids = ""; var name = ""; var label = ""; var length = ""; var tooltip = ""; var textMode = ""; var cssCls = ""; var resdOnly = ""; var validation = ""; var maxsize = ""; var fileAllowed = ""; var dataTextField = "";
    var dataValueField = ""; var requiredfield = ""; var datasource = ""; var option = ""; var defaultval = ""; var plugnPag = ""; var hdngText = "";  
</script>
    <script>
        $(document).ready(function () {
            //controlgenerete
            $("#txt").click(function () {
                clearAllVariable();
                debugger;
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
                var ctrl = '<input type="text" id="' + txtbxId + '" name="text" class="text">';
                var li = '<li id=' + chkids + ' data="" title=' + liData + ',' + txtbxId + ' onclick="clickFunc(' + chkids + ')">' + ctrl + '<span class="drag-area"> </li>';
                $("#listControl").append(li);
                testDrag();
            });
            $("#chk").click(function () {
                clearAllVariable();
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
                var ctrl = '<input type="checkbox" id="' + ChechId + '" name="' + ChechId + '" class="chk"></br><input type="checkbox" id="12" name="' + ChechId + '" class="chk">';
                var li = '<li id=' + chkids + ' data="" title=' + liData + ',' + ChechId + ' onclick="clickFunc(' + chkids + ')">' + ctrl + '<span class="drag-area"> </li>';
                $("#listControl").append(li);
                testDrag();
            });
            $("#rad").click(function () {
                clearAllVariable()

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
                var li = '<li id=' + chkids + '  title=' + liData + ',' + radiobtnId + ' data="" onclick="clickFunc(' + chkids + ')">' + ctrl + '<span class="drag-area"> </li>';
                $("#listControl").append(li);
                testDrag();
            });

            $("#filUld").click(function () {
                clearAllVariable();
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
                var ctrl = '<input type="file" id="' + filId + '" name="text" class="rad">';
                var li = '<li id=' + chkids + ' title=' + liData + ',' + filId + ' data="" onclick="clickFunc(' + chkids + ')" value="' + filId + '">' + ctrl + '<span class="drag-area"> </li>';
                $("#listControl").append(li);
                testDrag();
            });


            $("#SelectId").click(function () {
                clearAllVariable();
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
                var ctrl = '<select  id="' + drpId + '" name="text" class="select" > <option>option1</option></select>';
                var li = '<li id=' + chkids + ' title=' + liData + ',' + drpId + ' data="" onclick="clickFunc(' + chkids + ')" value="' + drpId + '">' + ctrl + '<span class="drag-area"> </li>';
                $("#listControl").append(li);
                testDrag();
            });

            $("#ListboxId").click(function () {
                clearAllVariable();
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
                var ctrl = '<select  id="' + drpId + '" name="text" class="select" multiple > <option>option1</option></select>';
                var li = '<li id=' + chkids + ' title=' + liData + ',' + drpId + ' data="" onclick="clickFunc(' + chkids + ')" value="' + drpId + '">' + ctrl + '<span class="drag-area"> </li>';
                $("#listControl").append(li);
                testDrag();
            });

            $("#PluginId").click(function () {
                clearAllVariable();
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
                var ctrl = '<div id="' + drpId + '">Plugin</div>';
                var li = '<li id=' + chkids + ' title=' + liData + ',' + drpId + ' data="" onclick="clickFunc(' + chkids + ')" value="' + drpId + '">' + ctrl + '<span class="drag-area"> </li>';
                $("#listControl").append(li);
                testDrag();
            });
            $("#hdngId").click(function () {
                clearAllVariable();
                if ($("#hdnHeading").val() == '') {
                    var list = 1;
                }
                else {
                    list = parseInt($("#hdnHeading").val()) + 1
                }
                $("#hdnHeading").val(list);
                var drpId = 'Lbl_' + list;
                var liid = drpId;
                var chkids = "'l" + liid + "'";
                var liData = "Heading";
                // var ctrl = '<input type="text" id="' + drpId + '" Text="<h1>Heading</h1>" name="text" class="text">'; //<h1 id="' + drpId + '">Heading</h1>';'<input type="text" id="' + txtbxId + '" name="text" class="text">';
                var ctrl = '<h1 id="' + drpId + '">Heading</h1>';
                //var ctrl = '<input type="text" id="' + drpId + '"  name="text" class="text"><h1>Heading</h1></input>';
                var li = '<li id=' + chkids + ' title=' + liData + ',' + drpId + ' data="" onclick="clickFunc(' + chkids + ')" value="' + drpId + '">' + ctrl + '<span class="drag-area"> </li>';
                $("#listControl").append(li);
                testDrag();
            });
        });
        function clearAllVariable() {
            Ids = ""; name = ""; label = ""; length = ""; tooltip = ""; textMode = ""; cssCls = ""; resdOnly = ""; validation = ""; maxsize = ""; fileAllowed = ""; dataTextField = "";
            dataValueField = ""; requiredfield = ""; datasource = ""; option = ""; defaultval = ""; plugnPag = ""; hdngText = "";
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    Form Name:
    <asp:DropDownList ID="ddlServiceId" runat="server"></asp:DropDownList>
        <br/>
    Form Logo:
    <asp:FileUpload  ID="fulLogo" runat="server"/>
        <br/>
    Form Header:<cc1:Editor ID="editHeader" runat="server" Width="600px" />
   <%-- <asp:TextBox ID="txtHeaderText" runat="server"></asp:TextBox>--%>
        <br/>
    Form Footer:<cc1:Editor ID="editFooter" runat="server" Width="600px" />
   <%-- <asp:TextBox ID="txtFooterText" runat="server"></asp:TextBox>--%>
   Allignment:
   <asp:RadioButtonList ID="rdbAllignment" runat="server" RepeatDirection="Horizontal" 
                                                 RepeatLayout="Flow" CssClass="rdBtn">
                                                <asp:ListItem Value="1"  Text="OneLine" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="TwoLine"></asp:ListItem>
                                                 <asp:ListItem Value="2" Text="3rdLine"></asp:ListItem>
                                            </asp:RadioButtonList>
    </div>
    <br/>
<div style="display:inline" class="nav">
<input type="button" id="txt" value="Add TextBox" style="" />
<input type="button" id="chk" value="Add CheckBox" style="" />
<input type="button" id="rad" value="Add RadioButton" style="" />
<input type="button" id="filUld" value="Add File Upload" style="" />
<input type="button" id="SelectId" value="Add Drop Dwon" style="" />
<input type="button" id="ListboxId" value="Add ListBox" style="" />
<input type="button" id="PluginId" value="Add Plugins" style="" />
<input type="button" id="hdngId" value="Add Heading" style="" />
<input type="hidden" id="hdftxt" runat="server" />
<input type="hidden" id="hdfcheck" runat="server" />	
 <input type="hidden" id="hdnrad" runat="server" />	
 <input type="hidden" id="hdnFile" runat="server" />	
 <input type="hidden" id="hdnDrop" runat="server" />	
  <input type="hidden" id="hdnListBox" runat="server" />
    <input type="hidden" id="hdnPlugin" runat="server" />
        <input type="hidden" id="hdnHeading" runat="server" />
 <input type="hidden" id="hdnIntialValue" runat="server" />	
 <input type="hidden" id="hdnLiDataValues" runat="server" />		
</div>




  <div id="dynamicControl">
  <div class="popupcls1">
<ul id="listControl" class="drag-list">
</ul>
</div>
<div id="divRadioId">

</div>

<div id="divAllNameId" style="display:none">
Name:
<input type="text" id="txtAllName" onblur="EachControlOnblur(this);" onkeyup="nospaces(this)" />
</div>
<div id="divAllLabelId" style="display:none">
Label:
<input type="text" id="txtLabel" onblur="EachControlOnblur(this);" />
</div>
<div id="divAllId" style="display:none">
ID:
<input type="text" id="txtAllId" onblur="EachControlOnblur(this);" />
</div>
<div id="divAllLngth" style="display:none">
Length:
<input type="text" id="txtAllLength" onkeypress="return isNumber(event);"  onblur="EachControlOnblur(this);" />
</div>
<div id="divAllVal" style="display:none">
Validation:
<select onchange="EachControlOnblur(this);" id="selVal">
<option value="0">Select</option>
  <option value="Email">Email</option>
  <option value="Character">Character</option>
   <option value="Number">Number</option>
    <option value="PhoneNumber">PhoneNumber</option>
</select>
</div>
<div id="divRequ" style="display:none">
Required:
<select onchange="EachControlOnblur(this);" id="SelRequ">
  <option value="1">False</option>
  <option value="0">True</option>
</select>
</div>
<div id="divToolTip" style="display:none">
ToolTip:
<input type="text" id="txtAllTooltip" onblur="EachControlOnblur(this);" />
</div>
<div id="divTextMode" style="display:none">
Text Mode:
<select onchange="EachControlOnblur(this);" id="selTextMode">
<option value="0">Select</option>
  <option value="SingleLine">SingleLine</option>
  <option value="MultiLine">MultiLine</option>
   <option value="Password">Password</option>
    <option value="DateTime">DateTime</option>
</select>
</div>
<div id="divCssClass" style="display:none">
Css Class:
<input type="text" id="txtCssCls" onblur="EachControlOnblur(this);" />
</div>
<div id="divDataSource" style="display:none">
DataSource TableName:
<select onchange="EachControlOnblur(this);" id="selDtSource">
  <option value="0">Select</option>
  <option value="T_GMS_CATEGORY">T_GMS_CATEGORY</option>
  <option value="T_GMS_SUBCATEGORY">T_GMS_SUBCATEGORY</option>
</select>
</div>
<div id="divDataValuFld" style="display:none">
DataValueField:
<select onchange="EachControlOnblur(this);" id="selDtValue">
<option value="0">Select</option>
<%--  <option value="CATEGORYID">CATEGORYID</option>
  <option value="SUBCATEGORYID">SUBCATEGORYID</option>--%>
</select>
</div>
<div id="divDataTextFld" style="display:none">
DataTextField:
<select onchange="EachControlOnblur(this);" id="selDtTextFld">
<option value="0">Select</option>
 <%-- <option value="CATEGORYNAME">CATEGORYNAME</option>
  <option value="SUBCATEGORYNAME">SUBCATEGORYNAME</option>--%>
</select>
</div>
<div id="divFileAllowed" style="display:none">
Type of FileAllowed:
<input type="text" id="txtFileAllow"  onblur="EachControlOnblur(this);"/>
</div>
<div id="divMaxSize" style="display:none">
Max Size:
<input type="text" id="txtMaxSz" onkeypress="return isNumber(event);"  onblur="EachControlOnblur(this);" />
</div>
<div id="divAllOption" style="display:none">
Option:
<input type="text" id="txtAllOption" onblur="EachControlOnblur(this);" />
</div>
<div id="divDefault" style="display:none">
Default Value:
<input type="text" id="txtAllDefault" onblur="EachControlOnblur(this);" />
</div>
<div id="divPlugin" style="display:none">
Plugin Pages:
<select onchange="EachControlOnblur(this);" id="selPlugin">
<option value="0">Select</option>
  <option value="Plugins.aspx">Plugins.aspx</option>
  <option value="Default.aspx">Default.aspx</option>
</select>
</div>
<div id="divText" style="display:none">
Text:
<input type="text" id="txtHdngText" onblur="EachControlOnblur(this);" />
</div>
<%--<asp:DropDownList ID="ddlATA"  data-placeholder="Select ATA."  style="width:100%" runat="server"></asp:DropDownList>--%>
</div>


   <asp:Button ID="Button1" runat="server" Text="Save" 
        OnClientClick="return lIEachData();"   CssClass="btn btn-success" 
        onclick="Button1_Click"   />   
    </form>

    <script language="javascript" type="text/javascript">
        pageHeader = "Escalation"
        strFirstLink = "Config"
        strLastLink = "Escalation"
        printMe = "no"
        $(document).ready(function () {
            debugger;
            FillTable();
            //FillPluginPages();
        });

        	
    </script>


 <%--<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>--%>

<%--  <script src="Scripts/jquery.ui.draggable.js" type="text/javascript"></script>--%>
<script src="js/drag-arrange.js" type="text/javascript"></script>

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
       //clickFunc
       function clickFunc(id) {
           debugger;

           ClearAllControl();
           var strType = $('#' + id).attr('title');
           var strdata = $('#' + id).attr('data');
           var array = strType.split(',');
           var type = array[0];
           var controlid = array[1];
           //tag = $('#' + array[1]).prop('type');
           tag = type;
           $('#selDtValue').children().remove();
           $('#selDtTextFld').children().remove();
           $("#hdnIntialValue").val(id + ',' + controlid);
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
               $('#divMaxSize').hide();
               $('#divAllOption').show();
               $('#divDefault').show();
               $('#divPlugin').hide();
               $('#divText').hide();

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
               $('#divMaxSize').hide();
               $('#divAllOption').show();
               $('#divDefault').show();
               $('#divPlugin').hide();
               $('#divText').hide();


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
               $('#divMaxSize').hide();
               $('#divAllOption').show();
               $('#divDefault').show();
               $('#divPlugin').hide();
               $('#divText').hide();

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
               $('#divMaxSize').hide();
               $('#divAllOption').show();
               $('#divDefault').show();
               $('#divMultiSelected').show();
               $('#divPlugin').hide();
               $('#divText').hide();
           }
           else if (type == "Plugin") {
               $('#txtAllId').val(controlid);
               $('#txtAllId').prop('readonly', true);
               $('#divAllNameId').show();
               $('#divAllLabelId').hide();
               $('#divAllId').show();
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
               $('#divPlugin').show();
               $('#divText').hide();

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

           }
           EditFunc(strdata);
       }

        </script>
        <script type="text/javascript">
            //EditFunc

            function EditFunc(strdata) {
                debugger;
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
                    //assigned value to the variable
                    Ids = array[0]; name = array[1]; label = array[2]; length = array[3]; tooltip = array[4]; textMode = array[5]; cssCls = array[6]; resdOnly = ""; validation = array[8]; maxsize = array[9]; fileAllowed = array[10]; dataTextField = array[11];
                    dataValueField = array[12]; requiredfield = array[13]; datasource = array[14]; option = array[15]; defaultval = array[16]; plugnPag = array[17]; hdngText = [18];

                }
            }

        </script>
           <script type="text/javascript">


               function EachControlOnblur(elem) {
                   debugger;
                   var strRadio = "";
                   var liIds = $("#hdnIntialValue").val();
                   var array1 = liIds.split(','); //ControlType and ControlId
                   var id = $(elem).attr("id");
                   var strType;
                   resdOnly = tag;
                   //.................................................checkbox get selected other onblur function
                   if ($('#txtAllDefault').val() != "") {
                       if (tag == "select") {
                           $('#' + array1[1]).val($('#txtAllDefault').val());

                       }
                       if (tag == "select-multiple") {
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
                       if (tag == "select-multiple") {
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
                       if (tag == "select-multiple") {
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
                       plugnPag = $('#' + id).val();
                   }
                   if (id == "txtHdngText") {
                       hdngText = $('#' + id).val();
                   }
                   strType = Ids + '+' + name + '+' + label + '+' + length + '+' + tooltip + '+' + textMode + '+' + cssCls + '+' + resdOnly + '+' + validation + '+' + maxsize + '+' + fileAllowed + '+' + dataTextField + '+' + dataValueField + '+' + requiredfield + '+' + datasource + '+' + option + '+' + defaultval + '+' + plugnPag + '+' + hdngText;
                   $('#' + array1[0]).attr("data", strType);
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
            debugger;
            var strStatus = null;

            if ($('#ddlServiceId').val() == 0) {
                alert('Please Select ServiceName');
                $('#ddlServiceId').focus()
                strStatus = false;
            }



            $('.drag-list li').each(function (i) {
                var cnt = parseInt(i) + 1;
                var strvalue = $(this).attr('data');
                if (strvalue == "") {
                    alert('Please set the properties of Line no. ' + cnt);
                    strStatus = false;
                }
                else {

                    $('#hdnLiDataValues').val($('#hdnLiDataValues').val() + strvalue + "@")
                    strStatus = true;
                }
            });
            return strStatus;

        }
    </script>
    <script type="text/javascript">
        //FillTable
        function FillTable() {
            debugger;
            $.ajax({
                type: "POST",
                url: "DynamicForm.aspx/GetTableName",
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
        //FillColumnName
        function FillColumnName(tablename) {
            $('#selDtValue').children().remove();
            $('#selDtTextFld').children().remove();
            debugger;
            $.ajax({
                type: "POST",
                url: "DynamicForm.aspx/GetColumnList",
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
            
           
            debugger;
            $.ajax({
                type: "POST",
                url: "DynamicForm.aspx/GetPluginPages",
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
       //number only
       function isNumber(e) {
           e = e || window.event;
           var charCode = e.which ? e.which : e.keyCode;
           return /\d/.test(String.fromCharCode(charCode));
       }

       //nospaces
       function nospaces(t) {
           if (t.value.match(/\s/g)) {
               t.value = t.value.replace(/\s/g, '');
           }
       }
   </script>
    <script type="text/javascript">
        function FillTable() {
            debugger;
            $.ajax({
                type: "POST",
                url: "Plugins.aspx/GetTableName",
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
        </script>
</body>
</html>
