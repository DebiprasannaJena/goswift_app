<%--'*******************************************************************************************************************
' File Name         : Proposals.aspx
' Description       : Show the list of all approved and pending proposals.
' Created by        : AMit Sahoo
' Created On        : 30th June 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Proposals.aspx.cs" Inherits="Proposals" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.menuproposal').addClass('active');
            $(".clstest").click(function () {
                debugger;
                $.ajax({
                    type: "POST",
                    url: "Proposals.aspx/ServiceDetail",
                    data: '{"id":"' + $('.clsService').val() + '","Tid":"' + $('.clsServiceType').val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $.each(r.d, function () {

                            $('#hdnAccountHead').val(r.d[0].vchAccountHead);
                            $('.clsAmount').text(r.d[0].decPaymentAmt);
                            $('#hdnTest').val(r.d[0].decPaymentAmt);
                            $('#hdnDes').val(r.d[0].vchDescription);
                            $('.clsDesc').text(r.d[0].vchDescription);
                        });
                    },
                    error: function (response) {
                        var msg = jQuery.parseJSON(response.responseText);
                        alert("Message: " + msg.Message + "<br /> StackTrace:" + msg.StackTrace + "<br /> ExceptionType:" + msg.ExceptionType);
                    }
                });
            });

            $((this).closest("tr").find("#btnModalSubmit")).click(function () {
                debugger;
                alert('x');
            });

        });
        function DocValid(Controlname) {
            debugger;
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
        function validate() {
            var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
            if (document.getElementById('hdnQueryCnt').value == "1") {
                if (blankFieldValidation('txtA1', 'Initial Response Details', projname) == false) {
                    return false;
                }
            }
            if (document.getElementById('hdnQueryCnt').value == "3") {
                if (blankFieldValidation('txtA2', 'Second Set Of Response Details', projname) == false) {
                    return false;
                }
            }
            if (document.getElementById('FileUpload1').value != "") {
                if (!DocValid('FileUpload1'))
                { return false; }
            }
            if (document.getElementById('FileUpload2').value != "") {
                if (!DocValid('FileUpload2'))
                { return false; }
            }
            if (document.getElementById('FileUpload3').value != "") {
                if (!DocValid('FileUpload3'))
                { return false; }
            }
            if (document.getElementById('FileUpload4').value != "") {
                if (!DocValid('FileUpload4'))
                { return false; }
            }
            if (document.getElementById('FileUpload5').value != "") {
                if (!DocValid('FileUpload5'))
                { return false; }
            }
            if (document.getElementById('FileUpload6').value != "") {
                if (!DocValid('FileUpload6'))
                { return false; }
            }
            var r = confirm("Are you sure you want to submit!");
            if (r == true) {
                return true;
            } else {

                return false;
            }
        }
        function setvaluesOfrow(flu) {
            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 1;
            debugger;
            // alert(rows);
            //document.getElementById('txtans').value = document.getElementById('gvProposal_hdnTextVal_' + rows).value;

            document.getElementById('lblProposalNo').innerHTML = document.getElementById('gvProposal_hdnTextVal_' + rows).value;
            document.getElementById('hdnProposalno').value = document.getElementById('gvProposal_hdnTextVal_' + rows).value;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Proposals.aspx/ShowQuery",
                data: '{"id":"' + document.getElementById('lblProposalNo').innerHTML + '"}',
                dataType: "json",
                success: function (r) {
                    document.getElementById('hdnQueryCnt').value = r.d.length;
                    $.each(r.d, function () {
                        document.getElementById('1stCnt').style = "display:none";
                        document.getElementById('2ndCnt').style = "display:none";
                        document.getElementById('divF1').style = "display:none";

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
                                    document.getElementById('hlDoc1').href = "QueryFiles/" + strarr[0];
                                    document.getElementById('divViewFiles1').style = "block";
                                }
                                else { document.getElementById('hlDoc1').style = "display:none"; }
                                if (strarr[1] != "") {
                                    document.getElementById('hlDoc2').href = "QueryFiles/" + strarr[1];
                                    document.getElementById('divViewFiles1').style = "block";
                                }
                                else { document.getElementById('hlDoc2').style = "display:none"; }
                                if (strarr[2] != "") {
                                    document.getElementById('hlDoc3').href = "QueryFiles/" + strarr[2];
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
            $('#charsLeft').html(1000 - $('#ContentPlaceHolder1_gvProposal_txtA1_' + arr[3]).val().length);

        }

        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

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
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#-,./;\'';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
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

    </script>
</head>
<body>
    <form id="form2" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:investoemenu ID="ineste" runat="server" />
                    </div>
                    <div class="tab-content clearfix">
                        <div class="tab-pane active" id="1a">
                            <div class="form-sec">
                                <div class="form-header">
                                    <a href="DraftedProposals.aspx" title="Drafted Proposals" class="pull-right proposalbtn">
                                        Drafted Proposals</a> <a href="PEAL/PromoterDetails.aspx" title="Create Proposal"
                                            class="pull-right proposalbtn">Create Proposal</a>
                                    <h2>
                                        Proposals</h2>
                                </div>
                                <div class="form-body minheight350">
                                    <div class="form-group">
                                        <div class="table-responsive ">
                                            <asp:GridView ID="gvProposal" runat="server" CssClass="table table-bordered bg-white"
                                                AllowPaging="true" PageSize="10" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found"
                                                OnPageIndexChanging="gvProposal_PageIndexChanging" CellPadding="4" GridLines="None"
                                                DataKeyNames="strProposalNo,strPEALCertificate,intQueryStatus,intExtendedStatus"
                                                OnRowDataBound="gvProposal_RowDataBound">
                                                <AlternatingRowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Proposal No.">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="Label2" ></asp:Label>--%>
                                                            <asp:HyperLink ID="hypLink" runat="server" Text='<%# Eval("strProposalNo") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name Of Company/Enterprises">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("strActionTakenBY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Industry Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("strQuerytype") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("strStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ActionAuthority" HeaderText="Action Taking Authority" />
                                                    <asp:BoundField DataField="dtmCreatedOn" HeaderText="Application Date" />
                                                    <asp:TemplateField HeaderText="Download" HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnPEALFile" runat="server" />
                                                            <asp:HyperLink ID="hplnkPEALCerti" runat="server" Target="_blank" ToolTip="Download PEAL Certificate">
                                                  <i class="fa fa-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Query">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnTextVal" runat="server" Value='<%# Eval("strProposalNo")%>'>
                                                            </asp:HiddenField>
                                                            <asp:LinkButton ID="LinkButton1" Text="Revert Query" runat="server" class="btn btn-success btn-sm"
                                                                data-toggle="modal" data-target='<%# "#"+Eval("strProposalNo")%>'></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnExtend" runat="server" CommandArgument='<%# Eval("strProposalNo") %>'
                                                                CssClass="btn btn-success" OnClick="lbtnExtend_Click">Extent</asp:LinkButton>
                                                            <div id='<%# Eval("strProposalNo")%>' class="modal fade" role="dialog">
                                                                <div class="modal-dialog modal-md">
                                                                    <!-- Modal content-->
                                                                    <div class="modal-content modal-primary ">
                                                                        <div class="modal-header">
                                                                            <button type="button" class="close" data-dismiss="modal">
                                                                                &times;</button>
                                                                            <h4 class="modal-title">
                                                                                Proposal No.<asp:Label ID="lblProposalNo" runat="server" Text=""></asp:Label>
                                                                                Query Details</h4>
                                                                        </div>
                                                                        <div class="modal-body">
                                                                            <div class="old-querydetails">
                                                                                <div id="Div1" class="form-group" runat="server">
                                                                                    <div id="QueryHist" runat="server">
                                                                                    </div>
                                                                                    <div class="clearfix">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group" id="divA1">
                                                                                    <div class="row">
                                                                                        <label class="col-sm-3">
                                                                                            Response Details</label>
                                                                                        <div class="col-sm-9">
                                                                                            <asp:TextBox ID="txtA1" onkeyup="setvalue(this);" runat="server" class="form-control"
                                                                                                TextMode="MultiLine" Onkeypress="return inputLimiter(event,'Address')" name="q17"
                                                                                                MaxLength="1000"></asp:TextBox>
                                                                                            <div id="1stCnt" style="display: none" class="text-red">
                                                                                                <i>Maximum <span id="charsLeft" class="mandatoryspan">1000</span> characters left</i>
                                                                                                *</div>
                                                                                            
                                                                                            <asp:HiddenField ID="hdnProposalno" runat="server" />
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
                                                                                            <%--<asp:FileUpload ID="FileUpload2" CssClass="form-control" runat="server" Width="150px" />
                                                                            <asp:FileUpload ID="FileUpload3" CssClass="form-control" runat="server" Width="150px" />--%>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <%-- <div class="form-group" id="divViewFiles1">
                                                                    <div class="row">
                                                                        <label class="col-sm-3">
                                                                            Files</label>
                                                                        <div class="col-sm-9">
                                                                            <asp:HyperLink ID="hlDoc1" runat="server" Target="_blank">
                                                                                <asp:Image ID="pdficon1" runat="server" ImageUrl="images/pdf.png" Height="25" Width="25" />
                                                                            </asp:HyperLink>
                                                                           <%-- <asp:HyperLink ID="hlDoc2" runat="server" Target="_blank">
                                                                                <asp:Image ID="pdficon2" runat="server" ImageUrl="images/pdf.png" Height="25" Width="25" /></asp:HyperLink>
                                                                            <asp:HyperLink ID="hlDoc3" runat="server" Target="_blank">
                                                                                <asp:Image ID="pdficon3" runat="server" ImageUrl="images/pdf.png" Height="25" Width="25" /></asp:HyperLink>--%>
                                                                            </div>
                                                                        </div>
                                                                        <div class="modal-footer">
                                                                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                                                                Close</button>
                                                                        </div>
                                                                        <div class="form-group" id="dvsubmit">
                                                                            <div class="row">
                                                                                <div class="col-sm-offset-3 col-sm-9">
                                                                                    <%--<input type="submit" value="Submit" class="btn btn-success"/>--%>
                                                                                    <asp:Button ID="btnSubmit" CommandArgument='<%# Eval("strProposalNo")%>' class="btn btn-success"
                                                                                        runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validate();" />
                                                                                    <input type="reset" value="Reset" class="btn btn-danger" style="display: none" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                            </div>
                                                            </div> </div> </div> </div>
                                                            <%--<button type="button" class="btn btn-success"  data-toggle="modal" data-target="#myModal">Revert Query</button>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Make Payment" HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <%--<asp:HiddenField ID="hdnTextVal" runat="server" Value='<%# Eval("strProposalNo")%>'>
                                                        </asp:HiddenField>--%>
                                                            <asp:HiddenField ID="hdnProposalNo11" runat="server" Value='<%# Eval("strProposalNo")%>'>
                                                            </asp:HiddenField>
                                                            <asp:HiddenField ID="hdnPaymentstatus" runat="server" Value='<%# Eval("intpaymentStatus")%>'>
                                                            </asp:HiddenField>
                                                            <asp:LinkButton ID="lnkMakePayment" Text="Make Payment" runat="server" class="btn btn-success btn-sm clstest"
                                                                data-toggle="modal" data-target='<%# "#P" + Eval("strProposalNo")%>'></asp:LinkButton>
                                                            <asp:Label ID="lblPaymentShow" runat="server" Text=""></asp:Label>
                                                            <div id='<%# "P"+Eval("strProposalNo")%>' class="modal fade" role="dialog">
                                                                <div class="modal-dialog modal-md">
                                                                    <!-- Modal content-->
                                                                    <div class="modal-content modal-primary ">
                                                                        <div class="modal-header">
                                                                            <button type="button" class="close" data-dismiss="modal">
                                                                                &times;</button>
                                                                            <h4 class="modal-title">
                                                                                Payment Details</h4>
                                                                        </div>
                                                                        <div class="modal-body">
                                                                            <div class="old-querydetails">
                                                                                <div class="form-group" id="div2">
                                                                                    <div class="row margin-bottom10">
                                                                                        <label class="col-sm-3 col-sm-offset-2">
                                                                                            Service Name</label>
                                                                                        <div class="col-sm-5">
                                                                                            <span class="colon">:</span>
                                                                                            <asp:DropDownList ID="ddlServiceName" CssClass="clsService form-control" runat="server">
                                                                                            </asp:DropDownList>
                                                                                            <asp:HiddenField ID="hdnServiceid" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row margin-bottom10">
                                                                                        <label class="col-sm-3 col-sm-offset-2">
                                                                                            Service Type</label>
                                                                                        <div class="col-sm-5">
                                                                                            <span class="colon">:</span>
                                                                                            <asp:DropDownList ID="ddlServiceType" CssClass="clsServiceType form-control" runat="server">
                                                                                            </asp:DropDownList>
                                                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <hr />
                                                                                    <%-- <div class="row">
                                                                                       
                                                                                        <label class="col-sm-3">
                                                                                            Amount</label>
                                                                                        <div class="col-sm-9">
                                                                                            
                                                                                            
                                                                                            <asp:HiddenField ID="hdnAmount" runat="server" />
                                                                                        </div>
                                                                                    </div>--%>
                                                                                    <%--<div class="row">
                                                                                        <label class="col-sm-3">
                                                                                            Account Head</label>
                                                                                        <div class="col-sm-9">
                                                                                      
                                                                                            <asp:Label ID="lblAccountHead" runat="server" CssClass="clsAccounthead" Text=""></asp:Label>
                                                                                        </div>
                                                                                    </div>--%>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12 text-center">
                                                                                            <p>
                                                                                                You are proceeding to pay</p>
                                                                                            <h4>
                                                                                                <asp:Label ID="lblDesc" runat="server" ForeColor="Red" CssClass="clsDesc" Text=""></asp:Label></h4>
                                                                                            <p>
                                                                                                for the request No.</p>
                                                                                            <h4>
                                                                                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h4>
                                                                                            <p>
                                                                                                of amount</p>
                                                                                            <h4>
                                                                                                Rs:<i class="fa fa-inr"></i>
                                                                                                <asp:Label ID="lblAmount" runat="server" CssClass="clsAmount"></asp:Label>
                                                                                                /-</h4>
                                                                                            <asp:Button ID="btnModalSubmit" runat="server" Text="Pay Now" data-toggle="modal"
                                                                                                CssClass="btn btn-success btn-sm clssave" data-target="#Divshow" OnClick="btnModalSubmit_Click" />
                                                                                            <%-- <asp:Button ID="Button2" runat="server" Text="Payment Confirmation" data-toggle="modal"
                                                                                                CssClass="btn btn-success btn-sm" />
                                                                                            <asp:Button ID="Button3" runat="server" Text="Cancel" data-toggle="modal" CssClass="btn btn-success btn-sm" />--%>
                                                                                            <br />
                                                                                            <br />
                                                                                            <p class="text-red ">
                                                                                                N.B: Avoid multiple payment for a single request</p>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <%-- <div class="modal-footer">
                                                                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                                                                Close</button>
                                                                        </div>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--<button type="button" class="btn btn-success"  data-toggle="modal" data-target="#myModal">Revert Query</button>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pagination-grid no-print" />
                                            </asp:GridView>
                                            <!-- Modal -->
                                            <div id="Divshow" class="modal fade" role="dialog">
                                                <div class="modal-dialog modal-md">
                                                    <!-- Modal content-->
                                                    <div class="modal-content modal-primary ">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal">
                                                                &times;</button>
                                                            <h4 class="modal-title">
                                                                Your Order No For this payment is<asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                                                Query Details</h4>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                                                Close</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hdnTest" runat="server" />
                                        <asp:HiddenField ID="hdnDes" runat="server" />
                                        <asp:HiddenField ID="hdnAccountHead" runat="server" />
                                    </div>
                                </div>
                            </div>
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
