<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublicDashBoardPeal.aspx.cs"
    Inherits="PublicDashBoardPeal" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <title>SWP(Single Window Portal)</title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src='//code.jquery.com/jquery-1.8.3.js'></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/css/bootstrap-datepicker3.min.css" />
    <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
        });

        function setDateValues(strFromDate, strToDate) {
            var appendId = "ContentPlaceHolder1_";
            var fromDate = $("#" + appendId + "txtFromDate").val();
            var toDate = $("#" + appendId + "txtToDate").val();
            $("#" + appendId + "txtFromDate").datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            }).datepicker("setDate", fromDate);
            $("#" + appendId + "txtToDate").datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            }).datepicker("setDate", toDate);
        }


        function ValidatePage() {
            var fDate = $("#txtFromDate").val();
            var tDate = $("#txtToDate").val();
            if (fDate == null || fDate == undefined || fDate == '') {
                jAlert('<strong>Please select from date.</strong>', 'GO-SWIFT');
                return false;
            }
            if (tDate == null || tDate == undefined || tDate == '') {
                jAlert('<strong>Please select to date.</strong>', 'GO-SWIFT');
                return false;
            }
            var dtmFromDate = new Date(GetDate(fDate));
            var dtmToDate = new Date(GetDate(tDate));

            if (dtmFromDate > dtmToDate) {
                jAlert('<strong>From date cannot be greater than to date.</strong>', 'GO-SWIFT');
                return false;
            }
            else {
                return true;
            }

        }

        function GetDate(str) {
            var arr = str.split('-');
            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
            var i = 1;
            for (i; i <= months.length; i++) {
                if (months[i] == arr[1]) {
                    break;
                }
            }
            var formatddate = i + '/' + arr[0] + '/' + arr[2];
            return formatddate;
        }
    </script>
    <style>
        .panel-default > .panel-heading
        {
            padding: 8px 15px !important;
            font-size: 18px;
            text-transform: uppercase;
        }
        
        .note
        {
            border: 1px solid #f0f0f0;
            padding: 15px;
            border-radius: 4px;
            background: #f9f9f9;
        }
        
        .note ol
        {
            margin-bottom: 0;
        }
        
        .note ol li
        {
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="content-form-section">
            <div class="col-sm-12">
                <div class="aboutcontent-sec">
                    <h2 class=" margin-bottom15">
                        PEAL Dashboard
                    </h2>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Search Details
                        </div>
                        <div class="panel-body">
                            <div class="form-group row NOPRINT">
                                <asp:UpdatePanel ID="up1" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-3">
                                            <label for="Country">
                                                District</label>
                                            <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlDistrict" runat="server">
                                                <asp:ListItem Value="0">---Select---</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-sm-3">
                                    <label for="State">
                                        From Date
                                    </label>
                                    <div class="input-group  date datePicker">
                                        <asp:TextBox runat="server" class="form-control" ID="txtFromDate" name="txtFromDate"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <label for="State">
                                        To Date
                                    </label>
                                    <div class="input-group  date datePicker">
                                        <asp:TextBox runat="server" class="form-control" ID="txtToDate"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <label for="State">
                                        Investment Amount
                                    </label>
                                    <asp:DropDownList ID="drpInvestmentAmt" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <label for="State">
                                        Sector of activity</label>
                                    <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row NOPRINT">
                                <div class="col-sm-3">
                                    <asp:Button ID="btnSearch" Style="margin-top: 22px" CssClass="btn btn-success" runat="server"
                                        Text="Search" OnClick="btnSearch_Click" OnClientClick="return ValidatePage();">
                                    </asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:Label ID="lblSearchDetails" runat="server"></asp:Label>
                        <asp:GridView ID="grdPealDetails" runat="server" class="table table-bordered table-hover"
                            AutoGenerateColumns="false" EmptyDataText="No Record(s) found...." ShowFooter="true"
                            OnRowDataBound="grdPealDetails_RowDataBound" DataKeyNames="intDistrictId">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <%#Eval("strDistrictName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Balance" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                                    Visible="false">
                                    <ItemTemplate>
                                        <%#Eval("cnt_CarryFwd_pending")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblCarryFwdPendingFooter" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application Received" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("cnt_Total")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblRcvdFooter" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("cnt_Approved")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblApprovedFooter" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rejected" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("cnt_rejected")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblRejectedFooter" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deferred" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                                    Visible="false">
                                    <ItemTemplate>
                                        <%#Eval("cnt_deferred")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblDefferecFooter" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. under query" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                                    Visible="false">
                                    <ItemTemplate>
                                        <%#Eval("cnt_Query")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblQuery1Footer" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pending" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("cnt_Pending")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblPendingFooter" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Application Pending" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%#Eval("int_Total_Pending")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalPendingFooter" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application Pending Beyond 30 days" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%#Eval("cnt_Total_ORTPSAtimeline")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblORTPSFooter" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Proposed Employment" DataField="cnt_Proposed_Emp" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="Right" Visible="false" />
                                <asp:BoundField HeaderText="Proposed Investment (in INR Lakh.)" DataField="total_Capital_Investment"
                                    FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" Visible="false" />
                                <asp:BoundField HeaderText="Avg. no of days for disposal" DataField="cnt_AvgDaysApproval"
                                    FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="Land Assessments Done" DataField="cnt_landAssessment"
                                    FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="Land Allotment Completed" DataField="cnt_landAllotment"
                                    FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="Avg. No. of days for Land Allotment (Mean)" DataField="cnt_AvgDaysAllotment"
                                    FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="Land allotment application pending beyond ORTPSA timeline"
                                    DataField="cnt_Land_Allotment_ORTPSA" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="Median" DataField="cnt_median" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderText="ORTPSA Timeline">
                                    <ItemTemplate>
                                        <%#Eval("intORTPSAtimeline")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Minimum Days Taken for Allotment">
                                    <ItemTemplate>
                                        <%#Eval("intMinApprovalDays")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Maximum Days Taken for Allotment">
                                    <ItemTemplate>
                                        <%#Eval("intMaxApprovalDays")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                 
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
