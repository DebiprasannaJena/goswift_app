<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublicDashBoardService.aspx.cs"
    Inherits="PublicDashBoardService" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <title>SWP(Single Window Portal)</title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src='//code.jquery.com/jquery-1.8.3.js'></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/css/bootstrap-datepicker3.min.css">
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
            debugger;
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
            debugger;
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
            debugger;
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
                        Service Dashboard
                    </h2>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Search Details</div>
                        <div class="panel-body">
                            <div class="form-group row NOPRINT">
                                <asp:UpdatePanel ID="up1" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-3">
                                            <label for="Department">
                                                Department</label>
                                            <asp:DropDownList ID="drpDepartment" runat="server" AutoPostBack="true" CssClass="form-control"
                                                OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged">
                                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <label for="Service">
                                                Service</label>
                                            <asp:DropDownList ID="drpService" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
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
                            </div>
                            <div class="form-group row NOPRINT">
                                <div class="col-sm-3">
                                    <label for="Country">
                                        District</label>
                                    <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlDistrict" runat="server">
                                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <label for="x">
                                        <br />
                                    </label>
                                    <asp:Button ID="btnSearch" CssClass="btn btn-success" runat="server" Text="Search"
                                        OnClick="btnSearch_Click" OnClientClick="return ValidatePage();"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:Label ID="lblSearchDetails" runat="server"></asp:Label>
                        <asp:GridView ID="grdDepartment" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found...."
                            DataKeyNames="intKey" CssClass="table table-bordered table-hover" OnRowDataBound="grdDepartment_RowDataBound"
                            ShowFooter="true" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hypDepartment" runat="server" Text='<%#Eval("strDeptName")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Balance" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%#Eval("intCarryFwdPending")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Application Received" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("intTotalApplication")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("intTotalApproved")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rejected" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("intTotalRejected")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. of Application with Query" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%#Eval("intTotalQueryRaised")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Application pending in current period" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%#Eval("intTotalPending")%>
                                    </ItemTemplate>                                     
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Application Pending" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("intAllTotalPending")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application Exceeds ORTPSA Timeline" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("intTotalORTPSAtimelinePassed")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mean" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("intAvgDaysApproval")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="4%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deferred" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%#Eval("intTotalDeferred")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Forwarded" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <%#Eval("intTotalForwarded")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Median" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("decMedian")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ORTPSA Timeline" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORTPSTimeline" runat="server" Text='<%#Eval("intORTPSAtimeline")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Minimum Days Taken for Approval" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("intMinApprovalDays")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Maximum Days Taken for Approval" FooterStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%#Eval("intMaxApprovalDays")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="divLastUpdate" style="text-align:left;color:black" runat="server"></div>
                    
                    <div id="divUpdateText" style="text-align:left;color:black" runat="server">The dashboard information is updated on a daily basis</div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
