<%--'*******************************************************************************************************************
' File Name         : Proposals.aspx
' Description       : Show the list of all approved and pending proposals.
' Created by        : AMit Sahoo
' Created On        : 30th June 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProposalsNew.aspx.cs" Inherits="ProposalsNew" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                            $('.Dv1').hide();
                            $('.Dv2').hide();

                        });
                    },
                    error: function (response) {
                        var msg = jQuery.parseJSON(response.responseText);
                        alert("Message: " + msg.Message + "<br /> StackTrace:" + msg.StackTrace + "<br /> ExceptionType:" + msg.ExceptionType);
                    }
                });
            });



        });
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
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
                // alert('Please Upload PDF file Only!');
                jAlert('<strong>Please Upload PDF file Only !</strong>', projname);
                return false;
            }
            else if (z.files[0].size > 4 * 1024 * 1024) {
                //                alert('The file size can not exceed 4MB.');
                //                document.getElementById(Controlname).focus();
                //                return false;
                jAlert('<strong>The file size can not exceed 4MB. !</strong>', projname);
                return false;
            }
            else
                return true;
        }
        function validate(obj) {
            debugger;
            var ID = obj.id;
            var arr = ID.split('_');

            //            if (document.getElementById('hdnQueryCnt').value == "1") {
            //            if (blankFieldValidation($('#gvProposal_txtA1_' + arr[2]), 'Initial Response Details', projname) == false) {
            //                    return false;
            //                }
            if ($('#gvProposal_txtA1_' + arr[2]).val() == "") {
                jAlert('<strong>Respond Details cannot be left blank!</strong>', projname);
                $('#gvProposal_txtA1_' + arr[2]).focus();
                return false;
            }
            //            }
            //            if (document.getElementById('hdnQueryCnt').value == "3") {
            //                if (blankFieldValidation('txtA2', 'Second Set Of Response Details', projname) == false) {
            //                    return false;
            //                }
            //            }
            if ($('#gvProposal_FileUpload1_' + arr[2]).val() != "") {
                if (!DocValid('#gvProposal_FileUpload1_' + arr[2]))
                { return false; }
            }
            //            if (document.getElementById('FileUpload2').value != "") {
            //                if (!DocValid('FileUpload2'))
            //                { return false; }
            //            }
            //            if (document.getElementById('FileUpload3').value != "") {
            //                if (!DocValid('FileUpload3'))
            //                { return false; }
            //            }
            //            if (document.getElementById('FileUpload4').value != "") {
            //                if (!DocValid('FileUpload4'))
            //                { return false; }
            //            }
            //            if (document.getElementById('FileUpload5').value != "") {
            //                if (!DocValid('FileUpload5'))
            //                { return false; }
            //            }
            //            if (document.getElementById('FileUpload6').value != "") {
            //                if (!DocValid('FileUpload6'))
            //                { return false; }
            //            }
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
            $('#charsLeft').html(1000 - $('#gvProposal_txtA1_' + arr[2]).val().length);

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
   <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        
        .modalPopup
        {
            background-color: #fbfbfb;
            border: 3px solid #b93607;
            margin: 0px;
        }
        .modalPopup .mhead
        {
            padding: 5px 15px;
            border-bottom: 1px solid #ccc;
            background: #b93607;
            color: #fff;
        }
        .modalPopup .mhead h4
        {
            display: inline-block;
        }
        .modalPopup .mhead a
        {
            float: right;
            color: #fff;
            text-decoration: none;
        }
        .modalPopup .mbody
        {
            padding: 30px 15px;
        }
        .radiodiv
        {
            padding: 10px 0px 20px;
        }
        .Confdiv
        {
            padding: 25px 120px 20px;
        }
        
        #PanelIdco h4
        {
            font-size: 17px;
            font-weight: bold;
            padding-bottom: 12px;
        }
        .radio-inline label
        {
            display: inline-block;
            padding-right: 20px;
            padding-left: 12px;
        }
        .reglogin
        {
            padding: 25px;
        }
        .reglogin p
        {
            text-align: justify;
        }
        .reglogin a
        {
            color: #0088cc;
            text-decoration: none;
        }
        .reglogin a:hover
        {
            color: #159f45;
        }
        .popBox
        {
            position: absolute;
            -webkit-box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            -moz-box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            background: #fffdef;
            padding: 8px;
            border: 1px solid #ddd;
            width: 93%;
            left: 15px;
            font-size: 14px !important;
        }
        #pop-up
        {
            top: 120px;
        }
        #pop-up1
        {
            top: 120px;
        }
        #pop-up2
        {
            top: 100px;
        }
        .row
        {
            margin-left: -15px;
            margin-right: 0;
        }
        .navbar-inverse
        {
            background-color: none !important;
            border-color: none !important;
        }
        .portlet-sec
        {
            margin: 16px 15px 8px;
            padding: 5px;
            border-radius: 2px;
        }
        .portlet-sec h3
        {
            text-transform: uppercase;
            font-size: 20px;
        }
        .portlet-sec h3 span
        {
            font-weight: 600;
            color: #ac2d00;
            padding: 0px 4px;
        }        
    </style>
</head>
<body>
    
    <form id="form2" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
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
                                    <a href="DraftedProposals.aspx" title="Draft Proposals" class="pull-right proposalbtn active">
                                        Draft Proposals</a> <a href="ProposalInstruction.aspx" title="Create Proposal (PEAL)"
                                            class="pull-right proposalbtn active">Create Proposal(PEAL)</a> <a href="Proposals.aspx"
                                                title="View Proposal" class="pull-right proposalbtn ">View Proposal</a>
                                    <h2>
                                        Proposals</h2>
                                </div>
                                <div class="form-body minheight350">
                                    <div class="form-group" id="divUnitName" runat="server">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Select Unit
                                            </label>
                                            <div class="col-sm-5">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Investor_Unit" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DrpDwn_Investor_Unit_SelectedIndexChanged"
                                                    ToolTip="Select unit to view Proposal details for specific unit !!">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="table-responsive ">
                                            <asp:GridView ID="gvProposal" runat="server" CssClass="table table-bordered bg-white"
                                                AllowPaging="true" PageSize="10" AutoGenerateColumns="False" EmptyDataText="No records found"
                                                OnPageIndexChanging="gvProposal_PageIndexChanging" CellPadding="4" GridLines="None"
                                                DataKeyNames="strProposalNo,strPEALCertificate,intQueryStatus,intExtendedStatus,strRemarks"
                                                OnRowDataBound="gvProposal_RowDataBound">
                                                <AlternatingRowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Proposal No.">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="Label2" ></asp:Label>--%>
                                                            <asp:HyperLink ID="hypLink" runat="server" Text='<%# Eval("strProposalNo") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="9%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name of the Company/Enterprise">
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
                                                    <asp:TemplateField HeaderText="Download">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnPEALFile" runat="server" />
                                                            <asp:HyperLink ID="hplnkPEALCerti" runat="server" Target="_blank" ToolTip="Download PEAL Certificate"> <i class="fa fa-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Query" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnTextVal" runat="server" Value='<%# Eval("strProposalNo")%>'>
                                                            </asp:HiddenField>
                                                            <asp:LinkButton ID="LinkButton1" Text="Respond Query" runat="server" class="btn btn-success btn-sm"
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
                                                                                            Respond Details</label>
                                                                                        <div class="col-sm-9">
                                                                                            <asp:TextBox ID="txtA1" onkeyup="setvalue(this);" runat="server" class="form-control"
                                                                                                TextMode="MultiLine" Onkeypress="return inputLimiter(event,'Address')" name="q17"
                                                                                                MaxLength="1000"></asp:TextBox>
                                                                                            <div id="1stCnt" class="text-red">
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
                                                                                        runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validate(this);" />
                                                                                    <input type="reset" value="Reset" class="btn btn-danger" style="display: none" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            </div> </div> </div>
                                                            <%--<button type="button" class="btn btn-success"  data-toggle="modal" data-target="#myModal">Revert Query</button>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--Added By Pranay Kumar for Addition of Query Details on 14-Sept-2017--%>
                                                    <%--<asp:BoundField DataField="strRemarks" HeaderText="Query Status" />--%>
                                                    <asp:TemplateField HeaderText="View Query Detail">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypQueryDtls" runat="server"></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <%--Ended By Pranay Kumar for Addition of Query Details on 14-Sept-2017--%>
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
                                                                                    <div class="row margin-bottom10 Dv1" runat="server">
                                                                                        <label class="col-sm-3 col-sm-offset-2">
                                                                                            Service Name</label>
                                                                                        <div class="col-sm-5">
                                                                                            <span class="colon">:</span>
                                                                                            <asp:DropDownList ID="ddlServiceName" CssClass="clsService form-control ss" runat="server">
                                                                                            </asp:DropDownList>
                                                                                            <asp:HiddenField ID="hdnServiceid" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row margin-bottom10 Dv2" runat="server">
                                                                                        <label class="col-sm-3 col-sm-offset-2">
                                                                                            Service Type</label>
                                                                                        <div class="col-sm-5">
                                                                                            <span class="colon">:</span>
                                                                                            <asp:DropDownList ID="ddlServiceType" CssClass="clsServiceType form-control" runat="server">
                                                                                            </asp:DropDownList>
                                                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                                        </div>
                                                                                    </div>
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
                                                                                    <p class="text-red text-center">
                                                                                        <small>*If already paid through online,click on 'Payment Confirmation' button otherwise
                                                                                            click on 'Pay Now' button to proceed for Payment</small></p>
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
                                                                                            <asp:Button ID="btnModalSubmit" runat="server" Text="Pay Now" CssClass="btn btn-success btn-sm clssave"
                                                                                                data-target="#Divshow" OnClick="btnModalSubmit_Click" />
                                                                                            <asp:Button ID="Button2" runat="server" Text="Payment Confirmation" PostBackUrl="PaymentConfirmation.aspx"
                                                                                                CssClass="btn btn-success btn-sm" />
                                                                                            <asp:Button ID="Button3" runat="server" Text="Cancel" PostBackUrl="Proposals.aspx"
                                                                                                data-toggle="modal" CssClass="btn btn-success btn-sm" />
                                                                                            <br />
                                                                                            <br />
                                                                                            <%--<p class="text-red ">
                                                                                                N.B: Avoid multiple payment for a single request</p>--%>
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
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Red" Font-Italic="true" />
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
                                    <%--POPPUP FOR UPDATE INFO--%>
                                     <asp:HiddenField ID="hdnIndId" runat="server" />
                                     <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe" runat="server"
        PopupControlID="pnlProfile" TargetControlID="hdnIndId" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlProfile" runat="server" CssClass="modalPopup" Style="display: none;
        width: 800px; height:385px">
        <div class="mhead">          
        <asp:LinkButton ID="Linkclose" runat="server" OnClick="Linkclose_Click"><i class="fa fa-close" onclick='return check()'></i></asp:LinkButton>               
          
            <h4>
                Update Information</h4>
        </div>
      
        <div class="mbody">
              <div class="form-section"> 
                  <div class="row">
                  <div class="col-sm-4">
                      <label>Name of the Contact Person   </label>                      
                     </div>
                       <div class="col-sm-4">
                           <asp:TextBox ID="txtContactPersn" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Name" 
                               ControlToValidate="txtContactPersn" ValidationGroup="a" ForeColor="Red" SetFocusOnError="true">
                           </asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Name." 
                             ControlToValidate="txtContactPersn" ValidationExpression="^[a-zA-Z ]+$" 
                             ValidationGroup="a" ForeColor="Red" SetFocusOnError="true" />
                           </div>
                      </div>
                <br />
                  <div class="row">
                  <div class="col-sm-4">
                      <label>E-Mail address of Contact Person  </label>
                    
                     </div>
                      <div class="col-sm-4">
                           <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Email" 
                               ControlToValidate="txtEmailId" ValidationGroup="a" ForeColor="Red" SetFocusOnError="true">
                           </asp:RequiredFieldValidator>
                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                            TargetControlID="txtEmailId" FilterMode="ValidChars" ValidChars="@._-" 
                              FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers">
                              </cc1:FilteredTextBoxExtender>
                           <asp:RegularExpressionValidator ID="validateEmail" runat="server" ErrorMessage="Invalid email." 
                               ControlToValidate="txtEmailId" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" 
                               ValidationGroup="a" ForeColor="Red" SetFocusOnError="true" />
                          </div>
                      </div>
                  <br />
                   <div class="row">
                  <div class="col-sm-4">
                      <label>Mobile Number of Contact Person  </label>                     
                     </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Mobile Number" 
                                 ControlToValidate="txtMobileNo" ValidationGroup="a" ForeColor="Red" SetFocusOnError="true">
                           </asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                   TargetControlID="txtMobileNo" InvalidChars="!<>%">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                      </div>
                  <br />
                  <div style="text-align: center; margin-right: 183px;">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success" ValidationGroup="a" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnHide" runat="server" Text="Skip" CssClass="btn btn-danger" OnClick="btnHide_Click" />
                  </div>
                </div>
            </div>
    </asp:Panel>
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
