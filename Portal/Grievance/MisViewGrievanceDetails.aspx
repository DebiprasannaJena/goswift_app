<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MisViewGrievanceDetails.aspx.cs"
    Inherits="Portal_Grievance_MisViewGrievanceDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!-- Start Styles
         =====================================================================-->
    <link href="../../PortalCSS/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/flash.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/pe-icon-7-stroke.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/themify-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/emojionearea.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/monthly.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/jquery.timepicker.css" rel="stylesheet" type="text/css" />
    <!-- End Styles
         =====================================================================-->
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.timepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.panel-title a').click(function () {

                $(this).find('i').toggleClass('fa-minus fa-plus');
            });
        })
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

        function valid() {

            if ($("#hdnNoofrecord").val() == "0") {
                if ($("#txtq1").val() == "") {
                    jAlert('<strong>Initial Query can not be left blank!</strong>', projname);
                    $("#txtq1").focus();
                    return false;
                }
            }
            if ($("#hdnNoofrecord").val() == "2") {
                if ($("#txtq2").val() == "") {
                    jAlert('<strong>Second Set Of Query can not be left blank!</strong>', projname);
                    $("#txtq2").focus();
                    return false;
                }
            }
        }

        function setvalue() {

            $('#charsLeft').html(1000 - $('#txtq1').val().length);
        }
        function setvalue1() {

            $('#charsLeft1').html(1000 - $('#txtq2').val().length);
        }

    </script>
    <style type="text/css">
        .lobipanel .panel-heading .panel-title {
            max-width: calc(100% - 0px);
        }

        .panel .panel-heading h4 {
            float: none;
            font-size: 15px;
            font-weight: 600;
            text-transform: uppercase;
        }

        .panel-group .panel {
            border-radius: 0;
            box-shadow: none;
            border-color: #EEEEEE;
            box-shadow: 0px 0px 1px #b3b3b3;
        }

        .panel-default > .panel-heading {
            padding: 0;
            border-radius: 0;
            color: #212121;
            background-color: #FAFAFA;
            border-color: #EEEEEE;
        }

        .panel-title {
            font-size: 14px;
        }

            .panel-title > a {
                display: block;
                padding: 6px;
                text-decoration: none;
            }

        .more-less {
            float: right;
            color: #022f56;
            margin-top: 5px;
            margin-right: 8px;
            width: 20px;
            background: #dcdcdc;
            height: 20px;
            text-align: center;
            padding-top: 3px;
        }

        .panel-default > .panel-heading + .panel-collapse > .panel-body {
            border-top-color: #EEEEEE;
        }

        .table tr td a {
            color: #337ab7;
            font-size: 14px;
        }

            .table tr td a .fa {
                font-size: 15px;
                display: block;
                margin-bottom: 10px;
            }

        .datalabel {
            margin-top: 6px;
            display: inline-block;
            margin-bottom: 6px;
        }

        .form-group {
            margin-bottom: 4px;
        }

        label {
            font-weight: 500;
        }

        .h4-header {
            margin: 8px 0px 8px;
            font-size: 15px;
            font-weight: 600;
            background: #e4e4e4;
            padding: 6px 6px;
        }

        @media print {
            body {
                font-size: 13px;
            }

            .content-header, .main-footer, .back-top {
                display: none;
            }

            .dropdown {
                display: none;
            }

            .navbar-brand {
                float: left;
            }

            .row {
                margin: 0px;
            }

            .header-investorDetails {
                display: none;
            }

            .investrs-tab {
                display: none;
            }

            .col-sm-4 {
                width: 33%;
                float: left;
            }

            label {
                font-weight: 400;
            }

            .iconsdiv, .footer-wrapper {
                display: none;
            }

            .form-group {
                margin-bottom: 0px;
            }

            header {
                border-bottom: 1px solid #ccc;
            }

            .form-header {
                padding: 0px;
                border-bottom: 0px;
            }

            .form-body {
                padding: 10px 0px;
            }

            .form-sec h2 {
                font-weight: 400;
                color: #000;
                border-bottom: 0px;
                background: #ccc;
            }

            .form-sec {
                margin-bottom: 10px;
            }

            .col-sm-3 {
                width: 25%;
                float: left;
            }

            .col-sm-4 {
                width: 30%;
            }

            .col-sm-3 {
                width: 25%;
            }

            .col-sm-6 {
                width: 50%;
                float: left;
            }

            .collapse, collapsed {
                display: block;
                height: auto !important;
            }

            .more-less {
                display: none;
            }

            .navbar-toggle {
                display: none;
            }

            .scrollup {
                display: none;
            }

            .panel-title {
                font-weight: 400;
            }

            .panel-body .h4-header, .table > tbody > tr > th {
                font-weight: 400;
            }

            a[href]:after {
                content: none !important;
            }

            .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9 {
                float: left;
                padding: 0px;
                padding-right: 10px;
            }

            .col-sm-2 {
                width: 16%;
            }

            .col-sm-8 {
                width: 70%;
            }

            .col-sm-12 {
                width: 100%;
            }

            label span {
                font-weight: 500;
                font-size: 12px;
            }

            table {
                width: 100%;
            }

            label a .fa {
                margin-top: 3px;
            }

            .panel-title > a {
                padding: 0px 10px;
            }

            .noprint {
                display: none;
            }

            .lobipanel > .panel-body {
                padding: 0px;
            }

            .panel-body {
                padding: 10px;
            }

            .panel .panel-heading h4 {
                padding-left: 0px;
            }

            .panel-group .panel {
                border: 1px solid #eee;
                box-shadow: none;
            }

            .h4-header {
                padding: 0px;
                font-size: 14px;
                background-color: #000;
            }

            .back-top {
                display: none;
            }

            .content {
                padding: 4px 0px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-group">
            <div class="row" align="right" style="margin-right: 10px;" class="noprint">
                <%--  <li><a  data-tooltip="Save as Pdf" data-toggle="tooltip" data-title="Save as Pdf" ><i class="panel-control-icon fa fa-file-pdf-o"></i></a></li>--%>
                <%--<a class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print"><i
                class="panel-control-icon fa fa-print"></i></a>--%><a href="javascript:history.back()"
                    data-tooltip="Back" data-toggle="tooltip" data-title="Back"><i class="panel-control-icon fa  fa-chevron-circle-left">
                    </i></a>
            </div>
        </div>

         <div class="content">
        <div class="panel-body">
            <div class="form-group">
                <label class="col-sm-2">
                    Grievance Id</label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Griv_Id" runat="server" CssClass="form-control-static" Font-Bold="true"
                        ForeColor="Red"></asp:Label>
                </div>
                <label class="col-sm-2">
                    Apply Date</label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Apply_Date" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
            </div>

            <div class="form-group">
                <div class="row">
                    <div class="col-sm-12">
                        <h4 class="h4-header">Applicant Details
                        </h4>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <label class="col-sm-2">
                    Name of the Company</label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Company_Name" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                </div>
                <label class="col-sm-2">
                    Industry Type
                </label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Industry_Type" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Blue"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
            </div>

            <div class="form-group">
                <label class="col-sm-2">
                    Applicant Name</label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Applicant_Name" runat="server" CssClass="form-control-static"></asp:Label>
                </div>
                <label class="col-sm-2">
                    Designation</label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Designation" runat="server" CssClass="form-control-static"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2">
                    District
                </label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_District" runat="server" CssClass="form-control-static"></asp:Label>
                </div>
                <label class="col-sm-2">
                    Investment Level</label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Investment_Level" runat="server" CssClass="form-control-static"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2">
                    Mobile No</label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Mobile_No" runat="server" CssClass="form-control-static"></asp:Label>
                </div>
                <label class="col-sm-2">
                    Email Id
                </label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Email" runat="server" CssClass="form-control-static"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-sm-12">
                        <h4 class="h4-header">Grievance Details
                        </h4>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2">
                    Grievance Type</label>
                <div class="col-sm-10">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Griv_Type" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2">
                    Grievance Sub Type
                </label>
                <div class="col-sm-10">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Griv_Sub_Type" runat="server" CssClass="form-control-static"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2">
                    Grievance Title</label>
                <div class="col-sm-10">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Griv_Title" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2">
                    Grievance Details
                </label>
                <div class="col-sm-10">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Griv_Details" runat="server" CssClass="form-control-static"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2">
                    Attachment-1
                </label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:HyperLink ID="Hyp_Attachment_1" runat="server" Target="_blank" ToolTip="Click here to view document !"><i class="fa fa-download"></i></asp:HyperLink>
                    <asp:Label ID="Lbl_Doc_Attachment_1" runat="server" CssClass="form-control-static"
                        ForeColor="Red"></asp:Label>
                </div>
                <label class="col-sm-2">
                    Attachment-2
                </label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:HyperLink ID="Hyp_Attachment_2" runat="server" Target="_blank" ToolTip="Click here to view document !"><i class="fa fa-download"></i></asp:HyperLink>
                    <asp:Label ID="Lbl_Doc_Attachment_2" runat="server" CssClass="form-control-static"
                        ForeColor="Red"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-sm-12">
                        <h4 class="h4-header">Action Details
                        </h4>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2">
                    Current Status
                </label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Status" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                </div>
                <label class="col-sm-2">
                    Action Date
                </label>
                <div class="col-sm-4">
                    <span class="colon">:</span>
                    <asp:Label ID="Lbl_Action_Date" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover"
                        AutoGenerateColumns="False" Width="100%" EmptyDataText="No Action Taken Yet..."
                        OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grievance Id" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Griv_Id" runat="server" Text='<%# Eval("vchGrivId") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("vchStatusName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Remarks" runat="server" Text='<%# Eval("vchRemarks") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reference Doc" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HiddenField ID="Hid_Ref_Doc_Name" runat="server" Value='<%#Eval("vchRefDoc") %>' />
                                    <asp:HyperLink ID="Hyp_Ref_Doc" runat="server" class="fa fa-download" aria-hidden="true" />
                                    <asp:Label ID="Lbl_Ref_Doc" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action Taken By" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Action_By" runat="server" Text='<%# Eval("vchActionBy") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action Date" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Lbl_Action_Date" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clearfix">
                </div>
            </div>
             
        </div>
             </div>
    </form>
</body>
</html>
