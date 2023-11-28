<%--'*******************************************************************************************************************
' File Name         : QueryIncentiveRevert.aspx
' Description       : Show the details of Query & Take Action as Revert/Extent for Proposal
' Created by        : Pranay Kumar
' Created On        : 11th Oct 2017
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryIncentiveRevert.aspx.cs"
    Inherits="incentives_QueryIncentiveRevert" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {
            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
        });

        function pageLoad() {
            $('.menuincentive').addClass('active');
        }


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
                jAlert('<strong>Response Details cannot be left blank!</strong>', projname);
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

            if ($("#grdRevertQuery_txtFileContent_" + arr[2]).val() == "") {
                jAlert('<strong>File description cannot be left blank !</strong>', projname);
                return false;
                $("#grdRevertQuery_txtFileContent_" + arr(2)).focus();
            }

            if ($("#grdRevertQuery_FileUpload1_" + arr[2]).val() == "") {
                jAlert('<strong>Please Upload File !!</strong>', projname);
                return false;
            }

            if ($("#grdRevertQuery_FileUpload1_" + arr[2]).val() != "") {
                if (!DocValid('#grdRevertQuery_FileUpload1_' + arr[2])) {
                    $("#grdRevertQuery_FileUpload1_" + arr[2]).val('');
                    return false;
                }
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
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div id="Div1" class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div id="exTab1">
                <div class="investrs-tab">
                    <uc5:pealmenu ID="ineste" runat="server" />
                </div>
                <div class="form-sec">
                    <div class="iconsdiv">
                        <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                            <i class="fa fa-print"></i></a><a href="javascript:history.back()" title="Back" id="A2"
                                class="pull-right printbtn bg-blue border-blue"><i class="fa fa-chevron-circle-left">
                                </i></a>
                    </div>
                    <h2>
                        Incentive Query Details
                    </h2>
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
                                            <asp:Label ID="lblQueryStatus" CssClass="label label-success" runat="server"></asp:Label></label>
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
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="form-group row" id="dvRevert" runat="server">
                                    <div class="col-sm-12">
                                        <h4>
                                            Respond Query</h4>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="old-querydetails">
                                            <div class="form-group" id="divA1">
                                                <div class="row">
                                                    <label class="col-sm-3">
                                                        Response Details</label>
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
                                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="File Description">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtFileContent" runat="server" CssClass="form-control GridText"
                                                                            MaxLength="200" Text='<%# Eval("FileContent") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Upload Document<span class="mandatoryspan pull-right">(Only pdf files are allowed, Max
                                                                            Size 12 MB) </span>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                                                        <asp:HyperLink ID="hypFiles" Target="_blank" class="btn btn-info btn-sm" Visible="false"
                                                                            runat="server"><i class="fa fa-download"></i></asp:HyperLink>
                                                                        <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FileName") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="BtnAdd" runat="server" Text="Add" Width="70%" CssClass="btn btn-success btn-sm"
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
                    </div>
                    <div class="col-sm-12" id="dvQueryMain1" runat="server">
                        <h4 class="nodata" style="margin: 8% 0%!important">
                            No Query</h4>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
