<%--'*******************************************************************************************************************
' File Name         : ApplicationDetails.aspx
' Description       : Show the  details of all Application Application
' Created by        : Prasun Kali
' Created On        : 21st August 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationDetails.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="ApplicationDetails" %>

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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menuservices').addClass('active');
            $("#BtnPayMultipe").click(function () {
                var chkboxrowcount = $("#<%=gvApplicationDetails.ClientID%> input[id*='ChkBxSelect']:checkbox:checked").size();
                if (chkboxrowcount == 0) {
                    alert("please select at least a record");
                    return false;
                }
                return true;
            });
        });
    </script>
    <script type="text/javascript">

        function htmlUnescape(value) {
            return String(value)
                .replace(/&quot;/g, '"')
                .replace(/&#39;/g, "'")
                .replace(/&lt;/g, '<')
                .replace(/&gt;/g, '>')
                .replace(/&amp;/g, '&');
        }

        function setvaluesOfrow(flu) {
            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 1;
            // alert(rows);
            //document.getElementById('txtans').value = document.getElementById('gvProposal_hdnTextVal_' + rows).value;
            document.getElementById('divResults').innerHTML = "";
            document.getElementById('divResults').innerHTML = htmlUnescape(document.getElementById('gvApplicationDetails_hdnremark_' + rows).value);

            document.getElementById('lblProposalNo').innerHTML = document.getElementById('gvApplicationDetails_hdnTextVal_' + rows).value;
            document.getElementById('hdnProposalno').value = document.getElementById('gvApplicationDetails_hdnTextVal_' + rows).value;
            document.getElementById('hdnservice').value = document.getElementById('gvApplicationDetails_hdnservice_' + rows).value;

            //            $.ajax({
            //                type: "POST",
            //                contentType: "application/json; charset=utf-8",
            //                url: "ApplicationDetails.aspx/ShowQuery",
            //                data: '{"id":"' + document.getElementById('lblProposalNo').innerHTML + '","sid":"' + document.getElementById("hdnservice").value + '"}',
            //                dataType: "json",
            //                success: function (r) {
            //                    document.getElementById('hdnQueryCnt').value = r.d.length;
            //                    $.each(r.d, function () {
            //                        //                        document.getElementById('1stCnt').style = "display:none";
            //                        //                        document.getElementById('2ndCnt').style = "display:none";
            //                        //                        document.getElementById('divF1').style = "display:none";

            //                        if (r.d.length == 1) {
            //                            document.getElementById('lblq1').innerHTML = r.d[0]['strRemarks'];
            //                            document.getElementById('txtA1').value = "";
            //                            document.getElementById('lblAns1').innerHTML = "";
            //                            document.getElementById('lblAns1').style = "visibility:hidden";
            //                            document.getElementById('lblq2').innerHTML = "";
            //                            document.getElementById('divQ1').style = "block";
            //                            document.getElementById('divA1').style = "block";
            //                            document.getElementById('divQ2').style = "display:none";
            //                            document.getElementById('divA2').style = "display:none";
            //                            document.getElementById('divF1').style = "block";
            //                            document.getElementById('divF2').style = "display:none";
            //                            document.getElementById('DvFile1').style = "block";
            //                            document.getElementById('divViewFiles1').style = "display:none";
            //                            document.getElementById('1stCnt').style = "block";
            //                            document.getElementById('txtA1').style = "block";

            //                        }
            //                        else if (r.d.length == 3) {
            //                            document.getElementById('lblq1').innerHTML = r.d[0]['strRemarks'];
            //                            document.getElementById('lblAns1').innerHTML = r.d[1]['strRemarks'];
            //                            document.getElementById('txtA1').style = "display:none";
            //                            document.getElementById('lblAns1').style = "block";
            //                            document.getElementById('lblq2').innerHTML = r.d[2]['strRemarks'];
            //                            document.getElementById('2ndCnt').style = "block";
            //                            if (r.d[1]['strFileName'] != "") {
            //                                var strarr = new Array(4);
            //                                strarr = r.d[1]['strFileName'].split(',');
            //                                if (strarr[0] != "") {
            //                                    document.getElementById('hlDoc1').href = "QueryFiles/Services/" + strarr[0];
            //                                    document.getElementById('divViewFiles1').style = "block";
            //                                }
            //                                else { document.getElementById('hlDoc1').style = "display:none"; }
            //                                if (strarr[1] != "") {
            //                                    document.getElementById('hlDoc2').href = "QueryFiles/Services/" + strarr[1];
            //                                    document.getElementById('divViewFiles1').style = "block";
            //                                }
            //                                else { document.getElementById('hlDoc2').style = "display:none"; }
            //                                if (strarr[2] != "") {
            //                                    document.getElementById('hlDoc3').href = "QueryFiles/Services/" + strarr[2];
            //                                    document.getElementById('divViewFiles1').style = "block";
            //                                } else { document.getElementById('hlDoc3').style = "display:none"; }


            //                                if (strarr[0] == "" && strarr[1] == "" && strarr[2] == "")
            //                                { document.getElementById('divViewFiles1').style = "display:none"; }
            //                            }
            //                            else {
            //                                document.getElementById('hlDoc1').href = "";
            //                                document.getElementById('hlDoc2').href = "";
            //                                document.getElementById('hlDoc3').href = "";
            //                            }
            //                            document.getElementById('divQ1').style = "block";
            //                            document.getElementById('divA1').style = "block";
            //                            document.getElementById('divQ2').style = "block";
            //                            document.getElementById('divA2').style = "block";
            //                            document.getElementById('divF1').style = "block";
            //                            document.getElementById('divF2').style = "block";
            //                            document.getElementById('DvFile1').style = "display:none";

            //                        }
            //                        //                        if (document.getElementById('lblq2').innerHTML != '') {
            //                        //                            document.getElementById('DvFile1').style = "display:none";
            //                        //                        }

            //                    });
            //                },
            //                error: function (msg) {
            //                    AjaxFailed;
            //                }
            //            });
        }

        function DocValid(Controlname) {
            var arr = new Array;
            var arr2 = new Array;
            var arrnew = new Array('pdf');
            var count = 0;
            var y, x, z;


            x = document.getElementById(Controlname).value;
            z = document.getElementById(Controlname);
            y = x.substring(x.lastIndexOf(".") - 1);
            arr = y.split('.');

            for (var j = 0; j < arrnew.length; j++) {
                if (arr[1] == arrnew[j])
                    count = 1;
            }

            if (count == 0) {
                alert('Please Upload PDF file Only!');
                document.getElementById(Controlname).focus();
                return false;
            }
            else if (z.files[0].size > 4 * 1024 * 1024) {
                alert('The file size can not exceed 4MB.');
                document.getElementById(Controlname).focus();
                return false;
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

        function setvalue() {

            $('#charsLeft').html(1000 - $('#txtA1').val().length);
        }

        function setvalue1() {

            $('#charsLeft1').html(1000 - $('#txtA2').val().length);
        }

        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }

        function validate() {
            if (document.getElementById("ddlDepartment").value == "Select") {
                alert('Please select Department');
                return false;
            }
            //            if(document.getElementById("ddlApplicationNo").value =="Select") {
            //                alert('Please select Application No');
            //                return false;
            //            }
        }
    </script>
    <style type="text/css">
        .bindlabel {
            border: 1px solid #cccccc;
            padding: 3px 10px;
            margin-top: 0px !important;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 0px;
            border-style: none;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 600px;
            height: 400px;
        }

        .se-pre-conloadr {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            text-align: center;
            vertical-align: middle;
            background: rgba(255,255,255,.7);
        }


        .pageResult {
            padding-bottom: 6px;
        }

            .pageResult a {
                font-size: 13px;
                font-weight: 600;
            }

                .pageResult a:hover, .pageResult a:focus {
                    text-decoration: none;
                    color: #59b2ff;
                }

            .pageResult span {
                font-size: 14px;
                color: #000;
            }

            .pageResult .fa {
                color: #337ab7;
            }

        .table .statusdetails {
        }

            .table .statusdetails .appstatus {
                display: block;
                color: #FF9800;
            }

        small {
            font-size: 80% !important;
            color: #464646;
        }

            small span {
                font-size: 100% !important;
            }

        .table .statusdetails .appstatus a {
            margin-left: 5px;
        }

        .linkcheck {
            color: #08599e !important;
            font-size: 16px;
            text-decoration: none;
            display: inline-block;
            margin-top: 4px;
        }

            .linkcheck:hover {
                color: #3099f3 !important;
            }
    </style>
</head>
<body>
    <div class="se-pre-conloadr">
        <img src="images/loader.gif" style="margin: 10% auto;" alt="" />
    </div>
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
                        <div class="form-sec ">
                            <div class="form-header">
                                <a href="ApplicationDetails.aspx" title="Drafted Proposals" class="pull-right proposalbtn active">Application Details</a>
                                <a href="DepartmentClearance.aspx" title="Drafted Proposals" class="pull-right proposalbtn">Apply Service</a>
                                <a href="DraftedServices.aspx" title="Drafted Services" class="pull-right proposalbtn ">Draft Services</a>
                                <h2>Application Details
                                <%--(Code-Industry Name)--%></h2>
                            </div>
                            <div class="form-body minheight350">
                                <div class="search-sec">
                                    <div class="form-group" id="divUnitName" runat="server">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Select Unit
                                            </label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Investor_Unit" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DrpDwn_Investor_Unit_SelectedIndexChanged"
                                                    ToolTip="Select unit to view Proposal details for specific unit !!">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <div class="row">
                                            <label class="col-md-2 col-sm-2">
                                                Department
                                            </label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-md-2 col-sm-2">
                                                Application No.</label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="ddlApplicationNo" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                                <span class="apply ">
                                                    <asp:Label ID="lblApply" runat="server" Text="Apply" Visible="false"></asp:Label>
                                                    <asp:Button ID="btnFilter" runat="server" Text="Search" CssClass="btn btn-success"
                                                        Width="80" OnClick="btnFilter_Click" OnClientClick="return validate();" />
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive" id="divGrd" style="margin-top: 15px;">
                                    <h4 class="margin-top10 margin-bottom10 text-red">Internal Services</h4>
                                    <div class="pageResult text-right">
                                        <i class="fa fa-list" aria-hidden="true" id="icon" runat="server"></i>
                                        <asp:LinkButton ID="lbtnAll" Visible="true" runat="server" Text="All" OnClick="lbtnAll_Click"></asp:LinkButton>
                                        &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                    </div>
                                    <asp:GridView ID="gvApplicationDetails" runat="server" CssClass="table table-bordered"
                                        AllowPaging="True" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                                        DataKeyNames="str_checkStatus,intServiceId,str_UlbCode,strProposalId,strCertificateFilename,Str_NocFileName,intStatus,ESIGNSTATUS"
                                        CellPadding="4" GridLines="None" OnRowDataBound="gvApplicationDetails_RowDataBound"
                                        OnPageIndexChanging="gvApplicationDetails_PageIndexChanging" OnRowCommand="gvApplicationDetails_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkBxSelect" runat="server" />
                                                    <%--  <asp:HiddenField ID="Hid_HOA_Count" runat="server" Value='<%# Eval("intHOACount") %>' />--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sl#.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsl" runat="server" Text='<%#(gvApplicationDetails.PageIndex * gvApplicationDetails.PageSize) + (gvApplicationDetails.Rows.Count + 1)%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department Name" HeaderStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartmentName" runat="server" Text='<%# Eval("str_Department") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="150px"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceName" runat="server" Text='<%# Eval("str_ServicesName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Applicant Name" HeaderStyle-Width="140px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplicantName" runat="server" Text='<%# Eval("str_ApplicantName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="140px"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Application No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplicationNo" runat="server" Text='<%# Eval("str_ApplicationNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Submitted On">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubmitedOn" runat="server" Text='<%# Eval("Requestdate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Query Status">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTextVal" runat="server" Value='<%# Eval("str_ApplicationNo")%>'></asp:HiddenField>
                                                    <asp:HiddenField ID="hdnservice" runat="server" Value='<%# Eval("intServiceId")%>'></asp:HiddenField>
                                                    <asp:HiddenField ID="hdnremark" runat="server" Value='<%# Eval("str_ApplicationStatus")%>'></asp:HiddenField>
                                                    <asp:LinkButton ID="lnkQuery" Text="Responded Query" CommandName="cmdQueryStatus"
                                                        OnClientClick="setvaluesOfrow(this);" Visible="false" runat="server" class="label-warning label label-default"
                                                        data-toggle="modal" data-target="#myModal"></asp:LinkButton>
                                                    <asp:HyperLink ID="hyprQuery" runat="server" Text='<%#Eval("str_QueryStatus") %>'
                                                        Target="_blank"></asp:HyperLink>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("str_QueryStatus") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("intQueryStatus") %>' Visible="false"></asp:Label>
                                                    <%--<button type="button" class="btn btn-success"  data-toggle="modal" data-target="#myModal">Revert Query</button>--%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Detail">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="btnPaymentStatus" Visible="false" Text="Pay Now" Class="btn btn-success btn-sm"
                                                        runat="server">Pay Now</asp:HyperLink>
                                                    <asp:LinkButton ID="btnPaymentStatus1" runat="server" Visible="false" Text="Pay Now"
                                                        Class="btn btn-success btn-sm"></asp:LinkButton>
                                                    <%--   <asp:Button ID="btnPaymentStatus" runat="server" Visible="false" Text="Pay Now" Class="btn btn-success btn-sm">
                                                </asp:Button>--%>
                                                    <asp:Label ID="lblpaymentStatus" runat="server" Text='<%#Eval("intPaymentStatus") %>'
                                                        Visible="false" CssClass="label label-default"></asp:Label>
                                                    <asp:Label ID="lblpaymentAmount" runat="server" Text='<%#Eval("Dec_Amount") %>' Visible="false"></asp:Label>
                                                    <asp:HiddenField ID="Hid_App_Fee" runat="server" Value='<%# Eval("decAppFee") %>' />
                                                    <asp:LinkButton ID="lnkLstTrnsctionId" runat="server" Visible='<%#(Convert.ToBoolean(Eval("vchType"))) %>'
                                                        Text="Transaction Details" data-toggle="modal" data-target='<%# "#o"+Eval("str_ApplicationNo") %>'></asp:LinkButton>
                                                    <div class="modal fade" id='<%# "o"+Eval("str_ApplicationNo") %>' tabindex="-1" role="dialog"
                                                        aria-hidden="true">
                                                        <div class="modal-dialog modal-lg">
                                                            <!-- Modal content-->
                                                            <div class="modal-content modal-primary ">
                                                                <div class="modal-header">
                                                                    <button type="button" class="close" data-dismiss="modal">
                                                                        &times;</button>
                                                                    <h4 class="modal-title">Transactional Details</h4>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading">
                                                                                    Successfull Transaction
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div id="OrderList" runat="server">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading">
                                                                                    Failure Transaction
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div id="OrderList1" runat="server">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                                                        Close</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Application Status (Last updated) " HeaderStyle-Width="160px">
                                                <ItemTemplate>
                                                    <%-- <asp:HyperLink ID="btnCertificate" Target="_blank" runat="server" ><img src="images/CheckStatus.png" /></asp:HyperLink>--%>
                                                    <div class="statusdetails">
                                                        <asp:Label ID="lblappstatsVal" runat="server" CssClass="appstatus" Text='<%#Eval("strStatus") %>'> </asp:Label>
                                                        <small>
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("UpdatedOn") %>'> </asp:Label></small>
                                                        &nbsp;
                                                    <asp:LinkButton ID="btnCertificate" runat="server" CommandName="cmdcheckStatus" CommandArgument='<%#DataBinder.Eval(Container,"rowindex") %>'
                                                        CssClass="linkcheck"><i class="fa fa-refresh"></i></asp:LinkButton>
                                                    </div>
                                                    <asp:HyperLink ID="btnservicelink" Target="_blank" runat="server" Visible="false" Text='<%# Eval("str_CorrectionRemark") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle Width="160px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View Detail">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="btnDetail" Target="_blank" runat="server" Visible="false" CssClass="btn btn-info btn-sm"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>
                                                    <asp:Label ID="lblDetail" runat="server" Text="NA" Visible="false" CssClass="label label-default"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Download Certificate">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="btnNoc" CssClass="btn btn-primary btn-sm" Target="_blank" runat="server"
                                                        Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                    <asp:HyperLink ID="btnDownload" CssClass="btn btn-primary btn-sm" Target="_blank"
                                                        runat="server" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("strCertificateFilename") %>'
                                                        Visible="false" CssClass="label label-default"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Certificate Download" Visible="False">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="downLoadCertficate" CssClass="btn btn-primary btn-sm" Target="_blank"
                                                        runat="server" Visible="false"><i class="fa fa-download"></i></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("str_CorrectionRemark") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-grid no-print" />
                                    </asp:GridView>
                                    <div>
                                        <asp:Button ID="BtnPayMultipe" runat="server" Text="Pay Now" CssClass="btn btn-danger" OnClick="BtnPayMultipe_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc3:footer ID="footer" runat="server" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-md">
                <!-- Modal content-->
                <div class="modal-content modal-primary ">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Application No.<asp:Label ID="lblProposalNo" runat="server" Text=""></asp:Label>
                            Query Details</h4>
                    </div>
                    <div class="modal-body">
                        <div class="old-querydetails">
                            <div class="form-group">
                                <div class="row" id="divResults">
                                </div>
                            </div>
                            <div class="form-group" id="divA1">
                                <div class="row">
                                    <label class="col-sm-3">
                                        Response Details</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtA1" onkeyup="setvalue();" runat="server" class="form-control"
                                            TextMode="MultiLine" Onkeypress="return inputLimiter(event,'Address')" name="q17"
                                            MaxLength="1000"></asp:TextBox>
                                        <div id="1stCnt" style="display: none" class="text-red">
                                            <i>Maximum <span id="charsLeft" class="mandatoryspan">1000</span> characters left</i>
                                            *
                                        </div>
                                        <%--<label class="bindlabel" id="lblAns1">
                                    </label>--%>
                                        <asp:HiddenField ID="hdnProposalno" runat="server" />
                                        <asp:HiddenField ID="hdnservice" runat="server" />
                                        <asp:HiddenField ID="hdnQueryCnt" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="divF1">
                                <div class="row" id="DvFile1">
                                    <label class="col-sm-3">
                                        Files</label>
                                    <div class="col-sm-9">
                                        <span style="width: 200px" class="mandatoryspan pull-right">(Only pdf files are allowed,
                                        Max Size 4 MB) </span>
                                        <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" Width="150px" />
                                        <asp:FileUpload ID="FileUpload2" CssClass="form-control" runat="server" Width="150px" />
                                        <asp:FileUpload ID="FileUpload3" CssClass="form-control" runat="server" Width="150px" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="dvsubmit">
                                <div class="row">
                                    <div class="col-sm-offset-3 col-sm-9">
                                        <%--<input type="submit" value="Submit" class="btn btn-success"/>--%>
                                        <asp:Button ID="btnSubmit" class="btn btn-success" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                            OnClientClick="return validate();" />
                                        <input type="reset" value="Reset" class="btn btn-danger" style="display: none" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="loader">
        </div>
        <script src="js/modernizr.js" type="text/javascript"></script>
    </form>
</body>
</html>