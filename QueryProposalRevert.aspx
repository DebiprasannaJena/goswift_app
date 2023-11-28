<%--'*******************************************************************************************************************
' File Name         : ApplicationDetails.aspx
' Description       : Show the details of Query & Take Action as Revert/Extent for Proposal
' Created by        : Pranay Kumar
' Created On        : 14-Sept-2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryProposalRevert.aspx.cs"
    Inherits="QueryProposalRevert" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

            $('.menuproposal').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
        });
   
    </script>
    <script type="text/javascript">
        function DocValid(Controlname) {
            debugger;
            var arr = new Array;
            var arr2 = new Array;
            var arrnew = new Array('pdf');
            var count = 0;
            var y, x, z;


            x = $(Controlname).val();
            z = Controlname;
            y = x.substring(x.lastIndexOf(".") - 1);
            arr = y.split('.');

            for (var j = 0; j < arrnew.length; j++) {
                if (arr[1] == arrnew[j])
                    count = 1;
            }

            if (count == 0) {
                jAlert('<strong>Please Upload PDF file Only !</strong>', projname);
                return false;
                document.getElementById(Controlname).focus();
            }
            else if (z.files[0].size > 10 * 1024 * 1024) {
                jAlert('<strong>The file size can not exceed 12 MB. !</strong>', projname);
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
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        function validate() {
            if ($("#txtA1").val() == "") {
                //alert('Response Details cannot be left blank');
                jAlert('<strong>Respond Details cannot be left blank!</strong>', projname);
                $("#txtA1").focus();
                return false;
            }
            //            if ($('#FileUpload1').val() != "") {
            //                if (!DocValid('#FileUpload1'))
            //                { return false; }
            //            }

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
                if (Filename[2].indexOf('.pdf') > -1) {
                    $('#hdnFileNames').val(Filename[2]);
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
    <div class="container">
        <uc2:header ID="header" runat="server" />
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
                            Proposal Query Details
                            <%--(Code-Industry Name)--%>
                        </h2>
                       
                            <%--<div class="col-sm-6">
                                <div class="ibox">
                                    <div class="ibox-title">
                                        <h5>
                                            Proposal Details</h5>                                        
                                    </div>
                                    <div class="ibox-content">
                                        <div class="form-group row">
                                            <label class="col-sm-4">
                                                Name Of Company/Enterprises</label>
                                            <div class="col-sm-8">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblCompanyName" CssClass="form-control-static" runat="server"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-4">
                                                Industry Type</label>
                                            <div class="col-sm-8">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblIndustryType" CssClass="form-control-static" runat="server"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-4">
                                                Proposal No.</label>
                                            <div class="col-sm-8">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblProposalNo" CssClass="form-control-static" runat="server"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>                                        
                                        <div class="form-group row">
                                            <label class="col-sm-4">
                                                Application Status</label>
                                            <div class="col-sm-8">
                                                <span class="colon">:</span>
                                                <asp:Label ID="lblApplicationStatus" CssClass="form-control-static" runat="server"></asp:Label>
                                                <asp:Label ID="Label1" runat="server" CssClass="label label-success" Text="Applied"></asp:Label>
                                                <asp:Label ID="Label2" runat="server" CssClass="label label-primary" Text="Reject"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>                                        
                                    </div>
                                </div>
                            </div>--%>
                            <div id="dvQueryMain" runat="server">
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
                                           <%-- <div class="col-sm-12">
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
                                                                Respond Details</label>
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
                                                                        <ItemStyle HorizontalAlign="Center"  Width="50px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="File Description">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtFileContent" runat="server" CssClass="form-control GridText" MaxLength="200"  Text='<%# Eval("FileContent") %>'
                                                                              ></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle  />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Upload Document<span class="mandatoryspan pull-right">(Only pdf
                                                                                files are allowed, Max Size 12 MB) </span>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server"  />
                                                                            <asp:HyperLink ID="hypFiles" Target="_blank" class="btn btn-info btn-sm" Visible="false"
                                                                                runat="server"><i class="fa fa-download"></i></asp:HyperLink>
                                                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FileName") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle  />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="BtnAdd"  runat="server" Text="Add" CssClass="btn btn-success btn-sm"
                                                                                CommandName="btnAddMore"  CommandArgument='<%# Eval("SLNO") %>'
                                                                                OnClientClick="return gridValidate(this);"></asp:Button>
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
                                                    <div class="col-sm-offset-3 col-sm-9">
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
                                <h4 class="nodata" style="margin:8% 0%!important">
                                    No Query</h4>
                            </div>
                        <div class="clearfix"> </div>
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
