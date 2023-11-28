<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ProposalDetails.aspx.cs" Inherits="Proposal_ProposalDetails" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

        function valid() {

            if ($("#ContentPlaceHolder1_hdnNoofrecord").val() == "0") {
                if ($("#ContentPlaceHolder1_txtq1").val() == "") {
                    jAlert('<strong>Initial Query can not be left blank!</strong>', projname);
                    $("#ContentPlaceHolder1_txtq1").focus();
                    return false;
                }
            }
            if ($("#ContentPlaceHolder1_hdnNoofrecord").val() == "2") {
                if ($("#ContentPlaceHolder1_txtq2").val() == "") {
                    jAlert('<strong>Second Set Of Query can not be left blank!</strong>', projname);
                    $("#ContentPlaceHolder1_txtq2").focus();
                    return false;
                }
            }
        }

        function setvalue() {
            $('#charsLeft').html(1000 - $('#ContentPlaceHolder1_txtq1').val().length);
        }

        function setvalue1() {
            $('#charsLeft1').html(1000 - $('#ContentPlaceHolder1_txtq2').val().length);
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
            padding: 6px 0px;
            color: #d9534f;
            border-bottom: 1px solid #dcdcdc;
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
            background: #eff7ff;
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
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Proposal Details</h1>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12 " data-lobipanel-child-inner-id="CgbyYkSXVZ">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading noprint">
                            <div class="dropdown">
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <%--  <li><a  data-tooltip="Save as Pdf" data-toggle="tooltip" data-title="Save as Pdf" ><i class="panel-control-icon fa fa-file-pdf-o"></i></a></li>--%>
                                    <li><a class="PrintBtn" data-tooltip="Print" data-toggle="tooltip" data-title="Print">
                                        <i class="panel-control-icon fa fa-print"></i></a></li>
                                    <li><a href="javascript:history.back()" data-tooltip="Back" data-toggle="tooltip"
                                        data-title="Back"><i class="panel-control-icon fa  fa-chevron-circle-left"></i></a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                <%--Header Information--%>
                                <div class="panel panel-default ">
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-2">
                                                    Proposal No.
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Proposal_No" CssClass="datalabel" runat="server"></asp:Label>
                                                </div>
                                                <label for="Iname" class="col-sm-2">
                                                    Date of Application
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Application_Date" CssClass="datalabel" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Company Information--%>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingOne">
                                        <h4 class="panel-title">
                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                                aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa fa-minus"></i>Company Information</a>
                                        </h4>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-3">
                                                        Name of the Company/Enterprise M/s
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblCompName" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Corporate Office Address</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-2">
                                                        Address
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblAddress" CssClass="datalabel" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Country" class="col-sm-2">
                                                        Country
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblCountry" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="State" class="col-sm-2">
                                                        State
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblState" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Address2" class="col-sm-2">
                                                        City
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblCity" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="MobileNo" class="col-sm-2">
                                                        Phone Number
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblISDPHNo" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:Label ID="lblPhoneStateCode" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:Label ID="lblPhoneNo" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="FaxNo" class="col-sm-2">
                                                        Fax Number
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblISDFXNo" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:Label ID="lblFaxNo" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Pin Code" class="col-sm-2">
                                                        PIN Code
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblPin" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Email" class="col-sm-2">
                                                        Email Address
                                                    </label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblEmail" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Correspondence Address
                                                        </h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-2">
                                                        Name of the Contact Person
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblContactPerson" CssClass="datalabel" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-2">
                                                        Address
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblCorAdd" CssClass="datalabel" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">

                                                    <label for="Country" class="col-sm-2">
                                                        Country
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblCorCountry" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="State" class="col-sm-2">
                                                        State
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblCorState" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Address2" class="col-sm-2">
                                                        City
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblCorCity" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="MobileNo" class="col-sm-2">
                                                        Mobile Number
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblISDMOB" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCorMobileNo" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="FaxNo" class="col-sm-2">
                                                        Fax Number
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblFaxCordet" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCorFaxNo" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Pin Code" class="col-sm-2">
                                                        PIN Code
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblCorrPin" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Email" class="col-sm-2">
                                                        Email Address
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblCorEmail" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="MobileNo" class="col-sm-2">
                                                        Constitution of Company/Enterprise
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblOtheConstituition" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <div id="dvConstothers" runat="server">
                                                        <label for="FaxNo" class="col-sm-2">
                                                            Others (Please specify)
                                                        </label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblOthers" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvPromoter" runat="server">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <h4 class="h4-header">Promoter Details</h4>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Partner" class="col-sm-2">
                                                            Name of promoter
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblNameOfPromoter" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvPartnership" runat="server">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <h4 class="h4-header">Partnership Details</h4>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Partner" class="col-sm-2">
                                                            Number of partner(s)</label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblNumberOfPartner" runat="server" CssClass="datalabel"></asp:Label>
                                                        </div>
                                                        <label for="ManagingPartner" class="col-sm-2">
                                                            Name of the managing partner</label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblManagPartner" runat="server" CssClass="datalabel"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvBoard" runat="server">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <h4 class="h4-header">Board of Directors</h4>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:GridView ID="GrdDesignation" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                                                EmptyDataText="No records found">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="vchName" HeaderText="Name" />
                                                                    <asp:BoundField DataField="vchDesignation" HeaderText="Designation" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvPrjMSME" runat="server">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-2">
                                                        Educational qualification</label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <label id="lblLinkEdu" runat="server" visible="false">
                                                        <asp:Label ID="lblEduQualif" runat="server" CssClass="datalabel"></asp:Label>
                                                        <asp:HiddenField ID="hdnQ" runat="server" />
                                                        <asp:HiddenField ID="hdnEdu" runat="server" />
                                                        <asp:HyperLink ID="hplnkEdu" runat="server" Target="_blank">
                                                   <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                            </label>
                                                        <label id="lblEduLabel" runat="server">-NA-</label>
                                                            
                                                    </div>
                                                    <label for="Iname" class="col-sm-2">
                                                        Technical qualification</label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <label id="lblLinkTechq" runat="server" visible="false">
                                                        <asp:Label ID="lblTechQual" runat="server" CssClass="datalabel"></asp:Label>
                                                        <asp:HiddenField ID="hdnTechQ" runat="server" />
                                                        <asp:HiddenField ID="hdnTecnical" runat="server" />
                                                        <asp:HyperLink ID="hplnkTechQ" runat="server" Target="_blank">
                                                    <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                             </label>
                                                    <label id="lblTechqLabel" runat="server">-NA-</label>
                                                    </div>
                                                    <label for="Iname" class="col-sm-2">
                                                        Experience in years</label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <label id ="lblLinkExperience" runat="server" visible="false">
                                                        <asp:Label ID="lblExperience" runat="server" CssClass="datalabel"></asp:Label>
                                                        <asp:HiddenField ID="hdnExperience" runat="server" />
                                                        <asp:HyperLink ID="hplnkExperience" runat="server" Target="_blank">
                                                   <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                            </label>
                                                    <label id="lblExperienceLabel" runat="server">-NA-</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Entrepreneur Registration Details</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div id="DVC1" runat="server">
                                                        <label for="Iname" class="col-sm-2">
                                                            Year of Establishment
                                                        </label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblYearIncorp" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div id="DVC2" runat="server">
                                                        <label for="Iname" class="col-sm-2">
                                                            Place of incorporation
                                                        </label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblPlaceIncor" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <label for="Iname" class="col-sm-2">
                                                        GSTIN
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                         <asp:Label ID="lblGSTIN" CssClass="datalabel" runat="server"></asp:Label>
                                                         <label id="lblGstLink" runat="server" visible="false">                                                      
                                                        <asp:HiddenField ID="hdnGstinFile" runat="server" />
                                                        <asp:HyperLink ID="hplnkGstin" CssClass="datalabel" runat="server" Target="_blank">
                                                <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                             </label>
                                                        <label id="lblGstLabel" runat="server">-NA-</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Country" class="col-sm-2">
                                                        PAN
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                         <label id="lblPanLink" runat="server" visible="false">
                                                        <asp:HiddenField ID="hdnPanFile" runat="server" />
                                                        <asp:HyperLink ID="hplnkPan" CssClass="datalabel" runat="server" Target="_blank">
                                                  <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                             </label>
                                                         <label id="lblpanLabel" runat="server">
                                                        -NA-
                                                    </label>
                                                    </div>
                                                    <label for="State" class="col-sm-2">
                                                        Memorandum & Articles of Association
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <div id="DVC3" runat="server">
                                                            <label id="lblMemoLink" runat="server" visible="false" >
                                                            <asp:HiddenField ID="hdnMemoFile" runat="server" />
                                                            <asp:HyperLink ID="hplnkMemo" CssClass="datalabel" runat="server" Target="_blank">
                                              <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>
                                                                </label>                                  
                                                        </div>
                                                        <label id="lblMemoLabel" runat="server">-NA-</label>
                                                    </div>
                                                    <label for="Address2" class="col-sm-2">
                                                        Certificate of incorporation /Registration/Partnership Deed
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <div id="DVC4" runat="server">
                                                             <label id="lblLinkCert" runat="server" visible="false">
                                                            <asp:HiddenField ID="hdnCerti" runat="server" />
                                                            <asp:HyperLink ID="hplnkCerti" CssClass="datalabel" runat="server" Target="_blank">
                                                 <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>  
                                                                 </label>
                                                        </div>
                                                    <label id="lblCertLabel" runat="server">-NA-</label>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Pin Code" class="col-sm-2">
                                                        Project Type
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblProjType" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnProjType" runat="server" />
                                                    </div>
                                                    <label for="Pin Code" class="col-sm-2">
                                                        Application For
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblApplicationFor" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnApplicationFor" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <%-- Financial Status --%>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Financial Status (INR in Lakhs)</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                            <tbody>
                                                                <tr>
                                                                    <th></th>
                                                                    <th align="center">
                                                                        <asp:Label ID="lblFinYear1" runat="server" Text="Financial Year 1"></asp:Label>
                                                                        <asp:HiddenField ID="hdnFinYear1" runat="server" />
                                                                    </th>
                                                                    <th align="center">
                                                                        <asp:Label ID="lblFinYear2" runat="server" Text="Financial Year 2"></asp:Label>
                                                                        <asp:HiddenField ID="hdnFinYear2" runat="server" />
                                                                    </th>
                                                                    <th align="center">
                                                                        <asp:Label ID="lblFinYear3" runat="server" Text="Financial Year 3"></asp:Label>
                                                                        <asp:HiddenField ID="hdnFinYear3" runat="server" />
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td >Annual Turn Over
                                                                         
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblAnnlCrntYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblAnnlLastYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblAnnlPrevToLastYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Profit After Tax
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblPftBTCrntYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblPftBTLastYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblPftBTPrevToLastYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr id="lblResSur" runat="server">
                                                                    <td>Reserve and Surplus
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblRSCrntyr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblRSLastYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblRSPrevTolastYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr id="lblShaCap" runat="server">
                                                                    <td>Share capital
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblSCCrntYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblSCLastYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblSCPrevToLastYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Net Worth
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblNWCrntYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblNWLastYr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblNWPrevTolastyr" runat="server" Text="Annual"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="AnnualReport" class="col-sm-6">
                                                     <%--   Audited Financial Statements for First Year(financial statements, profit/loss accounts,
                                                        balance sheet)--%>
                                                         <asp:Label ID="lblf1" runat="server"></asp:Label>
                                                         
                                                    </label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                         <label id="lblLinkAudit" runat="server" visible="false">
                                                        <asp:HiddenField ID="hdnAudit" runat="server" />
                                                        <asp:HyperLink ID="hplnkAudit" CssClass="datalabel" runat="server" Target="_blank">
                                                <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                              </label>
                                                    <label id="lblAuditLabel" runat="server">-NA-</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="AnnualReport" class="col-sm-6">
                                                      <%--  Audited Financial Statements for Second Year(financial statements, profit/loss accounts,
                                                        balance sheet)--%>
                                                         <asp:Label ID="lblf2" runat="server"></asp:Label>
                                                    </label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                        <label id ="lblLinkFySecond" runat="server" visible="false">
                                                        <asp:HiddenField ID="hdnFySecond" runat="server" />
                                                        <asp:HyperLink ID="hplnkFySecond" CssClass="datalabel" runat="server" Target="_blank">
                                                <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                            </label>
                                                    <label id="lblFySecondLabel" runat="server">-NA-</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="AnnualReport" class="col-sm-6">
                                                      <%--  Audited Financial Statements for Third Year(financial statements, profit/loss accounts,
                                                        balance sheet)--%>
                                                          <asp:Label ID="lblf3" runat="server"></asp:Label>
                                                    </label>
                                                    <div class="col-sm-3">
                                                        <span class="colon">:</span>
                                                         <label id ="lblLinkFyThird" runat="server" visible="false">
                                                        <asp:HiddenField ID="hdnFyThird" runat="server" />
                                                        <asp:HyperLink ID="hplnkFyThird" runat="server" CssClass="datalabel" Target="_blank">
                                                <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                              </label>
                                                    <label id="lblFyThirdLabel" runat="server">-NA-</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div id="dvNetWorth" runat="server">
                                                        <label for="NetWorth" class="col-sm-6">
                                                           <%-- Net worth certified by CA--%>
                                                             <asp:Label ID="lblf4" runat="server"></asp:Label>
                                                        </label>
                                                        <div class="col-sm-3">
                                                            <span class="colon">:</span>
                                                             <label id="lblLinkNetWorth" runat="server" visible="false">
                                                            <asp:HiddenField ID="hdnNetWorth" runat="server" />
                                                            <asp:HyperLink ID="hplnkNetWorth" runat="server" CssClass="datalabel" Target="_blank">
                                               <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>
                                                                 </label>
                                                         <label id="lblNetWorthLabel" runat="server">-NA-</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvExistingUnit" runat="server">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <h4 class="h4-header">Existing industry details</h4>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="PhoneNo" class="col-sm-2">
                                                            Existing industry name
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblExistInd" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="PhoneNo" class="col-sm-2">
                                                            District
                                                        </label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblDistrictName" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                        <label for="PhoneNo" class="col-sm-2">
                                                            Block
                                                        </label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblBlock" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                        <label for="PhoneNo" class="col-sm-2">
                                                            Whether land allotted by IDCO
                                                        </label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblLandAllotIDCO" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-2">
                                                            Extent of land(in acres)
                                                        </label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblExtentLand" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                        <label for="PhoneNo" class="col-sm-2">
                                                            Nature of activity
                                                        </label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblNatActivity" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                        <label for="Iname" class="col-sm-2">
                                                            Sector
                                                        </label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblSector" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-2">
                                                            Sub Sector
                                                        </label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblSubSector" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                        <label for="Iname" class="col-sm-2">
                                                            Capacity
                                                        </label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblCapacity" runat="server" Text="1" CssClass="datalabel"></asp:Label>
                                                            <asp:Label ID="lblUnitCapacity" runat="server" CssClass="datalabel"></asp:Label>
                                                        </div>
                                                        <div id="dvOthrs" runat="server">
                                                            <label for="PhoneNo" class="col-sm-2">
                                                                Others (Please specify)
                                                            </label>
                                                            <div class="col-sm-2">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lblOtherspecify" runat="server" CssClass="datalabel"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvmaterial" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Raw Material for Production</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="">
                                                            <asp:GridView ID="grdRawMaterials" class="table table-bordered table-hover" runat="server"
                                                                AutoGenerateColumns="false" Width="100%" EmptyDataText="No Record(s) Found...">
                                                                <Columns>
                                                                    <asp:BoundField DataField="vchRawMaterial" HeaderText="Raw material for production" />
                                                                    <asp:BoundField DataField="vchRawMeterialSrc" HeaderText="Material source" />
                                                                </Columns>
                                                            </asp:GridView>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Project Information--%>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingThree">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#IndustryInformation" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa fa-plus"></i>Project Information </a>
                                        </h4>
                                    </div>
                                    <div id="IndustryInformation" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Time" class="col-sm-2">
                                                        Name of the Unit
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblUnit" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Time" class="col-sm-2">
                                                        <asp:Label ID="lblEin" runat="server"></asp:Label>
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:HiddenField ID="hdnEin" runat="server" />
                                                        <asp:Label ID="lblEINIEM" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="CapitalInvestment" class="col-sm-2">
                                                        Is the project coming under Priority Sector
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblProjectComing" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnProjComing" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Time" class="col-sm-2">
                                                        Sector of activity
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblSectorActivity" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Time" class="col-sm-2">
                                                        Sub sector
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblSubSect" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Time" class="col-sm-2" style="display: none;">
                                                        Proposed Annual Capacity
                                                    </label>
                                                    <div class="col-sm-2" runat="server" visible="false">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblPropAnnual" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnAnnualUnit" runat="server" />
                                                        <asp:Label ID="lblAnnualUnit" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:GridView ID="grdProduct" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="vchProductName" HeaderText="Product name" />
                                                                <asp:BoundField DataField="vchProposedAnnualCapacity" HeaderText="Proposed annual capacity" />
                                                                <asp:BoundField DataField="vchProposedUnit" HeaderText="Unit" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Proposed Capital Investment (INR in Lakhs)</h4>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="PM" class="col-sm-4">
                                                        Land Including Land Development
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblLandincludinglanddevelopment" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="PM" class="col-sm-4">
                                                        Building & Civil Construction
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblBuildingCivilConstruction" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="PM" class="col-sm-4">
                                                        Plant & Machinery
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblPlantMachinery" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Others" class="col-sm-4">
                                                        Others
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblOthersProj" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="CapitalInvestment" class="col-sm-4">
                                                        Total Capital Investment
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblCapitalInvestment" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Type" class="col-sm-4">
                                                        Pollution Category
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblPolCat" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnPolCat" runat="server" />
                                                    </div>
                                                    <label for="Type" class="col-sm-4">
                                                        Period to commence commercial production (in months)
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblPerCommence" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Project Implementation Schedule</h4>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                            <tbody>
                                                                <tr>
                                                                    <th>Activities
                                                                    </th>
                                                                    <th>Months (Zero date starts from acquisition /allotment of land)
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>Ground Breaking
                                                                    </td>
                                                                    <td class="text-left">
                                                                        <asp:Label ID="lblGround" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Civil and Structural Completion
                                                                    </td>
                                                                    <td class="text-left">
                                                                        <asp:Label ID="lblCivilstructural" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Major Equipment Erection
                                                                    </td>
                                                                    <td class="text-left">
                                                                        <asp:Label ID="lblEquipment" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Start of Commercial Production
                                                                    </td>
                                                                    <td class="text-left">
                                                                        <asp:Label ID="lblCommercial" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <label id="lblLinkEIM" runat="server" visible="false">
                                                        <asp:Label ID="lblEIM" runat="server"></asp:Label>:
                                                        <asp:HiddenField ID="hdnIndustryEntMemorandum" runat="server" />
                                                        <asp:HyperLink ID="hplnkIndustryEntMemorandum" runat="server" Target="_blank">
                                        <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                             </label>
                                                        <label id="lblEimLabel" runat="server">-NA-</label>
                                                    </div>
                                                    <div class="col-sm-3" style="display: none">
                                                        <label for="Plan">
                                                            Manufacturing process flow :</label>
                                                         <label id="lblLinkFileMnfprocess" runat="server">
                                                        <asp:HiddenField ID="hdnFileMnfprocess" runat="server" />
                                                        <asp:HyperLink ID="hplnkFileMnfprocess" runat="server" Target="_blank">
                                        <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                             </label>
                                                        <label id="lblFileMnfprocessLabel" runat="server">-NA-</label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label for="Plan">
                                                            Feasibility Report :</label>
                                                         <label id="lblLinkFeasibilityReport" runat="server" visible="false">
                                                        <asp:HiddenField ID="hdnFeasibilityReport" runat="server" />
                                                        <asp:HyperLink ID="hplnkFeasibilityReport" runat="server" Target="_blank">
                                        <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                             </label>
                                                         <label id="lblFeasibilityReportLabel" runat="server">-NA-</label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="Plan">
                                                            Board resolution to take up the project :</label>
                                                         <label id="lblLinkBoardResolution" runat="server" visible="false">
                                                        <asp:HiddenField ID="hdnBoardResolution" runat="server" />
                                                        <asp:HyperLink ID="hplnkBoardResolution" runat="server" Target="_blank">
                                        <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                             </label>
                                                        <label id="lblBoardResolutionLabel" runat="server">-NA-</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Means of Finance for Capital Investment (INR in Lakh)
                                                    </h4>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Capacity Present" class="col-sm-4">
                                                        Equity Contribution
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblEquity" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Existing Business Intrest" class="col-sm-4">
                                                        Bank/Institutional Finance
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblBankFin" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Existing Business Intrest" class="col-sm-4">
                                                        Total
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblTotal" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="ProdInd" class="col-sm-4">
                                                        Foreign Direct Investment (if any)
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblFDI" runat="server" CssClass="datalabel" Text="NA"></asp:Label>
                                                    </div>
                                                    <label for="ProdInd" class="col-sm-4">
                                                        In case of any other source of finance, please upload relevant document
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                          <label id="lblLinkOtherFin" runat="server" visible="false">
                                                        <asp:HiddenField ID="hdnOtherFin" runat="server" />
                                                        <asp:HyperLink ID="hplnkOtherFin" CssClass="datalabel" runat="server" Target="_blank">
                                       <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                              </label>
                                                         <label id="lblOtherFinLabel" runat="server">-NA-</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                </div>
                                            </div>
                                            <div id="dvIRR" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">For IRR &amp; DSCR (INR in Lakh)</h4>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <label for="ProdInd">
                                                                IRR :</label>
                                                            <asp:Label ID="lblIRR" runat="server" Text="NA" CssClass="datalabel"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <label for="ProdInd">
                                                                DSCR :</label>
                                                            <asp:Label ID="lblDSCR" runat="server" CssClass="datalabel"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divGroupOfCompany" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Group of Company Details</h4>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <asp:GridView ID="Grd_GC_Net_Worth" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                                            OnRowDataBound="Grd_GC_Net_Worth_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SlNo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_GC_SlNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="5%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Company Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_GC_Company_Name_G" runat="server" Text='<%# Eval("vchCompanyName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Net worth of last financial year (INR in Lakh)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_GC_Net_Worth_G" runat="server" Text='<%# Eval("decNetWorth") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="28%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Document related to net worth">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="Hyp_View_GC_Doc" runat="server" Target="_blank" ToolTip="Click here to view document."><i class="fa fa-cloud-download"></i></asp:HyperLink>
                                                                        <asp:HiddenField ID="Hid_GC_Net_Worth_File_Name_G" runat="server" Value='<%# Eval("vchNetWorthDoc") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="30%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Employment</h4>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                            <tbody>
                                                                <tr>
                                                                    <th></th>
                                                                    <th align="center">Existing
                                                                    </th>
                                                                    <th align="center">Proposed
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>Managerial
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblManagerExist" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblManagerProposed" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Supervisory
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblSupervisorExist" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblSupervisorProposed" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Skilled
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblSkilledExist" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblSkilledProposed" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Semi skilled
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblSemiskilledExist" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblSemiskilledProposed" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Un skilled
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblUnskilledExist" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblUnskilledProposed" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Total employment
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblTotalExist" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblTotalProposed" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="ProdInd" class="col-sm-4">
                                                        Proposed direct employment (On Company Payroll)
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblDirectEmp" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="ProdInd" class="col-sm-4">
                                                        Proposed contractual employment
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblContractualEmp" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Projects at Other Locations</h4>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="ProdInd" class="col-sm-4">
                                                        Does the company have projects at other locations in India?
                                                    </label>
                                                    <div class="col-sm-7">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblProjectsLocation" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvLocDetIndia" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Location Details</h4>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:GridView ID="gvLOCDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="vchUnitName" HeaderText="Unit Name" />
                                                                    <asp:BoundField DataField="vchProduct" HeaderText="Product" />
                                                                    <asp:BoundField DataField="vchTotCapacity" HeaderText="Total capacity(With unit)" />
                                                                    <asp:BoundField DataField="vchStateName" HeaderText="State" />
                                                                    <asp:BoundField DataField="vchDistName" HeaderText="District" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="ProdInd" class="col-sm-4">
                                                        Is there any unit outside India</label>
                                                    <div class="col-sm-7">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblUnitOutSide" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvLocDetOutInd" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Location Details (Top Five as per Turnover)</h4>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:GridView ID="gvOtherUnits" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="vchUnitName" HeaderText="Unit Name" />
                                                                    <asp:BoundField DataField="vchProduct" HeaderText="Product" />
                                                                    <asp:BoundField DataField="vchTotCapacity" HeaderText="Total capacity(With unit)" />
                                                                    <asp:BoundField DataField="vchCountryName" HeaderText="Country" />
                                                                    <asp:BoundField DataField="vchCityName" HeaderText="City" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Land and Utility details--%>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div1">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#ProposedSiteDetails" aria-expanded="false" aria-controls="collapseThree">
                                                <i class="more-less fa fa-plus"></i>Land and Utility Requirement </a>
                                        </h4>
                                    </div>
                                    <div id="ProposedSiteDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Proposed Location of Land</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Name" class="col-sm-2">
                                                        Land required from government
                                                    </label>
                                                    <div class="col-sm-9">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblLandRequired" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnLandReq" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="area" class="col-sm-2">
                                                        District
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblDistrictLand" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="varea" class="col-sm-2">
                                                        Block
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblBlockLand" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="varea" class="col-sm-2">
                                                        Extent of land required (In Acre)
                                                        <%--<asp:Label ID="LblLandUnit" runat="server" Font-Bold="true"></asp:Label>--%>
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblExtentLandReq" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvLandReq" runat="server">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-2">
                                                            <label for="area">
                                                                Whether land is required in IDCO industrial estate</label>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblLandrequiredIDCO" runat="server" CssClass="datalabel"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="dvIDCOName" runat="server">
                                                    <div class="row">
                                                        <label for="varea" class="col-sm-2">
                                                            Name of the IDCO industrial estate</label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblIDCOName" runat="server" CssClass="datalabel"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="dvLandAcquired" runat="server">
                                                    <div class="row">
                                                        <label for="varea" class="col-sm-2">
                                                            Whether land to be acquired by IDCO</label>
                                                        <div class="col-sm-2">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lbllandacquired" runat="server" CssClass="datalabel"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvL1" runat="server">
                                                <div class="row">
                                                    <label for="varea" class="col-sm-2">
                                                        Project Land Use Statement
                                                    </label>
                                                    <div class="col-sm-7">
                                                        <span class="colon">:</span><label>
                                                             <label id="lblLinkProjectlandStatement" runat="server" visible="false">
                                                            <asp:HiddenField ID="hdnLandUsestmt" runat="server" />
                                                            <asp:HyperLink ID="hypProjectlandStatement" runat="server" Target="_blank">
                                                   <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>
                                                                  </label>
                                                    <label id="lblProjectlandStatementLablel" runat="server">-NA-</label>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvL2" runat="server">
                                                <div class="row">
                                                    <label class="col-sm-2">
                                                        Project Layout Plan
                                                    </label>
                                                    <div class="col-sm-7">
                                                        <span class="colon">:</span><label>
                                                            <label id="lblLinkProjectLaoutPlan" runat="server" visible="false">
                                                            <asp:HiddenField ID="hdnLayOutPln" runat="server" />
                                                            <asp:HyperLink ID="hypProjectLaoutPlan" runat="server" Target="_blank">
                                                   <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>
                                                                 </label>
                                                    <lable id="lblProjectLaoutPlanLabel" runat="server">-NA-</lable>

                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Power Requirement During Production</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-3">
                                                        Sources Of Regular Connection</label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="CheckBox2" class="col-sm-1">
                                                        GRID</label>
                                                    <label for="Capacity" class="col-sm-3">
                                                        Power demand from GRID (in KW)
                                                    </label>
                                                    <div class="col-sm-1">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblLoadGrid" runat="server" CssClass="datalabel"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="CheckBox2" class="col-sm-1">
                                                        CPP</label>
                                                    <label for="Capacity" class="col-sm-3">
                                                        Power drawal from CPP (in KW)</label>
                                                    <div class="col-sm-1">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblLoadCPP" runat="server" CssClass="datalabel"></asp:Label>
                                                    </div>
                                                    <label for="ICapacity" class="col-sm-3">
                                                        Capacity of the CPP Plant (in KW)
                                                    </label>
                                                    <div class="col-sm-1">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblCPPCapacity" runat="server" CssClass="datalabel"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="CheckBox2" class="col-sm-1">
                                                        IPP</label>

                                                    <label for="Capacity" class="col-sm-3">
                                                        Independent Power Producer (in KW)
                                                    </label>




                                                    <div class="col-sm-1">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="LblPowerDemandIPP" runat="server" CssClass="datalabel"></asp:Label>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Water Requirement</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered">
                                                            <tbody>
                                                                <tr>
                                                                    <th></th>
                                                                    <th align="center">Existing
                                                                    </th>
                                                                    <th align="center">Proposed
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>Total water requirement(in cusec)
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblWaterRequireExist" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblWaterReqireProposed" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Type3" class="col-sm-4">
                                                        Water required for production(in cusec)
                                                    </label>
                                                    <div class="col-sm-1">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblWaterReq" CssClass="datalabel" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Type2" class="col-sm-3">
                                                        Sources of water for Production
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblSurface" CssClass="datalabel" runat="server"></asp:Label>
                                                        <%--  <asp:Label ID="lblIdcoSupply" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:Label ID="lblRainWtrHarvesting" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:Label ID="lblother" CssClass="datalabel" runat="server"></asp:Label>
                                                        <asp:Label ID="lblsourceOther" CssClass="datalabel" runat="server"></asp:Label>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">Waste Water Management</h4>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="form-group">
                                            <div class="row">
                                                <label for="area" class="col-sm-5">
                                                    Quantum of recycling of waste water (in cusec)
                                                </label>
                                                <div class="col-sm-7">
                                                    <span class="colon">:</span><label>
                                                        <asp:Label ID="lblQuantum" runat="server"></asp:Label></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="varea" class="col-sm-5">
                                                    Waste conservation measures
                                                </label>
                                                <div class="col-sm-7">
                                                    <span class="colon">:</span>
                                                    <label id="lblLinkWaterFile" runat="server" visible="false">
                                                        <asp:HiddenField ID="hdnWaterFile" runat="server" />
                                                        <asp:HyperLink ID="hplnkWaterFile" runat="server" Target="_blank">
                                                            <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                    <label id="lblWaterFileLabel" runat="server">-NA-</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-5">
                                                    Waste water treatment technology and management of solid/hazardous waste
                                                </label>
                                                <div class="col-sm-7">
                                                    <span class="colon">:</span>
                                                    <label id="lblLinkHazardousFile" runat="server" visible="false">
                                                        <asp:HiddenField ID="hdnHazardousFile" runat="server" />
                                                        <asp:HyperLink ID="hplnkHazardousFile" runat="server" Target="_blank">
                                                            <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                     <label id="lblHazardousFileLabel" runat="server">-NA-</label>
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Query details--%>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div2">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#QueryDet" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus"></i>Query Details </a>
                                        </h4>
                                    </div>
                                    <div id="QueryDet" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="form-body">
                                                            <asp:GridView ID="grdQuery" class="table table-bordered table-hover" runat="server"
                                                                DataKeyNames="strFileName" AutoGenerateColumns="false" Width="100%" EmptyDataText="No Record(s) Found..."
                                                                OnRowDataBound="grdQuery_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl#">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="strQuerytype" HeaderText="Query Type" />
                                                                    <asp:BoundField DataField="strRemarks" HeaderText="Remarks" />
                                                                    <asp:TemplateField HeaderText="File Name">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnFile" runat="server" />
                                                                            <%--   <asp:HyperLink ID="hlQuery" runat="server" Target="_blank">
                                                                           <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                                            </asp:HyperLink>--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Action Details--%>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div3">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#ActionDet" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus"></i>Action Details</a>
                                        </h4>
                                    </div>
                                    <div id="ActionDet" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="form-body">
                                                            <asp:GridView ID="grdAction" class="table table-bordered table-hover" runat="server"
                                                                DataKeyNames="strFileName" AutoGenerateColumns="false" Width="100%" EmptyDataText="No Record(s) Found..."
                                                                OnRowDataBound="grdAction_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl#">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Reference File">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFile" runat="server"></asp:Label>
                                                                            <asp:HiddenField ID="hdnFileName" runat="server" Value='<%#Eval("strFileName") %>' />
                                                                            <asp:HyperLink ID="hprlnkQuery1" runat="server" class="fa fa-download" aria-hidden="true" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Certificate">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnFileCert" runat="server" Value='<%#Eval("strPEALCertificate") %>' />
                                                                            <asp:HyperLink ID="hprlnkQuery2" runat="server" class="fa fa-download" aria-hidden="true" />
                                                                            <asp:Label ID="lblCert" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Score Card">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnScoreCard" runat="server" Value='<%#Eval("strScorecard") %>' />
                                                                            <asp:HyperLink ID="hprlnkScoreCard" runat="server" class="fa fa-download" aria-hidden="true" />
                                                                            <asp:Label ID="lblScoreCard" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="strStatus" HeaderText="Status" />
                                                                    <asp:BoundField DataField="strRemarks" HeaderText="Remarks" NullDisplayText="NA" />
                                                                    <asp:BoundField DataField="strUpdatedOn" HeaderText="Date" />
                                                                </Columns>
                                                            </asp:GridView>
                                                            <div class="clearfix">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Land Recommendation  Details--%>

                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="Div4">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                href="#DivLandRecom" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus"></i>Land Recommendation Details</a>
                                        </h4>
                                    </div>

                                    <div id="DivLandRecom" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                        <div runat="server" id="DivLand">
                                            <div class="panel-body">
                                                <div class="form-body">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Name" class="col-sm-3">
                                                                Land required from government
                                                            </label>
                                                            <div class="col-sm-3">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="LblLandRequr" CssClass="datalabel" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="HdnLandRequr" runat="server" />
                                                            </div>
                                                            <label for="varea" class="col-sm-3">
                                                                Extent of land required (In Acre)                                                       
                                                            </label>
                                                            <div class="col-sm-2">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="LblLandRequAcre" CssClass="datalabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Name" class="col-sm-3">
                                                                Land Recommendation by SLFC (In Acre)
                                                            </label>
                                                            <div class="col-sm-3">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="LblLandRecoSLFC" CssClass="datalabel" runat="server"></asp:Label>

                                                            </div>
                                                            <label for="varea" class="col-sm-3">
                                                                Land Recommendation  Letter                                                      
                                                            </label>
                                                            <div class="col-sm-2">
                                                                <span class="colon">:</span>
                                                                <asp:HiddenField ID="HdnLandRecomDoc" runat="server" />
                                                                <asp:HyperLink ID="HplLandReacomDoc" CssClass="datalabel" runat="server" Target="_blank">
                                                                         <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                                </asp:HyperLink>
                                                                <asp:Label ID="LblLandRecom" CssClass="datalabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div runat="server" id="DivNoRecord" visible="false">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:Label ID="LblNoRecord" CssClass="datalabel" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- panel-group -->
                    </div>
                </div>
                <!-- panel-group -->
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {

            //$('.panel-title a').each(function() {
            //$(this).find('i').toggleClass('fa-minus fa-plus');    
            //});

            $('.panel-title a').click(function () {
                $(this).find('i').toggleClass('fa-minus fa-plus');
            });
        });
    </script>
</asp:Content>
