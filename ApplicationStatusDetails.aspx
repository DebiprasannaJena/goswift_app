<%--'*******************************************************************************************************************
' File Name         : ApplicationDetails.aspx
' Description       : Show the  details of Application
' Created by        : Prasun Kali
' Created On        : 21st August 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationStatusDetails.aspx.cs"
    Inherits="ApplicationStatusDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        .bindlabel
        {
            border: 1px solid #cccccc;
            padding: 3px 10px;
            margin-top: 0px !important;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 0px;
            border-style: none;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 600px;
            height: 400px;
        }
        .Querysec
        {
            background: #eee;
            padding: 10px;
        }
        .Querysec h4
        {
            margin: 0px;
            margin-bottom: 10px;
            font-size: 20px;
            border-bottom: 2px solid #bdbdbd;
            padding-bottom: 8px;
            font-weight: 700;
        }
        .Querysec .table
        {
            background: #fff;
        }
        .Querysec .table-bordered > tbody > tr > td, .Querysec .table-bordered > tbody > tr > th, .Querysec .table-bordered > tfoot > tr > td, .Querysec .table-bordered > tfoot > tr > th, .Querysec .table-bordered > thead > tr > td, .table-bordered > thead > tr > th
        {
            border: 1px solid #d0d0d0;
        }
    </style>
    <script>

        $(document).ready(function () {

            $('.menuservices').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
        });
   
    </script>
    <script type="text/javascript">
        function setvaluesOfrow(flu) {
            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 1;
            debugger;
            // alert(rows);
            //document.getElementById('txtans').value = document.getElementById('gvProposal_hdnTextVal_' + rows).value;

            document.getElementById('lblProposalNo').innerHTML = document.getElementById('gvApplicationDetails_hdnTextVal_' + rows).value;
            document.getElementById('hdnProposalno').value = document.getElementById('gvApplicationDetails_hdnTextVal_' + rows).value;
            document.getElementById('hdnservice').value = document.getElementById('gvApplicationDetails_hdnservice_' + rows).value;

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "ApplicationDetails.aspx/ShowQuery",
                data: '{"id":"' + document.getElementById('lblProposalNo').innerHTML + '","sid":"' + document.getElementById("hdnservice").value + '"}',
                dataType: "json",
                success: function (r) {
                    document.getElementById('hdnQueryCnt').value = r.d.length;
                    $.each(r.d, function () {
                        //                        document.getElementById('1stCnt').style = "display:none";
                        //                        document.getElementById('2ndCnt').style = "display:none";
                        //                        document.getElementById('divF1').style = "display:none";

                        if (r.d.length == 1) {
                            document.getElementById('lblq1').innerHTML = r.d[0]['strRemarks'];
                            document.getElementById('txtA1').value = "";
                            document.getElementById('lblAns1').innerHTML = "";
                            document.getElementById('lblAns1').style = "visibility:hidden";
                            document.getElementById('lblq2').innerHTML = "";
                            document.getElementById('divQ1').style = "block";
                            document.getElementById('divA1').style = "block";
                            document.getElementById('divQ2').style = "display:none";
                            document.getElementById('divA2').style = "display:none";
                            document.getElementById('divF1').style = "block";
                            document.getElementById('divF2').style = "display:none";
                            document.getElementById('DvFile1').style = "block";
                            document.getElementById('divViewFiles1').style = "display:none";
                            document.getElementById('1stCnt').style = "block";
                            document.getElementById('txtA1').style = "block";

                        }
                        else if (r.d.length == 3) {
                            document.getElementById('lblq1').innerHTML = r.d[0]['strRemarks'];
                            document.getElementById('lblAns1').innerHTML = r.d[1]['strRemarks'];
                            document.getElementById('txtA1').style = "display:none";
                            document.getElementById('lblAns1').style = "block";
                            document.getElementById('lblq2').innerHTML = r.d[2]['strRemarks'];
                            document.getElementById('2ndCnt').style = "block";
                            if (r.d[1]['strFileName'] != "") {
                                var strarr = new Array(4);
                                strarr = r.d[1]['strFileName'].split(',');
                                if (strarr[0] != "") {
                                    document.getElementById('hlDoc1').href = "QueryFiles/Services/" + strarr[0];
                                    document.getElementById('divViewFiles1').style = "block";
                                }
                                else { document.getElementById('hlDoc1').style = "display:none"; }
                                if (strarr[1] != "") {
                                    document.getElementById('hlDoc2').href = "QueryFiles/Services/" + strarr[1];
                                    document.getElementById('divViewFiles1').style = "block";
                                }
                                else { document.getElementById('hlDoc2').style = "display:none"; }
                                if (strarr[2] != "") {
                                    document.getElementById('hlDoc3').href = "QueryFiles/Services/" + strarr[2];
                                    document.getElementById('divViewFiles1').style = "block";
                                } else { document.getElementById('hlDoc3').style = "display:none"; }


                                if (strarr[0] == "" && strarr[1] == "" && strarr[2] == "")
                                { document.getElementById('divViewFiles1').style = "display:none"; }
                            }
                            else {
                                document.getElementById('hlDoc1').href = "";
                                document.getElementById('hlDoc2').href = "";
                                document.getElementById('hlDoc3').href = "";
                            }
                            document.getElementById('divQ1').style = "block";
                            document.getElementById('divA1').style = "block";
                            document.getElementById('divQ2').style = "block";
                            document.getElementById('divA2').style = "block";
                            document.getElementById('divF1').style = "block";
                            document.getElementById('divF2').style = "block";
                            document.getElementById('DvFile1').style = "display:none";

                        }
                        //                        if (document.getElementById('lblq2').innerHTML != '') {
                        //                            document.getElementById('DvFile1').style = "display:none";
                        //                        }

                    });
                },
                error: function (msg) {
                    AjaxFailed;
                }
            });
        }
        function DocValid(Controlname) {
            debugger;
            var arr = new Array;
            var arr2 = new Array;
            var arrnew = new Array('pdf');
            var count = 0;
            var y, x, z;


            x = $(Controlname).val();
            z = document.getElementById(Controlname);
            y = x.substring(x.lastIndexOf(".") - 1);
            arr = y.split('.');

            for (var j = 0; j < arrnew.length; j++) {
                if (arr[1] == arrnew[j])
                    count = 1;
            }

            if (count == 0) {
                jAlert('<strong>Please Upload PDF file Only !</strong>', projname);
                //document.getElementById(Controlname).focus();
                return false;
                document.getElementById(Controlname).focus();
            }
            else if (z.files[0].size > 4 * 1024 * 1024) {
                jAlert('<strong>The file size can not exceed 4MB. !</strong>', projname);
                //document.getElementById(Controlname).focus();
                return false;
                document.getElementById(Controlname).focus();
            }
            else
                return true;
        }
        function clear() {
            document.getElementById('txtA2').value = "";
            document.getElementById('txtA1').value = "";
            document.getElementById('FileUpload4').value = "";
            document.getElementById('FileUpload5').value = "";
            document.getElementById('FileUpload6').value = "";
            document.getElementById('FileUpload1').value = "";
            document.getElementById('FileUpload2').value = "";
            document.getElementById('FileUpload3').value = "";
        }
        function setvalue(obj) {
            var ID = obj.id;
            var arr = ID.split('_');
            $('#charsLeft').html(1000 - $('#txtA1').val().length);

        }
        function setvalue1() {

            $('#charsLeft1').html(1000 - $('#txtA2').val().length);
        }
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        function validate() {
            if ($("#txtA1").val() == "") {
                //alert('Response Details cannot be left blank');
                jAlert('<strong>Respond Details cannot be left blank!</strong>', projname);
                $("#txtA1").focus();
                return false;
            }
        }
        function gridValidate(obj) {
            var ID = obj.id;
            var arr = ID.split('_');
            //            if ($("#grdRevertQuery_txtFileContent_" + arr[2]).val() == "") {                
            //                jAlert('<strong>File Content cannot be left blank!</strong>', projname);
            //                return false;
            //                $("#grdRevertQuery_txtFileContent_" + arr(2)).focus();
            //                
            //            }
            //            
            if ($("#grdRevertQuery_FileUpload1_" + arr[2]).val() != "") {
                if (!DocValid('#grdRevertQuery_FileUpload1_' + arr[2]))
                { return false; }
            }
        }
        $(document).ready(function () {
            $("a").click(function (event) {
                debugger;
                var href = $(this).attr('href');
                //$(this).attr('href', '#');
                var Filename = href.split('/');
                if (Filename[3].indexOf('.pdf') > -1) {
                    $('#hdnFileNames').val(Filename[3]);
                    document.getElementById('<%= btnDownload.ClientID %>').click();
                }
                event.preventDefault();
            });
        });
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:investoemenu ID="ineste" runat="server" />
                    </div>
                    <div class="form-sec">
                        <div class="iconsdiv">
                            <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                <i class="fa fa-print"></i></a><a href="javascript:history.back()" title="Back" id="A2"
                                    class="pull-right printbtn bg-blue border-blue"><i class="fa fa-chevron-circle-left">
                                    </i></a>
                        </div>
                        <h2>
                            Application Status Details
                            <%--(Code-Industry Name)--%>
                        </h2>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="ibox">
                                    <%--                                    <div class="ibox-title">
                                        <h5>
                                            Application Details</h5>
                                    </div>--%>
                                    <div class="ibox-content">
                                        <div class="form-group row">
                                            <label class="col-sm-2">
                                                Department Name</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblDepartmntName" CssClass="form-control-static" runat="server"></asp:Label>
                                            </div>
                                            <label class="col-sm-2">
                                                Service Name</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblServiceName" CssClass="form-control-static" runat="server"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2">
                                                Applicant Name</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblApplicantName" CssClass="form-control-static" runat="server"></asp:Label>
                                            </div>
                                            <label class="col-sm-2">
                                                Application No.</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblApplicationNo" CssClass="form-control-static" runat="server"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2">
                                                Application Status</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <label class="form-control-static ">
                                                    <asp:Label ID="lblApplicationStatus" CssClass="label label-primary" runat="server"></asp:Label>
                                                </label>
                                            </div>
                                            <label class="col-sm-2">
                                                Download Certificate</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Label7" CssClass="form-control-static" runat="server" Text="NA"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12" id="dvQueryMain" runat="server">
                                <div class="ibox">
                                    <div class="ibox-title">
                                        <h5>
                                            Query</h5>
                                        <div class="pull-right">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td class="">
                                                            <div class="legendColorBox green">
                                                            </div>
                                                        </td>
                                                        <td class="legendLabel">
                                                            Responded
                                                        </td>
                                                        <td width="10">
                                                            &nbsp;
                                                        </td>
                                                        <td class=" ">
                                                            <div class="legendColorBox blue">
                                                            </div>
                                                        </td>
                                                        <td class="legendLabel">
                                                            Raised
                                                        </td>
                                                        <td width="10">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="form-group row">
                                            <label class="col-sm-3">
                                                Query Status</label>
                                            <div class="col-sm-8">
                                                <span class="colon">:</span>
                                                <label class="form-control-static">
                                                    <asp:Label ID="lblQueryStatus" CssClass="label label-primary" runat="server"></asp:Label></label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                <h4>
                                                    Query Details</h4>
                                            </div>
                                            <div class="col-sm-12" id="QueryHist" runat="server">
                                            </div>
                                            <asp:Button ID="btnDownload" runat="server" Text="Download" Style="display: none"
                                                OnClick="btnDownload_Click" />
                                            <asp:HiddenField ID="hdnFileNames" runat="server" />
                                            <%--<div class="col-sm-12">
                                                <table style="margin-left: 60px;">
                                                    <tbody>
                                                        <tr>
                                                            <td class="">
                                                                <div class="legendColorBox green">
                                                                </div>
                                                            </td>
                                                            <td class="legendLabel">
                                                                Reverted
                                                            </td>
                                                            <td width="10">
                                                                &nbsp;
                                                            </td>
                                                            <td class=" ">
                                                                <div class="legendColorBox blue">
                                                                </div>
                                                            </td>
                                                            <td class="legendLabel">
                                                                Replay
                                                            </td>
                                                            <td width="10">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="legendColorBox">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="legendColorBox">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <div class="messagebox">
                                                    <div class="itemdiv dialogdiv">
                                                        <div class="user">
                                                            <img src="images/user.png" alt="user img">
                                                        </div>
                                                        <div class="body">
                                                            <div class="time">
                                                                <i class="ace-icon fa fa-calendar"></i>12-Sep-2017
                                                            </div>
                                                            <div class="name">
                                                                <a href="#">Er Smt Archana Dash</a>
                                                            </div>
                                                            <div class="form-sec ">
                                                                <div class="form-header">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <div class="legendColorBox blue">
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                Replay
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="form-body">
                                                                    response details
                                                                </div>
                                                            </div>
                                                            <div class="text-right">
                                                                <a href="#" class="btn btn-info btn-sm"><i class="fa fa-download"></i></a>
                                                            </div>
                                                            <!--<div class="text">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque commodo massa sed ipsum porttitor facilisis.</div>-->
                                                        </div>
                                                    </div>
                                                    <div class="itemdiv dialogdiv">
                                                        <div class="user">
                                                            <img src="images/user.png" alt="user img">
                                                        </div>
                                                        <div class="body">
                                                            <div class="time">
                                                                <i class="ace-icon fa fa-calendar"></i>12-Sep-2017
                                                            </div>
                                                            <div class="name">
                                                                <a href="#">Sanghamitra Kumari Samal</a>
                                                            </div>
                                                            <div class="form-sec ">
                                                                <div class="form-header">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <div class="legendColorBox green">
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                Replay
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="form-body">
                                                                    response details
                                                                </div>
                                                            </div>
                                                            <div class="text-right">
                                                                <a href="#" class="btn btn-info btn-sm"><i class="fa fa-download"></i></a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="itemdiv dialogdiv">
                                                        <div class="user">
                                                            <img src="images/user.png" alt="user img">
                                                        </div>
                                                        <div class="body">
                                                            <div class="time">
                                                                <i class="ace-icon fa fa-calendar"></i>12-Sep-2017
                                                            </div>
                                                            <div class="name">
                                                                <a href="#">Er Smt Archana Dash</a>
                                                            </div>
                                                            <div class="form-sec ">
                                                                <div class="form-header">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <div class="legendColorBox blue">
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                Reverted
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="form-body">
                                                                    response details
                                                                </div>
                                                            </div>
                                                            <div class="text-right">
                                                                <a href="#" class="btn btn-info btn-sm"><i class="fa fa-download"></i></a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row" id="dvRevert" runat="server">
                                            <div class="col-sm-12">
                                                <h4>
                                                    Respond Query
                                                    <span style="color:red;">(Note: Maximum of five documents can be attached.)</span>

                                                </h4>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="old-querydetails">
                                                    <div class="form-group" id="divA1">
                                                        <div class="row">
                                                            <label class="col-sm-3">
                                                                Responded Details</label>
                                                            <div class="col-sm-9">
                                                                <span class="colon">:</span>
                                                                <asp:TextBox ID="txtA1" onkeyup="setvalue(this);" runat="server" class="form-control"
                                                                    TextMode="MultiLine" name="q17" MaxLength="1000"></asp:TextBox>
                                                                <div id="1stCnt" class="text-red">
                                                                    <i>Maximum <span id="charsLeft" class="mandatoryspan">1000</span> characters left</i>
                                                                    *</div>
                                                                <asp:HiddenField ID="hdnProposalno" runat="server" />
                                                                <asp:HiddenField ID="hdnQueryCnt" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <%--<asp:UpdatePanel ID="updGridview" runat="server">
                                                                <ContentTemplate>--%>
                                                                <asp:GridView ID="grdRevertQuery" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                    AllowPaging="True" PageSize="50" EmptyDataText="No Record(s) Found" CellPadding="2"
                                                                    border="1" HorizontalAlign="Center" ShowFooter="True" AllowSorting="True" TabIndex="7"
                                                                    OnRowDataBound="grdRevertQuery_RowDataBound" OnRowCommand="grdRevertQuery_RowCommand"
                                                                    DataKeyNames="SLNO" CssClass="table table-bordered bg-white">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSlno" runat="server" Text='<%# Eval("SLNO") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hdfItem" runat="server" Value='<%# Eval("SLNO") %>' />
                                                                                <asp:HiddenField ID="hdnFileName" runat="server" Value='<%# Eval("FileName") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="center" Width="40px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="File Description">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtFileContent" runat="server" MaxLength="200" Text='<%# Eval("FileContent") %>'
                                                                                    CssClass="GridText form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Upload Document<span class="mandatoryspan pull-right">(Only pdf files are allowed, Max
                                                                                    Size 4 MB) </span>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                                                                <asp:HyperLink ID="hypFiles" Target="_blank" class="btn btn-info btn-sm" Visible="false"
                                                                                    runat="server"><i class="fa fa-download"></i></asp:HyperLink>
                                                                                <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FileName") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="btn btn-success btn-sm"
                                                                                    CommandName="btnAddMore" CommandArgument='<%# Eval("SLNO") %>' OnClientClick="return gridValidate(this);">
                                                                                </asp:Button>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle CssClass="paging NOPRINT" />
                                                                    <PagerSettings NextPageText="Next" FirstPageText="First" LastPageText="Last" PreviousPageText="Prev" />
                                                                </asp:GridView>
                                                                <%-- </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="BtnAdd" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-offset-6 col-sm-12">
                                                        <asp:Button ID="btnSubmit" class="btn btn-success" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                            OnClientClick="return validate();" />
                                                        <input type="reset" value="Reset" class="btn btn-danger" style="display: none" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="dvExtent" runat="server">
                                            <label class="col-sm-4">
                                                Extent Query</label>
                                            <div class="col-sm-8">
                                                <span class="colon">:</span> <span style="margin-top: 8px;">
                                                    <asp:LinkButton ID="lbtnExtend" runat="server" CssClass="btn btn-success" OnClick="lbtnExtend_Click">Extent</asp:LinkButton>
                                                </span>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Modified By Pranay Kumar on 12-Sept-2017 for Revert Query Management--%>
                                <%--<div class="Querysec">
                                        <h4>
                                            Query</h4>

                                    </div>--%>
                                <%--Modified By Pranay Kumar on 12-Sept-2017 for Revert Query Management--%>
                            </div>
                            <div class="col-sm-12" id="dvQueryMain1" runat="server">
                                <h4 class="nodata" style="margin: 8% 0%!important">
                                    No Query</h4>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    </form>
</body>
</html>
